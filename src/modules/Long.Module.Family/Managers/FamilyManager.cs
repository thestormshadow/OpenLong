using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Managers;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.States.User;
using Long.Module.Family.Network;
using Long.Module.Family.Repositories;
using System.Collections.Concurrent;

namespace Long.Module.Family.Managers
{
    public sealed class FamilyManager : IFamilyManager
    {
        private readonly ConcurrentDictionary<uint, IFamily> families = new();
        private readonly ConcurrentDictionary<uint, DbFamilyBattleEffectShareLimit> familyBattlePowerLimit = new();

        public IFamilyWarManager FamilyWar => FamilyWarManager.Instance;

        public async Task<bool> InitializeAsync()
        {
            List<DbFamily> dbFamilies = await FamilyRepository.GetAsync();
            foreach (DbFamily dbFamily in dbFamilies)
            {
                var family = await States.Family.CreateAsync(dbFamily);
                if (family != null)
                {
                    families.TryAdd(family.Identity, family);
                }
            }

            foreach (IFamily family in families.Values)
            {
                family.LoadRelations();
            }

            foreach (DbFamilyBattleEffectShareLimit limit in await FamilyBattleEffectShareLimitRepository.GetAsync())
            {
                if (!familyBattlePowerLimit.ContainsKey(limit.Identity))
                {
                    familyBattlePowerLimit.TryAdd(limit.Identity, limit);
                }
            }

            return true;
        }

        public bool AddFamily(IFamily family)
        {
            return families.TryAdd(family.Identity, family);
        }

        public IFamily FindByUser(uint idUser)
        {
            return families.Values.FirstOrDefault(x => x.GetMember(idUser) != null);
        }

        public IFamily GetFamily(uint idFamily)
        {
            return families.TryGetValue((ushort)idFamily, out var family) ? family : null;
        }

        public IFamily GetFamily(string name)
        {
            return families.Values.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public IFamily GetOccupyOwner(uint idNpc)
        {
            return families.Values.FirstOrDefault(x => x.Occupy == idNpc);
        }

        public DbFamilyBattleEffectShareLimit GetSharedBattlePowerLimit(int level)
        {
            return familyBattlePowerLimit.Values.FirstOrDefault(x => x.Identity == level);
        }

        public IList<IFamily> QueryFamilies(Func<IFamily, bool> predicate)
        {
            return families.Values.Where(predicate).ToList();
        }

        public async Task SendNoFamilyAsync(Character user)
        {
            var msg = new MsgFamily
            {
                Action = MsgFamily.FamilyAction.Query
            };
            msg.Strings.Add("0 0 0 0 0 0 0 0");
            msg.Strings.Add("");
            msg.Strings.Add(user.Name);
            await user.SendAsync(msg);

            msg.Action = MsgFamily.FamilyAction.Quit;
            await user.SendAsync(msg);
        }

        public async Task<bool> CreateFamilyAsync(Character user, string name, uint proffer)
        {
            if (user == null || user.Family != null)
            {
                return false;
            }

            if (!RoleManager.IsValidName(name))
            {
                return false;
            }

            if (name.Length > 15)
            {
                return false;
            }

            if (GetFamily(name) != null)
            {
                return false;
            }

            if (!await user.SpendMoneyAsync((int)proffer, true))
            {
                return false;
            }

            user.Family = await States.Family.CreateAsync(user, name, proffer / 2);
            if (user.Family == null)
            {
                return false;
            }

            await user.Family.SendFamilyAsync(user);
            await user.Family.SendRelationsAsync(user);
            await user.Screen.SynchroScreenAsync();
            return true;
        }

        public async Task<bool> ChangeFamilyNameAsync(IFamily family, string newName)
        {
            if (newName.Length > 15)
            {
                return false;
            }

            if (!RoleManager.IsValidName(newName))
            {
                return false;
            }

            if (GetFamily(newName) != null)
            {
                return false;
            }

            await family.ChangeNameAsync(newName);
            return true;
        }

        public async Task<bool> DisbandFamilyAsync(Character user, IFamily family)
        {
            if (user?.Family == null)
            {
                return false;
            }

            if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
            {
                return false;
            }

            if (user.Family.MembersCount > 1)
            {
                return false;
            }

            await user.FamilyMember.DeleteAsync();
            await user.Family.SoftDeleteAsync();

            user.Family = null;

            await SendNoFamilyAsync(user);
            await user.Screen.SynchroScreenAsync();
            return true;
        }
    }
}
