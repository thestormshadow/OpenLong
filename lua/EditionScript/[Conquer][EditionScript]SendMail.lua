----------------------------------------------------------------------------
--Name:		[??][????]??????.lua
--Purpose:	??????
--Creator: 	??
--Created:	2016/06/14
----------------------------------------------------------------------------

-- ????
-- SendMail_

local tSendMail_Reward = {}
tSendMail_Reward[90123300] = {}
tSendMail_Reward[90123300]["Space"] = 1
tSendMail_Reward[90123300]["ActionId"] = 563999
tSendMail_Reward[90123300]["ExistDay"] = 30
tSendMail_Reward[90123300]["RewardItem"] = {}
tSendMail_Reward[90123300]["RewardItem"][1] = {}
tSendMail_Reward[90123300]["RewardItem"][1]["Id"] = 3100013
tSendMail_Reward[90123300]["RewardItem"][1]["Attr"] = "0 1 0 1440 1"
tSendMail_Reward[90123300]["Talk"] = tSendMail_Text[90123300]["Talk"]

tSendMail_Reward[90123301] = {}
tSendMail_Reward[90123301]["Space"] = 1
tSendMail_Reward[90123301]["ActionId"] = 564000
tSendMail_Reward[90123301]["ExistDay"] = 30
tSendMail_Reward[90123301]["RewardItem"] = {}
tSendMail_Reward[90123301]["RewardItem"][1] = {}
tSendMail_Reward[90123301]["RewardItem"][1]["Id"] = 3100014
tSendMail_Reward[90123301]["RewardItem"][1]["Attr"] = "0 1 0 1440 1"
tSendMail_Reward[90123301]["Talk"] = tSendMail_Text[90123301]["Talk"]

tSendMail_Reward[90123302] = {}
tSendMail_Reward[90123302]["Space"] = 1
tSendMail_Reward[90123302]["ActionId"] = 564001
tSendMail_Reward[90123302]["ExistDay"] = 30
tSendMail_Reward[90123302]["RewardItem"] = {}
tSendMail_Reward[90123302]["RewardItem"][1] = {}
tSendMail_Reward[90123302]["RewardItem"][1]["Id"] = 3100015
tSendMail_Reward[90123302]["RewardItem"][1]["Attr"] = "0 1 0 1440 1"
tSendMail_Reward[90123302]["Talk"] = tSendMail_Text[90123302]["Talk"]

tSendMail_Reward[90123303] = {}
tSendMail_Reward[90123303]["Space"] = 1
tSendMail_Reward[90123303]["ActionId"] = 564002
tSendMail_Reward[90123303]["ExistDay"] = 30
tSendMail_Reward[90123303]["RewardItem"] = {}
tSendMail_Reward[90123303]["RewardItem"][1] = {}
tSendMail_Reward[90123303]["RewardItem"][1]["Id"] = 3100016
tSendMail_Reward[90123303]["RewardItem"][1]["Attr"] = "0 1 0 1440 1"
tSendMail_Reward[90123303]["Talk"] = tSendMail_Text[90123303]["Talk"]

tSendMail_Reward[90123304] = {}
tSendMail_Reward[90123304]["Space"] = 1
tSendMail_Reward[90123304]["ActionId"] = 564003
tSendMail_Reward[90123304]["ExistDay"] = 30
tSendMail_Reward[90123304]["RewardItem"] = {}
tSendMail_Reward[90123304]["RewardItem"][1] = {}
tSendMail_Reward[90123304]["RewardItem"][1]["Id"] = 3100017
tSendMail_Reward[90123304]["RewardItem"][1]["Attr"] = "0 1 0 1440 1"
tSendMail_Reward[90123304]["Talk"] = tSendMail_Text[90123304]["Talk"]

function SendMail_Main(nIndex,nNowUserId)
    if tSendMail_Reward[nIndex] == nil then
        return
    end

    local nUserId = nNowUserId or Get_UserId()
    local nSpace = tSendMail_Reward[nIndex]["Space"]

    -- ??????(????????)
    if nSpace ~= nil and (not User_CheckLeftSpace(nSpace,nUserId)) then
        local nMoney = tSendMail_Reward[nIndex]["RewardMoney"] or 0
        local nEmoney = tSendMail_Reward[nIndex]["RewardEMoney"] or 0
        local nActionId = tSendMail_Reward[nIndex]["ActionId"] or 0
        local nEmoneyType = tSendMail_Reward[nIndex]["EmoneyType"] or 0
        local nExistDay = tSendMail_Reward[nIndex]["ExistDay"] or 0
        local sSender = tSendMail_Text[nIndex]["Sender"]
        local sTitle = tSendMail_Text[nIndex]["Title"]
        local sContent = tSendMail_Text[nIndex]["Content"]

        if Sys_SendMail(nUserId,nMoney,nEmoney,nActionId,nEmoneyType,nExistDay,sSender,sTitle,sContent) then
            User_TalkChannel2005(tSendMail_Text[nIndex]["Talk"],nUserId)
        end
    else
        RewardTemplate_Reward(tSendMail_Reward[nIndex],nUserId)
    end
end

-- ??????????SendMail
-- 1.??ID
-- 2 ??
-- 3 ??
-- 4 ??
-- 5 ??????
-- 6 ?????
-- 7 ??
-- 8 ??
-- ????????true?????false

-- cq_bonus?nActionId???
-- ??? ==1?????  ==2????? ==3????
-- ??????????? ????

function SendMail_GetBonusByAction(nUserId,nActionId)
    if nActionId <= 0 or nActionId == nil then
        return
    end

    local sStr = tostring(nActionId)
    local nLength = string.len(sStr)

    local nDis = "1"
    for i = 1,nLength-1 do
        nDis = nDis .. 0
    end
    nDis = tonumber(nDis)
    local nType = math.floor(nActionId/nDis)
    local nNum = nActionId%nDis
    local nExistDay = 30

    if nType == 1 then
        -- ???
        Sys_SendKOKMail(nUserId,nNum,0,0,nExistDay,tSendMail_Text["Mail"]["Sender"],tSendMail_Text["Mail"]["Title"],string.format(tSendMail_Text["Mail"]["Content"][1],nNum))
    elseif nType == 2 then
        -- ???
        Sys_SendKOKMail(nUserId,0,nNum,0,nExistDay,tSendMail_Text["Mail"]["Sender"],tSendMail_Text["Mail"]["Title"],string.format(tSendMail_Text["Mail"]["Content"][2],nNum))
    elseif nType == 3 then
        -- ???
        Sys_SendKOKMail(nUserId,0,0,nNum,nExistDay,tSendMail_Text["Mail"]["Sender"],tSendMail_Text["Mail"]["Title"],string.format(tSendMail_Text["Mail"]["Content"][3],nNum))
    else
        return
    end
end


-- ????
tGetKOKBonusByAction["tFunction"] = tGetKOKBonusByAction["tFunction"] or {}
table.insert(tGetKOKBonusByAction["tFunction"],SendMail_GetBonusByAction)
