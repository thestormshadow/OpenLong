using Long.Kernel.Scripting.Action;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Long.World.Enums;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgTransportor : MsgBase<GameClient>
    {
        public int Action { get; set; }
        public int Unknown8 { get; set; }
        public int Unknown12 { get; set; }
        public int Unknown16 { get; set; }
        public int Unknown20 { get; set; }
        public int Unknown24 { get; set; }
        public int Unknown28 { get; set; }
        public int Unknown32 { get; set; }
        public int Unknown36 { get; set; }
        public int Unknown40 { get; set; }
        public int Unknown44 { get; set; }
        public int Unknown48 { get; set; }
        public int Unknown52 { get; set; }
        public int Unknown56 { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = reader.ReadInt32();
            Unknown8 = reader.ReadInt32();
            Unknown12 = reader.ReadInt32();
            Unknown16 = reader.ReadInt32();
            Unknown20 = reader.ReadInt32();
            Unknown24 = reader.ReadInt32();
            Unknown28 = reader.ReadInt32();
            Unknown32 = reader.ReadInt32();
            Unknown36 = reader.ReadInt32();
            Unknown40 = reader.ReadInt32();
            Unknown44 = reader.ReadInt32();
            Unknown48 = reader.ReadInt32();
            Unknown52 = reader.ReadInt32();
            Unknown56 = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTransportor);
            writer.Write(Action);
            writer.Write(Unknown8);
            writer.Write(Unknown12);
            writer.Write(Unknown16);
            writer.Write(Unknown20);
            writer.Write(Unknown24);
            writer.Write(Unknown28);
            writer.Write(Unknown32);
            writer.Write(Unknown36);
            writer.Write(Unknown40);
            writer.Write(Unknown44);
            writer.Write(Unknown48);
            writer.Write(Unknown52);
            writer.Write(Unknown56);
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            var regions = user.Map.QueryRegions(RegionType.Transportor);
            foreach (var region in regions)
            {
                if (user.GetDistance((int)region.BoundX, (int)region.BoundY) <= region.Data0 / 2)
                {
                    return GameAction.ExecuteActionAsync(region.Data1, user, null, null, string.Empty);
                }
            }
            return Task.CompletedTask;
        }
    }
}
