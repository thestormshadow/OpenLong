using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.Modules.Systems.Rank;
using Long.Kernel.States.User;

namespace Long.Module.Fate.States
{
    public sealed class FateRankData
    {
        private readonly DbDynaRankRec rank;

        public FateRankData(DbDynaRankRec rank)
        {
            this.rank = rank;
        }

        public FateRankData(Character user, IFate.FateType fateType)
        {
            this.rank = new DbDynaRankRec
            {
                ObjId = user.Identity,
                UserId = user.Identity
            };

            Identity = user.Identity;
            Name = user.Name;
            Level = user.Level;
            Mesh = user.Mesh;

            switch (fateType)
            {
                case IFate.FateType.Dragon: rank.RankType = IDynamicRankManager.DragonFate; break;
                case IFate.FateType.Phoenix: rank.RankType = IDynamicRankManager.PhoenixFate; break;
                case IFate.FateType.Tiger: rank.RankType = IDynamicRankManager.TigerFate; break;
                case IFate.FateType.Turtle: rank.RankType = IDynamicRankManager.TurtleFate; break;
            }
        }

        public uint Identity { get; private set; }
        public string Name { get; private set; }
        public byte Level { get; private set; }
        public uint Mesh {  get; private set; }
        public uint Value 
        { 
            get => (uint)Math.Max(0, Math.Min(400, rank.Value)); 
            set => rank.Value = Math.Max(0, Math.Min(400, value)); 
        }

        public async Task<bool> InitializeAsync()
        {
            var user = await UserRepository.FindByIdentityAsync(rank.UserId);
            if (user == null)
            {
                return false;
            }

            Identity = user.Identity;
            Name = user.Name;
            Level = user.Level;
            Mesh = user.Mesh;
            return true;
        }

        public Task SaveAsync()
        {
            if (rank.Id == 0)
            {
                return ServerDbContext.CreateAsync(rank);
            }
            return ServerDbContext.UpdateAsync(rank);
        }

        public Task DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(rank);
        }
    }
}
