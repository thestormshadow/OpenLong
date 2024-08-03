using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgWeather : MsgBase<GameClient>
    {
        public uint WeatherType { get; set; }
        public uint Intensity { get; set; }
        public uint Direction { get; set; }
        public uint ColorArgb { get; set; }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgWeather);
            writer.Write(WeatherType);
            writer.Write(Intensity);
            writer.Write(Direction);
            writer.Write(ColorArgb);
            return writer.ToArray();
        }
    }
}
