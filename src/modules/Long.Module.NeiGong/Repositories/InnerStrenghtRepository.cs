using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.NeiGong.Repositories
{
    public static class InnerStrenghtRepository
    {
        public static async Task<List<DbInnerStrenghtSecret>> GetPlayerSecretsAsync(uint idPlayer)
        {
            await using var db = new ServerDbContext();
            return await db.InnerStrenghtSecrets.Where(x => x.PlayerIdentity == idPlayer).ToListAsync();
        }

        public static async Task<List<DbInnerStrenghtPlayer>> GetPlayerInnersAsync(uint idPlayer)
        {
            await using var db = new ServerDbContext();
            return await db.InnerStrenghtPlayers.Where(x => x.PlayerId == idPlayer).ToListAsync();
        }

        public static async Task<List<DbInnerStrenghtSecretType>> GetSecretTypeAsync()
        {
            await using var db = new ServerDbContext();
            return await db.InnerStrenghtSecretTypes.ToListAsync();
        }

        public static async Task<List<DbInnerStrenghtTypeInfo>> GetTypesAsync()
        {
            await using var db = new ServerDbContext();
            return await db.InnerStrenghtTypeInfos.ToListAsync();
        }

        public static async Task<List<DbInnerStrenghtTypeLevInfo>> GetTypeLevAsync()
        {
            await using var db = new ServerDbContext();
            return await db.InnerStrenghtTypeLevInfos.ToListAsync();
        }

        public static async Task<List<DbInnerStrengthRand>> GetRandRangeAsync()
        {
            await using var db = new ServerDbContext();
            return await db.InnerStrengthRands.ToListAsync();
        }
    }
}
