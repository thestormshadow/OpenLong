using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgHangUp : MsgBase<GameClient>
    {
        public HangUpMode Action { get; set; }
        public ulong Param { get; set; }
        public ulong Experience { get; set; }
        public string KillerName { get; set; } = string.Empty;
        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgHangUp);
            writer.Write((ushort)Action); // 4
            writer.Write(Param); // 6
            writer.Write(Experience); // 14
            writer.Write(KillerName, 16); // 22
            return writer.ToArray();
        }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (HangUpMode)reader.ReadUInt32();
            Param = reader.ReadUInt64();
        }

        public override async Task ProcessAsync(GameClient client)
        {
			Character user = client.Character;
            switch (Action)
            {
                case HangUpMode.Show:
                    {
                        Param = 341;
                        await user.SendAsync(this);
                        break;
                    }

                case HangUpMode.Interface:
                    {
                        Experience = user.AutoHangUpExperience;
                        await user.SendAsync(this);
                        break;
                    }

                case HangUpMode.Start:
                    {
                        if (user.Map == null || !user.Map.IsAutoHungUpMap())
                        {
                            return;
                        }

                        user.AutoHangUpExperience = 0;

                        user.IsAutoHangUp = true;
                        await user.SendAsync(this);
                        break;
                    }

                case HangUpMode.End:
                    {
                        await user.FinishAutoHangUpAsync(Action);
                        await user.SendAsync(new MsgHangUp
                        {
                            Action = HangUpMode.End,
                            Experience = user.AutoHangUpExperience
                        });
                        break;
                    }
            }
        }

        public enum HangUpMode
        {
            Show,
            Start,
            Interface,
            End,
            Unknown,
            KilledNoFirstCredit,
            KilledNoBlessing,
            ChangedMap
        }
    }
}
