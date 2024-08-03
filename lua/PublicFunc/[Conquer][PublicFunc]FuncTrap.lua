----------------------------------------------------------------------------
--Name:		[征服][公用函数]陷阱函数.lua
--Purpose:	陷阱函数接口
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
-- 陷阱函数命名前缀词：Trap_
--例子：


------------------------------------------------------------------------------
--bug：经测试type=330中配置的action无法触发，所以函数中配置的触发执行的函数也无法触发
--创建传送阵
--CreateTransportor
--参数说明:参1:表示玩家id或Npc id,参2:为0时表示绝对位置创建（根据指定坐标创建）, 为1时表示相对位置创建（玩家站在那里则创建在哪里）,参3:地图id,参4:x坐标,参5:y坐标,参6:半径,参7:传送阵的光效,参8:触发的函数
--成功返回true创建传送阵，失败返回false

-- function Trap_CreateTransportor(nRole,nFlag,nMapId,nPosX,nPosY,nRadius,sEffectTile,fLuaFunc)		
	-- if type(nRole) ~= "number" or nRole%1 ~= 0 or nRole < 0 then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 nRole 只能传大于等于0的整数")
		-- return
	-- end
	
	-- if nFlag ~= 0 and nFlag ~= 1 then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 nFlag 只能取0或1")
		-- return
	-- end
	
	-- if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId < 0 then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 nMapId 只能传大于等于0的整数")
		-- return
	-- end
	
	-- if type(nPosX) ~= "number" or nPosX%1 ~= 0 or nPosX < 0 then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 nPosX 只能传大于等于0的整数")
		-- return
	-- end
	
	-- if type(nPosY) ~= "number" or nPosY%1 ~= 0 or nPosY < 0 then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 nPosY 只能传大于等于0的整数")
		-- return
	-- end
	
	-- if type(nRadius) ~= "number" or nRadius%1 ~= 0 or nRadius < 0 then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 nRadius 只能传大于等于0的整数")
		-- return
	-- end
	
	-- if type(sEffectTile) ~= "string" then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 sEffectTile 只能是字符串")
		-- return
	-- end
		
	-- if type(fLuaFunc) ~= "string" then
		-- Sys_SaveAbnormalLog("函数 Trap_CreateTransportor 中 fLuaFunc 只能是字符串")
		-- return
	-- end

	-- return CreateTransportor(nRole,nFlag,nMapId,nPosX,nPosY,nRadius,sEffectTile,string.format("</F>%s",fLuaFunc))
-- end

--删除传送阵（人必须站在传送阵上否则删除会失败（不站在传送阵范围内上取不到传送阵的ID））
--DelTransportor
--参数说明: 参1:玩家ID（0为当前玩家ID）
--如果成功返回true删除传送阵,失败返回false

function Trap_DelTransportor(nUserId)	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Trap_DelTransportor 中 nUserId 只能是大于等于0的整数")
		return
	end

	local nTransportorId = Get_TransferId(nUserId)
	return DelTransportor(nTransportorId)
end


--创建陷阱
--CreateMapTrap
--参数说明: 参1:陷阱类型,参2:外观,参3:所属者;参4:地图id,参5:x坐标,参6:y坐标,nData:暂未使用(默认设为0);参7,参8:表示范围.
--如果失败返回false, 成功返回true.

function Trap_CreateMapTrap(nType,nLook,nOwnerId,nMapId,nPosX,nPosY,nPosCX,nPosCY)	

	if type(nType) ~= "number" or  nType%1 ~= 0 or nType <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nType 只能传大于0的整数")
		return
	end
	
	if type(nLook) ~= "number" or nLook%1 ~= 0 or nLook < 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nLook 只能传大于等于0的整数")
		return
	end
	
	if type(nOwnerId) ~= "number" or nOwnerId%1 ~= 0 or nOwnerId < 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nOwnerId 只能传大于等于0的整数")
		return
	end
	
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nPosX) ~= "number" or nPosX%1 ~= 0 or nPosX <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nPosX 只能传大于等于0的整数")
		return
	end
	
	if type(nPosY) ~= "number" or nPosY%1 ~= 0 or nPosY <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nPosY 只能传大于等于0的整数")
		return
	end
	
	if type(nPosCX) ~= "number" or nPosCX%1 ~= 0 or nPosCX < 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nPosCX 只能传大于等于0的整数")
		return
	end
	
	if type(nPosCY) ~= "number" or nPosCY%1 ~= 0 or nPosCY < 0 then
		Sys_SaveAbnormalLog("函数 Trap_CreateMapTrap 中 nPosCY 只能传大于等于0的整数")
		return
	end
	
	return CreateMapTrap(nType,nLook,nOwnerId,nMapId,nPosX,nPosY,0,nPosCX,nPosCY)
end

--删除一个陷阱.  
--EraseMapTrap
--参数说明:参1:表示陷阱id.（如果是触发形式，是可以传0.参数为0时表示删除自身，非触发形式，要指出陷阱的id）
--成功返回true删除陷阱,如果失败返回false

function Trap_EraseMapTrap(nTrapId)
	if nTrapId == nil then
		nTrapId = 0
	elseif type(nTrapId) ~= "number" or nTrapId%1 ~= 0 or nTrapId < 0 then
		Sys_SaveAbnormalLog("函数 Trap_EraseMapTrap 中 nTrapId 只能是大于等于0的整数")
		return
	end
	
	return EraseMapTrap(nTrapId)
end



--删除同类型的陷阱
--DelMapTrap
--参数说明: 参1:地图id,参2:陷阱类型.
--成功返回true删除同类型的陷阱,失败返回false

function Trap_DelMapTrap(nMapId,nType)	
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_DelMapTrap 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nType) ~= "number" or nType%1 ~= 0 or nType <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_DelMapTrap 中 nType 只能传大于0的整数")
		return
	end
	
	return DelMapTrap(nMapId,nType)
end




--目前只允许修改type 和look 暂时屏蔽其他
--修改的陷阱属性需要玩家离开陷阱一屏后再回来,修改的属性才会被刷新（如果没刷新:拿修改外观举例,修改后的外观会和修改前的同时存在）

--修改陷阱的type属性（不存盘）
--ChangeMapTrapAttr
--参数说明:参1:陷阱ID,参2:修改的值（需指定id来修改对应的陷阱属性）
--成功返回true并修改type属性,如果失败返回false

function Trap_ChangeTrapType(nTrapId,nData)
	if type(nTrapId) ~= "number" or nTrapId%1 ~= 0 or nTrapId <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_ChangeTrapType 中 nTrapId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData%1 ~= 0 or nData <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_ChangeTrapType 中 nData 只能传大于0的整数")
		return
	end
	
	return ChangeMapTrapAttr(nTrapId,G_TRAP_TYPE,nData)
end


--修改陷阱的look属性（即外观）（不存盘）
--ChangeMapTrapAttr
--参数说明:参1:陷阱ID,参2:修改的值（需指定id来修改对应的陷阱属性）
--成功返回true并修改look属性,如果失败返回false

function Trap_ChangeTrapLook(nTrapId,nData)
	if type(nTrapId) ~= "number" or nTrapId%1 ~= 0 or nTrapId <= 0 then
		Sys_SaveAbnormalLog("函数 Trap_ChangeTrapLook 中 nTrapId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Trap_ChangeTrapLook 中 nData 只能传大于等于0的整数")
		return
	end

	return ChangeMapTrapAttr(nTrapId,G_TRAP_LOOK,nData)
end
