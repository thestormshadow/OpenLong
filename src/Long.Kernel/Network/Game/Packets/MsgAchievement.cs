using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgAchievement : MsgBase<GameClient>
    {
        public enum AchievementRequest
        {
            Synchro = 0,
            Query = 1,
            Achieve = 2
        }

        public AchievementRequest Action { get; set; }
        public uint Identity { get; set; }
        public int Flag { get; set; }
        public int Count { get; set; }
        public List<uint> Flags { get; set; } = new List<uint>();

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgAchievement);
            writer.Write((int)Action);
            writer.Write(Identity);
            if (Flags.Count > 0)
            {
                writer.Write(Flag = Flags.Count);
            }
            else
            {
                writer.Write(Flag);
            }
            foreach (var flag in Flags)
            {
                writer.Write(flag);
            }
            return writer.ToArray();
        }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (AchievementRequest)reader.ReadInt32();
            Identity = reader.ReadUInt32();
            Flag = reader.ReadInt32();
            Count = reader.ReadInt32();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            switch (Action)
            {
                case AchievementRequest.Synchro:
                    {
                        Character target = RoleManager.GetUser(Identity);
                        if (target != null)
                        {
                            await target.Achievements.SendAsync(client.Character);
                        }
                        break;
                    }

                case AchievementRequest.Achieve:
                    {
                        await client.Character.Achievements.AwardAchievementAsync(Flag);
                        break;
                    }
            }
        }
    }
}
