----------------------------------------------------------------------------
--Name:		[征服][基础数据]程序接口常量.lua
--Purpose:	程序接口常量
--Creator: 	郑江文
--Created:	2014/09/01
----------------------------------------------------------------------------
G_PLAYER_BEGIN 						=	1000
G_PLAYER_ID 						=	1001	--当前玩家ID				支持操作:	get
G_PLAYER_Name 						=	1002	--玩家名字         				get
G_PLAYER_LookFace					=	1003	--玩家头像编号						get set
G_PLAYER_Hair 						=	1004	--玩家发型编号						get
G_PLAYER_Profession					=	1005	--玩家的职业						get set
G_PLAYER_Level						=	1006	--玩家等级							get		 add
G_PLAYER_MapID						=	1007	--玩家所在地图ID  					get
G_PLAYER_PosX						=	1008	--玩家所在地图X坐标  				get
G_PLAYER_PosY						=	1009	--玩家所在地图Y坐标				get
G_PLAYER_Virtue						=	1010	--玩家功德值						get
G_PLAYER_Meto						=	1011	--玩家转世							get
G_PLAYER_Sex						=	1012	--玩家性别							get
G_PLAYER_TeamMemberNum				=	1013	--玩家队伍成员数量，没有队伍为0	get
G_PLAYER_HomeID						=	1014	--玩家房子id						get
G_PLAYER_AccountId					=	1015	--玩家账号id						get
G_PLAYER_BattleEffect				=	1016	--玩家战斗力						get
G_PLAYER_MateName					=	1017	--玩家伴侣名称						get
G_PLAYER_RidingPoints				=	1018	--玩家骑宠积分						get
G_PLAYER_Life						=	1019	--玩家生命值						get		 add
G_PLAYER_MaxLife					=	1020	--玩家最大生命值					get
G_PLAYER_Mana						=	1021	--玩家法力值						get		 add
G_PLAYER_MaxMana					=	1022	--玩家最大法力值					get
G_PLAYER_Mentor						=	1023	--玩家点化机会						get		 add
G_PLAYER_Transfrom					=	1024	--变身ID							get	set
G_PLAYER_Money						=	1025	--玩家游戏币						get		 add
G_PLAYER_MoneyTrial					=	1026	--试炼窟天石操作							 add
G_PLAYER_EMoney						=	1027	--玩家天石							get		 add
G_PLAYER_EMoneyMono					=	1028	--玩家赠点							get		 add
G_PLAYER_Exp						=	1029	--玩家经验(add操作不增加贡献)		get		 add
G_PLAYER_ExpContribute				=	1030	--玩家经验(add操作增加贡献)				 add
G_PLAYER_PK							=	1031	--玩家PK值							get		 add
G_PLAYER_Strength					=	1032	--玩家力量值						get		 add
G_PLAYER_Speed						=	1033	--玩家灵巧值		 				get		 add
G_PLAYER_Health						=	1034	--玩家体力值						get		 add
G_PLAYER_Soul						=	1035	--玩家精神值						get		 add
G_PLAYER_SynRank					=	1036	--玩家帮派里排名					get
G_PLAYER_Iterator					=	1037	--用于在任务系统中叠代			get	set	 add
G_PLAYER_Crime						=	1038	--犯罪时间							get	set
G_PLAYER_GameCard					=	1039	--cq_card记录数量					get
G_PLAYER_GameCard2					=	1040	--cq_card2记录数量					get
G_PLAYER_XP							=	1041	--当前XP值							get	set	 add
G_PLAYER_EP							=	1042	--体力值							get	set	 add
G_PLAYER_AddPoint					=	1043	--玩家属性点						get	set	 add
G_PLAYER_ClientVersion				=	1044	--客户的版本						get
G_PLAYER_Peerage					=	1045	--玩家爵位							get
G_PLAYER_Businness					=	1046	--玩家商人状态						get	set
G_PLAYER_VIP						=	1047	--玩家VIP等级						get
G_PLAYER_VIPValue					=	1048	--玩家VIP值							get	set	 add
G_PLAYER_StorageMoney				=	1049	--玩家存储的钱						get		 add
G_PLAYER_FirstPro					=	1050	--职业								get
G_PLAYER_OldPro						=	1051	--前世职业							get
G_PLAYER_AddMount					=	1052	--坐骑移动力								 add
G_PLAYER_Cultivation				=	1053	--修行值							get		 add
G_PLAYER_PKProtocol					=	1054	--PK模式							get	set
G_PLAYER_StrengthValue				=	1055	--气力值							get		 add
G_PLAYER_SynID						=	1056	--帮派ID							get
G_PLAYER_FamilyID					=	1057	--家族ID							get
G_PLAYER_LevupExp					=	1058	--升级到下一级所需经验			get
G_PLAYER_MateID						=	1059	--获得配偶ID						get
G_PLAYER_GodBless					=	1060	--获得祝福剩余时间				get		 add
G_PLAYER_TransferDragon20			=	1061	--玩家转服龙珠20的数量			get
G_PLAYER_TransferDragon100			=	1062	--玩家转服龙珠100的数量			get
G_PLAYER_BusinessManDays			=	1063	--商人转正剩余天数				get
G_PLAYER_GodBlessTime				=	1064	--玩家祝福账号的到期时间		get
G_PLAYER_ExpPercent					=	1065	--玩家百分比经验(add操作不增加贡献)	get		add
G_PLAYER_ExpPercentContribute		=	1066	--玩家百分比经验(add操作增加贡献)		add
G_PLAYER_CultureValue    			=	1067		--获取玩家修为值				get add
G_PLAYER_League_Point    			=	1068		--获取玩家黄金联赛积分 		get add
G_PLAYER_League_ID    				=	1069		--获取玩家联盟id 		get
G_PLAYER_Service_Value    			=	1070		--玩家战功 		get add
G_PLAYER_League_Official     		=	1071		--获取玩家官职 		get 
G_PLAYER_ExpTime					=	1072	--通过exptime绐玩家加经验	add
G_PLAYER_LAST 						=	1999
                                                  
G_NPC_BEGIN 						=	2000
G_NPC_ID 							=	2001	--取当前NPC的ID			=id=0   
G_NPC_Name 							=	2002	--取NPC的姓名            
G_NPC_OwnerID 						=	2003	--取NPC的OwnerID
G_NPC_OwnerType 					=	2004	--取NPC的OwnerType
G_NPC_Type 							=	2005	--取NPC的类型        
G_NPC_LookFace 						=	2006	--取NPC的Lookface   	                         
G_NPC_MapID 						=	2007	--取NPC所在的地图编号		           
G_NPC_PosX 							=	2008	--取NPC所在地图的X坐标		             
G_NPC_PosY 							=	2009	--取NPC所在地图的Y坐标	
G_NPC_Data0 						=	2010	--取NPC身上的Data0值		
G_NPC_Data1 						=	2011	--取NPC身上的Data1值
G_NPC_Data2 						=	2012	--取NPC身上的Data2值		   
G_NPC_Data3 						=	2013	--取NPC身上的Data3值
G_NPC_DataStr 						=	2014	--取NPC身上的DataStr值
G_NPC_MaxLife 						=	2015	--取NPC最大血值
G_NPC_Life 							=	2016	--取NPC当前血值
G_NPC_LAST 							=	2099
                                                 
                                                 
G_MAP_BEGIN 						=	2100	--2101，2103-2107 要用GetMapInt获取，2103，2108-2111要用GetMapIntEx获取
G_MAP_ID 							=	2101	--取当前地图的ID			=ID=0
G_MAP_Name 							=	2102	--取地图的名称
G_MAP_Type 							=	2103	--取地图的属性
G_MAP_OwnerID 						=	2104	--取地图主人ID
G_MAP_RebornMap						=	2105	--取地图重生地图ID
G_MAP_PosX 							=	2106	--取地图重生点X坐标
G_MAP_PosY 							=	2107	--取地图重生点Y坐标    
G_MAP_SYNID 						=	2108	--帮派地图
G_MAP_RES_LEV 						=	2109	--资源等级
G_MAP_DOC 							=	2110	--mapdoc
G_MAP_STATUS 						=	2111	--设置状态
G_MAP_RESET_STATUS					=	2112	--重置状态
G_MAP_PORTAL_X 						=	2113	--取地图入口点X坐标
G_MAP_PORTAL_Y   					=	2114	--取地图入口点Y坐标 
G_MAP_LAST  						=	2199
                                                 
                                                 
G_DYNA_GLOBAL_BEGIN 				=	2200
G_DYNA_GLOBAL_ID					=	2201	-- cq_dyna_global_data (与表顺序对应=id在lua中无接口)
G_DYNA_GLOBAL_DATA0					=	2202
G_DYNA_GLOBAL_DATA1					=	2203
G_DYNA_GLOBAL_DATA2					=	2204
G_DYNA_GLOBAL_DATA3					=	2205
G_DYNA_GLOBAL_DATA4					=	2206
G_DYNA_GLOBAL_DATA5					=	2207
G_DYNA_GLOBAL_DATASTR0				=	2208
G_DYNA_GLOBAL_DATASTR1				=	2209
G_DYNA_GLOBAL_DATASTR2				=	2210
G_DYNA_GLOBAL_DATASTR3				=	2211
G_DYNA_GLOBAL_DATASTR4				=	2212
G_DYNA_GLOBAL_DATASTR5				=	2213
G_DYNA_GLOBAL_TIME0					=	2214
G_DYNA_GLOBAL_TIME1					=	2215
G_DYNA_GLOBAL_TIME2					=	2216
G_DYNA_GLOBAL_TIME3					=	2217
G_DYNA_GLOBAL_TIME4					=	2218
G_DYNA_GLOBAL_TIME5					=	2219
G_DYNA_GLOBAL_LAST					=	2249
                                    			 
G_TASKDETAIL_BEGIN					=	2250
G_TASKDETAIL_ID						=	2251	-- task_detail (与表顺序对应			=id在lua中无接口)
G_TASKDETAIL_USER_ID				=	2252
G_TASKDETAIL_TASK_ID				=	2253
G_TASKDETAIL_COMPLETE_FLAG			=	2254
G_TASKDETAIL_NOTIFY_FLAG			=	2255
G_TASKDETAIL_DATA1					=	2256
G_TASKDETAIL_DATA2					=	2257
G_TASKDETAIL_DATA3					=	2258
G_TASKDETAIL_DATA4					=	2259
G_TASKDETAIL_DATA5					=	2260
G_TASKDETAIL_DATA6					=	2261
G_TASKDETAIL_DATA7					=	2262
G_TASKDETAIL_TASK_OVERTIME			=	2263
G_TASKDETAIL_TASK_OVERTIME_SEC		=	2264
G_TASKDETAIL_TYPE			 	 	=	2265
G_MAX_ACCUMULATE_TIMES	         	=	2266
G_TASKDETAIL_LAST			 	 	=	2299
                                                 	
                                                 	
G_ITEM_BEGIN			 				=	2300
G_ITEM_ID			 				=	2301	--物品的ID			=ID=0
G_ITEM_Type			 				=	2302	--物品类型
G_ITEM_OwnerID			 			=	2303	--OwnerID
G_ITEM_PlayerID			 			=	2304	--PlayerID
G_ITEM_Amount			 			=	2305	--当前耐久
G_ITEM_AmountLimit			 	 	=	2306	--耐久上限
G_ITEM_Ident						=	2307	--是否鉴定
G_ITEM_Position						=	2308	--位置
G_ITEM_Gem1							=	2309	--第一个洞
G_ITEM_Gem2							=	2310	--第二个洞
G_ITEM_Magic1						=	2311	--第一种魔法效果
G_ITEM_Magic2						=	2312	--第二种魔法效果
G_ITEM_Addition						=	2313	--追加数值			--这个就是G_ITEM_Magic3
G_ITEM_Data							=	2314	--物品Data字段值
G_ITEM_ReduceDmg					=	2315	--神佑
G_ITEM_AddLife						=	2316	--生命加持
G_ITEM_AntiMonster					=	2317	--克制怪物
G_ITEM_Name							=	2318	--名字
G_ITEM_Color						=	2319	--颜色
G_ITEM_Monopoly						=	2320	--赠品装备属性
G_ITEM_AddExp						=	2321	--追加经验
G_ITEM_DelTime						=	2322	--时效性物品删除时间
G_ITEM_SaveTime						=	2323	--物品有效时间
G_ITEM_AcutionDeposit				=	2324	--拍卖行保证金
G_ITEM_LAST							=	2399
                                                 
G_TEAM_BEGIN						=	2400
G_TEAM_ID							=	2401	--队伍的ID
G_TEAM_Amount						=	2402	--队伍人数
G_TEAM_MinLev						=	2403	--组员最低等级
G_TEAM_MaxLev						=	2404	--组员最高等级
G_TEAM_MinMoney						=	2405	--组员最少金币
G_TEAM_Mate							=	2406	--组员是否为伴侣
G_TEAM_Friend						=	2407	--组员是否为朋友
G_TEAM_Alive						=	2408	--组员是否都活着
G_TEAM_LAST							=	2499
                                                 
G_TRAP_BEGIN						=	2500	-- cq_trap表
G_TRAP_ID							=	2501	
G_TRAP_TYPE							=	2502
G_TRAP_LOOK							=	2503
G_TRAP_OWNER_ID						=	2504    --暂未实现
G_TRAP_MAPID						=	2505
G_TRAP_PosX							=	2506
G_TRAP_PosY							=	2507
G_TRAP_DATA							=	2508	--暂未实现
G_TRAP_BOUND_CX						=	2509	--暂未实现
G_TRAP_BOUND_CY						=	2510	--暂未实现
G_TRAP_LAST							=	2599
                                                 	
G_EMONEY_CARD_BEGIN					=	2600	-- 天石卡表
G_EMONEY_CARD1						=	2601	-- emoney_card
G_EMONEY_CARD2						=	2602	-- emoney_card2
G_EMONEY_CARD3						=	2603	-- emoney_card3
G_EMONEY_CARD4						=	2604	-- emoney_card4
G_EMONEY_CARD_END					=	2699		
                                                 	
G_ITEMTYPE_BEGIN					=	2700	--cq_itemtype属性
G_ITEMTYPE_Name						=	2701	--名字
G_ITEMTYPE_Profession				=	2702	--职业限制
G_ITEMTYPE_Skill					=	2703	--武器限制
G_ITEMTYPE_Level					=	2704	--等级限制
G_ITEMTYPE_Sex						=	2705	--性别限制
G_ITEMTYPE_Monopoly					=	2706	--独占性
G_ITEMTYPE_Mask						=	2707	--(贵重物品之类的属性)
G_ITEMTYPE_EmoneyPrice				=	2708	--天石价格
G_ITEMTYPE_EmoneyMonoPrice			=	2709	--赠点价格
G_ITEMTYPE_SaveTime					=	2710	--保持时间
G_ITEMTYPE_TypeDesc					=	2711	--类型说明
G_ITEMTYPE_ItemDesc					=	2712	--物品说明
G_ITEMTYPE_AccumulateLimit			=	2713	--物品叠加数
G_ITEMTYPE_LAST						=	2799
                                                 
G_NPC_COUNT_BEGIN					=	2800
G_NPC_COUNT_ALL						=	2801	--取玩家地图上所有NPC的数量
G_NPC_COUNT_FURNITURE				=	2802	--取玩家地图上所有家具的数量
G_NPC_COUNT_NAME					=	2803	--取玩家地图上所有指定名字的NPC数量
G_NPC_COUNT_TYPE					=	2804	--取玩家地图上所有指定类型的NPC数量
G_NPC_COUNT_LAST					=	2899
                                                 
G_SYNDICATE_BEGIN					=	2800
G_SYNDICATE_ALLY_SYN0				=	2801	--同盟帮派1
G_SYNDICATE_ALLY_SYN1				=	2802	--同盟帮派2
G_SYNDICATE_ALLY_SYN2				=	2803	--同盟帮派3
G_SYNDICATE_ALLY_SYN3				=	2804	--同盟帮派4
G_SYNDICATE_ALLY_SYN4				=	2805	--同盟帮派5
G_SYNDICATE_ALLY_SYN5				=	2806	--同盟帮派6
G_SYNDICATE_ALLY_SYN6				=	2807	--同盟帮派7
G_SYNDICATE_ALLY_SYN7				=	2808	--同盟帮派8
G_SYNDICATE_ALLY_SYN8				=	2809	--同盟帮派9
G_SYNDICATE_ALLY_SYN9				=	2810	--同盟帮派10
G_SYNDICATE_ALLY_SYN10				=	2811	--同盟帮派11
G_SYNDICATE_ALLY_SYN11				=	2812	--同盟帮派12
G_SYNDICATE_ALLY_SYN12				=	2813	--同盟帮派13
G_SYNDICATE_ALLY_SYN13				=	2814	--同盟帮派14
G_SYNDICATE_ALLY_SYN14				=	2815	--同盟帮派15
G_SYNDICATE_ENEMY_SYN0				=	2816	--敌对帮派1
G_SYNDICATE_ENEMY_SYN1				=	2817	--敌对帮派2
G_SYNDICATE_ENEMY_SYN2				=	2818	--敌对帮派3
G_SYNDICATE_ENEMY_SYN3				=	2819	--敌对帮派4
G_SYNDICATE_ENEMY_SYN4				=	2820	--敌对帮派5
G_SYNDICATE_ENEMY_SYN5				=	2821	--敌对帮派6
G_SYNDICATE_ENEMY_SYN6				=	2822	--敌对帮派7
G_SYNDICATE_ENEMY_SYN7				=	2823	--敌对帮派8
G_SYNDICATE_ENEMY_SYN8				=	2824	--敌对帮派9
G_SYNDICATE_ENEMY_SYN9				=	2825	--敌对帮派10
G_SYNDICATE_ENEMY_SYN10				=	2826	--敌对帮派11
G_SYNDICATE_ENEMY_SYN11				=	2827	--敌对帮派12
G_SYNDICATE_ENEMY_SYN12				=	2828	--敌对帮派13
G_SYNDICATE_ENEMY_SYN13				=	2829	--敌对帮派14
G_SYNDICATE_ENEMY_SYN14				=	2830	--敌对帮派15
G_SYNDICATE_NAME					=	2831	--帮派名称
G_SYNDICATE_LEADER_NAME				=	2832	--帮派帮主名称
G_SYNDICATE_MONEY					=	2833	--帮派基金
G_SYNDICATE_MEMBER_AMOUNT			=	2834	--帮派成员数量
G_SYNDICATE_EMONEY					=	2835	--帮派天石
G_SYNDICATE_LEVEL					=	2837	--帮派等级

G_SYNDICATE_END						=	2899
                                                 	
G_SYN_MEMBER_ATTR_BEGIN				=	2900	
G_SYN_MEMBER_ATTR_RANK				=	2901	--帮派玩家的帮派内等级(指帮主，长老这种)
G_SYN_MEMBER_ATTR_PROFFER			=	2902	--帮派玩家的游戏币贡献值
G_SYN_MEMBER_ATTR_END				=	2999
                                                 
                                                 
G_GONGFU_ATTR_BEGIN				 	=	3000
G_GONGFU_ATTR_REALM				 	=	3001	--玩家的功力境界		get
G_GONGFU_ATTR_FREE_CULTIVATE_PARAM	=	3002	--玩家免费修炼参数		get	set	add
G_GONGFU_ATTR_GENUINEQI_LV		 	=	3003	--玩家真气等级			get	set	add
G_GONGFU_ATTR_END				 	=	3099	

G_MONSTER_BEGIN						=	3100	--怪物
G_MONSTER_ID						=	3101
G_MONSTER_Name						=	3102
G_MONSTER_Type						=	3103
G_MONSTER_MapID						=	3104
G_MONSTER_PosX						=	3105
G_MONSTER_PosY						=	3106
G_MONSTER_MaxLife					=	3107
G_MONSTER_Life						=	3108
G_MONSTER_Level						=	3109
G_MONSTER_LAST				 		=	3199	



----------------------------------------------------------征服里面常用到的一些上限
G_User_MaxLev = 140								--玩家等级上限
G_User_MaxEmoney = 999999999					--玩家天石上限
G_User_MaxEmoneyMono = 999999999				--玩家赠品天石上限
G_User_FreePractice = 1000000					--玩家免费修炼次数上限






