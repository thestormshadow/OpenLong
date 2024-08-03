using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class MagicManager
    {
        private static readonly ILogger logger = Log.ForContext<MagicManager>();
        private static readonly ConcurrentDictionary<uint, DbMagictype> magicTypes = new();
        private static readonly ConcurrentDictionary<uint, DbMonsterTypeMagic> monsterMagics = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Initializing Magic Manager");

            foreach (DbMagictype magicType in await MagicTypeRepository.GetAsync())
            {
                magicTypes.TryAdd(magicType.Id, magicType);
            }

            foreach (DbMonsterTypeMagic magic in await MonsterTypeMagicRepository.GetAsync())
            {
                monsterMagics.TryAdd(magic.Id, magic);
            }
        }

        public static byte GetMaxLevel(uint idType)
        {
            return (byte)(magicTypes.Values.Where(x => x.Type == idType).OrderByDescending(x => x.Level)
                                      .FirstOrDefault()?.Level ?? 0);
        }

        public static DbMagictype GetMagictype(uint idType, ushort level)
        {
            return magicTypes.Values.FirstOrDefault(x => x.Type == idType && x.Level == level);
        }

        public static List<DbMonsterTypeMagic> GetMonsterMagics(uint type)
        {
            return monsterMagics.Values.Where(x => x.MonsterType == type).ToList();
        }
    }
}
