using Long.Database.Entities;
using Long.Kernel.Database;

namespace Long.Module.Peerage.Repositories
{
    public class NobilityRepository
    {
        public static List<DbNobility> Get()
        {
            using var context = new ServerDbContext();
            return context.Nobilities.ToList();
        }

        public static async Task CreateAsync(DbNobility dbNobility)
        {
            await using var context = new ServerDbContext();
            context.Nobilities.Add(dbNobility);
            await context.SaveChangesAsync();
        }

        public static async Task UpdateAsync(DbNobility dbNobility)
        {
            await using var context = new ServerDbContext();
            context.Nobilities.Update(dbNobility);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteAsync(DbNobility dbNobility)
        {
            await using var context = new ServerDbContext();
            context.Nobilities.Remove(dbNobility);
            await context.SaveChangesAsync();
        }
    }
}
