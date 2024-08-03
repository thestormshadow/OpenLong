----------------------------------------------------------------------------
--Name:		[征服][公用函数]程序触发.lua
--Purpose:	程序触发
--Creator: 	郑江文
--Created:	2014/12/18

--玩家死亡触发
function Event_Kill_User()
	local nKillerId = Get_UserId()

	--各自活动有需求死亡的逻辑封装成函数插入表中。
	if type(tUserKilled["tFunction"]) == "table" then
		for _,func in ipairs(tUserKilled["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nKillerId)
			end
		end
	end
end

-- 玩家使用复活触发
function Event_Save_User()
	if type(tUserSave["tFunction"]) == "table" then
		for _,func in ipairs(tUserSave["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func()
			end
		end
	end
end

-- 玩家使用锁人触发
function Event_KeepGhost()
	if type(tKeepGhost["tFunction"]) == "table" then
		for _,func in ipairs(tKeepGhost["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func()
			end
		end
	end
end

-- 玩家使用解锁
function Event_ClearKeepGhost()
	if type(tClearKeepGhost["tFunction"]) == "table" then
		for _,func in ipairs(tClearKeepGhost["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func()
			end
		end
	end
end

-- 个人排位赛：	赢场：
-- 参1，玩家id，参2，场数
function UserArenicWins(nUserId,nFieldNum)
	if type(tArenicWins["tFunction"]) == "table" then
		for _,func in ipairs(tArenicWins["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nUserId,nFieldNum)
			end
		end
	end
end

-- 个人排位赛：	参赛场：
-- 参1，玩家id，参2，场数
function UserArenicCompetes(nUserId,nFieldNum)
	if type(tArenicCompetes["tFunction"]) == "table" then
		for _,func in ipairs(tArenicCompetes["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nUserId,nFieldNum)
			end
		end
	end
end

-- 组队排位赛：	赢场：
-- 参1，玩家id，参2，场数
function TeamArenicWins(nUserId,nFieldNum)
	if type(tTeamArenicWins["tFunction"]) == "table" then
		for _,func in ipairs(tTeamArenicWins["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nUserId,nFieldNum)
			end
		end
	end
end

-- 组队排位赛：	参赛场：
-- 参1，玩家id，参2，场数
function TeamArenicCompetes(nUserId,nFieldNum)
	if type(tTeamArenicCompetes["tFunction"]) == "table" then
		for _,func in ipairs(tTeamArenicCompetes["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nUserId,nFieldNum)
			end
		end
	end
end

-- 骑宠，玩家冲过终点是触发
-- 参数1，玩家ID
function RideArriveTerminal(nUserId)
	if type(tRideArrive["tFunction"]) == "table" then
		for _,func in ipairs(tRideArrive["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nUserId)
			end
		end
	end
end

-- 武功，每次修炼时触发：
-- 参数1：玩家id 参数2：玩家当日修炼次数
function TrainingGongfu(nUserId,nTrainNum)
	if type(tTrainGongFu["tFunction"]) == "table" then
		for _,func in ipairs(tTrainGongFu["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nUserId,nTrainNum)
			end
		end
	end
end

-- 服务端启动触发
function Event_Server_Start()
	if type(tServerStart["tFunction"]) == "table" then
		for _,func in ipairs(tServerStart["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func()
			end
		end
	end
end

-- 添加内功转换
-- 第一个参数是目标玩家ID
function InnerStrength_Exchange(nChangeUserId)
	local nUserId = Get_UserId()
	if type(tStrengthExchange["tFunction"]) == "table" then
		for _,func in ipairs(tStrengthExchange["tFunction"]) do
			if func ~= nil and type(func) == "function" then
				func(nUserId,nChangeUserId)
			end
		end
	end
end