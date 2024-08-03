using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;

namespace Long.Kernel.States.User
{
    public partial class Character
    {
        #region Status

        public bool IsAway { get; set; }

        public async Task LoadStatusAsync()
        {
            List<DbStatus> statusList = await StatusRepository.GetAsync(Identity);
            await using var serverDbContext = new ServerDbContext();
            foreach (DbStatus status in statusList)
            {
                if (UnixTimestamp.ToDateTime(status.EndTime) < DateTime.Now)
                {
                    serverDbContext.Status.Remove(status);
                    continue;
                }

                await AttachStatusAsync(status);
            }
            await serverDbContext.SaveChangesAsync();
            await CheckPkStatusAsync();
        }

        #endregion

        #region User Title

        public byte TitleSelect
        {
            get => user.TitleSelect;
            set => user.TitleSelect = value;
        }

        public uint Title
        {
            get => user.Title;
            set => user.Title = value;
        }

        public async Task LoadTitlesAsync()
        {
            const int GAME_STAFF_TITLE = 1;
            if (IsGm() && !HasTitle(GAME_STAFF_TITLE))
            {
                await AddTitleAsync(GAME_STAFF_TITLE);
                await BroadcastRoomMsgAsync(new MsgTitle
                {
                    Action = MsgTitle.TitleAction.Select,
                    Identity = Identity,
                    Title = GAME_STAFF_TITLE
                }, true);
            }
            else if (!IsGm() && HasTitle(GAME_STAFF_TITLE))
            {
                await RemoveTitleAsync(GAME_STAFF_TITLE);
                await BroadcastRoomMsgAsync(new MsgTitle
                {
                    Action = MsgTitle.TitleAction.Select,
                    Identity = Identity,
                    Title = 0
                }, true);
            }
            else if (TitleSelect != 0)
            {
                await BroadcastRoomMsgAsync(new MsgTitle
                {
                    Action = MsgTitle.TitleAction.Select,
                    Identity = Identity,
                    Title = (byte)TitleSelect
                }, true);
            }
        }

        private bool IsValidTitle(int title)
        {
            return title is > 0 and <= 32;
        }

        public bool HasTitle(int title)
        {
            uint titleFlag = 1u << (title - 1);
            return (user.Title & titleFlag) != 0;
        }

        public Task AddTitleAsync(int title)
        {
            if (HasTitle(title))
            {
                return Task.CompletedTask;
            }

            if (!IsValidTitle(title))
            {
                return Task.CompletedTask;
            }

            uint titleFlag = 1u << (title - 1);
            user.Title |= titleFlag;
            return SaveAsync();
        }

        public async Task RemoveTitleAsync(int title)
        {
            if (!HasTitle(title))
            {
                return;
            }

            uint titleFlag = 1u << (title - 1);
            user.Title &= ~titleFlag;

            if (TitleSelect == title)
            {
                TitleSelect = 0;
                await BroadcastRoomMsgAsync(new MsgTitle
                {
                    Action = MsgTitle.TitleAction.Select,
                    Identity = Identity
                }, true);
            }
            await SaveAsync();
        }

        public static Task AddTitleAsync(uint idUser, int title)
        {
            Character user = RoleManager.GetUser(idUser);
            if (user != null)
            {
                return user.AddTitleAsync(title);
            }
            else
            {
                uint titleFlag = 1u << (title - 1);
                return ServerDbContext.ScalarAsync($"UPDATE cq_user SET title = title | {titleFlag}, title_select = 0 WHERE id={idUser} LIMIT 1;");
            }
        }

        public static Task RemoveTitleAsync(uint idUser, int title)
        {
            Character user = RoleManager.GetUser(idUser);
            if (user != null)
            {
                return user.RemoveTitleAsync(title);
            }
            else
            {
                uint titleFlag = 1u << (title - 1);
                return ServerDbContext.ScalarAsync($"UPDATE cq_user SET title = title & ~{titleFlag}, title_select = 0 WHERE id={idUser} LIMIT 1;");
            }
        }

        public async Task SelectTitleAsync(int title)
        {
            if (TitleSelect != 0 && title == 0)
            {
                TitleSelect = 0;
                await BroadcastRoomMsgAsync(new MsgTitle
                {
                    Action = MsgTitle.TitleAction.Select,
                    Identity = Identity
                }, true);
                return;
            }

            if (!HasTitle(title))
            {
                return;
            }

            TitleSelect = (byte)title;
            await BroadcastRoomMsgAsync(new MsgTitle
            {
                Action = MsgTitle.TitleAction.Select,
                Identity = Identity,
                Title = (byte)title
            }, true);
            await SaveAsync();
        }

        public Task SendTitlesAsync()
        {
            var msg = new MsgTitle
            {
                Action = MsgTitle.TitleAction.Query,
                Identity = Identity
            };
            for (int title = 1; title <= 32; title++)
            {
                if (HasTitle(title))
                {
                    msg.Titles.Add((byte)title);
                }
            }
            if (msg.Titles.Count > 0)
            {
                return SendAsync(msg);
            }
            return Task.CompletedTask;
        }

        #endregion
    }
}
