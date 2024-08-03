using Long.Kernel.Managers;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgProcessGoalTaskOpt : MsgBase<GameClient>
    {
        public enum Action : byte
        {
            Display,
            ClaimStageReward,
            ClaimReward
        }

        public Action Mode { get; set; }
        public ushort Data { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (Action)reader.ReadByte();
            Data = reader.ReadUInt16();
            ushort test = reader.ReadUInt16();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgProcessGoalTaskOpt);
            writer.Write((byte)Mode);
            writer.Write(Data);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            switch (Mode)
            {
                case Action.Display:
                    {
                        await client.Character.StageGoal.SendAsync(Data);
                        break;
                    }

                case Action.ClaimStageReward:
                    {
                        if (await client.Character.StageGoal.ClaimTaskRewardAsync(Data))
                        {
                            await client.SendAsync(this);
                        }
                        break;
                    }

                case Action.ClaimReward:
                    {
                        if (await client.Character.StageGoal.ClaimGoalRewardAsync(Data))
                        {
                            await client.SendAsync(this);
                        }
                        break;
                    }
            }
        }
    }
}
