using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Guide.Repositories
{
    public static class TutorRepository
    {
        public static async Task<List<DbTutor>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Tutor.ToListAsync();
        }

        public static async Task<DbTutor> GetAsync(uint idStudent)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Tutor.FirstOrDefaultAsync(x => x.StudentId == idStudent);
        }

        public static async Task<List<DbTutor>> GetStudentsAsync(uint idTutor)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Tutor
                            .Where(x => x.GuideId == idTutor)
                            .ToListAsync();
        }
    }
}
