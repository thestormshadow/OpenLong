using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Module.Competion.Network;
using Long.Module.Competion.Repositories;
using Long.Network.Packets;
using Long.Shared;
using Serilog;
using System.Collections.Concurrent;
using static Long.Module.Competion.Network.MsgQuiz;
using static Long.Kernel.Service.RandomService;

namespace Long.Module.Competion.States
{
    public class QuizShowManager
    {
        private static readonly ILogger logger = Log.ForContext<QuizShowManager>();

        private const uint NPC_ID_U = 100012;
        private const int MAX_QUESTION = 20;
        private const int TIME_PER_QUESTION = 30;

        private const int TOTAL_EXP_REWARD = 600;

        private static readonly ushort[] ExperienceReward =
        {
            3000,
            1800,
            1200,
            600
        };

        private static readonly List<DbQuiz> AllQuestions = new();
        private static readonly List<DbQuiz> CurrentQuestions = new();
        private static readonly ConcurrentDictionary<uint, QuizUser> Users = new();

        private static DynamicNpc quizShowNpc;

        private static bool ready = false;
        private static int currentQuestionIndex;
        private static readonly TimeOutMS eventTimer = new(500);
        private static readonly TimeOut questionTimer = new(30);

        public static QuizStatus Status { get; private set; } = QuizStatus.Idle;

        public static async Task<bool> OnServerInitializeAsync()
        {
            quizShowNpc = RoleManager.GetRole<DynamicNpc>(NPC_ID_U);
            if (quizShowNpc == null)
            {
                logger.Error("Could not load NPC {0} for {1}", NPC_ID_U, "Quiz show");
                return false;
            }

            quizShowNpc.Data0 = 0;

            AllQuestions.AddRange(QuizRepository.Get());
            return true;
        }

        public static Task OnUserLoginAsync(Character user)
        {
            if (Status == QuizStatus.Idle)
            {
                return Task.CompletedTask;
            }

            if (!Users.TryGetValue(user.Identity, out var player))
            {
                player = new QuizUser
                {
                    Identity = user.Identity,
                    Name = user.Name
                };
                Users.TryAdd(user.Identity, player);
            }

            var msg = new MsgQuiz
            {
                Action = QuizAction.AfterReply,
                Param2 = player.TimeTaken,
                Param3 = player.Rank,
                Param6 = player.Points
            };
            List<QuizUser> top3 = GetTop3();
            foreach (QuizUser top in top3)
            {
                msg.Scores.Add(new QuizRank
                {
                    Name = top.Name,
                    Time = top.TimeTaken,
                    Score = top.Points
                });
            }

            return user.SendAsync(msg);
        }

        public static async Task OnTimerAsync()
        {
            if (!eventTimer.ToNextTime())
            {
                return;
            }

            if (Status == QuizStatus.Idle && quizShowNpc != null)
            {
                if (quizShowNpc.Data0 == 3 && !ready) // load
                {
                    Users.Clear();
                    CurrentQuestions.Clear();
                    var temp = new List<DbQuiz>(AllQuestions);
                    for (var i = 0; i < Math.Min(temp.Count, MAX_QUESTION); i++)
                    {
                        int idx = await NextAsync(temp.Count) % Math.Max(1, temp.Count);
                        CurrentQuestions.Add(temp[idx]);
                        temp.RemoveAt(idx);
                    }

                    foreach (Character user in RoleManager.QueryRoleByType<Character>())
                    {
                        if (!Users.TryGetValue(user.Identity, out QuizUser res))
                        {
                            Enter(user);
                        }
                        else
                        {
                            res.Canceled = false;
                        }
                    }

                    await BroadcastMsgAsync(new MsgQuiz
                    {
                        Action = QuizAction.Start,
                        Param1 = (ushort)(60 - DateTime.Now.Second),
                        Param2 = MAX_QUESTION,
                        Param3 = TIME_PER_QUESTION,
                        Param4 = ExperienceReward[0],
                        Param5 = ExperienceReward[1],
                        Param6 = ExperienceReward[2]
                    }).ConfigureAwait(false);
                    ready = true;
                    return;
                }

                if (quizShowNpc.Data0 == 4) // start
                {
                    Status = QuizStatus.Running;
                    currentQuestionIndex = -1;
                }
            }
            else
            {
                if (questionTimer.ToNextTime(TIME_PER_QUESTION) && ++currentQuestionIndex < CurrentQuestions.Count)
                {
                    DbQuiz question = CurrentQuestions[currentQuestionIndex];
                    foreach (QuizUser player in Users.Values.Where(x => !x.Canceled))
                    {
                        Character user = RoleManager.GetUser(player.Identity);
                        if (user == null)
                        {
                            continue;
                        }

                        if (!player.Replied)
                        {
                            player.Points += 1;
                            player.TimeTaken += TIME_PER_QUESTION;

                            var msg = new MsgQuiz
                            {
                                Action = QuizAction.AfterReply,
                                Param2 = player.TimeTaken,
                                Param3 = player.Rank = GetRanking(player.Identity),
                                Param6 = player.Points
                            };
                            List<QuizUser> top3 = GetTop3();
                            foreach (QuizUser top in top3)
                            {
                                msg.Scores.Add(new QuizRank
                                {
                                    Name = top.Name,
                                    Time = top.TimeTaken,
                                    Score = top.Points
                                });
                            }
                            await user.SendAsync(msg);
                        }

                        player.Replied = false;
                        player.CurrentQuestion = currentQuestionIndex;
                        ushort lastResult = 1;
                        if (currentQuestionIndex > 0)
                        {
                            lastResult = (ushort)(player.Correct ? 1 : 2);
                        }

                        player.Correct = false;
                        await user.SendAsync(new MsgQuiz
                        {
                            Action = QuizAction.Question,
                            Param1 = (ushort)(currentQuestionIndex + 1),
                            Param2 = lastResult,
                            Param3 = player.Experience,
                            Param4 = player.TimeTaken,
                            Param5 = player.Points,
                            Strings =
                            {
                                question.Question,
                                question.Answer1,
                                question.Answer2,
                                question.Answer3,
                                question.Answer4
                            }
                        });
                    }
                }
                else if (currentQuestionIndex >= CurrentQuestions.Count)
                {
                    Status = QuizStatus.Idle;

                    List<QuizUser> top3 = GetTop3();
                    foreach (QuizUser player in Users.Values.Where(x => !x.Canceled))
                    {
                        if (player.CurrentQuestion < currentQuestionIndex)
                        {
                            player.TimeTaken += TIME_PER_QUESTION;
                        }

                        var expBallReward = 0;
                        if (top3.Any(x => x.Identity == player.Identity))
                        {
                            int rank = GetRanking(player.Identity);
                            if (rank > 0 && rank <= 3)
                            {
                                expBallReward = ExperienceReward[rank];
                            }
                        }
                        else
                        {
                            expBallReward = ExperienceReward[3];
                        }

                        Character user = RoleManager.GetUser(player.Identity);
                        if (user != null)
                        {
                            var msg = new MsgQuiz
                            {
                                Action = QuizAction.Finish,
                                Param1 = player.Rank,
                                Param2 = (ushort)(player.Experience + expBallReward),
                                Param3 = player.TimeTaken,
                                Param4 = player.Points
                            };
                            foreach (QuizUser top in top3)
                            {
                                msg.Scores.Add(new QuizRank
                                {
                                    Name = top.Name,
                                    Time = top.TimeTaken,
                                    Score = top.Points
                                });
                            }
                            await user.SendAsync(msg);

                            await user.AddAttributesAsync(ClientUpdateType.QuizPoints, player.Points);
                            if (user.Level < Role.MAX_UPLEV)
                            {
                                await user.AwardExperienceAsync(user.CalculateExpBall(expBallReward));
                            }
                        }
                    }
                    if (quizShowNpc != null)
						quizShowNpc.Data0 = 0;

					ready = false;
                }
            }
        }

        #region Reply

        public static async Task OnReplyAsync(Character user, ushort idxQuestion, ushort reply)
        {
            if (Status != QuizStatus.Running)
            {
                return;
            }

            if (!Users.TryGetValue(user.Identity, out QuizUser player))
            {
                Users.TryAdd(user.Identity, player = new QuizUser
                {
                    Identity = user.Identity,
                    Name = user.Name,
                    TimeTaken = (ushort)(Math.Max(0, currentQuestionIndex - 1) * TIME_PER_QUESTION),
                    CurrentQuestion = currentQuestionIndex
                });
            }

            if (player.CurrentQuestion != currentQuestionIndex)
            {
                return;
            }

            DbQuiz question = CurrentQuestions[idxQuestion - 1];
            ushort points;
            int expBallAmount;
            if (question.Result == reply)
            {
                expBallAmount = TOTAL_EXP_REWARD / MAX_QUESTION;
                player.Points += points = (ushort)Math.Max(1, questionTimer.GetRemain());
                player.TimeTaken +=
                    (ushort)Math.Max(
                        1, Math.Min(TIME_PER_QUESTION, questionTimer.GetInterval() - questionTimer.GetRemain()));
                player.Correct = true;
            }
            else
            {
                expBallAmount = TOTAL_EXP_REWARD / MAX_QUESTION * 4;
                player.Points += points = 1;
                player.TimeTaken += TIME_PER_QUESTION;
                player.Correct = false;
            }

            player.Replied = true;
            player.Experience += (ushort)expBallAmount;
            await user.AwardExperienceAsync(user.CalculateExpBall(expBallAmount));

            var msg = new MsgQuiz
            {
                Action = QuizAction.AfterReply,
                Param2 = player.TimeTaken,
                Param3 = player.Rank = GetRanking(player.Identity),
                Param6 = player.Points
            };
            List<QuizUser> top3 = GetTop3();
            foreach (QuizUser top in top3)
            {
                msg.Scores.Add(new QuizRank
                {
                    Name = top.Name,
                    Time = top.TimeTaken,
                    Score = top.Points
                });
            }
            await user.SendAsync(msg);
        }

        #endregion

        #region Player

        public static bool Enter(Character user)
        {
            return Users.TryAdd(user.Identity, new QuizUser
            {
                Identity = user.Identity,
                Name = user.Name
            });
        }

        public static ushort GetRanking(uint idUser)
        {
            ushort pos = 1;
            foreach (QuizUser player in Users.Values
                                               .Where(x => !x.Canceled)
                                               .OrderByDescending(x => x.Points)
                                               .ThenBy(x => x.TimeTaken))
            {
                if (player.Identity == idUser)
                {
                    return pos;
                }

                pos++;
            }

            return pos;
        }

        private static List<QuizUser> GetTop3()
        {
            var rank = new List<QuizUser>();
            foreach (QuizUser player in Users.Values
                                               .Where(x => !x.Canceled)
                                               .OrderByDescending(x => x.Points)
                                               .ThenBy(x => x.TimeTaken))
            {
                if (rank.Count == 3)
                {
                    break;
                }

                rank.Add(player);
            }

            return rank;
        }

        #endregion

        #region Broadcast

        public static async Task BroadcastMsgAsync(IPacket msg)
        {
            foreach (QuizUser user in Users.Values.Where(x => !x.Canceled))
            {
                Character player = RoleManager.GetUser(user.Identity);
                if (player == null)
                {
                    continue;
                }

                await player.SendAsync(msg);
            }
        }

        #endregion

        #region Cancelation

        public static void Cancel(uint idUser)
        {
            if (Users.TryGetValue(idUser, out QuizUser value))
            {
                value.Canceled = true;
            }
        }

        public static bool IsCanceled(uint idUser)
        {
            return Users.TryGetValue(idUser, out QuizUser value) && value.Canceled;
        }

        #endregion

        public enum QuizStatus
        {
            Idle,
            Running
        }

        private class QuizUser
        {
            public uint Identity { get; set; }
            public string Name { get; set; }
            public ushort Points { get; set; }
            public ushort Experience { get; set; } // 600 = 1 expball
            public ushort TimeTaken { get; set; }  // in seconds
            public int CurrentQuestion { get; set; }
            public ushort Rank { get; set; }
            public bool Correct { get; set; }
            public bool Replied { get; set; }
            public bool Canceled { get; set; }
        }
    }
}
