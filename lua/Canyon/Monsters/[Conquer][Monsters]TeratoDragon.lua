------------------------------------------------------------------------------------
--Name:		[Conquer][Monsters]TeratoDragon
--Purpose:	Custom rewards for boss.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/08/21
------------------------------------------------------------------------------------

function Canyon_MonstersTeratoDragonDrop()

    local nUserId = Get_UserId()

    -- drop dragon balls
    local nRate = math.random(1,2)
    for i=1,nRate,1 do
        Monster_SysDropItem(1088000, nUserId)
        Sys_SaveActionParamLog("bossfight", string.format("1088000,4152,%d", i))
    end

    -- drop money
    Monster_SysDropItem(3006594, nUserId) -- 3006594	MagicSpar(200Thousand)
    Sys_SaveActionParamLog("bossfight", "3006594,4152")

end

tMonster[4152] = tMonster[4152] or {}
tMonster[4152]["tFunction"] = tMonster[4152]["tFunction"] or {}
table.insert(tMonster[4152]["tFunction"],Canyon_MonstersTeratoDragonDrop)