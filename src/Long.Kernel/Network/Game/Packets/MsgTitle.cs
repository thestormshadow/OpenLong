using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgTitle : MsgBase<GameClient>
    {
        public TitleAction Action { get; set; }
        public byte Count { get; set; }
        public uint Identity { get; set; }
        public byte Title { get; set; }
        public List<byte> Titles { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Identity = reader.ReadUInt32();
            Title = reader.ReadByte();
            Action = (TitleAction)reader.ReadByte();
            Count = reader.ReadByte();
            for (var i = 0; i < Count; i++)
            {
                Titles.Add(reader.ReadByte());
            }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTitle);
            writer.Write(Identity); // 4
            writer.Write(Title);
            writer.Write((byte)Action);
            writer.Write(Count = (byte)Titles.Count);
            foreach (byte b in Titles)
            {
                writer.Write(b);
            }

            return writer.ToArray();
        }

        public enum TitleAction : byte
        {
            Hide = 0,
            Add = 1,
            Remove = 2,
            Select = 3,
            Query = 4
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user == null)
            {
                return;
            }

            switch (Action)
            {
                case TitleAction.Query:
                    {
                        await user.SendTitlesAsync();
                        break;
                    }

                case TitleAction.Select:
                    {
                        await user.SelectTitleAsync(Title);
                        break;
                    }
            }
        }
    }
}
