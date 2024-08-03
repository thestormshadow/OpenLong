using Long.Login.Database.Entities;

namespace Long.Login.Database.Repositories
{
    public static class RealmUserRepository
    {
        public static bool Create(RealmUser user)
        {
            try
            {
                using var ctx = new ServerDbContext();
                ctx.RealmUsers.Add(user);
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
