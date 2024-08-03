----------------------------------------------------------------------------
--Name:		[征服][公用函数]系统函数.lua
--Purpose:	系统函数接口
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
-- 系统函数命名前缀词：Sys_
--例子：
--// 对应type=101，菜单文本
--// 菜单文本。 参数说明：strText表示显示的文本内容，可包含空格，也可为空行。如果失败返回false, 成功返回true。
--bool MenuText(string strText);

--function Sys_MenuText(sText)
--
--end

------------------------------------------------------------------------------

-- 用于脚本制作过程测试使用的log接口
-- 参数说明： 存放在log文件里面的内容
function Sys_SaveAbnormalLog(sText)
	local sPath = "syslog/AbnormalLog"

	SaveCustomLog(sPath,tostring(sText))
end




--//ACTION_USER_CUSTOM_LOG 1085
--bool SaveCustomLog(const char * pszLogFile, const char * pszText);
--外怪监狱：410
--sText表示Log配置信息,例如："0,0,0,0,10002354,2,728596,3"
function Sys_SaveActionLog(sText)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SaveActionLog 中 sText 只能传字符")
		return
	end
	
	local sUserName = Get_UserName()
	local sUserId = Get_UserId()
	local sUserLev = Get_UserLevel()
	local sUserPro = Get_UserProfession()
	local sUserMeto = Get_UserMetempsychosis() 
	local sLogUserData = sUserName..'['.. sUserId ..']['.. sUserLev ..']['.. sUserPro ..']['.. sUserMeto ..']'

	local sLogFile = "gmlog/action_log"
	local sLogText = string.format("410,%s,%s",sLogUserData,sText)
	return SaveCustomLog(sLogFile,sLogText)
end



--//ACTION_USER_CUSTOM_LOG 1085
--bool SaveCustomLog(const char * pszLogFile, const char * pszText);
--功能性脚本：340
--sText表示Log配置信息,例如："0,0,0,0,10002354,2,728596,3"
function Sys_SaveActionFuncLog(sText)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SaveActionFuncLog 中 sText 只能传字符")
		return
	end
	
	local sUserName = Get_UserName()
	local sUserId = Get_UserId()
	local sUserLev = Get_UserLevel()
	local sUserPro = Get_UserProfession()
	local sUserMeto = Get_UserMetempsychosis() 
	local sLogUserData = sUserName..'['.. sUserId ..']['.. sUserLev ..']['.. sUserPro ..']['.. sUserMeto ..']'

	local sLogFile = "gmlog/action_func_log"
	local sLogText = string.format("340,%s,%s",sLogUserData,sText)
	return SaveCustomLog(sLogFile,sLogText)
end




--//ACTION_USER_CUSTOM_LOG 1085
--bool SaveCustomLog(const char * pszLogFile, const char * pszText);
--活动脚本：350
--sText表示Log配置信息,例如："0,0,0,0,10002354,2,728596,3"
function Sys_SaveActionFestivalLog(sText)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SaveActionFestivalLog 中 sText 只能传字符")
		return
	end
	
	local sUserName = Get_UserName()
	local sUserId = Get_UserId()
	local sUserLev = Get_UserLevel()
	local sUserPro = Get_UserProfession()
	local sUserMeto = Get_UserMetempsychosis() 
	local sLogUserData = sUserName..'['.. sUserId ..']['.. sUserLev ..']['.. sUserPro ..']['.. sUserMeto ..']'

	local sLogFile = "gmlog/action_festival_log"
	local sLogText = string.format("350,%s,%s",sLogUserData,sText)
	return SaveCustomLog(sLogFile,sLogText)
end



--//ACTION_USER_CUSTOM_LOG 1085
--bool SaveCustomLog(const char * pszLogFile, const char * pszText);
--任务脚本：360
--sText表示Log配置信息,例如："0,0,0,0,10002354,2,728596,3"
function Sys_SaveActionTaskLog(sText)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SaveActionTaskLog 中 sText 只能传字符")
		return
	end
	
	local sUserName = Get_UserName()
	local sUserId = Get_UserId()
	local sUserLev = Get_UserLevel()
	local sUserPro = Get_UserProfession()
	local sUserMeto = Get_UserMetempsychosis() 
	local sLogUserData = sUserName..'['.. sUserId ..']['.. sUserLev ..']['.. sUserPro ..']['.. sUserMeto ..']'

	local sLogFile = "gmlog/action_task_log"
	local sLogText = string.format("360,%s,%s",sLogUserData,sText)
	return SaveCustomLog(sLogFile,sLogText)
end

--//ACTION_USER_CUSTOM_LOG 1085
--bool SaveCustomLog(const char * pszLogFile, const char * pszText);
--sText表示Log配置信息,例如："0,0,0,0,10002354,2,728596,3"
--sLogFileName表示存储文件名,例如zypk 传入
function Sys_SaveActionParamLog(sLogFileName,sText)

	if type(sLogFileName) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SaveActionParamLog 中 sText 只能传字符")
		return
	end

	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SaveActionParamLog 中 sText 只能传字符")
		return
	end
	
	local sUserName = Get_UserName()
	local sUserId = Get_UserId()
	local sUserLev = Get_UserLevel()
	local sUserPro = Get_UserProfession()
	local sUserMeto = Get_UserMetempsychosis() 
	local sUserAccountId = Get_UserAccountId()
	local sLogUserData = sUserName..'['.. sUserId ..']['.. sUserLev ..']['.. sUserPro ..']['.. sUserMeto ..']['..sUserAccountId..']'

	local sLogFile = "gmlog/"..sLogFileName
	local sLogText = string.format("%s,%s",sLogUserData,sText)
	return SaveCustomLog(sLogFile,sLogText)
end

--//ACTION_USER_CUSTOM_LOG 1085
--bool SaveCustomLog(const char * pszLogFile, const char * pszText);
--sText表示Log配置信息,例如："250	8819	1599	"
function Sys_SaveEmoneyBuy(sText)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SaveEmoneyBuy 中 sText 只能传字符")
		return
	end

	local sUserId = Get_UserId()
	local sUserName = Get_UserName()
	local sUserAccountId = Get_UserAccountId()
	local sLogUserData = sUserId ..'	'.. sUserName ..'	'.. sUserAccountId
	
	local sLogFile = "gmlog/emoney_buy"
	local sLogText = string.format("%s	%s",sLogUserData,sText)
	return SaveCustomLog(sLogFile,sLogText)
end

--// 对应type=101，菜单文本
--// 菜单文本。 参数说明：strText表示显示的文本内容，可包含空格，也可为空行。如果失败返回false, 成功返回true。
--bool MenuText(string strText, int nline);
--sText是npc对话的内容
function Sys_DialogText(sText,nline)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_DialogText 中 sText 只能传字符")
		return
	end

	if string.len(sText) >= 255 then
		Sys_SaveAbnormalLog("函数 Sys_DialogText 中 sText 超过255个字符")
		return
	end
	
	if nline == nil then
		nline = 0
	elseif type(nline) ~= "number" or nline < 0 then
		Sys_SaveAbnormalLog("函数 Sys_DialogText 中 nline 只能传大于等于0的整数")
		return
	end

	return MenuText(sText,nline)
end

--// 对应type=102，菜单超链接
--// 菜单超链接，指玩家选项显示的内容。参数说明: strText表示所要显示的文字内容, nAlign表示对齐方式, strFunc表示执行的函数.如果失败返回false，成功返回true.
--bool MenuLink(string strText, int nAlign, string strFunc);
--sOptionText表示选项内容
--nAlign表示对齐方式，默认写0
--fFunc表示点击选项的时候出发的函数

function Sys_DialogOption(sOptionText,strFunc,nAlign)
	if type(sOptionText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_DialogOption 中 sOptionText 只能传字符")
		return
	end
	
	if strFunc == nil then
		strFunc = "</F>NULL"
	elseif type(strFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_DialogOption 中 strFunc 只能传字符")
		return
	end

	if nAlign == nil then
		nAlign = 0
	elseif type(nAlign) ~= "number" or nAlign < 0 or nAlign%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Sys_DialogOption 中 nAlign 只能传大于0的整数")
		return
	end

	return MenuLink(sOptionText,nAlign,strFunc)
end


--// 对应type=103, 菜单输入框
--// 菜单输入框, 指可以让玩家输入内容的输入框. 参数说明: strText表示输入框上方的提示文字, nAcceptLen表示输入字符的长度, nPassword如果是非密码填0, pCallFunc表示执行的函数. 如果失败返回false, 成功返回true.
--bool MenuEdit(string strText, int nAcceptLen, int nPassword, int idTask, string strFunc);
--sText:表示输入框的提示文字
--nLen:表示输入字符的长度
--nPassword:表示输入的如果是非密码则写0
--fFunc：表示执行的函数

function Sys_DialogOptEdit(sText,nLen,sFunc,nPassword)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_DialogOptEdit 中 sText 只能传字符")
		return
	end
	
	if type(nLen) ~= "number" or nLen <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_DialogOptEdit 中 nLen 只能传大于0的数")
		return
	end
	
	if sFunc == nil then
		sFunc = "NULL"
	elseif type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_DialogOptEdit 中 sFunc 只能传字符")
		return
	end
	
	if nPassword == nil then
		nPassword = 0
	elseif type(nPassword) ~= "number" or (nPassword ~= 0 and  nPassword ~= 1) then
		Sys_SaveAbnormalLog("函数 Sys_DialogOptEdit 中 nPassword 只能传0或1")
		return
	end

	return MenuEdit(sText,nLen,nPassword,string.format("</F>%s",sFunc))
end

--// 对应type=104, 菜单图片(NPC使用)
--// 菜单图片. 参数说明: x, y 表示图片相对于整个对话框的x, y坐标; idPic表示这张图片在客户端下的npcface.ani文件中的编号, strFunc表示点击图片执行的函数. 如果失败返回false, 成功返回true.
--bool MenuPic(int x, int y, int idPic, int idTask, string strFunc);
--nFaceNum :npc头像编号
--x:默认写10
--y:默认写10

function Sys_DialogFace(nNpcID)
	local nFaceNum
	if nNpcID == nil then
		nNpcID = 0
	end
	local nNpcLookFace=math.floor(Get_NpcLookface(nNpcID)/10)
	if tNpcFace[nNpcLookFace] ~= nil then		
		nFaceNum=tNpcFace[nNpcLookFace]
	else
	    nFaceNum=9999
	end

	return MenuPic(10,10,nFaceNum,"</F>NULL")
end

--// 对应type=104, 菜单图片(物品使用)
--// 菜单图片. 参数说明: x, y 表示图片相对于整个对话框的x, y坐标; idPic表示这张图片在客户端下的npcface.ani文件中的编号, strFunc表示点击图片执行的函数. 如果失败返回false, 成功返回true.
--bool MenuPic(int x, int y, int idPic, int idTask, string strFunc);
--nFaceNum :npc头像编号
--x:默认写10
--y:默认写10
function Sys_DialogItemFace(nItemId)
	local nFaceNum
	if nItemId == nil then
		nItemId = 0
	end

	if tItemFace[nItemId] ~= nil then
		nFaceNum=tItemFace[nItemId]
	else
		nFaceNum=9999
	end
	
	return MenuPic(10,10,nFaceNum,"</F>NULL")
end

--// 对应type=120, 菜单创建
--// 菜单创建. 如果失败返回false, 成功返回true.
--bool MenuCreate(int idTask, string strFunc);
--nGlobalIdTask:执行的task_id
--fFunc:点击强行关闭的时候执行的函数

function Sys_DialogEnd(strFunc)
	if strFunc == nil then
		strFunc = "</F>NULL"
	elseif type(strFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_DialogEnd 中 strFunc 只能传字符")
		return
	end
	
	return MenuCreate(0,strFunc)
end



--// 对应type=105, 弹出确认对话框
--// 弹出确认对话框. 参数说明: strText表示文字串，strFunc表示确认后执行的函数, pszFailFunc表示取消后执行的函数. 如果失败返回false, 成功返回true.
--bool MsgBox(string strText, string strFunc, string strFailFunc);
--sText:表示105提示内容
--fFunc:表示点击确认执行的函数
--fFailFunc:表示点击取消执行的函数

function Sys_MsgBox(sText,sFunc,sFailFunc)
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_MsgBox 中 sText 只能传字符")
		return
	end
	
	if sFunc == nil then
		sFunc = "NULL"
	elseif type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_MsgBox 中 sFunc 只能传字符")
		return
	end
	
	if sFailFunc == nil then
		sFailFunc = "NULL"
	elseif type(sFailFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_MsgBox 中 sFailFunc 只能传字符")
		return
	end

	return MsgBox(sText,string.format("</F>%s",sFunc),string.format("</F>%s",sFailFunc))
end



--// 对应type=123, 检测服务器当前时间
--// 检查服务器当前时间格式. 参数说明: type表示指定所需要检测的时间类型, strParam表示时间格式. 如果失败返回false, 成功返回true.
--bool CheckTime(int type, string strParam);

--// 检测服务器当前时间.格式为:data指定所需要检测的时间类型 
--		//data=0-检查当前服务器详细时间param= "yy-mm-dd hh:mm yy-mm-dd hh:mm"; 
--		//data=1-检查年某天时间param="mm-dd hh:mm mm-dd hh:mm", 
--		//data=2-检查月某天时间param="dd hh:mm dd hh:mm", 
--		//data=3-检查周某天时间param="dd hh:mm dd hh:mm"(周一~周六为1~6,周日为0),
--		//data=4-检查日时间param="hh:mm hh:mm", 
--		//data=5-检查小时时间param="mm mm"(每个小时的第几分到底几分).
--		//★注意,该条action格式要求,param参数中,前面一个时间必须比后面一个要小.

--		//data=0-检查当前服务器详细时间param= "yy-mm-dd hh:mm yy-mm-dd hh:mm"; 
--sParam:表示时间段

function Sys_ChkFullTime(sParam)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_ChkFullTime 中 sParam 只能传字符")
		return
	end

	return CheckTime(0,sParam)
end

--		//data=1-检查年某天时间param="mm-dd hh:mm mm-dd hh:mm", 
function Sys_ChkDateTime(sParam)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_ChkDateTime 中 sParam 只能传字符")
		return
	end
	
	return CheckTime(1,sParam)
end

--		//data=2-检查月某天时间param="dd hh:mm dd hh:mm", 
function Sys_ChkMonthTime(sParam)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_ChkMonthTime 中 sParam 只能传字符")
		return
	end
	
	return CheckTime(2,sParam)
end

--		//data=3-检查周某天时间param="dd hh:mm dd hh:mm"(周一~周六为1~6,周日为0),
function Sys_ChkWeedTime(sParam)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_ChkWeedTime 中 sParam 只能传字符")
		return
	end
	
	return CheckTime(3,sParam)
end

--		//data=4-检查日时间param="hh:mm hh:mm", 
function Sys_ChkDayTime(sParam)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_ChkDayTime 中 sParam 只能传字符")
		return
	end
	
	return CheckTime(4,sParam)
end

--		//data=5-检查小时时间param="mm mm"(每个小时的第几分到底几分).
function Sys_ChkMinute(sParam)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_ChkMinute 中 sParam 只能传字符")
		return
	end
	
	return CheckTime(5,sParam)
end

--// 对应type=124, 向客户端发送界面命令
--// 向客户端发送界面命令. 参数说明: nData表示命令编号. 如果失败返回false, 成功返回true.
--bool PostCmd(int nData);

function Sys_PostCmd(nData)
	if type(nData) ~= "number" or nData%1 ~= 0 or nData < 0 then
		Sys_SaveAbnormalLog("函数 Sys_PostCmd 中 nData 只能传大于0的整数")
		return
	end
	
	return PostCmd(nData)
end

--// 对应type=125, 全服务器广播文字消息
--// 全服务器广播文字消息. 参数说明: nData表示广播的频道, strParam表示广播的内容. 如果失败返回false, 成功返回true.
--bool BrocastMsg(int nData, string strParam);

--// 	const unsigned short _TXTATR_NORMAL		=2000;
--// 	const unsigned short _TXTATR_ACTION		=_TXTATR_NORMAL+2=2002;	// 动作
--//	 	const unsigned short _TXTATR_TEAM			=_TXTATR_NORMAL+3=2003;	// 队伍
--// 	const unsigned short _TXTATR_SYSTEM		=_TXTATR_NORMAL+5=2005;	// 系统——显示在屏幕左上角
--// 	const unsigned short _TXTATR_TALK				=_TXTATR_NORMAL+7=2007;	// 交谈——显示在屏幕左下角
--// 	const unsigned short _TXTATR_GM 				=_TXTATR_NORMAL+11=2011;	// GM频道——显示在屏幕中央



--// 	const unsigned short _TXTATR_NORMAL		=2000;
function Sys_NormalBroadcast(sContent)
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_NormalBroadcast 中 sContent 只能传字符")
		return
	end
	
	return BrocastMsg(2000,sContent)
end

--// 	const unsigned short _TXTATR_ACTION		=_TXTATR_NORMAL+2=2002;	// 动作
function Sys_ActionBroadcast(sContent)
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_ActionBroadcast 中 sContent 只能传字符")
		return
	end
	
	return BrocastMsg(2002,sContent)
end

--//	 	const unsigned short _TXTATR_TEAM			=_TXTATR_NORMAL+3=2003;	// 队伍
function Sys_TeamBroadcast(sContent)
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_TeamBroadcast 中 sContent 只能传字符")
		return
	end
	
	return BrocastMsg(2003,sContent)
end

--// 	const unsigned short _TXTATR_SYSTEM		=_TXTATR_NORMAL+5=2005;	// 系统——显示在屏幕左上角
function Sys_SystemBroadcast(sContent)
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SystemBroadcast 中 sContent 只能传字符")
		return
	end
	
	return BrocastMsg(2005,sContent)
end

--// 	const unsigned short _TXTATR_TALK				=_TXTATR_NORMAL+7=2007;	// 交谈——显示在屏幕左下角
function Sys_TalkBroadcast(sContent)
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_TalkBroadcast 中 sContent 只能传字符")
		return
	end
	
	return BrocastMsg(2007,sContent)
end

--// 	const unsigned short _TXTATR_GM 				=_TXTATR_NORMAL+11=2011;	// GM频道——显示在屏幕中央
function Sys_GmBroadcast(sContent)
	if type(sContent) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_GmBroadcast 中 sContent 只能传字符")
		return
	end
	
	return BrocastMsg(2011,sContent)
end

--// 对应type=131, 自动寻路部分
--// 自动寻路. 参数说明: idUser表示玩家id,填0表示当前玩家; nPosX, nPosY表示x、y坐标. idNpc表示寻路所要找的npc, 如果写0的话，则不会弹出对话, idMap表示npc所在的地图. 如果失败返回false, 成功返回true.
--bool GotoSomeWhere(int idUser, int nPosX, int nPosY, int idNpc, int idMap);
--nUserId:玩家id，当前玩家默认值为0
--nPosX：X坐标
--nPosY：Y坐标
--nNpcId：寻路所要找的npcId，写0则寻路后不弹出弹框
--nMapId：寻路中npc所在的地图id

function Sys_GotoSomeWhere(nPosX,nPosY,nMapId,nNpcId,nUserId)
	if type(nPosX) ~= "number" or nPosX < 0 then
		Sys_SaveAbnormalLog("函数 Sys_GotoSomeWhere 中 nPosX 只能传大等于0的数")
		return
	end
	
	if type(nPosY) ~= "number" or nPosY < 0 then
		Sys_SaveAbnormalLog("函数 Sys_GotoSomeWhere 中 nPosY 只能传大等于0的数")
		return
	end
	
	if type(nMapId) ~= "number" or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_GotoSomeWhere 中 nMapId 只能传大于0的数")
		return
	end
	
	if nNpcId == nil then
		nNpcId = 0
	elseif type(nNpcId) ~= "number" or nNpcId < 0 or nNpcId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Sys_GotoSomeWhere 中 nNpcId 只能传大于0的整数")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Sys_GotoSomeWhere 中 nUserId 只能传大于0的整数")
		return
	end
	
	return GotoSomeWhere(nUserId,nPosX,nPosY,nNpcId,nMapId)
end
----= 1650			//发消息通知客户端点选目标,data=后面操作的action_id，param=[鼠标图片id，对应客户端Cursor.ini的记录]
--
----//发消息通知客户端点选目标，参数说明：idUser指玩家ID，idMouseType指鼠标点选的类型，pszCallLuaFunc指点选完后要调查的LUA函数，如果失败返回false，否则返回true。
----MouseWaitClick(OBJID idUser, OBJID idMouseType, LPCTSTR pszCallLuaFunc)

--nMouseType:表示鼠标点选的类型--如玫瑰"22"，详细见Cursor.ini  -->   玫瑰鼠标（22-23-1010）
--sFunc:表示点选完后要调查的LUA函数--如"Func_Next"
--nUserId:表示玩家的ID--默认0为当前玩家

function Sys_MouseWaitClick(nMouseType,sFunc,nUserId)
	if type(nMouseType) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_MouseWaitClick 中 nMouseType 只能传大于0的整数")
		return
	end

	if type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_MouseWaitClick 中 sFunc 只能传字符")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_MouseWaitClick 中 nUserId 只能传大于0的整数")
		return
	end

	return MouseWaitClick(nUserId,nMouseType,string.format("</F>%s",sFunc))
end

----	= 1651			//判断点选目标的类型 data：1表示点npc，param=‘npc名字’;data：2表示点怪物param=‘怪物id’;data：3表示判断点选玩家性别判断param=‘性别id’ 1男，2女
--
----//判断点选目标的类型，参数说明：idUser指玩家ID,nTargetType指点先的类型，对应CHOSEN_TYPE，szParam指点选后的操作参数，如果失败返回false，否则返回true。
----MouseJudgeType(OBJID idUser, OBJID idChosenTarget, int nTargetType, const char* szParam)

----npc:1
--sParam：表示点选NPC的名字 "我是NPC"
--nUserId：表示玩家的ID--默认0为当前玩家

function Sys_NpcMouseType(sParam,nUserId)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_NpcMouseType 中 sParam 只能传字符")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_NpcMouseType 中 nUserId 只能传大于0的整数")
		return
	end
	return MouseJudgeType(nUserId,1,sParam)
end

----monstertype:2
function Sys_MonsterMouseType(sParam,nUserId)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_NpcMouseType 中 sParam 只能传字符")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_NpcMouseType 中 nUserId 只能传大于0的整数")
		return
	end
	return MouseJudgeType(nUserId,2,sParam)
end

----usersex:3
function Sys_UserSexMouseType(sParam,nUserId)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_NpcMouseType 中 sParam 只能传字符")
		return
	end

	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_NpcMouseType 中 nUserId 只能传大于0的整数")
		return
	end
	return MouseJudgeType(nUserId,3,sParam)
end


----= 1652			//清除玩家当前指针选取状态 服务器新增清除玩家当前指针选取状态的action，服务器执行该action后，下发消息给客户端
--
----//清除玩家当前鼠标选取状态，参数说明：idUser指玩家ID，如果失败返回false，否则返回true。
----MouseClearStatus(OBJID idUser)

function Sys_MouseClearStatus(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_MouseClearStatus 中 nUserId 只能传大于0的整数")
		return
	end
	
	return MouseClearStatus(nUserId)
end

--
----= 1653			//点选目标的状态检查和修改	 该action的param的格式为“status [空格]操作符[空格]具体数值”
--
----//所点选目标的状态检查，参数说明：idUser指玩家ID，nStatus指所要检查的状态，如果失败返回false，否则返回true。
----bool MouseCheckStatus(OBJID idUser, int nStatus)
--
--function Sys_MouseCheckStatus(nUserId,nStatus)
--	nUserId = nUserId or 0
--	if nStatus then
--		return MouseCheckStatus(nUserId,nStatus)
--	end
--end
--
----//设置所点选目标的状态，参数说明：idUser指玩家ID，nStatus指所要设置的状态，如果失败返回false，否则返回true。
----bool MouseSetStatus(OBJID idUser, int nStatus)
--
--function Sys_MouseSetStatus(nUserId,nStatus)
--	nUserId = nUserId or 0
--	if nStatus then
--		return MouseSetStatus(nUserId,nStatus)
--	end
--end

--删除当前所点选的NPC或怪物，
--nUserId表示用户id.
--如果失败返回false，否则返回true。
function Sys_MouseDeleteChosen(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Sys_MouseDeleteChosen 第一个参数nUserId为整型并且大于等于0")
		return
	end
	
	return MouseDeleteChosen(nUserId)
end




--// 重置cq_dyna_global_data的data0-data5字段值. 参数说明: id表示表中的id字段值
--失败返回false,否则返回true。
function Sys_ResetAllSynaGlobalData(nGlobalId)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_ResetSynaGlobalData 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	return ResetAllSynaGlobalData(nGlobalId)
end

--// 重置cq_dyna_global_data的datastr0-datastr5字段值. 参数说明: id表示表中的id字段值
--失败返回false,否则返回true。
function Sys_ResetAllSynaGlobalDataStr(nGlobalId)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_ResetAllSynaGlobalDataStr 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	return ResetAllSynaGlobalDataStr(nGlobalId)
end

--// 重置cq_dyna_global_data的time0-time5字段值. 参数说明: id表示表中的id字段值
--失败返回false,否则返回true。
function Sys_ResetAllSynaGlobalTime(nGlobalId)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_ResetAllSynaGlobalTime 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	return ResetAllSynaGlobalTime(nGlobalId)
end



--// 设置cq_dyna_global_data的data0-data5字段值. 参数说明: id表示表中的id字段值, idx表示SCRIPT_PARAM_DYNA_GLOBAL_DATA0的枚举值， nData表示设置的值. 如果失败返回false，成功返回true.
--bool SetSynaGlobalData(int id, int nPos, int nData);
--data 0
function Sys_SetSynaGlobalData0(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData0 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData0 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalData(nGlobalId,G_DYNA_GLOBAL_DATA0,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATA1 		= 2203;
function Sys_SetSynaGlobalData1(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData1 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData1 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalData(nGlobalId,G_DYNA_GLOBAL_DATA1,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATA2		= 2204;
function Sys_SetSynaGlobalData2(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData2 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData2 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalData(nGlobalId,G_DYNA_GLOBAL_DATA2,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATA3		= 2205;
function Sys_SetSynaGlobalData3(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData3 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData3 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalData(nGlobalId,G_DYNA_GLOBAL_DATA3,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATA4		= 2206;
function Sys_SetSynaGlobalData4(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData4 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData4 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalData(nGlobalId,G_DYNA_GLOBAL_DATA4,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATA5		= 2207;
function Sys_SetSynaGlobalData5(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData5 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData5 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalData(nGlobalId,G_DYNA_GLOBAL_DATA5,nData)
end

--// 设置cq_dyna_global_data的datastr0-datastr5字段值. 参数说明: id表示表中的id字段值, idx表示SCRIPT_PARAM_DYNA_GLOBAL_DATASTR0的枚举值，strData表示设置的串. 如果失败返回false，成功返回true.
--bool SetSynaGlobalDataStr(int id, int nPos, string strData);
--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR0	= 2208;
function Sys_SetSynaGlobalDataStr0(nGlobalId,sText)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr0 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr0 中 sText 只能传字符")
		return
	end
	
	return SetSynaGlobalDataStr(nGlobalId,G_DYNA_GLOBAL_DATASTR0,sText)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR1	= 2209;
function Sys_SetSynaGlobalDataStr1(nGlobalId,sText)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr1 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr1 中 sText 只能传字符")
		return
	end
	
	return SetSynaGlobalDataStr(nGlobalId,G_DYNA_GLOBAL_DATASTR1,sText)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR2	= 2210;
function Sys_SetSynaGlobalDataStr2(nGlobalId,sText)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr2 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr2 中 sText 只能传字符")
		return
	end
	
	return SetSynaGlobalDataStr(nGlobalId,G_DYNA_GLOBAL_DATASTR2,sText)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR3	= 2211;
function Sys_SetSynaGlobalDataStr3(nGlobalId,sText)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr3 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr3 中 sText 只能传字符")
		return
	end
	
	return SetSynaGlobalDataStr(nGlobalId,G_DYNA_GLOBAL_DATASTR3,sText)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR4	= 2212;
function Sys_SetSynaGlobalDataStr4(nGlobalId,sText)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr4 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr4 中 sText 只能传字符")
		return
	end
	
	return SetSynaGlobalDataStr(nGlobalId,G_DYNA_GLOBAL_DATASTR4,sText)
end

--SCRIPT_PARAM_DYNA_GLOBAL_DATASTR5	= 2213;
function Sys_SetSynaGlobalDataStr5(nGlobalId,sText)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr5 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr5 中 sText 只能传字符")
		return
	end
	
	return SetSynaGlobalDataStr(nGlobalId,G_DYNA_GLOBAL_DATASTR5,sText)
end

--SCRIPT_PARAM_DYNA_GLOBAL_TIME0		= 2214;
--// 设置cq_dyna_global_data的time0-time5字段值. 参数说明: id表示表中的id字段值, idx表示SCRIPT_PARAM_DYNA_GLOBAL_TIME0的枚举值， nData表示设置的值. 如果失败返回false，成功返回true.
--bool SetSynaGlobalTime(int id, int nPos, int nData);
function Sys_SetSynaGlobalTime0(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime0 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime0 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalTime(nGlobalId,G_DYNA_GLOBAL_TIME0,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_TIME1		= 2215;
function Sys_SetSynaGlobalTime1(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime1 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime1 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalTime(nGlobalId,G_DYNA_GLOBAL_TIME1,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_TIME2		= 2216;
function Sys_SetSynaGlobalTime2(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime2 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime2 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalTime(nGlobalId,G_DYNA_GLOBAL_TIME2,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_TIME3		= 2217;
function Sys_SetSynaGlobalTime3(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime3 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime3 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalTime(nGlobalId,G_DYNA_GLOBAL_TIME3,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_TIME4		= 2218;
function Sys_SetSynaGlobalTime4(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime4 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime4 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalTime(nGlobalId,G_DYNA_GLOBAL_TIME4,nData)
end

--SCRIPT_PARAM_DYNA_GLOBAL_TIME5		= 2219;
function Sys_SetSynaGlobalTime5(nGlobalId,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime5 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime5 中 nData 只能传数字")
		return
	end
	
	return SetSynaGlobalTime(nGlobalId,G_DYNA_GLOBAL_TIME5,nData)
end

-- 设置cq_dyna_global_data的data0-data5字段值
function Sys_SetSynaGlobalData(nGlobalId,nPos,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData 中 nData 只能传数字")
		return
	end
	
	if type(nPos) ~= "number" or nPos < 0 or nPos%1 ~= 0 or nPos > 5 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalData 中 nPos 只能传0~5之间的整数")
		return
	end
	
	local nIndex = 0
	
	if nPos == 0 then
		nIndex = G_DYNA_GLOBAL_DATA0
	elseif nPos == 1 then
		nIndex = G_DYNA_GLOBAL_DATA1
	elseif nPos == 2 then
		nIndex = G_DYNA_GLOBAL_DATA2
	elseif nPos == 3 then
		nIndex = G_DYNA_GLOBAL_DATA3
	elseif nPos == 4 then
		nIndex = G_DYNA_GLOBAL_DATA4
	elseif nPos == 5 then
		nIndex = G_DYNA_GLOBAL_DATA5
	end

	return SetSynaGlobalData(nGlobalId,nIndex,nData)
end

-- 设置cq_dyna_global_data的datastr0-datastr5字段值
function Sys_SetSynaGlobalDataStr(nGlobalId,nPos,sText)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr 中 sText 只能传字符")
		return
	end
	
	if type(nPos) ~= "number" or nPos < 0 or nPos%1 ~= 0 or nPos > 5 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalDataStr 中 nPos 只能传0~5之间的整数")
		return
	end
	
	local nIndex = 0
	
	if nPos == 0 then
		nIndex = G_DYNA_GLOBAL_DATASTR0
	elseif nPos == 1 then
		nIndex = G_DYNA_GLOBAL_DATASTR1
	elseif nPos == 2 then
		nIndex = G_DYNA_GLOBAL_DATASTR2
	elseif nPos == 3 then
		nIndex = G_DYNA_GLOBAL_DATASTR3
	elseif nPos == 4 then
		nIndex = G_DYNA_GLOBAL_DATASTR4
	elseif nPos == 5 then
		nIndex = G_DYNA_GLOBAL_DATASTR5
	end

	return SetSynaGlobalDataStr(nGlobalId,nIndex,sText)
end

-- 设置cq_dyna_global_data的time0-time5字段值
function Sys_SetSynaGlobalTime(nGlobalId,nPos,nData)
	if type(nGlobalId) ~= "number" or nGlobalId%1 ~= 0 or nGlobalId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime 中 nGlobalId 只能传大于0的整数")
		return
	end
	
	if type(nData) ~= "number" then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime 中 nData 只能传数字")
		return
	end
	
	if type(nPos) ~= "number" or nPos < 0 or nPos%1 ~= 0 or nPos > 5 then
		Sys_SaveAbnormalLog("函数 Sys_SetSynaGlobalTime 中 nPos 只能传0~5之间的整数")
		return
	end
	
	local nIndex = 0
	
	if nPos == 0 then
		nIndex = G_DYNA_GLOBAL_TIME0
	elseif nPos == 1 then
		nIndex = G_DYNA_GLOBAL_TIME1
	elseif nPos == 2 then
		nIndex = G_DYNA_GLOBAL_TIME2
	elseif nPos == 3 then
		nIndex = G_DYNA_GLOBAL_TIME3
	elseif nPos == 4 then
		nIndex = G_DYNA_GLOBAL_TIME4
	elseif nPos == 5 then
		nIndex = G_DYNA_GLOBAL_TIME5
	end

	return SetSynaGlobalTime(nGlobalId,nIndex,nData)
end



--设置定时器，倒数记时完成后触发指定的函数
--nInterval 记时时长，单位：秒
--sFunc 计时完成后执行的函数
--例：Sys_SetLuaTimer(10,"</F>sFunc")
function Sys_SetLuaTimer(nInterval,sFunc)
	if type(nInterval) ~= "number" or nInterval%1 ~= 0 or nInterval <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetLuaTimer 第一个参数nInterval为整型且大于0")
		return
	end
	
	if sFunc == nil then
		sFunc = "NULL"
	elseif type(sFunc) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetLuaTimer 第二个参数sFunc为字符串")
		return
	end
	
	return SetLuaTimer(nInterval,string.format("</F>%s",sFunc))
end

--设置定时器，设置指定id定时器。
--nTimerId 定时器id.
--nInterval 记时时长，单位：秒.
--例：Sys_SetLuaEventTimer(1000,10)
function Sys_SetLuaEventTimer(nTimerId,nInterval)
	if type(nTimerId) ~= "number" or nTimerId%1 ~= 0 or nTimerId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetLuaEventTimer 第一个参数nTimerId为整型且大于0")
		return
	end
	
	if type(nInterval) ~= "number" or nInterval%1 ~= 0 or nInterval <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetLuaEventTimer 第二个参数nInterval为整型且大于0")
		return
	end
	
	return SetLuaEventTimer(nTimerId,nInterval)
end

--检查定时器是否存在。
--nTimerId 定时器id.
--例：Sys_ChkLuaEventTimer(1000)
function Sys_ChkLuaEventTimer(nTimerId)
	if type(nTimerId) ~= "number" or nTimerId%1 ~= 0 or nTimerId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_ChkLuaEventTimer 第一个参数nTimerId为整型且大于0")
		return
	end

	return ChkLuaEventTimer(nTimerId)
end

--删除定时器。
--nTimerId 定时器id.
--例：Sys_DelLuaEventTimer(1000)
function Sys_DelLuaEventTimer(nTimerId)
	if type(nTimerId) ~= "number" or nTimerId%1 ~= 0 or nTimerId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_KillLuaEventTimer 第一个参数nTimerId为整型且大于0")
		return
	end

	return KillLuaEventTimer(nTimerId)
end

--公告更新.
--nNumber 一段公告的序号 0开始，1中间语句，2结束。只有一条，就用2.
--sText 每行公告内容
--nUserId 用户id 可缺省
--例：Sys_SetMenuNotice(0,"第一行",0)||Sys_MenuNotice(0,"第一行")
function Sys_SetMenuNotice(nNumber,sText,nUserId)
	if type(nNumber) ~= "number" or nNumber%1 ~= 0 or nNumber < 0 or nNumber > 2 then
		Sys_SaveAbnormalLog("函数 Sys_SetMenuNotice 第一个参数nNumber为整型且范围在0--2")
		return
	end
	
	if type(sText) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetMenuNotice 第二个参数sText为字符串")
		return
	end
	
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId%1 ~= 0 or nUserId < 0 then
		Sys_SaveAbnormalLog("函数 Sys_SetMenuNotice 第三个参数nUserId为整型并且大于等于0")
		return
	end
	
	return MenuNotice(nUserId,nNumber,sText)
end

--在BBS公告板中，添加一条SYSTEM频道的消息，留言人为“SYSTEM”. 
--sParam是消息内容. 
--如果失败返回false, 成功返回true.
function Sys_SetEventBBS(sParam)
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_SetEventBBS 第一个参数sParam为字符串")
		return
	end
	
	return EventBBS(sParam)
end

-- //全服邀请对象筛选。 参数说明：idInvite为标识编号（限1~6）， param="attr opt data"，attr可选，要求支持一下类型的条件检查："pk" (==,>=,<=) // "profession" (==,>=,<=) "level" (==,>=,<=)"rankshow" (==,>=,<=)
-- //"metempsychosis" (==,>=,<=) "battlelev" (==,>=,<=)进行符合条件筛选的时候，只需在param最前面加一个标识编号就好了，如：'level >= 60 level <= 90'即标示筛选60~90级别的玩家
-- ACTION_SYS_INVITE_FILTER			= 129,			// 全服邀请对象筛选。
function Sys_InviteFilter(nInivteId,sParam)
	if type(nInivteId) ~= "number" or nInivteId%1 ~= 0 or nInivteId < 1 or nInivteId > 6 then
		Sys_SaveAbnormalLog("函数 Sys_InviteFilter 参数 nInivteId 只能传1到6的整数")
		return
	end
	
	if type(sParam) ~= "string" then
		Sys_SaveAbnormalLog("函数 Sys_InviteFilter 参数 sParam 只能传字符")
		return
	end
	
	return InviteFilter(nInivteId,sParam)
end

-- //删除全服邀请对象。参数说明：idInvite为标识编号（限1~6）
-- ACTION_SYS_INVITE_FILTER			= 129,			// 全服邀请对象筛选。
function Sys_DelInvite(nInivteId)
	if type(nInivteId) ~= "number" or nInivteId%1 ~= 0 or nInivteId < 1 or nInivteId > 6 then
		Sys_SaveAbnormalLog("函数 Sys_DelInvite 参数 nInivteId 只能传1到6的整数")
		return
	end
	
	return DelInvite(nInivteId)
end

-- // 全服邀请传送，参数说明：idMap nTransPosx1 nTransPosy1 nTransPosx2 nTransPosy2 nTransPosx3 nTransPosy3 nTransPosx4 nTransPosy4 nTransPosx5 nTransPosy5 nTransPosx6 nTransPosy6 nTransPosx7 nTransPosy7 nTransPosx8 nTransPosy8
-- // 对应记录地图id及8个落脚点坐标,服务端在该action执行时，先内存中记录全服邀请对象发送传送邀请,idStrSendInvite服务端在收到客户端提交的接受邀请的消息后，检查改玩家是否在内存记录的邀请列表中,
-- // 若该玩家在邀请列表中，则先保存玩家当前坐标，再根据配置的地图id及落脚点坐标，随机选取其中一个坐标，将该玩家传送到该坐标上，并将idStrTransOK下传给该玩家,idInvite为标识编号（限1~6）,nCloseSecs(秒)：全服邀请的关闭倒计时设置
-- tTransPosx 为坐标点的表 格式为 tTransPosx[1]["X"],tTransPosx[1]["Y"]
-- ACTION_SYS_INVITE_TRANS			 = 130,		// 全服邀请传送
function Sys_InviteTrans(nMapId,tTransPosx,nStrSendInviteId,nStrTransOKId,nInviteId,nCloseSecs)
	if type(nMapId) ~= "number" or nMapId%1 ~= 0 or nMapId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_InviteTrans 参数 nMapId 只能传大于0的整数")
		return
	end
	
	if type(nStrSendInviteId) ~= "number" or nStrSendInviteId%1 ~= 0 or nStrSendInviteId < 0 then
		Sys_SaveAbnormalLog("函数 Sys_InviteTrans 参数 nStrSendInviteId 只能传大于等于0的整数")
		return
	end
	
	if type(nStrTransOKId) ~= "number" or nStrTransOKId%1 ~= 0 or nStrTransOKId < 0 then
		Sys_SaveAbnormalLog("函数 Sys_InviteTrans 参数 nStrTransOKId 只能传大于等于0的整数")
		return
	end
	
	if type(nInviteId) ~= "number" or nInviteId%1 ~= 0 or nInviteId < 0 then
		Sys_SaveAbnormalLog("函数 Sys_InviteTrans 参数 nInviteId 只能传大于等于0的整数")
		return
	end
	
	if type(nCloseSecs) ~= "number" or nCloseSecs%1 ~= 0 or nCloseSecs < 0 then
		Sys_SaveAbnormalLog("函数 Sys_InviteTrans 参数 nCloseSecs 只能传大于等于0的整数")
		return
	end
	
	if type(tTransPosx) ~= "table" then
		Sys_SaveAbnormalLog("函数 Sys_InviteTrans 参数 tTransPosx 只能传表进去")
		return
	end
	
	for i,v in pairs(tTransPosx) do
		if type(v["X"]) ~= "number" or v["X"]%1 ~= 0 or v["X"] < 0 then
			local str = string.format("函数 Sys_InviteTrans 参数 tTransPosx 中 第%d组的X 只能传大等于0的整数",i)
			Sys_SaveAbnormalLog(str)
			return
		end
		
		if type(v["Y"]) ~= "number" or v["Y"]%1 ~= 0 or v["Y"] < 0 then
			local str = string.format("函数 Sys_InviteTrans 参数 tTransPosx 中 第%d组的Y 只能传大等于0的整数",i)
			Sys_SaveAbnormalLog(str)
			return
		end
	end
	
	local nTransPosx1 = tTransPosx[1]["X"]
	local nTransPosy1 = tTransPosx[1]["Y"]
	
	local nTransPosx2 = tTransPosx[2]["X"]
	local nTransPosy2 = tTransPosx[2]["Y"]
	
	local nTransPosx3 = tTransPosx[3]["X"]
	local nTransPosy3 = tTransPosx[3]["Y"]
	
	local nTransPosx4 = tTransPosx[4]["X"]
	local nTransPosy4 = tTransPosx[4]["Y"]
	
	local nTransPosx5 = tTransPosx[5]["X"]
	local nTransPosy5 = tTransPosx[5]["Y"]
	
	local nTransPosx6 = tTransPosx[6]["X"]
	local nTransPosy6 = tTransPosx[6]["Y"]
	
	local nTransPosx7 = tTransPosx[7]["X"]
	local nTransPosy7 = tTransPosx[7]["Y"]
	
	local nTransPosx8 = tTransPosx[8]["X"]
	local nTransPosy8 = tTransPosx[8]["Y"]
	
	return InviteTrans(nMapId,nTransPosx1,nTransPosy1,nTransPosx2,nTransPosy2,nTransPosx3,nTransPosy3,nTransPosx4,nTransPosy4,nTransPosx5,nTransPosy5,nTransPosx6,nTransPosy6,nTransPosx7,nTransPosy7,nTransPosx8,nTransPosy8,nStrSendInviteId,nStrTransOKId,nInviteId,nCloseSecs)
end

--------------------------------2015.5.25-------------------------------------
-- 新增lua接口函数：
-- FirstCreateCountry(unsigned int idSyn)
-- 参数1:获胜帮派id
-- 无返回值

-- 该接口用于帮派战胜利后自动创建联盟并升级为国家。接口内有进行一系列判断，只需要在帮派战后调用即可。
function Sys_FirstCreateCountry(nSynId)
	if type(nSynId) ~= "number" or nSynId%1 ~= 0 or nSynId <= 0 then
		Sys_SaveAbnormalLog("函数 Sys_FirstCreateCountry 参数 nSynId 只能传大于0的整数")
		return
	end 

	return FirstCreateCountry(nSynId)
end

-- 判断本服是否有国家（执政盟）。无参数。返回值：如果存在执政盟返回ture，否则返回false
-- bool HaveCountry()
function Sys_HaveCountry()
	return HaveCountry()
end

--------------------------------非程序接口函数封装-------------------------------------
--用于把一个字符串分割成字符串数组，存储于表中。
--sFullString 被分割的完整字符串
--sSeparator 分割符
--例：local a = Sys_Split("123,12,abc",",") ,返回a = {"123","12","abc"}。
function Sys_Split(sFullString, sSeparator)
	local nFindStartIndex = 1
	local nSplitIndex = 1
	local nSplitArray = {}
	while true do
		local nFindLastIndex = string.find(sFullString, sSeparator, nFindStartIndex)
		if not nFindLastIndex then
			nSplitArray[nSplitIndex] = string.sub(sFullString, nFindStartIndex, string.len(sFullString))
			break
		end
	nSplitArray[nSplitIndex] = string.sub(sFullString, nFindStartIndex, nFindLastIndex - 1)
	nFindStartIndex = nFindLastIndex + string.len(sSeparator)
	nSplitIndex = nSplitIndex + 1
	end
	
	return nSplitArray
end

--简单的随机概率函数
--"nStartNum,nEndNum"。"10 100"表示有1/10的机会是true。
function Sys_Random(nStartNum,nEndNum)
	local nNum = math.random(1,nEndNum)
	if nNum <= nStartNum then
		return true
	else
		return false
	end
end

--解析数字包含函数,类似我们的地图属性
--nConNum 单个地图属性
--nTotalNum 总的地图属性值
function Sys_ParseNumbersContain(nConNum,nTotalNum)
	local nPow = 0
	local nTemp = 0
	local bBool = false
	while nTotalNum ~= 0 and nTotalNum ~= nil do
		nTemp = nTotalNum % 2
		
		if nTemp ~= 0 then
			if nConNum == 2 ^ nPow then
				bBool = true
				break
			end
		end

		nPow = nPow + 1
		nTotalNum = (nTotalNum - nTemp) / 2
	end
	return bBool
end

-- 对应actiontype = 113
-- 清除对话框Task计数
-- 参数1：idUser表示玩家ID, 填0表示自己。如果失败返回false，成功返回true.
-- bool MenuTaskClear(UINT idUser);
function Sys_DialogTaskClear()
	return MenuTaskClear(0)
end

----- 2015.10.13 ----------
-- #判断是否打开二级密码
-- 对应ACTION:1053
-- LUA接口：IsOpenSecondPWD
-- 参数1：玩家id
-- 返回值：true表示打开二级密码 false表示没有打开二级密码
function Sys_IsOpenSecondPWD(nUserId)
	if nUserId == nil then
		nUserId = 0
	elseif type(nUserId) ~= "number" or nUserId < 0 or nUserId%1 ~= 0 then
		Sys_SaveAbnormalLog("函数 Sys_IsOpenSecondPWD 中 nUserId 只能传大于等于0的整数")
		return
	end
	return IsOpenSecondPWD(nUserId)
end

-- #判断帐号服务器是否正常连接
-- 对应ACTION:1215
-- LUA接口：IsAccountServerNormal
-- 参数：无
-- 返回值：true表示链接正常 false表示链接断开
function Sys_IsAccountServerNormal()
	return IsAccountServerNormal()
end

-- #判断当前是否在该服务器
-- 对应ACTION:1213
-- LUA接口：CheckServerName
-- 参数：服务器名称
-- 返回值：true表示当前服务器名称与传入服务器名称相同 false表示当前服务器名称与传入服务器名称不同
function Sys_CheckServerName(sServerName)
	if type(sServerName) ~= "string" or sServerName == nil then
		Sys_SaveAbnormalLog("函数 Sys_CheckServerName 中 sServerName 只能传字符")
		return
	end
	return CheckServerName(sServerName)
end

-- #判断转服服务器是否连接上
-- 对应ACTION:1205
-- LUA接口：IsChangeServerEnable
-- 参数：无
-- 返回值：true表示转服服务器已连接 false表示转服服务器未连接
function Sys_IsChangeServerEnable()
	return IsChangeServerEnable()
end

-- #判断转服服务器是否空闲
-- 对应ACTION:1214
-- LUA接口：IsChangeServerIdle
-- 参数：无
-- 返回值：true表示转服服务器空闲 false转服服务器忙
function Sys_IsChangeServerIdle()
	return IsChangeServerIdle()
end