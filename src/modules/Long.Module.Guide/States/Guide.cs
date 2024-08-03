using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Modules.Systems.Guide;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Guide.Managers;
using Long.Module.Guide.Repositories;
using Long.Shared;
using System.Collections.Concurrent;
using static Long.Kernel.StrRes;

namespace Long.Module.Guide.States
{
    public sealed class Guide : IGuide
    {
        private readonly Character user;
        private DbTutorAccess tutorAccess;
        private ConcurrentDictionary<uint, ITutor> students = new ();

        public Guide(Character user)
        {
            this.user = user;
        }

        public ITutor Tutor { get; set; }

        public async Task InitializeAsync()
        {
            Tutor = GuideManager.GetTutor(user.Identity);
            if (Tutor != null)
            {
                await Tutor.SendTutorAsync();
                await Tutor.SendStudentAsync();

                Character guide = Tutor.Guide;
                if (guide != null)
                {
                    await user.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower, (uint)Tutor.SharedBattlePower, (uint)guide.BattlePower);
                    await guide.SendAsync(string.Format(StrGuideStudentLogin, user.Name));
                }
            }

            IList<Tutor> apprentices = GuideManager.GetStudents(user.Identity);
            foreach (Tutor apprentice in apprentices)
            {
                students.TryAdd(apprentice.StudentIdentity, apprentice);
                await apprentice.SendTutorAsync();
                await apprentice.SendStudentAsync();

                Character student = apprentice.Student;
                if (student != null)
                {
                    await student.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower, (uint)apprentice.SharedBattlePower, (uint)user.BattlePower);
                    await student.SendAsync(string.Format(StrGuideTutorLogin, user.Name));
                }
            }

            tutorAccess = await TutorAccessRepository.GetAsync(user.Identity);
        }

        public int ApprenticeCount => students.Count;

        public ulong MentorExpTime
        {
            get => tutorAccess?.Experience ?? 0;
            set
            {
                tutorAccess ??= new DbTutorAccess
                {
                    GuideIdentity = user.Identity
                };
                tutorAccess.Experience = value;
            }
        }

        public ushort MentorAddLevexp
        {
            get => tutorAccess?.Composition ?? 0;
            set
            {
                tutorAccess ??= new DbTutorAccess
                {
                    GuideIdentity = user.Identity
                };
                tutorAccess.Composition = value;
            }
        }

        public ushort MentorGodTime
        {
            get => tutorAccess?.Blessing ?? 0;
            set
            {
                tutorAccess ??= new DbTutorAccess
                {
                    GuideIdentity = user.Identity
                };
                tutorAccess.Blessing = value;
            }
        }

        public static async Task<bool> CreateTutorRelationAsync(Character guide, Character apprentice)
        {
            if (guide.Level < apprentice.Level || guide.Metempsychosis < apprentice.Metempsychosis)
            {
                return false;
            }

            int deltaLevel = guide.Level - apprentice.Level;
            if (apprentice.Metempsychosis == 0)
            {
                if (deltaLevel < 30)
                {
                    return false;
                }
            }
            else if (apprentice.Metempsychosis == 1)
            {
                if (deltaLevel < 20)
                {
                    return false;
                }
            }
            else
            {
                if (deltaLevel < 10)
                {
                    return false;
                }
            }

            DbTutorType type = GuideManager.GetTutorType(guide.Level);
            if (type == null || guide.Guide.ApprenticeCount >= type.StudentNum)
            {
                return false;
            }

            if (apprentice.Guide.Tutor != null)
            {
                return false;
            }

            if (guide.Guide.IsApprentice(apprentice.Identity))
            {
                return false;
            }

            var dbTutor = new DbTutor
            {
                GuideId = guide.Identity,
                StudentId = apprentice.Identity,
                Date = (uint)UnixTimestamp.Now
            };
            if (!await ServerDbContext.CreateAsync(dbTutor))
            {
                return false;
            }

            var tutor = await States.Tutor.CreateAsync(dbTutor);

            apprentice.Guide.Tutor = tutor;
            await tutor.SendTutorAsync();
            guide.Guide.AddStudent(tutor);
            await tutor.SendStudentAsync();
            await apprentice.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower, (uint)tutor.SharedBattlePower, (uint)guide.BattlePower);
            return true;
        }

        public async Task SynchroStudentsAsync()
        {
            foreach (var apprentice in students.Values.Where(x => x.Student != null))
            {
                await apprentice.SendTutorAsync();
                await apprentice.SendStudentAsync();
            }
        }

        public async Task SynchroApprenticesSharedBattlePowerAsync()
        {
            foreach (Tutor apprentice in students.Values.Where(x => x.Student != null))
            {
                await apprentice.Student.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower,
                                                                (uint)apprentice.SharedBattlePower,
                                                                (uint)(apprentice.Guide?.BattlePower ?? 0));
            }
        }

        public bool IsTutor(uint idApprentice)
        {
            return students.ContainsKey(idApprentice);
        }

        public bool IsApprentice(uint idGuide)
        {
            return Tutor?.GuideIdentity == idGuide;
        }

        public bool AddStudent(ITutor student)
        {
            return students.TryAdd(student.StudentIdentity, student);
        }

        public void RemoveApprentice(uint idApprentice)
        {
            students.TryRemove(idApprentice, out _);
        }

        public ITutor GetStudent(uint idStudent)
        {
            return students.TryGetValue(idStudent, out var value) ? value : null;
        }

        public Task SaveAsync()
        {
            if (tutorAccess.Identity == 0)
            {
                return ServerDbContext.CreateAsync(tutorAccess);
            }
            return ServerDbContext.UpdateAsync(tutorAccess);
        }

        public async Task OnLogoutAsync()
        {
            if (Tutor != null)
            {
                await Tutor.SendStudentOfflineAsync();
            }

            foreach (Tutor apprentice in students.Values.Where(x => x.Student != null).Cast<Tutor>())
            {
                await apprentice.SendTutorOfflineAsync();
                await apprentice.Student.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower, 0, 0);
            }
        }
    }
}
