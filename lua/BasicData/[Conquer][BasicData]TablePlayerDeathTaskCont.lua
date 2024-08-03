----------------------------------------------------------------------------
--Name:		[征服][基础数据]玩家死亡触发关联表.lua
--Purpose:	玩家死亡触发关联表
--Creator: 	郑江文
--Created:	2014/12/18
----------------------------------------------------------------------------
--玩家死亡触发关联表
tUserKilled = {}

--例：
--tUserKilled["tFunction"] = tUserKilled["tFunction"] or {}
--table.insert(tUserKilled["tFunction"],func)

-- 玩家复活触发关联表
tUserSave = {}
--例：
--tUserSave["tFunction"] = tUserSave["tFunction"] or {}
--table.insert(tUserSave["tFunction"],func)

-- 玩家锁魂触发关联表
tKeepGhost = {}
--例：
--tKeepGhost["tFunction"] = tKeepGhost["tFunction"] or {}
--table.insert(tKeepGhost["tFunction"],func)

-- 玩家解锁触发关联表
tClearKeepGhost = {}
--例：
--tClearKeepGhost["tFunction"] = tClearKeepGhost["tFunction"] or {}
--table.insert(tClearKeepGhost["tFunction"],func)

-- 个人排位赛：	赢场：
tArenicWins = {}
--例：
--tArenicWins["tFunction"] = tArenicWins["tFunction"] or {}
--table.insert(tArenicWins["tFunction"],func)

-- 个人排位赛：	参赛场
tArenicCompetes = {}
--例：
--tArenicCompetes["tFunction"] = tArenicCompetes["tFunction"] or {}
--table.insert(tArenicCompetes["tFunction"],func)

-- 组队排位赛：	赢场：
tTeamArenicWins = {}
--例：
--tTeamArenicWins["tFunction"] = tTeamArenicWins["tFunction"] or {}
--table.insert(tTeamArenicWins["tFunction"],func)

-- 组队排位赛：	参赛场
tTeamArenicCompetes = {}
--例：
--tTeamArenicCompetes["tFunction"] = tTeamArenicCompetes["tFunction"] or {}
--table.insert(tTeamArenicCompetes["tFunction"],func)

-- 骑宠，玩家冲过终点是触发
tRideArrive = {}
--例：
--tRideArrive["tFunction"] = tRideArrive["tFunction"] or {}
--table.insert(tRideArrive["tFunction"],func)

-- 武功，每次修炼时触发：
tTrainGongFu = {}
--例：
--tTrainGongFu["tFunction"] = tTrainGongFu["tFunction"] or {}
--table.insert(tTrainGongFu["tFunction"],func)

-- 服务器启动完成触发：
tServerStart = {}
--例：
--tServerStart["tFunction"] = tServerStart["tFunction"] or {}
--table.insert(tServerStart["tFunction"],func)

-- 添加内功转换
tStrengthExchange = {}
--例：
--tStrengthExchange["tFunction"] = tStrengthExchange["tFunction"] or {}
--table.insert(tStrengthExchange["tFunction"],func)

--??????????????????????????????
tCheckInItemToCoatStorage = {}
--??
--tCheckInItemToCoatStorage["tFunction"] = tCheckInItemToCoatStorage["tFunction"] or {}
--table.insert(tCheckInItemToCoatStorage["tFunction"],func)

--?????????????LUA????????????????????
tCheckOutItemFromCoatStorage = {}
--??
--tCheckOutItemFromCoatStorage["tFunction"] = tCheckOutItemFromCoatStorage["tFunction"] or {}
--table.insert(tCheckOutItemFromCoatStorage["tFunction"],func)

--??????lua??
-- //????????
tProcessTrainingVitality = {}
--??
--tProcessTrainingVitality["tFunction"] = tProcessTrainingVitality["tFunction"] or {}
--table.insert(tProcessTrainingVitality["tFunction"],func)

-- //???????????????
tReplaceGongfuValue = {}
--??
--tReplaceGongfuValue["tFunction"] = tReplaceGongfuValue["tFunction"] or {}
--table.insert(tReplaceGongfuValue["tFunction"],func)

--//?????????
tAfterVexillum = {}
--??
--tAfterVexillum["tFunction"] = tAfterVexillum["tFunction"] or {}
--table.insert(tAfterVexillum["tFunction"],func)

--//???????????
tAfterCrossVexillum = {}
--??
--tAfterCrossVexillum["tFunction"] = tAfterCrossVexillum["tFunction"] or {}
--table.insert(tAfterCrossVexillum["tFunction"],func)

-- ???????????LUA??UserAchivementCount?????????????????
tAchivementCount = {}
--??
--tAchivementCount["tFunction"] = tAchivementCount["tFunction"] or {}
--table.insert(tAchivementCount["tFunction"],func)

-- ???????????
tDelUserItemFromCoatStorage = {}
--??
--tDelUserItemFromCoatStorage = tDelUserItemFromCoatStorage["tFunction"] or {}
--table.insert(tDelUserItemFromCoatStorage["tFunction"],func)

-- ????????
tQuizPersonalReward = {}
--??
--tQuizPersonalReward = tQuizPersonalReward["tFunction"] or {}
--table.insert(tQuizPersonalReward["tFunction"],func)

-- ?????????
tQuizRankingsReward = {}
--??
--tQuizRankingsReward = tQuizRankingsReward["tFunction"] or {}
--table.insert(tQuizRankingsReward["tFunction"],func)

-- BossRewardEnd ????
tBossRewardEnd = {}

-- BossDamageBonus ??????
tBossDamageBonus = {}

-- BossLastKnifeAward ??????
tBossLastKnifeAward ={}

--??????
tAwardHorseRace = {}

-- //???? (??InviteFilter????) ??? ???????true???false ????action ??129?
-- SetInviteFilter
tSetInviteFilter = {}

-- //???? (??InviteTrans????) ??? ???????true???false????action ??130?
-- SendInvite
tSendInvite = {}

-- ????PK???
tAwardCrossElite = {}


-- ??????
tCardsLotteryAgainCost = {}

-- ?????
tSendDivorceMail = {}

-- ??????????
tProcessMonsterDie = {}

-- ??????
tCardsLotteryRecordCost = {}

-- ??????
tGlobalLotteryRecordCost = {}

-- ????PK???
tUserAwardCrossClanPKPrize = {}

-- ?????????LUA??
tBloodTriggerLua = {}
-- ??????????ID
tProcessDelInstance = {}

-- ?????? ??
-- ????????????????
-- </F>GetBonusByAction</N>%lu</N>%lu
-- ?????????ID?actionID
-- GetBonusByAction
tGetKOKBonusByAction = {}