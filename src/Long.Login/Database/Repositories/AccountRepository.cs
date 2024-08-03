using Long.Login.Database.Entities;

namespace Long.Login.Database.Repositories
{
    public class AccountRepository
    {
        public static GameAccount GetByUsername(string username)
        {
            using var context = new ServerDbContext();
            return context.GameAccounts.Where(x => x.UserName == username).Take(1).FirstOrDefault();
        }

        public static GameAccount GetByID(int id)
        {
            using var context = new ServerDbContext();
            return context.GameAccounts.Where(x => x.Id == id).Take(1).FirstOrDefault();
        }
    }
}
