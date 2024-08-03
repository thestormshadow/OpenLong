----------------------------------------------------------------------------
--Name:		[征服][公用函数]怪物函数.lua
--Purpose:	怪物函数接口
--Creator: 	林锦
--Created:	2014/06/26
----------------------------------------------------------------------------
---------------------------------命名规范----------------------------------
------------------------------actiontype的分类------------------------------
--Sys  怪物所有
--Send 消息发送
--Get  获得属性
--Set  修改属性
--Chk  检查属性
--Del  删除属性
--Add  添加属性
------------------------------------------------------------------------------
-- 怪物函数命名前缀词：Monster_
--例子：
--bool DeleteMonster(OBJID idMap,OBJID idType, int nData, const char* pszName);

--function Monster_DelMonster(nMapId,nMonsterTypeId,nData,sMonsterName)
--
--end

------------------------------------------------------------------------------
--bool killMonsterDropItem(OBJID idUser, DWORD dwData);
--杀怪掉落物品
function Monster_SysDropItem(nItemtypeId,nUserId)
	if type(nItemtypeId) ~= "number" or nItemtypeId <= 0 or nItemtypeId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_SysDropItem 中 nItemtypeId 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_SysDropItem 中 nUserId 只能传大于等于0的整数")
		return
	end

	return killMonsterDropItem(nUserId,nItemtypeId)
end

--bool killMonsterDropMoney(OBJID idUser, DWORD dwData);
--杀怪掉落金币
function Monster_SysDropMoney(nMoney,nUserId)
	if type(nMoney) ~= "number" or nMoney <= 0 or nMoney%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_SysDropMoney 中 nMoney 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_SysDropMoney 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	return killMonsterDropMoney(nUserId,nMoney)
end


--bool killMonsterAddCulTation(OBJID idUser, int nLevFrom, int nLevTo ,__int64 i64AddCultivation);
function Monster_AddCultivation(nCultivation,nUserId)
	if type(nCultivation) ~= "number" or nCultivation <= 0 or nCultivation%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_AddCultivation 中 nCultivation 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_AddCultivation 中 nUserId 只能传大于等于0的整数")
		return
	end

	return killMonsterAddCulTation(nUserId,0,0,nCultivation)
end


--bool CreateMonster(int nOwnerType,OBJID idOwner, OBJID idMap, int nPosX, int nPosY, OBJID idGen, OBJID idType, int nData, const char* pszName);
--对应actiontype=2006，创建一个MONSTER。
--nOwnerType，idOwner，nData默认写0，不需要传参。
--pszName，怪物改名当前无效，不需要传参。
function Monster_AddMonster(nMapId,nPosX,nPosY,nGenId,nMonsterId)
	if type(nMapId) ~= "number" or nMapId <= 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_AddMonster 中 nMapId 只能传大于0的整数")
		return
	end

	if type(nPosX) ~= "number" or nPosX <= 0 or nPosX%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_AddMonster 中 nPosX 只能传大于0的整数")
		return
	end

	if type(nPosY) ~= "number" or nPosY <= 0 or nPosY%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_AddMonster 中 nPosY 只能传大于0的整数")
		return
	end
	
	if type(nGenId) ~= "number" or nGenId <= 0 or nGenId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_AddMonster 中 nGenId 只能传大于0的整数")
		return
	end

	if type(nMonsterId) ~= "number" or nMonsterId <= 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_AddMonster 中 nMonsterId 只能传大于0的整数")
		return
	end

	return CreateMonster(0,0,nMapId,nPosX,nPosY,nGenId,nMonsterId,0,"")
end


--int GetCountMonster(OBJID idMap,const char* pszField,const char* pszData);
--对应actiontype=2008，检查同地图的MONSTER数量。
--封装为2个函数，一个按名字查询，一个按generatorid查询。
function Monster_GetMonsterByName(nMapId,sMonsterName)
	if type(nMapId) ~= "number" or nMapId <= 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_GetMonsterByName 中 nMapId 只能传大于0的整数")
		return
	end

	if type(sMonsterName) ~= "string" then
		Sys_SaveAbnormalLog("函数 Monster_GetMonsterByName 中 sMonsterName 只能传字符串类型的参数")
		return
	end

	return GetCountMonster(nMapId,"name",sMonsterName)
end

function Monster_GetMonsterByGenId(nMapId,nGenId)
	if type(nMapId) ~= "number" or nMapId <= 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_GetMonsterByGenId 中 nMapId 只能传大于0的整数")
		return
	end

	if type(nGenId) ~= "number" or nGenId <= 0 or nGenId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_GetMonsterByGenId 中 nGenId 只能传大于0的整数")
		return
	end

	return GetCountMonster(nMapId,"gen_id",nGenId)
end


--bool DeleteMonster(OBJID idMap,OBJID idType, int nData, const char* pszName);
--对应actiontype=2009，param中的data和name参数可不用，不再封装。
--返回值为布尔型
function Monster_DelMonster(nMapId,nMonsterId)
	if type(nMapId) ~= "number" or nMapId <= 0 or nMapId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_DelMonster 中 nMapId 只能传大于0的整数")
		return
	end

	if type(nMonsterId) ~= "number" or nMonsterId <= 0 or nMonsterId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Monster_DelMonster 中 nMonsterId 只能传大于0的整数")
		return
	end

	return DeleteMonster(nMapId,nMonsterId,0,"")
end
