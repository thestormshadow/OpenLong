using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class SupermanRepository
    {
        public static async Task<List<DbSuperman>> GetAsync()
        {
            await using ServerDbContext serverDbContext = new();
            return await serverDbContext.Supermen.ToListAsync();
        }
    }
}
