using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Module.Relation.Network
{
    public sealed class MsgEnemyList : MsgBase<GameClient>
    {
        public uint Identity { get; set; }
        public int Count => Enemies.Count;
        public List<EnemyData> Enemies { get; set; } = new List<EnemyData>();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Identity = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgEnemyList);
            writer.Write(Identity);
            writer.Write(Count);
            foreach (var enemy in Enemies)
            {
                writer.Write(enemy.IsOnline ? 1 : 0);
                writer.Write(enemy.Identity);
                writer.Write(enemy.Nobility);
                writer.Write(enemy.Gender);
                writer.Write(enemy.Name, 16);
                writer.Write(enemy.Mesh);
                writer.Write(enemy.Level);
            }
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user?.Relation != null)
            {
                return user.Relation.SendAllEnemyAsync();
            }
            return Task.CompletedTask;
        }

        public struct EnemyData
        {
            public bool IsOnline { get; set; }
            public uint Identity { get; set; }
            public int Nobility { get; set; }
            public int Gender { get; set; }
            public uint Mesh { get; set; }
            public int Level { get; set; }
            public string Name { get; set; }
        }
    }
}
