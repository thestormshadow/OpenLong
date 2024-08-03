using Long.Database.Entities;
using Long.Module.Guide.Repositories;
using Long.Module.Guide.States;
using Serilog;

namespace Long.Module.Guide.Managers
{
    public class GuideManager
    {
        private static object guideLock = new object();
        private static readonly List<Tutor> guideRelations = new List<Tutor>();
        private static readonly List<DbTutorType> tutorType = new();
        private static readonly List<DbTutorBattleLimitType> tutorBattleLimitType = new();

        public static async Task InitializeAsync()
        {
            var guides = await TutorRepository.GetAsync();
            foreach (var g in guides)
            {
                var guide = await Tutor.CreateAsync(g);
                if (guide != null)
                {
                    guideRelations.Add(guide);
                }
            }

            tutorType.AddRange(await TutorTypeRepository.GetAsync());
            tutorBattleLimitType.AddRange(await TutorBattleLimitTypeRepository.GetAsync());
        }

        public static Tutor GetTutor(uint studentId)
        {
            lock (guideLock)
            {
                return guideRelations.FirstOrDefault(x => x.StudentIdentity == studentId);
            }
        }

        public static IList<Tutor> GetStudents(uint tutorId)
        {
            lock (guideLock)
            {
                return guideRelations.Where(x => x.GuideIdentity == tutorId).ToList();
            }
        }

        public static DbTutorBattleLimitType GetTutorBattleLimitType(int delta)
        {
            return tutorBattleLimitType.Aggregate((x, y) => Math.Abs(x.Id - delta) < Math.Abs(y.Id - delta) ? x : y);
        }

        public static DbTutorType GetTutorType(int level)
        {
            return tutorType.FirstOrDefault(x => level >= x.UserMinLevel && level <= x.UserMaxLevel);
        }
    }
}
