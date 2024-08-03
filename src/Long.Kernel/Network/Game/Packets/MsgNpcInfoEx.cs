using Long.Kernel.States.Npcs;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public class MsgNpcInfoEx : MsgBase<GameClient>
    {
        public MsgNpcInfoEx()
        {            
        }

        public MsgNpcInfoEx(DynamicNpc npc)
        {
            Identity = npc.Identity;
            MaxLife = npc.MaxLife;
            Life = npc.Life;
            PosX = npc.X;
            PosY = npc.Y;
            Lookface = (ushort)npc.Mesh;
            NpcType = npc.Type;
            Sort = (ushort)npc.Sort;
            if (npc.IsSynFlag() || npc.IsCtfFlag() || npc.Type == BaseNpc.COMPETE_BARRIER_NPC_)
            {
                Name = npc.Name;
            }
            else
            {
                Name = string.Empty;
            }
        }

        public uint Identity { get; set; }
        public uint MaxLife { get; set; }
        public uint Life { get; set; }
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public ushort Lookface { get; set; }
        public ushort NpcType { get; set; }
        public ushort Sort { get; set; }
        public string Name { get; set; }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgNpcInfoEx); // 2
            writer.Write(Identity); // 4
            writer.Write(0);        // 8
            writer.Write(MaxLife); // 12
            writer.Write(Life); // 16
            writer.Write(PosX); // 20
            writer.Write(PosY); // 22
            writer.Write(Lookface); // 24
            writer.Write(NpcType); // 26
            writer.Write(Sort); // 28
            writer.Write(new byte[10]);
            if (!string.IsNullOrEmpty(Name))
            {
                writer.Write(new List<string> { Name });
            }
            else
            {
                writer.Write(0);
            }

            return writer.ToArray();
        }
    }
}
