using Long.Kernel.Managers;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Kernel.Network.Cross.Packets;
using Long.Kernel.Network.Cross.Server.Packets;
using Long.Kernel.Processors;
using Long.Network;
using Long.Network.Packets;
using Long.Network.Packets.Cross;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Cross.Client
{
    public sealed class CrossClientSession : TcpClientSocket<CrossClientActor>
    {
        private static readonly ILogger logger = Log.ForContext<CrossClientSession>();

        public CrossClientSession()
            : base(NetworkDefinition.CROSS_FOOTER, true)
        {
        }

        public CrossClientActor Actor { get; private set; }
        public ulong LastPing { get; set; } = 0;
        public ulong LastPong { get; set; } = 0;

        public uint ServerID { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }

        protected override Task<CrossClientActor> ConnectedAsync(Socket socket, Memory<byte> buffer)
        {
            uint readProcessor = packetProcessor.SelectReadPartition();
            uint writeProcessor = packetProcessor.SelectWritePartition();
            Actor = new CrossClientActor(socket, buffer, readProcessor, writeProcessor, NetworkDefinition.CROSS_FOOTER)
            {
                Session = this
            };
            return Task.FromResult(Actor);
        }

        protected override async Task<bool> ExchangedAsync(CrossClientActor actor, Memory<byte> buffer)
        {
            try
            {
                MsgLongHandshake handshake = new MsgLongHandshake();
                handshake.Decode(buffer.ToArray());
                await handshake.ProcessAsync(actor);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on exchange!!! {0}", ex.Message);
                return false;
            }
        }

        protected override async Task ProcessAsync(CrossClientActor actor, byte[] message)
        {
            // Validate connection
            if (!actor.Socket.Connected)
            {
                return;
            }

            // Read in TQ's binary header
            var length = BitConverter.ToUInt16(message, 0);
            var type = (PacketType)BitConverter.ToUInt16(message, 2);

            // Switch on the packet type
            MsgBase<CrossClientActor> msg;
            try
            {
                switch (type) 
                {
                    case PacketType.MsgCrossLoginEx: msg = new MsgCrossLoginExC(); break;
                    case PacketType.MsgCrossRequestSwitch: msg = new MsgCrossRequestSwitchC(); break;
                    case PacketType.MsgCrossRequestSwitchEx: msg = new MsgCrossRequestSwitchExC(); break;
                    case PacketType.MsgCrossUserInfo: msg = new MsgCrossUserInfoC(); break;
                    case PacketType.MsgCrossRedirectUserPacket: msg = new MsgCrossRedirectUserPacketC(); break;
                    case PacketType.MsgCrossRedirectToUserPacket: msg = new MsgCrossRedirectToUserPacketC(); break;
                    case PacketType.MsgCrossItemInfo: msg = new MsgCrossItemInfoC(); break;
                    case PacketType.MsgCrossRealmAction: msg = new MsgCrossRealmActionC(); break;
                    case PacketType.MsgCrossBroadcastPacket: msg = new MsgCrossBroadcastPacketC(); break;
                    case PacketType.MsgCrossAstProfInfo: msg = new MsgCrossAstProfInfoC(); break;
                    case PacketType.MsgCrossTrainingVitalityInfo: msg = new MsgCrossTrainingVitalityInfoC(); break;
                    case PacketType.MsgCrossOwnKongFuInfo: msg = new MsgCrossOwnKongFuInfoC(); break;
                    case PacketType.MsgCrossNeigongInfo: msg = new MsgCrossNeigongInfoC(); break;
                    case PacketType.MsgCrossWeaponSkillInfo: msg = new MsgCrossWeaponSkillInfoC(); break;
                    case PacketType.MsgCrossMagicInfo: msg = new MsgCrossMagicInfoC(); break;
                    default:
                        {
                            logger.Warning($"Packet[{type}] Length[{length}] not handled by CrossClientSession.\n{PacketDump.Hex(message)}");
                            return;
                        }
                }

                msg.Decode(message);
                WorldProcessor.Instance.Queue(WorldProcessor.NO_MAP_GROUP, () => msg.ProcessAsync(actor));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on processing cross server message. {0}", ex.Message);
            }
        }

        protected override void Disconnected(CrossClientActor actor)
        {
            if (RealmConnectionManager.RealmSession?.Actor.ID == actor.ID)
            {
                logger.Information("Disconnected from the realm.");
                RealmConnectionManager.RealmSession = null;

                // TODO disconnect from all servers!
                return;
            }
            
            // TODO remove from child server pool
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
