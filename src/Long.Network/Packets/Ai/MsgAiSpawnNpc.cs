using Long.Network.Sockets;

namespace Long.Network.Packets.Ai
{
    public abstract class MsgAiSpawnNpc<T> : MsgBase<T> where T : TcpServerActor
	{
        public AiSpawnNpcMode Mode { get; set; }
        public int Count { get; set; }
        public List<SpawnNpc> List { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (AiSpawnNpcMode)reader.ReadInt32();
            Count = reader.ReadInt32();
            for (var i = 0; i < Count; i++)
                if (Mode == AiSpawnNpcMode.Spawn)
                {
                    uint id = reader.ReadUInt32();
                    uint generator = reader.ReadUInt32();
                    uint monsterType = reader.ReadUInt32();
                    uint idMap = reader.ReadUInt32();
                    ushort x = reader.ReadUInt16();
                    ushort y = reader.ReadUInt16();
                    uint ownerId = reader.ReadUInt32();
                    uint ownerType = reader.ReadUInt32();
                    uint data = reader.ReadUInt32();

                    List.Add(new SpawnNpc
                    {
                        Id = id,
                        GeneratorId = generator,
                        MonsterType = monsterType,
                        MapId = idMap,
                        X = x,
                        Y = y,
                        OwnerId = ownerId,
                        OwnerType = ownerType,
                        Data = data
                    });
                }
                else if (Mode == AiSpawnNpcMode.DestroyNpc)
                {
                    List.Add(new SpawnNpc
                    {
                        Id = reader.ReadUInt32()
                    });
                }
                else if (Mode == AiSpawnNpcMode.DestroyGenerator)
                {
                    List.Add(new SpawnNpc
                    {
                        GeneratorId = reader.ReadUInt32()
                    });
                }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgAiSpawnNpc);
            writer.Write((int)Mode);
            writer.Write(Count = List.Count);
            foreach (SpawnNpc item in List)
                if (Mode == AiSpawnNpcMode.Spawn)
                {
                    writer.Write(item.Id);
                    writer.Write(item.GeneratorId);
                    writer.Write(item.MonsterType);
                    writer.Write(item.MapId);
                    writer.Write(item.X);
                    writer.Write(item.Y);
                    writer.Write(item.OwnerId);
                    writer.Write(item.OwnerType);
                    writer.Write(item.Data);
                }
                else if (Mode == AiSpawnNpcMode.DestroyNpc)
                {
                    writer.Write(item.Id);
                }
                else if (Mode == AiSpawnNpcMode.DestroyGenerator)
                {
                    writer.Write(item.GeneratorId);
                }

            return writer.ToArray();
        }

        public struct SpawnNpc
        {
            public uint Id { get; set; }
            public uint GeneratorId { get; set; }
            public uint MonsterType { get; set; }
            public uint MapId { get; set; }
            public ushort X { get; set; }
            public ushort Y { get; set; }
            public uint OwnerId { get; set; }
            public uint OwnerType { get; set; }
            public uint Data { get; set; }
        }
    }

    public enum AiSpawnNpcMode
    {
        /// <summary>
        ///     Request to spawn the specified NPCs.
        /// </summary>
        Spawn,

        /// <summary>
        ///     Request to remove the specified NPCs.
        /// </summary>
        DestroyNpc,

        /// <summary>
        ///     Request to remove the NPCs from the specified generator.
        /// </summary>
        DestroyGenerator
    }
}
