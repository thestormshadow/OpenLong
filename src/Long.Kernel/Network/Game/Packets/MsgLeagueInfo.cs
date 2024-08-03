using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueInfo : MsgBase<GameClient>
    {
        public const uint FLAG_ALLOWANCE = 0x1,
                          FLAG_STIPEND = 0x2;

        public ulong Treasury { get; set; } // ?
        public uint GoldBricks { get; set; }
        public uint Stipend { get; set; }
        public uint ServerId { get; set; } // current server
        public uint RealmId { get; set; } // cross realm id
        public string Name { get; set; }
        public string LeaderName { get; set; }
        public string Bulletin { get; set; }
        public string Title { get; set; }
        public string PlunderServer { get; set; }
        public string InvadingUnion { get; set; }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeagueInfo);
            writer.Write(Treasury);
            writer.Write(GoldBricks);
            writer.Write(Stipend);
            writer.Write(ServerId);
            writer.Write(RealmId);
            writer.Write(Name, 16);
            writer.Write(LeaderName, 16);
            writer.Write(Bulletin, 256);
            writer.Write(Title, 10);
            writer.Write(PlunderServer, 16);
            writer.Write(InvadingUnion, 16);
            return writer.ToArray();
        }
    }
}
