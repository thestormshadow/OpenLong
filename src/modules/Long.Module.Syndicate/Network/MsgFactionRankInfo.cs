using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Syndicate.States;
using Long.Network.Packets;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgFactionRankInfo : MsgBase<GameClient>
    {
        public List<MemberListInfoStruct> Members = new();

        public RankRequestType DonationType { get; set; }
        public ushort Count { get; set; }
        public ushort MaxCount { get; set; } = MAX_COUNT;

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            DonationType = (RankRequestType)reader.ReadUInt16();
            Count = reader.ReadUInt16();
            MaxCount = reader.ReadUInt16();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgFactionRankInfo);
            writer.Write((ushort)DonationType);
            writer.Write(Count = (ushort)Members.Count);
            writer.Write((int)MAX_COUNT);
            writer.Write(0);
            foreach (MemberListInfoStruct member in Members)
            {
                writer.Write((uint)member.Rank); // 0
                writer.Write(member.Position); // 4
                writer.Write(member.Silvers); // 8
                writer.Write(member.ConquerPoints); // 12
                writer.Write(member.GuideDonation); // 16
                writer.Write(member.PkDonation); // 20
                writer.Write(member.ArsenalDonation); // 24
                writer.Write(member.RedRose); // 28
                writer.Write(member.WhiteRose); // 32
                writer.Write(member.Orchid); // 36
                writer.Write(member.Tulip); // 40
                writer.Write(member.TotalDonation); // 44
                writer.Write(member.PlayerName, 16); // 48
                writer.Write(0);
            }

            return writer.ToArray();
        }

        public struct MemberListInfoStruct
        {
            public uint PlayerIdentity { get; set; }
            public int Rank { get; set; }
            public int Position { get; set; }
            public int Silvers { get; set; }
            public uint ConquerPoints { get; set; }
            public int PkDonation { get; set; }
            public uint GuideDonation { get; set; }
            public uint ArsenalDonation { get; set; }
            public uint RedRose { get; set; }
            public uint WhiteRose { get; set; }
            public uint Orchid { get; set; }
            public uint Tulip { get; set; }
            public uint TotalDonation { get; set; }
            public int UsableDonation { get; set; }
            public string PlayerName { get; set; }
        }

        public const ushort MAX_COUNT = 10;

        public enum RankRequestType
        {
            Silvers,
            ConquerPoints,
            Guide,
            PK,
            Arsenal,
            RedRose,
            Orchid,
            WhiteRose,
            Tulip,
            Usable,
            Total
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            ISyndicate syn = user?.Syndicate;
            if (syn == null)
            {
                return;
            }

            List<ISyndicateMember> members = syn.QueryRank((int)DonationType);
            for (var i = 0; i < MAX_COUNT && i < members.Count; i++)
            {
                ISyndicateMember member = members[i];
                Members.Add(new MemberListInfoStruct
                {
                    PlayerIdentity = member.UserIdentity,
                    PlayerName = member.UserName,
                    Silvers = member.Silvers / 10000,
                    ConquerPoints = member.ConquerPointsDonation * 20,
                    GuideDonation = member.GuideDonation,
                    PkDonation = member.PkDonation,
                    ArsenalDonation = member.ArsenalDonation,
                    RedRose = member.RedRoseDonation,
                    WhiteRose = member.WhiteRoseDonation,
                    Orchid = member.OrchidDonation,
                    Tulip = member.TulipDonation,
                    TotalDonation = (uint)member.TotalDonation,
                    UsableDonation = member.UsableDonation,
                    Position = i,
                    Rank = (int)member.Rank
                });
            }

            await client.SendAsync(this);
        }
    }
}
