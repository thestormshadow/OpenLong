using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class UnionRepository
    {
        public static async Task<List<DbLeague>> GetLeaguesAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Leagues.ToListAsync();
        }

        public static async Task<List<DbLeagueMember>> GetLeagueMembersAsync(uint league)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.LeagueMembers.Where(x => x.LeagueId == league).ToListAsync();
        }

        public static async Task<List<DbOfficialPosition>> GetLeagueOfficialsAsync(uint league)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.OfficialPositions.Where(x => x.LeagueId == league).ToListAsync();
        }

        public static async Task<List<DbTokenType>> GetTokenTypesAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.TokenTypes.ToListAsync();
        }

        public static async Task<List<DbLeagueContribute>> GetContributeListAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.LeagueContributes.OrderBy(x => x.NeedContribute).ToListAsync();
        }

        public static async Task<List<DbOfficialType>> GetOfficialTypesAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.OfficialTypes.ToListAsync();
        }
    }
}
