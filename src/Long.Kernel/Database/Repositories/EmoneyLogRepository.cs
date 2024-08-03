using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public class EmoneyLogRepository
    {
        public static DbEMoney GetEmoneyCheckSum(uint idUser)
        {
            using var context = new ServerDbContext();
            return context.EMoneyLogs
                .Where(x => x.IdSource == idUser || x.IdTarget == idUser)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
        }
    }
}
