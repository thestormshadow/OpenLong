using Long.Ai.Database.Repositories;
using Long.Ai.States;
using Long.Ai.States.World;
using Long.Database.Entities;
using System.Collections.Concurrent;

namespace Long.Ai.Managers
{
    public static class RoleManager
    {
        public static int RolesCount => roleSet.Count;

        public static async Task<bool> InitializeAsync()
        {
            foreach (DbMonstertype mob in await MonsterypeRepository.GetAsync())
            {
                monsterTypes.TryAdd(mob.Id, mob);
            }
            foreach (DbMonsterTypeMagic magic in await MonsterTypeMagicRepository.GetAsync())
            {
                monsterMagics.TryAdd(magic.Id, magic);
            }
            return true;
        }

        public static void ClearUserSet()
        {
            userSet.Clear();
            foreach (var user in roleSet.Values.Where(x => x.IsPlayer()))
            {
                roleSet.TryRemove(user.Identity, out _);
            }
        }

        public static bool LoginUser(Character user)
        {
            userSet.TryAdd(user.Identity, user);
            roleSet.TryAdd(user.Identity, user);
            return true;
        }

        public static bool LogoutUser(uint idUser, out Character user)
        {
            roleSet.TryRemove(idUser, out _);
            if (!userSet.TryRemove(idUser, out user))
                return false;
            return true;
        }

        public static Character GetUser(uint idUser)
        {
            return userSet.TryGetValue(idUser, out Character client) ? client : null;
        }

        public static Character GetUser(string name)
        {
            return userSet.Values.FirstOrDefault(x => x.Name == name);
        }

        public static List<T> QueryRoleByMap<T>(uint idMap) where T : Role
        {
            return roleSet.Values.Where(x => x.MapIdentity == idMap && x is T).Cast<T>().ToList();
        }

        public static List<T> QueryRoleByType<T>() where T : Role
        {
            return roleSet.Values.Where(x => x is T).Cast<T>().ToList();
        }

        public static List<Character> QueryUserSetByMap(uint idMap)
        {
            return userSet.Values.Where(x => x.MapIdentity == idMap).ToList();
        }

        public static List<Character> QueryUserSet()
        {
            return userSet.Values.ToList();
        }

        /// <summary>
        ///     Attention, DO NOT USE to add <see cref="Character" />.
        /// </summary>
        public static bool AddRole(Role role)
        {
            return roleSet.TryAdd(role.Identity, role);
        }

        public static Role GetRole(uint idRole)
        {
            return roleSet.TryGetValue(idRole, out Role role) ? role : null;
        }

        public static T GetRole<T>(uint idRole) where T : Role
        {
            return roleSet.TryGetValue(idRole, out Role role) ? role as T : null;
        }

        public static T GetRole<T>(Func<T, bool> predicate) where T : Role
        {
            return roleSet.Values
                            .Where(x => x is T)
                            .Cast<T>()
                            .FirstOrDefault(x => predicate != null && predicate(x));
        }

        public static T FindRole<T>(uint idRole) where T : Role
        {
            foreach (GameMap map in MapManager.GameMaps.Values)
            {
                var result = map.QueryRole<T>(idRole);
                if (result != null)
                    return result;
            }

            return null;
        }

        public static T FindRole<T>(Func<T, bool> predicate) where T : Role
        {
            foreach (GameMap map in MapManager.GameMaps.Values)
            {
                T result = map.QueryRole(predicate);
                if (result != null)
                    return result;
            }

            return null;
        }

        /// <summary>
        ///     Attention, DO NOT USE to remove <see cref="Character" />.
        /// </summary>
        public static bool RemoveRole(uint idRole)
        {
            return roleSet.TryRemove(idRole, out _);
        }

        public static DbMonstertype GetMonstertype(uint type)
        {
            return monsterTypes.TryGetValue(type, out DbMonstertype mob) ? mob : null;
        }

        public static List<DbMonsterTypeMagic> GetMonsterMagics(uint type)
        {
            return monsterMagics.Values.Where(x => x.MonsterType == type).ToList();
        }

        private static ConcurrentDictionary<uint, Character> userSet = new();
        private static ConcurrentDictionary<uint, Role> roleSet = new();
        private static Dictionary<uint, DbMonstertype> monsterTypes = new();
        private static Dictionary<uint, DbMonsterTypeMagic> monsterMagics = new();
    }
}
