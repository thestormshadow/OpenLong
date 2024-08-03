using Long.Network.Sockets;
using Org.BouncyCastle.Math;
using ProtoBuf;
using System.Security.Cryptography;

namespace Long.Network.Packets.Handshake
{
    public abstract class MsgLongHandshake<TActor>
        : MsgProtoBufBase<TActor, MsgLongHandshake<TActor>.HandshakeData> where TActor : TcpServerActor

    {
        [ProtoContract]
        public struct HandshakeData
        {
            [ProtoMember(1)]
            public byte[] StartPadding { get; set; }
            [ProtoMember(2)]
            public byte[] MiddlePadding { get; set; }
            [ProtoMember(3)]
            public byte[] PublicKey { get; set; }
            [ProtoMember(4)]
            public byte[] Modulus { get; set; }
            [ProtoMember(5)]
            public byte[] EncryptIV { get; set; }
            [ProtoMember(6)]
            public byte[] DecryptIV { get; set; }
            [ProtoMember(7)]
            public byte[] FinalPadding { get; set; }
            [ProtoMember(8)]
            public byte[] FinalTrash { get; set; }
        }

        public MsgLongHandshake()
            : base(PacketType.MsgLongHandshake)
        {
            serializeWithHeaders = true;
        }

        public MsgLongHandshake(BigInteger publicKey, BigInteger modulus, byte[] eIv, byte[] dIv)
            : base(PacketType.MsgLongHandshake)
        {
            serializeWithHeaders = true;
            Data = new HandshakeData
            {
                StartPadding = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32)),
                MiddlePadding = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32)),
                PublicKey = publicKey.ToByteArrayUnsigned(),
                Modulus = modulus.ToByteArrayUnsigned(),
                EncryptIV = eIv,
                DecryptIV = dIv,
                FinalPadding = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32)),
                FinalTrash = RandomNumberGenerator.GetBytes(RandomNumberGenerator.GetInt32(8, 32))
            };
        }


    }
}
