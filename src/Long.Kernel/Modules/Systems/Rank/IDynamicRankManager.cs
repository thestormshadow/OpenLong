using Long.Database.Entities;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Rank
{
    public interface IDynamicRankManager
    {
        public const int RedRose    = 30000002;
        public const int WhiteRose  = 30000102;
        public const int Orchid     = 30000202;
        public const int Tulip      = 30000302;

        public const int Kiss = 30000402;
        public const int Love = 30000502;
        public const int Tins = 30000602;
        public const int Jade = 30000702;

        public const int DragonFate = 60000001;
        public const int PhoenixFate = 60000002;
        public const int TigerFate = 60000003;
        public const int TurtleFate = 60000004;

        public const int InnerStrength = 70000000;

        Task CreateOrUpdateAsync(uint rankType, uint idUser, long value);
        int QueryUserRank(uint rankType, uint userId, int limit);
        List<DbDynaRankRec> GetRank(uint rankType);
        Task SubmitUserFateRankAsync(Character user, IFate.FateType fateType);
        Task SubmitUserRankAsync(Character user, int rankType);
    }
}
