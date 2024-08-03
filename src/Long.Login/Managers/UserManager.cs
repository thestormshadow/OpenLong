using Long.Login.Network.Login.Packets;
using Long.Login.States;
using System.Collections.Concurrent;

namespace Long.Login.Managers
{
    public class UserManager
    {
        private static readonly ILogger logger = Log.ForContext<UserManager>();
        private static ConcurrentDictionary<Guid, User> users = new ();

        public static bool AddUser(User user)
        {
            return users.TryAdd(user.Guid, user);
        }

        public static User GetUser(uint accountID)
        {
            return users.Values.FirstOrDefault(x => x.AccountID == accountID);
        }

        public static User GetUser(Guid requestId)
        {
            return users.TryGetValue(requestId, out User user) ? user : null;
        }

        public static bool RemoveUser(Guid idUser)
        {
            return users.TryRemove(idUser, out _);
        }

        public static bool RemoveUser(uint accountID)
        {
            User user = GetUser(accountID);
            if (user != null)
            {
                return RemoveUser(user.Guid);
            }
            return false;
        }

        public static async Task OnTimerAsync()
        {
            foreach (var user in users.Values)
            {
                if (user.HasExpired)
                {
                    logger.Debug("User {0} login request expired.", user.UserName);
                    if (user.Client.Socket.Connected)
                    {
                        await user.Client.SendAsync(new MsgConnectEx(MsgConnectEx.RejectionCode.ValidationTimeout), () =>
                        {
                            user.Client.Disconnect();
                            return Task.CompletedTask;
                        });
                    }
                    RemoveUser(user.AccountID);
                }
            }
        }
    }
}
