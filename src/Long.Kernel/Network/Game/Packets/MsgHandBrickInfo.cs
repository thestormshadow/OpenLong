using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgHandBrickInfo : MsgBase<GameClient>
    {
        public enum BrickInfoAction
        {
            LeagueBricks, OpponentBricks, SubmitDialog
        }

        public BrickInfoAction Action { get; set; }
        public uint GoldBrick { get; set; }
        public uint Command { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string LeagueName { get; set; } = string.Empty;

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgHandBrickInfo);
            writer.Write((ushort)Action);
            writer.Write(GoldBrick);
            writer.Write(Command);
            writer.Write(new List<string>
            {
                CountryName,
                LeagueName,
            });
            return writer.ToArray();
        }
    }
}
