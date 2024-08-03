using Long.Login.Managers;
using Long.Login.Network.Game.Packets;
using Long.Network;
using Long.Network.Packets;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Login.Network.Game
{
    public sealed class GameServerSocket : TcpServerListener<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<GameServerSocket>();

        public static GameServerSocket Instance { get; private set; }

        public GameServerSocket()
            : base(100, readProcessors: 1, writeProcessors: 1 , footer: NetworkDefinition.ACCOUNT_FOOTER, exchange: true)
        {
            Instance = this;
            ExchangeStartPosition = 0;
        }

        protected override async Task<GameClient> AcceptedAsync(Socket socket, Memory<byte> buffer)
        {
            uint readProcessor = packetProcessor.SelectReadPartition();
            uint writeProcessor = packetProcessor.SelectWritePartition();
            GameClient gameClient = new GameClient(socket, buffer, readProcessor, writeProcessor);
            await gameClient.SendAsync(new MsgLongHandshake(gameClient.DiffieHellman.PublicKey, gameClient.DiffieHellman.Modulus, null, null));
            return gameClient;
        }

        protected override bool Exchanged(GameClient actor, ReadOnlySpan<byte> buffer)
        {
            try
            {
                MsgLongHandshake handshake = new MsgLongHandshake();
                handshake.Decode(buffer.ToArray());

                if (!actor.DiffieHellman.Initialize(handshake.Data.PublicKey, handshake.Data.Modulus))
                {
                    logger.Error("Could not initialize diffie hellman!! [{0}]", actor.IpAddress);
                    return false;
                }

                actor.Cipher.GenerateKeys(actor.DiffieHellman.SharedKey.ToByteArrayUnsigned(),
                    handshake.Data.EncryptIV,
                    handshake.Data.DecryptIV);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to exchange game server: {0} [{1}]\n" + PacketDump.Hex(buffer), actor.IpAddress, ex.Message);
                return false;
            }
        }

        protected override async Task ProcessAsync(GameClient actor, byte[] message)
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
            MsgBase<GameClient> msg;
            try
            {
                switch (type)
                {
                    case PacketType.MsgLoginRealmAuth: msg = new MsgLoginRealmAuth(); break;
                    case PacketType.MsgLoginUserExchangeEx: msg = new MsgLoginUserExchangeEx(); break;
                    case PacketType.MsgLoginRequestUserId: msg = new MsgLoginRequestUserId(); break;
                    case PacketType.MsgLoginAction: msg = new MsgLoginAction(); break;
                    default:
                        {
                            logger.Warning($"Packet[{type}] Length[{length}] not handled by server.\n{PacketDump.Hex(message)}");
                            return;
                        }
                }

                msg.Decode(message);
                await msg.ProcessAsync(actor);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on processing login message. {0}", ex.Message);
            }
        }

        protected override void Disconnected(GameClient actor)
        {
            if (actor.Realm != null)
            {
                RealmManager.RemoveRealm(actor.Realm.Id);
                logger.Information("Server {0} {1} has disconnected!", actor.Realm.RealmId, actor.Realm.Name);
            }
            else
            {
                logger.Warning("Server {0} has disconnected.", actor.IpAddress);
            }
        }
    }
}
