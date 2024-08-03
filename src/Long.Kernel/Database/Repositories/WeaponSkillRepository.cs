using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class WeaponSkillRepository
    {
        public static async Task<List<DbWeaponSkill>> GetAsync(uint idUser)
        {
            await using var db = new ServerDbContext();
            return db.WeaponSkills.Where(x => x.OwnerIdentity == idUser).ToList();
        }

        public static async Task<DbWeaponSkillUp> GetAsync(ushort weaponType, int level)
        {
            await using var db = new ServerDbContext();
            return await db.WeaponSkillUps.FirstOrDefaultAsync(x => x.WeaponType == weaponType && x.Level == level);
        }
    }
}
