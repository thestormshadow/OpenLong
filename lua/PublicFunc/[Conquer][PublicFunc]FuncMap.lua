----------------------------------------------------------------------------
--Name:		[征服][公用函数]地图函数.lua
--Purpose:	地图函数接口
--Creator: 	林锦
--Created:	2014/06/26
----------------------------------------------------------------------------
---------------------------------命名规范----------------------------------
------------------------------actiontype的分类------------------------------
--Sys  系统所有
--Send 消息发送
--Get  获得属性
--Set  修改属性
--Chk  检查属性
--Del  删除属性
--Add  添加属性
------------------------------------------------------------------------------
-- 地图函数命名前缀词：Map_
--例子：
--(fn_CountMapUser, "CountMapUser");//指定地图中的玩家人数,参1:地图ID, 参2:是否必须存活的玩家,参照 ACTION_MAP_MAPUSER  = 302

--function Map_CountMapUser(nMapId,nUserSurvival)
--
--end

------------------------------------------------------------------------------

--//ACTION_MAP_FIREWORKS 314,
--bool MapFireWorks(OBJID idUser);
--函数说明：	放焰火
--参数说明：	玩家ID
--返回值： 布尔值，成功 true，失败 false
function Map_FireWorks(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Map_FireWorks 第一个参数 nUserId 为整型并且大于等于0")
		return
	end
	
	return MapFireWorks(nUserId)
end

	
--(fn_CountMapUser, "CountMapUser");		//指定地图中的玩家人数				参1:地图ID, 参2:是否必须存活的玩家, 										参照 ACTION_MAP_MAPUSER  = 302
--函数说明：	指定地图中的玩家人数
--参数说明：	参1:地图ID, 参2:是否必须存活的玩家, 参数2只能是0或1
--返回值： 返回地图中的玩家人数
function Map_GetUserNum(nMapId, nUserSurvival)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_GetUserNum 中 nMapId 只能传大于0的整数")
		return
	end

	if type(nUserSurvival) ~= "number" or (nUserSurvival ~= 0 and nUserSurvival ~= 1) then
		Sys_SaveAbnormalLog("函数 Map_GetUserNum 中 nUserSurvival 的值只能为0或1")
		return
	end
	
	return CountMapUser(nMapId,nUserSurvival)
end

--(fn_BroadcastMapMsg, "BroadcastMapMsg");	//地图广播消息						参1:地图ID, 参2:内容,  														参照 ACTION_MAP_BROCASTMSG  = 303
--函数说明：	地图广播消息
--参数说明：	参1:地图ID, 参2:内容
--返回值： 布尔值，成功 true，失败 false
function Map_SendBroadcastMsg(nMapId, sMsg)
	if type(nMapId) ~= "number"  or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_SendBroadcastMsg 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(sMsg) ~= "string" then
		Sys_SaveAbnormalLog("函数 Map_SendBroadcastMsg 中 sMsg 只能为字符串类型")
		return
	end
	
	return BroadcastMapMsg(nMapId, sMsg)
end

--(fn_DropMapItem, "DropMapItem");		//地图指定坐标产生指定物品			参1:地图ID, 参2:X坐标, 参3:Y坐标, 参4:物品类型ID,							参照 ACTION_MAP_DROPITEM = 304
--函数说明：	地图指定坐标产生指定物品
--参数说明：	参1:地图ID, 参2:X坐标, 参3:Y坐标, 参4:物品类型ID,
--返回值： 布尔值，成功 true，失败 false
function Map_DropItem(nMapId, nPosX, nPosY, nItemId)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0  or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_DropItem 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nPosX) ~= "number" or nPosX%1 ~= 0  or nPosX < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropItem 中 nPosX 只能传大于0的整数")
		return
	end
	
	if type(nPosY) ~= "number" or nPosY%1 ~= 0  or nPosY < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropItem 中 nPosY 只能传大于0的整数")
		return
	end
	
	if type(nItemId) ~= "number" or nItemId%1 ~= 0  or nItemId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_DropItem 中 nItemId 只能传大于0的整数")
		return
	end
	
	return DropMapItem(nMapId, nPosX, nPosY, nItemId)
end

--(fn_CountMapMonster, "CountMapMonster");	//指定地图或当前地图的某区域内怪物数量 参1:地图ID, 参2:怪物类型ID, 参3:X坐标, 参4:Y坐标, 参5:X格子数, 参6:Y格子数   参照 ACTION_MAP_REGION_MONSTER= 307
--函数说明：	检查指定地图或当前地图的某区域内怪物数量
--参数说明：	参1:地图ID, 参2:怪物类型ID, 参3:X坐标, 参4:Y坐标, 参5:X格子数, 参6:Y格子数
--返回值： 返回指定地图或当前地图的某区域内怪物数量
function Map_GetMonsterNum(nMapId, nMonsterId, nPosX, nPosY, nCellx, nCelly)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0  or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_GetMonsterNum 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nMonsterId) ~= "number" or nMonsterId%1 ~= 0  or nMonsterId < 0 then
		Sys_SaveAbnormalLog("函数 Map_GetMonsterNum 中 nMonsterId 只能传大于0的整数")
		return
	end
	
	if type(nPosX) ~= "number" or nPosX%1 ~= 0   or nPosX < 0 then
		Sys_SaveAbnormalLog("函数 Map_GetMonsterNum 中 nPosX 只能传大于0的整数")
		return
	end
	
	if  type(nPosY) ~= "number" or nPosY%1 ~= 0  or nPosY < 0 then
		Sys_SaveAbnormalLog("函数 Map_GetMonsterNum 中 nPosY 只能传大于0的整数")
		return
	end
	
	if type(nCellx) ~= "number" or nCellx%1 ~= 0   or nCellx < 0 then
		Sys_SaveAbnormalLog("函数 Map_GetMonsterNum 中 nCellx 只能传大于0的整数")
		return
	end
	
	if type(nCelly) ~= "number" or nCelly%1 ~= 0  or nCelly < 0 then
		Sys_SaveAbnormalLog("函数 Map_GetMonsterNum 中 nCelly 只能传大于0的整数")
		return
	end
	
	return CountMapMonster(nMapId, nMonsterId, nPosX, nPosY, nCellx, nCelly)
end

--(fn_DropMultiItems, "DropMultiItems");		//地图批量产生指定物品				参1:地图ID, 参2:物品类型ID, 参3:X坐标, 参4:Y坐标, 参5:X格子数, 参6:Y格子数, 参7:数量, 参8:存在时间   参照 ACTION_MAP_DROP_MULTI_ITEMS = 308
--函数说明：	地图批量产生指定物品
--参数说明：	参1:地图ID, 参2:物品类型ID, 参3:X坐标, 参4:Y坐标, 参5:X格子数, 参6:Y格子数, 参7:数量, 参8:存在时间(单位是秒)
--返回值： 布尔值，成功 true，失败 false
function Map_DropMultiItems(nMapId, nItemId, nPosX, nPosY, nCellx, nCelly, nItemNum, nExistTime)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0  or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nItemId) ~= "number" or nItemId%1 ~= 0  or nItemId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nItemId 只能传大于0的整数")
		return
	end
	
	if type(nPosX) ~= "number" or nPosX%1 ~= 0   or nPosX < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nPosX 只能传大于0的整数")
		return
	end
	
	if  type(nPosY) ~= "number" or nPosY%1 ~= 0  or nPosY < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nPosY 只能传大于0的整数")
		return
	end
	
	if type(nCellx) ~= "number" or nCellx%1 ~= 0   or nCellx < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nCellx 只能传大于0的整数")
		return
	end
	
	if type(nCelly) ~= "number" or nCelly%1 ~= 0  or nCelly < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nCelly 只能传大于0的整数")
		return
	end
	
	if type(nItemNum) ~= "number" or nItemNum%1 ~= 0  or nItemNum < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nItemNum 只能传大于0的整数")
		return
	end
	
	if type(nExistTime) ~= "number" or nExistTime%1 ~= 0  or nExistTime < 0 then
		Sys_SaveAbnormalLog("函数 Map_DropMultiItems 中 nExistTime 只能传大于0的整数")
		return
	end
	
	return DropMultiItems(nMapId, nItemId, nPosX, nPosY, nCellx, nCelly, nItemNum, nExistTime)
end

--(fn_MapEffect, "MapEffect");			//在指定地图的指定地点显示地图特效	参1:地图ID, 参2:X坐标, 参3:Y坐标, 参4:特效									参照 ACTION_MAP_MAPEFFECT = 312
--函数说明：	在指定地图的指定地点显示地图特效
--参数说明：	参1:地图ID, 参2:X坐标, 参3:Y坐标, 参4:特效
--返回值： 布尔值，成功 true，失败 false
function Map_Effect(nMapId, nPosX, nPosY, sEffectName)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0  or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_Effect 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nPosX) ~= "number" or nPosX%1 ~= 0   or nPosX < 0 then
		Sys_SaveAbnormalLog("函数 Map_Effect 中 nPosX 只能传大于0的整数")
		return
	end
	
	if  type(nPosY) ~= "number" or nPosY%1 ~= 0  or nPosY < 0 then
		Sys_SaveAbnormalLog("函数 Map_Effect 中 nPosY 只能传大于0的整数")
		return
	end
	
	if type(sEffectName) ~= "string" then
		Sys_SaveAbnormalLog("函数 Map_Effect 中 sEffectName 只能为字符串类型")
		return
	end
	
	return MapEffect(nMapId, nPosX, nPosY, sEffectName)
end

--// 对应 ACTION_EVENT_MAPUSER_CHGMAP  = 2012,    // 把指定地图中的所有玩家切换到指定地图的指定地点, param="idOrgMap idTargetMap posx posy"
--// 把指定地图中的所有玩家切换到指定地图的指定地点. 参数说明: idMap表示所在地图, idTargetMap表示目标地图, nPosX, nPosY表示目标地图的x、y坐标. 如果失败返回false, 成功返回true.
--bool MapUserChgMap(int idMap, int idTargetMap, int nPosX, int nPosY);

--函数说明：	把指定地图中的所有玩家切换到指定地图的指定地点
--参数说明：	参数说明: idMap表示所在地图, idTargetMap表示目标地图, nPosX, nPosY表示目标地图的x、y坐标. 
--返回值： 布尔值，如果失败返回false, 成功返回true.
function Map_ChgUserPos(nMapId, nTargetMapId, nPosX, nPosY)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0  or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_ChgUserPos 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nTargetMapId) ~= "number" or nTargetMapId%1 ~= 0  or nTargetMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_ChgUserPos 中 nTargetMapId 只能传大于0的整数")
		return
	end
	
	if type(nPosX) ~= "number" or nPosX%1 ~= 0   or nPosX < 0 then
		Sys_SaveAbnormalLog("函数 Map_ChgUserPos 中 nPosX 只能传大于0的整数")
		return
	end
	
	if  type(nPosY) ~= "number" or nPosY%1 ~= 0  or nPosY < 0 then
		Sys_SaveAbnormalLog("函数 Map_ChgUserPos 中 nPosY 只能传大于0的整数")
		return
	end
	
	return MapUserChgMap(nMapId, nTargetMapId, nPosX, nPosY)
end


--// 对应 ACTION_EVENT_MAPUSER_EXEACTION  = 2013, // 地图中指定数量，随机挑选的玩家会执行指定的 ACTION, param = "idMap idAction data", data 为执行action的玩家数量，data 为 -1 时即所有玩家
--// 地图中指定数量的玩家会执行指定的函数. 参数说明: idMap表示所在地图, nData表示玩家数量, -1时即所有玩家, strLineFunc表示执行的函数. 如果失败返回false, 成功返回true.
--bool MapUserExeFunc(int idMap, int nData, string strLineFunc);

--函数说明：	地图中指定数量，随机挑选的玩家会执行指定的
--参数说明: idMap表示所在地图, nData表示玩家数量, -1时即所有玩家, strLineFunc表示执行的函数
--返回值： 布尔值，成功 true，失败 false
function Map_UserExeFunc(nMapId, nData, sFuncName)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0  or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_UserExeFunc 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0  then
		Sys_SaveAbnormalLog("函数 Map_UserExeFunc 中 nData 只能传整数")
		return
	end
	
	if type(sFuncName) ~= "string" then
		Sys_SaveAbnormalLog("函数 Map_UserExeFunc 中 sFuncName 只能为字符串类型")
		return
	end
	
	return MapUserExeFunc(nMapId,nData,string.format("</F>%s",sFuncName))
end

--// 地图相关, 对应 type = 311, type = 315, type = 332.
--// 修改玩家地图的亮度. 参数说明: idMap表示地图ID, dwRGB表示亮度(FFFFFFFF表示恢复). 如果失败返回false, 成功返回true.
--bool MapChangeLight(int idMap, int dwRGB);
--函数说明：	修改玩家地图的亮度
--参数说明：	idMap表示地图ID, dwRGB表示亮度(FFFFFFFF表示恢复) 备注：F为16进制数，需要转成10进制数传给服务端
--返回值： 布尔值，成功 true，失败 false
function Map_ChangeLight(nMapId, nDwRGB)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0  or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Map_ChangeLight 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nDwRGB) ~= "number" or nDwRGB%1 ~= 0  then
		Sys_SaveAbnormalLog("函数 Map_ChangeLight 中 nDwRGB 只能传整数")
		return
	end

	return MapChangeLight(nMapId, nDwRGB)
end

--// 放文字焰火. 参数说明: idUser表示玩家ID, pszWords表示文字. 如果失败返回false, 成功返回true.
--bool MapFireWorks2(int idUser, string pszWords);
--函数说明：	放文字焰火
--参数说明：	idUser表示玩家ID, pszWords表示文字
--返回值： 布尔值，成功 true，失败 false
function Map_FireWorks2(sEffectWords,nUserId)	
	if type(sEffectWords) ~= "string" then
		Sys_SaveAbnormalLog("函数 Map_FireWorks2 中 sEffectWords 只能为字符串类型")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Map_FireWorks2 第一个参数 nUserId 为整型并且大于等于0")
		return
	end

	return MapFireWorks2(nUserId, sEffectWords)
end