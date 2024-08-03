using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class NewbieInfoRepository
    {
        public static DbNewbieInfo Get(uint prof)
        {
            using var ctx = new ServerDbContext();
            return ctx.NewbieInfo.FirstOrDefault(x => x.Profession == prof);
        }
    }
}
