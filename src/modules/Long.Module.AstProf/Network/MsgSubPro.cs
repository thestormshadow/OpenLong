using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Serilog;
using static Long.Kernel.Modules.Systems.AstProf.IAstProf;

namespace Long.Module.AstProf.Network
{
    public sealed class MsgSubPro : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgSubPro>();

        public int Timestamp { get; set; }
        public AstProfAction Action { get; set; }
        public ulong Points { get; set; }
        public AstProfType Class
        {
            get => (AstProfType)Points;
            set => Points = (ulong)value;
        }
        public byte Level
        {
            get => BitConverter.GetBytes(Points)[1];
            set
            {
                byte[] val = BitConverter.GetBytes(Points);
                val[1] = value;
                Points = BitConverter.ToUInt64(val);
            }
        }
        public ulong Study { get; set; }
        public ulong AwardedStudy { get; set; }
        public int Count { get; set; }
        public List<AstProfStruct> Professions { get; set; } = new List<AstProfStruct>();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32(); // 4
            Action = (AstProfAction)reader.ReadUInt16(); // 8
            Points = reader.ReadUInt64(); // 10
            Study = reader.ReadUInt64(); // 18
            Count = reader.ReadByte(); // 26
            for (int i = 0; i < Count; i++)
            {
                Professions.Add(new AstProfStruct
                {
                    Class = (AstProfType)reader.ReadByte(),
                    Level = reader.ReadByte(),
                    Rank = reader.ReadByte()
                });
            }
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgSubPro);
            writer.Write(Timestamp); // 4
            writer.Write((ushort)Action); // 8
            writer.Write(Points); // 10
            writer.Write(Study); // 18
            writer.Write(Count = Professions.Count); // 26
            for (int i = 0; i < Professions.Count; i++)
            {
                writer.Write((byte)Professions[i].Class);
                writer.Write(Professions[i].Level);
                writer.Write(Professions[i].Rank);
            }
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user.IsOSUser())
            {
                return Task.CompletedTask;
            }

            switch (Action)
            {
                case AstProfAction.Switch: return user.AstProf.ActivateAsync(Class);
                case AstProfAction.RequestUplev: return user.AstProf.UpLevAsync(Class);
                case AstProfAction.MartialPromoted: return user.AstProf.PromoteAsync(Class);
                case AstProfAction.Info: return user.AstProf.SendAsync();
                case AstProfAction.LearnRemote: return user.AstProf.LearnAsync(Class);
                case AstProfAction.PromoteRemote: return user.AstProf.PromoteAsync(Class);
                default:
                    {
                        logger.Warning("MsgSubPro Unhandled action [{0}].\r\n" + PacketDump.Hex(Encode()), Action);
                        return Task.CompletedTask;
                    }
            }
        }

        public struct AstProfStruct
        {
            public AstProfType Class { get; set; }
            public byte Level { get; set; }
            public byte Rank { get; set; }
        }

        public enum AstProfAction : ushort
        {
            Switch = 0,
            Activate = 1,
            RequestUplev = 2,
            MartialUplev = 3,
            Learn = 4,
            MartialPromoted = 5,
            Info = 6,
            ShowGui = 7,
            UpdateStudy = 8,
            LearnRemote = 9,
            PromoteRemote = 10
        }
    }
}
