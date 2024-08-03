----------------------------------------------------------------------------
--Name:		[征服][功能脚本]气氛布置的地图光效.lua
--Purpose:	气氛布置的地图光效功能
--Creator: 	郑鋆
--Created:	2015/06/10
----------------------------------------------------------------------------

-- 命名前缀词： 
-- MoveTrap_

local MoveTrap_Info = {}
	-- MoveTrap_Info[1366] = {}
	-- MoveTrap_Info[1366]["ActivetyTime"] = "2015-07-15 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1366]["DayTime"] = {}
	-- MoveTrap_Info[1366]["DayTime"][1] = "15:00 15:00"
	-- MoveTrap_Info[1366]["DayTime"][2] = "19:00 19:00"
	-- MoveTrap_Info[1366]["DayTime"][3] = "20:00 20:00"
	-- MoveTrap_Info[1366]["DayTime"][4] = "21:00 21:00"
	-- MoveTrap_Info[1366]["TrapType"] = 1366
	-- MoveTrap_Info[1366]["Look"] = 1366
	-- MoveTrap_Info[1366]["MapInfo"] = {}
	-- MoveTrap_Info[1366]["MapInfo"][1] = {}
	-- MoveTrap_Info[1366]["MapInfo"][1]["MapId"] = 1011
	-- MoveTrap_Info[1366]["MapInfo"][1]["PosX"] = 188
	-- MoveTrap_Info[1366]["MapInfo"][1]["PosY"] = 269
	-- MoveTrap_Info[1366]["MapInfo"][2] = {}
	-- MoveTrap_Info[1366]["MapInfo"][2]["MapId"] = 1000
	-- MoveTrap_Info[1366]["MapInfo"][2]["PosX"] = 490
	-- MoveTrap_Info[1366]["MapInfo"][2]["PosY"] = 640

	-- MoveTrap_Info[1367] = {}
	-- MoveTrap_Info[1367]["ActivetyTime"] = "2015-07-15 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1367]["DayTime"] = {}
	-- MoveTrap_Info[1367]["DayTime"][1] = "15:00 15:00"
	-- MoveTrap_Info[1367]["DayTime"][2] = "19:00 19:00"
	-- MoveTrap_Info[1367]["DayTime"][3] = "20:00 20:00"
	-- MoveTrap_Info[1367]["DayTime"][4] = "21:00 21:00"
	-- MoveTrap_Info[1367]["TrapType"] = 1367
	-- MoveTrap_Info[1367]["Look"] = 1367
	-- MoveTrap_Info[1367]["MapInfo"] = {}
	-- MoveTrap_Info[1367]["MapInfo"][1] = {}
	-- MoveTrap_Info[1367]["MapInfo"][1]["MapId"] = 1002
	-- MoveTrap_Info[1367]["MapInfo"][1]["PosX"] = 310
	-- MoveTrap_Info[1367]["MapInfo"][1]["PosY"] = 258

	-- MoveTrap_Info[1368] = {}
	-- MoveTrap_Info[1368]["ActivetyTime"] = "2015-07-15 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1368]["DayTime"] = {}
	-- MoveTrap_Info[1368]["DayTime"][1] = "15:00 15:00"
	-- MoveTrap_Info[1368]["DayTime"][2] = "19:00 19:00"
	-- MoveTrap_Info[1368]["DayTime"][3] = "20:00 20:00"
	-- MoveTrap_Info[1368]["DayTime"][4] = "21:00 21:00"
	-- MoveTrap_Info[1368]["TrapType"] = 1368
	-- MoveTrap_Info[1368]["Look"] = 1368
	-- MoveTrap_Info[1368]["MapInfo"] = {}
	-- MoveTrap_Info[1368]["MapInfo"][1] = {}
	-- MoveTrap_Info[1368]["MapInfo"][1]["MapId"] = 1020
	-- MoveTrap_Info[1368]["MapInfo"][1]["PosX"] = 563
	-- MoveTrap_Info[1368]["MapInfo"][1]["PosY"] = 585
	-- MoveTrap_Info[1368]["MapInfo"][2] = {}
	-- MoveTrap_Info[1368]["MapInfo"][2]["MapId"] = 1015
	-- MoveTrap_Info[1368]["MapInfo"][2]["PosX"] = 731
	-- MoveTrap_Info[1368]["MapInfo"][2]["PosY"] = 574

	-- MoveTrap_Info[1369] = {}
	-- MoveTrap_Info[1369]["ActivetyTime"] = "2015-07-15 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1369]["DayTime"] = {}
	-- MoveTrap_Info[1369]["DayTime"][1] = "15:00 15:00"
	-- MoveTrap_Info[1369]["DayTime"][2] = "19:00 19:00"
	-- MoveTrap_Info[1369]["DayTime"][3] = "20:00 20:00"
	-- MoveTrap_Info[1369]["DayTime"][4] = "21:00 21:00"
	-- MoveTrap_Info[1369]["TrapType"] = 1369
	-- MoveTrap_Info[1369]["Look"] = 1063
	-- MoveTrap_Info[1369]["MapInfo"] = {}
	-- MoveTrap_Info[1369]["MapInfo"][1] = {}
	-- MoveTrap_Info[1369]["MapInfo"][1]["MapId"] = 1002
	-- MoveTrap_Info[1369]["MapInfo"][1]["PosX"] = 308
	-- MoveTrap_Info[1369]["MapInfo"][1]["PosY"] = 262
	-- MoveTrap_Info[1369]["MapInfo"][2] = {}
	-- MoveTrap_Info[1369]["MapInfo"][2]["MapId"] = 1002
	-- MoveTrap_Info[1369]["MapInfo"][2]["PosX"] = 316
	-- MoveTrap_Info[1369]["MapInfo"][2]["PosY"] = 259

	-- MoveTrap_Info[1370] = {}
	-- MoveTrap_Info[1370]["ActivetyTime"] = "2015-07-09 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1370]["DayTime"] = {}
	-- MoveTrap_Info[1370]["DayTime"][1] = "15:00 15:00"
	-- MoveTrap_Info[1370]["DayTime"][2] = "19:00 19:00"
	-- MoveTrap_Info[1370]["DayTime"][3] = "20:00 20:00"
	-- MoveTrap_Info[1370]["DayTime"][4] = "21:00 21:00"
	-- MoveTrap_Info[1370]["TrapType"] = 1370
	-- MoveTrap_Info[1370]["Look"] = 1370
	-- MoveTrap_Info[1370]["MapInfo"] = {}
	-- MoveTrap_Info[1370]["MapInfo"][1] = {}
	-- MoveTrap_Info[1370]["MapInfo"][1]["MapId"] = 1011
	-- MoveTrap_Info[1370]["MapInfo"][1]["PosX"] = 189
	-- MoveTrap_Info[1370]["MapInfo"][1]["PosY"] = 271
	-- MoveTrap_Info[1370]["MapInfo"][2] = {}
	-- MoveTrap_Info[1370]["MapInfo"][2]["MapId"] = 1020
	-- MoveTrap_Info[1370]["MapInfo"][2]["PosX"] = 563
	-- MoveTrap_Info[1370]["MapInfo"][2]["PosY"] = 585
	-- MoveTrap_Info[1370]["MapInfo"][3] = {}
	-- MoveTrap_Info[1370]["MapInfo"][3]["MapId"] = 1000
	-- MoveTrap_Info[1370]["MapInfo"][3]["PosX"] = 498
	-- MoveTrap_Info[1370]["MapInfo"][3]["PosY"] = 648
	-- MoveTrap_Info[1370]["MapInfo"][4] = {}
	-- MoveTrap_Info[1370]["MapInfo"][4]["MapId"] = 1015
	-- MoveTrap_Info[1370]["MapInfo"][4]["PosX"] = 724
	-- MoveTrap_Info[1370]["MapInfo"][4]["PosY"] = 573
	-- MoveTrap_Info[1370]["MapInfo"][5] = {}
	-- MoveTrap_Info[1370]["MapInfo"][5]["MapId"] = 1002
	-- MoveTrap_Info[1370]["MapInfo"][5]["PosX"] = 310
	-- MoveTrap_Info[1370]["MapInfo"][5]["PosY"] = 276

	-- MoveTrap_Info[1371] = {}
	-- MoveTrap_Info[1371]["ActivetyTime"] = "2015-07-09 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1371]["TrapType"] = 1371
	-- MoveTrap_Info[1371]["Look"] = 1371
	-- MoveTrap_Info[1371]["MapInfo"] = {}
	-- MoveTrap_Info[1371]["MapInfo"][1] = {}
	-- MoveTrap_Info[1371]["MapInfo"][1]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][1]["PosX"] = 550
	-- MoveTrap_Info[1371]["MapInfo"][1]["PosY"] = 588
	-- MoveTrap_Info[1371]["MapInfo"][2] = {}
	-- MoveTrap_Info[1371]["MapInfo"][2]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][2]["PosX"] = 550
	-- MoveTrap_Info[1371]["MapInfo"][2]["PosY"] = 567
	-- MoveTrap_Info[1371]["MapInfo"][3] = {}
	-- MoveTrap_Info[1371]["MapInfo"][3]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][3]["PosX"] = 562
	-- MoveTrap_Info[1371]["MapInfo"][3]["PosY"] = 596
	-- MoveTrap_Info[1371]["MapInfo"][4] = {}
	-- MoveTrap_Info[1371]["MapInfo"][4]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][4]["PosX"] = 562
	-- MoveTrap_Info[1371]["MapInfo"][4]["PosY"] = 593
	-- MoveTrap_Info[1371]["MapInfo"][5] = {}
	-- MoveTrap_Info[1371]["MapInfo"][5]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][5]["PosX"] = 562
	-- MoveTrap_Info[1371]["MapInfo"][5]["PosY"] = 590
	-- MoveTrap_Info[1371]["MapInfo"][6] = {}
	-- MoveTrap_Info[1371]["MapInfo"][6]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][6]["PosX"] = 562
	-- MoveTrap_Info[1371]["MapInfo"][6]["PosY"] = 576
	-- MoveTrap_Info[1371]["MapInfo"][7] = {}
	-- MoveTrap_Info[1371]["MapInfo"][7]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][7]["PosX"] = 562
	-- MoveTrap_Info[1371]["MapInfo"][7]["PosY"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][8] = {}
	-- MoveTrap_Info[1371]["MapInfo"][8]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][8]["PosX"] = 562
	-- MoveTrap_Info[1371]["MapInfo"][8]["PosY"] = 570
	-- MoveTrap_Info[1371]["MapInfo"][9] = {}
	-- MoveTrap_Info[1371]["MapInfo"][9]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][9]["PosX"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][9]["PosY"] = 597
	-- MoveTrap_Info[1371]["MapInfo"][10] = {}
	-- MoveTrap_Info[1371]["MapInfo"][10]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][10]["PosX"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][10]["PosY"] = 594
	-- MoveTrap_Info[1371]["MapInfo"][11] = {}
	-- MoveTrap_Info[1371]["MapInfo"][11]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][11]["PosX"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][11]["PosY"] = 591
	-- MoveTrap_Info[1371]["MapInfo"][12] = {}
	-- MoveTrap_Info[1371]["MapInfo"][12]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][12]["PosX"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][12]["PosY"] = 576
	-- MoveTrap_Info[1371]["MapInfo"][13] = {}
	-- MoveTrap_Info[1371]["MapInfo"][13]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][13]["PosX"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][13]["PosY"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][14] = {}
	-- MoveTrap_Info[1371]["MapInfo"][14]["MapId"] = 1020
	-- MoveTrap_Info[1371]["MapInfo"][14]["PosX"] = 573
	-- MoveTrap_Info[1371]["MapInfo"][14]["PosY"] = 570
	-- MoveTrap_Info[1371]["MapInfo"][15] = {}
	-- MoveTrap_Info[1371]["MapInfo"][15]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][15]["PosX"] = 489
	-- MoveTrap_Info[1371]["MapInfo"][15]["PosY"] = 639
	-- MoveTrap_Info[1371]["MapInfo"][16] = {}
	-- MoveTrap_Info[1371]["MapInfo"][16]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][16]["PosX"] = 505
	-- MoveTrap_Info[1371]["MapInfo"][16]["PosY"] = 655
	-- MoveTrap_Info[1371]["MapInfo"][17] = {}
	-- MoveTrap_Info[1371]["MapInfo"][17]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][17]["PosX"] = 491
	-- MoveTrap_Info[1371]["MapInfo"][17]["PosY"] = 629
	-- MoveTrap_Info[1371]["MapInfo"][18] = {}
	-- MoveTrap_Info[1371]["MapInfo"][18]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][18]["PosX"] = 481
	-- MoveTrap_Info[1371]["MapInfo"][18]["PosY"] = 638
	-- MoveTrap_Info[1371]["MapInfo"][19] = {}
	-- MoveTrap_Info[1371]["MapInfo"][19]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][19]["PosX"] = 491
	-- MoveTrap_Info[1371]["MapInfo"][19]["PosY"] = 665
	-- MoveTrap_Info[1371]["MapInfo"][20] = {}
	-- MoveTrap_Info[1371]["MapInfo"][20]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][20]["PosX"] = 502
	-- MoveTrap_Info[1371]["MapInfo"][20]["PosY"] = 665
	-- MoveTrap_Info[1371]["MapInfo"][21] = {}
	-- MoveTrap_Info[1371]["MapInfo"][21]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][21]["PosX"] = 513
	-- MoveTrap_Info[1371]["MapInfo"][21]["PosY"] = 653
	-- MoveTrap_Info[1371]["MapInfo"][22] = {}
	-- MoveTrap_Info[1371]["MapInfo"][22]["MapId"] = 1000
	-- MoveTrap_Info[1371]["MapInfo"][22]["PosX"] = 521
	-- MoveTrap_Info[1371]["MapInfo"][22]["PosY"] = 653

	-- MoveTrap_Info[1372] = {}
	-- MoveTrap_Info[1372]["ActivetyTime"] = "2015-07-15 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1372]["TrapType"] = 1372
	-- MoveTrap_Info[1372]["Look"] = 1368
	-- MoveTrap_Info[1372]["MapInfo"] = {}
	-- MoveTrap_Info[1372]["MapInfo"][1] = {}
	-- MoveTrap_Info[1372]["MapInfo"][1]["MapId"] = 1002
	-- MoveTrap_Info[1372]["MapInfo"][1]["PosX"] = 308
	-- MoveTrap_Info[1372]["MapInfo"][1]["PosY"] = 384
	-- MoveTrap_Info[1372]["MapInfo"][2] = {}
	-- MoveTrap_Info[1372]["MapInfo"][2]["MapId"] = 1002
	-- MoveTrap_Info[1372]["MapInfo"][2]["PosX"] = 308
	-- MoveTrap_Info[1372]["MapInfo"][2]["PosY"] = 362
	
	-- MoveTrap_Info[1373] = {}
	-- MoveTrap_Info[1373]["ActivetyTime"] = "2015-07-09 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1373]["TrapType"] = 1373
	-- MoveTrap_Info[1373]["Look"] = 1032
	-- MoveTrap_Info[1373]["MapInfo"] = {}
	-- MoveTrap_Info[1373]["MapInfo"][1] = {}
	-- MoveTrap_Info[1373]["MapInfo"][1]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][1]["PosX"] = 328
	-- MoveTrap_Info[1373]["MapInfo"][1]["PosY"] = 295
	-- MoveTrap_Info[1373]["MapInfo"][2] = {}
	-- MoveTrap_Info[1373]["MapInfo"][2]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][2]["PosX"] = 328
	-- MoveTrap_Info[1373]["MapInfo"][2]["PosY"] = 282
	-- MoveTrap_Info[1373]["MapInfo"][3] = {}
	-- MoveTrap_Info[1373]["MapInfo"][3]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][3]["PosX"] = 328
	-- MoveTrap_Info[1373]["MapInfo"][3]["PosY"] = 275
	-- MoveTrap_Info[1373]["MapInfo"][4] = {}
	-- MoveTrap_Info[1373]["MapInfo"][4]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][4]["PosX"] = 328
	-- MoveTrap_Info[1373]["MapInfo"][4]["PosY"] = 260
	-- MoveTrap_Info[1373]["MapInfo"][5] = {}
	-- MoveTrap_Info[1373]["MapInfo"][5]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][5]["PosX"] = 315
	-- MoveTrap_Info[1373]["MapInfo"][5]["PosY"] = 295
	-- MoveTrap_Info[1373]["MapInfo"][6] = {}
	-- MoveTrap_Info[1373]["MapInfo"][6]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][6]["PosX"] = 315
	-- MoveTrap_Info[1373]["MapInfo"][6]["PosY"] = 260
	-- MoveTrap_Info[1373]["MapInfo"][7] = {}
	-- MoveTrap_Info[1373]["MapInfo"][7]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][7]["PosX"] = 307
	-- MoveTrap_Info[1373]["MapInfo"][7]["PosY"] = 295
	-- MoveTrap_Info[1373]["MapInfo"][8] = {}
	-- MoveTrap_Info[1373]["MapInfo"][8]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][8]["PosX"] = 307
	-- MoveTrap_Info[1373]["MapInfo"][8]["PosY"] = 260
	-- MoveTrap_Info[1373]["MapInfo"][9] = {}
	-- MoveTrap_Info[1373]["MapInfo"][9]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][9]["PosX"] = 293
	-- MoveTrap_Info[1373]["MapInfo"][9]["PosY"] = 295
	-- MoveTrap_Info[1373]["MapInfo"][10] = {}
	-- MoveTrap_Info[1373]["MapInfo"][10]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][10]["PosX"] = 293
	-- MoveTrap_Info[1373]["MapInfo"][10]["PosY"] = 282
	-- MoveTrap_Info[1373]["MapInfo"][11] = {}
	-- MoveTrap_Info[1373]["MapInfo"][11]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][11]["PosX"] = 293
	-- MoveTrap_Info[1373]["MapInfo"][11]["PosY"] = 275
	-- MoveTrap_Info[1373]["MapInfo"][12] = {}
	-- MoveTrap_Info[1373]["MapInfo"][12]["MapId"] = 1002
	-- MoveTrap_Info[1373]["MapInfo"][12]["PosX"] = 293
	-- MoveTrap_Info[1373]["MapInfo"][12]["PosY"] = 260

	-- MoveTrap_Info[1374] = {}
	-- MoveTrap_Info[1374]["ActivetyTime"] = "2015-07-09 00:00 2015-07-22 23:59"
	-- MoveTrap_Info[1374]["TrapType"] = 1374
	-- MoveTrap_Info[1374]["Look"] = 1374
	-- MoveTrap_Info[1374]["MapInfo"] = {}
	-- MoveTrap_Info[1374]["MapInfo"][1] = {}
	-- MoveTrap_Info[1374]["MapInfo"][1]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][1]["PosX"] = 302
	-- MoveTrap_Info[1374]["MapInfo"][1]["PosY"] = 388
	-- MoveTrap_Info[1374]["MapInfo"][2] = {}
	-- MoveTrap_Info[1374]["MapInfo"][2]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][2]["PosX"] = 291
	-- MoveTrap_Info[1374]["MapInfo"][2]["PosY"] = 387
	-- MoveTrap_Info[1374]["MapInfo"][3] = {}
	-- MoveTrap_Info[1374]["MapInfo"][3]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][3]["PosX"] = 280
	-- MoveTrap_Info[1374]["MapInfo"][3]["PosY"] = 388
	-- MoveTrap_Info[1374]["MapInfo"][4] = {}
	-- MoveTrap_Info[1374]["MapInfo"][4]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][4]["PosX"] = 268
	-- MoveTrap_Info[1374]["MapInfo"][4]["PosY"] = 387
	-- MoveTrap_Info[1374]["MapInfo"][5] = {}
	-- MoveTrap_Info[1374]["MapInfo"][5]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][5]["PosX"] = 269
	-- MoveTrap_Info[1374]["MapInfo"][5]["PosY"] = 375
	-- MoveTrap_Info[1374]["MapInfo"][6] = {}
	-- MoveTrap_Info[1374]["MapInfo"][6]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][6]["PosX"] = 269
	-- MoveTrap_Info[1374]["MapInfo"][6]["PosY"] = 368
	-- MoveTrap_Info[1374]["MapInfo"][7] = {}
	-- MoveTrap_Info[1374]["MapInfo"][7]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][7]["PosX"] = 269
	-- MoveTrap_Info[1374]["MapInfo"][7]["PosY"] = 355
	-- MoveTrap_Info[1374]["MapInfo"][8] = {}
	-- MoveTrap_Info[1374]["MapInfo"][8]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][8]["PosX"] = 280
	-- MoveTrap_Info[1374]["MapInfo"][8]["PosY"] = 355
	-- MoveTrap_Info[1374]["MapInfo"][9] = {}
	-- MoveTrap_Info[1374]["MapInfo"][9]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][9]["PosX"] = 291
	-- MoveTrap_Info[1374]["MapInfo"][9]["PosY"] = 355
	-- MoveTrap_Info[1374]["MapInfo"][10] = {}
	-- MoveTrap_Info[1374]["MapInfo"][10]["MapId"] = 1002
	-- MoveTrap_Info[1374]["MapInfo"][10]["PosX"] = 302
	-- MoveTrap_Info[1374]["MapInfo"][10]["PosY"] = 354

-- 气氛布置的陷阱光效
function MoveTrap_Check()
	for i,v in pairs (MoveTrap_Info) do
		local sTime = v["ActivetyTime"]
		local nTrapType = v["TrapType"]
		-- 获取陷阱个数
		local nCount = Get_TrapCount(nTrapType)
		
		-- 判断活动时间
		if Sys_ChkFullTime(sTime) then
			local nLook = v["Look"]
			

			-- 判断是否有配每天的检测时间
			if v["DayTime"] ~= nil then
				-- 判断陷阱个数
				if nCount > 0 then
					for a,b in pairs (v["MapInfo"]) do
						local nMapId = b["MapId"]
						-- 删除陷阱
						Trap_DelMapTrap(nMapId,nTrapType)
					end
				end

				for nIndex = 1,#v["DayTime"] do
					-- 判断每天的时间
					if Sys_ChkDayTime(v["DayTime"][nIndex]) then
						for a,b in pairs (v["MapInfo"]) do
							local nMapId = b["MapId"]
							local nPosX = b["PosX"]
							local nPosY = b["PosY"]
							
							-- 创建陷阱
							Trap_CreateMapTrap(nTrapType,nLook,0,nMapId,nPosX,nPosY,0,0)
						end
					end
				end
				
			elseif nCount <= 0 then
				for a,b in pairs (v["MapInfo"]) do
					local nMapId = b["MapId"]
					local nPosX = b["PosX"]
					local nPosY = b["PosY"]
					
					-- 创建陷阱
					Trap_CreateMapTrap(nTrapType,nLook,0,nMapId,nPosX,nPosY,0,0)
				end
			end
		-- 判断陷阱个数
		elseif nCount > 0 then
			for a,b in pairs (v["MapInfo"]) do
				local nMapId = b["MapId"]

				-- 删除陷阱
				Trap_DelMapTrap(nMapId,nTrapType)
			end
		end
	end
end

-- 时间自检触发
tSystem_Prompet_Func = tSystem_Prompet_Func or {}
table.insert(tSystem_Prompet_Func,MoveTrap_Check)