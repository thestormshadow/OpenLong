using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class InstanceTypeRepository
    {
        public static DbInstanceType Get(uint instanceType)
        {
            using var ctx = new ServerDbContext();
            return ctx.InstanceTypes.FirstOrDefault(x => x.Id == instanceType);
        }
    }
}
