using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using System.Collections.Concurrent;

namespace Long.Kernel.States.Storage
{
    public sealed class TitleStorage
    {
        private static readonly ILogger logger = Log.ForContext<TitleStorage>();

        private readonly Character user;

        private const uint TITLE_LIMIT = 4000;

        private TitleStorageObject equipedWing;
        private TitleStorageObject equipedTitle;
        private readonly List<TitleStorageObject> userTitles = new();
        private readonly TimeOut timeOut;

        public TitleStorage(Character user)
        {
            this.user = user;
            timeOut = new TimeOut(10);
        }

        public uint WingId
        {
            get
            {
                if (equipedWing == null)
                {
                    return 0;
                }
                return equipedWing.Type * 10000 + equipedWing.TitleId;
            }
        }

        public uint TitleId
        {
            get
            {
                if (equipedTitle == null)
                {
                    return 0;
                }
                return equipedTitle.Type * 10000 + equipedTitle.TitleId;
            }
        }

        public uint Score => (uint)userTitles.Sum(x => x.Score);

        public bool GetTitle(uint type, uint id, out TitleStorageObject title)
        {
            title = userTitles.FirstOrDefault(x => x.Type == type && x.TitleId == id);
            return title != null;
        }

        public async Task InitializeAsync()
        {
            var userTitles = await UserTitleRepository.GetByUserAsync(user.Identity);
            foreach (var title in userTitles)
            {
                var t = new TitleStorageObject(title);
                if (t.HasExpired())
                {
                    await ServerDbContext.DeleteAsync(title);
                    continue;
                }

                if (GetTitle(t.Type, t.TitleId, out _))
                {
                    logger.Warning("Duplicate title {0} {1} for user {2} {3}", t.Type, t.TitleId, user.Identity, user.Name);
                    continue;
                }

                this.userTitles.Add(t);
                if (t.IsActive)
                {
                    if (t.Type < TITLE_LIMIT)
                    {
                        equipedTitle = t;
                    }
                    else
                    {
                        equipedWing = t;
                    }
                }
            }
        }

        public async Task<bool> AwardTitleAsync(uint titleType, uint titleId, uint timeOutSecs = 0)
        {
            if (GetTitle(titleType, titleId, out var title))
            {
                if (title.DelTime == 0)
                {
                    return false;
                }

                if (timeOutSecs != 0)
                {
                    title.DelTime += timeOutSecs;
                }
                else
                {
                    title.DelTime = 0;
                }

                await user.SendAsync(new MsgTitleStorage(MsgTitleStorage.TitleStorageAction.Load, 0, 0, 0, new MsgTitleInfoPB
                {
                    DelTime = title.DelTime,
                    Title = title.TitleId,
                    Type = title.Type,
                    Status = title.IsActive ? 1u : 0u
                }));
                await title.SaveAsync();
                return true;
            }

            var type = TitleStorageManager.GetTitleType(titleType, titleId);
            if (type == null)
            {
                logger.Error("Invalid title type {0} for user {1} {2}", titleType, user.Identity, user.Name);
                return false;
            }

            var dbTitle = new DbUserTitle
            {
                PlayerId = user.Identity,
                Status = 0,
                TitleId = type.Identity,
                Type = type.Type,
            };

            if (timeOutSecs > 0)
            {
                dbTitle.DelTime = (uint)(UnixTimestamp.Now + timeOutSecs);
            }

            await ServerDbContext.CreateAsync(dbTitle);

            title = new TitleStorageObject(dbTitle);

            userTitles.Add(title);
            await user.SendAsync(new MsgTitleStorage(MsgTitleStorage.TitleStorageAction.Load, 0, 0, 0, new MsgTitleInfoPB
            {
                DelTime = title.DelTime,
                Title = title.TitleId,
                Type = title.Type,
                Status = 0
            }));

            await EquipAsync(titleType, titleId);
            return true;
        }

        public async Task<bool> DeleteTitleAsync(uint titleType, uint idTitle)
        {
            if (!GetTitle(titleType, idTitle, out var title))
            {
                return false;
            }

            await title.DeleteAsync();
            userTitles.Remove(title);

            await user.SendAsync(new MsgTitleStorage(MsgTitleStorage.TitleStorageAction.DelUserData, 0, title.Type, title.TitleId));
            return true;
        }

        public async Task<bool> EquipAsync(uint titleType, uint idTitle)
        {
            if (!GetTitle(titleType, idTitle, out var title))
            {
                return false;
            }

            if (title.IsWing)
            {
                if (equipedWing != null)
                {
                    await UnEquipAsync(equipedTitle.Type, equipedWing.TitleId, false);
                }

                equipedWing = title;
            }
            else
            {
                if (equipedTitle != null)
                {
                    await UnEquipAsync(equipedTitle.Type, equipedTitle.TitleId, false);
                }

                equipedTitle = title;
            }

            await user.SendAsync(new MsgTitleStorage(MsgTitleStorage.TitleStorageAction.Equip, 0, title.Type, title.TitleId));
            await user.SendAsync(new MsgTitleStorage()
            {
                Data = new MsgTitleStoragePB
                {
                    Data = title.Type,
                    Data2 = title.TitleId,
                    Action = (uint)MsgTitleStorage.TitleStorageAction.Equip,
                    Life = 0,
                    Info = new()
                    {
                        new MsgTitleInfoPB
                        {
                            Type = title.Type,
                            Title = title.TitleId,
                            Status = title.IsWing ? 1 : title.Score
                        }
                    }
                }
            });

            await user.BroadcastRoomMsgAsync(new MsgPlayer(user), false);
            title.IsActive = true;
            await title.SaveAsync();
            return true;
        }

        public async Task<bool> UnEquipAsync(uint titleType, uint idTitle, bool screen = true)
        {
            TitleStorageObject title;
            if (equipedTitle?.TitleId == idTitle)
            {
                title = equipedTitle;
                equipedTitle = null;
            }
            else if (equipedWing?.TitleId == idTitle)
            {
                title = equipedWing;
                equipedWing = null;
            }
            else
            {
                return false;
            }

            await user.SendAsync(new MsgTitleStorage(MsgTitleStorage.TitleStorageAction.Unequip, 0, title.Type, title.TitleId));

            if (screen)
            {
                await user.BroadcastRoomMsgAsync(new MsgPlayer(user), false);
            }
            title.IsActive = false;
            await title.SaveAsync();
            return true;
        }

        public async Task OnTimerAsync()
        {
            if (!timeOut.ToNextTime())
            {
                return;
            }

            for (int i = userTitles.Count - 1; i >= 0; i--)
            {
                var title = userTitles[i];
                if (title.HasExpired())
                {
                    await UnEquipAsync(title.Type, title.TitleId);
                    userTitles.RemoveAt(i);
                    await title.DeleteAsync();
                }
            }
        }

        public async Task SendAllAsync()
        {
            MsgTitleStorage msg = new(MsgTitleStorage.TitleStorageAction.UseStorageUnit, Score, 0, 0);
            foreach (var title in userTitles)
            {
                // can fit up to 150
                msg.Data.Info.Add(new MsgTitleInfoPB
                {
                    Title = title.TitleId,
                    Type = title.Type,
                    DelTime = title.DelTime,
                    Status = title.IsActive ? 1u : 0u
                });
            }
            await user.SendAsync(msg);
        }
    }
}
