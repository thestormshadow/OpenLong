----------------------------------------------------------------------------
--Name:		[征服][功能脚本]移动NPC功能.lua
--Purpose:	移动NPC功能
--Creator: 	郑鋆
--Created:	2015/02/06
----------------------------------------------------------------------------

-- 移动NPC功能命名前缀词： 
-- MoveNpc_

local MoveNpc_NpcInfo = {}
	-- MoveNpc_NpcInfo[1] = {}
	-- MoveNpc_NpcInfo[1]["ActivetyTime"] = "2015-01-15 00:00 2015-02-05 23:59"	--活动时间
	-- MoveNpc_NpcInfo[1]["NpcId"] = 7000											--NPCID
	-- MoveNpc_NpcInfo[1]["ActivetyMapId"] = 1002									--活动时间内NPC所在的地图
	-- MoveNpc_NpcInfo[1]["ActivetyPosX"] = 212									--活动时间内坐标
	-- MoveNpc_NpcInfo[1]["ActivetyPosY"] = 212									--活动时间内坐标
	-- MoveNpc_NpcInfo[1]["AfterActivetyMapId"] = 5000								--活动时间外地图
	-- MoveNpc_NpcInfo[1]["AfterActivetyPosX"] = 100								--活动时间内坐标
	-- MoveNpc_NpcInfo[1]["AfterActivetyPosY"] = 100								--活动时间内坐标

------------------------------------------------------------------------------------
-- 150716[英文征服][活动脚本]8月团部争霸活动(9.17-9.30)  
-- SQL BY:王倩娜                                         
-- DATE:2015-07-16                                       
------------------------------------------------------------------------------------
	MoveNpc_NpcInfo[18290] = {}
	MoveNpc_NpcInfo[18290]["ActivetyTime"] = "2015-09-22 00:00 2015-10-05 23:59"
	MoveNpc_NpcInfo[18290]["NpcId"] = 18290
	MoveNpc_NpcInfo[18290]["ActivetyMapId"] = 1002
	MoveNpc_NpcInfo[18290]["ActivetyPosX"] = 315
	MoveNpc_NpcInfo[18290]["ActivetyPosY"] = 250
	MoveNpc_NpcInfo[18290]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[18290]["AfterActivetyPosX"] = 99
	MoveNpc_NpcInfo[18290]["AfterActivetyPosY"] = 99

	MoveNpc_NpcInfo[18291] = {}
	MoveNpc_NpcInfo[18291]["ActivetyTime"] = "2015-09-22 00:00 2015-10-05 23:59"
	MoveNpc_NpcInfo[18291]["NpcId"] = 18291
	MoveNpc_NpcInfo[18291]["ActivetyMapId"] = 1002
	MoveNpc_NpcInfo[18291]["ActivetyPosX"] = 321
	MoveNpc_NpcInfo[18291]["ActivetyPosY"] = 250
	MoveNpc_NpcInfo[18291]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[18291]["AfterActivetyPosX"] = 99
	MoveNpc_NpcInfo[18291]["AfterActivetyPosY"] = 99

	MoveNpc_NpcInfo[18387] = {}
	MoveNpc_NpcInfo[18387]["ActivetyTime"] = "2015-09-22 00:00 2015-10-05 23:59"
	MoveNpc_NpcInfo[18387]["NpcId"] = 18387
	MoveNpc_NpcInfo[18387]["ActivetyMapId"] = 1002
	MoveNpc_NpcInfo[18387]["ActivetyPosX"] = 315
	MoveNpc_NpcInfo[18387]["ActivetyPosY"] = 250
	MoveNpc_NpcInfo[18387]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[18387]["AfterActivetyPosX"] = 99
	MoveNpc_NpcInfo[18387]["AfterActivetyPosY"] = 99
	
	MoveNpc_NpcInfo[18388] = {}
	MoveNpc_NpcInfo[18388]["ActivetyTime"] = "2015-09-22 00:00 2015-10-05 23:59"
	MoveNpc_NpcInfo[18388]["NpcId"] = 18388
	MoveNpc_NpcInfo[18388]["ActivetyMapId"] = 1002
	MoveNpc_NpcInfo[18388]["ActivetyPosX"] = 315
	MoveNpc_NpcInfo[18388]["ActivetyPosY"] = 250
	MoveNpc_NpcInfo[18388]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[18388]["AfterActivetyPosX"] = 99
	MoveNpc_NpcInfo[18388]["AfterActivetyPosY"] = 99	

------------------------------------------------------------------------------------
--150529[英文征服][活动脚本]6月活动1-促销(6.25-7.09)
--SQL BY:许乐
--DATE:2015-05-29
------------------------------------------------------------------------------------
	--18532  EquipmentDealer
	-- MoveNpc_NpcInfo[18532] = {}
	-- MoveNpc_NpcInfo[18532]["ActivetyTime"] = "2015-06-25 00:00 2015-07-09 23:59"
	-- MoveNpc_NpcInfo[18532]["NpcId"] = 18532
	-- MoveNpc_NpcInfo[18532]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18532]["ActivetyPosX"] = 317
	-- MoveNpc_NpcInfo[18532]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18532]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18532]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18532]["AfterActivetyPosY"] = 99
	
	--18533   ItemDealer
	-- MoveNpc_NpcInfo[18533] = {}
	-- MoveNpc_NpcInfo[18533]["ActivetyTime"] = "2015-06-25 00:00 2015-07-09 23:59"
	-- MoveNpc_NpcInfo[18533]["NpcId"] = 18533
	-- MoveNpc_NpcInfo[18533]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18533]["ActivetyPosX"] = 323
	-- MoveNpc_NpcInfo[18533]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18533]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18533]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18533]["AfterActivetyPosY"] = 99

	
------------------------------------------------------------------------------------
--150624[英文征服][活动脚本]双子圣衣活动头盔升级(6.27-7.09)
--SQL BY:吴文鑫
--DATE:2015-06-24
------------------------------------------------------------------------------------
    --18666  暗黑魔龙战衣【炼狱版】兑换商
	-- MoveNpc_NpcInfo[18666] = {}
	-- MoveNpc_NpcInfo[18666]["ActivetyTime"] = "2015-06-27 00:00 2015-07-09 23:59"
	-- MoveNpc_NpcInfo[18666]["NpcId"] = 18666
	-- MoveNpc_NpcInfo[18666]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18666]["ActivetyPosX"] = 227
	-- MoveNpc_NpcInfo[18666]["ActivetyPosY"] = 231
	-- MoveNpc_NpcInfo[18666]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18666]["AfterActivetyPosX"] = 50
	-- MoveNpc_NpcInfo[18666]["AfterActivetyPosY"] = 50

------------------------------------------------------------------------------------
--150611[英文征服][活动脚本]7月份暑期促销之一四部分(7.9-7.22)
--SQL BY:许乐
--DATE:2015-06-11
------------------------------------------------------------------------------------
	--18645  豪华礼包促销商
	-- MoveNpc_NpcInfo[18645] = {}
	-- MoveNpc_NpcInfo[18645]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18645]["NpcId"] = 18645
	-- MoveNpc_NpcInfo[18645]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18645]["ActivetyPosX"] = 319
	-- MoveNpc_NpcInfo[18645]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18645]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18645]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18645]["AfterActivetyPosY"] = 99
	
	--18646   装备促销商
	-- MoveNpc_NpcInfo[18646] = {}
	-- MoveNpc_NpcInfo[18646]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18646]["NpcId"] = 18646
	-- MoveNpc_NpcInfo[18646]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18646]["ActivetyPosX"] = 333
	-- MoveNpc_NpcInfo[18646]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18646]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18646]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18646]["AfterActivetyPosY"] = 99
	
	
	--18647  豪华礼包促销商怀旧服
	-- MoveNpc_NpcInfo[18647] = {}
	-- MoveNpc_NpcInfo[18647]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18647]["NpcId"] = 18647
	-- MoveNpc_NpcInfo[18647]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18647]["ActivetyPosX"] = 319
	-- MoveNpc_NpcInfo[18647]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18647]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18647]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18647]["AfterActivetyPosY"] = 99
	
	--18648   装备促销商怀旧服
	-- MoveNpc_NpcInfo[18648] = {}
	-- MoveNpc_NpcInfo[18648]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18648]["NpcId"] = 18648
	-- MoveNpc_NpcInfo[18648]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18648]["ActivetyPosX"] = 333
	-- MoveNpc_NpcInfo[18648]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18648]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18648]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18648]["AfterActivetyPosY"] = 99
	
------------------------------------------------------------------------------------
--150617[英文征服][活动脚本]7月份暑期促销(二五部分)(7.9-7.22)
--SQL BY:陈莺
--DATE:2015-06-17
------------------------------------------------------------------------------------
--18656 大转盘娱乐大使
	-- MoveNpc_NpcInfo[18656] = {}
	-- MoveNpc_NpcInfo[18656]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18656]["NpcId"] = 18656
	-- MoveNpc_NpcInfo[18656]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18656]["ActivetyPosX"] = 314
	-- MoveNpc_NpcInfo[18656]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18656]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18656]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18656]["AfterActivetyPosY"] = 100
	
--18657 优质礼包促销商
	-- MoveNpc_NpcInfo[18657] = {}
	-- MoveNpc_NpcInfo[18657]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"	--活动时间
	-- MoveNpc_NpcInfo[18657]["NpcId"] = 18657										--NPCID
	-- MoveNpc_NpcInfo[18657]["ActivetyMapId"] = 1002									--活动时间内NPC所在的地图
	-- MoveNpc_NpcInfo[18657]["ActivetyPosX"] = 338									--活动时间内坐标
	-- MoveNpc_NpcInfo[18657]["ActivetyPosY"] = 249									--活动时间内坐标
	-- MoveNpc_NpcInfo[18657]["AfterActivetyMapId"] = 5000								--活动时间外地图
	-- MoveNpc_NpcInfo[18657]["AfterActivetyPosX"] = 100								--活动时间内坐标
	-- MoveNpc_NpcInfo[18657]["AfterActivetyPosY"] = 100
	
--18668 优质礼包促销商怀旧服
	-- MoveNpc_NpcInfo[18668] = {}
	-- MoveNpc_NpcInfo[18668]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"	--活动时间
	-- MoveNpc_NpcInfo[18668]["NpcId"] = 18668										--NPCID
	-- MoveNpc_NpcInfo[18668]["ActivetyMapId"] = 1002									--活动时间内NPC所在的地图
	-- MoveNpc_NpcInfo[18668]["ActivetyPosX"] = 338									--活动时间内坐标
	-- MoveNpc_NpcInfo[18668]["ActivetyPosY"] = 249									--活动时间内坐标
	-- MoveNpc_NpcInfo[18668]["AfterActivetyMapId"] = 5000								--活动时间外地图
	-- MoveNpc_NpcInfo[18668]["AfterActivetyPosX"] = 100								--活动时间内坐标
	-- MoveNpc_NpcInfo[18668]["AfterActivetyPosY"] = 100
	
	
------------------------------------------------------------------------------------
---NAME  :150611[英文征服][活动脚本]7月份暑期促销之三六部分(7.9-7.22)
---SQL BY:兰瑞妹
---DATE  :2015-06-11
------------------------------------------------------------------------------------
	--普通礼包促销商
	-- MoveNpc_NpcInfo[18650] = {}
	-- MoveNpc_NpcInfo[18650]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18650]["NpcId"] = 18650
	-- MoveNpc_NpcInfo[18650]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18650]["ActivetyPosX"] = 324
	-- MoveNpc_NpcInfo[18650]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18650]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18650]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18650]["AfterActivetyPosY"] = 99
	
	--普通礼包促销商怀旧服
	-- MoveNpc_NpcInfo[18667] = {}
	-- MoveNpc_NpcInfo[18667]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18667]["NpcId"] = 18667
	-- MoveNpc_NpcInfo[18667]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18667]["ActivetyPosX"] = 324
	-- MoveNpc_NpcInfo[18667]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18667]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18667]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18667]["AfterActivetyPosY"] = 99

	--装备促销商
	-- MoveNpc_NpcInfo[18651] = {}
	-- MoveNpc_NpcInfo[18651]["ActivetyTime"] = "2015-07-10 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18651]["NpcId"] = 18651
	-- MoveNpc_NpcInfo[18651]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18651]["ActivetyPosX"] = 343
	-- MoveNpc_NpcInfo[18651]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18651]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18651]["AfterActivetyPosX"] = 99
	-- MoveNpc_NpcInfo[18651]["AfterActivetyPosY"] = 99
	

------------------------------------------------------------------------------------
---150610[简体征服][活动脚本]2015暑期活动-VIP气氛布置
---SQL BY:郑鋆
---DATE:2015-06-10
------------------------------------------------------------------------------------
	-- MoveNpc_NpcInfo[18598] = {}
	-- MoveNpc_NpcInfo[18598]["ActivetyTime"] = "2015-07-09 00:00 2015-07-22 23:59"
	-- MoveNpc_NpcInfo[18598]["NpcId"] = 18598
	-- MoveNpc_NpcInfo[18598]["ActivetyMapId"] = 1015
	-- MoveNpc_NpcInfo[18598]["ActivetyPosX"] = 724
	-- MoveNpc_NpcInfo[18598]["ActivetyPosY"] = 573
	-- MoveNpc_NpcInfo[18598]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18598]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18598]["AfterActivetyPosY"] = 100

------------------------------------------------------------------------------------
---150925[英文征服][活动脚本]国庆金币促销（10.1-10.7）
---SQL BY:张世超
---DATE:2015-09-25
------------------------------------------------------------------------------------
	MoveNpc_NpcInfo[18858] = {}
	MoveNpc_NpcInfo[18858]["ActivetyTime"] = "2015-10-01 00:00 2015-10-07 23:59"
	MoveNpc_NpcInfo[18858]["NpcId"] = 18858
	MoveNpc_NpcInfo[18858]["ActivetyMapId"] = 1002
	MoveNpc_NpcInfo[18858]["ActivetyPosX"] = 300
	MoveNpc_NpcInfo[18858]["ActivetyPosY"] = 290
	MoveNpc_NpcInfo[18858]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[18858]["AfterActivetyPosX"] = 100
	MoveNpc_NpcInfo[18858]["AfterActivetyPosY"] = 100

	MoveNpc_NpcInfo[18859] = {}
	MoveNpc_NpcInfo[18859]["ActivetyTime"] = "2015-10-01 00:00 2015-10-07 23:59"
	MoveNpc_NpcInfo[18859]["NpcId"] = 18859
	MoveNpc_NpcInfo[18859]["ActivetyMapId"] = 1858
	MoveNpc_NpcInfo[18859]["ActivetyPosX"] = 78
	MoveNpc_NpcInfo[18859]["ActivetyPosY"] = 74
	MoveNpc_NpcInfo[18859]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[18859]["AfterActivetyPosX"] = 100
	MoveNpc_NpcInfo[18859]["AfterActivetyPosY"] = 100
	
	
---------------------------------------------------------------------------------------
---150724[英文征服][活动脚本]9月份大促销(9.03-9.16)
---SQL BY:陈莺                                     
---DATE:2015-07-24                                 
---------------------------------------------------------------------------------------
	-- MoveNpc_NpcInfo[18782] = {}
	-- MoveNpc_NpcInfo[18782]["ActivetyTime"] = "2015-11-01 00:00 2015-11-11 23:59"
	-- MoveNpc_NpcInfo[18782]["NpcId"] = 18782
	-- MoveNpc_NpcInfo[18782]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18782]["ActivetyPosX"] = 319
	-- MoveNpc_NpcInfo[18782]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18782]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18782]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18782]["AfterActivetyPosY"] = 100

	-- MoveNpc_NpcInfo[18783] = {}
	-- MoveNpc_NpcInfo[18783]["ActivetyTime"] = "2015-11-01 00:00 2015-11-11 23:59"
	-- MoveNpc_NpcInfo[18783]["NpcId"] = 18783
	-- MoveNpc_NpcInfo[18783]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18783]["ActivetyPosX"] = 338
	-- MoveNpc_NpcInfo[18783]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18783]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18783]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18783]["AfterActivetyPosY"] = 100

	-- MoveNpc_NpcInfo[18784] = {}
	-- MoveNpc_NpcInfo[18784]["ActivetyTime"] = "2015-11-01 00:00 2015-11-11 23:59"
	-- MoveNpc_NpcInfo[18784]["NpcId"] = 18784
	-- MoveNpc_NpcInfo[18784]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18784]["ActivetyPosX"] = 333
	-- MoveNpc_NpcInfo[18784]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18784]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18784]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18784]["AfterActivetyPosY"] = 100

	-- MoveNpc_NpcInfo[18785] = {}
	-- MoveNpc_NpcInfo[18785]["ActivetyTime"] = "2015-11-01 00:00 2015-11-11 23:59"
	-- MoveNpc_NpcInfo[18785]["NpcId"] = 18785
	-- MoveNpc_NpcInfo[18785]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18785]["ActivetyPosX"] = 324
	-- MoveNpc_NpcInfo[18785]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18785]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18785]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18785]["AfterActivetyPosY"] = 100

	-- MoveNpc_NpcInfo[18793] = {}
	-- MoveNpc_NpcInfo[18793]["ActivetyTime"] = "2015-11-01 00:00 2015-11-11 23:59"
	-- MoveNpc_NpcInfo[18793]["NpcId"] = 18793
	-- MoveNpc_NpcInfo[18793]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18793]["ActivetyPosX"] = 319
	-- MoveNpc_NpcInfo[18793]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18793]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18793]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18793]["AfterActivetyPosY"] = 100

	-- MoveNpc_NpcInfo[18794] = {}
	-- MoveNpc_NpcInfo[18794]["ActivetyTime"] = "2015-11-01 00:00 2015-11-11 23:59"
	-- MoveNpc_NpcInfo[18794]["NpcId"] = 18794
	-- MoveNpc_NpcInfo[18794]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18794]["ActivetyPosX"] = 338
	-- MoveNpc_NpcInfo[18794]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18794]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18794]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18794]["AfterActivetyPosY"] = 100
	
	
	-- MoveNpc_NpcInfo[18796] = {}
	-- MoveNpc_NpcInfo[18796]["ActivetyTime"] = "2015-11-01 00:00 2015-11-11 23:59"
	-- MoveNpc_NpcInfo[18796]["NpcId"] = 18796
	-- MoveNpc_NpcInfo[18796]["ActivetyMapId"] = 1002
	-- MoveNpc_NpcInfo[18796]["ActivetyPosX"] = 338
	-- MoveNpc_NpcInfo[18796]["ActivetyPosY"] = 249
	-- MoveNpc_NpcInfo[18796]["AfterActivetyMapId"] = 5000
	-- MoveNpc_NpcInfo[18796]["AfterActivetyPosX"] = 100
	-- MoveNpc_NpcInfo[18796]["AfterActivetyPosY"] = 100

---------------------------------------------------------------------------------------
---151105[英文征服][活动脚本]boss月活动制作
---SQL BY:魏贻逵                                    
---DATE:2015-11-05                                 
---------------------------------------------------------------------------------------

	MoveNpc_NpcInfo[9683] = {}
	MoveNpc_NpcInfo[9683]["ActivetyTime"] = "2015-12-03 00:00 2015-12-16 23:59"
	MoveNpc_NpcInfo[9683]["NpcId"] = 9683
	MoveNpc_NpcInfo[9683]["ActivetyMapId"] = 1036
	MoveNpc_NpcInfo[9683]["ActivetyPosX"] = 288
	MoveNpc_NpcInfo[9683]["ActivetyPosY"] = 215
	MoveNpc_NpcInfo[9683]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[9683]["AfterActivetyPosX"] = 100
	MoveNpc_NpcInfo[9683]["AfterActivetyPosY"] = 100
	
	MoveNpc_NpcInfo[9686] = {}
	MoveNpc_NpcInfo[9686]["ActivetyTime"] = "2015-12-03 00:00 2015-12-16 23:59"
	MoveNpc_NpcInfo[9686]["NpcId"] = 9686
	MoveNpc_NpcInfo[9686]["ActivetyMapId"] = 1036
	MoveNpc_NpcInfo[9686]["ActivetyPosX"] = 304
	MoveNpc_NpcInfo[9686]["ActivetyPosY"] = 215
	MoveNpc_NpcInfo[9686]["AfterActivetyMapId"] = 5000
	MoveNpc_NpcInfo[9686]["AfterActivetyPosX"] = 100
	MoveNpc_NpcInfo[9686]["AfterActivetyPosY"] = 100


--------------------------------------逻辑部分-----------------------------------------
-- 移动NPC
function MoveNpc_Move()
	for i,v in pairs (MoveNpc_NpcInfo) do
		local nNpcId = v["NpcId"]
		-- 获取NPC所在地图
		local nNpcMapId = Get_NpcMapID(nNpcId)
		
		-- 该NPC存在
		if nNpcMapId ~= nil then
			local nActivityTime = v["ActivetyTime"]
			local nActivetyMapId = v["ActivetyMapId"]
			local nActivetyPosX = v["ActivetyPosX"]
			local nActivetyPosY = v["ActivetyPosY"]
			local nAfterActivetyMapId = v["AfterActivetyMapId"]
			local nAfterActivetyPosX = v["AfterActivetyPosX"]
			local nAfterActivetyPosY = v["AfterActivetyPosY"]
			
			-- 判断是否在活动时间内
			if Sys_ChkFullTime(nActivityTime) then
				-- 判断是否已在活动地图内
				if nNpcMapId ~= nActivetyMapId then
					Npc_MoveNpcPos(nNpcId,nActivetyMapId,nActivetyPosX,nActivetyPosY)
				end
			else
				-- 判断是否已在该地图内
				if nNpcMapId ~= nAfterActivetyMapId then
					Npc_MoveNpcPos(nNpcId,nAfterActivetyMapId,nAfterActivetyPosX,nAfterActivetyPosY)
				end
			end
		end
	end
end

-- 时间自检触发
tSystem_Prompet_Func = tSystem_Prompet_Func or {}
table.insert(tSystem_Prompet_Func,MoveNpc_Move)