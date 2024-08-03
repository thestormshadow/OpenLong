using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Team.Network
{
    public sealed class MsgAuraGroup : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgAuraGroup>();

        public AuraGroupMode Mode { get; set; }
        public uint Identity { get; set; }
        public uint LeaderIdentity { get; set; }
        public uint Count { get; set; }
        public uint Unknown { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (AuraGroupMode)reader.ReadInt32();
            Identity = reader.ReadUInt32();
            LeaderIdentity = reader.ReadUInt32();
            Count = reader.ReadUInt32();
            Unknown = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgAuraGroup);
            writer.Write((int)Mode);
            writer.Write(Identity);
            writer.Write(LeaderIdentity);
            writer.Write(Count);
            writer.Write(Unknown);
            return writer.ToArray();
        }

        public enum AuraGroupMode : uint
        {
            AutoInvite,
            Leader = 1,
            Teammate = 2
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            switch (Mode)
            {
                case AuraGroupMode.AutoInvite:
                    {
                        if (user.Team == null)
                        {
                            return Task.CompletedTask;
                        }
                        user.Team.IsAutoInvite = Identity != 0;
                        return Task.CompletedTask;
                    }
                default:
                    {
                        logger.Warning("Invalid subtype {0} for MsgAuraGroup", Mode);
                        break;
                    }
            }
            return Task.CompletedTask;
        }
    }
}
