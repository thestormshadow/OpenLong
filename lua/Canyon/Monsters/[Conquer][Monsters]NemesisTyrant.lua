------------------------------------------------------------------------------------
-- Name:		[Conquer][Monsters]NemesisTyrant
-- Purpose:	Custom rewards for boss.
-- Creator: 	Felipe Vieira Vendramini
-- Created:	2023/08/21
------------------------------------------------------------------------------------

function Canyon_MonstersNemesisTyrantDrop()

    local nUserId = Get_UserId()

    -- drop dragon balls
    local nRate = math.random(1,5)
    for i=1,nRate,1 do
        Monster_SysDropItem(1088000, nUserId)
        Sys_SaveActionParamLog("bossfight", string.format("1088000,4220,%d", i))
    end

    -- drop money
    nRate = math.random(100)
    if nRate < 50 then
        if nRate < 25 then
            Monster_SysDropItem(3006598, nUserId) -- 3006598	MagicSpar(2.5Million)
            Sys_SaveActionParamLog("bossfight", "3006598,4220")
        else
            Monster_SysDropItem(3006599, nUserId) -- 3006599	MagicSpar(5Million)
            Sys_SaveActionParamLog("bossfight", "3006599,4220")
        end
    end

end

tMonster[4220] = tMonster[4220] or {}
tMonster[4220]["tFunction"] = tMonster[4220]["tFunction"] or {}
table.insert(tMonster[4220]["tFunction"],Canyon_MonstersNemesisTyrantDrop)