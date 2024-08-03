using Long.Network.Packets;
using Newtonsoft.Json;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgPkStatistic : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgPkStatistic>();

        public int Timestamp { get; set; }
        public int Action { get; set; }
        public int Count { get; set; }
        public int MaxCount { get; set; }
        public List<PkStatistic> Statistics { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32();
            Action = reader.ReadInt32();
            Count = reader.ReadInt32();
            MaxCount = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgPkStatistic);
            writer.Write(Timestamp);
            writer.Write(Action);
            writer.Write(Statistics.Count);
            writer.Write(MaxCount);
            foreach (var stc in Statistics)
            {
                writer.Write(stc.Name, 16); // 0
                writer.Write(stc.Times); // 16
                writer.Write(int.Parse(UnixTimestamp.ToDateTime(stc.LastKillTime).ToString("yyyyMMHHmm"))); // 20
                writer.Write(stc.BattlePower); // 60
                writer.Write(stc.MapId); // 24
            }
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            switch (Action)
            {
                case 0:
                    {
                        return client.Character.PkStatistic.SubmitAsync(MaxCount);
                    }
                default:
                    {
                        logger.Warning("Action [{Action}] is not being handled.\n{Dump}\n{Json}", Action, PacketDump.Hex(Encode()), JsonConvert.SerializeObject(this));
                        break;
                    }
            }
            return Task.CompletedTask;
        }

        public struct PkStatistic
        {
            public string Name { get; set; }
            public int Times { get; set; }
            public int LastKillTime { get; set; }
            public uint MapId { get; set; }
            public int BattlePower { get; set; }
        }

    }
}
