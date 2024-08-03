using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Fate.Network;
using Long.Module.Fate.Repositories;
using Long.Network.Packets.Cross;
using static Long.Kernel.Modules.Systems.Fate.IFate;

namespace Long.Module.Fate.States
{
    public sealed class Fate : IFate
    {
        private readonly Character user;
        private DbFatePlayer fatePlayer;
        private readonly List<DbFateProtect> fateProtect = new();

        public Fate(Character user)
        {
            this.user = user;
        }

        public int CriticalStrike { get; private set; }

        public int SkillCriticalStrike { get; private set; }

        public int Immunity { get; private set; }

        public int Breakthrough { get; private set; }

        public int Counteraction { get; private set; }

        public int HealthPoints { get; private set; }

        public int Attack { get; private set; }

        public int MagicAttack { get; private set; }

        public int MagicDefense { get; private set; }

        public int FinalDamage { get; private set; }

        public int FinalMagicDamage { get; private set; }

        public int FinalDefense { get; private set; }

        public int FinalMagicDefense { get; private set; }

        public async Task InitializeAsync()
        {
            fatePlayer = await FatePlayerRepository.GetAsync(user.Identity);
            if (fatePlayer != null)
            {
                fateProtect.AddRange(await FateProtectRepository.GetAsync(user.Identity));
                await SendAsync(true);
                FateManager.UpdateStatus(user);
                await SubmitRankAsync();
                await SendExpiryInfoAsync();
            }
        }

        public async Task UnlockAsync(FateType type)
        {
            DbConfig init = FateManager.GetInitializationRequirements(type);
            if (init == null)
            {
                return;
            }

            if (init.Data3 > user.Metempsychosis)
            {
                return;
            }

            if (init.Data2 > user.Level && init.Data3 >= user.Metempsychosis)
            {
                return;
            }

            if (type > FateType.Dragon && IsLocked(type - 1))
            {
                return;
            }

            if (type > FateType.Dragon && init.Data4 > user.Fate.GetScore(type - 1))
            {
                return;
            }

            if (!IsLocked(type))
            {
                return;
            }

            if (fatePlayer == null)
            {
                fatePlayer = new DbFatePlayer
                {
                    PlayerId = user.Identity
                };
                user.ChiPoints = Math.Max(4000, user.ChiPoints);
            }

            await FateManager.InitialFateAttributeAsync(user, type, fatePlayer);
            await user.SendAsync(new MsgPlayerAttribInfo(user));
        }

        public async Task GenerateAsync(FateType type, TrainingSave save)
        {
            if (IsLocked(type))
            {
                return;
            }

            int cost = 50;
            if (save.HasFlag(TrainingSave.Attr1))
            {
                cost += 50;
            }

            if (save.HasFlag(TrainingSave.Attr2))
            {
                cost += 50;
            }

            if (save.HasFlag(TrainingSave.Attr3))
            {
                cost += 50;
            }

            if (save.HasFlag(TrainingSave.Attr4))
            {
                cost += 50;
            }

            if (!await user.SpendStrengthValueAsync(cost, false))
            {
                return;
            }

            await FateManager.GenerateAsync(user, type, fatePlayer, save);
            await user.SendAsync(new MsgPlayerAttribInfo(user));

            await ActivityManager.UpdateTaskActivityAsync(user, ActivityManager.ActivityType.ChiStudy);
        }

        public async Task<bool> ProtectAsync(FateType fateType, bool update)
        {
            if (IsLocked(fateType))
            {
                return false;
            }

            DbFateProtect protect = fateProtect.Find(x => x.FateNo == (byte)fateType);
            if (protect != null)
            {
                if (!update)
                {
                    protect.ExpiryDate = UnixTimestamp.FromDateTime(DateTime.Now.AddDays(5));
                }
            }
            else
            {
                protect = new DbFateProtect
                {
                    PlayerId = user.Identity,
                    FateNo = (byte)fateType,
                    ExpiryDate = UnixTimestamp.FromDateTime(DateTime.Now.AddDays(5))
                };
            }

            if (fateType == FateType.Dragon)
            {
                protect.Attrib1 = fatePlayer.Fate1Attrib1;
                protect.Attrib2 = fatePlayer.Fate1Attrib2;
                protect.Attrib3 = fatePlayer.Fate1Attrib3;
                protect.Attrib4 = fatePlayer.Fate1Attrib4;
            }
            else if (fateType == FateType.Phoenix)
            {
                protect.Attrib1 = fatePlayer.Fate2Attrib1;
                protect.Attrib2 = fatePlayer.Fate2Attrib2;
                protect.Attrib3 = fatePlayer.Fate2Attrib3;
                protect.Attrib4 = fatePlayer.Fate2Attrib4;
            }
            else if (fateType == FateType.Tiger)
            {
                protect.Attrib1 = fatePlayer.Fate3Attrib1;
                protect.Attrib2 = fatePlayer.Fate3Attrib2;
                protect.Attrib3 = fatePlayer.Fate3Attrib3;
                protect.Attrib4 = fatePlayer.Fate3Attrib4;
            }
            else if (fateType == FateType.Turtle)
            {
                protect.Attrib1 = fatePlayer.Fate4Attrib1;
                protect.Attrib2 = fatePlayer.Fate4Attrib2;
                protect.Attrib3 = fatePlayer.Fate4Attrib3;
                protect.Attrib4 = fatePlayer.Fate4Attrib4;
            }
            else
            {
                return false;
            }

            fateProtect.Add(protect);
            //await SaveAsync();
            if (protect.Id == 0)
            {
                await ServerDbContext.CreateAsync(protect);
            }
            else
            {
                await ServerDbContext.UpdateAsync(protect);
            }

            await SendProtectInfoAsync();
            return true;
        }

        public bool IsValidProtection(FateType fateType)
        {
            DbFateProtect protect = fateProtect.Find(x => x.FateNo == (byte)fateType);
            if (protect == null)
            {
                return false;
            }

            return protect.ExpiryDate > UnixTimestamp.Now;
        }

        public int GetRestorationCost(FateType fateType)
        {
            DbFateProtect protect = fateProtect.Find(x => x.FateNo == (byte)fateType);
            if (protect == null)
            {
                return -1;
            }

            return (UnixTimestamp.Now - protect.ExpiryDate) / 60 * 2;
        }

        public async Task<bool> ExtendAsync(FateType fateType)
        {
            if (IsLocked(fateType))
            {
                return false;
            }

            DbFateProtect protect = fateProtect.Find(x => x.FateNo == (byte)fateType);
            if (protect == null)
            {
                return false;
            }

            protect.ExpiryDate = UnixTimestamp.FromDateTime(DateTime.Now.AddDays(5));
            if (protect.Id == 0)
            {
                await ServerDbContext.CreateAsync(protect);
            }
            else
            {
                await ServerDbContext.UpdateAsync(protect);
            }

            await SendProtectInfoAsync();
            return true;
        }

        public async Task<bool> RestoreProtectionAsync(FateType fateType)
        {
            if (IsLocked(fateType))
            {
                return false;
            }

            DbFateProtect protect = fateProtect.Find(x => x.FateNo == (byte)fateType);
            if (protect == null)
            {
                return false;
            }

            if (fateType == FateType.Dragon)
            {
                fatePlayer.Fate1Attrib1 = protect.Attrib1;
                fatePlayer.Fate1Attrib2 = protect.Attrib2;
                fatePlayer.Fate1Attrib3 = protect.Attrib3;
                fatePlayer.Fate1Attrib4 = protect.Attrib4;
            }
            else if (fateType == FateType.Phoenix)
            {
                fatePlayer.Fate2Attrib1 = protect.Attrib1;
                fatePlayer.Fate2Attrib2 = protect.Attrib2;
                fatePlayer.Fate2Attrib3 = protect.Attrib3;
                fatePlayer.Fate2Attrib4 = protect.Attrib4;
            }
            else if (fateType == FateType.Tiger)
            {
                fatePlayer.Fate3Attrib1 = protect.Attrib1;
                fatePlayer.Fate3Attrib2 = protect.Attrib2;
                fatePlayer.Fate3Attrib3 = protect.Attrib3;
                fatePlayer.Fate3Attrib4 = protect.Attrib4;
            }
            else if (fateType == FateType.Turtle)
            {
                fatePlayer.Fate4Attrib1 = protect.Attrib1;
                fatePlayer.Fate4Attrib2 = protect.Attrib2;
                fatePlayer.Fate4Attrib3 = protect.Attrib3;
                fatePlayer.Fate4Attrib4 = protect.Attrib4;
            }
            else
            {
                return false;
            }

            await SaveAsync();
            await SendAsync(true);
            FateManager.UpdateStatus(user);
            await SubmitRankAsync();
            await SendProtectInfoAsync();
            return true;
        }

        public async Task<bool> AbandonAsync(FateType fateType)
        {
            DbFateProtect protect = fateProtect.Find(x => x.FateNo == (byte)fateType);
            if (protect == null)
            {
                return false;
            }

            for (int i = 0; i < fateProtect.Count; i++)
            {
                if (fateProtect[i].Id == protect.Id)
                {
                    fateProtect.RemoveAt(i);
                    break;
                }
            }

            await ServerDbContext.DeleteAsync(protect);
            await SendAsync(true);
            FateManager.UpdateStatus(user);
            await SubmitRankAsync();
            await SendProtectInfoAsync();
            return true;
        }

        public bool IsLocked(FateType type)
        {
            if (fatePlayer == null)
            {
                return true;
            }

            if (type == FateType.Dragon)
            {
                return fatePlayer.Fate1Attrib1 == 0;
            }

            if (type == FateType.Phoenix)
            {
                return fatePlayer.Fate2Attrib1 == 0;
            }

            if (type == FateType.Tiger)
            {
                return fatePlayer.Fate3Attrib1 == 0;
            }

            if (type == FateType.Turtle)
            {
                return fatePlayer.Fate4Attrib1 == 0;
            }

            return true;
        }

        public int GetScore(FateType type)
        {
            return FateManager.GetScore(fatePlayer, type);
        }

        public int GetPower(TrainingAttrType attr)
        {
            return FateManager.GetPower(fatePlayer, attr);
        }

        public void RefreshPower()
        {
            if (fatePlayer != null)
            {
                ResetAttributes();
                AddAttribute(fatePlayer.Fate1Attrib1);
                AddAttribute(fatePlayer.Fate1Attrib2);
                AddAttribute(fatePlayer.Fate1Attrib3);
                AddAttribute(fatePlayer.Fate1Attrib4);

                AddAttribute(fatePlayer.Fate2Attrib1);
                AddAttribute(fatePlayer.Fate2Attrib2);
                AddAttribute(fatePlayer.Fate2Attrib3);
                AddAttribute(fatePlayer.Fate2Attrib4);

                AddAttribute(fatePlayer.Fate3Attrib1);
                AddAttribute(fatePlayer.Fate3Attrib2);
                AddAttribute(fatePlayer.Fate3Attrib3);
                AddAttribute(fatePlayer.Fate3Attrib4);

                AddAttribute(fatePlayer.Fate4Attrib1);
                AddAttribute(fatePlayer.Fate4Attrib2);
                AddAttribute(fatePlayer.Fate4Attrib3);
                AddAttribute(fatePlayer.Fate4Attrib4);
            }
        }

        private void ResetAttributes()
        {
            CriticalStrike = 0;
            SkillCriticalStrike = 0;
            Immunity = 0;
            Breakthrough = 0;
            Counteraction = 0;
            HealthPoints = 0;
            Attack = 0;
            MagicAttack = 0;
            MagicDefense = 0;
            FinalDamage = 0;
            FinalMagicDamage = 0;
            FinalDefense = 0;
            FinalMagicDefense = 0;
        }

        public void AddAttribute(int value)
        {
            int power = value % 10000;
            switch (FateManager.ReferenceType(value))
            {
                case TrainingAttrType.Criticalstrike:
                {
                    CriticalStrike += power;
                    break;
                }

                case TrainingAttrType.Skillcriticalstrike:
                {
                    SkillCriticalStrike += power;
                    break;
                }

                case TrainingAttrType.Immunity:
                {
                    Immunity += power;
                    break;
                }

                case TrainingAttrType.Breakthrough:
                {
                    Breakthrough += power;
                    break;
                }

                case TrainingAttrType.Counteraction:
                {
                    Counteraction += power;
                    break;
                }

                case TrainingAttrType.Health:
                {
                    HealthPoints += power;
                    break;
                }

                case TrainingAttrType.Attack:
                {
                    Attack += power;
                    break;
                }

                case TrainingAttrType.Magicattack:
                {
                    MagicAttack += power;
                    break;
                }

                case TrainingAttrType.Mdefense:
                {
                    MagicDefense += power;
                    break;
                }

                case TrainingAttrType.Finalattack:
                {
                    FinalDamage += power;
                    break;
                }

                case TrainingAttrType.Finalmagicattack:
                {
                    FinalMagicDamage += power;
                    break;
                }

                case TrainingAttrType.Damagereduction:
                {
                    FinalDefense += power;
                    break;
                }

                case TrainingAttrType.Magicdamagereduction:
                {
                    FinalMagicDefense += power;
                    break;
                }
            }
        }

        public Task SendAsync(bool update, Character target = null)
        {
            MsgTrainingVitalityInfo msg = new()
            {
                Mode = (ushort)(update ? 1 : 0),
                Identity = user.Identity
            };

            if (fatePlayer != null)
            {
                if (fatePlayer.Fate1Attrib1 != 0)
                {
                    msg.Datas.Add(new MsgTrainingVitalityInfo.TrainingData
                    {
                        Type = (byte)FateType.Dragon,
                        Power1 = fatePlayer.Fate1Attrib1,
                        Power2 = fatePlayer.Fate1Attrib2,
                        Power3 = fatePlayer.Fate1Attrib3,
                        Power4 = fatePlayer.Fate1Attrib4
                    });
                }

                if (fatePlayer.Fate2Attrib1 != 0)
                {
                    msg.Datas.Add(new MsgTrainingVitalityInfo.TrainingData
                    {
                        Type = (byte)FateType.Phoenix,
                        Power1 = fatePlayer.Fate2Attrib1,
                        Power2 = fatePlayer.Fate2Attrib2,
                        Power3 = fatePlayer.Fate2Attrib3,
                        Power4 = fatePlayer.Fate2Attrib4
                    });
                }

                if (fatePlayer.Fate3Attrib1 != 0)
                {
                    msg.Datas.Add(new MsgTrainingVitalityInfo.TrainingData
                    {
                        Type = (byte)FateType.Tiger,
                        Power1 = fatePlayer.Fate3Attrib1,
                        Power2 = fatePlayer.Fate3Attrib2,
                        Power3 = fatePlayer.Fate3Attrib3,
                        Power4 = fatePlayer.Fate3Attrib4
                    });
                }

                if (fatePlayer.Fate4Attrib1 != 0)
                {
                    msg.Datas.Add(new MsgTrainingVitalityInfo.TrainingData
                    {
                        Type = (byte)FateType.Turtle,
                        Power1 = fatePlayer.Fate4Attrib1,
                        Power2 = fatePlayer.Fate4Attrib2,
                        Power3 = fatePlayer.Fate4Attrib3,
                        Power4 = fatePlayer.Fate4Attrib4
                    });
                }
            }

            if (target == null)
            {
                msg.Strength = user.ChiPoints;
                msg.Data = fatePlayer?.AttribLockInfo ?? 0;
                return user.SendAsync(msg);
            }

            return target.SendAsync(msg);
        }

        public async Task SubmitRankAsync()
        {
            await DynamicRankManager.SubmitUserFateRankAsync(user, FateType.Dragon);
            await DynamicRankManager.SubmitUserFateRankAsync(user, FateType.Phoenix);
            await DynamicRankManager.SubmitUserFateRankAsync(user, FateType.Tiger);
            await DynamicRankManager.SubmitUserFateRankAsync(user, FateType.Turtle);
        }

        public Task SendProtectInfoAsync()
        {
            if (fateProtect.Count == 0)
            {
                return Task.CompletedTask;
            }

            MsgTrainingVitalityProtectInfo msg = new();
            foreach (DbFateProtect protect in fateProtect)
            {
                string expireTimeString = UnixTimestamp.ToDateTime(protect.ExpiryDate).ToString("yyMMddHHmm");
                uint.TryParse(expireTimeString, out uint expireTime);
                msg.Protects.Add(new MsgTrainingVitalityProtectInfo.ProtectInfo
                {
                    FateType = (FateType)protect.FateNo,
                    Seconds = expireTime,
                    Attribute1 = protect.Attrib1,
                    Attribute2 = protect.Attrib2,
                    Attribute3 = protect.Attrib3,
                    Attribute4 = protect.Attrib4
                });
            }

            return user.SendAsync(msg);
        }

        public Task SendExpiryInfoAsync()
        {
            MsgTrainingVitalityExpiryNotify msg = new();
            foreach (FateType protect in fateProtect.Select(x => (FateType)x.FateNo))
            {
                if (!IsValidProtection(protect))
                {
                    msg.Fates.Add(protect);
                }
            }
            if (msg.Fates.Count == 0)
            {
                return Task.CompletedTask;
            }
            return user.SendAsync(msg);
        }

        public async Task<bool> SaveAsync()
        {
            if (fatePlayer != null)
            {
                if (fatePlayer.Id == 0)
                {
                    return await ServerDbContext.CreateAsync(fatePlayer);
                }

                return await ServerDbContext.UpdateAsync(fatePlayer);
            }

            return true;
        }

        public Task InitializeOSDataAsync(CrossTrainingVitalityInfoPB fateInfo)
        {
            fatePlayer = new DbFatePlayer
            {
                PlayerId = user.Identity,
                Fate1Attrib1 = fateInfo.Fate1Attrib1,
                Fate1Attrib2 = fateInfo.Fate1Attrib2,
                Fate1Attrib3 = fateInfo.Fate1Attrib3,
                Fate1Attrib4 = fateInfo.Fate1Attrib4,
                Fate2Attrib1 = fateInfo.Fate2Attrib1,
                Fate2Attrib2 = fateInfo.Fate2Attrib2,
                Fate2Attrib3 = fateInfo.Fate2Attrib3,
                Fate2Attrib4 = fateInfo.Fate2Attrib4,
                Fate3Attrib1 = fateInfo.Fate3Attrib1,
                Fate3Attrib2 = fateInfo.Fate3Attrib2,
                Fate3Attrib3 = fateInfo.Fate3Attrib3,
                Fate3Attrib4 = fateInfo.Fate3Attrib4,
                Fate4Attrib1 = fateInfo.Fate4Attrib1,
                Fate4Attrib2 = fateInfo.Fate4Attrib2,
                Fate4Attrib3 = fateInfo.Fate4Attrib3,
                Fate4Attrib4 = fateInfo.Fate4Attrib4,
            };
            FateManager.UpdateStatus(user);
            return Task.CompletedTask;
        }

        public Task TransferOSDataAsync(ulong sessionId, uint serverId)
        {
            if (fatePlayer == null) { return Task.CompletedTask; }
            return RealmConnectionManager.SendOSMsgAsync(new MsgCrossTrainingVitalityInfoC
            {
                Data = new()
                {
                    SessionId = sessionId,
                    Fate1Attrib1 = fatePlayer.Fate1Attrib1,
                    Fate1Attrib2 = fatePlayer.Fate1Attrib2,
                    Fate1Attrib3 = fatePlayer.Fate1Attrib3,
                    Fate1Attrib4 = fatePlayer.Fate1Attrib4,
                    Fate2Attrib1 = fatePlayer.Fate2Attrib1,
                    Fate2Attrib2 = fatePlayer.Fate2Attrib2,
                    Fate2Attrib3 = fatePlayer.Fate2Attrib3,
                    Fate2Attrib4 = fatePlayer.Fate2Attrib4,
                    Fate3Attrib1 = fatePlayer.Fate3Attrib1,
                    Fate3Attrib2 = fatePlayer.Fate3Attrib2,
                    Fate3Attrib3 = fatePlayer.Fate3Attrib3,
                    Fate3Attrib4 = fatePlayer.Fate3Attrib4,
                    Fate4Attrib1 = fatePlayer.Fate4Attrib1,
                    Fate4Attrib2 = fatePlayer.Fate4Attrib2,
                    Fate4Attrib3 = fatePlayer.Fate4Attrib3,
                    Fate4Attrib4 = fatePlayer.Fate4Attrib4,
                }
            }, serverId);
        }
    }
}