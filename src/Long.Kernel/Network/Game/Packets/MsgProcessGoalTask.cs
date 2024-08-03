using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgProcessGoalTask : MsgBase<GameClient>
    {
        public int Param { get; set; }
        public bool Completed { get; set; }
        public List<GoalTaskStruct> Goals { get; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgProcessGoalTask);
            writer.Write(Param); // 4
            writer.Write(Completed); // 8
            writer.Write((ushort)Goals.Count); // 9
            foreach (var goal in Goals)
            {
                writer.Write(goal.Id); // 11 
                writer.Write(goal.Unknown); // 15
                writer.Write(goal.Claimed); // 19
            }
            return writer.ToArray();
        }

        public struct GoalTaskStruct
        {
            public int Id { get; set; }
            public int Unknown { get; set; }
            public bool Claimed { get; set; }
        }
    }
}
