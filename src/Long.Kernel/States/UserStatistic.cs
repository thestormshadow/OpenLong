using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.User;

namespace Long.Kernel.States
{
    public sealed class UserStatistic
    {
        private readonly List<DbStatistic> statistics = new();
        private readonly List<DbStatisticDaily> dailyStatistics = new();

        private readonly Character user;

        public UserStatistic(Character user)
        {
            this.user = user;
        }

        public async Task<bool> InitializeAsync()
        {
            try
            {
                var list = await StatisticRepository.GetAsync(user.Identity);
                foreach (var st in list)
                {
                    statistics.Add(st);
                }

                var dailyStc = await StatisticRepository.GetDailyAsync(user.Identity);
                foreach (var stc in dailyStc)
                {
                    dailyStatistics.Add(stc);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Statistic

        public Task<bool> IncrementValueAsync(uint idEvent, uint idType, uint amount = 1, bool update = true)
        {
            var currentStc = GetStc(idEvent, idType);
            uint data = amount;
            if (currentStc != null)
            {
                data += currentStc.Data;
            }
            return AddOrUpdateAsync(idEvent, idType, data, update);
        }

        public Task<bool> AddOrUpdateAsync(uint idEvent, uint idType, uint data, bool bUpdate)
        {
            var stc = GetStc(idEvent, idType);
            if (stc != null)
            {
                stc.Data = data;
                if (bUpdate)
                {
                    stc.Timestamp = (uint)DateTime.Now.ToUnixTimestamp();
                }
                return ServerDbContext.UpdateAsync(stc);
            }
            else
            {
                stc = new DbStatistic
                {
                    Data = data,
                    DataType = idType,
                    EventType = idEvent,
                    PlayerIdentity = user.Identity,
                    Timestamp = (uint)DateTime.Now.ToUnixTimestamp()
                };
                statistics.Add(stc);
                return ServerDbContext.CreateAsync(stc);
            }
        }

        public async Task<bool> SetTimestampAsync(uint idEvent, uint idType, DateTime? data)
        {
            DbStatistic stc = GetStc(idEvent, idType);
            if (stc == null)
            {
                await AddOrUpdateAsync(idEvent, idType, 0, true);
                stc = GetStc(idEvent, idType);

                if (stc == null)
                {
                    return false;
                }
            }

            stc.Timestamp = (uint)UnixTimestamp.FromDateTime(data);
            return await ServerDbContext.UpdateAsync(stc);
        }

        public uint GetValue(uint idEvent, uint idType = 0)
        {
            return statistics.FirstOrDefault(x => x.EventType == idEvent && x.DataType == idType)?.Data ?? 0u;
        }

        public DbStatistic GetStc(uint idEvent, uint idType = 0)
        {
            return statistics.FirstOrDefault(x => x.EventType == idEvent && x.DataType == idType);
        }

        public List<DbStatistic> GetStcList(uint idEvent)
        {
            return statistics.Where(x => x.EventType == idEvent).ToList();
        }

        public bool HasEvent(uint idEvent, uint idType)
        {
            return statistics.Any(x => x.EventType == idEvent && x.DataType == idType);
        }

        #endregion

        #region Daily Statistic

        public Task<bool> IncrementDailyValueAsync(uint idEvent, uint idType, uint amount = 1)
        {
            var currentStc = GetDailyStc(idEvent, idType);
            uint data = amount;
            if (currentStc != null)
            {
                data += currentStc.Data;
            }
            return AddOrUpdateDailyAsync(idEvent, idType, data);
        }

        public Task<bool> AddOrUpdateDailyAsync(uint idEvent, uint idType, uint data)
        {
            var stc = GetDailyStc(idEvent, idType);
            if (stc != null)
            {
                stc.Data = data;
                return ServerDbContext.UpdateAsync(stc);
            }
            else
            {
                stc = new DbStatisticDaily
                {
                    Data = data,
                    SubType = idType,
                    Type = idEvent,
                    PlayerId = user.Identity
                };
                dailyStatistics.Add(stc);
                return ServerDbContext.CreateAsync(stc);
            }
        }

        public async Task<bool> DeleteDailyAsync(uint idEvent, uint idType)
        {
            var stc = GetDailyStc(idEvent, idType);
            if (stc == null)
            {
                return false;
            }
            return await ServerDbContext.DeleteAsync(stc);
        }

        public uint GetDailyValue(uint idEvent, uint idType = 0)
        {
            return dailyStatistics.FirstOrDefault(x => x.Type == idEvent && x.SubType == idType)?.Data ?? 0u;
        }

        public DbStatisticDaily GetDailyStc(uint idEvent, uint idType = 0)
        {
            return dailyStatistics.FirstOrDefault(x => x.Type == idEvent && x.SubType == idType);
        }

        public bool HasDailyEvent(uint idEvent, uint idType)
        {
            return dailyStatistics.Any(x => x.Type == idEvent && x.SubType == idType);
        }

        public void ClearDailyStatistic()
        {
            dailyStatistics.Clear();
        }

        #endregion
    }
}
