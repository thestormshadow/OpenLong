------------------------------------------------------------------------------------
--- Name:		[Canyon][ActiveScript]The curse of the ghost lake
--- Creator: 	Felipe Vieira Vendramini
--- Created:	2023/09/06
------------------------------------------------------------------------------------

--- The curse of the ghost lake
--- A mysterious, thick fog has begun to spread across the Valley of Spirits, and the locals believe it to be
--- a sign of the terrible Curse of the Ghost Lake, which happens only once a century, coinciding with the Lantern
--- Festival. Players are summoned by the Elder of the Village to investigate the source of this ominous mist and
--- stop the curse from spreading further.
---
--- Maps
--- 1015 - BirdIsland 1015
--- 1553 - BirdVillage 1507
---
--- Instances
--- 1000 - ValleyOfSpirits
---
--- NPCs
--- 3458 - Derek - BirdVillage (88,113)
--- 3459 - Blair - BirdVillage (138,83)
--- 3460 - Marsa - BirdVillage (128,103)
--- 3461 - FengYan - BirdVillage (136,129)
--- 3462 - XiaoYing - BirdVillage (78,134)
--- 3463 - WiseElder - ValleyOfSpirits(109,56)
--- 3464 - Ghost - ValleyOfSpirits(127,148)
--- 3465 - Ghost - ValleyOfSpirits(127,113)
--- 3466 - Ghost - ValleyOfSpirits(84,122)
--- 3467 - Ghost - ValleyOfSpirits(31,69)
--- 3468 - Ghost - ValleyOfSpirits(132,77)
--- 3469 - Lantern - ValleyOfSpirits(84,62)
--- 3470 - Lantern - BirdIsland(770,549)
---

--local tCurseOfTheGhostLake_EventTime = "2023-10-26 00:00 2023-11-01 23:59"
-- The date NPCs must enter the map or leave the map
local tCurseOfTheGhostLake_NpcEventTime = "2023-06-26 00:00 2023-11-05 23:59"
local tCurseOfTheGhostLake_PreviousTime = "2023-06-26 00:00 2023-10-25 23:59"
local tCurseOfTheGhostLake_AfterTime = "2023-11-02 00:00 2023-11-05 23:59"
-- The event period
local tCurseOfTheGhostLake_EventTime = "2023-10-26 00:00 2023-11-01 23:59"

MoveNpc_NpcInfo[3470] = {}
MoveNpc_NpcInfo[3470]["ActivetyTime"] = tCurseOfTheGhostLake_NpcEventTime
MoveNpc_NpcInfo[3470]["NpcId"] = 3470
MoveNpc_NpcInfo[3470]["ActivetyMapId"] = 1015
MoveNpc_NpcInfo[3470]["ActivetyPosX"] = 770
MoveNpc_NpcInfo[3470]["ActivetyPosY"] = 549
MoveNpc_NpcInfo[3470]["AfterActivetyMapId"] = 5000
MoveNpc_NpcInfo[3470]["AfterActivetyPosX"] = 100
MoveNpc_NpcInfo[3470]["AfterActivetyPosY"] = 100

local tCurseOfTheGhostLake_Rewards = {}

local nCurseOfTheGhostLake_InstanceType = 1000

function CurseOfTheGhostLake_BirdIslandLanternEntrance()
    if Sys_ChkFullTime(tCurseOfTheGhostLake_EventTime) then
        LinkNpcGossipFunc_New(3470,"1-2")
    else
        if (Sys_ChkFullTime(tCurseOfTheGhostLake_PreviousTime)) then
            LinkNpcGossipFunc_New(3470,"1-1")
        else
            LinkNpcGossipFunc_New(3470,"1-3")
        end
    end
end

tNpcGossip[3470] = tNpcGossip[3470] or DefaultNpc:new{}
tNpcGossip[3470]["OptionHidden"] = 1

tNpcGossip[3470]["Text1-1"] = { 111 }
tNpcGossip[3470]["Text111"] = tCurseOfTheGhostLake_Text[3470]["Text111"]
tNpcGossip[3470]["tOption1-1"] = {1,2}
tNpcGossip[3470]["Option1"] = tCurseOfTheGhostLake_Text[3470]["Option1"]
tNpcGossip[3470]["Option2"] = tCurseOfTheGhostLake_Text[3470]["Option2"]

tNpcGossip[3470]["Text1-2"] = { 112 }
tNpcGossip[3470]["Text121"] = tCurseOfTheGhostLake_Text[3470]["Text111"]
tNpcGossip[3470]["tOption1-2"] = {3,1,4}
tNpcGossip[3470]["Option3"] = tCurseOfTheGhostLake_Text[3470]["Option3"]

tNpcGossip[3470]["Option4"] = tCurseOfTheGhostLake_Text[3470]["Option4"]

tNpcGossip[3470]["Text1-3"] = { 113 }
tNpcGossip[3470]["Text131"] = tCurseOfTheGhostLake_Text[3470]["Text111"]
tNpcGossip[3470]["tOption1-3"] = {5}
tNpcGossip[3470]["Option5"] = tCurseOfTheGhostLake_Text[3470]["Option5"]

