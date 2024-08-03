using Org.BouncyCastle.Math;
using Long.Network.Packets.Handshake;
using Long.Network.Sockets;
using System.Security.Cryptography;
using Long.Kernel.Settings;
using Long.Kernel.Network.Cross.Server;
using Long.Kernel.Network.Cross.Server.Packets;
using Long.Network.Packets.Cross;
using Long.Kernel.Network.Cross.Client;
using Long.Network.Security;

namespace Long.Kernel.Network.Cross.Packets
{
    public sealed class MsgLongHandshake : MsgLongHandshake<TcpServerActor>
    {
        public MsgLongHandshake()
        {
        }

        public MsgLongHandshake(BigInteger publicKey, BigInteger modulus, byte[] eIv, byte[] dIv)
            : base(publicKey, modulus, eIv ?? new byte[16], dIv ?? new byte[16])
        {
        }

        public override Task ProcessAsync(TcpServerActor client)
        {
            if (client is CrossClientActor actor)
            {
                if (!actor.DiffieHellman.Initialize(Data.PublicKey, Data.Modulus))
                {
                    throw new CryptographicException("Could not initialize diffie hellmann");
                }

                byte[] eIv = RandomNumberGenerator.GetBytes(16);
                byte[] dIv = RandomNumberGenerator.GetBytes(16);
                return actor.SendAsync(new MsgLongHandshake(actor.DiffieHellman.PublicKey, actor.DiffieHellman.Modulus, dIv, eIv), async () =>
                {
                    actor.Cipher = new BlowfishCipher();
                    actor.Cipher.GenerateKeys(
                        actor.DiffieHellman.SharedKey.ToByteArrayUnsigned(),
                        eIv,
                        dIv);

                    var serverSettings = GameServerSettings.Instance;
                    await actor.SendAsync(new MsgCrossLoginS
                    {
                        Data = new MsgCrossLogin<CrossServerActor>.CrossLoginPB
                        {
                            ServerID = serverSettings.Game.Guid.ToString(),
                            ServerIDX = serverSettings.Game.Id,
                            ServerName = serverSettings.Game.Name,
                            Username = actor.Session.Username,
                            Password = actor.Session.Password
                        }
                    });
                });
            }
            return Task.CompletedTask;
        }
    }
}
