using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public class LeavewordRepository
    {
        public static List<DbLeaveword> Get(uint idUser)
        {
            using var context = new ServerDbContext();
            return context.Leavewords.Where(x => x.RecvId == idUser).ToList();
        }

        public static List<DbLeaveword> GetWords(string senderName, uint targetId)
        {
            using var context = new ServerDbContext();
            return context.Leavewords.Where(x => x.RecvId == targetId && x.SendName == senderName).ToList();
        }

        public static List<DbSysLeaveword> GetSys(uint idUser)
        {
            using var context = new ServerDbContext();
            return context.SysLeavewords.Where(x => x.UserId == idUser).ToList();
        }
    }
}
