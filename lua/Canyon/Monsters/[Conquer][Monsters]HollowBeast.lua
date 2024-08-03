------------------------------------------------------------------------------------
--Name:		[Conquer][Monsters]HollowBeast
--Purpose:	Custom rewards for boss.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/08/21
------------------------------------------------------------------------------------

function Canyon_MonstersHollowBeastDrop()

    local nUserId = Get_UserId()

    -- drop dragon balls
    local nRate = math.random(1,5)
    for i=1,nRate,1 do
        Monster_SysDropItem(1088000, nUserId)
        Sys_SaveActionParamLog("bossfight", string.format("1088000,4211,%d", i))
    end

    -- drop money
    for i=1,5,1 do
        Monster_SysDropItem(3006597, nUserId)
        Sys_SaveActionParamLog("bossfight", string.format("3006597,4211,%d", i)) -- 3006597	MagicSpar(2Million)
    end

end

tMonster[4211] = tMonster[4211] or {}
tMonster[4211]["tFunction"] = tMonster[4211]["tFunction"] or {}
table.insert(tMonster[4211]["tFunction"],Canyon_MonstersHollowBeastDrop)