using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets
{
    public abstract class MsgProtoBufBase<TActor, TData> : MsgBase<TActor> where TActor : TcpServerActor
    {
        protected bool serializeWithHeaders = false;

        public MsgProtoBufBase(PacketType packetType)
        {
            Type = packetType;
        }

        public TData Data { get; set; }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)Type);
            if (serializeWithHeaders)
            {
                Serializer.SerializeWithLengthPrefix(writer.BaseStream, Data, PrefixStyle.Fixed32);
            }
            else
            {
                Serializer.Serialize(writer.BaseStream, Data);
            }
            return writer.ToArray();
        }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            if (serializeWithHeaders)
            {
                Data = Serializer.DeserializeWithLengthPrefix<TData>(reader.BaseStream, PrefixStyle.Fixed32);
            }
            else
            {
                Data = Serializer.Deserialize<TData>(reader.BaseStream);
            }
        }
    }
}