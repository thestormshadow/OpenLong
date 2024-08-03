using Long.Kernel.Network.Game;
using Long.Module.Pigeon.Managers;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Pigeon.Network
{
    public sealed class MsgPigeon : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgPigeon>();

        public List<string> Strings = new();

        public PigeonMode Mode { get; set; }
        public int Param { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (PigeonMode)reader.ReadInt32();
            Param = reader.ReadInt32();
            Strings = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgPigeon);
            writer.Write((int)Mode);
            writer.Write(Param);
            writer.Write(Strings);
            return writer.ToArray();
        }

        public enum PigeonMode
        {
            None,
            Query,
            QueryUser,
            Send,
            SuperUrgent,
            Urgent
        }

        public override async Task ProcessAsync(GameClient client)
        {
            PigeonMode action = (PigeonMode)((byte)Mode);
            byte subAction = (byte)((int)Mode >> 8);

            switch (action)
            {
                case PigeonMode.Query:
                case PigeonMode.QueryUser:
                    {
                        await PigeonManager.SendListAsync(client.Character, Mode, Param);
                        break;
                    }
                case PigeonMode.Send:
                    {
                        await PigeonManager.PushAsync(client.Character, Strings.FirstOrDefault());
                        await PigeonManager.SendListAsync(client.Character, PigeonMode.Query, 0);
                        break;
                    }
                case PigeonMode.SuperUrgent:
                case PigeonMode.Urgent:
                    {
                        await PigeonManager.AdditionAsync(client.Character, this);
                        await PigeonManager.SendListAsync(client.Character, PigeonMode.Query, 0);
                        break;
                    }
                default:
                    {
                        logger.Error("MsgPigeon {0}\n" + PacketDump.Hex(Encode()), Mode);
                        break;
                    }
            }
        }
    }
}