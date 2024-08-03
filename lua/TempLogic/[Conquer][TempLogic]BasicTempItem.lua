----------------------------------------------------------------------------
--Name:		[征服][模板逻辑]基础模板之物品模块.lua
--Purpose:	基础模板之物品模块
--Creator: 	郑江文
--Created:	2014/08/28
----------------------------------------------------------------------------

--物品Action接口函数(Action_id=98471500)
function LinkItemMain()
	local nItemId = Get_ItemType()
	local sItemName = Get_ItemName()

	if tItem[nItemId] ~= nil then
		if tItem[nItemId]["Time"] ~= nil then
			if Sys_ChkFullTime(tItem[nItemId]["Time"]) then
				ItemProcess(nItemId,sItemName)
			else
				if Item_ChkItem(nItemId) then
					Item_DelItem(nItemId)
					User_TalkChannel2005(tLuaRes[10004])
				end
			end
		else
			ItemProcess(nItemId,sItemName)
		end
	end
end

--//基础Item类的成员函数_流程函数
function ItemProcess(nItemId,sItemName)
	Sys_DialogTaskClear()
	if type(tItem[nItemId]["Function"]) == "function" then
		--������������ֱ��������Լ�����߼�
		tItem[nItemId]["Function"](nItemId,sItemName)
	else
		--��������Ʒ�Ի���ģ��
		AIFunc_New(nItemId, nil, 0, tItem[nItemId]["nPageNum"])
		Sys_DialogItemFace(nItemId)
		Sys_DialogEnd()
	end
end

--[[
@param
nItemId:传入该Item的ID
sIndex:从上一层传入的索引ID，第一层为nil传入
nPage:当前第几页选项，0为第一页
nPageNum:每页显示的选项数，默认为5
--]]
function AIFunc_New(nItemId, sIndex, nPage, nPageNum)
	local bFirst = false				--是否第一次走入此函数，即是否在ItemProcess所调用的
	local nPage = nPage or 0			--当前页码，0为第一页
	local nPageNum = nPageNum or 8		--每页显示的选项数
	local nCount = 1					--当前应该显示第几项
	local nFlag = 0						--为0代表没出对白，1代表非指定对白，2代表指定对白
	local n = 1							--存储显示的Text表序号

	--第一层对白，支持条件选择
	if sIndex == nil then
		sIndex = "1"
		bFirst = true --第一层，因为执行完后要回调到Item模板中，所以不能出现对话框结束的语句
	else
		if string.find(sIndex, "-") == nil then
			nFlag = 0
		else
			nFlag = 2
		end
		bFirst = false
	end

	--对Text进行处理，区分显示指定对白和非指定对白
	if nFlag == 2 then
		if tItem[nItemId]["Text"..sIndex] == nil or  tItem[nItemId]["Text"..sIndex] == {} then
			return
		end

		if tItem[nItemId]["ChkFunc"..sIndex] == nil or (type(tItem[nItemId]["ChkFunc"..sIndex]) == "function" and tItem[nItemId]["ChkFunc"..sIndex]() == true) then
			for _,v in ipairs(tItem[nItemId]["Text"..sIndex]) do
				if tItem[nItemId]["Text"..v] ~= nil and tItem[nItemId]["Text"..v] ~= "" then
					Sys_DialogText(tItem[nItemId]["Text"..v])
				end
			end
		else
			return
		end
	else
		for i = 1, 50 do
			if tItem[nItemId]["Text"..sIndex.."-"..i] ~= nil and tItem[nItemId]["Text"..sIndex.."-"..i] ~= "" then
				--功能对白出现检查函数
				if tItem[nItemId]["ChkFunc"..sIndex.."-"..i] == nil or (type(tItem[nItemId]["ChkFunc"..sIndex.."-"..i]) == "function" and tItem[nItemId]["ChkFunc"..sIndex.."-"..i]() == true) then

					--出对白,最多可以配置5条
					for _,v in ipairs(tItem[nItemId]["Text"..sIndex.."-"..i]) do
						if tItem[nItemId]["Text"..v] ~= nil and tItem[nItemId]["Text"..v] ~= "" then
							Sys_DialogText(tItem[nItemId]["Text"..v])
						end
					end

					--对白已出，不再检测表中值
					nFlag = 1
					n = i
					break
				end
			else
				break
			end
		end
	end

	--对传入的sIndex进行处理，使指定和非指定对白一致，方便下面的处理
	if nFlag == 2 then
		n = string.sub(sIndex, string.find(sIndex,"-") + 1)
		sIndex = string.sub(sIndex, 1, string.find(sIndex,"-") - 1)
	elseif nFlag == 0 then --没有可以显示的对白
		if bFirst then --第一次进入则显示默认对白否则直接中断
			Sys_DialogText(tItem[nItemId]["Text"])
			return
		else
			return
		end
	end

	--ѡ����ʾ����
	if nFlag ~= 0 then
	--ѡ����ڼ��
		if type(tItem[nItemId]["tOption"..sIndex.."-"..n]) == "table" and tItem[nItemId]["tOption"..sIndex.."-"..n] ~= {} then
			for _, v in ipairs(tItem[nItemId]["tOption"..sIndex.."-"..n]) do --����ѡ���

				if nCount <= nPageNum and ((_ > nPage * nPageNum and nPage == 0) or (_ > nPage * nPageNum - (2 * nPage - 1) and nPage >= 1)) then --δ����ÿҳѡ����
					if tItem[nItemId]["Option"..v] ~= nil and tItem[nItemId]["Option"..v] ~= "" then --���ڸ�ѡ��
						if tItem[nItemId]["OptionChkFunc"..v] == nil or (type(tItem[nItemId]["OptionChkFunc"..v]) == "function" and tItem[nItemId]["OptionChkFunc"..v]()) then --ChkFunc�ж�ͨ��
							local sTemp = ""
							local sFunc = ""
							if tItem[nItemId]["OptionPoint"..v] ~= nil and tItem[nItemId]["OptionPoint"..v] ~= "" then
								sTemp = tItem[nItemId]["OptionPoint"..v]
							else
								sTemp = nil
							end

							if tItem[nItemId]["OptionFunc"..v] ~= nil and tItem[nItemId]["OptionFunc"..v] ~= "" and type(tItem[nItemId]["OptionFunc"..v]) == "string" then
								sFunc = "</F>"..tItem[nItemId]["OptionFunc"..v]
							elseif sTemp ~= nil then
								sFunc = "</F>LinkItemGossipFunc_New</N>"..nItemId.."</S>"..sTemp.."</N>0</N>"..nPageNum
							else
								sFunc = "</F>NULL"
							end

							Sys_DialogOption(tItem[nItemId]["Option"..v], sFunc)
							nCount = nCount + 1
							--if nCount < nPageNum then
							--	nCount = nCount + 1
							--end

							if nCount == nPageNum and #tItem[nItemId]["tOption"..sIndex.."-"..n] > nPageNum and nPage == 0  then
								local sTemp = sIndex.."-"..n
								Sys_DialogOption(tLuaRes[10006], "</F>LinkItemGossipFunc_New</N>"..nItemId.."</S>"..sTemp.."</N>"..(nPage + 1).."</N>"..nPageNum)
								break
							elseif nCount == nPageNum - 1 and #tItem[nItemId]["tOption"..sIndex.."-"..n] > nPageNum * (nPage + 1) - 2 * nPage and nPage > 0 then
								local sTemp = sIndex.."-"..n
								Sys_DialogOption(tLuaRes[10006], "</F>LinkItemGossipFunc_New</N>"..nItemId.."</S>"..sTemp.."</N>"..(nPage + 1).."</N>"..nPageNum)
								break
							end
						end

					end
				--elseif nCount ==  nPageNum and #tItem[nItemId]["tOption"..sIndex.."-"..n] > nPageNum then
				--	local sTemp = sIndex.."-"..n
				--	Sys_DialogOption("��һҳ", "</F>LinkItemGossipFunc_New</N>"..nItemId.."</S>"..sTemp.."</N>"..(nPage + 1).."</N>"..nPageNum)
				--	break
				end
			end

			if nPage > 0 then
				local sTemp = sIndex.."-"..n
				Sys_DialogOption(tLuaRes[10005], "</F>LinkItemGossipFunc_New</N>"..nItemId.."</S>"..sTemp.."</N>"..(nPage - 1).."</N>"..nPageNum)
			end
		end
	end

	--�󲿴��������Ƿ�ItemProcess����ĺ���
	if not bFirst then
		--Sys_SystemBroadcast("nCount is "..nCount)
		if nCount == 1 then
			Sys_DialogOption(tItem[nItemId]["Option"] or tLuaRes[10002], "</F>NULL")
		end
		Sys_DialogItemFace(nItemId)
		Sys_DialogEnd()
	end
end

function LinkItemGossipFunc_New(nItemId, sIndex, nPage, nPageNum)
	return AIFunc_New(nItemId, sIndex, nPage, nPageNum)
end

--例：
--tItem[2021226] = tItem[2021226] or {}
--如果要用对话框来做就不要下面这个配置
--tItem[2021226]["Function"] = function (nItemId, sItemName)
--具体逻辑
--end

--下面是物品对话配置的简单范例
--tItem[721779]["Text1-1"] = {111}
--tItem[721779]["Text111"] = "ChkFunc1为false，这个肯定不显示。"
--tItem[721779]["ChkFunc1-1"] = function () return false end
--tItem[721779]["tOption1-1"] = {1, 22, 212}

--tItem[721779]["Text1-2"] = {121}
--tItem[721779]["Text121"] = "ChkFunc2为true，显示对白2，之后的不再检测。"

--tItem[721779]["ChkFunc1-2"] = function () return true end
--tItem[721779]["tOption1-2"] = {1, 2, 212}

--tItem[721779]["Text1-3"] = {131}
--tItem[721779]["Text131"] = "ChkFunc3为true，但已出对白2，此对白不会检测了。"

--tItem[721779]["ChkFunc1-3"] = function () return false end
--tItem[721779]["tOption1-3"] = {} --为空或nil则出默认Option

--tItem[721779]["Text2-1"] = "ChkFunc1为false，这个肯定不显示。"
--tItem[721779]["ChkFunc2-1"] = function () return false end
--tItem[721779]["tOption2-1"] = {1, 22, 212}

--tItem[721779]["Text2-2"] = "Text2-2"
--tItem[721779]["ChkFunc2-2"] = function () return true end
--tItem[721779]["tOption2-2"] = {1, 2, 212}

--tItem[721779]["Text2-3"] = "ChkFunc3为true，但已出对白2，此对白不会检测了。"
--tItem[721779]["ChkFunc2-3"] = function () return true end
--tItem[721779]["tOption2-3"] = {} --为空或nil则出默认Option


--tItem[721779]["Option1"] = "Option1"
--tItem[721779]["OptionChkFunc1"] = function () return true end --接函数名，为nil或函数返回true时显示Option1
--tItem[721779]["OptionFunc1"] = "" --可自定义所接函数
--tItem[721779]["OptionPoint1"] = "1" --可自定义所接对白，需要循环判断ChkFunc来出相应Text

--tItem[721779]["Option2"] = "Option2"
--tItem[721779]["OptionChkFunc2"] = nil
--tItem[721779]["OptionFunc2"] = "" --可自定义所接函数，为""或nil时跳过
--tItem[721779]["OptionPoint2"] = "2-1" --可自定义所接对白，有“-”号则直接索引到指定Text，不再循环查找对白

--tItem[721779]["Option212"] = "Option212"
--tItem[721779]["OptionChkFunc212"] = nil
--tItem[721779]["OptionFunc212"] = "" --可自定义所接函数，为""或nil时跳过
--tItem[721779]["OptionPoint212"] = "" --为nil或""则相当于接空
