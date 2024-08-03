------------------------------------------------------------------------------------
--Name:		[Canyon][ActiveScript]PigletHideNSeek
--Purpose:	Hide N Seek event for World Conquer Online.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/08/01
------------------------------------------------------------------------------------

local tHideNSeek_Maps = {}
tHideNSeek_Maps[1] = {}
tHideNSeek_Maps[1]["MapId"] = 1000
tHideNSeek_Maps[1]["PosX"] = {499,750,618,355,700,205,866,221,265,292,463}
tHideNSeek_Maps[1]["PosY"] = {299,480,737,312,441,365,665,201,193,457,529}
tHideNSeek_Maps[2] = {}
tHideNSeek_Maps[2]["MapId"] = 1001
tHideNSeek_Maps[2]["PosX"] = {121,669,685,541,334,473}
tHideNSeek_Maps[2]["PosY"] = {405,447,588,302,307,251}
tHideNSeek_Maps[3] = {}
tHideNSeek_Maps[3]["MapId"] = 1002
tHideNSeek_Maps[3]["PosX"] = {256,765,443,625,506,148,323,377,364, 88, 70}
tHideNSeek_Maps[3]["PosY"] = {129,549,241,514,745,381,510,222,652,316,241}
tHideNSeek_Maps[4] = {}
tHideNSeek_Maps[4]["MapId"] = 1011
tHideNSeek_Maps[4]["PosX"] = {374,473,333,824,673,589,229,634,553,361}
tHideNSeek_Maps[4]["PosY"] = {325,624,334,675,781,320,394,354,765,105}
tHideNSeek_Maps[5] = {}
tHideNSeek_Maps[5]["MapId"] = 1015
tHideNSeek_Maps[5]["PosX"] = {515,889,394,243,789,319,400,911,692}
tHideNSeek_Maps[5]["PosY"] = {713,741,477,171,546,197,222,611,517}
tHideNSeek_Maps[6] = {}
tHideNSeek_Maps[6]["MapId"] = 1020
tHideNSeek_Maps[6]["PosX"] = {586,633,416,777,479,676,471,307,184,478,588}
tHideNSeek_Maps[6]["PosY"] = {848,644,637,540,343,397,526,261,549,349,510}

local tHideNSeek_Params = {}
tHideNSeek_Params["GlobalId"] = 27000
tHideNSeek_Params["Global_IsRunning"] = 0
tHideNSeek_Params["Global_RewardClaimed"] = 1

local tHideNSeek_Rewards = {}

-- 1% HonorableMoneyBag It~contains~200,000,000~Silver.~Right~click~to~open.
tHideNSeek_Rewards[1] = {}
tHideNSeek_Rewards[1]["Rate"] = 100
tHideNSeek_Rewards[1]["ItemName"] = "HonorableMoneyBag"
tHideNSeek_Rewards[1]["ItemType"] = 3005945
tHideNSeek_Rewards[1]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[1]["Log"] = "3005945,0,1,100"

-- 3% SplendidMoneyBag	It~contains~50,000,000~Silver.~Right~click~to~open.
tHideNSeek_Rewards[2] = {}
tHideNSeek_Rewards[2]["Rate"] = 300
tHideNSeek_Rewards[2]["ItemName"] = "SplendidMoneyBag"
tHideNSeek_Rewards[2]["ItemType"] = 3005944
tHideNSeek_Rewards[2]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[2]["Log"] = "3005944,0,1,300"

-- 3% 3007441	FantasticPack	This~magical~pack~will~bring~you~5,000~CPs,~and~a~garment~chosen~from~Flame~Dance,~Imperial~Robe~and~Evil~Pumpkin.
tHideNSeek_Rewards[3] = {}
tHideNSeek_Rewards[3]["Rate"] = 300
tHideNSeek_Rewards[3]["ItemName"] = "FantasticPack"
tHideNSeek_Rewards[3]["ItemType"] = 3007441
tHideNSeek_Rewards[3]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[3]["Log"] = "3007441,0,1,300"

-- 5% 3007442	MysteriousPack	This~magical~pack~will~bring~you~3,000~CPs,~and~a~garment~chosen~from~Imperial~Robe~and~Evil~Pumpkin.
tHideNSeek_Rewards[4] = {}
tHideNSeek_Rewards[4]["Rate"] = 500
tHideNSeek_Rewards[4]["ItemName"] = "MysteriousPack"
tHideNSeek_Rewards[4]["ItemType"] = 3007442
tHideNSeek_Rewards[4]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[4]["Log"] = "3007442,0,1,500"

-- 10% 3007403	GoldenTreasureChest From~this~splendid~chest,~you~can~choose~to~claim~10~Chi~Point~Packs~(2000)~&~1~Senior~Training~Box~(50),~or~2,000~CPs.
tHideNSeek_Rewards[5] = {}
tHideNSeek_Rewards[5]["Rate"] = 1000
tHideNSeek_Rewards[5]["ItemName"] = "GoldenTreasureChest"
tHideNSeek_Rewards[5]["ItemType"] = 3007403
tHideNSeek_Rewards[5]["ItemAttr"] = "0"
tHideNSeek_Rewards[5]["Log"] = "3007403,0,1,1000"

-- 5% 722057	PowerEXPBall	Use~it~to~gain~10%~of~your~current~EXP.~Right~click~to~use.
tHideNSeek_Rewards[6] = {}
tHideNSeek_Rewards[6]["Rate"] = 500
tHideNSeek_Rewards[6]["ItemName"] = "PowerEXPBall"
tHideNSeek_Rewards[6]["ItemType"] = 722057
tHideNSeek_Rewards[6]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[6]["Log"] = "722057,0,1,500"

-- 10% 3006895	RobeofDarknessPack	Open~to~select~a~1%~blessed~30-day~Robe~of~Darkness~(Hades),~or~a~1%~blessed~30-day~King~Of~Scorpions.
tHideNSeek_Rewards[7] = {}
tHideNSeek_Rewards[7]["Rate"] = 1000
tHideNSeek_Rewards[7]["ItemName"] = "RobeofDarknessPack"
tHideNSeek_Rewards[7]["ItemType"] = 3006895
tHideNSeek_Rewards[7]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[7]["Log"] = "3006895,0,1,1000"

-- 25% 3006237	P7EquipmentSoulPack	Open~to~select~a~P7~Dragon~Soul~of~equipment.
tHideNSeek_Rewards[8] = {}
tHideNSeek_Rewards[8]["Rate"] = 2500
tHideNSeek_Rewards[8]["ItemName"] = "P7EquipmentSoulPack"
tHideNSeek_Rewards[8]["ItemType"] = 3006237
tHideNSeek_Rewards[8]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[8]["Log"] = "3006237,0,1,2500"

-- 50% 3006030	+6StoneValuePack	Open~to~receive~a~+6~Stone~and~a~+5~Stone.
tHideNSeek_Rewards[9] = {}
tHideNSeek_Rewards[9]["Rate"] = 5000
tHideNSeek_Rewards[9]["ItemName"] = "+6StoneValuePack"
tHideNSeek_Rewards[9]["ItemType"] = 3006030
tHideNSeek_Rewards[9]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[9]["Log"] = "3006030,0,1,5000"

-- 100% 3005673	FamedChiPack	It~contains~10,000~Chi~Points.~Right~click~to~open.
tHideNSeek_Rewards[10] = {}
tHideNSeek_Rewards[10]["Rate"] = 10000
tHideNSeek_Rewards[10]["ItemName"] = "FamedChiPack"
tHideNSeek_Rewards[10]["ItemType"] = 3005673
tHideNSeek_Rewards[10]["ItemAttr"] = "0 1"
tHideNSeek_Rewards[10]["Log"] = "3005673,0,1,10000"


function PigletHideNSeek_ServerStart()

    Sys_SetSynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_IsRunning"], 0) -- set event to idle
    Sys_SetSynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_RewardClaimed"], 0) -- set reward to unclaimed

end

function PigletHideNSeek_CheckStartTime()

    if Get_SysDynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_IsRunning"]) == 1 then
        -- event is already running?
        return
    end

    local nRate = math.random(100)
    if (nRate > 30) then
        -- event will not run this hour
        return
    end

    PigletHideNSeek_StartEvent()

end

function PigletHideNSeek_CheckEndTime()

    if PigletHideNSeek_RewardIsClaimable() then
        Sys_NormalBroadcast(tPigletHideNSeek_Message["EventEnd"])
    end

    Sys_SetSynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_IsRunning"], 0) -- set event to idle
    Npc_MoveNpcPos(10621, 5000, 72, 93)

end

function PigletHideNSeek_BroadcastMsg()

    if not PigletHideNSeek_RewardIsClaimable() then
        return
    end

    local time = os.date("*t")
    local currentMinute = time.min

    local nNpcMapId = Get_NpcMapID(10621)
    local sMapName = Get_MapName(nNpcMapId)

    if currentMinute == 15 or currentMinute == 20 or currentMinute == 25 or currentMinute == 30 then
        Sys_NormalBroadcast(tPigletHideNSeek_Message["EventRunningPrompt"])
    elseif currentMinute == 31 then
        Sys_NormalBroadcast(tPigletHideNSeek_Message["EventLast10MinutesPrompt"])
    elseif currentMinute == 36 then
        Sys_NormalBroadcast(string.format(tPigletHideNSeek_Message["EventLast5MinutesPrompt"], sMapName))
    elseif currentMinute == 40 then
        Sys_NormalBroadcast(string.format(tPigletHideNSeek_Message["EventLastMinutePrompt"], sMapName))
    end

end

function PigletHideNSeek_StartEvent()

    local nMapCount = #(tHideNSeek_Maps)
    local nMapIdx = math.random(1, nMapCount)
    local locationInformation = tHideNSeek_Maps[nMapIdx]

    local nPosCount = #(locationInformation["PosX"])
    local nPosIdx = math.random(1, nPosCount)
    local nPosX = locationInformation["PosX"][nPosIdx]
    local nPosY = locationInformation["PosY"][nPosIdx]

    if Npc_MoveNpcPos(10621, locationInformation["MapId"], nPosX, nPosY) then
        Sys_SetSynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_RewardClaimed"], 0) -- set reward to unclaimed
        Sys_SetSynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_IsRunning"], 1) -- event is running

        Sys_NormalBroadcast(tPigletHideNSeek_Message["EventStart"])
    end
end

function PigletHideNSeek_DeliverRewards()

    if not PigletHideNSeek_RewardIsClaimable then
        User_TalkChannel2011(tPigletHideNSeek_Message["NoReward"], nil)
        return
    end

    if not User_CheckLeftSpace(3, nil) then
        User_TalkChannel2011(tPigletHideNSeek_Message["NoSpace"], nil)
        return
    end

    local sUserName = Get_UserName(nil)
    local sMapName = Get_MapName(nil)
    local sItemName = PigletHideNSeek_GetReward()

    if sItemName == "None" then
        return
    end

    Sys_SetSynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_RewardClaimed"], 1) -- set reward to claimed
    Npc_MoveNpcPos(10621, 5000, 72, 93)

    Sys_NormalBroadcast(string.format(tPigletHideNSeek_Message["EventWinnerNotify"], sUserName, sMapName, sItemName))

    User_AddCultivation(2000, 0)

end

function PigletHideNSeek_GetReward()
    local nRate = math.random(10000)
    for i, v in pairs(tHideNSeek_Rewards) do
        if (nRate < v["Rate"]) then

            if Item_AddNewItem(v["ItemType"], v["ItemAttr"], nil) then
                Sys_SaveActionParamLog("piglet_hns", v["Log"])
            else
                Sys_SaveAbnormalLog(string.format("Error on Item_AddNewItem(%d, \"%s\", nil) for user %s [%d]", v["ItemType"], v["ItemAttr"], Get_UserName(0), Get_UserId()))
            end

            return v["ItemName"]
        end
    end
    Sys_SaveAbnormalLog("System could not get reward to Piglet Hide and seek event!!!")
    return "None"
end

function PigletHideNSeek_RewardIsClaimable()
    return Get_SysDynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_IsRunning"]) == 1
            and Get_SysDynaGlobalData(tHideNSeek_Params["GlobalId"], tHideNSeek_Params["Global_RewardClaimed"]) == 0
end

tServerStart["tFunction"] = tServerStart["tFunction"] or {}
table.insert(tServerStart["tFunction"],PigletHideNSeek_ServerStart)

tOntimerMin_M[11] = tOntimerMin_M[11] or {}
table.insert(tOntimerMin_M[11],PigletHideNSeek_CheckStartTime)

table.insert(tSystem_Prompet_Func,PigletHideNSeek_BroadcastMsg)

tOntimerMin_M[41] = tOntimerMin_M[41] or {}
table.insert(tOntimerMin_M[41],PigletHideNSeek_CheckEndTime)

tNpcGossip[10621] = tNpcGossip[10621] or DefaultNpc:new{}
tNpcGossip[10621]["OptionHidden"] = 1
tNpcGossip[10621]["Text1-1"] = { 111 }
tNpcGossip[10621]["Text111"] = tPigletHideNSeek_Text[10621]["Text111"]
tNpcGossip[10621]["ChkFunc1-1"] = function()
    local sUserName = Get_UserName(nil)
    tNpcGossip[10621]["Text111"] = string.format(tPigletHideNSeek_Text[10621]["Text111"], sUserName)
    return true
end
tNpcGossip[10621]["tOption1-1"] = {1,2}

tNpcGossip[10621]["Option1"] = tPigletHideNSeek_Text[10621]["Option1"]
tNpcGossip[10621]["OptionChkFunc1"] = function ()
    return PigletHideNSeek_RewardIsClaimable()
end
tNpcGossip[10621]["OptionFunc1"] = "PigletHideNSeek_DeliverRewards"

tNpcGossip[10621]["Option2"] = tPigletHideNSeek_Text[10621]["Option2"]
tNpcGossip[10621]["OptionChkFunc2"] = function ()
    return not PigletHideNSeek_RewardIsClaimable()
end
