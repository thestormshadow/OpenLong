using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Guide;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Guide.Managers;
using Long.Module.Guide.Network;
using Long.Module.Guide.Repositories;
using Long.Shared;
using static Long.Kernel.StrRes;

namespace Long.Module.Guide.States
{
    public sealed class Tutor : ITutor
    {
        public const int BETRAYAL_FLAG_TIMEOUT = 60 * 60 * 24 * 3;
        public const int STUDENT_BETRAYAL_VALUE = 50000;

        private readonly TimeOut betrayalCheck = new();

        private DbTutor tutor;
        private DbTutorContribution access;

        private Tutor()
        {
        }

        public static async Task<Tutor> CreateAsync(DbTutor tutor)
        {
            var guide = new Tutor
            {
                tutor = tutor,
                access = await TutorContributionRepository.GetGuideAsync(tutor.StudentId)
            };
            guide.access ??= new DbTutorContribution
            {
                TutorIdentity = tutor.GuideId,
                StudentIdentity = tutor.StudentId
            };

            DbUser dbMentor = await UserRepository.FindByIdentityAsync(tutor.GuideId);
            if (dbMentor == null)
            {
                return null;
            }

            guide.GuideName = dbMentor.Name;

            dbMentor = await UserRepository.FindByIdentityAsync(tutor.StudentId);
            if (dbMentor == null)
            {
                return null;
            }

            guide.StudentName = dbMentor.Name;

            if (guide.Betrayed)
            {
                guide.betrayalCheck.Startup(60);
            }

            return guide;
        }

        public uint GuideIdentity => tutor.GuideId;
        public string GuideName { get; private set; }

        public uint StudentIdentity => tutor.StudentId;
        public string StudentName { get; private set; }

        public bool Betrayed => tutor.BetrayalFlag != 0;
        public bool BetrayalCheck => Betrayed && betrayalCheck.IsActive() && betrayalCheck.ToNextTime();

        public Character Guide => RoleManager.GetUser(tutor.GuideId);
        public Character Student => RoleManager.GetUser(tutor.StudentId);

        public async Task<bool> AwardTutorExperienceAsync(uint addExpTime)
        {
            access.Experience += addExpTime;

            Character user = RoleManager.GetUser(access.TutorIdentity);
            if (user != null)
            {
                user.Guide.MentorExpTime += addExpTime;
            }
            else
            {
                DbTutorAccess tutorAccess = await TutorAccessRepository.GetAsync(access.TutorIdentity);
                tutorAccess ??= new DbTutorAccess
                {
                    GuideIdentity = GuideIdentity
                };
                tutorAccess.Experience += addExpTime;
                if (tutorAccess.Identity == 0)
                {
                    await ServerDbContext.CreateAsync(tutorAccess);
                }
                else
                {
                    await ServerDbContext.UpdateAsync(tutorAccess);
                }
            }

            return await SaveAsync();
        }

        public async Task<bool> AwardTutorGodTimeAsync(ushort addGodTime)
        {
            access.GodTime += addGodTime;

            Character user = RoleManager.GetUser(access.TutorIdentity);
            if (user != null)
            {
                user.Guide.MentorGodTime += addGodTime;
            }
            else
            {
                DbTutorAccess tutorAccess = await TutorAccessRepository.GetAsync(access.TutorIdentity);
                tutorAccess ??= new DbTutorAccess
                {
                    GuideIdentity = GuideIdentity
                };
                tutorAccess.Blessing += addGodTime;
                if (tutorAccess.Identity == 0)
                {
                    await ServerDbContext.CreateAsync(tutorAccess);
                }
                else
                {
                    await ServerDbContext.UpdateAsync(tutorAccess);
                }
            }

            return await SaveAsync();
        }

        public async Task<bool> AwardOpportunityAsync(ushort addTime)
        {
            access.PlusStone += addTime;

            Character user = RoleManager.GetUser(access.TutorIdentity);
            if (user != null)
            {
                user.Guide.MentorAddLevexp += addTime;
            }
            else
            {
                DbTutorAccess tutorAccess = await TutorAccessRepository.GetAsync(access.TutorIdentity);
                tutorAccess ??= new DbTutorAccess
                {
                    GuideIdentity = GuideIdentity
                };
                tutorAccess.Composition += addTime;
                if (tutorAccess.Identity == 0)
                {
                    await ServerDbContext.CreateAsync(tutorAccess);
                }
                else
                {
                    await ServerDbContext.UpdateAsync(tutorAccess);
                }
            }

            return await SaveAsync();
        }

        public int SharedBattlePower
        {
            get
            {
                Character mentor = Guide;
                Character student = Student;
                if (mentor == null || student == null)
                {
                    return 0;
                }

                if (mentor.PureBattlePower < student.PureBattlePower)
                {
                    return 0;
                }

                if (Betrayed)
                {
                    return 0;
                }

                DbTutorBattleLimitType limit = GuideManager.GetTutorBattleLimitType(student.PureBattlePower);
                if (limit == null)
                {
                    return 0;
                }

                DbTutorType type = GuideManager.GetTutorType(mentor.Level);
                if (type == null)
                {
                    return 0;
                }

                return (int)Math.Min(limit.BattleLevelLimit,
                                      (mentor.PureBattlePower - student.PureBattlePower) *
                                      (type.BattleLevelShare / 100f));
            }
        }

        public async Task BetrayAsync()
        {
            tutor.BetrayalFlag = UnixTimestamp.Now;
            await SaveAsync();
        }

        public async Task SendTutorAsync()
        {
            if (Student == null)
            {
                return;
            }

            int betrayalHours = 0;
            if (Betrayed)
            {
                betrayalHours = (int)(48 - (DateTime.Now - UnixTimestamp.ToDateTime(tutor.BetrayalFlag)).TotalHours);
            }

            await Student.SendAsync(new MsgGuideInfo
            {
                Identity = StudentIdentity,
                Level = Guide?.Level ?? 0,
                Blessing = access.GodTime,
                Composition = (ushort)access.PlusStone,
                Experience = access.Experience,
                IsOnline = Guide != null,
                Mesh = Guide?.Mesh ?? 0,
                Mode = MsgGuideInfo.RequestMode.Mentor,
                Syndicate = Guide?.SyndicateIdentity ?? 0,
                SyndicatePosition = (ushort)(Guide?.SyndicateRank ?? ISyndicateMember.SyndicateRank.None),
                Names = new List<string>
                {
                    GuideName,
                    StudentName,
                    Guide?.MateName ?? StrNone
                },
                EnroleDate = uint.Parse(UnixTimestamp.ToDateTime((int)tutor.Date).ToString("yyyyMMdd") ?? "0"),
                PkPoints = Guide?.PkPoints ?? 0,
                Profession = Guide?.Profession ?? 0,
                SharedBattlePower = (uint)SharedBattlePower,
                SenderIdentity = GuideIdentity,
                BetrayHour = (uint)(Betrayed ? betrayalHours : 999999)
            });
        }

        public Task SendTutorOfflineAsync()
        {
            if (Student == null)
            {
                return Task.CompletedTask;
            }

            int betrayalHours = 0;
            if (Betrayed)
            {
                betrayalHours = (int)(48 - (DateTime.Now - UnixTimestamp.ToDateTime(tutor.BetrayalFlag)).TotalHours);
            }

            return Student.SendAsync(new MsgGuideInfo
            {
                Identity = StudentIdentity,
                Level = 0,
                Blessing = access.GodTime,
                Composition = (ushort)access.PlusStone,
                Experience = access.Experience,
                IsOnline = false,
                Mesh = 0,
                Mode = MsgGuideInfo.RequestMode.Mentor,
                Syndicate = 0,
                SyndicatePosition = 0,
                Names = new List<string>
                {
                    GuideName,
                    StudentName,
                    StrNone
                },
                EnroleDate = uint.Parse(UnixTimestamp.ToDateTime((int)tutor.Date).ToString("yyyyMMdd") ?? "0"),
                PkPoints = 0,
                Profession = 0,
                SharedBattlePower = 0,
                SenderIdentity = GuideIdentity,
                BetrayHour = (uint)(Betrayed ? betrayalHours : 999999)
            });
        }

        public async Task SendStudentAsync()
        {
            if (Guide == null)
            {
                return;
            }

            int betrayalHours = 0;
            if (Betrayed)
            {
                betrayalHours = (int)(48 - (DateTime.Now - UnixTimestamp.ToDateTime(tutor.BetrayalFlag)).TotalHours);
            }

            await Guide.SendAsync(new MsgGuideInfo
            {
                Identity = StudentIdentity,
                Level = Student?.Level ?? 0,
                Blessing = access.GodTime,
                Composition = (ushort)access.PlusStone,
                Experience = access.Experience,
                IsOnline = Student != null,
                Mesh = Student?.Mesh ?? 0,
                Mode = MsgGuideInfo.RequestMode.Apprentice,
                Syndicate = Student?.SyndicateIdentity ?? 0,
                SyndicatePosition = (ushort)(Student?.SyndicateRank ?? ISyndicateMember.SyndicateRank.None),
                Names = new List<string>
                {
                    GuideName,
                    StudentName,
                    Student?.MateName ?? StrNone
                },
                EnroleDate = uint.Parse(UnixTimestamp.ToDateTime((int)tutor.Date).ToString("yyyyMMdd") ?? "0"),
                PkPoints = Student?.PkPoints ?? 0,
                Profession = Student?.Profession ?? 0,
                SharedBattlePower = 0,
                SenderIdentity = GuideIdentity,
                BetrayHour = (uint)(Betrayed ? betrayalHours : 999999)
            });
        }

        public Task SendStudentOfflineAsync()
        {
            if (Guide == null)
            {
                return Task.CompletedTask;
            }

            int betrayalHours = 0;
            if (Betrayed)
            {
                betrayalHours = (int)(48 - (DateTime.Now - UnixTimestamp.ToDateTime(tutor.BetrayalFlag)).TotalHours);
            }

            return Guide.SendAsync(new MsgGuideInfo
            {
                Identity = StudentIdentity,
                Level = 0,
                Blessing = access.GodTime,
                Composition = (ushort)access.PlusStone,
                Experience = access.Experience,
                IsOnline = false,
                Mesh = 0,
                Mode = MsgGuideInfo.RequestMode.Apprentice,
                Syndicate = 0,
                SyndicatePosition = 0,
                Names = new List<string>
                {
                    GuideName,
                    StudentName,
                    StrNone
                },
                EnroleDate = uint.Parse(UnixTimestamp.ToDateTime((int)tutor.Date).ToString("yyyyMMdd") ?? "0"),
                PkPoints = 0,
                Profession = 0,
                SharedBattlePower = 0,
                SenderIdentity = GuideIdentity,
                BetrayHour = (uint)(Betrayed ? betrayalHours : 999999)
            });
        }

        public async Task BetrayalTimerAsync()
        {
            /*
             * Since this will be called in a queue, it might be called twice per run, so we will trigger the TimeOut
             * to see it can be checked.
             */
            if (tutor.BetrayalFlag != 0)
            {
                if (tutor.BetrayalFlag + BETRAYAL_FLAG_TIMEOUT < UnixTimestamp.Now) // expired, leave mentor
                {
                    if (Guide != null)
                    {
                        await Guide.SendAsync(string.Format(StrGuideExpelTutor, StudentName));
                        Guide.Guide.RemoveApprentice(StudentIdentity);
                    }

                    if (Student != null)
                    {
                        await Student.SendAsync(string.Format(StrGuideExpelStudent, GuideName));
                        await Student.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower, 0, 0);
                        Student.Guide.Tutor = null;
                    }

                    await DeleteAsync();
                }
            }

            if (betrayalCheck.IsActive())
            {
                betrayalCheck.Update();
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await ServerDbContext.UpdateAsync(tutor) && await ServerDbContext.UpdateAsync(access);
        }

        public async Task<bool> DeleteAsync()
        {
            await ServerDbContext.DeleteAsync(tutor);
            await ServerDbContext.DeleteAsync(access);
            return true;
        }
    }
}
