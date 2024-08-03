----------------------------------------------------------------------------
--Name:		[征服][公用函数]物品函数.lua
--Purpose:	物品函数接口
--Creator: 	林锦
--Created:	2014/06/26
----------------------------------------------------------------------------
---------------------------------命名规范----------------------------------
------------------------------actiontype的分类------------------------------
--Sys  物品所有
--Send 消息发送
--Get  获得属性
--Set  修改属性
--Chk  检查属性
--Del  删除属性
--Add  添加属性
------------------------------------------------------------------------------
-- 物品函数命名前缀词：Item_
--例子：
--(fn_DeleteItem, "DeleteItem");//删除物品，参1:玩家ID, 参2:物品类型ID, 参3:数量, 参4:是否判断物品为乾坤袋

--function Item_DelItem(nUserId,nItemtypeId,nNum,bSash)
--
--end

------------------------------------------------------------------------------

--(fn_DeleteTaskItem, "DeleteTaskItem");		//删除任务物品  		参1:玩家ID, 参2:物品ID
--nItemId --对应cq_item表的id
function Item_DelTaskItem(nItemId,nUserId)
	if type(nItemId) ~= "number" or  nItemId <= 0 or nItemId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数Item_DelTaskItem的nItemId必须为整数且大于0。")
		return
	end
	
	if nUserId == nil then 
       nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数Item_DelTaskItem的nUserId必须为整数且不小于0。")
		return
	end
	
	local nItemTypeId = Get_ItemType(nItemId)
	
	if Item_ChkItem(nItemTypeId) then
		return DeleteTaskItem(nUserId,nItemId)
	else
		Sys_SaveAbnormalLog(string.format("函数Item_DelTaskItem中玩家ID为%d背包里ID位%d的物品。",nUserId,nItemTypeId))
		return
	end
end

--(fn_DeleteItem, "DeleteItem");			//删除物品  			参1:玩家ID, 参2:物品类型ID, 参3:是否为赠品, 参4:是否判断物品为乾坤袋
--nItemNum,nSash为可选参数，默认值0
function Item_DelItem(nItemId,nMonopoly,nSash,nUserId)
	if type(nItemId) ~= "number" or  nItemId <= 0 or nItemId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数 Item_DelItem 的nItemId必须为整数且大于0。")
		return
	end
	
	if nMonopoly == nil then 
	   nMonopoly = 1
	elseif type(nMonopoly) ~= "number" or nMonopoly < 0 or nMonopoly%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Item_DelItem 的nMonopoly必须为整数且不小于0。")
		return
	end
	
	if nSash == nil then 
	   nSash = 0
	elseif type(nSash) ~= "number" or nSash < 0 or nSash%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Item_DelItem 的nSash必须为整数且不小于0。")
		return
	end
	
	if nUserId == nil then 
       nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数 Item_DelItem 的nUserId必须为整数且不小于0。")
		return
	end

	if Item_ChkItem(nItemId,nMonopoly,nSash,nUserId) then
		return DeleteItem(nUserId,nItemId,nMonopoly,nSash)
	else
		Sys_SaveAbnormalLog(string.format("函数DeleteItem中玩家ID为%d背包里没有物品%d。",nUserId,nItemId))
		return
	end
end

--(fn_DeleteMultiItem, "DeleteMultiItem");	//删除多个物品			参1:玩家ID, 参2:物品1类型ID, 参3:物品2类型ID, 参4:物品数量, 参5:能否使用赠品, 参6:是否判断物品为乾坤袋
--nMonopoly,nSash为可选参数，默认值0
function Item_DelMulItem(nStartItemId,nEndItemId,nItemNum,nMonopoly,nSash,nUserId)
	if type(nStartItemId) ~= "number" or nStartItemId <= 0 or nStartItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_DelMulItem的nStartItemId必须为整数且大于0。")
		return
	end
	
	if type(nEndItemId) ~= "number" or nEndItemId <= 0 or  tonumber(nEndItemId) < tonumber(nStartItemId) or nEndItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_DelMulItem的nEndItemId必须为大于0的整数,且要大于nStartItemId。")
		return
	end
	
	if type(nItemNum) ~= "number" or nItemNum <= 0 or nItemNum%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_DelMulItem的nItemNum必须为整数且大于0。")
		return
	end
	
	if nMonopoly == nil then 
	   nMonopoly = 1
	elseif type(nMonopoly) ~= "number" or nMonopoly < 0 or nMonopoly%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_DelMulItem的nMonopoly必须为整数且不小于0。")
		return
	end
	
	if nSash == nil then 
	   nSash = 0
	elseif type(nSash) ~= "number" or nSash < 0 or nSash%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_DelMulItem的nSash必须为整数且不小于0。")
		return
	end
	
	if nUserId == nil then 
       nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数Item_DelMulItem的nUserId必须为整数且不小于0。")
		return
	end
	
	if Item_ChkMulItem(nStartItemId,nEndItemId,nItemNum,nMonopoly,nSash,nUserId) then
		return DeleteMultiItem(nUserId,nStartItemId,nEndItemId,nItemNum,nMonopoly,nSash)
	else
		Sys_SaveAbnormalLog(string.format("函数DeleteMultiItem中玩家ID为%d背包里没有%d个ID为%d到%d的物品。",nUserId,nItemNum,nStartItemId,nEndItemId))
		return
	end
end

--(fn_DelAllItemByType, "DelAllItemByType");	//删除所有某类型物品		参1:玩家ID, 参2:物品类型,		失败返回false，否则返回true
--参数说明： nItemId ：表示物品ID， nUserId ：表示玩家ID
function Item_DelAllItemByType(nItemId,nUserId)
	if type(nItemId) ~= "number" then
		Sys_SaveAbnormalLog("函数 Item_DelAllItemByType 中 nItemId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" then
		Sys_SaveAbnormalLog("函数 Item_DelAllItemByType 中 nUserId 只能传大于0的整数")
		return
	end
	
	if Item_ChkItem(nItemId) then
		return DelAllItemByType(nUserId,nItemId)
	else
		Sys_SaveAbnormalLog(string.format("函数DelAllItemByType中玩家ID为%d背包里没有ID为%d的物品。",nUserId,nItemId))
		return
	end
end


--(fn_CheckItem, "CheckItem");			//检查物品  			参1:玩家ID, 参2:物品类型ID, 参3:能否使用赠品, 参4:是否判断物品为乾坤袋
--nMonopoly,nSash为可选参数，默认值0
function Item_ChkItem(nItemId,nMonopoly,nSash,nUserId)
	if type(nItemId) ~= "number" or  nItemId <= 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkItem的nItemId必须为整数且大于0。")
		return
	end
	
	if nMonopoly == nil then 
	   nMonopoly = 1
	elseif type(nMonopoly) ~= "number" or nMonopoly < 0 or nMonopoly%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkItem的nMonopoly必须为整数且不小于0。")
		return
	end
	
	if nSash == nil then 
	   nSash = 0
	elseif type(nSash) ~= "number" or nSash < 0 or nSash%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkItem的nSash必须为整数且不小于0。")
		return
	end
	
	if nUserId == nil then 
       nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数Item_ChkItem的nUserId必须为整数且不小于0。")
		return
	end
	
	return CheckItem(nUserId,nItemId,nMonopoly,nSash)
end

--(fn_CheckMultiItem, "CheckMultiItem");		//检查多个物品			参1:玩家ID, 参2:物品1类型ID, 参3:物品2类型ID, 参4:物品数量, 参5:能否使用赠品, 参6:是否判断物品为乾坤袋
--nMonopoly,nSash为可选参数，默认值0
function Item_ChkMulItem(nStartItemId,nEndItemId,nItemNum,nMonopoly,nSash,nUserId)
	if type(nStartItemId) ~= "number" or nStartItemId <= 0 or nStartItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkMulItem的nStartItemId必须为整数且大于0。")
		return
	end
	
	if type(nEndItemId) ~= "number" or nEndItemId <= 0 or nEndItemId%1 ~= 0 or tonumber(nEndItemId) < tonumber(nStartItemId) then
		Sys_SaveAbnormalLog("函数Item_ChkMulItem的nEndItemId必须为大于0的整数,且要大于nStartItemId。")
		return
	end
	
	if type(nItemNum) ~= "number" or nItemNum <= 0 or nItemNum%1 ~= 0 then
	Sys_SaveAbnormalLog("函数Item_ChkMulItem的nItemNum必须为整数且大于0。")
		return
	end

	if nMonopoly == nil then 
	   nMonopoly = 1
	elseif type(nMonopoly) ~= "number" or nMonopoly < 0 or nMonopoly%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkMulItem的nMonopoly必须为整数且不小于0。")
		return
	end
	
	if nSash == nil then 
	   nSash = 0
	elseif type(nSash) ~= "number" or nSash < 0 or nSash%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkMulItem的nSash必须为整数且不小于0。")
		return
	end
	
	if nUserId == nil then 
       nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数Item_ChkMulItem的nUserId必须为整数且不小于0。")
		return
	end
	
	return CheckMultiItem(nUserId,nStartItemId,nEndItemId,nItemNum,nMonopoly,nSash)
end

--(fn_CheckAccumulate, "CheckAccumulate");	//检查物品类型			参1:玩家ID, 参2:物品类型ID, 参3:数量, 参4:是否判断物品为乾坤袋
--nItemNum,nSash为可选参数，nItemNum默认值1,nSash默认值0
function Item_ChkAccItem(nItemId,nItemNum,nSash,nUserId)
	if type(nItemId) ~= "number" or  nItemId <= 0 or nItemId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkAccItem的nItemId必须为整数且大于0。")
		return
	end
	
	if nItemNum == nil then 
	   nItemNum = 1
	elseif type(nItemNum) ~= "number" or nItemNum < 0 or nItemNum%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkAccItem必须为整数且不小于0。")
		return
	end
	
	if nSash == nil then 
	   nSash = 0
	elseif type(nSash) ~= "number" or nSash < 0 or nSash%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_ChkAccItem的nSash必须为整数且不小于0。")
		return
	end
	
	if nUserId == nil then 
       nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
	    Sys_SaveAbnormalLog("函数Item_ChkAccItem的nUserId必须为整数且不小于0。")
		return
	end

	return CheckAccumulate(nUserId,nItemId,nItemNum,nSash)
end

--(fn_AddNewItem, "AddNewItem");			//新增物品
--						// 添加物品程序自动区分是否叠加。data=itemtype_id,param="flag addamount monopoly save_time active onlinetime data reduce_dmg add_life addlevel_exp magic3 gem1 gem2 magic1 magic2 amount amount_limit anti_monster ident color",
--						// param可省略，所有缺省值为0(表示不修改), flag表示标识位,标志位第0位为0则不检查背包物品添加否则检查,标志位第1位为0则不继承赠品属性物品添加，否则继承;
--						// addamount表示添加物品的个数，为0只添加一个,monopoly表示赠品装备品质,save_time表示物品的有效时间（单位为分钟）,active表示激活,onlinetime表示时效性物品的删除时间
--						// data表示用做马匹物品的客户端表现颜色的记录，数值是R*65536+G*256+B叠加的结果,reduce_dmg表示装备神佑属性（1~7）,add_life表示装备加持生命,addlevel_exp表示追加升级经验,
--						// magic3表示装备追加数值（1~12）,gem1表示第一个洞（如果给的是没有镶嵌宝石的洞，这里写255就好了，如果是要给镶嵌好指定宝石的洞，这里就要写宝石编号）,gem2表示第二个洞
--						// magic1表示装备所带的第一种魔法效果,magic2表示装备所带的第二个魔法效果,amount表示当前耐久,amount_limit表示耐久上限,anti_monster表示装备克制怪物属性
--						// ident表示是否鉴定,color表示装备颜色

function Item_AddItem(nItemId,flag,addamount,monopoly,save_time,active,onlinetime,data,reduce_dmg,add_life,addlevel_exp,magic3,gem1,gem2,magic1,magic2,amount,amount_limit,anti_monster,ident,color,nUserId)
	if type(nItemId) ~= "number" or  nItemId <= 0 or nItemId%1 ~= 0 then 
		Sys_SaveAbnormalLog("函数Item_AddItem的nItemId必须为整数且大于0。")
		return
	end	
	
	if flag == nil then 
	   flag = 0
	elseif flag~= 0 and flag~= 1 then
		Sys_SaveAbnormalLog("函数Item_AddItem的flag必须为0或1。")
		return
	end

	if addamount == nil then 
		addamount = 0
	elseif type(addamount) ~= "number" or addamount < 0 or addamount%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的addamount必须大于等于0。")
		return
	end
	
	if monopoly == nil then 
		monopoly = 0
	elseif type(monopoly) ~= "number" or monopoly < 0 or monopoly%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的monopoly必须为整数且不小于0。")
		return
	end
	
	if save_time == nil then 
		save_time = 0
	elseif type(save_time) ~= "number" or save_time < 0 or save_time%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的save_time必须大于等于0。")
		return
	end
	
	if active == nil then 
		active = 0
	elseif type(active) ~= "number" or active < 0 or active%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的active必须大于等于0。")
		return
	end
	
	if onlinetime == nil then 
		onlinetime = 0
	elseif type(onlinetime) ~= "number" or onlinetime < 0 or onlinetime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的onlinetime必须大于等于0。")
		return
	end	
	
	if data == nil then 
		data = 0
	elseif type(data) ~= "number" or data < 0 or data%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的第data必须大于等于0。")
		return
	end

	if reduce_dmg == nil then 
		reduce_dmg = 0
	elseif type(reduce_dmg) ~= "number" or reduce_dmg < 0 or reduce_dmg%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的reduce_dmg必须为大于0的整数。")
		return
	end	
	
	if add_life == nil then 
		add_life = 0
	elseif type(add_life) ~= "number" or add_life < 0 or add_life%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的add_life必须为大于等于0的整数。")
		return
	end		   
	
	if addlevel_exp == nil then 
		addlevel_exp = 0
	elseif type(addlevel_exp) ~= "number" or addlevel_exp < 0 or addlevel_exp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的addlevel_exp必须为大于等于0的整数。")
		return
	end	
	
	if magic3 == nil then 
		magic3 = 0
	elseif type(magic3) ~= "number" or magic3 < 0 or magic3%1 ~= 0 or magic3 > 12 then
		Sys_SaveAbnormalLog("函数Item_AddItem的magic3必须为0~12之间的整数。")
		return
	end	
	
	if gem1 == nil then 
		gem1 = 0
	elseif type(gem1) ~= "number" or gem1 < 0 or gem1%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的gem1必须为大于0的整数。")
		return
	end	
	
	if gem2 == nil then 
		gem2 = 0
	elseif type(gem2) ~= "number" or gem2 < 0 or gem2%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的gem2必须为大于0的整数。")
		return
	end	
	
	if magic1 == nil then 
		magic1 = 0
	elseif type(magic1) ~= "number" or magic1 < 0 or magic1%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的magic1必须为大于0的整数。")
		return
	end	
	
	if magic2 == nil then 
		magic2 = 0
	elseif type(magic2) ~= "number" or magic2 < 0 or magic2%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的magic2必须为大于0的整数。")
		return
	end	
	
	if amount == nil then 
		amount = 0
	elseif type(amount) ~= "number" or amount < 0 or amount%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的amount必须为大于0的整数。")
		return
	end		
	
	if amount_limit == nil then 
		amount_limit = 0
	elseif type(amount_limit) ~= "number" or amount_limit < 0 or amount_limit%1~= 0 or amount_limit < amount then
		Sys_SaveAbnormalLog("函数Item_AddItem的amount_limit必须为大于等于0的整数且要大于等于amount。")
		return
	end		
	
	if anti_monster == nil then 
		anti_monster = 0
	elseif type(anti_monster) ~= "number" or anti_monster < 0 or anti_monster%1~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的anti_monster必须大于等于0。")
		return
	end		
	
	if ident == nil then 
		ident = 0
	elseif type(ident) ~= "number" or ident < 0 or ident%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的ident必须大于等于0。")
		return
	end		
	
	if color == nil then 
		color = 0
	elseif type(color) ~= "number" or (color < 0 or color > 9) or color%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的color必须必须在0-9之间。")
		return
	end		
	
	if nItemId == 300000 then 
	   local color1 = tonumber(reduce_dmg) * 65536
	   local color2 = tonumber(add_life) * 256
	   local color3 = tonumber(anti_monster)
	   local ndata = tonumber(data)
		if color1 + color2 + color3 ~= ndata then
			Sys_SaveAbnormalLog("函数Item_AddItem的第data必须为reduce_dmg*65536+add_life*256+anti_monster。")
			return
		end
	end
	
	if nUserId == nil then 
		nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddItem的nUserId必须为整数且不小于0。")
		return
	end
	
	return AddNewItem(nUserId,nItemId,flag,addamount,monopoly,save_time,active,onlinetime,data,reduce_dmg,add_life,addlevel_exp,magic3,gem1,gem2,magic1,magic2,amount,amount_limit,anti_monster,ident,color)
end

--(fn_AddNewItem, "AddNewItem");			//新增物品
--和上面的函数功能相同，参数用法不同
--sItemAttr 类似action param用法。这样后面把属性配在表里可以比较一目了然。
--例子：Item_AddNewItem(1000000,"0 5 3")
function Item_AddNewItem(nItemId,sItemAttr,nUserId) 
	local tItemAttr = Sys_Split(sItemAttr," ")
	local flag  = tonumber(tItemAttr[1])
	local addamount = tonumber(tItemAttr[2])
	local monopoly = tonumber(tItemAttr[3])
	local save_time = tonumber(tItemAttr[4])
	local active = tonumber(tItemAttr[5])
	local onlinetime = tonumber(tItemAttr[6])
	local data = tonumber(tItemAttr[7])
	local reduce_dmg = tonumber(tItemAttr[8])
	local add_life = tonumber(tItemAttr[9])
	local addlevel_exp = tonumber(tItemAttr[10])
	local magic3 = tonumber(tItemAttr[11])
	local gem1 = tonumber(tItemAttr[12])
	local gem2 = tonumber(tItemAttr[13])
	local magic1 = tonumber(tItemAttr[14])
	local magic2 = tonumber(tItemAttr[15])
	local amount = tonumber(tItemAttr[16])
	local amount_limit = tonumber(tItemAttr[17])
	local anti_monster = tonumber(tItemAttr[18])
	local ident = tonumber(tItemAttr[19])
	local color = tonumber(tItemAttr[20])
	
	if type(nItemId) ~= "number" or  nItemId <= 0 or nItemId%1 ~= 0 then 
		Sys_SaveAbnormalLog("函数Item_AddNewItem的nItemId必须为整数且大于0。")
		return
	end	
	
	if flag == nil then 
		flag = 0
	elseif flag~= 0 and flag~= 1 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的flag必须为0或1。")
		return
	end

	if addamount == nil then 
		addamount = 0
	elseif type(addamount) ~= "number" or addamount < 0 or addamount%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的addamount必须大于等于0。")
		return
	end
	
	if monopoly == nil then 
		monopoly = 0
	elseif type(monopoly) ~= "number" or monopoly < 0 or monopoly%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的monopoly必须为整数且不小于0。")
		return
	end
	
	if save_time == nil then 
		save_time = 0
	elseif type(save_time) ~= "number" or save_time < 0 or save_time%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的save_time必须大于等于0。")
		return
	end
	
	if active == nil then 
		active = 0
	elseif type(active) ~= "number" or active < 0 or active%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的active必须大于等于0。")
		return
	end
	
	if onlinetime == nil then 
		onlinetime = 0
	elseif type(onlinetime) ~= "number" or onlinetime < 0 or onlinetime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的onlinetime必须大于等于0。")
		return
	end	
	
	if data == nil then 
		data = 0
	elseif type(data) ~= "number" or data < 0 or data%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的第data必须大于等于0。")
		return
	end

	if reduce_dmg == nil then 
		reduce_dmg = 0
	elseif type(reduce_dmg) ~= "number" or reduce_dmg < 0 or reduce_dmg%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的reduce_dmg必须为大于0的整数。")
		return
	end	
	
	if add_life == nil then 
		add_life = 0
	elseif type(add_life) ~= "number" or add_life < 0 or add_life%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的add_life必须为大于等于0的整数。")
		return
	end		   
	
	if addlevel_exp == nil then 
		addlevel_exp = 0
	elseif type(addlevel_exp) ~= "number" or addlevel_exp < 0 or addlevel_exp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的addlevel_exp必须为大于等于0的整数。")
		return
	end	
	
	if magic3 == nil then 
		magic3 = 0
	elseif type(magic3) ~= "number" or magic3 < 0 or magic3%1 ~= 0 or magic3 > 12 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的magic3必须为0~12之间的整数。")
		return
	end	
	
	if gem1 == nil then 
		gem1 = 0
	elseif type(gem1) ~= "number" or gem1 < 0 or gem1%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的gem1必须为大于0的整数。")
		return
	end	
	
	if gem2 == nil then 
		gem2 = 0
	elseif type(gem2) ~= "number" or gem2 < 0 or gem2%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的gem2必须为大于0的整数。")
		return
	end	
	
	if magic1 == nil then 
		magic1 = 0
	elseif type(magic1) ~= "number" or magic1 < 0 or magic1%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的magic1必须为大于0的整数。")
		return
	end	
	
	if magic2 == nil then 
		magic2 = 0
	elseif type(magic2) ~= "number" or magic2 < 0 or magic2%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的magic2必须为大于0的整数。")
		return
	end	
	
	if amount == nil then 
		amount = 0
	elseif type(amount) ~= "number" or amount < 0 or amount%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的amount必须为大于0的整数。")
		return
	end		
	
	if amount_limit == nil then 
		amount_limit = 0
	elseif type(amount_limit) ~= "number" or amount_limit < 0 or amount_limit%1~= 0 or amount_limit < amount then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的amount_limit必须为大于等于0的整数且要大于等于amount。")
		return
	end		
	
	if anti_monster == nil then 
		anti_monster = 0
	elseif type(anti_monster) ~= "number" or anti_monster < 0 or anti_monster%1~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的anti_monster必须大于等于0。")
		return
	end		
	
	if ident == nil then 
		ident = 0
	elseif type(ident) ~= "number" or ident < 0 or ident%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的ident必须大于等于0。")
		return
	end		
	
	if color == nil then 
		color = 0
	elseif type(color) ~= "number" or (color < 0 or color > 9) or color%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的color必须必须在0-9之间。")
		return
	end		
	
	if nItemId == 300000 then 
	   local color1 = tonumber(reduce_dmg) * 65536
	   local color2 = tonumber(add_life) * 256
	   local color3 = tonumber(anti_monster)
	   local ndata = tonumber(data)
		if color1 + color2 + color3 ~= ndata then
			Sys_SaveAbnormalLog("函数Item_AddNewItem的第data必须为reduce_dmg*65536+add_life*256+anti_monster。")
			return
		end
	end
	
	if nUserId == nil then 
		nUserId = 0
	elseif type(nUserId) ~= "number" or  nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数Item_AddNewItem的nUserId必须为整数且不小于0。")
		return
	end
	
	return AddNewItem(nUserId,nItemId,flag,addamount,monopoly,save_time,active,onlinetime,data,reduce_dmg,add_life,addlevel_exp,magic3,gem1,gem2,magic1,magic2,amount,amount_limit,anti_monster,ident,color)
end
-----------20140822---------

