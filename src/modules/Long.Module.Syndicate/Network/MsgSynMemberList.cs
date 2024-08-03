using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgSynMemberList : MsgBase<GameClient>
    {
        public uint SubType { get; set; }
        public int Index { get; set; }
        public int Amount { get; set; }
        public List<MemberStruct> Members { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            SubType = reader.ReadUInt32();
            Index = reader.ReadInt32();
            Amount = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSynMemberList);
            writer.Write(SubType);
            writer.Write(Index);
            writer.Write(Amount = Members.Count);
            foreach (MemberStruct member in Members)
            {
                writer.Write(member.Name, 16);
                writer.Write(member.Nobility);
                writer.Write(member.LookFace % 10000 / 1000);
                writer.Write(member.Level);
                writer.Write((uint)member.Rank);
                writer.Write(member.PositionExpire);
                writer.Write(member.TotalDonation);
                writer.Write(member.IsOnline ? 1 : 0);
                writer.Write(0); // test
                writer.Write(member.Profession); // last login
                writer.Write(member.LastLoginSeconds);
                writer.Write(0);
            }

            return writer.ToArray();
        }

        public struct MemberStruct
        {
            public uint Identity { get; set; }
            public uint LookFace { get; set; }
            public string Name { get; set; }
            public int Level { get; set; }
            public int Nobility { get; set; }
            public int Rank { get; set; }
            public uint PositionExpire { get; set; }
            public int TotalDonation { get; set; }
            public bool IsOnline { get; set; }
            public int Profession { get; set; }
            public int LastLoginSeconds { get; set; }
        }

        public override Task ProcessAsync(GameClient client)
        {
            if (client.Character?.Syndicate == null)
            {
                return Task.CompletedTask;
            }

            return client.Character.Syndicate.SendMembersAsync(Index, client.Character);
        }
    }
}
