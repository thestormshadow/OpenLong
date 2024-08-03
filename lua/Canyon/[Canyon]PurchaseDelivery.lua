------------------------------------------------------------------------------------
--Name:		[Canyon]PurchaseDelivery
--Purpose:	Responsible for delivering rewards for website purchases.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/07/20
------------------------------------------------------------------------------------

------------------------------------------------------------------------------------
-- Parameters
-- Item: Array of items that will be achieved by the user
-- Item.ItemType: Item type of the item (cq_itemtype)
-- Item.ItemAttr: Attributes of the item (See Item_AddNewItem for reference)
--      flag addamount monopoly save_time active onlinetime data reduce_dmg add_life
--      addlevel_exp magic3 gem1 gem2 magic1 magic2 amount amount_limit anti_monster
--      ident color
-- Item.Log: Log entry that will be created after achieving the item
-- Log: Log entry that will be created after achieving other rewards
-- ConquerPoints: Amount of Conquer Points to be added to the account
-- ConquerPointsMono: Amount of Conquer Points (B) to be added to the account
-- Silvers: Amount of Silvers to be added to the account
-- Message: Message to be sent to the user after claiming
-- Broadcast: Message to be broadcasted to the world after the user claims the reward
-- Effect: Effect to be displayed in the player
------------------------------------------------------------------------------------

local tCanyonPurchaseTable = {}
    -- [193275] RobeofDarkness(Hades)(B) Permanent
    tCanyonPurchaseTable[1] = {}
    tCanyonPurchaseTable[1]["Item"] = {}
    tCanyonPurchaseTable[1]["Item"][1] = {}
    tCanyonPurchaseTable[1]["Item"][1]["ItemType"] = 193275
    tCanyonPurchaseTable[1]["Item"][1]["ItemAttr"] = "0 1 3 0 0 0 0 1"
    tCanyonPurchaseTable[1]["Item"][1]["Log"] = "1,193275,0,1,3,0,0,0,0,1"

local canyonPurchaseLogFile = "lua_purchase"

function Canyon_GetRewardById(nRewardId)
    for index, value in pairs(tCanyonPurchaseTable) do
        if (index == nRewardId) then
            return value
        end
    end
    return nil
end

function Canyon_PurchaseClaimReward(nRewardId)
    local nUserId = Get_UserId()

    if nUserId == nil or nUserId == 0 then
        Sys_SaveAbnormalLog("Cannot make purchase delivery to user id 0")
        return false
    end

    local tReward = Canyon_GetRewardById(nRewardId)
    if tReward == nil then
        Sys_SaveAbnormalLog(string.format("Invalid purchase reward [%d] for user [%d]", nRewardId, nUserId))
        return false
    end

    --if tReward["BagSpace"] ~= nil and tReward["BagSpace"] > 0 then
    --    if not User_CheckLeftSpace(tReward["BagSpace"]) then
    --        -- TODO: notify server 
    --        User_TalkChannel2005(string.format("You need to leave %d spaces in your inventory.", tReward["BagSpace"]))
    --        return false
    --    end
    --end

    -- Let's see if there are items to deliver first
    if tReward["Item"] ~= nil then
        for i, v in pairs(tReward["Item"]) do
            if Item_AddNewItem(v["ItemType"], v["ItemAttr"]) then
                Sys_SaveActionParamLog(canyonPurchaseLogFile, v["Log"])
            else
                Sys_SaveAbnormalLog(string.format("Could not add item reward [%d] for user [%d]", v["ItemType"], nUserId))
            end
        end
    end

    if tReward["Money"] ~= nil and type(tReward["Money"] == "number" and tReward["Money"] > 0) then
        User_AddMoney(tReward["Money"])
    end

    if tReward["ConquerPoints"] ~= nil and type(tReward["ConquerPoints"] == "number" and tReward["ConquerPoints"] > 0) then
        User_AddEMoney(tReward["ConquerPoints"])
    end

    if tReward["ConquerPointsMono"] ~= nil and type(tReward["ConquerPointsMono"] == "number" and tReward["ConquerPointsMono"] > 0) then
        User_AddEMoneyMono(tReward["ConquerPointsMono"])
    end

    if tReward["Log"] ~= nil then
        Sys_SaveActionParamLog(canyonPurchaseLogFile, tReward["Log"])
    end

    if tReward["Effect"] ~= nil then
        User_EffectAdd("self", tReward["Effect"])
    end

    if tReward["Message"] ~= nil then
        User_TalkChannel2005(tReward["Message"])
    end

    return true
end