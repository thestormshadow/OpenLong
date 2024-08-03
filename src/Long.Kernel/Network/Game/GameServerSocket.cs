using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Network.Game.Packets.TeamArena;
using Long.Kernel.Processors;
using Long.Kernel.Settings;
using Long.Kernel.States.User;
using Long.Network;
using Long.Network.Packets;
using Long.Network.Services;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Game
{
    public sealed class GameServerSocket : TcpServerListener<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<GameServerSocket>();

        public static GameServerSocket Instance { get; private set; }

        public GameServerSocket(GameServerSettings serverSettings)
            : base(serverSettings.Game.MaxOnlinePlayers,
                  exchange: true,
                  footer: NetworkDefinition.GAME_CLIENT_FOOTER,
                  readProcessors: serverSettings.Game.Listener.RecvProcessors,
                  writeProcessors: serverSettings.Game.Listener.SendProcessors)
        {
            if (Instance != null)
            {
                throw new ApplicationException("Cannot start server socket twice!");
            }

            Instance = this;
        }

        protected override async Task<GameClient> AcceptedAsync(Socket socket, Memory<byte> buffer)
        {
            uint readPartition = packetProcessor.SelectReadPartition();
            uint writePartition = packetProcessor.SelectWritePartition();
            var client = new GameClient(socket, buffer, readPartition, writePartition);

            await client.NdDiffieHellman.ComputePublicKeyAsync();

            await RandomnessService.Instance.NextBytesAsync(client.NdDiffieHellman.DecryptionIV);
            await RandomnessService.Instance.NextBytesAsync(client.NdDiffieHellman.EncryptionIV);

            var handshakeRequest = new MsgHandshake(
                client.NdDiffieHellman,
                client.NdDiffieHellman.EncryptionIV,
                client.NdDiffieHellman.DecryptionIV);

            await handshakeRequest.RandomizeAsync();
            await client.SendAsync(handshakeRequest);
            return client;
        }

        protected override bool Exchanged(GameClient actor, ReadOnlySpan<byte> buffer)
        {
            try
            {
                var msg = new MsgHandshake();
                msg.Decode(buffer.ToArray());
                actor.NdDiffieHellman.ComputePrivateKey(msg.ClientKey);
                actor.Cipher.GenerateKeys(actor.NdDiffieHellman.ProcessDHSecret(),
                    actor.NdDiffieHellman.EncryptionIV,
                    actor.NdDiffieHellman.DecryptionIV);
                actor.ReceiveTimeOutSeconds = 5; // somehow exchange is breaking after this sometimes
                actor.NdDiffieHellman = null;
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e, "Error on exchange {0}", e.Message);
            }
            return false;
        }

        protected override void Received(GameClient actor, ReadOnlySpan<byte> packet)
        {
            if (actor.ConnectionStage == TcpServerActor.Stage.Exchange)
            {
                actor.ReceiveTimeOutSeconds = 900;
                actor.ConnectionStage = TcpServerActor.Stage.Receiving;
            }
            base.Received(actor, packet);
        }

        protected override async Task ProcessAsync(GameClient actor, byte[] message)
        {
            // Validate connection
            if (!actor.Socket.Connected)
            {
                return;
            }

            // Read in TQ's binary header
            ushort length = BitConverter.ToUInt16(message, 0);
            PacketType type = (PacketType)BitConverter.ToUInt16(message, 2);

            try
            {
                bool handled = await OnNetworkMessageReceivedAsync(actor, type, message);
                MsgBase<GameClient> msg = null;
                switch (type)
                {
                    case PacketType.MsgConnect: msg = new MsgConnect(); break;
                    case PacketType.MsgAction: msg = new MsgAction(); break;
                    case PacketType.MsgWalk: msg = new MsgWalk(); break;
                    case PacketType.MsgRegister: msg = new MsgRegister(); break;
                    case PacketType.MsgItem: msg = new MsgItem(); break;
                    case PacketType.MsgTalk: msg = new MsgTalk(); break;
                    case PacketType.MsgName: msg = new MsgName(); break;
                    case PacketType.MsgLeaveWord: msg = new MsgLeaveWord(); break;
                    case PacketType.MsgPlayerAttribInfo: msg = new MsgPlayerAttribInfo(); break;
                    case PacketType.MsgEquipLock: msg = new MsgEquipLock(); break;
                    case PacketType.MsgMapItem: msg = new MsgMapItem(); break;
                    case PacketType.MsgSuperFlag: msg = new MsgSuperFlag(); break;
                    case PacketType.MsgPackage: msg = new MsgPackage(); break;
                    case PacketType.Msg2ndPsw: msg = new Msg2ndPsw(); break;
                    case PacketType.MsgNpc: msg = new MsgNpc(); break;
                    case PacketType.MsgNpcInfo: msg = new MsgNpcInfo(); break;
                    case PacketType.MsgTaskDialog: msg = new MsgTaskDialog(); break;
                    case PacketType.MsgInteract: msg = new MsgInteract(); break;
                    case PacketType.MsgDataArray: msg = new MsgDataArray(); break;
                    case PacketType.MsgInviteTrans: msg = new MsgInviteTrans(); break;
                    case PacketType.MsgMagicInfo: msg = new MsgMagicInfo(); break;
                    case PacketType.MsgGodExp: msg = new MsgGodExp(); break;
                    case PacketType.MsgGemEmbed: msg = new MsgGemEmbed(); break;
                    case PacketType.MsgItemStatus: msg = new MsgItemStatus(); break;
                    case PacketType.MsgQuench: msg = new MsgQuench(); break;
                    case PacketType.MsgNationality: msg = new MsgNationality(); break;
                    case PacketType.MsgTitle: msg = new MsgTitle(); break;
                    case PacketType.MsgVipUserHandle: msg = new MsgVipUserHandle(); break;
                    case PacketType.MsgTransportor: msg = new MsgTransportor(); break;
                    case PacketType.MsgAchievement: msg = new MsgAchievement(); break;
                    case PacketType.MsgAuction: msg = new MsgAuction(); break;
                    case PacketType.MsgAuctionItem: msg = new MsgAuctionItem(); break;
                    case PacketType.MsgAuctionQuery: msg = new MsgAuctionQuery(); break;
                    case PacketType.MsgMailList: msg = new MsgMailList(); break;
                    case PacketType.MsgMailOperation: msg = new MsgMailOperation(); break;
                    case PacketType.MsgMessageBoard: msg = new MsgMessageBoard(); break;
                    case PacketType.MsgActivityTaskReward: msg = new MsgActivityTaskReward(); break;
                    case PacketType.MsgProcessGoalTaskOpt: msg = new MsgProcessGoalTaskOpt(); break;
                    case PacketType.MsgSolidify: msg = new MsgSolidify(); break;
                    case PacketType.MsgAllot: msg = new MsgAllot(); break;
                    case PacketType.MsgSlotAction: msg = new MsgSlotAction(); break;
                    case PacketType.MsgLottery: msg = new MsgLottery(); break;
                    case PacketType.MsgLeagueOpt: msg = new MsgLeagueOpt(); break;
                    case PacketType.MsgSignIn: msg = new MsgSignIn(); break;
                    case PacketType.MsgCoatStorage: msg = new MsgCoatStorage(); break;
                    case PacketType.MsgTitleStorage: msg = new MsgTitleStorage(); break;
                    case PacketType.MsgItemRefineOpt: msg = new MsgItemPerfection(); break;
					case PacketType.MsgItemRefine: msg = new MsgItemPerfectionOperation(); break;
					case PacketType.MsgLoadMap: msg = new MsgLoadMap(); break;
                    case PacketType.MsgGLRankingList: msg = new MsgGLRankingList(); break;
                    case PacketType.MsgTeamArenaHeroData: msg = new MsgQualifierTeamPKStatistic(); break;
					case PacketType.MsgTeamArenaInteractive: msg = new MsgTeamArenaInteractive(); break;

					default:
                        {
                            if (!handled)
                            {
                                logger.Warning("Missing packet {0}, Length {1}\n" + PacketDump.Hex(message), type, length);
                            }
                            return;
                        }
                }

                msg.Decode(message);

                if (actor.Character?.Map != null)
                {
                    Character user = RoleManager.GetUser(actor.Character.Identity);
                    if (user == null || !user.Client.GUID.Equals(actor.GUID))
                    {
                        actor.Disconnect();
                        if (user != null)
                        {
                            await RoleManager.KickOutAsync(actor.Identity);
                        }
                        return;
                    }

                    WorldProcessor.Instance.Queue(actor.Character.Map.Partition, () => msg.ProcessAsync(actor));
                }
                else
                {
                    // we will not send all packets to NO_MAP_GROUP
                    // after this point we are only letting 1052 and first 10010 packet
                    if (type == PacketType.MsgConnect
                        || type == PacketType.MsgRegister
                        || type == PacketType.MsgRank
                        || msg is MsgAction action && action.Action != MsgAction.ActionType.MapJump)
                    {
                        WorldProcessor.Instance.Queue(WorldProcessor.NO_MAP_GROUP, () => msg.ProcessAsync(actor));
                    }
                    else
                    {
                        logger.Error("Message [{0}] sent out of map by client accountId: {1}, authority: {2}, ip: {3}, mac: {4}.", type, actor.AccountIdentity, actor.AuthorityLevel, actor.IpAddress, actor.MacAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on socket process! Message: {0}", ex.Message);
            }
        }

        protected override void Disconnected(GameClient actor)
        {
            if (actor.Creation != null)
            {
                Managers.RegistrationManager.Registration.Remove(actor.Creation.Token);
            }

            actor.Character?.QueueAction(actor.Character.OnLogoutAsync);
        }
    }
}
