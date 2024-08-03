----------------------------------------------------------------------------
--Name:		[征服][公用函数]取值函数.lua
--Purpose:	取值函数接口
--Creator: 	林锦
--Created:	2014/06/26
----------------------------------------------------------------------------
---------------------------------命名规范----------------------------------
------------------------------actiontype的分类------------------------------
--Get  获得属性
------------------------------取值对象的分类------------------------------
--Sys		获取系统属性
--User		获取玩家属性
--Npc		获取Npc属性
--Item		获取物品属性
--Monster	获取怪物属性
--Map		获取地图属性
--Trap		获取陷阱属性
--Task		获取任务掩码属性
----------------------------------------------------------------------------
-- 取值函数命名前缀词：Get_
--例子：
--获取玩家Id
--function Get_UserId(nUserId)
--
--end

------------------------------------------------------------------------------

------------------------------------------------------------------------------
-------------------------------------系统-------------------------------------
------------------------------------------------------------------------------

--cq_dyna_global_data 表属性data0
--SCRIPT_PARAM_DYNA_GLOBAL_DATA0 		= 2202;
--返回cq_dyna_global_data的data0
function Get_SysDynaGlobalData0(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData0 中 nSysDyGlobId 只能传大于0的整数")
		return
	end

	return GetSynaGlobalData(nSysDyGlobId,G_DYNA_GLOBAL_DATA0)
end

--cq_dyna_global_data 表属性data1
--SCRIPT_PARAM_DYNA_GLOBAL_DATA1 		= 2203;
--返回cq_dyna_global_data的data1
function Get_SysDynaGlobalData1(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData1 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalData(nSysDyGlobId,G_DYNA_GLOBAL_DATA1)
end

--cq_dyna_global_data 表属性data2
--SCRIPT_PARAM_DYNA_GLOBAL_DATA2		= 2204;
--返回cq_dyna_global_data的data2
function Get_SysDynaGlobalData2(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData2 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalData(nSysDyGlobId,G_DYNA_GLOBAL_DATA2)
end

--cq_dyna_global_data 表属性data3
--SCRIPT_PARAM_DYNA_GLOBAL_DATA3		= 2205;
--返回cq_dyna_global_data的data3
function Get_SysDynaGlobalData3(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData3 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalData(nSysDyGlobId,G_DYNA_GLOBAL_DATA3)
end

--cq_dyna_global_data 表属性data4
--SCRIPT_PARAM_DYNA_GLOBAL_DATA4		= 2206;
--返回cq_dyna_global_data的data4
function Get_SysDynaGlobalData4(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData4 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalData(nSysDyGlobId,G_DYNA_GLOBAL_DATA4)
end

--cq_dyna_global_data 表属性data5
--SCRIPT_PARAM_DYNA_GLOBAL_DATA5		= 2207;
--返回cq_dyna_global_data的data5
function Get_SysDynaGlobalData5(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData5 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalData(nSysDyGlobId,G_DYNA_GLOBAL_DATA5)
end

--cq_dyna_global_data 表属性datastr0
--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR0	= 2208;
--返回cq_dyna_global_data的datastr0
function Get_SysDynaGlobalDataStr0(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr0 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalDataStr(nSysDyGlobId,G_DYNA_GLOBAL_DATASTR0)
end

--cq_dyna_global_data 表属性datastr1
--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR1	= 2209;
--返回cq_dyna_global_data的datastr1
function Get_SysDynaGlobalDataStr1(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr1 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalDataStr(nSysDyGlobId,G_DYNA_GLOBAL_DATASTR1)
end

--cq_dyna_global_data 表属性datastr2
--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR2	= 2210;
--返回cq_dyna_global_data的datastr2
function Get_SysDynaGlobalDataStr2(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr2 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalDataStr(nSysDyGlobId,G_DYNA_GLOBAL_DATASTR2)
end

--cq_dyna_global_data 表属性datastr3
-- SCRIPT_PARAM_DYNA_GLOBAL_DATASTR3	= 2211;
--返回cq_dyna_global_data的datastr3
function Get_SysDynaGlobalDataStr3(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr3 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalDataStr(nSysDyGlobId,G_DYNA_GLOBAL_DATASTR3)
end

--cq_dyna_global_data 表属性datastr4
-- SCRIPT_PARAM_DYNA_GLOBAL_DATASTR4	= 2212;
--返回cq_dyna_global_data的datastr4
function Get_SysDynaGlobalDataStr4(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr4 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalDataStr(nSysDyGlobId,G_DYNA_GLOBAL_DATASTR4)
end

--cq_dyna_global_data 表属性datastr5
-- SCRIPT_PARAM_DYNA_GLOBAL_DATASTR5	= 2213;
--返回cq_dyna_global_data的datastr5
function Get_SysDynaGlobalDataStr5(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr5 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalDataStr(nSysDyGlobId,G_DYNA_GLOBAL_DATASTR5)
end

--cq_dyna_global_data 表属性TIME0
-- SCRIPT_PARAM_DYNA_GLOBAL_TIME0		= 2214;
--返回cq_dyna_global_data的TIME0
function Get_SysDynaGlobalTime0(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime0 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalTime(nSysDyGlobId,G_DYNA_GLOBAL_TIME0)
end

--cq_dyna_global_data 表属性TIME1
-- SCRIPT_PARAM_DYNA_GLOBAL_TIME1		= 2215;
--返回cq_dyna_global_data的TIME1
function Get_SysDynaGlobalTime1(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime1 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalTime(nSysDyGlobId,G_DYNA_GLOBAL_TIME1)
end

--cq_dyna_global_data 表属性TIME2
-- SCRIPT_PARAM_DYNA_GLOBAL_TIME2		= 2216;
--返回cq_dyna_global_data的TIME2
function Get_SysDynaGlobalTime2(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime2 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalTime(nSysDyGlobId,G_DYNA_GLOBAL_TIME2)
end

--cq_dyna_global_data 表属性TIME3
-- SCRIPT_PARAM_DYNA_GLOBAL_TIME3		= 2217;
--返回cq_dyna_global_data的TIME3
function Get_SysDynaGlobalTime3(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime3 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalTime(nSysDyGlobId,G_DYNA_GLOBAL_TIME3)
end

--cq_dyna_global_data 表属性TIME4
-- SCRIPT_PARAM_DYNA_GLOBAL_TIME4		= 2218;
--返回cq_dyna_global_data的TIME4
function Get_SysDynaGlobalTime4(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime4 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalTime(nSysDyGlobId,G_DYNA_GLOBAL_TIME4)
end

--cq_dyna_global_data 表属性TIME5
-- SCRIPT_PARAM_DYNA_GLOBAL_TIME5		= 2219;
--返回cq_dyna_global_data的TIME5
function Get_SysDynaGlobalTime5(nSysDyGlobId)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime5 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	return GetSynaGlobalTime(nSysDyGlobId,G_DYNA_GLOBAL_TIME5)
end

-- 获取cq_dyna_global_data 表中的data0 ~ 5的值
function Get_SysDynaGlobalData(nSysDyGlobId,nPos)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	if type(nPos) ~= "number" or nPos < 0 or nPos%1 ~= 0 or nPos > 5 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalData 中 nPos 只能传0~5之间的整数")
		return
	end
	
	local nIndex = 0
	
	if nPos == 0 then
		nIndex = G_DYNA_GLOBAL_DATA0
	elseif nPos == 1 then
		nIndex = G_DYNA_GLOBAL_DATA1
	elseif nPos == 2 then
		nIndex = G_DYNA_GLOBAL_DATA2
	elseif nPos == 3 then
		nIndex = G_DYNA_GLOBAL_DATA3
	elseif nPos == 4 then
		nIndex = G_DYNA_GLOBAL_DATA4
	elseif nPos == 5 then
		nIndex = G_DYNA_GLOBAL_DATA5
	end
	
	return GetSynaGlobalData(nSysDyGlobId,nIndex)
end

-- 获取cq_dyna_global_data 表中的datastr0 ~ 5的值
function Get_SysDynaGlobalDataStr(nSysDyGlobId,nPos)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	if type(nPos) ~= "number" or nPos < 0 or nPos%1 ~= 0 or nPos > 5 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalDataStr 中 nPos 只能传0~5之间的整数")
		return
	end
	
	local nIndex = 0
	
	if nPos == 0 then
		nIndex = G_DYNA_GLOBAL_DATASTR0
	elseif nPos == 1 then
		nIndex = G_DYNA_GLOBAL_DATASTR1
	elseif nPos == 2 then
		nIndex = G_DYNA_GLOBAL_DATASTR2
	elseif nPos == 3 then
		nIndex = G_DYNA_GLOBAL_DATASTR3
	elseif nPos == 4 then
		nIndex = G_DYNA_GLOBAL_DATASTR4
	elseif nPos == 5 then
		nIndex = G_DYNA_GLOBAL_DATASTR5
	end
	
	return GetSynaGlobalDataStr(nSysDyGlobId,nIndex)
end

-- 获取cq_dyna_global_data 表中的Time0 ~ 5的值
function Get_SysDynaGlobalTime(nSysDyGlobId,nPos)
	if type(nSysDyGlobId) ~= "number" or nSysDyGlobId <= 0 or nSysDyGlobId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime 中 nSysDyGlobId 只能传大于0的整数")
		return
	end
	
	if type(nPos) ~= "number" or nPos < 0 or nPos%1 ~= 0 or nPos > 5 then
		Sys_SaveAbnormalLog("函数 Get_SysDynaGlobalTime 中 nPos 只能传0~5之间的整数")
		return
	end
	
	local nIndex = 0
	
	if nPos == 0 then
		nIndex = G_DYNA_GLOBAL_TIME0
	elseif nPos == 1 then
		nIndex = G_DYNA_GLOBAL_TIME1
	elseif nPos == 2 then
		nIndex = G_DYNA_GLOBAL_TIME2
	elseif nPos == 3 then
		nIndex = G_DYNA_GLOBAL_TIME3
	elseif nPos == 4 then
		nIndex = G_DYNA_GLOBAL_TIME4
	elseif nPos == 5 then
		nIndex = G_DYNA_GLOBAL_TIME5
	end
	
	return GetSynaGlobalTime(nSysDyGlobId,nIndex)
end

--获取客户端上传的一个子串. 例如：103type那种输入框。无参数。 
--返回客户端上传的一个子串.
function Get_SysAcceptStr()
	return GetAcceptStr()
end

--获取指定时间到开服时间有多少天（原action中 PARA_SERVER_REMIAN_DAYS对应内容）无参数。
--返回天数
function Get_SysServerRemainDays()
	return GetServerRemainDays()
end

--LUA接口：GetServerName
--参数：服务器ID，若为0表示获取本服服务器名称
--返回值：查询的服务器名称不存在返回"null"，成功返回具体服务器名称，如"龙腾虎跃"
function Get_SysServerName(nServerID)
	if nServerID == nil then
		nServerID = 0
	elseif type(nServerID) ~= "number" or nServerID < 0 or nServerID%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_SysServerName 中 nServerID 只能传大等于0的整数")
		return
	end
	
	return GetServerName(nServerID)
end

------------------------------------------------------------------------------
-------------------------------------玩家-------------------------------------
------------------------------------------------------------------------------

--取玩家id
--SCRIPT_PARAM_PLAYER_ID 	1001	//当前玩家ID,id=0
--返回玩家id
function Get_UserId()
	return GetUserInt(0,G_PLAYER_ID)
end

--取玩家名字
--SCRIPT_PARAM_PLAYER_Name,	1002	//玩家名字
--返回玩家名字，字符串型
function Get_UserName(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserName 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserStr(nUserId,G_PLAYER_Name)
end

--获取地图mapdoc 
function Get_MapDoc(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapDoc 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapIntEx(nMapId,G_MAP_DOC)
end

--[[
程序还没做完。
--取玩家头像编号
--SCRIPT_PARAM_PLAYER_LookFace,	1003	//玩家头像编号
--返回玩家头像编号
function Get_UserLookFace(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLookFace 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,1003)
end

--取玩家发型编号
--SCRIPT_PARAM_PLAYER_Hair,	1004	//玩家发型编号
--返回玩家发型编号
function Get_UserHair(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserHair 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,1004)
end
]]

--取玩家职业
--SCRIPT_PARAM_PLAYER_Profession,	1005	//玩家的职业
--返回玩家职业编号
function Get_UserProfession(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserProfession 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Profession)
end

--取玩家等级
--SCRIPT_PARAM_PLAYER_Level,	1006	//玩家等级
--返回玩家当前等级
function Get_UserLevel(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLevel 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Level)
end


--取玩家所在地图id
--SCRIPT_PARAM_PLAYER_MapID,	1007	//玩家所在地图ID
--返回玩家当前所在地图id
function Get_UserMapId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMapId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_MapID)
end

--取玩家x坐标
--SCRIPT_PARAM_PLAYER_PosX,	1008	//玩家所在地图X坐标
--返回玩家当前的x坐标
function Get_UserPositionX(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserPositionX 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_PosX)
end

--取玩家y坐标
--SCRIPT_PARAM_PLAYER_PosY,	1009	//玩家所在地图Y坐标
--返回玩家当前的y坐标
function Get_UserPositionY(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserPositionY 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_PosY)
end


--取玩家功德值
--SCRIPT_PARAM_PLAYER_Virtue,	1010	//玩家功德值
--返回玩家的功德值
function Get_UserVirtue(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserVirtue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Virtue)
end


--取玩家转世次数
--SCRIPT_PARAM_PLAYER_Meto,	1011	//玩家转世
--返回玩家的转世次数
function Get_UserMetempsychosis(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMetempsychosis 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Meto)
end

--取玩家性别
--SCRIPT_PARAM_PLAYER_Sex,	1012	//玩家性别
--返回值为：
--1，表示男性;
--2，表示女性
function Get_UserSex(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSex 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Sex)
end

--取玩家队伍成员数量
--SCRIPT_PARAM_PLAYER_TeamMemberNum,	1013	//玩家队伍成员数量，没有队伍为0
--返回玩家当前队伍的人数(包含队长)
--队长和队员都可触发
function Get_UserTeamNumbers(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamNumbers 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_TeamMemberNum)
end

--取玩家房子Id
--SCRIPT_PARAM_PLAYER_HomeID,	1014	//玩家房子id
--返回玩家的房子id
--若没房子,返回:0
function Get_UserHouseId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserHouseId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_HomeID)
end

--取玩家账号Id
--SCRIPT_PARAM_PLAYER_AccountId,	1015	//玩家账号id
--返回玩家的账号Id
function Get_UserAccountId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserAccountId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_AccountId)
end


--取玩家战斗力
--SCRIPT_PARAM_PLAYER_BattleEffect,	1016	//玩家战斗力
--返回玩家的战斗力值
function Get_UserBattleLevel(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserBattleLevel 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_BattleEffect)
end

--取玩家伴侣名字
--SCRIPT_PARAM_PLAYER_MateName,	1017	//玩家伴侣名称
--返回玩家的伴侣名字，字符串型
function Get_UserMateName(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMateName 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserStr(nUserId,G_PLAYER_MateName)
end

--取骑宠比赛积分
--SCRIPT_PARAM_PLAYER_RidingPoints,	1018	//玩家骑宠积分
--返回玩家的骑宠比赛积分
function Get_UserRidingPoints(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserRidingPoints 中 nUserId 只能传大等于0的整数")
		return
	end

	return GetUserInt(nUserId,G_PLAYER_RidingPoints)
end


--取玩家当前生命值
--SCRIPT_PARAM_PLAYER_Life,	1019	//玩家生命值
--返回玩家当前生命值
function Get_UserLife(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLife 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Life)
end


--取玩家最大生命值
--SCRIPT_PARAM_PLAYER_MaxLife,	1020	//玩家最大生命值
--返回玩家最大生命值
function Get_UserMaxLife(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMaxLife 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_MaxLife)
end

--取玩家当前法力值
--SCRIPT_PARAM_PLAYER_Mana,	1021	//玩家法力值
--返回玩家当前法力值
--没有法力值的职业,返回:0
function Get_UserMana(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMana 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Mana)
end

--取玩家最大法力值
--SCRIPT_PARAM_PLAYER_MaxMana,	1022	//玩家最大法力值
--返回玩家最大法力值
--没有法力值的职业,返回:0
function Get_UserMaxMana(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMaxMana 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_MaxMana)
end

--取玩家点化机会
--SCRIPT_PARAM_PLAYER_Mentor,	1023	//玩家点化机会
--返回玩家点化机会
--返回值:点化机会*100
--ps:0.1个点化机会,返回值为10
function Get_UserMentor(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMentor 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Mentor)
end

--取玩家变身ID
--SCRIPT_PARAM_PLAYER_Transfrom,	1024	//变身ID
--返回玩家变身ID
--没变身状态返回值为:-1
--变身状态,返回变身对应的怪物的lookface字段值
function Get_UserTransformId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTransformId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Transfrom)
end

--取玩家金币数
--SCRIPT_PARAM_PLAYER_Money,	1025	//玩家游戏币
--返回玩家金币数
-- function Get_UserMoney(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserMoney 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserInt(nUserId,G_PLAYER_Money)
-- end


---bug
--取玩家试炼窟天石操作
--SCRIPT_PARAM_PLAYER_MoneyTrial,	1026	//试炼窟天石操作
--返回玩家试炼窟天石操作
--默认返回值:-1
-- function Get_UserTrialMoney(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserTrialMoney 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserInt(nUserId,G_PLAYER_MoneyTrial)
-- end


--取玩家天石数
--SCRIPT_PARAM_PLAYER_EMoney,	1027	//玩家天石
--返回玩家天石数
function Get_UserEMoney(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserEMoney 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_EMoney)
end


--取玩家赠品天石数
--SCRIPT_PARAM_PLAYER_EMoneyMono,	1028	//玩家赠点
--返回玩家赠品天石数
function Get_UserMonoEMoney(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMonoEMoney 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_EMoneyMono)
end


--取玩家经验值
--SCRIPT_PARAM_PLAYER_Exp,	1029	//玩家经验(add操作不增加贡献)
--返回玩家经验值
--返回cq_user表的exp字段值
--玩家在线获得经验不会马上改变该字段的值,下线后才会被写入数据库.
function Get_UserExp(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserExp 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Exp)
end

--取玩家PK值
--SCRIPT_PARAM_PLAYER_PK,	1031	//玩家PK值
--返回玩家PK值
--返回cq_user表的pk字段值
--立刻写库
function Get_UserPk(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserPk 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_PK)
end


--取玩家力量值
--SCRIPT_PARAM_PLAYER_Strength,	1032	//玩家力量值
--返回玩家力量值
--返回cq_user的strength字段值
function Get_UserStrength(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStrength 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Strength)
end

--取玩家灵巧值
--SCRIPT_PARAM_PLAYER_Speed,	1033	//玩家灵巧值
--返回玩家灵巧值
--返回cq_user的speed字段值
function Get_UserSpeed(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSpeed 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Speed)
end

--取玩家体质值
--SCRIPT_PARAM_PLAYER_Health,	1034	//玩家体力值
--返回玩家体质值
--返回cq_user的Health字段值
function Get_UserHealth(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserHealth 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Health)
end


--取玩家精神值
--SCRIPT_PARAM_PLAYER_Soul,	1035	//玩家精神值
--返回玩家精神值
--返回cq_user的soul字段值
function Get_UserSoul(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSoul 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Soul)
end


--取玩家帮派里排名
--SCRIPT_PARAM_PLAYER_SynRank,	1036	//玩家帮派里排名
--返回帮派职位编号
function Get_UserGuildRank(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGuildRank 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_SynRank)
end

--暂时不管了。
--用于在任务系统中叠代
--SCRIPT_PARAM_PLAYER_Iterator,	1037	//用于在任务系统中叠代
--返回
-- function Get_UserIterator(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserIterator 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserInt(nUserId,1037)
-- end

--取玩家犯罪时间
--SCRIPT_PARAM_PLAYER_Crime,	1038	//犯罪时间
--返回玩家犯罪时间
--当玩家名字处于pk后蓝色闪烁的时候,返回1
--其余时候返回0
--红名以上未测试
function Get_UserCrimeTime(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserCrimeTime 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Crime)
end

--取玩家cq_card记录数量，大卡
--SCRIPT_PARAM_PLAYER_GameCard,	1039	//cq_card记录数量
--返回玩家cq_card记录数量，大卡
function Get_UserGameCard(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGameCard 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_GameCard)
end

--取玩家cq_card2记录数量，小卡
--SCRIPT_PARAM_PLAYER_GameCard2,	1040	//cq_card2记录数量
--返回玩家cq_card2记录数量，小卡
function Get_UserGameCard2(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGameCard2 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_GameCard2)
end

--取玩家当前xp值
--SCRIPT_PARAM_PLAYER_XP,	1041	//当前XP值
--返回玩家当前xp值
--只返回整10的值
--例:1-9,返回值=0;10-19,返回值为10.
--爆xp选择技能过程,返回值=100
--xp过程返回值=0
function Get_UserXp(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserXp 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_XP)
end

--取玩家体力值
--SCRIPT_PARAM_PLAYER_EP,	1042	//体力值
--返回玩家体力值
function Get_UserEp(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserEp 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_EP)
end

--取玩家属性点
--SCRIPT_PARAM_PLAYER_,	1043	//玩家属性点
--返回玩家属性点
--返回cq_user表additional_point字段值
function Get_UserAddPoint(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserAddPoint 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_AddPoint)
end

--暂时取消，没什么用，只返回客户端版本号
--取客户的版本号
--SCRIPT_PARAM_PLAYER_ClientVersion,	1044	//客户的版本
--返回客户的版本号
--返回:一直返回145
-- function Get_UserClientVersion(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserClientVersion 中 nUserId 只能传大等于0的整数")
		-- return
	-- end

	-- return GetUserInt(nUserId,G_PLAYER_ClientVersion)
-- end

--取玩家爵位
--SCRIPT_PARAM_PLAYER_Peerage,	1045	//玩家爵位
--返回玩家爵位
function Get_UserPeerage(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserPeerage 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Peerage)
end

--暂时不管
--取玩家交易状态
--SCRIPT_PARAM_PLAYER_Businness,	1046	//玩家交易状态
--返回玩家交易状态
-- function Get_UserBusinness(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserBusinness 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserInt(nUserId,1046)
-- end

--取玩家VIP等级
--SCRIPT_PARAM_PLAYER_VIP,	1047	//玩家VIP等级
--返回玩家VIP等级
function Get_UserVip(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserVip 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_VIP)
end

--暂时不管
--取玩家VIP值
--SCRIPT_PARAM_PLAYER_VIPValue,	1048	//玩家VIP值
--返回玩家VIP值
-- function Get_UserVipValue(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserVipValue 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserInt(nUserId,1048)
-- end

--取玩家仓库里的金币数量
--SCRIPT_PARAM_PLAYER_StorageMoney,	1049	//玩家存储的钱
--返回玩家仓库里的金币数量
-- function Get_UserStorageMoney(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserStorageMoney 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserInt(nUserId,G_PLAYER_StorageMoney)
-- end



--取玩家前前世职业
--SCRIPT_PARAM_PLAYER_FirstPro,	1050	//职业
--返回玩家前前世职业编号
function Get_UserFirstPro(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserFirstPro 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_FirstPro)
end


--取玩家前世职业
--SCRIPT_PARAM_PLAYER_OldPro,	1051	//前世职业
--返回玩家前世职业编号
function Get_UserOldPro(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserOldPro 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_OldPro)
end

--暂时不管
--取玩家坐骑移动力
--SCRIPT_PARAM_PLAYER_AddMount,	1052	//坐骑移动力
--返回玩家坐骑移动力
-- function Get_UserMountSpeed(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserMountSpeed 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserInt(nUserId,G_PLAYER_AddMount)
-- end

--取玩家修行值
--SCRIPT_PARAM_PLAYER_Cultivation,	1054	//修行值
--返回玩家修行值
function Get_UserCultivation(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserCultivation 中 nUserId 只能传大等于0的整数")
		return
	end

	return GetUserInt(nUserId,G_PLAYER_Cultivation)
end


--取玩家PK协议
--SCRIPT_PARAM_PLAYER_PKProtocol,	1055	//PK模式
--返回玩家PK协议
--新号,返回值为0
--25级时,接受pk协议,返回值为1
--不接受,返回值为2
function Get_UserPKProtocol(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserPKProtocol 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_PKProtocol)
end


--取玩家炼气的气力值
--SCRIPT_PARAM_PLAYER_StrengthValue,	1056	//气力值
--返回玩家炼气的气力值
function Get_UserStrengthValue(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStrengthValue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_StrengthValue)
end

--取玩家帮派ID
--SCRIPT_PARAM_PLAYER_SynID,	1057	//帮派ID
--返回玩家帮派ID
function Get_UserGuildId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGuildId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_SynID)
end

--bug,一直返回null，没有这个索引编号
--取玩家帮派名字
--SCRIPT_PARAM_PLAYER_SynName,	1058	//帮派名字
--返回玩家帮派名字
-- function Get_UserGuildName(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserGuildName 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetUserStr(nUserId,1058)
-- end

--取玩家家族ID
--SCRIPT_PARAM_PLAYER_FamilyID,	1059	//家族ID
--返回玩家家族ID
function Get_UserFamilyId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserFamilyId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_FamilyID)
end

--取玩家升级到下一级所需经验
--SCRIPT_PARAM_PLAYER_LevupExp,	1060	//升级到下一级所需经验
--返回玩家升级到下一级所需经验
function Get_UserLevupExp(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLevupExp 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_LevupExp)
end

--取玩家配偶ID
--SCRIPT_PARAM_PLAYER_MateID,	1061	//获得配偶ID
--返回玩家配偶ID
function Get_UserMateId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserMateId 中 nUserId 只能传大等于0的整数")
		return
	end

	return GetUserInt(nUserId,G_PLAYER_MateID)
end

--取玩家获得祝福剩余时间
--SCRIPT_PARAM_PLAYER_GodBless,	1062	//获得祝福剩余时间
--返回玩家获得祝福剩余时间
function Get_UserBless(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserBless 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_GodBless)
end

--取寄存器变量-数值型
--3.获取寄存器变量值接口：int  GetUserVarData(int UserId, int nInx);
--参数1：玩家id，参数2：索引
--返回值：寄存器变量值
--当寄存器值为空的时候,返回null
--当寄存器值设置为0的时候,返回为0
function Get_UserVarData(nInx,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserVarData 中 nUserId 只能传大等于0的整数")
		return
	end
	
	if type(nInx) ~= "number" or nInx < 0 or nInx > 7 or nInx%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserVarData 中 nInx 只能传0~7的整数")
		return
	end

	return GetUserVarData(nUserId,nInx)
end

--取寄存器变量-字符串型
--5.获取寄存器变量值（字符串类型）接口：char*  GetUserVarStr(int UserId, int nInx);
--参数1：玩家id，参数2：索引
--返回值：寄存器变量值
function Get_UserVarStr(nInx,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserVarStr 中 nUserId 只能传大等于0的整数")
		return
	end
	
	if type(nInx) ~= "number" or nInx < 0 or nInx > 7 or nInx%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserVarStr 中 nInx 只能传0~7的整数")
		return
	end

	return GetUserVarStr(nUserId,nInx)
end

-- 获取玩家队伍ID
-- 参数说明： 		nUserId:玩家ID
function Get_UserTeamId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTeamAttr(nUserId,G_TEAM_ID)
end

-- 获取玩家队伍人数
-- 参数说明： 		nUserId:玩家ID	
-- 返回队伍人数 整型
function Get_UserTeamAmount(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamAmount 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTeamAttr(nUserId,G_TEAM_Amount)
end

-- 获取玩家队伍里组员最低等级
-- 参数说明： 		nUserId:玩家ID	
-- 返回最低等级值 整型
function Get_UserTeamMinLev(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamMinLev 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTeamAttr(nUserId,G_TEAM_MinLev)
end

-- 获取玩家队伍里组员最高等级
-- 参数说明： 		nUserId:玩家ID	
-- 返回最高等级值 整型
function Get_UserTeamMaxLev(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamMaxLev 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTeamAttr(nUserId,G_TEAM_MaxLev)
end

-- 获取玩家队伍里组员最少金币
-- 参数说明： 		nUserId:玩家ID	
-- 返回金币数量 整型
-- function Get_UserTeamMinMoney(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_UserTeamMinMoney 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetTeamAttr(nUserId,G_TEAM_MinMoney)
-- end

-- 获取玩家队伍里组员是否为伴侣
-- 参数说明： 		nUserId:玩家ID
-- 是的话返回1  不是的话返回0
function Get_UserTeamMate(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamMate 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTeamAttr(nUserId,G_TEAM_Mate)
end

-- 获取玩家队伍里组员是否为朋友
-- 参数说明： 		nUserId:玩家ID
-- 是的话返回1  不是的话返回0
function Get_UserTeamFriend(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamFriend 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTeamAttr(nUserId,G_TEAM_Friend)
end

-- 获取玩家队伍里组员是否都活着
-- 参数说明： 		nUserId:玩家ID
-- 是的话返回1  不是的话返回0
function Get_UserTeamAlive(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserTeamAlive 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTeamAttr(nUserId,G_TEAM_Alive)
end

--nVarNumber为寄存器编号，返回时，该对应的寄存器存有玩家的最后上线时间与当前时间的时间差（单位：天）
--返回时间差（天）
--占用1号寄存器
--若服务器时间倒转，执行失败。服务器时间只能顺延。
function Get_UserLastLoginTime(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLastLoginTime 中 nUserId 只能传大于等于0的整数")
		return
	end
	UserLastLoginOperator(nUserId,1,1)
	local nDays = Get_UserVarData(1,0)
	User_SetVarData(1,0,0)

	return nDays
end

-- SCRIPT_PARAM_SYN_MEMBER_ATTR_RANK	 = 2901,		//帮派玩家的帮派内等级(指帮主，长老这种)
--错误时，返回-1
--返回字符串，等级名称。（帮主，副帮主等）
function Get_UserGuildRankName(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGuildRankName 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GetSynMemberStr(nUserId,G_SYN_MEMBER_ATTR_RANK)
end


--SCRIPT_PARAM_SYN_MEMBER_ATTR_PROFFER = 2902,		//帮派玩家的游戏币贡献值
--没有帮派时，返回0
--返回在帮派内的贡献度值
function Get_UserGuildContribution(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGuildContribution 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GetSynMemberInt(nUserId,G_SYN_MEMBER_ATTR_PROFFER)
end

--获取帮派的整型属性_帮派成员数量
--参数说明：nGuildId指要操作的玩家帮派ID, nIdx指SCRIPT_PARAM_SYNDICATE_BEGIN-SCRIPT_PARAM_SYNDICATE_END的枚举值，如果失败返回"-1"，否则返回具体的值。
--LUA_FUNC(GetSynInt)
--int idObj	= Lua_GetParamInt(1);
--int nIdx	= Lua_GetParamInt(2);
--SCRIPT_PARAM_SYNDICATE_MEMBER_AMOUNT = 2834,					//帮派成员数量
--返回
function Get_UserSynDicateMemberAmount(nGuildId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSynDicateMemberAmount 中 nGuildId 只能大于0的整数")
		return
	end

	return GetSynInt(nGuildId,G_SYNDICATE_MEMBER_AMOUNT)
end

--获取帮派的整型属性_帮派玩家的帮派内等级(指帮主，长老这种)
--参数说明：nGuildId指要操作的玩家帮派ID, nIdx指SCRIPT_PARAM_SYNDICATE_BEGIN-SCRIPT_PARAM_SYNDICATE_END的枚举值，如果失败返回"-1"，否则返回具体的值。
--LUA_FUNC(GetSynInt)
--int idObj	= Lua_GetParamInt(1);
--int nIdx	= Lua_GetParamInt(2);
--SCRIPT_PARAM_SYN_MEMBER_ATTR_RANK	 = 2901,		//帮派玩家的帮派内等级(指帮主，长老这种)
--返回
function Get_UserSynMemberAttrRank(nGuildId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSynMemberAttrRank 中 nGuildId 只能大于0的整数")
		return
	end
	
	return GetSynInt(nGuildId,G_SYN_MEMBER_ATTR_RANK)
end	

	
--获取帮派的字符串类型属性_帮派名称
--参数说明：nGuildId指要操作的玩家帮派ID, nIdx指SCRIPT_PARAM_SYNDICATE_BEGIN-SCRIPT_PARAM_SYNDICATE_END的枚举值，如果失败返回"null"，否则返回具体的值。
--LUA_FUNC(GetSynStr)
--int idObj	= Lua_GetParamInt(1);
--int nIdx	= Lua_GetParamInt(2);
--SCRIPT_PARAM_SYNDICATE_NAME			 = 2831,					//帮派名称
--返回
function Get_UserSynDicateName(nGuildId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSynDicateName 中 nGuildId 只能大于0的整数")
		return
	end
	
	return GetSynStr(nGuildId,G_SYNDICATE_NAME)
end	

--获取帮派的字符串类型属性_帮派帮主名称
--参数说明：nGuildId指要操作的玩家帮派ID, nIdx指SCRIPT_PARAM_SYNDICATE_BEGIN-SCRIPT_PARAM_SYNDICATE_END的枚举值，如果失败返回"null"，否则返回具体的值。
--LUA_FUNC(GetSynStr)
--int idObj	= Lua_GetParamInt(1);
--int nIdx	= Lua_GetParamInt(2);
--SCRIPT_PARAM_SYNDICATE_LEADER_NAME	 = 2832,					//帮派帮主名称
--返回
function Get_UserSynDicateLeaderName(nGuildId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSynDicateLeaderName 中 nGuildId 只能大于0的整数")
		return
	end
	
	return GetSynStr(nGuildId,G_SYNDICATE_LEADER_NAME)
end

-- 获取帮派基金
function Get_UserSynMoney(nGuildId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSynMoney 中 nGuildId 只能大于0的整数")
		return
	end
	
	return GetSynInt(nGuildId,G_SYNDICATE_MONEY)
end

-- 获取帮派天石
function Get_UserSynEmoney(nGuildId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSynEmoney 中 nGuildId 只能大于0的整数")
		return
	end
	
	return GetSynInt(nGuildId,G_SYNDICATE_EMONEY)
end

--天石卡表有此类型的未使用的天石卡数量.  
--nCard表示天石卡表号,指SCRIPT_PARAM_EMONEY_CARD1的枚举值{2601,2602,2603,2604}
--nCardType表示天石卡类型, 只有emoney_card时才使用, 其它表填0. 
--nUserId表示用户id.
--返回具体的数量.
--例:GetEmoneyCardCount(2601,780000,0)||GetEmoneyCardCount(2602,0,0)
function Get_UserEmoneyCardCount(nCard,nCardType,nUserId)
	if type(nCard) ~= "number" or nCard%1 ~= 0 or nCard < 2601 or nCard > 2604 then
		Sys_SaveAbnormalLog("函数 Get_UserEmoneyCardCount 第一个参数nCard为整型且范围在2601--2604")
		return
	end
	
	if type(nCardType) ~= "number" or nCardType%1 ~= 0 or nCardType < 0 then
		Sys_SaveAbnormalLog("函数 Get_UserEmoneyCardCount 第二个参数nCardType为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Get_UserEmoneyCardCount 第三个参数nUserId为整型并且大于等于0")
		return
	end
	
	return GetEmoneyCardCount(nUserId,nCard,nCardType)
end

-- //获取玩家功夫属性的整型属性，参数说明：idUser指要操作的USERID, nIdx指SCRIPT_PARAM_GONGFU_ATTR_BEGIN-SCRIPT_PARAM_GONGFU_ATTR_END的枚举值，如果失败返回"-1"，否则返回具体的值。
-- LUA_FUNC(GetGongFuInt)
	-- int idUser	= Lua_GetParamInt(1);
	-- int nIdx	= Lua_GetParamInt(2);
function Get_UserGongFuInt(nIdx,nUserId)
	if type(nIdx) ~= "number" or nIdx < 0 or nIdx%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGongFuInt 第1个参数nIdx必须为整型并且大于等于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGongFuInt 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	return GetGongFuInt(nUserId,nIdx)
end

-- 玩家的功力境界
function Get_UserGongFuSkill(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGongFuInt 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	return GetGongFuInt(nUserId,G_GONGFU_ATTR_REALM)
end

-- 玩家免费修炼次数
function Get_UserGongFureePractNum(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGongFuInt 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	return GetGongFuInt(nUserId,G_GONGFU_ATTR_FREE_CULTIVATE_PARAM)
end

-- 玩家真气等级
function Get_UserGongFuQiLev(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserGongFuInt 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	return GetGongFuInt(nUserId,G_GONGFU_ATTR_GENUINEQI_LV)
end
------------------------------------------------------------------------------
-------------------------------------Npc--------------------------------------
------------------------------------------------------------------------------

--取当前NPC的ID
--NPC的ID  =  2001
--返回npc Id 整型
function Get_NpcId()
	return GetNpcInt(0,G_NPC_ID)
end

--取NPC的名字
--NPC的名字 = 2002
--返回npc名字，字符串型
function Get_NpcName(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcName 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcStr(nNpcId,G_NPC_Name)
end

--取NPC的OwnerID
--NPC的OwnerID = 2003
--返回取NPC的OwnerID，整型
function Get_NpcOwnerID(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcOwnerID 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_OwnerID)
end


--取NPC的OwnerType
--NPC的OwnerType = 2004
--返回取NPC的OwnerType，整型
function Get_NpcOwnerType(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcOwnerType 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_OwnerType)
end

--取NPC的Type
--NPC的Type = 2005
--返回取NPC的Type，整型
function Get_NpcType(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcType 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_Type)
end


--取NPC的Lookface
--NPC的Lookface = 2006
--返回取NPC的Lookface，整型
function Get_NpcLookface(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcLookface 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_LookFace)
end

--取NPC所在的地图编号
--NPC的MapID = 2007
--返回取NPC的MapID，整型
function Get_NpcMapID(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcMapID 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_MapID)
end

--取NPC所在地图的X坐标
--NPC的X坐标 = 2008
--返回取NPC的X坐标，整型
function Get_NpcPositionX(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcPositionX 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_PosX)
end

--取NPC所在地图的Y坐标
--NPC的Y坐标 = 2009
--返回取NPC的Y坐标，整型
function Get_NpcPositionY(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcPositionY 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_PosY)
end

--取NPC身上的Data0值 
--NPC的Data0 = 2010
--返回取NPC的Data0值，整型
function Get_NpcData0(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcData0 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_Data0)
end

--取NPC身上的Data1值 
--NPC的Data1 = 2011
--返回取NPC的Data1值，整型
function Get_NpcData1(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcData1 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_Data1)
end

--取NPC身上的Data2值 
--NPC的Data2 = 2012
--返回取NPC的Data2值，整型
function Get_NpcData2(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcData2 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_Data2)
end

--取NPC身上的Data3值 
--NPC的Data3 = 2013
--返回取NPC的Data3值，整型
function Get_NpcData3(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcData3 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_Data3)
end

--取NPC身上的DataStr值 
--NPC的DataStr = 2014
--返回取NPC的DataStr值，字符串
function Get_NpcDataStr(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcDataStr 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcStr(nNpcId,G_NPC_DataStr)
end

--取NPC最大血值
--NPC的MaxLife = 2015
--返回取NPC的MaxLife，整型
function Get_NpcMaxLife(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcMaxLife 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_MaxLife)
end

--取NPC当前血值
--NPC的Life = 2016
--返回取NPC的Life，整型
function Get_NpcLife(nNpcId)
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcLife 中 nNpcId 只能传大等于0的整数")
		return
	end
	
	return GetNpcInt(nNpcId,G_NPC_Life)
end

--SCRIPT_PARAM_NPC_COUNT_ALL,=2801				//取玩家地图上所有NPC的数量
--返回数值=cq_npc表+cq_dynanpc表的当前地图NPC总和
function Get_NpcCount(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcCount 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	return GetNpcCount(nUserId,G_NPC_COUNT_ALL,"")
end

--SCRIPT_PARAM_NPC_COUNT_FURNITURE,=2802			//取玩家地图上所有家具的数量
function Get_NpcCountByFurniture(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcCountByFurniture 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	return GetNpcCount(nUserId,G_NPC_COUNT_FURNITURE,"")
end

--SCRIPT_PARAM_NPC_COUNT_NAME,=2803				//取玩家地图上所有指定名字的NPC数量
--返回数值=cq_npc表+cq_dynanpc表的当前地图符合条件的NPC总和
function Get_NpcCountByName(sNpcName,nUserId)
	if type(sNpcName) ~= "string" then
		Sys_SaveAbnormalLog("函数 Get_NpcCountByName 中 sNpcName 只能传字符串类型的参数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcCountByName 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GetNpcCount(nUserId,G_NPC_COUNT_NAME,sNpcName)
end

--SCRIPT_PARAM_NPC_COUNT_TYPE,=2804				//取玩家地图上所有指定类型的NPC数量
--返回数值=cq_npc表+cq_dynanpc表的当前地图符合条件的NPC总和
function Get_NpcCountByType(nNpcType,nUserId)
	if type(nNpcType) ~= "number" or nNpcType <= 0 or nNpcType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcCountByType 中 nNpcType 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_NpcCountByType 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GetNpcCount(nUserId,G_NPC_COUNT_TYPE,nNpcType)
end




------------------------------------------------------------------------------
-------------------------------------物品-------------------------------------
------------------------------------------------------------------------------
--取cq_item表Type
--SCRIPT_PARAM_ITEM_Type,=2302				//物品类型
--返回cq_item表Type，失败返回-1
function Get_ItemType(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemType 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Type)
end

--取cq_item表OwnerID
--SCRIPT_PARAM_ITEM_OwnerID,=2303			//OwnerID
--返回cq_item表OwnerID，失败返回-1
function Get_ItemOwnerId(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemOwnerId 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_OwnerID)
end

--取cq_item表PlayerID
--SCRIPT_PARAM_ITEM_PlayerID,=2304			//PlayerID
--返回cq_item表PlayerID，失败返回-1
function Get_ItemPlayerId(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemPlayerId 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_PlayerID)
end

--取cq_item表Amount
--SCRIPT_PARAM_ITEM_Amount,=2305			//当前耐久
--返回cq_item表Amount，失败返回-1
function Get_ItemAmount(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemAmount 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Amount)
end

--取cq_item表AmountLimit
--SCRIPT_PARAM_ITEM_AmountLimit,=2306		//耐久上限
--返回cq_item表AmountLimit，失败返回-1
function Get_ItemAmountLimit(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemAmountLimit 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_AmountLimit)
end

--取cq_item表Ident
--SCRIPT_PARAM_ITEM_Ident,=2307			//是否鉴定
--返回cq_item表Ident，失败返回-1
function Get_ItemIdent(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemIdent 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Ident)
end

--取cq_item表Position
--SCRIPT_PARAM_ITEM_Position,=2308			//位置
--返回cq_item表Position，失败返回-1
function Get_ItemPosition(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemPosition 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Position)
end

--取cq_item表Gem1
--SCRIPT_PARAM_ITEM_Gem1,=2309				//第一个洞
--返回cq_item表Gem1，失败返回-1
function Get_ItemGem1(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemGem1 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Gem1)
end

--取cq_item表Gem2
--SCRIPT_PARAM_ITEM_Gem2,=2310				//第二个洞
--返回cq_item表Gem2，失败返回-1
function Get_ItemGem2(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemGem2 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Gem2)
end

--取cq_item表Magic1
--SCRIPT_PARAM_ITEM_Magic1,=2311			//第一种魔法效果
--返回cq_item表Magic1，失败返回-1
function Get_ItemMagic1(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemMagic1 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Magic1)
end

--取cq_item表Magic2
--SCRIPT_PARAM_ITEM_Magic2,=2312			//第二种魔法效果
--返回cq_item表Magic2，失败返回-1
function Get_ItemMagic2(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemMagic2 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Magic2)
end

--取cq_item表Magic3
--SCRIPT_PARAM_ITEM_Addition,=2313			//追加数值			//这个就是SCRIPT_PARAM_ITEM_Magic3
--返回cq_item表Magic3，失败返回-1
function Get_ItemMagic3(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemMagic3 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Addition)
end

--取cq_item表Data
--SCRIPT_PARAM_ITEM_Data,=2314				//物品Data字段值
--返回cq_item表Data，失败返回-1
function Get_ItemData(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemData 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Data)
end

--取cq_item表ReduceDmg
--SCRIPT_PARAM_ITEM_ReduceDmg,=2315		//神佑
--返回cq_item表ReduceDmg，失败返回-1
function Get_ItemReduceDmg(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemReduceDmg 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_ReduceDmg)
end

--取cq_item表AddLife
--SCRIPT_PARAM_ITEM_AddLife,=2316			//生命加持
--返回cq_item表AddLife，失败返回-1
function Get_ItemAddLife(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemAddLife 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_AddLife)
end

--取cq_item表AntiMonster
--SCRIPT_PARAM_ITEM_AntiMonster,=2317		//克制怪物
--返回cq_item表AntiMonster，失败返回-1
function Get_ItemAntiMonster(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemAntiMonster 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_AntiMonster)
end

--只会显示name原名，不会有“精品”“+6”等字样。
--取物品的name
--SCRIPT_PARAM_ITEM_Name,=2318				//名字
--返回物品的name，cq_item表并没有Name字段，失败返回-1
function Get_ItemName(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemName 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemStr(nItemId,G_ITEM_Name)
end

--取cq_item表Color
--SCRIPT_PARAM_ITEM_Color,=2319			//颜色
--返回cq_item表Color，失败返回-1
function Get_ItemColor(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemColor 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Color)
end

--取cq_item表Monopoly
--SCRIPT_PARAM_ITEM_Monopoly,=2320			//赠品装备属性
--返回cq_item表Monopoly，失败返回-1
function Get_ItemMonopoly(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemMonopoly 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_Monopoly)
end

--取cq_item表AddExp
--SCRIPT_PARAM_ITEM_AddExp,=2321			//追加经验
--返回cq_item表AddExp，失败返回-1
function Get_ItemAddExp(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemAddExp 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_AddExp)
end

--取cq_item表del_time
--SCRIPT_PARAM_ITEM_DelTime,=2322			//时效性物品删除时间
--返回cq_item表del_time，失败返回-1
function Get_ItemDelTime(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemDelTime 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_DelTime)
end

--取cq_item表SaveTime
--SCRIPT_PARAM_ITEM_SaveTime,=2323			//物品有效时间
--返回cq_item表SaveTime，失败返回-1
function Get_ItemSaveTime(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemSaveTime 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_SaveTime)
end

--取cq_itemtype表acution_deposit
--SCRIPT_PARAM_ITEM_AcutionDeposit,=2324	//拍卖行保证金
--返回cq_item表acution_deposit，失败返回-1
function Get_ItemAcutionDeposit(nItemId)
	if nItemId == nil then
		nItemId = 0
	elseif type(nItemId) ~= "number" or nItemId < 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemAcutionDeposit 中 nItemId 只能传大等于0的整数")
		return
	end

	return GetItemInt(nItemId,G_ITEM_AcutionDeposit)
end

--返回itemid,
--堆栈式，若物品A先入手，B后入手，此时，返回B的ID
--若B被丢弃或出售或交易，再次执行函数，返回A的ID.
--//最后入手的物品ID   	参1:玩家ID，		失败返回ID_NONE，否则返回物品ID
--LUA_FUNC(GetLastAddItemID)
--	OBJID idUser = Lua_GetParamUInt(1);
function Get_ItemLastAdd(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemLastAdd 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GetLastAddItemID(nUserId)
end

--返回itemid
--丢弃，出售有效，交易无效。
--//最后删除的物品ID   	参1:玩家ID，		失败返回ID_NONE，否则返回物品ID
--LUA_FUNC(GetLastDelItemID)
--	OBJID idUser = Lua_GetParamUInt(1);
function Get_ItemLastDel(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemLastDel 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GetLastDelItemID(nUserId)
end

--取cq_itemtype表Name
--SCRIPT_PARAM_ITEMTYPE_Name,=2701				//名字
--返回cq_item表Name，失败返回-1
function Get_ItemtypeName(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemSaveTime 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeStr(nItemtypeId,G_ITEMTYPE_Name)
end

--取cq_itemtype表Profession
--SCRIPT_PARAM_ITEMTYPE_Profession,=2702		//职业限制
--返回cq_item表req_Profession，失败返回-1
function Get_ItemtypeProfession(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeProfession 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_Profession)
end

--取cq_itemtype表req_weaponskill
--SCRIPT_PARAM_ITEMTYPE_Skill,=2703			//武器限制
--返回cq_item表req_weaponSkill，失败返回-1
function Get_ItemtypeSkill(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeSkill 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_Skill)
end

--取cq_itemtype表Level
--SCRIPT_PARAM_ITEMTYPE_Level,=2704			//等级限制
--返回cq_item表req_Level，失败返回-1
function Get_ItemtypeLevel(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeLevel 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_Level)
end

--取cq_itemtype表Sex
--SCRIPT_PARAM_ITEMTYPE_Sex,=2705				//性别限制
--返回cq_item表req_Sex，失败返回-1
function Get_ItemtypeSex(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeSex 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_Sex)
end

--取cq_itemtype表Monopoly
--SCRIPT_PARAM_ITEMTYPE_Monopoly,=2706			//独占性
--返回cq_item表Monopoly，失败返回-1
function Get_ItemtypeMonopoly(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeMonopoly 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_Monopoly)
end

--取cq_itemtype表type_Mask
--SCRIPT_PARAM_ITEMTYPE_Mask,=2707				//(贵重物品之类的属性)
--返回cq_item表Mask，失败返回-1
function Get_ItemtypeMask(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeMask 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_Mask)
end

--取cq_itemtype表EmoneyPrice
--SCRIPT_PARAM_ITEMTYPE_EmoneyPrice,=2708		//天石价格
--返回cq_item表EmoneyPrice，失败返回-1
function Get_ItemtypeEmoneyPrice(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeEmoneyPrice 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_EmoneyPrice)
end

--取cq_itemtype表EmoneyMonoPrice
--SCRIPT_PARAM_ITEMTYPE_EmoneyMonoPrice,=2709	//赠点价格
--返回cq_item表EmoneyMonoPrice，失败返回-1
function Get_ItemtypeEmoneyMonoPrice(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeEmoneyMonoPrice 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_EmoneyMonoPrice)
end

--取cq_itemtype表SaveTime
--SCRIPT_PARAM_ITEMTYPE_SaveTime,=2710			//保持时间
--返回cq_item表Save_Time，失败返回-1
function Get_ItemtypeSaveTime(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeSaveTime 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_SaveTime)
end

--取cq_itemtype表TypeDesc
--SCRIPT_PARAM_ITEMTYPE_TypeDesc,=2711			//类型说明
--返回cq_item表TypeDesc，失败返回-1
function Get_ItemtypeTypeDesc(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeTypeDesc 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeStr(nItemtypeId,G_ITEMTYPE_TypeDesc)
end

--取cq_itemtype表ItemDesc
--SCRIPT_PARAM_ITEMTYPE_ItemDesc,=2712			//物品说明
--返回cq_item表ItemDesc，失败返回-1
function Get_ItemtypeItemDesc(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeItemDesc 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeStr(nItemtypeId,G_ITEMTYPE_ItemDesc)
end

--// 获取装备子类型. 参数说明: idUser表示玩家ID; nPos表示装备位置(1-8). 如果失败返回0, 成功返回装备子类型(类型的十万位、万位、千位).
-- LUA_FUNC(GetEquipSubType)
	-- OBJID idUser = Lua_GetParamUInt(1);
	-- int   nPos   = Lua_GetParamInt(2);
function Get_ItemSubType(nPos,nUserId)
	if type(nPos) ~= "number" or nPos < 0 or nPos > 8 or nPos%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemSubType 中 nPos 只能传1~8的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemSubType 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	return GetEquipSubType(nUserId,nPos)
end

--取cq_itemtype表AccumulateLimit
--G_ITEMTYPE_AccumulateLimit,=2713			//物品叠加数
--返回cq_item表AccumulateLimit
function Get_ItemtypeAccumulateLimit(nItemtypeId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_ItemtypeAccumulateLimit 中 nItemtypeId 只能传大于0的整数")
		return
	end

	return GetItemTypeInt(nItemtypeId,G_ITEMTYPE_AccumulateLimit)
end

------------------------------------------------------------------------------
-------------------------------------怪物-------------------------------------
------------------------------------------------------------------------------

--G_MONSTER_Name,					=	3102
function Get_MonsterName(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterName 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterStr(nMonsterId,G_MONSTER_Name)
end

--G_MONSTER_Type,					=	3103
function Get_MonsterType(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterType 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterInt(nMonsterId,G_MONSTER_Type)
end

--G_MONSTER_MapID,					=	3104
function Get_MonsterMapID(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterMapID 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterInt(nMonsterId,G_MONSTER_MapID)
end

--G_MONSTER_PosX,					=	3105
function Get_MonsterPosX(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterPosX 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterInt(nMonsterId,G_MONSTER_PosX)
end

--G_MONSTER_PosY,					=	3106
function Get_MonsterPosY(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterPosY 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterInt(nMonsterId,G_MONSTER_PosY)
end

--G_MONSTER_MaxLife,				=	3107
function Get_MonsterMaxLife(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterMaxLife 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterInt(nMonsterId,G_MONSTER_MaxLife)
end

--G_MONSTER_Life,					=	3108
function Get_MonsterLife(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterLife 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterInt(nMonsterId,G_MONSTER_Life)
end

--G_MONSTER_Level,					=	3109
function Get_MonsterLevel(nMonsterId)
	if nMonsterId == nil then
		nMonsterId = 0
	elseif type(nMonsterId) ~= "number" or nMonsterId < 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MonsterLevel 中 nMonsterId 只能传大等于0的整数")
		return
	end

	return GetMonsterInt(nMonsterId,G_MONSTER_Level)
end


------------------------------------------------------------------------------
-------------------------------------地图-------------------------------------
------------------------------------------------------------------------------
--SCRIPT_PARAM_MAP_Name,= 2102          //取地图的名称
function Get_MapName(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapName 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapStr(nMapId,G_MAP_Name)
end


--SCRIPT_PARAM_MAP_Type,= 2103			//取地图的属性
function Get_MapType(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapType 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapInt(nMapId,G_MAP_Type)
end


--SCRIPT_PARAM_MAP_OwnerID,= 2104		//取地图主人ID
function Get_MapOwnerId(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapOwnerId 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapInt(nMapId,G_MAP_OwnerID)
end

--SCRIPT_PARAM_MAP_RebornMap,= 2105		//取地图重生地图ID
function Get_MapRebornMapId(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapRebornMapId 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapInt(nMapId,G_MAP_RebornMap)
end

--SCRIPT_PARAM_MAP_PosX,= 2106			//取地图重生点X坐标
function Get_MapRebornX(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapRebornX 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapInt(nMapId,G_MAP_PosX)
end

--SCRIPT_PARAM_MAP_PosY,= 2107			//取地图重生点Y坐标
function Get_MapRebornY(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapRebornY 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapInt(nMapId,G_MAP_PosY)
end

--G_MAP_RES_LEV 						=	2109	--资源等级
function Get_MapResLev(nMapId)
	if nMapId == nil then
		nMapId = Get_UserMapId(0)
	elseif type(nMapId) ~= "number" or nMapId < 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MapResLev 中 nMapId 只能传大于等于0的整数")
		return
	end

	return GetMapIntEx(nMapId,G_MAP_RES_LEV)
end


------------------------------------------------------------------------------
-------------------------------------陷阱-------------------------------------
------------------------------------------------------------------------------

--bug,该接口这次的版本还未上，这里备注下
--函数说明：	获取传送阵ID
--参数说明：	nUserId 玩家ID
-- function Get_TransferId(nUserId)
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TransferId 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetLastTransportor(nUserId)
-- end

--函数说明：	 获取类型为type的陷阱数量
--参数说明：	nType表示陷阱类型，返回值是陷阱数量
function Get_TrapCount(nType)
	if type(nType) ~= "number" or nType < 0 or nType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TrapCount 中 nType 只能传大等于0的整数")
		return
	end

	return GetTrapCount(nType)
end

--G_TRAP_ID							=	2501
function Get_TrapID()
	return GetMapTrapInt(0,G_TRAP_ID)
end

--G_TRAP_TYPE							=	2502
function Get_TrapType(nTrapId)
	if nTrapId == nil then
		nTrapId = 0
	elseif type(nTrapId) ~= "number" or nTrapId < 0 or nTrapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TrapType 中 nTrapId 只能传大等于0的整数")
		return
	end

	return GetMapTrapInt(nTrapId,G_TRAP_TYPE)
end

--G_TRAP_LOOK							=	2503
function Get_TrapLook(nTrapId)
	if nTrapId == nil then
		nTrapId = 0
	elseif type(nTrapId) ~= "number" or nTrapId < 0 or nTrapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TrapLook 中 nTrapId 只能传大等于0的整数")
		return
	end

	return GetMapTrapInt(nTrapId,G_TRAP_LOOK)
end

--G_TRAP_MAPID						=	2505
function Get_TrapMAPID(nTrapId)
	if nTrapId == nil then
		nTrapId = 0
	elseif type(nTrapId) ~= "number" or nTrapId < 0 or nTrapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TrapMAPID 中 nTrapId 只能传大等于0的整数")
		return
	end

	return GetMapTrapInt(nTrapId,G_TRAP_MAPID)
end

--G_TRAP_PosX							=	2506
function Get_TrapPosX(nTrapId)
	if nTrapId == nil then
		nTrapId = 0
	elseif type(nTrapId) ~= "number" or nTrapId < 0 or nTrapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TrapPosX 中 nTrapId 只能传大等于0的整数")
		return
	end

	return GetMapTrapInt(nTrapId,G_TRAP_PosX)
end

--G_TRAP_PosY							=	2507
function Get_TrapPosY(nTrapId)
	if nTrapId == nil then
		nTrapId = 0
	elseif type(nTrapId) ~= "number" or nTrapId < 0 or nTrapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TrapPosY 中 nTrapId 只能传大等于0的整数")
		return
	end

	return GetMapTrapInt(nTrapId,G_TRAP_PosY)
end

------------------------------------------------------------------------------
-------------------------------------掩码-------------------------------------
------------------------------------------------------------------------------
--函数说明：	获取stc掩码值
--参数说明：	nUserId 玩家ID，nEvent 掩码前3位，nType掩码后2位 例：stc(123,45) nEvent = 123, nType = 45
function Get_UserStatisticValue(nEvent,nType,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStatisticValue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	if type(nEvent) ~= "number" or nEvent <= 0 or nEvent%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStatisticValue 中 nEvent 只能传大于0的整数")
		return
	end
	
	if type(nType) ~= "number" or nType < 0 or nType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStatisticValue 中 nType 只能传大于等于0的整数")
		return
	end
	
	return GetUserStatistic(nUserId, nEvent, nType)
end

--函数说明：	获取stc掩码值 1067
--参数说明：	nUserId 玩家ID，nEvent 掩码前3位，nType掩码后2位 例：stc(123,45) nEvent = 123, nType = 45
function Get_UserStatisticDailyValue(nEvent,nType,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStatisticDailyValue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	if type(nEvent) ~= "number" or nEvent <= 0 or nEvent%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStatisticDailyValue 中 nEvent 只能传大于0的整数")
		return
	end
	
	if type(nType) ~= "number" or nType <= 0 or nType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStatisticDailyValue 中 nType 只能传大于0的整数")
		return
	end
	
	return GetUserStatisticDaily(nUserId, nEvent, nType)
end

--函数说明：	获取stc掩码值时间戳
--参数说明：	nUserId 玩家ID，nEvent 掩码前3位，nType掩码后2位 例：stc(123,45) nEvent = 123, nType = 45
function Get_UserStcTimestampValue(nEvent,nType,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStcTimestampValue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	if type(nEvent) ~= "number" or nEvent <= 0 or nEvent%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStcTimestampValue 中 nEvent 只能传大于0的整数")
		return
	end
	
	if type(nType) ~= "number" or nType < 0 or nType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserStcTimestampValue 中 nType 只能传大于等于0的整数")
		return
	end
	
	return GetUserStcTimestamp(nUserId, nEvent, nType)
end

-- bug：返回值为0
-- 获取玩家task_detail表的id值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
-- 这边没有需要取到taskDetailId,所以屏蔽掉
-- function Get_TaskDetailId(nQuestId,nUserId)
	-- if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailId 中 nQuestId 只能传大于0的整数")
		-- return
	-- end

	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailId 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_ID)
-- end

-- bug：返回值为0
-- 获取玩家task_detail表的user_id值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
-- 没有需要取到UserId,所以屏蔽掉
-- function Get_TaskDetailUserId(nQuestId,nUserId)
	-- if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailUserId 中 nQuestId 只能传大于0的整数")
		-- return
	-- end

	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailUserId 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_USER_ID)
-- end

-- bug：返回值为0
-- 获取玩家task_detail表的task_id值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
-- 没有需要取到task_id,所以屏蔽掉
-- function Get_TaskDetailTaskId(nQuestId,nUserId)
	-- if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailTaskId 中 nQuestId 只能传大于0的整数")
		-- return
	-- end
	
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailTaskId 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_TASK_ID)
-- end

-- 获取玩家task_detail表的Complete_Flag值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
function Get_TaskDetailCompleteFlag(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailCompleteFlag 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailCompleteFlag 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_COMPLETE_FLAG)
end

-- 获取玩家task_detail表的Notify_Flag值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
function Get_TaskDetailNotifyFlag(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailNotifyFlag 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailNotifyFlag 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_NOTIFY_FLAG)
end

-- 获取玩家task_detail表的data1值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
function Get_TaskDetailData1(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData1 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData1 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_DATA1)
end

-- 获取玩家task_detail表的data2值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
function Get_TaskDetailData2(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData2 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData2 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_DATA2)
end

-- 获取玩家task_detail表的data3值
-- 参数说明：nUserId：玩家ID
-- 参数说明：nQuestId：玩家task_detail表的task_id值
function Get_TaskDetailData3(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData3 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData3 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_DATA3)
end

-- 获取玩家task_detail表的data4值
function Get_TaskDetailData4(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData4 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData4 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_DATA4)
end

-- 获取玩家task_detail表的data5值
function Get_TaskDetailData5(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData5 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData5 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_DATA5)
end

-- 获取玩家task_detail表的data6值
function Get_TaskDetailData6(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData6 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData6 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_DATA6)
end

-- 获取玩家task_detail表的data7值
function Get_TaskDetailData7(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData7 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailData7 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_DATA7)
end

-- 获取玩家task_detail表的task_overtime值
function Get_TaskDetailovertime(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailovertime 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetailovertime 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_TASK_OVERTIME)
end

-- 获取玩家task_detail表的type值
--bug 显示一直为0
-- 服务端没有取这个值得接口
-- function Get_TaskDetailtype(nQuestId,nUserId)
	-- if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailtype 中 nQuestId 只能传大于0的整数")
		-- return
	-- end
	
	-- if nUserId == nil then
		-- nUserId = 0
	-- elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		-- Sys_SaveAbnormalLog("函数 Get_TaskDetailtype 中 nUserId 只能传大等于0的整数")
		-- return
	-- end
	
	-- return GetTaskDetailData(nUserId,nQuestId,G_TASKDETAIL_TYPE)
-- end

-- 获取玩家task_detail表的max_accumulate_times 值
function Get_MaxAccumulateTime(nQuestId,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MaxAccumulateTime 中 nQuestId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_MaxAccumulateTime 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,G_MAX_ACCUMULATE_TIMES)
end

-- 获取玩家task_detail表的数据
-- nQuestId 任务ID
-- sPos 对应的任务字段 只能传 "CompleteFlag"，"NotifyFlag"，"1"，"2"，"3"，"4"，"5"，"6"，"7"，"OverTime"，"OverTimeSec"
-- nUserId 玩家ID
function Get_TaskDetail(nQuestId,sPos,nUserId)
	if type(nQuestId) ~= "number" or nQuestId <= 0 or nQuestId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetail 中 nQuestId 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetail 中 nUserId 只能传大等于0的整数")
		return
	end
	
	local nIndex = 0
	
	if sPos == "CompleteFlag" then
		nIndex = G_TASKDETAIL_COMPLETE_FLAG
	elseif sPos == "NotifyFlag" then
		nIndex = G_TASKDETAIL_NOTIFY_FLAG
	elseif sPos == "1" then
		nIndex = G_TASKDETAIL_DATA1
	elseif sPos == "2" then
		nIndex = G_TASKDETAIL_DATA2
	elseif sPos == "3" then
		nIndex = G_TASKDETAIL_DATA3
	elseif sPos == "4" then
		nIndex = G_TASKDETAIL_DATA4
	elseif sPos == "5" then
		nIndex = G_TASKDETAIL_DATA5
	elseif sPos == "6" then
		nIndex = G_TASKDETAIL_DATA6
	elseif sPos == "7" then
		nIndex = G_TASKDETAIL_DATA7
	elseif sPos == "OverTime" then
		nIndex = G_TASKDETAIL_TASK_OVERTIME
	elseif sPos == "OverTimeSec" then
		nIndex = G_TASKDETAIL_TASK_OVERTIME_SEC
	end
	
	if nIndex == 0 then
		Sys_SaveAbnormalLog("函数 Get_TaskDetail 中 sPos 传入的格式有错")
		return
	end
	
	return GetTaskDetailData(nUserId,nQuestId,nIndex)
end

-------------------------------------------2014.12.5
-- 获取玩家所有内功的内力总值
-- 参数说明：参1：idUser玩家ID，返回值：玩家的内力总值
function Get_InnerStrengthTotalValue(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_InnerStrengthTotalValue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetInnerStrengthTotalValue(nUserId)
end

-- 获取玩家指定内功类型的等级
-- 参数1：玩家ID，参数2：内功类型；返回值：对应的内功等级
function Get_InnerStrengthLevByType(nType,nUserId)
	if type(nType) ~= "number" or nType%1 ~= 0 or nType <= 0 then
		Sys_SaveAbnormalLog("函数 Get_InnerStrengthLevByType 中 nType 只能传大于0小于14的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_InnerStrengthLevByType 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetInnerStrengthLevByType(nUserId,nType)
end

-- 获取玩家修为值
function Get_UserCultureValue(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserCultureValue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_CultureValue)
end

--2015.01.08
--添加获取玩家当前已完成跨服任务的次数

--	// 获得当前已完成跨服任务次数，返回-1表示错误，否则返回具体的值
--	// 参数1: 玩家ID

function Get_CompleteOSTaskAmount(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_CompleteOSTaskAmount 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GetCompleteOSTaskAmount(nUserId)
end

--添加获取玩家每日可完成的跨服任务次数

--	// 获得每日可完成跨服任务次数，返回-1表示错误，否则返回具体的值
--	// 无参数

function Get_OSTaskMaxCompleteTimes()
	return GetOSTaskMaxCompleteTimes()
end

--------------------2014.12.16
-- 获得所在国金砖数
-- Int GetCountryBrickAmount();
-- 返回值：所在国金砖数
function Get_CountryBrickAmount()
	return GetCountryBrickAmount()
end



--------------2014.12.19
-- 以下几个接口只能在Event_Kill_User事件中使用
-- 获取目标玩家(被杀玩家)
-- // 获取目标玩家(被杀玩家)整形属性。 参数1：目标玩家ID（传0为默认目标玩家）， nIdx 使用的枚举和接口GetUserInt相同(不可使用这个接口获取被杀玩家ID)，失败返回-1，否则返回具体值.
function Get_TargetUserProfession(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TargetUserProfession 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTargetUserInt(nUserId,G_PLAYER_Profession)
end

--获取目标玩家(被杀玩家)名字，字符串型
function Get_TargetUserName(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_TargetUserName 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTargetUserStr(nUserId,G_PLAYER_Name)
end
--------------------------------------------------------------------------------------

--------------2015.7.6
-- 新增获取黄金联赛积分的接口
function Get_UserLeaguePoint(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLeaguePoint 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_League_Point)
end

-- 获取本服执政盟id
function Get_CountryId()
	return GetCountryId()
end

-- 获取玩家联盟id
function Get_UserLeagueId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLeagueId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_League_ID)
end

-- 获取玩家战功值
function Get_UserServiceValue(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserServiceValue 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetUserInt(nUserId,G_PLAYER_Service_Value)
end

----2015.07.24新添加接口

--获取玩家官职：
-- 用来获取玩家官职。该值用2进制表示玩家所有的官职，如果某位为1，表示玩家有该官职，否则没有。
-- 该数值从低位到高位依次表示国王，左宰相，右宰相，左元帅，右元帅，护国将军，卫国将军，镇邦将军，
-- 争夷将军，御林军，皇后（国夫），贵妃（贵君），佳人（相公）
function Get_UserLeagueOfficial(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLeagueOfficial 中 nUserId 只能传大等于0的整数")
		return
	end

	return GetUserInt(nUserId,G_PLAYER_League_Official)
end

-- 获取后宫嫔妃的接口
function Get_Harem(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_Harem 中 nUserId 只能传大等于0的整数")
		return
	end
	
	local nOfficial = Get_UserLeagueOfficial(nUserId)
	local nSex = Get_UserSex(nUserId)
	
	if Sys_ParseNumbersContain(4096,nOfficial) then
		local sOfficial = {tLuaRes[10022],tLuaRes[10021]}
		return sOfficial[nSex]
	elseif Sys_ParseNumbersContain(2048,nOfficial) then
		local sOfficial = {tLuaRes[10020],tLuaRes[10019]}
		return sOfficial[nSex]
	elseif Sys_ParseNumbersContain(1024,nOfficial) then
		local sOfficial = {tLuaRes[10018],tLuaRes[10017]}
		return sOfficial[nSex]
	end
end

-- 新增lua接口：
-- GetVexillumRank(int idSyn)
-- 参数1：帮派id，如果帮派传0，默认取当前玩家所在帮派id
-- 返回值：帮派在战旗赛中的名次(返回0表示第一名,名次要+1)
function Get_VexillumRank(nGuildId)
	if nGuildId == nil then
		nGuildId = 0
	elseif type(nGuildId) ~= "number" or nGuildId < 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_VexillumRank 中 nGuildId 只能传大等于0的整数")
		return
	end
	
	return GetVexillumRank(nGuildId)
end

--获取帮派等级
--参数说明：nGuildId指要操作的玩家帮派ID, nIdx指SCRIPT_PARAM_SYNDICATE_BEGIN-SCRIPT_PARAM_SYNDICATE_END的枚举值，如果失败返回"-1"，否则返回具体的值。
-- GetSynInt新增属性类型SCRIPT_PARAM_SYNDICATE_LEVEL=2837, 获取帮派等级
function Get_UserSynLevel(nGuildId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserSynLevel 中 nGuildId 只能大于0的整数")
		return
	end
	
	return GetSynInt(nGuildId,G_SYNDICATE_LEVEL)
end	

-- 获取被杀或者被锁魂玩家联盟id
function Get_TargetUserLeagueId(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLeagueId 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetTargetUserInt(nUserId,G_PLAYER_League_ID)
end

-- 新增lua接口：
-- GetBrickQuality(int idUser)
-- 参数1：玩家id，如果玩家id为0.默认取当前玩家id
-- 返回值：金砖品质从低到高分别为0-4，错误返回-1
function Get_BrickQuality(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_BrickQuality 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return GetBrickQuality(nUserId)
end