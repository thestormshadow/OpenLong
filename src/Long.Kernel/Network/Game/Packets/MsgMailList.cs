using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMailList : MsgBase<GameClient>
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public ushort Unknown { get; set; }
        public ushort MaxPages { get; set; }
        public List<MailListStruct> MailList { get; set; } = new List<MailListStruct>();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Count = reader.ReadInt32();
            Page = reader.ReadInt32();
            Unknown = reader.ReadUInt16();
            MaxPages = reader.ReadUInt16();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgMailList);
            writer.Write(Count = MailList.Count);
            writer.Write(Page);
            writer.Write((int)MaxPages);
            foreach (var mail in MailList)
            {
                writer.Write(mail.EmailIdentity);   // 4    16
                writer.Write(mail.SenderName, 32);  // 36   20
                writer.Write(mail.Header, 32);      // 68   52
                writer.Write(new byte[32]);         // 100  84
                writer.Write(mail.Money);           // 104  116
                writer.Write(mail.ConquerPoints);   // 108  120
                writer.Write(mail.Timestamp);       // 112  124
                writer.Write(mail.HasAttachment);   // 116  128
                writer.Write(mail.ItemType);        // 120  132
            }
            return writer.ToArray();
        }

        public struct MailListStruct
        {
            public uint EmailIdentity { get; set; }
            public string SenderName { get; set; }
            public string Header { get; set; }
            public uint Money { get; set; }
            public uint ConquerPoints { get; set; }
            public int Timestamp { get; set; }
            public int HasAttachment { get; set; }
            public uint ItemType { get; set; }
        }

        public override Task ProcessAsync(GameClient client)
        {
            return client.Character.MailBox.SendListAsync(Page);
        }
    }
}
