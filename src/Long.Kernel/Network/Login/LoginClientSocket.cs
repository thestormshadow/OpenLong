using Long.Kernel.Managers;
using Long.Kernel.Network.Login.Packets;
using Long.Network;
using Long.Network.Packets;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Login
{
    public sealed class LoginClientSocket : TcpClientSocket<LoginServer>
    {
        private static readonly ILogger logger = Log.ForContext<LoginClientSocket>();

        public static LoginClientSocket Instance { get; private set; }
        public LoginServer Client { get; set; }

        public LoginClientSocket()
            : base(NetworkDefinition.ACCOUNT_FOOTER, true)
        {
        }

        protected override Task<LoginServer> ConnectedAsync(Socket socket, Memory<byte> buffer)
        {
            uint readProcessor = packetProcessor.SelectReadPartition();
            uint writeProcessor = packetProcessor.SelectWritePartition();
            LoginServer loginServer = new LoginServer(socket, buffer, readProcessor, writeProcessor);
            Instance = this;
            Instance.Client = loginServer;
            return Task.FromResult(loginServer);
        }

        protected override async Task<bool> ExchangedAsync(LoginServer actor, Memory<byte> buffer)
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

        protected override async Task ProcessAsync(LoginServer actor, byte[] message)
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
            MsgBase<LoginServer> msg;
            try
            {
                switch (type)
                {
                    case PacketType.MsgLoginRealmAuthEx: msg = new MsgLoginRealmAuthEx(); break;
                    case PacketType.MsgLoginUserExchange: msg = new MsgLoginUserExchange(); break;
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

        protected override void Disconnected(LoginServer actor)
        {
            logger.Information("Account server disconnected...");
            RealmManager.SetAccountServerStatus(false);
            Instance = null;
        }
    }
}
