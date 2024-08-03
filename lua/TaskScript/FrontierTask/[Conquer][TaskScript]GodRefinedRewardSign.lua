------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]神品精炼版本奖励-签到礼包
--Purpose:	签到礼包
--Creator: 	吴文鑫
--Created:	2016/04/10
------------------------------------------------------------------------------------

-- 命名前缀
--GodRefinedRewardSign_
--12000348


-- 常量表
local tGodRefinedRewardSign_Constant = {}

	tGodRefinedRewardSign_Constant["7Days"] = "0 0 3 10080 1"
	--打开神魂、淬炼礼包
	tGodRefinedRewardSign_Constant["SoulRefine"] = "0,0,%d,1,12000348,2,%d,1"
	--打开新做礼包
	tGodRefinedRewardSign_Constant["OpenNewPack"] = "angelwing"


	tGodRefinedRewardSign_Constant["Level"] = 110
	tGodRefinedRewardSign_Constant["Metempsychosis"] = 1
	
	
local tGodRefinedRewardSign_SignGift = {}
--随机给奖励
--随机给国境任务完成卷
	tGodRefinedRewardSign_SignGift[1] = {}
	tGodRefinedRewardSign_SignGift[1]["ItemAttr"] = "0 0 3 2880 1"
	
	tGodRefinedRewardSign_SignGift[1]["Item_1"] = {}
	tGodRefinedRewardSign_SignGift[1]["Item_1"][1] = 3100019
	-- tGodRefinedRewardSign_SignGift[1]["Item_1"][2] = 3100020
	tGodRefinedRewardSign_SignGift[1]["Item_1"][2] = 3600022

--4星5天骑宠/服装外套（赠）激活
-- 188495	烈火柔情套装	0	0	红与黄的经典配色凸显了热情似火的性格，下身的精心设计又不失柔情浪漫，不论身在何处都会受到众人瞩目！	0
-- 189085	雀翎轻衫	0	0	穿上之后无拘无束，冥冥之中透着一股仙气。	0
-- 189145	真武圣铠	9	0	为跨服战旗争霸赛亚军帮派的帮主量身打造的圣铠，穿戴起来，极为威风，乃世间难得一见的极品。	0
-- 192125	梦幻仙缘套装【珍藏版】	0	0	这一件卓然不凡的传奇外套，会带来一丝梦幻中的仙缘，能让穿上它的人有如仙人临世、睥睨凡尘。	0
-- 192565	情人之泪	0	0	情人节服装,简洁大气,使用黑色的调调来展示庄严肃穆的爱情话题,旨在告诉心爱的情人“以后你的眼泪由我来流,你在我身边只需欢笑便可”,浪漫十足。	0
-- 192575	龙凤呈祥	0	0	经典的颜色搭配，精妙的福字图案，灵动的服装剪裁，处处散发着传统的韵味。	0
-- 192605	御风逍遥	0	0	柔软光滑的丝缎，精致优雅的剪裁，华丽尊贵的头饰，举手投足间无不显露出一种俏皮清新、飘然出尘的风情。	0
-- 192615	御龙九天	0	0	稀有的金色绸缎与上好乌甲精心打造而成的外套，肩上闪闪发光的特殊龙纹装饰更衬托出着装之人的卓然超群。	0
-- 193205	洋洋得意套装	0	0	羊年新春专属外套，所到之处立刻成为全城瞩目的焦点，拉风帅气，洋洋得意！	0
-- 193225	春风得意	9	0	大胆的用色、精细的裁剪、点睛的细节，都让这身衣裳更加完美。和煦的春风吹过，衣袂飘飘，灼灼其华，全身上上下下都散发着“称心如意”之感。	0
-- 193295	孔雀东南飞	0	0	外套设计的灵感来源于高贵的鸟中之王——孔雀。得体的剪裁完美地呈现了人体的曲线，神秘的孔雀图案藏匿于外套的各个角落，栩栩如生宛若随时要东南飞。	0
-- 193325	江山一统	0	0	江山壮阔多娇，美人倾城一笑，该外套十分适合闯荡江湖的少侠们——是谁说不能爱江山也爱美人，待那江山一统，便又是个绝美的千古传说。	0
-- 193555	喵基尼【魅力版】	0	0	喵基尼，由喵喵界最有名的时尚喵人喵奈儿亲手打造，穿上它，不管走到哪里都会引起万喵尖叫。此外套可以升级为夺目的喵基尼【璀璨版】。	0
-- 193695	蝶恋花	0	0	蝶舞翩跹，风过留香；名花解语，嫣然绽放。	0

-- 200412	土豪团长专属豪骑	0	0	震惊壕界的高大上豪骑，绝对彰显您的身份地位权势，骑上仅需1秒钟大家都想当您的好朋友！	0
-- 200421	傲世龙神外套【龙年绝版】	0	0	十二守护神兽之一，于壬辰之年首度现世。传说拥有十二守护神兽之人将可以获得异界的秘宝！	0	
-- 200449	筋斗云	0	0	白白的，软软的，摸起来很舒服。快骑上它，享受腾云驾雾的快感吧！只有内心纯洁的人才能坐上去哦！	0
-- 200471	蟠蛇战辕【蛇年绝版】	0	0	十二守护神兽之一，于癸巳之年首度现世。传说拥有十二守护神兽之人将可以获得异界的秘宝！	0
-- 200481	冰魄灵狮外套【十周年绝版】	0	0	冰魄之躯彰显着它的神秘、高贵和气度不凡，古朴的花纹镌刻在它健壮有力的身躯上，不负兽中之王的美誉。	0
-- 200495	马上有对象【马年绝版】	0	0	十二守护神兽之一，于甲午之年首度现世。传说拥有十二守护神兽之人将可以获得异界的秘宝！	0
-- 200499	萌肥圆小羊驼	0	0	一只脸上永远带着红晕的害羞系小羊驼，卖萌装无辜不在话下，负重驮东西也是杠杠滴，是居家旅行的必备神兽。	0
-- 200500	福来哥	0	0	2014年巴西世界杯吉祥物，一只身穿同巴西国旗颜色一致的T恤及短裤的犰狳，是“足球”同“生态”的结合，代表着将环保理念注入足球运动。	0
-- 200525	开泰宝羚【羊年绝版】	0	0	十二守护神兽之一，于乙未之年首度现世。传说拥有十二守护神兽之人将可以获得异界的秘宝！	0
-- 200543	喵呜将军【魅力版】	0	0	喵呜将军，以一技凌波喵步在喵喵界享誉盛名，最喜爱沐浴在阳光下……睡觉。此外套可以升级为鲜丽的喵呜将军【璀璨版】。	0
-- 200549	灵犀玉麒麟【世纪婚礼专用】	0	0	象征祥瑞的上古神兽，乃是上仙月老的坐骑。据说只有心有灵犀的情侣才能获得此神兽，故月老将其取之名曰：灵犀玉麒麟。	0

	tGodRefinedRewardSign_SignGift[2] = {}
	tGodRefinedRewardSign_SignGift[2]["ItemAttr"] = "0 0 3 7200 1 0 0 1"

	tGodRefinedRewardSign_SignGift[2]["Item_1"] = {}
	tGodRefinedRewardSign_SignGift[2]["Item_1"][1] = 188495
	tGodRefinedRewardSign_SignGift[2]["Item_1"][2] = 189085
	tGodRefinedRewardSign_SignGift[2]["Item_1"][3] = 192125
	tGodRefinedRewardSign_SignGift[2]["Item_1"][4] = 192605
	tGodRefinedRewardSign_SignGift[2]["Item_1"][5] = 193205
	tGodRefinedRewardSign_SignGift[2]["Item_1"][6] = 193225
	tGodRefinedRewardSign_SignGift[2]["Item_1"][7] = 193295
	tGodRefinedRewardSign_SignGift[2]["Item_1"][8] = 193325
	tGodRefinedRewardSign_SignGift[2]["Item_1"][9] = 193555
	tGodRefinedRewardSign_SignGift[2]["Item_1"][10] = 200495
	tGodRefinedRewardSign_SignGift[2]["Item_1"][11] = 200500
	tGodRefinedRewardSign_SignGift[2]["Item_1"][12] = 200543
	tGodRefinedRewardSign_SignGift[2]["Item_1"][13] = 200549

--随机给 3100022 七阶武器神魂礼包\3100023 七阶防具配饰神魂礼包
	tGodRefinedRewardSign_SignGift[3] = {}
	tGodRefinedRewardSign_SignGift[3]["ItemAttr"] = "0 0 3 10080 1"
	tGodRefinedRewardSign_SignGift[3]["Item_1"] = {}
	tGodRefinedRewardSign_SignGift[3]["Item_1"][1] = 3100022
	tGodRefinedRewardSign_SignGift[3]["Item_1"][2] = 3100023

	
--每日签到礼包
tGodRefinedRewardSign_SignGift[3100011] = {}
tGodRefinedRewardSign_SignGift[3100011]["ItemChanceSum"] = 10000
-- tGodRefinedRewardSign_SignGift[3100011]["EmoneyMonoLimit"] = 50
tGodRefinedRewardSign_SignGift[3100011]["Space"] = 1
tGodRefinedRewardSign_SignGift[3100011]["Metempsychosis"] = 2
tGodRefinedRewardSign_SignGift[3100011]["Level"] = 0

--2转以上
--300分钟经验或100点气力可选	20.00%
--设计意见改为满级给气力未满级给经验
--改为给1天时效的3008965	300分钟经验礼包
tGodRefinedRewardSign_SignGift[3100011][1] = {}
tGodRefinedRewardSign_SignGift[3100011][1]["ItemChanceSum"] = 10000

tGodRefinedRewardSign_SignGift[3100011][1][1] = {}
tGodRefinedRewardSign_SignGift[3100011][1][1]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][1]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100011][1][1]["Item_1"] = 3008965
tGodRefinedRewardSign_SignGift[3100011][1][1]["ItemAttr"] = "0 0 3 1440 1"
tGodRefinedRewardSign_SignGift[3100011][1][1]["Index"] = 1
tGodRefinedRewardSign_SignGift[3100011][1][1]["Log"] = "0,0,3100011,1,12000348,2,3008965,1"

--300点修行值			5.00%
tGodRefinedRewardSign_SignGift[3100011][1][2] = {}
tGodRefinedRewardSign_SignGift[3100011][1][2]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][2]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][1][2]["Cultivation"] = 300
tGodRefinedRewardSign_SignGift[3100011][1][2]["Index"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][2]["Log"] = "0,0,3100011,1,12000348,2,6,300"

--日常完成卷（时效）		15.00%
--3600023 战功显赫嘉奖包
tGodRefinedRewardSign_SignGift[3100011][1][3] = {}
tGodRefinedRewardSign_SignGift[3100011][1][3]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][3]["ItemChance"] = 1500
tGodRefinedRewardSign_SignGift[3100011][1][3]["Item_1"] = 3600023
tGodRefinedRewardSign_SignGift[3100011][1][3]["ItemAttr"] = "0 1"
tGodRefinedRewardSign_SignGift[3100011][1][3]["Index"] = 3
tGodRefinedRewardSign_SignGift[3100011][1][3]["Log"] = "0,0,3100011,1,12000348,2,3600023,1"

--日常重置卷（时效）		5.00%
--激活2天时效，赠，天道酬勤卷  ID = 3001407
tGodRefinedRewardSign_SignGift[3100011][1][4] = {}
tGodRefinedRewardSign_SignGift[3100011][1][4]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][4]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][1][4]["Item_1"] = 3001407
tGodRefinedRewardSign_SignGift[3100011][1][4]["ItemAttr"] = "0 0 3 2880 1"
tGodRefinedRewardSign_SignGift[3100011][1][4]["Index"] = 4
tGodRefinedRewardSign_SignGift[3100011][1][4]["Log"] = "0,0,3100011,1,12000348,2,3001407,1"

--国境任务完成卷（时效）		5.00%
--激活2天时效，新制作3个物品，打开直接完成新国境杀怪，新国境建设，新国境BOSS，3个任务并获得奖励。
tGodRefinedRewardSign_SignGift[3100011][1][5] = {}
tGodRefinedRewardSign_SignGift[3100011][1][5]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][5]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][1][5]["Other"] = 1
tGodRefinedRewardSign_SignGift[3100011][1][5]["Index"] = 5
tGodRefinedRewardSign_SignGift[3100011][1][5]["Log"] = "0,0,3100011,1,12000348,2,%d,1"

--矿洞冒险完成卷（时效）		15.00%
--激活2天时效，ID= 3008732 星月宝盒
tGodRefinedRewardSign_SignGift[3100011][1][6] = {}
tGodRefinedRewardSign_SignGift[3100011][1][6]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][6]["ItemChance"] = 1500
tGodRefinedRewardSign_SignGift[3100011][1][6]["Item_1"] = 3008732
tGodRefinedRewardSign_SignGift[3100011][1][6]["ItemAttr"] = "0 1"
tGodRefinedRewardSign_SignGift[3100011][1][6]["Index"] = 6
tGodRefinedRewardSign_SignGift[3100011][1][6]["Log"] = "0,0,3100011,1,12000348,2,3008732,1"

--阴之玉*1（时效）		10.00%
--激活2天时效，ID= 3008059 阴之玉
tGodRefinedRewardSign_SignGift[3100011][1][7] = {}
tGodRefinedRewardSign_SignGift[3100011][1][7]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][7]["ItemChance"] = 1000
tGodRefinedRewardSign_SignGift[3100011][1][7]["Item_1"] = 3008059
tGodRefinedRewardSign_SignGift[3100011][1][7]["ItemAttr"] = "0 0 3 10080 1"
tGodRefinedRewardSign_SignGift[3100011][1][7]["Index"] = 7
tGodRefinedRewardSign_SignGift[3100011][1][7]["Log"] = "0,0,3100011,1,12000348,2,3008059,1"

--永久护心丹*5			5.00%
--ID= 3002030 强效护心丹
tGodRefinedRewardSign_SignGift[3100011][1][8] = {}
tGodRefinedRewardSign_SignGift[3100011][1][8]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][8]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][1][8]["Item_1"] = 3002030
tGodRefinedRewardSign_SignGift[3100011][1][8]["ItemAttr"] = "0 5"
tGodRefinedRewardSign_SignGift[3100011][1][8]["Index"] = 8
tGodRefinedRewardSign_SignGift[3100011][1][8]["Log"] = "0,0,3100011,1,12000348,2,3002030,5"

--50点赠点			5.00%
--改为 3002559	经验保护丹*2 720828	记忆宝珠
tGodRefinedRewardSign_SignGift[3100011][1][9] = {}
tGodRefinedRewardSign_SignGift[3100011][1][9]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][9]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][1][9]["Item_1"] = {}
tGodRefinedRewardSign_SignGift[3100011][1][9]["Item_1"][1] = 3002559
tGodRefinedRewardSign_SignGift[3100011][1][9]["Item_1"][2] = 720828
tGodRefinedRewardSign_SignGift[3100011][1][9]["ItemAttr"] = {}
tGodRefinedRewardSign_SignGift[3100011][1][9]["ItemAttr"][1] = "0 2 3"
tGodRefinedRewardSign_SignGift[3100011][1][9]["ItemAttr"][2] = "0 0 3"
tGodRefinedRewardSign_SignGift[3100011][1][9]["Index"] = 9
tGodRefinedRewardSign_SignGift[3100011][1][9]["Log"] = "0,0,3100011,1,12000348,2,3002559[720828],2[1]"


--200点气力值			5.00%
tGodRefinedRewardSign_SignGift[3100011][1][10] = {}
tGodRefinedRewardSign_SignGift[3100011][1][10]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][10]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][1][10]["Strengthvalue"] = 200
tGodRefinedRewardSign_SignGift[3100011][1][10]["Index"] = 10
tGodRefinedRewardSign_SignGift[3100011][1][10]["Log"] = "0,0,3100011,1,12000348,2,12,200"

--通神丹*2			10.00%
--ID= 3003125 通神丹
tGodRefinedRewardSign_SignGift[3100011][1][11] = {}
tGodRefinedRewardSign_SignGift[3100011][1][11]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][1][11]["ItemChance"] = 1000
tGodRefinedRewardSign_SignGift[3100011][1][11]["Item_1"] = 3003125
tGodRefinedRewardSign_SignGift[3100011][1][11]["ItemAttr"] = "0 2 3"
tGodRefinedRewardSign_SignGift[3100011][1][11]["Index"] = 11
tGodRefinedRewardSign_SignGift[3100011][1][11]["Log"] = "0,0,3100011,1,12000348,2,3003125,2"

---------------------------2转以下
--300分钟经验或100点气力可选	20.00%
--设计意见改为满级给气力未满级给经验
--改为给1天时效的3008965	300分钟经验礼包
tGodRefinedRewardSign_SignGift[3100011][2] = {}
tGodRefinedRewardSign_SignGift[3100011][2]["ItemChanceSum"] = 10000

tGodRefinedRewardSign_SignGift[3100011][2][1] = {}
tGodRefinedRewardSign_SignGift[3100011][2][1]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][1]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100011][2][1]["Item_1"] = 3008965
tGodRefinedRewardSign_SignGift[3100011][2][1]["ItemAttr"] = "0 0 3 1440 1"
tGodRefinedRewardSign_SignGift[3100011][2][1]["Index"] = 1
tGodRefinedRewardSign_SignGift[3100011][2][1]["Log"] = "0,0,3100011,1,12000348,2,3008965,1"

--300点修行值			5.00%
tGodRefinedRewardSign_SignGift[3100011][2][2] = {}
tGodRefinedRewardSign_SignGift[3100011][2][2]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][2]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][2][2]["Cultivation"] = 300
tGodRefinedRewardSign_SignGift[3100011][2][2]["Index"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][2]["Log"] = "0,0,3100011,1,12000348,2,6,300"

--日常完成卷（时效）		15.00%
--2转以下改为	3007311	物资募集符
tGodRefinedRewardSign_SignGift[3100011][2][3] = {}
tGodRefinedRewardSign_SignGift[3100011][2][3]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][3]["ItemChance"] = 1500
tGodRefinedRewardSign_SignGift[3100011][2][3]["Item_1"] = 3007311
tGodRefinedRewardSign_SignGift[3100011][2][3]["ItemAttr"] = "0 1"
tGodRefinedRewardSign_SignGift[3100011][2][3]["Index"] = 3
tGodRefinedRewardSign_SignGift[3100011][2][3]["Log"] = "0,0,3100011,1,12000348,2,3007311,1"

--日常重置卷（时效）		5.00%
--激活2天时效，赠，天道酬勤卷  ID = 3001407
tGodRefinedRewardSign_SignGift[3100011][2][4] = {}
tGodRefinedRewardSign_SignGift[3100011][2][4]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][4]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][2][4]["Item_1"] = 3001407
tGodRefinedRewardSign_SignGift[3100011][2][4]["ItemAttr"] = "0 0 3 2880 1"
tGodRefinedRewardSign_SignGift[3100011][2][4]["Index"] = 4
tGodRefinedRewardSign_SignGift[3100011][2][4]["Log"] = "0,0,3100011,1,12000348,2,3001407,1"

--国境任务完成卷（时效）		5.00%
--激活2天时效，新制作3个物品，打开直接完成新国境杀怪，新国境建设，新国境BOSS，3个任务并获得奖励。
tGodRefinedRewardSign_SignGift[3100011][2][5] = {}
tGodRefinedRewardSign_SignGift[3100011][2][5]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][5]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][2][5]["Other"] = 1
tGodRefinedRewardSign_SignGift[3100011][2][5]["Index"] = 5
tGodRefinedRewardSign_SignGift[3100011][2][5]["Log"] = "0,0,3100011,1,12000348,2,%d,1"

--矿洞冒险完成卷（时效）		15.00%
--激活2天时效，ID= 3008732 星月宝盒
--2转以下改为	3008729	征战抚恤令
tGodRefinedRewardSign_SignGift[3100011][2][6] = {}
tGodRefinedRewardSign_SignGift[3100011][2][6]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][6]["ItemChance"] = 1500
tGodRefinedRewardSign_SignGift[3100011][2][6]["Item_1"] = 3008729
tGodRefinedRewardSign_SignGift[3100011][2][6]["ItemAttr"] = "0 1"
tGodRefinedRewardSign_SignGift[3100011][2][6]["Index"] = 6
tGodRefinedRewardSign_SignGift[3100011][2][6]["Log"] = "0,0,3100011,1,12000348,2,3008729,1"

--阴之玉*1（时效）		10.00%
--激活2天时效，ID= 3008059 阴之玉
tGodRefinedRewardSign_SignGift[3100011][2][7] = {}
tGodRefinedRewardSign_SignGift[3100011][2][7]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][7]["ItemChance"] = 1000
tGodRefinedRewardSign_SignGift[3100011][2][7]["Item_1"] = 3008059
tGodRefinedRewardSign_SignGift[3100011][2][7]["ItemAttr"] = "0 0 3 10080 1"
tGodRefinedRewardSign_SignGift[3100011][2][7]["Index"] = 7
tGodRefinedRewardSign_SignGift[3100011][2][7]["Log"] = "0,0,3100011,1,12000348,2,3008059,1"

--永久护心丹*5			5.00%
--ID= 3002030 强效护心丹
tGodRefinedRewardSign_SignGift[3100011][2][8] = {}
tGodRefinedRewardSign_SignGift[3100011][2][8]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][8]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][2][8]["Item_1"] = 3002030
tGodRefinedRewardSign_SignGift[3100011][2][8]["ItemAttr"] = "0 5"
tGodRefinedRewardSign_SignGift[3100011][2][8]["Index"] = 8
tGodRefinedRewardSign_SignGift[3100011][2][8]["Log"] = "0,0,3100011,1,12000348,2,3002030,5"

--50点赠点			5.00%
--改为 3002559	经验保护丹*2 720828	记忆宝珠
tGodRefinedRewardSign_SignGift[3100011][2][9] = {}
tGodRefinedRewardSign_SignGift[3100011][2][9]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][9]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][2][9]["Item_1"] = {}
tGodRefinedRewardSign_SignGift[3100011][2][9]["Item_1"][1] = 3002559
tGodRefinedRewardSign_SignGift[3100011][2][9]["Item_1"][2] = 720828
tGodRefinedRewardSign_SignGift[3100011][2][9]["ItemAttr"] = {}
tGodRefinedRewardSign_SignGift[3100011][2][9]["ItemAttr"][1] = "0 2 3"
tGodRefinedRewardSign_SignGift[3100011][2][9]["ItemAttr"][2] = "0 0 3"
tGodRefinedRewardSign_SignGift[3100011][2][9]["Index"] = 9
tGodRefinedRewardSign_SignGift[3100011][2][9]["Log"] = "0,0,3100011,1,12000348,2,3002559[720828],2[1]"


--200点气力值			5.00%
tGodRefinedRewardSign_SignGift[3100011][2][10] = {}
tGodRefinedRewardSign_SignGift[3100011][2][10]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][10]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100011][2][10]["Strengthvalue"] = 200
tGodRefinedRewardSign_SignGift[3100011][2][10]["Index"] = 10
tGodRefinedRewardSign_SignGift[3100011][2][10]["Log"] = "0,0,3100011,1,12000348,2,12,200"

--通神丹*2			10.00%
--ID= 3003125 通神丹
tGodRefinedRewardSign_SignGift[3100011][2][11] = {}
tGodRefinedRewardSign_SignGift[3100011][2][11]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100011][2][11]["ItemChance"] = 1000
tGodRefinedRewardSign_SignGift[3100011][2][11]["Item_1"] = 3003125
tGodRefinedRewardSign_SignGift[3100011][2][11]["ItemAttr"] = "0 2 3"
tGodRefinedRewardSign_SignGift[3100011][2][11]["Index"] = 11
tGodRefinedRewardSign_SignGift[3100011][2][11]["Log"] = "0,0,3100011,1,12000348,2,3003125,2"







--高级签到礼盒
tGodRefinedRewardSign_SignGift[3100012] = {}
tGodRefinedRewardSign_SignGift[3100012]["ItemChanceSum"] = 10000
tGodRefinedRewardSign_SignGift[3100012]["EmoneyMonoLimit"] = 200
tGodRefinedRewardSign_SignGift[3100012]["Space"] = 1

-- 200点赠点			8.00%			直接给
tGodRefinedRewardSign_SignGift[3100012][1] = {}
tGodRefinedRewardSign_SignGift[3100012][1]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][1]["ItemChance"] = 800
tGodRefinedRewardSign_SignGift[3100012][1]["EmoneyMono"] = 200
tGodRefinedRewardSign_SignGift[3100012][1]["Index"] = 1
tGodRefinedRewardSign_SignGift[3100012][1]["Log"] = "0,0,3100012,1,12000348,2,3,200"

-- 4星5天骑宠/服装外套（赠）激活	20.00%			5天时效激活
tGodRefinedRewardSign_SignGift[3100012][2] = {}
tGodRefinedRewardSign_SignGift[3100012][2]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][2]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100012][2]["Other"] = 2
tGodRefinedRewardSign_SignGift[3100012][2]["Index"] = 2
tGodRefinedRewardSign_SignGift[3100012][2]["Log"] = "0,0,3100012,1,12000348,2,%d,1"


-- 赤炼石+4（赠）			5.00%			ID= 730004 赤炼石
tGodRefinedRewardSign_SignGift[3100012][3] = {}
tGodRefinedRewardSign_SignGift[3100012][3]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][3]["ItemChance"] = 500
tGodRefinedRewardSign_SignGift[3100012][3]["Item_1"] = 730004
tGodRefinedRewardSign_SignGift[3100012][3]["ItemAttr"] = "0 0 3 2880 1"
tGodRefinedRewardSign_SignGift[3100012][3]["Index"] = 3
tGodRefinedRewardSign_SignGift[3100012][3]["Log"] = "0,0,3100012,1,12000348,2,730004,1"

-- 天机果*1 不可交易		4.00%			ID= 3001044 百炼天机果
tGodRefinedRewardSign_SignGift[3100012][4] = {}
tGodRefinedRewardSign_SignGift[3100012][4]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][4]["ItemChance"] = 400
tGodRefinedRewardSign_SignGift[3100012][4]["Item_1"] = 3001044
tGodRefinedRewardSign_SignGift[3100012][4]["ItemAttr"] = ""
tGodRefinedRewardSign_SignGift[3100012][4]["Index"] = 4
tGodRefinedRewardSign_SignGift[3100012][4]["Log"] = "0,0,3100012,1,12000348,2,3001044,1"

-- 7阶神魂*1 （赠）		6.00%			开启也必须是赠品，ID= 3008055 七阶武器神魂礼包/ID= 3008056 七阶防具配饰神魂礼包
tGodRefinedRewardSign_SignGift[3100012][5] = {}
tGodRefinedRewardSign_SignGift[3100012][5]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][5]["ItemChance"] = 600
tGodRefinedRewardSign_SignGift[3100012][5]["Other"] = 3
tGodRefinedRewardSign_SignGift[3100012][5]["Index"] = 5
tGodRefinedRewardSign_SignGift[3100012][5]["Log"] = "0,0,3100012,1,12000348,2,%d,1"

-- 1000点气力值			4.00%			直接给
tGodRefinedRewardSign_SignGift[3100012][6] = {}
tGodRefinedRewardSign_SignGift[3100012][6]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][6]["ItemChance"] = 400
tGodRefinedRewardSign_SignGift[3100012][6]["Strengthvalue"] = 1000
tGodRefinedRewardSign_SignGift[3100012][6]["Index"] = 6
tGodRefinedRewardSign_SignGift[3100012][6]["Log"] = "0,0,3100012,1,12000348,2,12,1000"


-- 秘制修炼丹*10			10.00%			ID= 3002926 秘制免费修炼丹
tGodRefinedRewardSign_SignGift[3100012][7] = {}
tGodRefinedRewardSign_SignGift[3100012][7]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][7]["ItemChance"] = 1000
tGodRefinedRewardSign_SignGift[3100012][7]["Item_1"] = 3002926
tGodRefinedRewardSign_SignGift[3100012][7]["ItemAttr"] = "0 10"
tGodRefinedRewardSign_SignGift[3100012][7]["Index"] = 7
tGodRefinedRewardSign_SignGift[3100012][7]["Log"] = "0,0,3100012,1,12000348,2,3002926,10"

-- 究级通神丹*2（赠）		20.00%			ID= 3003126 究极通神丹
tGodRefinedRewardSign_SignGift[3100012][8] = {}
tGodRefinedRewardSign_SignGift[3100012][8]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][8]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100012][8]["Item_1"] = 3003126
tGodRefinedRewardSign_SignGift[3100012][8]["ItemAttr"] = "0 2 3"
tGodRefinedRewardSign_SignGift[3100012][8]["Index"] = 8
tGodRefinedRewardSign_SignGift[3100012][8]["Log"] = "0,0,3100012,1,12000348,2,3003126,2"

-- 金炼石+3 1颗			8.00%			星殒石100点1颗+10点的2颗 ID问严振飞
tGodRefinedRewardSign_SignGift[3100012][9] = {}
tGodRefinedRewardSign_SignGift[3100012][9]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][9]["ItemChance"] = 800
tGodRefinedRewardSign_SignGift[3100012][9]["Item_1"] = {}
tGodRefinedRewardSign_SignGift[3100012][9]["Item_1"][1] = 3009000
tGodRefinedRewardSign_SignGift[3100012][9]["Item_1"][2] = 3009001
tGodRefinedRewardSign_SignGift[3100012][9]["ItemAttr"] = {}
tGodRefinedRewardSign_SignGift[3100012][9]["ItemAttr"][1] = "0 2 0 2880 1"
tGodRefinedRewardSign_SignGift[3100012][9]["ItemAttr"][2] = "0 0 0 2880 1"
tGodRefinedRewardSign_SignGift[3100012][9]["Index"] = 9
tGodRefinedRewardSign_SignGift[3100012][9]["Log"] = "0,0,3100012,1,12000348,2,3009000[3009001],2[1]"

-- 金炼石+2 2颗			15.00%			星殒石100点1颗
tGodRefinedRewardSign_SignGift[3100012][10] = {}
tGodRefinedRewardSign_SignGift[3100012][10]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100012][10]["ItemChance"] = 1500
tGodRefinedRewardSign_SignGift[3100012][10]["Item_1"] = 3009001
tGodRefinedRewardSign_SignGift[3100012][10]["ItemAttr"] = "0 0 0 2880 1"
tGodRefinedRewardSign_SignGift[3100012][10]["Index"] = 10
tGodRefinedRewardSign_SignGift[3100012][10]["Log"] = "0,0,3100012,1,12000348,2,3009001,1"


-- 3100013 2天满勤大礼包
tGodRefinedRewardSign_SignGift[3100013] = {}
tGodRefinedRewardSign_SignGift[3100013]["IsDay"] = 1
tGodRefinedRewardSign_SignGift[3100013]["Item_1"] = 3100012
tGodRefinedRewardSign_SignGift[3100013]["ItemAttr"] = "0 0 3 1440 1"
tGodRefinedRewardSign_SignGift[3100013]["Other"] = 2
tGodRefinedRewardSign_SignGift[3100013]["Log"] = "0,0,3100013,1,12000348,2,3100012[%d],1[1]"
tGodRefinedRewardSign_SignGift[3100013]["Space"] = 1
tGodRefinedRewardSign_SignGift[3100013]["Index"] = 1

-- 3100014 7天满勤大礼包
tGodRefinedRewardSign_SignGift[3100014] = {}
tGodRefinedRewardSign_SignGift[3100014]["IsDay"] = 1
tGodRefinedRewardSign_SignGift[3100014]["Item_1"] = 3100012
tGodRefinedRewardSign_SignGift[3100014]["ItemAttr"] = "0 0 3 1440 1"
tGodRefinedRewardSign_SignGift[3100014]["EmoneyMono"] = 200
tGodRefinedRewardSign_SignGift[3100014]["ItemChanceSum"] = 10000
tGodRefinedRewardSign_SignGift[3100014]["EmoneyMonoLimit"] = 200
tGodRefinedRewardSign_SignGift[3100014]["Space"] = 1

-- 4星5天骑宠或者服装外套*1（赠）激活	40%		5天时效激活
tGodRefinedRewardSign_SignGift[3100014][1] = {}
tGodRefinedRewardSign_SignGift[3100014][1]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100014][1]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100014][1]["Other"] = 2
tGodRefinedRewardSign_SignGift[3100014][1]["Index"] = 1
tGodRefinedRewardSign_SignGift[3100014][1]["Log"] = "0,0,3100014,1,12000348,2,3100012[3][%d],1[200][1]"

-- 50赠点				5%		直接给
--改为 3007286	豪华七星断续礼包
tGodRefinedRewardSign_SignGift[3100014][2] = {}
tGodRefinedRewardSign_SignGift[3100014][2]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100014][2]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100014][2]["Item_1"] = 3007286
tGodRefinedRewardSign_SignGift[3100014][2]["ItemAttr"] = ""
tGodRefinedRewardSign_SignGift[3100014][2]["Index"] = 2
tGodRefinedRewardSign_SignGift[3100014][2]["Log"] = "0,0,3100014,1,12000348,2,3100012[3][3007286],1[200][1]"


-- 秘制修炼丹*10				15%		ID= 3002926 秘制免费修炼丹
tGodRefinedRewardSign_SignGift[3100014][3] = {}
tGodRefinedRewardSign_SignGift[3100014][3]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100014][3]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100014][3]["Item_1"] = 3002926
tGodRefinedRewardSign_SignGift[3100014][3]["ItemAttr"] = "0 10"
tGodRefinedRewardSign_SignGift[3100014][3]["Index"] = 3
tGodRefinedRewardSign_SignGift[3100014][3]["Log"] = "0,0,3100014,1,12000348,2,3100012[3][3002926],1[200][1]"


-- 究级通神丹*2				40%		ID= 3003126 究极通神丹
tGodRefinedRewardSign_SignGift[3100014][4] = {}
tGodRefinedRewardSign_SignGift[3100014][4]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100014][4]["ItemChance"] = 4000
tGodRefinedRewardSign_SignGift[3100014][4]["Item_1"] = 3003126
tGodRefinedRewardSign_SignGift[3100014][4]["ItemAttr"] = "0 2 3"
tGodRefinedRewardSign_SignGift[3100014][4]["Index"] = 4
tGodRefinedRewardSign_SignGift[3100014][4]["Log"] = "0,0,3100014,1,12000348,2,3100012[3][3003126],1[200][1]"

-- 3100015 14天满勤大礼包
--3100046,'房屋设计图

tGodRefinedRewardSign_SignGift[3100015] = {}
tGodRefinedRewardSign_SignGift[3100015]["IsDay"] = 1
tGodRefinedRewardSign_SignGift[3100015]["Item_1"] = {}
tGodRefinedRewardSign_SignGift[3100015]["Item_1"][1] = 3100012
tGodRefinedRewardSign_SignGift[3100015]["Item_1"][2] = 730003
tGodRefinedRewardSign_SignGift[3100015]["Item_1"][3] = 3100046
tGodRefinedRewardSign_SignGift[3100015]["ItemAttr"] = {}
tGodRefinedRewardSign_SignGift[3100015]["ItemAttr"][1] = "0 0 3 1440 1"
tGodRefinedRewardSign_SignGift[3100015]["ItemAttr"][2] = "0 0 3 2880 1"
tGodRefinedRewardSign_SignGift[3100015]["ItemAttr"][3] = "0 7"
tGodRefinedRewardSign_SignGift[3100015]["ItemChanceSum"] = 10000
tGodRefinedRewardSign_SignGift[3100015]["Space"] = 3

  -- 赤炼石+1 *2 （赠）			60.00%		ID= 730001 赤炼石
  --改为 秘令随机*2 属性达到完美的玩家给500点气力值

tGodRefinedRewardSign_SignGift[3100015][1] = {}
tGodRefinedRewardSign_SignGift[3100015][1]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100015][1]["ItemChance"] = 6000
tGodRefinedRewardSign_SignGift[3100015][1]["Item_1"] = 3008175
tGodRefinedRewardSign_SignGift[3100015][1]["ItemAttr"] = "0 2"
tGodRefinedRewardSign_SignGift[3100015][1]["Strengthvalue_1"] = 500
--属性
tGodRefinedRewardSign_SignGift[3100015][1]["MaxAttr"] = 900
tGodRefinedRewardSign_SignGift[3100015][1]["Index"] = 1
tGodRefinedRewardSign_SignGift[3100015][1]["Log"] = "0,0,3100015,1,12000348,2,3100012[730003][3100046][3008175],1[1][7][2]"
tGodRefinedRewardSign_SignGift[3100015][1]["StrLog"] = "0,0,3100015,1,12000348,2,3100012[730003][3100046][12],1[1][7][500]"


  -- 赤炼石+2 *1（赠）			10.00%		ID= 730002 赤炼石
tGodRefinedRewardSign_SignGift[3100015][2] = {}
tGodRefinedRewardSign_SignGift[3100015][2]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100015][2]["ItemChance"] = 1000
tGodRefinedRewardSign_SignGift[3100015][2]["Item_1"] = 730002
tGodRefinedRewardSign_SignGift[3100015][2]["ItemAttr"] = "0 0 3 2880 1"
tGodRefinedRewardSign_SignGift[3100015][2]["Index"] = 2
tGodRefinedRewardSign_SignGift[3100015][2]["Log"] = "0,0,3100015,1,12000348,2,3100012[730003][3100046][730002],1[1][7][1]"

-- 赤炼石+1 *3（赠）			30.00%		ID= 730001 赤炼石
--改为500点气力值
tGodRefinedRewardSign_SignGift[3100015][3] = {}
tGodRefinedRewardSign_SignGift[3100015][3]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100015][3]["ItemChance"] = 3000
tGodRefinedRewardSign_SignGift[3100015][3]["Strengthvalue"] = 500
tGodRefinedRewardSign_SignGift[3100015][3]["Index"] = 3
tGodRefinedRewardSign_SignGift[3100015][3]["Log"] = "0,0,3100015,1,12000348,2,3100012[730003][3100046][12],1[1][7][500]"


-- 3100016 21天满勤大礼包
tGodRefinedRewardSign_SignGift[3100016] = {}
tGodRefinedRewardSign_SignGift[3100016]["IsDay"] = 1
tGodRefinedRewardSign_SignGift[3100016]["Item_1"] = {}
tGodRefinedRewardSign_SignGift[3100016]["Item_1"][1] = 3100012
tGodRefinedRewardSign_SignGift[3100016]["Item_1"][2] = 1088000
tGodRefinedRewardSign_SignGift[3100016]["ItemAttr"] = {}
tGodRefinedRewardSign_SignGift[3100016]["ItemAttr"][1] = "0 0 3 1440 1"
tGodRefinedRewardSign_SignGift[3100016]["ItemAttr"][2] = "0 0 3"
tGodRefinedRewardSign_SignGift[3100016]["Strengthvalue"] = 1000
tGodRefinedRewardSign_SignGift[3100016]["ItemChanceSum"] = 10000
tGodRefinedRewardSign_SignGift[3100016]["Space"] = 2


-- 7阶神魂*1 （赠）	70.00%	200			开启也必须是赠品，ID= 3008055 七阶武器神魂礼包/ID= 3008056 七阶防具配饰神魂礼包
tGodRefinedRewardSign_SignGift[3100016][1] = {}
tGodRefinedRewardSign_SignGift[3100016][1]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100016][1]["ItemChance"] = 7000
tGodRefinedRewardSign_SignGift[3100016][1]["Other"] = 3
tGodRefinedRewardSign_SignGift[3100016][1]["Index"] = 1
tGodRefinedRewardSign_SignGift[3100016][1]["Log"] = "0,0,3100016,1,12000348,2,3100012[1088000][12][%d],1[1][1000][1]"

-- 天机果*2 不可交易	10.00%	1020			ID= 3001044 百炼天机果
tGodRefinedRewardSign_SignGift[3100016][2] = {}
tGodRefinedRewardSign_SignGift[3100016][2]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100016][2]["ItemChance"] = 1000
tGodRefinedRewardSign_SignGift[3100016][2]["Item_1"] = 3001044
tGodRefinedRewardSign_SignGift[3100016][2]["ItemAttr"] = ""
tGodRefinedRewardSign_SignGift[3100016][2]["Index"] = 2
tGodRefinedRewardSign_SignGift[3100016][2]["Log"] = "0,0,3100016,1,12000348,2,3100012[1088000][12][3001044],1[1][1000][1]"

-- 天机果*1 不可交易	20.00%	510			ID= 3001044 百炼天机果
--改为	3001045	洗髓神露
tGodRefinedRewardSign_SignGift[3100016][3] = {}
tGodRefinedRewardSign_SignGift[3100016][3]["RandomItemChanceType"] = 2
tGodRefinedRewardSign_SignGift[3100016][3]["ItemChance"] = 2000
tGodRefinedRewardSign_SignGift[3100016][3]["Item_1"] = 3001045
tGodRefinedRewardSign_SignGift[3100016][3]["ItemAttr"] = ""
tGodRefinedRewardSign_SignGift[3100016][3]["Index"] = 3
tGodRefinedRewardSign_SignGift[3100016][3]["Log"] = "0,0,3100016,1,12000348,2,3100012[1088000][12][3001045],1[1][1000][1]"

-- 3100017 28天满勤大礼包
-- 高级签到礼包第五次开必得:精炼石+4 *2			(星殒石100点3颗+10点的6颗)*2
-- +高级签到子包

tGodRefinedRewardSign_SignGift[3100017] = {}
tGodRefinedRewardSign_SignGift[3100017]["IsDay"] = 1
tGodRefinedRewardSign_SignGift[3100017]["Item_1"] = {}
tGodRefinedRewardSign_SignGift[3100017]["Item_1"][1] = 3100012
tGodRefinedRewardSign_SignGift[3100017]["Item_1"][2] = 3009000
tGodRefinedRewardSign_SignGift[3100017]["Item_1"][3] = 3009001
tGodRefinedRewardSign_SignGift[3100017]["ItemAttr"] = {}
tGodRefinedRewardSign_SignGift[3100017]["ItemAttr"][1] = "0 0 3 1440 1"
tGodRefinedRewardSign_SignGift[3100017]["ItemAttr"][2] = "0 4 0 2880 1"
tGodRefinedRewardSign_SignGift[3100017]["ItemAttr"][3] = "0 2 0 2880 1"
tGodRefinedRewardSign_SignGift[3100017]["Space"] = 6
tGodRefinedRewardSign_SignGift[3100017]["Log"] = "0,0,3100017,1,12000348,2,3100012[3009000][3009001],1[4][2]"
tGodRefinedRewardSign_SignGift[3100017]["Index"] = 1

local tGodRefinedRewardSign_Strike = {}
tGodRefinedRewardSign_Strike["Space"] = 7
tGodRefinedRewardSign_Strike["Score"] = 300
tGodRefinedRewardSign_Strike["TaskId"] = 35007 
tGodRefinedRewardSign_Strike["Log"] = "0,0,3100019,1,12000348,2,0,0"

local tGodRefinedRewardSign_Supply = {}
tGodRefinedRewardSign_Supply["Space"] = 7
tGodRefinedRewardSign_Supply["Log"] = "0,0,3100020,1,12000348,2,0,0"

local tGodRefinedRewardSign_SupplyStc = {}
tGodRefinedRewardSign_SupplyStc["EventType"] = 139
tGodRefinedRewardSign_SupplyStc["DataType"] = 01
tGodRefinedRewardSign_SupplyStc["Date"] = 10

local tGodRefinedRewardSign_SupplyTaskId = {}
tGodRefinedRewardSign_SupplyTaskId["HandIn"] = 35014
tGodRefinedRewardSign_SupplyTaskId["AwardTime"] = 10

-- 新增常量
local tGodRefinedRewardSign_Cont = {}
tGodRefinedRewardSign_Cont["JZJSD"] = 728596
tGodRefinedRewardSign_Cont["Level140"] = 140
tGodRefinedRewardSign_Cont["DailySignGift"] = 3100011










---------------------------------------------礼包逻辑
--签到礼包通用逻辑
function GodRefinedRewardSign_OpenSignGift(nItemId)
	-- 判断赠品服务器
	if SpecialServer_ChkNoGiftServer() then
		DailyActive_UpgradeGift_Daily(nItemId)
		-- 新的签到奖励
		return
	end
	-- 判断背包空间
	if tGodRefinedRewardSign_SignGift[nItemId]["Space"] ~= nil then
		local nSpace = tGodRefinedRewardSign_SignGift[nItemId]["Space"]
		--鸡年年兽活动获得 稀有课本随机包
		if nItemId == 3100011 then
			if Sys_ChkFullTime(tItemOutputOfNien_Constant["ActivityTime"]) then
				nSpace = nSpace +1
			end
		end
		
		--活动期间额外获得狐仙灵玉赠
		if nItemId == 3100011 then
			local sFoxTime = FoxRidingAct_GetActivityTime()
			if Sys_ChkFullTime(sFoxTime) then
				nSpace = nSpace +1
			end
		end
		
		------190102[简体征服][活动脚本]雷神预热活动制作
		if nItemId == 3100011 then
			
			if Sys_ChkFullTime(tActivityTime["ThorWarmUp"]["ActivityTime"]) then
				 if User_JudgeLevelAndMetempsychosis(80,0) then
					nSpace = nSpace +1
				end
			end
		end
		
		-- 忍者特权月
		if nItemId == 3100011 then
			if Sys_ChkFullTime(tActivityTime["NanjaPrivilege"]["ActivityTime"]) then
				 if User_JudgeLevelAndMetempsychosis(80,0) then
					nSpace = nSpace +1
				end
			end
		end
		-- 190704[简体征服][活动脚本]2019七夕花魁赛
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["ValentinesDay2018"]["ActivityTime"]) then
				nSpace = nSpace +1
			
			end
		end
		-- 20190930[简体征服][活动脚本]万圣节活动
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["HalloweenReward"]["ActivityTime"]) then
				nSpace = nSpace +1
			end
		end
		-- 91015[简体征服][活动脚本]感恩节---活动三小鸡快跑部分
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["ThanksChickenTantivy"]["ActTime"]) then
				nSpace = nSpace + 1
			end
		end
		-- 191112[简体征服][活动脚本]全球圣诞元旦活动-放烟花（12.24-1.8）
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["GlobalChristmas"]["ActivityTime"]) then
				nSpace = nSpace + 1
				if User_JudgeLevelAndMetempsychosis(0,2,nUserId) then
					nSpace = nSpace + 1
				end
			end
		end
		-- 191202[英文征服][活动脚本]新年活跃福利活动（1.2-1.31）
		if nItemId == 3100011 then
			if Sys_ChkFullTime(tActivityTime["SpringActiveWelfare"]["ActivityTime"]) then
				nSpace = nSpace + 1
			end
		end
		
		-- [简体征服][活动脚本]2020全球中国年(1.17-2.2)
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["SpringFestival2020Nian"]["ActivityTime"]) then
				if SpringFestival2020Nian_ChkLevel() then
					nSpace = nSpace + 2
				end
			end
		end
		
		-- 200103[简体征服][活动脚本]全球情人节活动--跨服互动活动
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["ValentineDayAct_2019"]["ActTime"]) then
				if User_JudgeLevelAndMetempsychosis(80,0,nUserId) then
					nSpace = nSpace + 1
				end
			end
		end
		
		-- [简体征服][活动脚本]武汉加油2
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["AtivityForPray"]["ActivityTime2"]) then
				if User_JudgeLevelAndMetempsychosis(80,0) then
					nSpace = nSpace + 1
				end
			end
		end
		
		-- 200228[简体征服][活动脚本]全球愚人节活动气氛和藏宝图发放部分
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["AprilFoolsDay"]["ActivityTime"]) then
				if User_JudgeLevelAndMetempsychosis(80,0) then
					nSpace = nSpace + 1
				end
			end
		end
		-- 200609[简体征服][活动脚本]全球七夕花魁活动-惊喜事件
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["AprilFoolsDay"]["ActivityTime"]) then
				if User_JudgeLevelAndMetempsychosis(80,0) then
					nSpace = nSpace + 1
				end
			end
		end
		-- 200316[简体征服][活动脚本]全球周年庆征服品牌月活动
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["Anniversary2020"]["ActivityTime"]) then
				if User_JudgeLevelAndMetempsychosis(80,0) then
					nSpace = nSpace + 1
				end
			end
		end
		
		-- 200416[英文征服][活动脚本]5月熔炼炉回收（5.26-6.08）
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["SmeltingRecovery"]["ActivityTime"]) then
				if User_JudgeLevelAndMetempsychosis(15,2) then
					nSpace = nSpace + 1
				end
			end
		end
		-- 200506[英文征服][活动脚本]6月夏日祭-花火大会
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			if Sys_ChkFullTime(tActivityTime["HanabiTaiKai"]["ActivityTime"]) then
				nSpace = nSpace + 1
			end
		end
		if not User_CheckLeftSpace(nSpace) then
			User_TalkChannel2005(string.format(tGodRefinedRewardSign_Text["NoSpace"],nSpace))
			return
		end
	end
	
	-- 判断玩家赠品天石是否达到上限
	if tGodRefinedRewardSign_SignGift[nItemId]["EmoneyMonoLimit"] ~= nil then
		local nUserEmoney = Get_UserMonoEMoney()
		-- local nMaxEmoney = G_User_MaxEmoneyMono

		if nUserEmoney + tGodRefinedRewardSign_SignGift[nItemId]["EmoneyMonoLimit"] > G_User_MaxEmoneyMono then
			User_TalkChannel2005(tGodRefinedRewardSign_Text["EmoneyMonoLimit"])
			return
		end

	end
	
	if Item_ChkItem(nItemId) and Item_DelItem(nItemId) then
		local sLog = ""
		local nIndex
		local nRandom
		local nItemRandom
		local bBool = true
		
	----随机几率
		if tGodRefinedRewardSign_SignGift[nItemId]["ItemChanceSum"] ~= nil then
			local flat,tAward
			if tGodRefinedRewardSign_SignGift[nItemId]["Metempsychosis"] ~= nil then
				if User_JudgeLevelAndMetempsychosis(tGodRefinedRewardSign_SignGift[nItemId]["Level"],tGodRefinedRewardSign_SignGift[nItemId]["Metempsychosis"]) then
					flat,tAward = Probabil_RandomAward(tGodRefinedRewardSign_SignGift[nItemId],1)
				else
					flat,tAward = Probabil_RandomAward(tGodRefinedRewardSign_SignGift[nItemId],2)
				end
			else
				flat,tAward = Probabil_RandomAward(tGodRefinedRewardSign_SignGift,nItemId)
			end
		
			local nItem = tAward[1]["tAward"][1]["Item_1"]
			local nItemAttr = tAward[1]["tAward"][1]["ItemAttr"]
			nIndex = tAward[1]["tAward"][1]["Index"]
			sLog = tAward[1]["tAward"][1]["Log"]

			-- 给修行值
			if tAward[1]["tAward"][1]["Cultivation"] then
				User_AddCultivation(tAward[1]["tAward"][1]["Cultivation"])
			end
			
			-- 给气力值
			if tAward[1]["tAward"][1]["Strengthvalue"] then
				--如果是给经验
				if tAward[1]["tAward"][1]["Exp"] then
					local nLevel = Get_UserLevel()
					if nLevel < G_User_MaxLev then
						-- 未达到满级获得钟经验
						User_AddExpTime(tAward[1]["tAward"][1]["Exp"])
						sLog = tAward[1]["tAward"][1]["ExpLog"]
						nRandom = 1
					else
						User_AddStrengthValue(tAward[1]["tAward"][1]["Strengthvalue"])
						nRandom = 2
					end
				else
					User_AddStrengthValue(tAward[1]["tAward"][1]["Strengthvalue"])
				end
			end
			
			-- 给赠天石
			if tAward[1]["tAward"][1]["EmoneyMono"] then
				User_AddEMoneyMono(tAward[1]["tAward"][1]["EmoneyMono"])
			end
			
			--随机给物品
			if tAward[1]["tAward"][1]["Other"] then
				local nOther = tAward[1]["tAward"][1]["Other"]
				nRandom,nItemRandom = GodRefinedRewardSign_RandomGift(nOther)
				sLog = string.format(sLog,nItemRandom)
			end
			
			--是否需要判断满属性
			if tAward[1]["tAward"][1]["MaxAttr"] then
				local nAttr = GodRefinedRewardSign_GetAttr()
				if	nAttr >= tAward[1]["tAward"][1]["MaxAttr"] then
					bBool = false
					nRandom = 2
				else
					nRandom = 1
				end
			end
			
			if bBool then
				-- 获得物品
				if nItem ~= nil then
					if type(nItem) == "table" then
						for i = 1,#nItem do
							Item_AddNewItem(nItem[i],nItemAttr[i])
						end
					else
						Item_AddNewItem(nItem,nItemAttr)
					end
				end
			
			else
				User_AddStrengthValue(tAward[1]["tAward"][1]["Strengthvalue_1"])
				sLog = tAward[1]["tAward"][1]["StrLog"]
			end
			
			
		end
		
		--如果是N天签到礼包必给部分
		if tGodRefinedRewardSign_SignGift[nItemId]["IsDay"] ~= nil then
			local nItem = tGodRefinedRewardSign_SignGift[nItemId]["Item_1"]
			local nItemAttr = tGodRefinedRewardSign_SignGift[nItemId]["ItemAttr"]
			
			if type(nItem) == "table" then
				for i = 1,#nItem do
					Item_AddNewItem(nItem[i],nItemAttr[i])
				end
			else
				Item_AddNewItem(nItem,nItemAttr)
			end
			
			if tGodRefinedRewardSign_SignGift[nItemId]["Other"] then
				nRandom,nItemRandom = GodRefinedRewardSign_RandomGift(tGodRefinedRewardSign_SignGift[nItemId]["Other"])
				sLog = string.format(sLog,nItemRandom)
			end
			
			-- 给赠天石
			if tGodRefinedRewardSign_SignGift[nItemId]["EmoneyMono"] then
				User_AddEMoneyMono(tGodRefinedRewardSign_SignGift[nItemId]["EmoneyMono"])
			end
			
			--给气力
			if tGodRefinedRewardSign_SignGift[nItemId]["Strengthvalue"] then
				User_AddStrengthValue(tGodRefinedRewardSign_SignGift[nItemId]["Strengthvalue"])
			end
			
			if tGodRefinedRewardSign_SignGift[nItemId]["ItemChanceSum"] == nil then
				nIndex = tGodRefinedRewardSign_SignGift[nItemId]["Index"]
			end
		end
		
		
		-- 每日签到礼包给 鸡年年兽活动获得 稀有课本随机包,打开获得1个
		if nItemId == 3100011 then
			ItemOutputOfNien_AwardBookRandomPack(1)
		end

		--活动期间额外获得狐仙灵玉赠
		if nItemId == 3100011 then
			FoxRidingAct_QianDaoPag()
		end
		
		---190102[简体征服][活动脚本]雷神预热活动制作
		if nItemId == 3100011 then
			ThorWarmUp_GetExtraReward(5)
		end
		
		-- 忍者特权月
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			 WarriorPrivilege_Get(2)
		end
		-- 190704[简体征服][活动脚本]2019七夕花魁赛
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			 ValentinesDay2018_DayPackageUse()
		end
		
		-- 190130[简体征服][活动脚本]情人节时尚比拼
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			ValentineCompetition_SignPack()
		end
		-- 万圣节活动
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			HalloweenReward_DailyPack()
		end
			-- 191015[简体征服][活动脚本]感恩节---活动三小鸡快跑部分
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			ThanksChickenTantivy_DayPackageUse(1)
		end
		-- 全球圣诞元旦活动-放烟花
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			ShootOff_Fireworks_DailyPack(nItemId)
		end
		-- 新年活跃福利活动
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			SpringActiveWelfare_DailyPack()
		end
		
		-- [简体征服][活动脚本]2020全球中国年(1.17-2.2)
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			SpringFestival2020Nian_Sign()
		end
		
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			ValentineDayAct_2019_DayPackageUse()
		end
		
		-- [简体征服][活动脚本]武汉加油2
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			AtivityForPray_DailySignPack()
		end
		
		-- 200228[简体征服][活动脚本]全球愚人节活动气氛和藏宝图发放部分
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			AprilFoolsDayAtmosphere_DailyPack(1)
		end
		-- 200316[简体征服][活动脚本]全球周年庆征服品牌月活动
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			Anni17Map_DailyPack(1)
		end
		
		-- 200416[英文征服][活动脚本]5月熔炼炉回收（5.26-6.08）
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			SmeltingRecovery_Sign()
		end
		-- 200506[英文征服][活动脚本]6月夏日祭-花火大会
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			Hanabi_Sign()
		end
		-- 200609[简体征服][活动脚本]全球七夕花魁活动-惊喜事件
		if nItemId == tGodRefinedRewardSign_Cont["DailySignGift"] then
			ValentinesDaySurprise_DailyPack(9)
		end
		-- 打log
		Sys_SaveActionFestivalLog(sLog)
		-- 给提示
		local sContent = ""

		if tGodRefinedRewardSign_SignGift[nItemId]["Metempsychosis"] ~= nil then
			if User_JudgeLevelAndMetempsychosis(tGodRefinedRewardSign_SignGift[nItemId]["Level"],tGodRefinedRewardSign_SignGift[nItemId]["Metempsychosis"]) then
				sContent = tGodRefinedRewardSign_Text[nItemId][1][nIndex][nRandom] or tGodRefinedRewardSign_Text[nItemId][1][nIndex]
			else
				sContent = tGodRefinedRewardSign_Text[nItemId][2][nIndex][nRandom] or tGodRefinedRewardSign_Text[nItemId][2][nIndex]
			end
		else
			sContent = tGodRefinedRewardSign_Text[nItemId][nIndex][nRandom] or tGodRefinedRewardSign_Text[nItemId][nIndex]
		end
		
		
		User_TalkChannel2005(sContent)

	end
end

--随机给物品函数
function GodRefinedRewardSign_RandomGift(nOther)
	local nRandom = math.random(1,#tGodRefinedRewardSign_SignGift[nOther]["Item_1"])
	local nItemRandom = tGodRefinedRewardSign_SignGift[nOther]["Item_1"][nRandom]
	local nItemAttr = tGodRefinedRewardSign_SignGift[nOther]["ItemAttr"]
	Item_AddNewItem(nItemRandom,nItemAttr) 
	return nRandom,nItemRandom
end


---- 3100019 妙计锦囊·突袭
function GodRefinedRewardSign_OpenTrickTipsStrike(nItemId,nUserId)
	local nNowUserId = nUserId or Get_UserId()
--等级判断
	if not GodRefinedRewardSign_SatisfyCondition(nNowUserId) then
		User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["Level"],nNowUserId)
		return
	end
--调用FirstPerson的判断stc掩码
	if not FirstPerson_JudgeStc(nNowUserId) then
		User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["Complete"],nNowUserId)
		return
	end
	
	local nTaskId = tGodRefinedRewardSign_Strike["TaskId"]
	-- 获取积分
	local nScore = Get_TaskDetail(nTaskId,"6",nNowUserId)
	if nItemId ~= tGodRefinedRewardSign_Constant["ItemId"] then 
		if nScore >= tGodRefinedRewardSign_Strike["Score"] then
			User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["TaskComplete"],nNowUserId)
			return
		end
	end 
	-- 判断背包空间
	-- if not User_CheckLeftSpace(tGodRefinedRewardSign_Strike["Space"]) then
		-- User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["NoSpace"])
		-- return
	-- end
	
	if Item_ChkItem(nItemId,1,0,nNowUserId) and Item_DelItem(nItemId,1,0,nNowUserId) then
		--调用FirstPerson的任务完成函数
		FirstPerson_SetComplete(nNowUserId)
		User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["RewardItem"],nNowUserId)
		Sys_SaveActionTaskLog(string.format(tGodRefinedRewardSign_Strike["Log"],nItemId),nNowUserId)
	end
end

---- 3100020 妙计锦囊·补给
function GodRefinedRewardSign_OpenTrickTipsSupply(nItemId,nUserId)
	local nNowUserId = nUserId or Get_UserId()
	if not GodRefinedRewardSign_SatisfyCondition(nNowUserId) then
		User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["Level"],nNowUserId)
		return
	end

	local nEventType = tGodRefinedRewardSign_SupplyStc["EventType"]
	local nDataType = tGodRefinedRewardSign_SupplyStc["DataType"]
	Kuafu_Defense_ClearStc(nEventType,nDataType,nNowUserId)
	--已完成
	if Task_ChkStcValue(nEventType,nDataType,">=",tGodRefinedRewardSign_SupplyStc["Date"],nNowUserId) then
		User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["Complete"],nNowUserId)
		return
	end
	
	local nTaskId = tGodRefinedRewardSign_SupplyTaskId["HandIn"]
	--检测掩码是否存在
	if not Task_ChkTaskDetail(nTaskId) then
		return
	end
	
	local nTime = Get_TaskDetail(nTaskId,"6",nNowUserId)
	--任务条件达成
	if nItemId ~= tGodRefinedRewardSign_Constant["ItemId"] then 
		if (nTime >= tGodRefinedRewardSign_SupplyTaskId["AwardTime"]) then
			User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["TaskComplete"],nNowUserId)
			return
		end	
	end
	
	--背包空间不足
	-- if not User_CheckLeftSpace(tGodRefinedRewardSign_Supply["Space"]) then
		-- User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["NoSpace"])
		-- return
	-- end
	

	if Item_ChkItem(nItemId,1,0,nNowUserId) and Item_DelItem(nItemId,1,0,nNowUserId) then
		--满足条件，获得奖励
		Task_SetStatistic(nEventType,nDataType,tGodRefinedRewardSign_SupplyStc["Date"],1,nNowUserId)
		Task_SetStcTimestamp(nEventType,nDataType,0,nNowUserId)
		Kuafu_Defense_Award(0,"Normal",nNowUserId)
		User_TalkChannel2005(tGodRefinedRewardSign_Text[nItemId]["Awarditem"],nNowUserId)
		Sys_SaveActionTaskLog(string.format(tGodRefinedRewardSign_Supply["Log"],nItemId),nNowUserId)
	end
end

--是否满足等级条件
function GodRefinedRewardSign_SatisfyCondition(nUserId)
	local nLev = Get_UserLevel(nUserId)
	local nMete = Get_UserMetempsychosis(nUserId)
	local nLevLimit = tGodRefinedRewardSign_Constant["Level"]
	local nMeteLimit = tGodRefinedRewardSign_Constant["Metempsychosis"]
	
	if (nMete < nMeteLimit) or (nMete == nMeteLimit and nLev < nLevLimit) then
		return false
	else 
		return true
	end

end


-- 获取玩家现有属性点
function GodRefinedRewardSign_GetAttr()
	local nStrength = Get_UserStrength()
	local nSpeed = Get_UserSpeed()
	local nHealth = Get_UserHealth()
	local nSoul = Get_UserSoul()
	local nPoint = Get_UserAddPoint()
	
	return nStrength + nSpeed + nHealth + nSoul + nPoint
end





--神魂、淬炼礼包选择
function GodRefinedRewardSign_SouRefinelSel(nThisItemId,nAwardItemId,sOptionText)
	
	local sOptionText = tGodRefinedRewardSign_Text[nThisItemId][sOptionText]
	
	--重设对白文字
	tItem[nThisItemId]["Text411"] = string.format(tGodRefinedRewardSign_Text[nThisItemId]["Text411"],sOptionText)
	--重设选项函数
	tItem[nThisItemId]["OptionFunc411"] = "GodRefinedRewardSign_SoulRefineAward</N>"..nThisItemId.."</N>"..nAwardItemId
	
	LinkItemGossipFunc_New(nThisItemId,"4-1")
	
	
end

--确认选择获得物品
function GodRefinedRewardSign_SoulRefineAward(nThisItemId,nAwardItemId)
	
	if not Item_ChkItem(nThisItemId) then
		User_TalkChannel2005(tGodRefinedRewardSign_Text["NoThisItem"])
		return
	end
	
	if not Item_DelItem(nThisItemId) then
		return
	end
	
	if not Item_AddNewItem(nAwardItemId,tGodRefinedRewardSign_Constant["7Days"]) then
		return
	end
	
	local sLog = string.format(tGodRefinedRewardSign_Constant["SoulRefine"],nThisItemId,nAwardItemId)
	Sys_SaveActionFestivalLog(sLog)
	
	local sAwardText = string.format(tGodRefinedRewardSign_Text["ExchangeSucc"],Get_ItemtypeName(nAwardItemId))
	
	User_TalkChannel2005(sAwardText)
	User_EffectAdd("self",tGodRefinedRewardSign_Constant["OpenNewPack"])
end




----------------------------------------------物品配置
-- 每日签到礼包
tItem[3100011] = tItem[3100011] or {}
tItem[3100011]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenSignGift(nItemId)
end

-- 高级签到礼盒
tItem[3100012] = tItem[3100012] or {}
tItem[3100012]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenSignGift(nItemId)
end

-- 3100013, 2天满勤大礼包
tItem[3100013] = tItem[3100013] or {}
tItem[3100013]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenSignGift(nItemId)
end

-- 3100014, 7天满勤大礼包
tItem[3100014] = tItem[3100014] or {}
tItem[3100014]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenSignGift(nItemId)
end

-- 3100015, 14天满勤大礼包
tItem[3100015] = tItem[3100015] or {}
tItem[3100015]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenSignGift(nItemId)
end

-- 3100016, 21天满勤大礼包
tItem[3100016] = tItem[3100016] or {}
tItem[3100016]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenSignGift(nItemId)
end

-- 3100017, 28天满勤大礼包
tItem[3100017] = tItem[3100017] or {}
tItem[3100017]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenSignGift(nItemId)
end


-- 3100019 妙计锦囊·突袭
tItem[3100019] = tItem[3100019] or {}
tItem[3100019]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenTrickTipsStrike(nItemId)
end
-- 3100020 妙计锦囊·补给
tItem[3100020] = tItem[3100020] or {}
tItem[3100020]["Function"] = function(nItemId,sItemName)
	GodRefinedRewardSign_OpenTrickTipsSupply(nItemId)
end
-- 3100021 妙计锦囊·降伏
-- tItem[3100021] = tItem[3100021] or {}
-- tItem[3100021]["Function"] = function(nItemId,sItemName)
	-- GodRefinedRewardSign_OpenTrickTipsSubdue(nItemId)
-- end

-- 3100022 七阶武器神魂礼包
-- 3100023 七阶防具配饰神魂礼包

--七阶武器神魂礼包
tItem[3100022] = tItem[3100022] or {}

tItem[3100022]["Text1-1"] = {111}
tItem[3100022]["Text111"] = tGodRefinedRewardSign_Text[3100022]["Text111"]
tItem[3100022]["tOption1-1"] = {111,112,113,114,115,116,117,118}
tItem[3100022]["Option111"] = tGodRefinedRewardSign_Text[3100022]["Option111"]
tItem[3100022]["OptionFunc111"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800020</S>Option111"
tItem[3100022]["Option112"] = tGodRefinedRewardSign_Text[3100022]["Option112"]
tItem[3100022]["OptionFunc112"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800111</S>Option112"
tItem[3100022]["Option113"] = tGodRefinedRewardSign_Text[3100022]["Option113"]
tItem[3100022]["OptionFunc113"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800215</S>Option113"
tItem[3100022]["Option114"] = tGodRefinedRewardSign_Text[3100022]["Option114"]
tItem[3100022]["OptionFunc114"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800811</S>Option114"
tItem[3100022]["Option115"] = tGodRefinedRewardSign_Text[3100022]["Option115"]
tItem[3100022]["OptionFunc115"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800810</S>Option115"
tItem[3100022]["Option116"] = tGodRefinedRewardSign_Text[3100022]["Option116"]
tItem[3100022]["OptionFunc116"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800142</S>Option116"
tItem[3100022]["Option117"] = tGodRefinedRewardSign_Text[3100022]["Option117"]
tItem[3100022]["OptionPoint117"] = "2-1"
tItem[3100022]["Option118"] = tGodRefinedRewardSign_Text[3100022]["Option118"]

tItem[3100022]["Text2-1"] = {211}
tItem[3100022]["Text211"] = tGodRefinedRewardSign_Text[3100022]["Text211"]
tItem[3100022]["tOption2-1"] = {211,212,213,214,215,216,217,218}
tItem[3100022]["Option211"] = tGodRefinedRewardSign_Text[3100022]["Option211"]
tItem[3100022]["OptionFunc211"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800917</S>Option211"
tItem[3100022]["Option212"] = tGodRefinedRewardSign_Text[3100022]["Option212"]
tItem[3100022]["OptionFunc212"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800255</S>Option212"
tItem[3100022]["Option213"] = tGodRefinedRewardSign_Text[3100022]["Option213"]
tItem[3100022]["OptionFunc213"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800422</S>Option213"
tItem[3100022]["Option214"] = tGodRefinedRewardSign_Text[3100022]["Option214"]
tItem[3100022]["OptionFunc214"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800522</S>Option214"
tItem[3100022]["Option215"] = tGodRefinedRewardSign_Text[3100022]["Option215"]
tItem[3100022]["OptionFunc215"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800618</S>Option215"
tItem[3100022]["Option216"] = tGodRefinedRewardSign_Text[3100022]["Option216"]
tItem[3100022]["OptionFunc216"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>800725</S>Option216"
tItem[3100022]["Option217"] = tGodRefinedRewardSign_Text[3100022]["Option217"]
tItem[3100022]["OptionPoint217"] = "1-1"
tItem[3100022]["Option218"] = tGodRefinedRewardSign_Text[3100022]["Option218"]
tItem[3100022]["OptionPoint218"] = "3-1"

tItem[3100022]["Text3-1"] = {311}
tItem[3100022]["Text311"] = tGodRefinedRewardSign_Text[3100022]["Text311"]
tItem[3100022]["tOption3-1"] = {311,312,315,316,317,318,319,3110,3111,314}
tItem[3100022]["Option311"] = tGodRefinedRewardSign_Text[3100022]["Option311"]
tItem[3100022]["OptionFunc311"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>801004</S>Option311"
tItem[3100022]["Option312"] = tGodRefinedRewardSign_Text[3100022]["Option312"]
tItem[3100022]["OptionFunc312"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>801104</S>Option312"
tItem[3100022]["Option315"] = tGodRefinedRewardSign_Text[3100022]["Option315"]
tItem[3100022]["OptionFunc315"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>801216</S>Option315"
tItem[3100022]["Option316"] = tGodRefinedRewardSign_Text[3100022]["Option316"]
tItem[3100022]["OptionFunc316"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>801218</S>Option316"
tItem[3100022]["Option317"] = tGodRefinedRewardSign_Text[3100022]["Option317"]
tItem[3100022]["OptionFunc317"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>801308</S>Option317"

tItem[3100022]["Option318"] = tGodRefinedRewardSign_Text[3100022]["Option318"]
tItem[3100022]["OptionFunc318"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>827010</S>Option318"
tItem[3100022]["Option319"] = tGodRefinedRewardSign_Text[3100022]["Option319"]
tItem[3100022]["OptionFunc319"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>827011</S>Option319"
tItem[3100022]["Option3110"] = tGodRefinedRewardSign_Text[3100022]["Option3110"]
tItem[3100022]["OptionFunc3110"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>827012</S>Option3110"
tItem[3100022]["Option3111"] = tGodRefinedRewardSign_Text[3100022]["Option3111"]
tItem[3100022]["OptionFunc3111"] = "GodRefinedRewardSign_SouRefinelSel</N>3100022</N>827013</S>Option3111"


tItem[3100022]["Option313"] = tGodRefinedRewardSign_Text[3100022]["Option313"]
tItem[3100022]["OptionPoint313"] = "2-1"
tItem[3100022]["Option314"] = tGodRefinedRewardSign_Text[3100022]["Option314"]

tItem[3100022]["Text4-1"] = {411}
tItem[3100022]["Text411"] = tGodRefinedRewardSign_Text[3100022]["Text411"]
tItem[3100022]["tOption4-1"] = {411,412}
tItem[3100022]["Option411"] = tGodRefinedRewardSign_Text[3100022]["Option411"]
tItem[3100022]["OptionFunc411"] = "GodRefinedRewardSign_SoulRefineAward</N>3100022</N>801102"
tItem[3100022]["Option412"] = tGodRefinedRewardSign_Text[3100022]["Option412"]


--七阶防具神魂礼包
tItem[3100023] = tItem[3100023] or {}

tItem[3100023]["Text1-1"] = {111}
tItem[3100023]["Text111"] = tGodRefinedRewardSign_Text[3100023]["Text111"]
tItem[3100023]["tOption1-1"] = {111,112,113,114,115,116}
tItem[3100023]["Option111"] = tGodRefinedRewardSign_Text[3100023]["Option111"]
tItem[3100023]["OptionPoint111"] = "2-1"
tItem[3100023]["Option112"] = tGodRefinedRewardSign_Text[3100023]["Option112"]
tItem[3100023]["OptionPoint112"] = "2-2"
tItem[3100023]["Option113"] = tGodRefinedRewardSign_Text[3100023]["Option113"]
tItem[3100023]["OptionPoint113"] = "2-3"
tItem[3100023]["Option114"] = tGodRefinedRewardSign_Text[3100023]["Option114"]
tItem[3100023]["OptionPoint114"] = "2-4"
tItem[3100023]["Option115"] = tGodRefinedRewardSign_Text[3100023]["Option115"]
tItem[3100023]["OptionPoint115"] = "2-5"
tItem[3100023]["Option116"] = tGodRefinedRewardSign_Text[3100023]["Option116"]

tItem[3100023]["Text2-1"] = {211}
tItem[3100023]["Text211"] = tGodRefinedRewardSign_Text[3100023]["Text211"]
tItem[3100023]["tOption2-1"] = {211,212,213,214,215,216}
tItem[3100023]["Option211"] = tGodRefinedRewardSign_Text[3100023]["Option211"]
tItem[3100023]["OptionFunc211"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>820073</S>Option211"
tItem[3100023]["Option212"] = tGodRefinedRewardSign_Text[3100023]["Option212"]
tItem[3100023]["OptionFunc212"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>820074</S>Option212"
tItem[3100023]["Option213"] = tGodRefinedRewardSign_Text[3100023]["Option213"]
tItem[3100023]["OptionFunc213"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>820075</S>Option213"
tItem[3100023]["Option214"] = tGodRefinedRewardSign_Text[3100023]["Option214"]
tItem[3100023]["OptionFunc214"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>820076</S>Option214"
tItem[3100023]["Option215"] = tGodRefinedRewardSign_Text[3100023]["Option215"]
tItem[3100023]["OptionPoint215"] = "1-1"
tItem[3100023]["Option216"] = tGodRefinedRewardSign_Text[3100023]["Option216"]


tItem[3100023]["Text2-2"] = {221}
tItem[3100023]["Text221"] = tGodRefinedRewardSign_Text[3100023]["Text221"]
tItem[3100023]["tOption2-2"] = {221,222,223,224}
tItem[3100023]["Option221"] = tGodRefinedRewardSign_Text[3100023]["Option221"]
tItem[3100023]["OptionFunc221"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>822071</S>Option221"
tItem[3100023]["Option222"] = tGodRefinedRewardSign_Text[3100023]["Option222"]
tItem[3100023]["OptionFunc222"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>822072</S>Option222"
tItem[3100023]["Option223"] = tGodRefinedRewardSign_Text[3100023]["Option223"]
tItem[3100023]["OptionPoint223"] = "1-1"
tItem[3100023]["Option224"] = tGodRefinedRewardSign_Text[3100023]["Option224"]

tItem[3100023]["Text2-3"] = {231}
tItem[3100023]["Text231"] = tGodRefinedRewardSign_Text[3100023]["Text231"]
tItem[3100023]["tOption2-3"] = {231,232,233,234}
tItem[3100023]["Option231"] = tGodRefinedRewardSign_Text[3100023]["Option231"]
tItem[3100023]["OptionFunc231"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>821034</S>Option231"
tItem[3100023]["Option232"] = tGodRefinedRewardSign_Text[3100023]["Option232"]
tItem[3100023]["OptionFunc232"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>821033</S>Option232"
tItem[3100023]["Option233"] = tGodRefinedRewardSign_Text[3100023]["Option233"]
tItem[3100023]["OptionPoint233"] = "1-1"
tItem[3100023]["Option234"] = tGodRefinedRewardSign_Text[3100023]["Option234"]

tItem[3100023]["Text2-4"] = {241}
tItem[3100023]["Text241"] = tGodRefinedRewardSign_Text[3100023]["Text241"]
tItem[3100023]["tOption2-4"] = {241,242,243,244,245,246,247}
tItem[3100023]["Option241"] = tGodRefinedRewardSign_Text[3100023]["Option241"]
tItem[3100023]["OptionFunc241"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>823058</S>Option241"
tItem[3100023]["Option242"] = tGodRefinedRewardSign_Text[3100023]["Option242"]
tItem[3100023]["OptionFunc242"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>823059</S>Option242"
tItem[3100023]["Option243"] = tGodRefinedRewardSign_Text[3100023]["Option243"]
tItem[3100023]["OptionFunc243"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>823061</S>Option243"
tItem[3100023]["Option244"] = tGodRefinedRewardSign_Text[3100023]["Option244"]
tItem[3100023]["OptionFunc244"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>823062</S>Option244"
tItem[3100023]["Option245"] = tGodRefinedRewardSign_Text[3100023]["Option245"]
tItem[3100023]["OptionFunc245"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>823060</S>Option245"
tItem[3100023]["Option246"] = tGodRefinedRewardSign_Text[3100023]["Option246"]
tItem[3100023]["OptionPoint246"] = "1-1"
tItem[3100023]["Option247"] = tGodRefinedRewardSign_Text[3100023]["Option247"]

tItem[3100023]["Text2-5"] = {251}
tItem[3100023]["Text251"] = tGodRefinedRewardSign_Text[3100023]["Text251"]
tItem[3100023]["tOption2-5"] = {251,252,253,254,255}
tItem[3100023]["Option251"] = tGodRefinedRewardSign_Text[3100023]["Option251"]
tItem[3100023]["OptionFunc251"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>824018</S>Option251"
tItem[3100023]["Option252"] = tGodRefinedRewardSign_Text[3100023]["Option252"]
tItem[3100023]["OptionFunc252"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>824019</S>Option252"
tItem[3100023]["Option253"] = tGodRefinedRewardSign_Text[3100023]["Option253"]
tItem[3100023]["OptionFunc253"] = "GodRefinedRewardSign_SouRefinelSel</N>3100023</N>824020</S>Option253"
tItem[3100023]["Option254"] = tGodRefinedRewardSign_Text[3100023]["Option254"]
tItem[3100023]["OptionPoint254"] = "1-1"
tItem[3100023]["Option255"] = tGodRefinedRewardSign_Text[3100023]["Option255"]

tItem[3100023]["Text4-1"] = {411}
tItem[3100023]["Text411"] = tGodRefinedRewardSign_Text[3100023]["Text411"]
tItem[3100023]["tOption4-1"] = {411,412}
tItem[3100023]["Option411"] = tGodRefinedRewardSign_Text[3100023]["Option411"]
tItem[3100023]["OptionFunc411"] = "GodRefinedRewardSign_SoulRefineAward</N>3100023</N>801102"
tItem[3100023]["Option412"] = tGodRefinedRewardSign_Text[3100023]["Option412"]








