using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.User;
using System.Globalization;

namespace Long.Module.Qualifying.Network.States
{
    public sealed class QualifierInformation
	{
        private readonly DbArenic arenic;

        public QualifierInformation(DbArenic arenic)
        {
            this.arenic = arenic;
        }

        public QualifierInformation(Character user, byte type)
        {
            arenic = new DbArenic
            {
                UserId = user.Identity,
                Date = uint.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Type = type
            };

            Name = user.Name;
            Mesh = user.Mesh;
            Level = user.Level;
            Profession = user.Profession;
        }

        public uint UserId => arenic.UserId;

        public string Name { get; private set; }
        public uint Mesh { get; private set; }
        public byte Level { get; private set; }
        public byte Profession { get; private set; }

        public DateTime Date => DateTime.ParseExact(arenic.Date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

        public uint AthletePoint 
        {
            get => arenic.AthletePoint; 
            set => arenic.AthletePoint = value;
        }

        public uint CurrentHonor 
        {
            get => arenic.CurrentHonor;
            set => arenic.CurrentHonor = value;
        }

        public uint HistoryHonor 
        {
            get => arenic.HistoryHonor;
            set => arenic.HistoryHonor = value;
        }

        public uint DayWins 
        {
            get => arenic.DayWins;
            set => arenic.DayWins = value;
        }

        public uint DayLoses 
        {
            get => arenic.DayLoses; 
            set => arenic.DayLoses = value;
        }

        public async Task<bool> InitializeAsync()
        {
            DbUser user = await UserRepository.FindByIdentityAsync(UserId);
            if (user == null)
            {
                return false;
            }

            Name = user.Name;
            Mesh = user.Mesh;
            Level = user.Level;
            Profession = user.Profession;
            return true;
        }

        public Task<bool> SaveAsync()
        {
            return ServerDbContext.UpdateAsync(this.arenic);
        }

        public Task<bool> DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(this.arenic);
        }
    }
}
