----------------------------------------------------------------------------
--Name:		[征服][模板逻辑]基础模板之npc模块.lua
--Purpose:	基础模板之npc模块
--Creator: 	郑江文
--Created:	2014/08/28
----------------------------------------------------------------------------

--//定义基础NPC类
DefaultNpc	=	{
	Text=tLuaRes[10001], --默认对白
	Option=tLuaRes[10002]	 --默认选项
}

function DefaultNpc:new(o)
	o =  o or {}
	setmetatable(o,self)
	self.__index = self
	return o
end

--//cq_NPC接口函数(Action_id=94418000)
function  LinkNpcMain(...)
	local nNpcId = Get_NpcId()
	local arg = {...}

	if tNpcGossip[nNpcId] ~= nil then
		tNpcGossip[nNpcId]:NpcProcess(nNpcId,arg[1],arg[2],arg[3],arg[4])
	else
		--闲聊表 tNpcGossip 未配置 对白
		Sys_DialogText(tLuaRes[10001])
		Sys_DialogOption(tLuaRes[10002],"</F>NULL")
		Sys_DialogFace(nNpcId)
		Sys_DialogEnd()
	end
end

--//基础NPC类的成员函数_流程函数
function DefaultNpc:NpcProcess(nNpcId,...)
	--功能 NPC处理
	local arg = {...}
	---print(111111)
	if type(tNpcGossip[nNpcId]["Function"]) == "function" then
		tNpcGossip[nNpcId]["Function"](arg[1],arg[2],arg[3],arg[4])
	else
		--������ڶ���ģ�壬���["Text1"]�ֶ��Ƿ������ã������ߵڶ��ף������ߵ����ס�
		tNpcGossip[nNpcId]:AIFunc_New(nNpcId, nil, 0, tNpcGossip[nNpcId]["nPageNum"])
	end

	--NPC对话开始选项
	if type(tNpcGossip[nNpcId]["OptionStarFunc"]) == "function" then
		tNpcGossip[nNpcId]["OptionStarFunc"](nNpcId,arg[1],arg[2],arg[3],arg[4])
	end

	--NPC任务结束表判断
	--local i = 1
	--if tNpcQuestEnd[nNpcId] ~= nil then
	--	while tNpcQuestEnd[nNpcId][i] ~= nil do
	--		local QuestId=tNpcQuestEnd[nNpcId][i]
	--		if PlayerInQuest(QuestId) then
	--			Sys_DialogOption("？"..tQuestTemplate[QuestId].Title,"</F>LinkQuestFinishDialog</N>"..QuestId)
	--		end
	--		i = i+1
	--	end
	--end

	--NPC任务中间表判断
	--local m = 1
	--if tNpcQuestMid[nNpcId] ~= nil then
	--	while tNpcQuestMid[nNpcId][m] ~= nil do
	--		local QuestId=tNpcQuestMid[nNpcId][m]
	--		if PlayerInQuest(QuestId) and type(tQuestTemplate[QuestId]["QuestMidNPC"]) == "function" then
	--			tQuestTemplate[QuestId]:QuestMidNPC(QuestId)
	--		end
	--		m = m+1
	--	end
	--end

	--NPC任务起始表判断
	--local j = 1
	--if tNpcQuestStar[nNpcId] ~= nil then
	--	while tNpcQuestStar[nNpcId][j] ~= nil and type(tNpcQuestStar[nNpcId][j]) == "number" do
	--		local QuestId=tNpcQuestStar[nNpcId][j]
	--		if not PlayerChkTask(QuestId) then
	--			if tQuestTemplate[QuestId]:IsQuestAvailable(QuestId) then
	--				Sys_DialogOption("！"..tQuestTemplate[QuestId].Title,"</F>LinkQuestAcceptDialog</N>"..QuestId)--接受任务显示选项
	--			end
	--		end
	--		j = j+1
	--	end
	--end

	--NPC对话结束选项
	if type(tNpcGossip[nNpcId]["OptionEndFunc"]) == "function" then
		tNpcGossip[nNpcId]["OptionEndFunc"](nNpcId,arg[1],arg[2],arg[3],arg[4])
	end

	if not(tNpcGossip[nNpcId]["OptionHidden"] == 1) then
		if tNpcGossip[nNpcId]["Option"] ~= nil and tNpcGossip[nNpcId]["Option"] ~= "" then
			Sys_DialogOption(tNpcGossip[nNpcId]["Option"],"</F>NULL")
		else
			Sys_DialogOption(tLuaRes[10003],"</F>NULL")
		end
	end
	Sys_DialogFace(nNpcId)
	Sys_DialogEnd()
end

--[[
@param
nNpcId:传入该NPC的ID
sIndex:从上一层传入的索引ID，第一层为nil传入
nPage:当前第几页选项，0为第一页
nPageNum:每页显示的选项数，默认为5
--]]
function DefaultNpc:AIFunc_New(nNpcId, sIndex, nPage, nPageNum)
	local bFirst = false				--�Ƿ��һ������˺��������Ƿ���NpcProcess�����õġ�
	local nPage = nPage or 0			--��ǰҳ�룬0Ϊ��һҳ
	local nPageNum = nPageNum or 8		--ÿҳ��ʾ��ѡ����
	local nCount = 1					--��ǰӦ����ʾ�ڼ���
	local nFlag = 0						--Ϊ0����û���԰ף�1�����ָ���԰ף�2����ָ���԰�
	local n = 1							--�洢��ʾ��Text�����

	--��һ��԰ף�֧������ѡ��
	if sIndex == nil then
		sIndex = "1"
		bFirst = true --��һ�㣬��Ϊִ�����Ҫ�ص���NPCģ���У����Բ��ܳ��ֶԻ�����������
	else
		if string.find(sIndex, "-") == nil then
			nFlag = 0
		else
			nFlag = 2
		end
		bFirst = false
	end

	--��Text���д���������ʾָ���԰׺ͷ�ָ���԰�
	if nFlag == 2 then
		if tNpcGossip[nNpcId]["Text"..sIndex] == nil or  tNpcGossip[nNpcId]["Text"..sIndex] == {} then
			return
		end

		if tNpcGossip[nNpcId]["ChkFunc"..sIndex] == nil or (type(tNpcGossip[nNpcId]["ChkFunc"..sIndex]) == "function" and tNpcGossip[nNpcId]["ChkFunc"..sIndex]() == true) then
			for _,v in ipairs(tNpcGossip[nNpcId]["Text"..sIndex]) do
				if tNpcGossip[nNpcId]["Text"..v] ~= nil and tNpcGossip[nNpcId]["Text"..v] ~= "" then
					Sys_DialogText(tNpcGossip[nNpcId]["Text"..v])
				end
			end
		else
			return
		end
	else
		for i = 1, 50 do
			if tNpcGossip[nNpcId]["Text"..sIndex.."-"..i] ~= nil and tNpcGossip[nNpcId]["Text"..sIndex.."-"..i] ~= "" then
				--���ܶ԰׳��ּ�麯��
				if tNpcGossip[nNpcId]["ChkFunc"..sIndex.."-"..i] == nil or (type(tNpcGossip[nNpcId]["ChkFunc"..sIndex.."-"..i]) == "function" and tNpcGossip[nNpcId]["ChkFunc"..sIndex.."-"..i]() == true) then

					--���԰�,����������5��
					for _,v in ipairs(tNpcGossip[nNpcId]["Text"..sIndex.."-"..i]) do
						if tNpcGossip[nNpcId]["Text"..v] ~= nil and tNpcGossip[nNpcId]["Text"..v] ~= "" then
							Sys_DialogText(tNpcGossip[nNpcId]["Text"..v])
						end
					end

					--�԰��ѳ������ټ�����ֵ
					nFlag = 1
					n = i
					break
				end
			else
				break
			end
		end
	end

	--�Դ����sIndex���д���ʹָ���ͷ�ָ���԰�һ�£���������Ĵ���
	if nFlag == 2 then
		n = string.sub(sIndex, string.find(sIndex,"-") + 1)
		sIndex = string.sub(sIndex, 1, string.find(sIndex,"-") - 1)
	elseif nFlag == 0 then --û�п�����ʾ�Ķ԰�
		if bFirst then --��һ�ν�������ʾĬ�϶԰׷���ֱ���ж�
			Sys_DialogText(tNpcGossip[nNpcId]["Text"])
			return
		else
			return
		end
	end

	--ѡ����ʾ����
	if nFlag ~= 0 then
	--ѡ����ڼ��
		if type(tNpcGossip[nNpcId]["tOption"..sIndex.."-"..n]) == "table" and tNpcGossip[nNpcId]["tOption"..sIndex.."-"..n] ~= {} then
			for _, v in ipairs(tNpcGossip[nNpcId]["tOption"..sIndex.."-"..n]) do --����ѡ���

				if nCount <= nPageNum and ((_ > nPage * nPageNum and nPage == 0) or (_ > nPage * nPageNum - (2 * nPage - 1) and nPage >= 1)) then --δ����ÿҳѡ����
					if tNpcGossip[nNpcId]["Option"..v] ~= nil and tNpcGossip[nNpcId]["Option"..v] ~= "" then --���ڸ�ѡ��
						if tNpcGossip[nNpcId]["OptionChkFunc"..v] == nil or (type(tNpcGossip[nNpcId]["OptionChkFunc"..v]) == "function" and tNpcGossip[nNpcId]["OptionChkFunc"..v]()) then --ChkFunc�ж�ͨ��
							local sTemp = ""
							local sFunc = ""
							if tNpcGossip[nNpcId]["OptionPoint"..v] ~= nil and tNpcGossip[nNpcId]["OptionPoint"..v] ~= "" then
								sTemp = tNpcGossip[nNpcId]["OptionPoint"..v]
							else
								sTemp = nil
							end

							if tNpcGossip[nNpcId]["OptionFunc"..v] ~= nil and tNpcGossip[nNpcId]["OptionFunc"..v] ~= "" and type(tNpcGossip[nNpcId]["OptionFunc"..v]) == "string" then
								sFunc = "</F>"..tNpcGossip[nNpcId]["OptionFunc"..v]
							elseif sTemp ~= nil then
								sFunc = "</F>LinkNpcGossipFunc_New</N>"..nNpcId.."</S>"..sTemp.."</N>0</N>"..nPageNum
							else
								sFunc = "</F>NULL"
							end

							Sys_DialogOption(tNpcGossip[nNpcId]["Option"..v], sFunc)
							nCount = nCount + 1
							--if nCount < nPageNum then
							--	nCount = nCount + 1
							--end

							if nCount == nPageNum and #tNpcGossip[nNpcId]["tOption"..sIndex.."-"..n] > nPageNum and nPage == 0  then
								local sTemp = sIndex.."-"..n
								Sys_DialogOption(tLuaRes[10006] , "</F>LinkNpcGossipFunc_New</N>"..nNpcId.."</S>"..sTemp.."</N>"..(nPage + 1).."</N>"..nPageNum)
								break
							elseif nCount == nPageNum - 1 and #tNpcGossip[nNpcId]["tOption"..sIndex.."-"..n] > nPageNum * (nPage + 1) - 2 * nPage and nPage > 0 then
								local sTemp = sIndex.."-"..n
								Sys_DialogOption(tLuaRes[10006] , "</F>LinkNpcGossipFunc_New</N>"..nNpcId.."</S>"..sTemp.."</N>"..(nPage + 1).."</N>"..nPageNum)
								break
							end
						end

					end
				--elseif nCount ==  nPageNum and #tNpcGossip[nNpcId]["tOption"..sIndex.."-"..n] > nPageNum then
				--	local sTemp = sIndex.."-"..n
				--	Sys_DialogOption("��һҳ", "</F>LinkNpcGossipFunc_New</N>"..nNpcId.."</S>"..sTemp.."</N>"..(nPage + 1).."</N>"..nPageNum)
				--	break
				end
			end

			if nPage > 0 then
				local sTemp = sIndex.."-"..n
				Sys_DialogOption(tLuaRes[10005], "</F>LinkNpcGossipFunc_New</N>"..nNpcId.."</S>"..sTemp.."</N>"..(nPage - 1).."</N>"..nPageNum)
			end
		end
	end

	--�󲿴��������Ƿ�NpcProcess����ĺ���
	if not bFirst then
		--Sys_SystemBroadcast("nCount is "..nCount)
		if nCount == 1 then
			Sys_DialogOption(tNpcGossip[nNpcId]["Option"] or tLuaRes[10002], "</F>NULL")
		end
		Sys_DialogFace(nNpcId)
		Sys_DialogEnd()
	end
end

function LinkNpcGossipFunc_New(nNpcId, sIndex, nPage, nPageNum)
	return tNpcGossip[nNpcId]:AIFunc_New(nNpcId, sIndex, nPage, nPageNum)
end

--������ ����
-- function LinkQuestAcceptDialog(QuestId)
	-- return tQuestTemplate[QuestId]:QuestAcceptDialog(QuestId)
-- end

--������ ����
-- function LinkQuestFinishDialog(QuestId)
	-- return tQuestTemplate[QuestId]:QuestFinishDialog(QuestId)
-- end

-------------------------NPC������԰�ģ��ʾ��-------------------------------
