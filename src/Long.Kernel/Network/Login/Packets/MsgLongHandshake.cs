using Long.Kernel.Settings;
using Long.Network.Packets.Handshake;
using Org.BouncyCastle.Math;
using System.Security.Cryptography;

namespace Long.Kernel.Network.Login.Packets
{
    public sealed class MsgLongHandshake : MsgLongHandshake<LoginServer>
    {
        public MsgLongHandshake()
        {
        }

        public MsgLongHandshake(BigInteger publicKey, BigInteger modulus, byte[] eIv, byte[] dIv)
            : base(publicKey, modulus, eIv ?? new byte[16], dIv ?? new byte[16])
        {
        }

        public override async Task ProcessAsync(LoginServer client)
        {
            if (!client.DiffieHellman.Initialize(Data.PublicKey, Data.Modulus))
            {
                throw new CryptographicException("Could not initialize diffie hellmann");
            }

            byte[] eIv = RandomNumberGenerator.GetBytes(16);
            byte[] dIv = RandomNumberGenerator.GetBytes(16);
            await client.SendAsync(new MsgLongHandshake(client.DiffieHellman.PublicKey, client.DiffieHellman.Modulus, dIv, eIv), async () =>
            {
                client.Cipher.GenerateKeys(client.DiffieHellman.SharedKey.ToByteArrayUnsigned(),
                    eIv,
                    dIv);

                var serverSettings = GameServerSettings.Instance;
                await client.SendAsync(new MsgLoginRealmAuth()
                {
                    Data = new MsgLoginRealmAuth.RealmAuthData
                    {
                        Username = serverSettings.Game.Username,
                        Password = serverSettings.Game.Password,
                        RealmID = serverSettings.Game.Guid.ToString()
                    }
                });
            });
        }
    }
}
