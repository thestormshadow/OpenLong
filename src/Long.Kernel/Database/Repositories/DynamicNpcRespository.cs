using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class DynamicNpcRespository
    {
        public static async Task<List<DbDynanpc>> GetAsync()
        {
            await using var context = new ServerDbContext();
            return context.DynamicNpcs.ToList();
        }
    }
}
