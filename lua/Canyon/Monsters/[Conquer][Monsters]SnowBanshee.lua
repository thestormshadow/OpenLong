------------------------------------------------------------------------------------
--Name:		[Conquer][Monsters]SnowBanshee
--Purpose:	Custom rewards for boss.
--Creator: 	Felipe Vieira Vendramini
--Created:	2023/08/21
------------------------------------------------------------------------------------

function Canyon_MonstersSnowBansheeDrop()

    local nUserId = Get_UserId()

    -- drop dragon balls
    local nRate = math.random(1,5)
    for i=1,nRate,1 do
        Monster_SysDropItem(1088000, nUserId)
        Sys_SaveActionParamLog("bossfight", string.format("1088000,4171,%d", i))
    end

    -- drop money
    nRate = math.random(100)
    if nRate < 50 then
        if nRate < 25 then
            Monster_SysDropItem(3006594, nUserId) -- 3006594	MagicSpar(200Thousand)
            Sys_SaveActionParamLog("bossfight", "3006594,4171")
        else
            Monster_SysDropItem(3006595, nUserId) -- 3006595	MagicSpar(500Thousand)
            Sys_SaveActionParamLog("bossfight", "3006595,4171")
        end
    end

end

tMonster[4171] = tMonster[4171] or {}
tMonster[4171]["tFunction"] = tMonster[4171]["tFunction"] or {}
table.insert(tMonster[4171]["tFunction"],Canyon_MonstersSnowBansheeDrop)

tMonster[4212] = tMonster[4212] or {}
tMonster[4212]["tFunction"] = tMonster[4212]["tFunction"] or {}
table.insert(tMonster[4212]["tFunction"],Canyon_MonstersSnowBansheeDrop)