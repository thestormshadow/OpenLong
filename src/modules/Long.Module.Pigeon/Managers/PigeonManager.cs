using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Pigeon.Network;
using Long.Module.Pigeon.Repositories;
using Long.Shared;
using System.Drawing;
using static Long.Kernel.StrRes;

namespace Long.Module.Pigeon.Managers
{
    public static class PigeonManager
    {
        private const int PIGEON_PRICE = 5;
        private const int PIGEON_ADDITION = 5;
        private const int PIGEON_TOP_ADDITION = 15;
        private const int PIGEON_MAX_MSG_LENGTH = 80;
        private const int PIGEON_STAND_SECS = 60;

        private static readonly List<DbPigeon> pastPigeons = new();
        private static readonly List<DbPigeonQueue> nextPigeons = new();
        private static DbPigeon currentPigeon;

        private static readonly TimeOut nextTimer = new(PIGEON_STAND_SECS);

        private static readonly object LockObject = new();

        public static async Task<bool> InitializeAsync()
        {
            nextPigeons.AddRange((await PigeonQueueRepository.GetAsync()).OrderBy(x => x.NextIdentity));
            await SyncAsync();
            return true;
        }

        public static async Task<bool> PushAsync(Character sender, string message, bool showError = true,
                                                 bool forceShow = false)
        {
            if (message.Length > PIGEON_MAX_MSG_LENGTH)
            {
                if (showError)
                {
                    await sender.SendAsync(StrPigeonSendErrStringTooLong);
                }

                return false;
            }

            if (string.IsNullOrEmpty(message))
            {
                if (showError)
                {
                    await sender.SendAsync(StrPigeonSendErrEmptyString);
                }

                return false;
            }

            if (OnQueueByUser(sender.Identity) >= 5 && !sender.IsGm())
            {
                if (showError)
                {
                    await sender.SendAsync(StrPigeonSendOver5Pieces);
                }

                return false;
            }

            if (!await sender.SpendBoundConquerPointsAsync(Character.EmoneyOperationType.Pigeon, PIGEON_PRICE))
            {
                if (showError)
                {
                    await sender.SendAsync(StrPigeonUrgentErrNoEmoney);
                }

                return false;
            }

            var pigeon = new DbPigeonQueue
            {
                UserIdentity = sender.Identity,
                UserName = sender.Name,
                Message = message[..Math.Min(message.Length, PIGEON_MAX_MSG_LENGTH)],
                Addition = 0,
                NextIdentity = 0
            };
            await ServerDbContext.CreateAsync(pigeon);

            lock (LockObject)
            {
                nextPigeons.Add(pigeon);
            }

            if (forceShow || nextTimer.IsTimeOut(PIGEON_STAND_SECS))
            {
                await SyncAsync();
            }

            await RebuildQueueAsync();
            await sender.SendAsync(StrPigeonSendProducePrompt);
            return true;
        }

        public static async Task AdditionAsync(Character sender, MsgPigeon request)
        {
            DbPigeonQueue pigeon = null;
            var position = 0;

            lock (LockObject)
            {
                for (var i = 0; i < nextPigeons.Count; i++)
                {
                    if (nextPigeons[i].Identity == request.Param &&
                        nextPigeons[i].UserIdentity == sender.Identity)
                    {
                        position = i;
                        pigeon = nextPigeons[i];
                        break;
                    }
                }
            }

            if (pigeon == null)
            {
                return;
            }

            var newPos = 0;
            switch ((MsgPigeon.PigeonMode)((byte)request.Mode))
            {
                case MsgPigeon.PigeonMode.Urgent:
                    if (!await sender.SpendBoundConquerPointsAsync(Character.EmoneyOperationType.Pigeon, PIGEON_ADDITION))
                    {
                        await sender.SendAsync(StrPigeonUrgentErrNoEmoney);
                        return;
                    }

                    pigeon.Addition += PIGEON_ADDITION;
                    newPos = Math.Max(0, position - 5);
                    break;
                case MsgPigeon.PigeonMode.SuperUrgent:
                    if (!await sender.SpendBoundConquerPointsAsync(Character.EmoneyOperationType.Pigeon, PIGEON_TOP_ADDITION))
                    {
                        await sender.SendAsync(StrPigeonUrgentErrNoEmoney);
                        return;
                    }

                    pigeon.Addition += PIGEON_TOP_ADDITION;
                    newPos = 0;
                    break;
            }

            lock (LockObject)
            {
                nextPigeons.RemoveAt(position);
                nextPigeons.Insert(newPos, pigeon);
            }

            await RebuildQueueAsync();
            await sender.SendAsync(StrPigeonSendProducePrompt);
        }

        public static Task RebuildQueueAsync()
        {
            uint idx = 0;
            lock (LockObject)
            {
                foreach (DbPigeonQueue queued in nextPigeons)
                {
                    queued.NextIdentity = idx++;
                }
            }
            return ServerDbContext.UpdateRangeAsync(nextPigeons.ToArray());
        }

        public static async Task SyncAsync()
        {
            if (nextPigeons.Count == 0)
            {
                return;
            }

            nextTimer.Startup(PIGEON_STAND_SECS);

            await ServerDbContext.CreateAsync(currentPigeon = new DbPigeon
            {
                UserIdentity = nextPigeons[0].UserIdentity,
                UserName = nextPigeons[0].UserName,
                Addition = nextPigeons[0].Addition,
                Message = nextPigeons[0].Message,
                Time = (uint)DateTime.Now.ToUnixTimestamp(),
            });
            await ServerDbContext.DeleteAsync(nextPigeons[0]);

            lock (LockObject)
            {
                nextPigeons.RemoveAt(0);
                pastPigeons.Add(currentPigeon);
            }

            await RebuildQueueAsync();
            await RoleManager.BroadcastWorldMsgAsync(new MsgTalk(TalkChannel.Broadcast, Color.White,
                                                            MsgTalk.ALLUSERS, currentPigeon.UserName, currentPigeon.Message));
        }

        public static async Task OnTimerAsync()
        {
            if (nextPigeons.Count == 0)
            {
                return;
            }

            if (!nextTimer.ToNextTime(PIGEON_STAND_SECS))
            {
                return;
            }

            await SyncAsync();
        }

        public static async Task SendListAsync(Character user, MsgPigeon.PigeonMode request, int page)
        {
            const int ipp = 10;
            List<DbPigeonQueue> temp;
            lock (LockObject)
            {
                if (request == MsgPigeon.PigeonMode.Query)
                {
                    temp = new List<DbPigeonQueue>(nextPigeons);
                }
                else
                {
                    temp = new List<DbPigeonQueue>(nextPigeons.FindAll(x => x.UserIdentity == user.Identity));
                }
            }

            var pos = (uint)(page * ipp);
            DbPigeonQueue[] queryList;
            lock (LockObject)
            {
                queryList = temp.Skip((int)pos).Take(ipp).ToArray();
            }

            var msg = new MsgPigeonQuery
            {
                Mode = (uint)page
            };
            bool sent = false;
            foreach (DbPigeonQueue pigeon in queryList)
            {
                if (msg.Messages.Count >= 5)
                {
                    sent = true;
                    await user.SendAsync(msg);
                    msg.Total = 5;
                    msg.Messages.Clear();
                }

                msg.Messages.Add(new MsgPigeonQuery.PigeonMessage
                {
                    Identity = pigeon.Identity,
                    UserIdentity = pigeon.UserIdentity,
                    UserName = pigeon.UserName,
                    Addition = pigeon.Addition,
                    Message = pigeon.Message,
                    Position = pos++
                });
            }
            if (!sent || msg.Messages.Count > 0)
            {
                await user.SendAsync(msg);
            }
        }

        public static async Task SendToUserAsync(Character user)
        {
            if (currentPigeon != null)
            {
                await user.SendAsync(new MsgTalk(TalkChannel.Broadcast, Color.White,
                                                 MsgTalk.ALLUSERS, currentPigeon.UserName, currentPigeon.Message));
            }
        }

        public static int OnQueueByUser(uint idUser)
        {
            lock (LockObject)
            {
                return nextPigeons.Count(x => x.UserIdentity == idUser);
            }
        }
    }
}
