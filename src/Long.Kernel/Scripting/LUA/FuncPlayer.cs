using Canyon.Game.Scripting.Attributes;
using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Realm;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.World.Enums;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgName;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;
using static Long.Kernel.States.User.Character;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        private Character GetUser(int userId)
        {
            if (userId <= 0)
            {
                return user;
            }
            else
            {
                return RoleManager.GetUser((uint)userId);
            }
        }

        [LuaFunction]
        public long GetUserInt(int userId, int index)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_PLAYER_ID: return (int)user.Identity;
                case G_PLAYER_LookFace: return (int)user.Mesh;
                case G_PLAYER_Hair: return user.Hairstyle;
                case G_PLAYER_Profession: return user.Profession;
                case G_PLAYER_Level: return user.Level;
                case G_PLAYER_MapID: return (int)user.MapIdentity;
                case G_PLAYER_PosX: return user.X;
                case G_PLAYER_PosY: return user.Y;
                case G_PLAYER_Virtue: return (int)user.VirtuePoints;
                case G_PLAYER_Meto: return user.Metempsychosis;
                case G_PLAYER_Sex: return user.Gender;
                case G_PLAYER_TeamMemberNum: return user.Team?.MemberCount ?? 0;
                case G_PLAYER_HomeID: return (int)user.HomeIdentity;
                case G_PLAYER_AccountId: return (int)user.Client.AccountIdentity;
                case G_PLAYER_BattleEffect: return user.BattlePower;
                case G_PLAYER_RidingPoints: return (int)user.HorseRacingPoints;
                case G_PLAYER_Life: return (int)user.Life;
                case G_PLAYER_MaxLife: return (int)user.MaxLife;
                case G_PLAYER_Mana: return (int)user.Mana;
                case G_PLAYER_MaxMana: return (int)user.MaxMana;
                case G_PLAYER_Mentor: return (int)user.EnlightenPoints;
                case G_PLAYER_Money: return (long)user.Silvers;
                case G_PLAYER_EMoney: return user.ConquerPoints;
                case G_PLAYER_EMoneyMono: return user.ConquerPointsBound;
                case G_PLAYER_Exp:
                case G_PLAYER_ExpContribute: return (long)user.Experience;
                case G_PLAYER_PK: return user.PkPoints;
                case G_PLAYER_Strength: return user.Strength;
                case G_PLAYER_Speed: return user.Speed;
                case G_PLAYER_Health: return user.Vitality;
                case G_PLAYER_Soul: return user.Spirit;
                case G_PLAYER_SynRank: return (long)user.SyndicateRank;
                case G_PLAYER_Iterator: return user.Iterator;
                case G_PLAYER_Crime: return user.IsCrime() ? 1 : 0;
                case G_PLAYER_XP: return user.XpPoints;
                case G_PLAYER_EP: return user.Energy;
                case G_PLAYER_AddPoint: return user.AttributePoints;
                case G_PLAYER_ClientVersion: return 0;
                case G_PLAYER_Peerage: return (long)(user.NobilityRank);
                case G_PLAYER_Businness: return user.IsMerchant() ? 1 : 0;
                case G_PLAYER_VIP: return user.VipLevel;
                case G_PLAYER_VIPValue: return user.VipLevel;
                case G_PLAYER_StorageMoney: return user.StorageMoney;
                case G_PLAYER_FirstPro: return user.FirstProfession;
                case G_PLAYER_OldPro: return user.PreviousProfession;
                case G_PLAYER_Cultivation: return user.StudyPoints;
                case G_PLAYER_PKProtocol: return (long)user.PkMode;
                case G_PLAYER_StrengthValue: return user.ChiPoints;
                case G_PLAYER_SynID: return user.SyndicateIdentity;
                case G_PLAYER_FamilyID: return user.FamilyIdentity;
                case G_PLAYER_LevupExp:
                    {
                        DbLevelExperience db = ExperienceManager.GetLevelExperience(user?.Level ?? 0);
                        return (long)(db?.Exp ?? 0);
                    }
                case G_PLAYER_MateID: return user.MateIdentity;
                case G_PLAYER_GodBless:
                    {
                        if (user.IsBlessed)
                        {
                            return (long)(DateTime.Now - user.HeavenBlessingExpires).TotalSeconds;
                        }
                        return 0;
                    }
                case G_PLAYER_BusinessManDays: return user.BusinessManDays;
                case G_PLAYER_GodBlessTime:
                    {
                        if (user.IsBlessed)
                        {
                            return UnixTimestamp.FromDateTime(user.HeavenBlessingExpires);
                        }
                        return 0;
                    }
                case G_PLAYER_ExpPercent:
                case G_PLAYER_ExpPercentContribute:
                    {
                        DbLevelExperience db = ExperienceManager.GetLevelExperience(user?.Level ?? 0);
                        if (db != null)
                        {
                            return (long)(user.Experience / (double)db.Exp * 100);
                        }
                        return 0;
                    }
                case G_PLAYER_CultureValue: return user.CultureValue;
                case G_PLAYER_League_ID: return user.Union?.Identity ?? 0;
                case G_PLAYER_League_Official: return (long)(user.UnionMember?.OfficialType ?? KingdomOfficial.OfficialPosition.Member);
                case G_PLAYER_League_Point: return (int)user.LeaguePoints;
                default:
                    return 0;
            }
        }

        [LuaFunction]
        public long GetTargetUserInt(int targetUser, int index)
        {
            Character target = RoleManager.GetUser((uint)targetUser) ?? role as Character;
            if (target == null || target.Identity == 0)
            {
                return 0;
            }
            return GetUserInt((int)target.Identity, index);
        }

        [LuaFunction]
        public string GetUserStr(int userId, int index)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return StrNone;
            }

            switch (index)
            {
                case G_PLAYER_Name: return user.Name;
                case G_PLAYER_MateName: return user.MateName;
            }
            return StrNone;
        }

        [LuaFunction]
        public string GetTargetUserStr(int userId, int index)
        {
            Character target = RoleManager.GetUser((uint)userId) ?? role as Character;
            if (target == null || target.Identity == 0)
            {
                return StrNone;
            }
            return GetUserStr((int)target.Identity, index);
        }

        [LuaFunction]
        public bool AddUserInt(int userId, int index, long value, int unknown)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            switch (index)
            {
                case G_PLAYER_Life:
                    {
                        return user.AddAttributesAsync(ClientUpdateType.Hitpoints, value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Mana:
                    {
                        return user.AddAttributesAsync(ClientUpdateType.Mana, value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Money:
                    {
                        return user.ChangeMoneyAsync(value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_EMoney:
                    {
                        if (user.ChangeConquerPointsAsync((int)value).GetAwaiter().GetResult())
                        {
                            user.SaveEmoneyLogAsync(EmoneyOperationType.Lua, 0, 0, (uint)value).GetAwaiter().GetResult();
                            return true;
                        }
                        return false;
                    }
                case G_PLAYER_EMoneyMono:
                    {
                        if (user.ChangeBoundConquerPointsAsync((int)value).GetAwaiter().GetResult())
                        {
                            user.SaveEmoneyMonoLogAsync(EmoneyOperationType.Lua, 0, 0, (uint)value).GetAwaiter().GetResult();
                            return true;
                        }
                        return false;
                    }
                case G_PLAYER_League_Point:
                    {
                        return user.ChangeGoldenLeaguePointsAsync(value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Exp:
                    {
                        return user.AwardExperienceAsync(value, true).GetAwaiter().GetResult();
                    }
                case G_PLAYER_ExpContribute:
                    {
                        return user.AwardExperienceAsync(value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Strength:
                    {
                        return user.AddAttributesAsync(ClientUpdateType.Strength, value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Speed:
                    {
                        return user.AddAttributesAsync(ClientUpdateType.Agility, value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Health:
                    {
                        return user.AddAttributesAsync(ClientUpdateType.Vitality, value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Soul:
                    {
                        return user.AddAttributesAsync(ClientUpdateType.Spirit, value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_CultureValue:
                    {
                        return user.ChangeCultureAsync((int)value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_StrengthValue:
                    {
                        return user.ChangeStrengthValueAsync((int)value).GetAwaiter().GetResult();
                    }
            }
            return false;
        }

        [LuaFunction]
        public bool SetUserInt(int userId, int index, long value, int unknown)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            switch (index)
            {
                case G_PLAYER_LookFace:
                    {
                        return user.SetAttributesAsync(ClientUpdateType.Mesh, (ulong)value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Profession:
                    {
                        return user.SetAttributesAsync(ClientUpdateType.Class, (ulong)value).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Transfrom:
                    {
                        if (value == 0)
                        {
                            user.ClearTransformationAsync().GetAwaiter().GetResult();
                            return true;
                        }
                        return user.TransformAsync((uint)value, unknown, true).GetAwaiter().GetResult();
                    }
                case G_PLAYER_Sex:
                    {
                        BodyType newBody = user.Body;
                        ushort usNewBody = (ushort)value;
                        if (usNewBody == 1)
                        {
                            if (user.Body != BodyType.AgileFemale && user.Body != BodyType.MuscularFemale)
                            {
                                return false;
                            }

                            if (user.Body == BodyType.AgileFemale)
                            {
                                newBody = BodyType.AgileMale;
                            }
                            else if (user.Body == BodyType.MuscularFemale)
                            {
                                newBody = BodyType.MuscularMale;
                            }
                        }

                        if (usNewBody == 2)
                        {
                            if (user.Body != BodyType.AgileMale && user.Body != BodyType.MuscularMale)
                            {
                                return false;
                            }

                            if (user.Body == BodyType.AgileMale)
                            {
                                newBody = BodyType.AgileFemale;
                            }
                            else if (user.Body == BodyType.MuscularMale)
                            {
                                newBody = BodyType.MuscularFemale;
                            }
                        }

                        if (user.UserPackage[Item.ItemPosition.Garment] != null)
                        {
                            user.UserPackage.UnEquipAsync(Item.ItemPosition.Garment).GetAwaiter().GetResult();
                        }
                        if (user.UserPackage[Item.ItemPosition.SecondaryGarment] != null)
                        {
                            user.UserPackage.UnEquipAsync(Item.ItemPosition.SecondaryGarment).GetAwaiter().GetResult();
                        }
                        user.Body = newBody;
                        user.SynchroAttributesAsync(ClientUpdateType.Mesh, user.Mesh, true).GetAwaiter().GetResult();
                        user.SaveAsync().GetAwaiter().GetResult();
                        return true;
                    }
            }

            return false;
        }

        [LuaFunction]
        public bool IsMoneyPackForbidden(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        [LuaFunction]
        public bool IsUserAlreadyCreateGongFu(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return user.JiangHu.HasJiangHu;
        }

        [LuaFunction]
        public bool CheckLeftSpace(int userId, int spaceNum)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            return user.UserPackage.IsPackSpare(spaceNum);
        }

        [LuaFunction]
        public bool Effect(int userId, string type, string effect, string mode)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            var msg = new MsgName
            {
                Identity = user.Identity,
                Action = StringAction.RoleEffect
            };
            msg.Strings.Add(effect);

            switch (type.ToLower())
            {
                case "self":
                    {
                        user.BroadcastRoomMsgAsync(msg, true).GetAwaiter().GetResult();
                        return true;
                    }

                case "couple":
                    {
                        user.BroadcastRoomMsgAsync(msg, true).GetAwaiter().GetResult();

                        Character couple = RoleManager.GetUser(user.MateIdentity);
                        if (couple == null)
                        {
                            return true;
                        }

                        msg.Identity = couple.Identity;
                        couple.BroadcastRoomMsgAsync(msg, true).GetAwaiter().GetResult();
                        return true;
                    }

                case "team":
                    {
                        if (user.Team == null)
                        {
                            return false;
                        }

                        foreach (Character member in user.Team.Members)
                        {
                            msg.Identity = member.Identity;
                            member.BroadcastRoomMsgAsync(msg, true).GetAwaiter().GetResult();
                        }
                        return true;
                    }
            }
            return false;
        }

        [LuaFunction]
        public bool IsLearnedInnerStrengthType(int idUser, byte nInnerStrengthType)
        {
            Character user = GetUser(idUser);
            if (user?.InnerStrength == null)
            {
                return false;
            }

            return user.InnerStrength.HasLearnedStrengthType(nInnerStrengthType);
        }

        [LuaFunction]
        public int GetInnerStrengthLevByType(int idUser, byte nInnerStrengthType)
        {
            Character user = GetUser(idUser);
            if (user?.InnerStrength == null)
            {
                return 0;
            }
            return user.InnerStrength.GetInnerStrengthLevelByType(nInnerStrengthType);
        }

        [LuaFunction]
        public int GetInnerStrengthTotalValue(int idUser)
        {
            Character user = GetUser(idUser);
            if (user?.InnerStrength == null)
            {
                return 0;
            }

            return user.InnerStrength.TotalValue;
        }

        [LuaFunction]
        public bool LearningInnerStrength(int idUser, byte nInnerStrengthType)
        {
            Character user = GetUser(idUser);
            if (user?.InnerStrength == null)
            {
                return false;
            }

            return user.InnerStrength.UnlockAsync(nInnerStrengthType).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool IsInLeague(int idUser)
        {
            Character user = GetUser(idUser);
            return user?.Union != null;
        }

        [LuaFunction]
        public int GetCountryBrickAmount()
        {
            return (int)(KingdomManager.Kingdom?.Brick ?? 0);
        }

        [LuaFunction]
        public int GetCountryId()
        {
            return (int)(KingdomManager.Kingdom?.Identity ?? 0);
        }

        [LuaFunction]
        public long GetUserVarData(int idUser, int index)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return 0;
            }
            return user.VarData[index];
        }

        [LuaFunction]
        public void SetUserVarData(int idUser, int index, long value)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return;
            }

            user.VarData[index] = value;
        }

        [LuaFunction]
        public string GetUserVarDataStr(int idUser, int index)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return string.Empty;
            }
            return user.VarString[index];
        }

        [LuaFunction]
        public void SetUserVarDataStr(int idUser, int index, string value)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return;
            }

            user.VarString[index] = value;
        }

        [LuaFunction]
        public bool IsOSUser(int idUser)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return true;
            }

            return user.OriginServer.ServerId != RealmManager.ServerIdentity;
        }

        [LuaFunction]
        public int UserGetSpeakerNumEveryDay(int idUser)
        {
            logger.Warning("UserGetSpeakerNumEveryDay(int idUser) not handled");
            return 0;
        }

        [LuaFunction]
        public bool AddLeagueMoney(int leagueId, int money)
        {
            logger.Warning("AddLeagueMoney(int leagueId, int money) not handled");
            return true;
        }

        [LuaFunction]
        public bool AddRoleStatus(int nUserId, int nStatus, int nPower, int nSecs, int nTimes, int nunRemainTime, int nunEndTime, int nucRecordable, int nusFrom, int nunData)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            user.AttachStatusAsync(user, StatusSet.GetRealStatus(nStatus), nPower, nSecs, nTimes, null, nucRecordable != 0).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool DelRoleStatus(int nUserId, int nStatus)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            user.DetachStatusAsync(StatusSet.GetRealStatus(nStatus)).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool ChkRoleStatus(int nUserId, int nStatus)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            return user.QueryStatus(StatusSet.GetRealStatus(nStatus)) != null;
        }

        [LuaFunction]
        public bool AddUserTeamStatus(int nStatus, int nPower, int nSecs, int nTimes, int nUserId)
        {
            Character user = GetUser(nUserId);
            if (user?.Team == null)
            {
                return false;
            }
            foreach (var member in user.Team.Members)
            {
                member.AttachStatusAsync(user, StatusSet.GetRealStatus(nStatus), nPower, nSecs, nTimes, null).GetAwaiter().GetResult();
            }
            return true;
        }

        [LuaFunction]
        public bool DelUserTeamStatus(int nUserId, int nStatus)
        {
            Character user = GetUser(nUserId);
            if (user?.Team == null)
            {
                return false;
            }
            foreach (var member in user.Team.Members)
            {
                member.DetachStatusAsync(StatusSet.GetRealStatus(nStatus)).GetAwaiter().GetResult();
            }
            return true;
        }

        [LuaFunction]
        public bool ChkUserTeamStatus(int nUserId, int nStatus)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            foreach (var member in user.Team.Members)
            {
                if (member.QueryStatus(StatusSet.GetRealStatus(nStatus)) == null)
                {
                    return false;
                }
            }
            return true;
        }

        [LuaFunction]
        public bool TeamExeFuncByLeader(int userId, int range, string function)
        {
            Character user = GetUser(userId);
            if (user?.Team?.IsLeader((uint)userId) != true)
            {
                return false;
            }

            function = LuaScriptManager.ParseTaskDialogAnswerToScript(function);
            foreach (var member in user.Team.Members)
            {
                if (range > 0 && member.GetDistance(user) > range)
                {
                    continue;
                }
                LuaScriptManager.Run(member, user, null, Array.Empty<string>(), function);
            }
            return true;
        }

        [LuaFunction]
        public bool TeamExeFuncByTeamer(int userId, int range, string function)
        {
            Character user = GetUser(userId);
            if (user?.Team == null)
            {
                return false;
            }

            function = LuaScriptManager.ParseTaskDialogAnswerToScript(function);
            foreach (var member in user.Team.Members)
            {
                if (range > 0 && member.GetDistance(user) > range)
                {
                    continue;
                }
                LuaScriptManager.Run(member, user, null, Array.Empty<string>(), function);
            }
            return true;
        }

        [LuaFunction]
        public bool IsConcubines(int userId)
        {
            logger.Warning("IsConcubines(int userId) not handled");
            return false;
        }

        [LuaFunction]
        public bool HaveConcubines(int userId)
        {
            logger.Warning("HaveConcubines(int userId) not handled");
            return false;
        }

        [LuaFunction]
        public long GetGongFuInt(int userId, int index)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_GONGFU_ATTR_REALM:
                    {
                        return user.JiangHu?.InnerPower ?? 0;
                    }
                case G_GONGFU_ATTR_FREE_CULTIVATE_PARAM:
                    {
                        return user.JiangHu?.FreeCaltivateParam ?? 0;
                    }
                case G_GONGFU_ATTR_GENUINEQI_LV:
                    {
                        return user.JiangHu?.Talent ?? 0;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        [LuaFunction]
        public bool Talk(int userId, int talkChannel, string message)
        {
            Character user = GetUser(userId);
            user?.SendAsync(message, (TalkChannel)talkChannel, Color.White).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool StartHangUp(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        [LuaFunction]
        public bool StopHangUp(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        [LuaFunction]
        public bool IsAutoHangUp(int userId)
        {
            Character user = GetUser(userId);
            return user != null && user.IsAutoHangUp;
            return false;
        }

        [LuaFunction]
        public bool UserRandTrans(int idUser, int idMap, int nCheck)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            Point targetPos = map.QueryRandomPositionAsync().GetAwaiter().GetResult();
            if (targetPos.Equals(default))
            {
                return false;
            }
            return user.FlyMapAsync(map.Identity, targetPos.X, targetPos.Y).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool UserRandBoundTrans(int idUser, int idMap, int nCheck, int nBoundX, int nBoundY, int nBoundCX, int nBoundCY)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            Point targetPos = map.QueryRandomPositionNoRadiusAsync(nBoundX, nBoundY, nBoundCX, nBoundCY).GetAwaiter().GetResult();
            if (targetPos.Equals(default))
            {
                return false;
            }
            return user.FlyMapAsync(map.Identity, targetPos.X, targetPos.Y).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool IsInPlunderWar(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return false;
        }

        [LuaFunction]
        public int CalEquipItemScore(int nUserId, int nEquipPos)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return 0;
            }

            return 0;
        }

        // ACTION:   type=1730，param格式"opt type title savetime", opt可选"check  add  del  time", type 称号类型，title为称号ID，savetime为称号时效(分钟)
        // LUA：
        // AwardTitle，获得称号，参1: 玩家ID，参2:称号类型，参3:称号ID，参4:时效(分钟) ，成功返回true，否则返回false
        // DeleteTitle，删除称号，参1: 玩家ID，参2:称号类型，参3:称号ID，成功返回true，否则返回false
        // CheckTitle，是否已拥有该称号，参1: 玩家ID，参2:称号类型，参3:称号ID，已拥有返回true，否则返回false
        // TitleAddTime，延长称号时效，参1: 玩家ID，参2:称号类型，参3:称号ID，参4:延长时间(分钟)，成功返回true，否则返回false
        [LuaFunction]
        public bool AwardTitle(int nUserId, int nTitleType, int nTitleId, int nSaveTime)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            return user.TitleStorage.AwardTitleAsync((uint) nTitleType, (uint)nTitleId, (uint)nSaveTime * 60).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool DeleteTitle(int nUserId, int nTitleType, int nTitleId)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            return user.TitleStorage.DeleteTitleAsync((uint) nTitleType, (uint)nTitleId).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool CheckTitle(int nUserId, int nTitleType, int nTitleId, int nChkType)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            return user.TitleStorage.GetTitle((uint)nTitleType, (uint)nTitleId, out _);
        }

        [LuaFunction]
        public bool TitleAddTime(int nUserId, int nTitleType, int nTitleId, int nAddTime)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            return user.TitleStorage.AwardTitleAsync((uint)nTitleType, (uint)nTitleId, (uint)nAddTime * 60).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool TestUserHaveCoat(int nUserId, int nCoatType, int nCoatId)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            // nCoatType 1 = Garment, 2 = MountArmor
            // Our storage will deal without ok
            return user.CoatStorage.IsCoatTypeStored((uint)nCoatId);
        }

        [LuaFunction]
        public bool UserChangeServer(int nUserId, string sServerName)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }
            return RealmManager.ConnectUserToServerAsync(user, sServerName).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool UserSetExplore(int nUserId, int nSecs, string sContent, int nActionId, string sFunc, string sFailFunc)
        {
            Character user = GetUser(nUserId);
            if (user == null)
            {
                return false;
            }

            ProgressBar progressBar = new(nSecs + 1, sFunc, sFailFunc)
            {
                Command = (uint)2
            };
            user.AwaitingProgressBar = progressBar;

            user.SendAsync(new MsgAction
            {
                Action = ActionType.ProgressBar,
                Identity = user.Identity,
                Command = progressBar.Command,
                Direction = 1,
                MapColor = (uint)nSecs,
                Strings = new List<string>
                {
                    sContent.Replace('~', ' ')
                }
            }).GetAwaiter().GetResult();
            return true;
        }
    }
}
