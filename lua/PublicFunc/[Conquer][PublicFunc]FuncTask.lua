----------------------------------------------------------------------------
--Name:		[征服][公用函数]任务函数.lua
--Purpose:	任务函数接口
--Creator: 	林锦
--Created:	2014/06/26
----------------------------------------------------------------------------
---------------------------------命名规范----------------------------------
------------------------------actiontype的分类------------------------------
--Sys  任务所有
--Send 消息发送
--Get  获得属性
--Set  修改属性
--Chk  检查属性
--Del  删除属性
--Add  添加属性
------------------------------------------------------------------------------
-- 任务函数命名前缀词：Task_
--例子：
--// 判断任务是否存在. 参数说明: idUser表示用户ID, idTask表示任务ID. 如果不存在返回false, 存在返回true.
--bool IsExistTaskDetail(int idUser, int idTask);

--function Task_ChkTaskDetailExist(nUserId,nTsakid)
--
--end

------------------------------------------------------------------------------

--设置用户统计数据
--nUserId 玩家id 默认为0
--nType 目前只能为1
--nSubType 目前只能为1-3
--nData 设置的值 大于等于0
--nSave 是否即时写库，0或1
--返回设置成功或失败 bool型
function Task_SetStatisticDaily(nType,nSubType,nData,nSave,nUserId)
	if nType ~= 1 then
		Sys_SaveAbnormalLog("函数 Task_SetStatisticDaily 第一个参数nType只能为 1")
		return
	end
	
	if nSubType ~= 1 and nSubType ~= 2 and nSubType ~= 3 then
		Sys_SaveAbnormalLog("函数 Task_SetStatisticDaily 第二个参数nSubType只能为 1-3")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStatisticDaily 第三个参数nData只能大于等于 0")
		return
	end
	
	if nSave ~= 0 and nSave ~= 1 then
		Sys_SaveAbnormalLog("函数 Task_SetStatisticDaily 第四个参数nSave只能为 0-1")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStatisticDaily 第五个参数nUserId为整型并且大于等于0")
		return
	end
	
	return SetUserStatisticDaily(nUserId,nType,nSubType,nData,nSave);
end

--删除用户统计数据
--nUserId 玩家id 默认为0
--nType 目前只能为1
--nSubType 目前只能为1-3
--返回删除成功或失败 bool型
function Task_DelStatisticDaily(nType,nSubType,nUserId)
	if nType ~= 1 then
		Sys_SaveAbnormalLog("函数 Task_DelStatisticDaily 第一个参数只能为 1")
		return
	end
	
	if nSubType ~= 1 and nSubType ~= 2 and nSubType ~= 3 then
		Sys_SaveAbnormalLog("函数 Task_DelStatisticDaily 第二个参数只能为 1-3")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_DelStatisticDaily 第三个参数nUserId为整型并且大于等于0")
		return
	end
	
	return DeleteUserStatisticDaily(nUserId,nType,nSubType);
end

--检查用户统计数据是否存在
--nUserId 玩家id 默认为0
--nType 目前只能为1
--nSubType 目前只能为1-3
--返回检查成功或失败 bool型
function Task_ChkStatisticDaily(nType,nSubType,nUserId)
	if nType ~= 1 then
		Sys_SaveAbnormalLog("函数 Task_ChkStatisticDaily 第一个参数nType只能为 1")
		return
	end
	
	if nSubType ~= 1 and nSubType ~= 2 and nSubType ~= 3 then
		Sys_SaveAbnormalLog("函数 Task_ChkStatisticDaily 第二个参数nSubType只能为 1-3")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_ChkStatisticDaily 第三个参数nUserId为整型并且大于等于0")
		return
	end
	
	return IsexitUserStatisticDaily(nType,nSubType,nUserId);
end

--设置用户stc掩码
--nUserId 玩家id 默认为0
--nEvent 事件id,大于0
--nType Type,大于0
--nData 设置的值,大于等于0
--nSave 是否即时写库，0或1
--返回设置用户stc掩码成功或失败 bool型
function Task_SetStatistic(nEvent,nType,nData,nSave,nUserId)
	if type(nEvent) ~= "number" or nEvent%1 ~= 0 or nEvent <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStatistic 第一个参数nEvent为整型并且大于0")
		return
	end
	
	if type(nType) ~= "number" or nType%1 ~= 0 or nType < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStatistic 第二个参数nType为整型并且大于等于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStatistic 第三个参数nData为整型并且大于等于0")
		return
	end
	
	if nSave ~= 0 and nSave ~= 1 then
		Sys_SaveAbnormalLog("函数 Task_SetStatistic 第四个参数nSave只能为 0-1")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStatistic 第五个参数nUserId为整型并且大于等于0")
		return
	end
	
	return SetUserStatistic(nUserId,nEvent,nType,nData,nSave);
end

--设置用户stc掩码时间戳
--nUserId 玩家id 默认为0
--nEvent 事件id,大于0
--nType Type,大于0
--nData 时间戳,大于等于0
--返回设置用户stc掩码时间戳成功或失败 bool型
function Task_SetStcTimestamp(nEvent,nType,nData,nUserId)
	if type(nEvent) ~= "number" or nEvent%1 ~= 0 or nEvent <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStcTimestamp 第一个参数nEvent为整型并且大于0")
		return
	end
	
	if type(nType) ~= "number" or nType%1 ~= 0 or nType < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStcTimestamp 第二个参数nType为整型并且大于等于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStcTimestamp 第三个参数nData为整型并且大于等于0")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetStcTimestamp 第四个参数nUserId为整型并且大于等于0")
		return
	end
	return SetUserStcTimestamp(nUserId,nEvent,nType,nData);
end

--删除stc用户掩码
--nUserId 玩家id 默认为0
--nEvent 事件id,大于0
--nType Type,大于0
--返回删除用户stc掩码成功或失败 bool型
function Task_DelStatistic(nEvent,nType,nUserId)
	if type(nEvent) ~= "number" or nEvent%1 ~= 0 or nEvent <= 0 then
		Sys_SaveAbnormalLog("函数 Task_DelStatistic 第一个参数nEvent为整型并且大于0")
		return
	end
	
	if type(nType) ~= "number" or nType%1 ~= 0 or nType < 0 then
		Sys_SaveAbnormalLog("函数 Task_DelStatistic 第二个参数nType为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_DelStatistic 第三个参数nUserId为整型并且大于等于0")
		return
	end

	return DeleteUserStatistic(nUserId,nEvent,nType);
end

--检查用户stc掩码是否存在
--nUserId 玩家id 默认为0
--nEvent 事件id,大于0
--nType Type,大于0
--返回检查用户stc掩码是否存在成功或失败 bool型
function Task_ChkStatistic(nEvent,nType,nUserId)
	if type(nEvent) ~= "number" or nEvent%1 ~= 0 or nEvent <= 0 then
		Sys_SaveAbnormalLog("函数 Task_ChkStatistic 第一个参数nEvent为整型并且大于0")
		return
	end
	
	if type(nType) ~= "number" or nType%1 ~= 0 or nType < 0 then
		Sys_SaveAbnormalLog("函数 Task_ChkStatistic 第二个参数nType为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_ChkStatistic 第三个参数nUserId为整型并且大于等于0")
		return
	end
	
	return IsexitUserStatistic(nUserId,nEvent,nType);
end

--设置用户任务掩码对应字段值
--索引值对照表如下：
--task_detail表
--SCRIPT_PARAM_TASKDETAIL_ID				= 2251,（不能设置）
--SCRIPT_PARAM_TASKDETAIL_Task_ID			= 2252,（不能设置）
--SCRIPT_PARAM_TASKDETAIL_TASK_ID			= 2253,（不能设置）
--SCRIPT_PARAM_TASKDETAIL_COMPLETE_FLAG	= 2254,
--SCRIPT_PARAM_TASKDETAIL_NOTIFY_FLAG		= 2255,
--SCRIPT_PARAM_TASKDETAIL_DATA1			= 2256,
--SCRIPT_PARAM_TASKDETAIL_DATA2			= 2257,
--SCRIPT_PARAM_TASKDETAIL_DATA3			= 2258,
--SCRIPT_PARAM_TASKDETAIL_DATA4			= 2259,
--SCRIPT_PARAM_TASKDETAIL_DATA5			= 2260,
--SCRIPT_PARAM_TASKDETAIL_DATA6			= 2261,
--SCRIPT_PARAM_TASKDETAIL_DATA7			= 2262,
--SCRIPT_PARAM_TASKDETAIL_TASK_OVERTIME	= 2263,
--SCRIPT_PARAM_TASKDETAIL_TYPE			= 2264,（不能设置）
--以下针对各个索引进行封装

--设置用户任务掩码CompleteFlag
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailCompleteFlag(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailCompleteFlag 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailCompleteFlag 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailCompleteFlag 第三个参数nUserId为整型并且大于等于0")
		return
	end
	
	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_COMPLETE_FLAG,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailCompleteFlag 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码NotifyFlag
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailNotifyFlag(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailNotifyFlag 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailNotifyFlag 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailNotifyFlag 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_NOTIFY_FLAG,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailNotifyFlag 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Data1
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailData1(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData1 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData1 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData1 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_DATA1,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailData1 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Data2
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailData2(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData2 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData2 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData2 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_DATA2,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailData2 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Data3
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailData3(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData3 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData3 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData3 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_DATA3,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailData3 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Data4
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailData4(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData4 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData4 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData4 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_DATA4,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailData4 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Data5
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailData5(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData5 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData5 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData5 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_DATA5,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailData5 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Data6
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailData6(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData6 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData6 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData6 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_DATA6,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailData6 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Data7
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailData7(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData7 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData7 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailData7 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_DATA7,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailData7 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--设置用户任务掩码Overtime
--nTaskId Taskid,大于0
--nData 设置的值,大于等于0
--nUserId 玩家id 默认为0
--返回设置成功或失败 bool型
function Task_SetTaskDetailTaskOvertime(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailTaskOvertime 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailTaskOvertime 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetTaskDetailTaskOvertime 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_TASKDETAIL_TASK_OVERTIME,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetTaskDetailTaskOvertime 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end


--Task_detail 时间间隔（天、秒）判断函数

--判断Task_Detai掩码时间戳是否超过多少时间
--nQuestId掩码id
--nDelay，延时时间
--nTimeType，延时类型，默认0。0:秒；1:分钟；2：小时；3，天（绝对时间）;4,天（相对时间）。
--超过，返回ture
--未超过，返回false
function Task_DetailInterval(nQuestId,nDelay,nTimeType,nUserId)
	local nTaskTime = Get_TaskDetailData7(nQuestId,nUserId)
	local nNow = os.time()
--nTaskTime返回nil，表示传入参数错误。报log。
	if nTaskTime == nil then
		Sys_SaveAbnormalLog("函数 Task_DetailInterval 传入的参数:["..nQuestId.."]格式错误。玩家Id："..Get_UserId())
		return false
	end
--返回0，表示玩家该掩码未使用过，或者未有时间戳操作，直接返回true
	if nTaskTime == 0 then
		return true
	end

	if type(nDelay) ~= "number" or nDelay%1 ~= 0 or nDelay <= 0 then
		Sys_SaveAbnormalLog("函数 Task_DetailInterval 第三个参数 nDelay 为整型并且大于0")
		return
	end

	if nTimeType == nil then
		nTimeType = 0
	elseif type(nTimeType) ~= "number" or nTimeType < 0 or nTimeType > 4 or nTimeType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Task_DetailInterval 中 nTimeType 只能传0~3的整数")
		return
	end
--秒，直接减
	if nTimeType == 0 then
		return nNow - nTaskTime >= nDelay
	end
--分钟，先除以60
	if nTimeType == 1 then
		return (nNow - nTaskTime)/60 >= nDelay
	end
--小时，先除3600=60*60
	if nTimeType == 2 then
		return (nNow - nTaskTime)/3600 >= nDelay
	end
--天，先除86400=60*60*24 (绝对时间)
	if nTimeType == 3 then
		return (nNow - nTaskTime)/86400 >= nDelay
	end
--天(相对时间)
	if nTimeType == 4 then
		local sYear
		local sMonth
		local sDay
		local sNowYear
		local sNowMonth
		local sNowDay
		local sNowTime
		local sTime
		sNowTime = os.date("%Y%m%d",nNow - (nDelay - 1)*86400)
		sTime = os.date("%Y%m%d",nTaskTime)
		
		sYear = string.sub(sTime,1,4)
		sMonth = string.sub(sTime,5,6)
		sDay = string.sub(sTime,7,8)
		sNowYear = string.sub(sNowTime,1,4)
		sNowMonth = string.sub(sNowTime,5,6)
		sNowDay = string.sub(sNowTime,7,8)
		
		if tonumber(sYear) < tonumber(sNowYear) then
			return true
		elseif tonumber(sYear) > tonumber(sNowYear) then
			return false
		elseif tonumber(sMonth) < tonumber(sNowMonth) then
			return true
		elseif tonumber(sMonth) > tonumber(sNowMonth) then
			return false
		elseif tonumber(sDay) < tonumber(sNowDay) then
			return true
		else
			return false
		end
	end
end


--新增用户任务掩码
--nUserId 玩家id 默认为0
--nTaskId Taskid,大于0
--nLimitTime 限时时间，单位：秒。0表示非限时。
--返回新增用户任务掩码成功或失败 bool型
function Task_AddTaskDetail(nTaskId,nLimitTime,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_AddTaskDetail 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if nLimitTime == nil then
		nLimitTime = 0
	elseif type(nLimitTime) ~= "number" or nLimitTime%1 ~= 0 or nLimitTime < 0 then
		Sys_SaveAbnormalLog("函数 Task_AddTaskDetail 第二个参数nLimitTime为整型并且>=0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_AddTaskDetail 第三个参数nUserId为整型并且大于等于0")
		return
	end
	
	if Task_ChkTaskDetail(nTaskId,nUserId) then
		Sys_SaveAbnormalLog(string.format("函数 Task_AddTaskDetail 中玩家身上已有任务ID为%d的掩码",nTaskId))
		return
	else
		return AddTaskDetail(nUserId,nTaskId,nLimitTime)
	end
end

--删除用户任务掩码
--nUserId 玩家id 默认为0
--nTaskId Taskid,大于0
--返回删除用户任务掩码成功或失败 bool型
function Task_DelTaskDetail(nTaskId,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_DelTaskDetail 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_DelTaskDetail 第二个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return DeleteTaskDetail(nUserId,nTaskId)
	else
		Sys_SaveAbnormalLog(string.format("函数 DeleteTaskDetail 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end

--检查用户任务掩码是否存在
--nUserId 玩家id 默认为0
--nTaskId Taskid,大于0
--返回检查用户任务掩码是否存在成功或失败 bool型
function Task_ChkTaskDetail(nTaskId,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_ChkTaskDetail 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_ChkTaskDetail 第二个参数nUserId为整型并且大于等于0")
		return
	end
	
	return IsExistTaskDetail(nUserId,nTaskId);
end


--判断stc掩码时间戳是否超过多少时间
--nEvent,nType,stc掩码id
--nDelay，延时时间
--nTimeType，延时类型，默认0。0:秒；1:分钟；2：小时；3，天（绝对时间）;4,天（相对时间）。
--超过，返回ture
--未超过，返回false
function Task_StcInterval(nEvent,nType,nDelay,nTimeType)
	local nStcTime = Get_UserStcTimestampValue(nEvent,nType)
	local nNow = os.time()
--nStcTime返回nil，表示传入参数错误。报log。
	if nStcTime == nil then
		Sys_SaveAbnormalLog("函数 Task_StcInterval 传入的参数[nEvent,nType]:["..nEvent..","..nType.."]格式错误。玩家Id："..Get_UserId())
		return false
	end
--返回0，表示玩家该掩码未使用过，或者未有时间戳操作，直接返回true
	if nStcTime == 0 then
		return true
	end

	if type(nDelay) ~= "number" or nDelay%1 ~= 0 or nDelay <= 0 then
		Sys_SaveAbnormalLog("函数 Task_StcInterval 第三个参数 nDelay 为整型并且大于0")
		return
	end

	if nTimeType == nil then
		nTimeType = 0
	elseif type(nTimeType) ~= "number" or nTimeType < 0 or nTimeType > 4 or nTimeType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Task_StcInterval 中 nTimeType 只能传0~3的整数")
		return
	end
--秒，直接减
	if nTimeType == 0 then
		return nNow - nStcTime >= nDelay
	end
--分钟，先除以60
	if nTimeType == 1 then
		return (nNow - nStcTime)/60 >= nDelay
	end
--小时，先除3600=60*60
	if nTimeType == 2 then
		return (nNow - nStcTime)/3600 >= nDelay
	end
--天，先除86400=60*60*24 (绝对时间)
	if nTimeType == 3 then
		return (nNow - nStcTime)/86400 >= nDelay
	end
--天(相对时间)
	if nTimeType == 4 then
		local sYear
		local sMonth
		local sDay
		local sNowYear
		local sNowMonth
		local sNowDay
		local sNowTime
		local sTime
		sNowTime = os.date("%Y%m%d",nNow - (nDelay - 1)*86400)
		sTime = os.date("%Y%m%d",nStcTime)
		sYear = string.sub(sTime,1,4)
		sMonth = string.sub(sTime,5,6)
		sDay = string.sub(sTime,7,8)
		sNowYear = string.sub(sNowTime,1,4)
		sNowMonth = string.sub(sNowTime,5,6)
		sNowDay = string.sub(sNowTime,7,8)
		
		if tonumber(sYear) < tonumber(sNowYear) then
			return true
		elseif tonumber(sYear) > tonumber(sNowYear) then
			return false
		elseif tonumber(sMonth) < tonumber(sNowMonth) then
			return true
		elseif tonumber(sMonth) > tonumber(sNowMonth) then
			return false
		elseif tonumber(sDay) < tonumber(sNowDay) then
			return true
		else
			return false
		end
	end
end

--对用户stc掩码进行加减
--nUserId 玩家id 默认为0
--nEvent 事件id,大于0
--nType Type,大于0
--nData 加减的值(减的话，这个值传负数进来就可以了)
--nSave 是否即时写库，0或1
--返回设置用户stc掩码成功或失败 bool型
function Task_AddStatistic(nEvent,nType,nData,nSave,nUserId)
	local nValue = Get_UserStatisticValue(nEvent,nType,nUserId)
	
	if nValue == nil then
		Sys_SaveAbnormalLog("函数 Task_AddStatistic 传入的参数[nEvent,nType]:["..nEvent..","..nType.."]格式错误。玩家Id："..Get_UserId())
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Task_AddStatistic 中 nData 只能传整数")
		return
	end
	
	local sNewValue = nValue + nData
	local sReturnValue = Task_SetStatistic(nEvent,nType,sNewValue,nSave,nUserId)
	
	if sReturnValue == nil then
		Sys_SaveAbnormalLog("函数 Task_AddStatistic 传入的参数[nnSave]:["..nSave.."]格式错误。玩家Id："..Get_UserId())
		return false
	else
		return true
	end
end

--对用户stc掩码进行判断
--nUserId 玩家id 默认为0
--nEvent 事件id,大于0
--nType Type,大于0
-- sOpt 操作符 只能传 ">=",">","<=","<","==","~=" 这几个操作符
-- nData 判断的值
function Task_ChkStcValue(nEvent,nType,sOpt,nData,nUserId)
	local nValue = Get_UserStatisticValue(nEvent,nType,nUserId)
	
	if nValue == nil then
		Sys_SaveAbnormalLog("函数 Task_ChkStcValue 传入的参数[nEvent,nType]:["..nEvent..","..nType.."]格式错误。玩家Id："..Get_UserId())
		return
	end
	
	if sOpt == nil or type(sOpt) ~= "string" or (sOpt ~= ">=" and sOpt ~= ">" and sOpt ~= "<=" and sOpt ~= "<" and sOpt ~= "==" and sOpt ~= "~=") then
		Sys_SaveAbnormalLog("函数 Task_ChkStcValue 中 sOpt 传入的格式有错")
		return
	end
	
	if nData == nil or type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Task_ChkStcValue 中 nData 只能传数字")
		return
	end
	
	if sOpt == ">=" then
		return nValue >= nData
	end
	
	if sOpt == ">" then
		return nValue > nData
	end
	
	if sOpt == "<=" then
		return nValue <= nData
	end
	
	if sOpt == "<" then
		return nValue < nData
	end
	
	if sOpt == "==" then
		return nValue == nData
	end
	
	if sOpt == "~=" then
		return nValue ~= nData
	end
end

-- 判断用户任务掩码值
--nUserId 玩家id 默认为0
-- nQuestId 任务掩码ID
-- sPos 对应的任务字段 只能传 "CompleteFlag"，"NotifyFlag"，"1"，"2"，"3"，"4"，"5"，"6"，"7"，"OverTime"，"OverTimeSec"
-- sOpt 操作符 只能传 ">=",">","<=","<","==","~=" 这几个操作符
-- nData 判断的值
function Task_ChkTaskDetailValue(nQuestId,sPos,sOpt,nData,nUserId)
	local nValue = Get_TaskDetail(nQuestId,sPos,nUserId)
	
	if nValue == nil then
		Sys_SaveAbnormalLog("函数 Task_ChkTaskDetailValue 传入的参数[nQuestId,sPos]:["..nQuestId..","..sPos .."]格式错误。玩家Id："..Get_UserId())
		return
	end
	
	if sOpt == nil or type(sOpt) ~= "string" or (sOpt ~= ">=" and sOpt ~= ">" and sOpt ~= "<=" and sOpt ~= "<" and sOpt ~= "==" and sOpt ~= "~=") then
		Sys_SaveAbnormalLog("函数 Task_ChkTaskDetailValue 中 sOpt 传入的格式有错")
		return
	end
	
	if nData == nil or type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Task_ChkTaskDetailValue 中 nData 只能传数字")
		return
	end
	
	if sOpt == ">=" then
		return nValue >= nData
	end
	
	if sOpt == ">" then
		return nValue > nData
	end
	
	if sOpt == "<=" then
		return nValue <= nData
	end
	
	if sOpt == "<" then
		return nValue < nData
	end
	
	if sOpt == "==" then
		return nValue == nData
	end
	
	if sOpt == "~=" then
		return nValue ~= nData
	end
end

--2015.01.08
--新添加弹出玩家跨服任务累积界面

--	// 弹出任务累积的界面，成功返回true，否则返回false
--	// 参数1: 玩家ID,  参数2: 任务ID,  

function Task_OpenTaskAccumulateWindow(nTaskId,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_CompleteOSTaskAmount 中 nUserId 只能传大于等于0的整数")
		return
	end

	if type(nTaskId) ~= "number" or nTaskId <= 0 or nTaskId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_CompleteOSTaskAmount 中 nTaskId 只能传大于0的整数")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return OpenTaskAccumulateWindow(nUserId,nTaskId)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_OpenTaskAccumulateWindow 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end


-- 2015.01.09
-- 设置玩家task_detail中max_accumulate_times 值
function Task_SetMaxAccumulateTimes(nTaskId,nData,nUserId)
	if type(nTaskId) ~= "number" or nTaskId%1 ~= 0 or nTaskId <= 0 then
		Sys_SaveAbnormalLog("函数 Task_SetMaxAccumulateTimes 第一个参数nTaskId为整型并且大于0")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetMaxAccumulateTimes 第二个参数nData为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Task_SetMaxAccumulateTimes 第三个参数nUserId为整型并且大于等于0")
		return
	end

	if Task_ChkTaskDetail(nTaskId,nUserId) then
		return SetTaskDetailData(nUserId,nTaskId,G_MAX_ACCUMULATE_TIMES,nData)
	else
		Sys_SaveAbnormalLog(string.format("函数 Task_SetMaxAccumulateTimes 中玩家身上没有任务ID为%d的掩码",nTaskId))
		return
	end
end
