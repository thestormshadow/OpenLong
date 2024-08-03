using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.User;
using Long.Shared.Helpers;

namespace Long.Kernel.States
{
    public sealed class DailySignIn
    {
        private static readonly ILogger logger = Log.ForContext<DailySignIn>();
        private static readonly ILogger gmLog = Logger.CreateLogger("signin");

        private static bool initialized;
        private static DbConfig dailyRewardConfig;
        private static DbConfig dailyRewardDayConfig;
        private static DbConfig dailyRewardActionConfig;

        private readonly Character user;
        private DbSignInEveryday signIn;

        public DailySignIn(Character user)
        {
            this.user = user;
        }

        public uint CurrentRewardIndex
        {
            get
            {
                int signInDays = SignInDays;
                if (signInDays < 2)
                    return 0;
                if (signInDays < 7) 
                    return 1;
                if (signInDays < 14)
                    return 2;
                if (signInDays < 21)
                    return 3;
                if (signInDays < 28)
                    return 4;
                return 5;
            }
        }

        public int SignInDays
        {
            get
            {
                int result = 0;
                for (int i = 1; i <= DateTime.Now.Day; i++)
                {
                    uint flag = DayToFlag(i);
                    if ((signIn.SignInDay & flag) == flag)
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public byte RemainingCamulate => (byte)Math.Max(0, FillSignInLimit - signIn.FillSignTimes);

        public uint FillSignInLimit
        {
            get
            {
                switch (user.VipLevel)
                {
                    case 1: return 1;
                    case 2: return 2;
                    case 3: return 4;
                    case 4: return 6;
                    case 5: return 8;
                    case 6: return 10;
                    default: return 0;
                }
            }
        }

        public static async Task StaticInitializeAsync()
        {
            logger.Information("Initialize daily signin data");
            
            dailyRewardConfig = await ConfigRepository.GetSingleAsync(6015);
            dailyRewardDayConfig = await ConfigRepository.GetSingleAsync(6016);
            dailyRewardActionConfig = await ConfigRepository.GetSingleAsync(6017);
            initialized = dailyRewardConfig != null && dailyRewardDayConfig != null && dailyRewardActionConfig != null;
        }

        public async Task InitializeAsync()
        {
            signIn = DailySignInRepository.Get(user.Identity);
            signIn ??= new DbSignInEveryday
            {
                PlayerId = user.Identity
            };
            await SendAsync();
        }

        public async Task<bool> SignInAsync()
        {
            if (!initialized)
            {
                return false;
            }

            uint dayFlag = DayToFlag(DateTime.Now.Day);
            if ((signIn.SignInDay & dayFlag) ==  dayFlag)
            {
                return false;
            }

            signIn.SignInDay |= dayFlag;
            await GameAction.ExecuteActionAsync((uint)dailyRewardConfig.Data1, user, user, null);
            await GetAccmulateRewardAsync();
            await SaveAsync();
            return true;
        }

        public async Task<bool> LateSignInAsync()
        {
            if (!initialized)
            {
                return false;
            }

            if (DateTime.Now.Day <= 1)
            {
                return false;
            }

            if (signIn.FillSignTimes >= FillSignInLimit)
            {
                return false;
            }

            uint dayFlag = 0;
            for (int day = DateTime.Now.Day - 1; day >= 1; day--)
            {
                dayFlag = DayToFlag(day);
                if ((signIn.SignInDay & dayFlag) == 0)
                {
                    break;
                }
            }

            signIn.SignInDay |= dayFlag;
            signIn.FillSignTimes += 1;
            await GameAction.ExecuteActionAsync((uint)dailyRewardConfig.Data1, user, user, null);
            await GetAccmulateRewardAsync();
            await SaveAsync();
            return true;
        }

        public async Task<bool> GetAccmulateRewardAsync()
        {
            if (!initialized)
            {
                return false;
            }

            int claim = 0;
            int days = SignInDays;
            if (days >= dailyRewardDayConfig.Data1 && signIn.AwardCamulate < 1)
            {
                signIn.AwardCamulate = 1;
                claim++;
                await GameAction.ExecuteActionAsync((uint)dailyRewardActionConfig.Data1, user, user, null);
            }

            if (days >= dailyRewardDayConfig.Data2 && signIn.AwardCamulate < 2)
            {
                signIn.AwardCamulate = 2;
                claim++;
                await GameAction.ExecuteActionAsync((uint)dailyRewardActionConfig.Data2, user, user, null);
            }

            if (days >= dailyRewardDayConfig.Data3 && signIn.AwardCamulate < 3)
            {
                signIn.AwardCamulate = 3;
                claim++;
                await GameAction.ExecuteActionAsync((uint)dailyRewardActionConfig.Data3, user, user, null);
            }

            if (days >= dailyRewardDayConfig.Data4 && signIn.AwardCamulate < 4)
            {
                signIn.AwardCamulate = 4;
                claim++;
                await GameAction.ExecuteActionAsync((uint)dailyRewardActionConfig.Data4, user, user, null);
            }

            if (days >= dailyRewardDayConfig.Data5 && signIn.AwardCamulate < 5)
            {
                signIn.AwardCamulate = 5;
                claim++;
                await GameAction.ExecuteActionAsync((uint)dailyRewardActionConfig.Data5, user, user, null);
            }

            return claim != 0;
        }

        private uint DayToFlag(int day)
        {
            return (1u << (day - 1));
        }

        public Task SendAsync()
        {
            if (signIn != null)
            {
				return user.SendAsync(new MsgSignIn
				{
					Action = MsgSignIn.MsgSignInType.Display,
					RewardAmount = signIn.AwardCamulate,
					SignInDays = signIn.SignInDay,
					CanFillSignInTimes = RemainingCamulate
				});
			}
            else
            {
                InitializeAsync();
			}
            return Task.CompletedTask;
        }

        public Task SaveAsync()
        {
            if (signIn.Id == 0)
            {
                return ServerDbContext.CreateAsync(signIn);
            }
            return ServerDbContext.UpdateAsync(signIn);
        }

        public Task ResetAsync()
        {
            signIn = new DbSignInEveryday
            {
                PlayerId = user.Identity
            };
            return Task.CompletedTask;
        }
    }
}
