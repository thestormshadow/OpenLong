using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public class UserRepository
    {
        private static readonly ILogger logger = Log.ForContext<UserRepository>();

        /// <summary>
        ///     Fetches a character record from the database using the character's name as a
        ///     unique key for selecting a single record. Character name is indexed for fast
        ///     lookup when logging in.
        /// </summary>
        /// <param name="name">Character's name</param>
        /// <returns>Returns character details from the database.</returns>
        public static async Task<DbUser> FindAsync(string name)
        {
            await using var db = new ServerDbContext();
            return await db.Users
                .Where(x => x.Name == name)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        ///     Fetches a character record from the database using the character's associated
        ///     AccountID as a unique key for selecting a single record.
        /// </summary>
        /// <param name="accountID">Primary key for fetching character info</param>
        /// <returns>Returns character details from the database.</returns>
        public static async Task<DbUser> FindAsync(uint accountID)
        {
            await using var db = new ServerDbContext();
            return await db.Users
                .Where(x => x.AccountIdentity == accountID)
                .SingleOrDefaultAsync();
        }

        public static async Task<DbUser> FindByIdentityAsync(uint id)
        {
            await using var db = new ServerDbContext();
            return await db.Users
                .Where(x => x.Identity == id)
                .SingleOrDefaultAsync();
        }

        /// <summary>Checks if a character exists in the database by name.</summary>
        /// <param name="name">Character's name</param>
        /// <returns>Returns true if the character exists.</returns>
        public static async Task<bool> ExistsAsync(string name)
        {
            await using var db = new ServerDbContext();
            return await db.Users
                .Where(x => x.Name == name)
                .AnyAsync();
        }

        /// <summary>
        ///     Creates a new character using a character model. If the character primary key
        ///     already exists, then character creation will fail.
        /// </summary>
        /// <param name="character">Character model to be inserted to the database</param>
        public static async Task CreateAsync(DbUser character)
        {
            await using var db = new ServerDbContext();
            db.Users.Add(character);
            await db.SaveChangesAsync();
        }

        public static async Task<bool> UpdateAsync(DbUser character)
        {
            try
            {
                await using var db = new ServerDbContext();
                db.Users.Update(character);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on Update user! {0}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> DeleteAsync(DbUser character)
        {
            try
            {
                await using var db = new ServerDbContext();
                db.Users.Remove(character);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on delete user! {0}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> UpdateAsync(uint idUser, string column, object value)
        {
            try
            {
                await using var context = new ServerDbContext();
                await context.Database.ExecuteSqlRawAsync($"UPDATE `cq_user` SET `{column}` = ? WHERE `id` = ? LIMIT 1;", value, idUser);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on UserRepository.UpdateAsync(uint, string, object): {0}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> IncrementAsync(uint idUser, string column, ulong value)
        {
            try
            {
                await using var context = new ServerDbContext();
                await context.Database.ExecuteSqlRawAsync($"UPDATE `cq_user` SET `{column}` = `{column}` + ? WHERE `id` = ? LIMIT 1;", value, idUser);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on UserRepository.IncrementAsync(uint, string, ulong): {0}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> DecrementAsync(uint idUser, string column, ulong value)
        {
            try
            {
                await using var context = new ServerDbContext();
                await context.Database.ExecuteSqlRawAsync($"UPDATE `cq_user` SET `{column}` = `{column}` - ? WHERE `id` = ? LIMIT 1;", value, idUser);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on UserRepository.DecrementAsync(uint, string, ulong): {0}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> SavePositionAsync(uint idUser, uint idMap, int x, int y)
        {
            try
            {
                await using var context = new ServerDbContext();
                await context.Database.ExecuteSqlRawAsync($"UPDATE `cq_user` SET `recordmap_id` = ?, `recordx` = ?, `recordy` = ? WHERE `id` = ? LIMIT 1;", idMap, x, y, idUser);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on UserRepository.SavePositionAsync(uint, uint, int, int): {0}", ex.Message);
                return false;
            }
        }

		public static async Task<List<DbUser>> GetHonorRankAsync(int from, int limit)
		{
			await using var db = new ServerDbContext();
			return await db.Users
							.Where(x => x.AthleteHistoryHonorPoints > 0)
							.OrderByDescending(x => x.AthleteHistoryHonorPoints)
							.ThenByDescending(x => x.AthleteHistoryWins)
							.ThenBy(x => x.AthleteHistoryLoses)
							.Skip(from)
							.Take(limit)
							.ToListAsync();
		}

		public static async Task<int> GetHonorRankCountAsync()
		{
			await using var db = new ServerDbContext();
			return await db.Users
							.Where(x => x.AthleteHistoryHonorPoints > 0)
							.OrderByDescending(x => x.AthleteHistoryHonorPoints)
							.ThenByDescending(x => x.AthleteHistoryWins)
							.ThenBy(x => x.AthleteHistoryLoses)
							.CountAsync();
		}
	}
}
