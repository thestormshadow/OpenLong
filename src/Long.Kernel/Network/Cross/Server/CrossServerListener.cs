using Long.Kernel.Managers;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Kernel.Network.Cross.Packets;
using Long.Kernel.Network.Cross.Server.Packets;
using Long.Kernel.Processors;
using Long.Network;
using Long.Network.Packets;
using Long.Network.Packets.Cross;
using Long.Network.Security;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Cross.Server
{
    public sealed class CrossServerListener : TcpServerListener<CrossServerActor>
    {
        private static readonly ILogger logger = Log.ForContext<CrossServerListener>();

        public static CrossServerListener Instance { get; private set; }

        public CrossServerListener()
            : base(exchange: true, footer: NetworkDefinition.CROSS_FOOTER, readProcessors: 1, writeProcessors: 1)
        {
            Instance = this;
            ExchangeStartPosition = 0;
        }

        protected override async Task<CrossServerActor> AcceptedAsync(Socket socket, Memory<byte> buffer)
        {
            uint readProcessor = packetProcessor.SelectReadPartition();
            uint writeProcessor = packetProcessor.SelectWritePartition();
            CrossServerActor actor = new CrossServerActor(socket, buffer, readProcessor, writeProcessor, NetworkDefinition.CROSS_FOOTER);
            await actor.SendAsync(new MsgLongHandshake(actor.DiffieHellman.PublicKey, actor.DiffieHellman.Modulus, null, null));
            return actor;
        }

        protected override bool Exchanged(CrossServerActor actor, ReadOnlySpan<byte> buffer)
        {
            try
            {
                MsgLongHandshake handshake = new MsgLongHandshake();
                handshake.Decode(buffer.ToArray());

                if (!actor.DiffieHellman.Initialize(handshake.Data.PublicKey, handshake.Data.Modulus))
                {
                    logger.Error("Could not initialize diffie hellman!! {0}", actor.IpAddress);
                    return false;
                }

                actor.Cipher = new BlowfishCipher();
                actor.Cipher.GenerateKeys(
                    actor.DiffieHellman.SharedKey.ToByteArrayUnsigned(),
                    handshake.Data.EncryptIV, 
                    handshake.Data.DecryptIV);
                return true;              
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to exchange game server: {0} [{1}]", actor.IpAddress, ex.Message);
                return false;
            }
        }

        protected override async Task ProcessAsync(CrossServerActor actor, byte[] message)
        {
            // Validate connection
            if (!actor.Socket.Connected)
            {
                return;
            }

            // Read in TQ's binary header
            var length = BitConverter.ToUInt16(message, 0);
            var type = (PacketType)BitConverter.ToUInt16(message, 2);

            try
            {
                // Switch on the packet type
                MsgBase<CrossServerActor> msg;
                
                switch (type)
                {
                    case PacketType.MsgCrossLogin: msg = new MsgCrossLoginS(); break;
                    case PacketType.MsgCrossRequestSwitch: msg = new MsgCrossRequestSwitchS(); break;
                    case PacketType.MsgCrossRequestSwitchEx: msg = new MsgCrossRequestSwitchExS(); break;
                    case PacketType.MsgCrossUserInfo: msg = new MsgCrossUserInfoS(); break;
                    case PacketType.MsgCrossRedirectUserPacket: msg = new MsgCrossRedirectUserPacketS(); break;
                    case PacketType.MsgCrossRedirectToUserPacket: msg = new MsgCrossRedirectToUserPacketS(); break;
                    case PacketType.MsgCrossItemInfo: msg = new MsgCrossItemInfoS(); break;
                    case PacketType.MsgCrossRealmAction: msg = new MsgCrossRealmActionS(); break;
                    case PacketType.MsgCrossBroadcastPacket: msg = new MsgCrossBroadcastPacketS(); break;
                    case PacketType.MsgCrossAstProfInfo: msg = new MsgCrossAstProfInfoS(); break;
                    case PacketType.MsgCrossTrainingVitalityInfo: msg = new MsgCrossTrainingVitalityInfoS(); break;
                    case PacketType.MsgCrossOwnKongFuInfo: msg = new MsgCrossOwnKongFuInfoS(); break;
                    case PacketType.MsgCrossNeigongInfo: msg = new MsgCrossNeigongInfoS(); break;
                    case PacketType.MsgCrossWeaponSkillInfo: msg = new MsgCrossWeaponSkillInfoS(); break;
                    case PacketType.MsgCrossMagicInfo: msg = new MsgCrossMagicInfoS(); break;
                    default:
                        {
                            logger.Warning("[CrossServerListener] Packet {0} Length {1} not handled.\n" + PacketDump.Hex(message), type, length);
                            return;
                        }
                }

                msg.Decode(message);
                WorldProcessor.Instance.Queue(WorldProcessor.NO_MAP_GROUP, () => msg.ProcessAsync(actor));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[CrossServerListener] Error on processing message. {0}", ex.Message);
            }
        }

        protected override void Disconnected(CrossServerActor actor)
        {
            // hmmm, server and realm have different behaviors
            if (RealmConnectionManager.RemoveConnection(actor.ServerId))
            {
                if (RealmManager.GetServer(actor.ServerId, out var server))
                {
                    logger.Information("Server {0} has disconnected.", server.ServerName);
                }
                else
                {
                    logger.Information("Server {0} has disconnected.", actor.ServerId);
                }
            }
        }
    }
}
