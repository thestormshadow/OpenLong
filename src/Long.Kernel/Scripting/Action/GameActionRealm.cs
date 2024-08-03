using Long.Database.Entities;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States;
using Long.Kernel.Managers;
using Long.Kernel.Settings;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionRealmIsOSUserAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }
            return user.IsOSUser();
        }

        private static async Task<bool> ExecuteActionRealmTeleportAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (user.IsOSUser())
            {
                return await RealmManager.ReturnToServerAsync(user);
            }
            return false;
        }

        private static async Task<bool> ExecuteActionRealmGoToServerAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            uint serverId = uint.Parse(param);
            if (!RealmManager.IsServerConnected(serverId))
            {
                return false;
            }

            if (serverId == user.OriginServer.ServerId)
            {
                return await RealmManager.ReturnToServerAsync(user);
            }

            return await RealmManager.ConnectUserToServerAsync(user, serverId);
        }

        private static async Task<bool> ExecuteActionRealmTeleport2Async(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (GameServerSettings.IsRealm)
            {
                return false;
            }

            if (!RealmManager.IsRealmConnected())
            {
                return false;
            }

            return await RealmManager.ConnectUserToServerAsync(user, RealmManager.RealmIdentity);
        }
    }
}
