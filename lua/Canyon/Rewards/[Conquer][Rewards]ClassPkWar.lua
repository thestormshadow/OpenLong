------------------------------------------------------------------------------------
--Name:		[Conquer][Rewards]ClassPkWar
--Purpose:	Defines rewards for Class PK War event.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/08/28
------------------------------------------------------------------------------------

local tConquerEventClassPkWarReward = {}
tConquerEventClassPkWarReward[1] = {}
tConquerEventClassPkWarReward[1][1] = {}
tConquerEventClassPkWarReward[1][1]["ItemType"] = 720028
tConquerEventClassPkWarReward[1][1]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[1][1]["Log"] = "130,720028,0,1"
tConquerEventClassPkWarReward[1][2] = {}
tConquerEventClassPkWarReward[1][2]["ItemType"] = 3005944
tConquerEventClassPkWarReward[1][2]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[1][2]["Log"] = "130,3005944,0,1"
tConquerEventClassPkWarReward[1][3] = {}
tConquerEventClassPkWarReward[1][3]["ItemType"] = 722057
tConquerEventClassPkWarReward[1][3]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[1][3]["Log"] = "130,722057,0,1"
tConquerEventClassPkWarReward[1][4] = {}
tConquerEventClassPkWarReward[1][4]["ItemType"] = 3006895
tConquerEventClassPkWarReward[1][4]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[1][4]["Log"] = "130,3006895,0,1"
tConquerEventClassPkWarReward[1][5] = {}
tConquerEventClassPkWarReward[1][5]["ItemType"] = 730006
tConquerEventClassPkWarReward[1][5]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[1][5]["Log"] = "130,730006,0,1"
tConquerEventClassPkWarReward[1][6] = {}
tConquerEventClassPkWarReward[1][6]["ItemType"] = 723700
tConquerEventClassPkWarReward[1][6]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[1][6]["Log"] = "130,723700,0,1"

tConquerEventClassPkWarReward[2] = {}
tConquerEventClassPkWarReward[2][1] = {}
tConquerEventClassPkWarReward[2][1]["ItemType"] = 1088000
tConquerEventClassPkWarReward[2][1]["ItemAttr"] = "0 8"
tConquerEventClassPkWarReward[2][1]["Log"] = "120,1088000,0,8"
tConquerEventClassPkWarReward[2][2] = {}
tConquerEventClassPkWarReward[2][2]["ItemType"] = 3007366
tConquerEventClassPkWarReward[2][2]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[2][2]["Log"] = "120,3007366,0,1"
tConquerEventClassPkWarReward[2][3] = {}
tConquerEventClassPkWarReward[2][3]["ItemType"] = 727843
tConquerEventClassPkWarReward[2][3]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[2][3]["Log"] = "120,727843,0,1"
tConquerEventClassPkWarReward[2][4] = {}
tConquerEventClassPkWarReward[2][4]["ItemType"] = 730005
tConquerEventClassPkWarReward[2][4]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[2][4]["Log"] = "120,730005,0,1"
tConquerEventClassPkWarReward[2][5] = {}
tConquerEventClassPkWarReward[2][5]["ItemType"] = 723700
tConquerEventClassPkWarReward[2][5]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[2][5]["Log"] = "120,723700,0,1"

tConquerEventClassPkWarReward[3] = {}
tConquerEventClassPkWarReward[3][1] = {}
tConquerEventClassPkWarReward[3][1]["ItemType"] = 1088000
tConquerEventClassPkWarReward[3][1]["ItemAttr"] = "0 5"
tConquerEventClassPkWarReward[3][1]["Log"] = "100,1088000,0,5"
tConquerEventClassPkWarReward[3][2] = {}
tConquerEventClassPkWarReward[3][2]["ItemType"] = 3006025
tConquerEventClassPkWarReward[3][2]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[3][2]["Log"] = "100,3006025,0,1"
tConquerEventClassPkWarReward[3][3] = {}
tConquerEventClassPkWarReward[3][3]["ItemType"] = 727845
tConquerEventClassPkWarReward[3][3]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[3][3]["Log"] = "100,727845,0,1"
tConquerEventClassPkWarReward[3][4] = {}
tConquerEventClassPkWarReward[3][4]["ItemType"] = 730004
tConquerEventClassPkWarReward[3][4]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[3][4]["Log"] = "100,730004,0,1"
tConquerEventClassPkWarReward[3][5] = {}
tConquerEventClassPkWarReward[3][5]["ItemType"] = 723700
tConquerEventClassPkWarReward[3][5]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[3][5]["Log"] = "100,723700,0,1"

tConquerEventClassPkWarReward[4] = {}
tConquerEventClassPkWarReward[4][1] = {}
tConquerEventClassPkWarReward[4][1]["ItemType"] = 1088000
tConquerEventClassPkWarReward[4][1]["ItemAttr"] = "0 3"
tConquerEventClassPkWarReward[4][1]["Log"] = "70,1088000,0,3"
tConquerEventClassPkWarReward[4][2] = {}
tConquerEventClassPkWarReward[4][2]["ItemType"] = 730003
tConquerEventClassPkWarReward[4][2]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[4][2]["Log"] = "70,730003,0,1"
tConquerEventClassPkWarReward[4][3] = {}
tConquerEventClassPkWarReward[4][3]["ItemType"] = 723700
tConquerEventClassPkWarReward[4][3]["ItemAttr"] = "0 1"
tConquerEventClassPkWarReward[4][3]["Log"] = "70,723700,0,1"

function ConquerEventReward_ClassPkWarWinner(nRewardTier)

    if nRewardTier < 1 or nRewardTier > 4 then
        Sys_SaveAbnormalLog(string.format("Invalid reward tear for class pk reward. %d", nRewardTier))
        return
    end

    local nUserId = Get_UserId()
    local nUserLev = Get_UserLevel(nUserId)
    local sTierString = "-100"

    if nUserLev >= 130 then
        sTierString = "+130"
    elseif nUserLev >= 120 and nUserLev < 130 then
        sTierString = "120-129"
    elseif nUserLev >= 100 and nUserLev < 120 then
        sTierString = "100-119"
    end

    for i, v in pairs(tConquerEventClassPkWarReward[nRewardTier]) do
        if Item_AddNewItem(v["ItemType"], v["ItemAttr"], nUserId) then
            Sys_SaveActionParamLog("class_pk_lua_reward", v["Log"])
        else
            Sys_SaveAbnormalLog(string.format("Could not add reward [%d;%s] to user [%d] for class pk war tier %d", v["ItemType"], v["ItemAttr"], nUserId, nRewardTier))
        end
    end
end
