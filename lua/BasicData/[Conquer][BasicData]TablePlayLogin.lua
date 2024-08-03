----------------------------------------------------------------------------
--Name:		[征服][基础数据]上线函数表.lua
--Purpose:	定义上线函数表、用法
--Creator: 	郑江文
--Created:	2014/09/03
----------------------------------------------------------------------------
--玩家每次上线触发函数表
--tSystem_PlayLogin_Func={func1,...}
--Actionl.id=1000000(玩家每次上线都会触发)
tSystem_PlayLogin_Func = {}

--每次上线触发函数
function System_PlayLogin()
	local nHour=tonumber(os.date("%H"))
	local nMinute=tonumber(os.date("%M"))
	local ndate=tonumber(os.date("%d"))
	local nweek=tonumber(os.date("%w"))
	for _,func in ipairs(tSystem_PlayLogin_Func) do
		if func ~= nil and type(func) == "function" then
			func(ndate,nweek,nHour,nMinute)
		end
	end
end

--首次上线触发函数表
--tSystem_PlayLoginFirst_Func={func1,...}
--Action.id=1000500(玩家新建号第一次上线才会触发)
tSystem_PlayLoginFirst_Func = {}

--首次上线触发函数
function System_PlayLoginFirst()
	local nHour=tonumber(os.date("%H"))
	local nMinute=tonumber(os.date("%M"))
	local ndate=tonumber(os.date("%d"))
	local nweek=tonumber(os.date("%w"))
	for _,func in ipairs(tSystem_PlayLoginFirst_Func) do
		func(ndate,nweek,nHour,nMinute)
	end
end