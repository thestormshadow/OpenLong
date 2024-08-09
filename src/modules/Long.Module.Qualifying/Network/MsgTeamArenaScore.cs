using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgTeamArenaScore : MsgBase<GameClient>
    {
        public uint TeamId0 { get; set; }
        public int Rank0 { get; set; }
        public string Name0 { get; set; }
        public int Damage0 { get; set; }

        public uint TeamId1 { get; set; }
        public int Rank1 { get; set; }
        public string Name1 { get; set; }
        public int Damage1 { get; set; }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgTeamArenaScore);
            
            writer.Write(TeamId0);
            writer.Write(Rank0);
            writer.Write(Name0, 16);
            writer.Write(Damage0);

            writer.Write(TeamId1);
            writer.Write(Rank1);
            writer.Write(Name1, 16);
            writer.Write(Damage1);

            writer.Write(1);
            return writer.ToArray();
        }
    }
}
