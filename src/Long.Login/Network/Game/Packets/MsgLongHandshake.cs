using Long.Network.Packets.Handshake;
using Org.BouncyCastle.Math;

namespace Long.Login.Network.Game.Packets
{
    public sealed class MsgLongHandshake : MsgLongHandshake<GameClient>
    {
        public MsgLongHandshake()
        {
        }

        public MsgLongHandshake(BigInteger publicKey, BigInteger modulus, byte[] eIv, byte[] dIv)
            : base(publicKey, modulus, eIv ?? new byte[16], dIv ?? new byte[16])
        {
        }
    }
}
