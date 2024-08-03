using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgActivityTask : MsgBase<GameClient>
    {
        public enum Action
        {
            ResetActivityTask,
            DeleteActivityTask,
            AddActivityTask,
            UpdateActivityTask
        }

        public Action Mode { get; set; }
        public byte Count => (byte)Activities.Count;
        public List<Activity> Activities { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (Action)reader.ReadByte();
            int count = reader.ReadByte();
            for (int i = 0; i < count; i++)
            {
                uint id = reader.ReadUInt32();
                byte completed = reader.ReadByte();
                byte progress = reader.ReadByte();
                Activities.Add(new Activity
                {
                    Id = id,
                    Completed = completed,
                    Progress = progress
                });
            }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgActivityTask);
            writer.Write((byte)Mode);
            writer.Write(Count);
            foreach (var activity in Activities)
            {
                writer.Write(activity.Id);
                writer.Write(activity.Completed);
                writer.Write(activity.Progress);
            }
            return writer.ToArray();
        }

        public struct Activity
        {
            public uint Id { get; set; }
            public byte Completed { get; set; }
            public byte Progress { get; set; }
        }
    }
}
