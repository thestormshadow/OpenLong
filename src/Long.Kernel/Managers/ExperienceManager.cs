using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.Magics;
using System.Collections.Concurrent;
using static Long.Kernel.Modules.Systems.AstProf.IAstProf;

namespace Long.Kernel.Managers
{
	public class ExperienceManager
	{
		private static readonly ILogger logger = Log.ForContext<ExperienceManager>();

		private static readonly ConcurrentDictionary<byte, DbLevelExperience> userLevelExperience = new();
		private static readonly ConcurrentDictionary<AstProfType, List<DbLevelExperience>> subProfessionLevelExperience = new();
		private static readonly ConcurrentDictionary<uint, DbPointAllot> pointAllot = new();
		private static readonly List<DbRebirth> rebirths = new();
		private static readonly List<MagicTypeOperation> magicTypeOperations = new();
		private static ConcurrentDictionary<uint, ExperienceMultiplierData> experienceMultiplierData = new();

		public static async Task<bool> InitializeAsync()
		{
			logger.Information("Starting experience manager");

			foreach (DbPointAllot auto in await PointAllotRepository.GetAsync())
			{
				pointAllot.TryAdd(AllotIndex(auto.Profession, auto.Level), auto);
			}

			List<DbLevelExperience> levelExperiences = await LevelExperienceRepository.GetAsync();
			foreach (DbLevelExperience lev in levelExperiences.Where(x => x.Type == 0))
			{
				userLevelExperience.TryAdd(lev.Level, lev);
			}

			foreach (DbLevelExperience lev in levelExperiences
				.Where(x => x.Type >= 1 && x.Type <= 9)
				.OrderBy(x => x.Type)
				.ThenBy(x => x.Level))
			{
				if (!subProfessionLevelExperience.TryGetValue((AstProfType)lev.Type, out var list))
				{
					subProfessionLevelExperience.TryAdd((AstProfType)lev.Type, new List<DbLevelExperience>());
				}
				subProfessionLevelExperience[(AstProfType)lev.Type].Add(lev);
			}

			rebirths.AddRange(await RebirthRepository.GetAsync());

			foreach (DbMagictypeOp operation in await MagictypeOperationRepository.GetAsync())
			{
				magicTypeOperations.Add(new MagicTypeOperation(operation));
			}

			return true;
		}

		public static DbRebirth GetRebirth(int profNow, int profNext, int currMete)
		{
			return rebirths.FirstOrDefault(x => x.NeedProfession == profNow && x.NewProfession == profNext && x.Metempsychosis == currMete);
		}

		public static List<ushort> GetMagictypeOp(MagicTypeOperation.MagicOperation op, int profNow, int profNext, int metempsychosis)
		{
			List<ushort> list = new();
			foreach (var ope in magicTypeOperations.Where(x => x.ProfessionAgo == profNow && x.ProfessionNow == profNext &&
												  x.RebirthTime == metempsychosis && x.Operation == op))
			{
				list.AddRange(ope.Magics);
			}
			return list;
		}

		public static List<ushort> GetMagictypeOp(MagicTypeOperation.MagicOperation op, int profession)
		{
			List<ushort> list = new();
			foreach (var ope in magicTypeOperations.Where(x => x.ProfessionNow == profession && x.Operation == op))
			{
				list.AddRange(ope.Magics);
			}
			return list;
		}

		public static DbLevelExperience GetLevelExperience(byte level)
		{
			return userLevelExperience.TryGetValue(level, out DbLevelExperience value) ? value : null;
		}

		public static DbLevelExperience GetAstProfExperience(AstProfType type, int currentLevel)
		{
			if (subProfessionLevelExperience.TryGetValue(type, out var list))
			{
				return list.FirstOrDefault(x => x.Level == currentLevel);
			}
			return null;
		}

		public static int GetLevelLimit()
		{
			return userLevelExperience.Count + 1;
		}

		public static DbPointAllot GetPointAllot(ushort profession, ushort level)
		{
			return pointAllot.TryGetValue(AllotIndex(profession, level), out DbPointAllot point) ? point : null;
		}

		private static uint AllotIndex(ushort prof, ushort level)
		{
			return (uint)((prof << 16) + level);
		}

		public static bool AddExperienceMultiplierData(uint idUser, float multiplier, int seconds)
		{
			ExperienceMultiplierData value = GetExperienceMultiplierData(idUser);
			if (!value.Equals(default) && value.ExperienceMultiplier > multiplier)
			{
				return false;
			}

			experienceMultiplierData.TryRemove(idUser, out _);

			value = new ExperienceMultiplierData
			{
				UserId = idUser,
				ExperienceMultiplier = multiplier,
				EndTime = DateTime.Now.AddSeconds(seconds)
			};
			return experienceMultiplierData.TryAdd(idUser, value);
		}

		public static ExperienceMultiplierData GetExperienceMultiplierData(uint idUser)
		{
			if (!experienceMultiplierData.TryGetValue(idUser, out var data))
			{
				return default;
			}
			if (DateTime.Now > data.EndTime)
			{
				experienceMultiplierData.TryRemove(idUser, out _);
				return default;
			}
			return data;
		}

		public struct ExperienceMultiplierData
		{
			public uint UserId { get; set; }
			public float ExperienceMultiplier { get; set; }
			public DateTime EndTime { get; set; }
			public readonly bool IsActive => EndTime > DateTime.Now;
			public readonly int RemainingSeconds => (int)(IsActive ? (EndTime - DateTime.Now).TotalSeconds : 0);
		}
	}
}
