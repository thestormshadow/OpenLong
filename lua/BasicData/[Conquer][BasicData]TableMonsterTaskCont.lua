----------------------------------------------------------------------------
--Name:		[征服][基础数据]Monster任务关联表.lua
--Purpose:	Monster任务关联表
--Creator: 	郑江文
--Created:	2014/12/10
----------------------------------------------------------------------------

--怪物关联表
--tMonster[怪物ID]={任务ID,...}
tMonster={}
--[[
tMonster[怪物ID]["tFunction"] 							--怪物死亡调用的函数

--]]

--例：
--tMonster[4919] = tMonster[4919] or {}
--tMonster[4919]["tFunction"] = tMonster[4919]["tFunction"] or {}
--table.insert(tMonster[4919]["tFunction"],Christmas_2010_Monster_JWMH)