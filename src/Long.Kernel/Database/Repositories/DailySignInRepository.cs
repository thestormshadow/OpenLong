using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public class DailySignInRepository
    {
        public static DbSignInEveryday Get(uint playerId)
        {
            using var ctx = new ServerDbContext();
            return ctx.SignInEverydays.Where(x => x.PlayerId == playerId).Take(1).FirstOrDefault();
        }
    }
}
