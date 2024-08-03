----------------------------------------------------------------------------
--Name:		[征服][基础数据]NPC闲聊表.lua
--Purpose:	NPC闲聊表
--Creator: 	郑江文
--Created:	2014/08/28
----------------------------------------------------------------------------

--NPC闲聊表,支持多种闲聊提示（普通任务NPC，闲聊NPC）
--tNpcGossip[NPCID]["Text"]={"闲聊1","闲聊2",...}
--tNpcGossip[NPCID]["Option"]={"闲聊选项1","闲聊选项2",...}
--tNpcGossip[NPCID]["Function"]=功能NPC接的函数
tNpcGossip={}

--[[
NPC闲聊表,支持多种闲聊提示（普通任务NPC，闲聊NPC）
tNpcGossip[NPCID]["Text"]							--闲聊内容
tNpcGossip[NPCID]["Option"]							--闲聊选项
tNpcGossip[NPCID]["Function"]						--功能NPC接的函数
tNpcGossip[nNpcId]["OptionHidden"]					--为1则不显示tNpcGossip[nNpcId]["Option"]或默认的“告辞”选项，否则为显示

--非任务NPC功能模块配置说明
tNpcGossip[44622]["Text1"] = "ChkFunc1为false，这个肯定不显示。"
tNpcGossip[44622]["ChkFunc1"] = function () return false end
tNpcGossip[44622]["Text2"] = "ChkFunc2为true，显示对白2，之后的不再检测。"
tNpcGossip[44622]["ChkFunc2"] = function () return true end
tNpcGossip[44622]["Text3"] = "ChkFunc3为true，但已出对白2，此对白不会检测了。"
tNpcGossip[44622]["ChkFunc3"] = function () return true end

tNpcGossip[44622]["Option1"] = "Option1"
tNpcGossip[44622]["OptionChkFunc1"] = function () return true end --接函数名，为nil或函数返回true时显示Option1
tNpcGossip[44622]["OptionFunc1"] = "SysFunc_TheFirstPetBattle_SysMsg" --可自定义所接函数
tNpcGossip[44622]["Option2"] = "Option2"
tNpcGossip[44622]["OptionChkFunc2"] = nil
tNpcGossip[44622]["OptionFunc2"] = "" --可自定义所接函数，为""或nil时跳过

--第二层
--tNpcGossip[44622]["Text"..sIndex..j] --此为除第一层外的每层数据构建，sIndex为上一层传入的选项值，j为这一层的并行配置，会逐一检测
tNpcGossip[44622]["Text21"] = "ChkFunc21为false，这个肯定不显示。"
tNpcGossip[44622]["ChkFunc21"] = function () return false end
tNpcGossip[44622]["Text22"] = "ChkFunc22为true，显示对白22，之后的不再检测。"
tNpcGossip[44622]["ChkFunc22"] = function () return true end
tNpcGossip[44622]["Text23"] = "ChkFunc23为true，但已出对白22，此对白不会检测了。"
tNpcGossip[44622]["ChkFunc23"] = function () return true end

tNpcGossip[44622]["Option21"] = "Option21"
tNpcGossip[44622]["OptionChkFunc21"] = function () return true end --接函数名，为nil或函数返回true时显示Option1
tNpcGossip[44622]["Option22"] = "Option22"
tNpcGossip[44622]["OptionChkFunc22"] = nil
tNpcGossip[44622]["OptionFunc22"] = "" --可自定义所接函数，为""或nil时跳过

--第三层
--tNpcGossip[44622]["Text"..sIndex..j] --此为除第一层外的每层数据构建，sIndex为上一层传入的选项值，j为这一层的并行配置，会逐一检测
tNpcGossip[44622]["Text211"] = "ChkFunc211为false，这个肯定不显示。"
tNpcGossip[44622]["ChkFunc211"] = function () return false end
tNpcGossip[44622]["Text212"] = "ChkFunc212为true，显示对白212，之后的不再检测。"
tNpcGossip[44622]["ChkFunc212"] = function () return true end
tNpcGossip[44622]["Text213"] = "ChkFunc213为true，但已出对白212，此对白不会检测了。"
tNpcGossip[44622]["ChkFunc213"] = function () return true end

tNpcGossip[44622]["Option211"] = "Option211"
tNpcGossip[44622]["OptionChkFunc211"] = function () return true end --接函数名，为nil或函数返回true时显示Option1
tNpcGossip[44622]["OptionFunc211"] = "SysFunc_TheFirstPetBattle_SysMsg" --可自定义所接函数
tNpcGossip[44622]["Option212"] = "Option212"
tNpcGossip[44622]["OptionChkFunc212"] = nil
tNpcGossip[44622]["OptionFunc212"] = "" --可自定义所接函数，为""或nil时跳过
--]]
