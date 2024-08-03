using Long.Login.Database.Entities;

namespace Long.Login.Database.Repositories
{
    public static class VipRepository
    {
        public static GameAccountVip GetAccountVip(int accountId)
        {
            using var ctx = new ServerDbContext();
            return ctx.GameAccountVips.Where(x => x.ConquerAccountId == accountId && x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now).Take(1).FirstOrDefault();
        }
    }
}
