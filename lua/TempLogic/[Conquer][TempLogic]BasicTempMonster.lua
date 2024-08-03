----------------------------------------------------------------------------
--Name:		[征服][模板逻辑]基础模板之怪物模块.lua
--Purpose:	基础模板之怪物模块
--Creator: 	郑江文
--Created:	2014/12/10
----------------------------------------------------------------------------

--任务怪物触发Action接口函数(action.id=94416550)，实时触发。（适合用来做怪物掉落）
function LinkMonsterMain()
	local nMonsterTypeId = Get_MonsterType()

	-- 判断是否有取到怪物ID
	if nMonsterTypeId == 0 or nMonsterTypeId == nil then
		Sys_SaveAbnormalLog("nMonsterTypeId的值没有取到或者为0")
		return
	end

	if type(tMonsterFunction) == "table" then
		for _,func in ipairs(tMonsterFunction) do
			if func ~= nil and type(func) == "function" then
				func(nMonsterTypeId)
			end
		end
	end

	--怪物表是否有值
	if tMonster[nMonsterTypeId] == nil then
		return
	end
	--功能怪
	if type(tMonster[nMonsterTypeId]["tFunction"]) == "table" then
		for _,func in ipairs(tMonster[nMonsterTypeId]["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nMonsterTypeId)
			end
		end
	end
end

--例：
--tMonster[5830] = tMonster[5830] or {}
--tMonster[5830]["tFunction"] = tMonster[5830]["tFunction"] or {}
--table.insert(tMonster[5830]["tFunction"],RandMission_55136_Monster_Killed)
