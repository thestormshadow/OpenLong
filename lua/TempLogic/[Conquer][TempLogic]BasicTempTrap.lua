----------------------------------------------------------------------------
--Name:		[征服][模板逻辑]基础模板之陷阱模块.lua
--Purpose:	基础模板之陷阱模块
--Creator: 	郑江文
--Created:	2014/12/10
----------------------------------------------------------------------------

--陷阱接口函数(Action_id=94427500)
function LinkTrapMain()
	local nTrapId = Get_TrapID()
	local nTrapType = Get_TrapType()

	--陷阱表是否有值
	if tTrap[nTrapType] == nil then
		return
	end
	--功能陷阱触发函数
	if type(tTrap[nTrapType]["Function"]) == "function" then
		tTrap[nTrapType]["Function"](nTrapId,nTrapType)
	end
	--任务陷阱触发函数
	--for i,v in ipairs(tTrap[nTrapId]) do
	--	if tQuestTemplate[v] ~= nil and PlayerInQuest(v) and type(tQuestTemplate[v]["TrapOnQuest"]) == "function" then
	--		tQuestTemplate[v]:TrapOnQuest(v,nTrapId)
	--	end
	--end
end
