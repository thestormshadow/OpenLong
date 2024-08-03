------------------------------------------------------------------------------------
--Name:		[Conquer][Event]DisCity
--Purpose:	Defines rewards for Dis City event.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/08/02
------------------------------------------------------------------------------------

local tConquerEventDisCityMapRewards = {}
      -- 10x Dragon balls
      tConquerEventDisCityMapRewards[1] = {}
      tConquerEventDisCityMapRewards[1]["ItemType"] = 1088000
      tConquerEventDisCityMapRewards[1]["Num"] = 10
      tConquerEventDisCityMapRewards[1]["X"] = 125
      tConquerEventDisCityMapRewards[1]["Y"] = 116
      tConquerEventDisCityMapRewards[1]["CX"] = 50
      tConquerEventDisCityMapRewards[1]["CY"] = 60
      tConquerEventDisCityMapRewards[1]["Duration"] = 300

      -- 10x MagicSpar(200Thousand)
      tConquerEventDisCityMapRewards[2] = {}
      tConquerEventDisCityMapRewards[2]["ItemType"] = 3006594
      tConquerEventDisCityMapRewards[2]["Num"] = 10
      tConquerEventDisCityMapRewards[2]["X"] = 125
      tConquerEventDisCityMapRewards[2]["Y"] = 116
      tConquerEventDisCityMapRewards[2]["CX"] = 50
      tConquerEventDisCityMapRewards[2]["CY"] = 60
      tConquerEventDisCityMapRewards[2]["Duration"] = 300

      -- 5x MagicSpar(500Thousand)
      tConquerEventDisCityMapRewards[3] = {}
      tConquerEventDisCityMapRewards[3]["ItemType"] = 3006595
      tConquerEventDisCityMapRewards[3]["Num"] = 5
      tConquerEventDisCityMapRewards[3]["X"] = 125
      tConquerEventDisCityMapRewards[3]["Y"] = 116
      tConquerEventDisCityMapRewards[3]["CX"] = 50
      tConquerEventDisCityMapRewards[3]["CY"] = 60
      tConquerEventDisCityMapRewards[3]["Duration"] = 300

      -- 10x ModestyBook
      tConquerEventDisCityMapRewards[4] = {}
      tConquerEventDisCityMapRewards[4]["ItemType"] = 723342
      tConquerEventDisCityMapRewards[4]["Num"] = 10
      tConquerEventDisCityMapRewards[4]["X"] = 125
      tConquerEventDisCityMapRewards[4]["Y"] = 116
      tConquerEventDisCityMapRewards[4]["CX"] = 50
      tConquerEventDisCityMapRewards[4]["CY"] = 60
      tConquerEventDisCityMapRewards[4]["Duration"] = 300

-- 188915	GoldCloth(Saint)
-- 193275	RobeofDarkness(Hades)
-- 193445	FrozenFantasy[Glaze]
-- 193525	FrozenFantasy[Glory]

function ConquerEventReward_DisCityRewardMapDrop()
    for k, v in pairs(tConquerEventDisCityMapRewards) do
        Map_DropMultiItems(4025,v["ItemType"],v["X"],v["Y"],v["CX"],v["CY"],v["Num"],v["Duration"])
    end
end

function ConquerEventReward_DisCityRandomShinyGarment()

    local nUserId = Get_UserId()
    local rate = math.random(100)
    local nItemType = 0

    if rate < 25 then
        nItemType = 188915
    elseif rate < 50 then
        nItemType = 193275
    elseif rate < 75 then
        nItemType = 193445
    else
        nItemType = 193525
    end

    if Item_AddNewItem(nItemType, "0 1 0 10080 0 0 0 1", nUserId) then
        Sys_SaveActionParamLog("discity_shiny", string.format("%d,0 1 0 10080 0 0 0 1", nItemType))
    else
        Sys_SaveAbnormalLog(string.format("Could not add [%d] to [userid:%d]", nItemType, nUserId))
    end
end

-- 5059 UltimatePluto
tMonster[5059] = tMonster[5059] or {}
tMonster[5059]["tFunction"] = tMonster[5059]["tFunction"] or {}
table.insert(tMonster[5059]["tFunction"],ConquerEventReward_DisCityRewardMapDrop)