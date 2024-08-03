----------------------------------------------------------------------------
--Name:		[征服][公用函数]玩家函数.lua
--Purpose:	玩家函数接口
--Creator: 	林锦
--Created:	2014/06/26
----------------------------------------------------------------------------
---------------------------------命名规范----------------------------------
------------------------------actiontype的分类------------------------------
--Sys  玩家所有
--Send 消息发送
--Get  获得属性
--Set  修改属性
--Chk  检查属性
--Del  删除属性
--Add  添加属性
--Dec  扣除属性
------------------------------------------------------------------------------
-- 玩家函数命名前缀词：User_
--例子：
--(fn_CheckLeftSpace, "CheckLeftSpace");//检查玩家空格子数，参1:玩家ID, 参2:空格数量


--function User_ChkBagSpace(nUserId,nNum)
--
--end

------------------------------------------------------------------------------

--检查玩家空格子数 
--(fn_CheckLeftSpace, "CheckLeftSpace");		
--参数说明：参1:玩家ID, 参2:空格数量
--返回：有空间 ture 没空间false
function User_CheckLeftSpace(nSpaceNum,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_CheckLeftSpace 第二个参数nUserId 为整型并且大于等于0")
		return
	end	
	
	if type(nSpaceNum) ~= "number" or nSpaceNum%1 ~= 0 or nSpaceNum <= 0 then
		Sys_SaveAbnormalLog("函数 User_CheckLeftSpace 中 nSpaceNum 只能传大于0的整数")
		return
	end

	return CheckLeftSpace(nUserId,nSpaceNum)
end


--玩家变身
--TransformByItem		
--参数说明：参1:玩家ID, 参2:技能类型ID, 参3:技能等级, 参4:怪物类型ID, 参5:持续时间
--返回
function User_TransForm(nSkillType,nSkillLevel,nMonsterType,nTime,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TransForm 中 nUserId 为整型并且大于等于0")
		return
	end	
	
	if type(nSkillType) ~= "number" or nSkillType%1 ~= 0 or nSkillType <= 0 then
		Sys_SaveAbnormalLog("函数 User_TransForm 中 nSkillType 只能传大于0的整数")
		return
	end
	
	if type(nSkillLevel) ~= "number" or nSkillLevel%1 ~= 0 or nSkillLevel < 0 then
		Sys_SaveAbnormalLog("函数 User_TransForm 中 nSkillLevel 只能传大于等于0的整数")
		return
	end
	
	if type(nMonsterType) ~= "number" or nMonsterType%1 ~= 0 or nMonsterType <= 0 then
		Sys_SaveAbnormalLog("函数 User_TransForm 中 nMonsterType 只能传大于0的整数")
		return
	end
		
	if type(nTime) ~= "number" or nTime%1 ~= 0 or nTime <= 0 then
		Sys_SaveAbnormalLog("函数 User_TransForm 中 nTime 只能传大于0的整数")
		return
	end	

	return TransformByItem(nUserId,nSkillType,nSkillLevel,nMonsterType,nTime)
end


--玩家切地图
--(fn_ChgMap, "ChgMap");			
--参数说明：参1:玩家ID, 参2:地图ID, 参3:坐标X, 参4:坐标Y, 参5:是否可以出监狱 	参照 ACTION_USER_CHGMAP = 1003
--返回玩家切地图的坐标点
function User_ChgMap(nMapId,nCellx,nCelly,nIsOutPrison,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_ChgMap  中 nUserId 为整型并且大于等于0")
		return
	end	
	
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgMap 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nCellx) ~= "number" or nCellx%1 ~= 0 or nCellx <= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgMap 中 nCellx 只能传大于0的整数")
		return
	end
	
	if type(nCelly) ~= "number" or nCelly%1 ~= 0 or nCelly <= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgMap 中 nCelly 只能传大于0的整数")
		return
	end
	
	if nIsOutPrison == nil then
		nIsOutPrison = 0
	elseif type(nIsOutPrison) ~= "number" or nIsOutPrison%1 ~= 0 or nIsOutPrison < 0 or nIsOutPrison > 1  then
		Sys_SaveAbnormalLog("函数 User_ChgMap 中 nIsOutPrison 只能传（0，1）的整数")
		return
	end	

	return ChgMap(nUserId,nMapId,nCellx,nCelly,nIsOutPrison)
end


--记录玩家坐标	
--(fn_RecordPoint, "RecordPoint");	
--参数说明：参1:玩家ID, 参2:地图ID, 参3:坐标X, 参4:坐标Y 			参照 ACTION_USER_RECORDPOINT = 1004 
----返回记录玩家坐标
function User_RecordPoint(nMapId,nCellx,nCelly,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_RecordPoint  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 User_RecordPoint 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nCellx) ~= "number" or nCellx%1 ~= 0 or nCellx <= 0 then
		Sys_SaveAbnormalLog("函数 User_RecordPoint 中 nCellx 只能传大于0的整数")
		return
	end
	
	if type(nCelly) ~= "number" or nCelly%1 ~= 0 or nCelly <= 0 then
		Sys_SaveAbnormalLog("函数 User_RecordPoint 中 nCelly 只能传大于0的整数")
		return
	end
	
	return RecordPoint(nUserId,nMapId,nCellx,nCelly)
end

--玩家发言之2000频道	
--(fn_Talk, "Talk");
--参数说明：参1:玩家ID, 参2:频道, 参3:内容  						参照 ACTION_USER_TALK = 1010
--频道=2000;
--返回2000频道	
function User_TalkChannel2000(sContent,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2002  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2002 中 sContent 只能传字符串")
		return
	end	
	
	return Talk(nUserId,2000,sContent)
end

--玩家发言之2002动作频道	
--(fn_Talk, "Talk");
--参数说明：参1:玩家ID, 参2:频道, 参3:内容  						参照 ACTION_USER_TALK = 1010
--频道=2002;	 	动作
--返回2002动作频道	
function User_TalkChannel2002(sContent,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2002  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2002 中 sContent 只能传字符串")
		return
	end	
	
	return Talk(nUserId,2002,sContent)
end



--玩家发言之2003队伍频道	
--(fn_Talk, "Talk");				
--参数说明：参1:玩家ID, 参2:频道, 参3:内容  						参照 ACTION_USER_TALK = 1010
--频道=2003;	 	动作
--返回2003队伍频道的对白
function User_TalkChannel2003(sContent,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2003  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2003 中 sContent 只能传字符串")
		return
	end	

	return Talk(nUserId,2003,sContent)
end


--玩家发言之2005系统频道	
--(fn_Talk, "Talk");				
--参数说明：参1:玩家ID, 参2:频道, 参3:内容  						参照 ACTION_USER_TALK = 1010
--频道=2005;	 	系统
--返回2005系统频道
function User_TalkChannel2005(sContent,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2005  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2005 中 sContent 只能传字符串")
		return
	end	
	
	return Talk(nUserId,2005,sContent)
end



--玩家发言之2007交谈频道	
--(fn_Talk, "Talk");			
--参数说明：参1:玩家ID, 参2:频道, 参3:内容  						参照 ACTION_USER_TALK = 1010
--频道=2007;	 	交谈
--返回2007交谈频道
function User_TalkChannel2007(sContent,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2007  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2007 中 sContent 只能传字符串")
		return
	end	
	
	return Talk(nUserId,2007,sContent)
end



--玩家发言之2011GM频道	
--(fn_Talk, "Talk");		
--参数说明：参1:玩家ID, 参2:频道, 参3:内容  						参照 ACTION_USER_TALK = 1010
--频道=2011;	 	GM频道
--返回2011GM频道
function User_TalkChannel2011(sContent,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2011  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TalkChannel2011 中 sContent 只能传字符串")
		return
	end		

	return Talk(nUserId,2011,sContent)
end



--玩家添加特效
--(fn_Effect, "Effect");	
--参数说明：参1:玩家ID, 参2:szObj, 参3:szEffect, 参4:szOpt   		参照 ACTION_USER_EFFECT = 1027,
--szObj支持"self", "couple", "team", szEffect为特效名称, szOpt支持"add", "del"
--返回玩家添加特效
function User_EffectAdd(sSzObj,sEffect,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_EffectAdd  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sEffect) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_EffectAdd 中 sEffect 只能传字符串")
		return
	end
	
	if type(sSzObj) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_EffectAdd 中 sSzObj 只能传字符串")
		return
	end		
	
	if sSzObj == "self" then
		return Effect(nUserId,"self",sEffect,"add")
	elseif 	sSzObj == "couple" then 
		return Effect(nUserId,"couple",sEffect,"add")
	elseif 	sSzObj == "team"	then
		return Effect(nUserId,"team",sEffect,"add")
	end

end



--玩家删除特效
--(fn_Effect, "Effect");	
--参数说明：参1:玩家ID, 参2:szObj, 参3:szEffect, 参4:szOpt   		参照 ACTION_USER_EFFECT = 1027,
--szObj支持"self", "couple", "team", szEffect为特效名称, szOpt支持"add", "del"
--返回玩家删除特效
function User_EffectDel(sSzObj,sEffect,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_EffectDel  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(sEffect) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_EffectDel 中 sEffect 只能传字符串")
		return
	end
	
	if type(sSzObj) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_EffectDel 中 sSzObj 只能传字符串")
		return
	end		
	
	if sSzObj == "self" then
		return Effect(nUserId,"self",sEffect,"del")
	elseif 	sSzObj == "couple" then 
		return Effect(nUserId,"couple",sEffect,"del")
	elseif 	sSzObj == "team"	then
		return Effect(nUserId,"team",sEffect,"del")
	end
end



--玩家修正战斗最终所获经验值
--SetExpControl	
--参数说明：参1:玩家ID, 参2:加成百分比, 参3:时间, 					参照 ACTION_USER_PLUSEXP = 1048
--返回玩家修正战斗最终所获经验值(X倍经验)
function User_SetExpControl(nPercent,nTime,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_SetExpControl  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nPercent) ~= "number" or nPercent%1 ~= 0 or nPercent <= 0 then
		Sys_SaveAbnormalLog("函数 User_SetExpControl 中 nPercent 只能传大于0的整数")
		return
	end
	
	if type(nTime) ~= "number" or nTime%1 ~= 0 or nTime <= 0 then
		Sys_SaveAbnormalLog("函数 User_SetExpControl 中 nTime 只能传大于0的整数")
		return
	end	

	return SetExpControl(nUserId,nPercent,nTime)
end



--向在队伍频道中广播一条消息
--(fn_BroadcastTeamMsg, "BroadcastTeamMsg");	
--参数说明：参1:玩家ID, 参2:内容			参照 ACTION_TEAM_BROADCAST = 1101,
--返回队伍频道中广播一条消息
function User_BroadcastTeamMsg(nContent,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_BroadcastTeamMsg  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_BroadcastTeamMsg 中 nContent 只能传字符串")
		return
	end			
	
	return BroadcastTeamMsg(nUserId,nContent)
end

--组队切屏
--(fn_TeamChgMap, "TeamChgMap");		
--参数说明：参1:玩家ID, 参2:地图ID，参3:X坐标, 参4:Y坐标				参照 ACTION_TEAM_CHGMAP        = 1107
--返回组队切屏（必须队长触发）
function User_TeamChgMap(nMapId,nCellx,nCelly,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TeamChgMap  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 User_TeamChgMap 中 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nCellx) ~= "number" or nCellx%1 ~= 0 or nCellx <= 0 then
		Sys_SaveAbnormalLog("函数 User_TeamChgMap 中 nCellx 只能传大于0的整数")
		return
	end
	
	if type(nCelly) ~= "number" or nCelly%1 ~= 0 or nCelly <= 0 then
		Sys_SaveAbnormalLog("函数 User_TeamChgMap 中 nCelly 只能传大于0的整数")
		return
	end	
	
	return TeamChgMap(nUserId,nMapId,nCellx,nCelly)
end

--玩家是否队长
--(fn_IsTeamLeader, "IsTeamLeader");		
--参数说明：参1:玩家ID
--返回玩家是否队长
function User_IsTeamLeader(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_IsTeamLeader  中 nUserId 为整型并且大于等于0")
		return
	end	
	
	return IsTeamLeader(nUserId)
end

--全队执行某个LUA函数(只有队长可以触发)	
--TeamExeFuncByLeader	
--参数说明：参1:玩家ID  参2:范围(1:触发者附近 2:本地图 3:所有地图)  参3:LUA函数     失败返回false，否则返回true
--返回全队执行某个LUA函数(只有队长可以触发)	
function User_TeamExeFuncByLeader(nRange,sFunc,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TeamExeFuncByLeader  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nRange) ~= "number" or nRange%1 ~= 0 or nRange <= 0 or nRange > 3 then
		Sys_SaveAbnormalLog("函数 User_TeamExeFuncByLeader 中 nRange 只能传(1,2,3)的整数")
		return
	end
	
	if type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TeamExeFuncByLeader 中 sFunc 只能传字符串")
		return
	end		
	
	return TeamExeFuncByLeader(nUserId,nRange,string.format("</F>%s",sFunc))
end

--全队执行某个LUA函数(队员可以触发)	
--TeamExeFuncByTeamer			
--参数说明：参1:玩家ID  参2:范围(1:触发者附近 2:本地图 3:所有地图)  参3:LUA函数     失败返回false，否则返回true
--返回全队执行某个LUA函数(队员可以触发)	
function User_TeamExeFuncByTeamer(nRange,sFunc,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TeamExeFuncByTeamer  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nRange) ~= "number" or nRange%1 ~= 0 or nRange <= 0 or nRange > 3 then
		Sys_SaveAbnormalLog("函数 User_TeamExeFuncByTeamer 中 nUserId 只能传(1,2,3)的整数")
		return
	end
	
	if type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_TeamExeFuncByTeamer 中 sFunc 只能传字符串")
		return
	end		
	
	return TeamExeFuncByTeamer(nUserId,nRange,string.format("</F>%s",sFunc))
end



--将玩家传送到地图的随机点
--ACTION_USER_RAND_TRANS   = 1509,     
--bool UserRandTrans(int idUser, int idMap, int nCheck);            
--参数说明: idUser表示指定玩家, 填0表示当前玩家, idMap表示指定的地图, 为0时表示玩家当前所在地图, nCheck表示检查地图属性, 非0表示要检查. 如果失败返回false, 成功返回true.
--返回将玩家传送到地图的随机点
function User_UserRandTrans(nMapId,nCheck,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandTrans  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandTrans 中 nMapId 只能传大于等于0的整数")
		return
	end
	
	if nCheck == nil then
		nCheck = 0
	elseif type(nCheck) ~= "number" or nCheck%1 ~= 0 or nCheck < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandTrans 中 nCheck 只能传大于0的整数")
		return
	end	

	return UserRandTrans(nUserId,nMapId,nCheck)
end

--将玩家传送到地图的指定区域.
--bool UserRandBoundTrans(int idUser, int idMap, int nCheck， int nBoundX, int nBoundY, int nBoundCX, int nBoundCY);
--参数说明: idUser表示指定玩家, 填0表示当前玩家, idMap表示指定的地图, 为0时表示玩家当前所在地图, nCheck表示检查地图属性，非0表示要检查;
--nBoundX, nBoundY表示这个区域的左上角的坐标, nBoundCX, nBoundCY表示这个区域的范围. 如果失败返回false, 成功返回true.
--返回将玩家传送到地图的指定区域.
function User_UserRandBoundTrans(nMapId,nBoundX,nBoundY,nBoundCX,nBoundCY,nCheck,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandBoundTrans  中 nUserId 为整型并且大于等于0")
		return
	end
	
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandBoundTrans 中 nMapId 只能传大于0的整数")
		return
	end
	
	if nCheck == nil then
		nCheck = 0
	elseif type(nCheck) ~= "number" or nCheck%1 ~= 0 or nCheck < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandBoundTrans 中 nCheck 只能传大于等于0的整数")
		return
	end	
	
	if type(nBoundX) ~= "number" or nBoundX%1 ~= 0 or nBoundX < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandBoundTrans 中 nBoundX 只能传大于等于0的整数")
		return
	end
	
	if type(nBoundY) ~= "number" or nBoundY%1 ~= 0 or nBoundY < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandBoundTrans 中 nBoundY 只能传大于等于0的整数")
		return
	end
	
	if type(nBoundCX) ~= "number" or nBoundCX%1 ~= 0 or nBoundCX < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandBoundTrans 中 nBoundCX 只能传大于等于0的整数")
		return
	end
	
	if type(nBoundCY) ~= "number" or nBoundCY%1 ~= 0 or nBoundCY < 0 then
		Sys_SaveAbnormalLog("函数 User_UserRandBoundTrans 中 nBoundCY 只能传大于等于0的整数")
		return
	end	
	
	return UserRandBoundTrans(nUserId,nMapId,nCheck,nBoundX,nBoundY,nBoundCX,nBoundCY)
end

-- 4. 设置寄存器变量值接口：void  SetUserVarData(int UserId, int nInx, int nData);
-- 参数1：玩家id，参数2：索引;3.寄存器变量值
-- 返回值：无
function User_SetVarData(nInx,nData,nUserId)
	if type(nInx) ~= "number" or nInx > 7 or nInx < 0 or nInx%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetVarData 参数 nInx 必须为整型并且在0-7之间")
		return
	end
	
	if type(nData) ~= "number" or nData < 0 or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetVarData 参数 nData 必须为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetVarData 参数 nUserId 必须为整型并且大于等于0")
		return
	end
	
	return SetUserVarData(nUserId,nInx,nData)
end


-- 6. 设置寄存器变量值字符串类型）接口：void  SetUserVarStr(int UserId, int nInx, char* pData);
-- 参数1：玩家id，参数2：索引;3.寄存器变量值
-- 返回值：无
function User_SetVarStr(nInx,sDataStr,nUserId)
	if type(nInx) ~= "number" or nInx > 7 or nInx < 0 or nInx%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetVarStr 参数 nInx 必须为整型并且在0-7之间")
		return
	end
	
	if type(sDataStr) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_SetVarStr 参数 sDataStr 必须为字符串")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetVarStr 参数 nUserId 必须为整型并且大于等于0")
		return
	end
	
	return SetUserVarStr(nUserId,nInx,sDataStr)
end

-- //ACTION_USER_CAL_EXP 1084  ACTION_USER_TIME_TO_EXP	1086
-- bool CalcUserExp(OBJID idUser, __int64 n64ExpAdd, DWORD dwIdxLev, DWORD dwIdxPercent);
-- bool CalcUserTimeToExp(OBJID idUser, DWORD dwTime, DWORD idx);
--1084	计算指定数值的exp对当前用户的影响, 输出目标级别和百分比数值到指定的寄存器变量
--// param为"exp_add idx_lev idx_percent"
--// exp_add为添加的exp值, idx_lev为目标级别输出的寄存器变量索引, idx_percent为目标百分比输出的寄存器变量索引,
--// 寄存器变量索引为 0-7
-- bool CalcUserExp(OBJID idUser, __int64 n64ExpAdd, DWORD dwIdxLev, DWORD dwIdxPercent);
function User_CalcExp(nExpAdd,nUserId)
	if type(nExpAdd) ~= "number" or nExpAdd < 0 or nExpAdd%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CalcExp 参数 nExpAdd 必须为整型并且大于等于0")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CalcExp 参数 nUserId 必须为整型并且大于等于0")
		return
	end

	return Sys_Split(CalcUserExp(nUserId,nExpAdd)," ")
end


-- 1086 // 折算一定的升级时间到当前用户的经验数字, 输出到指定的寄存器变量                                       
-- // param为"time idx_exp", time为升级时间，单位和cq_levexp表中的uplevtime相同，idx_exp为寄存器变量索引， 
-- // 寄存器变量索引为 0-7                                                                                 
-- bool CalcUserTimeToExp(OBJID idUser, DWORD dwTime, DWORD idx);
function User_CalcTimeToExp(nTime,nUserId)
	if type(nTime) ~= "number" or nTime < 0 or nTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CalcTimeToExp 参数 nTime 必须为整型并且大于等于0")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CalcTimeToExp 参数 nUserId 必须为整型并且大于等于0")
		return
	end
	
	return CalcUserTimeToExp(nUserId,nTime)
end


-- //user status
-- //ACTION_USER_STATUS 1082

--AddRoleStatus(objid idRole,int nType,int nPower,int nInterval,int ucLeaveTimes,int unRemainTime,int unEndTime,int ucRecordable,int usFrom,int unData)
--idRole 角色ID  nType 状态类型 nInterval 状态持续时间 ucLeaveTimes 状态次数 unRemainTime 剩余时间 unEndTime 有效时长 ucRecordable 是否入库 usFrom 数据来源 unData 其它内容(action传0)

-- bool DelRoleStatus(OBJID idUser, DWORD dwStatus);
-- bool ChkRoleStatus(OBJID idUser, DWORD dwStatus);
-- bool AddUserTeamStatus(OBJID idUser, DWORD dwStatus, int nPower , int nSecs, int nTimes);
-- bool DelUserTeamStatus(OBJID idUser, DWORD dwStatus);
-- bool ChkUserTeamStatus(OBJID idUser, DWORD dwStatus);
-- 1082,			// 附加或删除指定的状态，param为"obj opr status power seconds times", 
-- // obj支持"self", "mate", "couple", "team", 注意team不包含自己, 如果没有队伍，返回失败
-- //		另外，"mate"选项如果对方不在线，返回失败
-- // opr支持"add", "del", "chk"
-- // power为状态力度参数，
-- // seconds为状态延续的秒数
-- // times为状态激发的次数, 只激发一次的填0


--1000007--你把idRole改为 用户id,直接调用接口不行
function User_AddRoleStatus(nStatus,nPower,nSecs,nTimes,nunRemainTime,nunEndTime,nucRecordable,nusFrom,nunData,nUserId)
	if type(nStatus) ~= "number" or nStatus <= 0 or nStatus%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nStatus 必须为整型并且大于0")
		return
	end
	
	if type(nPower) ~= "number" or nPower < 0 or nPower%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nPower 必须为整型并且大于等于0")
		return
	end

	if type(nSecs) ~= "number" or nSecs < 0 or nSecs%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nSecs 必须为整型并且大于等于0")
		return
	end

	if type(nTimes) ~= "number" or nTimes < 0 or nTimes%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nTimes 必须为整型并且大于等于0")
		return
	end
	
	if type(nunRemainTime) ~= "number" or nunRemainTime < 0 or nunRemainTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nunRemainTime 必须为整型并且大于等于0")
		return
	end

	if type(nunEndTime) ~= "number" or nunEndTime < 0 or nunEndTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nunEndTime 必须为整型并且大于等于0")
		return
	end

	if type(nucRecordable) ~= "number" or nucRecordable < 0 or nucRecordable%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nucRecordable 必须为整型并且大于等于0")
		return
	end

	if type(nusFrom) ~= "number" or nusFrom < 0  or nusFrom%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nusFrom 必须为整型并且大于等于0")
		return
	end

	if type(nunData) ~= "number" or nunData < 0  or nunData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nunData 必须为整型并且大于等于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId <= 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRoleStatus 参数 nUserId 必须为整型并且大于等于0")
		return
	end
	
	return AddRoleStatus(nUserId,nStatus,nPower,nSecs,nTimes,nunRemainTime,nunEndTime,nucRecordable,nusFrom,nunData)
end

function User_DelRoleStatus(nStatus,nUserId)
	if type(nStatus) ~= "number" or nStatus <= 0 or nStatus%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelRoleStatus 参数 nStatus 必须为整型并且大于0")
		return
	end
	
	if type(nUserId) ~= "number" or nUserId <= 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelRoleStatus 参数 nUserId 必须为整型并且大于等于0")
		return
	end

	return DelRoleStatus(nUserId,nStatus)
end

function User_ChkRoleStatus(nStatus,nUserId)
	if type(nStatus) ~= "number" or nStatus <= 0 or nStatus%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkRoleStatus 参数 nStatus 必须为整型并且大于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId <= 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkRoleStatus 参数 nUserId 必须为整型并且大于等于0")
		return
	end

	return ChkRoleStatus(nUserId,nStatus)
end

-- bool AddUserTeamStatus(OBJID idUser, DWORD dwStatus, int nPower , int nSecs, int nTimes);
function User_AddTeamStatus(nStatus,nPower,nSecs,nTimes,nUserId)
	if type(nStatus) ~= "number" or nStatus <= 0 or nStatus%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddTeamStatus 参数 nStatus 必须为整型并且大于0")
		return
	end

	if type(nPower) ~= "number" or nPower < 0 or nPower%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddTeamStatus 参数 nPower 必须为整型并且大于等于0")
		return
	end

	if type(nSecs) ~= "number" or nSecs < 0 or nSecs%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddTeamStatus 参数 nSecs 必须为整型并且大于等于0")
		return
	end
	
	if type(nTimes) ~= "number" or nTimes < 0 or nTimes%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddTeamStatus 参数 nTimes 必须为整型并且大于等于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId <= 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddTeamStatus 参数 nUserId 必须为整型并且大于等于0")
		return
	end

	return AddUserTeamStatus(nUserId,nStatus,nPower,nSecs,nTimes)
end

-- bool DelUserTeamStatus(OBJID idUser, DWORD dwStatus);
function User_DelTeamStatus(nStatus,nUserId)
	if type(nStatus) ~= "number" or nStatus <= 0 or nStatus%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelTeamStatus 参数 nStatus 必须为整型并且大于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId <= 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelTeamStatus 参数 nUserId 必须为整型并且大于等于0")
		return
	end

	return DelUserTeamStatus(nUserId,nStatus)
end

-- bool ChkUserTeamStatus(OBJID idUser, DWORD dwStatus);
function User_ChkTeamStatus(nStatus,nUserId)
	if type(nStatus) ~= "number" or nStatus <= 0 or nStatus%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkTeamStatus 参数 nStatus 必须为整型并且大于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId <= 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkTeamStatus 参数 nUserId 必须为整型并且大于0")
		return
	end

	return ChkUserTeamStatus(nUserId,nStatus)
end


-----------20140822---------
--// 记录玩家练气和武功日志. 参数说明: idUser表示玩家ID. 如果失败返回false, 成功返回true.
--LUA_FUNC(GongfuAndFateValueLog)
--	OBJID idUser = Lua_GetParamUInt(1);
--生成文件在gmlog/syn_templog
function User_GongfuAndFateValueLog(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_GongfuAndFateValueLog 中 nUserId 只能传大于等于0的整数")
		return
	end

	return GongfuAndFateValueLog(nUserId)
end




-- //检查背包剩余空间是否有data数量大小   	参1:玩家ID，参2:格子数量，	有队员空间小于nData就返回false，否则返回true
-- LUA_FUNC(TeamLeaveSpace)
	-- OBJID idUser		= Lua_GetParamUInt(1);
	-- int nData			= Lua_GetParamUInt(2);
--只能队长触发，所有队员触发都返回false
function User_ChkTeamSpace(nSpace,nUserId)
	if type(nSpace) ~= "number" or nSpace <= 0 or nSpace%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkTeamSpace 中 nSpace 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkTeamSpace 中 nUserId 只能传大于等于0的整数")
		return
	end

	return TeamLeaveSpace(nUserId,nSpace)
end


-- //队伍成员添加物品(在调用此接口的时候必须先调用TeamLeaveSpace来判断背包空间)   	参1:玩家ID，参2:物品类型ID，		失败返回false，否则返回true
-- LUA_FUNC(TeamAddItem)
	-- OBJID idUser		= Lua_GetParamUInt(1);
	-- OBJID idItemType	= Lua_GetParamUInt(2);

function User_AddTeamItem(nItemtype,nUserId)
	if type(nItemtype) ~= "number" or nItemtype <= 0 or nItemtype%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddTeamItem 中 nItemtype 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddTeamItem 中 nUserId 只能传大于等于0的整数")
		return
	end

	if User_ChkTeamSpace(1) then
		return TeamAddItem(nUserId,nItemtype)
	else 
		Sys_SaveAbnormalLog("队伍中有玩家的背包空间不足，请提醒队员整理背包。")
		return
	end

end



-- //队伍成员删除物品(在调用此接口之前必须先调用TeamCheckItem接口判断是否有物品)   	参1:玩家ID，参2:物品类型ID，		失败返回false，否则返回true
-- LUA_FUNC(TeamDelItem)
	-- OBJID idUser		= Lua_GetParamUInt(1);
	-- OBJID idItemType	= Lua_GetParamUInt(2);
-- //队伍成员检查是否有某物品   	参1:玩家ID，参2:物品类型ID，		失败返回false，否则返回true

function User_DelTeamItem(nItemtype,nUserId)

	if type(nItemtype) ~= "number" or nItemtype <= 0 or nItemtype%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelTeamItem 中 nItemtype 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelTeamItem 中 nUserId 只能传大于等于0的整数")
		return
	end

	if User_ChkTeamItem(nItemtype) then
		return TeamDelItem(nUserId,nItemtype)
	else 
		Sys_SaveAbnormalLog("队伍中有玩家没有该物品，请提醒队员检查是否有该物品。")
		return
	end

end



-- LUA_FUNC(TeamCheckItem)
	-- OBJID idUser		= Lua_GetParamUInt(1);
	-- OBJID idItemType	= Lua_GetParamUInt(2);
--所有成员都有才ture，否则返回false
function User_ChkTeamItem(nItemtype,nUserId)
	if type(nItemtype) ~= "number" or nItemtype <= 0 or nItemtype%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkTeamItem 中 nItemtype 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkTeamItem 中 nUserId 只能传大于等于0的整数")
		return
	end

	return TeamCheckItem(nUserId,nItemtype)
end

-- //脱下装备   	参1:玩家ID，		失败返回false，否则返回true
-- LUA_FUNC(UnequipItem)
	-- OBJID idUser	= Lua_GetParamUInt(1);
	-- int nPosition	= Lua_GetParamUInt(2);

function User_UnequipItem(nPos,nUserId)
	if type(nPos) ~= "number" or nPos < 0 or nPos > 8 or nPos%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_UnequipItem 中 nPos 只能传1~8的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_UnequipItem 中 nUserId 只能传大于等于0的整数")
		return
	end

	return UnequipItem(nUserId,nPos)
end

-- //武器技能等级检查		参1: 玩家ID，参2:技能类型ID，参3：等级	小于nLev返回false，否则返回true
-- LUA_FUNC(SkillCheckLev)
	-- OBJID idUser	= Lua_GetParamInt(1);
	-- OBJID idType	= Lua_GetParamInt(2);
	-- int	  nLev		= Lua_GetParamInt(3);

function User_SkillChkLev(nSkillType,nLev,nUserId)
	if type(nSkillType) ~= "number" or nSkillType <= 0 or nSkillType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillChkLev 中 nSkillType 只能传大于0的整数")
		return
	end

	if type(nLev) ~= "number" or nLev <= 0 or nLev%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillChkLev 中 nTime 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillChkLev 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SkillCheckLev(nUserId,nSkillType,nLev)
end


-- //学习武器技能		参1: 玩家ID，参2:技能类型ID，参3：等级	失败返回false，否则返回true
-- LUA_FUNC(LearnSkill)
	-- OBJID idUser	= Lua_GetParamInt(1);
	-- OBJID idType	= Lua_GetParamInt(2);
	-- int	  nLev		= Lua_GetParamInt(3);
--可增可减，只要武器技能编号存在就可以设置。
--执行成功后，level字段等于nlev参数。
function User_SkillLearn(nSkillType,nLev,nUserId)
	if type(nSkillType) ~= "number" or nSkillType <= 0 or nSkillType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillLearn 中 nSkillType 只能传大于0的整数")
		return
	end

	if type(nLev) ~= "number" or nLev <= 0 or nLev%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillLearn 中 nTime 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillLearn 中 nUserId 只能传大于等于0的整数")
		return
	end

	return LearnSkill(nUserId,nSkillType,nLev)
end

-- //增加武器技能经验		参1: 玩家ID，参2:技能类型ID，参3：经验。	失败返回false，否则返回true
-- LUA_FUNC(SkillAddExp)
	-- OBJID idUser	= Lua_GetParamInt(1);
	-- OBJID idType	= Lua_GetParamInt(2);
	-- int	  nExp		= Lua_GetParamInt(3);
--客户端会立刻显示，但数据库不会时时保存。
--未学过的武器熟练度，数据库会新建数据，但客户端看不到。
function User_SkillAddExp(nSkillType,nExp,nUserId)
	if type(nSkillType) ~= "number" or nSkillType <= 0 or nSkillType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillAddExp 中 nSkillType 只能传大于0的整数")
		return
	end

	if type(nExp) ~= "number" or nExp <= 0 or nExp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillAddExp 中 nExp 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillAddExp 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SkillAddExp(nUserId,nSkillType,nExp)
end


-- //增加武器技能经验		参1: 玩家ID，参2:技能类型ID，参3：时间(程序将换算成经验)。	失败返回false，否则返回true
-- LUA_FUNC(SkillAddLevTime)
	-- OBJID idUser	= Lua_GetParamInt(1);
	-- OBJID idType	= Lua_GetParamInt(2);
	-- int	  nTime		= Lua_GetParamInt(3);
--客户端会立刻显示，但数据库不会时时保存。
--未学过的武器熟练度，数据库会新建数据，但客户端看不到。
function User_SkillAddExpByTime(nSkillType,nTime,nUserId)
	if type(nSkillType) ~= "number" or nSkillType <= 0 or nSkillType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillAddExpByTime 中 nSkillType 只能传大于0的整数")
		return
	end

	if type(nTime) ~= "number" or nTime <= 0 or nTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillAddExpByTime 中 nTime 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SkillAddExpByTime 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SkillAddExp(nUserId,nSkillType,nTime)
end


-- //掷骰子分配物品		参1:玩家ID，参2:物品类型ID，参3：分配类型(目前只有0这一种)		失败返回false，否则返回true
-- LUA_FUNC(TeamDice)
	-- OBJID	idUser		= Lua_GetParamUInt(1);
	-- OBJID	idItemType	= Lua_GetParamUInt(2);
	-- int		nType		= Lua_GetParamUInt(3);
--队员队长都可以触发。
function User_TeamDice(nItemtype,nUserId)
	if type(nItemtype) ~= "number" or nItemtype <= 0 or nItemtype%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_TeamDice 中 nItemtype 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_TeamDice 中 nUserId 只能传大于等于0的整数")
		return
	end

	return TeamDice(nUserId,nItemtype,0)
end


-- //玩家最后一次离线时间相关操作		参1:玩家ID，参2:操作方式，参3：存取		失败返回false，否则返回true
-- LUA_FUNC(UserLastLoginOperator)
	-- OBJID	idUser		= Lua_GetParamUInt(1);
	-- OBJID	nType		= Lua_GetParamUInt(2);
	-- UINT	unParam		= Lua_GetParamUInt(3);

--nTime的格式为"yyyymmdd"，例如："20070405"
--如果最后一次离线时间，比输入的时间参数早，则返回false。
--反之，返回ture。
function User_LastLoginOperatorByTime(nTime,nUserId)
	if nTime == nil then
		nTime = os.date("%Y%m%d")
	elseif type(nTime) ~= "number" or nTime <= 0 or nTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_LastLoginOperatorByTime 中 nTime 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_LastLoginOperatorByTime 中 nUserId 只能传大于等于0的整数")
		return
	end
	BrocastMsg(2011,tostring(UserLastLoginOperator(nUserId,0,nTime)))
	return UserLastLoginOperator(nUserId,0,nTime)
end




-- //判定玩家身上是否有光环，参数说明： idUser玩家ID ,有光环返回真，无光环返回假
-- LUA_FUNC(IsExistHalo)
	-- OBJID idUser = Lua_GetParamUInt(1);

function User_IsExistHalo(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsExistHalo 中 nUserId 只能传大于等于0的整数")
		return
	end

	return IsExistHalo(nUserId)
end


-- // 删除玩家身上的光环，参数说明： idUser玩家ID
-- LUA_FUNC(ClsHalo)
	-- OBJID idUser = Lua_GetParamUInt(1);
--不论是否有无光环，删除玩家身上所有光环，且永远返回ture。
function User_DelHalo(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelHalo 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	if User_IsExistHalo(nUserId) then
		return ClsHalo(nUserId)
	else
		return true
	end
end

-- //玩家保存指定信息到gm log	参数说明：szParam 指定要保存的信息
-- LUA_FUNC(MsgToGMLog)
	-- const char* szParam= Lua_GetParamString(1);
--生成的log在gmlog/月份/action.log下
function User_GmLog(sLogParam)
	if type(sLogParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_GmLog 中 sLogParam 只能传字符串类型的参数")
		return
	end

	return MsgToGMLog(sLogParam)
end

--SCRIPT_PARAM_PLAYER_LookFace,	1003	//玩家头像编号	get 	set	
--玩家外形会直接改变
function User_SetLookFace(nFace,nUserId)
	if type(nFace) ~= "number" or nFace <= 0 or nFace%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetLookFace 中 nFace 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetLookFace 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_LookFace,nFace,0)
end

--SCRIPT_PARAM_PLAYER_Profession,	1005	//玩家的职业	get 	set	
--若职业编号不存在，能执行成功，但是人物面板的职业描述会出现？？
function User_SetProfession(nProfession,nUserId)
	if type(nProfession) ~= "number" or nProfession <= 0 or nProfession%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetProfession 中 nProfession 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetProfession 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_Profession,nProfession,0)
end

--只能传0，变成成初始状态。
--SCRIPT_PARAM_PLAYER_Transfrom,	1024	//变身ID	get	set	
function User_SetTransform(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetTransform 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_Transfrom,0,0)
end

--SCRIPT_PARAM_PLAYER_Crime,	1038	//犯罪时间	get	set	
--设置不为0，玩家进入pk后的蓝名闪烁状态
function User_SetCrime(nCrime,nUserId)
	if type(nCrime) ~= "number" or nCrime <= 0 or nCrime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetCrime 中 nCrime 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetCrime 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_Crime,nCrime,0)
end

--SCRIPT_PARAM_PLAYER_XP,	1041	//当前XP值	get	set	add
function User_SetXp(nXp,nUserId)
	if type(nXp) ~= "number" or nXp < 0 or nXp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetXp 中 nXp 只能传大于等于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetXp 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_XP,nXp,0)
end

--SCRIPT_PARAM_PLAYER_EP,	1042	//体力值	get	set	add
function User_SetEp(nEp,nUserId)
	if type(nEp) ~= "number" or nEp < 0 or nEp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetEp 中 nEp 只能传大于等于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetEp 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_EP,nEp,0)
end

--SCRIPT_PARAM_PLAYER_AddPoint,	1043	//玩家属性点	get	set	add
function User_SetAddPoint(nAddPoint,nUserId)
	if type(nAddPoint) ~= "number" or nAddPoint < 0 or nAddPoint%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetAddPoint 中 nAddPoint 只能传大于等于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetAddPoint 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_AddPoint,nAddPoint,0)
end

--SCRIPT_PARAM_PLAYER_PKProtocol,	1054	//PK模式	get	set	
--修改cq_user表的PKProtocol字段
--重新登录才有效。
function User_SetPkProtocol(nPkProtocol,nUserId)
	if  nPkProtocol == 0 or nPkProtocol == 1 or nPkProtocol == 2 then
		Sys_SaveAbnormalLog("函数 User_SetPkProtocol 中 nPkProtocol 只能传0,1,2.")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetPkProtocol 中 nUserId 只能传大于等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_PKProtocol,nPkProtocol,0)
end

--SCRIPT_PARAM_PLAYER_Level,	1006	//玩家等级	get		add
function User_AddLevel(nAddLevel,nUserId)
	if type(nAddLevel) ~= "number" or nAddLevel <= 0 or nAddLevel%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddLevel 中 nAddLevel 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddLevel 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Level,nAddLevel,0)
end

--SCRIPT_PARAM_PLAYER_Life,	1019	//玩家生命值	get		add
function User_AddLife(nAddLife,nUserId)
	if type(nAddLife) ~= "number" or nAddLife%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddLife 中 nAddLife 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddLife 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Life,nAddLife,0)
end

--SCRIPT_PARAM_PLAYER_Mana,	1021	//玩家法力值	get		add
function User_AddMana(nAddMana,nUserId)
	if type(nAddMana) ~= "number" or nAddMana%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMana 中 nAddMana 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMana 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Mana,nAddMana,0)
end

--SCRIPT_PARAM_PLAYER_Mentor,	1023	//玩家点化机会	get		add
--100点=1次点化机会
function User_AddMentor(nAddMentor,nUserId)
	if type(nAddMentor) ~= "number" or nAddMentor <= 0 or nAddMentor%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMentor 中 nAddMentor 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMentor 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Mentor,nAddMentor,0)
end

--SCRIPT_PARAM_PLAYER_Money,	1025	//玩家游戏币	get		add
function User_AddMoney(nAddMoney,nUserId)
	if type(nAddMoney) ~= "number" or nAddMoney%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMoney 中 nAddMoney 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMoney 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Money,nAddMoney,0)
end

--SCRIPT_PARAM_PLAYER_EMoney,	1027	//玩家天石	get		add
function User_AddEMoney(nAddEMoney,nUserId)
	if type(nAddEMoney) ~= "number" or nAddEMoney%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddEMoney 中 nAddEMoney 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddEMoney 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_EMoney,nAddEMoney,0)
end

--SCRIPT_PARAM_PLAYER_EMoneyMono,	1028	//玩家赠点	get		add
function User_AddEMoneyMono(nAddEMoneyMono,nUserId)
	if type(nAddEMoneyMono) ~= "number" or nAddEMoneyMono%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddEMoneyMono 中 nAddEMoneyMono 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddEMoneyMono 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_EMoneyMono,nAddEMoneyMono,0)
end

--SCRIPT_PARAM_PLAYER_Exp,	1029	//玩家经验(add操作不增加贡献)	get		add
function User_AddExp(nAddExp,nUserId)
	if type(nAddExp) ~= "number" or nAddExp <= 0 or nAddExp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExp 中 nAddExp 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExp 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Exp,nAddExp,0)
end

--SCRIPT_PARAM_PLAYER_ExpContribute,	1030	//玩家经验(add操作增加贡献)			add
function User_AddExpContribute(nAddExpContribute,nUserId)
	if type(nAddExpContribute) ~= "number" or nAddExpContribute <= 0 or nAddExpContribute%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpContribute 中 nAddExpContribute 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpContribute 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_ExpContribute,nAddExpContribute,0)
end


-- SCRIP_PARAM_PLAYER_ExpPercent	1065 //按百分比增加经验(不增加贡献)。
function User_AddExpPercent(nAddPercent,nUserId)
	if type(nAddPercent) ~= "number" or nAddPercent < 0 or nAddPercent > 100 or nAddPercent%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpPercent 中 nAddPercent 只能传0-100的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpPercent 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_ExpPercent,nAddPercent,0)
end


-- SCRIP_PARAM_PLAYER_ExpPercentContribute	1066 //按百分比增加经验(增加贡献)
function User_AddExpPercentContribute(nAddPercentContribute,nUserId)
	if type(nAddPercentContribute) ~= "number" or nAddPercentContribute < 0 or nAddPercentContribute > 100 or nAddPercentContribute%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpPercentContribute 中 nAddPercentContribute 只能传0-100的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpPercentContribute 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_ExpPercentContribute,nAddPercentContribute,0)
end

-- //增加玩家的经验时间（折算一定的升级时间到当前用户的经验）
-- //注意：nTime为增加经验的时间。单位是分钟，
-- //用到的接口：	AddUserInt 程序直接加经验
-- //User_AddExpTime(30)表示增加当前玩家30分钟经验。
function User_AddExpTime(nTime,nUserId)
	if type(nTime) ~= "number" or nTime < 0 or nTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpTime 参数 nTime 必须为整型并且大于等于0")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpTime 参数 nUserId 必须为整型并且大于等于0")
		return
	end
	
	local nAddTime = nTime * 10

	return AddUserInt(nUserId,G_PLAYER_ExpTime,nAddTime,0)
end

--SCRIPT_PARAM_PLAYER_PK,	1031	//玩家PK值	get		add
--30变红，100变黑
function User_AddPk(nAddPk,nUserId)
	if type(nAddPk) ~= "number" or nAddPk%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddPk 中 nAddPk 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddPk 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_PK,nAddPk,0)
end

--SCRIPT_PARAM_PLAYER_Strength,	1032	//玩家力量值	get		add
--建议不要使用，原因是，执行完后，面板上剩余点数显示很奇怪。
--当加数值时，如果面板在打开状态，不会直接加在力量值，而是剩余点数显示出加上的点数，且不会出现加点按钮。面板切换后，显示正常。
--当减数值时，情况同加法，只是在剩余数值部分会显示负数。
function User_AddStrength(nAddStrength,nUserId)
	if type(nAddStrength) ~= "number" or nAddStrength%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddStrength 中 nAddStrength 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddStrength 中 nUserId 只能传大于等于0的整数")
		return
	end

	if nAddStrength < 0 then
		local nStrength = Get_UserStrength()
		
		if nStrength + nAddStrength < 0 then
			Sys_SaveAbnormalLog(string.format("函数 User_AddStrength 中力量值不足%d",math.abs(nAddStrength)))
			return
		end
		
		if AddUserInt(nUserId,G_PLAYER_Strength,nAddStrength,0) then
			return User_AddAddPoint(math.abs(nAddStrength),nUserId)
		end
	else
		return AddUserInt(nUserId,G_PLAYER_Strength,nAddStrength,0)
	end
end

--SCRIPT_PARAM_PLAYER_Speed,	1033	//玩家灵巧值	get		add
function User_AddSpeed(nAddSpeed,nUserId)
	if type(nAddSpeed) ~= "number" or nAddSpeed%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddSpeed 中 nAddSpeed 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddSpeed 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	if nAddSpeed < 0 then
		local nSpeed = Get_UserSpeed()
		
		if nSpeed + nAddSpeed < 0 then
			Sys_SaveAbnormalLog(string.format("函数 User_AddSpeed 中灵巧值不足%d",math.abs(nAddSpeed)))
			return
		end
		
		if AddUserInt(nUserId,G_PLAYER_Speed,nAddSpeed,0) then
			return User_AddAddPoint(math.abs(nAddSpeed),nUserId)
		end
	else
		return AddUserInt(nUserId,G_PLAYER_Speed,nAddSpeed,0)
	end
end

--SCRIPT_PARAM_PLAYER_Health,	1034	//玩家体力值	get		add
function User_AddHealth(nAddHealth,nUserId)
	if type(nAddHealth) ~= "number" or nAddHealth%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddHealth 中 nAddHealth 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddHealth 中 nUserId 只能传大于等于0的整数")
		return
	end

	if nAddHealth < 0 then
		local nHealth = Get_UserHealth()
		
		if nHealth + nAddHealth < 0 then
			Sys_SaveAbnormalLog(string.format("函数 User_AddHealth 中体力值不足%d",math.abs(nAddHealth)))
			return
		end
		
		if AddUserInt(nUserId,G_PLAYER_Health,nAddHealth,0) then
			return User_AddAddPoint(math.abs(nAddHealth),nUserId)
		end
	else
		return AddUserInt(nUserId,G_PLAYER_Health,nAddHealth,0)
	end
end

--SCRIPT_PARAM_PLAYER_Soul,	1035	//玩家精神值	get		add
function User_AddSoul(nAddSoul,nUserId)
	if type(nAddSoul) ~= "number" or nAddSoul%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddSoul 中 nAddSoul 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddSoul 中 nUserId 只能传大于等于0的整数")
		return
	end

	if nAddSoul < 0 then
		local nSoul = Get_UserSoul()
		
		if nSoul + nAddSoul < 0 then
			Sys_SaveAbnormalLog(string.format("函数 User_AddSoul 中精神值不足%d",math.abs(nAddSoul)))
			return
		end
		
		if AddUserInt(nUserId,G_PLAYER_Soul,nAddSoul,0) then
			return User_AddAddPoint(math.abs(nAddSoul),nUserId)
		end
	else
		return AddUserInt(nUserId,G_PLAYER_Soul,nAddSoul,0)
	end
end

--SCRIPT_PARAM_PLAYER_XP,	1041	//当前XP值	get	set	add
--传入负数无效
function User_AddXp(nAddXp,nUserId)
	if type(nAddXp) ~= "number" or nAddXp <= 0 or nAddXp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddXp 中 nAddXp 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddXp 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_XP,nAddXp,0)
end

--SCRIPT_PARAM_PLAYER_EP,	1042	//体力值	get	set	add
function User_AddEp(nAddEp,nUserId)
	if type(nAddEp) ~= "number" or nAddEp%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddEp 中 nAddEp 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddEp 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_EP,nAddEp,0)
end

--SCRIPT_PARAM_PLAYER_AddPoint,	1043	//玩家属性点	get	set	add
--当传入负数，若超出玩家当前剩余点数，会出现错误，剩余点数变成65535-扣太多的点数。
--所以，干脆把负数禁掉。
function User_AddAddPoint(nAddAddPoint,nUserId)
	if type(nAddAddPoint) ~= "number" or nAddAddPoint <=0 or nAddAddPoint%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAddPoint 中 nAddAddPoint 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAddPoint 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_AddPoint,nAddAddPoint,0)
end

--SCRIPT_PARAM_PLAYER_StorageMoney,	1049	//玩家存储的钱	get		add
--当扣掉的钱大于玩家当前仓库的钱的时，不执行。
function User_AddStorageMoney(nAddStorageMoney,nUserId)
	if type(nAddStorageMoney) ~= "number" or nAddStorageMoney%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddStorageMoney 中 nAddStorageMoney 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddStorageMoney 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_StorageMoney,nAddStorageMoney,0)
end

--SCRIPT_PARAM_PLAYER_AddMount,	1052	//坐骑移动力			add
--输入负数无效
function User_AddMount(nAddMount,nUserId)
	if type(nAddMount) ~= "number" or nAddMount <= 0 or nAddMount%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMount 中 nAddMount 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddMount 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_AddMount,nAddMount,0)
end

--SCRIPT_PARAM_PLAYER_Cultivation,	1053	//修行值	get		add
function User_AddCultivation(nAddCultivation,nUserId)
	if type(nAddCultivation) ~= "number" or nAddCultivation <= 0 or nAddCultivation%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddCultivation 中 nAddCultivation 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddCultivation 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Cultivation,nAddCultivation,0)
end

--SCRIPT_PARAM_PLAYER_StrengthValue,	1055	//气力值	get		add
function User_AddStrengthValue(nAddStrengthValue,nUserId)
	if type(nAddStrengthValue) ~= "number" or nAddStrengthValue <= 0 or nAddStrengthValue%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddStrengthValue 中 nAddStrengthValue 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddStrengthValue 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_StrengthValue,nAddStrengthValue,0)
end

--SCRIPT_PARAM_PLAYER_GodBless,	1060	//获得祝福剩余时间	get		add
--单位小时，负数无效，所以禁掉。
function User_AddBless(nAddBless,nUserId)
	if type(nAddBless) ~= "number" or nAddBless <=0 or nAddBless%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddBless 中 nAddBless 只能传大于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddBless 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_GodBless,nAddBless,0)
end

-- /玩家添加道具状态，参数说明： idUser玩家ID , idProp状态类型ID, unRemainTime 剩余时间
-- LUA_FUNC(AddPropStatus)
	-- OBJID idUser		   = Lua_GetParamUInt(1);
	-- infoProps.idProp	   = Lua_GetParamULong(2);
	-- infoProps.unRemainTime = Lua_GetParamUInt(3);
function User_AddPropStatus(nPropId,nUnRemainTime,nUserId)
	if type(nPropId) ~= "number" or nPropId < 0 or nPropId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddPropStatus 第1个参数nPropId必须为整型并且大于等于0")
		return
	end

	if type(nUnRemainTime) ~= "number" or nUnRemainTime < 0 or nUnRemainTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddPropStatus 第2个参数nUnRemainTime必须为整型并且大于等于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddPropStatus 第3个参数nUserId必须为整型并且大于等于0")
		return
	end
	
	return AddPropStatus(nUserId,nPropId,nUnRemainTime)
end


-- //删除玩家所有的物暴、法暴等特殊属性状态，参数说明： idUser玩家ID
-- LUA_FUNC(DelAllAttribStatus)
	-- OBJID idUser = Lua_GetParamUInt(1);
function User_DelAllAttribStatus(nUserId)
	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelAllAttribStatus 第1个参数nUserId必须为整型并且大于等于0")
		return
	end
	
	return DelAllAttribStatus(nUserId)
end

-- // 设置玩家死亡，并指定重生地图地点. 参数说明: idUser表示玩家ID; idRebornMap表示重生地图ID; nPosX, nPosY表示坐标. 如果失败返回false, 成功返回true.
-- LUA_FUNC(UserDie)
	-- OBJID idUser = Lua_GetParamUInt(1);
	-- OBJID idRebornMap = Lua_GetParamUInt(2);
	-- int nPosX = Lua_GetParamInt(3);
	-- int nPosY = Lua_GetParamInt(4);
function User_Die(nRebornMap,nPosX,nPosY,nUserId)
	if type(nRebornMap) ~= "number" or nRebornMap < 0 or nRebornMap%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_Die 第1个参数nRebornMap必须为整型并且大于等于0")
		return
	end

	if type(nPosX) ~= "number" or nPosX < 0 or nPosX%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_Die 第2个参数nPosX必须为整型并且大于等于0")
		return
	end

	if type(nPosY) ~= "number" or nPosY < 0 or nPosY%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_Die 第3个参数nPosY必须为整型并且大于等于0")
		return
	end
	
	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_Die 第4个参数nUserId必须为整型并且大于等于0")
		return
	end

	return UserDie(nUserId,nRebornMap,nPosX,nPosY)
end

-- //设置玩家功夫属性的整型属性，参数说明：idUser指要操作的USERID, nIdx指SCRIPT_PARAM_GONGFU_ATTR_BEGIN-SCRIPT_PARAM_GONGFU_ATTR_END的枚举值，nData表示要设置的值，无返回值。
-- LUA_FUNC(SetGongFuInt)
	-- int idUser	= Lua_GetParamInt(1);
	-- int nIdx	= Lua_GetParamInt(2);
	-- int nData	= Lua_GetParamInt(3);
function User_SetGongFuInt(nIdx,nData,nUserId)
	if type(nIdx) ~= "number" or nIdx < 0 or nIdx%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetGongFuInt 第1个参数nIdx必须为整型并且大于等于0")
		return
	end

	if type(nData) ~= "number" or nData < 0 or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetGongFuInt 第2个参数nData必须为整型并且大于等于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetGongFuInt 第3个参数nUserId必须为整型并且大于等于0")
		return
	end
	
	if IsUserAlreadyCreateGongFu(nUserId) then
		return SetGongFuInt(nUserId,nIdx,nData)
	else
		Sys_SaveAbnormalLog("函数 User_SetGongFuInt 中玩家还没有自创武功")
		return
	end
end

-- //设置玩家功夫属性的免费修炼次数
-- nData表示要设置的值，无返回值。
function User_SetGongFuFreePractNum(nData,nUserId)
	if type(nData) ~= "number" or nData < 0 or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetGongFuFreePractNum 第1个参数nData必须为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetGongFuFreePractNum 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	if IsUserAlreadyCreateGongFu(nUserId) then
		return SetGongFuInt(nUserId,G_GONGFU_ATTR_FREE_CULTIVATE_PARAM,nData)
	else
		Sys_SaveAbnormalLog("函数 User_SetGongFuFreePractNum 中玩家还没有自创武功")
		return
	end
end

-- //设置玩家功夫属性的真气等级
-- nData表示要设置的值，无返回值。
function User_SetGongFuQiLeve(nData,nUserId)
	if type(nData) ~= "number" or nData < 0 or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetGongFuQiLeve 第1个参数nData必须为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetGongFuQiLeve 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	if IsUserAlreadyCreateGongFu(nUserId) then
		return SetGongFuInt(nUserId,G_GONGFU_ATTR_GENUINEQI_LV,nData)
	else
		Sys_SaveAbnormalLog("函数 User_SetGongFuQiLeve 中玩家还没有自创武功")
		return
	end
end

-- //增加玩家功夫属性的整型属性值，参数说明：idUser指要操作的USERID, nIdx指SCRIPT_PARAM_GONGFU_ATTR_BEGIN-SCRIPT_PARAM_GONGFU_ATTR_END的枚举值，nData表示要设置的值，无返回值。
-- LUA_FUNC(AddGongFuInt)
	-- int idUser	= Lua_GetParamInt(1);
	-- int nIdx	= Lua_GetParamInt(2);
	-- int nData	= Lua_GetParamInt(3);
function User_AddGongFuInt(nIdx,nData,nUserId)
	if type(nIdx) ~= "number" or nIdx < 0 or nIdx%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddGongFuInt 第1个参数nIdx必须为整型并且大于等于0")
		return
	end

	if type(nData) ~= "number" or nData < 0 or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddGongFuInt 第2个参数nData必须为整型并且大于等于0")
		return
	end

	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddGongFuInt 第3个参数nUserId必须为整型并且大于等于0")
		return
	end

	return AddGongFuInt(nUserId,nIdx,nData)
end

-- //增加玩家功夫属性的免费修炼次数
-- nData表示要增加的值，无返回值。
function User_AddGongFuFreePractNum(nData,nUserId)
	if type(nData) ~= "number" or nData < 0 or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddGongFuFreePractNum 第1个参数nData必须为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddGongFuFreePractNum 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	if IsUserAlreadyCreateGongFu(nUserId) then
		return AddGongFuInt(nUserId,G_GONGFU_ATTR_FREE_CULTIVATE_PARAM,nData)
	else
		Sys_SaveAbnormalLog("函数 User_AddGongFuFreePractNum 中玩家还没有自创武功")
		return
	end
end

-- //增加玩家功夫属性的真气等级
-- nData表示要增加的值，无返回值。
function User_AddGongFuQiLeve(nData,nUserId)
	if type(nData) ~= "number" or nData < 0 or nData%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddGongFuQiLeve 第1个参数nData必须为整型并且大于等于0")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddGongFuQiLeve 第2个参数nUserId必须为整型并且大于等于0")
		return
	end

	if IsUserAlreadyCreateGongFu(nUserId) then
		return AddGongFuInt(nUserId,G_GONGFU_ATTR_GENUINEQI_LV,nData)
	else
		Sys_SaveAbnormalLog("函数 User_AddGongFuQiLeve 中玩家还没有自创武功")
		return
	end
end

-- //判断是否已经创建过功夫，参数说明：idUser用户ID， 返回值：已经创建返回true，否则返回false
-- LUA_FUNC(IsUserAlreadyCreateGongFu)
	-- OBJID idUser			= Lua_GetParamULong(1);
function User_IsAlreadyCreateGongFu(nUserId)
	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsAlreadyCreateGongFu 第1个参数nUserId必须为整型并且大于等于0")
		return
	end

	return IsUserAlreadyCreateGongFu(nUserId)
end

-- //队长添加周围成员状态，参数说明：idUser角色ID	nType 状态类型, nPower  状态效果，nInterval 状态持续时间，ucLeaveTimes状态次数, unRemainTime 剩余时间, unEndTime 有效时长 , ucRecordable 是否入库, usFrom 数据来源 , unData 其它内容（action传0）
-- LUA_FUNC(AddUserAroundTeamerStatus)
	-- OBJID idUser			= Lua_GetParamULong(1);
	-- statusInfo.nType		= Lua_GetParamInt(2);
	-- statusInfo.nPower	    = Lua_GetParamInt(3);
	-- statusInfo.nInterval	= Lua_GetParamInt(4);
	-- statusInfo.ucLeaveTimes = (UCHAR)Lua_GetParamUInt(5);
	-- statusInfo.unRemainTime = Lua_GetParamUInt(6);
	-- statusInfo.unEndTime 	= Lua_GetParamUInt(7);
	-- statusInfo.ucRecordable = (UCHAR) Lua_GetParamUInt(8);
	-- USHORT usFrom		    = Lua_GetParamUInt(9);
	-- statusInfo.unData		= Lua_GetParamUInt(10);

function User_AddAroundTeamerStatus(nType,nPower,nInterval,nLeaveTimes,nRemainTime,nEndTime,nRecordable,nFrom,nData,nUserId)
	if type(nType) ~= "number" or nType%1 ~= 0 or nType <= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nMagicType 只能传大于0的整数")
		return
	end
	
	if type(nPower) ~= "number" or nPower%1 ~= 0 or nPower < 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nPower 只能传大于等于0的整数")
		return
	end
	
	if type(nInterval) ~= "number" or nInterval%1 ~= 0 or nInterval <= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nInterval 只能传大于0的整数")
		return
	end
		
	if type(nLeaveTimes) ~= "number" or nLeaveTimes%1 ~= 0 or nLeaveTimes <= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nLeaveTimes 只能传大于0的整数")
		return
	end	

	if type(nRemainTime) ~= "number" or nRemainTime%1 ~= 0 or nRemainTime <= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nRemainTime 只能传大于0的整数")
		return
	end	

	if type(nEndTime) ~= "number" or nEndTime%1 ~= 0 or nEndTime <= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nEndTime 只能传大于0的整数")
		return
	end	

	if type(nRecordable) ~= "number" or nRecordable%1 ~= 0 or nRecordable <= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nRecordable 只能传大于0的整数")
		return
	end	

	if type(nFrom) ~= "number" or nFrom%1 ~= 0 or nFrom <= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中 nFrom 只能传大于0的整数")
		return
	end	

	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAroundTeamerStatus 中nUserId必须为整型并且大于等于0")
		return
	end
	
	if nData == nil then
		nData = 0
	elseif type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 User_TaskReward 中 nData 只能传大于等于0的整数")
		return
	end

	return AddUserAroundTeamerStatus(nUserId,nType,nPower,nInterval,nLeaveTimes,nRemainTime,nEndTime,nRecordable,nFrom,nData)
end

	
-- //队长删除周围队伍成员状态，参数说明：idUser用户ID，szParam为状态类型参数字符串 最多输入15个状态类型数据
-- LUA_FUNC(DelUserAroundTeamerStatus)
	-- OBJID idUser   = Lua_GetParamULong(1);
	-- const char* szParam = Lua_GetParamString(2);
function User_DelAroundTeamerStatus(sParam,nUserId)
	if type(sParam) ~= "string"  then
		Sys_SaveAbnormalLog("函数 User_DelAroundTeamerStatus 中 sParam 只能传大于0的整数")
		return
	end
	
	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelAroundTeamerStatus 中nUserId必须为整型并且大于等于0")
		return
	end

	return DelUserAroundTeamerStatus(nUserId,sParam)
end

	
-- //队长检查周围队伍成员状态，参数说明：idUser用户ID，szParam为状态类型参数字符串 最多输入15个状态类型数据
-- LUA_FUNC(ChkUserAroundTeamerStatus)
	-- OBJID idUser   = Lua_GetParamULong(1);
	-- const char* szParam = Lua_GetParamString(2);
function User_ChkAroundTeamerStatus(sParam,nUserId)
	if type(sParam) ~= "string"  then
		Sys_SaveAbnormalLog("函数 User_ChkAroundTeamerStatus 中 sParam 只能传大于0的整数")
		return
	end
	
	if type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkAroundTeamerStatus 中nUserId必须为整型并且大于等于0")
		return
	end
	return ChkUserAroundTeamerStatus(nUserId,sParam)
end

--任务轮盘抽奖界面. 
--参数说明: idUser表示玩家ID; idTask表示任务ID. 如果失败返回false, 成功返回true.
--LUA_FUNC(UserTaskReward)
--OBJID idUser = Lua_GetParamUInt(1);
--OBJID idTask = Lua_GetParamUInt(2);
--返回

function User_TaskReward(nTask,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_TaskReward 中 nUserId 只能传大于等于0的整数")
		return
	end		
	if type(nTask) ~= "number" or nTask%1 ~= 0 or nTask < 0 then
		Sys_SaveAbnormalLog("函数 User_TaskReward 中 nTask 只能传大于等于0的整数")
		return
	end	
	
	return UserTaskReward(nUserId,nTask)

end

--通知客户端打开网页
--参数说明: idUser表示玩家ID, pszParam表示网址. 如果失败返回false, 成功返回true.
--LUA_FUNC(SendWebPage)
--OBJID idUser = Lua_GetParamUInt(1);
--const char* pszParam = Lua_GetParamString(2);
--返回通知客户端打开网页
function User_SendWebPage(sPszParam,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_SendWebPage 中 nUserId 只能传大于等于0的整数")
		return
	end		
	if type(sPszParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_SendWebPage 中 sPszParam 只能传字符串")
		return
	end		
	return SendWebPage(nUserId,sPszParam)
end

--通知客户端打开界面
--参数说明: 通知客户端打开界面. 参1: idUser表示玩家ID, 参2：idNpc表示NPC的ID，默认填0表示点击的NPC， 参3：dwDialog表示界面ID. 如果失败返回false, 成功返回true.
--LUA_FUNC(OpenDialog)
--OBJID idUser = Lua_GetParamUInt(1);

--DWORD dwDialog = Lua_GetParamUInt(2);
--返回通知客户端打开界面
function User_OpenDialog(ndwDialog,nNpcID,nUserId)
	if ndwDialog == nil then
		ndwDialog = 0
	elseif type(ndwDialog) ~= "number" or ndwDialog%1 ~= 0 or ndwDialog < 0 then
		Sys_SaveAbnormalLog("函数 User_OpenDialog 中 ndwDialog 只能传大于等于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_OpenDialog 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	if nNpcID == nil then
		nNpcID = 0
	elseif type(nNpcID) ~= "number" or nNpcID%1 ~= 0 or nNpcID < 0 then
		Sys_SaveAbnormalLog("函数 User_OpenDialog 中 nNpcID 只能传大于等于0的整数")
		return
	end

	return OpenDialog(nUserId,nNpcID,ndwDialog)
end


--屏幕效果,包括震动1,缩放2,变暗变亮4,由DATA来指定,可以叠加. 
--参数说明: idUser表示玩家ID, nData表示效果. 如果失败返回false, 成功返回true.
--LUA_FUNC(Screffect)
--OBJID idUser = Lua_GetParamUInt(1);
--int nData = Lua_GetParamInt(2);
--返回屏幕效果
function User_Screffect(nData,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_Screffect 中 nUserId 只能传大于等于0的整数")
		return
	end		
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 User_Screffect 中 nData 只能传大于等于0的整数")
		return
	end	
	return Screffect(nUserId,nData)
end


--玩家进入副本
--参数说明： idUser玩家ID ,idInstanceType 副本类型, nNumLimit副本数量限制 0为不限制数量. bIsInvite是否邀请队友进入, nTimeLimit设置队友进入时间 bIsInvite为真时此值有用
--LUA_FUNC(EnterInstance)
--OBJID idUser = Lua_GetParamUInt(1);
--OBJID idInstanceType = Lua_GetParamUInt(2);
--int   nNumLimit = Lua_GetParamInt(3);
--int   nIsInvite = Lua_GetParamBool(4);
--int   nTimeLimit = Lua_GetParamInt(5);
--返回玩家进入副本
function User_EnterInstance(nInstanceType,nNumLimit,nIsInvite,nTimeLimit,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_EnterInstance 中 nUserId 只能传大于等于0的整数")
		return
	end		
	if type(nInstanceType) ~= "number" or nInstanceType%1 ~= 0 or nInstanceType < 0 then
		Sys_SaveAbnormalLog("函数 User_EnterInstance 中 nInstanceType 只能传大于等于0的整数")
		return
	end	
	if nNumLimit == nil then
		nNumLimit = 0
	elseif  type(nNumLimit) ~= "number" or nNumLimit%1 ~= 0 or nNumLimit < 0 then
		Sys_SaveAbnormalLog("函数 User_EnterInstance 中 nNumLimit 只能传大于等于0的整数")
		return
	end		
	if nIsInvite == nil then
		nIsInvite = 0
	elseif type(nIsInvite) ~= "number" or nIsInvite%1 ~= 0 or nIsInvite < 0 then
		Sys_SaveAbnormalLog("函数 User_EnterInstance 中 nIsInvite 只能传大于等于0的整数")
		return
	end	
	if nIsInvite == nil or nIsInvite == 0 then
		nTimeLimit = 0
	elseif type(nTimeLimit) ~= "number" or nTimeLimit%1 ~= 0 or nTimeLimit < 0 then
		Sys_SaveAbnormalLog("函数 User_EnterInstance 中 nTimeLimit 只能传大于等于0的整数")
		return
	end	
	return EnterInstance(nUserId,nInstanceType,nNumLimit,nIsInvite,nTimeLimit)
end



--发送文字花. 
--参数说明: idUser表示玩家ID, nFlowerType为1:红色,2:黄色,3:玫瑰色 ,4:三色花 ,5三色花; pszWords表示发送的话. 如果失败返回false, 成功返回true.
--LUA_FUNC(WordsFlower)
--OBJID idUser = Lua_GetParamUInt(1);
--int nFlowerType = Lua_GetParamInt(2);
--const char* pszWords = Lua_GetParamString(3);
--返回文字花（只有4:三色花 可以播）
function User_WordsFlower(nFlowerType,nPszWords,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_WordsFlower 中 nUserId 只能传大于等于0的整数")
		return
	end		
	if type(nFlowerType) ~= "number" or nFlowerType%1 ~= 0 or nFlowerType < 0 or nFlowerType > 5 then
		Sys_SaveAbnormalLog("函数 User_WordsFlower 中 nFlowerType 只能传(1,2,3,4,5)的整数")
		return
	end	
	if type(nPszWords) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_WordsFlower 中 nPszWords 只能传字符串")
		return
	end		
	return WordsFlower(nUserId,nFlowerType,nPszWords)
end


--执行指定的sql语句, 取回指定的一个数据字段值, 以字符串形式存入到指定的寄存器变量. 
--参数说明: idUser表示玩家ID; nIdx表示寄存器索引; pszSQL表示SQL语句. 如果失败返回false, 成功返回true.
--LUA_FUNC(UserDbField)
--OBJID idUser = Lua_GetParamUInt(1);
--int nIdx = Lua_GetParamInt(2);
--const char* pszSQL = Lua_GetParamString(3);
--返回
function User_DbField(nPszSQL,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_DbField 中 nUserId 只能传大于等于0的整数")
		return
	end		
	
	if type(nPszSQL) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_DbField 中 nPszSQL 只能传字符串")
		return
	end		
	
	return UserDbField(nUserId,nPszSQL)
end

-- 玩家等级及转世次数判断
function User_JudgeLevelAndMetempsychosis(nLevel,nMetempsychosis)
	if type(nLevel) ~= "number" or nLevel%1 ~= 0 or nLevel < 0 then
		Sys_SaveAbnormalLog("函数 User_JudgeLevelAndMetempsychosis 中 nLevel 只能传大于等于0的整数")
		return
	end
	
	if type(nMetempsychosis) ~= "number" or nMetempsychosis%1 ~= 0 or nMetempsychosis < 0 then
		Sys_SaveAbnormalLog("函数 User_JudgeLevelAndMetempsychosis 中 nMetempsychosis 只能传大于等于0的整数")
		return
	end
	
	local nUserId = Get_UserId()
	local nUserMetempsychosis = Get_UserMetempsychosis(nUserId)
	local nUserLev = Get_UserLevel(nUserId)
	
	if nUserMetempsychosis < nMetempsychosis then
		return false
	end
	
	if nUserMetempsychosis > nMetempsychosis then
		return true
	end
	
	if nUserLev < nLevel then
		return false
	else
		return true
	end
end

-- actionType 3701 读条封装
-- nSecs 读条的秒数
-- sContent 读条内显示的文字
-- nActionId 读条时执行的动作
-- sFunc 读条成功时执行的函数
-- sFileFunc 读条失败时执行的函数
function User_SetExplore(nSecs,sContent,nActionId,sFunc,sFileFunc,nUserId)
	if type(nSecs) ~= "number" or nSecs%1 ~= 0 or nSecs <= 0 then
		Sys_SaveAbnormalLog("函数 User_SetExplore 中 nSecs 只能传大于0的整数")
		return
	end 
	
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_SetExplore 中 sContent 只能传字符串类型的参数")
		return
	end
	
	if type(nActionId) ~= "number" or nActionId%1 ~= 0 or nActionId < 0 then
		Sys_SaveAbnormalLog("函数 User_SetExplore 中 nActionId 只能传大于等于0的整数")
		return
	end
	
	if sFunc == nil then
		sFunc = "NULL"
	elseif type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_SetExplore 中 sFunc 只能传字符")
		return
	end
	
	if sFileFunc == nil then
		sFileFunc = "NULL"
	elseif type(sFileFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_SetExplore 中 sFileFunc 只能传字符")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetExplore 中 nUserId 只能传大于0的整数")
		return
	end
	
	return UserSetExplore(nUserId,nSecs,sContent,nActionId,string.format("</F>%s",sFunc),string.format("</F>%s",sFileFunc))
end

------------------------------------2014.12.5
-- 检测玩家是否学习指定类型的内功、
-- 参数说明：参1：idUser玩家ID； 返回值：已经学习返回true，否则返回false
function User_IsLearnInnerStrengthType(nType,nUserId)
	if type(nType) ~= "number" or nType%1 ~= 0 or nType <= 0 then
		Sys_SaveAbnormalLog("函数 User_IsLearnInnerStrengthType 中 nType 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_IsLearnInnerStrengthType 中 nUserId 只能传大于等于0的整数")
		return
	end

	return IsLearnedInnerStrengthType(nUserId,nType)
end

-- 玩家学习指定类型的内功
-- 参数说明：参1：玩家ID，参2：内功类型；返回值：成功返回true，否则返回false
function User_LearningInnerStrength(nType,nUserId)
	if type(nType) ~= "number" or nType%1 ~= 0 or nType <= 0 then
		Sys_SaveAbnormalLog("函数 User_LearningInnerStrength 中 nType 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_LearningInnerStrength 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	if User_IsLearnInnerStrengthType(nType,nUserId) then
		Sys_SaveAbnormalLog("函数 User_LearningInnerStrength 中玩家已经学习过该类型内功")
		return
	else
		return LearningInnerStrength(nUserId,nType)
	end
end

-- 玩家添加修为值
function User_AddCultureValue(nValue,nUserId)
	if type(nValue) ~= "number" or nValue%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddCultureValue 中 nValue 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddCultureValue 中 nUserId 只能传大于等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_CultureValue,nValue,0)
end
--------------2014.12.17
-- 是否跨服玩家接口
-- 返回true表示是跨服玩家，false表示本服玩家
function User_IsCross(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsCross 中 nUserId 只能传大于0的整数")
		return
	end
	
	return IsOSUser(nUserId)
end

-- 搬金砖：开始执行探索动作
-- BeginMoveGoldBrick（int idUser, int nAction, const char* pszDescribe, int idNpc）
-- 参数1：玩家id，如果传0，会自动取当前调用者id
-- 参数2：表示探索动作id
-- 参数3：表示描述文字
-- 参数4：点击的npc id

function User_BeginMoveGoldBrick(nActionId,sDesc,nNpcId,nUserId)
	if type(nActionId) ~= "number" or nActionId < 0 or nActionId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_BeginMoveGoldBrick 中 nActionId 只能传大等于0的整数")
		return
	end
	
	if type(sDesc) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_BeginMoveGoldBrick 中 sDesc 只能字符串")
		return
	end
	
	if type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_BeginMoveGoldBrick 中 nNpcId 只能传大等于0的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_BeginMoveGoldBrick 中 nUserId 只能传大于0的整数")
		return
	end
	
	return BeginMoveGoldBrick(nUserId,nActionId,sDesc,nNpcId)
end


--------------2014.12.19
-- 以下几个接口只能在Event_Kill_User事件中使用
-- // 判断目标玩家(被杀玩家)是否为跨服玩家，  参数1：目标玩家ID（传0为默认目标玩家）， 是跨服玩家返回true,否则返回false.
function User_TargetIsCross(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_TargetIsCross 中 nUserId 只能传大于0的整数")
		return
	end
	
	return IsTargetOSUser(nUserId)
end

--------------2015.01.13
-- //通过玩家改变（增加或减少）帮派资金，参1：idUser用户ID，参2：idSyn表示帮派id，默认取当前玩家的帮派id，参3：n64Data表示要改变的数值 返回值：成功返回true，否则返回false
function User_ChgSynMoney(nGuildId,nMoneyNum,nUserId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgSynMoney 中 nGuildId 只能大于0的整数")
		return
	end
	
	if type(nMoneyNum) ~= "number" or nMoneyNum%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgSynMoney 中 nMoneyNum 只能传整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgSynMoney 中 nUserId 只能传大于0的整数")
		return
	end

	return ChgSynMoneyByUser(nUserId,nGuildId,nMoneyNum)
end

-- //通过玩家改变（增加或减少）帮派天石，参1：idUser用户ID，参2：idSyn表示帮派id，默认取当前玩家的帮派id，参3：nData表示要改变的数值 返回值：已经创建返回true，否则返回false
function User_ChgSynEMoney(nGuildId,nEMoneyNum,nUserId)
	if type(nGuildId) ~= "number" or nGuildId <= 0 or nGuildId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgSynMoney 中 nGuildId 只能大于0的整数")
		return
	end
	
	if type(nEMoneyNum) ~= "number" or nEMoneyNum%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgSynMoney 中 nEMoneyNum 只能传整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChgSynMoney 中 nUserId 只能传大于0的整数")
		return
	end

	return ChgSynEMoneyByUser(nUserId,nGuildId,nEMoneyNum)
end

--------------2015.01.21
--检查玩家是否达成此项成就
--nAchPos 成就标识位
--nUserId 玩家ID
--例如：User_ChkAchByAchPosition(11002)
function User_ChkAchByAchPosition(nAchPos,nUserId)
	if type(nAchPos) ~= "number" or nAchPos <= 0 or nAchPos%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkAchByAchPosition 中 nAchPos 只能大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkAchByAchPosition 中 nUserId 只能传大于0的整数")
		return
	end
	
	return IsOwnAchByAchPositon(nUserId,nAchPos)
end

--设置玩家达成此项成就
--nAchPos 成就标识位
--nUserId 玩家ID
--例如：User_AddAchByAchPosition(11002)
function User_AddAchByAchPosition(nAchPos,nUserId)
	if type(nAchPos) ~= "number" or nAchPos <= 0 or nAchPos%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAchByAchPosition 中 nAchPos 只能大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddAchByAchPosition 中 nUserId 只能传大于0的整数")
		return
	end
	
	return SetAchByPosition(nUserId,nAchPos)
end

--删除玩家此项成就
--nAchPos 成就标识位
--nUserId 玩家ID
--例如：User_DelAchByAchPosition(11002)
function User_DelAchByAchPosition(nAchPos,nUserId)
	if type(nAchPos) ~= "number" or nAchPos <= 0 or nAchPos%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelAchByAchPosition 中 nAchPos 只能大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DelAchByAchPosition 中 nUserId 只能传大于0的整数")
		return
	end
	
	return ClsAchByPosition(nUserId,nAchPos)
end


--播放声音
--User_MediaPlay
--LUA接口：UserMediaPlay
--参数1：sPszMedia媒体文件相对路径名
--参数2：nLoop，如果为0，则是无限循环，否则只播放一次
--参数3：nBroadcast是否为广播消息，0表示发送给当前玩家(play)，非0表示发送给附近玩家(broadcasts)
--参数4、5："x, y"为地图坐标，如果都设置为0，作为背景声效/音乐播放
--参数6：nUserId,用户ID
--返回值：成功返回true，失败返回false
--type:1029
--例如：User_MediaPlay("sound/Piano_do.mp3") 或 /callluafunc </F>User_MediaPlay</S>sound/Piano_do.mp3
function User_MediaPlay(sPszMedia,nLoop,nBroadcast,nCellx,nCelly,nUserId)
	if nBroadcast == nil then
		nBroadcast = 0
	elseif type(nBroadcast) ~= "number" or nBroadcast < 0 or nBroadcast%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_MediaPlay 中 nBroadcast 只能传大于等于0的整数")
		return
	end
	
	if nCellx == nil then
		nCellx = 0
	elseif type(nCellx) ~= "number" or nCellx%1 ~= 0 or nCellx < 0 then
		Sys_SaveAbnormalLog("函数 User_MediaPlay 中 nCellx 只能传大于等于0的整数")
		return
	end
	
	if nCelly == nil then
		nCelly = 0
	elseif type(nCelly) ~= "number" or nCelly%1 ~= 0 or nCelly < 0 then
		Sys_SaveAbnormalLog("函数 User_MediaPlay 中 nCelly 只能传大于等于0的整数")
		return
	end
	
	if nLoop == nil then
		nLoop = 1
	elseif type(nLoop) ~= "number" or nLoop%1 ~= 0 or nLoop < 0 then
		Sys_SaveAbnormalLog("函数 User_MediaPlay 中 nLoop 只能传大于等于0的整数")
		return
	end
	
	if type(sPszMedia) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_MediaPlay 中 sPszMedia 只能传字符串")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_MediaPlay 中 nUserId 只能传大于0的整数")
		return
	end
	
	return UserMediaPlay(nUserId,nBroadcast,nCellx,nCelly,nLoop,sPszMedia)
end

--------------2015.03.12
-- 1.判断是否可以放入背包一定数量的金币
-- CanPutMoney2Bag
-- 参数1：玩家id
-- 参数2：金币数,只能范围为正负21亿之间的整数
-- 返回值：true表示可以，false表示不可以，会超过上限。
function User_CanPutMoney2Bag(nBagMoneyNum,nUserId)
	if type(nBagMoneyNum) ~= "number" or nBagMoneyNum%1 ~= 0 or math.abs(nBagMoneyNum) > 2100000000  then
		Sys_SaveAbnormalLog("函数 User_CanPutMoney2Bag 中 nBagMoneyNum 只能范围为正负21亿之间的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CanPutMoney2Bag 中 nUserId 只能传大等于于0的整数")
		return
	end
	
	return CanPutMoney2Bag(nUserId,nBagMoneyNum)
end

-- 2.判断是否可以放入仓库一定数量的金币
-- CanPutMoney2Storage
-- 参数1：玩家id
-- 参数2：金币数,只能范围为正负21亿之间的整数
-- 返回值：true表示可以，false表示不可以，会超过上限。
function User_CanPutMoney2Storage(nStorageMoneyNum,nUserId)
	if type(nBagMoneyNum) ~= "number" or nStorageMoneyNum%1 ~= 0 or math.abs(nStorageMoneyNum) > 2100000000 then
		Sys_SaveAbnormalLog("函数 User_CanPutMoney2Bag 中 nBagMoneyNum 只能范围为正负21亿之间的整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CanPutMoney2Bag 中 nUserId 只能传大等于于0的整数")
		return
	end
	
	return CanPutMoney2Storage(nUserId,nStorageMoneyNum)
end

-- 玩家定时器LUA接口
-- LUA接口：User_SetTimer
-- 参数1：nTimeDelay倒计时时间
-- 参数2：sFunc定时时间到调用的脚本
-- 参数3：nType为0不做客户端表现
-- 参数4：nUserId,用户ID
-- 返回值：成功返回true，失败返回false
-- type:1071
-- 例如：/callluafunc </F>User_SetTimer</N>15</S></N>1
function User_SetTimer(nTimeDelay,sFunc,nType,nUserId)
	if type(nTimeDelay) ~= "number" or nTimeDelay%1 ~= 0 or nTimeDelay <= 0 then
		Sys_SaveAbnormalLog("函数 User_SetTimer 第一个参数nTimeDelay为整型且大于0")
		return
	end
	
	if sFunc == nil then
		sFunc = "NULL"
	elseif type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 User_SetTimer 第二个参数sFunc为字符串")
		return
	end
	
	if nType == nil then
		nType = 0
	elseif type(nType) ~= "number" or nType < 0 or nType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetTimer 中 nType 只能传大于等于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetTimer 中 nUserId 只能传大等于于0的整数")
		return
	end
	
	return UserSetTimer(nUserId,nType,string.format("</F>%s",sFunc),nTimeDelay)
end



--按比例扣血LUA接口，对应action type=1510
--LUA接口：UserDecLife
--参数1：idUser,用户ID
--参数2：usType,为0表示扣除玩家总血量的百分比，为1表示扣除玩家当前血量的百分比
--参数3：nPercent，百分比，为0-100的值
--返回值：成功返回true，失败返回false

function User_DecLifePercent(nPercent,nType,nUserId)
	if type(nPercent) ~= "number" or nPercent < 0 or nPercent > 100 or nPercent%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DecLifePercent 中 nPercent 只能传0-100的整数")
		return
	end

	if type(nType) ~= "number" or nType%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DecLifePercent 中 nType 只能传0或者1")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_DecLifePercent 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	UserDecLife(nUserId,nType,nPercent)
end

--发送定制网络消息ActionDefine(1075)
--参数1：idUser,用户ID
--参数2：nBroadcast是否为广播消息，0表示发送给当前玩家(send)，非0表示发送给附件玩家(broadcast)
--参数3：nType为类型
--参数4：nData为数据
--返回值：成功返回true，失败返回false
--PS:User_SendNetWorkMsg(0,1000,0) 代表普通杀怪模式 0 1000 send
function User_SendNetWorkMsg(nData,nType,nBroadcast,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 User_SendNetWorkMsg  中 nUserId 为整型并且大于等于0")
		return
	end	
	
	if nBroadcast == nil then
		nBroadcast = 0
	elseif type(nBroadcast) ~= "number" or nBroadcast%1 ~= 0 or nBroadcast < 0 then
		Sys_SaveAbnormalLog("函数 User_SendNetWorkMsg  中 nBroadcast 为整型并且大于等于0")
		return
	end
	
	if nType == nil then
		nType = 1000
	elseif type(nType) ~= "number" or nType%1 ~= 0 or nType < 0 then
		Sys_SaveAbnormalLog("函数 User_SendNetWorkMsg  中 nType 为整型并且大于等于0")
		return
	end
	
	if nData == nil then
		nData = 0
	elseif type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 User_SendNetWorkMsg  中 nData 为整型并且大于等于0")
		return
	end
	return UserCustomMsg(nUserId,nBroadcast,nType,nData)
end

-- 判断玩家是否加入了联盟，参数1：玩家id。返回值：如果加入了联盟返回ture，否则返回false
-- bool IsInLeague（int idUser）;
function User_IsInLeague(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsInLeague 中 nUserId 只能传大于等于0的整数")
		return
	end

	return IsInLeague(nUserId)
end

--------------2015.7.6
-- 新增修改黄金联赛积分的接口
function User_AddLeaguePoint(nPoint,nUserId)
	if type(nPoint) ~= "number" or nPoint%1 ~= 0 then 
		Sys_SaveAbnormalLog("函数 User_AddLeaguePoint 中 nPoint 只能传整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddLeaguePoint 中 nUserId 只能传大等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_League_Point,nPoint,0)
end

-- // 判断玩家是否可以掠夺当前的执政盟。参数1:玩家ID，如果可以掠夺，返回true，否则返回false。
-- IsInPlunderWar
function User_IsInPlunderWar(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsInPlunderWar 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return IsInPlunderWar(nUserId)
end

-- IsLeagueLeader（int idUser）
-- 玩家是否是联盟盟主
-- 参数1：玩家id
-- 返回值：如果是返回true，否则返回false
function User_IsLeagueLeader(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsLeagueLeader 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return IsLeagueLeader(nUserId)
end

-- CanAddLeagueMoney(int idLeague, int nMoney)
-- 判断联盟基金
-- 参数1：联盟id
-- 参数2：需要增加或减少的资金,正数表示增加，负数表示减少
-- 返回值:成功返回true，失败返回false
function User_CanAddLeagueMoney(nMoney,nUserId)
	if type(nMoney) ~= "number" or nMoney%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CanAddLeagueMoney 中 nMoney 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CanAddLeagueMoney 中 nUserId 只能传大等于0的整数")
		return
	end
	
	-- 获取联盟ID
	local nLeagueId = Get_UserLeagueId(nUserId)
	
	if type(nLeagueId) ~= "number" or nLeagueId < 0 or nLeagueId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLeagueId 中返回的 nLeagueId 值有错")
		return
	end
	
	return CanAddLeagueMoney(nLeagueId,nMoney)
end

-- AddLeagueMoney(int idLeague, int nMoney)
-- 加联盟基金
-- 参数1：联盟id
-- 参数2：需要增加或减少的资金,正数表示增加，负数表示减少
-- 返回值：成功返回true,失败返回false
function User_AddLeagueMoney(nMoney,nUserId)
	if type(nMoney) ~= "number" or nMoney%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddLeagueMoney 中 nMoney 只能传整数")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddLeagueMoney 中 nUserId 只能传大等于0的整数")
		return
	end
	
	-- 获取联盟ID
	local nLeagueId = Get_UserLeagueId(nUserId)
	
	if type(nLeagueId) ~= "number" or nLeagueId < 0 or nLeagueId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Get_UserLeagueId 中返回的 nLeagueId 值有错")
		return
	end
	
	return AddLeagueMoney(nLeagueId,nMoney)
end

-- 添加战功值
function User_AddServiceValue(nValue,nUserId)
	if type(nValue) ~= "number" or nValue < 0 or nValue%1 ~= 0 then 
		Sys_SaveAbnormalLog("函数 User_AddServiceValue 中 nValue 只能传整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddServiceValue 中 nUserId 只能传大等于0的整数")
		return
	end

	return AddUserInt(nUserId,G_PLAYER_Service_Value,nValue,0)
end

-- 新增一个LUA接口，用于获取玩家每天可免费领取的大喇叭数量N，对应LUA接口说明如下：
-- LUA接口：UserGetSpeakerNumEveryDay
-- 参数1：玩家ID，若为0，则为当前玩家
-- 返回值：玩家每天可免费领取的大喇叭数量，若玩家身兼数值（嫔妃、朝廷重臣、御林军），为各个职位可免费领取数相加。
function User_GetSpeakerNumEveryDay(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_GetSpeakerNumEveryDay 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return UserGetSpeakerNumEveryDay(nUserId)
end

-- 给经验时间奖励，满级改成给修行值
-- 返回值 0表示参数传错了，1表示加经验，2表示加修行值
function User_AddExpOrCultureValue(nExpTime,nCultivation)
	if type(nExpTime) ~= "number" or nExpTime < 0 or nExpTime%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpOrCultureValue 中 nExpTime 只能传大等于0的整数")
		return 0
	end
	
	if nCultivation == nil then
		nCultivation = nExpTime/2
	elseif type(nCultivation) ~= "number" or nCultivation < 0 or nCultivation%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddExpOrCultureValue 中 nCultivation 只能传大等于0的整数")
		return 0
	end
	
	local nUserId = Get_UserId()
	local nLevel = Get_UserLevel(nUserId)
	
	if nLevel < 140 then
		User_AddExpTime(nExpTime,nUserId)
		return 1
	else
		User_AddCultivation(nCultivation,nUserId)
		return 2
	end
end

------2015.07.24新增接口

--判断玩家是否在执政盟中（国家）(只能用于调用lua的玩家本人)

function User_ChkUserInCountry(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChkUserInCountry 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return IsUserInCountry(nUserId)
end

-- 新增lua接口：
 -- IsConcubines（int idUser）是否是嫔妃
-- 参数1：玩家id
-- 返回值：是，返回true，否则返回false
function User_IsConcubines(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsConcubines 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return IsConcubines(nUserId)
end

-- HaveConcubines（int idUser）是否有嫔妃
-- 参数1：玩家id
-- 返回值：有，返回true，否则返回false
function User_HaveConcubines(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_HaveConcubines 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return HaveConcubines(nUserId)
end

-- 设置玩家性别
function User_SetSex(nSex,nUserId)
	if type(nSex) ~= "number" or (nSex ~= 1 and nSex ~= 2)then
		Sys_SaveAbnormalLog("函数 User_SetSex 中 nSex 只能传1或者2")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_SetSex 中 nUserId 只能传大等于0的整数")
		return
	end

	return SetUserInt(nUserId,G_PLAYER_Sex,nSex,0)
end

-- 增加骑宠积分的接口
function User_AddRidingPoints(nAddRidingPoints,nUserId)
	if type(nAddRidingPoints) ~= "number" or nAddRidingPoints%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRidingPoints 中 nAddRidingPoints 只能传整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_AddRidingPoints 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return AddUserInt(nUserId,G_PLAYER_RidingPoints,nAddRidingPoints,0)
end

-- IsResistPlunderWar
-- 判断玩家所在联盟是否处于被掠夺状态，并且此时玩家是否处于本服，参1: 玩家ID，如果是返回true，否则返回false
function User_IsResistPlunderWar(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_IsResistPlunderWar 中 nUserId 只能传大等于0的整数")
		return
	end
	
	return IsResistPlunderWar(nUserId)
end

--2015年9月22添加接口

--//判断玩家当前所在服务器是否处于九龙罩状态：如果是返回true，否则返回false
--LUA_FUNC(IsImmunePlunder)

function User_IsImmunePlunder()

	return IsImmunePlunder()
end



----- 2015.10.13 ----------
-- #检测玩家是否存在师徒关系
-- 对应ACTION:1206
-- LUA接口：UserCheckGuide
-- 参数1：玩家id
-- 返回值：true表示存在师徒关系 false表示不存在师徒关系
function User_CheckGuide(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CheckGuide 中 nUserId 只能传大于等于0的整数")
		return
	end
	return UserCheckGuide(nUserId)
end

-- #检测玩家是否存在商业伙伴关系
-- 对应ACTION:1207
-- LUA接口：UserCheckTradeBuddy
-- 参数1：玩家id
-- 返回值：true表示存在商业伙伴关系 false表示不存在商业伙伴关系
function User_CheckTradeBuddy(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CheckTradeBuddy 中 nUserId 只能传大于等于0的整数")
		return
	end
	return UserCheckTradeBuddy(nUserId)
end

-- #检测玩家是否存在拍卖行物品
-- 对应ACTION:1210
-- LUA接口：UserHasAuctionItem
-- 参数1：玩家id
-- 返回值：true表示存在拍卖行物品 false表示不存在拍卖行物品
function User_HasAuctionItem(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_HasAuctionItem 中 nUserId 只能传大于等于0的整数")
		return
	end
	return UserHasAuctionItem(nUserId)
end

-- #检测玩家是否存在邮件
-- 对应ACTION:1211
-- LUA接口：UserHasMail
-- 参数1：玩家id
-- 返回值：true表示有邮件 false表示没有邮件
function User_HasMail(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_HasMail 中 nUserId 只能传大于等于0的整数")
		return
	end
	return UserHasMail(nUserId)
end

-- #对cq_pk_item表的检查
-- 对应ACTION:2205
-- LUA接口：UserCheckPkItem
-- 参数1：玩家id
-- 参数2：为0，表示被扣押的装备，对应action中的target，为1表示扣押别人的物品，对应action中的hunter
-- 返回值：true表示有对应物品 false表示没有对应物品
function User_CheckPkItem(nNumber,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CheckPkItem 中 nUserId 只能传大于等于0的整数")
		return
	end
	
	if type(nNumber) ~= "number" or nNumber%1 ~= 0 or nNumber < 0 or nNumber > 1 then
		Sys_SaveAbnormalLog("函数 User_CheckPkItem 第一个参数nNumber为整型且范围在0--1")
		return
	end
	
	return UserCheckPkItem(nUserId,nNumber)
end

-- #检测玩家是否存在未领天石卡，一定要同步查表，放在最后面
-- 对应ACTION:1209
-- LUA接口：UserCheckCard
-- 参数1：玩家id 
-- 返回值：true表示有未领天石卡 false表示没有未领天石卡
function User_CheckCard(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_CheckCard 中 nUserId 只能传大于等于0的整数")
		return
	end
	return UserCheckCard(nUserId)
end

-- #游服发起转服要求
-- 对应ACTION:1212
-- LUA接口：UserChangeServer
-- 参数1：玩家id
-- 参数2：转入服务器名称
-- 返回值：true表示成功 false表示失败
function User_ChangeServer(sServerName,nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 User_ChangeServer 中 nUserId 只能传大于等于0的整数")
		return
	end
	if type(sServerName) ~= "string" or sServerName == nil then
		Sys_SaveAbnormalLog("函数 Sys_CheckServerName 中 sServerName 只能传字符")
		return
	end
	return UserChangeServer(nUserId,sServerName)
end