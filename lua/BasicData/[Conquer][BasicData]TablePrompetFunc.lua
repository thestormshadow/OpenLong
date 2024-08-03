----------------------------------------------------------------------------
--Name:		[征服][基础数据]系统自检功能表.lua
--Purpose:	定义系统自检功能表
--Creator: 	郑江文
--Created:	2014/09/03
----------------------------------------------------------------------------
--系统自检功能表
--tSystem_Prompet_Func={要通过自检实现的函数,...}
tSystem_Prompet_Func={}
tOntimerMin_M={}
tOntimerMin_HM={}
tOntimerMin_wHM={}
tOntimerMin_dHM={}


function System_Prompet()
	local nHour = tonumber(os.date("%H"))
	local nMinute = tonumber(os.date("%M"))
	local ndate = tonumber(os.date("%d"))
	local nweek = tonumber(os.date("%w"))
	local nHM = tonumber(os.date("%H%M"))
	local nwHM = tonumber(os.date("%w%H%M"))
	local ndHM = tonumber(os.date("%d%H%M"))
	--分钟
	if tOntimerMin_M[nMinute] ~= nil then
		for _,func in ipairs(tOntimerMin_M[nMinute]) do
			if func ~= nil and type(func) == "function" then
				func(nMinute)
			end
		end
	end
	--小时/分钟
	if tOntimerMin_HM[nHM] ~= nil then
		for _,func in ipairs(tOntimerMin_HM[nHM]) do
			if func ~= nil and type(func) == "function" then
				func(nHour,nMinute)
			end
		end
	end
	--周/小时/分钟
	if tOntimerMin_wHM[nwHM] ~= nil then
		for _,func in ipairs(tOntimerMin_wHM[nwHM]) do
			if func ~= nil and type(func) == "function" then
				func(nHour,nMinute,ndate,nweek)
			end
		end
	end
	--天/小时/分钟
	if tOntimerMin_dHM[ndHM] ~= nil then
		for _,func in ipairs(tOntimerMin_dHM[ndHM]) do
			if func ~= nil and type(func) == "function" then
				func(nHour,nMinute,ndate,nweek)
			end
		end
	end
	
	--版本信息提示
	for _,func in ipairs(tSystem_Prompet_Func) do
		if func ~= nil and type(func) == "function" then
			func(nHour,nMinute,ndate,nweek)
		end
	end
end