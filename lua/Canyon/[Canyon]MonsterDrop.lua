------------------------------------------------------------------------------------
--Name:		[Canyon]MonsterDrop
--Purpose:	Defines an interface for all common monsters drop.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/07/17
------------------------------------------------------------------------------------

------------------------------------------------------------------------------------
-- Parameters
-- ItemType: the item type to be dropped
-- Money: amount of money to be dropped
-- ConquerPoints: amount of Conquer Points to be dropped
-- ConquerPointsMono: amount of CPs(B) to be dropped
-- MinRate: n in MaxRate
-- MaxRate: MinRate in n
-- Rate: n in 10000
-- AutoHunt: [boolean] if set means that it will be dropped only on auto hunt or off
-- Child: [Object array]
-- OpenDate: [yyyy-MM-dd HH:mm] when drop starts
-- CloseDate: [yyyy-MM-dd HH:mm] when drop ends
-- Message: Valid for parent and child, no args
-- ActionLog: Always require two integer args, monsterid and mapid
------------------------------------------------------------------------------------
-- When Child is set Rate will define the coeficient for the drops inside
-- Rate inside of a child is the amount necessary for that element
-- Example: if main object has 100 MaxRate all Childs must set a Rate, or they wont
-- be hit.
-- generatedRandom = rand(Parent[MaxRate])
-- if child[Rate] < generatedRandom then drop
-- Max depth = 1
------------------------------------------------------------------------------------

local tCanyonValidMonsterDrops = {}
    -- [1088000] DragonBall
    tCanyonValidMonsterDrops[1] = {}
    tCanyonValidMonsterDrops[1]["ItemType"] = 1088000
    tCanyonValidMonsterDrops[1]["MinRate"] = 2350
    tCanyonValidMonsterDrops[1]["MaxRate"] = 10200000
    tCanyonValidMonsterDrops[1]["Message"] = "You have found a Dragon Ball."
    tCanyonValidMonsterDrops[1]["ActionLog"] = "%d,%d,1,1088000,2350,10200000"
    tCanyonValidMonsterDrops[1]["AutoHunt"] = true

    -- Conquer Points
    tCanyonValidMonsterDrops[2] = {}
    tCanyonValidMonsterDrops[2]["ConquerPoints"] = 215
    tCanyonValidMonsterDrops[2]["MinRate"] = 2350
    tCanyonValidMonsterDrops[2]["MaxRate"] = 10200000
    tCanyonValidMonsterDrops[2]["Message"] = "You have found 215 CPs."
    tCanyonValidMonsterDrops[2]["ActionLog"] = "%d,%d,2,215,2350,10200000"
    tCanyonValidMonsterDrops[2]["Effect"] = "angelwing"
	tCanyonValidMonsterDrops[2]["AutoHunt"] = false

    -- [700001-700061] Common Gems
    tCanyonValidMonsterDrops[3] = {}
    tCanyonValidMonsterDrops[3]["MinRate"] = 130
    tCanyonValidMonsterDrops[3]["MaxRate"] = 350000
    tCanyonValidMonsterDrops[3]["Rate"] = 100
    tCanyonValidMonsterDrops[3]["Child"] = {}
    tCanyonValidMonsterDrops[3]["Child"][1] = {}
    tCanyonValidMonsterDrops[3]["Child"][1]["Rate"] = 10
    tCanyonValidMonsterDrops[3]["Child"][1]["ItemType"] = 700001
    tCanyonValidMonsterDrops[3]["Child"][2] = {}
    tCanyonValidMonsterDrops[3]["Child"][2]["Rate"] = 20
    tCanyonValidMonsterDrops[3]["Child"][2]["ItemType"] = 700011
    tCanyonValidMonsterDrops[3]["Child"][3] = {}
    tCanyonValidMonsterDrops[3]["Child"][3]["Rate"] = 30
    tCanyonValidMonsterDrops[3]["Child"][3]["ItemType"] = 700021
    tCanyonValidMonsterDrops[3]["Child"][4] = {}
    tCanyonValidMonsterDrops[3]["Child"][4]["Rate"] = 40
    tCanyonValidMonsterDrops[3]["Child"][4]["ItemType"] = 700031
    tCanyonValidMonsterDrops[3]["Child"][5] = {}
    tCanyonValidMonsterDrops[3]["Child"][5]["Rate"] = 50
    tCanyonValidMonsterDrops[3]["Child"][5]["ItemType"] = 700041
    tCanyonValidMonsterDrops[3]["Child"][6] = {}
    tCanyonValidMonsterDrops[3]["Child"][6]["Rate"] = 60
    tCanyonValidMonsterDrops[3]["Child"][6]["ItemType"] = 700051
    tCanyonValidMonsterDrops[3]["Child"][7] = {}
    tCanyonValidMonsterDrops[3]["Child"][7]["Rate"] = 70
    tCanyonValidMonsterDrops[3]["Child"][7]["ItemType"] = 700061
    tCanyonValidMonsterDrops[3]["Child"][8] = {}
    tCanyonValidMonsterDrops[3]["Child"][8]["Rate"] = 80
    tCanyonValidMonsterDrops[3]["Child"][8]["ItemType"] = 700071
    tCanyonValidMonsterDrops[3]["Child"][9] = {}
    tCanyonValidMonsterDrops[3]["Child"][9]["Rate"] = 90
    tCanyonValidMonsterDrops[3]["Child"][9]["ItemType"] = 700101
    tCanyonValidMonsterDrops[3]["Child"][10] = {}
    tCanyonValidMonsterDrops[3]["Child"][10]["Rate"] = 100
    tCanyonValidMonsterDrops[3]["Child"][10]["ItemType"] = 700121

    -- [1088001] Meteor
    tCanyonValidMonsterDrops[4] = {}
    tCanyonValidMonsterDrops[4]["ItemType"] = 1088001
    tCanyonValidMonsterDrops[4]["MinRate"] = 500
    tCanyonValidMonsterDrops[4]["MaxRate"] = 2700000
    tCanyonValidMonsterDrops[4]["ActionLog"] = "%d,%d,4,1088001,500,2700000"

    -- Conquer Points Bound
    tCanyonValidMonsterDrops[5] = {}
    tCanyonValidMonsterDrops[5]["MinRate"] = 43
    tCanyonValidMonsterDrops[5]["MaxRate"] = 15450
    tCanyonValidMonsterDrops[5]["Rate"] = 100
    tCanyonValidMonsterDrops[5]["Child"] = {}
    tCanyonValidMonsterDrops[5]["Child"][1] = {}
    tCanyonValidMonsterDrops[5]["Child"][1]["Rate"] = 25
    tCanyonValidMonsterDrops[5]["Child"][1]["ConquerPointsMono"] = 1
    tCanyonValidMonsterDrops[5]["Child"][1]["Message"] = "You`ve found 1 CP(B)."
    tCanyonValidMonsterDrops[5]["Child"][1]["ActionLog"] = "%d,%d,5,1,1,43,15450,25"
    tCanyonValidMonsterDrops[5]["Child"][2] = {}
    tCanyonValidMonsterDrops[5]["Child"][2]["Rate"] = 50
    tCanyonValidMonsterDrops[5]["Child"][2]["ConquerPointsMono"] = 3
    tCanyonValidMonsterDrops[5]["Child"][2]["Message"] = "You`ve found 3 CPs(B)."
    tCanyonValidMonsterDrops[5]["Child"][2]["ActionLog"] = "%d,%d,5,2,3,43,15450,50"
    tCanyonValidMonsterDrops[5]["Child"][3] = {}
    tCanyonValidMonsterDrops[5]["Child"][3]["Rate"] = 75
    tCanyonValidMonsterDrops[5]["Child"][3]["ConquerPointsMono"] = 5
    tCanyonValidMonsterDrops[5]["Child"][3]["Message"] = "You`ve found 5 CPs(B)."
    tCanyonValidMonsterDrops[5]["Child"][3]["ActionLog"] = "%d,%d,5,3,5,43,15450,75"
    tCanyonValidMonsterDrops[5]["Child"][4] = {}
    tCanyonValidMonsterDrops[5]["Child"][4]["Rate"] = 100
    tCanyonValidMonsterDrops[5]["Child"][4]["ConquerPointsMono"] = 7
    tCanyonValidMonsterDrops[5]["Child"][4]["Message"] = "You`ve found 7 CPs(B)."
    tCanyonValidMonsterDrops[5]["Child"][4]["ActionLog"] = "%d,%d,5,4,7,43,15450,100"

    -- FireOfHell
    tCanyonValidMonsterDrops[6] = {}
    tCanyonValidMonsterDrops[6]["ItemType"] = 1060101
    tCanyonValidMonsterDrops[6]["MinRate"] = 43
    tCanyonValidMonsterDrops[6]["MaxRate"] = 750000
    tCanyonValidMonsterDrops[6]["ActionLog"] = "%d,%d,6,1060101,43,750000"

    -- Bomb
    tCanyonValidMonsterDrops[7] = {}
    tCanyonValidMonsterDrops[7]["ItemType"] = 1060100
    tCanyonValidMonsterDrops[7]["MinRate"] = 43
    tCanyonValidMonsterDrops[7]["MaxRate"] = 450000
    tCanyonValidMonsterDrops[6]["ActionLog"] = "%d,%d,7,1060100,43,450000"

local tCanyonValidMonsterTypeIds = {
    1, --	    Pheasant
    2, --	    Turtledove
    3, --	    Robin
    4, --	    Apparition
    5, --	    Poltergeist
    6, --	    WingedSnake
    7, --	    Bandit
    8, --	    Ratling
    9, --	    FireSpirit
    10, --	    Macaque
    11, --	    GiantApe
    12, --	    ThunderApe
    13, --	    Snakeman
    14, --	    SandMonster
    15, --	    HillMonster
    16, --	    RockMonster
    17, --	    BladeGhost
    18, --	    Birdman
    19, --	    HawKing
    20, --	    TombBat
    55, --	    BanditL97
    56, --	    BloodyBat
    57, --	    BullMonster
    58, --	    RedDevilL117
    59, --	    RockMonsterL15
    64, --	    HeavyGhostL23
    65, --	    WingedSnakeL28
    66, --	    BanditL33
    67, --	    FireRatL38
    68, --	    FireSpiritL43
    69, --	    MacaqueL48
    70, --	    GiantApeL53
    71, --	    ThunderApeL58
    72, --	    SnakemanL63
    73, --	    SandMonsterL68
    74, --	    HillMonsterL73
    75, --	    RockMonsterL78
    76, --	    BladeGhostL83
    77, --	    BirdmanL88
    78, --	    HawkL93
    79, --	    BanditL98
    80, --	    TombBatL103
    81, --	    BloodyBatL108
    82, --	    BullMonsterL113
    83, --	    RedDevilL118
    84, --	    Banditti
    120, --     ElfApe
    121, --     SlowApe
    122, --     SnakeMonster
    1400, --	HugeSnake
    1401, --	BanditLeader
    1402, --	HugeSpirit
    1403, --	CateranSoldier
    1404, --	SeniorCateran
    1405, --	CateranLeader
    1406, --	ChiefCateran
    1407, --	HugeApe
    1408, --	SeniorApe
    1409, --	AlienApe
    1410, --	SeniorSnakeman
    1411, --	Serpent
    1412, --	SeniorSerpent
    1413, --	AlienSerpent
    1414, --	Basilisk
    2410, --	SerpentEnvoy
    2411, --	IcySerpent
    2412, --	SerpentLord
    2413, --	SerpentKing
    2414, --	FrostSerpent
    2415, --	BladeDevilEnvoy
    2416, --	IcyBladeDevil
    2417, --	BladeDevilLord
    2418, --	BladeDevilKing
    2419, --	FrostBladeDevil
    2460, --	DarkLady
    2461, --	DarkElf
    2465, --	NightmareLady
    2466, --	GrottoLady
    2473, --	SnakeChief
    2474, --	SnakeHubbub
    2478, --	HeadlessGeneral
    2479, --	HeadlessSoldier
}

local tCanyonMonsterDropLogFilename = "lua_monster_drop"

function Canyon_MonsterDropRegister()
    for index, value in pairs(tCanyonValidMonsterTypeIds) do
        tMonster[value] = tMonster[value] or {}
        tMonster[value]["tFunction"] = tMonster[value]["tFunction"] or {}
        table.insert(tMonster[value]["tFunction"],Canyon_MonstersHollowBeastDrop)
    end
end

function Canyon_MonsterDropContainsMonster(nMonsterType)
    for index, value in pairs(tCanyonValidMonsterTypeIds) do
        if value == nMonsterType then
            return true
        end
    end
    return false
end

function Canyon_MonsterDrop()
    local nMonsterId = Get_MonsterType()
    local nUserId = Get_UserId()

    if type(nMonsterId) ~= "number" or nMonsterId <= 0 then
        Sys_SaveAbnormalLog("Canyon_MonsterDrop called by a non monster")
        return
    end

    if not Canyon_MonsterDropContainsMonster(nMonsterId) then
        return
    end

    for i, v in pairs(tCanyonValidMonsterDrops) do
        if Sys_Random(v["MinRate"], v["MaxRate"]) then
            if v["Child"] ~= nil then
                if v["Rate"] == nil or type(v["Rate"]) ~= "number" or v["Rate"] < 2 then
                    Sys_SaveAbnormalLog(string.format("Canyon_MonsterDrop invalid rate [%s] for child [%d]!", v["Rate"], i))
                else
                    local nLocalRate = math.random(1, v["Rate"])
                    for localIndex, localValue in pairs(v["Child"]) do
                        if nLocalRate < localValue["Rate"] then
                            if Canyon_MonsterDropExecute(localValue, nUserId) then
                                if v["Effect"] ~= nil then
                                    User_EffectAdd("self", v["Effect"])
                                end
                                if localValue["Effect"] ~= nil then
                                    User_EffectAdd("self", localValue["Effect"])
                                end
                                if localValue["Message"] ~= nil then
                                    User_TalkChannel2005(localValue["Message"])
                                end
                                if localValue["ActionLog"] ~= nil then
                                    Sys_SaveActionParamLog(tCanyonMonsterDropLogFilename, string.format(localValue["ActionLog"], Get_MonsterType(0), Get_MonsterMapID(0)))
                                end
                            end
                            return
                        end -- leave Canyon_MonsterDropExecute(localValue)
                    end -- leave local for
                end -- end child drop rate
            else
                if Canyon_MonsterDropExecute(v, nUserId) == true then
                    if v["Effect"] ~= nil then
                        User_EffectAdd("self", v["Effect"])
                    end
                    if v["Message"] ~= nil then
                        User_TalkChannel2005(v["Message"])
                    end
                    if v["ActionLog"] ~= nil then
                        Sys_SaveActionParamLog(tCanyonMonsterDropLogFilename, string.format(v["ActionLog"], Get_MonsterType(0), Get_MonsterMapID(0)))
                    end
                    return
                end -- leave Canyon_MonsterDropExecute
            end -- leave child if
        end -- leave sys random if
    end -- leave for
end -- end function

function Canyon_MonsterDropExecute(v, nUserId)
    if v["AutoHunt"] ~= nil and type(v["AutoHunt"]) == "boolean" then
        local isAutoHunting = IsAutoHangUp(0)
        if v["AutoHunt"] == false and isAutoHunting == true then
            return false
        end
    end

    if v["Money"] ~= nil and type(v["Money"] == "number" and v["Money"] > 0) then
        User_AddMoney(v["Money"])
        return true
    end

    if v["ConquerPoints"] ~= nil and type(v["ConquerPoints"] == "number" and v["ConquerPoints"] > 0) then
        User_AddEMoney(v["ConquerPoints"])
        return true
    end

    if v["ConquerPointsMono"] ~= nil and type(v["ConquerPointsMono"] == "number" and v["ConquerPointsMono"] > 0) then
        User_AddEMoneyMono(v["ConquerPointsMono"])
        return true
    end

    if v["ItemType"] ~= nil and type(v["ItemType"]) == "number" and v["ItemType"] > 0 then
        if v["ItemTypeMode"] == nil or v["ItemTypeMode"] == "drop" then
            Monster_SysDropItem(v["ItemType"], nUserId)
            return true
        else
            if nUserId ~= nil and type(nUserId) == "number" and nUserId > 0 then
                Item_AddNewItem(v["ItemType"], "", nUserId)
                return true
            end
        end
    end
    return false
end

tServerStart["tFunction"] = tServerStart["tFunction"] or {}
table.insert(tServerStart["tFunction"],Canyon_MonsterDropRegister)
