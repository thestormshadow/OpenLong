using Microsoft.EntityFrameworkCore;

namespace Long.Database
{
    public abstract class AbstractDbContext : DbContext
    {
        private static string ConnectionString => $"server={Configuration.Hostname};database={Configuration.Schema};user={Configuration.Username};password={Configuration.Password};port={Configuration.Port}";

        /// <summary>
        ///     Configures the database to be used for this context. This method is called
        ///     for each instance of the context that is created. For this project, the MySQL
        ///     connector will be initialized with a connection string from the server's
        ///     configuration file.
        /// </summary>
        /// <param name="options">Builder to create the context</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies(false);
            options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }

        public static DatabaseConfiguration Configuration { get; set; }
    }
}
