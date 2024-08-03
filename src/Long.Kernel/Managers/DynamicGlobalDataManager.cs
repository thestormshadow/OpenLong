using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class DynamicGlobalDataManager
    {
        public const int MaxParams = 6;

        private static readonly ILogger logger = Log.ForContext<DynamicGlobalDataManager>();
        private static readonly ConcurrentDictionary<uint, DbDynaGlobalData> globalData = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Initializing Dynamic Global Data Manager!!!");

            var data = await DynamicGlobalDataRepository.GetAsync();
            foreach (var g in data)
            {
                globalData.TryAdd(g.Id, g);
            }
        }

        public static Task<bool> SaveAsync(DbDynaGlobalData data)
        {
            if (data.Id == 0)
            {
                return ServerDbContext.CreateAsync(data);
            }
            return ServerDbContext.UpdateAsync(data);
        }

        public static async Task<DbDynaGlobalData> GetAsync(uint id)
        {
            if (globalData.TryGetValue(id, out var result))
            {
                return result;
            }
            result = new DbDynaGlobalData
            {
                Id = id,
                Datastr0 = string.Empty,
                Datastr1 = string.Empty,
                Datastr2 = string.Empty,
                Datastr3 = string.Empty,
                Datastr4 = string.Empty,
                Datastr5 = string.Empty,
            };
            globalData.TryAdd(id, result);
            await ServerDbContext.CreateAsync(result);
            return result;
        }

        public static long GetData(DbDynaGlobalData data, int idx)
        {
            switch (idx)
            {
                case 0: return data.Data0;
                case 1: return data.Data1;
                case 2: return data.Data2;
                case 3: return data.Data3;
                case 4: return data.Data4;
                case 5: return data.Data5;
                default: return 0;
            }
        }

        public static void ChangeData(DbDynaGlobalData data, int idx, long value)
        {
            switch (idx)
            {
                case 0: data.Data0 = value; break;
                case 1: data.Data1 = value; break;
                case 2: data.Data2 = value; break;
                case 3: data.Data3 = value; break;
                case 4: data.Data4 = value; break;
                case 5: data.Data5 = value; break;
            }
        }

        public static string GetStringData(DbDynaGlobalData data, int idx)
        {
            switch (idx)
            {
                case 0: return data.Datastr0;
                case 1: return data.Datastr1;
                case 2: return data.Datastr2;
                case 3: return data.Datastr3;
                case 4: return data.Datastr4;
                case 5: return data.Datastr5;
                default: return string.Empty;
            }
        }

        public static void ChangeStringData(DbDynaGlobalData data, int idx, string value)
        {
            switch (idx)
            {
                case 0: data.Datastr1 = value; break;
                case 1: data.Datastr1 = value; break;
                case 2: data.Datastr2 = value; break;
                case 3: data.Datastr3 = value; break;
                case 4: data.Datastr4 = value; break;
                case 5: data.Datastr5 = value; break;
            }
        }

        public static uint GetTime(DbDynaGlobalData data, int idx)
        {
            switch (idx)
            {
                case 0: return data.Time0;
                case 1: return data.Time1;
                case 2: return data.Time2;
                case 3: return data.Time3;
                case 4: return data.Time4;
                case 5: return data.Time5;
                default: return 0;
            }
        }

        public static void ChangeTime(DbDynaGlobalData data, int idx, uint value)
        {
            switch (idx)
            {
                case 0: data.Time0 = value; break;
                case 1: data.Time1 = value; break;
                case 2: data.Time2 = value; break;
                case 3: data.Time3 = value; break;
                case 4: data.Time4 = value; break;
                case 5: data.Time5 = value; break;
            }
        }
    }
}
