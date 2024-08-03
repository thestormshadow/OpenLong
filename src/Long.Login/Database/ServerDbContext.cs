using Long.Database;
using Long.Login.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Login.Database
{
    public class ServerDbContext : AbstractDbContext
    {
        public virtual DbSet<GameAccount> GameAccounts { get; set; }
        public virtual DbSet<GameAccountAuthority> GameAccountsAuthority { get; set; }
        public virtual DbSet<GameAccountVip> GameAccountVips { get; set; }
        public virtual DbSet<RealmData> RealmDatas { get; set; }
        public virtual DbSet<CityLocation> CityLocations { get; set; }
        public virtual DbSet<GameAccountLoginRecord> GameAccountLoginRecords { get; set; }
        public virtual DbSet<RealmUser> RealmUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RealmData>().HasKey(x => new { x.RealmID, x.RealmIdx });
        }
    }
}