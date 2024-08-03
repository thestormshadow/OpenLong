namespace Long.Module.Booth.Network
{
    public sealed class MsgNpcInfoEx : Kernel.Network.Game.Packets.MsgNpcInfoEx
    {
        public MsgNpcInfoEx(States.Booth npc)
        {
            Identity = npc.Identity;
            MaxLife = npc.MaxLife;
            Life = npc.Life;
            PosX = npc.X;
            PosY = npc.Y;
            Lookface = (ushort)npc.Mesh;
            NpcType = npc.Type;
            Sort = (ushort)npc.Sort;
            Name = npc.Name;
        }
    }
}
