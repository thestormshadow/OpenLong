using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Shared.Helpers;
using System.Drawing;
using static Long.Kernel.Managers.AchievementManager;
using static Long.Kernel.Network.Game.Packets.MsgAchievement;

namespace Long.Kernel.States
{
    public sealed class Achievements
    {
        private static readonly ILogger gmLogger = Logger.CreateLogger("achievements");

        private readonly Character user;
        private DbAchievement achievements;

        public Achievements(Character user)
        {
            this.user = user;
        }

        public uint Points
        {
            get => achievements.Point;
            set => achievements.Point = value;
        }

        public async Task InitializeAsync()
        {
            achievements = await AchievementRepository.GetAsync(user.Identity);
            if (achievements == null)
            {
                achievements = new DbAchievement
                {
                    UserIdentity = user.Identity
                };
                await ServerDbContext.CreateAsync(achievements);
            }

            await SendAsync();
        }

        public bool HasAchievement(int fullFlag)
        {
            var result = IdToFlag(fullFlag);
            uint flag = 1u << result;
            int type = IdToType(fullFlag);
            uint currFlag;
            switch (type)
            {
                case 0: currFlag = achievements.Achieve1; break;
                case 1: currFlag = achievements.Achieve2; break;
                case 2: currFlag = achievements.Achieve3; break;
                case 3: currFlag = achievements.Achieve4; break;
                case 4: currFlag = achievements.Achieve5; break;
                case 5: currFlag = achievements.Achieve6; break;
                case 6: currFlag = achievements.Achieve7; break;
                case 7: currFlag = achievements.Achieve8; break;
                case 8: currFlag = achievements.Achieve9; break;
                case 9: currFlag = achievements.Achieve10; break;
                case 10: currFlag = achievements.Achieve11; break;
                case 11: currFlag = achievements.Achieve12; break;
                case 12: currFlag = achievements.Achieve13; break;
                default:
                    return false;
            }
            return (currFlag & flag) != 0;
        }

        public Task<bool> AwardAchievementAsync(AchievementType fullFlag)
        {
            return AwardAchievementAsync((int)fullFlag);
        }

        public async Task<bool> AwardAchievementAsync(int fullFlag)
        {
            var result = IdToFlag(fullFlag);
            uint flag = 1u << result;
            int type = IdToType(fullFlag);

            uint currFlag;
            switch (type)
            {
                case 0: currFlag = achievements.Achieve1; break;
                case 1: currFlag = achievements.Achieve2; break;
                case 2: currFlag = achievements.Achieve3; break;
                case 3: currFlag = achievements.Achieve4; break;
                case 4: currFlag = achievements.Achieve5; break;
                case 5: currFlag = achievements.Achieve6; break;
                case 6: currFlag = achievements.Achieve7; break;
                case 7: currFlag = achievements.Achieve8; break;
                case 8: currFlag = achievements.Achieve9; break;
                case 9: currFlag = achievements.Achieve10; break;
                case 10: currFlag = achievements.Achieve11; break;
                case 11: currFlag = achievements.Achieve12; break;
                case 12: currFlag = achievements.Achieve13; break;
                default:
                    return false;
            }

            if ((currFlag & flag) != 0)
            {
                return false;
            }

            DbAchievementType achievementType = AchievementManager.GetAchievementType(fullFlag);
            if (achievementType == null)
            {
                return false;
            }

            Points += achievementType.Point;

            if (!await AchievementManager.ProcessAsync(user, fullFlag))
            {
                return false;
            }

            gmLogger.Information($"{user.Identity},{user.Name},{achievementType.Identity},{achievementType.Name},{achievementType.Point}");

            switch (type)
            {
                case 0: achievements.Achieve1 |= flag; break;
                case 1: achievements.Achieve2 |= flag; break;
                case 2: achievements.Achieve3 |= flag; break;
                case 3: achievements.Achieve4 |= flag; break;
                case 4: achievements.Achieve5 |= flag; break;
                case 5: achievements.Achieve6 |= flag; break;
                case 6: achievements.Achieve7 |= flag; break;
                case 7: achievements.Achieve8 |= flag; break;
                case 8: achievements.Achieve9 |= flag; break;
                case 9: achievements.Achieve10 |= flag; break;
                case 10: achievements.Achieve11 |= flag; break;
                case 11: achievements.Achieve12 |= flag; break;
                case 12: achievements.Achieve13 |= flag; break;
                default:
                    return false;
            }

            await SaveAsync();

            await user.SendAsync(new MsgAchievement
            {
                Action = AchievementRequest.Achieve,
                Identity = user.Identity,
                Flag = fullFlag
            });

            string message = string.Format(StrAchievementReceive, user.Name, fullFlag);
            await user.BroadcastRoomMsgAsync(message, TalkChannel.Talk, Color.White);
            if (user.Syndicate != null)
            {
                await user.Syndicate.SendAsync(message, 0, Color.White);
            }
            if (user.Family != null)
            {
                await user.Family.SendAsync(message, 0, Color.White);
            }
            if (user.Relation != null)
            {
                await user.Relation.SendToFriendsAsync(message, Color.White);
            }
            return true;
        }

        public Task SendAsync(Character target = null)
        {
            MsgAchievement msg = new()
            {
                Identity = user.Identity,
                Action = AchievementRequest.Synchro
            };
            msg.Flags.Add(achievements.Achieve1);
            msg.Flags.Add(achievements.Achieve2);
            msg.Flags.Add(achievements.Achieve3);
            msg.Flags.Add(achievements.Achieve4);
            msg.Flags.Add(achievements.Achieve5);
            msg.Flags.Add(achievements.Achieve6);
            msg.Flags.Add(achievements.Achieve7);
            msg.Flags.Add(achievements.Achieve8);
            msg.Flags.Add(achievements.Achieve9);
            msg.Flags.Add(achievements.Achieve10);
            msg.Flags.Add(achievements.Achieve11);
            msg.Flags.Add(achievements.Achieve12);
            msg.Flags.Add(achievements.Achieve13);
            if (target != null)
            {
                return target.SendAsync(msg);
            }
            return user.SendAsync(msg);
        }

        public Task SaveAsync()
        {
            return ServerDbContext.UpdateAsync(achievements);
        }

        private int IdToFlag(int id)
        {
            return (id / 100 % 100 - 1) * 32 + (id % 100 - 1);
        }

        private int IdToType(int id)
        {
            return (id / 100 % 100 - 1);
        }
    }
}
