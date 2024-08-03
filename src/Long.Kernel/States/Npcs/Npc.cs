using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;

namespace Long.Kernel.States.Npcs
{
    public sealed class Npc : BaseNpc
    {
        private readonly DbNpc npc;

        public Npc(DbNpc npc)
            : base(npc.Id)
        {
            this.npc = npc;

            idMap = npc.Mapid;
            X = npc.Cellx;
            Y = npc.Celly;

            Name = npc.Name;
        }

        #region Type

        public override ushort Type => npc.Type;

        public override NpcSort Sort => (NpcSort)npc.Sort;

        public override uint OwnerIdentity
        {
            get => npc.Ownerid;
            set => npc.Ownerid = value;
        }

        #endregion

        #region Map and Position

        public override async Task<bool> ChangePosAsync(uint idMap, ushort x, ushort y)
        {
            if (await base.ChangePosAsync(idMap, x, y))
            {
                npc.Mapid = idMap;
                npc.Celly = y;
                npc.Cellx = x;
                await SaveAsync();
                return true;
            }

            return false;
        }

        #endregion

        #region Task and Data

        public override uint Task0 => npc.Task0;
        public override uint Task1 => npc.Task1;
        public override uint Task2 => npc.Task2;
        public override uint Task3 => npc.Task3;
        public override uint Task4 => npc.Task4;
        public override uint Task5 => npc.Task5;
        public override uint Task6 => npc.Task6;
        public override uint Task7 => npc.Task7;

        public override int Data0
        {
            get => npc.Data0;
            set => npc.Data0 = value;
        }

        public override int Data1
        {
            get => npc.Data1;
            set => npc.Data1 = value;
        }

        public override int Data2
        {
            get => npc.Data2;
            set => npc.Data2 = value;
        }

        public override int Data3
        {
            get => npc.Data3;
            set => npc.Data3 = value;
        }

        public override string DataStr
        {
            get => npc.Datastr;
            set => npc.Datastr = value;
        }

        #endregion

        #region Socket

        public override async Task SendSpawnToAsync(Character player)
        {
            await player.SendAsync(new MsgNpcInfo
            {
                Identity = Identity,
                Lookface = (ushort)npc.Lookface,
                Sort = npc.Sort,
                PosX = X,
                PosY = Y,
                NpcType = npc.Type,
                ServerId = (uint)(Type == FRONTIER_SERVER_TRANS_NPC ? Data0 : 0)
            });
        }

        #endregion

        #region Database

        public override async Task<bool> SaveAsync()
        {
            return await ServerDbContext.UpdateAsync(npc);
        }

        public override async Task<bool> DeleteAsync()
        {
            return await ServerDbContext.DeleteAsync(npc);
        }

        #endregion
    }
}
