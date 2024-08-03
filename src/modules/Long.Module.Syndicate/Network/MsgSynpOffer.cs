using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgSynpOffer : MsgBase<GameClient>
    {
        public MsgSynpOffer()
        {

        }

        public MsgSynpOffer(ISyndicateMember member)
        {
            Identity = uint.MaxValue;
            Silver = member.Silvers / 10000;
            ConquerPoints = member.ConquerPointsDonation * 20;
            GuideDonation = member.GuideDonation;
            PkDonation = member.PkDonation;
            ArsenalDonation = member.ArsenalDonation;
            RedRoseDonation = member.RedRoseDonation;
            OrchidDonation = member.WhiteRoseDonation;
            WhiteRoseDonation = member.OrchidDonation;
            TulipDonation = member.TulipDonation;
            SilverTotal = (uint)(member.SilversTotal / 10000);
            ConquerPointsTotal = member.ConquerPointsTotalDonation * 20;
            GuideTotal = member.GuideDonation;
            PkTotal = member.PkTotalDonation;
        }

        public uint Identity { get; set; }
        public int Silver { get; set; }
        public uint ConquerPoints { get; set; }
        public uint GuideDonation { get; set; }
        public int PkDonation { get; set; }
        public uint ArsenalDonation { get; set; }
        public uint RedRoseDonation { get; set; }
        public uint OrchidDonation { get; set; }
        public uint WhiteRoseDonation { get; set; }
        public uint TulipDonation { get; set; }
        public uint SilverTotal { get; set; }
        public uint ConquerPointsTotal { get; set; }
        public uint GuideTotal { get; set; }
        public int PkTotal { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Identity = reader.ReadUInt32();
            Silver = reader.ReadInt32();
            ConquerPoints = reader.ReadUInt32();
            GuideDonation = reader.ReadUInt32();
            ArsenalDonation = reader.ReadUInt32();
            RedRoseDonation = reader.ReadUInt32();
            OrchidDonation = reader.ReadUInt32();
            WhiteRoseDonation = reader.ReadUInt32();
            TulipDonation = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSynpOffer);
            writer.Write(Identity);           // 4
            writer.Write(Silver);             // 8
            writer.Write(ConquerPoints);      // 12
            writer.Write(GuideDonation);      // 16
            writer.Write(PkDonation);         // 20
            writer.Write(ArsenalDonation);    // 24
            writer.Write(RedRoseDonation);    // 28
            writer.Write(OrchidDonation);  // 32
            writer.Write(WhiteRoseDonation);     // 36
            writer.Write(TulipDonation);      // 40
            writer.Write(SilverTotal);        // 44 Total Silver
            writer.Write(ConquerPointsTotal); // 48 Total Emoney
            writer.Write(GuideTotal);         // 52 Guide
            writer.Write(PkTotal);            // 56 PK
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user == null)
            {
                return;
            }

            if (user.SyndicateIdentity > 0)
            {
                await user.SendAsync(new MsgSynpOffer(user.SyndicateMember));
            }
        }
    }
}
