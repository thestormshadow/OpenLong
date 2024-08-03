using Long.Database.Entities;
using Long.Kernel.Database;

namespace Long.Kernel.States.Realm
{
    public sealed class KingdomOfficial
    {
        private readonly DbOfficialPosition officialPosition;

        public KingdomOfficial(DbOfficialPosition officialPosition) 
        {
            this.officialPosition = officialPosition;
        }

        public uint UserId => officialPosition.PlayerId;

        public uint LeagueId => officialPosition.LeagueId;

        public OfficialPosition OfficialType
        {
            get => (OfficialPosition)officialPosition.OfficialType;
            set
            {
                officialPosition.OfficialType = (ushort)value;
                officialPosition.OfficialTime = uint.Parse(DateTime.Now.ToString("yyMMdd"));
            }
        }

        public OfficialPositionFlag OfficialTypeFlag => GetOfficialTypeFlag(OfficialType);

        public static OfficialPositionFlag GetOfficialTypeFlag(OfficialPosition position)
        {
            switch (position)
            {
                case OfficialPosition.Emperor: return OfficialPositionFlag.Emperor;
                case OfficialPosition.LeftMinister: return OfficialPositionFlag.LeftMinister;
                case OfficialPosition.RightMinister: return OfficialPositionFlag.RightMinister;
                case OfficialPosition.LeftMarshal: return OfficialPositionFlag.LeftMarshal;
                case OfficialPosition.RightMarshal: return OfficialPositionFlag.RightMarshal;
                case OfficialPosition.SecurityGeneral: return OfficialPositionFlag.SecurityGeneral;
                case OfficialPosition.DefenseGeneral: return OfficialPositionFlag.DefenseGeneral;
                case OfficialPosition.PacificationGeneral: return OfficialPositionFlag.PacificationGeneral;
                case OfficialPosition.ConquerGeneral: return OfficialPositionFlag.PacificationGeneral;
                case OfficialPosition.SeniorConcubine1:
                case OfficialPosition.SeniorConcubine2: return OfficialPositionFlag.SeniorConcubine;
                case OfficialPosition.Concubine1:
                case OfficialPosition.Concubine2:
                case OfficialPosition.Concubine3:
                case OfficialPosition.Concubine4:
                case OfficialPosition.Concubine5:
                case OfficialPosition.Concubine6:
                case OfficialPosition.Concubine7: return OfficialPositionFlag.Concubine;
                default: return 0;
            }
        }

        public uint SalaryTime
        {
            get => officialPosition.SalaryTime;
            set => officialPosition.SalaryTime = value;
        }

        public uint OfficialTime
        {
            get => officialPosition.OfficialTime;
            set => officialPosition.OfficialTime = value;
        }

        public Task SaveAsync() => ServerDbContext.UpdateAsync(officialPosition);
        public Task DeleteAsync() => ServerDbContext.DeleteAsync(officialPosition);

        public enum OfficialPosition
        {
            Member,
            Emperor = 1000,
            LeftMinister = 2000,
            RightMinister = 2010,
            LeftMarshal = 2020,
            RightMarshal = 2030,
            SecurityGeneral = 2040,
            DefenseGeneral = 2050,
            PacificationGeneral = 2060,
            ConquerGeneral = 2070,
            ImperialGuard = 3000,
            Empress = 4000,
            SeniorConcubine1 = 4011,
            SeniorConcubine2 = 4012,
            Concubine1 = 4021,
            Concubine2 = 4022,
            Concubine3 = 4023,
            Concubine4 = 4024,
            Concubine5 = 4025,
            Concubine6 = 4026,
            Concubine7 = 4027
        }

        [Flags]
        public enum OfficialPositionFlag : uint
        {
            Member = 0,
            Emperor = 0x1,
            LeftMinister = 0x2,
            RightMinister = 0x4,
            LeftMarshal = 0x8,
            RightMarshal = 0x10,
            SecurityGeneral = 0x20,
            DefenseGeneral = 0x40,
            PacificationGeneral = 0x80,
            ConquerGeneral = 0x100,
            ImperialGuard = 0x200,
            Empress = 0x400,
            SeniorConcubine = 0x800,
            Concubine = 0x1000,
        }
    }
}
