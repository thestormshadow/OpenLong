using Long.Database.Entities;
using Long.Database;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using Serilog;

namespace Long.Ai.Database
{
    public class ServerDbContext : AbstractDbContext
    {
        private static readonly Serilog.ILogger logger = Log.ForContext<ServerDbContext>();

        public virtual DbSet<DbMap> Maps { get; set; }
        public virtual DbSet<DbDynamap> DynaMaps { get; set; }
        public virtual DbSet<DbRegion> Regions { get; set; }
        public virtual DbSet<DbConfig> Configs { get; set; }
        public virtual DbSet<DbMagictype> Magictype { get; set; }
        public virtual DbSet<DbMonstertype> Monstertype { get; set; }
        public virtual DbSet<DbMonsterTypeMagic> MonsterTypeMagic { get; set; }
        public virtual DbSet<DbGenerator> Generator { get; set; }
        public virtual DbSet<DbMonsterCluster> MonsterCluster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static bool Ping()
        {
            using var ctx = new ServerDbContext();
            try
            {
                return ctx.Database.CanConnect();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "{Message}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> CreateAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                serverDbContext.Add(entity);
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "CreateAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public async static Task<bool> CreateAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                foreach (var entity in entities)
                {
                    serverDbContext.Add(entity);
                }
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "CreateAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public async static Task<bool> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                serverDbContext.Update(entity);
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "UpdateAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> UpdateAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                foreach (var entity in entities)
                {
                    serverDbContext.Update(entity);
                }
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "UpdateAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> SaveAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                serverDbContext.Update(entity);
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "SaveAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> SaveRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                foreach (var entity in entities)
                {
                    serverDbContext.Update(entity);
                }
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "SaveAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                serverDbContext.Remove(entity);
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "DeleteAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public static async Task<bool> DeleteRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            where T : class
        {
            try
            {
                await using var serverDbContext = new ServerDbContext();
                foreach (var entity in entities)
                {
                    serverDbContext.Remove(entity);
                }
                await serverDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "DeleteAsync has throw: {ExceptionMessage}", ex.Message);
                return false;
            }
        }

        public static async Task<string> ScalarAsync(string query)
        {
            await using var db = new ServerDbContext();
            DbConnection connection = db.Database.GetDbConnection();
            ConnectionState state = connection.State;

            string result;
            try
            {
                if ((state & ConnectionState.Open) == 0)
                {
                    await connection.OpenAsync();
                }

                DbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;

                result = (await cmd.ExecuteScalarAsync())?.ToString();
            }
            finally
            {
                if (state != ConnectionState.Closed)
                {
                    await connection.CloseAsync();
                }
            }

            return result;
        }

        public static async Task<DataTable> SelectAsync(string query)
        {
            await using var db = new ServerDbContext();
            var result = new DataTable();
            DbConnection connection = db.Database.GetDbConnection();
            ConnectionState state = connection.State;

            try
            {
                if (state != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                DbCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                await using DbDataReader reader = await command.ExecuteReaderAsync();
                result.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (state != ConnectionState.Closed)
                {
                    await connection.CloseAsync();
                }
            }

            return result;
        }
    }
}
