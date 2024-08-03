using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgProcessGoalInfo : MsgBase<GameClient>
    {
        public List<GoalInfo> Goals { get; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgProcessGoalInfo);
            writer.Write((ushort)Goals.Count);
            foreach (var goal in Goals)
            {
                writer.Write(goal.Id);
                writer.Write(goal.Finished ? 1 : 0); // finished bool
                writer.Write(goal.ClaimEnable);
                writer.Write(goal.Unknown5);
            }
            return writer.ToArray();
        }

        public struct GoalInfo
        {
            public int Id { get; set; }
            public bool Finished { get; set; }
            public bool ClaimEnable { get; set; }
            public byte Unknown5 { get; set; }
        }
    }
}
