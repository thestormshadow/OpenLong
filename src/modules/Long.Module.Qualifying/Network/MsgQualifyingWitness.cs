using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgQualifierWitness : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgQualifierWitness>();

        public WitnessAction Action { get; set; }
        public uint Command { get; set; }
        public uint Identity { get; set; }
        public int WitnessCount { get; set; }
        public int Cheers1 { get; set; }
        public int Cheers2 { get; set; }
        public List<WitnessModel> Witnesses { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (WitnessAction)reader.ReadUInt16();
            Command = reader.ReadUInt32();
            Identity = reader.ReadUInt32();
            WitnessCount = reader.ReadInt32();
            Cheers1 = reader.ReadInt32();
            Cheers2 = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgQualifyingWitness);
            writer.Write((ushort)Action); // 4
            writer.Write((ulong)0); // 6
            //writer.Write(Identity); // 10
            writer.Write(Witnesses.Count); // 14
            writer.Write(Cheers1); // 18
            writer.Write(Cheers2); // 22
            foreach (var witness in Witnesses)
            {
                writer.Write(witness.Mesh);
                writer.Write(witness.Name, 16);
                writer.Write(witness.Id);
                writer.Write(witness.Level);
                writer.Write(witness.Profession);
                writer.Write(witness.Rank);
            }
            return writer.ToArray();
        }

        public struct WitnessModel
        {
            public uint Id { get; set; }
            public string Name { get; set; }
            public uint Mesh { get; set; }
            public int Level { get; set; }
            public int Profession { get; set; }
            public int Rank { get; set; }
        }

        public enum WitnessAction : ushort
        {
            RequestView = 0,
            QuitView = 1,
            Watchers = 2,
            Leave = 3,
            Fighters = 4
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            ArenaQualifier qualifier = EventManager.GetEvent<ArenaQualifier>();
            if (qualifier == null)
            {
                return;
            }

            switch (Action)
            {
                case WitnessAction.RequestView:
                    {
                        if (!user.IsAlive)
                        {
                            return;
                        }

                        await qualifier.WatchAsync(user, Identity);
                        break;
                    }

                case WitnessAction.QuitView:
                    {
                        await qualifier.WitnessExitAsync(user);
                        break;
                    }

                case WitnessAction.Fighters:
                    {
                        await qualifier.WitnessVoteAsync(user, Identity);
                        break;
                    }

                default:
                    {
                        logger.Warning($"Invalid MsgArenicWitness:Action ({Action})\n{PacketDump.Hex(Encode())}");
                        break;
                    }
            }
        }
    }
}
