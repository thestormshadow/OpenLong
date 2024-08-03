using Long.Network.Packets.Ai;
using Org.BouncyCastle.Math;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiHandshake : MsgAiHandshake<GameServer>
    {
        public MsgAiHandshake()
        {
        }

        public MsgAiHandshake(BigInteger publicKey, BigInteger modulus, byte[] eIv, byte[] dIv)
            : base(publicKey, modulus, eIv ?? new byte[16], dIv ?? new byte[16])
        {
        }

        public override async Task ProcessAsync(GameServer client)
        {
            if (!client.DiffieHellman.Initialize(PublicKey, Modulus))
            {
                throw new Exception("Could not initialize Diffie-Helmman!!!");
            }

            client.Cipher.GenerateKeys(new object[]
			{
				client.DiffieHellman.SharedKey.ToByteArrayUnsigned(),
                EncryptIV,
                DecryptIV
            });

            client.Stage = GameServer.ConnectionStage.Authenticating;
            await client.SendAsync(new MsgAiLoginExchange
            {
                Data = new MsgAiLoginExchangeContract
				{
					UserName = ServerConfiguration.Configuration.Ai.Username,
					Password = ServerConfiguration.Configuration.Ai.Password,
					ServerName = ServerConfiguration.Configuration.Ai.Name
				}               
            });
        }
    }
}
