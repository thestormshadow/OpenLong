using Long.Kernel.States.User;
using Long.Kernel.States;
using System.Collections.Concurrent;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using System.Runtime.Caching;
using Long.Kernel.States.World;
using System.Drawing;
using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Network.Packets;
using Long.Kernel.Settings;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Network.Packets.Cross;
using Long.Kernel.States.Npcs;
using System.Diagnostics;
using Long.Kernel.Database;

namespace Long.Kernel.Managers
{
    public class RoleManager
    {
        private static readonly ILogger logger = Log.ForContext<RoleManager>();

        private static readonly ConcurrentDictionary<uint, Character> userSet = new();
        private static readonly ConcurrentDictionary<uint, Role> roleSet = new();
		private static readonly ConcurrentDictionary<uint, DbSuperman> superman = new();
		private static readonly ConcurrentDictionary<uint, DbMonstertype> monsterTypes = new();
		private static readonly ConcurrentDictionary<uint, DbMonsterTypeMagic> monsterMagics = new();

		private static bool isShutdown;
        private static bool isMaintenanceEntrance;
        private static bool isCooperatorMode;
        private static GameServerSettings serverSettings => GameServerSettings.Instance;

        public static int OnlineUniquePlayers => userSet.Values.Select(x => x.Client.MacAddress).Distinct().Count();
        public static int OnlinePlayers => userSet.Count;
        public static int RolesCount => roleSet.Count;
        public static int AiCount => roleSet.Values.Count(x => x.IsMonster());
        public static int MaxOnlinePlayers { get; private set; }

        private static readonly MemoryCache logins = MemoryCache.Default;

        public static async Task InitializeAsync()
        {
			logger.Information("Starting Role Manager");

			var supermen = await SupermanRepository.GetAsync();
			foreach (var superman in supermen)
			{
				RoleManager.superman.TryAdd(superman.UserIdentity, superman);
			}
			foreach (DbMonstertype mob in await MonsterypeRepository.GetAsync())
            {
                monsterTypes.TryAdd(mob.Id, mob);
            }
			foreach (DbMonsterTypeMagic magic in await MonsterTypeMagicRepository.GetAsync())
			{
				monsterMagics.TryAdd(magic.Id, magic);
			}
		}

        #region Login Request

        public static void SaveLoginRequest(string token, TransferAuthArgs transferAuthArgs)
        {
            var timeoutPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddSeconds(30) };
            logins.Set(token.ToString(), transferAuthArgs, timeoutPolicy);
        }

        public static TransferAuthArgs GetLoginRequest(string token)
        {
            return logins.Get(token) as TransferAuthArgs;
        }

        public static void RemoveLoginRequest(string token)
        {
            logins.Remove(token);
        }

        #endregion

        #region User

        public static async Task<bool> LoginUserAsync(GameClient user)
        {
            if (isShutdown)
            {
                await user.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.ServerDown);
                return false;
            }

            if (isMaintenanceEntrance)
            {
                await user.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.ServerLocked);
                return false;
            }

            if (isCooperatorMode)
            {
                await user.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.NonCooperatorAccount);
                return false;
            }

            if (userSet.TryGetValue(user.Character.Identity, out Character concurrent))
            {
                string message;
                if (user.IpAddress != concurrent.Client.IpAddress)
                {
                    logger.Warning("User {0}[{1}] has been disconnected due to duplicate login from different networks [{2}].", user.Character.Name, concurrent.Client.IpAddress, user.IpAddress);
                    message = StrAnotherLoginSameIp;
                }
                else
                {
                    logger.Warning("User {0} has been disconnected due to duplicated login.", user.Character.Name);
                    message = StrAnotherLoginOtherIp;
                }
                await concurrent.Client.DisconnectWithMessageAsync(message);
                await user.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.PleaseTryAgainLater);
                return false;
            }

            //if (user.Character.IsPm() && user.AccountIdentity < 10_000)
            //{
            //    await user.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.AccountLocked);
            //    logger.Information($"{user.Character.Name} no administration account ID.");
            //    return false;
            //}

            if (userSet.Count > serverSettings.Game.MaxOnlinePlayers && user.AuthorityLevel <= 1 && !user.Character.IsGm())
            {
                await user.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.ServerFull);
                logger.Information($"{user.Character.Name} tried to login and server is full.");
                return false;
            }

            RealmManager.GetServer(RealmManager.ServerIdentity, out var serverConfig);

            user.Character.OriginServer = serverConfig;

            userSet.TryAdd(user.Character.Identity, user.Character);
            roleSet.TryAdd(user.Character.Identity, user.Character);

            logger.Information("{0} has logged in.", user.Character.Name);
            MaxOnlinePlayers = Math.Max(MaxOnlinePlayers, userSet.Count);
            return true;
        }

        public static async Task<bool> LoginUserAsync(Character user, uint serverId)
        {
            if (userSet.TryGetValue(user.Identity, out Character concurrent))
            {
                // TODO remove? do something
                return false;
            }

            if (!RealmManager.GetServer(serverId, out var originServer))
            {
                logger.Error("Could not login {0} {1} {2} from {3}. Invalid server ID.", user.Identity, user.OriginalUserId, user.Name, serverId);
                return false;
            }

            user.OriginServer = originServer;

            RealmManager.GetServer(RealmManager.ServerIdentity, out var thisServer);
            user.CurrentServer = thisServer;

            userSet.TryAdd(user.Identity, user);
            roleSet.TryAdd(user.Identity, user);

            logger.Information("{0} from {1} has logged in.", user.Name, originServer.ServerName);

            MaxOnlinePlayers = Math.Max(MaxOnlinePlayers, userSet.Count);
            return true;
        }

        public static void ForceLogoutUser(uint idUser)
        {
            userSet.TryRemove(idUser, out _);
            roleSet.TryRemove(idUser, out _);
        }

        public static async Task KickOutAsync(uint idUser, string reason = "")
        {
            if (userSet.TryGetValue(idUser, out Character user))
            {
                if (user.OriginServer.ServerId == RealmManager.ServerIdentity)
                {
                    await user.Client.DisconnectWithMessageAsync(string.Format(StrKickout, reason));
                }
                else
                {
                    await SendOSMsgAsync(new MsgCrossRealmActionC
                    {
                        Data = new CrossRealmActionPB
                        {
                            Action = (uint)CrossRealmAction.KickoutPlayer,
                            Data = user.Identity,
                            Command = user.OriginalUserId
                        }
                    }, user.OriginServer.ServerId);
                }
                logger.Information("In Map[{0}], kick out User[{1}]. Reason: {2}", user.MapIdentity, user.Identity, reason);
            }
        }

        public static async Task KickOutAllAsync(string reason = "", bool isShutdown = false)
        {
            if (isShutdown)
            {
                RoleManager.isShutdown = true;
            }

            foreach (Character user in userSet.Values)
            {
                await user.Client.DisconnectWithMessageAsync(string.Format(StrKickout, reason));
                logger.Information($"User {user.Name} has been kicked (kickoutall): {reason}");
            }
        }

        public static Character GetUserByAccount(uint idAccount)
        {
            return userSet.Values.FirstOrDefault(x => x.Client?.AccountIdentity == idAccount);
        }

        public static Character GetUser(uint idUser)
        {
            return userSet.TryGetValue(idUser, out Character client) ? client : null;
        }

        public static Character GetUser(string name)
        {
            return userSet.Values.FirstOrDefault(x => x.Name == name);
        }

        public static int CountUserByMacAddress(string macAddress)
        {
            return userSet.Values.Count(x => macAddress.Equals(x.Client?.MacAddress, StringComparison.InvariantCultureIgnoreCase));
        }

        public static List<Character> QueryUserSetByMap(uint idMap)
        {
            return userSet.Values.Where(x => x.MapIdentity == idMap).ToList();
        }

        public static List<Character> QueryUserSet()
        {
            return userSet.Values.ToList();
        }

        #endregion

        #region Role

        public static List<T> QueryRoleByMap<T>(uint idMap) where T : Role
        {
            return roleSet.Values.Where(x => x.MapIdentity == idMap && x is T).Cast<T>().ToList();
        }

        public static List<T> QueryRoleByType<T>() where T : Role
        {
            return roleSet.Values.Where(x => x is T).Cast<T>().ToList();
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

        public static List<Role> QueryRoles(Func<Role, bool> predicate)
        {
            return roleSet.Values.Where(predicate).ToList();
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
                {
                    return result;
                }
            }

            return null;
        }

        public static T FindRole<T>(Func<T, bool> predicate) where T : Role
        {
            foreach (GameMap map in MapManager.GameMaps.Values)
            {
                T result = map.QueryRole(predicate);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public static List<T> FindRoles<T>(Func<T, bool> predicate) where T : Role
        {
            List<T> result = new();
            foreach (GameMap map in MapManager.GameMaps.Values)
            {
                T role = map.QueryRole(predicate);
                if (role != null)
                {
                    result.Add(role);
                }
            }
            return result;
        }

        /// <summary>
        ///     Attention, DO NOT USE to remove <see cref="Character" />.
        /// </summary>
        public static bool RemoveRole(uint idRole)
        {
            return roleSet.TryRemove(idRole, out _);
        }

		#endregion

		#region Superman
		public static async Task AddOrUpdateSupermanAsync(uint idUser, int amount)
		{
			if (!superman.TryGetValue(idUser, out var sm))
			{
				superman.TryAdd(idUser, sm = new DbSuperman
				{
					UserIdentity = idUser,
					Amount = (uint)amount
				});
				await ServerDbContext.UpdateAsync(sm);
			}
			else
			{
				sm.Amount = (uint)amount;
				await ServerDbContext.UpdateAsync(sm);
			}
		}

		public static int GetSupermanPoints(uint idUser)
		{
			return (int)(superman.TryGetValue(idUser, out var value) ? value.Amount : 0);
		}

		public static int GetSupermanRank(uint idUser)
		{
			int result = 1;
			foreach (var super in superman.Values.OrderByDescending(x => x.Amount))
			{
				if (super.UserIdentity == idUser)
				{
					return result;
				}

				result++;
			}
			return result;
		}
		#endregion

		#region Monster

		public static DbMonstertype GetMonstertype(uint type)
        {
            return monsterTypes.TryGetValue(type, out DbMonstertype mob) ? mob : null;
        }

		public static List<DbMonsterTypeMagic> GetMonsterMagics(uint type)
		{
			return monsterMagics.Values.Where(x => x.MonsterType == type).ToList();
		}

		#endregion

		#region User validation

		public static bool IsValidName(string szName)
        {
            if (long.TryParse(szName, out _))
            {
                return false;
            }

            if (string.IsNullOrEmpty(szName))
            {
                return false;
            }

            foreach (var c in szName)
            {
                if (c < ' ')
                {
                    return false;
                }

                switch (c)
                {
                    case ' ':
                    case ';':
                    case ',':
                    case '/':
                    case '\\':
                    case '=':
                    case '%':
                    case '@':
                    case '\'':
                    case '"':
                    case '[':
                    case ']':
                    case '?':
                    case '{':
                    case '}':
                        return false;
                }
            }

            string lower = szName.ToLower();
            return InvalidNames.All(part => !lower.Contains(part));
        }

        private static readonly string[] InvalidNames =
        {
            "{", "}", "[", "]", "(", ")", "\"", "[gm]", "[pm]", "'", "´", "`", "admin", "helpdesk", " ",
            "bitch", "puta", "whore", "ass", "fuck", "cunt", "fdp", "porra", "poha", "caralho", "caraio",
            "system", "allusers", "none"
        };

        #endregion

        #region Broadcast

        public static async Task BroadcastWorldMsgAsync(string message, TalkChannel channel, Color? color = null)
        {
            foreach (var user in userSet.Values)
            {
                await user.SendAsync(message, channel, color);
            }
        }

        public static Task BroadcastWorldMsgAsync(IPacket msg, uint ignoreUserId = 0)
        {
            return BroadcastWorldMsgAsync(msg.Encode(), ignoreUserId);
        }

        public static async Task BroadcastWorldMsgAsync(byte[] msg, uint ignoreUserId = 0)
        {
            foreach (var user in userSet.Values)
            {
                if (user.Identity != ignoreUserId)
                {
                    await user.SendAsync(msg);
                }
            }
        }

		#endregion

		public static long RoleTimerTicks { get; private set; }
		public static int ProcessedRoles { get; private set; }

		public static async Task OnRoleTimerAsync()
		{
			int processedRoles = 0;
			Stopwatch sw = Stopwatch.StartNew();
			foreach (var role in roleSet.Values.Where(x => x is not Character && x is not BaseNpc))
			{
				await role.OnTimerAsync();
				processedRoles++;
			}
			sw.Stop();
			ProcessedRoles = processedRoles;
			RoleTimerTicks = sw.ElapsedTicks;
		}

		public static void SetMaintenanceStart()
        {
            isMaintenanceEntrance = true;
        }

        public static void ToggleCooperatorMode()
        {
            isCooperatorMode = !isCooperatorMode;
            if (isCooperatorMode)
            {
                logger.Information("Cooperator mode has been enabled! Only cooperators accounts will be able to login.");
            }
            else
            {
                logger.Information("Cooperator mode has been disabled! All accounts are enabled to login.");
            }
        }
    }
}
