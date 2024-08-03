using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.States.User;
using Long.Shared;
using Long.Shared.Helpers;
using Serilog;

namespace Long.Module.Family.States
{
    public sealed class FamilyMember : IFamilyMember
    {
        private static readonly ILogger logger = Logger.CreateLogger("family_member");

        private DbFamilyAttr familyUserAttribute;

        private FamilyMember() 
        {            
            // only static creation!!
        }

        #region Static Creation

        public static async Task<FamilyMember> CreateAsync(Character player, Family family, IFamily.FamilyRank rank = IFamily.FamilyRank.Member, uint proffer = 0)
        {
            if (player == null || family == null || rank == IFamily.FamilyRank.None)
            {
                return null;
            }

            DbFamilyAttr attr = new()
            {
                FamilyIdentity = family.Identity,
                UserIdentity = player.Identity,
                Proffer = proffer,
                AutoExercise = 0,
                ExpDate = 0,
                JoinDate = (uint)DateTime.Now.ToUnixTimestamp(),
                Rank = (byte)rank
            };
            if (!await ServerDbContext.CreateAsync(attr))
            {
                return null;
            }

            FamilyMember member = new()
            {
                familyUserAttribute = attr,
                Name = player.Name,
                MateIdentity = player.MateIdentity,
                Level = player.Level,
                LookFace = player.Mesh,
                Profession = player.Profession,

                FamilyIdentity = family.Identity,
                FamilyName = family.Name
            };
            if (!await member.SaveAsync())
            {
                return null;
            }

            logger.Information($"[{player.Identity}],[{player.Name}],[{family.Identity}],[{family.Name}],[Join]");
            return member;
        }

        public static async Task<FamilyMember> CreateAsync(DbFamilyAttr player, Family family)
        {
            DbUser dbUser = await UserRepository.FindByIdentityAsync(player.UserIdentity);
            if (dbUser == null)
            {
                return null;
            }

            FamilyMember member = new()
            {
                familyUserAttribute = player,
                Name = dbUser.Name,
                MateIdentity = dbUser.Mate,
                Level = dbUser.Level,
                LookFace = dbUser.Mesh,
                Profession = dbUser.Profession,

                FamilyIdentity = family.Identity,
                FamilyName = family.Name
            };

            return member;
        }

        #endregion

        #region Properties

        public uint Identity => familyUserAttribute.UserIdentity;
        public string Name { get; private set; }
        public byte Level { get; private set; }
        public uint MateIdentity { get; private set; }
        public uint LookFace { get; private set; }
        public ushort Profession { get; private set; }

        public IFamily.FamilyRank Rank
        {
            get => (IFamily.FamilyRank)familyUserAttribute.Rank;
            set => familyUserAttribute.Rank = (byte)value;
        }

        public DateTime JoinDate => UnixTimestamp.ToDateTime(familyUserAttribute.JoinDate);

        public uint Proffer
        {
            get => familyUserAttribute.Proffer;
            set => familyUserAttribute.Proffer = value;
        }

        public Character User => RoleManager.GetUser(Identity);

        public bool IsOnline => User != null;

        public void ChangeName(string name)
        {
            Name = name;
        }

        #endregion

        #region Family Properties

        public uint FamilyIdentity { get; private set; }
        public string FamilyName { get; private set; }

        #endregion

        #region Database

        /// <remarks>ONLY SAVE! AVOID CREATION CALLS</remarks>
        public Task<bool> SaveAsync()
        {
            return ServerDbContext.UpdateAsync(familyUserAttribute);
        }


        public Task<bool> DeleteAsync()
        {
            logger.Information($"[{familyUserAttribute.UserIdentity}],[{FamilyIdentity}],[{FamilyName}],[Exit]");
            return ServerDbContext.DeleteAsync(familyUserAttribute);
        }

        #endregion
    }
}
