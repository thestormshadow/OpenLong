using Long.Kernel.Network.Ai;
using Long.Network.Packets.Ai;
using Org.BouncyCastle.Math;
using System.Security.Cryptography;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiHandshake : MsgAiHandshake<AiClient>
    {
        public MsgAiHandshake()
        {
        }

        public MsgAiHandshake(BigInteger publicKey, BigInteger modulus, byte[] eIv, byte[] dIv)
            : base(publicKey, modulus, eIv ?? new byte[16], dIv ?? new byte[16])
        {
        }

        public override async Task ProcessAsync(AiClient client)
        {
            if (!client.DiffieHellman.Initialize(PublicKey, Modulus))
            {
                throw new Exception("Couldn't initialize Diffie-Hellman!!!");
            }

            byte[] iv = RandomNumberGenerator.GetBytes(16);
            //await client.SendNoQueueAsync(new MsgAiHandshake(client.DiffieHellman.PublicKey, client.DiffieHellman.Modulus, iv, iv));

            client.Cipher.GenerateKeys(new object[]
            {
                client.DiffieHellman.SharedKey.ToByteArrayUnsigned(),
                iv,
                iv
            });
        }
    }
}
