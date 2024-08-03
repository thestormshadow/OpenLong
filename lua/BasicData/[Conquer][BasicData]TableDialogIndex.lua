----------------------------------------------------------------------------
--Name:		[征服][基础数据]中文索引表.lua
--Purpose:	中文索引表
--Creator: 	郑江文
--Created:	2014/08/28
----------------------------------------------------------------------------
--key为10000~99999
--10000-29999 公用函数、模板逻辑
--30000-59999 功能脚本
--60000-99999 任务脚本
tLuaRes = {}
tLuaRes[10001] = "Eh..."
tLuaRes[10002] = "See~you."
tLuaRes[10003] = "Leave."
tLuaRes[10004] = "The item is useless now, and you threw it away."
tLuaRes[10005] = "Previous."
tLuaRes[10006] = "Next."

tBackpackLetter_Text = tBackpackLetter_Text or {}
-----------------------------------国家官职名字-------------------------------------
tLuaRes[10007] = "Emperor"
tLuaRes[10008] = "L.Premier"
tLuaRes[10009] = "R.Premier"
tLuaRes[10010] = "L.Marshal"
tLuaRes[10011] = "R.Marshal"
tLuaRes[10012] = "SEC.Gen."
tLuaRes[10013] = "DEF.Gen."
tLuaRes[10014] = "PAC.Gen."
tLuaRes[10015] = "CO.Gen."
tLuaRes[10016] = "Imprl.Guard"
tLuaRes[10017] = "Empress"
tLuaRes[10018] = "Empress"
tLuaRes[10019] = "SeniorConc."
tLuaRes[10020] = "SeniorConc."
tLuaRes[10021] = "Conc."
tLuaRes[10022] = "Conc."

------------------------------------------------------------------------------------
--Name:			[征服][节日任务]悬挂同心锁（2.11-2.14）
--Purpose:		2015年情人节悬挂同心锁（2.11-2.14）
--Creator: 		郑鋆
--Created:		2014/11/28
------------------------------------------------------------------------------------
-- 文字对白
tValentinesDay2015_ConcentricLock_Text = {}
tValentinesDay2015_ConcentricLock_Text[17276] = {}
-- 活动前
tValentinesDay2015_ConcentricLock_Text[17276]["Text111"] = "Have you ever heard about that? If lovers leave the Heart Lock on a romantic place, their love"
tValentinesDay2015_ConcentricLock_Text[17276]["Text112"] = "~will be everlasting. If you`re interested, come and claim a Heart Lock from Feb. 11th to 14th."
tValentinesDay2015_ConcentricLock_Text[17276]["Text113"] = ""
-- 活动后
tValentinesDay2015_ConcentricLock_Text[17276]["Text121"] = "People believe that leaving the Heart Lock on the bridge symbolizes their eternal love."
tValentinesDay2015_ConcentricLock_Text[17276]["Text122"] = ""
-- 活动中
tValentinesDay2015_ConcentricLock_Text[17276]["Text131"] = "People believe that leaving the Heart Lock on the bridge symbolizes their eternal love. If you`ve"
tValentinesDay2015_ConcentricLock_Text[17276]["Text132"] = "~reached Level 80 or get reborn and you want to pray for everlasting love, come and claim your"
tValentinesDay2015_ConcentricLock_Text[17276]["Text133"] = "~Heart Lock from Feb. 11th to 14th."

-- 玩家等级不足
tValentinesDay2015_ConcentricLock_Text[17276]["Text311"] = "Sorry, the Heart Locks are only for heroes who have reached Level 80 or got reborn."
tValentinesDay2015_ConcentricLock_Text[17276]["Text312"] = "~You need to make yourself stronger, first."

-- 玩家选1、失败、当日已完成该任务
tValentinesDay2015_ConcentricLock_Text[17276]["Text321"] = "Come on, you`ve already hung a Heart Lock, today. For a good prayer, you only need to do it once."

-- 玩家选1、失败、玩家处于异性组队状态
tValentinesDay2015_ConcentricLock_Text[17276]["Text331"] = "Well, you have a partner in a team, now. If you want to pray for your own love, you need to be alone first."
tValentinesDay2015_ConcentricLock_Text[17276]["Text332"] = "~If it`s the love between you and your partner you would like to pray for, you need to hang the Heart Lock for both of you."

-- 玩家选1、失败、玩家背包满
tValentinesDay2015_ConcentricLock_Text[17276]["Text341"] = "Come on, I can`t put anything into a full bag. You need to make some room in your inventory, first."

-- 玩家选1、成功、给玩家道具、并导航到第一座桥
tValentinesDay2015_ConcentricLock_Text[17276]["Text351"] = "Please take the Heart Lock and the Heart Key. You`ll need to hang the lock on the bridge (589,355) in the southeast of Wind Plain,"
tValentinesDay2015_ConcentricLock_Text[17276]["Text352"] = "~and then throw the key from the bridge (132,345) in the northwest of Wind Plain. Good luck!"
tValentinesDay2015_ConcentricLock_Text[17276]["Text353"] = ""

-- 玩家选1、补领失败、玩家已完成任务提醒领取奖励
tValentinesDay2015_ConcentricLock_Text[17276]["Text411"] = "It seems you`ve done the love prayer with Heart Lock and Heart Key. Actually, you can claim a reward from me."

-- 玩家选1、补领失败、玩家背包满
tValentinesDay2015_ConcentricLock_Text[17276]["Text421"] = "Your inventory is too full to contain anything. Please make some room, first."

-- 玩家选1、补领失败、玩家身上已有道具或已使用道具
tValentinesDay2015_ConcentricLock_Text[17276]["Text431"] = "You`ve claimed a Heart Lock and a Heart Key, haven`t you? I guess they`re still in your inventory, or maybe you`ve already used them for the love prayer."

-- 玩家选1、补领成功
tValentinesDay2015_ConcentricLock_Text[17276]["Text441"] = "Okay. Now, you`re ready to do the love prayer.  Go hang the lock on the bridge (589,355) in the southeast of Wind Plain,"
tValentinesDay2015_ConcentricLock_Text[17276]["Text442"] = "~and then throw the key from the bridge (132,345) in the northwest of Wind Plain. Remember to be sincere while praying."
tValentinesDay2015_ConcentricLock_Text[17276]["Text443"] = ""

-- 玩家选2、失败、已领取当天奖励
tValentinesDay2015_ConcentricLock_Text[17276]["Text511"] = "You`ve already claimed the reward, today. See, I have no more for you."

-- 玩家选2、失败、玩家未完该活动
tValentinesDay2015_ConcentricLock_Text[17276]["Text521"] = "You haven`t finished the love prayer with the Heart Lock and Heart Key. If so, you can`t claim the reward."

-- 玩家选2、失败、背包满
tValentinesDay2015_ConcentricLock_Text[17276]["Text531"] = "Your inventory is too full! I can`t put anything into it. Why not tidy it up, first?"

-- 玩家选2、成功、领取活动奖励
tValentinesDay2015_ConcentricLock_Text[17276]["Text541"] = "Trust me, your love prayer will be heard. Here is a reward for you."

-- 玩家选3、活动介绍
tValentinesDay2015_ConcentricLock_Text[17276]["Text611"] = "During the event, all qualified heroes can claim a Heart Lock and a Heart Key to pray for eternal love."
tValentinesDay2015_ConcentricLock_Text[17276]["Text612"] = "~First, go to the bridge (589,355) on the southeast of Wind Plain, and right click to hang the Heart Lock."
tValentinesDay2015_ConcentricLock_Text[17276]["Text613"] = "~Then, throw the Heart Key over the bridge (132,345) in the northeast. Don`t forget to claim a reward"
tValentinesDay2015_ConcentricLock_Text[17276]["Text614"] = "~from me after finishing these things."

-- 选项文字
tValentinesDay2015_ConcentricLock_Text[17276]["Option1"] = "Sounds~romantic."
tValentinesDay2015_ConcentricLock_Text[17276]["Option2"] = "I~believe~it~so."
tValentinesDay2015_ConcentricLock_Text[17276]["Option3"] = "I~want~to~hang~the~lock."
tValentinesDay2015_ConcentricLock_Text[17276]["Option4"] = "I~lost~the~lock~and~key."
tValentinesDay2015_ConcentricLock_Text[17276]["Option5"] = "Claim~my~reward."
tValentinesDay2015_ConcentricLock_Text[17276]["Option6"] = "How~to~use~the~lock?"
tValentinesDay2015_ConcentricLock_Text[17276]["Option7"] = "I~have~no~Valentine`s~Day."
tValentinesDay2015_ConcentricLock_Text[17276]["Option9"] = "You`re~right."
tValentinesDay2015_ConcentricLock_Text[17276]["Option10"] = "Okay."
tValentinesDay2015_ConcentricLock_Text[17276]["Option11"] = "Sorry..."
tValentinesDay2015_ConcentricLock_Text[17276]["Option12"] = "Okay."
tValentinesDay2015_ConcentricLock_Text[17276]["Option13"] = "I~need~your~guidance."
tValentinesDay2015_ConcentricLock_Text[17276]["Option14"] = "Great."
tValentinesDay2015_ConcentricLock_Text[17276]["Option15"] = "Sorry..."
tValentinesDay2015_ConcentricLock_Text[17276]["Option16"] = "Okay."
tValentinesDay2015_ConcentricLock_Text[17276]["Option17"] = "I~really~want~it."
tValentinesDay2015_ConcentricLock_Text[17276]["Option18"] = "Go~to~the~bridge~in~southeast."

-- 物品使用提示
tValentinesDay2015_ConcentricLock_Text[3005388] = {}
tValentinesDay2015_ConcentricLock_Text[3005388]["NoPosition"] = "You can`t leave the Heart Lock here. Go to the bridge (589,355) in the southeast of Wind Plain."
tValentinesDay2015_ConcentricLock_Text[3005388]["Complete"] = "The Heart Locks on the bridge are symbols of everlasting love. Go find another bridge, and throw the Heart Key."
tValentinesDay2015_ConcentricLock_Text[3005388]["Content"] = "Hanging"

tValentinesDay2015_ConcentricLock_Text[3005389] = {}
tValentinesDay2015_ConcentricLock_Text[3005389]["NoPosition"] = "You can`t throw the Heart Key here. Go to the bridge (132,345) in the northwest of Wind Plain."
tValentinesDay2015_ConcentricLock_Text[3005389]["Complete"] = "The Heart Key has been thrown into the lake, symbolizing unbreakable love. Go claim your reward from Matchmaker Kiu."
tValentinesDay2015_ConcentricLock_Text[3005389]["Content"] = "Discarding"

tValentinesDay2015_ConcentricLock_Text["BeOverdue"] = "Valentine`s Day is over, and the item is useless now."
tValentinesDay2015_ConcentricLock_Text["ExtraProps"] = "You`ve already used the item and completed this step."
tValentinesDay2015_ConcentricLock_Text["Step"] = "You need to hang the Heart Lock on the bridge first, and then throw the Heart Key into the lake."
tValentinesDay2015_ConcentricLock_Text["Excess"] = "You just need to hang one Heart Lock, while other relevant items have been discarded."
tValentinesDay2015_ConcentricLock_Text["NextDay"] = "The expired item has been discarded by you. You can claim a new Heart Lock from Matchmaker Kiu, today."

-- 陷阱提示
tValentinesDay2015_ConcentricLock_Text[992303] = "Right click the Heart Lock to hang it on the bridge."
tValentinesDay2015_ConcentricLock_Text[992304] = "Right click the Heart Key to throw it into the lake."

------------------------------------------------------------------------------------
--Name:			[征服][节日任务]比翼双飞鸟（2.11-2.14）
--Purpose:		2015年情人节比翼双飞鸟（2.11-2.14）
--Creator: 		郑鋆
--Created:		2014/11/28
------------------------------------------------------------------------------------
-- 中文提示	
tValentinesDay2015_BiyiBirds_Text = {}
tValentinesDay2015_BiyiBirds_Text[17302] = {}
-- 活动前对白
tValentinesDay2015_BiyiBirds_Text[17302]["Text111"] = "Love is the most beautiful thing that everyone desires for. However, there are some monsters with"
tValentinesDay2015_BiyiBirds_Text[17302]["Text112"] = "~a disgusting hobby trying to separate the love birds from Feb. 11th to 14th. If you`re free at that"
tValentinesDay2015_BiyiBirds_Text[17302]["Text113"] = "~time, come and give me a hand."
-- 活动后对白
tValentinesDay2015_BiyiBirds_Text[17302]["Text121"] = "See, there are birds in couples on the trees. Where is my love?"
tValentinesDay2015_BiyiBirds_Text[17302]["Text122"] = ""
-- 活动中对白
tValentinesDay2015_BiyiBirds_Text[17302]["Text131"] = "I don`t really understand those monsters. They must be sick to separate the couples of love birds."
tValentinesDay2015_BiyiBirds_Text[17302]["Text132"] = "~We have to do something to save them from the monsters. If you`ve reached Level 80 or got reborn,"
tValentinesDay2015_BiyiBirds_Text[17302]["Text133"] = "~come and give me a hand from Feb. 11th to 14th. You know, I`m always generous to those who help me."
tValentinesDay2015_BiyiBirds_Text[17302]["Text134"] = ""

-- 接1?姑娘莫急，交给在下?等级不够
tValentinesDay2015_BiyiBirds_Text[17302]["Text311"] = "Thanks for your kindness, but it`s too dangerous for you to face off those disgusting monsters."
tValentinesDay2015_BiyiBirds_Text[17302]["Text312"] = "~Only when you reach Level 80 or get reborn, you may become strong enough."

-- 接1?姑娘莫急，交给在下?今天已完成过
tValentinesDay2015_BiyiBirds_Text[17302]["Text321"] = "You`ve already rescued a couple of love birds. They looks so sweet, right?"
tValentinesDay2015_BiyiBirds_Text[17302]["Text322"] = ""

-- 接1?姑娘莫急，交给在下?今天已完成并未领取奖励
tValentinesDay2015_BiyiBirds_Text[17302]["Text331"] = "You`ve already rescued a couple of love birds. Don`t forget to claim your reward."

-- 接1?姑娘莫急，交给在下?成功
tValentinesDay2015_BiyiBirds_Text[17302]["Text341"] = "Good. The couple of love birds were separately locked up in the Winged Cage and the Couple Cage."
tValentinesDay2015_BiyiBirds_Text[17302]["Text342"] = "~Some monsters on Wind Plain have the Winged Box or Couple Box with corresponding cage inside,"
tValentinesDay2015_BiyiBirds_Text[17302]["Text343"] = "~while some don`t. When you have the Winged Cage and the Couple Cage together, you can open"
tValentinesDay2015_BiyiBirds_Text[17302]["Text344"] = "~them at the same time on the city wall (TwinCity 248,292) and set the love birds free."

-- 【接1、姑娘莫急、玩家接任务但未完成任务】
tValentinesDay2015_BiyiBirds_Text[17302]["Text351"] = "Have you found the Winged Cage and the Couple Cage? The love birds were separately locked up in the two cages. Hurry and rescue them!"

-- 接2?领取奖励?已领取过
tValentinesDay2015_BiyiBirds_Text[17302]["Text411"] = "You`ve claimed the reward, haven`t you? I hope you like it. Thanks!"
tValentinesDay2015_BiyiBirds_Text[17302]["Text412"] = ""

-- 接2?领取奖励?任务未完成
tValentinesDay2015_BiyiBirds_Text[17302]["Text421"] = "It`s not time for rewards. Remember, the love birds are still in distress. You should help them out, first!"
tValentinesDay2015_BiyiBirds_Text[17302]["Text422"] = ""

-- 接2?领取奖励?玩家背包满
tValentinesDay2015_BiyiBirds_Text[17302]["Text431"] = "Hey, I can`t put anything into your full inventory. Why not make some room, first?"

-- 接2?领取奖励?成功领取
tValentinesDay2015_BiyiBirds_Text[17302]["Text441"] = "Thanks for rescuing the love birds. Well, here`s a reward for you as I promised."
tValentinesDay2015_BiyiBirds_Text[17302]["Text442"] = ""

-- 接3?可否请姑娘详细一说
tValentinesDay2015_BiyiBirds_Text[17302]["Text511"] = "Some monsters captured couples of love birds, and separately locked up them in Winged Cage and"
tValentinesDay2015_BiyiBirds_Text[17302]["Text512"] = "~Couple Cage. While hunting monsters on Wind Plain, you`ll have a chance to receive Winged Box or Couple Box"
tValentinesDay2015_BiyiBirds_Text[17302]["Text513"] = "~with corresponding cage inside. You may also get empty boxes. When you collect a Winged Cage and a"
tValentinesDay2015_BiyiBirds_Text[17302]["Text514"] = "~Couple Cage, open them at the same time on the city wall (TwinCity 248,292) and set the love birds free."

-- 选项
tValentinesDay2015_BiyiBirds_Text[17302]["Option1"] = "I`ll~help."
tValentinesDay2015_BiyiBirds_Text[17302]["Option2"] = "You`ll~find~your~true~love."
tValentinesDay2015_BiyiBirds_Text[17302]["Option3"] = "Count~on~me!"
tValentinesDay2015_BiyiBirds_Text[17302]["Option4"] = "Claim~my~reward."
tValentinesDay2015_BiyiBirds_Text[17302]["Option5"] = "Tell~me~more."
tValentinesDay2015_BiyiBirds_Text[17302]["Option6"] = "I`ll~talk~to~you~later."
tValentinesDay2015_BiyiBirds_Text[17302]["Option7"] = "You`re~right."
tValentinesDay2015_BiyiBirds_Text[17302]["Option8"] = "Love~is~love."
tValentinesDay2015_BiyiBirds_Text[17302]["Option9"] = "You`re~right."
tValentinesDay2015_BiyiBirds_Text[17302]["Option10"] = "Don`t~worry."
tValentinesDay2015_BiyiBirds_Text[17302]["Option11"] = "You`re~welcome."
tValentinesDay2015_BiyiBirds_Text[17302]["Option12"] = "I`m~on~the~way."
tValentinesDay2015_BiyiBirds_Text[17302]["Option13"] = "Okay."
tValentinesDay2015_BiyiBirds_Text[17302]["Option14"] = "You~can~count~on~me."

tValentinesDay2015_BiyiBirds_Text[3005441] = {}
tValentinesDay2015_BiyiBirds_Text[3005441]["NoItem"] = "You opened the Winged Box, and found nothing inside."
tValentinesDay2015_BiyiBirds_Text[3005441]["RewardItem"] = "You opened the Winged Box, and received a Winged Cage."
tValentinesDay2015_BiyiBirds_Text[3005441]["Reward"] = "You killed monsters without mercy, and found a Winged Box on the ground. Hurry and check it."

tValentinesDay2015_BiyiBirds_Text[3005442] = {}
tValentinesDay2015_BiyiBirds_Text[3005442]["NoItem"] = "You opened the Couple Box, and found nothing inside."
tValentinesDay2015_BiyiBirds_Text[3005442]["RewardItem"] = "You opened the Couple Box, and received a Couple Cage."
tValentinesDay2015_BiyiBirds_Text[3005442]["Reward"] = "The monster fell down and dropped a Couple Box. Hurry and check it."

tValentinesDay2015_BiyiBirds_Text["BeOverdue"] = "Valentine`s Day has ended. The item became useless, and you threw it away."
tValentinesDay2015_BiyiBirds_Text["BagFull"] = "Your inventory is full. Please make some room, first."
tValentinesDay2015_BiyiBirds_Text["NotAllItem"] = "You`re unable to open the cage. The bird in the cage is crying for its lover."
tValentinesDay2015_BiyiBirds_Text["UseItemNavigat"] = "You can`t open the cage, here. Please walk onto the city wall (TwinCity 248,292), and set the love birds free."
tValentinesDay2015_BiyiBirds_Text["AllItem"] = "You`ve collected the Winged Cage and the Couple Cage. Go and set the birds free on the city wall (TwinCity 248,292)."
tValentinesDay2015_BiyiBirds_Text["NextDay"] = "There is nothing in the cage."
tValentinesDay2015_BiyiBirds_Text["HaveComplete"] = "You`ve already set the love birds free from the cage."
tValentinesDay2015_BiyiBirds_Text["Complete"] = "You unlocked the cages, and set the love birds free!"

tValentinesDay2015_BiyiBirds_Text[992310] = {}
tValentinesDay2015_BiyiBirds_Text[992310][1] = "This is the right place to set the love birds free."
tValentinesDay2015_BiyiBirds_Text[992310][2] = "This is the right place to set the love birds free."
tValentinesDay2015_BiyiBirds_Text[992311] = "Go collect the Winged Box and the Couple Box from monsters on Wind Plain."


------------------------------------------------------------------------------------
--Name:		[征服][任务脚本]通用礼包逻辑修改为用LUA
--Purpose:	通用礼包逻辑修改为用LUA
--Creator: 	严振飞
--Created:	2014/10/28
------------------------------------------------------------------------------------
tFestivalGeneralPackage = {}
tFestivalGeneralPackage["Bag_Full"] = "Your inventory is full. Please make some room first, and then login again to receive a gift."
tFestivalGeneralPackage["Get_Gift"] = "You received a Festival Joy Pack!"
tFestivalGeneralPackage["OverLimit_Msg"] = "You can`t claimed more than %s Festival Joy Packs in a day."


------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]情人节活动之神父的祝福
--Purpose:	情人节活动之神父的祝福
--Creator: 	严振飞
--Created:	2014/11/07
------------------------------------------------------------------------------------
-------------NPC对白
-- //查理神父
tValentinesDay2015_Priest_Text = {}
tValentinesDay2015_Priest_Text[17285] = {}

-- 活动前对白
tValentinesDay2015_Priest_Text[17285]["Text111"] = "Every year when Valentine`s Day comes, I`ll travel around and deliver blessing to lovers. If you`re looking for true"
tValentinesDay2015_Priest_Text[17285]["Text112"] = "~love and seeking for happiness, come and find me on Feb. 11th - 14th."
tValentinesDay2015_Priest_Text[17285]["Text113"] = ""

-- 活动中对白
tValentinesDay2015_Priest_Text[17285]["Text121"] = "Happy Valentine`s Day! As called by goddess of love, I`m delivering blessing to lovers in Twin City. If you`re looking for true"
tValentinesDay2015_Priest_Text[17285]["Text122"] = "~love and seeking for happiness, come and find me on Feb. 11th - 14th. I`ll do a prayer for you."
tValentinesDay2015_Priest_Text[17285]["Text123"] = ""

-- 活动后对白
tValentinesDay2015_Priest_Text[17285]["Text131"] = "What a romantic festival! Thanks for giving me an unforgettable memory of love."
tValentinesDay2015_Priest_Text[17285]["Text132"] = ""

-- 【我想了解活动详情。】
tValentinesDay2015_Priest_Text[17285]["Text211"] = "Of course, you`re interested in the events. So, what would you like to learn more?"

-- 【我想了解活动详情。】--> 我想知道情人节有什么活动。
tValentinesDay2015_Priest_Text[17285]["Text311"] = "I`m responsible for delivering blessing and explaining the events of Valentine Days. During the festival, you can claim blessing"
tValentinesDay2015_Priest_Text[17285]["Text312"] = "~from me every day, and sign up for wonderful quests with my friends around the flower bed in Twin City. After completing all the events, you can"
tValentinesDay2015_Priest_Text[17285]["Text313"] = "~also claim a special gift pack from me, once in a day. So, which quest are you interested?"

-- 【我想了解活动详情。】--> 我想知道情人节有什么活动。--> 比翼双飞。
tValentinesDay2015_Priest_Text[17285]["Text411"] = "In the season of love, people are immersed in sweetness, but Smart Sue looks worried. Some disgusting monsters captured"
tValentinesDay2015_Priest_Text[17285]["Text412"] = "~couples of love birds and separated them in cages. Sue is calling heroes above Level 80 or reborn to rescue the love birds"
tValentinesDay2015_Priest_Text[17285]["Text413"] = "~from monsters on Wind Plain from Feb. 11th to 14th."

-- 【我想了解活动详情。】--> 我想知道情人节有什么活动。--> 美女与野兽。
tValentinesDay2015_Priest_Text[17285]["Text511"] = "Siegfride was cursed to be a beast since he refused to put up an old woman. It`s said only when he learns to be kind to people"
tValentinesDay2015_Priest_Text[17285]["Text512"] = "~and receives 99 Lover`s Roses from Feb. 11th to 14th, the curse can be dispelled. Siegfride`s lover, Bella, is asking help from the"
tValentinesDay2015_Priest_Text[17285]["Text513"] = "~heroes who`ve reached Level 80 or got reborn."

-- 【我想了解活动详情。】--> 我想知道情人节有什么活动。--> 悬挂同心锁。
tValentinesDay2015_Priest_Text[17285]["Text611"] = "It`s an old story. Lovers who hang the Heart Lock on a spiritual bridge will never apart. Matchmaker Kiu just happened"
tValentinesDay2015_Priest_Text[17285]["Text612"] = "~to have some Heart Locks. If you`ve reached Level 80 or got reborn and you`re interested, go claim one from Matchmaker"
tValentinesDay2015_Priest_Text[17285]["Text613"] = "~Kiu on Feb. 11th - 14th for a love prayer."

-- 【我想了解活动详情。】--> 我想知道情人节有什么活动。--> 眉来眼去传真情。
tValentinesDay2015_Priest_Text[17285]["Text711"] = "From Feb. 11th to 14th, Lady Autumn and Master Hwang will appear at (TwinCity 285,359) and share a legendary"
tValentinesDay2015_Priest_Text[17285]["Text712"] = "~scripture of martial arts, True Love Manual, with Level 80+ or reborn heroes as a Valentine`s Day gift."

-- 【我想了解活动详情。】--> 我想知道情人节有什么活动。--> 甜蜜约会之非诚勿扰。
tValentinesDay2015_Priest_Text[17285]["Text811"] = "Matchmaker Yiu is incredibly busy. His assistant, Fat, disappeared. There are many single men and women waiting for a match"
tValentinesDay2015_Priest_Text[17285]["Text812"] = "~in his office. All in all, Yiu is seriously short of hands. If you`ve reached Level 80 or got reborn, go and do him a favor."
tValentinesDay2015_Priest_Text[17285]["Text813"] = ""

-- 【我想了解活动详情。】--> 爱情玫瑰要怎么换取外套呢？
tValentinesDay2015_Priest_Text[17285]["Text911"] = "The Love Rose with delicate petals and mysterious scent is a blessing to Valentine`s Day. From Feb. 11th to 14th, every time"
tValentinesDay2015_Priest_Text[17285]["Text912"] = "~when you complete a quest of Valentine`s Day, you`ll be rewarded with a Love Rose. When you have 5 Love Roses, you can"
tValentinesDay2015_Priest_Text[17285]["Text913"] = "~sweep them for a 3-hour 1%% Blessed weapon accessory, Rod of Rose, with me."

-- 【我想了解活动详情。】--> 我想知道送长梗玫瑰的事情。
tValentinesDay2015_Priest_Text[17285]["Text1011"] = "To celebrate the romantic festival, heroes are presented with the Roses of Everlasting. I want you to share that with those friends"
tValentinesDay2015_Priest_Text[17285]["Text1012"] = "~working for Valentine`s Day events. Good things will become better when you share. After that, you can claim a Festival Joy Pack from me."
tValentinesDay2015_Priest_Text[17285]["Text1013"] = "~By the way, if you lost the Roses of Everlasting before finishing the job, you can login again to reclaim another bunch."

-- 【我要领取全活动完成奖励。】
tValentinesDay2015_Priest_Text[17285]["Text1111"] = "Valentine`s Day has passed, and you can`t claim a reward for completing all the quest."
tValentinesDay2015_Priest_Text[17285]["Text1121"] = "Thanks for coming, but you`re still too green for the quests of Valentine`s Day."
tValentinesDay2015_Priest_Text[17285]["Text1122"] = "~Keep practicing, and come back when you`re qualified."
tValentinesDay2015_Priest_Text[17285]["Text1131"] = "Come on, I can`t put anything into your full inventory. You need to make some room, first."
tValentinesDay2015_Priest_Text[17285]["Text1141"] = "It seems you`ve already claimed a gift pack. You must be too excited to check it well."
tValentinesDay2015_Priest_Text[17285]["Text1151"] = "Buddy, you haven`t completed all the quests of Valentine`s Day. You`ll find more interesting things in Valentine`s Day celebration."
tValentinesDay2015_Priest_Text[17285]["Text1161"] = "Here`s a Festival Joy Pack for you. Wish you a happy life!"

-- 【我要换取情人节外套。】
tValentinesDay2015_Priest_Text[17285]["Text1211"] = "Let me see... Wow, you really have 5 Love Roses! Look, you can swap them for a 3-hour 1%% Blessed weapon accessory or garment."
tValentinesDay2015_Priest_Text[17285]["Text1212"] = "~So, which one do you want, the Rod of Roses or the Cogs of the Heart?"
tValentinesDay2015_Priest_Text[17285]["Text1213"] = ""

-- 【我要换取情人节外套。】--> 我要换取玫瑰风暴武器外套。
tValentinesDay2015_Priest_Text[17285]["Text1311"] = "The Rod of Rose belongs to you, now. Happy Valentine`s Day!"

-- 【我要换取情人节外套。】--> 我要换取情人之泪外套。
tValentinesDay2015_Priest_Text[17285]["Text1411"] = "The Cogs of the Heart belong to you, now. Happy Valentine`s Day!"

-- 【我要换取情人节外套。】--> 我要换取玫瑰风暴武器外套。
tValentinesDay2015_Priest_Text[17285]["Text1511"] = "The event is over, and you can`t swap for Rod of Roses now."
tValentinesDay2015_Priest_Text[17285]["Text1521"] = "Buddy, I know you really want the beautiful weapon accessory, but you should train yourself strong enough first."
tValentinesDay2015_Priest_Text[17285]["Text1522"] = "~When you reach Level 80 or get reborn, you`ll be able to take the quests for winning a Rod of Roses."
tValentinesDay2015_Priest_Text[17285]["Text1531"] = "Why are you carrying a full bag? You need to make some room in the inventory for the reward, first."
tValentinesDay2015_Priest_Text[17285]["Text1541"] = "Buddy, I know you really want the beautiful weapon accessory, but you should have 5 Love Roses to swap for it. Go collect the roses from Valentine`s Day quests."
tValentinesDay2015_Priest_Text[17285]["Text1551"] = "Are you sure you want to swap the 5 Love Roses for a Rod of Roses weapon accessory?"

-- 【我要换取情人节外套。】--> 我要换取情人之泪外套。
tValentinesDay2015_Priest_Text[17285]["Text1611"] = "The event is over, and you can`t swap for Cogs of the Heart now."
tValentinesDay2015_Priest_Text[17285]["Text1621"] = "Buddy, I know you really want the delicate garment, but you should train yourself strong enough first."
tValentinesDay2015_Priest_Text[17285]["Text1622"] = "~When you reach Level 80 or get reborn, you`ll be able to take the quests for winning the Cogs of the Heart."
tValentinesDay2015_Priest_Text[17285]["Text1631"] = "Why are you carrying a full bag? You need to make some room in the inventory for the reward, first."
tValentinesDay2015_Priest_Text[17285]["Text1641"] = "Buddy, I know you really want the delicate garment, but you should have 5 Love Roses to swap for it. Go collect the roses from Valentine`s Day quests."
tValentinesDay2015_Priest_Text[17285]["Text1651"] = "Are you sure you want to swap the 5 Love Roses for Cogs of the Heart garment?"

-- 【我要签到领祝福。】
tValentinesDay2015_Priest_Text[17285]["Text1711"] = "The event is over, and you can`t claim the sign-up blessing now."
tValentinesDay2015_Priest_Text[17285]["Text1721"] = "You`re not qualified for the blessing reward. Keep practicing."
tValentinesDay2015_Priest_Text[17285]["Text1731"] = "You`ve already claimed the blessing. Come and try again tomorrow."
tValentinesDay2015_Priest_Text[17285]["Text1741"] = "The priest`s smile makes you happy and warm, and you received 3 hours of Heaven Blessing."

-- 【我要领取送长梗玫瑰奖励。】（领取礼包后选项消失）
tValentinesDay2015_Priest_Text[17285]["Text1811"] = "It seems someone hasn`t received your Rose of Everlasting. You need to finish the job, first."
tValentinesDay2015_Priest_Text[17285]["Text1821"] = "Thanks for delivering the Roses of Everlasting to my friends. Here`s a Festival Joy Pack for you."

-------------选项
tValentinesDay2015_Priest_Text[17285]["Option11"] = "Great."
tValentinesDay2015_Priest_Text[17285]["Option12"] = "Sign~up~for~blessing."
tValentinesDay2015_Priest_Text[17285]["Option13"] = "Tell~me~more~about~the~event."
tValentinesDay2015_Priest_Text[17285]["Option14"] = "I`ve~completed~rose~delivery."
tValentinesDay2015_Priest_Text[17285]["Option15"] = "I`ve~completed~all~the~quests."
tValentinesDay2015_Priest_Text[17285]["Option16"] = "Swap~Love~Roses."
tValentinesDay2015_Priest_Text[17285]["Option17"] = "What~a~good~priest."
tValentinesDay2015_Priest_Text[17285]["Option18"] = "Good~luck~for~you."
tValentinesDay2015_Priest_Text[17285]["Option21"] = "What~for~Valentine`s~Day?"
tValentinesDay2015_Priest_Text[17285]["Option22"] = "Roses~of~Everlasting~delivery."
tValentinesDay2015_Priest_Text[17285]["Option23"] = "How~to~swap~the~Love~Roses?"
tValentinesDay2015_Priest_Text[17285]["Option24"] = "I`m~just~passing~by."
tValentinesDay2015_Priest_Text[17285]["Option31"] = "Love~Birds."
tValentinesDay2015_Priest_Text[17285]["Option32"] = "Beauty~and~the~Beast."
tValentinesDay2015_Priest_Text[17285]["Option33"] = "The~Heart~Lock."
tValentinesDay2015_Priest_Text[17285]["Option34"] = "True~Love~Manual."
tValentinesDay2015_Priest_Text[17285]["Option35"] = "Sweet~Date."
tValentinesDay2015_Priest_Text[17285]["Option36"] = "Sounds~interesting."
tValentinesDay2015_Priest_Text[17285]["Option41"] = "I`m~on~the~way."
tValentinesDay2015_Priest_Text[17285]["Option42"] = "Learn~about~other~things."
tValentinesDay2015_Priest_Text[17285]["Option51"] = "I`m~on~the~way."
tValentinesDay2015_Priest_Text[17285]["Option52"] = "Learn~about~other~things."
tValentinesDay2015_Priest_Text[17285]["Option61"] = "I`m~on~the~way."
tValentinesDay2015_Priest_Text[17285]["Option62"] = "Learn~about~other~things."
tValentinesDay2015_Priest_Text[17285]["Option71"] = "I`m~on~the~way."
tValentinesDay2015_Priest_Text[17285]["Option72"] = "Learn~about~other~things."
tValentinesDay2015_Priest_Text[17285]["Option81"] = "I`m~on~the~way."
tValentinesDay2015_Priest_Text[17285]["Option82"] = "Learn~about~other~things."
tValentinesDay2015_Priest_Text[17285]["Option91"] = "Swap~the~Love~Roses."
tValentinesDay2015_Priest_Text[17285]["Option92"] = "I`ll~go~collect~Love~Roses."
tValentinesDay2015_Priest_Text[17285]["Option101"] = "I~see."
tValentinesDay2015_Priest_Text[17285]["Option111"] = "I~see."
tValentinesDay2015_Priest_Text[17285]["Option112"] = "I`ll~come~back."
tValentinesDay2015_Priest_Text[17285]["Option113"] = "I`ll~do~it~now."
tValentinesDay2015_Priest_Text[17285]["Option114"] = "Sorry,~I~forgot."
tValentinesDay2015_Priest_Text[17285]["Option11501"] = "I`ll~go~find~Smart~Sue."
tValentinesDay2015_Priest_Text[17285]["Option11502"] = "I`ll~go~find~Bella."
tValentinesDay2015_Priest_Text[17285]["Option11503"] = "I`ll~go~find~Matchmaker~Kiu."
tValentinesDay2015_Priest_Text[17285]["Option11504"] = "I`ll~go~find~Master~Hwang."
tValentinesDay2015_Priest_Text[17285]["Option11505"] = "I`ll~go~find~Lady~Autumn."
tValentinesDay2015_Priest_Text[17285]["Option11506"] = "I`ll~go~find~Matchmaker~Yiu."
tValentinesDay2015_Priest_Text[17285]["Option11507"] = "I~need~to~take~a~rest."
tValentinesDay2015_Priest_Text[17285]["Option116"] = "Happy~Valentine`s~Day!"
tValentinesDay2015_Priest_Text[17285]["Option121"] = "Swap~for~Rod~of~Roses."
tValentinesDay2015_Priest_Text[17285]["Option122"] = "Swap~for~Cogs~of~the~Heart."
tValentinesDay2015_Priest_Text[17285]["Option123"] = "I`ll~think~about~it."
tValentinesDay2015_Priest_Text[17285]["Option131"] = "Happy~Valentine`s~Day!"
tValentinesDay2015_Priest_Text[17285]["Option141"] = "Happy~Valentine`s~Day!"
tValentinesDay2015_Priest_Text[17285]["Option151"] = "I~see."
tValentinesDay2015_Priest_Text[17285]["Option152"] = "Alright."
tValentinesDay2015_Priest_Text[17285]["Option153"] = "I`ll~do~it~now."
tValentinesDay2015_Priest_Text[17285]["Option154"] = "What~a~pity."
tValentinesDay2015_Priest_Text[17285]["Option155"] = "Yes."
tValentinesDay2015_Priest_Text[17285]["Option156"] = "No."
tValentinesDay2015_Priest_Text[17285]["Option161"] = "I~see."
tValentinesDay2015_Priest_Text[17285]["Option162"] = "Alright."
tValentinesDay2015_Priest_Text[17285]["Option163"] = "I`ll~do~it~now."
tValentinesDay2015_Priest_Text[17285]["Option164"] = "What~a~pity."
tValentinesDay2015_Priest_Text[17285]["Option165"] = "Yes."
tValentinesDay2015_Priest_Text[17285]["Option166"] = "No."

tValentinesDay2015_Priest_Text[17285]["Option171"] = "Okay."
tValentinesDay2015_Priest_Text[17285]["Option172"] = "Alright."
tValentinesDay2015_Priest_Text[17285]["Option173"] = "I~see."
tValentinesDay2015_Priest_Text[17285]["Option174"] = "Good!"
tValentinesDay2015_Priest_Text[17285]["Option181"] = "I`ll~go~find~Smart~Sue."
tValentinesDay2015_Priest_Text[17285]["Option182"] = "I`ll~go~find~Bella."
tValentinesDay2015_Priest_Text[17285]["Option183"] = "I`ll~go~find~Matchmaker~Kiu."
tValentinesDay2015_Priest_Text[17285]["Option184"] = "I`ll~go~find~Master~Hwang."
tValentinesDay2015_Priest_Text[17285]["Option185"] = "I`ll~go~find~Lady~Autumn."
tValentinesDay2015_Priest_Text[17285]["Option186"] = "I`ll~go~find~Matchmaker~Yiu."
tValentinesDay2015_Priest_Text[17285]["Option187"] = "I~need~to~take~a~rest."
tValentinesDay2015_Priest_Text[17285]["Option188"] = "Happy~Valentine`s~Day!"

-------------其他NPC对白
-- //苏巧儿
tValentinesDay2015_Priest_Text[17302] = {}
tValentinesDay2015_Priest_Text[17302]["Text4011"] = "Thanks! I really love roses. They look more beautiful on Valentine`s Day, don`t they?"
tValentinesDay2015_Priest_Text[17302]["Option402"] = "Indeed."

-- //美女贝儿
tValentinesDay2015_Priest_Text[17287] = {}
tValentinesDay2015_Priest_Text[17287]["Text4011"] = "Wow, this is my favorite flower! Thanks!"
tValentinesDay2015_Priest_Text[17287]["Option402"] = "You`re~welcome."

-- //红娘娇燕燕
tValentinesDay2015_Priest_Text[17276] = {}
tValentinesDay2015_Priest_Text[17276]["Text4011"] = "Ha, it`s the first time I received roses as a matchmaker. I guess, my love is not far away."
tValentinesDay2015_Priest_Text[17276]["Option402"] = "Good~luck!"

-- //男媒婆姚大痣
tValentinesDay2015_Priest_Text[17321] = {}
tValentinesDay2015_Priest_Text[17321]["Text4011"] = "Are you sure the roses are for me? I`m really surprised. Thank you very much!"
tValentinesDay2015_Priest_Text[17321]["Option402"] = "Happy~Valentine`s~Day!"

-------------其他对白
tValentinesDay2015_Priest_Text["Login_Success"] = "You received Roses of Everlasting. Go and present them to the people working for Valentine`s Day celebration. After that, you can claim a reward from Priest Charlie."
tValentinesDay2015_Priest_Text["Login_BagFull"] = "Your inventory is full. Please make some room first, and then login again to receive the Roses of Everlasting."
tValentinesDay2015_Priest_Text["Item_LoveRose"] = "You can collect Love Roses by participating Valentine`s Day events. When you have 5 Love Roses, swap them for a weapon accessory or garment with Charlie."
tValentinesDay2015_Priest_Text["Item_LongRose"] = "You`ve smelt the roses, and received 1-day Heaven Blessing. Hurry and deliver the roses to the people working for Valentine`s Day celebration."
tValentinesDay2015_Priest_Text["Del_LoveRose"] = "Valentine`s Day has passed, and the Love Roses withered."
tValentinesDay2015_Priest_Text["Del_LongRose"] = "Valentine`s Day has passed, and the Roses of Everlasting disappeared."
tValentinesDay2015_Priest_Text["Flower_Finish"] = "You`ve delivered all the roses. Go and claim your reward from Priest Charlie."
tValentinesDay2015_Priest_Text["NoLongRose_Text"] = "Where is the rose you mentioned?"
tValentinesDay2015_Priest_Text["NoLongRose_Option"] = "I`ll~check~it~again."
tValentinesDay2015_Priest_Text["LongRose_Option"] = "Present~the~rose."

--祝福奖励（数字表示STC掩码编号）
tValentinesDay2015_Priest_Text["AddBless_SysTip"] = {}
tValentinesDay2015_Priest_Text["AddBless_SysTip"][11] = "You smelt the roses, and received 1 day of Heaven Blessing!"
tValentinesDay2015_Priest_Text["AddBless_SysTip"][12] = "Priest Charlie gave you a warm smile, and you received 3 hours of Heaven Blessing!"

-------------物品对白
tValentinesDay2015_Priest_ItemText = {}
--长梗玫瑰
tValentinesDay2015_Priest_ItemText[3005411] = {}
tValentinesDay2015_Priest_ItemText[3005411]["Text111"] = "Are you sure you want to find Priest Charlie for Valentine`s Day events?"

-------------选项
tValentinesDay2015_Priest_ItemText[3005411]["Option11"] = "Yes."
tValentinesDay2015_Priest_ItemText[3005411]["Option12"] = "No."


--3小时玫瑰风暴武器外套礼盒
tValentinesDay2015_Priest_ItemText[3005577] = {}
tValentinesDay2015_Priest_ItemText[3005577]["Text111"] = "Which one do you want to claim?"
--【玫瑰风暴（短武器）。】
tValentinesDay2015_Priest_ItemText[3005577]["Text211"] = "Are you sure you want the 3-hour 1%% Blessed 1-handed weapon accessory, Rod of Rose?"
--【玫瑰风暴（长武器）。】
tValentinesDay2015_Priest_ItemText[3005577]["Text311"] = "Are you sure you want the 3-hour 1%% Blessed 2-handed weapon accessory, Rod of Rose?"

-------------选项
tValentinesDay2015_Priest_ItemText[3005577]["Option11"] = "Rod~of~Roses~(1-handed)."
tValentinesDay2015_Priest_ItemText[3005577]["Option12"] = "Rod~of~Roses~(2-handed)."
tValentinesDay2015_Priest_ItemText[3005577]["Option21"] = "Yes."
tValentinesDay2015_Priest_ItemText[3005577]["Option22"] = "No."
tValentinesDay2015_Priest_ItemText[3005577]["Option31"] = "Yes."
tValentinesDay2015_Priest_ItemText[3005577]["Option32"] = "No."

-------------其他对白
--获得玫瑰风暴外套
tValentinesDay2015_Priest_ItemText["ChgCoat_Success"] = {}
tValentinesDay2015_Priest_ItemText["ChgCoat_Success"][3] = "You received a 3-hour 1%% Blessed 1-handed weapon accessory, Rod of Rose!"
tValentinesDay2015_Priest_ItemText["ChgCoat_Success"][4] = "You received a 3-hour 1%% Blessed 2-handed weapon accessory, Rod of Rose!"



------------------------------------------------------------------------------------
--Name:			[征服][节日任务]眉来眼去传真情（2.11-2.14）
--Purpose:		2015年情人眉来眼去传真情（2.11-2.14）
--Creator: 		陈麟泉
--Created:		2014/11/05
------------------------------------------------------------------------------------

tValentinesDay2015_Flirt_NpcGossip = {}

--素秋师妹
tValentinesDay2015_Flirt_NpcGossip[10078] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["BeforeEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["BeforeEvent"]["Text1"] = "It`s been a long time since Master Hwang and I started training martial arts on the mountain. Boring...ing..., you know."
tValentinesDay2015_Flirt_NpcGossip[10078]["BeforeEvent"]["Text2"] = "~Anyway, we`re going to celebrate Valentine`s Day in Twin City, and share the `True Love Manual` with heroes above Level 80 or reborn from Feb. 11th to 14th."
tValentinesDay2015_Flirt_NpcGossip[10078]["BeforeEvent"]["Option1"] = "I~can`t~wait~to~see~it."

tValentinesDay2015_Flirt_NpcGossip[10078]["AfterEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["AfterEvent"]["Text1"] = "It`s good to see lovers being together. Master Hwang and I have to return to the mountain. Wish you everlasting love!"
tValentinesDay2015_Flirt_NpcGossip[10078]["AfterEvent"]["Option1"] = "See~you."

tValentinesDay2015_Flirt_NpcGossip[10078]["AtEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["AtEvent"]["Text1"] = "Happy Valentine`s Day! No matter you`re man or woman, take the chance to confess your love. Come and study the True Love Manual with your beloved, and let him/her understand your heart."
tValentinesDay2015_Flirt_NpcGossip[10078]["AtEvent"]["Option1"] = "Gimme~the~manual."
tValentinesDay2015_Flirt_NpcGossip[10078]["AtEvent"]["Option2"] = "I~lost~the~manual."
tValentinesDay2015_Flirt_NpcGossip[10078]["AtEvent"]["Option3"] = "Tell~me~more."
tValentinesDay2015_Flirt_NpcGossip[10078]["AtEvent"]["Option4"] = "Roses~of~Everlasting~for~you."
tValentinesDay2015_Flirt_NpcGossip[10078]["AtEvent"]["Option5"] = "I`ll~talk~to~you~later."

tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceEvent"]["Text1"] = "True Love Manual is a martial art book divided into 3 stages: Love Glance, Lingering Sense, and Forever Love. When you train the proficiency to a certain level, your stage will increase. Girls, come"
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceEvent"]["Text2"] = "~and claim True Love Manual (B) from me. Boys, go find Master Hwang for True Love Manual (A). The boy and the girl with the manuals can make a team and hunt monsters together to rise the proficiency."
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceEvent"]["Option1"] = "What`re~the~stages~for?"
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceEvent"]["Option2"] = "Learn~about~other~things."
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceEvent"]["Option3"] = "Got~it."

tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceReward"] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceReward"]["Text1"] = "For rewards. When you kill a monster, you`ll earn 1 proficiency point. 10 points to reach Love Glance stage, and get 20-minute EXP; 60 points for Lingering Sense, and get 40-minute EXP;"
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceReward"]["Text2"] = "~and 160 points for Forever Love, and get a Festival Joy Pack. You can also train it alone, but you`ll miss some EXP reward."
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceReward"]["Option1"] = "Learn~about~other~things."
tValentinesDay2015_Flirt_NpcGossip[10078]["IntroduceReward"]["Option2"] = "I~see."

tValentinesDay2015_Flirt_NpcGossip[10078]["GiveLongStemRose"] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["GiveLongStemRose"]["Text1"] = "Rose? Is it from Master Hwang? Alright. I really hope he could be a little more romantic sometimes."
tValentinesDay2015_Flirt_NpcGossip[10078]["GiveLongStemRose"]["Option1"] = "He~will~understand."

tValentinesDay2015_Flirt_NpcGossip[10078]["SysMsg"] = {}
tValentinesDay2015_Flirt_NpcGossip[10078]["SysMsg"]["WrongSex"] = "Sorry, you got the wrong person. Master Hwang has True Love Manual (A) for boys."
tValentinesDay2015_Flirt_NpcGossip[10078]["SysMsg"]["ItemGet"] = "You`ve got True Love Manual (B). I suggest you to team up with a boy who has True Love Manual (A), and study the manual by hunting monsters together."

--药师师兄
tValentinesDay2015_Flirt_NpcGossip[10079] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["BeforeEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["BeforeEvent"]["Text1"] = "Valentine`s Day is coming, and my lady, Autumn, wants to celebrate it in Twin City. From Feb. 11th to 14th, we`ll also share the `True Love Manual` with heroes above Level 80 or reborn."
tValentinesDay2015_Flirt_NpcGossip[10079]["BeforeEvent"]["Option1"] = "I~can`t~wait~to~see~it."

tValentinesDay2015_Flirt_NpcGossip[10079]["AfterEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["AfterEvent"]["Text1"] = "I don`t care what others think. I just want Autumn to be happy. Time to say goodbye."
tValentinesDay2015_Flirt_NpcGossip[10079]["AfterEvent"]["Option1"] = "Good~bye."

tValentinesDay2015_Flirt_NpcGossip[10079]["AtEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["AtEvent"]["Text1"] = "Happy Valentine`s Day! It`s really a good chance to confess your love, isn`t it? Come and study the True Love Manual with your beloved. I bet your love will be eternal after that."
tValentinesDay2015_Flirt_NpcGossip[10079]["AtEvent"]["Option1"] = "Gimme~the~manual."
tValentinesDay2015_Flirt_NpcGossip[10079]["AtEvent"]["Option2"] = "I~lost~the~manual."
tValentinesDay2015_Flirt_NpcGossip[10079]["AtEvent"]["Option3"] = "Tell~me~more."
tValentinesDay2015_Flirt_NpcGossip[10079]["AtEvent"]["Option4"] = "Roses~of~Everlasting~for~you."
tValentinesDay2015_Flirt_NpcGossip[10079]["AtEvent"]["Option5"] = "I`ll~talk~to~you~later."

tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceEvent"] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceEvent"]["Text1"] = "Actually, the True Love Manual is a martial art book in 3 stages: Love Glance, Lingering Sense, and Forever Love. When you train the proficiency to a certain level, your stage will increase. I have"
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceEvent"]["Text2"] = "~True Love Manual (A) for boys, while Lady Autumn has True Love Manual (B) for girls. The boy and the girl with the manuals can make a team and hunt monsters together to rise the proficiency."
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceEvent"]["Option1"] = "What`re~the~stages~for?"
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceEvent"]["Option2"] = "Learn~about~other~things."
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceEvent"]["Option3"] = "Got~it."

tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceReward"] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceReward"]["Text1"] = "For rewards. Killing each monster will earn yourself 1 proficiency point. 10 points to reach Love Glance stage, and get 20-minute EXP; 60 points for Lingering Sense, and get 40-minute EXP;"
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceReward"]["Text2"] = "~and 160 points for Forever Love, and get a Festival Joy Pack. You can also train it alone, but you`ll miss some EXP reward."
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceReward"]["Option1"] = "Learn~about~other~things."
tValentinesDay2015_Flirt_NpcGossip[10079]["IntroduceReward"]["Option2"] = "I~see."

tValentinesDay2015_Flirt_NpcGossip[10079]["GiveLongStemRose"] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["GiveLongStemRose"]["Text1"] = "What? Rose? Oh, no! Lady Autumn must have seen it, and she would misunderstand. Autumn? Come on, I can explain!"
tValentinesDay2015_Flirt_NpcGossip[10079]["GiveLongStemRose"]["Option1"] = "Sorry..."

tValentinesDay2015_Flirt_NpcGossip[10079]["SysMsg"] = {}
tValentinesDay2015_Flirt_NpcGossip[10079]["SysMsg"]["WrongSex"] = "Sorry, you got the wrong person. Master Hwang has True Love Manual (B) for girls."
tValentinesDay2015_Flirt_NpcGossip[10079]["SysMsg"]["ItemGet"] = "You`ve got True Love Manual (A). I suggest you to team up with a girl who has True Love Manual (B), and study the manual by hunting monsters together."

--心法
tValentinesDay2015_Flirt_ItemDialog = {}

tValentinesDay2015_Flirt_ItemDialog[1] = {}
tValentinesDay2015_Flirt_ItemDialog[1]["Text"] = "You`ve trained the proficiency of True Love Manual to %d point(s). You need to get 10 points to reach Love Glance stage."
tValentinesDay2015_Flirt_ItemDialog[1]["Option1"] = "I~see."

tValentinesDay2015_Flirt_ItemDialog[2] = {}
tValentinesDay2015_Flirt_ItemDialog[2]["Text"] = "You`ve trained the proficiency of True Love Manual to %d point(s). You need to get 60 points to reach Lingering Sense stage."
tValentinesDay2015_Flirt_ItemDialog[2]["Option1"] = "I~see."

tValentinesDay2015_Flirt_ItemDialog[3] = {}
tValentinesDay2015_Flirt_ItemDialog[3]["Text"] = "You`ve trained the proficiency of True Love Manual to %d point(s). You need to get 160 points to reach Forever Love stage."
tValentinesDay2015_Flirt_ItemDialog[3]["Option1"] = "I~see."

tValentinesDay2015_Flirt_ItemDialog[4] = {}
tValentinesDay2015_Flirt_ItemDialog[4]["Text"] = "You`ve acquired the essence of True Love. You can claim a reward, now."
tValentinesDay2015_Flirt_ItemDialog[4]["Option1"] = "Claim~my~reward."
tValentinesDay2015_Flirt_ItemDialog[4]["Option2"] = "I~see."

--系统提示内容
tValentinesDay2015_Flirt_SysMsgContent = {}
tValentinesDay2015_Flirt_SysMsgContent["ItemNotFoundMal"] = "You`ve discarded True Love Manual (A). If you want to continue the study, go claim another piece from Master Hwang (TwinCity 287,359)."
tValentinesDay2015_Flirt_SysMsgContent["ItemNotFoundFem"] = "You`ve discarded True Love Manual (B). If you want to continue the study, go claim another piece from Lady Autumn (TwinCity 284,359)."
tValentinesDay2015_Flirt_SysMsgContent["TaskComplete"] = "You`ve trained the True Love Manual to Forever Love stage. Go and claim your reward."
tValentinesDay2015_Flirt_SysMsgContent["ItemExpired"] = "Valentine`s Day has passed, and the magic of the True Love Manual has vanished."
tValentinesDay2015_Flirt_SysMsgContent["WrongTeam"] = "You can`t study True Love Manual in current team.  Group training of True Love Manual should be a team of a boy and a girl."
tValentinesDay2015_Flirt_SysMsgContent["TaskFinished"] = "You seem quite happy. You must have trained the True Love Manual to Forever Love stage."
tValentinesDay2015_Flirt_SysMsgContent["BagFullItem"] = "Your inventory is full. You need to make some room, first."
tValentinesDay2015_Flirt_SysMsgContent["BagFullReward"] = "Your inventory is full. You need to make some room for the reward, first."
tValentinesDay2015_Flirt_SysMsgContent["ItemExist"] = "You`ve already claimed a True Love Manual. Go and study it with your beloved who also has a True Love Manual."
tValentinesDay2015_Flirt_SysMsgContent["LevelNotEnough"] = "You`re still too green to understand the meaning of True Love Manual. You should reach at least Level 80, first."
tValentinesDay2015_Flirt_SysMsgContent["RewardGet"] = "Congrats! You and your partner have trained the True Love Manual to Forever Love stage, and received a Festival Joy Pack and a Love Rose."

--奖励表
tValentinesDay2015_Flirt_Reward = {}
tValentinesDay2015_Flirt_Reward[1] = {}
tValentinesDay2015_Flirt_Reward[1]["SysMsg1"] = "You and your partner have trained the True Love Manual to Love Glance stage, and received 20 minutes of EXP!"
tValentinesDay2015_Flirt_Reward[1]["SysMsg2"] = "You and your partner have trained the True Love Manual to Love Glance stage, and received 10 Study Points!"
tValentinesDay2015_Flirt_Reward[2] = {}
tValentinesDay2015_Flirt_Reward[2]["SysMsg1"] = "You and your partner have trained the True Love Manual to Lingering Sense stage, and received 40 minutes of EXP!"
tValentinesDay2015_Flirt_Reward[2]["SysMsg2"] = "You and your partner have trained the True Love Manual to Lingering Sense stage, and received 20 Study Points!"

------------------------------------------------------------------------------------
--Name:			[征服][节日任务]2015年情人节之甜蜜约会之非诚勿扰（2.11-2.14）
--Creator: 		魏贻逵
--Created:		2014/12/03
------------------------------------------------------------------------------------

--提示
tValentinesDay2015_feichengwulao_Text = {}
tValentinesDay2015_feichengwulao_Text["MsgBox"] = {}
tValentinesDay2015_feichengwulao_Text["MsgBox"]["chgmap"] = "You entered Matchmaker Yiu`s office, and got impressed by the luxury decoration."
tValentinesDay2015_feichengwulao_Text["MsgBox"]["chgmap1"] = "Thanks for your help! Don`t forget to claim your reward from my boss."
tValentinesDay2015_feichengwulao_Text["MsgBox"]["Complete"] = "You`ve already successfully brought the two persons together. Go and make another match."
tValentinesDay2015_feichengwulao_Text["MsgBox"]["Complete1"] = "You`ve successfully made 3 matches. Go and claim a reward from Matchmaker Yiu."
tValentinesDay2015_feichengwulao_Text["MsgBox"]["clear"] = "It`s late. You were teleported out."


--对白
tValentinesDay2015_feichengwulao_Text[17321] = {}
tValentinesDay2015_feichengwulao_Text[17321]["Text111"] = "It`s hard to be a good matchmaker, and even harder to be a good male matchmaker."
tValentinesDay2015_feichengwulao_Text[17321]["Text112"] = "~To make perfect matches, I`ve racked my brains. Hey, are you free on Feb. 11th - 14th?"
tValentinesDay2015_feichengwulao_Text[17321]["Text113"] = "~It`s always busy on Valentine`s Day, and I`ll need your help."
tValentinesDay2015_feichengwulao_Text[17321]["Option1"] = "Sounds~funny."

tValentinesDay2015_feichengwulao_Text[17321]["Text121"] = "Thanks for your help! I`ve successfully brought many couples together on Valentine`s Day,"
tValentinesDay2015_feichengwulao_Text[17321]["Text122"] = "~and I felt so good."
tValentinesDay2015_feichengwulao_Text[17321]["Option2"] = "You`re~a~good~matchmaker."

tValentinesDay2015_feichengwulao_Text[17321]["Text131"] = "Did you see my assistant, Fat? Come on! If nobody helps me, I`ll die. There are so many single"
tValentinesDay2015_feichengwulao_Text[17321]["Text132"] = "~men and women waiting for a match in my office! Have you reached Level 80 or got reborn? If so,"
tValentinesDay2015_feichengwulao_Text[17321]["Text133"] = "~come and give me a hand from Feb. 11th to 14th. I really, really, really need help!"
tValentinesDay2015_feichengwulao_Text[17321]["Text134"] = ""
tValentinesDay2015_feichengwulao_Text[17321]["Option3"] = "I~can~help."
tValentinesDay2015_feichengwulao_Text[17321]["Option4"] = "Claim~my~reward."
tValentinesDay2015_feichengwulao_Text[17321]["Option5"] = "How~to~make~a~match?"
tValentinesDay2015_feichengwulao_Text[17321]["Option6"] = "I`m~also~waiting~for~love."

--tValentinesDay2015_feichengwulao_Text[17321]["Text311"] = "Um... It`s not time for the work, now. Look, I want you to work in my office"
--tValentinesDay2015_feichengwulao_Text[17321]["Text312"] = "~from 19:00 to 23:00, everyday during the celebration. You know, shyness goes when night comes."
--tValentinesDay2015_feichengwulao_Text[17321]["Option7"] = "You`re~right."

tValentinesDay2015_feichengwulao_Text[17321]["Text321"] = "Wait, you haven`t reached Level 80 nor got reborn. Come on, you`re still too green"
tValentinesDay2015_feichengwulao_Text[17321]["Text322"] = "~to deal with such a profound thing. Keep practicing."
tValentinesDay2015_feichengwulao_Text[17321]["Option8"] = "Alright."

tValentinesDay2015_feichengwulao_Text[17321]["Text331"] = "Hey, I knew you. You`ve already helped me, today. I just can`t bother you any more."
tValentinesDay2015_feichengwulao_Text[17321]["Text332"] = "~You should accompany your beloved for a wonderful festival."
tValentinesDay2015_feichengwulao_Text[17321]["Option9"] = "You`re~right."

tValentinesDay2015_feichengwulao_Text[17321]["Text341"] = "Buddy, you`ve helped make some matches today. Thank you very much! You deserve a nice reward."
tValentinesDay2015_feichengwulao_Text[17321]["Text342"] = "~Hey, don`t forget to say love to the people you love."
tValentinesDay2015_feichengwulao_Text[17321]["Option10"] = "Okay."

tValentinesDay2015_feichengwulao_Text[17321]["Text411"] = "Eh? You`ve already claimed the reward, today."
tValentinesDay2015_feichengwulao_Text[17321]["Text412"] = ""
tValentinesDay2015_feichengwulao_Text[17321]["Option11"] = "Sorry,~I~forgot."

tValentinesDay2015_feichengwulao_Text[17321]["Text421"] = "Wait, you haven`t made 3 matches for me in the office. Finish the job first, and you can claim a reward from me."
tValentinesDay2015_feichengwulao_Text[17321]["Option12"] = "I~see."

tValentinesDay2015_feichengwulao_Text[17321]["Text431"] = "Your inventory is full. You need to make some room, first."
tValentinesDay2015_feichengwulao_Text[17321]["Option13"] = "I`ll~do~it~now."

tValentinesDay2015_feichengwulao_Text[17321]["Text441"] = "Ah, you do have a gift for making matches! Thanks for your help. Here`s a reward for you."
tValentinesDay2015_feichengwulao_Text[17321]["Text442"] = ""
tValentinesDay2015_feichengwulao_Text[17321]["Option14"] = "Thanks."

tValentinesDay2015_feichengwulao_Text[17321]["Text511"] = "In my office, you`ll see many lovers-to-be dating. I want you to do something to enhance the"
tValentinesDay2015_feichengwulao_Text[17321]["Text512"] = "~atmosphere of love around them, such as playing Guzheng, turning on the Gramophone, painting"
tValentinesDay2015_feichengwulao_Text[17321]["Text513"] = "~for them on the table, dimming the candle light, taking flowers from the Dried Flower Vase..."
tValentinesDay2015_feichengwulao_Text[17321]["Text514"] = "~If you successfully make 3 matches, you can claim a reward from me."
tValentinesDay2015_feichengwulao_Text[17321]["Option15"] = "It`s~quite~interesting."

tValentinesDay2015_feichengwulao_Text[17322] = {}
tValentinesDay2015_feichengwulao_Text[17322]["Text111"] = "Assistant Fat often gets lazy, leaving me working alone like a dog. The boss is also looking for him."
tValentinesDay2015_feichengwulao_Text[17322]["Text112"] = "~Thanks for your assistance."
tValentinesDay2015_feichengwulao_Text[17322]["Text113"] = ""
tValentinesDay2015_feichengwulao_Text[17322]["Option1"] = "I~want~to~leave~the~office."
tValentinesDay2015_feichengwulao_Text[17322]["Option2"] = "How~to~make~a~match?"
tValentinesDay2015_feichengwulao_Text[17322]["Option3"] = "You`re~welcome."

tValentinesDay2015_feichengwulao_Text[17322]["Text121"] = "It seems you haven`t made 3 matches in the office. Are you sure you want to leave now?"
tValentinesDay2015_feichengwulao_Text[17322]["Option4"] = "Yes,~I`ve~decided."
tValentinesDay2015_feichengwulao_Text[17322]["Option5"] = "No,~not~now."

tValentinesDay2015_feichengwulao_Text[17322]["Text211"] = "As you can see, there are single men and women here. You need to do something to enhance the"
tValentinesDay2015_feichengwulao_Text[17322]["Text212"] = "~atmosphere of love around them, such as playing Guzheng, turning on the Gramophone, painting"
tValentinesDay2015_feichengwulao_Text[17322]["Text213"] = "~for them on the table, dimming the candle light, taking flowers from the Dried Flower Vase..."
tValentinesDay2015_feichengwulao_Text[17322]["Text214"] = "~If you successfully make 3 matches, you can claim a reward from Matchmaker Yiu. Got it?"
tValentinesDay2015_feichengwulao_Text[17322]["Option6"] = "Yeah."

tValentinesDay2015_feichengwulao_Text[17323] = {}
tValentinesDay2015_feichengwulao_Text[17323]["Text111"] = "If there is western music here, we can dance together."
tValentinesDay2015_feichengwulao_Text[17323]["Option1"] = "Got~it!"
tValentinesDay2015_feichengwulao_Text[17323]["Text121"] = "Wow, you`re the best dancer I`ve ever seen."
tValentinesDay2015_feichengwulao_Text[17323]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17324] = {}
tValentinesDay2015_feichengwulao_Text[17324]["Text111"] = "I just happened to learn some western dance. It`ll be nice if we have a chance to dance together."
tValentinesDay2015_feichengwulao_Text[17324]["Option1"] = "Got~it!"
tValentinesDay2015_feichengwulao_Text[17324]["Text121"] = "My lady, you`re a very good dancer. You don`t know how beautiful you are."
tValentinesDay2015_feichengwulao_Text[17324]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17325] = {}
tValentinesDay2015_feichengwulao_Text[17325]["Text111"] = "The moon is beautiful, isn`t it?"
tValentinesDay2015_feichengwulao_Text[17325]["Option1"] = "Boring..."
tValentinesDay2015_feichengwulao_Text[17325]["Text121"] = "I really love the music played with Guzheng. It has beautiful sound and rich meaning."
tValentinesDay2015_feichengwulao_Text[17325]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17326] = {}
tValentinesDay2015_feichengwulao_Text[17326]["Text111"] = "What a beautiful night!"
tValentinesDay2015_feichengwulao_Text[17326]["Option1"] = "Got~it!"
tValentinesDay2015_feichengwulao_Text[17326]["Text121"] = "Ha, I love Guzheng too. See, we share the same hobby."
tValentinesDay2015_feichengwulao_Text[17326]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17327] = {}
tValentinesDay2015_feichengwulao_Text[17327]["Text111"] = "Hey... Why are you staring at me? Come on... I`ll be shy."
tValentinesDay2015_feichengwulao_Text[17327]["Option1"] = "Got~it!"
tValentinesDay2015_feichengwulao_Text[17327]["Text121"] = "We look like a perfect match in the picture, don`t we?"
tValentinesDay2015_feichengwulao_Text[17327]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17328] = {}
tValentinesDay2015_feichengwulao_Text[17328]["Text111"] = "For a pretty girl like you, I wish this moment could be forever."
tValentinesDay2015_feichengwulao_Text[17328]["Option1"] = "Got~it!"
tValentinesDay2015_feichengwulao_Text[17328]["Text121"] = "If we can be together... Yeah, we`ll be together forever."
tValentinesDay2015_feichengwulao_Text[17328]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17329] = {}
tValentinesDay2015_feichengwulao_Text[17329]["Text111"] = "Did you smell the flower? It`s good."
tValentinesDay2015_feichengwulao_Text[17329]["Option1"] = "Boring..."
tValentinesDay2015_feichengwulao_Text[17329]["Text121"] = "Ha, you`re really a dull boy. Why do you take my hand till now..."
tValentinesDay2015_feichengwulao_Text[17329]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17330] = {}
tValentinesDay2015_feichengwulao_Text[17330]["Text111"] = "Eh... Why do you keep your hands behind your backs?"
tValentinesDay2015_feichengwulao_Text[17330]["Option1"] = "Boring..."
tValentinesDay2015_feichengwulao_Text[17330]["Text121"] = "Ha, I finally took your hand, my love."
tValentinesDay2015_feichengwulao_Text[17330]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17331] = {}
tValentinesDay2015_feichengwulao_Text[17331]["Text111"] = "The candle light is too bright, isn`t it?"
tValentinesDay2015_feichengwulao_Text[17331]["Option1"] = "Got~it!"
tValentinesDay2015_feichengwulao_Text[17331]["Text121"] = "The light is just good. You look more beautiful, now."
tValentinesDay2015_feichengwulao_Text[17331]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17332] = {}
tValentinesDay2015_feichengwulao_Text[17332]["Text111"] = "What does Matchmaker Yiu really think? The candle light is so bright that I feel uncomfortable."
tValentinesDay2015_feichengwulao_Text[17332]["Option1"] = "Boring..."
tValentinesDay2015_feichengwulao_Text[17332]["Text121"] = "Now, I feel better with the candle light."
tValentinesDay2015_feichengwulao_Text[17332]["Option2"] = "Good!~I~made~it!"

tValentinesDay2015_feichengwulao_Text[17409] = {}
tValentinesDay2015_feichengwulao_Text[17409]["Text111"] = "It seems the man and the woman want to be together. Hurry and create an opportunity for them to dance together."
tValentinesDay2015_feichengwulao_Text[17409]["Option1"] = "Turn~on~the~Gramophone."
tValentinesDay2015_feichengwulao_Text[17409]["Option2"] = "They~should~not~be~together."
tValentinesDay2015_feichengwulao_Text[17409]["Text121"] = "As the music started, the man and the woman embraced together and danced. You successfully made a match!"
tValentinesDay2015_feichengwulao_Text[17409]["Option3"] = "It`s~so~good."

tValentinesDay2015_feichengwulao_Text[17410] = {}
tValentinesDay2015_feichengwulao_Text[17410]["Text111"] = "The man the woman are so quiet. Hurry and create an opportunity for them to chat."
tValentinesDay2015_feichengwulao_Text[17410]["Option1"] = "Play~Guzheng."
tValentinesDay2015_feichengwulao_Text[17410]["Option2"] = "They~should~not~be~together."
tValentinesDay2015_feichengwulao_Text[17410]["Text121"] = "With the beautiful melody and moonlight, the man the woman found they shared the same hobby and started chatting. You successfully made a match!"
tValentinesDay2015_feichengwulao_Text[17410]["Option3"] = "It`s~so~good."

tValentinesDay2015_feichengwulao_Text[17411] = {}
tValentinesDay2015_feichengwulao_Text[17411]["Text111"] = "The date between the man and the woman goes well. Draw a picture for them to enhance the atmosphere of love."
tValentinesDay2015_feichengwulao_Text[17411]["Option1"] = "Draw~them~a~picture."
tValentinesDay2015_feichengwulao_Text[17411]["Option2"] = "They~should~not~be~together."
tValentinesDay2015_feichengwulao_Text[17411]["Text121"] = "The man and the woman got impressed by the picture you drew for them, and decided to be together. You successfully made a match!"
tValentinesDay2015_feichengwulao_Text[17411]["Option3"] = "It`s~so~good."

tValentinesDay2015_feichengwulao_Text[17412] = {}
tValentinesDay2015_feichengwulao_Text[17412]["Text111"] = "It seems both the man and the woman are willing to be hand in hand. Take some flowers from the vase, and throw them into the air."
tValentinesDay2015_feichengwulao_Text[17412]["Option1"] = "Throw~flowers."
tValentinesDay2015_feichengwulao_Text[17412]["Option2"] = "They~should~not~be~together."
tValentinesDay2015_feichengwulao_Text[17412]["Text121"] = "The man and the woman smelt the flower petals in the air, and uncontrollably took each other`s hand. You successfully made a match!"
tValentinesDay2015_feichengwulao_Text[17412]["Option3"] = "It`s~so~good."

tValentinesDay2015_feichengwulao_Text[17413] = {}
tValentinesDay2015_feichengwulao_Text[17413]["Text111"] = "The bright candle light makes the man and the woman feel shy and anxious."
tValentinesDay2015_feichengwulao_Text[17413]["Option1"] = "Dim~the~candle~light."
tValentinesDay2015_feichengwulao_Text[17413]["Option2"] = "They~should~not~be~together."
tValentinesDay2015_feichengwulao_Text[17413]["Text121"] = "In the dimness of the light, the man kissed the woman. You successfully made a match!"
tValentinesDay2015_feichengwulao_Text[17413]["Option3"] = "It`s~so~good."




------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]春节活动（元宵）之张灯结彩贺元宵（3.1—3.5）
--Purpose:	春节活动（元宵）之张灯结彩贺元宵（3.1—3.5）
--Creator: 	陈浩文
--Created:	2014/12/17
------------------------------------------------------------------------------------
-- 命名前缀
-- FestivalSpring2015_Zhangdengjiecai_
-------------采莲姑娘NPC对白-------------
-- //采莲姑娘
tFestivalSpring2015_Zhangdengjiecai_Text = {}
tFestivalSpring2015_Zhangdengjiecai_Text[10753] = {}

-- 活动前对白
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text111"] = "Spring has come, and we`re going to celebrate the Lantern Festival on March 1st to 5th."
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text112"] = "~Guess what we are going to do on the festival? If you`ve reached Level 80 or got reborn,"
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text113"] = "~come and claim a Wish Lantern from me. Each one who lights up the lantern will receive blessing and a gift from me."

-- 活动中对白
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text121"] = "During the Lantern Festival, people will go out at night, and carry paper lanterns. See, I`ve prepared"
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text122"] = "~some Wish Lanterns for heroes above Level 80 or reborn. If you`re qualified, you can light the"
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text123"] = "~lantern one time in a day, and receive blessing and a gift from me."
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text124"] = ""


-- 活动后对白
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text131"] = "Happy Lantern Festival! May you a happy life in the New Year!"
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text132"] = ""

-- 等级不足
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text211"] = "Sorry, only the heroes who`ve reached Level 80 or got reborn are able to light the Wish Lantern."

-- 已完成点灯对白 
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Text221"] = "You`ve already lighted the lantern, today. If you`re still interested, please come and retry tomorrow."

--选项
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Option11"] = "Great!"
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Option12"] = "Light~the~Wish~Lantern."
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Option13"] = "I`ll~talk~to~you~later."
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Option14"] = "Thanks!"
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Option21"] = "Alright."
tFestivalSpring2015_Zhangdengjiecai_Text[10753]["Option22"] = "Alright."
-------------祝福灯笼NPC对白-------------

-- //祝福灯笼
tFestivalSpring2015_Zhangdengjiecai_Text[10754] = {}

-- 未活动中对白
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text111"] = "This is a beautiful lantern. Wish you a colorful life in the New Year!"

-- 接受任务对白
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text121"] = "This is a beautiful lantern. Wish you a colorful life in the New Year!"

-- 已完成点灯对白 
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text131"] = "You`ve already lighted the lantern, today. If you`re still interested, please come and retry tomorrow."

--【点灯。】
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text211"] = "Sorry, only the heroes who`ve reached Level 80 or got reborn are able to light the Wish Lantern."
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text221"] = "Your inventory is full. You need to make some room, first."
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text231"] = "Wish you dream come true! The Wish Lantern blessed you with a Festival Joy Pack and 60 minutes of EXP!"
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text241"] = "Wish you a harvest in the New Year! The Wish Lantern blessed you with a Festival Joy Pack and 1 day of Heaven Blessing!"
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text251"] = "Wish you a splendid life! The Wish Lantern blessed you with a Festival Joy Pack and 30 Study Points!"
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Text261"] = "Wish you a happy life! The Wish Lantern blessed you with a Festival Joy Pack and 20 Chi Points!"

--选项
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Option11"] = "I~see."
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Option12"] = "Light~the~lantern."
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Option13"] = "Alright."
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Option14"] = "I~see."
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Option21"] = "Okay."
tFestivalSpring2015_Zhangdengjiecai_Text[10754]["Option22"] = "Good!"
----------------特效文字--------------------
--烟花特效文字
tFestivalSpring2015_Zhangdengjiecai_Effect={}
tFestivalSpring2015_Zhangdengjiecai_Effect["Effect"]="Happy~Lantern~Festival!"
------------------------------------------------------------------------------------



------------------------------------------------------------------------------------
--Name:		[简体征服][节日任务]元宵节之横扫八方战元宵
--Purpose:	元宵节之横扫八方战元宵
--Creator: 	严振飞
--Created:	2014/12/09
------------------------------------------------------------------------------------

------------------------------------------NPC对白-------------------------------------------
-- //大将军卫无敌

tFestivalSpring2015_FestivalWar_Text = {}
tFestivalSpring2015_FestivalWar_Text[17432] = {}
-- 活动前对白
tFestivalSpring2015_FestivalWar_Text[17432]["Text111"] = "You must have heard about the Lantern Festival celebration from March 1st to 5th. The more the people,"
tFestivalSpring2015_FestivalWar_Text[17432]["Text112"] = "~the more the danger. I just worry something bad may happen during the celebration. If you`ve reached Level 80"
tFestivalSpring2015_FestivalWar_Text[17432]["Text113"] = "~or got reborn, come and give me a hand at that time."
-- 活动中对白
tFestivalSpring2015_FestivalWar_Text[17432]["Text121"] = "What a busy festival! People are everywhere in the city. What`s worse, the noise has attracted monsters"
tFestivalSpring2015_FestivalWar_Text[17432]["Text122"] = "~all around the land. Look, I`ve dispatched a team to block the monsters in the Evil Field. However, it`s not"
tFestivalSpring2015_FestivalWar_Text[17432]["Text123"] = "~enough. From March 1st to 5th, I need helps from Level 80+ or reborn heroes. I`ll give a nice pay for that."
-- 活动后对白（通用）
tFestivalSpring2015_FestivalWar_Text[17432]["Text131"] = "We finally drove away the monsters. Thanks to everyone who had ever lent me a hand."


-- 【前往妖魔战场。】
tFestivalSpring2015_FestivalWar_Text[17432]["Text211"] = "Come on, you should reach at least Level 80 or get reborn to deal with those monsters. They`re much more fierce than you imagined."
tFestivalSpring2015_FestivalWar_Text[17432]["Text221"] = "Hey, you`ve already given a help in the Evil Field, today. Why not take a rest, and join the fight again tomorrow?"
tFestivalSpring2015_FestivalWar_Text[17432]["Text231"] = "I just found that you`ve successfully defeated the monsters in the Evil Field. You can claim the reward, now."

-- 【返回双龙城。】
tFestivalSpring2015_FestivalWar_Text[17432]["Text311"] = "Are you sure you want to return to Twin City? You haven`t smashed the monsters` attack."
tFestivalSpring2015_FestivalWar_Text[17432]["Text321"] = "Thanks for smashing the monsters` attack! I`ll send you to Twin City, right now."

-- 【兑换皮毛。】
tFestivalSpring2015_FestivalWar_Text[17432]["Text411"] = "Eh? You`ve already claimed today`s reward, haven`t you?"
tFestivalSpring2015_FestivalWar_Text[17432]["Text421"] = "Buddy, you should break down the monsters` attack before you can claim a reward from me."
tFestivalSpring2015_FestivalWar_Text[17432]["Text431"] = "Your inventory is too full to contain anything. You need to make some room, first."
tFestivalSpring2015_FestivalWar_Text[17432]["Text441"] = "You`ve showed your courage to me. Look, I have a Festival Joy Pack for you. Just take it."


-- 【愿闻其详。】
tFestivalSpring2015_FestivalWar_Text[17432]["Text521"] = "If you can help, I`ll send you to the Evil Field one time in a day. Defeat the monsters in the field, and you can"
tFestivalSpring2015_FestivalWar_Text[17432]["Text522"] = "~claim a Festival Joy Pack from me. Listen, those monsters are different. They know how to fight in a formation."
tFestivalSpring2015_FestivalWar_Text[17432]["Text523"] = "~I want you to do what I tell you during the fight. If you did something wrong, you need to start over. "



------------------------------------------大将军卫无敌--选项-------------------------------------------
tFestivalSpring2015_FestivalWar_Text[17432]["Option11"] = "Sure."
tFestivalSpring2015_FestivalWar_Text[17432]["Option12"] = "Enter~the~Evil~Field."
tFestivalSpring2015_FestivalWar_Text[17432]["Option13"] = "Return~to~Twin~City."
tFestivalSpring2015_FestivalWar_Text[17432]["Option14"] = "Claim~my~reward."
tFestivalSpring2015_FestivalWar_Text[17432]["Option15"] = "Tell~me~more."
tFestivalSpring2015_FestivalWar_Text[17432]["Option16"] = "I`ll~talk~to~you~later."
tFestivalSpring2015_FestivalWar_Text[17432]["Option17"] = "Great!"
tFestivalSpring2015_FestivalWar_Text[17432]["Option21"] = "Alright."
tFestivalSpring2015_FestivalWar_Text[17432]["Option22"] = "Yeah."
tFestivalSpring2015_FestivalWar_Text[17432]["Option23"] = "Okay."
tFestivalSpring2015_FestivalWar_Text[17432]["Option31"] = "I~have~to~leave."
tFestivalSpring2015_FestivalWar_Text[17432]["Option32"] = "I~changed~my~mind."
tFestivalSpring2015_FestivalWar_Text[17432]["Option33"] = "See~you."
tFestivalSpring2015_FestivalWar_Text[17432]["Option41"] = "Sorry,~I~forgot."
tFestivalSpring2015_FestivalWar_Text[17432]["Option42"] = "I~see."
tFestivalSpring2015_FestivalWar_Text[17432]["Option43"] = "I`ll~do~it~now."
tFestivalSpring2015_FestivalWar_Text[17432]["Option44"] = "Thanks!"
tFestivalSpring2015_FestivalWar_Text[17432]["Option51"] = "Learn~about~something~else."
tFestivalSpring2015_FestivalWar_Text[17432]["Option52"] = "Got~it."


-------------其他对白
tFestivalSpring2015_FestivalWar_Text["Join"] = "You`ve entered the Evil Field. Get yourself ready for combat!"
tFestivalSpring2015_FestivalWar_Text["Leave"] = "You`ve left the Evil Field, and returned to Twin City."
tFestivalSpring2015_FestivalWar_Text["KillError"] = "Oops! You messed up the fighting pace. Make sure you kill the correct monster in a correct order."
tFestivalSpring2015_FestivalWar_Text["Finish"] = "You`ve destroyed the formation formed by the monsters in the Evil Field. Go and claim a reward from General Wai!"
--怪物提示
tFestivalSpring2015_FestivalWar_Text["Monster"] = {}
tFestivalSpring2015_FestivalWar_Text["Monster"][1] = "Currently, the Flame Leopard is the key of the monster formation. Hurry and kill 1 Flame Leopard!"
tFestivalSpring2015_FestivalWar_Text["Monster"][2] = "Currently, the Jade Snake is the key of the monster formation. Hurry and kill 1 Jade Snake!"
tFestivalSpring2015_FestivalWar_Text["Monster"][3] = "Currently, the Lava Devil is the key of the monster formation. Hurry and kill 1 Lava Devil!"
tFestivalSpring2015_FestivalWar_Text["Monster"][4] = "Currently, the Water Ghost is the key of the monster formation. Hurry and kill 1 Water Ghost!"
tFestivalSpring2015_FestivalWar_Text["Monster"][5] = "Currently, the Infernal Lord is the key of the monster formation. Hurry and kill 1 Infernal Lord!"


------------------------------------------------------------------------------------
--Name:		[简体征服][节日任务]元宵节之热热闹闹舞龙灯
--Purpose:	元宵节之热热闹闹舞龙灯
--Creator: 	严振飞
--Created:	2014/12/09
------------------------------------------------------------------------------------

------------------------------------------NPC对白-------------------------------------------
-- //舞者小丸子

tFestivalSpring2015_DragonLight_Text = {}
tFestivalSpring2015_DragonLight_Text[17436] = {}
-- 活动前对白
tFestivalSpring2015_DragonLight_Text[17436]["Text111"] = "The Lantern Festival will be celebrated from March 1st to 5th. If you`re above Level 80 or reborn,"
tFestivalSpring2015_DragonLight_Text[17436]["Text112"] = "~you should never miss the dragon dance performance on the festival. Come and pray for a harvest!"
-- 活动后对白
tFestivalSpring2015_DragonLight_Text[17436]["Text121"] = "Many people have revealed a talent for the dragon dance. Should I make a dragon dance contest?"
-- 活动中对白
tFestivalSpring2015_DragonLight_Text[17436]["Text131"] = "Happy Lantern Festival! If you`re above Level 80 or reborn and you`re longing for good luck in the New Year,"
tFestivalSpring2015_DragonLight_Text[17436]["Text132"] = "~make a team with your friends and give us a dragon dance! I`m looking forward to your performance"
tFestivalSpring2015_DragonLight_Text[17436]["Text133"] = "~between March 1st and 5th."


-- 【弟兄们来舞龙喽！】
tFestivalSpring2015_DragonLight_Text[17436]["Text211"] = "Make sure you`re in a team, first. The dragon dance requires teamwork, and you can`t do it alone."
tFestivalSpring2015_DragonLight_Text[17436]["Text212"] = "It`s impossible to wield the dragon lantern by yourself alone. It`s quite a big thing."
tFestivalSpring2015_DragonLight_Text[17436]["Text221"] = "Hey, where are your teammates? The dragon dance requires teamwork. Go and team up with your friends!"
tFestivalSpring2015_DragonLight_Text[17436]["Text222"] = ""
tFestivalSpring2015_DragonLight_Text[17436]["Text231"] = "Sorry, you`re not the team leader. If you`re interested in the dragon dance, go and ask your team leader"
tFestivalSpring2015_DragonLight_Text[17436]["Text232"] = "~to sign up for the show."
tFestivalSpring2015_DragonLight_Text[17436]["Text241"] = "I`m afraid you can`t sign up for the dragon lion show. Make sure each one in your team has reached Level 80 or got reborn,"
tFestivalSpring2015_DragonLight_Text[17436]["Text242"] = "~and none of them has joined the event today."
tFestivalSpring2015_DragonLight_Text[17436]["Text243"] = ""
tFestivalSpring2015_DragonLight_Text[17436]["Text251"] = "You`re still too green to wield the dragon lantern. I await your return when you reach Level 80 or get reborn."

-- 【领取奖赏。】
tFestivalSpring2015_DragonLight_Text[17436]["Text311"] = "Sorry, I didn`t see you in the dragon dance show."
tFestivalSpring2015_DragonLight_Text[17436]["Text321"] = "You`ve already claimed the reward today, haven`t you?"
tFestivalSpring2015_DragonLight_Text[17436]["Text331"] = "Buddy, you`re carrying a full bag. Why not make some room in your inventory, first?"
tFestivalSpring2015_DragonLight_Text[17436]["Text341"] = "Wow, you guys really impressed me with such a wonderful dance! You deserve to be rewarded."
tFestivalSpring2015_DragonLight_Text[17436]["Text342"] = ""


-- 【我该怎么舞龙？】
tFestivalSpring2015_DragonLight_Text[17436]["Text411"] = "It`s easy. Just team up with at least one of your friends who`ve reached Level 80 or got reborn."
tFestivalSpring2015_DragonLight_Text[17436]["Text412"] = "~Then, ask the team leader to sign up with me. After the show, each one in the team can claim a reward from me."




------------------------------------------舞者小丸子--选项-------------------------------------------
tFestivalSpring2015_DragonLight_Text[17436]["Option11"] = "Sounds~good."
tFestivalSpring2015_DragonLight_Text[17436]["Option12"] = "Good~idea!"
tFestivalSpring2015_DragonLight_Text[17436]["Option13"] = "Dance!~It`s~my~stage."
tFestivalSpring2015_DragonLight_Text[17436]["Option14"] = "Claim~my~reward."
tFestivalSpring2015_DragonLight_Text[17436]["Option15"] = "How~to~dance?"
tFestivalSpring2015_DragonLight_Text[17436]["Option16"] = "I`m~just~passing~by."
tFestivalSpring2015_DragonLight_Text[17436]["Option21"] = "Okay."
tFestivalSpring2015_DragonLight_Text[17436]["Option22"] = "Okay."
tFestivalSpring2015_DragonLight_Text[17436]["Option23"] = "I~see."
tFestivalSpring2015_DragonLight_Text[17436]["Option24"] = "I~see."
tFestivalSpring2015_DragonLight_Text[17436]["Option25"] = "I`ll~talk~to~you~later."
tFestivalSpring2015_DragonLight_Text[17436]["Option31"] = "Maybe,~I~got~it~wrong."
tFestivalSpring2015_DragonLight_Text[17436]["Option32"] = "Sorry,~I~forgot."
tFestivalSpring2015_DragonLight_Text[17436]["Option33"] = "I`ll~do~it~now."
tFestivalSpring2015_DragonLight_Text[17436]["Option34"] = "Thanks!"
tFestivalSpring2015_DragonLight_Text[17436]["Option41"] = "I~see."
-------------其他对白
tFestivalSpring2015_DragonLight_Text["Finish"] = "The dragon dance is impressive! Go and claim a reward from Dancer Bella!"




------------------------------------------------------------------------------------
--Name:		[简体征服][节日任务]元宵节之砰砰啪啪拆爆竹
--Purpose:	元宵节之砰砰啪啪拆爆竹
--Creator: 	严振飞
--Created:	2014/12/09
------------------------------------------------------------------------------------

------------------------------------------NPC对白-------------------------------------------
-- //爆竹大师张巧手

tFestivalSpring2015_RemoveFireks_Text = {}
tFestivalSpring2015_RemoveFireks_Text[17416] = {}
-- 活动前对白
tFestivalSpring2015_RemoveFireks_Text[17416]["Text111"] = "I got a bad news. The Predator stole a box of firecrackers when it fled away. I know the monster."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text112"] = "~It wouldn`t let things go easily. I`m afraid it`ll come back to revenge during the Lantern Festival"
tFestivalSpring2015_RemoveFireks_Text[17416]["Text113"] = "~celebration from March 1st to 5th. If you`re above Level 80 or reborn, pay me a visit at that time."
-- 活动中对白
tFestivalSpring2015_RemoveFireks_Text[17416]["Text121"] = "Emergency! The Predator stole a box of firecrackers, and threw them everywhere on Wind Plain."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text122"] = "~If you`re above Level 80 or reborn, come and clear these explosives between March 1st and 5th."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text123"] = "~Your help will get paid. By the way, if you happen to collect Predator`s Tooth, give it to me for a gift."
-- 活动后对白（通用）
tFestivalSpring2015_RemoveFireks_Text[17416]["Text131"] = "Thanks for clearing the Risky Firecrackers! We can finally enjoy the festival, now."


-- 【我也来帮忙！】
tFestivalSpring2015_RemoveFireks_Text[17416]["Text211"] = "It`s too dangerous for you to clear the explosives. Keep practicing, and come back when you reach Level 80 or get reborn."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text221"] = "You`ve already cleared 10 Risky Firecrackers for me, today. It`s enough. Don`t push yourself too hard."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text231"] = "Um? The Disassemble Device is just in your inventory. You don`t need to claim another one."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text241"] = "Buddy, your inventory is full! I have a Disassemble Device for you. Please make some room, first."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text251"] = "Master Zhang gave you a Disassemble Device. Go and clear the Risky Firecrackers on Wind Plain."

-- 【领取奖励。】
tFestivalSpring2015_RemoveFireks_Text[17416]["Text311"] = "You look quite familiar. Oh, you helped me clear 10 Risky Firecrackers today. It`s enough. Please take a rest."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text321"] = "Have you cleared 10 Risky Firecrackers? It seems the answer is `NO`. Then, finish your job, first."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text331"] = "Your inventory is full, you know? Please make some room for the reward, first."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text341"] = "Well done! Thanks for your help. See, here is a Festival Joy Pack for you."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text342"] = ""

-- 【兑换牙齿。】
tFestivalSpring2015_RemoveFireks_Text[17416]["Text411"] = "Where is the tooth you mentioned? I can`t find it in your inventory."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text421"] = "Ah, it`s exactly what I want. You can have the reward, now."


-- 【怎么回事？】
tFestivalSpring2015_RemoveFireks_Text[17416]["Text511"] = "I`ve spent days and nights, and finally made this Disassemble Device. Since limited time, the device hasn`t not been improved."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text512"] = "~When you use it to disassemble the Risky Firecrackers, it may carelessly set off the firecracker. Anyway, if you`re able to"
tFestivalSpring2015_RemoveFireks_Text[17416]["Text513"] = "~clear 10 Risky Firecrackers, you can claim a Festival Joy Pack from me."


-- 【怎么回事？】--爆竹拆解器怎么用？
tFestivalSpring2015_RemoveFireks_Text[17416]["Text611"] = "When you locate a Risky Firecracker, turn on the Disassemble Device. It`ll automatically clear the risk, while it may"
tFestivalSpring2015_RemoveFireks_Text[17416]["Text612"] = "~fail sometimes and set off the firecracker. Don`t worry, you`ll just fall into faint for a while if it fails. When"
tFestivalSpring2015_RemoveFireks_Text[17416]["Text613"] = "~you clear 10 Risky Fireworks, report back to me and claim your reward."


-- 【怎么回事？】--怎么找年兽牙齿？
tFestivalSpring2015_RemoveFireks_Text[17416]["Text711"] = "While disassembling the Risky Firework, you may find Predator`s Tooth."
tFestivalSpring2015_RemoveFireks_Text[17416]["Text712"] = "~If so, bring it to me. I`ll give a good price for it."


------------------------------------------爆竹大师张巧手--选项-------------------------------------------
tFestivalSpring2015_RemoveFireks_Text[17416]["Option11"] = "Got~it."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option12"] = "I~can~help."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option13"] = "Claim~my~reward."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option14"] = "I~got~Predator`s~Tooth."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option15"] = "Tell~me~more."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option16"] = "I`m~just~passing~by."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option17"] = "Good."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option21"] = "Alright."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option22"] = "You`re~right."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option23"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option24"] = "I`ll~do~it~now."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option25"] = "I`m~on~the~way."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option31"] = "Alright."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option32"] = "Alright."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option33"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option34"] = "Thanks!"
tFestivalSpring2015_RemoveFireks_Text[17416]["Option41"] = "Really?~I~must~lose~it~somewhere."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option42"] = "Thanks!"
tFestivalSpring2015_RemoveFireks_Text[17416]["Option51"] = "How~to~use~the~device."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option52"] = "How~to~collect~Predator`s~Tooth."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option53"] = "Learn~about~something~else."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option54"] = "I~see."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option61"] = "Learn~about~something~else."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option62"] = "I~see."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option71"] = "Learn~about~something~else."
tFestivalSpring2015_RemoveFireks_Text[17416]["Option72"] = "I~see."




----------------------------------------------------------------------------------------
-- //危险的爆竹
tFestivalSpring2015_RemoveFireks_Text[17417] = {}
-- 活动前对白
tFestivalSpring2015_RemoveFireks_Text[17417]["Text111"] = "Someone dropped the firecracker here. It seems dangerous."
-- 活动后对白
tFestivalSpring2015_RemoveFireks_Text[17417]["Text121"] = "The Predator dropped some firecrackers here. Fortunately, a heavy rain made them wet and erased the danger."
-- 活动中对白（无任务）
tFestivalSpring2015_RemoveFireks_Text[17417]["Text131"] = "The Predator dropped some firecrackers here for a revenge. It may explode at any time. Hurry and talk to Master Zhang for a solution."
-- 活动中对白（有任务，已领取奖励）
tFestivalSpring2015_RemoveFireks_Text[17417]["Text141"] = "You`ve cleared enough amount of Risky Firecrackers. Take a rest."
-- 活动中对白（有任务,爆炸）
tFestivalSpring2015_RemoveFireks_Text[17417]["Text151"] = "The Disassemble Device falsely set off the firecracker, and you dropped into a faint. So far, you`ve cleared %s Risky Firecracker(s)."
-- 活动中对白（有任务,出选项）
tFestivalSpring2015_RemoveFireks_Text[17417]["Text161"] = "The Predator dropped some firecrackers here for a revenge. It may explode at any time. Do you want to clear it?"


-- 【尝试拆除。】
tFestivalSpring2015_RemoveFireks_Text[17417]["Text211"] = "There is no Disassemble Device in your inventory. Go claim another one from Master Zhang."
tFestivalSpring2015_RemoveFireks_Text[17417]["Text221"] = "You should get closer to the Risky Firecracker to disassemble it."
tFestivalSpring2015_RemoveFireks_Text[17417]["Text231"] = "Your inventory is full. You need to make some room, first."
tFestivalSpring2015_RemoveFireks_Text[17417]["Text241"] = "The Disassemble Device falsely set off the firecracker, and you dropped into a faint. Till now, you`ve cleared %s Risky Firecracker(s)."
tFestivalSpring2015_RemoveFireks_Text[17417]["Text251"] = "You successfully disassembled the Risky Firecracker. So far, you`ve cleared %s Risky Firecracker(s)."
tFestivalSpring2015_RemoveFireks_Text[17417]["Text261"] = "You successfully disassembled the Risky Firecracker and received Predator`s Tooth. So far, you`ve cleared %s Risky Firecracker(s)."


------------------------------------------危险的爆竹--选项-------------------------------------------
-- //危险的爆竹
tFestivalSpring2015_RemoveFireks_Text[17417]["Option11"] = "Indeed."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option12"] = "Good."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option13"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option14"] = "Alright."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option15"] = "I~see."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option16"] = "I~want~to~clear~it."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option17"] = "I`m~not~ready,~yet."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option21"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option22"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option23"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option24"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option25"] = "Okay."
tFestivalSpring2015_RemoveFireks_Text[17417]["Option26"] = "Okay."


-------------其他对白
tFestivalSpring2015_RemoveFireks_Text["Succee"] = "You`ve successfully cleared 10 Risky Fireworks. Hurry and claim your reward from Master Zhang."
tFestivalSpring2015_RemoveFireks_Text["GetGeneralPag"] = "Master Zhang gave you a Festival Joy Pack. Hurry and check it!"
tFestivalSpring2015_RemoveFireks_Text["Item_TimeOut"] = "The Lantern Festival has passed, and the item became useless, so you threw it away."
tFestivalSpring2015_RemoveFireks_Text["Item_MonsterCoat"] = "Master Zhang is looking for the Predator`s Teeth. You can swap it for a gift from Master Zhang."
tFestivalSpring2015_RemoveFireks_Text["AddExp"] = "You received 30 minutes of EXP!"
tFestivalSpring2015_RemoveFireks_Text["AddCultivation"] = "You received 20 Study Points!"
tFestivalSpring2015_RemoveFireks_Text["ExploreText"] = "Disassembling"


------------------------------------------------------------------------------------
--Name:		[简体征服][活动任务]2015年春节活动之元宵
--Purpose:	2015年春节活动之元宵
--Creator: 	丁晨
--Created:	2014/12/15
------------------------------------------------------------------------------------

------------------------------------------NPC对白------------------------------------
-- //元宵福利美眉
-- 活动前
tFestivalSpring2015_LanternFestival_Text = {}
tFestivalSpring2015_LanternFestival_Text[17425] = {}
tFestivalSpring2015_LanternFestival_Text[17425]["Text111"] = "From March 1st to 5th, the Quick Luck event will be held in Twin City. For heroes above Level 80 or reborn,"
tFestivalSpring2015_LanternFestival_Text[17425]["Text112"] = "~you can also claim a Spring Coupon from me during the first 30 minutes of every hour at that time. Shh,"
tFestivalSpring2015_LanternFestival_Text[17425]["Text113"] = "~it`s a secret. The first three heroes who claim the coupon at 12:00, 19:00 and 21:00 will be able to swap"
tFestivalSpring2015_LanternFestival_Text[17425]["Text114"] = "~for a super prize."

--活动后
tFestivalSpring2015_LanternFestival_Text[17425]["Text121"] ="Though I nearly crashed on the busy days, I really enjoyed myself. There were so many people showing interest"
tFestivalSpring2015_LanternFestival_Text[17425]["Text122"] ="~in me. Each of them tried very hard to touch me for the coupons."

--活动中
tFestivalSpring2015_LanternFestival_Text[17425]["Text131"] ="Happy Lantern Festival! The Quick Luck event is heating up from March 1st to 5th! If you`re above Level 80"
tFestivalSpring2015_LanternFestival_Text[17425]["Text132"] ="~or reborn, you can claim a Spring Coupon from me during the first 30 minutes of every hour. Shh,"
tFestivalSpring2015_LanternFestival_Text[17425]["Text133"] ="~it`s a secret. The first three heroes who claim and activate the coupon at 12:00, 19:00 and 21:00 will be able to swap for a super prize."

--活动规则
tFestivalSpring2015_LanternFestival_Text[17425]["Text141"] ="For 12:00, it`s a Dragon Ball (B) for the 1st place, a Super Gem (B) for the 2nd place, and a 3-day Fancy Alpaca (B) for the 3rd place.\n"
tFestivalSpring2015_LanternFestival_Text[17425]["Text142"] ="~For 19:00, it`s a bottle of Oblivion Dew (B) for the 1st place, a +3 Stone (B) for the 2nd place, and a 3-day Rod of Roses Bag (B) for the 3rd place.\n"
tFestivalSpring2015_LanternFestival_Text[17425]["Text143"] ="~For 21:00, it`s 500 Chi Points for the 1st place, 1000 Study Points for the 2nd place, and 3 Small Lottery Tickets (B) for the 3rd place."


tFestivalSpring2015_LanternFestival_Text[17425]["Text151"] ="As long as you`ve reached Level 80 or got reborn, you can claim a Spring Coupon from me, everyday from March 1st to 5th."
tFestivalSpring2015_LanternFestival_Text[17425]["Text152"] ="~But remember, you should within the first 30 minutes of every hour. What`s more, I`ve prepared a super prize for the first three heroes"
tFestivalSpring2015_LanternFestival_Text[17425]["Text153"] ="~who claim and activate the coupon at 12:00, 19:00 and 21:00."


--领取失败
--失败、等级不足
tFestivalSpring2015_LanternFestival_Text[17425]["Text311"] ="Sorry, the Spring Coupons are only for heroes who`ve reached Level 80 or reborn."
--失败、背包满
tFestivalSpring2015_LanternFestival_Text[17425]["Text312"] ="Your inventory is too full to contain anything. You need to make some room, first."
--失败、已领取
tFestivalSpring2015_LanternFestival_Text[17425]["Text313"] ="Hey, you`ve already claimed a Spring Coupon from me, haven`t you? I have no more for you, today."
--失败、时间范围外
tFestivalSpring2015_LanternFestival_Text[17425]["Text314"] ="Sorry, you came at the wrong time. The Spring Coupon is available during the first 30 minutes of every hour."

--选项卡
tFestivalSpring2015_LanternFestival_Text[17425]["Option11"] = "Sounds~wonderful."
tFestivalSpring2015_LanternFestival_Text[17425]["Option12"] = "Yeah,~just~for~the~coupons."
tFestivalSpring2015_LanternFestival_Text[17425]["Option131"] = "Give~me~the~coupon."
tFestivalSpring2015_LanternFestival_Text[17425]["Option132"] = "I`ll~talk~to~you~later."
tFestivalSpring2015_LanternFestival_Text[17425]["Option133"] = "What`s~the~super~prize?"
tFestivalSpring2015_LanternFestival_Text[17425]["Option134"] = "What`re~the~rules?"
tFestivalSpring2015_LanternFestival_Text[17425]["Option135"] = "Learn~about~something~else."
tFestivalSpring2015_LanternFestival_Text[17425]["Option311"] = "Alright."
tFestivalSpring2015_LanternFestival_Text[17425]["Option312"] = "I`ll~come~back~soon."
tFestivalSpring2015_LanternFestival_Text[17425]["Option313"] = "Okay."
tFestivalSpring2015_LanternFestival_Text[17425]["Option314"] = "Okay."


--系统提示seckill

--领取成功
tFestivalSpring2015_LanternFestival_Text["Success"] ="Lady Fancy gave you a Spring Coupon. Hurry and activate it."

tFestivalSpring2015_LanternFestival_Text["Notification"] ="The Quick Luck at %s:00 is about to start in 2 minutes. Hurry and try your luck at Lady Fancy."

tFestivalSpring2015_LanternFestival_Text["Notifier"] ="The 1st place of the Quick Luck event has come out. Congratulations to %s! The prize is %s!"

tFestivalSpring2015_LanternFestival_Text["NotifiBag"] ="Your inventory is full. You need to make some room, first."

tFestivalSpring2015_LanternFestival_Text["FestivalFail"] ="The celebration has ended, and the item became useless, so you threw it away."

tFestivalSpring2015_LanternFestival_Text["AddExp"] ="You received %s minutes of EXP!"

tFestivalSpring2015_LanternFestival_Text["OverFlow"] ="You successfully taken the 1st place of this round`s Quick Luck, and received %s!"

tFestivalSpring2015_LanternFestival_Text["AddCultivation"] = "You received %s Study Points!"

tFestivalSpring2015_LanternFestival_Text["Cultivation"] = "%s Study Points"

tFestivalSpring2015_LanternFestival_Text["Average"] =  "%s Chi Points"

tFestivalSpring2015_LanternFestival_Text["Present"] = "(B)"
tFestivalSpring2015_LanternFestival_Text["PresentClear"] = "The item has disappeared."

tFestivalSpring2015_LanternFestival_Text["RewardBag"] = "Which one do you want to claim?"
tFestivalSpring2015_LanternFestival_Text["ShortReward"] = "1-handed~Rod~of~Roses."
tFestivalSpring2015_LanternFestival_Text["LongReward"] = "2-handed~Rod~of~Roses."
tFestivalSpring2015_LanternFestival_Text["Think"] = "I`ll~think~about~it."
tFestivalSpring2015_LanternFestival_Text["BefPreClear"] = "Clear yesterday`s Spring Coupon."
tFestivalSpring2015_LanternFestival_Text["ImmediatelyUse"] ="Activate the Spring Coupon, now?"
tFestivalSpring2015_LanternFestival_Text["Rose"]= "You received a 3-day weapon accessory, Rod of Roses (B)!"

----------------------------------------------------春节跨服任务----------------------------
------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]跨服春节任务（2.15~3.5）--跨服拜乐
--Purpose:		
--Creator: 		张磊
--Created:		01/08/2015
------------------------------------------------------------------------------------




--中文索引
tCrossFestivalTask_GrabEnvelope_Text = {}
tCrossFestivalTask_GrabEnvelope_Text[17280] = {}
tCrossFestivalTask_GrabEnvelope_Text[17980] = {}
tCrossFestivalTask_GrabEnvelope_Text[10329] = {}
tCrossFestivalTask_GrabEnvelope_Text[18281] = {}
tCrossFestivalTask_GrabEnvelope_Text[18282] = {}

--帮派跨服任务大使

--任务主对白
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text111"] = "Are you looking for a new challenge? From March 12th to 18th, all Level 70+ of 2nd-reborn heroes are able to"
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text112"] = "~go abroad and compete with heroes from other servers. If you`re interested, come and see me!"
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text113"] = ""

--活动时间前的对白
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text121"] = "Are you looking for a new challenge? From March 12th to 18th, all Level 70+ of 2nd-reborn heroes are able to"
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text122"] = "~go abroad and compete with heroes from other servers. If you`re interested, come and find me at that time."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text123"] = ""

----活动时间前，点击了解战旗掠夺战。
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text811"] = "During the event, heroes above Level 70 of 2nd rebirth are encouraged to destroy the Kingdom War Fund in other server."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text812"] = "~If you make it, report back to this server. I`ll reward you with 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training."

tCrossFestivalTask_GrabEnvelope_Text[17280]["Text813"] = ""

----活动时间前，点击了解异兽争霸赛。
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1011"] = "The Alien Beast threats to invade the Realm at 20:45 and 21:30 everyday during the event. If you`re a guild member, kill the beast and contribute 200 million to your Guild Fund."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1012"] = "~You can also collect individual points in the Realm by killing the Golden Lions, the Golden Lion King and the Chest Beasts, or lighting the lantern, or defeating heroes of other server."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1013"] = "~When you earn 50 points, you can claim 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training from me, up to 3 times in a day."



--活动时间后对白
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text131"] = "The Eve of the Kingdom War has ended. Thanks for your participation!"
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text132"] = ""
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text133"] = ""

--点击 跨服访使节。
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text251"] = "You must have felt the tense situation. Heroes in every server are squaring up for a terrifying war."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text252"] = "~To seize the first opportunity, I want heroes above Level 70 of 2nd rebirth to destroy the Kingdom War Fund of other server. I promise"
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text253"] = "~a big reward for that."


----了解活动详情。
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text411"] = "As long as you`ve reached Level 70 of 2nd rebirth, you can travel to other server and destroy its Kingdom War Fund."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text412"] = "~If you make it, report back to me in this server. I`ll reward you with 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and"

tCrossFestivalTask_GrabEnvelope_Text[17280]["Text413"] = "~1 Free Course for Jiang Hu training."

--接受完任务后提示
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text211"] = "You`ve already accepted the quest. Travel to other server and destroy its Kingdom War Fund."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text212"] = ""


--失败、玩家等级不足
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text221"] = "You should reach at least Level 70 of 2nd rebirth, first."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text222"] = "~It`s too dangerous for you to be exposed to the outside world."

--已经领取过奖励点击领取奖励
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1101"] = "You`ve already claimed a piece of reward. Why not leave the chance to others?"

--完成任务点击接受任务
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1201"] = "You`ve already completed the `Fire Strike` quest. If you`re still interested, come and see me tomorrow."

--未完成任务点击领取奖励提示
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1301"] = "Have you destroyed any Kingdom War Fund of any server? It seems a `No`. So, complete your job first."



--背包已的提示
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1401"] = "Your inventory is full. You need to make some room, first."

--完成任务提示
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1501"] = "Well done! Thanks for your service. These are your rewards: 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1601"] = "Well done! Thanks for your service. These are your rewards: 2 Festival Joy Packs, 1 Protection Pill, and 1 Free Course for Jiang Hu training."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1701"] = "Well done! Thanks for your service. These are your rewards: 2 Festival Joy Packs and 1 Protection Pill."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text1801"] = "Well done! Thanks for your service. These are your rewards:  2 Festival Joy Packs, 1 Protection Pill, and 1 Talent for Jiang Hu training."

--成功完成任务
-- tCrossFestivalTask_GrabEnvelope_Text["NoSynCompleteTask"] = "恭喜阁下获得了2份节日欢庆礼包。"

--领取了红包，点击接受任务提示
tCrossFestivalTask_GrabEnvelope_Text["SecStemp"] = "You`ve successfully destroyed the Kingdom War Fund of other server. Go claim your reward from the Realm General."

--领取了红包，点击国家使节
tCrossFestivalTask_GrabEnvelope_Text["RepeatLightBox"] = "You successfully destroyed the Kingdom War Fund. Go back to your server and claim rewards from the Realm General."

--没任务点击领取奖励
tCrossFestivalTask_GrabEnvelope_Text["MsgAccept"] = "You need to accept the quest, first."
tCrossFestivalTask_GrabEnvelope_Text["ClickLantern"] = "You lit the Lantern, and received 5 individual points."



--本服玩家点击赐福提示
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text321"] = "Only the heroes from other server may destroy this Kingdom War Fund."

--跨服玩家不能接任务和上交红包
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text331"] = "You`re from other server. You need to claim your reward from the Realm General in your server."

--回本服领任务提示
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text711"] = "You need to accept the `Fire Strike` quest in your server, first."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text712"] = ""
tCrossFestivalTask_GrabEnvelope_Text[17280]["Text713"] = ""

--国家使节
tCrossFestivalTask_GrabEnvelope_Text[17980]["Text111"] = "The Kingdom War Fund is ready, which is a symbol of confidence and high morale."

tCrossFestivalTask_GrabEnvelope_Text[17980]["CrossResiveEnvelope"] = "You successfully destroyed the Kingdom War Fund and frustrated people in this server. Hurry and report back to the Realm General in your server."

tCrossFestivalTask_GrabEnvelope_Text[17980]["Text211"] = "Destroying this server`s Kingdom War Fund can only weaken their confidence for a time. Are you sure you want to do it?"

--烧毁国战储备金时的提示 2007
tCrossFestivalTask_GrabEnvelope_Text["MsgBox_2007_1"] = "The Kingdom War Fund has been destroyed by %s from other server. Such a provocation cannot be forgiven!"
tCrossFestivalTask_GrabEnvelope_Text["MsgBox_2007_2"] = "Hurry, hurry! Our Kingdom War Fund was destroyed by the enemies from other server. Hurry and take a revenge to their server."
tCrossFestivalTask_GrabEnvelope_Text["MsgBox_2007_3"] = "Our Kingdom War Fun was destroyed by %s from other server. No way to let the thing go easy."




----活动时间前的选项
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option1"] = "Learn~about~`Fire~Strike`."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option3"] = "Learn~about~`Alien~Threat`."

----活动时间前点击了解活动内容，选项通用（跨服访使节、异兽争霸赛）

tCrossFestivalTask_GrabEnvelope_Text[17280]["Option4"] = "Back~to~previous~topic."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option5"] = "I`ll~talk~to~you~later."

----活动后对白选项
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option6"] = "See~you."

tCrossFestivalTask_GrabEnvelope_Text[17280]["Option8"] = "Count~me~in!"
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option9"] = "Claim~my~reward."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option10"] = "Tell~me~more."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option11"] = "Back~to~previous~topic."

--
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option20"] = "Fire~Strike."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option22"] = "Alien~Threat."

----国家使节
tCrossFestivalTask_GrabEnvelope_Text[17980]["Option12"] = "For~one~goal;~for~Kingdom~War!"

----跨服国家使节
tCrossFestivalTask_GrabEnvelope_Text[17980]["Option13"] = "I~want~to~destroy~it!"
tCrossFestivalTask_GrabEnvelope_Text[17980]["Option14"] = "I`m~ready."
tCrossFestivalTask_GrabEnvelope_Text[17980]["Option15"] = "I`ll~talk~to~you~later."

----可不要小看了我！
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option16"] = "Don`t~think~little~of~me."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option17"] = "I~see."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option18"] = "Okay."
tCrossFestivalTask_GrabEnvelope_Text[17280]["Option19"] = "Thanks!"


--------添加“军务备战总兵”和“军务备战参将”对白
tCrossFestivalTask_GrabEnvelope_Text[18281]["Text111"] = "The Kingdom War is on the verge of breaking out. Keep yourself on toe, and be careful of enemy infiltration."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Text112"] = "~If you have any question about the war, ask me."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text211"] = "At the beginning of the war, ambitious guild leader can build an alliance with CPs. If there is not any alliance in your server,"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Text212"] = "~the first winning guild of Guild War will become an alliance. There can be several alliances in one server."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text311"] = "Many things are available for an alliance. For example, issue various orders, start a plunder war, lead the alliance members, and award contributors."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Text312"] = "~Which kind of thing would you like to learn more about?"

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text411"] = "The Chief of the Alliance can issue a Plunder Order to start a plunder war against the target kingdom. During the war,"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Text412"] = "~heroes can plunder gold bricks from the target kingdom and increase the fund for the alliance."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text511"] = "The Chief of the Alliance can issue an Award Order to award the alliance members nearby."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Text512"] = ""

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text611"] = "The Chief of the Alliance can issue a Call Order to gather and call all alliance members to fight for the alliance."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text711"] = "Individuals who haven`t participated in any guilds can also choose an alliance to serve and fight for."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text811"] = "At a certain time on every Sunday, the alliance with the most amount of gold bricks will upgrade to be kingdom,"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Text812"] = "~while its Chief will be automatically crowned to be Emperor. There will be only one kingdom in each server."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text911"] = "When an alliance upgrades to be kingdom, there will be exclusive privileges except regular functions of alliance. Which one are you interested in?"

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text1011"] = "The Emperor can pay salary to all members serving the kingdom."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text1111"] = "The Emperor can establish a perfect ruling system by appointing Premier, Marshals, Generals, Palace Guards, and also Consorts in the Harem."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text1211"] = "The Emperor and the kingdom`s officials will enjoy special appearance or steed to show their power and honor."

tCrossFestivalTask_GrabEnvelope_Text[18281]["Text1311"] = "Part of the officials are entitled to use some of the Emperor`s privileges."


--军务备战参将
tCrossFestivalTask_GrabEnvelope_Text[18282]["Text111"] = "A great war is about to fire. If you want to make a name, talk to the Realm General. He will have some cross-server quests"
tCrossFestivalTask_GrabEnvelope_Text[18282]["Text112"] = "~available during the war. It`s also a way for you to earn wonderful rewards and contribute fund for your alliance. Which one"
tCrossFestivalTask_GrabEnvelope_Text[18282]["Text113"] = "~would you like to know more?"

tCrossFestivalTask_GrabEnvelope_Text[18282]["Text211"] = "During the war, when the Chief of the Alliance or the Emperior issues a plunder order, you can travel to the target kingdom and"
tCrossFestivalTask_GrabEnvelope_Text[18282]["Text212"] = "~plunder gold bricks from its Holy Altar in Twin City. After that, submit the gold bricks you plundered to the Realm General for a nice reward."

tCrossFestivalTask_GrabEnvelope_Text[18282]["Text311"] = "You will be asked to travel to other server. When you arrive, try your best to contact the City Lurker, Mastery Man, Military Spy, Official Agent, and Travelling Scout."
tCrossFestivalTask_GrabEnvelope_Text[18282]["Text312"] = "~If you`re able to bring back their information to the Realm General in your server, you`ll be nicely rewarded."

tCrossFestivalTask_GrabEnvelope_Text[18282]["Text411"] = "When you defeat a certain number of fighters in other server, you can go back to the Realm General in your server and claim a reward."
tCrossFestivalTask_GrabEnvelope_Text[18282]["Text412"] = ""

tCrossFestivalTask_GrabEnvelope_Text[18282]["Text511"] = "Some people from other servers have infiltrated into your server for scout and loot. If you`re able to kill a certain number of them,"
tCrossFestivalTask_GrabEnvelope_Text[18282]["Text512"] = "~you can claim a reward from the Realm General in your server."


--新增选项
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option40"] = "How~to~build~an~alliance?"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option41"] = "What`s~alliance~for?"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option42"] = "How~to~serve~an~alliance?"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option43"] = "What`s~kingdom?"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option44"] = "What`s~kingdom~for?"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option45"] = "Fight,~and~conquer!"

tCrossFestivalTask_GrabEnvelope_Text[18281]["Option46"] = "The~Kingdom~War."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option47"] = "Plunder~Order."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option48"] = "Award~Order."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option49"] = "Call~Order."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option50"] = "Other~orders"
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option51"] = "Salary~Order."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option52"] = "Appointment."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option53"] = "Appearance~and~steed."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option54"] = "Officials`~privileges."
tCrossFestivalTask_GrabEnvelope_Text[18281]["Option55"] = "Other~privileges."

tCrossFestivalTask_GrabEnvelope_Text[18282]["Option60"] = "Gold~Brick~Plunder."
tCrossFestivalTask_GrabEnvelope_Text[18282]["Option61"] = "The~Undercover."
tCrossFestivalTask_GrabEnvelope_Text[18282]["Option62"] = "The~Pioneers."
tCrossFestivalTask_GrabEnvelope_Text[18282]["Option63"] = "Kingdom~Rush."
tCrossFestivalTask_GrabEnvelope_Text[18282]["Option64"] = "Other~quests."
tCrossFestivalTask_GrabEnvelope_Text[18282]["Option65"] = "Fight,~and~conquer!"


------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]春节跨服活动之帮派异兽争霸赛(2.05-2.07)
--Purpose:		
--Creator: 		丁晨
--Created:		01/08/2015
------------------------------------------------------------------------------------


tCrossFestivalTask_MonsterNian_Text = {}
tCrossFestivalTask_MonsterNian_Text["BroadcastBef"] = "The Alien Beast has appeared! Hurry and keep watch at (234,243) in the Realm server!"
tCrossFestivalTask_MonsterNian_Text["BroadcastKillExplain"] = "The Alien Beast has appeared at (234,243) in the Realm server. Hurry and strench to your sword!"
tCrossFestivalTask_MonsterNian_Text["BroadcastKillFinish"] = "%s defeated the Alien Beast, contributing over 100 million Guild Fund for %s!"
tCrossFestivalTask_MonsterNian_Text["BroadcastKillFinishNoFac"] = "You successfully killed the Alien Beast. Hurry and claim 200 million Guild Fund from the Realm General (311,283)!"

tCrossFestivalTask_MonsterNian_Text["MonsterBoss"] = "AlienBeast"
tCrossFestivalTask_MonsterNian_Text["MonsterSmall"] = "GoldenLion"
tCrossFestivalTask_MonsterNian_Text["BroadcastKillFinishBox"] = "You successfully killed the Golden Lion, and received 1 individual point!"
tCrossFestivalTask_MonsterNian_Text["BroadcastKillFinishPlayer"] = "You successfully killed an enemy, and received 8 individual points!"
tCrossFestivalTask_MonsterNian_Text["BroadcastKillFinishGoldBox"] = "You successfully killed the Chest Beast, and received 5 individual points!"
tCrossFestivalTask_MonsterNian_Text["BroadcastKillFinishWolf"] = "You successfully killed the Golden Lion King, and received 30 individual points!"

tCrossFestivalTask_MonsterNian_Text[17280] = {}


--活动中
tCrossFestivalTask_MonsterNian_Text[17280]["Text111"] = "Heroes emerge in monster madness. The Alien Beast threat to ruin the land at 20:45 and 21:30,"
tCrossFestivalTask_MonsterNian_Text[17280]["Text112"] = "~everyday during the event. If you`ve reached Level 70 of 2nd rebirth, come and fight!"
tCrossFestivalTask_MonsterNian_Text[17280]["Text113"] = ""
--领取成功
tCrossFestivalTask_MonsterNian_Text[17280]["Text115"] = "Excellent! You did us a great service for killing the Alien Beast! Come and take your reward."
tCrossFestivalTask_MonsterNian_Text[17280]["Text116"] = ""

--背包已满
tCrossFestivalTask_MonsterNian_Text[17280]["Text117"] ="Your inventory is too full to contain anything. You need to make some room, first."
--活动后
tCrossFestivalTask_MonsterNian_Text[17280]["Text118"] ="The Eve of the Kingdom War has passed."

tCrossFestivalTask_MonsterNian_Text[17280]["Text120"] ="You can`t claim the Guild Fund since you`re not in any guild."
tCrossFestivalTask_MonsterNian_Text[17280]["Text121"] ="Which kind of reward would you like to claim?"

--了解活动详情
tCrossFestivalTask_MonsterNian_Text[17280]["Text122"] ="The Alien Beast threats to invade the Realm at 20:45 and 21:30 everyday during the event. If you`re a guild member, kill the beast to win 200 million for your Guild Fund."
tCrossFestivalTask_MonsterNian_Text[17280]["Text123"] ="~You can also collect individual points in the Realm by killing Golden Lions, Golden Lion King and Chest Beasts, or lighting the lantern, or defeating heroes of other server."
tCrossFestivalTask_MonsterNian_Text[17280]["Text124"] ="~When you earn 50 points, you can claim 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training from me, up to 3 times in a day."
tCrossFestivalTask_MonsterNian_Text[17280]["Text125"] =""
tCrossFestivalTask_MonsterNian_Text[17280]["Text127"] ="You should kill the Alien Beast before you can claim a reward from me. The Alien Beast is known to appear at 20:45 and 21:30 in the Realm server."
tCrossFestivalTask_MonsterNian_Text[17280]["Text128"] ="You don`t have 50 individual points to claim a reward."
tCrossFestivalTask_MonsterNian_Text[17280]["Text129"] ="You`ve claimed the reward 3 times, today. If you`re still interested, please retry tomorrow."
tCrossFestivalTask_MonsterNian_Text[17280]["Text130"] ="You should reach at least Level 70 of 2nd rebirth before you can take part in this event."
--获得基金奖励
tCrossFestivalTask_MonsterNian_Text["FactionMoney"] = "You`ve contributed %d Guild Fund for your guild."

tCrossFestivalTask_MonsterNian_Text[17280]["Text131"] ="You`ve earned %s individual points. You can also collect the points in the Realm by killing Golden Lions, Golden Lion King and Chest Beasts, or lighting the lantern, or defeating heroes"
tCrossFestivalTask_MonsterNian_Text[17280]["Text132"] ="~of other server. When you earn 50 points, you can claim 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training from me, up to 3 times in a day."

tCrossFestivalTask_MonsterNian_Text[17280]["Text133"] ="The Golden Lion, the Golden Lion King and the Chest Beast may appear in the Realm at a certain time."
tCrossFestivalTask_MonsterNian_Text[17280]["Text134"] ="~Killing them will earn yourself 1 point, 30 points, and 5 points respectively. You`ll also receive 5 points"
tCrossFestivalTask_MonsterNian_Text[17280]["Text135"] ="~by lighting the lantern in the Realm, and 8 points by defeating a hero from other server without inceasing your PK points."



tCrossFestivalTask_MonsterNian_Text["BagLetter"] ="The portal connecting to other servers will be open between March 12th and 18th. If you`ve reached Level 70 of 2nd rebirth,"
tCrossFestivalTask_MonsterNian_Text["BagLetterNext"] ="~you can travel to other server and compete with heroes from other server."

tCrossFestivalTask_MonsterNian_Text["BagLetterCross"] ="During the event, all Level 70+ of 2nd-reborn heroes are encouraged to destroy the Kingdom War Fund in other server. If you succeed,"
tCrossFestivalTask_MonsterNian_Text["BagLetterCrossNext"] ="~go back to your server, and claim 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course from the Realm General (TwinCity 311,283)."

tCrossFestivalTask_MonsterNian_Text["BagLetterNianA"] ="The Alien Beast threat to invade the Realm at 20:45 and 21:30 everyday during the event. If you`re a guild member, go eliminate the beast"
tCrossFestivalTask_MonsterNian_Text["BagLetterNianB"] ="~to win 200 million for your Guild Fund. Besides, your contributions of killing the Golden Lions, the Golden Lion King and the Chest Beasts,"
tCrossFestivalTask_MonsterNian_Text["BagLetterNianC"] ="~lighting the lantern, and defeating heroes of other server will be counted into your individual points in the Realm. When you earn 50 points,"
tCrossFestivalTask_MonsterNian_Text["BagLetterNianD"] ="~you can claim 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training from the Realm General (TwinCity 311,283), up to 3 times in a day."

--背包信提示
tCrossFestivalTask_MonsterNian_Text["BagLetterNoti"] ="Your inventory is full. Please make some room, and login again to receive the invitation of `The Eve of Kingdom War`."
tCrossFestivalTask_MonsterNian_Text["BagLetterGetSucc"] ="You received an invitation of `The Eve of Kingdom War`."
tCrossFestivalTask_MonsterNian_Text["BeOverdue"] = "The item is useless now, so you threw it away."
tCrossFestivalTask_MonsterNian_Text["GetRewards"] ="%s killed the Alien Beast, contributing over 100 million Guild Fund for %s!"
tCrossFestivalTask_MonsterNian_Text["GetRewardsBag"] ="You`ve earned %s individual points. You can also collect the points in the Realm by killing Golden Lions, Golden Lion King and Chest Beasts, or lighting the lantern, or defeating heroes"
tCrossFestivalTask_MonsterNian_Text["GetRewardsBagText"] ="~of other server. When you earn 50 points, you can claim 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training from me, up to 3 times in a day."
tCrossFestivalTask_MonsterNian_Text["BossBox"]="The Alien Beast has appeared! Hurry and eliminate it!"

--地图公告
tCrossFestivalTask_MonsterNian_Text["KillMaster"] ="%s successfully killed the Alien Beast, making a great contribution to the land."
tCrossFestivalTask_MonsterNian_Text["KillSmallMaster"] ="%s killed the Golden Lion King, and received 30 individual points!"

tCrossFestivalTask_MonsterNian_Text["RewardFive"]="You`ve already earned 50 individual points. You can claim a reward from the Realm General, now."

--完成任务提示
tCrossFestivalTask_MonsterNian_Text[17280]["Text1600"] = "Well done! Thanks for your service. These are your rewards: 2 Festival Joy Packs, 1 Protection Pill, 1 Talent and 1 Free Course for Jiang Hu training."
tCrossFestivalTask_MonsterNian_Text[17280]["Text1601"] = "Well done! Thanks for your service. These are your rewards: 2 Festival Joy Packs, 1 Protection Pill, and 1 Free Course for Jiang Hu training."
tCrossFestivalTask_MonsterNian_Text[17280]["Text1602"] = "Well done! Thanks for your service. These are your rewards: 2 Festival Joy Packs and 1 Protection Pill."
tCrossFestivalTask_MonsterNian_Text[17280]["Text1603"] = "Well done! Thanks for your service. These are your rewards: 2 Festival Joy Packs, 1 Protection Pill, and 1 Talent for Jiang Hu training."

--选项卡
tCrossFestivalTask_MonsterNian_Text[17280]["Option1"] = "Claim~my~reward."
tCrossFestivalTask_MonsterNian_Text[17280]["Option2"] = "I`ll~talk~to~you~later."
tCrossFestivalTask_MonsterNian_Text[17280]["Option3"] = "Thanks!"
tCrossFestivalTask_MonsterNian_Text[17280]["Option4"] = "I`ll~do~it~now."
tCrossFestivalTask_MonsterNian_Text[17280]["Option5"] = "Alright."
tCrossFestivalTask_MonsterNian_Text[17280]["Option6"] = "Alright."
tCrossFestivalTask_MonsterNian_Text[17280]["Option7"] = "I`ll~think~about~it."
tCrossFestivalTask_MonsterNian_Text[17280]["Option8"] = "Tell~me~more."
tCrossFestivalTask_MonsterNian_Text[17280]["Option9"] = "I`ll~talk~to~you~later."
tCrossFestivalTask_MonsterNian_Text[17280]["Option10"] = "Back~to~previous~topic."
tCrossFestivalTask_MonsterNian_Text[17280]["Option11"] ="Claim~`Guild~Fund`~reward."
tCrossFestivalTask_MonsterNian_Text[17280]["Option12"] ="Claim~`individual~points`~reward."
tCrossFestivalTask_MonsterNian_Text[17280]["Option13"] ="View~my~points~in~the~Realm."
tCrossFestivalTask_MonsterNian_Text[17280]["Option14"] ="How~to~get~points?"


tCrossFestivalTask_MonsterNian_Text["Option14"] ="Head~to~see~the~Realm~General."
tCrossFestivalTask_MonsterNian_Text["Option15"] ="Learn~about~`Fire~Strike`."
tCrossFestivalTask_MonsterNian_Text["Option16"] ="Learn~about~`Alien~Threat`."
tCrossFestivalTask_MonsterNian_Text["Option17"] ="I~see."
tCrossFestivalTask_MonsterNian_Text["Option18"] ="View~my~points."



--灯笼
tCrossFestivalTask_MonsterNian_Text[18018] = {}
tCrossFestivalTask_MonsterNian_Text[18018]["Text111"]="The Realm is under threat, and heroes are called to clear the evils and earn super honor!"
tCrossFestivalTask_MonsterNian_Text[18018]["Option1"] ="Great!"

--BOSSNPC
tCrossFestivalTask_MonsterNian_Text[18262] = {}
tCrossFestivalTask_MonsterNian_Text[18262]["Text111"]="Stupid humans, come and challenge me at 20:45 and 21:30, every night. I`ll let you know the taste of fear."
tCrossFestivalTask_MonsterNian_Text[18262]["Option1"] ="Let`s~wait~and~see."

--BOSSNPC
tCrossFestivalTask_MonsterNian_Text[18101] = {}
tCrossFestivalTask_MonsterNian_Text[18101]["Text111"]="Alien Beast may appear at any time. If you want to travel to other server, go find the Cross-server Keeper."
tCrossFestivalTask_MonsterNian_Text[18101]["Option1"] ="I~see."



------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]复活节活动之神秘的宝藏（4.2-4.8）
--Purpose:	复活节活动之神秘的宝藏（4.2-4.8）
--Creator: 	陈浩文
--Created:	2015/01/23
------------------------------------------------------------------------------------
-- 命名前缀
-- Easter2015_Shenmishixiang_
-------------双龙城 探险者乔治-------------
-- //双龙城 探险者乔治
tEaster2015_Shenmishixiang_Text = {}
tEaster2015_Shenmishixiang_Text[10821] = {}

-- 活动前对白
tEaster2015_Shenmishixiang_Text[10821]["Text111"] = "Buddy, I can`t even tell you how excited I am. I found the way to get treasure on Easter Island!"
tEaster2015_Shenmishixiang_Text[10821]["Text112"] = "~However, I need to confirm it a few times. If you`re Level 80 or above, come and see me between"
tEaster2015_Shenmishixiang_Text[10821]["Text113"] = "~April 2nd and 8th. I`ll reveal the secret at that time."

-- 活动后对白
tEaster2015_Shenmishixiang_Text[10821]["Text121"] = "Easter Day has passed, and those Rock Freaks are frozen again. Maybe next year, they will come to life."
tEaster2015_Shenmishixiang_Text[10821]["Text122"] = ""

-- 活动中对白
tEaster2015_Shenmishixiang_Text[10821]["Text131"] = "The Rock Freaks on the Easter Island are mysterious and wicked. They were cursed and frozen for stealing"
tEaster2015_Shenmishixiang_Text[10821]["Text132"] = "~treasures. However, when Easter arrives every year, they will come to life. If you`re able to defeat them,"
tEaster2015_Shenmishixiang_Text[10821]["Text133"] = "~you`ll have their treasures. From April 2nd to 8th, enjoy your adventure on the Easter Island!"

--等级不足
tEaster2015_Shenmishixiang_Text[10821]["Text211"] = "Buddy, you should reach at least Level 80 before you can go to the Easter Island. Those Rock Freaks can easily"
tEaster2015_Shenmishixiang_Text[10821]["Text212"] = "~crush a little thing like you."
--当天已获得过礼包
tEaster2015_Shenmishixiang_Text[10821]["Text221"] = "You`ve made a harvest from the Easter Island today, haven`t you? It`s not a good thing to take too many things"
tEaster2015_Shenmishixiang_Text[10821]["Text222"] = "~from those cursed freaks."
--了解详情
tEaster2015_Shenmishixiang_Text[10821]["Text231"] = "I just came back from the Easter Island with a good harvest. Listen, you`ll have a chance to receive the Lucky Crystal"
tEaster2015_Shenmishixiang_Text[10821]["Text232"] = "~after killing a Rock Freak on the island. If you see a Giant Rock Freak, don`t let it go. You can always collect crystal from"
tEaster2015_Shenmishixiang_Text[10821]["Text233"] = "~the Giant Rock Freaks. When you collect 7 Lucky Crystal and combine them, you`ll receive a treasure. Then, it`s time to leave."
tEaster2015_Shenmishixiang_Text[10821]["Text234"] = "~Greed will push yourself into the whirl of curse."

--选项
tEaster2015_Shenmishixiang_Text[10821]["Option11"] = "I~can`t~wait~to~know~it."
tEaster2015_Shenmishixiang_Text[10821]["Option12"] = "Maybe."
tEaster2015_Shenmishixiang_Text[10821]["Option13"] = "Go~to~the~Easter~Island."
tEaster2015_Shenmishixiang_Text[10821]["Option14"] = "Tell~me~more."
tEaster2015_Shenmishixiang_Text[10821]["Option15"] = "I`m~not~interested."
tEaster2015_Shenmishixiang_Text[10821]["Option21"] = "Alright."
tEaster2015_Shenmishixiang_Text[10821]["Option22"] = "You`re~right."
tEaster2015_Shenmishixiang_Text[10821]["Option23"] = "I~see."

-------------复活节岛 探险者乔治-------------

-- //复活节岛 探险者乔治
tEaster2015_Shenmishixiang_Text[10822] = {}

-- 活动前对白
tEaster2015_Shenmishixiang_Text[10822]["Text111"] = "Good to see you here. It`s the Easter Island. I just discovered a secret of treasure on the island."
tEaster2015_Shenmishixiang_Text[10822]["Text112"] = "~From April 2nd to 8th, I`ll reveal the secret to heroes above Level 80. Don`t miss the chance!"
tEaster2015_Shenmishixiang_Text[10822]["Text113"] = ""

-- 活动后对白
tEaster2015_Shenmishixiang_Text[10822]["Text121"] = "Easter Day has passed, and those Rock Freaks are frozen again. Maybe next year, they will come to life."
tEaster2015_Shenmishixiang_Text[10822]["Text122"] = ""

-- 活动中对白
tEaster2015_Shenmishixiang_Text[10822]["Text131"] = "Good to see you here. It`s the Easter Island. You may have seen those Rock Freaks. They were cursed and frozen for stealing"
tEaster2015_Shenmishixiang_Text[10822]["Text132"] = "~treasures. When Easter arrives, they come to life. If you`re able to defeat them between April 2nd and 8th, you`ll have their treasures."
tEaster2015_Shenmishixiang_Text[10822]["Text133"] = ""

-- 返回双龙城
tEaster2015_Shenmishixiang_Text[10822]["Text211"] = "I`m going to send you out of the Easter Island. Be ready!"
--了解详情
tEaster2015_Shenmishixiang_Text[10822]["Text221"] = "Look at the Rock Freaks. You may receive Lucky Crystal when you kill them. While for a Giant Rock Freak, you`ll"
tEaster2015_Shenmishixiang_Text[10822]["Text222"] = "~definitely get the Lucky Crystal after killing it. When you collect 7 Lucky Crystal and combine them, you`ll receive"
tEaster2015_Shenmishixiang_Text[10822]["Text223"] = "~a treasure. Don`t be greedy for more treasures, or you`ll be cursed."

--选项
tEaster2015_Shenmishixiang_Text[10822]["Option11"] = "Of~course."
tEaster2015_Shenmishixiang_Text[10822]["Option12"] = "Maybe."
tEaster2015_Shenmishixiang_Text[10822]["Option13"] = "Return~to~Twin~City."
tEaster2015_Shenmishixiang_Text[10822]["Option14"] = "Tell~me~more."
tEaster2015_Shenmishixiang_Text[10822]["Option15"] = "I`m~just~passing~by."
tEaster2015_Shenmishixiang_Text[10822]["Option21"] = "I`m~ready!"
tEaster2015_Shenmishixiang_Text[10822]["Option22"] = "I~see."

-------------消息窗口-------------
tEaster2015_Shenmishixiang_Text["MsgBox"] = {}
--【玩家进入新地图105弹框提示】
tEaster2015_Shenmishixiang_Text["MsgBox"]["ChgMap"] = "Go collect 7 Lucky Crystal from the Rock Freaks and the Giant Rock Freaks! With the crystal, you can combine them into a treasure."
--【玩家获得7个幸运石晶105弹框提示】
tEaster2015_Shenmishixiang_Text["MsgBox"]["GetSeven"] = "You`ve collected 7 Lucky Crystal. Do you want to combine them into a treasure?"
--【玩家右键点击幸运石晶、失败、数量不够105弹框提示】
tEaster2015_Shenmishixiang_Text["MsgBox"]["NoSeven"] = "You should have 7 Lucky Crystal to combine."
--【玩家右键点击幸运石晶、二次确认融合105弹框提示】
tEaster2015_Shenmishixiang_Text["MsgBox"]["GetSevenConfirm"] = "You`re going to combine 7 Lucky Crystal for a treasure."
--【玩家融合幸运石晶后获得礼包提示、地图外】
tEaster2015_Shenmishixiang_Text["MsgBox"]["Jierilibao"] = "You successfully combined 7 Lucky Crystal, and you received a Festival Joy Pack!"

-------------玩家聊天窗口 1010 2005-------------
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]={}

--【背包满通用提示】
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]["NoLeftSpace"] = "Your inventory is full. You need to make some room, first."
--【活动过期删除道具提示】
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]["Aftime"] = "The item is useless now, and you threw it away."
--【玩家进入新地图提示】
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]["JoinOne"] = "With the help of George, you arrived on the Easter Island."
--【玩家离开新地图提示】
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]["JoinZero"] = "George teleported you back to Twin City."
--【玩家每获得1个幸运石晶提示】
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]["GetOne"] = "You received 1 Lucky Crystal!"
--【玩家二次使用幸运石晶提示】
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]["SecondUse"] = "Failed to combine. You`ve already received a treasure on the Easter Island, today."
--【活动后将玩家传送到双龙城】
tEaster2015_Shenmishixiang_Text["TalkChannel2005"]["LeaveIsland"] = "Easter Day has passed, and you were teleported out of the Easter Island."
-------------读条-------------
tEaster2015_Shenmishixiang_Text["SetExplore"] = {}
tEaster2015_Shenmishixiang_Text["SetExplore"]["Use"] = "Activating the Lucky Crystal."

------------------------------------------------------------------------------------



------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]复活节天天来签到（4.2-4.8）
--Purpose:	复活节天天来签到（4.2-4.8）
--Creator: 	陈浩文
--Created:	2015/01/29
------------------------------------------------------------------------------------
-- 命名前缀
-- Easter2015_Tiantianlaiqiandao_
-------------双龙城 签到使天天-------------
-- //双龙城 签到使天天
tEaster2015_Tiantianlaiqiandao_Text = {}
tEaster2015_Tiantianlaiqiandao_Text[10826] = {}

-- 活动前对白
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text111"] = "All is renewed in spring, and that is the spirit of Easter! From April 2nd to 8th,"
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text112"] = "~all Level 80+ heroes are welcome to play interesting lottery game here!"

-- 活动后对白
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text121"] = "The Easter celebration is over, and it`s time to say goodbye."
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text122"] = "~You must have gotten lots of presents! See you next year!"

-- 活动中 已签到抽奖
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text131"] = "You`ve already claimed a gift, today. Why not leave the chance to others?"

-- 活动中 没有签到抽奖
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text141"] = "All is renewed in spring, and that is the spirit of Easter! All Level 80+ heroes are given a chance to"
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text142"] = "~draw a lottery here, everyday from April 2nd to 8th. See, there are tons of gifts waiting for you to win!"
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text143"] = ""

-- 我要签到抽奖，等级不足
tEaster2015_Tiantianlaiqiandao_Text[10826]["Text211"] = "Sorry, the event is only for Level 80+ heroes. Keep practicing, and come back when you are qualified."

--选项
tEaster2015_Tiantianlaiqiandao_Text[10826]["Option11"] = "Sounds~nice."
tEaster2015_Tiantianlaiqiandao_Text[10826]["Option12"] = "See~you."
tEaster2015_Tiantianlaiqiandao_Text[10826]["Option13"] = "Alright."
tEaster2015_Tiantianlaiqiandao_Text[10826]["Option14"] = "Let~me~draw!"
tEaster2015_Tiantianlaiqiandao_Text[10826]["Option15"] = "I`ll~talk~to~you~later."
tEaster2015_Tiantianlaiqiandao_Text[10826]["Option21"] = "Alright."

-- 系统提示
tEaster2015_Tiantianlaiqiandao_Text["TalkChannel2005"]={}

-- 背包满提示
tEaster2015_Tiantianlaiqiandao_Text["TalkChannel2005"]["NoLeftSpace"] = "Your inventory is full. Please make some room, first."
tEaster2015_Tiantianlaiqiandao_Text["TalkChannel2005"]["Get"] = "You draw a(n) %s!"

-- 奖励物品名称
tEaster2015_Tiantianlaiqiandao_Text["Zero"]={}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][1] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][1]["ItemName"] = "Exp~Potion~(B)."
tEaster2015_Tiantianlaiqiandao_Text["Zero"][2] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][2]["ItemName"] = "Exp~Ball~(B)."
tEaster2015_Tiantianlaiqiandao_Text["Zero"][3] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][3]["ItemName"] = "Praying~Stone~(S)~(B)."
tEaster2015_Tiantianlaiqiandao_Text["Zero"][4] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][4]["ItemName"] = "Endurance~Book~(B)."
tEaster2015_Tiantianlaiqiandao_Text["Zero"][5] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][5]["ItemName"] = "Memory~Agate~(B)."
tEaster2015_Tiantianlaiqiandao_Text["Zero"][6] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][6]["ItemName"] = "P1~Soul~Pack~(B)."
tEaster2015_Tiantianlaiqiandao_Text["Zero"][7] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][7]["ItemName"] = "Weapon~Refinery~Pack~(B)."
tEaster2015_Tiantianlaiqiandao_Text["Zero"][8] = {}
tEaster2015_Tiantianlaiqiandao_Text["Zero"][8]["ItemName"] = "Quest~Chance~B~(B)."

tEaster2015_Tiantianlaiqiandao_Text["One"]={}
tEaster2015_Tiantianlaiqiandao_Text["One"][1] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][1]["ItemName"] = "Exp~Potion"
tEaster2015_Tiantianlaiqiandao_Text["One"][2] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][2]["ItemName"] = "Exp~Ball~(B)."
tEaster2015_Tiantianlaiqiandao_Text["One"][3] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][3]["ItemName"] = "Praying~Stone~(S)~(B)."
tEaster2015_Tiantianlaiqiandao_Text["One"][4] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][4]["ItemName"] = "Endurance~Book~(B)."
tEaster2015_Tiantianlaiqiandao_Text["One"][5] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][5]["ItemName"] = "Memory~Agate~(B)."
tEaster2015_Tiantianlaiqiandao_Text["One"][6] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][6]["ItemName"] = "Antique~Soul~Pack."
tEaster2015_Tiantianlaiqiandao_Text["One"][7] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][7]["ItemName"] = "Superior~Refinery~Pack."
tEaster2015_Tiantianlaiqiandao_Text["One"][8] = {}
tEaster2015_Tiantianlaiqiandao_Text["One"][8]["ItemName"] = "Quest~Chance~B~(B)."

tEaster2015_Tiantianlaiqiandao_Text["Two"]={}
tEaster2015_Tiantianlaiqiandao_Text["Two"][1] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][1]["ItemName"] = "Exp~Potion."
tEaster2015_Tiantianlaiqiandao_Text["Two"][2] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][2]["ItemName"] = "Exp~Ball."
tEaster2015_Tiantianlaiqiandao_Text["Two"][3] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][3]["ItemName"] = "Praying~Stone~(M)."
tEaster2015_Tiantianlaiqiandao_Text["Two"][4] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][4]["ItemName"] = "Endurance~Book."
tEaster2015_Tiantianlaiqiandao_Text["Two"][5] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][5]["ItemName"] = "Memory~Agate."
tEaster2015_Tiantianlaiqiandao_Text["Two"][6] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][6]["ItemName"] = "Antique~Soul~Pack."
tEaster2015_Tiantianlaiqiandao_Text["Two"][7] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][7]["ItemName"] = "Superior~Refinery~Pack."
tEaster2015_Tiantianlaiqiandao_Text["Two"][8] = {}
tEaster2015_Tiantianlaiqiandao_Text["Two"][8]["ItemName"] = "Sash~(S)."

------------------------------------------------------------------------------------


------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]复活节限量特卖（4.2-4.8）
--Purpose:	复活节限量特卖（4.2-4.8）
--Creator: 	严振飞
--Created:	2015/02/02
------------------------------------------------------------------------------------

-------------NPC对白
-- //========划算姐========\\
tEaster2015_Sale_Text = {}
tEaster2015_Sale_Text[18221] = {}
--【第一层】
-- 活动前对白
tEaster2015_Sale_Text[18221]["Text111"] = "BIG SALE! BIG SALE in the Easter Discount Store from April 2nd to 8th! It`s a good chance for Level 80+ heroes"
tEaster2015_Sale_Text[18221]["Text112"] = "~to buy favorable gift packs!"
-- 活动后对白
tEaster2015_Sale_Text[18221]["Text121"] = "Oh, too much money to count. I can`t help loving myself for making such a good deal."
-- 活动中对白
tEaster2015_Sale_Text[18221]["Text131"] = "The Easter Discount Store is having a big sale now! From April 2nd to 8th, all Level 80 heroes"
tEaster2015_Sale_Text[18221]["Text132"] = "~are welcome to buy favorable gift packs in the store!"

--【如何进入特卖场？】
tEaster2015_Sale_Text[18221]["Text211"] = "I`ll send you to the Discount Store during the event time. Look, there will be 3 rounds of sale at"
tEaster2015_Sale_Text[18221]["Text212"] = "~8:00 - 10:00, 12:00 - 14:00, and 18:00 - 22:00, everyday from April 2nd to 8th. If you`re interested, find me at that time."

--【怎么抢购礼包？】
tEaster2015_Sale_Text[18221]["Text311"] = "There will be 30 Treasure Chests available for each round of sale. Each Treasure Chest is for 20,000 Silver,"
tEaster2015_Sale_Text[18221]["Text312"] = "~and each one can buy up to 3 Treasure Chests in a day. Remember, good deal waits for no man. Hurry up!"
tEaster2015_Sale_Text[18221]["Text313"] = ""

--【我要进入特卖场。】
--特卖时间外
tEaster2015_Sale_Text[18221]["Text411"] = "Buddy, it`s not the right time for the big sale. The Discount Store will open at 8:00 - 10:00, 12:00 - 14:00, and 18:00 - 22:00."
tEaster2015_Sale_Text[18221]["Text412"] = "~So, I can send you there at that time."
--等级不足
tEaster2015_Sale_Text[18221]["Text421"] = "It seems dangerous for you to go to such a crowded place. Keep practicing, and come back when you reach at least Level 80."
tEaster2015_Sale_Text[18221]["Text422"] = ""

-------------选项
tEaster2015_Sale_Text[18221]["Option11"] = "I`m~looking~forward~to~it."
tEaster2015_Sale_Text[18221]["Option12"] = "Enjoy."
tEaster2015_Sale_Text[18221]["Option13"] = "Where~is~the~store?"
tEaster2015_Sale_Text[18221]["Option14"] = "Enter~the~store."
tEaster2015_Sale_Text[18221]["Option15"] = "Return~to~Twin~City."
tEaster2015_Sale_Text[18221]["Option16"] = "I~can`t~wait~to~buy."
tEaster2015_Sale_Text[18221]["Option21"] = "What`re~the~rules?"
tEaster2015_Sale_Text[18221]["Option22"] = "I~want~to~buy."
tEaster2015_Sale_Text[18221]["Option31"] = "I~see."
tEaster2015_Sale_Text[18221]["Option32"] = "I~want~to~buy."
tEaster2015_Sale_Text[18221]["Option41"] = "Sorry,~I~forgot."
tEaster2015_Sale_Text[18221]["Option42"] = "I~see."


-- //========宝箱========\\
tEaster2015_Sale_Text[18227] = {}
tEaster2015_Sale_Text[18227]["Text111"] = "Bling, bling, bling... This is the Treasure Chest for sale. Just pay 20,000 Silver, and you can take it home."

--【我要抢购你。】
tEaster2015_Sale_Text[18227]["Text211"] = "Your inventory is too full to contain anything. Please make some room, first."
tEaster2015_Sale_Text[18227]["Text221"] = "You`ve already bought 3 Treasure Chests in the Discount Store. No more for today, okay?"
tEaster2015_Sale_Text[18227]["Text231"] = "Congratulations! This Treasure Chest belongs to you, now."
tEaster2015_Sale_Text[18227]["Text241"] = "Sorry, you don`t have enough money for the Treasure Chest."

-------------选项
tEaster2015_Sale_Text[18227]["Option11"] = "I~want~to~buy."
tEaster2015_Sale_Text[18227]["Option12"] = "I`ll~think~about~it."
tEaster2015_Sale_Text[18227]["Option21"] = "I`ll~do~it~now."
tEaster2015_Sale_Text[18227]["Option22"] = "Alright."
tEaster2015_Sale_Text[18227]["Option23"] = "Great!"
tEaster2015_Sale_Text[18227]["Option24"] = "Alright."

-------------其他
tEaster2015_Sale_Text["SaleMap"] = "You entered the Easter Discount Store."
tEaster2015_Sale_Text["DragonCity"] = "You go back to the Dragon City."
tEaster2015_Sale_Text["BoxName"] = "TreasureChest"
tEaster2015_Sale_Text["TooFar"] = "This Treasure Chest is waiting for an owner."
tEaster2015_Sale_Text["LeaveMap"] = "You should be in the Easter Discount Store to join the big sale."





------------------------------------------------------------------------------------
--Name:			[英文征服][活动脚本]复活节活动之圣火传递(4.02-4.08)
--Purpose:		复活节活动之圣火传递
--Creator: 		丁晨
--Created:		2015/02/05
------------------------------------------------------------------------------------
tEaster2015_Fire_Text = {}
tEaster2015_Fire_Text[18289] ={}
--活动前对白
tEaster2015_Fire_Text[18289]["Text111"]= "From April 2nd to 8th, I`ll light the Easter Torch in Twin City. All Level 80+ heroes"
tEaster2015_Fire_Text[18289]["Text112"]= "~can claim torch spirit from me, and deliver it to the world, as well as the best wishes."
tEaster2015_Fire_Text[18289]["Text113"]= ""

--活动后对白
tEaster2015_Fire_Text[18289]["Text114"]= "I believe the Easter Torch will bring people good luck and hope."

--活动中对白
tEaster2015_Fire_Text[18289]["Text115"]= "From April 2nd to 8th, I`ll light the Easter Torch in Twin City. All Level 80+ heroes"
tEaster2015_Fire_Text[18289]["Text116"]= "~can claim torch spirit from me, and deliver it to the world, as well as the best wishes."
tEaster2015_Fire_Text[18289]["Text117"]= ""
-----------------------------------------------Option3------------------------------------------
--已领取对白
tEaster2015_Fire_Text[18289]["Text118"]= "You`ve received the torch spirit. Hurry and deliver the spirit to the right place."
--条件不足对白
tEaster2015_Fire_Text[18289]["Text119"]= "You should reach at least Level 80 to control the torch spirit. Keep practicing, and come back when you`re qualified."
--满足条件
tEaster2015_Fire_Text[18289]["Text120"]= "You received the torch spirit. Hurry and deliver it to (278,385) in Twin City."
-----------------------------------------------Option4------------------------------------------
--已领取对白
tEaster2015_Fire_Text[18289]["Text121"]= "You`ve claimed the reward for today`s Easter Torch Delivery, haven`t you?"
--条件不足对白
tEaster2015_Fire_Text[18289]["Text122"]= "Have you delivered the torch spirit to the right place? It seems `No`. Please do your job, first."
--背包满
tEaster2015_Fire_Text[18289]["Text123"]= "Your inventory is full. Please make some room, first. I`ll be here waiting."
--领取成功
tEaster2015_Fire_Text[18289]["Text124"]= "Thanks for your service. Please take this Festival Joy Pack."
-----------------------------------------------Option5------------------------------------------
--已领取对白
tEaster2015_Fire_Text[18289]["Text125"]= "You`ve claimed the reward for ranking the 1st place in today`s Easter Torch Delivery, haven`t you?"
--条件不足对白
tEaster2015_Fire_Text[18289]["Text126"]="Sorry, you failed to rank the 1st place in today`s Easter Torch Delivery. There is not an extra reward for you."
--背包满
tEaster2015_Fire_Text[18289]["Text127"]= "Your inventory is full. Why not make some room, first?"
--领取成功
tEaster2015_Fire_Text[18289]["Text128"]= "Wow, you ranked the 1st place in today`s Easter Torch Delivery! You deserve to be rewarded with a Festival Joy Pack. Just take it!"
-----------------------------------------------Option6------------------------------------------
tEaster2015_Fire_Text[18289]["Text129"]= "The torch spirit is invisible. I`ll attach it to your soul. When you have the spirit, go light the torches at 5 places"
tEaster2015_Fire_Text[18289]["Text130"]= "~in a certain order: (257,272), (283,270), (290,252), (292,229), and (315,229) at last. When you finish the job,"
tEaster2015_Fire_Text[18289]["Text131"]= "~I`ll reward you with a Festival Joy Pack. For the hero who first completes the delivery arranged at 08:00, 12:00,"
tEaster2015_Fire_Text[18289]["Text132"]= "~16:00 and 22:00, he or she will receive an extra Festival Joy Pack."

--活动前对白
tEaster2015_Fire_Text[18289]["Option1"] = "I`m~looking~forward~to~it."
--活动后对白
tEaster2015_Fire_Text[18289]["Option2"] = "Happy~Easter~Day!"
--活动中对白
tEaster2015_Fire_Text[18289]["Option3"] = "Bless~me~with~the~spirit."
tEaster2015_Fire_Text[18289]["Option4"] = "Claim~my~reward."
tEaster2015_Fire_Text[18289]["Option5"] = "Claim~extra~reward."
tEaster2015_Fire_Text[18289]["Option6"] = "Tell~me~more."
tEaster2015_Fire_Text[18289]["Option7"] = "I`ll~talk~to~you~later."
-----------------------------------------------Option3------------------------------------------
--已领取对白
tEaster2015_Fire_Text[18289]["Option8"] = "Okay."
--条件不足对白
tEaster2015_Fire_Text[18289]["Option9"] = "I~see."
--领取成功
tEaster2015_Fire_Text[18289]["Option10"] = "Got~it."
-----------------------------------------------Option4------------------------------------------
--已领取对白
tEaster2015_Fire_Text[18289]["Option11"] = "Okay."
--条件不足对白
tEaster2015_Fire_Text[18289]["Option12"] = "I~see."
--背包满
tEaster2015_Fire_Text[18289]["Option13"] = "Okay."
--领取成功
tEaster2015_Fire_Text[18289]["Option14"] = "Thanks."
-----------------------------------------------Option5------------------------------------------
--已领取对白
tEaster2015_Fire_Text[18289]["Option15"] = "I~see."
--条件不足对白
tEaster2015_Fire_Text[18289]["Option16"] = "Alright."
--背包满
tEaster2015_Fire_Text[18289]["Option17"] = "Okay."
--领取成功
tEaster2015_Fire_Text[18289]["Option18"] = "Thanks."
-----------------------------------------------Option6------------------------------------------
tEaster2015_Fire_Text[18289]["Option19"] = "I~see."
tEaster2015_Fire_Text[18289]["Option20"] = "Auto-pathfinding."
--------------------------------105当有任务玩家踩到前四个陷阱时的提示---------------------------
tEaster2015_Fire_Text["BefTrap"]="You`ve~arrived~at~Torch~%s.~You~need~to~confirm,~and~then~go~on~to~find~Torch~%s."
--------------------------------105当有任务玩家踩到前五个陷阱时的提示---------------------------
tEaster2015_Fire_Text["EndTrap"]="You~successfully~completed~the~torch~delivery~quest.~Go~claim~your~reward~from~the~Easter~Torch~Envoy."
tEaster2015_Fire_Text["SpriteGuide"]="Do~you~want~the~torch~spirit~to~take~you~to~the~right~place?"
--------------------------------------系统广播--------------------------------------------------
tEaster2015_Fire_Text["BefBroadCast"]="The Easter Torch Delivery will start in 5 minutes to give away blessings and festival gifts!"
tEaster2015_Fire_Text["BroadCast"]="The Easter Torch Delivery has started. Hurry and join the event to get blessings and festival gifts!"
--------------------------------------活动期间不再时间段领任务----------------------------------
tEaster2015_Fire_Text["AbsentTime"]="You can join the Easter Torch Delivery at 8:00, 12:00, 16:00, or 22:00."

tEaster2015_Fire_Text["TimeGetAlready"]="You`ve already joined the event today. If you`re still interested, come and play tomorrow."


----------------------------------------------------周年庆点赞、签到----------------------------
------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]周年庆点赞、签到(4.27-5.11)
--Purpose:		周年庆活动--点赞、签到(4.27-5.11)
--Creator:		王倩娜
--Created:		2015/07/04
------------------------------------------------------------------------------------



------------------------------------------点赞-------------------------------------------
--16495	求赞狂人高大上
tAnniversary2015_Like_Text = {}
tAnniversary2015_Like_Text[16495] = {}

tAnniversary2015_Like_Text[16495]["Text111"] = "Hey, guys! Let`s hug for the forthcoming CO Anniversary Celebration! From May 7th to 27th,"
tAnniversary2015_Like_Text[16495]["Text112"] = "~all Level 80+ heroes who `Like` Conquer Online will be rewarded with my warm hug and wonderful gifts."
tAnniversary2015_Like_Text[16495]["Text113"] = "~I await your Likes!"
tAnniversary2015_Like_Text[16495]["Text121"] = "Hey, guys! Let`s hug for the CO Anniversary Celebration! From May 7th to 27th, all Level 80+ heroes who `Like` Conquer Online"
tAnniversary2015_Like_Text[16495]["Text122"] = "~will be rewarded with my warm hug and wonderful gifts."
tAnniversary2015_Like_Text[16495]["Text123"] = "~I await your Likes!"
tAnniversary2015_Like_Text[16495]["Text131"] = "The Anniversary Celebration has ended, but your love for CO still lingers in the air."

tAnniversary2015_Like_Text[16495]["Text211"] = "Sorry, you should reach at least Level 80 to join the event. Keep practicing, and come back when you`re qualified."   
tAnniversary2015_Like_Text[16495]["Text221"] = "Come on, you don`t have any `Like` to hand in. Go and collect some from the monsters in the wild."
tAnniversary2015_Like_Text[16495]["Text231"] = "The more Likes you submit, the better. For heroes handing in 100 `Like`, we`ll reward you with a nice gift."
tAnniversary2015_Like_Text[16495]["Text232"] = "~If I receive more than 10,000 Likes from this server, all participants will receive an extra reward."
tAnniversary2015_Like_Text[16495]["Text233"] = "~So, how many Likes would you like to hand in?"

tAnniversary2015_Like_Text[16495]["Text311"] = "Sorry, you don`t have enough Likes to give me."
tAnniversary2015_Like_Text[16495]["Text321"] = "You`ve successfully handed in %d Likes. Good job!"

tAnniversary2015_Like_Text[16495]["Text411"] = "You should give at least 100 Likes before you can claim a reward from me."
tAnniversary2015_Like_Text[16495]["Text421"] = "You`ve got your reward, today. Anyway, thanks for your support!"
tAnniversary2015_Like_Text[16495]["Text431"] = "Come on, your inventory is too full to contain anything. You need to make some room, first."
tAnniversary2015_Like_Text[16495]["Text441"] = "Ah, you`re really a big fan of CO! Here is a gift pack for you."

tAnniversary2015_Like_Text[16495]["Text511"] = "Don`t look at me like that. You can`t earn a reward without giving me enough Likes. Go and hunt some Likes, first."
tAnniversary2015_Like_Text[16495]["Text521"] = "You`ve already claimed the reward today, haven`t you? Let me know if you`re still interested tomorrow."
tAnniversary2015_Like_Text[16495]["Text531"] = "Wow, it`s amazing! I`ve received more than 10,000 from this server. You deserve to be nicely rewarded!"

tAnniversary2015_Like_Text[16495]["Text611"] = "You`ve handed in %d Likes. Thanks for your support."
tAnniversary2015_Like_Text[16495]["Text621"] = "You haven`t handed in any Likes. Go and make a try!"

tAnniversary2015_Like_Text[16495]["Text711"] = "So far, I`ve received %d Likes in total from this server. You`re doing a nice job!"
tAnniversary2015_Like_Text[16495]["Text721"] = "So far, I`ve received %d Likes in total from this server, requiring %d more Likes to hit 10,000 for an extra reward."

tAnniversary2015_Like_Text[16495]["Text811"] = "During the event, you`ll have a chance to get Likes while hunting in the wild. You can hand in the Likes"
tAnniversary2015_Like_Text[16495]["Text812"] = "~to me to show your love for CO. When you give 100 Likes, you can claim a reward from me. When the server"
tAnniversary2015_Like_Text[16495]["Text813"] = "~totally contribute more than 10,000 Likes, each participant will receive an extra reward."

--选项
tAnniversary2015_Like_Text[16495]["Option1"] = "I`m~looking~forward~to~it."
tAnniversary2015_Like_Text[16495]["Option2"] = "I~love~CO!"
tAnniversary2015_Like_Text[16495]["Option3"] = "Hand~in~Likes."
tAnniversary2015_Like_Text[16495]["Option4"] = "Claim~my~reward~for~100~Likes."
tAnniversary2015_Like_Text[16495]["Option5"] = "Claim~the~extra~reward."
tAnniversary2015_Like_Text[16495]["Option6"] = "View~my~submit."
tAnniversary2015_Like_Text[16495]["Option7"] = "View~server`s~`Like`~amount."
tAnniversary2015_Like_Text[16495]["Option8"] = "What`re~the~rules?"
tAnniversary2015_Like_Text[16495]["Option9"] = "Let`s~just~hug."
tAnniversary2015_Like_Text[16495]["Option10"] = "I~see."

tAnniversary2015_Like_Text[16495]["Option11"] = "1~Likes."
tAnniversary2015_Like_Text[16495]["Option12"] = "5~Likes."
tAnniversary2015_Like_Text[16495]["Option13"] = "10~Likes."
tAnniversary2015_Like_Text[16495]["Option14"] = "100~Likes."
tAnniversary2015_Like_Text[16495]["Option15"] = "I~need~a~break."
tAnniversary2015_Like_Text[16495]["Option16"] = "Great!"
tAnniversary2015_Like_Text[16495]["Option17"] = "Alright."
tAnniversary2015_Like_Text[16495]["Option18"] = "I~see."
tAnniversary2015_Like_Text[16495]["Option19"] = "Thanks!"
tAnniversary2015_Like_Text[16495]["Option20"] = "I~see."
tAnniversary2015_Like_Text[16495]["Option21"] = "It`s~my~pleasure."
tAnniversary2015_Like_Text[16495]["Option22"] = "Okay."
tAnniversary2015_Like_Text[16495]["Option23"] = "Indeed."
tAnniversary2015_Like_Text[16495]["Option24"] = "Sounds~good."

-- 提示
tAnniversary2015_Like_Text["GetGarmentPack"] = "You received a Anniversary Garment Pack. Go and pick what you like."
tAnniversary2015_Like_Text["SpaceFull"] = "Your inventory is full. You need to make some room, first."
tAnniversary2015_Like_Text["GetLike"] = "You received %d Likes. Give them to Mr. Like."

tAnniversary2015_Like_Text[3006048] = {}
tAnniversary2015_Like_Text[3006048]["Text111"] = "Cheers for CO Anniversary! From May 7th to 27th, all Level 80+ Heroes"
tAnniversary2015_Like_Text[3006048]["Text112"] = "~will be amused by 6 events for the celebration: `Sign-in Gift`, `Likes for Reward`, `Melody of Conquer`,"
tAnniversary2015_Like_Text[3006048]["Text113"] = "~`Paint Zone`, `Four Mascots` and `Carnival Barbecue`. Have a nice day!"
tAnniversary2015_Like_Text[3006048]["Text114"] = ""
tAnniversary2015_Like_Text[3006048]["Option1"] = "Take~me~to~the~celebration."

tAnniversary2015_Like_Text[3006048]["SpaceFull"] = "CO Anniversary Celebration will begin! Please make sure you still have room in the inventory for a Happy Anniversary Card!"
tAnniversary2015_Like_Text[3006048]["GetCard"] = "You received a Happy Anniversary Card. Hurry and check it in your inventory."
tAnniversary2015_Like_Text[3006048]["GetExp"] = "You received 30 minutes of EXP."

------------------------------------------签到-------------------------------------------
-- 16497 签到登记使者亲亲
tAnniversary2015_SignIn_Text = {}
tAnniversary2015_SignIn_Text[16497] = {}

tAnniversary2015_SignIn_Text[16497]["Text111"] = "The grand anniversary of CO is just around the corner! I`m here to give away wonderful gifts"
tAnniversary2015_SignIn_Text[16497]["Text112"] = "~to you guys. The only thing you need to do is sign-in. Pretty easy, huh? So, if you`re a hero"
tAnniversary2015_SignIn_Text[16497]["Text113"] = "~at Level 80 or above, just come to sign in between May 7th and May 27th. I`ll be right here waiting!"
tAnniversary2015_SignIn_Text[16497]["Option1"] = "I`ll~come~for~sure."

tAnniversary2015_SignIn_Text[16497]["Text131"] = "The anniversary event is over. See you next year!"
tAnniversary2015_SignIn_Text[16497]["Option2"] = "See~you!"

tAnniversary2015_SignIn_Text[16497]["Text121"] = "The grand anniversary of CO has arrived! I`m here to give away wonderful gifts to you guys. The only thing you need to do is sign-in."
tAnniversary2015_SignIn_Text[16497]["Text122"] = "~Pretty easy, huh? So, if you`re a hero at level 80 or above, just come to sign in between May 7th and May 27th. May I help you?"
tAnniversary2015_SignIn_Text[16497]["Option3"] = "I~wanna~sign~in!"
tAnniversary2015_SignIn_Text[16497]["Option4"] = "Check~my~sign-in~record."
tAnniversary2015_SignIn_Text[16497]["Option5"] = "Tell~me~the~rules."
tAnniversary2015_SignIn_Text[16497]["Option6"] = "No,~thanks."

tAnniversary2015_SignIn_Text[16497]["Text211"] = "During the event, you can sign in here once a day. If you sign in 3 days, 6 days, 9 days, 12 days, 13 days and 14 days in a row, I`ll award you with a nice gift!"
tAnniversary2015_SignIn_Text[16497]["Text212"] = "~Of course, if you fail to sign in continuously, you`ll have to start over."
tAnniversary2015_SignIn_Text[16497]["Option7"] = "What`s~the~gift?"
tAnniversary2015_SignIn_Text[16497]["Option8"] = "Got~it."

tAnniversary2015_SignIn_Text[16497]["Text221"] = "Every time you sign in, you`ll receive some EXP (10*sign-in days) and Study Points. After signing in a certain days in a row, you`ll receive the following gifts:\n\n"
tAnniversary2015_SignIn_Text[16497]["Text222"] = "3 days - 1 Memory Agate (B)\n"
tAnniversary2015_SignIn_Text[16497]["Text223"] = "6 days - 1 Endurance Book Pack (B)\n"
tAnniversary2015_SignIn_Text[16497]["Text224"] = "9 days - 1 EXP Ball Pack (B)\n"
tAnniversary2015_SignIn_Text[16497]["Text225"] = "12 days - 1 Quest Chance B (B)\n"
tAnniversary2015_SignIn_Text[16497]["Text226"] = "13 days - 1 Justice Scroll (B)\n"
tAnniversary2015_SignIn_Text[16497]["Text227"] = "14 days - 1 Sash (S)\n"
	
tAnniversary2015_SignIn_Text[16497]["Text311"] = "You`ve signed in %d days in a row! Good job! Keep it up!"
tAnniversary2015_SignIn_Text[16497]["Option15"] = "I~will."

tAnniversary2015_SignIn_Text[16497]["Text411"] = "Sorry, you haven`t reached Level 80 yet. Come on! Take more tough trainings! It won`t be a problem for you!"
tAnniversary2015_SignIn_Text[16497]["Option16"] = "Definitely."

tAnniversary2015_SignIn_Text[16497]["Text421"] = "Your inventory is full. Please arrange it first!"
tAnniversary2015_SignIn_Text[16497]["Option17"] = "Gimme~a~second."

tAnniversary2015_SignIn_Text[16497]["Text431"] = "Hey, you`ve signed in today! Come again tomorrow."
tAnniversary2015_SignIn_Text[16497]["Option18"] = "My~bad."

tAnniversary2015_SignIn_Text[16497]["Text441"] = "Great! You just signed up successfully! Let me see...%d days in a row. Keep it up!"
tAnniversary2015_SignIn_Text[16497]["Option19"] = "I~will!"

tAnniversary2015_SignIn_Text[16497]["Text451"] = "Great! You just signed up successfully! Let me see...%d days in a row! Here`s a(n) %s for you! Keep it up!"

--提示
tAnniversary2015_SignIn_Text["GetReward"] = "You signed up successfully and received a Festival Joy Pack!"


tAnniversary2015_SignIn_Text["RewardItem"] = {}
tAnniversary2015_SignIn_Text["RewardItem"][3] = "MemoryAgate(B)"
tAnniversary2015_SignIn_Text["RewardItem"][6] = "EnduranceBookPack(B)"
tAnniversary2015_SignIn_Text["RewardItem"][9] = "EXPBallPack(B)"
tAnniversary2015_SignIn_Text["RewardItem"][12] = "QuestChanceB(B)"
tAnniversary2015_SignIn_Text["RewardItem"][13] = "JusticeScroll(B)"
tAnniversary2015_SignIn_Text["RewardItem"][14] = "Sash(S)"



------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]调色大比拼（4.27-5.25）
--Purpose:	调色大比拼
--Creator: 	严振飞
--Created:	2015/03/17
------------------------------------------------------------------------------------

---------------------NPC对话
-- // ==调色大师王芝芝== \\
tAnniversary2015_ColorMatch_Text = {}
tAnniversary2015_ColorMatch_Text[16499] = {}
--活动前
tAnniversary2015_ColorMatch_Text[16499]["Text111"] = "I`m the best painter ever on Wind Plain. Hey, what`s the look on your face? You think I`m talking fish?"
tAnniversary2015_ColorMatch_Text[16499]["Text112"] = "~You know what, I`m gonna paint for the anniversary of CO in a few days. If you`re a hero at Level 80"
tAnniversary2015_ColorMatch_Text[16499]["Text113"] = "~or above, come and help me mix the paint between May 7th and May 27th. I`ll show you my talent then!"
tAnniversary2015_ColorMatch_Text[16499]["Text114"] = "~Of course, my awesome rewards won`t let you down!"
--活动后
tAnniversary2015_ColorMatch_Text[16499]["Text121"] = "The anniversary is over. See you next year!"
--活动中
tAnniversary2015_ColorMatch_Text[16499]["Text131"] = "I`m the best painter ever on the Wind Plain. Hey, what`s the look on your face? You think I`m talking fish?"
tAnniversary2015_ColorMatch_Text[16499]["Text132"] = "~ You know what, I`m painting for the anniversary of CO right now. If you`re a hero at level 80 or above,"
tAnniversary2015_ColorMatch_Text[16499]["Text133"] = "~help me mix the paint between May 7th and May 27th. I`ll show you my talent! Of course, my awesome rewards won`t let you down!"


---【我要调色。】
tAnniversary2015_ColorMatch_Text[16499]["Text211"] = "Ah, you haven`t reached Level 80. Why not take more trainings to get tougher?"
tAnniversary2015_ColorMatch_Text[16499]["Text221"] = "Ah, your inventory is full. Make a space for the Palette, would you?"
tAnniversary2015_ColorMatch_Text[16499]["Text231"] = "The color I need today is %s. Here`s a Palette for you. You can get paint from these paint boxes. Just use 2 or more paint"
tAnniversary2015_ColorMatch_Text[16499]["Text232"] = "~to mix into a new color. You can also check the target color and the method with the Palette. After you make it, hand in to me the new paint."
tAnniversary2015_ColorMatch_Text[16499]["Text233"] = "~Oh, keep in mind, the paint you get from the boxes will dry in 10 minutes in your inventory. Don`t waste them."
tAnniversary2015_ColorMatch_Text[16499]["Text234"] = ""


--【上交调配好的颜色。】
tAnniversary2015_ColorMatch_Text[16499]["Text311"] = "Hey, you`ve helped me today! Thanks for you help! Why not go somewhere else and have fun? See you tomorrow!"
tAnniversary2015_ColorMatch_Text[16499]["Text321"] = "Wow, you`re a genius! It`s pure and nice! Here`s your reward. You deserve it. I`m hiring apprentices recently. Any thoughts?"
tAnniversary2015_ColorMatch_Text[16499]["Text331"] = "Oh, don`t fool me, my hero. I need %s, and you haven`t got this color."
tAnniversary2015_ColorMatch_Text[16499]["Text332"] = "~Come on! Have a try! It won`t be a problem for a hero like you!"


--【我该怎么做？】
tAnniversary2015_ColorMatch_Text[16499]["Text411"] = "During the event, I`ll tell you the color I need every day. You can get paint from the paint boxes next to me."
tAnniversary2015_ColorMatch_Text[16499]["Text412"] = "~Just use 2 or more paint to mix into a new color. You can also check the target color and the method with the Palette."
tAnniversary2015_ColorMatch_Text[16499]["Text413"] = "~After you make it, hand in to me the new paint. Remember, the paint you get from the boxes will dry in 10 minutes in your inventory."
tAnniversary2015_ColorMatch_Text[16499]["Text414"] = "~Don`t waste them."
--【具体配色规则】
tAnniversary2015_ColorMatch_Text[16499]["Text511"] = "Here are the methods:\n"
tAnniversary2015_ColorMatch_Text[16499]["Text512"] = "Orange = Red + Yellow\n"
tAnniversary2015_ColorMatch_Text[16499]["Text513"] = "Purple = Red + Blue\n"
tAnniversary2015_ColorMatch_Text[16499]["Text514"] = "Green = Yellow + Blue\n"
tAnniversary2015_ColorMatch_Text[16499]["Text515"] = "Black = Orange + Blue = Purple + Yellow = Green + Red\n"


--NPC选项
tAnniversary2015_ColorMatch_Text[16499]["Option11"] = "Oh,~we`ll~see!"
tAnniversary2015_ColorMatch_Text[16499]["Option12"] = "See~you!"
tAnniversary2015_ColorMatch_Text[16499]["Option13"] = "Cool.~I~wanna~help~you~now!"
tAnniversary2015_ColorMatch_Text[16499]["Option14"] = "Gimme~another~Palette."
tAnniversary2015_ColorMatch_Text[16499]["Option15"] = "Here`s~the~mixed~paint!"
tAnniversary2015_ColorMatch_Text[16499]["Option16"] = "What~should~I~do?"
tAnniversary2015_ColorMatch_Text[16499]["Option17"] = "I~don`t~feel~like~the~smell!"
tAnniversary2015_ColorMatch_Text[16499]["Option18"] = "Okay."
tAnniversary2015_ColorMatch_Text[16499]["Option21"] = "Alright."
tAnniversary2015_ColorMatch_Text[16499]["Option22"] = "One~second."
tAnniversary2015_ColorMatch_Text[16499]["Option23"] = "I~got~you."
tAnniversary2015_ColorMatch_Text[16499]["Option31"] = "You~have~a~good~day"
tAnniversary2015_ColorMatch_Text[16499]["Option32"] = "Hell~no!~Thanks~for~the~reward."
tAnniversary2015_ColorMatch_Text[16499]["Option33"] = "You~really~know~me."
tAnniversary2015_ColorMatch_Text[16499]["Option41"] = "Show~me~the~mixing~methods."
tAnniversary2015_ColorMatch_Text[16499]["Option51"] = "I~got~it."


-- // ==红色颜料箱== \\
tAnniversary2015_ColorMatch_Text[16500] = {}
--闲聊对白
tAnniversary2015_ColorMatch_Text[16500]["Text111"] = "This is a Red Paint Box. Be careful. Don`t knock it over."
--选项
tAnniversary2015_ColorMatch_Text[16500]["Option11"] = "Get~some~paint."
tAnniversary2015_ColorMatch_Text[16500]["Option12"] = "I~don`t~need~this~color."


-- // ==黄色颜料箱== \\
tAnniversary2015_ColorMatch_Text[16501] = {}
--闲聊对白
tAnniversary2015_ColorMatch_Text[16501]["Text111"] = "This is a Yellow Paint Box. Be careful. Don`t knock it over."

--选项
tAnniversary2015_ColorMatch_Text[16501]["Option11"] = "Get~some~paint."
tAnniversary2015_ColorMatch_Text[16501]["Option12"] = "I~don`t~need~this~color."

-- // ==蓝色颜料箱== \\
tAnniversary2015_ColorMatch_Text[16502] = {}
--闲聊对白
tAnniversary2015_ColorMatch_Text[16502]["Text111"] = "This is a Blue Paint Box. Be careful. Don`t knock it over."

--选项
tAnniversary2015_ColorMatch_Text[16502]["Option11"] = "Get~some~paint."
tAnniversary2015_ColorMatch_Text[16502]["Option12"] = "I~don`t~need~this~color."



---------------------物品对白
tAnniversary2015_ColorMatch_Text["ColorDsic"] = {}
tAnniversary2015_ColorMatch_Text["ColorDsic"]["Text1"] = "The color Zoe needs today is %s."
tAnniversary2015_ColorMatch_Text["ColorDsic"]["Text2"] = "Here are the mixing methods:\n"
tAnniversary2015_ColorMatch_Text["ColorDsic"]["Text3"] = "Orange = Red + Yellow\n"
tAnniversary2015_ColorMatch_Text["ColorDsic"]["Text4"] = "Purple = Red + Blue\n"
tAnniversary2015_ColorMatch_Text["ColorDsic"]["Text5"] = "Green = Yellow + Blue\n"
tAnniversary2015_ColorMatch_Text["ColorDsic"]["Text6"] = "Black = Orange + Blue = Purple + Yellow = Green + Red\n"
tAnniversary2015_ColorMatch_Text["ColorDsic"]["Option1"] = "Close."


--颜料
tAnniversary2015_ColorMatch_Text[3003507] = "Red"
tAnniversary2015_ColorMatch_Text[3003508] = "Yellow"
tAnniversary2015_ColorMatch_Text[3003509] = "Blue"
tAnniversary2015_ColorMatch_Text[3003510] = "Orange"
tAnniversary2015_ColorMatch_Text[3003511] = "Purple"
tAnniversary2015_ColorMatch_Text[3003512] = "Green"
tAnniversary2015_ColorMatch_Text[3003513] = "Black"
--任务需求颜料
tAnniversary2015_ColorMatch_Text["Orange"] = "Orange"
tAnniversary2015_ColorMatch_Text["Violet"] = "Purple"
tAnniversary2015_ColorMatch_Text["Green"] = "Green"
tAnniversary2015_ColorMatch_Text["Black"] = "Black"


--其他
tAnniversary2015_ColorMatch_Text["HaveColorDisc"] = "You`ve got a Palette in your inventory."
tAnniversary2015_ColorMatch_Text["RewardSuccess"] = "You`ve claimed another Palette. Hurry to mix the paint as required."
tAnniversary2015_ColorMatch_Text["ExploreText"] = "Getting~some~paint..."
tAnniversary2015_ColorMatch_Text["BagFull"] = "Your inventory is full. Please arrange it first."
tAnniversary2015_ColorMatch_Text["GetColor"] = "You`ve got some %s Paint from the box. Please use it in 10 minutes before it dries."
tAnniversary2015_ColorMatch_Text["DelColorDisc"] = "The anniversary was over, and the Palette disappeared."
tAnniversary2015_ColorMatch_Text["DelColor"] = "The anniversary was over, and the %s Paint disappeared."
tAnniversary2015_ColorMatch_Text["CloseColorDisc"] = "You`ve closed the Palette."
tAnniversary2015_ColorMatch_Text["NoColorDisc"] = "Sorry, you don`t have a Palette in the inventory."
tAnniversary2015_ColorMatch_Text["UseColor"] = "You poured out some %s Paint on the Palette. Please mix it with another paint."
tAnniversary2015_ColorMatch_Text["ErrorColor"] = "You don`t have the required paint in your inventory!"
tAnniversary2015_ColorMatch_Text["SuccessColor"] = "You mixed 2 paint into %s Paint successfully!"
tAnniversary2015_ColorMatch_Text["FailColor"] = "You mixed 2 paint into a weird color. Please try another color."




------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]周年吉祥物（4.27-5.25）
--Purpose:	调色大比拼
--Creator: 	严振飞
--Created:	2015/03/17
------------------------------------------------------------------------------------

---------------------NPC对话
-- // ==吉祥物大使吉祥== \\
tAnniversary2015_Mascot_Text = {}
tAnniversary2015_Mascot_Text[16507] = {}
--活动前
tAnniversary2015_Mascot_Text[16507]["Text111"] = "The anniversary of CO is just a few days away! I wanna express my best wishes to CO, too! Mascot is a good idea, isn`t it?"
tAnniversary2015_Mascot_Text[16507]["Text112"] = "~But I don`t have enough mascots. If you`re a hero at Level 80 or above, please come and help me between May 7th and May 27th!"
tAnniversary2015_Mascot_Text[16507]["Text113"] = "~You`re gonna help this little cute girl, aren`t you? I`ll give you some marvelous gifts for sure!"

--活动后
tAnniversary2015_Mascot_Text[16507]["Text121"] = "The anniversary is over! See you next year!"
--活动中
tAnniversary2015_Mascot_Text[16507]["Text131"] = "The grand anniversary of CO has finally come! I wanna express my best wishes to CO, too! Mascot is a good idea, isn`t it?"
tAnniversary2015_Mascot_Text[16507]["Text132"] = "~But I don`t have enough mascots. If you`re a hero at Level 80 or above, please come and help me between May 7th and May 27th."
tAnniversary2015_Mascot_Text[16507]["Text133"] = "~You`re gonna help this little cute girl, aren`t you? I`ll give you some marvelous gifts for sure!"


---【我要帮你。】
tAnniversary2015_Mascot_Text[16507]["Text211"] = "You haven`t reached Level 80 yet! I don`t think you`re able to help me right now."

tAnniversary2015_Mascot_Text[16507]["Text221"] = "Here`s a Mascot Bag. Take it. From the monsters in the wild, you`ll have a chance to get 4 tokens: C, O, 4, and Ever."
tAnniversary2015_Mascot_Text[16507]["Text222"] = "~If you give different tokens to different mascots outside the Twin City, they may be willing to go with you. They`re Red Fox,"
tAnniversary2015_Mascot_Text[16507]["Text223"] = "~Blue Fox, Pink Hare and White Hare. Please bring them all to me!"
tAnniversary2015_Mascot_Text[16507]["Text224"] = ""

tAnniversary2015_Mascot_Text[16507]["Text231"] = "Your inventory is full. I can`t put it in there. Would you arrange it now?"


--【喏，给您吉祥四宝。】
tAnniversary2015_Mascot_Text[16507]["Text311"] = "Oh, you`ve brought me 4 mascots today! I just can`t express my thankfulness! If you wanna help me more, please come again tomorrow!"
tAnniversary2015_Mascot_Text[16507]["Text321"] = "Sorry, I didn`t find 4 mascots in your Mascot Bag. There must be a mistake. Would you double-check for me?"
tAnniversary2015_Mascot_Text[16507]["Text331"] = "Wow! It`s really cool! Look at them! Thanks for your help! Here`s a reward for you!"

--【我该怎么做？】
tAnniversary2015_Mascot_Text[16507]["Text411"] = "During this event, I`ll give you a Mascot Bag if you wanna help me. You`ll have a chance to get 4 tokens from monsters in the wild: C, O, 4, and Ever."
tAnniversary2015_Mascot_Text[16507]["Text412"] = "~If you give different tokens to different mascots outside the Twin City, they may be willing to go with you. They`re Red Fox, Blue Fox, Pink Hare and"
tAnniversary2015_Mascot_Text[16507]["Text413"] = "~White Hare. Please bring them all to me!"
tAnniversary2015_Mascot_Text[16507]["Text414"] = ""
--【具体配色规则】
tAnniversary2015_Mascot_Text[16507]["Text511"] = "Good question! Give `C Token` to Red Fox, `O Token` to Pink Hare,"
tAnniversary2015_Mascot_Text[16507]["Text512"] = "~`4 Token` to White Hare, and `Ever Token` to Blue Fox. Any more questions?"
tAnniversary2015_Mascot_Text[16507]["Text513"] = ""


--NPC选项
tAnniversary2015_Mascot_Text[16507]["Option11"] = "Yeah.~Even~without~the~gifts!"
tAnniversary2015_Mascot_Text[16507]["Option12"] = "See~you!"
tAnniversary2015_Mascot_Text[16507]["Option13"] = "Sure.~I~can~help~you~now."
tAnniversary2015_Mascot_Text[16507]["Option14"] = "Gimme~another~Mascot~Bag."
tAnniversary2015_Mascot_Text[16507]["Option15"] = "I`ve~brought~4~mascots~for~you!"
tAnniversary2015_Mascot_Text[16507]["Option16"] = "How~can~I~help~you?"
tAnniversary2015_Mascot_Text[16507]["Option17"] = "I`ll~come~back~later."
tAnniversary2015_Mascot_Text[16507]["Option21"] = "What~a~girl..."
tAnniversary2015_Mascot_Text[16507]["Option22"] = "Sounds~fun!"
tAnniversary2015_Mascot_Text[16507]["Option23"] = "Okay."
tAnniversary2015_Mascot_Text[16507]["Option31"] = "Sure.~I~definitely~will."
tAnniversary2015_Mascot_Text[16507]["Option32"] = "Sorry,~my~bad."
tAnniversary2015_Mascot_Text[16507]["Option33"] = "I`m~glad~you~like~them!"

tAnniversary2015_Mascot_Text[16507]["Option41"] = "Which~token~should~I~give?"
tAnniversary2015_Mascot_Text[16507]["Option51"] = "It`s~crystal~clear!"


-- // ==红狐== \\
tAnniversary2015_Mascot_Text[16508] = {}
--闲聊对白
tAnniversary2015_Mascot_Text[16508]["Text111"] = "Don`t look at me in that way! I`m not going anywhere!"
--选项
tAnniversary2015_Mascot_Text[16508]["Option11"] = "Here`s~a~token~for~you!"
tAnniversary2015_Mascot_Text[16508]["Option12"] = "Take~it~easy,~dude."
--没宠物袋对白
tAnniversary2015_Mascot_Text[16508]["Text121"] = "Haha, I won`t tell you Little Peanut has a Mascot Bag to catch me. Oh, no, what did I say?!"
tAnniversary2015_Mascot_Text[16508]["Option13"] = "Thanks~for~telling~me."


-- // ==粉兔== \\
tAnniversary2015_Mascot_Text[16512] = {}
--闲聊对白
tAnniversary2015_Mascot_Text[16512]["Text111"] = "Don`t look at me in that way! I`m not going anywhere!"
--选项
tAnniversary2015_Mascot_Text[16512]["Option11"] = "Here`s~a~token~for~you!"
tAnniversary2015_Mascot_Text[16512]["Option12"] = "Take~it~easy,~dude."

-- // ==银兔== \\
tAnniversary2015_Mascot_Text[16516] = {}
--闲聊对白
tAnniversary2015_Mascot_Text[16516]["Text111"] = "Don`t look at me in that way! I`m not going anywhere!"
--选项
tAnniversary2015_Mascot_Text[16516]["Option11"] = "Here`s~a~token~for~you!"
tAnniversary2015_Mascot_Text[16516]["Option12"] = "Take~it~easy,~dude."

-- // ==蓝狐== \\
tAnniversary2015_Mascot_Text[16520] = {}
--闲聊对白
tAnniversary2015_Mascot_Text[16520]["Text111"] = "Don`t look at me in that way! I`m not going anywhere!"
--选项
tAnniversary2015_Mascot_Text[16520]["Option11"] = "Here`s~a~token~for~you!"
tAnniversary2015_Mascot_Text[16520]["Option12"] = "I~don`t~need~this~color."



---------------------物品对白
--宠物袋
tAnniversary2015_Mascot_Text["PetPagTimeOut"] = "The anniversary was over and the Mascot Bag disappeared."
tAnniversary2015_Mascot_Text["PetMaxText"] = "You`ve had all the 4 mascots in the bag! Now hand in it to Little Peanut for your reward!"
tAnniversary2015_Mascot_Text["PetLackText"] = "You still need to catch:"
tAnniversary2015_Mascot_Text["PetLackText1"] = {}
tAnniversary2015_Mascot_Text["PetLackText1"][3] = "Red Fox."
tAnniversary2015_Mascot_Text["PetLackText1"][4] = "Pink Hare."
tAnniversary2015_Mascot_Text["PetLackText1"][5] = "White Hare."
tAnniversary2015_Mascot_Text["PetLackText1"][6] = "Blue Fox."
tAnniversary2015_Mascot_Text["PetLackText2"] = "~You need to bring back 4 mascots for Little Peanut: Red Fox, Pink Hare, White Hare and Blue Fox. Please"
tAnniversary2015_Mascot_Text["PetLackText3"] = "~~Give `C Token` to Red Fox, `O Token` to Pink Hare, `4 Token` to White Hare, and `Ever Token` to Blue Fox."
tAnniversary2015_Mascot_Text["PetLackText4"] = ""


--选项
tAnniversary2015_Mascot_Text["PetMaxText-Option1"] = "Head~to~see~Little~Peanut."
tAnniversary2015_Mascot_Text["PetMaxText-Option2"] = "Close."
tAnniversary2015_Mascot_Text["PetLackText-Option"] = "Close."

--令牌名字
tAnniversary2015_Mascot_Text["TokenName"] = {}
tAnniversary2015_Mascot_Text["TokenName"][3003515] = "C"
tAnniversary2015_Mascot_Text["TokenName"][3003516] = "O"
tAnniversary2015_Mascot_Text["TokenName"][3003517] = "4"
tAnniversary2015_Mascot_Text["TokenName"][3003518] = "Ever"


--其他
tAnniversary2015_Mascot_Text["HavePetBag"] = "You`ve had a Mascot Bag in the inventory."
tAnniversary2015_Mascot_Text["RewardSuccess"] = "You received another Mascot Bag. Hurry up to bring back 4 mascots!"
tAnniversary2015_Mascot_Text["HaveMascot"] = "You`ve got this mascot in your bag already."
tAnniversary2015_Mascot_Text["NoCorrectPet"] = "You don`t have the right token for the %s."
tAnniversary2015_Mascot_Text["AddExpTime"] = "You`ve done all you could, but the %s still refused to go with you. You received 10 minutes of EXP."
tAnniversary2015_Mascot_Text["Defeat"] = "You`ve done all you could, but the %s still refused to go with you."
tAnniversary2015_Mascot_Text["Success"] = "You made it! The %s jumped into your Mascot Bag! Good job!"
tAnniversary2015_Mascot_Text["Monster"] = "You killed the monster and found a token on the ground!"
tAnniversary2015_Mascot_Text["DelToken"] = "The anniversary was over and the `%s Token` disappeared."
tAnniversary2015_Mascot_Text["ClosePetPag"] = "You`ve already put away the Mascot Bag."







------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]周年庆活动之篝火晚会之烧烤（4.27-5.25）
--Purpose:	周年庆活动之篝火晚会之烧烤（4.27-5.25）
--Creator: 	陈浩文
--Created:	2015/02/27
------------------------------------------------------------------------------------
-- 命名前缀
-- Anniversary2015_Barbecue_
-------------双龙城 烧烤筹备专员老猪-------------
-- //双龙城 烧烤筹备专员老猪
tAnniversary2015_Barbecue_Text = {}
tAnniversary2015_Barbecue_Text[18292] = {}

-- 活动前对白
tAnniversary2015_Barbecue_Text[18292]["Text1011"] = "Barbecue time! A good anniversary can never do without a barbecue! Never! Tell you what,"
tAnniversary2015_Barbecue_Text[18292]["Text1012"] = "~I like roast chickens, ducks, fish, vegetables...Everything! Oh, no roast suckling pig!"
tAnniversary2015_Barbecue_Text[18292]["Text1013"] = "~You don`t like it, do you?"

-- 活动后对白
tAnniversary2015_Barbecue_Text[18292]["Text1021"] = "The anniversary is over. A barbecue without roast suckling pigs is really nice, isn`t it?"

-- 活动中（未领取任务）
tAnniversary2015_Barbecue_Text[18292]["Text1031"] = "Barbecue time! A good anniversary can never do without a barbecue! Never! If you`re a hero at"
tAnniversary2015_Barbecue_Text[18292]["Text1032"] = "~Level 80 or above, please help me prepare the barbecue between May 7th and May 27th."
tAnniversary2015_Barbecue_Text[18292]["Text1033"] = "~It`s kinda easy for you guys. Tell you what, you won`t work for nothing. I`ve got plenty of gifts"
tAnniversary2015_Barbecue_Text[18292]["Text1034"] = "~for you guys to grab!"

-- 活动中（已领取任务）
tAnniversary2015_Barbecue_Text[18292]["Text1041"] = "Barbecue time! A good anniversary can never do without a barbecue! Never! If you`re a hero at"
tAnniversary2015_Barbecue_Text[18292]["Text1042"] = "~Level 80 or above, please help me prepare the barbecue between May 7th and May 27th."
tAnniversary2015_Barbecue_Text[18292]["Text1043"] = "~It`s kinda easy for you guys. Tell you what, you won`t work for nothing. I`ve got plenty of gifts"
tAnniversary2015_Barbecue_Text[18292]["Text1044"] = "~for you guys to grab!"

-- 【我来帮你吧！（未领取任务）】
tAnniversary2015_Barbecue_Text[18292]["Text2011"] = "I`ll give you a Fishing Pole if you wanna help me. Just use it to get some Basses from the river."
tAnniversary2015_Barbecue_Text[18292]["Text2012"] = "~After that, go find Mrs. Stingy to buy some vegetables and kill some Pheasants for chickens. Oh, sounds pretty brutal."
tAnniversary2015_Barbecue_Text[18292]["Text2013"] = "~Anyway, roast them all beside the Bonfire! I need 3 Roasted Fish, 3 Roasted Chickens and 1 Roasted Vegetable."
tAnniversary2015_Barbecue_Text[18292]["Text2014"] = ""

-- 【接1 我来帮你吧！（已领取任务）】
tAnniversary2015_Barbecue_Text[18292]["Text2021"] = "You just use the Fishing Pole to get some Basses from the river. Then go find Mrs. Stingy to buy"
tAnniversary2015_Barbecue_Text[18292]["Text2022"] = "~some vegetables and kill some Pheasants for chickens. After that roast them all beside the Bonfire."
tAnniversary2015_Barbecue_Text[18292]["Text2023"] = "~I need 3 Roast Basses, 3 Roast Chickens and 1 Roast Vegetable."
tAnniversary2015_Barbecue_Text[18292]["Text2024"] = ""

-- 【接1 我来帮你吧！（已完成任务）】
tAnniversary2015_Barbecue_Text[18292]["Text2031"] = "Oh, you`ve helped me once today, my hero! I know you`re really busy."
tAnniversary2015_Barbecue_Text[18292]["Text2032"] = "~If you come to help me tomorrow, I would appreciate it."

-- 【领取鱼竿。（未领取任务）】
tAnniversary2015_Barbecue_Text[18292]["Text2041"] = "It`s a perfect place for barbecue, isn`t it? Will you help me with the barbecue?"
tAnniversary2015_Barbecue_Text[18292]["Text2042"] = ""

-- 【领取鱼竿。（已领取任务 补领失败 已有）】
tAnniversary2015_Barbecue_Text[18292]["Text2051"] = "Hey, you`ve got a Fishing Pole in the inventory."
tAnniversary2015_Barbecue_Text[18292]["Text2052"] = ""

-- 【领取鱼竿。（已领取任务 补领失败 背包满）】
tAnniversary2015_Barbecue_Text[18292]["Text2061"] = "Oh, you don`t have a space in the inventory for the Fishing Pole."
tAnniversary2015_Barbecue_Text[18292]["Text2062"] = ""

-- 【领取鱼竿。（已领取任务 补领成功）】
tAnniversary2015_Barbecue_Text[18292]["Text2071"] = "Here it is. Don`t lose it again, hero. I don`t have too many of them."
tAnniversary2015_Barbecue_Text[18292]["Text2072"] = ""

-- 【领取鱼竿。（已完成任务）】
tAnniversary2015_Barbecue_Text[18292]["Text2081"] = "Oh, you`ve helped me once today, my hero! I know you`re really busy."
tAnniversary2015_Barbecue_Text[18292]["Text2082"] = "~If you come to help me tomorrow, I would appreciate it."


-- 【上交烧烤。（未领取任务）】
tAnniversary2015_Barbecue_Text[18292]["Text2091"] = "Sorry, you didn`t even get a Fishing Pole from me. How could you get what I need?"
tAnniversary2015_Barbecue_Text[18292]["Text2092"] = ""
tAnniversary2015_Barbecue_Text[18292]["Text2093"] = ""

-- 【上交烧烤。（已领取任务 失败 不够）】
tAnniversary2015_Barbecue_Text[18292]["Text2101"] = "Let me see...Sorry, you don`t have all I need. Remember? 3 Roast Basses, 3 Roast Chickens and 1 Roast Vegetable."
tAnniversary2015_Barbecue_Text[18292]["Text2102"] = ""
-- 【上交烧烤。（已领取任务 失败 背包满）】
tAnniversary2015_Barbecue_Text[18292]["Text2111"] = "You don`t have enough spaces in the inventory for my reward! You know what to do?"
tAnniversary2015_Barbecue_Text[18292]["Text2112"] = "~Clear at least 1 empty space!"

-- 【上交烧烤。（已领取任务 成功）】
tAnniversary2015_Barbecue_Text[18292]["Text2121"] = "Great! You`ve got all I need! They look so delicious! Here`s your reward!"
tAnniversary2015_Barbecue_Text[18292]["Text2122"] = ""
tAnniversary2015_Barbecue_Text[18292]["Text2123"] = ""

-- 【上交烧烤。（已完成任务）】
tAnniversary2015_Barbecue_Text[18292]["Text2131"] = "Oh, you`ve helped me once today, my hero! I know you`re really busy."
tAnniversary2015_Barbecue_Text[18292]["Text2132"] = "~If you come to help me tomorrow, I would appreciate it."

-- 【要怎么帮忙？】
tAnniversary2015_Barbecue_Text[18292]["Text2141"] = "I`ll give you a Fishing Pole. You just use it to get some Basses from the river. After that, go find"
tAnniversary2015_Barbecue_Text[18292]["Text2142"] = "~Mrs. Stingy to buy some vegetables and kill some Pheasants for chickens. Oh, sounds pretty brutal."
tAnniversary2015_Barbecue_Text[18292]["Text2143"] = "~Anyway, roast them all beside the Bonfire! I need 3 Roast Basses, 3 Roast Chickens and 1 Roast Vegetable."
tAnniversary2015_Barbecue_Text[18292]["Text2144"] = "~Of course, if you wanna roast some for yourself, I won`t stop you."

-- 【我来帮你吧！】 【我来帮忙吧。（失败 等级不足）】
tAnniversary2015_Barbecue_Text[18292]["Text3011"] = "Oh, you haven`t reached Level 80, hero. Why not take more trainings and get tougher?"
tAnniversary2015_Barbecue_Text[18292]["Text3012"] = ""
-- 【我来帮你吧！】 【我来帮忙吧。（失败 今天已经完成过一次了）】
tAnniversary2015_Barbecue_Text[18292]["Text3021"] = "Oh, you`ve helped me once today, my hero! I know you`re really busy."
tAnniversary2015_Barbecue_Text[18292]["Text3022"] = "~If you come to help me tomorrow, I would appreciate it."
-- 【我来帮你吧！】 【我来帮忙吧。（失败 背包已满）】
tAnniversary2015_Barbecue_Text[18292]["Text3031"] = "Oh, you don`t have a space in the inventory for the Fishing Pole."
tAnniversary2015_Barbecue_Text[18292]["Text3032"] = ""
-- 【我来帮你吧！】 【我来帮忙吧。（背包中没鱼竿 成功）】
--105提示：烧烤筹备专员开心地把一根鱼竿塞给了你，并满脸期待地望着你。你快去准备烧烤吧！

-- 【我来帮你吧！】 【我来帮忙吧。（背包中有鱼竿 成功）】
--1010 2005提示：阁下的背包中已经有一根鱼竿了，快去准备烧烤吧！（接任务）

--选项
tAnniversary2015_Barbecue_Text[18292]["Option101"] = "It~really~tastes~good,~hmm..."
tAnniversary2015_Barbecue_Text[18292]["Option102"] = "Look~at~you..."
tAnniversary2015_Barbecue_Text[18292]["Option103"] = "Sure.~Let`s~do~this!"
tAnniversary2015_Barbecue_Text[18292]["Option104"] = "Gimme~a~Fishing~Pole."
tAnniversary2015_Barbecue_Text[18292]["Option105"] = "Go~buy~some~vegetable."
tAnniversary2015_Barbecue_Text[18292]["Option106"] = "Go~get~some~chickens."
tAnniversary2015_Barbecue_Text[18292]["Option107"] = "I`ve~got~what~you~want!"
tAnniversary2015_Barbecue_Text[18292]["Option108"] = "How~can~I~help~you?"
tAnniversary2015_Barbecue_Text[18292]["Option109"] = "Ouch!~My~belly~hurts!"
--【我来帮你吧！】
tAnniversary2015_Barbecue_Text[18292]["Option201"] = "Sure.~I~can~help."
tAnniversary2015_Barbecue_Text[18292]["Option202"] = "I`m~kinda~busy~right~now."
tAnniversary2015_Barbecue_Text[18292]["Option203"] = "Sounds~easy."
tAnniversary2015_Barbecue_Text[18292]["Option204"] = "See~you~tomorrow."
--【领取鱼竿。】
tAnniversary2015_Barbecue_Text[18292]["Option205"] = "Sure."
tAnniversary2015_Barbecue_Text[18292]["Option206"] = "I~see."
tAnniversary2015_Barbecue_Text[18292]["Option207"] = "Wait~a~second."
tAnniversary2015_Barbecue_Text[18292]["Option208"] = "Thanks."
tAnniversary2015_Barbecue_Text[18292]["Option209"] = "See~you~tomorrow."
--【上交烧烤。】
tAnniversary2015_Barbecue_Text[18292]["Option210"] = "My~bad."
tAnniversary2015_Barbecue_Text[18292]["Option211"] = "My~bad."
tAnniversary2015_Barbecue_Text[18292]["Option212"] = "One~second."
tAnniversary2015_Barbecue_Text[18292]["Option213"] = "Cool!"
tAnniversary2015_Barbecue_Text[18292]["Option214"] = "See~you~tomorrow."
-- 【要怎么帮忙？】
tAnniversary2015_Barbecue_Text[18292]["Option215"] = "How~about~roasting~a~pig?"
--【我来帮你吧！】 【我来帮忙吧。】
tAnniversary2015_Barbecue_Text[18292]["Option301"] = "Sure."
tAnniversary2015_Barbecue_Text[18292]["Option302"] = "See~you~tomorrow."
tAnniversary2015_Barbecue_Text[18292]["Option303"] = "Wait~a~second."


-------------双龙城 精明的大婶-------------
-- //双龙城 精明的大婶
tAnniversary2015_Barbecue_Text[18294] = {}

-- 非活动时间 或者 活动时间未接任务
tAnniversary2015_Barbecue_Text[18294]["Text111"] = "Fresh vegetables! Fair price! Guys, take a look!"
-- 活动中 且 接有任务对白
tAnniversary2015_Barbecue_Text[18294]["Text121"] = "Fresh vegetables! Fair price! Guys, take a look!"

-- 【我要买菜。】
tAnniversary2015_Barbecue_Text[18294]["Text211"] = "Wanna buy some? 1 piece of vegetable is for 10 Silver. If you buy 10 pieces,"
tAnniversary2015_Barbecue_Text[18294]["Text212"] = "~I`ll give you 10% discount! God. Why would they call me stingy?"

-- 【我要买菜。】【给我来一篮吧。】
tAnniversary2015_Barbecue_Text[18294]["Text311"] = "You don`t have enough Silver. Make sure you bring the wallet, next time."
tAnniversary2015_Barbecue_Text[18294]["Text321"] = "Your inventory is too full for the vegetable. Would you arrange it now?"

-- 【我要买菜。】【给我来十篮！】
tAnniversary2015_Barbecue_Text[18294]["Text331"] = "You don`t have enough Silver. Make sure you bring the wallet, next time."
tAnniversary2015_Barbecue_Text[18294]["Text341"] = "Your inventory is too full for the vegetable. Would you arrange it now?"

--选项
tAnniversary2015_Barbecue_Text[18294]["Option11"] = "Sounds~good."
tAnniversary2015_Barbecue_Text[18294]["Option12"] = "I~wanna~buy~some."
tAnniversary2015_Barbecue_Text[18294]["Option13"] = "Sounds~good."
tAnniversary2015_Barbecue_Text[18294]["Option21"] = "I~want~1~piece."
tAnniversary2015_Barbecue_Text[18294]["Option22"] = "I~want~10~pieces."
tAnniversary2015_Barbecue_Text[18294]["Option31"] = "I`ll~think~about~it."
tAnniversary2015_Barbecue_Text[18294]["Option32"] = "Alright."
tAnniversary2015_Barbecue_Text[18294]["Option33"] = "I`ll~think~about~it."
tAnniversary2015_Barbecue_Text[18294]["Option34"] = "Okay."

-- 系统提示 
--105
tAnniversary2015_Barbecue_Text["MsgBox"] = {}
tAnniversary2015_Barbecue_Text["MsgBox"]["YuganGet"] = "You received a Fishing Pole."
tAnniversary2015_Barbecue_Text["MsgBox"]["YuganReGet"] = "You received another Fishing Pole."
tAnniversary2015_Barbecue_Text["MsgBox"]["YuganGoto"] = "Go fishing now?"
tAnniversary2015_Barbecue_Text["MsgBox"]["YuganGetFish"] = "Good job! You`ve got a Bass!"
tAnniversary2015_Barbecue_Text["MsgBox"]["BarbecueGoto"] = "You`d better get closer to the Bonfire to roast it."
tAnniversary2015_Barbecue_Text["MsgBox"]["Eat"] = "You ate the Roast Bass and received 5 minutes of EXP."
--1010
tAnniversary2015_Barbecue_Text["TalkChannel2005"]={}
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganBefTime"] = "It`s the Fishing Pole given by Barbecue Pigsy. You`d better ask him how to use it."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganAfTime"] = "The barbecue was over, and the Fishing Pole disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganNoAccept"] = "Barbecue Pigsy needs help right now. Why not find him to learn more details?"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganNotInMap"] = "Failed to enable auto-pathfinding. Please go to the Wind Plain first."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganComplete"] = "You`d better take a rest and go fishing again tomorrow."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganNoLeftSpace"] = "Because your inventory is full, you have to free the Bass."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganMiss"] = "You`ve lost your Fishing Pole. Please get another one from Barbecue Pigsy."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuganInBag"] = "There is a Fishing Pole in your inventory. Gear up for the barbecue!"
--大嘴巴鱼
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuBefTime"] = "This Bass looks very delicious."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuAfTime"] = "The barbecue was over, and the Bass disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuComplete"] = "You can`t roast more Basses today."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuNoAccept"] = "Barbecue Pigsy needs help right now. Why not find him to learn more details?"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuNotInMap"] = "You should be on Wind Plain to enable auto-pathfinding."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuNoLeftSpace"] = "Your inventory is full. Please clear at least 1 empty space, first."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuNotInBag"] = "You don`t have a Bass in the inventory."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuSuccess"] = "You received a Roast Bass! It smells really good!"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuNotSuccess"] = "You didn`t roast long enough and received an Unroasted Bass."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["YuFail"] = "You were too close to the Bonfire, and the Bass was burnt and disappeared!"
--鲜嫩小鸡肉
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouBefTime"] = "The Chicken can be used during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouAfTime"] = "The barbecue was over, and the Chicken disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouComplete"] = "You can`t roast more Chickens today."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouNoAccept"] = "Barbecue Pigsy needs help right now. Why not find him to learn more details?"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouNotInMap"] = "You should be on Wind Plain to enable auto-pathfinding."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouNoLeftSpace"] = "Your inventory is full. Please clear at least 1 empty space, first."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouNotInBag"] = "There`s no Chicken in your inventory."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouSuccess"] = "You received a Roast Chicken! It smells really good!"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouNotSuccess"] = "You didn`t roast long enough and received an Unroasted Chicken."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["JirouFail"] = "You were too close to the Bonfire, and the Chicken was burnt and disappeared!"

--一份蔬菜
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiBefTime"] = "The Vegetable can be used during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiAfTime"] = "The barbecue was over, and the Vegetable disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiComplete"] = "You can`t roast more Vegetables today."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiNoAccept"] = "Barbecue Pigsy needs help right now. Why not find him to learn more details?"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiNotInMap"] = "You should be on Wind Plain to enable auto-pathfinding."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiNoLeftSpace"] = "Your inventory is full. Please clear at least 1 empty space, first."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiNotInBag"] = "There`s no Vegetable in your inventory."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiSuccess"] = "You received a Roast Vegetable! It smells really good!"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiNotSuccess"] = "You didn`t roast long enough and received an Unroasted Vegetable."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShucaiFail"] = "You were too close to the Bonfire, and the Vegetable was burnt and disappeared!"
--烤鱼
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoyuBefTime"] = "The Roast Bass can be eaten during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoyuAfTime"] = "The barbecue was over, and the Roast Bass disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoyuComplete"] = "You can`t eat more than 10 Roast Basses a day!"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoyuFullLevel"] = "You`re at the highest level and can`t get more EXP."
--烤鸡肉
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaojirouBefTime"] = "The Roast Chicken can be eaten during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaojirouAfTime"] = "The barbecue was over, and the Roast Chicken disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaojirouComplete"] = "You can`t eat more than 10 Roast Chickens a day!"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaojirouFullLevel"] = "You`re at the highest level and can`t get more EXP."
--烤蔬菜
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoshucaiBefTime"] = "The Roast Vegetable can be eaten during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoshucaiAfTime"] = "The barbecue was over, and the Roast Vegetable disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoshucaiComplete"] = "You can`t eat more than 10 pieces of Roast Vegetable a day!"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["KaoshucaiFullLevel"] = "You`re at the highest level and can`t get more EXP."

--未熟的烤鱼
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuBefTime"] = "The Unroasted Bass can be used during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuAfTime"] = "The barbecue was over, and the Unroasted Bass disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuComplete"] = "You can`t roast more Basses today."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuNoAccept"] = "Barbecue Pigsy needs help right now. Why not find him to learn more details?"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuNotInMap"] = "You should be on Wind Plain to enable auto-pathfinding."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuNoLeftSpace"] = "Your inventory is full. Please clear at least 1 empty space, first."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuNotInBag"] = "There`s no Unroasted Bass in your inventory."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengyuSuccess"] = "You received a Roast Bass! It smells really good!"
--未熟的烤鸡肉
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouBefTime"] = "The Unroasted Chicken can be used during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouAfTime"] = "The barbecue was over, and the Unroasted Chicken disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouComplete"] = "You can`t roast more Chickens today."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouNoAccept"] = "Barbecue Pigsy needs help right now. Why not find him to learn more details?"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouNotInMap"] = "You should be on Wind Plain to enable auto-pathfinding."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouNoLeftSpace"] = "Your inventory is full. Please clear at least 1 empty space, first."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouNotInBag"] = "There`s no Unroasted Chicken in your inventory."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengjirouSuccess"] = "You received a Roast Chicken! It smells really good!"
--未熟的烤蔬菜
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiBefTime"] = "The Unroasted Vegetable can be used during the anniversary."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiAfTime"] = "The barbecue was over, and the Unroasted Vegetable disappeared."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiComplete"] = "You can`t roast more Vegetable today."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiNoAccept"] = "Barbecue Pigsy needs help right now. Why not find him to learn more details?"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiNotInMap"] = "You should be on Wind Plain to enable auto-pathfinding."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiNoLeftSpace"] = "Your inventory is full. Please clear at least 1 empty space, first."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiNotInBag"] = "There`s no Unroasted Vegetable in your inventory."
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["ShengshucaiSuccess"] = "You received a piece of Roast Vegetable! It smells really good!"
tAnniversary2015_Barbecue_Text["TalkChannel2005"]["GetJirou"] = "You can collect fresh chickens from the Pheasants."
--3701 读条
tAnniversary2015_Barbecue_Text["SetExplore"] = {}
tAnniversary2015_Barbecue_Text["SetExplore"]["YuganFishing"] = "Fishing..."
tAnniversary2015_Barbecue_Text["SetExplore"]["Barbecuing"] = "Roasting..."

------------------------------------------------------------------------------------








------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]周年庆活动之篝火晚会之喝酒唱歌（4.27-5.25）
--Creator: 	陈浩文
--Created:	2015/02/27
------------------------------------------------------------------------------------
-- 命名前缀
-- Anniversary2015_Sing_
-------------双龙城 欢乐音乐大师-------------
-- //双龙城 欢乐音乐大师
tAnniversary2015_Sing_Text = {}
tAnniversary2015_Sing_Text[18298] = {}

-- 活动前对白
tAnniversary2015_Sing_Text[18298]["Text1011"] = "Music time! Music is the best way to celebrate the anniversary, isn`t it? If you`re a hero at"
tAnniversary2015_Sing_Text[18298]["Text1012"] = "~Level 80 or above, just come and play music between May 7th and May 27th! Show me what you got!"
tAnniversary2015_Sing_Text[18298]["Text1013"] = ""

-- 活动后对白
tAnniversary2015_Sing_Text[18298]["Text1021"] = "The anniversary is over. See you next year!"

-- 活动中（未领取任务）
tAnniversary2015_Sing_Text[18298]["Text1031"] = "Music time! Music is the best way to celebrate the anniversary, isn`t it? How`s your gift of music?"
tAnniversary2015_Sing_Text[18298]["Text1032"] = "~Actions speak louder than words. If you can play the right notes as required, I`ll award you with a cool gift."
tAnniversary2015_Sing_Text[18298]["Text1033"] = "~If not, you`ll be punished to have a drink! Don`t get drunk, anyway! So, you`re ready?"
tAnniversary2015_Sing_Text[18298]["Text1034"] = ""

-- 活动中（已领取任务）
tAnniversary2015_Sing_Text[18298]["Text1041"] = "Music time! Music is the best way to celebrate the anniversary, isn`t it? How`s your gift of music?"
tAnniversary2015_Sing_Text[18298]["Text1042"] = "~Actions speak louder than words. If you can play the right notes as required, I`ll award you with a cool gift."
tAnniversary2015_Sing_Text[18298]["Text1043"] = "~If not, you`ll be punished to have a drink! Don`t get drunk, anyway! So, you`re ready?"
tAnniversary2015_Sing_Text[18298]["Text1044"] = ""

-- 【我来挑战琴谱。（今日已完成）】
tAnniversary2015_Sing_Text[18298]["Text2011"] = "Hah! You really enjoy it, don`t you? However, you can only play it once a day. Just come tomorrow!"
tAnniversary2015_Sing_Text[18298]["Text2012"] = ""

-- 【我来挑战琴谱。（失败 等级不足）】
tAnniversary2015_Sing_Text[18298]["Text2021"] = "Sorry, you haven`t reached Level 80. Why not take more trainings to get tougher?"
tAnniversary2015_Sing_Text[18298]["Text2022"] = ""

-- 【我来挑战琴谱。（成功）】
tAnniversary2015_Sing_Text[18298]["Text2031"] = "The notes I gave you are Do1, Re2, Mi3, Fa4, So5, La6 and Ti7. Once you start, you need to play"
tAnniversary2015_Sing_Text[18298]["Text2032"] = "~the right notes one by one as required in 15 seconds. If you play the wrong note, you`ll be punished"
tAnniversary2015_Sing_Text[18298]["Text2033"] = "~to have a drink in 15 seconds before you continue. If you fail to play the right note in 15 seconds,"
tAnniversary2015_Sing_Text[18298]["Text2034"] = "~you`ll have to drink and start again. Are you ready?"

-- 【领取音符和美酒。（失败 今日任务已完成）】
tAnniversary2015_Sing_Text[18298]["Text2041"] = "Oh, you`ve played this game today. You really enjoy it, don`t you? Why not leave them to other people?"
tAnniversary2015_Sing_Text[18298]["Text2042"] = ""

-- 【领取音符和美酒。（失败 背包空间不足）】
tAnniversary2015_Sing_Text[18298]["Text2051"] = "Look, I have 7 notes and 1 bottle of Good Wine. Please make sure you have at least 8 spaces in your inventory, first."
tAnniversary2015_Sing_Text[18298]["Text2052"] = ""
tAnniversary2015_Sing_Text[18298]["Text2053"] = ""

-- 【领取音符和美酒。（失败 都有）】
tAnniversary2015_Sing_Text[18298]["Text2061"] = "Hey, you`ve got all the 7 notes and a Good Wine! Take a look at your inventory!"
tAnniversary2015_Sing_Text[18298]["Text2062"] = ""
tAnniversary2015_Sing_Text[18298]["Text2063"] = ""
-- 【领取音符和美酒。（成功）】
--105提示：欢乐音乐大师补全了你的音符和美酒，重新开始挑战吧！


-- 【领取奖励。（失败-已领取）】
tAnniversary2015_Sing_Text[18298]["Text2071"] = "Sorry, you`ve got your gift already."
tAnniversary2015_Sing_Text[18298]["Text2072"] = ""

-- 【领取奖励。（失败-未挑战）】
tAnniversary2015_Sing_Text[18298]["Text2081"] = "Come on, you need to sign up for this game, first."
tAnniversary2015_Sing_Text[18298]["Text2082"] = ""

-- 【领取奖励。（失败-背包满）】
tAnniversary2015_Sing_Text[18298]["Text2091"] = "Your inventory is full. Please make some room, first."
tAnniversary2015_Sing_Text[18298]["Text2092"] = ""

-- 【领取奖励。（成功）】
tAnniversary2015_Sing_Text[18298]["Text2101"] = "You`re really a music genius! Here`s your reward!"
tAnniversary2015_Sing_Text[18298]["Text2102"] = ""
tAnniversary2015_Sing_Text[18298]["Text2103"] = ""

-- 【要怎么挑战？】
tAnniversary2015_Sing_Text[18298]["Text2111"] = "I`ll give you 7 notes: Do1, Re2, Mi3, Fa4, So5, La6 and Ti7. Just right click them to play."
tAnniversary2015_Sing_Text[18298]["Text2112"] = "~You need to play the right notes one by one as required in 15 seconds. If you play the wrong note,"
tAnniversary2015_Sing_Text[18298]["Text2113"] = "~you`ll be punished to have a drink in 15 seconds before you continue. If you fail to play the right note in 15 seconds, you`ll have to drink and start again."

-- 【我来挑战琴谱。】 【我要挑战。（背包空间不足）】
tAnniversary2015_Sing_Text[18298]["Text3011"] = "Please prepare 8 spaces in your inventory first."
tAnniversary2015_Sing_Text[18298]["Text3012"] = ""


-- 【我来挑战琴谱。】 【我要挑战。（成功）】
--（成功-空间足够直接给一套音符+酒）（接受任务，掩码置为1）

--选项
tAnniversary2015_Sing_Text[18298]["Option101"] = "Cool!~I`ll~play!"
tAnniversary2015_Sing_Text[18298]["Option102"] = "See~you!"
tAnniversary2015_Sing_Text[18298]["Option103"] = "Yeah.~Let`s~start!"
tAnniversary2015_Sing_Text[18298]["Option104"] = "Gimme~the~notes~and~wine!"
tAnniversary2015_Sing_Text[18298]["Option105"] = "I`m~coming~for~the~reward."
tAnniversary2015_Sing_Text[18298]["Option106"] = "Sounds~fun!~Tell~me~more."
tAnniversary2015_Sing_Text[18298]["Option107"] = "=Sorry,~I`m~an~idiot~in~music."
--【我来挑战琴谱。】
tAnniversary2015_Sing_Text[18298]["Option201"] = "Oh,~I~just~can`t~wait."
tAnniversary2015_Sing_Text[18298]["Option202"] = "Sure."
tAnniversary2015_Sing_Text[18298]["Option203"] = "Yes!"
tAnniversary2015_Sing_Text[18298]["Option204"] = "I`m~not~ready,~yet."
--【领取音符和美酒。】
tAnniversary2015_Sing_Text[18298]["Option205"] = "My~bad."
tAnniversary2015_Sing_Text[18298]["Option206"] = "Sure.~One~second."
tAnniversary2015_Sing_Text[18298]["Option207"] = "My~bad."
--【领取奖励。】
tAnniversary2015_Sing_Text[18298]["Option208"] = "Sorry,~I~forgot."
tAnniversary2015_Sing_Text[18298]["Option209"] = "Okay."
tAnniversary2015_Sing_Text[18298]["Option210"] = "One~second."
tAnniversary2015_Sing_Text[18298]["Option211"] = "Really?~Am~I?~Thank~you!"
-- 【要怎么挑战？】
tAnniversary2015_Sing_Text[18298]["Option212"] = "I~see."
--【我来挑战琴谱。】 【我要挑战。】
tAnniversary2015_Sing_Text[18298]["Option301"] = "Okay."

-- 系统提示 
--105
tAnniversary2015_Sing_Text["MsgBox"] = {}
tAnniversary2015_Sing_Text["MsgBox"]["GetItem"] = "You`ve received 7 notes and a Good Wine from Debra."
tAnniversary2015_Sing_Text["MsgBox"]["PlayBegin"] = "Now, please play %s"
tAnniversary2015_Sing_Text["MsgBox"]["PlayNow"] = "Good job! Now, please play %s"
tAnniversary2015_Sing_Text["MsgBox"]["PlayEnd"] = "Well done! Now you can get a reward from Debra!"

--1010
tAnniversary2015_Sing_Text["TalkChannel2005"]={}
tAnniversary2015_Sing_Text["TalkChannel2005"]["WineBefTime"] = "The Good Wine can be used during the anniversary."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Do1BefTime"] = "The Do1 can be used to play Do1 during the anniversary."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Re2BefTime"] = "The Re2 can be used to play Re2 during the anniversary."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Mi3BefTime"] = "The Mi3 can be used to play Mi3 during the anniversary."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Fa4BefTime"] = "The Fa4 can be used to play Fa4 during the anniversary."
tAnniversary2015_Sing_Text["TalkChannel2005"]["So5BefTime"] = "The So5 can be used to play So5 during the anniversary."
tAnniversary2015_Sing_Text["TalkChannel2005"]["La6BefTime"] = "The La6 can be used to play La6 during the anniversary."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Ti7BefTime"] = "The Ti7 can be used to play Ti7 during the anniversary."

tAnniversary2015_Sing_Text["TalkChannel2005"]["WineAfTime"] = "The anniversary was over, and the Good Wine disappeared."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Do1AfTime"] = "The anniversary was over, and the Do1 Note disappeared."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Re2AfTime"] = "The anniversary was over, and the Re2 Note disappeared."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Mi3AfTime"] = "The anniversary was over, and the Mi3 Note disappeared."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Fa4AfTime"] = "The anniversary was over, and the Fa4 Note disappeared."
tAnniversary2015_Sing_Text["TalkChannel2005"]["So5AfTime"] = "The anniversary was over, and the So5 Note disappeared."
tAnniversary2015_Sing_Text["TalkChannel2005"]["La6AfTime"] = "The anniversary was over, and the La6 Note disappeared."
tAnniversary2015_Sing_Text["TalkChannel2005"]["Ti7AfTime"] = "The anniversary was over, and the Ti7 Note disappeared."

tAnniversary2015_Sing_Text["TalkChannel2005"]["PlayError"] = "You played the wrong note! Please drink up the Good Wine in 15 seconds and continue. "
tAnniversary2015_Sing_Text["TalkChannel2005"]["PlayOverTimePlay"] = "You were too slow! Please drink up the Good Wine and start again. "
tAnniversary2015_Sing_Text["TalkChannel2005"]["PlayOverTimeDrink"] = "Please drink up the Good Wine before you continue. "
tAnniversary2015_Sing_Text["TalkChannel2005"]["PlayDrink"] = "You drank up the Good Wine! It tasted really good! Keep going!"
tAnniversary2015_Sing_Text["TalkChannel2005"]["PlayNoNeedDrink"] = "The wine is exclusively prepared for `Melody of Conquer` event. Talk to Musician Debra for more details."

------------------------------------------------------------------------------------

------------------------------------------------------------------------------------
---Name:150616[英文征服][任务脚本]八阵图调查(7.2-7.16)
--Creator: 	陈莺
--Created:	2015/06/16
--------------------------------------------------------------------------------

tBackpackLetter_Text[3006797] = {}
tBackpackLetter_Text[3006797]["NoSpace"] = "Your inventory is full. Please make some room, and login again to get a Quest Questionnaire."
tBackpackLetter_Text[3006797]["RewardItem"] = "You received a Quest Questionnaire. Hurry and check it in your inventory."
tBackpackLetter_Text[3006797]["OverTime"] = "The questionnaire has expired, and you threw it away."
tBackpackLetter_Text[3006797]["RewardExp"] = "You checked the questionnaire, and received 30 minutes of EXP!"
tBackpackLetter_Text[3006797]["Cultivation"] = "You received 15 Study Points!"

tBackpackLetter_Text[3006797]["Text111"] = "Subject: Survey Regarding the Quest, Eight-Diagram Dimensions (Moon Box)\n\n"
tBackpackLetter_Text[3006797]["Text112"] = "Content:"
tBackpackLetter_Text[3006797]["Text113"] = "Do you know the Moon Box? Have you ever tried or completed the quest Eight-Diagram Dimensions?"
tBackpackLetter_Text[3006797]["Text114"] = "~Here is a survey regarding this quest. we sincerely hope that "
tBackpackLetter_Text[3006797]["Text115"] = "~you could answer it. Thanks!"
tBackpackLetter_Text[3006797]["Option1"] = "Give~my~answer."
tBackpackLetter_Text[3006797]["Option2"] = "Discard."

tBackpackLetter_Text[3006797]["WebDialog"] = "http://poll.99.com/survey.php?sv_id=704"



------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
--Name:			[简体征服][活动脚本]2015暑期活动-冰镇西瓜(7.9-7.22)
--Purpose:		
--Creator: 		张磊
--Created:		05/11/2015
------------------------------------------------------------------------------------

SummerActivities_Watermelon_Text = {}

--西瓜小贩王坡
SummerActivities_Watermelon_Text[15806] = {}
--活动时间前
SummerActivities_Watermelon_Text[15806]["Text111"] = "Hey, guys! A new batch of watermelons will arrive on July 9th - 22nd."
SummerActivities_Watermelon_Text[15806]["Text112"] = "~Don`t miss your chance to taste the sweetest watermelons!"
SummerActivities_Watermelon_Text[15806]["Text113"] = ""

SummerActivities_Watermelon_Text[15806]["Option1"] = "I`m~looking~forward~to~it."

--活动时间内
SummerActivities_Watermelon_Text[15806]["Text121"] = "Hey! Do you know watermelons taste better after being iced? I really want to try, but I can`t walk away."
SummerActivities_Watermelon_Text[15806]["Text122"] = "~If you`ve reached Level 80, bring me some Iced Watermelons. I`ve prepared some wonderful rewards."
SummerActivities_Watermelon_Text[15806]["Text123"] = ""
SummerActivities_Watermelon_Text[15806]["Text124"] = ""

SummerActivities_Watermelon_Text[15806]["Option2"] = "Buy~a~watermelon."
SummerActivities_Watermelon_Text[15806]["Option3"] = "I~have~a~Iced~Watermelon."
SummerActivities_Watermelon_Text[15806]["Option4"] = "What~can~I~do?"
SummerActivities_Watermelon_Text[15806]["Option5"] = "I`m~busy."


--活动时间外
SummerActivities_Watermelon_Text[15806]["Text131"] = "Thanks for your help! I feel my power recovered after eating the Iced Watermelon."

SummerActivities_Watermelon_Text[15806]["Option6"] = "That`s~good."


--等级不够提示
SummerActivities_Watermelon_Text[15806]["Text211"] = "Kid, it takes time to ice watermelons. Why not come back to me when you reach Level 80?"
SummerActivities_Watermelon_Text[15806]["Text212"] = ""

SummerActivities_Watermelon_Text[15806]["Option7"] = "Alright."

--玩家身上银两不够
SummerActivities_Watermelon_Text[15806]["Text311"] = "Come on, it can`t be cheaper. I only charge 100 Silver for a Big Watermelon. Make sure you bring enough money next time."

--玩家背包空间不足
SummerActivities_Watermelon_Text[15806]["Text411"] = "Hey, your inventory is full. Come back when you have some room for the Big Watermelon. I`ll be here waiting."

--当天已经领取了奖励
SummerActivities_Watermelon_Text[15806]["Text511"] = "Thanks for bringing me such a cold and sweet watermelon! It`s cool!"

SummerActivities_Watermelon_Text[15806]["Option8"] = "You`re~welcome."

--上交冰镇西瓜时没有西瓜
SummerActivities_Watermelon_Text[15806]["Text611"] = "Stop playing around, okay? I`m deadly busy. Find me when you have an Iced Watermelon with you!"
SummerActivities_Watermelon_Text[15806]["Text612"] = ""

--我要怎么帮你呢？
SummerActivities_Watermelon_Text[15806]["Text711"] = "It`s easy. Just buy a Big Watermelon from me, and keep it in the Cold Well for 1 minute. Then, bring it back to me."
SummerActivities_Watermelon_Text[15806]["Text712"] = "~It shouldn`t be less than 1 minute. I want a watermelon cold enough. But if you keep the watermelon for too long, it may be stolen."
SummerActivities_Watermelon_Text[15806]["Text713"] = ""
SummerActivities_Watermelon_Text[15806]["Text714"] = ""

SummerActivities_Watermelon_Text[15806]["Option9"] = "Show~me~the~way."
SummerActivities_Watermelon_Text[15806]["Option10"] = "I~see."


--冰凉的水井
SummerActivities_Watermelon_Text[15807] = {}

--活动时间内的对白
SummerActivities_Watermelon_Text[15807]["Text111"] = "This well with cold and clear water is a good place to ice watermelons."

SummerActivities_Watermelon_Text[15807]["Option1"] = "Put~in~watermelon."
SummerActivities_Watermelon_Text[15807]["Option2"] = "Take~out~the~watermelon."
SummerActivities_Watermelon_Text[15807]["Option3"] = "Leave."

--非活动时间
SummerActivities_Watermelon_Text[15807]["Text121"] = "The water in the well is really cold."

--放西瓜 - 没有西瓜
SummerActivities_Watermelon_Text[15807]["Text211"] = "You don`t have any watermelons with you. Go buy some from Vendor Wang."
SummerActivities_Watermelon_Text[15807]["Option4"] = "Okay."

--成功放入西瓜
SummerActivities_Watermelon_Text[15807]["Text311"] = "You`ve put a Big Watermelon in the Cold Well."
SummerActivities_Watermelon_Text[15807]["Option5"] = "Okay."

--取西瓜 - 不足10分钟
SummerActivities_Watermelon_Text[15807]["Text411"] = "It hasn`t been 1 minute since you put this watermelon here. It must not be cold enough."

--10分钟-20分钟
SummerActivities_Watermelon_Text[15807]["Text511"] = "It hasn`t been 20 minutes since you put this watermelon here. Are you sure you want to take it out?"
SummerActivities_Watermelon_Text[15807]["Option6"] = "Yes!"
SummerActivities_Watermelon_Text[15807]["Option7"] = "No."

--取出西瓜时 空间不足
SummerActivities_Watermelon_Text[15807]["Text611"] = "Your inventory is too full to contain more Iced Watermelons. Make some room, first."

--取出失败
SummerActivities_Watermelon_Text[15807]["Text711"] = "What a pity. This watermelon is not cold enough after nearly 20 minutes. It seems you need to put it in the well again."
SummerActivities_Watermelon_Text[15807]["Option8"] = "Alright."

--30分钟以上 取出失败
SummerActivities_Watermelon_Text[15807]["Text811"] = "You put your hands into the Well, but got nothing. Unluckily, the watermelon has been stolen."
SummerActivities_Watermelon_Text[15807]["Text812"] = "~You need to buy another one to ice."
SummerActivities_Watermelon_Text[15807]["Option9"] = "Oh,~no!"

--放西瓜的时候，已经有西瓜在井里
SummerActivities_Watermelon_Text[15807]["Text911"] = "Your watermelon is already in the Well."
SummerActivities_Watermelon_Text[15807]["Option10"] = "I~see."

--取西瓜的时候，没有放入过井中
SummerActivities_Watermelon_Text[15807]["Text1011"] = "Buddy, you haven`t put any watermelon in the Well."
SummerActivities_Watermelon_Text[15807]["Option11"] = "Oh,~sorry."


--非对白提示
SummerActivities_Watermelon_Text["BuyWatermelon"] = "You paid 100 Silver for a Big Watermelon. Hurry and put it in the Cold Well."
SummerActivities_Watermelon_Text["CompleteMsg"] = "You received a Festival Joy Pack from Vendor Wang!"
SummerActivities_Watermelon_Text["GetIcedWatermelon"] = "You received an Iced Watermelon."

--活动时间内的物品使用提示
SummerActivities_Watermelon_Text["ClickMelonInTime"] = "You ate Vendor Wang`s watermelon. It`s so delicious!"
SummerActivities_Watermelon_Text["ClickMelonAfterTime"] = "Bleck! This watermelon tasted very strange, and your threw it away."


------------------------------------------------------------------------------------
--Name:			[简体征服][活动脚本]2015暑期活动(7.9-7.22)
--Purpose:		
--Creator: 		张磊
--Created:		05/11/2015
------------------------------------------------------------------------------------


------------------------------------------NPC对话-------------------------------------------

SummerActivities_Prevent_Text = {}

SummerActivities_Prevent_Text[15810] = {}
--活动时间前
SummerActivities_Prevent_Text[15810]["Text111"] = "Many people are concerned about sunburn in summer. Look, I just made 3 kinds of sunscreen products."
SummerActivities_Prevent_Text[15810]["Text112"] = "~I need people above Level 80 or reborn to test their effects on July 9th to 22nd."
SummerActivities_Prevent_Text[15810]["Text113"] = "~Anyone who helps me complete the test will be rewarded with a super gift."
SummerActivities_Prevent_Text[15810]["Text114"] = ""

SummerActivities_Prevent_Text[15810]["Option1"] = "It`s~a~good~thing."

--活动时间内
SummerActivities_Prevent_Text[15810]["Text121"] = "Hello, everyone! I`m looking for Level 80+ or reborn heroes to test 3 sunscreen products for me from July 9th to 22nd."
SummerActivities_Prevent_Text[15810]["Text122"] = "~Interested? Try any of that, and come back to me after 30 minutes. If it didn`t work on you,"
SummerActivities_Prevent_Text[15810]["Text123"] = "~you can try another kind. As long as you find any sunscreen works, you can claim a super gift from me."
SummerActivities_Prevent_Text[15810]["Text124"] = "~Since sunscreen may cause skin damage, everyone can only test once in a day."
SummerActivities_Prevent_Text[15810]["Text125"] = ""

SummerActivities_Prevent_Text[15810]["Option2"] = "Choose~a~product~to~test."
SummerActivities_Prevent_Text[15810]["Option3"] = "Check~the~result."
SummerActivities_Prevent_Text[15810]["Option4"] = "I`ll~come~back~later."

--活动时间外
SummerActivities_Prevent_Text[15810]["Text131"] = "Thanks everyone for testing the sunscreen products! If I can bring them on stream, I`ll send you some free samples."
SummerActivities_Prevent_Text[15810]["Text132"] = ""

SummerActivities_Prevent_Text[15810]["Option5"] = "Yes,~you~can!"

--等级不够提示
SummerActivities_Prevent_Text[15810]["Text211"] = "Hey, you`re still too young. The sunscreen may hurt your skin before the sunray. Keep practicing, and come back when you reach Level 80."
SummerActivities_Prevent_Text[15810]["Text212"] = ""

SummerActivities_Prevent_Text[15810]["Option6"] = "Alright."

--当天已经测试过了提示

SummerActivities_Prevent_Text[15810]["Text311"] = "Well, you`ve taken one test today. Your skin needs a good rest."

SummerActivities_Prevent_Text[15810]["Option7"] = "Alright."

--点击选择防晒霜测试，进入下一层对白
SummerActivities_Prevent_Text[15810]["Text411"] = "There are 3 kinds of sunscreens: Herbal Sunscreen, Moisture Sunscreen, and Olive Sunscreen."
SummerActivities_Prevent_Text[15810]["Text412"] = "~I want to see which skin type they are suitable for, and their effects. So, which product"
SummerActivities_Prevent_Text[15810]["Text413"] = "~would you like? Try one of them, and come back to me after 30 minutes to check the result."

SummerActivities_Prevent_Text[15810]["Option8"] = "Herbal~Sunscreen."
SummerActivities_Prevent_Text[15810]["Option9"] = "Moisture~Sunscreen."
SummerActivities_Prevent_Text[15810]["Option10"] = "Olive~Sunscreen."
SummerActivities_Prevent_Text[15810]["Option11"] = "I~need~to~wash~face,~first."

--背包空间不足
SummerActivities_Prevent_Text[15810]["Text511"] = "Hey, your inventory is full. Please make at least 1 space for my gift, first."
SummerActivities_Prevent_Text[15810]["Option12"] = "I~see."

--没有测试过提示
SummerActivities_Prevent_Text[15810]["Text611"] = "Wait, you haven`t tried any of my sunscreens. How can you do the test?"
SummerActivities_Prevent_Text[15810]["Option13"] = "Sorry,~I~forgot."

--第一种防晒霜
--测试失败
SummerActivities_Prevent_Text[15810]["Text711"] = "Let me see... Your skin turned a little red, and had some red spots. This sunscreen isn`t suitable for you."
SummerActivities_Prevent_Text[15810]["Option14"] = "Okay.~I`ll~try~another~kind."

--第二种防晒霜
--测试失败
SummerActivities_Prevent_Text[15810]["Text811"] = "Um... This sunscreen carefully dealt with your sensitive skin, but it works inefficiently at blocking sunray."

--第三种防晒霜
--测试失败
SummerActivities_Prevent_Text[15810]["Text911"] = "Sigh... This sunscreen made your skin sticky, and caused a big pimple on your skin. It`s not good."
SummerActivities_Prevent_Text[15810]["Option15"] = "Okay.~I`ll~try~another~kind."

--使用防晒霜不足30分钟给提示
SummerActivities_Prevent_Text[15810]["Text1011"] = "You need to wait 30 minutes to see the sun-blocking effect. Be patient, okay?"
SummerActivities_Prevent_Text[15810]["Option16"] = "I~see."


--大于30分钟给提示
SummerActivities_Prevent_Text["Morethan30minutes11512"] = "It`s been 30 minutes since you tried the Herbal Sunscreen. Go find Lily White to check the result."
SummerActivities_Prevent_Text["Morethan30minutes11513"] = "It`s been 30 minutes since you tried the Moisture Sunscreen. Go find Lily White to check the result."
SummerActivities_Prevent_Text["Morethan30minutes11514"] = "It`s been 30 minutes since you tried the Olive Sunscreen. Go find Lily White to check the result."

--正在测试中（时间小于1天且小于30分钟）
SummerActivities_Prevent_Text["Lessthan30minutes11512"] = "Lily used the Herbal Sunscreen on you. Wait 30 minutes, and go find Lily to check the result."
SummerActivities_Prevent_Text["Lessthan30minutes11513"] = "Lily used the Moisture Sunscreen on you. Wait 30 minutes, and go find Lily to check the result."
SummerActivities_Prevent_Text["Lessthan30minutes11514"] = "Lily used the Olive Sunscreen on you. Wait 30 minutes, and go find Lily to check the result."

--完成测试的提示
SummerActivities_Prevent_Text["Successful"] = "This sunscreen is suitable for your skin. To thank your help, Lily presented you a summer pack."

------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]魔术大师(7.9-7.22)
--Purpose:		暑期活动--魔术大师(7.9-7.22)
--Creator:		王倩娜
--Created:		2015/07/05
------------------------------------------------------------------------------------


tSummerActive2015_MagicMaster_Text = {}
tSummerActive2015_MagicMaster_Text[15812] = {}

tSummerActive2015_MagicMaster_Text[15812]["Text111"] = "What`s the secret in this heavy Magic Box? The answer will be unveiled on July 9th."
tSummerActive2015_MagicMaster_Text[15812]["Option1"] = "I`m~waiting~to~see."

tSummerActive2015_MagicMaster_Text[15812]["Text121"] = "Are you coming for the Magic Box?"
tSummerActive2015_MagicMaster_Text[15812]["Option2"] = "Yeah!"

tSummerActive2015_MagicMaster_Text[15812]["Text131"] = "The Magic Box tightly closed after delivering many treasures to people."

tSummerActive2015_MagicMaster_Text[15812]["Text211"] = "You should reach Level 80 to acquire the ability to pry into the Magic Box."

tSummerActive2015_MagicMaster_Text[15812]["Text221"] = "You can see nothing in the Magic Box. It`s black. What`s the treasure inside?"

tSummerActive2015_MagicMaster_Text[15812]["Text311"] = {}
tSummerActive2015_MagicMaster_Text[15812]["Text311"][1] = "A weak light flashed, and you saw a square item."
tSummerActive2015_MagicMaster_Text[15812]["Text311"][2] = "A weak light flashed, and you saw a round item."
tSummerActive2015_MagicMaster_Text[15812]["Text311"][3] = "A weak light flashed, and you saw something like a bag."

tSummerActive2015_MagicMaster_Text[15812]["Text411"] = "Finally, you saw it clearly. It`s a(n) [%s]. Go find Magician Ngau, and ask him to conjure it up."

tSummerActive2015_MagicMaster_Text["PeepContent"] = "Prying"

tSummerActive2015_MagicMaster_Text["GetExp"] = "You pried into the Magic Box, and received 60 minutes of EXP."

tSummerActive2015_MagicMaster_Text["OnlyTime"] = "You can only pry into the Magic Box once in a day."

tSummerActive2015_MagicMaster_Text[15811] = {}

tSummerActive2015_MagicMaster_Text[15811]["Text111"] = "You must know who I am. Yeah, I`m magician Ngau. See what I bring to Twin City this time?"
tSummerActive2015_MagicMaster_Text[15811]["Text112"] = "~It`s my favorite magic box. What`s inside? Ah, it`s a secret."
tSummerActive2015_MagicMaster_Text[15811]["Text113"] = "~Follow your curiosity, and find me on July 9th - 22nd."
tSummerActive2015_MagicMaster_Text[15811]["Option1"] = "Sounds~interesting."

tSummerActive2015_MagicMaster_Text[15811]["Text121"]  = "See, this is my magic box. Of course, you have no idea about what`s inside."
tSummerActive2015_MagicMaster_Text[15811]["Text122"]  = "~When people pry into the box, they will see different items. You don`t need to tell me what you see. I know it, and I`ll conjure it up."
tSummerActive2015_MagicMaster_Text[15811]["Text123"]  = "~It`s the magic."
tSummerActive2015_MagicMaster_Text[15811]["Option2"] = "Conjure~my~treasure~up."
tSummerActive2015_MagicMaster_Text[15811]["Option3"] = "Tell~me~more."
tSummerActive2015_MagicMaster_Text[15811]["Option4"] = "Just~tricks."

tSummerActive2015_MagicMaster_Text[15811]["Text131"] = "It`s impossible to refuse people in Twin City. You`re so enthusiastic. Time to say goodbye!"
tSummerActive2015_MagicMaster_Text[15811]["Option5"] = "Bye!"

tSummerActive2015_MagicMaster_Text[15811]["Text211"] = "Sorry, this game is only for Level 80+ heroes. It should be easy for you to reach, right?"
tSummerActive2015_MagicMaster_Text[15811]["Option6"] = "Okay."

tSummerActive2015_MagicMaster_Text[15811]["Text221"] = "Um... I caught the picture in your mind. It`s all black. Ah, you didn`t see the item in the box clearly. If so, I have no way to conjure it up."
tSummerActive2015_MagicMaster_Text[15811]["Text222"] = "~Try again tomorrow. The more times you try, the clearer you can see."
tSummerActive2015_MagicMaster_Text[15811]["Option7"] = "Alright."

tSummerActive2015_MagicMaster_Text[15811]["Text231"] = "Your inventory is almost full. Make some room, first."

tSummerActive2015_MagicMaster_Text[15811]["Text311"] = "Well, it`s the time to witness the miracle. 1, 2, 3, go! Ah, it`s a(n) %s that you saw in the box, right?"
tSummerActive2015_MagicMaster_Text[15811]["Option8"] = "Wow,~it`s~amazing!"

tSummerActive2015_MagicMaster_Text[15811]["Text411"] = "My Magic Box is quite arrogant. Only heroes above Level 80 can pry into it, once in a day."
tSummerActive2015_MagicMaster_Text[15811]["Text412"] = "~The more times you try, the clearer you can see inside. You need to pry into the box 3 days in a row"
tSummerActive2015_MagicMaster_Text[15811]["Text413"] = "~to clearly see what`s the treasure inside. Then, you can ask me to conjure the treasure up."
tSummerActive2015_MagicMaster_Text[15811]["Text414"] = "~Remember, if you forget to do that one day, you need to start it over."
tSummerActive2015_MagicMaster_Text[15811]["Option9"] = "I~see."

tSummerActive2015_MagicMaster_Text["GetReward"] = "Magician Ngau was murmuring, and stretched his hands to the box. The hands directly went through the cover, and took out a(n) %s!"



--物品名称
tSummerActive2015_MagicMaster_Text["RewardName"] = {}
tSummerActive2015_MagicMaster_Text["RewardName"][721022] = "MoonBox"
tSummerActive2015_MagicMaster_Text["RewardName"][721032] = "MoonBox"
tSummerActive2015_MagicMaster_Text["RewardName"][721042] = "MoonBox"
tSummerActive2015_MagicMaster_Text["RewardName"][721052] = "MoonBox"
tSummerActive2015_MagicMaster_Text["RewardName"][721063] = "MoonBox"
tSummerActive2015_MagicMaster_Text["RewardName"][720894] = "EnduranceBookPack(5)"
tSummerActive2015_MagicMaster_Text["RewardName"][3000138] = "EliteMeteorScrollPack"
tSummerActive2015_MagicMaster_Text["RewardName"][3000121] = "StandardLotteryPack"
tSummerActive2015_MagicMaster_Text["RewardName"][3000122] = "BigLotteryPack"
tSummerActive2015_MagicMaster_Text["RewardName"][3000123] = "LuxuryLotteryPack"
                                
tSummerActive2015_MagicMaster_Text["RewardName"][721259] = "CelestialStone"
tSummerActive2015_MagicMaster_Text["RewardName"][710214] = "QuestChanceB"
tSummerActive2015_MagicMaster_Text["RewardName"][700102] = "RefinedThunderGem"
tSummerActive2015_MagicMaster_Text["RewardName"][700122] = "RefinedGloryGem"
tSummerActive2015_MagicMaster_Text["RewardName"][1088000] = "DragonBall"
tSummerActive2015_MagicMaster_Text["RewardName"][729242] = "VitalPill"
                                
tSummerActive2015_MagicMaster_Text["RewardName"][721316] = "EXPBallPack"
tSummerActive2015_MagicMaster_Text["RewardName"][720886] = "BigSkillProficiencyPack"
tSummerActive2015_MagicMaster_Text["RewardName"][727061] = "ThunderGemPack"
tSummerActive2015_MagicMaster_Text["RewardName"][727060] = "GloryGemPack"
tSummerActive2015_MagicMaster_Text["RewardName"][720881] = "HorseRacingPointsPack(2K)"
tSummerActive2015_MagicMaster_Text["RewardName"][723712] = "+1StonePack"
------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]2015暑期活动之畅销冷饮(7.9-7.22)
--Creator: 		张世超
--Created:		2015/05/06
------------------------------------------------------------------------------------
tSummerActive2015_IceCream_Text = {}
tSummerActive2015_IceCream_Text[15814] = {}
tSummerActive2015_IceCream_Text[15815] = {}
tSummerActive2015_IceCream_Text[15816] = {}
tSummerActive2015_IceCream_Text[15817] = {}
tSummerActive2015_IceCream_Text[15818] = {}
tSummerActive2015_IceCream_Text[15819] = {}
tSummerActive2015_IceCream_Text[15820] = {}
tSummerActive2015_IceCream_Text[15821] = {}
tSummerActive2015_IceCream_Text[15822] = {}
tSummerActive2015_IceCream_Text[15823] = {}	
tSummerActive2015_IceCream_Text[15824] = {}
tSummerActive2015_IceCream_Text[15825] = {}
tSummerActive2015_IceCream_Text[15826] = {}
tSummerActive2015_IceCream_Text[15827] = {}
tSummerActive2015_IceCream_Text[15828] = {}	
tSummerActive2015_IceCream_Text[15829] = {}	
tSummerActive2015_IceCream_Text[15813] = {}	

-- tSummerActive2015_IceCream_Text[15814] = {}
tSummerActive2015_IceCream_Text[15814]["Text111"] = "This bellflower is red. Its color will change in a while."
tSummerActive2015_IceCream_Text[15814]["Option1"] = "Oh,~it`s~nice!"

tSummerActive2015_IceCream_Text[15818]["Text111"] = "This bellflower is yellow. Its color will change in a while."
tSummerActive2015_IceCream_Text[15818]["Option1"] = "Oh,~it`s~nice!"

tSummerActive2015_IceCream_Text[15822]["Text111"] = "This bellflower is green. Its color will change in a while."
tSummerActive2015_IceCream_Text[15822]["Option1"] = "Oh,~it`s~nice!"

tSummerActive2015_IceCream_Text[15826]["Text111"] = "This Blue Bellflower scents the air, and makes you happy."
tSummerActive2015_IceCream_Text[15826]["Text112"] = "~Its color will change in a while. Watch your time to pick."
tSummerActive2015_IceCream_Text[15826]["Option1"] = "Pick."
tSummerActive2015_IceCream_Text[15826]["Option2"] = "What~a~beautiful~flower!"

tSummerActive2015_IceCream_Text[15826]["Text121"] = "This bellflower is blue. Its color will change in a while."
tSummerActive2015_IceCream_Text[15826]["Option10"] = "Oh,~it`s~nice!"


tSummerActive2015_IceCream_Text[15826]["MsgBox1"]  = "Buddy, the event has ended."
tSummerActive2015_IceCream_Text[15826]["MsgBox2"]  = "You`ve collected 1 Blue Bellflower`s Petal. Give it to Annie."
tSummerActive2015_IceCream_Text[15826]["MsgBox3"]  = "Your inventory is too full to contain the Blue Bellflower`s Petal. Make some room, first."
tSummerActive2015_IceCream_Text[15826]["MsgBox4"]  = "You need to be closer to the flower to pick it."
tSummerActive2015_IceCream_Text[15826]["MsgBox5"]  = "This bellflower has been picked. It takes time to grow a new flower bud."
tSummerActive2015_IceCream_Text[15826]["MsgBox6"]  = "You received a Blue Bellflower`s Petal."
tSummerActive2015_IceCream_Text[15826]["MsgBox7"]  = "You received 2 Blue Bellflower`s Petals."

tSummerActive2015_IceCream_Text[15827]["MsgBox1"]  = "Buddy, the event has ended."
tSummerActive2015_IceCream_Text[15827]["MsgBox2"]  = "You`ve collected 1 Blue Bellflower`s Petal. Give it to Annie."
tSummerActive2015_IceCream_Text[15827]["MsgBox3"]  = "Your inventory is too full to contain the Blue Bellflower`s Petal. Make some room, first."
tSummerActive2015_IceCream_Text[15827]["MsgBox4"]  = "You need to be closer to the flower to pick it."
tSummerActive2015_IceCream_Text[15827]["MsgBox5"]  = "This bellflower has been picked. It takes time to grow a new flower bud."
tSummerActive2015_IceCream_Text[15827]["MsgBox6"]  = "You received a Blue Bellflower`s Petal."
tSummerActive2015_IceCream_Text[15827]["MsgBox7"]  = "You received 2 Blue Bellflower`s Petals."

tSummerActive2015_IceCream_Text[15828]["MsgBox1"]  = "Buddy, the event has ended."
tSummerActive2015_IceCream_Text[15828]["MsgBox2"]  = "You`ve collected 1 Blue Bellflower`s Petal. Give it to Annie."
tSummerActive2015_IceCream_Text[15828]["MsgBox3"]  = "Your inventory is too full to contain the Blue Bellflower`s Petal. Make some room, first."
tSummerActive2015_IceCream_Text[15828]["MsgBox4"]  = "You need to be closer to the flower to pick it."
tSummerActive2015_IceCream_Text[15828]["MsgBox5"]  = "This bellflower has been picked. It takes time to grow a new flower bud."
tSummerActive2015_IceCream_Text[15828]["MsgBox6"]  = "You received a Blue Bellflower`s Petal."
tSummerActive2015_IceCream_Text[15828]["MsgBox7"]  = "You received 2 Blue Bellflower`s Petals."

tSummerActive2015_IceCream_Text[15829]["MsgBox1"]  = "Buddy, the event has ended."
tSummerActive2015_IceCream_Text[15829]["MsgBox2"]  = "You`ve collected 1 Blue Bellflower`s Petal. Give it to Annie."
tSummerActive2015_IceCream_Text[15829]["MsgBox3"]  = "Your inventory is too full to contain the Blue Bellflower`s Petal. Make some room, first."
tSummerActive2015_IceCream_Text[15829]["MsgBox4"]  = "You need to be closer to the flower to pick it."
tSummerActive2015_IceCream_Text[15829]["MsgBox5"]  = "This bellflower has been picked. It takes time to grow a new flower bud."
tSummerActive2015_IceCream_Text[15829]["MsgBox6"]  = "You received a Blue Bellflower`s Petal."
tSummerActive2015_IceCream_Text[15829]["MsgBox7"]  = "You received 2 Blue Bellflower`s Petals."

--冰淇淋女孩安妮 活动前对白
tSummerActive2015_IceCream_Text[15813]["Text111"] = "What`s the best treat this summer? Blue bellflower ice cream! It`s brought many people a chilly taste of summer."
tSummerActive2015_IceCream_Text[15813]["Text112"] = "~However, I`m running out of ingredients. Could you collect some Blue Bellflower`s Petals for me from July 9th to 22nd?"
tSummerActive2015_IceCream_Text[15813]["Text113"] = "~I`ll pay for that."
tSummerActive2015_IceCream_Text[15813]["Text114"] = ""
tSummerActive2015_IceCream_Text[15813]["Option1"] = "No~problem."

--冰淇淋女孩安妮 活动后对白
tSummerActive2015_IceCream_Text[15813]["Text121"] = "People in Twin City are very warm-hearted! Without your help, I can`t run my business so well. Thanks!"
tSummerActive2015_IceCream_Text[15813]["Text122"] = ""
tSummerActive2015_IceCream_Text[15813]["Option2"] = "You`re~welcome."

--冰淇淋女孩安妮 活动对白
tSummerActive2015_IceCream_Text[15813]["Text131"] = "Um... I`m running out of ingredients for making ice cream. Could you help?"
tSummerActive2015_IceCream_Text[15813]["Text132"] = "~If you`re above Level 80 or reborn, collect some Blue Bellflower`s Petals for me from July 9th to 22nd."
tSummerActive2015_IceCream_Text[15813]["Text133"] = "~I`ll pay a nice reward for that."
tSummerActive2015_IceCream_Text[15813]["Option3"] = "Let~me~help~you."
tSummerActive2015_IceCream_Text[15813]["Option4"] = "I~brought~some~petals."
tSummerActive2015_IceCream_Text[15813]["Option5"] = "Tell~me~more."
tSummerActive2015_IceCream_Text[15813]["Option6"] = "I`m~busy."

--了解活动详情。
tSummerActive2015_IceCream_Text[15813]["Text211"] = "Blue bellflower ice cream is made of Blue Bellflower`s Petal."
tSummerActive2015_IceCream_Text[15813]["Text212"] = "~Bellflowers grow flowers of 4 colors: red, yellow, green, and blue. It can change color constantly. Only when it turns to blue, collect the petals."
tSummerActive2015_IceCream_Text[15813]["Text213"] = "~I know it`s inhuman to work in such a hot day. Just help me once in a day, okay?"
tSummerActive2015_IceCream_Text[15813]["Text214"] = ""
tSummerActive2015_IceCream_Text[15813]["Option7"] = "I~see."

--等级判定	
tSummerActive2015_IceCream_Text[15813]["Text311"] = "Sorry, you`re still too young to pick bellflowers in the wild. "
tSummerActive2015_IceCream_Text[15813]["Text312"] = "~Why not come see me when you reach Level 80 or get reborn?"
tSummerActive2015_IceCream_Text[15813]["Option10"] = "Alright."

--接任务	
tSummerActive2015_IceCream_Text[15813]["Text321"] = "You can probably find Blue Bellflowers outside Twin City. If you get some, collect 1 petal for me."
tSummerActive2015_IceCream_Text[15813]["Text322"] = ""
tSummerActive2015_IceCream_Text[15813]["Option11"] = "I`m~on~the~way."

--已接任务未上交
tSummerActive2015_IceCream_Text[15813]["Text331"] = "Thanks! Please collect 1 Blue Bellflower`s Petal for me."
tSummerActive2015_IceCream_Text[15813]["Option12"] = "Okay."

--当天已完成
tSummerActive2015_IceCream_Text[15813]["Text341"] = "You`ve already helped me once today. It`s really hot, and you need a good rest."
tSummerActive2015_IceCream_Text[15813]["Option13"] = "You`re~right."

--数量不足
tSummerActive2015_IceCream_Text[15813]["Text351"] = "I need many petals to make ice cream. Could you collect 1 Blue Bellflower`s Petal, first?"
tSummerActive2015_IceCream_Text[15813]["Option14"] = "Okay."

tSummerActive2015_IceCream_Text[15813]["MsgBox1"]  = "You received a Festival Joy Pack!"
tSummerActive2015_IceCream_Text[15813]["MsgBox2"]  = "Hurry and collect 1 Blue Bellflower`s Petal for Annie."

------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]暑期活动-无畏勇者
--Creator: 		魏贻逵
--Created:		2015/5/11
------------------------------------------------------------------------------------

--相关提示
tSummeractivities_brave_Text = {}
tSummeractivities_brave_Text["NoItem"] = "You don`t have any Bravery Tokens with you, so you can`t get any rewards."
tSummeractivities_brave_Text["Limitlevel"] = "As you have reached the max level, you can`t receive the EXP."
tSummeractivities_brave_Text["AddExpTime"] = "You inserted 1 Bravery Token, and received 12 minutes of EXP!"
tSummeractivities_brave_Text["AddCultivation"] = "You inserted 1 Bravery Token, and received 10 Study Points!"
tSummeractivities_brave_Text["Addstrengthvalue"] = "You inserted 1 Bravery Token, and received 5 Chi Points."
tSummeractivities_brave_Text["Far"] = "You`re too far away from me. Get closer, and take your Bravery Token."
tSummeractivities_brave_Text["Complete"] = "It`s not the time to claim the Bravery Token. You can claim one token every 3 minutes."
tSummeractivities_brave_Text["Space"] = "Your inventory is almost full. Make some room, first."
tSummeractivities_brave_Text["Reward"] = "You won a Bravery Token!"
tSummeractivities_brave_Text["Lead"] = "All the Bravery Tokens have been claimed."
tSummeractivities_brave_Text["Lastpoint"] = "Fight and kill! The rest tokens will be rewarded to the only and last one who survives in the Blood Arena."
-- tSummeractivities_brave_Text["Reward1"] = "恭喜你获得了暑假欢笑大礼包！"
tSummeractivities_brave_Text["Win"] = "You`re the last survivor in the Blood Arena, and won all the rest Bravery Tokens!"
tSummeractivities_brave_Text["Broadcast"] = "The Blood Arena has opened. Talk to Champion Ling Wan to enter the field."

tSummeractivities_brave_Text["Text1"] = "The Blood Arena has opened. Talk to Champion Ling Wan to enter the field."
tSummeractivities_brave_Text["Option1"] = "Okay."
tSummeractivities_brave_Text["Option2"] = "I`m~not~ready,~yet!"

tSummeractivities_brave_Text[10296] = {}
tSummeractivities_brave_Text[10296]["Text111"] ="A person with the will, the faith, and the boldness to fight can break everything in his way. Are you a real fighter?"
tSummeractivities_brave_Text[10296]["Text112"] = "~Fight in the Blood Arena, and show off your power! On July 9th - 22nd, I await you!"
tSummeractivities_brave_Text[10296]["Option1"] = "I`ll~be~there!"

tSummeractivities_brave_Text[10296]["Text121"] ="Your courage is adorable."
tSummeractivities_brave_Text[10296]["Option2"] = "Thanks!"

tSummeractivities_brave_Text[10296]["Text131"] ="Pay attention! All Level 80+ heroes are invited to the Blood Arena at 18:00 - 18:15, from July 9th to 22nd."
tSummeractivities_brave_Text[10296]["Text132"] ="~The only rule is that there are no rules. The longer you fight, the more you win! Every 3 minutes, you can claim a Bravery Token from Ling Chiu."
tSummeractivities_brave_Text[10296]["Option3"] = "I`m~in!"
tSummeractivities_brave_Text[10296]["Option4"] = "Tell~me~more."
tSummeractivities_brave_Text[10296]["Option5"] = "Sorry,~I~hate~violence."

tSummeractivities_brave_Text[10296]["Text411"] = "Bravery Token is a good thing. Insert it into the censer behind me, and you`ll get a reward. Choose one from 12 minutes of EXP, 10 Study Points, and 5 Chi Points."
tSummeractivities_brave_Text[10296]["Text412"] = "~If you earn 5 tokens or more, I`ll present you an extra Festival Joy Pack. Only once in a day."
tSummeractivities_brave_Text[10296]["Option6"] = "Anything~else?"
tSummeractivities_brave_Text[10296]["Option7"] = "I`m~in!"
tSummeractivities_brave_Text[10296]["Option8"] = "I`m~not~interested."

tSummeractivities_brave_Text[10296]["Text611"] = "People are free to enter or leave the Blood Arena at 18:00 - 18:15, everyday. At 19:00, all fighters there will be sent to Twin City. Ling Chiu has prepared 1,000 Bravery Tokens."
tSummeractivities_brave_Text[10296]["Text612"] = "~If you`re the only and last one who survives in the end, you`ll be rewarded with 30 tokens. Good luck!"
tSummeractivities_brave_Text[10296]["Option9"] = "I~want~to~fight!"
tSummeractivities_brave_Text[10296]["Option10"] = "It`s~not~my~game."

tSummeractivities_brave_Text[10296]["Text211"] = "The Blood Arena is available from 18:00 to 18:15. Be patient."
tSummeractivities_brave_Text[10296]["Option11"] = "Alright."

tSummeractivities_brave_Text[10296]["Text221"] = "This challenge is for heroes at Level 80 or above. Keep practicing."
tSummeractivities_brave_Text[10296]["Option12"] = "Alright."

tSummeractivities_brave_Text[10297] = {}
tSummeractivities_brave_Text[10297]["Text111"] = "Are you sure you want to exchange 1 Bravery Token for 12 minutes of EXP?"

tSummeractivities_brave_Text[10298] = {}
tSummeractivities_brave_Text[10298]["Text111"] = "Are you sure you want to exchange 1 Bravery Token for 10 Study Points?"

tSummeractivities_brave_Text[10299] = {}
tSummeractivities_brave_Text[10299]["Text111"] = "Are you sure you want to exchange 1 Bravery Token for 5 Chi Points?"


------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]暑期活动-无影盗贼团
--Creator: 		魏贻逵
--Created:		2015/5/11
------------------------------------------------------------------------------------

--相关提示
tSummeractivities_thief_Text = {}
-- tSummeractivities_thief_Text["Reward"] = "恭喜你获得暑假欢笑大礼包!"
tSummeractivities_thief_Text["Drop"] = "You touched the Strange Shadow, and some Silver fell from the sky. Is anyone screaming?"
tSummeractivities_thief_Text["Throw"] = "Sprinkle"
tSummeractivities_thief_Text["Complete"] = "You successfully revealed 5 thieves. Report back to Commander Chin!"
tSummeractivities_thief_Text["Grow"] = "You used the Flour at the Strange Shadow, and revealed a Shadow Thief."
tSummeractivities_thief_Text["Grow1"] = "You used the Flour at the Strange Shadow, and found he is the Shadows` Leader."
tSummeractivities_thief_Text["Full"] = "Your inventory is too full to contain more things. Please make some room, first."
tSummeractivities_thief_Text["Drop1"] = "The Shadows` Leader was defeated, and dropped many Festival Joy Packs!"
tSummeractivities_thief_Text["Item1"] = "This handkerchief belongs the first beauty of Twin City. Its fragrance attracted a swam of butterflies around you."
tSummeractivities_thief_Text["Item2"] = "Wang Yee`s rotten socks made you dizzy for 5 seconds."
tSummeractivities_thief_Text["Item3"] = "You lost direction for 10 seconds by the strong smell of the pants."
tSummeractivities_thief_Text["Monster"] = "ShadowThief"

tSummeractivities_thief_Text["Text1"] = "There is someone?"
tSummeractivities_thief_Text["Option1"] = "Oh,~bad!"
tSummeractivities_thief_Text["Text2"] = "Run!"
tSummeractivities_thief_Text["Option2"] = "Oh,~bad!"
tSummeractivities_thief_Text["Text3"] = "You can never catch me."
tSummeractivities_thief_Text["Option3"] = "No~way~to~escape!"
tSummeractivities_thief_Text["Option4"] = "Head~to~see~Commander~Chin."

tSummeractivities_thief_Text[10301] = {}
tSummeractivities_thief_Text[10301]["Text111"] ="It`s really annoying. Theft cases happened one after another in Twin City. Luckily, I got some clues."
tSummeractivities_thief_Text[10301]["Text112"] = "~Hey, you look able. Would you like to track some thieves for me from July 9th to 22th?"
tSummeractivities_thief_Text[10301]["Option1"] = "It`s~my~pleasure."

tSummeractivities_thief_Text[10301]["Text121"] ="Finally, I have all the Shadow thieves in my prison. People got their lost items back. Thanks for your help!"
tSummeractivities_thief_Text[10301]["Option2"] = "You`re~welcome."

tSummeractivities_thief_Text[10301]["Text131"] ="Listen, those thieves with invisible skill are from a theft group, Shadows. We can use Flour to reveal them from the dark."
tSummeractivities_thief_Text[10301]["Text132"] ="~If you`ve reached Level 80, give me a hand! Go reveal 5 Shadow thieves in Twin City, Phoenix Castle, Ape City, Desert City, or Bird Island. You won`t do that for free."
tSummeractivities_thief_Text[10301]["Option3"] = "Claim~some~Flour."
tSummeractivities_thief_Text[10301]["Option4"] = "Claim~my~reward."
tSummeractivities_thief_Text[10301]["Option5"] = "Return~the~lost~item."
tSummeractivities_thief_Text[10301]["Option6"] = "Tell~me~more."

tSummeractivities_thief_Text[10301]["Text211"] = "If you find a Strange Shadow, use the Flour first to reveal him from the dark, or he`ll escape. Then, kick him as you wish."
tSummeractivities_thief_Text[10301]["Text212"] = "~By the way, if you lost the Flour, you can claim another bag from me."
tSummeractivities_thief_Text[10301]["Option7"] = "Anything~else?"
tSummeractivities_thief_Text[10301]["Option8"] = "I~see."

tSummeractivities_thief_Text[10301]["Text311"] = "Knock thieves down, and you may find items stolen from Twin City. While returning items, you may get a surprise. If something makes you uncomfortable, I`m sorry."
tSummeractivities_thief_Text[10301]["Text312"] = "~The Shadows` Leader is known to carry tons of treasures. Don`t miss the chance!"
tSummeractivities_thief_Text[10301]["Option9"] = "Got~it!"

tSummeractivities_thief_Text[10301]["Text411"] = "Thanks for coming, but those Shadow thieves are too dangerous for you. Keep practicing, and come back when you reach Level 80."
tSummeractivities_thief_Text[10301]["Option10"] = "Alright."

tSummeractivities_thief_Text[10301]["Text421"] = "You have already had a bag of Flour in your inventory."
tSummeractivities_thief_Text[10301]["Option11"] = "Okay."

tSummeractivities_thief_Text[10301]["Text431"] = "Your inventory is full. You need to make some room, first."
tSummeractivities_thief_Text[10301]["Option12"] = "I`ll~do~it~now."

tSummeractivities_thief_Text[10301]["Text441"] = "This bag of Flour was specially made to break Shadow thieves` invisibility. Once you find some Strange Shadows, sprinkle the Flour at them."
tSummeractivities_thief_Text[10301]["Option13"] = "I~see."

tSummeractivities_thief_Text[10301]["Text511"] = "You`ve claimed your reward today, haven`t you?"
tSummeractivities_thief_Text[10301]["Option14"] = "Sorry,~I~forgot."

tSummeractivities_thief_Text[10301]["Text521"] = "=Come on, you haven`t found 5 Shadow thieves. Complete your job, first!"
tSummeractivities_thief_Text[10301]["Option15"] = "Okay."

tSummeractivities_thief_Text[10301]["Text611"] = "Well done! The owner will be very happy. I`ll reward you with 10 minutes of EXP!"
tSummeractivities_thief_Text[10301]["Option16"] = "Thanks!"

tSummeractivities_thief_Text[10301]["Text621"] = "Well done! The owner will be very happy. I`ll reward you with 5 Study Points!"
tSummeractivities_thief_Text[10301]["Option17"] = "Thanks!"

tSummeractivities_thief_Text[10301]["Text631"] = "You know what, you don`t have any lost items with you."
tSummeractivities_thief_Text[10301]["Option18"] = "Sorry,~my~mistake."

------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]暑期活动种瓜得瓜和总NPC
--Purpose:	暑期活动种瓜得瓜和总NPC
--Creator: 	郑鋆
--Created:	2015/05/04
------------------------------------------------------------------------------------

-- 中文索引表
tSummerActive2015_ZhongGuaDeGua_Text = {}
-- 西瓜太郎
tSummerActive2015_ZhongGuaDeGua_Text[18510] = {}
-- 活动前
tSummerActive2015_ZhongGuaDeGua_Text[18510]["111"] = "I may look young, but I`m a good hand at growing watermelons! Are you interested? Come and find me on July 9th - 22nd!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["112"] = "~You`ll harvest endless fun, and also delicate gifts!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["113"] = ""
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option1"] = "I`ll~be~there!"

-- 活动中	00:00 22:59
tSummerActive2015_ZhongGuaDeGua_Text[18510]["121"] = "Awful heat! I know people love my watermelons. But, but can I have a rest? I`m almost dry."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["122"] = "~Look, I need hands above Level 80 in growing watermelons outside Twin City from July 9th to 22nd."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["123"] = "~If you`re interested, you can claim Watermelon Seeds from me. Come on, I promise nice rewards to helpers!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option2"] = "Give~me~the~seeds."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option3"] = "Submit~watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option4"] = "Claim~my~reward."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option5"] = "View~my~points."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option6"] = "Tell~me~more."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option7"] = "I`m~busy."

-- 活动中	23点
tSummerActive2015_ZhongGuaDeGua_Text[18510]["131"] = "Just earn 20 points during 00:00 - 22:59, and you can swap for a gift pack with me. Only once in a day."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["132"] = "~If you earn the most points on the day, you can also claim a super reward from me during 00:00 - 23:00 on the next day."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["133"] = "~Here`s a secret: you may encounter the Earth God in watermelon field. If so, you`re very lucky to win a mysterious gift."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option8"] = "Give~me~the~seeds."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option9"] = "View~my~points."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option10"] = "Tell~me~more."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option11"] = "I`m~busy."

-- 活动后
tSummerActive2015_ZhongGuaDeGua_Text[18510]["141"] = "Wow, I received a huge mountain of watermelons! Growing watermelon is really funny, isn`t it?"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["142"] = "~Hey, I have a gift for the one who earned the most points on July 22nd."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["143"] = "~If you did, remember to claim the super reward from me before 23:00, July 23rd."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option12"] = "Claim~my~super~reward."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option13"] = "It`s~a~nice~experience."

-- 已经结束
tSummerActive2015_ZhongGuaDeGua_Text[18510]["211"] = "You`ve missed the time to claim the last super reward."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option14"] = "Oh,~no..."

-- 不是第一名玩家
tSummerActive2015_ZhongGuaDeGua_Text[18510]["221"] = "Sorry, you`re not the one who earned the most points yesterday. So, you can`t win the super reward."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option15"] = "Alright."

-- 领取过了
tSummerActive2015_ZhongGuaDeGua_Text[18510]["231"] = "You`ve claimed your super reward today, haven`t you?"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option16"] = "Sorry,~I~forgot."

-- 空间满
tSummerActive2015_ZhongGuaDeGua_Text[18510]["241"] = "You got a full bag. Why not make some room, first?"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option17"] = "I`ll~do~it~now."

-- 获得对白
tSummerActive2015_ZhongGuaDeGua_Text[18510]["251"] = "Wow, you`re amazing! How could you grow so many watermelons in such a short time? You deserve my super reward!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option18"] = "Thanks!"

-- 等级不足
tSummerActive2015_ZhongGuaDeGua_Text[18510]["311"] = "Hey, you haven`t reached Level 80. Keep practicing, and come back when you reach the goal."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option19"] = "I~see."

-- cd
tSummerActive2015_ZhongGuaDeGua_Text[18510]["321"] = "Um... It hasn`t been 1 hour since you claimed seeds from me. "
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option20"] = "Alright."

-- 给西瓜种子
tSummerActive2015_ZhongGuaDeGua_Text[18510]["331"] = "Look, here are 10 Watermelon Seeds for you. Go and try!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option21"] = "Okay."

-- 不在规定时间
tSummerActive2015_ZhongGuaDeGua_Text[18510]["411"] = "Thanks for coming, but I don`t take your watermelon after 23:00. Be earlier, next time."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option22"] = "Alright."

tSummerActive2015_ZhongGuaDeGua_Text[18510]["421"] = "Well, which kind of watermelon are you going to swap for points?"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option23"] = "1~Green~Watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option24"] = "1~Emerald~Watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option25"] = "1~Jade~Watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option26"] = "Let~me~see."

-- 没有西瓜
tSummerActive2015_ZhongGuaDeGua_Text[18510]["431"] = "Where is the watermelon you were talking about? You don`t have any of that!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option27"] = "Sorry,~my~mistake."

tSummerActive2015_ZhongGuaDeGua_Text[18510]["441"] = "Um... This watermelon looks strange. Perhaps, you did something wrong while growing it. Anyway, thank you. I`ll give you 1 point."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option28"] = "Swap~more~watermelons."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option29"] = "Thanks!"

tSummerActive2015_ZhongGuaDeGua_Text[18510]["451"] = "This watermelon looks a little small, but tastes good. Good job! You earned 3 points!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option30"] = "Swap~more~watermelons."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option31"] = "Thanks!"

tSummerActive2015_ZhongGuaDeGua_Text[18510]["461"] = "Ah, what a nice watermelon! It must be awfully sweet. Excellent! You earned 5 points!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option32"] = "Swap~more~watermelons."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option33"] = "Thanks!"

tSummerActive2015_ZhongGuaDeGua_Text[18510]["511"] = "Watch the time. You can`t claim rewards after 23:00. Be earlier, next time."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option34"] = "I~see."

tSummerActive2015_ZhongGuaDeGua_Text[18510]["521"] = "Which kind of reward would you like to claim?"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option35"] = "Claim~a~reward~for~20~points."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option36"] = "Super~reward."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option37"] = "Let~me~see..."

-- 今天已经领过奖了
tSummerActive2015_ZhongGuaDeGua_Text[18510]["531"] = "Look, you`ve claimed your reward today. Only once in a day, okay?"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option38"] = "Okay."

-- 分数不足
tSummerActive2015_ZhongGuaDeGua_Text[18510]["541"] = "Hey, you haven`t earned 20 points. I have no rewards for you."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option39"] = "Alright."

tSummerActive2015_ZhongGuaDeGua_Text[18510]["551"] = "Good job! Thanks for growing watermelons for me. Here`s a reward for you."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option40"] = "Thanks!"

tSummerActive2015_ZhongGuaDeGua_Text[18510]["561"] = "Come on, you haven`t given me any watermelons. Do your job first, and I`ll give the points."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option41"] = "I~see."

-- 第一
tSummerActive2015_ZhongGuaDeGua_Text[18510]["571"] = "Wow, you`re doing a great job! You`ve earned %d points, ranking the 1st place! Hold your seat, and I`ll surprise you tomorrow."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option42"] = "Great!"

-- 不是第一
tSummerActive2015_ZhongGuaDeGua_Text[18510]["581"] = "You`ve earned %d point(s). Though you didn`t rank the 1st place today, you did a nice job. Go for the first place, and you`ll earn an extra reward."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option43"] = "Okay."

-- 我要了解更多。
tSummerActive2015_ZhongGuaDeGua_Text[18510]["611"] = "If you`ve reached Level 80, you can claim Watermelon Seeds from me."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["612"] = "~These seeds may grow 3 kinds of watermelons. I`ll give you points for those watermelons:"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["613"] = "~5 points for a Jade Watermelon, 3 points for a Emerald Watermelon, and 1 point for a Green Watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["614"] = "~By the way, you can hunt monsters that match your level to collect seeds."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option44"] = "What~to~do~with~the~points?"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option45"] = "I~have~other~things~to~do."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option46"] = "I`m~just~walking~by."

tSummerActive2015_ZhongGuaDeGua_Text[18510]["711"] = "Just earn 20 points during 00:00 - 22:59, and you can swap for a gift pack with me. Only once in a day."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["712"] = "~If you earn the most points on the day, you can also claim a super reward from me during 00:00 - 23:00, next day."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["713"] = "~Here`s a secret: you may encounter the Earth God in watermelon field. If so, you`re very lucky to win a mysterious gift."
tSummerActive2015_ZhongGuaDeGua_Text[18510]["714"] = ""
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option47"] = "Let~me~try!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["Option48"] = "I~have~other~things~to~do."

tSummerActive2015_ZhongGuaDeGua_Text[18510]["RewardItem"] = "Congrats! %s did a great job in `Reap What You Sow`, and won a Dragon Ball and 10 Meteor Scrolls!"
tSummerActive2015_ZhongGuaDeGua_Text[18510]["NoBag"] = "You got a full bag. Please make some room in your inventory, first."

-- 暑假欢笑大使
tSummerActive2015_ZhongGuaDeGua_Text[18511] = {}
-- 活动前
tSummerActive2015_ZhongGuaDeGua_Text[18511]["111"] = "Nothing can be better than a fulfilling summer vocation. From July 9th to 22nd, we`re going to"
tSummerActive2015_ZhongGuaDeGua_Text[18511]["112"] = "~hold a series of summer events. Let`s have an unforgettable vocation!"
tSummerActive2015_ZhongGuaDeGua_Text[18511]["113"] = ""
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option1"] = "Great!"

-- 活动期间
tSummerActive2015_ZhongGuaDeGua_Text[18511]["121"] = "Good day! It`s really hot, but I think your passion is more fierce than the weather. From July 9th to 22nd,"
tSummerActive2015_ZhongGuaDeGua_Text[18511]["122"] = "~all heroes above Level 80 are welcome to our summer party! So, which event are you interested?"
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option2"] = "The~Shadows."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option3"] = "The~Great~Magician."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option4"] = "Blood~Arena."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option5"] = "Reap~What~You~Sow."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option6"] = "Iced~Watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option7"] = "Sunblock~Test."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option8"] = "Flowery~Ice~Cream."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option9"] = "I`ll~talk~to~you~later."

-- 活动后
tSummerActive2015_ZhongGuaDeGua_Text[18511]["131"] = "I`m happy you embraced pleasure like me at the summer party. Let`s look forward to the summer vocation next year!"
tSummerActive2015_ZhongGuaDeGua_Text[18511]["132"] = ""
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option10"] = "See~you!"

-- 活动：追缉无影盗贼团。
tSummerActive2015_ZhongGuaDeGua_Text[18511]["211"] = "A theft group, named `The Shadows`, broke the peace of Twin City, and became more and more rampant."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["212"] = "~Commander Chin will offer a high pay to heroes for tracking the Shadows. A hero like you would never turn a blind eye to evil."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option11"] = "Go~find~Commander~Chin."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option12"] = "Learn~about~something~else."

-- 活动：魔术大师。
tSummerActive2015_ZhongGuaDeGua_Text[18511]["311"] = "Famous Magician Ngau has arrived in Twin City with his mysterious Magic Box. It`s said different people will see different treasures in the box."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["312"] = "~Why not go and try?"
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option13"] = "Go~find~Magician~Ngau."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option14"] = "Learn~about~something~else."

-- 活动：无畏勇者。
tSummerActive2015_ZhongGuaDeGua_Text[18511]["411"] = "To train fighters` mind, Champion Ling Wan opened the Blood Arena. It`s a terrific stage to improve fighting skills, and to harvest wonderful rewards."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["412"] = ""
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option15"] = "Take~me~to~see~Ling~Wan."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option16"] = "Learn~about~something~else."

-- 活动：种瓜得瓜。
tSummerActive2015_ZhongGuaDeGua_Text[18511]["511"] = "When summer arrives, Melon Boy welcomes his most busy time. There are so many people who love his watermelons."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["512"] = "~He is asking for hands to help him grow watermelons, and promises a nice pay."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option17"] = "Take~me~to~see~Melon~Boy."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option18"] = "Learn~about~something~else."

-- 活动：冰镇西瓜。
tSummerActive2015_ZhongGuaDeGua_Text[18511]["611"] = "Vendor Wang is too busy to ice his watermelons. Without iced watermelons, summer is not a summer."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["612"] = "~If you`re free, go help Vendor Wang. He is always generous to helpers."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option19"] = "Take~me~to~see~Vendor~Wang."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option20"] = "Learn~about~something~else."

-- 活动：防晒大测验。
tSummerActive2015_ZhongGuaDeGua_Text[18511]["711"] = "Are you concerned about sunburn in such hot days? Don`t worry, Lily White has produced some sunscreen products."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["712"] = "~Now, she needs people to test those sunscreens. Anyone who successfully tests the sunscreen will be nicely rewarded."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option21"] = "Take~me~to~see~Lily."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option22"] = "Learn~about~something~else."

-- 活动：蓝桔梗冰淇淋。
tSummerActive2015_ZhongGuaDeGua_Text[18511]["811"] = "No treat is better than Annie`s ice cream. This summer, Annie made a new type, the Blue Bellflower ice cream."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["812"] = "~However, she is running out of ingredients. It`ll be very nice if you can collect some Blue Bellflower`s Petals for her."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option23"] = "Take~me~to~see~Annie."
tSummerActive2015_ZhongGuaDeGua_Text[18511]["Option24"] = "Learn~about~something~else."

-- 西瓜种子
tSummerActive2015_ZhongGuaDeGua_Text[3001503] = {}
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["BeOverdue"] = "The Watermelon Seed went moldy, and you threw it away."
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["Level"] = "You haven`t reached Level 80. Keep practicing."
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["Range"] = "Plant the seed around (TwinCity 280,371)."
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["Read"] = "Planting"
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["Money"] = "You were blessed by the Earth God, and received 50,000 Silver!"
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["NoSpace"] = "You need to clear 1 empty space in your inventory for the watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["WhiteWatermelon"] = "With your good care, the watermelons grow well. You harvested a Jade Watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["JadeWatermelon"] = "Your watermelons looks fine. You harvested an Emerald Watermelon."
tSummerActive2015_ZhongGuaDeGua_Text[3001503]["GreenWatermelon"] = "Your farming skill needs to be improved. Anyway, you harvested a Green Watermelon."

tSummerActive2015_ZhongGuaDeGua_Text["KillMonster"] = "Your enemy fell down, and dropped a Watermelon Seed."
-- 暑假欢笑大礼包
tSummerActive2015_ZhongGuaDeGua_Text[3001524] = {}
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["PolyGodDan"] = "You opened the Festival Joy Pack, and received an EXP Ball (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["PrayerStone"] = "You opened the Festival Joy Pack, and received a Praying Stone (S) (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["XuanLingBalam"] = "You opened the Festival Joy Pack, and received an Endurance Book (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["KunLunSnow"] = "You opened the Festival Joy Pack, and received an Exp Potion (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["MemorySarah"] = "You opened the Festival Joy Pack, and received a Memory Agate (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["WaterFiberDust"] = "You opened the Festival Joy Pack, and received a bottle of Clean Water (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["BreakTheCity"] = "You opened the Festival Joy Pack, and received a Bomb (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["KingKongLing"] = "You opened the Festival Joy Pack, and received a Yin Yang Fruit (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["Emerald"] = "You opened the Festival Joy Pack, and received an Emerald (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["LingshiNirvana"] = "You opened the Festival Joy Pack, and received a CelestialStone (B)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["Saddle"] = "You opened the Festival Joy Pack, and received 5 Saddles!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["Cuilian"] = "You opened the Festival Joy Pack, and received a RefineryPack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["XuanYuanGem"] = "You opened the Festival Joy Pack, and received a Tortoise Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["LingGem"] = "You opened the Festival Joy Pack, and received a Glory Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["GemOfWrath"] = "You opened the Festival Joy Pack, and received a Thunder Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001524]["MeteorVolume"] = "You opened the Festival Joy Pack, and received a Meteor Scroll!"

-- 暑假欢笑大礼包
tSummerActive2015_ZhongGuaDeGua_Text[3001525] = {}
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["NoSpace"] = "Your inventory is full. Please make some room, first."
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["MiaoStyle"] = "You opened the Festival Joy Pack, and received a 7-day Colorful Dress!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["PrairieWind"] = "You opened the Festival Joy Pack, and received a 7-day garment, Prairie Wind!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["TianshanCharm"] = "You opened the Festival Joy Pack, and received a 7-day garment, Song of Tianshan!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["CaiZhuang"] = "You opened the Festival Joy Pack, and received a 7-day garment, South of Cloud!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["TajikCostumes"] = "You opened the Festival Joy Pack, and received a 7-day garment, Bonfire Night!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["HomeStyle"] = "You opened the Festival Joy Pack, and received a 7-day Angelical Dress!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["DevouringTiger"] = "You opened the Festival Joy Pack, and received a 7-day mount armor, Soul Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["PurpleIceTiger"] = "You opened the Festival Joy Pack, and received a 7-day mount armor, Icy Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["PurpleXuanHuLan"] = "You opened the Festival Joy Pack, and received a 7-day mount armor, HazeTiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["JadeTiger"] = "You opened the Festival Joy Pack, and received a 7-day mount armor, Emerald Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["InkXuanLingHu"] = "You opened the Festival Joy Pack, and received a 7-day mount armor, Abyss Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["TheTigerSpirit"] = "You opened the Festival Joy Pack, and received a 7-day mount armor, Jasper Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["BreakTheCity"] = "You opened the Festival Joy Pack, and received a Bomb!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["MemorySarah"] = "You opened the Festival Joy Pack, and received a Memory Agate!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["JinLinGem"] = "You opened the Festival Joy Pack, and received 2 Refined Kylin Gems!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["GreenRainbowGem"] = "You opened the Festival Joy Pack, and received 2 Refined Rainbow Gems!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["AJewel"] = "You opened the Festival Joy Pack, and received 2 Refined Fury Gems!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["HateDragonGems"] = "You opened the Festival Joy Pack, and received 2 Refined Dragon Gems!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["FengYinGem"] = "You opened the Festival Joy Pack, and received 2 Refined Phoenix Gems!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["PurpleGem"] = "You opened the Festival Joy Pack, and received 2 Refined Violet Gems!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["TheMoonGem"] = "You opened the Festival Joy Pack, and received 2 Refined Moon Gems!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["SkillBook"] = "You opened the Festival Joy Pack, and received a Big Skill Proficiency Pack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["DelicateSpirit"] = "You opened the Festival Joy Pack, and received an Antique Soul Pack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["SeniorCuilian"] = "You opened the Festival Joy Pack, and received a Superior Refinery Pack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["TheHeartBreaks"] = "You opened the Festival Joy Pack, and received a Penitence Amulet!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["Moonlight"] = "You opened the Festival Joy Pack, and received a Moon Box!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["QianKundai"] = "You opened the Festival Joy Pack, and received a Sash (S)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001525]["BusinessDirectory"] = "You opened the Festival Joy Pack, and received a Quest Chance B!"

-- 暑假欢笑大礼包
tSummerActive2015_ZhongGuaDeGua_Text[3001526] = {}
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["NoSpace"] = "Your inventory is full. Please make some room, first."
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["MiaoStyle"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed Colorful Dress!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["PrairieWind"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed garment, Prairie Wind!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["TianshanCharm"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed garment, Song of Tianshan!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["CaiZhuang"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed garment, South of Cloud!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["TajikCostumes"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed garment, Bonfire Night!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["HomeStyle"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed Angelical Dress!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["ExclusiveCoatArms"] = "You opened the Festival Joy Pack, and received a -1 Weapon Accessory Pack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["DevouringTiger"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed mount armor, Soul Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["PurpleIceTiger"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed mount armor, Icy Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["PurpleXuanHuLan"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed mount armor, Haze Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["JadeTiger"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed mount armor, Emerald Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["InkXuanLingHu"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed mount armor, Abyss Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["TheTigerSpirit"] = "You opened the Festival Joy Pack, and received a 7-day 1%% Blessed mount armor, Jasper Tiger!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["DukangWine"] = "You opened the Festival Joy Pack, and received 2 bottles of Health Wine!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["SeniorCuilian"] = "You opened the Festival Joy Pack, and received a Superior Refinery Pack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["DelicateSpirit"] = "You opened the Festival Joy Pack, and received an Antique Soul Pack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["TheHeartBreaks"] = "You opened the Festival Joy Pack, and received a Penitence Amulet!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["TheMare"] = "You opened the Festival Joy Pack, and received a +2 Maroon Steed!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["RedRockLian"] = "You opened the Festival Joy Pack, and received a +2 Stone!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["QianKundai"] = "You opened the Festival Joy Pack, and received a Sash (S)!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["TaoyuanLingYu"] = "You opened the Festival Joy Pack, and received 3 Small Lottery Tickets!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["JinLinGem"] = "You opened the Festival Joy Pack, and received a Super Kylin Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["GreenRainbowGem"] = "You opened the Festival Joy Pack, and received a Super Rainbow Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["AJewel"] = "You opened the Festival Joy Pack, and received a Super Fury Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["HateDragonGems"] = "You opened the Festival Joy Pack, and received a Super Dragon Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["FengYinGem"] = "You opened the Festival Joy Pack, and received a Super Phoenix Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["PurpleGem"] = "You opened the Festival Joy Pack, and received a Super Violet Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["TheMoonGem"] = "You opened the Festival Joy Pack, and received a Super Moon Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["Reel"] = "You opened the Festival Joy Pack, and received an Endeavor Scroll!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["LingGem"] = "You opened the Festival Joy Pack, and received a Refined Glory Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["GemOfWrath"] = "You opened the Festival Joy Pack, and received a Refined Thunder Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["XuanYuanGem"] = "You opened the Festival Joy Pack, and received a Refined Tortoise Gem!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["Strength"] = "You opened the Festival Joy Pack, and received a 150 Points Chi Pack!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["OneDukangWine"] = "You opened the Festival Joy Pack, and received a bottle of Health Wine!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["DragonBall"] = "You opened the Festival Joy Pack, and received a Dragon Ball!"
tSummerActive2015_ZhongGuaDeGua_Text[3001526]["SysMsg"] = "Congratulations! %s opened the Festival Joy Pack, and received a Dragon Ball!"

-- 夏日冰饮
tSummerActive2015_ZhongGuaDeGua_Text[3006619] = {}
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["111"] = "A wave of refreshing events are coming to freeze this summer! So, what`re they?"
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["112"] = "~The Shadows, the Great Magician, Blood Arena, Reap What You Sow, Iced Watermelon, Sunblock Test,"
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["113"] = "~and Flowery Ice Cream. From July 9th to 22nd, talk to Joy in Twin City for more details."
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["114"] = ""
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["Option1"] = "Take~me~to~see~Joy."
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["Option2"] = "Close.~(vanish~after~reading)"

tSummerActive2015_ZhongGuaDeGua_Text[3006619]["BeOverdue"] = "The Summer Drink tasted weird, and you threw it away."
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["RewardExp"] = "You drank the cup, and received 30 minutes of EXP!"
tSummerActive2015_ZhongGuaDeGua_Text[3006619]["RewardCultivation"] = "You drank the cup, and received 15 Study Points!"


------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]VIP特权月线上活动
--Purpose:		2015暑期活动-VIP特权月线上活动
--Creator: 		郑鋆
--Created:		2015/4/17
------------------------------------------------------------------------------------
-- 文字表
tVipPrivilegeOnlineActive2015_Text = {}
-- VIP特权度假村导游先森
tVipPrivilegeOnlineActive2015_Text[18464] = {}

-- 活动前对白
tVipPrivilegeOnlineActive2015_Text[18464]["111"] = "Happy summer holiday! The VIP Resort must be your first choice to spend a relaxed holiday."
tVipPrivilegeOnlineActive2015_Text[18464]["112"] = "~From July 15th to 22nd, all VIP heroes can find me to enter the VIP Resort."
tVipPrivilegeOnlineActive2015_Text[18464]["113"] = "~While others have a chance to seize limited tickets to the VIP Resort from me at some fixed times."
tVipPrivilegeOnlineActive2015_Text[18464]["114"] = ""
tVipPrivilegeOnlineActive2015_Text[18464]["Option1"] = "I`m~looking~forward~to~it!"

-- 活动中对白
tVipPrivilegeOnlineActive2015_Text[18464]["121"] = "Happy summer holiday! The VIP Resort must be your first choice to spend a relaxed holiday."
tVipPrivilegeOnlineActive2015_Text[18464]["122"] = "~From July 15th to 22nd, all VIP heroes can find me to enter the VIP Resort."
tVipPrivilegeOnlineActive2015_Text[18464]["123"] = "~While others have a chance to seize the tickets to the VIP Resort from me at some fixed times."
tVipPrivilegeOnlineActive2015_Text[18464]["124"] = ""
tVipPrivilegeOnlineActive2015_Text[18464]["Option2"] = "Enter~the~VIP~Resort."
tVipPrivilegeOnlineActive2015_Text[18464]["Option3"] = "I~want~the~ticket!"
tVipPrivilegeOnlineActive2015_Text[18464]["Option4"] = "Learn~about~the~resort."
tVipPrivilegeOnlineActive2015_Text[18464]["Option5"] = "I`m~not~interested."

-- 活动后对白
tVipPrivilegeOnlineActive2015_Text[18464]["131"] = "The summer holiday has ended, and the VIP Resort has closed. See you!"
tVipPrivilegeOnlineActive2015_Text[18464]["Option6"] = "See~you."

-- VIP玩家
tVipPrivilegeOnlineActive2015_Text[18464]["211"] = "Oh, your grace. You`re a VIP member. You can enter and leave the VIP Resort with no need of any tickets."
tVipPrivilegeOnlineActive2015_Text[18464]["Option7"] = "Great!"

-- 玩家身上有度假村门票
tVipPrivilegeOnlineActive2015_Text[18464]["221"] = "Buddy, you have already had a Resort Ticket with you. No need to claim more."
tVipPrivilegeOnlineActive2015_Text[18464]["Option8"] = "Okay."

-- 玩家今天进过度假村
tVipPrivilegeOnlineActive2015_Text[18464]["231"] = "You`ve entered the VIP Resort with a ticket once today. You can`t claim more."
tVipPrivilegeOnlineActive2015_Text[18464]["Option9"] = "Alright."

-- 背包空间
tVipPrivilegeOnlineActive2015_Text[18464]["241"] = "I can`t put the ticket into your full bag. Make some room in your inventory, first."
tVipPrivilegeOnlineActive2015_Text[18464]["Option10"] = "I`ll~do~it~now."

-- 本时段没有度假村凭证
tVipPrivilegeOnlineActive2015_Text[18464]["251"] = "Sorry, all the 20 Resort Tickets for this time has been claimed. Be earlier next time! Remember,"
tVipPrivilegeOnlineActive2015_Text[18464]["252"] = "~none-VIP heroes have a chance to claim Resort Tickets at 00:00, 08:00, 12:00, 16:00 and 20:00"
tVipPrivilegeOnlineActive2015_Text[18464]["253"] = "~Only 20 tickets for each time. One can only claim up to 1 ticket, every day."
tVipPrivilegeOnlineActive2015_Text[18464]["Option11"] = "I~see."

-- 领取度假村凭证
tVipPrivilegeOnlineActive2015_Text[18464]["261"] = "Ah, you got a ticket! With it, you can enter the VIP Resort for one time. So, if you leave the VIP Resort,"
tVipPrivilegeOnlineActive2015_Text[18464]["262"] = "~you can`t enter it again, and you can`t claim more tickets from me on that day."
tVipPrivilegeOnlineActive2015_Text[18464]["Option12"] = "Great!~I~got~the~ticket!"

-- 非VIP玩家没有度假村凭证
tVipPrivilegeOnlineActive2015_Text[18464]["311"] = "Sorry, I can`t let you in. The VIP Resort is open to VIP heroes and those who have the Resort Tickets."
tVipPrivilegeOnlineActive2015_Text[18464]["312"] = ""
tVipPrivilegeOnlineActive2015_Text[18464]["Option13"] = "Alright."

-- 非VIP玩家有度假村凭证
tVipPrivilegeOnlineActive2015_Text[18464]["321"] = "You`re welcome to the VIP Resort. Let`s go!"
tVipPrivilegeOnlineActive2015_Text[18464]["Option14"] = "Go!"

-- 非VIP玩家进入过度假村
tVipPrivilegeOnlineActive2015_Text[18464]["331"] = "Sorry, you can`t enter the VIP Resort again, since you`ve already played in the resort today."
tVipPrivilegeOnlineActive2015_Text[18464]["Option15"] = "Alright."

-- VIP玩家
tVipPrivilegeOnlineActive2015_Text[18464]["341"] = "Oh, your grace! I`ll show you the way to the VIP Resort. Have fun!"
tVipPrivilegeOnlineActive2015_Text[18464]["Option16"] = "Thanks!"

-- 活动详情
tVipPrivilegeOnlineActive2015_Text[18464]["411"] = "The VIP Resort was built for VIP heroes, while others can also enter it with a ticket."
tVipPrivilegeOnlineActive2015_Text[18464]["412"] = "~If you`re not a VIP, go and seize a ticket at 00:00, 08:00, 12:00, 16:00 or 20:00. Only 20 tickets for each time."
tVipPrivilegeOnlineActive2015_Text[18464]["413"] = "~Each one can claim up to 1 ticket everyday. In addition, VIP heroes can play all the events and claim rewards,"
tVipPrivilegeOnlineActive2015_Text[18464]["414"] = "~while none-VIP heroes are available for part of the events, and there are no rewards for them."
tVipPrivilegeOnlineActive2015_Text[18464]["Option17"] = "What~are~those~events?"

tVipPrivilegeOnlineActive2015_Text[18464]["511"] = "`Lingering in Water` contains 3 events: sunbathing, diving in the sea world, and playing with fish."
tVipPrivilegeOnlineActive2015_Text[18464]["512"] = "~`Beach Darts` asks you to hit the center of the target. `Cool Summer` prepares you a piece of Beer & Chicken"
tVipPrivilegeOnlineActive2015_Text[18464]["513"] = "~at Receptionist Summer."
tVipPrivilegeOnlineActive2015_Text[18464]["Option18"] = "Got~it."

tVipPrivilegeOnlineActive2015_Text[18464]["RewardItem"] = "You received a Resort Ticket. Talk to Guide Sam with it to enter the VIP Resort."
tVipPrivilegeOnlineActive2015_Text[18464]["NoVIP"] = "Guide Sam led you to the VIP Resort. Please note that if you leave the VIP Resort, you can`t enter it again."
tVipPrivilegeOnlineActive2015_Text[18464]["VIP"] = "Guide Sam led you to the VIP Resort."

-- 度假村招待专员夏呵呵
tVipPrivilegeOnlineActive2015_Text[18465] = {}

-- 活动中对白
tVipPrivilegeOnlineActive2015_Text[18465]["111"] = "Welcome to the VIP Resort! For VIPs, you can play all the events and enjoy an unforgettable holiday here."
tVipPrivilegeOnlineActive2015_Text[18465]["112"] = "~For none-VIPs, you can play the `Beach Darts`, free of charge, however, you can`t claim the rewards."
tVipPrivilegeOnlineActive2015_Text[18465]["113"] = "~Anyway, you`ll have a good time in our resort."
tVipPrivilegeOnlineActive2015_Text[18465]["Option1"] = "Join~`Lingering~in~Water`."
tVipPrivilegeOnlineActive2015_Text[18465]["Option2"] = "Join~`Beach~Darts`."
tVipPrivilegeOnlineActive2015_Text[18465]["Option3"] = "Join~`Cool~Summer`."
tVipPrivilegeOnlineActive2015_Text[18465]["Option4"] = "Reward~for~(Lingering~in~Water)."
tVipPrivilegeOnlineActive2015_Text[18465]["Option5"] = "Reward~(Beach~Darts)."
tVipPrivilegeOnlineActive2015_Text[18465]["Option6"] = "Learn~about~the~events."
tVipPrivilegeOnlineActive2015_Text[18465]["Option7"] = "Leave~the~resort."
tVipPrivilegeOnlineActive2015_Text[18465]["Option8"] = "I`ll~go~play!"

-- 活动后对白
tVipPrivilegeOnlineActive2015_Text[18465]["121"] = "Hey, you`re late, the summer event in the resort has ended. See you next time."
tVipPrivilegeOnlineActive2015_Text[18465]["Option9"] = "I~want~to~leave."

-- 领取“水的亲密接触”礼物。
-- 非VIP玩家
tVipPrivilegeOnlineActive2015_Text[18465]["211"] = "Sorry, you`re not a VIP. The rewards are prepared for VIPs."
tVipPrivilegeOnlineActive2015_Text[18465]["Option10"] = "Alright."

-- 领取过奖励
tVipPrivilegeOnlineActive2015_Text[18465]["221"] = "You`ve claimed your reward today, haven`t you?"
tVipPrivilegeOnlineActive2015_Text[18465]["Option11"] = "Sorry,~I~forgot."

-- 没有完成任务
tVipPrivilegeOnlineActive2015_Text[18465]["231"] = "Wait, you haven`t finished all the events of the `Lingering in Water`. They are sunbathing, diving in the sea world, and playing with fish."
tVipPrivilegeOnlineActive2015_Text[18465]["232"] = "~Play them all, and you can claim a gift from me."
tVipPrivilegeOnlineActive2015_Text[18465]["Option12"] = "I~see."

-- 领取奖励
tVipPrivilegeOnlineActive2015_Text[18465]["241"] = "How do you feel with the `Lingering in Water`? Yeah, it`s amazing."
tVipPrivilegeOnlineActive2015_Text[18465]["242"] = "~I`m happy you like it. Here is a gift for you."
tVipPrivilegeOnlineActive2015_Text[18465]["Option13"] = "Thanks!"

-- 领取“沙滩休闲飞镖”礼物。
-- 没有飞镖靶子
tVipPrivilegeOnlineActive2015_Text[18465]["311"] = "Sorry, you need to hit the target`s center, before you can claim a reward from me."
tVipPrivilegeOnlineActive2015_Text[18465]["312"] = "~Try more, and play more."
tVipPrivilegeOnlineActive2015_Text[18465]["Option14"] = "I~see."

-- 背包空间满
tVipPrivilegeOnlineActive2015_Text[18465]["321"] = "Your inventory is too full to contain more things. Please make some room, first."
tVipPrivilegeOnlineActive2015_Text[18465]["Option15"] = "Okay."

-- 领取奖励
tVipPrivilegeOnlineActive2015_Text[18465]["331"] = "Do you feel refresh and vigorous after playing the `Beach Darts`?"
tVipPrivilegeOnlineActive2015_Text[18465]["332"] = "~I`m happy that you like it. Here`s a gift for you."
tVipPrivilegeOnlineActive2015_Text[18465]["Option16"] = "Thanks!"

-- 我要“清凉一夏”。
-- 已领取过
tVipPrivilegeOnlineActive2015_Text[18465]["411"] = "My friend, I remember you. You`ve already claimed a piece of Beer & Chicken, today. Enjoy yourself."
tVipPrivilegeOnlineActive2015_Text[18465]["Option17"] = "Okay."

-- 领取啤酒和炸鸡
tVipPrivilegeOnlineActive2015_Text[18465]["421"] = "What you think about snowing in summer? It must be cool! Wish my Beer & Chicken bring you a romantic snow and a "
tVipPrivilegeOnlineActive2015_Text[18465]["422"] = "~cold summer."
tVipPrivilegeOnlineActive2015_Text[18465]["Option18"] = "Thanks!"

-- 活动总介绍。
tVipPrivilegeOnlineActive2015_Text[18465]["511"] = "The VIP Resort was built for VIP heroes, while others can also enter it with a ticket."
tVipPrivilegeOnlineActive2015_Text[18465]["512"] = "~If you`re a VIP, you can play all the events in the resort and claim rewards from me."
tVipPrivilegeOnlineActive2015_Text[18465]["513"] = "~While none-VIP heroes are only available for the `Beach Darts`, and there are no rewards for them."
tVipPrivilegeOnlineActive2015_Text[18465]["Option19"] = "What~are~the~events?"

tVipPrivilegeOnlineActive2015_Text[18465]["611"] = "`Lingering in Water` contains 3 events: sunbathing, diving in the sea world, and playing with fish."
tVipPrivilegeOnlineActive2015_Text[18465]["612"] = "~`Beach Darts` asks you to hit the center of the target. `Cool Summer` prepares you a piece of Beer & Chicken"
tVipPrivilegeOnlineActive2015_Text[18465]["613"] = "~at me."
tVipPrivilegeOnlineActive2015_Text[18465]["Option20"] = "I~see."

-- 离开地图。
tVipPrivilegeOnlineActive2015_Text[18465]["711"] = "Well, I`ll lead you out of the VIP Resort."
tVipPrivilegeOnlineActive2015_Text[18465]["Option21"] = "Okay."

tVipPrivilegeOnlineActive2015_Text[18465]["VIP"] = "Receptionist Summer helped you get out of the VIP Resort."
tVipPrivilegeOnlineActive2015_Text[18465]["RewardItem"] = "You received a piece of Beer & Chicken. If you want a snow, you should have the Beer & Chicken."

-- 度假毯
-- 活动中对白
tVipPrivilegeOnlineActive2015_Text[18467] = {}
tVipPrivilegeOnlineActive2015_Text[18467]["111"] = "This soft carpet will serve you in sunbathing, diving in the sea world, and playing with a shoal of fish."
tVipPrivilegeOnlineActive2015_Text[18467]["112"] = "~No matter what you do, you`ll surely embrace delight and leisure."
tVipPrivilegeOnlineActive2015_Text[18467]["113"] = "~Play all the 3 events, and you can claim a gift from Receptionist Summer. So, what do you like to do with this carpet?"
tVipPrivilegeOnlineActive2015_Text[18467]["Option1"] = "Take~a~sunbathing."
tVipPrivilegeOnlineActive2015_Text[18467]["Option2"] = "Dive~in~the~sea~world."
tVipPrivilegeOnlineActive2015_Text[18467]["Option3"] = "Play~with~fish."
tVipPrivilegeOnlineActive2015_Text[18467]["Option4"] = "I~haven`t~decided,~yet."

-- 闲聊对白
tVipPrivilegeOnlineActive2015_Text[18467]["121"] = "This carpet is soft and smooth. However, it`s wet, and you can use it."
tVipPrivilegeOnlineActive2015_Text[18467]["122"] = ""
tVipPrivilegeOnlineActive2015_Text[18467]["Option5"] = "Alright."

-- 非VIP玩家
tVipPrivilegeOnlineActive2015_Text[18467]["211"] = "Sorry, this carpet is for VIP heroes."
tVipPrivilegeOnlineActive2015_Text[18467]["Option6"] = "What~a~pity."

-- 已领取过奖励
tVipPrivilegeOnlineActive2015_Text[18467]["221"] = "You`ve already played this event, and also claimed a reward from Receptionist Summer. Go and play other events!"
tVipPrivilegeOnlineActive2015_Text[18467]["222"] = ""
tVipPrivilegeOnlineActive2015_Text[18467]["Option7"] = "Okay."

-- 已完成任务
tVipPrivilegeOnlineActive2015_Text[18467]["231"] = "You`ve played all the events of the `Lingering in Water`. Go claim a reward from Receptionist Summer."
tVipPrivilegeOnlineActive2015_Text[18467]["232"] = ""
tVipPrivilegeOnlineActive2015_Text[18467]["Option8"] = "Great!~I`m~on~the~way."

-- 已完成该选项
tVipPrivilegeOnlineActive2015_Text[18467]["241"] = "You`ve already joined this event. There are more events waiting for you. Go and play!"
tVipPrivilegeOnlineActive2015_Text[18467]["Option9"] = "Okay."

tVipPrivilegeOnlineActive2015_Text[18467]["Project"] = {}
tVipPrivilegeOnlineActive2015_Text[18467]["Project"]["Complete"] = "You`ve finished the 3 events of the `Lingering in Water`. Go claim your reward from Receptionist Summer."
tVipPrivilegeOnlineActive2015_Text[18467]["Project"][1] = {}
tVipPrivilegeOnlineActive2015_Text[18467]["Project"][1]["Read"] = "Sunbathing"
tVipPrivilegeOnlineActive2015_Text[18467]["Project"][1]["Complete"] = "While you were enjoying sunshine and wind, you found a gift near your carpet. Receptionist Summer has put it into your gift pack. You can claim it after a while."

tVipPrivilegeOnlineActive2015_Text[18467]["Project"][2] = {}
tVipPrivilegeOnlineActive2015_Text[18467]["Project"][2]["Read"] = "Diving"
tVipPrivilegeOnlineActive2015_Text[18467]["Project"][2]["Complete"] = "You dove under the sea, and found a shiny gift around the coral. Receptionist Summer has put it into your gift pack. You can claim it after a while."

tVipPrivilegeOnlineActive2015_Text[18467]["Project"][3] = {}
tVipPrivilegeOnlineActive2015_Text[18467]["Project"][3]["Read"] = "Playing"
tVipPrivilegeOnlineActive2015_Text[18467]["Project"][3]["Complete"] = "While playing with fish, you found a fish spit a gift from its mouth. Receptionist Summer has put it into your gift pack. You can claim it after a while."

-- 飞镖靶子
-- 活动中对白
tVipPrivilegeOnlineActive2015_Text[18471] = {}
tVipPrivilegeOnlineActive2015_Text[18471]["111"] = "Happy summer holiday! The rules are simple. All VIP heroes can cast darts at me, while the VIP who hits the center will be rewarded."
tVipPrivilegeOnlineActive2015_Text[18471]["112"] = "~The reward can be claimed from Receptionist Summer. Don`t lose heart if you fail."
tVipPrivilegeOnlineActive2015_Text[18471]["113"] = "~I`ll give you many chances."
tVipPrivilegeOnlineActive2015_Text[18471]["114"] = ""
tVipPrivilegeOnlineActive2015_Text[18471]["Option1"] = "Cast~the~dart."
tVipPrivilegeOnlineActive2015_Text[18471]["Option2"] = "I`m~not~ready,~yet."

-- 闲聊对白
tVipPrivilegeOnlineActive2015_Text[18471]["121"] = "Who am I? I`m your target. I really like watching people`s faces after they cast darts at me."
tVipPrivilegeOnlineActive2015_Text[18471]["122"] = ""
tVipPrivilegeOnlineActive2015_Text[18471]["Option3"] = "You`re~special."

-- 领取过奖励
tVipPrivilegeOnlineActive2015_Text[18471]["211"] = "Buddy, you`ve already shown me your skill today. If you`re still interested, play with me tomorrow."
tVipPrivilegeOnlineActive2015_Text[18471]["Option4"] = "Okay."

-- VIP
tVipPrivilegeOnlineActive2015_Text[18471]["221"] = "Hey, I still remember your beautiful posture while hitting the target`s center. Go claim your reward from Receptionist Summer!"
tVipPrivilegeOnlineActive2015_Text[18471]["222"] = ""
tVipPrivilegeOnlineActive2015_Text[18471]["Option5"] = "Okay."

tVipPrivilegeOnlineActive2015_Text[18471][1] = {}
tVipPrivilegeOnlineActive2015_Text[18471][1]["Vip"] = {}
tVipPrivilegeOnlineActive2015_Text[18471][1]["Vip"][1] = "Chuu... Excellent! You hit the target`s center. Go claim your reward from Receptionist Summer."
tVipPrivilegeOnlineActive2015_Text[18471][1]["Vip"][2] = "Wow, you must be a professional archer! You easily hit the target`s center. Go claim your reward from Receptionist Summer."
tVipPrivilegeOnlineActive2015_Text[18471][1]["NoVip"] = {}
tVipPrivilegeOnlineActive2015_Text[18471][1]["NoVip"][1] = "Chuu... Excellent! You hit the target`s center."
tVipPrivilegeOnlineActive2015_Text[18471][1]["NoVip"][2] = "Wow, you must be a professional archer! You easily hit the target`s center."

tVipPrivilegeOnlineActive2015_Text[18471][2] = {}
tVipPrivilegeOnlineActive2015_Text[18471][2][1] = "What a pity, you almost hit the target`s center. Don`t lose heart."
tVipPrivilegeOnlineActive2015_Text[18471][2][2] = "You sneezed at the critical moment, and the dart flied away from the target. Keep practicing!"

tVipPrivilegeOnlineActive2015_Text[18471][3] = {}
tVipPrivilegeOnlineActive2015_Text[18471][3][1] = "A strange wind blew, and your dart disappeared."
tVipPrivilegeOnlineActive2015_Text[18471][3][2] = "Um... You hit the wrong target."

-- 啤酒和炸鸡
tVipPrivilegeOnlineActive2015_Text[3004104] = {}
tVipPrivilegeOnlineActive2015_Text[3004104]["BeOverdue"] = "The VIP Privilege event has ended, and this item is useless now. You threw it away."
tVipPrivilegeOnlineActive2015_Text[3004104]["Overdue"] = "The Beer & Chicken brought you a romantic flowery snow. Enjoy a cool summer!"

-- VIP特权度假邀请卡
tVipPrivilegeOnlineActive2015_Text[3006618] = {}
tVipPrivilegeOnlineActive2015_Text[3006618]["111"] = "From July 15th to 22nd, VIPs are invited to a wonderful trip in the VIP Resort with fresh cool ocean breeze"
tVipPrivilegeOnlineActive2015_Text[3006618]["112"] = "~and fantastic events like Lingering in Water, Cool Summer, and Beach Darts. Come and talk to Guide Sam"
tVipPrivilegeOnlineActive2015_Text[3006618]["113"] = "~Twin City to enter the VIP Resort! The Blessing Fox will also present the most sincere blessings to VIP heroes"
tVipPrivilegeOnlineActive2015_Text[3006618]["114"] = "~at that time. Trust me, you will never want to miss it."
tVipPrivilegeOnlineActive2015_Text[3006618]["115"] = ""
tVipPrivilegeOnlineActive2015_Text[3006618]["Option1"] = "Take~me~to~see~Sam."
tVipPrivilegeOnlineActive2015_Text[3006618]["Option2"] = "Close.~(Vanish~after~reading)"

tVipPrivilegeOnlineActive2015_Text[3006618]["BeOverdue"] = "The VIP Resort Invitation has expired, and you threw it away."
tVipPrivilegeOnlineActive2015_Text[3006618]["RewardExp"] = "You checked the VIP Resort Invitation, and received 30 minutes of EXP!"
tVipPrivilegeOnlineActive2015_Text[3006618]["RewardCultivation"] = "You checked the VIP Resort Invitation, and received 15 Study Points!"

-- 系统公告
tVipPrivilegeOnlineActive2015_Text["SysMsg"] = {}
tVipPrivilegeOnlineActive2015_Text["SysMsg"][7] = "Pay attention! Guide Sam (300,366) will give out limited VIP Resort Tickets at 08:00. First come, first served!"
tVipPrivilegeOnlineActive2015_Text["SysMsg"][11] = "Pay attention! Guide Sam (300,366) will give out limited VIP Resort Tickets at 12:00. First come, first served!"
tVipPrivilegeOnlineActive2015_Text["SysMsg"][15] = "Pay attention! Guide Sam (300,366) will give out limited VIP Resort Tickets at 16:00. First come, first served!"
tVipPrivilegeOnlineActive2015_Text["SysMsg"][19] = "Pay attention! Guide Sam (300,366) will give out limited VIP Resort Tickets at 20:00. First come, first served!"
tVipPrivilegeOnlineActive2015_Text["SysMsg"][23] = "Pay attention! Guide Sam (300,366) will give out limited VIP Resort Tickets at 00:00. First come, first served!"
------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]VIP福利大赠送
--Purpose:		2015暑期活动-VIP福利大赠送
--Creator: 		郑鋆
--Created:		2015/4/17
------------------------------------------------------------------------------------
	
-- 文字索引表
tVipWelfareGifts2015_Text = {}
-- VIP福利宝贝狐
tVipWelfareGifts2015_Text[18466] = {}
-- 活动前对白
tVipWelfareGifts2015_Text[18466]["111"] = "The summer holiday is coming, and we`re going to welcome the splendid VIP Privilege event."
tVipWelfareGifts2015_Text[18466]["112"] = "~From July 15th to 22nd, Blessing Fox will present the most sincere blessings to VIP heroes in Twin City."
tVipWelfareGifts2015_Text[18466]["113"] = "~Don`t miss the chance! Blessing Fox has prepared so many gifts for you!"
tVipWelfareGifts2015_Text[18466]["Option1"] = "I`ll~be~there."

-- 活动中对白
tVipWelfareGifts2015_Text[18466]["121"] = "The VIP Blessing Giveaway really comes! As long as you`re a VIP, you can claim a gift from me,"
tVipWelfareGifts2015_Text[18466]["122"] = "~everyday during the event. In addition, I`ve prepared a nice reward for daily check-in."
tVipWelfareGifts2015_Text[18466]["123"] = "~Check in for a certain number of days as required by Blessing Fox, and you`ll be nicely rewarded."
tVipWelfareGifts2015_Text[18466]["Option2"] = "Today`s~check-in."
tVipWelfareGifts2015_Text[18466]["Option3"] = "Claim~today`s~gift."
tVipWelfareGifts2015_Text[18466]["Option4"] = "Tell~me~more."
tVipWelfareGifts2015_Text[18466]["Option5"] = "I`ll~talk~to~you~later."

-- 活动后对白
tVipWelfareGifts2015_Text[18466]["131"] = "Do you like my gift? Thanks! I know my effort is worthy. See you next year!"
tVipWelfareGifts2015_Text[18466]["132"] = ""
tVipWelfareGifts2015_Text[18466]["133"] = ""
tVipWelfareGifts2015_Text[18466]["Option6"] = "See~you."

-- 非VIP
tVipWelfareGifts2015_Text[18466]["211"] = "Sorry, this event is for VIP heroes."
tVipWelfareGifts2015_Text[18466]["212"] = ""
tVipWelfareGifts2015_Text[18466]["Option7"] = "Alright."

-- 签到过
tVipWelfareGifts2015_Text[18466]["221"] = "Stop kidding me. You`ve already checked in, today. Blessing Fox has a good memory."
tVipWelfareGifts2015_Text[18466]["Option8"] = "Alright."

-- 背包满
tVipWelfareGifts2015_Text[18466]["231"] = "Your inventory is full. Why not make some room for the gift? I have a lot of beautiful things for you."
tVipWelfareGifts2015_Text[18466]["Option9"] = "I`ll~do~it~now."

-- 领取今日福利
tVipWelfareGifts2015_Text[18466]["311"] = "Wait, you`ve already claimed today`s gift, haven`t you? I`m not that old to forget things."
tVipWelfareGifts2015_Text[18466]["Option10"] = "Sorry..."

-- 了解活动详情
tVipWelfareGifts2015_Text[18466]["411"] = "VIP heroes can claim a gift from me, everyday during the event. The gifts are given according to your VIP level,"
tVipWelfareGifts2015_Text[18466]["412"] = "~including double EXP, Chi Points, VIP Training Pack, Study Points, VIP Penitence Amulet Pack,"
tVipWelfareGifts2015_Text[18466]["413"] = "~and other VIP gift packs. In addition, Blessing Fox has a special gift pack for daily check-in."
tVipWelfareGifts2015_Text[18466]["Option11"] = "What~gift~for~daily~check-in?"
tVipWelfareGifts2015_Text[18466]["Option12"] = "Learn~about~other~things."
tVipWelfareGifts2015_Text[18466]["Option13"] = "I~see."

tVipWelfareGifts2015_Text[18466]["511"] = "For VIP heroes in Jiang Hu, I`ll give a Heaven Key and a VIP Talent Pack for their each check-in."
tVipWelfareGifts2015_Text[18466]["512"] = "~While for VIP heroes not in Jiang Hu, it`s a Heaven Key and a VIP Travel Pack. If you check in for 3, 5 or 7 days in a row,"
tVipWelfareGifts2015_Text[18466]["513"] = "~you`ll be extra rewarded with a VIP Summer Pack, a VIP Singing Pack or a VIP Passion Pack."
tVipWelfareGifts2015_Text[18466]["514"] = "~If you forget to check in in the middle, your check-in days will be reset."
tVipWelfareGifts2015_Text[18466]["Option14"] = "Learn~about~other~things."
tVipWelfareGifts2015_Text[18466]["Option15"] = "Got~it."

tVipWelfareGifts2015_Text["RewardItem"] = {}
tVipWelfareGifts2015_Text["RewardItem"][1] = "You received a Heaven Key and 1 VIP Talent Packs!"
tVipWelfareGifts2015_Text["RewardItem"][2] = "You received a Heaven Key and a VIP Travel Pack!"
tVipWelfareGifts2015_Text["RewardItem"][3] = "You received a Heaven Key, a VIP Talent Pack and a VIP Summer Pack!"
tVipWelfareGifts2015_Text["RewardItem"][4] = "You received a Heaven Key, a VIP Travel Pack and a VIP Summer Pack!"
tVipWelfareGifts2015_Text["RewardItem"][5] = "You received a Heaven Key, a VIP Talent Pack and a VIP Singing Pack!"
tVipWelfareGifts2015_Text["RewardItem"][6] = "You received a Heaven Key, a VIP Travel Pack and a VIP Singing Pack!"
tVipWelfareGifts2015_Text["RewardItem"][7] = "You received a Heaven Key, a VIP Talent Pack and a VIP Passion Pack!"
tVipWelfareGifts2015_Text["RewardItem"][8] = "You received a Heaven Key, a VIP Travel Pack and a VIP Passion Pack!"

tVipWelfareGifts2015_Text["Welfare"] = {}
tVipWelfareGifts2015_Text["Welfare"][1] = "You received 3-hour double EXP, 100 Chi Points, a VIP Training Pack, 50 Study Points and a VIP Penitence Amulet Pack!"
tVipWelfareGifts2015_Text["Welfare"][2] = "You received 3-hour double EXP, 100 Chi Points, 50 Study Points and a VIP Penitence Amulet Pack!"
tVipWelfareGifts2015_Text["Welfare"][3] = "You received 4-hour double EXP, 300 Chi Points, 2 VIP Training Packs, 100 Study Points and 2 VIP Penitence Amulet Packs!"
tVipWelfareGifts2015_Text["Welfare"][4] = "You received 4-hour double EXP, 300 Chi Points, 100 Study Points and 2 VIP Penitence Amulet Packs!"
tVipWelfareGifts2015_Text["Welfare"][5] = "You received 5-hour double EXP, 400 Chi Points, 3 VIP Training Packs, 150 Study Points and 3 VIP Penitence Amulet Packs!"
tVipWelfareGifts2015_Text["Welfare"][6] = "You received 5-hour double EXP, 400 Chi Points, 150 Study Points and 3 VIP Penitence Amulet Packs!"
tVipWelfareGifts2015_Text["Welfare"][7] = "You received 6-hour double EXP, 500 Chi Points, 4 VIP Training Packs, 200 Study Points and 4 VIP Penitence Amulet Packs!"
tVipWelfareGifts2015_Text["Welfare"][8] = "You received 6-hour double EXP, 500 Chi Points, 200 Study Points and 4 VIP Penitence Amulet Packs!"

-- VIP真气礼包
tVipWelfareGifts2015_Text[3004027] = {}
tVipWelfareGifts2015_Text[3004027]["BeOverdue"] = "The~magic of the gift pack has vanished, and you threw it away."
tVipWelfareGifts2015_Text[3004027]["Full"] = "You`re with full Talent Amount. You need to consume some first."
tVipWelfareGifts2015_Text[3004027]["Msg"] = "You received 2 Talents of Jiang Hu."

-- VIP夏日炎炎礼包
tVipWelfareGifts2015_Text[3004028] = {}
tVipWelfareGifts2015_Text[3004028]["BeOverdue"] = "The magic of the gift pack has vanished, and you threw it away."
tVipWelfareGifts2015_Text[3004028]["Full"] = "Your inventory is full. Make some room first, and then open the pack."
tVipWelfareGifts2015_Text[3004028]["Msg"] = {}
tVipWelfareGifts2015_Text[3004028]["Msg"][1] = "You received a Heaven Key and 3 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004028]["Msg"][2] = "You received a Heaven Key and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004028]["Msg"][3] = "You received 2 Heaven Keys and 4 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004028]["Msg"][4] = "You received 2 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004028]["Msg"][5] = "You received 3 Heaven Keys and 5 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004028]["Msg"][6] = "You received 3 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004028]["Msg"][7] = "You received 4 Heaven Keys and 6 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004028]["Msg"][8] = "You received 4 Heaven Keys and a VIP Travel Pack!"

-- VIP夏日蝉鸣礼包
tVipWelfareGifts2015_Text[3004029] = {}
tVipWelfareGifts2015_Text[3004029]["BeOverdue"] = "The magic of the gift pack has vanished, and you threw it away."
tVipWelfareGifts2015_Text[3004029]["Full"] = "Your inventory is full. Make some room first, and then open the pack."
tVipWelfareGifts2015_Text[3004029]["Msg"] = {}
tVipWelfareGifts2015_Text[3004029]["Msg"][1] = "You received 2 Heaven Keys and 5 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004029]["Msg"][2] = "You received 2 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004029]["Msg"][3] = "You received 3 Heaven Keys and 6 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004029]["Msg"][4] = "You received 3 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004029]["Msg"][5] = "You received 4 Heaven Keys and 7 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004029]["Msg"][6] = "You received 4 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004029]["Msg"][7] = "You received 5 Heaven Keys and 8 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004029]["Msg"][8] = "You received 5 Heaven Keys and a VIP Travel Pack!"

-- VIP夏日倾情礼包
tVipWelfareGifts2015_Text[3004030] = {}
tVipWelfareGifts2015_Text[3004030]["BeOverdue"] = "The magic of the gift pack has vanished, and you threw it away."
tVipWelfareGifts2015_Text[3004030]["Full"] = "Your inventory is full. Make some room first, and then open the pack."
tVipWelfareGifts2015_Text[3004030]["Msg"] = {}
tVipWelfareGifts2015_Text[3004030]["Msg"][1] = "You received 3 Heaven Keys and 7 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004030]["Msg"][2] = "You received 3 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004030]["Msg"][3] = "You received 4 Heaven Keys and 8 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004030]["Msg"][4] = "You received 4 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004030]["Msg"][5] = "You received 5 Heaven Keys and 9 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004030]["Msg"][6] = "You received 5 Heaven Keys and a VIP Travel Pack!"
tVipWelfareGifts2015_Text[3004030]["Msg"][7] = "You received 6 Heaven Keys and 10 VIP Talent Packs!"
tVipWelfareGifts2015_Text[3004030]["Msg"][8] = "You received 6 Heaven Keys and a VIP Travel Pack!"

-- VIP修炼礼包
tVipWelfareGifts2015_Text[3004031] = {}
tVipWelfareGifts2015_Text[3004031]["BeOverdue"] = "The magic of the gift pack has vanished, and you threw it away."
tVipWelfareGifts2015_Text[3004031]["Full"] = "You`re with full Free Training chances. You need to consume some first."
tVipWelfareGifts2015_Text[3004031]["Msg"] = "You received 3 Free Training chances."

-- VIP清心符礼包
tVipWelfareGifts2015_Text[3004032] = {}
tVipWelfareGifts2015_Text[3004032]["BeOverdue"] = "The magic of the gift pack has vanished, and you threw it away."
tVipWelfareGifts2015_Text[3004032]["111"] = "At the cost of 30 CPs, you can open the VIP Penitence Amulet Pack and receive 2 - 10 Penitence Amulets (B) from the pack."
tVipWelfareGifts2015_Text[3004032]["112"] = "~Do you want to open it, now?"
tVipWelfareGifts2015_Text[3004032]["Option1"] = "Yes."

tVipWelfareGifts2015_Text[3004032]["121"] = "Are you sure you want to pay 30 CPs to open this pack?"
tVipWelfareGifts2015_Text[3004032]["Option2"] = "Yes."
tVipWelfareGifts2015_Text[3004032]["Option3"] = "No."

tVipWelfareGifts2015_Text[3004032]["131"] = "You don`t have enough CPs to open this pack."
tVipWelfareGifts2015_Text[3004032]["Option4"] = "I~see."

tVipWelfareGifts2015_Text[3004032]["141"] = "Your inventory is full. Make some room first, and then open the pack."
tVipWelfareGifts2015_Text[3004032]["Option5"] = "Okay."

tVipWelfareGifts2015_Text[3004032]["Msg"] = {}
tVipWelfareGifts2015_Text[3004032]["Msg"][1] = "You received 2 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][2] = "You received 3 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][3] = "You received 4 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][4] = "You received 5 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][5] = "You received 6 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][6] = "You received 7 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][7] = "You received 8 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][8] = "You received 9 Penitence Amulets (B)!"
tVipWelfareGifts2015_Text[3004032]["Msg"][9] = "You received 10 Penitence Amulets (B)!"

-- VIP行走江湖礼包
tVipWelfareGifts2015_Text[3004117] = {}
tVipWelfareGifts2015_Text[3004117]["BeOverdue"] = "The magic of the gift pack has vanished, and you threw it away."
tVipWelfareGifts2015_Text[3004117]["NoItem"] = "You don`t have that pack in your inventory."
-- VIP1-3
tVipWelfareGifts2015_Text[3004117]["111"] = "If you choose the EXP reward when you`re with full EXP, you won`t receive the EXP. So, choose wisely."
tVipWelfareGifts2015_Text[3004117]["112"] = "~What do you want to claim from this pack?"
tVipWelfareGifts2015_Text[3004117]["Option1"] = "60~minutes~of~EXP."
tVipWelfareGifts2015_Text[3004117]["Option2"] = "30~Study~Points."
-- VIP4
tVipWelfareGifts2015_Text[3004117]["121"] = "If you choose the EXP reward when you`re with full EXP, you won`t receive the EXP. So, choose wisely."
tVipWelfareGifts2015_Text[3004117]["122"] = "~What do you want to claim from this pack?"
tVipWelfareGifts2015_Text[3004117]["Option3"] = "120~minutes~of~EXP."
tVipWelfareGifts2015_Text[3004117]["Option4"] = "100~Study~Points."
-- VIP5
tVipWelfareGifts2015_Text[3004117]["131"] = "If you choose the EXP reward when you`re with full EXP, you won`t receive the EXP. So, choose wisely."
tVipWelfareGifts2015_Text[3004117]["132"] = "~What do you want to claim from this pack?"
tVipWelfareGifts2015_Text[3004117]["Option5"] = "200~minutes~of~EXP."
tVipWelfareGifts2015_Text[3004117]["Option6"] = "150~Study~Points."
-- VIP6
tVipWelfareGifts2015_Text[3004117]["141"] = "If you choose the EXP reward when you`re with full EXP, you won`t receive the EXP. So, choose wisely."
tVipWelfareGifts2015_Text[3004117]["142"] = "~What do you want to claim from this pack?"
tVipWelfareGifts2015_Text[3004117]["Option7"] = "320~minutes~of~EXP."
tVipWelfareGifts2015_Text[3004117]["Option8"] = "220~Study~Points."
-- 60分钟经验
tVipWelfareGifts2015_Text[3004117]["211"] = "Please note that, if you`re already with full EXP and you choose the EXP reward, you won`t have your EXP increased."
tVipWelfareGifts2015_Text[3004117]["212"] = "~So, are you sure you want to claim 60 minutes of EXP?"
tVipWelfareGifts2015_Text[3004117]["Option9"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option10"] = "No."
-- 30点修行值
tVipWelfareGifts2015_Text[3004117]["221"] = "Are you sure you want to claim 30 Study Points?"
tVipWelfareGifts2015_Text[3004117]["Option11"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option12"] = "No."
-- 120分钟经验
tVipWelfareGifts2015_Text[3004117]["311"] = "Please note that, if you`re already with full EXP and you choose the EXP reward, you won`t have your EXP increased."
tVipWelfareGifts2015_Text[3004117]["312"] = "~So, are you sure you want to claim 120 minutes of EXP?"
tVipWelfareGifts2015_Text[3004117]["Option13"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option14"] = "No."
-- 100点修行值
tVipWelfareGifts2015_Text[3004117]["321"] = "Are you sure you want to claim 100 Study Points?"
tVipWelfareGifts2015_Text[3004117]["Option15"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option16"] = "No."
-- 200分钟经验
tVipWelfareGifts2015_Text[3004117]["411"] = "Please note that, if you`re already with full EXP and you choose the EXP reward, you won`t have your EXP increased."
tVipWelfareGifts2015_Text[3004117]["412"] = "~So, are you sure you want to claim 200 minutes of EXP?"
tVipWelfareGifts2015_Text[3004117]["Option17"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option18"] = "No."
-- 150点修行值
tVipWelfareGifts2015_Text[3004117]["421"] = "Are you sure you want to claim 150 Study Points?"
tVipWelfareGifts2015_Text[3004117]["Option19"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option20"] = "No."
-- 320分钟经验
tVipWelfareGifts2015_Text[3004117]["511"] = "Please note that, if you`re already with full EXP and you choose the EXP reward, you won`t have your EXP increased."
tVipWelfareGifts2015_Text[3004117]["512"] = "~So, are you sure you want to claim 320 minutes of EXP?"
tVipWelfareGifts2015_Text[3004117]["Option21"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option22"] = "No."
-- 220点修行值
tVipWelfareGifts2015_Text[3004117]["521"] = "Are you sure you want to claim 220 Study Points?"
tVipWelfareGifts2015_Text[3004117]["Option23"] = "Yes."
tVipWelfareGifts2015_Text[3004117]["Option24"] = "No."

tVipWelfareGifts2015_Text[3004117]["Msg"] = {}
tVipWelfareGifts2015_Text[3004117]["Msg"][1] = "You received 60 minutes of EXP!"
tVipWelfareGifts2015_Text[3004117]["Msg"][2] = "You received 30 Study Points!"
tVipWelfareGifts2015_Text[3004117]["Msg"][3] = "You received 120 minutes of EXP!"
tVipWelfareGifts2015_Text[3004117]["Msg"][4] = "You received 100 Study Points!"
tVipWelfareGifts2015_Text[3004117]["Msg"][5] = "You received 200 minutes of EXP!"
tVipWelfareGifts2015_Text[3004117]["Msg"][6] = "You received 150 Study Points!"
tVipWelfareGifts2015_Text[3004117]["Msg"][7] = "You received 320 minutes of EXP!"
tVipWelfareGifts2015_Text[3004117]["Msg"][8] = "You received 220 Study Points!"


----------------------------------------------------------------------------
--Name:		[征服][功能脚本]背包信.lua
--Purpose:	背包信
--Creator: 	郑鋆
--Created:	2015/05/27
----------------------------------------------------------------------------

tBackpackLetter_Text[3006618] = {}
tBackpackLetter_Text[3006618]["NoSpace"] = "Your inventory is full. Please make some room, and then login again to receive a VIP Resort Invitation."
tBackpackLetter_Text[3006618]["RewardItem"] = "You received a VIP Resort Invitation. Hurry and check it in your inventory."

tBackpackLetter_Text[3006619] = {}
tBackpackLetter_Text[3006619]["NoSpace"] = "Your inventory is full. Please make some room, and then login again to receive a cup of Summer Drink."
tBackpackLetter_Text[3006619]["RewardItem"] = "You received a cup of Summer Drink. Enjoy it!"

tBackpackLetter_Text[3007157] = {}
tBackpackLetter_Text[3007157]["NoSpace"] = "Your inventory is full. Please make some room, and login again to receive a letter about the Secret of Snow."
tBackpackLetter_Text[3007157]["RewardItem"] = "You received a letter about the Secret of Snow. Hurry and check it in your inventory."


----------------------------------------------------------------------------
--Name:		[征服][任务脚本]战力锦标赛.lua
--Purpose:	战力锦标赛
--Creator: 	丁晨
--Created:	2015/06/30
----------------------------------------------------------------------------


tPowerChampionships_Match_Text ={}

--NPC——锦标赛入场员
--活动前
tPowerChampionships_Match_Text[18662] = {}
tPowerChampionships_Match_Text[18662]["Text111"] ="The Battle Power Tournament will be held at 20:00 - 21:00, this coming July 12th, July 19th, July 26th, and August 2nd."
tPowerChampionships_Match_Text[18662]["Text112"] ="~Participants will be fighting in 3 BP divisions. All champions will receive great prizes like tons of CPs and Chi Points. For heroes who achieve"
tPowerChampionships_Match_Text[18662]["Text113"] ="~consecutive wins, we`ll award an extra bonus. Are you interested? Come and sign up with me 15 minutes before the match starts."

--欧服
tPowerChampionships_Match_Text[18662]["Text151"] ="The Battle Power Tournament will be held at 22:00 - 23:00, this coming July 12th, July 19th, July 26th, and August 2nd."

tPowerChampionships_Match_Text[18662]["Option1"] = "I~can`t~wait~to~prove~myself!"

--活动后
tPowerChampionships_Match_Text[18662]["Text114"] ="The Battle Power Tournament has ended. See you next time."
tPowerChampionships_Match_Text[18662]["Option2"] = "See~you."

--活动期间
tPowerChampionships_Match_Text[18662]["Text115"] ="The Battle Power Tournament will be held at 20:00 - 21:00, this coming July 12th, July 19th, July 26th, and August 2nd."

--欧服
tPowerChampionships_Match_Text[18662]["Text152"] ="The Battle Power Tournament will be held at 22:00 - 23:00, this coming July 12th, July 19th, July 26th, and August 2nd."

tPowerChampionships_Match_Text[18662]["Text116"] ="~Participants will be fighting in 3 BP divisions. All champions will receive great prizes like tons of CPs and Chi Points. If you achieve consecutive wins,"
tPowerChampionships_Match_Text[18662]["Text117"] ="~you can claim an extra reward with your BP Tokens after August 2nd. Get prepared, and sign up with me 15 minutes before the match starts."
tPowerChampionships_Match_Text[18662]["Option3"] = "Join~250-300~BP~division."
tPowerChampionships_Match_Text[18662]["Option4"] = "Join~301-350~BP~division."
tPowerChampionships_Match_Text[18662]["Option5"] = "Join~350+~BP~division."
tPowerChampionships_Match_Text[18662]["Option6"] = "Claim~reward~for~2~wins."
tPowerChampionships_Match_Text[18662]["Option7"] = "Claim~reward~for~3~wins."
tPowerChampionships_Match_Text[18662]["Option8"] = "Claim~reward~for~4~wins."
tPowerChampionships_Match_Text[18662]["Option9"] = "Rules~and~prizes."
tPowerChampionships_Match_Text[18662]["Option10"] = "Sounds~exciting."

--非比赛时间
tPowerChampionships_Match_Text[18662]["Text118"] ="You`re unable to sign up, since it isn`t the right time. Remember, the tournament is held at 20:00 - 21:00, this coming July 12th,"

--欧服
tPowerChampionships_Match_Text[18662]["Text153"] ="You`re unable to sign up, since it isn`t the right time. Remember, the tournament is held at 22:00 - 23:00, this coming July 12th,"

tPowerChampionships_Match_Text[18662]["Text119"] ="~July 19th, July 26th, and August 2nd. During the 15 minutes before each match, you can sign up with me. Don`t miss the time!"
tPowerChampionships_Match_Text[18662]["Option11"] = "I~see."

--战力不足
tPowerChampionships_Match_Text[18662]["Text120"] ="Your Battle Power doesn`t meet the requirement of this division. Go pick your BP division."
tPowerChampionships_Match_Text[18662]["Option12"] = "Okay."


--玩家并非蝉联冠军
tPowerChampionships_Match_Text[18662]["Text121"] ="You can`t claim the reward for consecutive wins, since you failed to meet the requirement."
tPowerChampionships_Match_Text[18662]["Option13"] = "Alright."

--领取蝉联冠军
tPowerChampionships_Match_Text[18662]["Text122"] ="Get the rewards you deserve! Here are %s Chi Points for you. Congratulations!"
tPowerChampionships_Match_Text[18662]["Option14"] = "Thanks!"

--了解战力锦标赛活动
tPowerChampionships_Match_Text[18662]["Text123"] ="The tournament is divided into 3 groups based on BP value. Choose a group according to your BP, and sign up with me"
tPowerChampionships_Match_Text[18662]["Text124"] ="~15 minutes before the match starts. The only hero who survives in the field till the last will be the winner to be rewarded by the Prize Presenter."
tPowerChampionships_Match_Text[18662]["Text125"] ="~If the match concludes with no champion, all participants will be teleported out."
tPowerChampionships_Match_Text[18662]["Text126"] ="~Is there anything else you want to know about?"
tPowerChampionships_Match_Text[18662]["Option15"] = "Rewards~for~the~3~divisions."
tPowerChampionships_Match_Text[18662]["Option16"] = "Rewards~for~consecutive~wins."

--了解不同战力段的奖励。
tPowerChampionships_Match_Text[18662]["Text127"] ="250-300 BP Division: \n300 CPs, 5,000 Chi Points, 1 week of honor halo, and an Average BP Token;\n"
tPowerChampionships_Match_Text[18662]["Text128"] ="301-350 BP Division: \n500 CPs, 10,000 Chi Points, 1 week of honor halo, and an Elite BP Token;\n"
tPowerChampionships_Match_Text[18662]["Text129"] ="350+ BP Division: \n1,000 CPs, 15,000 Chi Points, 1 week of honor halo, and a Super BP Token.\n"
tPowerChampionships_Match_Text[18662]["Option17"] = "I~see."

--了解蝉联冠军奖励。
tPowerChampionships_Match_Text[18662]["Text130"] ="Heroes who achieve 2, 3, or 4 consecutive wins in the tournament can claim an extra reward from me"
tPowerChampionships_Match_Text[18662]["Text131"] ="~after August 2nd. If you win such an honor, come and find me with your BP Tokens for the reward:\n"
tPowerChampionships_Match_Text[18662]["Text132"] ="250-300 BP Division: 5,000 Chi Points for 2 consecutive wins;\n"
tPowerChampionships_Match_Text[18662]["Text133"] ="                            10,000 Chi Points for 3 consecutive wins;\n"
tPowerChampionships_Match_Text[18662]["Text134"] ="                            20,000 Chi Points for 4 consecutive wins;"
tPowerChampionships_Match_Text[18662]["Option18"] = "Next."

--下一页
tPowerChampionships_Match_Text[18662]["Text135"] ="301-350 BP Division: 10,000 Chi Points for 2 consecutive wins;\n"
tPowerChampionships_Match_Text[18662]["Text136"] ="                            20,000 Chi Points for 3 consecutive wins;\n"
tPowerChampionships_Match_Text[18662]["Text137"] ="                            30,000 Chi Points for 4 consecutive wins;\n"
tPowerChampionships_Match_Text[18662]["Text138"] ="350+ BP Division: 20,000 Chi Points for 2 consecutive wins;\n"
tPowerChampionships_Match_Text[18662]["Text139"] ="                        30,000 Chi Points for 3 consecutive wins;\n"
tPowerChampionships_Match_Text[18662]["Text140"] ="                        50,000 Chi Points for 4 consecutive wins;"
tPowerChampionships_Match_Text[18662]["Option19"] = "I~see."

--NPC——赛场颁奖员
--主对白
tPowerChampionships_Match_Text[18663]={}
tPowerChampionships_Match_Text[18663]["Text141"] ="There will be no mercy for everyone in the tournament. Take care of yourself. When the match ends, the only hero"
tPowerChampionships_Match_Text[18663]["Text142"] ="~who survives in the field will be the winner, and he/she can claim a prize from me."
tPowerChampionships_Match_Text[18663]["Option20"] = "Claim~my~prize."
tPowerChampionships_Match_Text[18663]["Option21"] = "I~quit."
tPowerChampionships_Match_Text[18663]["Option22"] = "Got~it."

--非唯一获胜者
tPowerChampionships_Match_Text[18663]["Text143"] ="You still have opponents in the field. Go and fight till the last!"
tPowerChampionships_Match_Text[18663]["Option23"] = "Alright."

--已领取过
tPowerChampionships_Match_Text[18663]["Text144"] ="I know you`re the winner of the tournament, but you`ve already claimed your prize. I have no more for you."
tPowerChampionships_Match_Text[18663]["Option24"] = "I~see."

--领取成功
tPowerChampionships_Match_Text[18663]["Text145"] ="Congratulations! You won the championship in %s-%s BP division! Get your reward, %s!"
tPowerChampionships_Match_Text[18663]["Option25"] = "Thanks!"

--比赛还没开始
tPowerChampionships_Match_Text[18663]["Text146"] ="The tournament has not started. If you give up and leave, you need to sign up again. Have you decided?"
tPowerChampionships_Match_Text[18663]["Text147"] =""
tPowerChampionships_Match_Text[18663]["Option26"] = "Yes."
tPowerChampionships_Match_Text[18663]["Option27"] = "No,~I~changed~my~mind."

--比赛进行中
tPowerChampionships_Match_Text[18663]["Text148"] ="The tournament has not ended. Are you sure you want to quit, and return to Twin City?"
tPowerChampionships_Match_Text[18663]["Option28"] = "Yes."
tPowerChampionships_Match_Text[18663]["Option29"] = "No,~I~changed~my~mind."

--背包空间不足
tPowerChampionships_Match_Text[18663]["Text149"] ="You`re carrying a full inventory. Please make some room, first."
tPowerChampionships_Match_Text[18663]["Option30"] ="I~see."

--背包天石不足
tPowerChampionships_Match_Text[18663]["Text150"] ="You`re carrying the maximum amount of CPs. I suggest you to deposit or spend some, first."
tPowerChampionships_Match_Text[18663]["Option31"] ="Okay."

tPowerChampionships_Match_Text[18663]["Text151"] = "You can change the halo to be Honor Halo of Battle Power Tournament which will last for 1 week. So, what do you say?"
tPowerChampionships_Match_Text[18663]["Option32"] = "Change~it~now!"
tPowerChampionships_Match_Text[18663]["Option33"] = "I~need~to~think~it~over."

--2005
tPowerChampionships_Match_Text["Text20051"] ="You`ve quit the Battle Power Tournament and returned to Twin City. If you want to resume, please sign up again with the BP Tournament Manager (295,141)."
tPowerChampionships_Match_Text["Text20052"] ="You`ve quit the Battle Power Tournament and returned to Twin City."
tPowerChampionships_Match_Text["Text20053"] ="Wrong map!"
tPowerChampionships_Match_Text["Text20054"] ="The Battle Power Tournament is about to kick off! Hurry and sign up with the BP Tournament Manager (295,141) in Twin City."
tPowerChampionships_Match_Text["Text20055"] ="Today`s Battle Power Tournament has ended."
tPowerChampionships_Match_Text["Text20056"] ="The Battle Power Tournament --- starts fighting!"
tPowerChampionships_Match_Text["Text20057"] ="Your inventory is full. Please make some room first, and login again to receive a BP Tournament Invitation."
tPowerChampionships_Match_Text["Text20058"] ="You received a BP Tournament Invitation. Hurry and check it in your inventory."
tPowerChampionships_Match_Text["Text20059"] ="You read the BP Tournament Invitation, and received 30 minutes of EXP!"
tPowerChampionships_Match_Text["Text200510"] ="You received 15 Study Points!"

tPowerChampionships_Match_Text["Text200511"] ="Congratulations! You won the championship in %s BP division! Here are your rewards: %s CPs, %s Chi Points, 1 week of honor halo, and a %s!"
tPowerChampionships_Match_Text["TextPower3950"] = "250-300"
tPowerChampionships_Match_Text["TextPower3951"] = "301-350"
tPowerChampionships_Match_Text["TextPower3952"] = "350+"

--背包信
tPowerChampionships_Match_Text["Letter"] ="Battle Power Tournament will be held at 20:00 - 21:00, on July 12th, July 19th, July 26th, and August 2nd. Tons of CPs are waiting for the winners! Talk to the BP Tournament Manager for more details."

--欧服
tPowerChampionships_Match_Text["Letter1"] ="Battle Power Tournament will be held at 22:00 - 23:00, on July 12th, July 19th, July 26th, and August 2nd. Tons of CPs are waiting for the winners! Talk to the BP Tournament Manager for more details."

tPowerChampionships_Match_Text["Option32"] ="Head~to~see~the~manager."
tPowerChampionships_Match_Text["Option33"] ="Abandon~after~reading."
tPowerChampionships_Match_Text["LetterOver"] ="The event has ended, and the invitation disappeared."
tPowerChampionships_Match_Text["LetterOver1"] = "I~see."

--Log
tPowerChampionships_Match_Text["BattleEffect"] = "Battle Power Tournament, Honor Halo of Battle Power Tournament"

------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]天机果活动
--Purpose:	天机果活动
--Creator: 	郑鋆
--Created:	2015/06/02
------------------------------------------------------------------------------------
-- 中文索引表
-- 调皮的蟠桃
tBackpackLetter_Text[3006281] = {}
tBackpackLetter_Text[3006281]["NoSpace"] = "Your inventory is too full to contain a Wise Peach. Please make some room first, and then login again."
tBackpackLetter_Text[3006281]["RewardItem"] = "A Wise Peach slid out of the Peach Garden, and dropped into your inventory."

tPeachaGardenActive_Text = {}
tPeachaGardenActive_Text[3006281] = {}
tPeachaGardenActive_Text[3006281]["111"] = "The Peach Garden in heaven is open to all Level 80 and above heroes from July 14th to August 8th."
tPeachaGardenActive_Text[3006281]["112"] = "~Go find the Young Peach Tree to enter the garden for a peach banquet. There are peerless peaches"
tPeachaGardenActive_Text[3006281]["113"] = "~and wonderful rewards waiting for you!"
tPeachaGardenActive_Text[3006281]["Option1"] = "Take~me~to~the~tree."
tPeachaGardenActive_Text[3006281]["Option2"] = "Discard~it~after~reading."

tPeachaGardenActive_Text[3006281]["BeOverdue"] = "The Peach Banquet has ended, and the Wise Peach is rotten."
tPeachaGardenActive_Text[3006281]["RewardExp"] = "You ate the Wise Peach, and received 30 minutes of EXP!"
tPeachaGardenActive_Text[3006281]["RewardCultivation"] = "You ate the Wise Peach, and received 15 Study Points!"

-- 蟠桃树（市场）
tPeachaGardenActive_Text[18619] = {}
tPeachaGardenActive_Text[18619]["111"] = "The immortal peach trees in heaven are having fruits to ripen. The Queen is going to feast"
tPeachaGardenActive_Text[18619]["112"] = "~all Level 80 and above heroes with the peaches from July 14th to August 8th. What`re you"
tPeachaGardenActive_Text[18619]["113"] = "~waiting for? Let`s enjoy the banquet with peerless peaches and various treasures!"
tPeachaGardenActive_Text[18619]["Option1"] = "I`m~looking~forward~to~it!"

-- 活动中主对白
tPeachaGardenActive_Text[18619]["121"] = "The Peach Garden in heaven is open to all Level 80 and above heroes from July 14th to August 8th."
tPeachaGardenActive_Text[18619]["122"] = "~What`re you waiting for? Let`s enjoy the banquet with super peaches and various treasures!"
tPeachaGardenActive_Text[18619]["123"] = ""
tPeachaGardenActive_Text[18619]["Option2"] = "Head~to~the~Peach~Garden."
tPeachaGardenActive_Text[18619]["Option3"] = "Tell~me~more."
tPeachaGardenActive_Text[18619]["Option4"] = "Sounds~wonderful."

-- 活动后
tPeachaGardenActive_Text[18619]["131"] = "The feast of peaches has ended, and the Peach Garden is closed. Join me in the next peach banquet! See you."
tPeachaGardenActive_Text[18619]["Option5"] = "See~you."

-- 玩家等级不足
tPeachaGardenActive_Text[18619]["211"] = "Sorry, the peaches are only for Level 80 and above heroes. Keep practicing, and come back when you`re qualified."
tPeachaGardenActive_Text[18619]["212"] = ""
tPeachaGardenActive_Text[18619]["Option6"] = "Alright."

-- 非活动时间
tPeachaGardenActive_Text[18619]["221"] = "The feast of peaches has ended, and the Peach Garden is closed."
tPeachaGardenActive_Text[18619]["Option7"] = "Alright."

-- 蟠桃游园会介绍
tPeachaGardenActive_Text[18619]["311"] = "After entering the garden, you need to drink the Power Liquor to boost your strength, making yourself able to pick peaches."
tPeachaGardenActive_Text[18619]["312"] = "~In each round, you can pick peaches for 5 times, each with different requirements. After that, just restart as many rounds as you like."
tPeachaGardenActive_Text[18619]["313"] = "~You know, you can collect the Power Liquor by helping the Peach Manager clear the garden, or participating in rebate events on the panel."
tPeachaGardenActive_Text[18619]["314"] = ""
tPeachaGardenActive_Text[18619]["Option8"] = "What~do~you~mean~requirements?"

tPeachaGardenActive_Text[18619]["321"] = "The more times you try, the more Power Liquor you will need, and also the better treasures you will receive. Wish you good luck to win a Golden Peach!"
tPeachaGardenActive_Text[18619]["322"] = "~Well, I`ll show you the required amount for each time:\n"
tPeachaGardenActive_Text[18619]["323"] = "The 1st Time: 1 bottle of Power Liquor;\n"
tPeachaGardenActive_Text[18619]["324"] = "The 2nd Time: 2 bottles of Power Liquor;\n"
tPeachaGardenActive_Text[18619]["325"] = "The 3rd Time: 3 bottles of Power Liquor;\n"
tPeachaGardenActive_Text[18619]["326"] = "The 4th Time: 5 bottles of Power Liquor;\n"
tPeachaGardenActive_Text[18619]["327"] = "The 5th Time: 6 bottles of Power Liquor.\n"
tPeachaGardenActive_Text[18619]["Option9"] = "I~see."

tPeachaGardenActive_Text[18619]["ChgMap"] = "You`re teleported to the beautiful Peach Garden in heaven. Hurry and go pick peaches!"
tPeachaGardenActive_Text[18619]["MsgBox"] = "You`ll receive free chances of picking peaches in the Peach Garden by helping the Peach Manager clear the garden."

-- 蟠桃大仙（蟠桃乐园）
-- 活动中主对白
tPeachaGardenActive_Text[18484] = {}
tPeachaGardenActive_Text[18484]["111"] = "The immortal peach trees are having juicy fruits. You`re welcome to enjoy a feast of peaches and treasures!"
tPeachaGardenActive_Text[18484]["112"] = "~Wish you good luck here, to win fabulous items like the Peerless Peaches and the Golden Peaches!"
tPeachaGardenActive_Text[18484]["Option1"] = "I~want~the~Power~Liquor."
tPeachaGardenActive_Text[18484]["Option2"] = "Send~me~to~Twin~City."
tPeachaGardenActive_Text[18484]["Option3"] = "Tell~me~more."

-- 活动后
tPeachaGardenActive_Text[18484]["121"] = "The Peach Garden is closed. Hope you to see you again next year."
tPeachaGardenActive_Text[18484]["Option5"] = "See~you."

-- 拜求大力神酒
tPeachaGardenActive_Text[18484]["211"] = "What? I`ve given you some Power Liquor today. What a bad memory you have."
tPeachaGardenActive_Text[18484]["Option6"] = "Oh,~sorry."

-- 交任务、失败、未完成任务
tPeachaGardenActive_Text[18484]["221"] = "You need to uproot 10 Cloud Weeds, or kill 10 Weed Bugs, to win a bottle Power Liquor."
tPeachaGardenActive_Text[18484]["Option7"] = "I~see."

-- 交任务、成功
tPeachaGardenActive_Text[18484]["231"] = "Good job! Here is a bottle of Power Liquor for you."
tPeachaGardenActive_Text[18484]["Option8"] = "Thanks!"

tPeachaGardenActive_Text[18484]["311"] = "While in the garden, you can drink the Power Liquor to boost your strength, so you`re able to pick peaches."
tPeachaGardenActive_Text[18484]["312"] = "~In each round, you can pick peaches for 5 times, each with different requirements. After that, just restart as many rounds as you like."
tPeachaGardenActive_Text[18484]["313"] = "~You know, you can collect the Power Liquor by helping me clear the garden, or participating in rebate events on the panel."
tPeachaGardenActive_Text[18484]["314"] = ""
tPeachaGardenActive_Text[18484]["Option9"] = "What~requirements?"

tPeachaGardenActive_Text[18484]["411"] = "Thanks for helping me clean the Peach Garden. Here are %d bottles of Power Liquor and 1 piece of Peach Peel for you!\n"
tPeachaGardenActive_Text[18484]["412"] = "(You can refresh to get up to 3 bottles of Power Liquor)"
tPeachaGardenActive_Text[18484]["Option11"] = "Claim~rewards."
tPeachaGardenActive_Text[18484]["Option12"] = "Refresh~Power~Liquor.~(3~CPs)"
tPeachaGardenActive_Text[18484]["RewardItem"] = "You received %d bottles of Power Liquor and 1 piece of Peach Peel."

tPeachaGardenActive_Text[18484]["ChgMap"] = "You were teleported to Twin City."
tPeachaGardenActive_Text[18484]["NoSpace"] = "Your inventory is full. You need to clear at least 1 empty space, first."
tPeachaGardenActive_Text[18484]["NoEmoney"] = "You don`t have 3 CPs."

-- 蟠桃树
tPeachaGardenActive_Text[18599] = {}
-- 活动前
tPeachaGardenActive_Text[18599]["111"] = "The peach tree put forth leaves once every thousand years, and it requires more years for the fruit to ripen. Please wait..."
tPeachaGardenActive_Text[18599]["Option1"] = "I~can`t~wait~to~taste!"

-- 活动中主对白
tPeachaGardenActive_Text[18599]["121"] = "                          Peach Status\n"
tPeachaGardenActive_Text[18599]["122"] = "------------------------------------------------------------\n\n"

-- 翻译的时候注意，下面这几个一行一行翻译，不要跟其它的混合。
tPeachaGardenActive_Text[18599]["123"] = "Peach Quality: [*] 1-Star\n"
tPeachaGardenActive_Text[18599]["124"] = "Reward: have a chance to receive a Small Peach which is a material of making attribute tokens.\n\n"

tPeachaGardenActive_Text[18599]["125"] = "Peach Quality: [**] 2-Star\n"
tPeachaGardenActive_Text[18599]["126"] = "Reward: have a chance to receive many Small Peaches, and also a shoot to win an attribute re-allocator.\n\n"

tPeachaGardenActive_Text[18599]["127"] = "Peach Quality: [***] 3-Star\n"
tPeachaGardenActive_Text[18599]["128"] = "Reward: have a chance to receive a large number of Small Peaches and attribute re-allocators.\n\n"

tPeachaGardenActive_Text[18599]["129"] = "Peach Quality: [****] 4-Star\n"
tPeachaGardenActive_Text[18599]["1210"] = "Reward: have a chance to receive a legendary Mystery Fruit.\n\n"

tPeachaGardenActive_Text[18599]["1211"] = "Peach Quality: [*****] 5-Star\n"
tPeachaGardenActive_Text[18599]["1212"] = "Reward: have a chance to receive the Peerless Peach which can dramatically increase your attribute.\n\n"

tPeachaGardenActive_Text[18599]["Option3"] = "Pick~peaches~(%d*PowerLiquor)."
tPeachaGardenActive_Text[18599]["Option4"] = "Pick~the~Golden~Peach."
tPeachaGardenActive_Text[18599]["Option18"] = "Better~the~reward~of~picking."

-- 活动后
tPeachaGardenActive_Text[18599]["131"] = "The peach tree is prosperous, however, all the peaches have been picked."
tPeachaGardenActive_Text[18599]["Option6"] = "Oh,~no!"

-- 玩家选1、失败、非活动时间
tPeachaGardenActive_Text[18599]["211"] = "Sorry, you can`t pick peaches now since it`s not the right time."
tPeachaGardenActive_Text[18599]["Option7"] = "Okay."

-- 玩家选1、失败、背包满
tPeachaGardenActive_Text[18599]["221"] = "Your inventory is full. Please make some room, first."
tPeachaGardenActive_Text[18599]["Option8"] = "I`ll~do~it~now."

-- 玩家选2、失败、玩家身上无此物品
tPeachaGardenActive_Text[18599]["311"] = "You can`t pick peaches without the Power Liquor! If you help the Peach Manager clear the garden, he`ll reward you some."
tPeachaGardenActive_Text[18599]["Option12"] = "Okay."

-- 玩家选2、二次确认扣除大力神酒
tPeachaGardenActive_Text[18599]["321"] = "This is your Time %d to pick peaches, this round. Are you sure you want to consume %d bottle(s) of Power Liquor for the job?"
tPeachaGardenActive_Text[18599]["Option13"] = "Yes."
tPeachaGardenActive_Text[18599]["Option30"] = "Always~`YES`.~Do~not~remind."
tPeachaGardenActive_Text[18599]["Option14"] = "I~haven`t~decided~it,~yet."

-- 玩家第五次采摘蟠桃王单独对白
tPeachaGardenActive_Text[18599]["411"] = "Open your eyes! A Golden Peach is in front of you! It may disappear at any time."
tPeachaGardenActive_Text[18599]["412"] = "~Hurry and pick it!"
tPeachaGardenActive_Text[18599]["413"] = "~(If you close it, you`ll lose a chance to pick the Golden Peach.)"
tPeachaGardenActive_Text[18599]["414"] = ""
tPeachaGardenActive_Text[18599]["Option15"] = "Let~me~try!"

-- 玩家选3、采摘失败
tPeachaGardenActive_Text[18599]["421"] = "The Golden Peach disappeared, and you failed to capture it."
tPeachaGardenActive_Text[18599]["Option16"] = "What~a~pity."

-- 玩家选3、采摘成功
tPeachaGardenActive_Text[18599]["431"] = "Wow! You got sharp eyes! The Golden Peach belongs to you, now."
tPeachaGardenActive_Text[18599]["Option17"] = "Great!"

tPeachaGardenActive_Text[18599]["521"] = "You`re going to pick peaches at the 4th time, this round. Are you sure you want to consume 5 bottles of Power Liquor and a Double Joy Ticket for the job?"
tPeachaGardenActive_Text[18599]["Option22"] = "Yes!"
tPeachaGardenActive_Text[18599]["Option23"] = "Let~me~think~about~it."

tPeachaGardenActive_Text[18599]["541"] = "You`re going to pick peaches at the 5th time, this round. Are you sure you want to consume 6 bottles of Power Liquor and a Double Surprise Ticket for the job?"
tPeachaGardenActive_Text[18599]["Option26"] = "Yes!"
tPeachaGardenActive_Text[18599]["Option27"] = "Let~me~think~about~it."

tPeachaGardenActive_Text[18599]["611"] = "After cancelling the confirmation, you`ll not receive the pop-up reminder while picking peaches. Have you decided?"
tPeachaGardenActive_Text[18599]["Option28"] = "Yes."
tPeachaGardenActive_Text[18599]["Option29"] = "No,~I~changed~my~mind."

tPeachaGardenActive_Text[18599]["RewardItem"] = "You ate the peach, and received %s!"
tPeachaGardenActive_Text[18599]["NoItem"] = "Make sure you have the %s to pick peaches."

tPeachaGardenActive_Text["Weed"] = "Uprooting"
tPeachaGardenActive_Text["WeedSuccess"] = "You successfully uprooted a weed! You still need to uproot %d Cloud Weeds or Weed Bugs to claim a bottle of Power Liquor from the Peach Manager."
tPeachaGardenActive_Text["KillMonster"] = "You successfully killed a Weed Bug! You still need to uproot %d Cloud Weeds or Weed Bugs to claim a bottle of Power Liquor from the Peach Manager."
tPeachaGardenActive_Text["Demand"] = "You`ve finished clearing the Peach Garden. Go and claim a bottle of Power Liquor from the Peach Manager."

-- 洗髓神露碎片
tPeachaGardenActive_Text[3006284] = {}
tPeachaGardenActive_Text[3006284]["NoItem"] = "Failed to combine. You should have at least 10 Mystery Dew Scraps to combine."
tPeachaGardenActive_Text[3006284]["Success"] = "You successfully combined the scraps into a bottle of Mystery Dew!"

-- 小仙桃
tPeachaGardenActive_Text[3006286] = {}
tPeachaGardenActive_Text[3006286]["One"] = "You ate the Small Peach, and received a(n) %s!"
tPeachaGardenActive_Text[3006286]["Two"] = "You ate the Small Peach, and received 2 %s!"

-- 仙桃壳
tPeachaGardenActive_Text[3006287] = {}
tPeachaGardenActive_Text[3006287]["NoItem"] = "Failed to combine. You should have at least 2 pieces of Peach Peel to combine."
tPeachaGardenActive_Text[3006287]["Success"] = "You successfully combined the pieces into a %s!"

-- 力量洗点丹
tPeachaGardenActive_Text[3006288] = {}
tPeachaGardenActive_Text[3006288]["NoItem"] = "You don`t have any points of Strength to re-allot. Please check it again!"
tPeachaGardenActive_Text[3006288]["Success"] = "You successfully made 1 point of Strength allocatable!"
tPeachaGardenActive_Text[3006288]["111"] = "Are you sure you want to make 1 point of Strength allocatable?"
tPeachaGardenActive_Text[3006288]["Option1"] = "Yes."

-- 敏捷洗点丹
tPeachaGardenActive_Text[3006289] = {}
tPeachaGardenActive_Text[3006289]["NoItem"] = "You don`t have any points of Agility to re-allot. Please check it again!"
tPeachaGardenActive_Text[3006289]["Success"] = "You successfully made 1 point of Agility allocatable!"
tPeachaGardenActive_Text[3006289]["111"] = "Are you sure you want to make 1 point of Agility allocatable?"
tPeachaGardenActive_Text[3006289]["Option1"] = "Yes."

-- 体质洗点丹
tPeachaGardenActive_Text[3006322] = {}
tPeachaGardenActive_Text[3006322]["NoItem"] = "You don`t have any points of Vitality to re-allot. Please check it again!"
tPeachaGardenActive_Text[3006322]["Success"] = "You successfully made 1 point of Vitality allocatable!"
tPeachaGardenActive_Text[3006322]["111"] = "Are you sure you want to make 1 point of Vitality allocatable?"
tPeachaGardenActive_Text[3006322]["Option1"] = "Yes."

-- 精神洗点丹
tPeachaGardenActive_Text[3006323] = {}
tPeachaGardenActive_Text[3006323]["NoItem"] = "You don`t have any points of Spirit to re-allot. Please check it again!"
tPeachaGardenActive_Text[3006323]["Success"] = "You successfully made 1 point of Spirit allocatable!"
tPeachaGardenActive_Text[3006323]["111"] = "Are you sure you want to make 1 point of Spirit allocatable?"
tPeachaGardenActive_Text[3006323]["Option1"] = "Yes."

-- 力量秘令转化卡
tPeachaGardenActive_Text[3006285] = {}
tPeachaGardenActive_Text[3006285]["111"] = "Which attribute token would you like to convert the [Strength Token] into?"
tPeachaGardenActive_Text[3006285]["Option1"] = "Spirit~Token."
tPeachaGardenActive_Text[3006285]["Option2"] = "Vitality~Token."
tPeachaGardenActive_Text[3006285]["Option3"] = "Agility~Token."

tPeachaGardenActive_Text[3006285]["211"] = "Are you sure you want to convert the [Strength Token] into a [Spirit Token]?"
tPeachaGardenActive_Text[3006285]["Option4"] = "Yes."
tPeachaGardenActive_Text[3006285]["Option5"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006285]["221"] = "Are you sure you want to convert the [Strength Token] into a [Vitality Token]?"
tPeachaGardenActive_Text[3006285]["Option6"] = "Yes."
tPeachaGardenActive_Text[3006285]["Option7"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006285]["231"] = "Are you sure you want to convert the [Strength Token] into an [Agility Token]?"
tPeachaGardenActive_Text[3006285]["Option8"] = "Yes."
tPeachaGardenActive_Text[3006285]["Option9"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006285]["NoItem"] = "Where is your Strength Token Converter? You should have one to continue."
tPeachaGardenActive_Text[3006285]["NoReqItem"] = "Where is your Strength Token? You should have one to continue."

-- 敏捷秘令转化卡
tPeachaGardenActive_Text[3006798] = {}
tPeachaGardenActive_Text[3006798]["111"] = "Which attribute token would you like to convert the [Agility Token] into?"
tPeachaGardenActive_Text[3006798]["Option1"] = "Spirit~Token."
tPeachaGardenActive_Text[3006798]["Option2"] = "Vitality~Token."
tPeachaGardenActive_Text[3006798]["Option3"] = "Strength~Token."

tPeachaGardenActive_Text[3006798]["211"] = "Are you sure you want to convert the [Agility Token] into a [Spirit Token]?"
tPeachaGardenActive_Text[3006798]["Option4"] = "Yes."
tPeachaGardenActive_Text[3006798]["Option5"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006798]["221"] = "Are you sure you want to convert the [Agility Token] into a [Vitality Token]?"
tPeachaGardenActive_Text[3006798]["Option6"] = "Yes."
tPeachaGardenActive_Text[3006798]["Option7"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006798]["231"] = "Are you sure you want to convert the [Agility Token] into a [Strength Token]?"
tPeachaGardenActive_Text[3006798]["Option8"] = "Yes."
tPeachaGardenActive_Text[3006798]["Option9"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006798]["NoItem"] = "Where is your Agility Token Converter? You should have one to continue."
tPeachaGardenActive_Text[3006798]["NoReqItem"] = "Where is your Agility Token? You should have one to continue."

-- 体质秘令转化卡
tPeachaGardenActive_Text[3006799] = {}
tPeachaGardenActive_Text[3006799]["111"] = "Which attribute token would you like to convert the [Vitality Token] into?"
tPeachaGardenActive_Text[3006799]["Option1"] = "Strength~Token."
tPeachaGardenActive_Text[3006799]["Option2"] = "Spirit~Token."
tPeachaGardenActive_Text[3006799]["Option3"] = "Agility~Token."

tPeachaGardenActive_Text[3006799]["211"] = "Are you sure you want to convert the [Vitality Token] into a [Strength Token]?"
tPeachaGardenActive_Text[3006799]["Option4"] = "Yes."
tPeachaGardenActive_Text[3006799]["Option5"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006799]["221"] = "Are you sure you want to convert the [Vitality Token] into a [Spirit Token]?"
tPeachaGardenActive_Text[3006799]["Option6"] = "Yes."
tPeachaGardenActive_Text[3006799]["Option7"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006799]["231"] = "Are you sure you want to convert the [Vitality Token] into an [Agility Token]?"
tPeachaGardenActive_Text[3006799]["Option8"] = "Yes."
tPeachaGardenActive_Text[3006799]["Option9"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006799]["NoItem"] = "Where is your Vitality Token Converter? You should have one to continue."
tPeachaGardenActive_Text[3006799]["NoReqItem"] = "Where is your Vitality Token? You should have one to continue."

-- 精神秘令转化卡
tPeachaGardenActive_Text[3006800] = {}
tPeachaGardenActive_Text[3006800]["111"] = "Which attribute token would you like to convert the [Spirit Token] into?"
tPeachaGardenActive_Text[3006800]["Option1"] = "Strength~Token."
tPeachaGardenActive_Text[3006800]["Option2"] = "Vitality~Token."
tPeachaGardenActive_Text[3006800]["Option3"] = "Agility~Token."

tPeachaGardenActive_Text[3006800]["211"] = "Are you sure you want to convert the [Spirit Token] into a [Strength Token]?"
tPeachaGardenActive_Text[3006800]["Option4"] = "Yes."
tPeachaGardenActive_Text[3006800]["Option5"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006800]["221"] = "Are you sure you want to convert the [Spirit Token] into a [Vitality Token]?"
tPeachaGardenActive_Text[3006800]["Option6"] = "Yes."
tPeachaGardenActive_Text[3006800]["Option7"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006800]["231"] = "Are you sure you want to convert the [Spirit Token] into an [Agility Token]?"
tPeachaGardenActive_Text[3006800]["Option8"] = "Yes."
tPeachaGardenActive_Text[3006800]["Option9"] = "I~need~to~think~it~over."

tPeachaGardenActive_Text[3006800]["NoItem"] = "Where is your Spirit Token Converter? You should have one to continue."
tPeachaGardenActive_Text[3006800]["NoReqItem"] = "Where is your Spirit Token? You should have one to continue."

-- 双拼大力神酒（2杯）
tPeachaGardenActive_Text[3006802] = {}
tPeachaGardenActive_Text[3006802]["Success"] = "You received 2 cups of Power Liquor!"

-- 特饮大力神酒（4杯）
tPeachaGardenActive_Text[3006803] = {}
tPeachaGardenActive_Text[3006803]["Success"] = "You received 4 cups of Power Liquor!"

-- 豪华大力神酒（6杯）
tPeachaGardenActive_Text[3006804] = {}
tPeachaGardenActive_Text[3006804]["Success"] = "You received 6 cups of Power Liquor!"

-- 绝世蟠桃
tPeachaGardenActive_Text[3006744] = {}
tPeachaGardenActive_Text[3006744]["Success"] = "You ate the Peerless Peach, and received %d Attribute Points!"
tPeachaGardenActive_Text[3006744]["111"] = "You want to eat the Peerless Peach directly? If you eat it together with the Peerless Peach Stimulant, the energy of the peach will be enhanced by 10 times."
tPeachaGardenActive_Text[3006744]["Option1"] = "Eat~it~directly."
tPeachaGardenActive_Text[3006744]["Option2"] = "Eat~it~with~CPs~&~Stimulant."

tPeachaGardenActive_Text[3006744]["121"] = "The Peerless Peach will bring you 50 Attribute Points if you eat it together with a Peerless Peach Stimulant and 20,000 CPs. So, continue?"
tPeachaGardenActive_Text[3006744]["Option3"] = "Yes!"
tPeachaGardenActive_Text[3006744]["Option4"] = "No,~I~changed~my~mind."

tPeachaGardenActive_Text[3006744]["NotItem"] = "Make sure you have a Peerless Peach with you, first."
tPeachaGardenActive_Text[3006744]["NotPowerfulItem"] = "Make sure you have a Peerless Peach and a Peerless Peach Stimulant with you, first."

-- 蟠桃王
tPeachaGardenActive_Text[3006282] = {}
tPeachaGardenActive_Text[3006282]["Success"] = "You ate the Golden Peach, and received %d points of Vatality!"

tPeachaGardenActive_Text["Strength"] = "You successfully converted the token into a Strength Token."
tPeachaGardenActive_Text["Soul"] = "You successfully converted the token into a Spirit Token."
tPeachaGardenActive_Text["Health"] = "You successfully converted the token into a Vitality Token."
tPeachaGardenActive_Text["Speed"] = "You successfully converted the token into a Spirit Token."
tPeachaGardenActive_Text["NoSpace"] = "Your inventory is full. Please make some room, first."
tPeachaGardenActive_Text["BeOverdue"] = "The Peach Banquet has ended, and the magic of this item vanished."

tPeachaGardenActive_Text["RewardItem"] = "You picked %s on the Peach Tree."
tPeachaGardenActive_Text["GetItem"] = "%s %s(s)"
tPeachaGardenActive_Text["Symbol"] = ","
tPeachaGardenActive_Text["Broadcast"] = "Congratulations! %s received %s in the Peach Banquet. Want to perfect your attribute? Talk to the Young Peach Tree (321,267) for more details."
tPeachaGardenActive_Text["NoEmoney"] = "You don`t have 20,000 CPs to activate the Peerless Peach Stimulant."

tPeachaGardenActive_Text["NoMetempsychosis"] = "Please use it after the 1st rebirth."
tPeachaGardenActive_Text["NotAttr"] = "You have a maximum amount of attribute points, and you can`t get more. After the event, you can exchange them for Chi Points with the Peach Manager."
tPeachaGardenActive_Text["NoWeed"] = "You`ve helped the Peach Manager clear the garden, today.  If you`re still interested, please come tomorrow."

-- 桃园仙女
tPeachaGardenActive_Text[18601] = {}
tPeachaGardenActive_Text[18601]["111"] = "My hero, welcome to the Peach Garden! Do you have "
tPeachaGardenActive_Text[18601]["112"] = "the Power Liquor? If yes, drink it to pick the "
tPeachaGardenActive_Text[18601]["113"] = "peaches for Attribute Points. If no, I suggest you "
tPeachaGardenActive_Text[18601]["114"] = "to collect some by helping the Peach Manager, or "
tPeachaGardenActive_Text[18601]["115"] = "buy some with CPs from Sun Wukong."
tPeachaGardenActive_Text[18601]["Option1"] = "Got~it!"



-- 神机道长
tPeachaGardenActive_Text[18767] = {}
tPeachaGardenActive_Text[18767]["111"] = "Your Attribute Points haven`t reached the maximum. Don`t waste your Mystery Fruit for Chi Points."
tPeachaGardenActive_Text[18767]["Option1"] = "Okay."

tPeachaGardenActive_Text[18767]["121"] = "From August 1st to 12th, I`ll be here collecting the items you received"
tPeachaGardenActive_Text[18767]["122"] = "~from the Peach Banquet. You can exchange Mystery Fruit, Peerless Peach,"
tPeachaGardenActive_Text[18767]["123"] = "~Golden Peach, Double Joy Ticket or Double Surprise Ticket for Chi Points with me."
tPeachaGardenActive_Text[18767]["Option2"] = "Mystery~Fruit=>1000~Chi~Pts."
tPeachaGardenActive_Text[18767]["Option3"] = "Peerless~Peach=>2000~Chi~Pts."
tPeachaGardenActive_Text[18767]["Option4"] = "Golden~Peach=>5000~Chi~Pts."
tPeachaGardenActive_Text[18767]["Option5"] = "Joy~Ticket=>500~Chi~Pts."
tPeachaGardenActive_Text[18767]["Option6"] = "Surprise~Ticket=>650~Chi~Pts."

tPeachaGardenActive_Text[18767]["131"] = "From August 1st to 12th, I`ll be here collecting the items you received"
tPeachaGardenActive_Text[18767]["132"] = "~from the Peach Banquet. You can exchange Mystery Fruit, Peerless Peach,"
tPeachaGardenActive_Text[18767]["133"] = "~Golden Peach, Double Joy Ticket or Double Surprise Ticket for Chi Points with me."
tPeachaGardenActive_Text[18767]["Option9"] = "Good."

tPeachaGardenActive_Text[18767]["211"] = "Are you sure you want to exchange %s for %d Chi Points?"
tPeachaGardenActive_Text[18767]["Option7"] = "Yes."
tPeachaGardenActive_Text[18767]["Option8"] = "No."

tPeachaGardenActive_Text[18767]["Success"] = "You received %d Chi Points!"
tPeachaGardenActive_Text[18767]["NoItem"] = "Make sure you have the item with you, first!"



------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]卡牌月活动制作
--Purpose:	卡牌月活动制作
--Creator: 	严振飞
--Created:	2015/05/28
------------------------------------------------------------------------------------
----------------------------------NPC对白--------------------------------------
tFestivalCardMonth_Text = {}

-- // ==奥林匹斯神使== \\
tFestivalCardMonth_Text[18577] = {}
--活动前对白
tFestivalCardMonth_Text[18577]["Text111"] = "Mount Olympus has sank, and the whole world fell into a chaos. The Twelve Olympians are suffering their darkest days in prison."
tFestivalCardMonth_Text[18577]["Text112"] = "~Are you the `CHOSEN HERO` to free the holy land from evils? Come and prove yourself from August 4th to August 17th!"
tFestivalCardMonth_Text[18577]["Option11"] = "It`s~me!"

--活动后对白
tFestivalCardMonth_Text[18577]["Text121"] = "With the help of the chosen heroes, the deities finally retrieved peace and glory on Mount Olympus."
tFestivalCardMonth_Text[18577]["Option12"] = "Great!"

--活动中对白
tFestivalCardMonth_Text[18577]["Text131"] = "The Twelve Olympians were trapped by evils, suffering their darkest days in prison. Heroes above Level 120 or reborn"
tFestivalCardMonth_Text[18577]["Text132"] = "~are chosen to help these great deities from August 4th to August 17th. To challenge the evils, you`ll need the God cards collected by"
tFestivalCardMonth_Text[18577]["Text133"] = "~using Chi Token or hunting BOSS. Additonally, hunting monsters in the wild may bring you Holy Item cards to upgrade the God cards."
tFestivalCardMonth_Text[18577]["Option13"] = "Enter~Mount~Olympus."
tFestivalCardMonth_Text[18577]["Option14"] = "Return~to~Twin~City."
tFestivalCardMonth_Text[18577]["Option15"] = "View~my~God~Points."
tFestivalCardMonth_Text[18577]["Option16"] = "Buy~card~packs."
tFestivalCardMonth_Text[18577]["Option17"] = "Reclaim~God~cards."
tFestivalCardMonth_Text[18577]["Option18"] = "Tell~me~more."
tFestivalCardMonth_Text[18577]["Option19"] = "I`ll~think~about~it."

--【进入奥林匹斯山.】
-- 等级不足
tFestivalCardMonth_Text[18577]["Text211"] = "Mount Olympus has been occupied by demons. It`s too dangerous. You should reach at least Level 120 or get reborn to challenge."
tFestivalCardMonth_Text[18577]["Option21"] = "Alright."
-- 成功进入
tFestivalCardMonth_Text[18577]["ChgMap"] = "You`ve entered Mount Olympus. Danger is everywhere. Take care of yourself!"

-- 【查询当前活动积分.】
tFestivalCardMonth_Text[18577]["Text311"] = "You`ve earned %d God Points. Upgrading God cards with Holy Item cards, defeating demons on Mount Olympus,"
tFestivalCardMonth_Text[18577]["Text312"] = "~and discovering the ultimate treasure of Olympus will bring you tons of God Points. When the crisis is resolved, you`ll be rewarded according to your total God Points."
tFestivalCardMonth_Text[18577]["Option31"] = "View~the~ranking~and~rewards."
tFestivalCardMonth_Text[18577]["Option32"] = "Learn~about~something~else."
tFestivalCardMonth_Text[18577]["Option33"] = "I~see."

-- 【购买幸运礼包.】
-- 初始对白
tFestivalCardMonth_Text[18577]["Text411"] = "There are two packs for sale. Lucky Card Pack randomly gives a God pack, God`s spirit fragment, Holy Item card, or Holy Item Fragment."
tFestivalCardMonth_Text[18577]["Text412"] = "~For the Blessing Card Pack, you can open it 10 times to win a God pack, God`s spirit fragment, Holy Item card, or Holy Item Fragment each time."
tFestivalCardMonth_Text[18577]["Text413"] = "~So, which pack do you want to buy?"
tFestivalCardMonth_Text[18577]["Option41"] = "Lucky~Card~Pack.~(27~CPs)"
tFestivalCardMonth_Text[18577]["Option42"] = "Blessing~Card~Pack.~(270~CPs)"
tFestivalCardMonth_Text[18577]["Option43"] = "Learn~about~other~things."
tFestivalCardMonth_Text[18577]["Option44"] = "I`ll~talk~to~you~later."
-- 修为不足
tFestivalCardMonth_Text[18577]["Text421"] = "Sorry, the pack is only for heroes who`ve reached Level 120 or got reborn."
tFestivalCardMonth_Text[18577]["Option45"] = "Alright."
-- 天石不足
tFestivalCardMonth_Text[18577]["Text431"] = "You don`t have enough CPs to buy this pack. Make sure you bring enough money, next time."
-- 背包空间不足
tFestivalCardMonth_Text[18577]["Text441"] = "Your inventory is full. Why not tidy it up, first?"
-- 二次确认
tFestivalCardMonth_Text[18577]["Text451"] = "Are you sure you want to pay %d for the %s?"
tFestivalCardMonth_Text[18577]["Option46"] = "Yes."
tFestivalCardMonth_Text[18577]["Option47"] = "No,~I~changed~my~mind."


-- 【补领主神卡.】
-- 初始对白第一页
tFestivalCardMonth_Text[18577]["Text511"] = "Did you lose a God card? Look, you have 3 free chances to reclaim it. When you run out of the chances,"
tFestivalCardMonth_Text[18577]["Text512"] = "~you need to pay 99 CPs to retrieve it. Now, tell me which God card you need?"
tFestivalCardMonth_Text[18577]["Text513"] = ""
tFestivalCardMonth_Text[18577]["Option511"] = "Zeus."
tFestivalCardMonth_Text[18577]["Option512"] = "Hera."
tFestivalCardMonth_Text[18577]["Option513"] = "Poseidon."
tFestivalCardMonth_Text[18577]["Option514"] = "Demeter."
tFestivalCardMonth_Text[18577]["Option515"] = "Athena."
tFestivalCardMonth_Text[18577]["Option516"] = "Apollo."
tFestivalCardMonth_Text[18577]["Option517"] = "Next."
tFestivalCardMonth_Text[18577]["Option518"] = "I`ll~talk~to~you~later."
-- 初始对白第二页
tFestivalCardMonth_Text[18577]["Option521"] = "Artemis."
tFestivalCardMonth_Text[18577]["Option522"] = "Aphrodite."
tFestivalCardMonth_Text[18577]["Option523"] = "Hermes."
tFestivalCardMonth_Text[18577]["Option524"] = "Ares."
tFestivalCardMonth_Text[18577]["Option525"] = "Hephaistos."
tFestivalCardMonth_Text[18577]["Option526"] = "Dionysus."
tFestivalCardMonth_Text[18577]["Option527"] = "Previous."
tFestivalCardMonth_Text[18577]["Option528"] = "I`ll~talk~to~you~later."
-- 等级不足
tFestivalCardMonth_Text[18577]["Text531"] = "Sorry, you should reach at least Level 120 or get reborn to claim the God cards."
tFestivalCardMonth_Text[18577]["Option531"] = "Okay."
-- 背包空间不足
tFestivalCardMonth_Text[18577]["Text541"] = "Your inventory is too full to contain anything. Please make some room, first."
tFestivalCardMonth_Text[18577]["Option532"] = "I`ll~do~it~now."
-- 未拥有
tFestivalCardMonth_Text[18577]["Text551"] = "You have never had this God card before. What do you mean `reclaim`? Sorry, I say NO."
-- 包裹内已有
tFestivalCardMonth_Text[18577]["Text561"] = "Are you serious? This God card is currently in your inventory. Please check it again."
-- 补领超过三次天石不足
tFestivalCardMonth_Text[18577]["Text571"] = "You don`t have enough CPs to reclaim the God card."
-- 免费补领二次确认
tFestivalCardMonth_Text[18577]["Text581"] = "Are you sure you want to reclaim 1 %s?"
tFestivalCardMonth_Text[18577]["Option581"] = "Yes."
tFestivalCardMonth_Text[18577]["Option582"] = "I~haven`t~decided,~yet!"
-- 天石补领二次确认
tFestivalCardMonth_Text[18577]["Text591"] = "Are you sure you want to pay 99 CPs to reclaim 1 %s?"


-- 【了解活动详情.】
tFestivalCardMonth_Text[18577]["Text611"] = "The demons invaded Mount Olympus and threw the Twelve Olympians into prison. If you have a God card,"
tFestivalCardMonth_Text[18577]["Text612"] = "~you can challenge corresponding demon, and get a chance to win corresponding treasure. The first hero"
tFestivalCardMonth_Text[18577]["Text613"] = "~who defeats a demon will receive the best reward. When you clear all the demons, you can start hunting the mythical Olympus Treasure."
tFestivalCardMonth_Text[18577]["Option60"] = "How~to~collect~God~cards?"
tFestivalCardMonth_Text[18577]["Option61"] = "How~to~upgrade~God~cards?"
tFestivalCardMonth_Text[18577]["Option62"] = "How~to~collect~Holy~Item?"
tFestivalCardMonth_Text[18577]["Option63"] = "How~to~make~Holy~Item?"
tFestivalCardMonth_Text[18577]["Option64"] = "How~to~challenge~the~demons?"
tFestivalCardMonth_Text[18577]["Option65"] = "Olympus~Treasure?"
tFestivalCardMonth_Text[18577]["Option66"] = "Learn~about~other~things."
tFestivalCardMonth_Text[18577]["Option67"] = "I~see."
--主神卡如何获得？
tFestivalCardMonth_Text[18577]["Text621"] = "When you read Olympians Oracle, you`ll receive a random God card. You can also use the Chi Token,"
tFestivalCardMonth_Text[18577]["Text622"] = "~to win a random God pack. Besides, killing BOSS may bring you random God`s spirit fragments. Every"
tFestivalCardMonth_Text[18577]["Text623"] = "~10 fragments of the same type can be combined into a God pack. If you have the God card when you open a God pack, you`ll receive 3 corresponding Holy Item cards instead."
tFestivalCardMonth_Text[18577]["Option68"] = "Learn~about~other~things."
tFestivalCardMonth_Text[18577]["Option69"] = "Got~it."
-- 主神卡如何升级？
tFestivalCardMonth_Text[18577]["Text631"] = "There are 3 Holy Item cards for each Olympian to upgrade. You should have all the 3 Holy Item cards to complete the upgrading."
tFestivalCardMonth_Text[18577]["Text632"] = "~After the upgrading, the Olympian will have better attack, dealing more damage on the demon."
tFestivalCardMonth_Text[18577]["Text633"] = ""
-- 圣物卡如何获得？
tFestivalCardMonth_Text[18577]["Text641"] = "When you open the Active Packs, you may receive a random Holy Item card. You can also go hunting BOSS to collect the Holy Item fragments."
tFestivalCardMonth_Text[18577]["Text642"] = "~Besides, a Holy Item card can be split into 3 Holy Item Fragments and a free chance to make a Holy Item card."
-- 圣物卡如何打造？
tFestivalCardMonth_Text[18577]["Text651"] = "You can combine 10 Holy Item Fragments into a random Holy Item card, or 30 pieces into a specific Holy Item card. You`ll obtain 10 free chances"
tFestivalCardMonth_Text[18577]["Text652"] = "~of making cards, every day. The chance can be accumulated. So, what if you run out of the chances? Just pay 27 CPs, each time."
tFestivalCardMonth_Text[18577]["Text653"] = ""
-- 了解恶魔挑战.
tFestivalCardMonth_Text[18577]["Text661"] = "The challenge is divided into 3 stages. Every day, you have 10 free chances to challenge. When you have no free chances left,"
tFestivalCardMonth_Text[18577]["Text662"] = "~you need to pay 27 CPs, each time. If you continue the challenge after completing all the stages, you`ll win"
tFestivalCardMonth_Text[18577]["Text663"] = "~a corresponding God card or Holy Item card."
-- 了解奥林匹斯的宝藏.
tFestivalCardMonth_Text[18577]["Text671"] = "Try your best to finish all the demon challenges, so you can claim a great reward. The early you claim, the better the reward."
tFestivalCardMonth_Text[18577]["Text672"] = "~Additionally, you`re allowed to enter the Heart of Olympus for the mythical treasure. When you discover random 8 treasures,"
tFestivalCardMonth_Text[18577]["Text673"] = "~your treasure hunting ends and you`ll be teleported out."


--=======================================
-- 成功返回双龙城
tFestivalCardMonth_Text[18591] = {}
tFestivalCardMonth_Text[18591]["ChgMap"] = "You`ve returned to Twin City."


-- // ==奥林匹斯的宝藏== \\
tFestivalCardMonth_Text[18578] = {}
-- 活动时间外
tFestivalCardMonth_Text[18578]["Text111"] = "The crisis has been resolved, and you were teleported out of Mount Olympus."
tFestivalCardMonth_Text[18578]["Option11"] = "I~see."
-- 初始对白
tFestivalCardMonth_Text[18578]["Text121"] = "You`ll need to break 4 seals to access the Heart of Olympus where the ultimate treasure was buried."
tFestivalCardMonth_Text[18578]["Text122"] = "~Every time when you defeat 3 demons, you can lift a seal, and receive a special treasure. So, you know what to do? Clear all the demons!\n\n"
tFestivalCardMonth_Text[18578]["Text124"] = "    Currently, %d heroes have obtained the ultimate treasure.\n\n"
tFestivalCardMonth_Text[18578]["Option12"] = "Break~the~seal!"
tFestivalCardMonth_Text[18578]["Option13"] = "Enter~the~Heart~of~Olympus."
tFestivalCardMonth_Text[18578]["Option14"] = "What`re~the~treasures?"
tFestivalCardMonth_Text[18578]["Option15"] = "Sounds~interesting."
-- 所有宝藏已领取
tFestivalCardMonth_Text[18578]["Text131"] = "My hero, you`ve already unlocked the Heart of Olympus, and claimed all the treasures. Nothing here now."
tFestivalCardMonth_Text[18578]["Option16"] = "Alright."
-- 开宝箱失败
tFestivalCardMonth_Text[18578]["Text141"] = "You should defeat %d demons to lift a seal on the Heart of Olympus. Keep working hard!\n\n"
tFestivalCardMonth_Text[18578]["Text142"] = "    Demons Killed: %d"
tFestivalCardMonth_Text[18578]["Option17"] = "Okay."
-- 背包空间不足
tFestivalCardMonth_Text[18578]["Text151"] = "You`re carrying a full bag. You need to make some room for the treasure, first."
tFestivalCardMonth_Text[18578]["Option18"] = "I~see."
-- 点天石数达上限
tFestivalCardMonth_Text[18578]["Text161"] = "You`re carrying the maximum amount of CPs. Please deposit or spend some, first."
tFestivalCardMonth_Text[18578]["Option19"] = "I~see."
-- 战胜3\6\9个恶魔奖励
tFestivalCardMonth_Text[18578]["Text171"] = "You`ve defeated %d demons, and broke Seal No.%d. Here are %d CPs (B) and %d Chi Points for you!"
tFestivalCardMonth_Text[18578]["Option20"] = "Thanks!"
-- 战胜所有恶魔奖励（10名以内）
tFestivalCardMonth_Text[18578]["Text181"] = "Excellent! You`ve swept all demons off, becoming the No.%d hero to unlock the Heart of Olympus. You`ll win %d Chi Points, %d Senior Training Pills (B), 1 %d-day Wind Walk garment and %d God Points."
tFestivalCardMonth_Text[18578]["Option21"] = "Thanks!"
-- 战胜所有恶魔奖励（10名以下）
tFestivalCardMonth_Text[18578]["Text191"] = "Excellent! You`ve swept all demons off, becoming the No.%d hero to unlock the Heart of Olympus. The treasure belongs to you now: 10 Vital Pills (B) and 10 Senior Training Pills (B)."
tFestivalCardMonth_Text[18578]["Option22"] = "Thanks!"

-- 【进入奥林匹斯之心.】
-- 没有完成所有恶魔挑战
tFestivalCardMonth_Text[18578]["Text211"] = "Only when you defeat all the demons on Mount Olympus, you can access the Heart of Olympus."
tFestivalCardMonth_Text[18578]["Option211"] = "I~see."
-- 已进入过一次
tFestivalCardMonth_Text[18578]["Text221"] = "You`ve already obtained the ultimate treasure in the Heart of Olympus. Why not leave the chance to others?"
tFestivalCardMonth_Text[18578]["Option221"] = "Alright."

tFestivalCardMonth_Text[18578]["GongGao"] = "Congratulations! %s defeated all the demons, and became the No.%d hero to unlock the Heart of Olympus."

-- 【查看宝藏详情】
--初始对白
tFestivalCardMonth_Text[18578]["Text311"] = "When you defeat 3, 6, 9 or 12 demons on Mount Olympus, you`ll receive amazing treasures:\n\n"
tFestivalCardMonth_Text[18578]["Text312"] = "    Defeat 3 demons to win 50 CPs (B) and 500 Chi Points.\n"
tFestivalCardMonth_Text[18578]["Text313"] = "    Defeat 6 demons to win 150 CPs (B) and 1500 Chi Points.\n"
tFestivalCardMonth_Text[18578]["Text314"] = "    Defeat 9 demons to win 300 CPs (B) and 5000 Chi Points.\n"
tFestivalCardMonth_Text[18578]["Text315"] = "    Heroes who defeat 12 demons will be rewarded according to their rank.\n\n"
tFestivalCardMonth_Text[18578]["Option311"] = "Reward~for~ranking~No.1~-~5."
tFestivalCardMonth_Text[18578]["Option312"] = "Reward~for~ranking~after~No.5."
tFestivalCardMonth_Text[18578]["Option313"] = "Learn~about~other~things."
tFestivalCardMonth_Text[18578]["Option314"] = "I~see."
-- 查看前5名奖励
tFestivalCardMonth_Text[18578]["Text321"] = " Rank                   Reward\n"
tFestivalCardMonth_Text[18578]["Text322"] = "    1    |    50000 Chi Points, 50 Senior Training Pills (B)\n                a 30-day Wind Walk garment, 1000 God Points.\n"
tFestivalCardMonth_Text[18578]["Text323"] = "    2    |    30000 Chi Points, 40 Senior Training Pills (B)\n                a 30-day Wind Walk garment, 900 God Points.\n"
tFestivalCardMonth_Text[18578]["Text324"] = "    3    |    20000 Chi Points, 30 Senior Training Pills (B)\n                a 7-day Wind Walk garment, 800 God Points.\n"
tFestivalCardMonth_Text[18578]["Text325"] = "    4    |    10000 Chi Points, 30 Senior Training Pills (B)\n                a 7-day Wind Walk garment, 700 God Points.\n"
tFestivalCardMonth_Text[18578]["Text326"] = "    5    |    10000 Chi Points, 30 Senior Training Pills (B)\n                a 7-day Wind Walk garment, 600 God Points.\n"
tFestivalCardMonth_Text[18578]["Option321"] = "Previous."
tFestivalCardMonth_Text[18578]["Option322"] = "Close."
-- 查看前5名奖励
tFestivalCardMonth_Text[18578]["Text331"] = " Rank                   Reward\n"
tFestivalCardMonth_Text[18578]["Text332"] = "    6     |    10000 Chi Points, 30 Senior Training Pills (B)\n                  a 7-day Wind Walk garment, 500 God Points.\n"
tFestivalCardMonth_Text[18578]["Text333"] = "    7     |    10000 Chi Points, 30 Senior Training Pills (B)\n                  a 7-day Wind Walk garment, 400 God Points.\n"
tFestivalCardMonth_Text[18578]["Text334"] = "    8     |    10000 Chi Points, 30 Senior Training Pills (B)\n                  a 7-day Wind Walk garment, 300 God Points.\n"
tFestivalCardMonth_Text[18578]["Text335"] = "    9     |    10000 Chi Points, 30 Senior Training Pills (B)\n                  a 7-day Wind Walk garment, 200 God Points.\n"
tFestivalCardMonth_Text[18578]["Text336"] = "    10   |    10000 Chi Points, 30 Senior Training Pills (B)\n                  a 7-day Wind Walk garment, 100 God Points.\n"
tFestivalCardMonth_Text[18578]["Text337"] = "    10+ |    10 Vital Pills (B) and 10 Senior Training Pills (B)."

tFestivalCardMonth_Text[18578]["Option331"] = "Previous."
tFestivalCardMonth_Text[18578]["Option332"] = "Close."





--进入奥林匹斯之心提示
tFestivalCardMonth_Text[18578]["ChgMap"] = "In the Heart of Olympus, just kill 8 monsters to win a random reward."



--======================================= 十二怪物NPC对白 ========================================
-- // ==万妖之祖堤丰== \\
tFestivalCardMonth_Text[18579] = {}
-- 活动时间外
tFestivalCardMonth_Text[18579]["Text111"] = "The crisis has been resolved, and you were teleported out of Mount Olympus."
tFestivalCardMonth_Text[18579]["Option11"] = "Okay."

-- 初始对白
tFestivalCardMonth_Text[18579]["Text121"] = "The last child of Gaia, Typhon was the most deadly monster in myth. He was known as the Father of All Monsters,"
tFestivalCardMonth_Text[18579]["Text122"] = "~with the power over storm winds.\n\n    Required Card: Zeus\n"
tFestivalCardMonth_Text[18579]["Text123"] = "    Current Challenge Stage: %s\n    Current Rewards: %s\n\n"
tFestivalCardMonth_Text[18579]["Option12"] = "Challenge~the~demon."
tFestivalCardMonth_Text[18579]["Option13"] = "Leave."
-- 没有主神卡
tFestivalCardMonth_Text[18579]["Text131"] = "You should have the Zeus card to challenge Typhon."
tFestivalCardMonth_Text[18579]["Option14"] = "Okay."
-- 挑战恶魔
tFestivalCardMonth_Text[18579]["Text141"] = "Anyone who dares to awaken Typhon will be punished"
tFestivalCardMonth_Text[18579]["Text142"] = "~and lose his mind.\n\n"
tFestivalCardMonth_Text[18579]["Text143"] = "    Current HP: %d\n    %s Attack: %d\n"
tFestivalCardMonth_Text[18579]["Text144"] = "    Remaining Free Chances to Challenge: %d\n\n"
tFestivalCardMonth_Text[18579]["Option15"] = "Let`s~fight!"
tFestivalCardMonth_Text[18579]["Option16"] = "I~changed~my~mind."
-- 免费挑战次数不足
tFestivalCardMonth_Text[18579]["Text151"] = "You`ve ran out of free chances. If you want to continue, you need to pay some CPs. So, what do you say?"
tFestivalCardMonth_Text[18579]["Option17"] = "Pay~27~CPs~for~1~chance."
tFestivalCardMonth_Text[18579]["Option23"] = "Pay~135~CPs~for~5~chances."
tFestivalCardMonth_Text[18579]["Option18"] = "I~need~to~think~about~it."
-- 背包空间不足
tFestivalCardMonth_Text[18579]["Text161"] = "Your inventory is full. Please make some room, first."
tFestivalCardMonth_Text[18579]["Option19"] = "Okay."
--挑战完成提示
tFestivalCardMonth_Text[18579]["Text171"] = "You`ve proved your thrilling power! You defeated %s, and received %d %s"
tFestivalCardMonth_Text[18579]["Text172"] = "~%s, 1 %s and %d God Points!"
tFestivalCardMonth_Text[18579]["Option20"] = "Thanks!"
-- 天石不足
tFestivalCardMonth_Text[18579]["Text181"] = "Sorry, you don`t have enough CPs to continue the challenge."
tFestivalCardMonth_Text[18579]["Option21"] = "Alright."
--击败恶魔给奖励
tFestivalCardMonth_Text[18579]["Text191"] = "You do have a special ability. You inflicted %d points of damage on the demon, draining his HP down to %d. Continue to challenge?"
tFestivalCardMonth_Text[18579]["Option22"] = "Yes!"
tFestivalCardMonth_Text[18579]["GongGao"] = "%s activated the %s card, and won the battle on Stage %d against %s!"




-- // ==狂暴女神厄喀德娜== \\
tFestivalCardMonth_Text[18580] = {}
-- 初始对白
tFestivalCardMonth_Text[18580]["Text121"] = "Echidna was half beautiful woman and half fearsome snake. She was the mate of Typhon,"
tFestivalCardMonth_Text[18580]["Text122"] = "~and bore many fierce monsters.\n\n    Required Card: Hera\n"
tFestivalCardMonth_Text[18580]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18580]["Option13"] = "Leave."
-- 没有主神卡
tFestivalCardMonth_Text[18580]["Text131"] = "You should have the Hera card to challenge Echidna."
tFestivalCardMonth_Text[18580]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18580]["Text141"] = "As a monster of the underworld, Echidna developed a hobby of eating raw flesh."
tFestivalCardMonth_Text[18580]["Text142"] = "\n\n"


-- // ==海怪利维坦== \\
tFestivalCardMonth_Text[18581] = {}
-- 初始对白
tFestivalCardMonth_Text[18581]["Text121"] = "Leviathan was a giant sea monster who was able to breathe fire.\n\n"
tFestivalCardMonth_Text[18581]["Text122"] = "    Required Card: Poseidon\n"
tFestivalCardMonth_Text[18581]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18581]["Option13"] = "Leave."
-- 没有主神卡
tFestivalCardMonth_Text[18581]["Text131"] = "You should have the Poseidon card to challenge Leviathan."
tFestivalCardMonth_Text[18581]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18581]["Text141"] = "Fire-breathing Leviathan has strong scales and fearsome teeth."
tFestivalCardMonth_Text[18581]["Text142"] = "~Who dares to open his mouth?\n\n"


-- // ==喷火兽喀迈拉== \\
tFestivalCardMonth_Text[18582] = {}
-- 初始对白
tFestivalCardMonth_Text[18582]["Text121"] = "Chimera was a fire breathing three-headed monster with lion in front and snake behind, a goat in the middle."
tFestivalCardMonth_Text[18582]["Text122"] = "~He was one of the offspring of Typhon and Echidna.\n\n    Required Card: Demeter\n"
tFestivalCardMonth_Text[18582]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18582]["Option13"] = "Leave."
-- 没有主神卡
tFestivalCardMonth_Text[18582]["Text131"] = "You should have the Demeter card to challenge Chimera."
tFestivalCardMonth_Text[18582]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18582]["Text141"] = "Composed of the parts of three animals, Chimera was born with incredible power."
tFestivalCardMonth_Text[18582]["Text142"] = "\n\n"


-- // ==蛇发女妖美杜莎== \\
tFestivalCardMonth_Text[18583] = {}
-- 初始对白
tFestivalCardMonth_Text[18583]["Text121"] = "Medusa was originally a beautiful maiden, but was transformed into a horrific monster because of her disrespect to Athena.\n\n"
tFestivalCardMonth_Text[18583]["Text122"] = "    Required Card: Athena\n"
tFestivalCardMonth_Text[18583]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18583]["Option13"] = "Leave"
-- 没有主神卡
tFestivalCardMonth_Text[18583]["Text131"] = "You should have the Athena card to challenge Medusa."
tFestivalCardMonth_Text[18583]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18583]["Text141"] = "Medusa was the only one of the three Gorgons who was mortal,"
tFestivalCardMonth_Text[18583]["Text142"] = "~Her gaze could turn onlookers to stone.\n\n"


-- // ==半牛怪弥诺陶洛斯== \\
tFestivalCardMonth_Text[18584] = {}
-- 初始对白
tFestivalCardMonth_Text[18584]["Text121"] = "Minotaur was a monster with the head of a bull and the body of a man.\n\n"
tFestivalCardMonth_Text[18584]["Text122"] = "    Required Card: Apollo\n"
tFestivalCardMonth_Text[18584]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18584]["Option13"] = "Leave"
-- 没有主神卡
tFestivalCardMonth_Text[18584]["Text131"] = "You should have the Apollo card to challenge Minotaur."
tFestivalCardMonth_Text[18584]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18584]["Text141"] = "Where Minotaur went, where blood and flesh scattered everywhere."
tFestivalCardMonth_Text[18584]["Text142"] = "\n\n"


-- // ==海妖斯库拉== \\
tFestivalCardMonth_Text[18585] = {}
-- 初始对白
tFestivalCardMonth_Text[18585]["Text121"] = "Scylla was a tentacled monster who fed on passing sailors in the straits.\n\n"
tFestivalCardMonth_Text[18585]["Text122"] = "    Required Card: Artemis\n"
tFestivalCardMonth_Text[18585]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18585]["Option13"] = "Leave."
-- 没有主神卡
tFestivalCardMonth_Text[18585]["Text131"] = "You should have the Artemis card to challenge Scylla."
tFestivalCardMonth_Text[18585]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18585]["Text141"] = "Scylla often hides in narrow gap to ambush the passing enemies."
tFestivalCardMonth_Text[18585]["Text142"] = "\n\n"


-- // ==食人女怪安普莎== \\
tFestivalCardMonth_Text[18586] = {}
-- 初始对白
tFestivalCardMonth_Text[18586]["Text121"] = "Empuse was a half-woman and half-donkey demon as the servant of the goddess Hecate in the underworld.\n\n"
tFestivalCardMonth_Text[18586]["Text122"] = "    Required Card: Aphrodite\n"
tFestivalCardMonth_Text[18586]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18586]["Option13"] = "Leave."
-- 没有主神卡
tFestivalCardMonth_Text[18586]["Text131"] = "You should have the Aphrodite card to challenge Empusa."
tFestivalCardMonth_Text[18586]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18586]["Text141"] = "Empusa is able to disguise herself as a beautiful woman,"
tFestivalCardMonth_Text[18586]["Text142"] = "~and seduces young men to drink their blood and eat their flesh.\n\n"


-- // ==鸟身女妖哈耳庇埃== \\
tFestivalCardMonth_Text[18587] = {}
-- 初始对白
tFestivalCardMonth_Text[18587]["Text121"] = "Harpy was a female monster of half bird and half human, whose face was always pale due to hunger.\n\n"
tFestivalCardMonth_Text[18587]["Text122"] = "    Required Card: Hermes\n"
tFestivalCardMonth_Text[18587]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18587]["Option13"] = "Leave."
-- 没有主神卡
tFestivalCardMonth_Text[18587]["Text131"] = "You should have the Hermes card to challenge Harpy."
tFestivalCardMonth_Text[18587]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18587]["Text141"] = "Harpy is known for the proclivity for stealing food."
tFestivalCardMonth_Text[18587]["Text142"] = "\n\n"


-- // ==冥犬刻耳柏洛斯== \\
tFestivalCardMonth_Text[18588] = {}
-- 初始对白
tFestivalCardMonth_Text[18588]["Text121"] = "Cerberus was the offspring of Typhon and Echidna, who guarded the gates of the Underworld to prevent"
tFestivalCardMonth_Text[18588]["Text122"] = "~the dead from escaping.\n\n    Required Card: Ares\n"
tFestivalCardMonth_Text[18588]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18588]["Option13"] = "Leave"
-- 没有主神卡
tFestivalCardMonth_Text[18588]["Text131"] = "You should have the Ares card to challenge Cerberus."
tFestivalCardMonth_Text[18588]["Option14"] = "Okay."
-- 挑战恶魔
tFestivalCardMonth_Text[18588]["Text141"] = "Cerberus possesses multiple heads, and is able to spit deadly venom."
tFestivalCardMonth_Text[18588]["Text142"] = "\n\n"


-- // ==勾魂使者凯瑞斯== \\
tFestivalCardMonth_Text[18589] = {}
-- 初始对白
tFestivalCardMonth_Text[18589]["Text121"] = "Ker was a ruthless death spirit with a thirst for human blood who brought the dead to the Underworld.\n\n"
tFestivalCardMonth_Text[18589]["Text122"] = "    Required Card: Hephaistos\n"
tFestivalCardMonth_Text[18589]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18589]["Option13"] = "Leave"
-- 没有主神卡
tFestivalCardMonth_Text[18589]["Text131"] = "You should have the Hephaistos card to challenge Ker."
tFestivalCardMonth_Text[18589]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18589]["Text141"] = "As a death demon, Ker has grim eyes, white teeth and thrilling claws."
tFestivalCardMonth_Text[18589]["Text142"] = "\n\n"


-- // ==独眼巨人兰杜迦== \\
tFestivalCardMonth_Text[18590] = {}
-- 初始对白
tFestivalCardMonth_Text[18590]["Text121"] = "Cyclope was an one-eyed demon who was rescued by Zeus from the dark abyss and inhabited Sicily.\n\n"
tFestivalCardMonth_Text[18590]["Text122"] = "    Required Card: Dionysus\n"
tFestivalCardMonth_Text[18590]["Option12"] = "Challenge~this~demon."
tFestivalCardMonth_Text[18590]["Option13"] = "Leave"
-- 没有主神卡
tFestivalCardMonth_Text[18590]["Text131"] = "You should have the Dionysus card to challenge Cyclope."
tFestivalCardMonth_Text[18590]["Option14"] = "I~see."
-- 挑战恶魔
tFestivalCardMonth_Text[18590]["Text141"] = "With stubbornness and irritability in character, Cyclope often easily lose his temper and destroy everything."
tFestivalCardMonth_Text[18590]["Text142"] = "\n\n"


----------------------------------------------------------------------------------------------
-- 怪物阶段奖励
tFestivalCardMonth_Text["PhaseAward"] = {}
-- 万妖之祖堤丰
tFestivalCardMonth_Text["PhaseAward"][18579] = {}
tFestivalCardMonth_Text["PhaseAward"][18579][1] = "1 Zeus Pack, 1 Primary Light Pack"
tFestivalCardMonth_Text["PhaseAward"][18579][2] = "2 Zeus Packs, 1 Medium Light Pack"
tFestivalCardMonth_Text["PhaseAward"][18579][3] = "3 Lucky Card Packs, 1 Senior Light Pack"

-- 狂暴女神厄喀德娜
tFestivalCardMonth_Text["PhaseAward"][18580] = {}
tFestivalCardMonth_Text["PhaseAward"][18580][1] = "1 Hera Pack, 1 Primary Light Pack"
tFestivalCardMonth_Text["PhaseAward"][18580][2] = "2 Hera Packs, 1 Medium Light Pack"
tFestivalCardMonth_Text["PhaseAward"][18580][3] = "3 Lucky Card Packs, 1 Senior Light Pack"

-- 海怪利维坦
tFestivalCardMonth_Text["PhaseAward"][18581] = {}
tFestivalCardMonth_Text["PhaseAward"][18581][1] = "1 Poseidon Pack, 1 Primary Light Pack"
tFestivalCardMonth_Text["PhaseAward"][18581][2] = "2 Poseidon Packs, 1 Medium Light Pack"
tFestivalCardMonth_Text["PhaseAward"][18581][3] = "3 Lucky Card Packs, 1 Senior Light Pack"

-- 喷火兽喀迈拉
tFestivalCardMonth_Text["PhaseAward"][18582] = {}
tFestivalCardMonth_Text["PhaseAward"][18582][1] = "1 Demeter Pack, 1 Primary Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18582][2] = "2 Demeter Packs, 1 Medium Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18582][3] = "3 Lucky Card Packs, 1 Senior Anthem Pack"

-- 蛇发女妖美杜莎
tFestivalCardMonth_Text["PhaseAward"][18583] = {}
tFestivalCardMonth_Text["PhaseAward"][18583][1] = "1 Athena Pack, 1 Primary Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18583][2] = "2 Athena Packs, 1 Medium Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18583][3] = "3 Lucky Card Packs, 1 Senior Anthem Pack"

-- 半牛怪弥诺陶洛斯
tFestivalCardMonth_Text["PhaseAward"][18584] = {}
tFestivalCardMonth_Text["PhaseAward"][18584][1] = "1 Apollo Pack, 1 Primary Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18584][2] = "2 Apollo Packs, 1 Medium Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18584][3] = "3 Lucky Card Packs, 1 Senior Anthem Pack"

-- 海妖斯库拉
tFestivalCardMonth_Text["PhaseAward"][18585] = {}
tFestivalCardMonth_Text["PhaseAward"][18585][1] = "1 Artemis Pack, 1 Primary Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18585][2] = "2 Artemis Packs, 1 Medium Anthem Pack"
tFestivalCardMonth_Text["PhaseAward"][18585][3] = "3 Lucky Card Packs, 1 Senior Anthem Pack"

-- 食人女怪安普莎
tFestivalCardMonth_Text["PhaseAward"][18586] = {}
tFestivalCardMonth_Text["PhaseAward"][18586][1] = "1 Aphrodite Pack, 1 Primary Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18586][2] = "2 Aphrodite Packs, 1 Medium Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18586][3] = "3 Lucky Card Packs, 1 Senior Prayer Pack"

-- 鸟身女妖哈耳庇埃
tFestivalCardMonth_Text["PhaseAward"][18587] = {}
tFestivalCardMonth_Text["PhaseAward"][18587][1] = "1 Hermes Pack, 1 Primary Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18587][2] = "2 Hermes Packs, 1 Medium Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18587][3] = "3 Lucky Card Packs, 1 Senior Prayer Pack"

-- 冥犬刻耳柏洛斯
tFestivalCardMonth_Text["PhaseAward"][18588] = {}
tFestivalCardMonth_Text["PhaseAward"][18588][1] = "1 Ares Pack, 1 Primary Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18588][2] = "2 Ares Packs, 1 Medium Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18588][3] = "3 Lucky Card Packs, 1 Senior Prayer Pack"

-- 勾魂使者凯瑞斯
tFestivalCardMonth_Text["PhaseAward"][18589] = {}
tFestivalCardMonth_Text["PhaseAward"][18589][1] = "1 Hephaistos Pack, 1 Primary Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18589][2] = "2 Hephaistos Packs, 1 Medium Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18589][3] = "3 Lucky Card Packs, 1 Senior Prayer Pack"

-- 独眼巨人兰杜迦
tFestivalCardMonth_Text["PhaseAward"][18590] = {}
tFestivalCardMonth_Text["PhaseAward"][18590][1] = "1 Dionysus Pack, 1 Primary Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18590][2] = "2 Dionysus Packs, 1 Medium Prayer Pack"
tFestivalCardMonth_Text["PhaseAward"][18590][3] = "3 Lucky Card Packs, 1 Senior Prayer Pack"


--NPC其他
tFestivalCardMonth_Text["LucyBagSuccess"] = "You successfully received 1 %s!"
tFestivalCardMonth_Text["SupCardConfSuccess"] = "You`ve claimed 1 %s card. Please take it carefully."
tFestivalCardMonth_Text["FightComplete"] = "Completed"
tFestivalCardMonth_Text["FightCompleteAwrad"] = "Some Holy Item Fragments or a random holy item for %s."



----------------------------------物品使用对白--------------------------------------
-- 物品过期
tFestivalCardMonth_Text["Item_TimeOut"] = "The crisis has been resolved, and these items became useless, so you threw them away."

-------------------------------------------【主神卡对白】------------------------------------------------
--主对白
tFestivalCardMonth_Text["GodCard_DialogText"] = "%s is one of the cards of the Twelve Olympians.\nTo upgrade this card, you`ll need Holy Items: %s, one piece for each.\n\n    Level: %d\n    Attack: %d\n\n"
tFestivalCardMonth_Text["GodCard_Option1"] = "Upgrade~the~card."
tFestivalCardMonth_Text["GodCard_Option2"] = "Tell~me~more."
--升级成功对白
tFestivalCardMonth_Text["GodCard_UplevSuccess"] = "You`ve successfully upgraded %s, and received 10 God Points. Card Attributes:\n\n    Level: %d\n    Attack: %d\n\n"
tFestivalCardMonth_Text["GodCard_Option3"] = "Keep~upgrading."
tFestivalCardMonth_Text["GodCard_Option4"] = "Close."
--升级失败对白
tFestivalCardMonth_Text["GodCard_UplevFail"] = "You don`t have all the required Holy Item cards to upgrade this God card. You should provide 1 %s, 1 %s and 1 %s."
--活动过期删除提示
tFestivalCardMonth_Text["GodCard_TimeOut"] = "The crisis has been resolved, and your Level %d %s turned into %d Study Points."


--【主神卡介绍】
tFestivalCardMonth_Text["GodCard_DescText1"] = {}
tFestivalCardMonth_Text["GodCard_DescText2"] = {}
tFestivalCardMonth_Text["GodCard_DescText3"] = {}
--【选项】
tFestivalCardMonth_Text["GodCard_Option5"] = "Previous."
tFestivalCardMonth_Text["GodCard_Option6"] = "Leave."

--主神卡1-天父宙斯-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006620] = "As the supreme ruler of the Olympians, Zeus has a deep insight into everything in the world."
tFestivalCardMonth_Text["GodCard_DescText2"][3006620] = "~Known as the God of Sky, he is able to summon thunderbolt and storm."
tFestivalCardMonth_Text["GodCard_DescText3"][3006620] = ""
--主神卡2-天后赫拉-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006621] = "Hera is the Queen of the gods, as well as the wife of Zeus. She is ravishingly beautiful, with"
tFestivalCardMonth_Text["GodCard_DescText2"][3006621] = "~piercing insight and bright eyes. Being the goddess of marriage and family, she takes special"
tFestivalCardMonth_Text["GodCard_DescText3"][3006621] = "~care of married women."
--主神卡3-海神波塞冬-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006622] = "Poseidon is one of the Twelve Olympians, and also known as the God of the Sea and the Earth Shaker"
tFestivalCardMonth_Text["GodCard_DescText2"][3006622] = "~due to his marvelous power."
tFestivalCardMonth_Text["GodCard_DescText3"][3006622] = ""
--主神卡4-丰饶女神德墨忒尔-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006623] = "Demeter is one of the Twelve Olympians, who presided over grains and the fertility of the earth."
tFestivalCardMonth_Text["GodCard_DescText2"][3006623] = "~As the goddess of the harvest, she presents humankind agriculture, and frees them from low"
tFestivalCardMonth_Text["GodCard_DescText3"][3006623] = "~civilization."
--主神卡5-智慧女神雅典娜-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006624] = "Athena is one of the Twelve Olympians, who is the ruler of dark clouds and thunder. She is also"
tFestivalCardMonth_Text["GodCard_DescText2"][3006624] = "~known as the goddess of wisdom, law and justice, and war strategy."
tFestivalCardMonth_Text["GodCard_DescText3"][3006624] = ""
--主神卡6-光明之神阿波罗-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006625] = "Apollo is one of the Twelve Olympians, who is the son of Zeus and Leto. Artemis is his twin sister."
tFestivalCardMonth_Text["GodCard_DescText2"][3006625] = "~He is defined as the god of music and arts, medicine and healing, and inspiration."
tFestivalCardMonth_Text["GodCard_DescText3"][3006625] = ""
--主神卡7-月之女神阿耳忒弥斯-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006626] = "Artemis is one of the Twelve Olympians, as a symbol of the stainless maiden. As the goddess of hunt, moon and virginity"
tFestivalCardMonth_Text["GodCard_DescText2"][3006626] = "~she presides over childbirth and relieves disease in women."
tFestivalCardMonth_Text["GodCard_DescText3"][3006626] = ""
--主神卡8-爱之女神阿佛洛狄忒-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006627] = "Aphrodite is one of the Twelve Olympians, who is the goodess of love, beauty and desire."
tFestivalCardMonth_Text["GodCard_DescText2"][3006627] = "~She was born in the sea foam with immense beauty and incomparable elegance."
tFestivalCardMonth_Text["GodCard_DescText3"][3006627] = ""
--主神卡9-商业之神赫耳墨斯-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006628] = "Hermes is one of the Twelve Olympians, who is the son of Zeus and Maia. With a pair of winged sandals and a winged staff with twin"
tFestivalCardMonth_Text["GodCard_DescText2"][3006628] = "~snakes, he is able to move swiftly, serving as the messenger of the gods. He is also regarded as the patron of commerce."
tFestivalCardMonth_Text["GodCard_DescText3"][3006628] = ""
--主神卡10-战神阿瑞斯-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006629] = "Ares is one of the Twelve Olympians, who is a reincarnation of military spirit. As the god of war, he is considered as"
tFestivalCardMonth_Text["GodCard_DescText2"][3006629] = "~the most violent and murderous deity."
tFestivalCardMonth_Text["GodCard_DescText3"][3006629] = ""
--主神卡11-火神赫淮斯托斯-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006630] = "Hephaistos is one of the Twelve Olympians, who is the first son of Zeus and Hera. As the god of fire and forges,"
tFestivalCardMonth_Text["GodCard_DescText2"][3006630] = "~he makes all the weapons of the gods in Olympus."
tFestivalCardMonth_Text["GodCard_DescText3"][3006630] = ""
--主神卡12-火神赫淮斯托斯-介绍.
tFestivalCardMonth_Text["GodCard_DescText1"][3006631] = "Dionysus is one of the Twelve Olympians, who is able to make immense wine and create happiness."
tFestivalCardMonth_Text["GodCard_DescText2"][3006631] = "~He is also considered as the god of art and theater."
tFestivalCardMonth_Text["GodCard_DescText3"][3006631] = ""

--主神卡名字
tFestivalCardMonth_Text["GodCard"] = {}
tFestivalCardMonth_Text["GodCard"][3006620] = "Zeus"
tFestivalCardMonth_Text["GodCard"][3006621] = "Hera"
tFestivalCardMonth_Text["GodCard"][3006622] = "Poseidon"
tFestivalCardMonth_Text["GodCard"][3006623] = "Demeter"
tFestivalCardMonth_Text["GodCard"][3006624] = "Athena"
tFestivalCardMonth_Text["GodCard"][3006625] = "Apollo"
tFestivalCardMonth_Text["GodCard"][3006626] = "Artemis"
tFestivalCardMonth_Text["GodCard"][3006627] = "Aphrodite"
tFestivalCardMonth_Text["GodCard"][3006628] = "Hermes"
tFestivalCardMonth_Text["GodCard"][3006629] = "Ares"
tFestivalCardMonth_Text["GodCard"][3006630] = "Hephaistos"
tFestivalCardMonth_Text["GodCard"][3006631] = "Dionysus"


-------------------------------------------【主神卡包对白】------------------------------------------------
tFestivalCardMonth_Text["CardPag_PagFull"] = "Your inventory is full. Please make some room, first."
tFestivalCardMonth_Text["CardPag_GetHoly"] = "You opened the %s, and received %s, one for each."
tFestivalCardMonth_Text["CardPag_GetGodCard"] = "You opened the %s and received a(n) %s!"

--主神卡包名字
tFestivalCardMonth_Text["CardPag"] = {}
tFestivalCardMonth_Text["CardPag"][3006632] = "ZeusPack"
tFestivalCardMonth_Text["CardPag"][3006633] = "HeraPack"
tFestivalCardMonth_Text["CardPag"][3006634] = "PoseidonPack"
tFestivalCardMonth_Text["CardPag"][3006635] = "DemeterPack"
tFestivalCardMonth_Text["CardPag"][3006636] = "AthenaPack"
tFestivalCardMonth_Text["CardPag"][3006637] = "ApolloPack"
tFestivalCardMonth_Text["CardPag"][3006638] = "ArtemisPack"
tFestivalCardMonth_Text["CardPag"][3006639] = "AphroditePack"
tFestivalCardMonth_Text["CardPag"][3006640] = "HermesPack"
tFestivalCardMonth_Text["CardPag"][3006641] = "AresPack"
tFestivalCardMonth_Text["CardPag"][3006642] = "HephaistosPack"
tFestivalCardMonth_Text["CardPag"][3006643] = "DionysusPack"


-----------------------------------------【主神卡碎片对白】------------------------------------------------
tFestivalCardMonth_Text["CardPiece_Success"] = "You successfully combined 10 %s into a(n) %s!"
tFestivalCardMonth_Text["CardPiece_Fail"] = "You should have 10 %s to combine them into a(n) %s."

-------------------------------------------【圣物卡对白】------------------------------------------------
tFestivalCardMonth_Text["HolyCard_BagFull"] = "Your inventory is full. Please make some room before splitting the Holy Item card."
tFestivalCardMonth_Text["HolyCard_Success"] = "You successfully split the Holy Item card, and received 3 Holy Item Fragments and 1 free chance to make a Holy Item card."



-- 圣物卡分解二次确认
tFestivalCardMonth_Text["HolyCard_DecText"] = "Are you sure you want to split the %s?"
tFestivalCardMonth_Text["HolyCard_DecOption"] = "Yes."


----圣物卡介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"] = {}
tFestivalCardMonth_Text["HolyCard_DialogText2"] = {}
tFestivalCardMonth_Text["HolyCard_Option1"] = "Split~the~Holy~Item~card."
-- tFestivalCardMonth_Text["HolyCard_Option2"] = "Close."
--霹雳权杖-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006656] = "This sceptre is a holy item for Zeus, which can breathe fire and call down thunderbolt. Use it to enhance the power of Zeus."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006656] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--Zeus`Shield-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006657] = "This shield is a holy item for Zeus, which is reliable and unbreakable. Use it to enhance the power of Zeus."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006657] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--OliveCrown-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006658] = "This crown is a holy item for Zeus, which is a symbol of peace and supremacy. Use it to enhance the power of Zeus."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006658] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--LotusStaff-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006659] = "This staff is a holy item for Hera, which has a lotus on the top. Use it to enhance the power of Hera."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006659] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--GoldenCrown-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006660] = "This crown is a holy item for Hera, which is a symbol of dignity and supremacy. Use it to enhance the power of Hera."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006660] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--SacredLily-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006661] = "This lily is a holy item for Hera, which is Hera`s favorite. Use it to enhance the power of Hera."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006661] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--Poseidon`sTrident-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006662] = "This trident is a holy item for Poseidon, which symbolizes Poseidon`s ruling over the sea. Use it to enhance the power of Poseidon."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006662] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--HaloofTide-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006663] = "This halo is a holy item for Poseidon, which can awaken storms and tsunamis. Use it to enhance the power of Poseidon."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006663] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--DivineDolphin-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006664] = "This dolphin is a holy beast for Poseidon, which brings good wEarth. Use it to enhance the power of Poseidon."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006664] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--GoldenWheat-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006665] = "This wheat is a holy item for Demeter, which symbolizes fertility. Use it to enhance the power of Demeter."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006665] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--FortuneSickle-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006666] = "This sickle is a holy item for Demeter, which gives luck in every endeavor. Use it to enhance the power of Demeter."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006666] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--DreamStaff-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006667] = "This staff is a holy item for Demeter, which can turn anything it touches into gold. Use it to enhance the power of Demeter."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006667] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--ShieldofDefense-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006668] = "This shield is a holy item for Athena, which gives a strong sense of safety. Use it to enhance the power of Athena."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006668] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--SculptureofVictory-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006669] = "This sculpture is a holy item for Athena, which is a symbol of justice. Use it to enhance the power of Athena."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006669] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--PallasSpear-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006670] = "This spear is a holy item for Athena, which represents her determination to protect the commons. Use it to enhance the power of Athena"
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006670] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--SunChariot-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006671] = "This chariot is a holy item for Apollo, which glows with golden light. Use it to enhance the power of Apollo."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006671] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--LightArrow-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006672] = "This arrow is a holy item for Apollo, which never misses a target. Use it to enhance the power of Apollo."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006672] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--SwanHarp-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006673] = "This harp is a holy item for Apollo, which can play wonderful melodies. Use it to enhance the power of Apollo."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006673] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--SilverChariot-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006674] = "This chariot is a holy item for Artemis, which delivers a sense of freedom. Use it to enhance the power of Artemis."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006674] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--HalfMoonBow-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006675] = "This bow is a holy item for Artemis, which is an excellent weapon for hunting. Use it to enhance the power of Artemis."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006675] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--LightofMoon-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006676] = "This light is a holy item for Artemis, which symbolizes nature, freedom and purity. Use it to enhance the power of Artemis."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006676] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--LoveRose-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006677] = "This rose is a holy item for Aphrodite, which symbolizes love and romance. Use it to enhance the power of Aphrodite."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006677] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--LoveBelt-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006678] = "This belt is a holy item for Aphrodite, which is a tribute from women in the wedding. Use it to enhance the power of Aphrodite."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006678] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--LoveMirror-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006679] = "This mirror is a holy item for Aphrodite, which gives a channel to through people`s dream. Use it to enhance the power of Aphrodite."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006679] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--WingedSandals-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006680] = "This sandals is a holy item for Hermes, which is a symbol of speed and youth. Use it to enhance the power of Hermes."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006680] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--StaffofTwinSnakes-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006681] = "This staff is a holy item for Hermes, which can hypnotize or awaken humans. Use it to enhance the power of Hermes."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006681] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--Lyre-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006682] = "This lyre is a holy item for Hermes, which was made of tortoise`s shell. Use it to enhance the power of Hermes."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006682] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--BronzeWarspear-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006683] = "This warspear is a holy item for Ares, which is a symbol of military spirit. Use it to enhance the power of Ares."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006683] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--TorchofWar-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006684] = "This torch is a holy item for Ares, which can burn everything into ashes. Use it to enhance the power of Ares."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006684] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--Ares`Helmet-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006685] = "This helmet is a holy item for Ares, which is strong enough to withstand any attacks. Use it to enhance the power of Ares."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006685] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--DivineHammer-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006686] = "This hammer is a holy item for Hephaistos, which helps him forge various weapons for deities. Use it to enhance the power of Hephaistos."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006686] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--GeniusTongs-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006687] = "This tongs is a holy item for Hephaistos, which contributes a lot to genius inventions. Use it to enhance the power of Hephaistos."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006687] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--ForeverAnvil-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006688] = "This anvil is a holy item for Hephaistos, which is indispensable to forge holy weapons. Use it to enhance the power of Hephaistos."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006688] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."

--Grapevine-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006689] = "This grapevine is a holy item for Dionysus, which symbolizes tenacious vitality. Use it to enhance the power of Dionysus."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006689] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--ThyrsusStaff-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006690] = "This staff is a holy item for Dionysus, which is topped with a pine cone. Use it to enhance the power of Dionysus."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006690] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."
--WineCup-介绍
tFestivalCardMonth_Text["HolyCard_DialogText1"][3006691] = "This cup is a holy item for Dionysus, which symbolizes fascinating wine. Use it to enhance the power of Dionysus."
tFestivalCardMonth_Text["HolyCard_DialogText2"][3006691] = "~You can also split it to get 3 Holy Item Fragments and 1 free chance to make a Holy Item card."




--主神器名字
tFestivalCardMonth_Text["HolyCard"] = {}
tFestivalCardMonth_Text["HolyCard"][3006620] = "Sceptre of Thunderbolt, Zeus` Shield and Olive Crown"
tFestivalCardMonth_Text["HolyCard"][3006621] = "Lotus Staff, Golden Crown and Sacred Lily"
tFestivalCardMonth_Text["HolyCard"][3006622] = "Poseidon`s Trident, Halo of Tide and Divine Dolphin"
tFestivalCardMonth_Text["HolyCard"][3006623] = "Golden Wheat, Fortune Sickle and Dream Staff"
tFestivalCardMonth_Text["HolyCard"][3006624] = "Shield of Defense, Sculpture of Victory and Pallas Spear"
tFestivalCardMonth_Text["HolyCard"][3006625] = "Sun Chariot, Light Arrow and Swan Harp"
tFestivalCardMonth_Text["HolyCard"][3006626] = "Silver Chariot, Half Moon Bow and Light of Moon"
tFestivalCardMonth_Text["HolyCard"][3006627] = "Love Rose, Love Belt and Love Mirror"
tFestivalCardMonth_Text["HolyCard"][3006628] = "Winged Sandals, Staff of Twin Snakes and Lyre"
tFestivalCardMonth_Text["HolyCard"][3006629] = "Bronze Warspear, Torch of War and Ares` Helmet"
tFestivalCardMonth_Text["HolyCard"][3006630] = "Divine Hammer, Genius Tongs and Forever Anvil"
tFestivalCardMonth_Text["HolyCard"][3006631] = "Grapevine, Thyrsus Staff and Wine Cup"


-------------------------------------------【圣物卡碎片】------------------------------------------------
tFestivalCardMonth_Text[3006692] = {}
-- 初始对白
tFestivalCardMonth_Text[3006692]["Text111"] = "You can combine 10 Holy Item Fragments into a random Holy Item card, or 30 pieces into a specific Holy Item card."
tFestivalCardMonth_Text[3006692]["Text112"] = "~You`ll obtain 10 free chances of making cards, every day. If you run out of the chances, you need to pay 27 CPs, each time.\n\n"
tFestivalCardMonth_Text[3006692]["Text113"] = "    Remaining Free Chances: %d\n\n"
tFestivalCardMonth_Text[3006692]["Option11"] = "Make~a~random~card."
tFestivalCardMonth_Text[3006692]["Option12"] = "Make~a~specific~card."
-- 背包空间不足
tFestivalCardMonth_Text[3006692]["Text121"] = "Your inventory is full. Why not make some room, first?"
tFestivalCardMonth_Text[3006692]["Option13"] = "I`ll~do~it~now."
-- 数量不足
tFestivalCardMonth_Text[3006692]["Text131"] = "Sorry, you don`t have enough Holy Item Fragments to combine."
tFestivalCardMonth_Text[3006692]["Option14"] = "I~see."
-- 天石不足
tFestivalCardMonth_Text[3006692]["Text141"] = "Sorry, you don`t have enough CPs to make a Holy Item card."
tFestivalCardMonth_Text[3006692]["Option15"] = "I~see."
-- 天石二次确认
tFestivalCardMonth_Text[3006692]["Text151"] = "Are you sure you want to pay 27 CPs to make a Holy Item card?"
tFestivalCardMonth_Text[3006692]["Option16"] = "Yes."
tFestivalCardMonth_Text[3006692]["Option17"] = "No."
-- 成功打造
tFestivalCardMonth_Text[3006692]["CreateSuccess"] = "You successfully made a(n) %s!"

-- 【指定打造】
-- 选择主神（第一页）
tFestivalCardMonth_Text[3006692]["Text211"]  = "Which God would you like to make a Holy Item card for?"
tFestivalCardMonth_Text[3006692]["Option211"] = "Zeus."
tFestivalCardMonth_Text[3006692]["Option212"] = "Hera."
tFestivalCardMonth_Text[3006692]["Option213"] = "Poseidon."
tFestivalCardMonth_Text[3006692]["Option214"] = "Demeter."
tFestivalCardMonth_Text[3006692]["Option215"] = "Athena."
tFestivalCardMonth_Text[3006692]["Option216"] = "Apollo."
tFestivalCardMonth_Text[3006692]["Option217"] = "Next."
tFestivalCardMonth_Text[3006692]["Option218"] = "Back~to~the~first~page."
-- 选择主神（第二页）
tFestivalCardMonth_Text[3006692]["Text221"] = "Which God would you like to make a Holy Item card for?"
tFestivalCardMonth_Text[3006692]["Option221"] = "Artemis."
tFestivalCardMonth_Text[3006692]["Option222"] = "Aphrodite."
tFestivalCardMonth_Text[3006692]["Option223"] = "Hermes."
tFestivalCardMonth_Text[3006692]["Option224"] = "Ares."
tFestivalCardMonth_Text[3006692]["Option225"] = "Hephaistos."
tFestivalCardMonth_Text[3006692]["Option226"] = "Dionysus."
tFestivalCardMonth_Text[3006692]["Option227"] = "Previous."
tFestivalCardMonth_Text[3006692]["Option228"] = "Back~to~the~first~page."

-- 【第二层选择对白】（前6主神）
-- 天父宙斯(Zeus)
tFestivalCardMonth_Text[3006692]["Text311"] = "Which holy item would you like to make for Zeus?"
tFestivalCardMonth_Text[3006692]["Option311"] = "Sceptre~of~Thunderbolt."
tFestivalCardMonth_Text[3006692]["Option312"] = "Zeus`~Shield."
tFestivalCardMonth_Text[3006692]["Option313"] = "Olive~Crown."
tFestivalCardMonth_Text[3006692]["Option314"] = "Back~to~the~first~page."
tFestivalCardMonth_Text[3006692]["Option315"] = "Previous."
-- 天后赫拉(Hera)
tFestivalCardMonth_Text[3006692]["Text321"] = "Which holy item would you like to make for Hera?"
tFestivalCardMonth_Text[3006692]["Option321"] = "Lotus~Staff."
tFestivalCardMonth_Text[3006692]["Option322"] = "Golden~Crown."
tFestivalCardMonth_Text[3006692]["Option323"] = "Sacred~Lily."
-- 海神波塞冬(Poseidon)
tFestivalCardMonth_Text[3006692]["Text331"] = "Which holy item would you like to make for Poseidon?"
tFestivalCardMonth_Text[3006692]["Option331"] = "Poseidon`s~Trident."
tFestivalCardMonth_Text[3006692]["Option332"] = "Halo~of~Tide."
tFestivalCardMonth_Text[3006692]["Option333"] = "Divine~Dolphin."
-- 丰饶女神德墨忒尔(Demeter)
tFestivalCardMonth_Text[3006692]["Text341"] = "Which holy item would you like to make for Demeter?"
tFestivalCardMonth_Text[3006692]["Option341"] = "Golden~Wheat."
tFestivalCardMonth_Text[3006692]["Option342"] = "Fortune~Sickle."
tFestivalCardMonth_Text[3006692]["Option343"] = "Dream~Staff."
-- 智慧女神雅典娜(Athena)
tFestivalCardMonth_Text[3006692]["Text351"] = "Which holy item would you like to make for Athena?"
tFestivalCardMonth_Text[3006692]["Option351"] = "Shield~of~Defense."
tFestivalCardMonth_Text[3006692]["Option352"] = "Sculpture~of~Victory."
tFestivalCardMonth_Text[3006692]["Option353"] = "Pallas~Spear."
-- 光明之神阿波罗(Apollo)
tFestivalCardMonth_Text[3006692]["Text361"] = "Which holy item would you like to make for Apollo?"
tFestivalCardMonth_Text[3006692]["Option361"] = "Sun~Chariot."
tFestivalCardMonth_Text[3006692]["Option362"] = "Light~Arrow."
tFestivalCardMonth_Text[3006692]["Option363"] = "Swan~Harp."

-- 【第二层选择对白】（后6主神）
-- 月之女神阿耳忒弥斯(Artemis)
tFestivalCardMonth_Text[3006692]["Text411"] = "Which holy item would you like to make for Artemis?"
tFestivalCardMonth_Text[3006692]["Option411"] = "Silver~Chariot."
tFestivalCardMonth_Text[3006692]["Option412"] = "Half~Moon~Bow."
tFestivalCardMonth_Text[3006692]["Option413"] = "Light~of~Moon."
tFestivalCardMonth_Text[3006692]["Option414"] = "Previous."
tFestivalCardMonth_Text[3006692]["Option415"] = "Back~to~the~first~page."
-- 爱之女神阿佛洛狄忒(Aphrodite)
tFestivalCardMonth_Text[3006692]["Text421"] = "Which holy item would you like to make for Aphrodite?"
tFestivalCardMonth_Text[3006692]["Option421"] = "Love~Rose."
tFestivalCardMonth_Text[3006692]["Option422"] = "Love~Belt."
tFestivalCardMonth_Text[3006692]["Option423"] = "Love~Mirror."
-- 商业之神赫耳墨斯(Hermes)
tFestivalCardMonth_Text[3006692]["Text431"] = "Which holy item would you like to make for Hermes?"
tFestivalCardMonth_Text[3006692]["Option431"] = "Winged~Sandals."
tFestivalCardMonth_Text[3006692]["Option432"] = "Staff~of~Twin~Snakes."
tFestivalCardMonth_Text[3006692]["Option433"] = "Lyre."
-- 战神阿瑞斯(Ares)
tFestivalCardMonth_Text[3006692]["Text441"] = "Which holy item would you like to make for Ares?"
tFestivalCardMonth_Text[3006692]["Option441"] = "Bronze~Warspear."
tFestivalCardMonth_Text[3006692]["Option442"] = "Torch~of~War."
tFestivalCardMonth_Text[3006692]["Option443"] = "Ares`~Helmet."
-- 火神赫菲斯托斯(Hephaistos)
tFestivalCardMonth_Text[3006692]["Text451"] = "Which holy item would you like to make for Hephaistos?"
tFestivalCardMonth_Text[3006692]["Option451"] = "Divine~Hammer."
tFestivalCardMonth_Text[3006692]["Option452"] = "Genius~Tongs."
tFestivalCardMonth_Text[3006692]["Option453"] = "Forever~Anvil."
-- 酒神狄俄倪索斯(Dionysus)
tFestivalCardMonth_Text[3006692]["Text461"] = "Which holy item would you like to make for Dionysus?"
tFestivalCardMonth_Text[3006692]["Option461"] = "Grapevine."
tFestivalCardMonth_Text[3006692]["Option462"] = "Thyrsus~Staff."
tFestivalCardMonth_Text[3006692]["Option463"] = "Wine~Cup."

--主神卡碎片名字
tFestivalCardMonth_Text["CardPiece"] = {}
tFestivalCardMonth_Text["CardPiece"][3006644] = "Zeus`SpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006645] = "Hera`sSpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006646] = "Poseidon`sSpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006647] = "Demeter`sSpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006648] = "Athena`sSpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006649] = "Apollo`sSpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006650] = "Artemis`SpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006651] = "Aphrodite`sSpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006652] = "Hermes`SpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006653] = "Ares`SpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006654] = "Hephaistos`SpiritFragment"
tFestivalCardMonth_Text["CardPiece"][3006655] = "Dionysus`SpiritFragment"


------------------------------------------------【恶魔挑战书】------------------------------------------------
-- 使用后提示
tFestivalCardMonth_Text[3006693] = {}
tFestivalCardMonth_Text[3006693]["UseDialog"] = "You received 3 extra free chances to challenge the demons on Mount Olympus!"


------------------------------------------------【圣物打造书】------------------------------------------------
-- 使用后提示
tFestivalCardMonth_Text[3006694] = {}
tFestivalCardMonth_Text[3006694]["UseDialog"] = "You received 3 extra free chance to make a Holy Item card."


------------------------------------------------【主神卡幸运礼包】------------------------------------------------
-- 背包空间不足
tFestivalCardMonth_Text[3006695] = {}
tFestivalCardMonth_Text[3006695]["PagFull"] = "Your inventory is full. Please make some room, first."
tFestivalCardMonth_Text[3006695]["AwardItem"] = "You received %d %s!"

------------------------------------------------【主神卡幸运连抽礼包】------------------------------------------------
-- 背包空间不足
tFestivalCardMonth_Text[3006696] = {}
tFestivalCardMonth_Text[3006696]["PagFull"] = "Your inventory is full. Please make some room, first."
-- 获得奖励提示
tFestivalCardMonth_Text[3006696]["Text111"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text112"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text113"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text114"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text115"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text116"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text117"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text118"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text119"] = "Time %d : %d %s!\n"
tFestivalCardMonth_Text[3006696]["Text1110"] = "Time %d : %d %s!"
tFestivalCardMonth_Text[3006696]["Option11"] = "I~see."



------------------------------------------------【奥林匹斯神谕】------------------------------------------------
tFestivalCardMonth_Text[3006697] = {}
-- 初始对白
tFestivalCardMonth_Text[3006697]["Text111"] = "Mount Olympus has sank, and the whole world fell into a chaos. The Twelve Olympians are suffering their darkest days in prison."
tFestivalCardMonth_Text[3006697]["Text112"] = "~Are you the `CHOSEN HERO` to free the holy land from evils? Come and prove yourself from August 4th to August 17th!"
tFestivalCardMonth_Text[3006697]["Text113"] = "~The Olympus Emissary awaits you."
tFestivalCardMonth_Text[3006697]["Option11"] = "Go~find~emissary.~(Discard~it)"
-- 背包信提示
tFestivalCardMonth_Text[3006697]["Destroy"] = "You received a card of %s and %d %s"
tFestivalCardMonth_Text[3006697][30] = " minute of EXP!"
tFestivalCardMonth_Text[3006697][15] = " Study Points!"

-- 背包空间不足
tBackpackLetter_Text[3006697] = {}
tBackpackLetter_Text[3006697]["NoSpace"] = "Your inventory is full. Please make some room, and login again to receive the Olympians Oracle."
tBackpackLetter_Text[3006697]["RewardItem"] = "You received the Olympians Oracle. Hurry and check it in your inventory."

------------------------------------------【A-B-C可选类礼包】-------------------------------------------------

-----------------------【初级圣光礼包】
tFestivalCardMonth_Text[3006864] = {}
-- 初始对白
tFestivalCardMonth_Text[3006864]["Text111"] = "Gift 1: A Meteor Scroll and 5 EXP Ball (B);\n"
tFestivalCardMonth_Text[3006864]["Text112"] = "Gift 2: A Vital Pill (B) and 5 Favored Training Pills (B).\n"
tFestivalCardMonth_Text[3006864]["Text113"] = "~So, which one would you like to claim from this pack?"
tFestivalCardMonth_Text[3006864]["Option11"] = "Gift~1."
tFestivalCardMonth_Text[3006864]["Option12"] = "Gift~2."
tFestivalCardMonth_Text[3006864]["Option13"] = "Gift~3."
-- 背包空间不足
tFestivalCardMonth_Text[3006864]["Text121"] = "Your inventory is full. Please make some room, first."
tFestivalCardMonth_Text[3006864]["Option14"] = "Okay."
-- 给奖励提示
tFestivalCardMonth_Text[3006864]["Reward0"] = "You received "
tFestivalCardMonth_Text[3006864]["Reward1"] = "%d %s (B)"
tFestivalCardMonth_Text[3006864]["Reward11"] = "%d %s"
tFestivalCardMonth_Text[3006864]["Reward2"] = ", %d %s (B)"
tFestivalCardMonth_Text[3006864]["Reward21"] = ", %d %s"
tFestivalCardMonth_Text[3006864]["Reward3"] = ", %d %s (B)"




-----------------------【中级圣光礼包】
tFestivalCardMonth_Text[3006865] = {}
tFestivalCardMonth_Text[3006865]["Text111"] = "Gift 1: 3 Penitence Amulets (B), 2 +2 Steed (B) and a selective P5 Dragon Soul pack for armor;\n"
tFestivalCardMonth_Text[3006865]["Text112"] = "Gift 2: 2 Vital Pills (B) and 1 Modesty Book (B).\n"

-----------------------【高级圣光礼包】
tFestivalCardMonth_Text[3006866] = {}
tFestivalCardMonth_Text[3006866]["Text111"] = "Gift 1: 1 +3 Stone (B) and a selective P6 Dragon Soul pack for armor;\n"
tFestivalCardMonth_Text[3006866]["Text112"] = "Gift 2: 1 Frozen Chi Pill (B).\n"

-----------------------【初级圣歌礼包】
tFestivalCardMonth_Text[3006867] = {}
tFestivalCardMonth_Text[3006867]["Text111"] = "Gift 1: 3  EXP Potions (B), 3 Meteors and 3 EXP Balls (B);\n"
tFestivalCardMonth_Text[3006867]["Text112"] = "Gift 2: 3 Penitence Amulets (B) and 3 Favored Training Pills (B).\n"

-----------------------【中级圣歌礼包】
tFestivalCardMonth_Text[3006868] = {}
tFestivalCardMonth_Text[3006868]["Text111"] = "Gift 1: 1 Meteor Scroll, 1 +2 Stone (B) and 1 Modesty Book (B);\n"
tFestivalCardMonth_Text[3006868]["Text112"] = "Gift 2: 1 Vital Pill (B), 5 Special Training Pills (B) and 6 Cordiality Books (B).\n"

-----------------------【高级圣歌礼包】
tFestivalCardMonth_Text[3006869] = {}
tFestivalCardMonth_Text[3006869]["Text111"] = "Gift 1: 1 Super Dragon Gem (B) and 5 Penitence Amulets (B);\n"
tFestivalCardMonth_Text[3006869]["Text112"] = "Gift 2: 1 Super Phoenix Gem (B) and 5 Penitence Amulets (B);\n"
tFestivalCardMonth_Text[3006869]["Text114"] = "Gift 3: 1 Vital Pill (B), 2 Senior Training Pills (B) and 5 Favored Training Pills (B).\n"

-----------------------【初级圣佑礼包】
tFestivalCardMonth_Text[3006870] = {}
tFestivalCardMonth_Text[3006870]["Text111"] = "Gift 1: 2 EXP Potions (B), 2 Meteors and 2 EXP Balls (B);\n"
tFestivalCardMonth_Text[3006870]["Text112"] = "Gift 2: 2 Penitence Amulets (B) and 2 Favored Training Pills (B).\n"

-----------------------【中级圣佑礼包】
tFestivalCardMonth_Text[3006871] = {}
tFestivalCardMonth_Text[3006871]["Text111"] = "Gift 1: 2 EXP Balls (B), 2 +1 Stones (B) and 1 Praying Stone (S) (B);\n"
tFestivalCardMonth_Text[3006871]["Text112"] = "Gift 2: 3 Favored Training Pills (B) and 3 Special Training Pills (B).\n"

-----------------------【高级圣佑礼包】
tFestivalCardMonth_Text[3006872] = {}
tFestivalCardMonth_Text[3006872]["Text111"] = "Gift 1: 1 Meteor Scroll, 1 +2 Steed (B) and 6 Cordiality Books (B);\n"
tFestivalCardMonth_Text[3006872]["Text112"] = "Gift 2: 1 Vital Pill (B) and 1 Senior Training Pill (B).\n"

------------------------------------------------【神赐宝箱】------------------------------------------------
-- 天石宝箱
tFestivalCardMonth_Text[3006873] = {}
tFestivalCardMonth_Text[3006873]["EMoneyFull"] = "You`re carrying the maximum amount of CPs. Please deposit or spend some, first."
tFestivalCardMonth_Text[3006873]["AwardItem"] = "Lucky day! You opened the %s, and received %d CPs!"
-- 气力值宝箱
tFestivalCardMonth_Text[3006874] = {}
tFestivalCardMonth_Text[3006874]["AwardItem"] = "Lucky day! You opened the %s, and received %d Chi Points!"
-- 赠品天石宝箱
tFestivalCardMonth_Text[3006875] = {}
tFestivalCardMonth_Text[3006875]["AwardItem"] = "Lucky day! You opened the %s, and received %d CPs (B)!"
-- 龙珠宝箱
tFestivalCardMonth_Text[3006876] = {}
tFestivalCardMonth_Text[3006876]["AwardItem"] = "Lucky day! You opened the %s, and received %d Dragon Balls!"
-- Meteor Scroll宝箱
tFestivalCardMonth_Text[3006878] = {}
tFestivalCardMonth_Text[3006878]["AwardItem"] = "Lucky day! You opened the %s, and received %d Meteor Scrolls!"

------------------------------------------------【神赐 Stone宝箱】------------------------------------------------
-- 赤练石宝箱
tFestivalCardMonth_Text[3006877] = {}
tFestivalCardMonth_Text[3006877][1] = "Lucky day! You opened the %s, and received a +6 Stone!"
tFestivalCardMonth_Text[3006877][2] = "Lucky day! You opened the %s, and received a +5 Stone!"
tFestivalCardMonth_Text[3006877][3] = "Lucky day! You opened the %s, and received a +4 Stone!"
tFestivalCardMonth_Text[3006877][4] = "Lucky day! You opened the %s, and received a +3 Stone!"

----------------------------------------------【活跃包给圣物卡】------------------------------------------------
tFestivalCardMonth_Text["ActiveBag"] = "You received a(n) %s!"


------------------------------------------------【怪物提示】------------------------------------------------
tFestivalCardMonth_Text["Monster"] = {}
tFestivalCardMonth_Text["Monster"]["FullBag"] = "Your inventory is full. Please make some room, first."
tFestivalCardMonth_Text["Monster"]["Item"] = "You received a(n) %s!"
tFestivalCardMonth_Text[3949] = {}
tFestivalCardMonth_Text[3949]["ChgMap"] = "You successfully obtained 8 pieces of ultimate treasures, and was teleported out of the Heart of Olympus."


------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]打怪掉宝活动
--Purpose:	打怪掉宝活动
--Creator: 	郑鋆
--Created:	2015/05/10
------------------------------------------------------------------------------------
-- 中文索引表
tKillMonsterDropItem_Text = {}
-- 地宫守卫者
tKillMonsterDropItem_Text[18515] = {}
tKillMonsterDropItem_Text[18515]["111"] = "Thousands of years ago, spooky devils were imprisoned in the Underworld. Now, the seal of justice is weak,"
tKillMonsterDropItem_Text[18515]["112"] = "~and this area is turning into an evil lair. Heroes, come to suppress the devils, and win yourselves great treasures from October 22th to November 21th!"
tKillMonsterDropItem_Text[18515]["Option1"] = "Enter~the~Underworld."
tKillMonsterDropItem_Text[18515]["Option3"] = "Tell~me~more."
tKillMonsterDropItem_Text[18515]["Option4"] = "Sounds~thrilling."

tKillMonsterDropItem_Text[18515]["121"] = "People said calm has been restored in the Underworld, but I`ll still keep alert. Nobody knows when another crisis will come."
tKillMonsterDropItem_Text[18515]["Option5"] = "Good."

-- 玩家选1、没有2转就出个二次确认，有二转就不用
tKillMonsterDropItem_Text[18515]["211"] = "The Underworld is extremely dangerous. You`d better be 2nd-reborn to challenge. If you insist to go, that`s okay."
tKillMonsterDropItem_Text[18515]["Option6"] = "Yes.~I`ve~made~up~my~mind."
tKillMonsterDropItem_Text[18515]["Option9"] = "Wait,~I~haven`t~decided."

-- 前往九幽地宫失败、背包满
tKillMonsterDropItem_Text[18515]["221"] = "Your inventory is full. I suggest you to make some room, first. You know, you may receive treasures while hunting devils."
tKillMonsterDropItem_Text[18515]["Option7"] = "I`ll~do~it~now."

-- 详情介绍
tKillMonsterDropItem_Text[18515]["311"] = "The Underworld has 3 floors. The deeper you go, the better treasure you may discover. Killing the devil in the deepest will bring you"
tKillMonsterDropItem_Text[18515]["312"] = "~fabulous treasures like +6 Stones, P7 Dragon Souls, or rare Super Gems. The Underworld is so dangerous that we suggest heroes of"
tKillMonsterDropItem_Text[18515]["313"] = "~2nd rebirth to challenge it."
tKillMonsterDropItem_Text[18515]["Option8"] = "I~see."

tKillMonsterDropItem_Text["NineQuietFam"] = "This waste land is the Underworld."
tKillMonsterDropItem_Text["CloudGateClosed"] = "You were teleported to Desert."
tKillMonsterDropItem_Text["NineQuietFamOne"] = "Darkness is an eternal theme for the Underworld."
tKillMonsterDropItem_Text["NineQuietFamTwo"] = "The Underworld F2 delivers a stronger sense of terror."
tKillMonsterDropItem_Text["NineQuietFamThree"] = "The horrible atmosphere in the Underworld F3 makes the living hard to breathe."
tKillMonsterDropItem_Text["NoSpace"] = "Your inventory is too full to contain anything. Please make some room, first."

tKillMonsterDropItem_Text[7706] = "[%s] killed the Spooky Wolf in the Underworld, and received tons of rewards!"
tKillMonsterDropItem_Text[7707] = "[%s] killed the Spooky Wolf in the Underworld, and received tons of rewards!"
tKillMonsterDropItem_Text[7708] = "[%s] killed the Spooky Wolf in the Underworld, and received tons of rewards!"

-- 地宫宝物回收商
tKillMonsterDropItem_Text[18536] = {}
-- 活动中主对白
tKillMonsterDropItem_Text[18536]["111"] = "You know, I`m hooked to the Underworld treasures these days. If you have some, you can exchange them for Ghost Points with me."
tKillMonsterDropItem_Text[18536]["112"] = "~Every 1,000 Ghost Points can be swapped for 200 Chi Points."
tKillMonsterDropItem_Text[18536]["113"] = "~Let me see... Oh, you have %d Ghost Points, now."
tKillMonsterDropItem_Text[18536]["Option1"] = "Swap~Treasure~for~Ghost~Pts."
tKillMonsterDropItem_Text[18536]["Option2"] = "Swap~Ghost~Pts~for~Chi~Pts."
tKillMonsterDropItem_Text[18536]["Option3"] = "I`ll~think~about~it."

-- 活动后对白
tKillMonsterDropItem_Text[18536]["121"] = "I`ve been tired of the Underworld treasures. So, goodbye."
tKillMonsterDropItem_Text[18536]["Option4"] = "Goodbye."

-- 礼包兑换积分
tKillMonsterDropItem_Text[18536]["211"] = "I only accept the packs collected from the Underworld. So, which pack would you like to swap for the Ghost Points?"
tKillMonsterDropItem_Text[18536]["212"] = "~Different pack gives different amount of points."
tKillMonsterDropItem_Text[18536]["Option5"] = "RefinedGemPack->10Pts."
tKillMonsterDropItem_Text[18536]["Option6"] = "RareRefinedGemPack->80Pts."
tKillMonsterDropItem_Text[18536]["Option7"] = "SuperThunderGemPack->1600Pts."
tKillMonsterDropItem_Text[18536]["Option8"] = "SuperGloryGemPack->1600Pts."
tKillMonsterDropItem_Text[18536]["Option9"] = "SuperTortoiseGemPack->1600Pts."
tKillMonsterDropItem_Text[18536]["Option10"] = "DragonBallPack->240Pts."
tKillMonsterDropItem_Text[18536]["Option11"] = "Next~page."
tKillMonsterDropItem_Text[18536]["Option12"] = "I~want~other~things."

tKillMonsterDropItem_Text[18536]["Option13"] = "+1StonePack->25Pts."
tKillMonsterDropItem_Text[18536]["Option14"] = "+2StonePack->100Pts."
tKillMonsterDropItem_Text[18536]["Option15"] = "+3StonePack->300Pts."
tKillMonsterDropItem_Text[18536]["Option16"] = "+6StonePack->8000Pts."
tKillMonsterDropItem_Text[18536]["Option17"] = "Previous~page."
tKillMonsterDropItem_Text[18536]["Option18"] = "I~want~other~things."

-- 玩家身上无该礼包
tKillMonsterDropItem_Text[18536]["231"] = "Sorry, I can`t give you any Ghost Points since you don`t have such a pack with you."
tKillMonsterDropItem_Text[18536]["Option19"] = "Alright."

-- 二次确认
tKillMonsterDropItem_Text[18536]["241"] = "Are you sure you want to swap the %s for %d Ghost Points?"
tKillMonsterDropItem_Text[18536]["Option20"] = "Yes!"
tKillMonsterDropItem_Text[18536]["Option21"] = "No,~I~changed~my~mind."

-- 成功
tKillMonsterDropItem_Text[18536]["251"] = "Here are %d Ghost Points for you! You can swap every 1,000 Ghost Points for 200 Chi Points."
tKillMonsterDropItem_Text[18536]["Option22"] = "Thanks!"

-- 玩家选2、失败、积分不足
tKillMonsterDropItem_Text[18536]["311"] = "Sorry, you don`t 1,000 Ghost Points to swap for the Chi Points."
tKillMonsterDropItem_Text[18536]["Option23"] = "Alright."

tKillMonsterDropItem_Text[18536]["321"] = "You`ve already swapped Ghost Points for Chi Points 10 times, today. No more today, got it?"
tKillMonsterDropItem_Text[18536]["Option24"] = "I~see."

tKillMonsterDropItem_Text[18536]["331"] = "You have %d Ghost Points now. Are you sure you want to swap 1000 Ghost Points for 200 Chi Points?"
tKillMonsterDropItem_Text[18536]["Option25"] = "Yes!"
tKillMonsterDropItem_Text[18536]["Option26"] = "I~haven`t~decided,~yet!"

tKillMonsterDropItem_Text[18536]["341"] = "You successfully swapped for 200 Chi Points!"
tKillMonsterDropItem_Text[18536]["Option27"] = "Thanks!"
tKillMonsterDropItem_Text[18536]["Option28"] = "DragonSoulTradeScroll(1200pts)"
-- 九幽宝箱
tKillMonsterDropItem_Text[18537] = {}
-- 活动中主对白
tKillMonsterDropItem_Text[18537]["111"] = "This treasure box belongs to the Underworld Guard. From October 22th to November 21th, if you`re"
tKillMonsterDropItem_Text[18537]["112"] = "~able to kill 10 monsters in the Underworld, you can unlock this box and claim the treasure inside."
tKillMonsterDropItem_Text[18537]["113"] = "~Only once in a day."
tKillMonsterDropItem_Text[18537]["Option1"] = "Unlock~this~box."
tKillMonsterDropItem_Text[18537]["Option2"] = "Leave."

-- 活动后对白
tKillMonsterDropItem_Text[18537]["121"] = "The Underworld has returned to a calm, and this box is empty."
tKillMonsterDropItem_Text[18537]["Option3"] = "I~see."

-- 失败、需击杀10只
tKillMonsterDropItem_Text[18537]["211"] = "You should kill at least 10 monsters in the Underworld to unlock this box."
tKillMonsterDropItem_Text[18537]["Option4"] = "I~see."

-- 失败每天只能打开1次
tKillMonsterDropItem_Text[18537]["221"] = "You`ve already opened this box once, today."
tKillMonsterDropItem_Text[18537]["Option5"] = "Okay."

-- 失败每天只能打开1次
tKillMonsterDropItem_Text[18537]["231"] = "Your inventory is full. Please make some room, first."
tKillMonsterDropItem_Text[18537]["Option6"] = "Okay."


tKillMonsterDropItem_Text[18537][1] = "You received a Senior Training Pill (B)!"
tKillMonsterDropItem_Text[18537][2] = "You received a Protection Pill!"
tKillMonsterDropItem_Text[18537][3] = "You received a +2 Stone (B)!"
tKillMonsterDropItem_Text[18537][4] = "You received 2 Favored Training Pills (B)!"
tKillMonsterDropItem_Text[18537][5] = "You received 50 Chi Points!"
tKillMonsterDropItem_Text[18537][6] = "You received 10 CPs (B)!"
tKillMonsterDropItem_Text[18537][7] = "You received 60 Study Points!"

tKillMonsterDropItem_Text[18537]["MonsterNum"] = "You successfully killed 10 monsters in the Underworld. Go and claim your reward in the Underworld Treasure Box!"

--------------------------------------------------------------物品对白
-- 10分钟经验礼包
tKillMonsterDropItem_Text[3006531] = {}
tKillMonsterDropItem_Text[3006531]["Max"] = "Failed to open. You can open up to 10 EXP Packs (10mins) in a day."
tKillMonsterDropItem_Text[3006531]["Exp"] = "You received 10 minutes of EXP!"
tKillMonsterDropItem_Text[3006531]["Cultivation"] = "You received 5 Study Points!"

-- 30分钟经验礼包
tKillMonsterDropItem_Text[3006532] = {}
tKillMonsterDropItem_Text[3006532]["Max"] = "Failed to open. You can open up to 10 EXP Packs (30mins) in a day."
tKillMonsterDropItem_Text[3006532]["Exp"] = "You received 30 minutes of EXP!"
tKillMonsterDropItem_Text[3006532]["Cultivation"] = "You received 15 Study Points!"

-- 50分钟经验礼包
tKillMonsterDropItem_Text[3006533] = {}
tKillMonsterDropItem_Text[3006533]["Max"] = "Failed to open. You can open up to 10  EXP Packs (50mins) in a day."
tKillMonsterDropItem_Text[3006533]["Exp"] = "You received 50 minutes of EXP!"
tKillMonsterDropItem_Text[3006533]["Cultivation"] = "You received 25 Study Points!"

-- 10点修行值礼包
tKillMonsterDropItem_Text[3006534] = {}
tKillMonsterDropItem_Text[3006534]["UseItem"] = "You received 10 Study Points!"

-- 30点修行值礼包
tKillMonsterDropItem_Text[3006535] = {}
tKillMonsterDropItem_Text[3006535]["UseItem"] = "You received 30 Study Points!"

-- 50点修行值礼包
tKillMonsterDropItem_Text[3006536] = {}
tKillMonsterDropItem_Text[3006536]["UseItem"] = "You received 50 Study Points!"

-- 10点气力值礼包
tKillMonsterDropItem_Text[3006537] = {}
tKillMonsterDropItem_Text[3006537]["UseItem"] = "You received 10 Chi Points!"

-- 30点气力值礼包
tKillMonsterDropItem_Text[3006538] = {}
tKillMonsterDropItem_Text[3006538]["UseItem"] = "You received 30 Chi Points!"

-- 50点气力值礼包
tKillMonsterDropItem_Text[3006539] = {}
tKillMonsterDropItem_Text[3006539]["UseItem"] = "You received 50 Chi Points!"

-- 通神丹礼包
tKillMonsterDropItem_Text[3006540] = {}
tKillMonsterDropItem_Text[3006540]["UseItem"] = "You received a Special Training Pill (B)!"

-- 免费强炼丹礼包
tKillMonsterDropItem_Text[3006541] = {}
tKillMonsterDropItem_Text[3006541]["UseItem"] = "You received a Favored Training Pill (B)!"

-- 真气礼包
tKillMonsterDropItem_Text[3006542] = {}
tKillMonsterDropItem_Text[3006542]["UseItem"] = "You received 1 Talent for Jiang Hu training!"
tKillMonsterDropItem_Text[3006542]["NoCreateGongFu"] = "You should unlock the Jiang Hu system before you can use this pack."
tKillMonsterDropItem_Text[3006542]["Full"] = "Failed to use this pack. Your Talent of Jiang Hu is full."

-- 修炼礼包
tKillMonsterDropItem_Text[3006543] = {}
tKillMonsterDropItem_Text[3006543]["UseItem"] = "You received 1 Free Course for Jiang Hu training!"
tKillMonsterDropItem_Text[3006543]["NoCreateGongFu"] = "You should unlock the Jiang Hu system before you can use this pack."
tKillMonsterDropItem_Text[3006543]["Full"] = "Failed to use this pack. You have the maximum number of Free Courses."

-- 良品宝石礼包
tKillMonsterDropItem_Text[3006544] = {}
tKillMonsterDropItem_Text[3006544]["UseItem"] = "You received a(n) %s (B)!"

-- 良品稀有宝石礼包
tKillMonsterDropItem_Text[3006545] = {}
tKillMonsterDropItem_Text[3006545]["UseItem"] = "You received a(n) %s (B)!"
tKillMonsterDropItem_Text[3006545]["TalkBroadcast"] = "Surprise! %s killed a demon in the Underworld, and received a Refined Gem Pack!"

-- 优质天怒宝石礼包
tKillMonsterDropItem_Text[3006546] = {}
tKillMonsterDropItem_Text[3006546]["UseItem"] = "You received a Super Thunder Gem (B)!"
tKillMonsterDropItem_Text[3006546]["Broadcast"] = "Congratulations! %s killed a demon in the Underworld, and received a Super Thunder Gem Pack!"

-- 优质地灵宝石礼包
tKillMonsterDropItem_Text[3006547] = {}
tKillMonsterDropItem_Text[3006547]["UseItem"] = "You received a Super Glory Gem (B)!"
tKillMonsterDropItem_Text[3006547]["Broadcast"] = "Congratulations! %s killed a demon in the Underworld, and received a Super Glory Gem Pack!"

-- 优质玄元宝石礼包
tKillMonsterDropItem_Text[3006548] = {}
tKillMonsterDropItem_Text[3006548]["UseItem"] = "You received a Super Tortoise Gem (B)!"
tKillMonsterDropItem_Text[3006548]["Broadcast"] = "Congratulations! %s killed a demon in the Underworld, and received a Super Tortoise Gem Pack!"

-- 龙珠礼包
tKillMonsterDropItem_Text[3006549] = {}
tKillMonsterDropItem_Text[3006549]["UseItem"] = "You received a Dragon Ball (B)!"
tKillMonsterDropItem_Text[3006549]["TalkBroadcast"] = "Surprise! %s killed a demon in the Underworld, and received a Dragon Ball!"

-- +1赤炼石礼包
tKillMonsterDropItem_Text[3006551] = {}
tKillMonsterDropItem_Text[3006551]["UseItem"] = "You received a +1 Stone (B)!"

-- +2赤炼石礼包
tKillMonsterDropItem_Text[3006552] = {}
tKillMonsterDropItem_Text[3006552]["UseItem"] = "You received a +2 Stone (B)!"
tKillMonsterDropItem_Text[3006552]["TalkBroadcast"] = "Surprise! %s killed a demon in the Underworld, and received a +2 Stone Pack!"

-- +3赤炼石礼包
tKillMonsterDropItem_Text[3006553] = {}
tKillMonsterDropItem_Text[3006553]["UseItem"] = "You received a +3 Stone (B)!"
tKillMonsterDropItem_Text[3006553]["Broadcast"] = "Surprise! %s killed a demon in the Underworld, and received a +3 Stone Pack!"

-- +6赤炼石礼包
tKillMonsterDropItem_Text[3006554] = {}
tKillMonsterDropItem_Text[3006554]["UseItem"] = "You received a +6 Stone (B)!"
tKillMonsterDropItem_Text[3006554]["Broadcast"] = "Congratulations! %s killed a demon in the Underworld, and received a +6 Stone Pack!"

-- 九幽藏宝图
tKillMonsterDropItem_Text[3006699] = {}
tKillMonsterDropItem_Text[3006699]["111"] = "This ancient map will lead you to the entrance of the Underworld. Heroes, go subdue the"
tKillMonsterDropItem_Text[3006699]["112"] = "~demons in the Underworld from October 22th to November 21th, and earn yourself great rewards. Killing the devil in the deepest"
tKillMonsterDropItem_Text[3006699]["113"] = "~will bring you fabulous treasures like +6 Stones, P7 Dragon Souls, Super Thunder Gems, or Super Glory Gem."
tKillMonsterDropItem_Text[3006699]["Option1"] = "Enter~the~Underworld."

tKillMonsterDropItem_Text[3006699]["BeOverdue"] = "The magic on this map has vanished, and you threw it away."
tKillMonsterDropItem_Text[3006699]["RewardExp"] = "You read the treasure map, and received 30 minutes of EXP!"
tKillMonsterDropItem_Text[3006699]["RewardCultivation"] = "As you have reached the max level, you received 15 Study Points instead!"

-- 七阶武器神魂礼包
tKillMonsterDropItem_Text[3006745] = {}
tKillMonsterDropItem_Text[3006745]["Broadcast"] = "Congratulations! %s killed a demon in the Underworld, and received a P7 Weapon Soul Pack (B)!"
tKillMonsterDropItem_Text[3006745]["111"] = "The following Dragon Souls (B) are used for weapon. Which one would you like to claim?"
tKillMonsterDropItem_Text[3006745]["Option1"] = "MonsterSaber[1-handedWeapon]."
tKillMonsterDropItem_Text[3006745]["Option2"] = "SkyHammer[1-handedWeapon]."
tKillMonsterDropItem_Text[3006745]["Option3"] = "SkyHalberd[2-handedWeapon]."
tKillMonsterDropItem_Text[3006745]["Option4"] = "RepentRapier[Rapier]."
tKillMonsterDropItem_Text[3006745]["Option5"] = "DeathPistol[Pistol]."
tKillMonsterDropItem_Text[3006745]["Option6"] = "ShadowKatana[Katana]."
tKillMonsterDropItem_Text[3006745]["Option7"] = "Next~page."
tKillMonsterDropItem_Text[3006745]["Option8"] = "I~need~to~think~it~over."

tKillMonsterDropItem_Text[3006745]["Option9"] = "GhostKnife[ThrowingKnife]."
tKillMonsterDropItem_Text[3006745]["Option10"] = "DemonScythe[Scythe]."
tKillMonsterDropItem_Text[3006745]["Option11"] = "SpiritShield[Shield]."
tKillMonsterDropItem_Text[3006745]["Option12"] = "TimeBacksword[Backsword]."
tKillMonsterDropItem_Text[3006745]["Option13"] = "SunBow[Bow]."
tKillMonsterDropItem_Text[3006745]["Option14"] = "Previous~page."
tKillMonsterDropItem_Text[3006745]["Option15"] = "Next~page."

tKillMonsterDropItem_Text[3006745]["Option16"] = "BuddaBeads[Beads]."
tKillMonsterDropItem_Text[3006745]["Option17"] = "WarCraze[Nunchaku]."
tKillMonsterDropItem_Text[3006745]["Option18"] = "WonderHossu[Hossu]."
tKillMonsterDropItem_Text[3006745]["Option19"] = "Previous~page."

tKillMonsterDropItem_Text[3006745]["211"] = " Are you sure you want to claim this bound Dragon Soul, Monster Saber [1-handed Weapon]?"
tKillMonsterDropItem_Text[3006745]["Option20"] = "Yes."
tKillMonsterDropItem_Text[3006745]["Option21"] = "No."

tKillMonsterDropItem_Text[3006745]["221"] = " Are you sure you want to claim this bound Dragon Soul, Sky Hammer [1-handed Weapon]?"
tKillMonsterDropItem_Text[3006745]["Option22"] = "Yes."

tKillMonsterDropItem_Text[3006745]["231"] = " Are you sure you want to claim this bound Dragon Soul, Sky Halberd [2-handed Weapon]?"
tKillMonsterDropItem_Text[3006745]["Option23"] = "Yes."

tKillMonsterDropItem_Text[3006745]["241"] = " Are you sure you want to claim this bound Dragon Soul, Repent Rapier [Rapier]?"
tKillMonsterDropItem_Text[3006745]["Option24"] = "Yes."

tKillMonsterDropItem_Text[3006745]["251"] = " Are you sure you want to claim this bound Dragon Soul, Death Pistol [Pistol]?"
tKillMonsterDropItem_Text[3006745]["Option25"] = "Yes."

tKillMonsterDropItem_Text[3006745]["261"] = " Are you sure you want to claim this bound Dragon Soul, Shadow Katana [Katana]?"
tKillMonsterDropItem_Text[3006745]["Option26"] = "Yes."

tKillMonsterDropItem_Text[3006745]["311"] = " Are you sure you want to claim this bound Dragon Soul, Ghost Knife [ThrowingKnife]?"
tKillMonsterDropItem_Text[3006745]["Option27"] = "Yes."

tKillMonsterDropItem_Text[3006745]["321"] = " Are you sure you want to claim this bound Dragon Soul, Demon Scythe [Scythe]?"
tKillMonsterDropItem_Text[3006745]["Option28"] = "Yes."

tKillMonsterDropItem_Text[3006745]["331"] = " Are you sure you want to claim this bound Dragon Soul, Spirit Shield [Shield]?"
tKillMonsterDropItem_Text[3006745]["Option29"] = "Yes."

tKillMonsterDropItem_Text[3006745]["341"] = " Are you sure you want to claim this bound Dragon Soul, Time Backsword [Backsword]?"
tKillMonsterDropItem_Text[3006745]["Option30"] = "Yes."

tKillMonsterDropItem_Text[3006745]["351"] = " Are you sure you want to claim this bound Dragon Soul, Sun Bow [Bow]?"
tKillMonsterDropItem_Text[3006745]["Option31"] = "Yes."

tKillMonsterDropItem_Text[3006745]["411"] = " Are you sure you want to claim this bound Dragon Soul, Budda Beads [Beads]?"
tKillMonsterDropItem_Text[3006745]["Option32"] = "Yes."

tKillMonsterDropItem_Text[3006745]["421"] = " Are you sure you want to claim this bound Dragon Soul, War Craze [Nunchaku]?"
tKillMonsterDropItem_Text[3006745]["Option33"] = "Yes."

tKillMonsterDropItem_Text[3006745]["431"] = " Are you sure you want to claim this bound Dragon Soul, Wonder Hossu [Hossu]?"
tKillMonsterDropItem_Text[3006745]["Option34"] = "Yes."

tKillMonsterDropItem_Text[3006745][800020] = "You received a Dragon Soul, Monster Saber (B)!"
tKillMonsterDropItem_Text[3006745][800111] = "You received a Dragon Soul, Sky Hammer (B)!"
tKillMonsterDropItem_Text[3006745][800215] = "You received a Dragon Soul, Sky Halberd (B)!"
tKillMonsterDropItem_Text[3006745][800811] = "You received a Dragon Soul, Repent Rapier (B)!"
tKillMonsterDropItem_Text[3006745][800810] = "You received a Dragon Soul, Death Pistol (B)!"
tKillMonsterDropItem_Text[3006745][800142] = "You received a Dragon Soul, Shadow Katana (B)!"
tKillMonsterDropItem_Text[3006745][800917] = "You received a Dragon Soul, Ghost Knife (B)!"
tKillMonsterDropItem_Text[3006745][800255] = "You received a Dragon Soul, Demon Scythe (B)!"
tKillMonsterDropItem_Text[3006745][800422] = "You received a Dragon Soul, Spirit Shield (B)!"
tKillMonsterDropItem_Text[3006745][800522] = "You received a Dragon Soul, Time Backsword (B)!"
tKillMonsterDropItem_Text[3006745][800618] = "You received a Dragon Soul, Sun Bow (B)!"
tKillMonsterDropItem_Text[3006745][800725] = "You received a Dragon Soul, Budda Beads (B)!"
tKillMonsterDropItem_Text[3006745][801004] = "You received a Dragon Soul, War Craze (B)!"
tKillMonsterDropItem_Text[3006745][801104] = "You received a Dragon Soul, Wonder Hossu (B)!"

-- 七阶防具配饰神魂礼包（赠）
tKillMonsterDropItem_Text[3006746] = {}
tKillMonsterDropItem_Text[3006746]["Broadcast"] = "Congratulations! %s killed a demon in the Underworld, and received a P7 Armor Soul Pack (B)!"
tKillMonsterDropItem_Text[3006746]["111"] = "What kind of armor or accessories would you like the Dragon Soul (B) used for?"
tKillMonsterDropItem_Text[3006746]["Option1"] = "Headgear"
tKillMonsterDropItem_Text[3006746]["Option2"] = "Armor."
tKillMonsterDropItem_Text[3006746]["Option3"] = "Necklace,~Bag"
tKillMonsterDropItem_Text[3006746]["Option4"] = "Ring,~Heavy~Ring,~Bracelet."
tKillMonsterDropItem_Text[3006746]["Option5"] = "Boots."

tKillMonsterDropItem_Text[3006746]["211"] = "The following P7 Dragon Souls (B) are used for headgear. Which one would you like to claim?"
tKillMonsterDropItem_Text[3006746]["Option6"] = "MoonHeadgear[P-Immunity]."
tKillMonsterDropItem_Text[3006746]["Option7"] = "SunHeadgear[P-Anti-break]."
tKillMonsterDropItem_Text[3006746]["Option8"] = "StarHeadgear[M-Immunity]."
tKillMonsterDropItem_Text[3006746]["Option9"] = "IceHeadgear[M-Anti-break]."
tKillMonsterDropItem_Text[3006746]["Option10"] = "Previous~page."

tKillMonsterDropItem_Text[3006746]["221"] = "The following P7 Dragon Souls (B) are used for armor. Which one would you like to claim?"
tKillMonsterDropItem_Text[3006746]["Option11"] = "NetherArmor[P-Defense]."
tKillMonsterDropItem_Text[3006746]["Option12"] = "EclipseArmor[M-Defense]."

tKillMonsterDropItem_Text[3006746]["231"] = "The following P7 Dragon Souls (B) are used for Necklace or Bag. Which one would you like to claim?"
tKillMonsterDropItem_Text[3006746]["Option13"] = "FervorBag."
tKillMonsterDropItem_Text[3006746]["Option14"] = "HeavenNecklace."

tKillMonsterDropItem_Text[3006746]["241"] = "The following P7 Dragon Souls (B) are used for Ring, Heavy Ring or Bracelet. Which one would you like to claim?"
tKillMonsterDropItem_Text[3006746]["Option15"] = "CraneRing[Strike]."
tKillMonsterDropItem_Text[3006746]["Option16"] = "DragonRing[Break]."
tKillMonsterDropItem_Text[3006746]["Option17"] = "LionHeavyRing[Strike]."
tKillMonsterDropItem_Text[3006746]["Option18"] = "TigerHeavyRing[Break]."
tKillMonsterDropItem_Text[3006746]["Option19"] = "RainbowBracelet."

tKillMonsterDropItem_Text[3006746]["251"] = "The following P7 Dragon Souls (B) are used for Boots. Which one would you like to claim?"
tKillMonsterDropItem_Text[3006746]["Option20"] = "FoxBoots[Strike]."
tKillMonsterDropItem_Text[3006746]["Option21"] = "DragonBoots[Break]."
tKillMonsterDropItem_Text[3006746]["Option22"] = "CraneBoots[M-Attack]."

tKillMonsterDropItem_Text[3006746]["311"] = " Are you sure you want to claim this bound Dragon Soul, Moon Headgear [P-Immunity]?"
tKillMonsterDropItem_Text[3006746]["Option23"] = "Yes."

tKillMonsterDropItem_Text[3006746]["321"] = " Are you sure you want to claim this bound Dragon Soul, Sun Headgear [P-Anti-break]?"
tKillMonsterDropItem_Text[3006746]["Option24"] = "Yes."

tKillMonsterDropItem_Text[3006746]["331"] = " Are you sure you want to claim this bound Dragon Soul, Star Headgear [M-Immunity]?"
tKillMonsterDropItem_Text[3006746]["Option25"] = "Yes."

tKillMonsterDropItem_Text[3006746]["341"] = " Are you sure you want to claim this bound Dragon Soul, Ice Headgear [M-Anti-break]?"
tKillMonsterDropItem_Text[3006746]["Option26"] = "Yes."

tKillMonsterDropItem_Text[3006746]["411"] = " Are you sure you want to claim this bound Dragon Soul, Nether Armor [P-Defense]?"
tKillMonsterDropItem_Text[3006746]["Option27"] = "Yes."

tKillMonsterDropItem_Text[3006746]["421"] = " Are you sure you want to claim this bound Dragon Soul, Eclipse Armor [M-Defense]?"
tKillMonsterDropItem_Text[3006746]["Option28"] = "Yes."

tKillMonsterDropItem_Text[3006746]["511"] = " Are you sure you want to claim this bound Dragon Soul, Fervor Bag?"
tKillMonsterDropItem_Text[3006746]["Option29"] = "Yes."

tKillMonsterDropItem_Text[3006746]["521"] = " Are you sure you want to claim this bound Dragon Soul, Heaven Necklace?"
tKillMonsterDropItem_Text[3006746]["Option30"] = "Yes."

tKillMonsterDropItem_Text[3006746]["611"] = " Are you sure you want to claim this bound Dragon Soul, Crane Ring [Strike]?"
tKillMonsterDropItem_Text[3006746]["Option31"] = "Yes."

tKillMonsterDropItem_Text[3006746]["621"] = " Are you sure you want to claim this bound Dragon Soul, Dragon Ring [Break]?"
tKillMonsterDropItem_Text[3006746]["Option32"] = "Yes."

tKillMonsterDropItem_Text[3006746]["631"] = " Are you sure you want to claim this bound Dragon Soul, Lion Heavy Ring [Strike]?"
tKillMonsterDropItem_Text[3006746]["Option33"] = "Yes."

tKillMonsterDropItem_Text[3006746]["641"] = " Are you sure you want to claim this bound Dragon Soul, Tiger Heavy Ring [Break]?"
tKillMonsterDropItem_Text[3006746]["Option34"] = "Yes."

tKillMonsterDropItem_Text[3006746]["651"] = " Are you sure you want to claim this bound Dragon Soul, Rainbow Bracelet?"
tKillMonsterDropItem_Text[3006746]["Option35"] = "Yes."

tKillMonsterDropItem_Text[3006746]["711"] = " Are you sure you want to claim this bound Dragon Soul, Fox Boots [Strike]?"
tKillMonsterDropItem_Text[3006746]["Option36"] = "Yes."

tKillMonsterDropItem_Text[3006746]["721"] = " Are you sure you want to claim this bound Dragon Soul, Dragon Boots [Break]?"
tKillMonsterDropItem_Text[3006746]["Option37"] = "Yes."

tKillMonsterDropItem_Text[3006746]["731"] = " Are you sure you want to claim this bound Dragon Soul, Crane Boots [M-Attack]?"
tKillMonsterDropItem_Text[3006746]["Option38"] = "Yes."
tKillMonsterDropItem_Text[3006746]["Option39"] = "No."

tKillMonsterDropItem_Text[3006746][820073] = "You received a Dragon Soul, Moon Headgear (B)!"
tKillMonsterDropItem_Text[3006746][820074] = "You received a Dragon Soul, Sun Headgear (B)!"
tKillMonsterDropItem_Text[3006746][820075] = "You received a Dragon Soul, Star Headgear (B)!"
tKillMonsterDropItem_Text[3006746][820076] = "You received a Dragon Soul, Ice Headgear (B)!"
tKillMonsterDropItem_Text[3006746][822071] = "You received a Dragon Soul, Nether Armor (B)!"
tKillMonsterDropItem_Text[3006746][822072] = "You received a Dragon Soul, Eclipse Armor (B)!"
tKillMonsterDropItem_Text[3006746][821034] = "You received a Dragon Soul, Fervor Bag (B)!"
tKillMonsterDropItem_Text[3006746][821033] = "You received a Dragon Soul, Heaven Necklace (B)!"
tKillMonsterDropItem_Text[3006746][823058] = "You received a Dragon Soul, Crane Ring (B)!"
tKillMonsterDropItem_Text[3006746][823059] = "You received a Dragon Soul, Dragon Ring (B)!"
tKillMonsterDropItem_Text[3006746][823061] = "You received a Dragon Soul, Lion Heavy Ring (B)!"
tKillMonsterDropItem_Text[3006746][823062] = "You received a Dragon Soul, Tiger Heavy Ring (B)!"
tKillMonsterDropItem_Text[3006746][823060] = "You received a Dragon Soul, Rainbow Bracelet (B)!"
tKillMonsterDropItem_Text[3006746][824018] = "You received a Dragon Soul, Fox Boots (B)!"
tKillMonsterDropItem_Text[3006746][824019] = "You received a Dragon Soul, Dragon Boots (B)!"
tKillMonsterDropItem_Text[3006746][824020] = "You received a Dragon Soul, Crane Boots (B)!"

-- 六阶神魂随机包
tKillMonsterDropItem_Text[3003382] = {}
tKillMonsterDropItem_Text[3003382]["Broadcast"] = "Surprise! %s killed a demon in the Underworld, and received a P6 Random Soul Pack!"

-- 九幽神魂礼包洗赠卷
tKillMonsterDropItem_Text[3006550] = {}
tKillMonsterDropItem_Text[3006550]["111"] = "When you make a pack unbound, the Dragon Soul in the pack will also be unbound. So, which pack would you like to make it unbound?"
tKillMonsterDropItem_Text[3006550]["Option1"] = "P7~Weapon~Soul~Pack~(B)."
tKillMonsterDropItem_Text[3006550]["Option2"] = "P7~Armor~Soul~Pack~(B)"
tKillMonsterDropItem_Text[3006550][3006745] = "Make sure you have a P7 Weapon Soul Pack (B) with you, first."
tKillMonsterDropItem_Text[3006550][3006746] = "Make sure you have a P7 Armor Soul Pack (B) with you, first."
tKillMonsterDropItem_Text[3006550][3004247] = "You successfully made the P7 Weapon Soul Pack unbound!"
tKillMonsterDropItem_Text[3006550][3004248] = "You successfully made the P7 Armor Soul Pack unbound!"
tKillMonsterDropItem_Text[3006550]["NoItem"] = "Make sure you have a Dragon Soul Trade Scroll with you, first."
tKillMonsterDropItem_Text[3006550]["TalkBroadcast"] = "Surprise! %s killed a demon in the Underworld, and received a Dragon Soul Trade Scroll!"

-- 七阶武器神魂礼包碎片（赠）
tKillMonsterDropItem_Text[3006765] = {}
tKillMonsterDropItem_Text[3006765]["NoItem"] = "Failed to combine. You don`t have 15 P7 Weapon Soul Scraps (B)."
tKillMonsterDropItem_Text[3006765]["CompoundItem"] = "You received a P7 Weapon Soul Pack (B)!"
tKillMonsterDropItem_Text[3006765]["UserTalkChannel"] = "You received a P7 Weapon Soul Pack (B)!"
tKillMonsterDropItem_Text[3006765]["TalkBroadcast"] = "Surprise! %s killed a demon in the Underworld, and received a P7 Weapon Soul Scrap (B)!"

-- 七阶防具配饰神魂礼包碎片（赠）
tKillMonsterDropItem_Text[3006766] = {}
tKillMonsterDropItem_Text[3006766]["NoItem"] = "Failed to combine. You don`t have 15 P7 Armor Soul Scraps (B)."
tKillMonsterDropItem_Text[3006766]["CompoundItem"] = "You received a P7 Armor Soul Pack (B)!"
tKillMonsterDropItem_Text[3006766]["TalkBroadcast"] = "Surprise! %s killed a demon in the Underworld, and received a P7 Armor Soul Scrap (B)!"

------------------------------------------------------------------------------------
----------------------------------------------------------------------------
--Name:		[征服][功能脚本]背包信.lua
--Purpose:	背包信
--Creator: 	郑鋆
--Created:	2015/05/27
----------------------------------------------------------------------------
tBackpackLetter_Text[3006699] = {}
tBackpackLetter_Text[3006699]["NoSpace"] = "Your inventory is full. Please make some room, and login again to receive an Underworld Map."
tBackpackLetter_Text[3006699]["RewardItem"] = "You received an Underworld Map. Hurry and check it in your inventory."





------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]大漠寻宝活动
--Purpose:	大漠寻宝活动
--Creator: 	吴文鑫
--Created:	2015/07/28
------------------------------------------------------------------------------------
-- 中文索引表
tTheTreasureHunt_Text = {}
tTheTreasureHunt_Text["NoSpace"] = "Your inventory is full. Please make some room, first."
tTheTreasureHunt_Text["NoEmoney"] = "You don`t have enough CPs to buy the Bravery Amulet."
tTheTreasureHunt_Text["EmoneyMonoLimit"] = "You`re carrying the maximum amount of CPs (B). You need to spend some, first."
tTheTreasureHunt_Text["MoneyLimit"] = "You`re carrying the maximum amount of Silver. Please spend or deposit some, first."
tTheTreasureHunt_Text["EnterCave"] = "You`ve entered the Dragon Lair where countless treasures were buried in the deep. You can buy a Bravery Amulet from the Dark Explorer to boost your attack ability."
tTheTreasureHunt_Text["LeaveCave"] = "You left the Dragon Lair for Twin City."
tTheTreasureHunt_Text["LeaveCave1"] = "You`ve left the Dragon Lair with a heavy bag of treasures."
tTheTreasureHunt_Text["BeOverdue"] = "The Dragon Lair has collapsed, and this item is useless now."
tTheTreasureHunt_Text["BeOverPower"] = "You can`t receive the rewards since your Battle Power is beyond the limit of this stage."
tTheTreasureHunt_Text["BeOverPower1"] = "You can't continue to challenge since your Battle Power exceeds the limit of this stage."
tTheTreasureHunt_Text["BeOverRank"] = "You`ve already challenged the monsters of this stage. There is no reward."
tTheTreasureHunt_Text["BuyCharacter"] = "You paid 1 CP for a Bravery Amulet. increasing Final P-Attack and Final M-Attack by 1000, reducing Final P-Damage and Final M-Damage by 1000, and enhancing P-Strike and M-Strike by 10%."
tTheTreasureHunt_Text["None"] = "None"

tTheTreasureHunt_Text["Killboss"] = "Excellent! You killed %s and won the stage treasure! Talk to the Dragon Lair Guide to leave."

-- tTheTreasureHunt_Text["BraveUse"] = "You activated the Bravery Amulet,"
tTheTreasureHunt_Text["BraveNoUse"] = "You can only use the Bravery Amulet in the Dragon Lair."
tTheTreasureHunt_Text["HaveBrave"] = "You`ve activated the power of Bravery Amulet. No need to buy more amulets."
tTheTreasureHunt_Text["NpcName"] = {}
tTheTreasureHunt_Text["NpcName"][1] = "DarkExplorer(Normal)"
tTheTreasureHunt_Text["NpcName"][2] = "DarkExplorer(Hard)"
tTheTreasureHunt_Text["NpcName"][3] = "DarkExplorer(Expert)"


--------------------------------------------------------------NPC对白
--活动前
tTheTreasureHunt_Text[18711] = {}
tTheTreasureHunt_Text[18711]["111"] = "The Dragon Lair has long been famous for its fabulous treasures, as well as furious monsters."
tTheTreasureHunt_Text[18711]["112"] = "~For Level 100+ heroes who are expecting an exciting adventure, I welcome you to explore the Dragon Lair from August 31st to September 12th."
tTheTreasureHunt_Text[18711]["Option1"] = "I~love~challenge."
--活动后
tTheTreasureHunt_Text[18711]["121"] = "The Dragon Lair has collapsed. If I discover something good, I`ll call you."
tTheTreasureHunt_Text[18711]["122"] = ""
tTheTreasureHunt_Text[18711]["Option2"] = "Hope~I~didn`t~miss~anything."

--活动中未满足等级条件
tTheTreasureHunt_Text[18711]["131"] = "Thousands of people went to Dragon Lair for treasures, but very few of them can make a harvest."
tTheTreasureHunt_Text[18711]["132"] = "~If you want to take the challenge, you should reach at least Level 100. I don`t want to see you"
tTheTreasureHunt_Text[18711]["133"] = "~die in vain."
tTheTreasureHunt_Text[18711]["Option3"] = "Alright."

--活动中满足等级条件
tTheTreasureHunt_Text[18711]["141"] = "The Dragon Lair has long been famous for its fabulous treasures, as well as furious monsters."
tTheTreasureHunt_Text[18711]["142"] = "~To win these treasures, you should clear the stage fast enough. While opening the Rare Dragon Box,"
tTheTreasureHunt_Text[18711]["143"] = "~you have a chance to win valuable treasures like DB Scroll, +6 Steed and +6 Stone. Are you interested?"
tTheTreasureHunt_Text[18711]["144"] = "~The Dragon Lair Guide will give you a hand in Twin City from August 31st to September 12th."

tTheTreasureHunt_Text[18711]["Option4"] = "Challenge~the~Dragon~Lair."
tTheTreasureHunt_Text[18711]["Option5"] = "View~the~challenge~rankings."
-- tTheTreasureHunt_Text[18711]["Option6"] = "查询今日排行榜。"
tTheTreasureHunt_Text[18711]["Option7"] = "Tell~me~more."
tTheTreasureHunt_Text[18711]["Option8"] = "No,~I`m~not~interested."




----进入禁闭龙穴。
--今日已完成
tTheTreasureHunt_Text[18711]["211"] = "You`ve already challenged the Dragon Lair, today. Why not take a rest?" 
tTheTreasureHunt_Text[18711]["Option9"] = "Alright."

--今日已选择难度
tTheTreasureHunt_Text[18711]["221"] = "The seal on the Dragon Lair has recovered. To explore the lair, you need to start over,"
tTheTreasureHunt_Text[18711]["222"] = "~and you won`t be rewarded for the rounds you cleared before. Don`t hesitate."
tTheTreasureHunt_Text[18711]["Option10"] = "Enter~the~Dragon~Lair."

--今日未选择难度
tTheTreasureHunt_Text[18711]["231"] = "You can choose a stage to challenge the Dragon Lair according to your ability: \n"
tTheTreasureHunt_Text[18711]["232"] = "~Normal stage is for heroes whose Battle Power is less than 250;\n"
tTheTreasureHunt_Text[18711]["233"] = "~Hard stage is for heroes whose Battle Power is between 250 and 330;\n"
tTheTreasureHunt_Text[18711]["234"] = "~Expert stage is for heroes whose Battle Power is more than 330.\n"
tTheTreasureHunt_Text[18711]["235"] = "~Remember, challenging a stage that its Battle Power limit is lower than yours will bring no reward."
tTheTreasureHunt_Text[18711]["Option11"] = "Normal~stage~(BP:~250-)."
tTheTreasureHunt_Text[18711]["Option12"] = "Hard~stage~(BP:~250-330)."
tTheTreasureHunt_Text[18711]["Option13"] = "Expert~stage~(BP:~330+)."
tTheTreasureHunt_Text[18711]["Option14"] = "I`ll~think~it~over."

--创建副本
--组队状态
tTheTreasureHunt_Text[18711]["241"] = "You can`t challenge the Dragon Lair while you`re in a team."
tTheTreasureHunt_Text[18711]["Option15"] = "I~see."
--不在NPC所在地图
tTheTreasureHunt_Text[18711]["251"] = "You can`t enter the Dragon Lair from this place."


--战力不符

tTheTreasureHunt_Text[18711]["261"] = "Your Battle Power is beyond the limit of the chosen stage. Even you clear this stage, you won`t get any rewards. Are you sure you want to continue?"
tTheTreasureHunt_Text[18711]["Option17"] = "Yes."
tTheTreasureHunt_Text[18711]["Option18"] = "Wait,~I~changed~my~mind."

tTheTreasureHunt_Text[18711]["271"] = "Your Battle Power is beyond the limit of the chosen stage. Even you clear this stage, you won`t get any rewards. Are you sure you want to continue?"
tTheTreasureHunt_Text[18711]["Option19"] = "Yes."

tTheTreasureHunt_Text[18711]["311"] = "You guys really amazed me in the challenge of Dragon Lair. Well, yesterday`s rankings have come out."
tTheTreasureHunt_Text[18711]["312"] = "~Which one do you want to check?"
tTheTreasureHunt_Text[18711]["Option20"] = "Normal~stage~ranking."
tTheTreasureHunt_Text[18711]["Option21"] = "Hard~stage~ranking."
tTheTreasureHunt_Text[18711]["Option22"] = "Expert~stage~ranking."
tTheTreasureHunt_Text[18711]["Option40"] = "What`re~the~prizes?"
tTheTreasureHunt_Text[18711]["Option23"] = "I`ll~talk~to~you~later."

tTheTreasureHunt_Text[18711]["321"] = "Normal Stage Ranking\n"
tTheTreasureHunt_Text[18711]["322"] = "1st Place: %s\n"
tTheTreasureHunt_Text[18711]["323"] = "2nd Place: %s\n"
tTheTreasureHunt_Text[18711]["324"] = "3rd Place: %s"
tTheTreasureHunt_Text[18711]["Option24"] = "Claim~my~prize."
tTheTreasureHunt_Text[18711]["Option25"] = "View~other~rankings."
tTheTreasureHunt_Text[18711]["Option26"] = "I`ll~talk~to~you~later."

tTheTreasureHunt_Text[18711]["331"] = "Hard Stage Ranking\n"
tTheTreasureHunt_Text[18711]["332"] = "1st Place: %s\n"
tTheTreasureHunt_Text[18711]["333"] = "2nd Place: %s\n"
tTheTreasureHunt_Text[18711]["334"] = "3rd Place: %s"
tTheTreasureHunt_Text[18711]["Option27"] = "Claim~my~prize."

tTheTreasureHunt_Text[18711]["341"] = "Expert Stage Ranking\n"
tTheTreasureHunt_Text[18711]["342"] = "1st Place: %s\n"
tTheTreasureHunt_Text[18711]["343"] = "2nd Place: %s\n"
tTheTreasureHunt_Text[18711]["344"] = "3rd Place: %s"
tTheTreasureHunt_Text[18711]["Option28"] = "Claim~my~prize."


tTheTreasureHunt_Text[18711]["351"] = "Sorry, I can`t give you any prizes. I didn`t see your name on the list. "
tTheTreasureHunt_Text[18711]["Option29"] = "Sorry,~my~fault."

tTheTreasureHunt_Text[18711]["361"] = "You`ve already claimed this prize, today."
tTheTreasureHunt_Text[18711]["Option30"] = "Alright."

tTheTreasureHunt_Text[18711]["371"] = "My heroes, your inventory is too full to contain this prize. Could you make some room, first?"
tTheTreasureHunt_Text[18711]["Option31"] = "I`ll~do~it~now."

tTheTreasureHunt_Text[18711]["381"] = "You ranked No. %d in the normal challenge stage yesterday. I saw you killing twisted Phantom in the lair so fast."
tTheTreasureHunt_Text[18711]["382"] = "~To show my respect, I`ll give you all treasures I have."
tTheTreasureHunt_Text[18711]["Option32"] = "Thanks!"

tTheTreasureHunt_Text[18711]["391"] = "You ranked No. %d in the hard challenge stage yesterday. I saw you killing twisted Phantom in the lair so fast."
tTheTreasureHunt_Text[18711]["392"] = "~To show my respect, I`ll give you all treasures I have."

tTheTreasureHunt_Text[18711]["3101"] = "You ranked No. %d in the expert challenge stage yesterday. I saw you killing twisted Phantom in the lair so fast."
tTheTreasureHunt_Text[18711]["3102"] = "~To show my respect, I`ll give you all treasures I have."


tTheTreasureHunt_Text[18711]["411"] = "In the challenge, the top 3 heroes who eliminate the Phantom in the lair with the shortest time will be nicely rewarded."
tTheTreasureHunt_Text[18711]["412"] = "~We refresh the ranking at 00:00 everyday. So, which ranking would you like to check?"
tTheTreasureHunt_Text[18711]["Option33"] = "Normal~stage~ranking."
tTheTreasureHunt_Text[18711]["Option34"] = "Hard~stage~ranking."
tTheTreasureHunt_Text[18711]["Option35"] = "Expert~stage~ranking."
-- tTheTreasureHunt_Text[18711]["Option36"] = "暂不查询。"

tTheTreasureHunt_Text[18711]["421"] = "Normal Stage Ranking\n"
tTheTreasureHunt_Text[18711]["422"] = "1st Place: %s\n"
tTheTreasureHunt_Text[18711]["423"] = "2nd Place: %s\n"
tTheTreasureHunt_Text[18711]["424"] = "3rd Place: %s"
tTheTreasureHunt_Text[18711]["Option37"] = "View~other~rankings."

tTheTreasureHunt_Text[18711]["425"] = "1st Place: %s    %ds  \n"
tTheTreasureHunt_Text[18711]["426"] = "2nd Place: %s    %ds  \n"
tTheTreasureHunt_Text[18711]["427"] = "3rd Place: %s    %ds  "

tTheTreasureHunt_Text[18711]["431"] = "Hard Stage Ranking\n"
tTheTreasureHunt_Text[18711]["432"] = "1st Place: %s\n"
tTheTreasureHunt_Text[18711]["433"] = "2nd Place: %s\n"
tTheTreasureHunt_Text[18711]["434"] = "3rd Place: %s"

tTheTreasureHunt_Text[18711]["435"] = "1st Place: %s    %ds  \n"
tTheTreasureHunt_Text[18711]["436"] = "2nd Place: %s    %ds  \n"
tTheTreasureHunt_Text[18711]["437"] = "3rd Place: %s    %ds  "

tTheTreasureHunt_Text[18711]["441"] = "Expert Stage Ranking\n"
tTheTreasureHunt_Text[18711]["442"] = "1st Place: %s\n"
tTheTreasureHunt_Text[18711]["443"] = "2nd Place: %s\n"
tTheTreasureHunt_Text[18711]["444"] = "3rd Place: %s"

tTheTreasureHunt_Text[18711]["445"] = "1st Place: %s    %ds  \n"
tTheTreasureHunt_Text[18711]["446"] = "2nd Place: %s    %ds  \n"
tTheTreasureHunt_Text[18711]["447"] = "3rd Place: %s    %ds  "

tTheTreasureHunt_Text[18711]["511"] = "The challenge starts as soon as you choose a stage and enter the Dragon Lair with the lair guide`s help."
tTheTreasureHunt_Text[18711]["512"] = "~At the 1st round, you`re asked to unlock all the Treasure Chests within 30 seconds. If you fail,"
tTheTreasureHunt_Text[18711]["513"] = "~these chests will turn into brutal knights, reducing the reward by half."
tTheTreasureHunt_Text[18711]["Option38"] = "How~to~clear~this~stage?"
-- tTheTreasureHunt_Text[18711]["Option39"] = "奖励详情。"
-- tTheTreasureHunt_Text[18711]["Option41"] = "查看其它。"
-- tTheTreasureHunt_Text[18711]["Option42"] = "知道了。"

tTheTreasureHunt_Text[18711]["521"] = "There are totally 8 rounds in this stage. If you spend more than 4 minutes in breaking through them, you`ll awaken the Phantom of Evil Lord."
tTheTreasureHunt_Text[18711]["522"] = "~If less than 4 minutes, the Phantom of Evil King will appear. As far as know, the King of Evil Spirit is carrying better rewards."
tTheTreasureHunt_Text[18711]["523"] = "~Don`t let your enemy just go."
tTheTreasureHunt_Text[18711]["Option43"]  = "What`re~the~rewards?"
-- tTheTreasureHunt_Text[18711]["Option44"]  = "原来如此。"

tTheTreasureHunt_Text[18711]["531"] = "Different stages give different rewards. Additionally, you may receive Dragon Box Scraps during the challenge."
tTheTreasureHunt_Text[18711]["532"] = "~You can combine 10 scraps into a Dragon Box. If you get 10 Dragon Boxes, I suggest you to upgrade them"
tTheTreasureHunt_Text[18711]["533"] = "~into a Rare Dragon Box. Look, I have a reward list for each stage. Which one are you interested?"
tTheTreasureHunt_Text[18711]["Option45"] = "Normal~stage."
tTheTreasureHunt_Text[18711]["Option46"] = "Hard~stage."
tTheTreasureHunt_Text[18711]["Option47"] = "Expert~stage."
tTheTreasureHunt_Text[18711]["Option48"] = "Let~me~see."
-- tTheTreasureHunt_Text[18711]["Option49"] = "无需了解。"
 
tTheTreasureHunt_Text[18711]["541"] = "Different stages give different rewards. Which one do you want to know?"
tTheTreasureHunt_Text[18711]["Option50"] = "Normal~stage."
tTheTreasureHunt_Text[18711]["Option51"] = "Hard~stage."
tTheTreasureHunt_Text[18711]["Option52"] = "Expert~stage."
tTheTreasureHunt_Text[18711]["Option53"] = "I~have~something~else~to~ask."
-- tTheTreasureHunt_Text[18711]["Option54"] = "无需了解。"


tTheTreasureHunt_Text[18711]["551"] = "The rewards for the normal stage\n"
tTheTreasureHunt_Text[18711]["552"] = "Unlocking the Treasure Chests on the first 8 rounds: random treasures like Dragon Box Scraps.\n"
tTheTreasureHunt_Text[18711]["553"] = "Smashing the Lord`s Phantom: 5 Dragon Box Scraps, 45-minute EXP, 30 Study Points and 150,000 Sliver.\n"
tTheTreasureHunt_Text[18711]["554"] = "Smashing the King`s Phantom: 10 Dragon Box Scraps, 90-minute EXP, 60 Study Points and 300,000 Sliver."
tTheTreasureHunt_Text[18711]["Option55"] = "Learn~about~something~else."
-- tTheTreasureHunt_Text[18711]["Option56"] = "知道了。"

tTheTreasureHunt_Text[18711]["561"] = "The rewards for the hard stage\n"
tTheTreasureHunt_Text[18711]["562"] = "Unlocking the Treasure Chests on the first 8 rounds: random treasures like Dragon Box Scraps and Dragon Boxes.\n"
tTheTreasureHunt_Text[18711]["563"] = "Smashing the Lord`s Phantom: 1 Dragon Box, 90-minute EXP, 60 Study Points and 300,000 Sliver.\n"
tTheTreasureHunt_Text[18711]["564"] = "Smashing the King`s Phantom: 2 Dragon Boxes, 180-minute EXP, 120 Study Points and 600,000 Sliver."

tTheTreasureHunt_Text[18711]["571"] = "The rewards for the expert stage\n"
tTheTreasureHunt_Text[18711]["572"] = "Unlocking the Treasure Chests on the first 8 rounds: random treasures like Dragon Box Scraps and Dragon Boxes.\n"
tTheTreasureHunt_Text[18711]["573"] = "Smashing the Lord`s Phantom: 7 Dragon Box Scraps, 1 Dragon Box, 90-minute EXP, 60 Study Points, 60 Chi Points and 300,000 Sliver.\n"
tTheTreasureHunt_Text[18711]["574"] = "Smashing the King`s Phantom: 4 Dragon Box Scraps, 3 Dragon Boxes, 180-minute EXP, 120 Study Points, 120 Chi Points and 600,000 Sliver."

tTheTreasureHunt_Text[18711]["581"] = "The rewards for Top 3 winners on the normal stage\n"
tTheTreasureHunt_Text[18711]["582"] = "1st Place: 300 Study Points and 5 EXP Balls (B).\n"
tTheTreasureHunt_Text[18711]["583"] = "2nd Place: 200 Study Points and 4 EXP Balls (B).\n"
tTheTreasureHunt_Text[18711]["584"] = "3rd Place: 100 Study Points and 3 EXP Balls (B)."
tTheTreasureHunt_Text[18711]["Option57"] = "Learn~about~other~stages."


tTheTreasureHunt_Text[18711]["591"] = "The rewards for Top 3 winners on the hard stage\n"
tTheTreasureHunt_Text[18711]["592"] = "1st Place: 500 Chi Points and 5 Special Training Pills (B).\n"
tTheTreasureHunt_Text[18711]["593"] = "2nd Place: 400 Chi Points and 4 Special Training Pills (B).\n"
tTheTreasureHunt_Text[18711]["594"] = "3rd Place: 300 Chi Points and 3 Special Training Pills (B)."

tTheTreasureHunt_Text[18711]["5101"] = "The rewards for Top 3 winners on the expert stage\n"
tTheTreasureHunt_Text[18711]["5102"] = "1st Place: 1000 Chi Points and 5 Favored Training Pills (B).\n"
tTheTreasureHunt_Text[18711]["5103"] = "2nd Place: 800 Chi Points and 4 Favored Training Pills (B).\n"
tTheTreasureHunt_Text[18711]["5104"] = "3rd Place: 500 Chi Points and 3 Favored Training Pills (B)."

tTheTreasureHunt_Text[18711]["611"] = "You need to wait 1 minute before you can enter the Dragon Lair, again."
tTheTreasureHunt_Text[18711]["Option58"] = "Okay."

tTheTreasureHunt_Text[18711]["621"] = "Your Battle Power is lower than the limit of this stage. Are you sure you want to continue?"


tTheTreasureHunt_Text[18711]["711"] = "Which ranking do you want to check?"
tTheTreasureHunt_Text[18711]["Option62"] = "Prizes~for~yesterday`s~Top~3."
tTheTreasureHunt_Text[18711]["Option63"] = "View~today`s~ranking."
tTheTreasureHunt_Text[18711]["Option64"] = "I`ll~talk~to~you~later."




tTheTreasureHunt_Text[18712] = {}
tTheTreasureHunt_Text[18712]["111"] = "There is nothing in the Dragon Lair. Are you confused? Haha, the Lair Knights must have buried the treasures."
tTheTreasureHunt_Text[18712]["112"] = "~Be careful, these knights are hiding in the darkness."
tTheTreasureHunt_Text[18712]["Option1"] = "It`s~horrible."

tTheTreasureHunt_Text[18712]["121"] = "Another name for Dragon Lair is DANGER! The lair owner is a mighty Phantom who possesses the most valuable treasures. You`re asked to eliminate it."
tTheTreasureHunt_Text[18712]["122"] = "~Look, I just made some Bravery Amulets, 1 CP for each."
tTheTreasureHunt_Text[18712]["123"] = "~You can use it to boost your attack ability for 5 minutes in the lair."
-- tTheTreasureHunt_Text[18712]["Option2"] = "开始闯关。"
tTheTreasureHunt_Text[18712]["Option3"] = "Buy~1~Bravery~Amulet.~(1~CP)"
tTheTreasureHunt_Text[18712]["Option4"] = "I~want~to~leave."
-- tTheTreasureHunt_Text[18712]["Option5"] = "了解龙穴详情。"
-- tTheTreasureHunt_Text[18712]["Option6"] = "我先四处看看。"

-- tTheTreasureHunt_Text[18712]["211"] = "    开始闯关后，阁下需在30秒内奋力敲击第一轮的宝箱来开启奖励，"
-- tTheTreasureHunt_Text[18712]["212"] = "若30秒后未全部开启，则它们会变身为龙穴侍卫，"
-- tTheTreasureHunt_Text[18712]["213"] = "龙穴侍卫的奖励只有宝箱的一半。"

tTheTreasureHunt_Text[18712]["131"] = "You successfully broke through this stage and won the ultimate treasure. You deserve the title of super hero!"
tTheTreasureHunt_Text[18712]["132"] = "The twisted phantom has been eliminated, and the Dragon Lair may collapase anytime. You`d better get out of here quickly!"



tTheTreasureHunt_Text[18712]["311"] = "You haven`t completed the challenge. Just give up the ultimate treasures? If you leave now,"
tTheTreasureHunt_Text[18712]["312"] = "~you have to start over when you enter the lair again, and there is no reward for the rounds you completed before."
tTheTreasureHunt_Text[18712]["Option27"] = "I~have~to~leave."
tTheTreasureHunt_Text[18712]["Option28"] = "I`d~like~to~stay."

--------------------------------------------------------------刷怪提示

tTheTreasureHunt_MonsterNameText = {}
tTheTreasureHunt_MonsterNameText[7750] = "CommonIronChest(Normal)"
tTheTreasureHunt_MonsterNameText[7751] = "EliteIronChest(Normal)"
tTheTreasureHunt_MonsterNameText[7752] = "SuperIronChest(Normal)"
tTheTreasureHunt_MonsterNameText[7753] = "EpicIronChest(Normal)"
tTheTreasureHunt_MonsterNameText[7754] = "CommonBronzeChest(Normal)"
tTheTreasureHunt_MonsterNameText[7755] = "EliteBronzeChest(Normal)"
tTheTreasureHunt_MonsterNameText[7756] = "SuperBronzeChest(Normal)"
tTheTreasureHunt_MonsterNameText[7757] = "EpicBronzeChest(Normal)"
tTheTreasureHunt_MonsterNameText[7758] = "CommonIronKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7759] = "EliteIronKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7760] = "SuperIronKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7761] = "EpicIronKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7762] = "CommonBronzeKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7763] = "EliteBronzeKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7764] = "SuperBronzeKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7765] = "EpicBronzeKnight(Normal)"
tTheTreasureHunt_MonsterNameText[7766] = "PhantomofDarkKing"
tTheTreasureHunt_MonsterNameText[7767] = "PhantomofDarkLord"
tTheTreasureHunt_MonsterNameText[7768] = "CommonBronzeChest(Hard)"
tTheTreasureHunt_MonsterNameText[7769] = "EliteBronzeChest(Hard)"
tTheTreasureHunt_MonsterNameText[7770] = "SuperBronzeChest(Hard)"
tTheTreasureHunt_MonsterNameText[7771] = "EpicBronzeChest(Hard)"
tTheTreasureHunt_MonsterNameText[7772] = "CommonSilverChest(Hard)"
tTheTreasureHunt_MonsterNameText[7773] = "EliteSilverChest(Hard)"
tTheTreasureHunt_MonsterNameText[7774] = "SuperSilverChest(Hard)"
tTheTreasureHunt_MonsterNameText[7775] = "EpicSilverChest(Hard)"
tTheTreasureHunt_MonsterNameText[7776] = "CommonBronzeKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7777] = "EliteBronzeKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7778] = "SuperBronzeKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7779] = "EpicBronzeKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7780] = "CommonSilverKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7781] = "EliteSilverKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7782] = "SuperSilverKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7783] = "EpicSilverKnight(Hard)"
tTheTreasureHunt_MonsterNameText[7784] = "PhantomofBloodKing"
tTheTreasureHunt_MonsterNameText[7785] = "PhantomofBloodLord"
tTheTreasureHunt_MonsterNameText[7786] = "CommonSilverChest(Expert)"
tTheTreasureHunt_MonsterNameText[7787] = "EliteSilverChest(Expert)"
tTheTreasureHunt_MonsterNameText[7788] = "SuperSilverChest(Expert)"
tTheTreasureHunt_MonsterNameText[7789] = "EpicSilverChest(Expert)"
tTheTreasureHunt_MonsterNameText[7790] = "CommonGoldenChest(Expert)"
tTheTreasureHunt_MonsterNameText[7791] = "EliteGoldenChest(Expert)"
tTheTreasureHunt_MonsterNameText[7792] = "SuperGoldenChest(Expert)"
tTheTreasureHunt_MonsterNameText[7793] = "EpicGoldenChest(Expert)"
tTheTreasureHunt_MonsterNameText[7794] = "CommonSilverKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7795] = "EliteSilverKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7796] = "SuperSilverKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7797] = "EpicSilverKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7798] = "CommonGoldenKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7799] = "EliteGoldenKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7800] = "SuperGoldenKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7801] = "EpicGoldenKnight(Expert)"
tTheTreasureHunt_MonsterNameText[7802] = "PhantomofEvilKing"
tTheTreasureHunt_MonsterNameText[7803] = "PhantomofEvilLord"






tTheTreasureHunt_BrushMonsterText = {}
tTheTreasureHunt_BrushMonsterText[1] = "A flash fire appeared in the lair, and you saw 5 %ss."
-- tTheTreasureHunt_BrushMonsterText[1][1] = "龙穴内鬼火闪动，阁下看到了5个财宝IronChest。"
-- tTheTreasureHunt_BrushMonsterText[1][2] = "龙穴内鬼火闪动，阁下看到了5个财宝BronzeChest。"
-- tTheTreasureHunt_BrushMonsterText[1][3] = "龙穴内鬼火摇曳，阁下看到了5个财宝SilverChest。"
-- tTheTreasureHunt_BrushMonsterText[1][4] = "龙穴内鬼火摇曳，阁下看到了5个财宝GoldenChest。"
-- tTheTreasureHunt_BrushMonsterText[1][5] = "龙穴内鬼火跳跃，阁下看到了5个珍宝IronChest。"
-- tTheTreasureHunt_BrushMonsterText[1][6] = "龙穴内鬼火跳跃，阁下看到了5个珍宝BronzeChest。"
-- tTheTreasureHunt_BrushMonsterText[1][7] = "闯关高潮已然到来，阁下看到了5个珍宝SilverChest。"
-- tTheTreasureHunt_BrushMonsterText[1][8] = "闯关即将大功告成，阁下看到了5个珍宝GoldenChest。"

-- tTheTreasureHunt_BrushMonsterText[2] = {}
tTheTreasureHunt_BrushMonsterText[2] = "30 seconds has passed. The %s you haven`t unlocked turn into %s. Be careful!"
-- tTheTreasureHunt_BrushMonsterText[2][1] = "30秒过去，还未被开启的财宝IronChest摇身变成了财宝IronKnight，请小心它的袭击!"
-- tTheTreasureHunt_BrushMonsterText[2][2] = "30秒过去，还未被开启的财宝BronzeChest摇身变成了财宝BronzeKnight，请小心它的袭击!"
-- tTheTreasureHunt_BrushMonsterText[2][3] = "30秒过去，还未被开启的财宝SilverChest摇身变成了财宝SilverKnight，请小心它的袭击!"
-- tTheTreasureHunt_BrushMonsterText[2][4] = "30秒过去，还未被开启的财宝GoldenChest摇身变成了财宝GoldenKnight，请小心它的袭击!"
-- tTheTreasureHunt_BrushMonsterText[2][5] = "30秒过去，还未被开启的珍宝IronChest摇身变成了珍宝IronKnight，请小心它的袭击!"
-- tTheTreasureHunt_BrushMonsterText[2][6] = "30秒过去，还未被开启的珍宝BronzeChest摇身变成了珍宝BronzeKnight，请小心它的袭击!"
-- tTheTreasureHunt_BrushMonsterText[2][7] = "30秒过去，还未被开启的珍宝SilverChest摇身变成了珍宝SilverKnight，请小心它的袭击!"
-- tTheTreasureHunt_BrushMonsterText[2][8] = "30秒过去，还未被开启的珍宝GoldenChest摇身变成了珍宝GoldenKnight，请小心它的袭击!"

tTheTreasureHunt_MonsterDeathText = {}
tTheTreasureHunt_MonsterDeathText[1] = "You successfully unlocked 1 %s!"
-- tTheTreasureHunt_MonsterDeathText[1][1] = "阁下手脚麻利，迅速打开了1只财宝IronChest!"
-- tTheTreasureHunt_MonsterDeathText[1][2] = "阁下手脚麻利，迅速打开了1只财宝BronzeChest!"
-- tTheTreasureHunt_MonsterDeathText[1][3] = "阁下手脚麻利，迅速打开了1只财宝SilverChest!"
-- tTheTreasureHunt_MonsterDeathText[1][4] = "阁下手脚麻利，迅速打开了1只财宝GoldenChest!"
-- tTheTreasureHunt_MonsterDeathText[1][5] = "阁下手脚麻利，迅速打开了1只珍宝IronChest!"
-- tTheTreasureHunt_MonsterDeathText[1][6] = "阁下手脚麻利，迅速打开了1只珍宝BronzeChest!"
-- tTheTreasureHunt_MonsterDeathText[1][7] = "阁下手脚麻利，迅速打开了1只珍宝SilverChest!"
-- tTheTreasureHunt_MonsterDeathText[1][8] = "阁下手脚麻利，迅速打开了1只珍宝GoldenChest!"

tTheTreasureHunt_MonsterDeathText[2] = "You successfully unlocked 5 %ss within 30 seconds in this round."
-- tTheTreasureHunt_MonsterDeathText[2][1] = "阁下眼疾手快，30秒内打开了这轮的5只财宝IronChest!"
-- tTheTreasureHunt_MonsterDeathText[2][2] = "阁下眼疾手快，30秒内打开了这轮的5只财宝BronzeChest!"
-- tTheTreasureHunt_MonsterDeathText[2][3] = "阁下眼疾手快，30秒内打开了这轮的5只财宝SilverChest!"
-- tTheTreasureHunt_MonsterDeathText[2][4] = "阁下眼疾手快，30秒内打开了这轮的5只财宝GoldenChest!"
-- tTheTreasureHunt_MonsterDeathText[2][5] = "阁下眼疾手快，30秒内打开了这轮的5只珍宝IronChest!"
-- tTheTreasureHunt_MonsterDeathText[2][6] = "阁下眼疾手快，30秒内打开了这轮的5只珍宝BronzeChest!"
-- tTheTreasureHunt_MonsterDeathText[2][7] = "阁下眼疾手快，30秒内打开了这轮的5只珍宝SilverChest!"
-- tTheTreasureHunt_MonsterDeathText[2][8] = "阁下眼疾手快，30秒内打开了这轮的5只珍宝GoldenChest!"


tTheTreasureHunt_MonsterBossText = {}
tTheTreasureHunt_MonsterBossText[1] = "The flame of death was lit up, awakening %s from the darkness."
tTheTreasureHunt_MonsterBossText[2] = "You heard a great noise, and saw a shadow getting close. %s has appeared!"




--------------------------------------------------------------物品对白
tBackpackLetter_Text[3006949] = {}
tBackpackLetter_Text[3006949]["NoSpace"] = "Your inventory is full. Please make some room, and login again to receive the Dragon Lair Scroll."
tBackpackLetter_Text[3006949]["RewardItem"] = "You received a Dragon Lair Scroll. Hurry and check it in your inventory."

-- 龙穴古书
tTheTreasureHunt_Text[3006949] = {}
tTheTreasureHunt_Text[3006949]["111"] = "The Dragon Lair has long been famous for its fabulous treasures, as well as furious monsters. To win these treasures, you should clear the stage within a certain time."
tTheTreasureHunt_Text[3006949]["112"] = "~If you`ve reached Level 100, go find the Dragon Lair Guide for more details in Twin City from August 31st to September 12th. (This scroll will disappear after you read it.)"
tTheTreasureHunt_Text[3006949]["Option1"] = "Take~me~to~see~the~guide."
tTheTreasureHunt_Text[3006949]["BeOverdue"] = "The Dragon Lair Scroll is broken, and you threw it away."
tTheTreasureHunt_Text[3006949]["RewardExp"] = "You read the Dragon Lair Scroll, and received 30 minutes of EXP!"
tTheTreasureHunt_Text[3006949]["RewardCultivation"] = "You read the Dragon Lair Scroll, and received 15 Study Points!"

-- 龙穴宝盒
tTheTreasureHunt_Text[3006951] = {}
tTheTreasureHunt_Text[3006951]["111"] = "This shiny box is full of valuable treasures like Dragon Ball (B), +3 Steed (B) and +2 Stone (B)."
tTheTreasureHunt_Text[3006951]["112"] = "~What you can get from this box depends on your luck. If you have 10 Dragon Box, you can also combine them into a Rare Dragon Box. So, what do you say?"
tTheTreasureHunt_Text[3006951]["Option1"] = "Open~the~box."
tTheTreasureHunt_Text[3006951]["Option2"] = "Combine~10~Dragon~Boxes."

tTheTreasureHunt_Text[3006951][1] = {}
tTheTreasureHunt_Text[3006951][1][1] = "You received 2 EXP Balls (B)!"
tTheTreasureHunt_Text[3006951][1][2] = "You received Praying Stone (S) (B)!"
tTheTreasureHunt_Text[3006951][1][3] = "You received Endurance Book (B)!"
tTheTreasureHunt_Text[3006951][1][4] = "You received +1 Stone (B)!"
tTheTreasureHunt_Text[3006951][1][5] = "You received +2 Stone (B)!"
tTheTreasureHunt_Text[3006951][1][6] = "You received EXP Potion (B)!"
tTheTreasureHunt_Text[3006951][1][7] = "You received +2 Steed Pack (B)!"
tTheTreasureHunt_Text[3006951][1][8] = "You received Dragon Ball (B)!"
tTheTreasureHunt_Text[3006951][1][9] = "You received Meteor Scroll!"
tTheTreasureHunt_Text[3006951][1][10] = "You received Class 1 Money Bag!"
tTheTreasureHunt_Text[3006951][1][11] = "You received Elite Gem Bag!"

tTheTreasureHunt_Text[3006951][2] = {}
tTheTreasureHunt_Text[3006951][2][1] = "You received 2 Chi Pills (100) (B)!"
tTheTreasureHunt_Text[3006951][2][2] = "You received Favored Training Pill (B)!"
tTheTreasureHunt_Text[3006951][2][3] = "You received Special Training Pill (B)!"
tTheTreasureHunt_Text[3006951][2][4] = "You received 2 Protection Pills (B)!"
tTheTreasureHunt_Text[3006951][2][5] = "You received 2 +1 Stones (B)!"
tTheTreasureHunt_Text[3006951][2][6] = "You received 2 +2 Stones (B)!"
tTheTreasureHunt_Text[3006951][2][7] = "You received +3 Steed Pack (B)!"
tTheTreasureHunt_Text[3006951][2][8] = "You received Dragon Ball (B)!"
tTheTreasureHunt_Text[3006951][2][9] = "You received Meteor Scroll!"
tTheTreasureHunt_Text[3006951][2][10] = "You received Class 2 Money Bag!"
tTheTreasureHunt_Text[3006951][2][11] = "You received Super Dragon Gem!"
tTheTreasureHunt_Text[3006951][2][12] = "You received Super Phoenix Gem!"
tTheTreasureHunt_Text[3006951][2][13] = "You received Super Kylin Gem!"
tTheTreasureHunt_Text[3006951][2][14] = "You received Super Fury Gem!"

tTheTreasureHunt_Text[3006951]["NoItem"] = "Make sure you have 10 Dragon Boxes, so you can combine them into a Rare Dragon Box."
tTheTreasureHunt_Text[3006951]["UseItem"] = "You successfully combined 10 Dragon Boxes into a Rare Dragon Box!"

-- 龙穴宝箱
tTheTreasureHunt_Text[3007022] = {}
tTheTreasureHunt_Text[3007022][1] = {}
tTheTreasureHunt_Text[3007022][1][1] = "You received EXP Party Pack!"
tTheTreasureHunt_Text[3007022][1][2] = "You received Praying Stone (M)!"
tTheTreasureHunt_Text[3007022][1][3] = "You received Endurance Book Pack (5)!"
tTheTreasureHunt_Text[3007022][1][4] = "You received +2 Stone!"
tTheTreasureHunt_Text[3007022][1][5] = "You received +3 Stone!"
tTheTreasureHunt_Text[3007022][1][6] = "You received +5 Stone (B)!"
tTheTreasureHunt_Text[3007022][1][7] = "You received +3 Steed Pack (B)!"
tTheTreasureHunt_Text[3007022][1][8] = "You received Dragon Ball (B)!"
tTheTreasureHunt_Text[3007022][1][9] = "You received Meteor Scroll Box!"
tTheTreasureHunt_Text[3007022][1][10] = "You received Class 2 Money Bag!"
tTheTreasureHunt_Text[3007022][1][11] = "You received 100 CPs (B)!"
tTheTreasureHunt_Text[3007022][1][12] = "You received Super Dragon Gem!"
tTheTreasureHunt_Text[3007022][1][13] = "You received Super Phoenix Gem!"


tTheTreasureHunt_Text[3007022][2] = {}
tTheTreasureHunt_Text[3007022][2][1] = "You received Vital Pill (B)!"
tTheTreasureHunt_Text[3007022][2][2] = "You received Favored Training Pack (5)!"
tTheTreasureHunt_Text[3007022][2][3] = "You received Special Training Pack (5)!"
tTheTreasureHunt_Text[3007022][2][4] = "You received Super Protection Pill!"
tTheTreasureHunt_Text[3007022][2][5] = "You received +3 Stone!"
tTheTreasureHunt_Text[3007022][2][6] = "You received +6 Stone!"
tTheTreasureHunt_Text[3007022][2][7] = "You received +3 Steed Pack!"
tTheTreasureHunt_Text[3007022][2][8] = "You received Valued Steed Pack!"
tTheTreasureHunt_Text[3007022][2][9] = "You received Dragon Ball!"
tTheTreasureHunt_Text[3007022][2][10] = "You received DB Scroll!"
tTheTreasureHunt_Text[3007022][2][11] = "You received Meteor Scroll Box!"
tTheTreasureHunt_Text[3007022][2][12] = "You received Super Tortoise Gem!"
tTheTreasureHunt_Text[3007022][2][13] = "You received Super Thunder Gem!"
tTheTreasureHunt_Text[3007022][2][14] = "You received Super Glory Gem!"
tTheTreasureHunt_Text[3007022][2][15] = "You received Frozen Chi Pill (B)!"
tTheTreasureHunt_Text[3007022][2][16] = "You received Class 3 Money Bag!"
tTheTreasureHunt_Text[3007022][2][17] = "You received 200 CPs (B)!"
tTheTreasureHunt_Text[3007022][2][18] = "You received Permanent Stone (B)!"

-- 龙穴宝盒碎片
tTheTreasureHunt_Text[3006953] = {}
tTheTreasureHunt_Text[3006953]["NoItem"] = "Make sure you have 10 Dragon Box Scraps, so you can combine them into a Dragon Box."
tTheTreasureHunt_Text[3006953]["UseItem"] = "You successfully combined 10 scraps into a Dragon Box!"

-- +2 Steed Pack
tTheTreasureHunt_Text[3007031] = {}
tTheTreasureHunt_Text[3007031]["UseItem"] = {}
tTheTreasureHunt_Text[3007031]["UseItem"][1] = "You received a +2 White Steed."
tTheTreasureHunt_Text[3007031]["UseItem"][2] = "You received a +2 Maroon Steed."
tTheTreasureHunt_Text[3007031]["UseItem"][3] = "You received a +2 Black Steed."
-- +3 Steed Pack
tTheTreasureHunt_Text[3007032] = {}
tTheTreasureHunt_Text[3007032]["UseItem"] = {}
tTheTreasureHunt_Text[3007032]["UseItem"][1] = "You received a +3 White Steed."
tTheTreasureHunt_Text[3007032]["UseItem"][2] = "You received a +3 Maroon Steed."
tTheTreasureHunt_Text[3007032]["UseItem"][3] = "You received a +3 Black Steed."

-- 15 minutes of EXP包
tTheTreasureHunt_Text[3007023] = {}
tTheTreasureHunt_Text[3007023]["Exp"] = "You received 15 minutes of EXP!"
tTheTreasureHunt_Text[3007023]["Cultivation"] = "Since you`ve reached the max level, you received 7 Study Points!"

-- 30 minutes of EXP包
tTheTreasureHunt_Text[3007024] = {}
tTheTreasureHunt_Text[3007024]["Exp"] = "You received 30 minutes of EXP!"
tTheTreasureHunt_Text[3007024]["Cultivation"] = "Since you`ve reached the max level, you received 15 Study Points!"

-- 20 Study Points礼包
tTheTreasureHunt_Text[3007025] = {}
tTheTreasureHunt_Text[3007025]["UseItem"] = "You received 20 Study Points!"

-- 40 Study Points礼包
tTheTreasureHunt_Text[3007026] = {}
tTheTreasureHunt_Text[3007026]["UseItem"] = "You received 40 Study Points!"

-- 迷你银两包
tTheTreasureHunt_Text[3007027] = {}
tTheTreasureHunt_Text[3007027]["UseItem"] = "You received 50,000 Silver!"

-- 经济银两包
tTheTreasureHunt_Text[3007028] = {}
tTheTreasureHunt_Text[3007028]["UseItem"] = "You received 100,000 Silver!"

-- 丰足银两包
tTheTreasureHunt_Text[3007029] = {}
tTheTreasureHunt_Text[3007029]["UseItem"] = "You received 200,000 Silver!"



tTheTreasureHunt_Text[3007119] = {}
tTheTreasureHunt_Text[3007119]["UseItem"] = "You received 10 Study Points!"

tTheTreasureHunt_Text[3007120] = {}
tTheTreasureHunt_Text[3007120]["Exp"] = "You received 60 minutes of EXP!"
tTheTreasureHunt_Text[3007120]["Cultivation"] = "Since you`ve reached the max level, you received 30 Study Points!"

tTheTreasureHunt_Text[3007121] = {}
tTheTreasureHunt_Text[3007121]["UseItem"] = "You received 40 Chi Points!"

tTheTreasureHunt_Text[3007122] = {}
tTheTreasureHunt_Text[3007122]["UseItem"] = "You received 20 Chi Points!"



------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]月卡包线上版制作
--Purpose:	月卡包线上版制作
--Creator: 	严振飞
--Created:	2015/07/08
------------------------------------------------------------------------------------
----------------------------------NPC对白--------------------------------------
tFestivalMonthCardPackage_Text = {}
------------------------------【月卡包贩售商】---------------------------------------
tFestivalMonthCardPackage_Text[18710] = {}
tFestivalMonthCardPackage_Text[18710]["GetReward"] = "You received a(n) %s!"

-- 【初始对白】（活动前）
tFestivalMonthCardPackage_Text[18710]["Text111"] = "Here`s a good news! We`re going to promote a series of Moon Packs from September 10th to November 10th."
tFestivalMonthCardPackage_Text[18710]["Text112"] = "~These packs supplied at the most favorable price offer valuable items like +Stones, Steed, Talisman and equipment."
tFestivalMonthCardPackage_Text[18710]["Option111"] = "I~can`t~wait~to~see~it!"
-- 【初始对白】（活动后）
tFestivalMonthCardPackage_Text[18710]["Text121"] = "The Season Sale is over. Don`t forget to open your Moon Packs before the expiration date."
tFestivalMonthCardPackage_Text[18710]["Text122"] = ""
tFestivalMonthCardPackage_Text[18710]["Option121"] = "Okay."
-- 【初始对白】（可购买优质月卡礼包）
tFestivalMonthCardPackage_Text[18710]["Text131"] = "From September 10th to November 10th, I grandly introduce a series of Moon Packs priced at 149 CPs each. You can open this pack every day to"
tFestivalMonthCardPackage_Text[18710]["Text132"] = "~claim wonderful bound items like +Stones, Steed, Talisman and equipment. Just little money for big prizes. Don`t miss the chance!"
tFestivalMonthCardPackage_Text[18710]["Option131"] = "Buy~Bright~Moon~Pack~(149CPs)."
tFestivalMonthCardPackage_Text[18710]["Option132"] = "Show~me~other~packs."
tFestivalMonthCardPackage_Text[18710]["Option180"] = "Check~out~more~details."
tFestivalMonthCardPackage_Text[18710]["Option133"] = "I`ll~talk~to~you~later."
-- 【初始对白】（已买优质月卡礼包未开完包）
tFestivalMonthCardPackage_Text[18710]["Text141"] = "We`re promoting a series of Moon Packs from September 10th to November 10th. Do you like the Bright Moon Pack you just bought?"
tFestivalMonthCardPackage_Text[18710]["Text142"] = "~If you want to buy a Luxury Moon Pack, you need to wait at least 15 days. Look, our promotion packs are available to you now."
tFestivalMonthCardPackage_Text[18710]["Option141"] = "Soul~Promotion~Pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option142"] = "EXP~Promotion~Pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option143"] = "DB~Promotion~Pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option144"] = "Gem~Promotion~Pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option145"] = "I`ll~think~about~it."
-- 【初始对白】（可购买豪华月卡礼包）
tFestivalMonthCardPackage_Text[18710]["Text151"] = "We`re promoting a series of Moon Packs from September 10th to November 10th. If your Bright Moon Pack is empty now, I recommend you the Luxury Moon Pack."
tFestivalMonthCardPackage_Text[18710]["Option151"] = "Buy~Luxury~Moon~Pack~(149CPs)."
tFestivalMonthCardPackage_Text[18710]["Option152"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option153"] = "I`ll~think~about~it."
-- 【初始对白】（已买豪华月卡礼包未开完包）
tFestivalMonthCardPackage_Text[18710]["Text161"] = "We`re promoting a series of Moon Packs from September 10th to November 10th. It seems you`ve bought a Luxury Moon Pack."
tFestivalMonthCardPackage_Text[18710]["Text162"] = "~If you want to buy a Grand Moon Pack, you need to wait at least 15 days. Look, our promotion packs are available to you now."
-- 【初始对白】（可购买至尊月卡礼包）
tFestivalMonthCardPackage_Text[18710]["Text171"] = "We`re promoting a series of Moon Packs from September 10th to November 10th. If your Luxury Moon Pack is empty now, I highly recommend you the Grand Moon Pack."
tFestivalMonthCardPackage_Text[18710]["Option171"] = "Buy~Grand~Moon~Pack~(149CPs)."
tFestivalMonthCardPackage_Text[18710]["Option172"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option173"] = "I`ll~think~about~it."
-- 【初始对白】（已买至尊月卡礼包未开完包）
tFestivalMonthCardPackage_Text[18710]["Text181"] = "We`re promoting a series of Moon Packs from September 10th to November 10th. You`ve bought a Grand Moon Pack, right?"
tFestivalMonthCardPackage_Text[18710]["Text182"] = "~You need to wait at least 15 days to buy another one. Look, our promotion packs are available to you now."
-- 【初始对白】（继续购买至尊月卡礼包）
tFestivalMonthCardPackage_Text[18710]["Text191"] = "We`re promoting a series of Moon Packs from September 10th to November 10th. If your Grand Moon Pack is empty now, you can buy another one."
tFestivalMonthCardPackage_Text[18710]["Option191"] = "Buy~Grand~Moon~Pack~(149CPs)."
tFestivalMonthCardPackage_Text[18710]["Option192"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option193"] = "I`ll~think~about~it."



-- 【购买各礼包】
-- 背包空间不足
tFestivalMonthCardPackage_Text[18710]["Text211"] = "Your inventory is full. Please make some room, first."
tFestivalMonthCardPackage_Text[18710]["Option211"] = "Okay."
-- 天石数不足
tFestivalMonthCardPackage_Text[18710]["Text221"] = "You don`t have enough CPs for the pack."
-- 二次确认
tFestivalMonthCardPackage_Text[18710]["Text231"] = "Are you sure you want to pay %d CPs for a(n) %s?"
tFestivalMonthCardPackage_Text[18710]["Option231"] = "Yes."
tFestivalMonthCardPackage_Text[18710]["Option232"] = "No."
-- 购买优质月卡礼包
tFestivalMonthCardPackage_Text[18710]["Text241"] = "                                                Bright Moon Pack\n"
tFestivalMonthCardPackage_Text[18710]["Text242"] = "--------------------------------------------------------------------------------------------------\n"
tFestivalMonthCardPackage_Text[18710]["Text243"] = "Duration: 20 days\n"
tFestivalMonthCardPackage_Text[18710]["Text244"] = "Open Times: totally 15 times, once in a day\n"
tFestivalMonthCardPackage_Text[18710]["Text245"] = "Open to Get (each time): Stone Steed Pack (B)*1\n                                             Super Riding Crop Scrap (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text246"] = "Extra Gift for 1st Time to Open: 1%% Blessed Super Heaven Fan (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text247"] = "Extra Gift for 10 Times to Open: +3 Stone (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text248"] = "--------------------------------------------------------------------------------------------------\n\n"
tFestivalMonthCardPackage_Text[18710]["Option241"] = "Buy~this~pack~(149~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option242"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option243"] = "Close."

-- 购买神魂特惠礼包
tFestivalMonthCardPackage_Text[18710]["Text251"] = "                                                Soul Promotion Pack\n"
tFestivalMonthCardPackage_Text[18710]["Text252"] = "--------------------------------------------------------------------------------------------------\n"
tFestivalMonthCardPackage_Text[18710]["Text253"] = "Duration: 20 days\n"
tFestivalMonthCardPackage_Text[18710]["Text254"] = "Open Times: totally 15 times, once in a day\n"
tFestivalMonthCardPackage_Text[18710]["Text255"] = "Open to Get (each time): Dragon Crystal (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text256"] = "Extra Gift for 1st Time to Open: Permanent Stone Scrap (B)*6\n"
tFestivalMonthCardPackage_Text[18710]["Text257"] = "Extra Gift for 10 Times to Open: P6 Dragon Soul Pack (B)*1\n                                                         L5 Refinery Pack (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text258"] = "--------------------------------------------------------------------------------------------------\n\n"
tFestivalMonthCardPackage_Text[18710]["Option251"] = "Buy~this~pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option252"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option253"] = "Close."

-- 购买经验特惠礼包
tFestivalMonthCardPackage_Text[18710]["Text261"] = "                                                EXP Promotion Pack\n"
tFestivalMonthCardPackage_Text[18710]["Text262"] = "--------------------------------------------------------------------------------------------------\n"
tFestivalMonthCardPackage_Text[18710]["Text263"] = "Duration: 20 days\n"
tFestivalMonthCardPackage_Text[18710]["Text264"] = "Open Times: totally 15 times, once in a day\n"
tFestivalMonthCardPackage_Text[18710]["Text265"] = "Open to Get (each time): EXP Ball (B)*5, EXP Potion (B)*2\n"
tFestivalMonthCardPackage_Text[18710]["Text266"] = "Extra Gift for 1st Time to Open: EXP Ball Fragment (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text267"] = "Extra Gift for 10 Times to Open: EXP Care Pills (B)*10\n"
tFestivalMonthCardPackage_Text[18710]["Text268"] = "--------------------------------------------------------------------------------------------------\n\n"
tFestivalMonthCardPackage_Text[18710]["Option261"] = "Buy~this~pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option262"] = "Close."
-- 购买龙珠特惠礼包
tFestivalMonthCardPackage_Text[18710]["Text271"] = "                                                DB Promotion Pack\n"
tFestivalMonthCardPackage_Text[18710]["Text272"] = "--------------------------------------------------------------------------------------------------\n"
tFestivalMonthCardPackage_Text[18710]["Text273"] = "Duration: 20 days\n"
tFestivalMonthCardPackage_Text[18710]["Text274"] = "Open Times: totally 15 times, once in a day\n"
tFestivalMonthCardPackage_Text[18710]["Text275"] = "Open to Get (each time): Dragon Ball Scrap (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text276"] = "Extra Gift for 1st Time to Open: Modesty Book (B)*2\n"
tFestivalMonthCardPackage_Text[18710]["Text277"] = "Extra Gift for 10 Times to Open: Dragon Ball Scrap (B)*5\n"
tFestivalMonthCardPackage_Text[18710]["Text278"] = "--------------------------------------------------------------------------------------------------\n\n"
tFestivalMonthCardPackage_Text[18710]["Option271"] = "Buy~this~pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option272"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option273"] = "Close."
-- 购买宝石特惠礼包
tFestivalMonthCardPackage_Text[18710]["Text281"] = "                                                Gem Promotion Pack\n"
tFestivalMonthCardPackage_Text[18710]["Text282"] = "--------------------------------------------------------------------------------------------------\n"
tFestivalMonthCardPackage_Text[18710]["Text283"] = "Duration: 20 days\n"
tFestivalMonthCardPackage_Text[18710]["Text284"] = "Open Times: totally 15 times, once in a day\n"
tFestivalMonthCardPackage_Text[18710]["Text285"] = "Open to Get (each time): Gem Star (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text286"] = "Extra Gift for 1st Time to Open: Penitence Amulet (B)*10\n"
tFestivalMonthCardPackage_Text[18710]["Text287"] = "Extra Gift for 10 Times to Open: Gem Star (B)*5\n"
tFestivalMonthCardPackage_Text[18710]["Text288"] = "--------------------------------------------------------------------------------------------------\n\n"
tFestivalMonthCardPackage_Text[18710]["Option281"] = "Buy~this~pack~(99~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option282"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option283"] = "Close."
-- 已有特惠包
tFestivalMonthCardPackage_Text[18710]["Text291"] = "Sorry, you can`t buy more than one promotion pack here within 15 days."
tFestivalMonthCardPackage_Text[18710]["Option291"] = "I~see."
-- 购买豪华月卡礼包
tFestivalMonthCardPackage_Text[18710]["Text2101"] = "                                                Luxury Moon Pack\n"
tFestivalMonthCardPackage_Text[18710]["Text2102"] = "--------------------------------------------------------------------------------------------------\n"
tFestivalMonthCardPackage_Text[18710]["Text2103"] = "Duration: 20 days\n"
tFestivalMonthCardPackage_Text[18710]["Text2104"] = "Open Times: totally 15 times, once in a day\n"
tFestivalMonthCardPackage_Text[18710]["Text2105"] = "Open to Get (each time): Stone Steed Pack (B)*1\n                                             Super Gear Token Scrap (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text2106"] = "Extra Gift for 1st Time to Open: 1%% Blessed Super Star Tower (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text2107"] = "Extra Gift for 10 Times to Open: +3 Stone (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text2108"] = "--------------------------------------------------------------------------------------------------\n\n"
tFestivalMonthCardPackage_Text[18710]["Option2101"] = "Buy~this~pack~(149~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option2102"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option2103"] = "Close."
-- 购买至尊月卡礼包
tFestivalMonthCardPackage_Text[18710]["Text2111"] = "                                                Grand Moon Pack\n"
tFestivalMonthCardPackage_Text[18710]["Text2112"] = "--------------------------------------------------------------------------------------------------\n"
tFestivalMonthCardPackage_Text[18710]["Text2113"] = "Duration: 20 days\n"
tFestivalMonthCardPackage_Text[18710]["Text2114"] = "Open Times: totally 15 times, once in a day\n"
tFestivalMonthCardPackage_Text[18710]["Text2115"] = "Open to Get (each time): Stone Steed Pack (B)*1\n                                             Super Gear Token Scrap (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text2116"] = "Extra Gift for 1st Time to Open: Super Weapon Token (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text2117"] = "Extra Gift for 10 Times to Open: +3 Stone (B)*1\n"
tFestivalMonthCardPackage_Text[18710]["Text2118"] = "--------------------------------------------------------------------------------------------------\n\n"
tFestivalMonthCardPackage_Text[18710]["Option2111"] = "Buy~this~pack~(149~CPs)."
tFestivalMonthCardPackage_Text[18710]["Option2112"] = "Specify~these~rewards."
tFestivalMonthCardPackage_Text[18710]["Option2113"] = "Close."




-- 【查看礼包详情】
-- 优质月卡礼包
tFestivalMonthCardPackage_Text[18710]["Text311"] = "The Stone Steed Pack has +2 Stone (B) and +2 Steed (B) to choose from."
tFestivalMonthCardPackage_Text[18710]["Text312"] = "~15 Super Riding Crop Scraps (B) can be combined into a Super Riding Crop (B)."
tFestivalMonthCardPackage_Text[18710]["Option311"] = "Learn~about~something~else."
tFestivalMonthCardPackage_Text[18710]["Option312"] = "Close."
-- 豪华月卡礼包
tFestivalMonthCardPackage_Text[18710]["Text321"] = "The Stone Steed Pack has +2 Stone (B) and +2 Steed (B) to choose from."
tFestivalMonthCardPackage_Text[18710]["Text322"] = "~15 Super Gear Token Scraps (B) can be combined into a Super Gear Token."
tFestivalMonthCardPackage_Text[18710]["Text323"] = "~You can activate this token to select a piece of Level 100 equipment (B)."
tFestivalMonthCardPackage_Text[18710]["Option321"] = "Learn~about~something~else."
-- 至尊月卡礼包
tFestivalMonthCardPackage_Text[18710]["Text331"] = "The Stone Steed Pack has +2 Stone (B) and +2 Steed (B) to choose from."
tFestivalMonthCardPackage_Text[18710]["Text332"] = "~15 Super Gear Token Scraps (B) can be combined into a Super Gear Token."
tFestivalMonthCardPackage_Text[18710]["Text333"] = "~You can activate this token to select a piece of Level 100 equipment (B)."
tFestivalMonthCardPackage_Text[18710]["Text334"] = "~With the Super Weapon Token, you can select a Level 100 Super 2-Soc. weapon (B)."
tFestivalMonthCardPackage_Text[18710]["Option331"] = "Learn~about~something~else."
-- 神魂特惠礼包
tFestivalMonthCardPackage_Text[18710]["Text341"] = "You can exchange 1 Dragon Crystal for a P4 Dragon Soul Pack (B), 2 crystals for a P5 Dragon Soul Pack (B),"
tFestivalMonthCardPackage_Text[18710]["Text342"] = "~4 crystals for a P6 Dragon Soul Pack (B), or 8 crystals for a P7 Dragon Soul Pack (B)."
tFestivalMonthCardPackage_Text[18710]["Text343"] = "~Every 10 Permanent Stone Scraps (B) can be combined into a Permanent Stone (B). If you use it directly, you have a 10%% chance to get a Permanent Stone (B)."
tFestivalMonthCardPackage_Text[18710]["Option341"] = "Learn~about~something~else."
-- 龙珠特惠礼包
tFestivalMonthCardPackage_Text[18710]["Text351"] = "Every 7 Dragon Ball Scraps (B) can be combined into a Dragon Ball (B). If you use it directly, you have a 1/7 chance to get a Dragon Ball (B)."
tFestivalMonthCardPackage_Text[18710]["Option351"] = "Learn~about~something~else."
-- 宝石特惠礼包
tFestivalMonthCardPackage_Text[18710]["Text361"] = "You can exchange 1 Gem Star for 1 Refined Tortoise Gem (B), 2 stars for 1 Super Dragon/Phoenix Gem (B),"
tFestivalMonthCardPackage_Text[18710]["Text362"] = "~3 stars for 1 Refined Thunder/Glory Gem (B), or 15 stars for 1 Super Tortoise Gem (B)."
tFestivalMonthCardPackage_Text[18710]["Option361"] = "Learn~about~something~else."



-- 【查看其他礼包】
tFestivalMonthCardPackage_Text[18710]["Text411"] = "You can buy Moon Packs in the order: Bright, Luxury, Grand, and then all Grand ones. When you buy a Moon Pack,"
tFestivalMonthCardPackage_Text[18710]["Text412"] = "~you can keep it for 20 days, and have to wait 15 days to buy another one. You can only have one Moon Pack in your inventory"
tFestivalMonthCardPackage_Text[18710]["Text413"] = "~at a time. During the above-mentioned 15 days, you can consider our promotion packs. Which pack would you like to know more?"
tFestivalMonthCardPackage_Text[18710]["Option41"] = "Luxury~Moon~Pack."
tFestivalMonthCardPackage_Text[18710]["Option42"] = "Grand~Moon~Pack."
tFestivalMonthCardPackage_Text[18710]["Option43"] = "Soul~Promotion~Pack."
tFestivalMonthCardPackage_Text[18710]["Option44"] = "EXP~Promotion~Pack."
tFestivalMonthCardPackage_Text[18710]["Option45"] = "DB~Promotion~Pack."
tFestivalMonthCardPackage_Text[18710]["Option46"] = "Gem~Promotion~Pack."
tFestivalMonthCardPackage_Text[18710]["Option47"] = "Learn~about~something~else."
tFestivalMonthCardPackage_Text[18710]["Option48"] = "Close."
-- 选项
tFestivalMonthCardPackage_Text[18710]["Option51"] = "Learn~about~something~else."
tFestivalMonthCardPackage_Text[18710]["Option52"] = "Close."

----------------------------------物品对白--------------------------------------
--删除礼包
tFestivalMonthCardPackage_Text["DelItem"] = "Your %s is empty now, and you can buy another Moon Pack from the retailer (295,295) in Twin City."
-- 公告
tFestivalMonthCardPackage_Text["Notice"] = "Precious Moon Packs have been put on the shelf, which contain fabulous treasures! Talk to the retailor (295,295) in Twin City for more details."
-- 活动时间过后
tFestivalMonthCardPackage_Text["TimeEnd"] = "The event has ended, and you threw the item away."
------------------------------【优质月卡礼包】---------------------------------------
tFestivalMonthCardPackage_Text["CardPag"] = {}
tFestivalMonthCardPackage_Text["CardPag"]["FullBag"] = "Your inventory is full. You need to make some room, first."
tFestivalMonthCardPackage_Text["CardPag"]["OpenFail"] = "You`ve opened this pack, today. Please retry tomorrow."
tFestivalMonthCardPackage_Text["CardPag"]["OpenStart"] = "You received "
tFestivalMonthCardPackage_Text["CardPag"]["OpenMid"] = "! As you`ve opened this pack %d time(s)"
tFestivalMonthCardPackage_Text["CardPag"]["OpenAdd"] = ", you also received "
tFestivalMonthCardPackage_Text["CardPag"]["OpenReward"] = ", %d %s (B)"
tFestivalMonthCardPackage_Text["CardPag"]["HeavenFan"] = "1%% Blessed "
tFestivalMonthCardPackage_Text["CardPag"]["Stone"] = "+3 "


-------------------------------------【宝石结晶】---------------------------------------
tFestivalMonthCardPackage_Text[3006937] = {}
-- 初始对白
tFestivalMonthCardPackage_Text[3006937]["Text111"] = "Different gem requires different amount of Gem Stars. So, which gem would you like to swap for?"
tFestivalMonthCardPackage_Text[3006937]["Option11"] = "RefinedTortoiseGem(B)~(1star)."
tFestivalMonthCardPackage_Text[3006937]["Option12"] = "SuperDragonGem(B)~(2stars)."
tFestivalMonthCardPackage_Text[3006937]["Option13"] = "SuperPhoenixGem(B)~(2stars)."
tFestivalMonthCardPackage_Text[3006937]["Option14"] = "RefinedThunderGem(B)~(3stars)."
tFestivalMonthCardPackage_Text[3006937]["Option15"] = "RefinedGloryGem(B)~(3stars)."
tFestivalMonthCardPackage_Text[3006937]["Option16"] = "SuperTortoiseGem(B)~(15stars)."
-- 背包满
tFestivalMonthCardPackage_Text[3006937]["Text121"] = "You`re carrying a full inventory. Please make some room, first."
tFestivalMonthCardPackage_Text[3006937]["Option17"] = "I`ll~do~it~now."
-- 结晶数量不够
tFestivalMonthCardPackage_Text[3006937]["Text131"] = "Sorry, you don`t have enough Gem Stars to swap for this gem."
-- 二次确认
tFestivalMonthCardPackage_Text[3006937]["Text141"] = "Are you sure you want to swap %d Gem Stars for the %s (B)?"
tFestivalMonthCardPackage_Text[3006937]["Option18"] = "Yes."
-- 获得宝石
tFestivalMonthCardPackage_Text[3006937]["GemCrystal"] = "You successfully swapped %d Gem Stars for 1 %s (B)!"





-------------------------------------【背包信】---------------------------------------
tFestivalMonthCardPackage_Text[3006938] = {}
-- 初始对白
tFestivalMonthCardPackage_Text[3006938]["Text111"] = "From September 10th to November 10th, a series of Moon Packs and promotion packs will be supplied to you at the most favorable price."
tFestivalMonthCardPackage_Text[3006938]["Text112"] = "~If you`re interested, talk to Moon Pack Retailer (295,295) in Twin City."
tFestivalMonthCardPackage_Text[3006938]["Option11"] = "Discard~it~after~reading."
tFestivalMonthCardPackage_Text[3006938]["Letter"] = "You received %d%s "
tFestivalMonthCardPackage_Text[3006938][30] = " minutes of EXP!"
tFestivalMonthCardPackage_Text[3006938][15] = " Study Points!"
-- 背包空间不足
tBackpackLetter_Text[3006938] = {}
tBackpackLetter_Text[3006938]["NoSpace"] = "Your inventory is full. Please make some room, and login again to receive a Season Sale Leaflet."
tBackpackLetter_Text[3006938]["RewardItem"] = "You received a Season Sale Leaflet. Hurry and check it in your inventory."




------------------------------------------------------------------------------------

--Name:			[征服][活动脚本]劳工节大罢工(9.7-9.13)
--Creator: 		魏贻逵
--Created:		2015/05/20
------------------------------------------------------------------------------------

--工会的铁匠 工会的裁缝 工会的厨师 工会的马夫
		tLaborday2015_Strike_Labor_Text = {}
		tLaborday2015_Strike_Labor_Text["Text1"] = {}
		tLaborday2015_Strike_Labor_Text["Text1"][1] = "Strikers: No held wages!"
		tLaborday2015_Strike_Labor_Text["Text1"][2] = "Strikers: Shorter hours!"
		tLaborday2015_Strike_Labor_Text["Text1"][3] = "Strikers: No forced overtime!"
		tLaborday2015_Strike_Labor_Text["Text1"][4] = "Strikers: Better Better safety conditions!"
		tLaborday2015_Strike_Labor_Text["Option1"] = "I`ll~stand~by~you."

		tLaborday2015_Strike_Labor_Text["Text2"] = "Thank god! Finally, we won!"
		tLaborday2015_Strike_Labor_Text["Option2"] = "Have~some~wine."
		tLaborday2015_Strike_Labor_Text["Option3"] = "Aye,~aye."

		tLaborday2015_Strike_Labor_Text["Text3"] = "What wine? You don`t have any wine on you!"
		tLaborday2015_Strike_Labor_Text["Option4"] = "My~fault."

		tLaborday2015_Strike_Labor_Text[8165] = "You gave the wine to the Ironsmith. Go back to Ironsmith Chiu, to claim your reward."
		tLaborday2015_Strike_Labor_Text[8171] = "You gave the Sachet to the Tailor. Go back to Tailor Hui, to claim your reward."
		tLaborday2015_Strike_Labor_Text[8177] = "You gave the Recipe to the Chef. Go back to Chef Zhang, to claim your reward."
		tLaborday2015_Strike_Labor_Text[8183] = "You gave the Snack to the Stableman. Go back to Stableman Wong, to claim your reward."

--士兵
		tLaborday2015_Strike_Soldier_Text = {}
		tLaborday2015_Strike_Soldier_Text[1] = {}
		tLaborday2015_Strike_Soldier_Text[1]["Text"] = "What on earth do the workers want from us?"
		tLaborday2015_Strike_Soldier_Text[1]["Option"] = "Better~treatment."
		
		tLaborday2015_Strike_Soldier_Text[2] = {}
		tLaborday2015_Strike_Soldier_Text[2]["Text"] = "The only thing they`re gonna get from me, is a fat lip!"
		tLaborday2015_Strike_Soldier_Text[2]["Option"] = "You`re~a~bad~man."

----铁匠赵刚 裁缝惠娘 厨师冯善 马夫王宝??
--第1?段
		tLaborday2015_Strike_Npc_Text = {}
		tLaborday2015_Strike_Npc_Text[1] = {}
		tLaborday2015_Strike_Npc_Text[1][8153] = {}
		tLaborday2015_Strike_Npc_Text[1][8153][1] = "The knife is sharp, now. Please give it to Ironsmith Chiu, to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][8153][2] = "You received a Blunt Knife and Grindstone. Please grind the knife and return it to Ironsmith Chiu."
		tLaborday2015_Strike_Npc_Text[1][8153][3] = "Please find Ironsmith Chiu, to receive the Blunt Knife."
		tLaborday2015_Strike_Npc_Text[1][8153][4] = "Please find Ironsmith Chiu to get a Grindstone."
		tLaborday2015_Strike_Npc_Text[1][8153][5] = "The knife is sharp, now. Please give it to Ironsmith Chiu, to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][720780] = "Grind"
		tLaborday2015_Strike_Npc_Text[1][720781] = "Grind"

		tLaborday2015_Strike_Npc_Text[1][8154] = {}
		tLaborday2015_Strike_Npc_Text[1][8154][1] = "The dress is finished. Please give it to Tailor Hui, to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][8154][2] = "You received a Button and Unfinished Dress. Please sew the button on the dress, then return it to Tailor Hui."
		tLaborday2015_Strike_Npc_Text[1][8154][3] = "Please find Tailor Hui to get an Unfinished Dress."
		tLaborday2015_Strike_Npc_Text[1][8154][4] = "Please find Tailor Hui to get a Button."
		tLaborday2015_Strike_Npc_Text[1][8154][5] = "The dress is finished. Please give it to Tailor Hui, to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][720782] = "Sew"
		tLaborday2015_Strike_Npc_Text[1][720783] = "Sew"

		tLaborday2015_Strike_Npc_Text[1][8155] = {}
		tLaborday2015_Strike_Npc_Text[1][8155][1] = "You mixed the wine. Please give it to Chef Zhang, to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][8155][2] = "You received the Rice Wine and Fruit Wine. Please mix the wine, then return it to Chef Zhang."
		tLaborday2015_Strike_Npc_Text[1][8155][3] = "Please find Chef Zhang to get a bottle of Rice Wine."
		tLaborday2015_Strike_Npc_Text[1][8155][4] = "Please find Chef Zhang to get a bottle of Fruit Wine."
		tLaborday2015_Strike_Npc_Text[1][8155][5] = "You mixed the wine. Please give it to Chef Zhang, to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][720784] = "Mix"
		tLaborday2015_Strike_Npc_Text[1][720785] = "Mix"

		tLaborday2015_Strike_Npc_Text[1][8156] = {}
		tLaborday2015_Strike_Npc_Text[1][8156][1] = "Please take some Forage to the stable and then find Stableman Wong to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][8156][2] = "You received some Forage. Please take it to the stable and then find Stableman Wong to claim your reward.'"
		tLaborday2015_Strike_Npc_Text[1][8156][3] = "Please find Stableman Wong to get some Forage."
		tLaborday2015_Strike_Npc_Text[1][8156][4] = "You took the Forage to the stable. Please find Stableman Wong, to claim your reward."
		tLaborday2015_Strike_Npc_Text[1][720786] = "Place"

--第2?段
		tLaborday2015_Strike_Npc_Text[2] = {}
		tLaborday2015_Strike_Npc_Text[2][8153] = {}
		tLaborday2015_Strike_Npc_Text[2][8153][1] = "Ironsmith Chiu has signed the Attendance Sheet."
		tLaborday2015_Strike_Npc_Text[2][8153][2] = "You received some Leaflets. Please hand them out in Twin City (310,290)."

		tLaborday2015_Strike_Npc_Text[2][720787] = {}
		tLaborday2015_Strike_Npc_Text[2][720787][1] = "You need to distribute the Leaflets around Twin City (310,290)."
		tLaborday2015_Strike_Npc_Text[2][720787][2] = "Distribute"
		tLaborday2015_Strike_Npc_Text[2][720787][3] = "Please find Ironsmith Chiu to get some Leaflets."
		tLaborday2015_Strike_Npc_Text[2][720787][4] = "You have done the quest once, today. You can`t obtain another Festival Joy Pack."

		tLaborday2015_Strike_Npc_Text[2][8154] = {}
		tLaborday2015_Strike_Npc_Text[2][8154][1] = "Tailor Hui has signed the Attendance Sheet."
		tLaborday2015_Strike_Npc_Text[2][8154][2] = "Please make some room for the cloth."
		tLaborday2015_Strike_Npc_Text[2][8154][3] = "You received a piece of cloth. Take it to Tailor Hui."

		tLaborday2015_Strike_Npc_Text[2][8155] = {}
		tLaborday2015_Strike_Npc_Text[2][8155][1] = "Chef Zhang has signed the Attendance Sheet."
		tLaborday2015_Strike_Npc_Text[2][8155][2] = "Your inventory is full. Please make some room for the egg."
		tLaborday2015_Strike_Npc_Text[2][8155][3] = "You obtained an egg. Please give it to Chef Zhang."
		
		tLaborday2015_Strike_Npc_Text[2][8156] = {}
		tLaborday2015_Strike_Npc_Text[2][8156][1] = "Stableman Wong has signed the Attendance Sheet."

		tLaborday2015_Strike_Npc_Text[2][720788] = {}
		tLaborday2015_Strike_Npc_Text[2][720788][1] = "You need to throw the forage in the river, in Twin City (250,380)."
		tLaborday2015_Strike_Npc_Text[2][720788][2] = "Throw"
		tLaborday2015_Strike_Npc_Text[2][720788][3] = "Please find Stableman Wong to get some Forage."
		tLaborday2015_Strike_Npc_Text[2][720788][4] = "You have already destroyed a bag of forage. You can`t get another Festival Joy Pack."


--第3?段
		tLaborday2015_Strike_Npc_Text[3] = {}
		tLaborday2015_Strike_Npc_Text[3]["Option1"] = "I`ll~let~him~know."
		
		tLaborday2015_Strike_Npc_Text[3][8153] = {}
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"] = {}

		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][1] = "We insist on a 30 percent wage increase!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][1] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][2] = "We insist on better Better safety conditions!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][2] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][3] = "We insist on shorter work hours!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][3] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][4] = "We insist on a minimum wage of at least 1000 Silver, every week!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][4] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][5] = "We should be paid extra, if we are asked to work overtime!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][5] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][6] = "We insist on two days off, each week!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][6] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][7] = "We should be paid on time. No held wages or excuses!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][7] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][8] = "We should have the right to enjoy official holidays!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][8] = "~We are determined not to return to work, unless our demands are met!"
                                           
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][9] = "The government should regulate the 1st Monday of September as Labor Day!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][9] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][10] = "We insist on free schools, so we can send our children to get an education!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][10] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][11] = "Women should have the same rights as men, in finding a job! They should not be discriminated against."
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][11] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8153]["Text1"][12] = "We insist child labor should be made illegal!"
		tLaborday2015_Strike_Npc_Text[3][8153]["Text2"][12] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8154] = {}
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"] = {}

		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][1] = "We insist on a 30 percent wage increase!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][1] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][2] = "We insist on better Better safety conditions!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][2] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][3] = "We insist on shorter work hours!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][3] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][4] = "We insist on a minimum wage of at least 1,000 Silver, every week!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][4] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][5] = "We should be paid extra, if we are asked to work overtime!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][5] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][6] = "We insist on two days off, each week!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][6] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][7] = "We should be paid on time. No held wages or excuses!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][7] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][8] = "We should have the right to enjoy official holidays!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][8] = "~We are determined not to return to work, unless our demands are met!"
                                           
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][9] = "The government should regulate the 1st Monday of September as Labor Day!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][9] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][10] = "Women should have the same rights as men, in finding a job! They should not be discriminated against."
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][10] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][11] = "We insist on free schools, so we can send our children to get an education!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][11] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8154]["Text1"][12] = "We insist child labor should be made illegal!"
		tLaborday2015_Strike_Npc_Text[3][8154]["Text2"][12] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8155] = {}
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"] = {}

		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][1] = "We insist on a 30 percent wage increase!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][1] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][2] = "We insist on better Better safety conditions!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][2] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][3] = "We insist on shorter work hours!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][3] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][4] = "We insist on a minimum wage of at least 1,000 Silver, every week!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][4] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][5] = "We should be paid extra, if we are asked to work overtime!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][5] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][6] = "We insist on two days off, each week!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][6] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][7] = "We should be paid on time. No held wages or excuses!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][7] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][8] = "We should have the right to enjoy official holidays!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][8] = "~We are determined not to return to work, unless our demands are met!"
                                           
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][9] = "The government should regulate the 1st Monday of September as Labor Day!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][9] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][10] = "We insist on free schools, so we can send our children to get an education!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][10] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][11] = "Women should have the same rights as men, in finding a job! They should not be discriminated against."
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][11] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8155]["Text1"][12] = "We insist child labor should be made illegal!"
		tLaborday2015_Strike_Npc_Text[3][8155]["Text2"][12] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8156] = {}
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"] = {}

		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][1] = "We insist on a 30 percent wage increase!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][1] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][2] = "We insist on better Better safety conditions!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][2] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][3] = "We insist on shorter work hours!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][3] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][4] = "We insist on a minimum wage of at least 1,000 Silver, every week!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][4] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][5] = "We should be paid extra, if we are asked to work overtime!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][5] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][6] = "We insist on two days off, each week!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][6] = "~We are determined not to return to work, unless our demands are met!"

		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][7] = "We should be paid on time. No held wages or excuses!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][7] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][8] = "We should have the right to enjoy official holidays!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][8] = "~We are determined not to return to work, unless our demands are met!"
                                           
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][9] = "The government should regulate the 1st Monday of September as Labor Day!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][9] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][10] = "We insist on free schools, so we can send our children to get an education!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][10] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][11] = "We insist child labor should be made illegal!"
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][11] = "~We are determined not to return to work, unless our demands are met!"
		                                   
		tLaborday2015_Strike_Npc_Text[3][8156]["Text1"][12] = "Women should have the same rights as men, in finding a job! They should not be discriminated against."
		tLaborday2015_Strike_Npc_Text[3][8156]["Text2"][12] = "~We are determined not to return to work, unless our demands are met!"
		
		tLaborday2015_Strike_Npc_Text[4] = {}
		tLaborday2015_Strike_Npc_Text[4]["Option1"] = "Certainly."
		tLaborday2015_Strike_Npc_Text[4]["Option2"] = "You`re~welcome."

		tLaborday2015_Strike_Npc_Text[4][8153] = {}
		tLaborday2015_Strike_Npc_Text[4][8153]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[4][8153]["Text1"][1] = "Great! Do you think you could buy me a Lucky Bow?"
		tLaborday2015_Strike_Npc_Text[4][8153]["Text1"][2] = "Great! Do you think you could buy me a Lucky Backsword?"
		tLaborday2015_Strike_Npc_Text[4][8153]["Text1"][3] = "Great! Do you think you could buy me a Metal Katana?"
		tLaborday2015_Strike_Npc_Text[4][8153]["Text1"][4] = "Great! Do you think you could buy me a Lucky Blade?"
		
		tLaborday2015_Strike_Npc_Text[4][8154] = {}
		tLaborday2015_Strike_Npc_Text[4][8154]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[4][8154]["Text1"][1] = "Great! I need you to pick me up some Violet Dye?"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text1"][2] = "Great! I need you to pick me up some Blue Dye??"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text1"][3] = "Great! I need you to pick me up some Green Dye?"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text1"][4] = "Great! I need you to pick me up some Brown Dye?"
		
		tLaborday2015_Strike_Npc_Text[4][8155] = {}
		tLaborday2015_Strike_Npc_Text[4][8155]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[4][8155]["Text1"][1] = "Great! I think she would really love a new pair of Beryl Earrings?"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text1"][2] = "Great! I think she would really love a new Copper Ring?"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text1"][3] = "Great! I think she would really love a new Peach Bracelet?"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text1"][4] = "Great! I think she would really love a new Thread Necklace?"
		
		tLaborday2015_Strike_Npc_Text[4][8156] = {}
		tLaborday2015_Strike_Npc_Text[4][8156]["Text1"] = {}
		tLaborday2015_Strike_Npc_Text[4][8156]["Text1"][1] = "Great! Could you get me a Bronze Hood?"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text1"][2] = "Great! Could you get me a Crane Veil?"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text1"][3] = "Great! Could you get me a Vine Headband?"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text1"][4] = "Great! Could you get me a Bronze Helmet?"
		
		tLaborday2015_Strike_Npc_Text[4][8153]["Text2"] = {}
		tLaborday2015_Strike_Npc_Text[4][8153]["Text2"][1] = "Excuse me? Where is the Lucky Bow?"
		tLaborday2015_Strike_Npc_Text[4][8153]["Text2"][2] = "Excuse me? Where is the Lucky Backsword?"
		tLaborday2015_Strike_Npc_Text[4][8153]["Text2"][3] = "Excuse me? Where is the Metal Katana?"
		tLaborday2015_Strike_Npc_Text[4][8153]["Text2"][4] = "Excuse me? Where is the Lucky Blade?"
		
		tLaborday2015_Strike_Npc_Text[4][8154]["Text2"] = {}
		tLaborday2015_Strike_Npc_Text[4][8154]["Text2"][1] = "Excuse me? Where is the Violet Dye?"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text2"][2] = "Excuse me? Where is the Blue Dye?"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text2"][3] = "Excuse me? Where is the Green Dye?"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text2"][4] = "Excuse me? Where is the Brown Dye?"
		
		tLaborday2015_Strike_Npc_Text[4][8155]["Text2"] = {}
		tLaborday2015_Strike_Npc_Text[4][8155]["Text2"][1] = "Excuse me? Where are the Beryl Earrings?"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text2"][2] = "Excuse me? Where is the Copper Ring?"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text2"][3] = "Excuse me? Where is the Peach Bracelet?"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text2"][4] = "Excuse me? Where is the Thread Necklace?"
		
		tLaborday2015_Strike_Npc_Text[4][8156]["Text2"] = {}
		tLaborday2015_Strike_Npc_Text[4][8156]["Text2"][1] = "Excuse me? Where is the Bronze Hood?"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text2"][2] = "Excuse me? Where is the Crane Veil?"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text2"][3] = "Excuse me? Where is the Vine Headband?"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text2"][4] = "Excuse me? Where is the Bronze Helmet?"

		tLaborday2015_Strike_Npc_Text[4][8153]["Text3"] = "You received a bottle of Aged Wine. Please give it to the Ironsmith."
		tLaborday2015_Strike_Npc_Text[4][8154]["Text3"] = "You received a Sachet. Please give it to the Tailor."
		tLaborday2015_Strike_Npc_Text[4][8155]["Text3"] = "You received a Recipe. Please give it to the Chef."
		tLaborday2015_Strike_Npc_Text[4][8156]["Text3"] = "You received a Snack. Please give it to the Stableman."

		tLaborday2015_Strike_Npc_Text[4][8153]["Text4"] = "Haven`t you taken the wine yet? Hurry up and give my regards to my friend!"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text4"] = "Haven`t you got the Sachet yet? Hurry up and give my regards to my sister."
		tLaborday2015_Strike_Npc_Text[4][8155]["Text4"] = "Haven`t you got the Recipe yet? Hurry up and give my regards to my brother."
		tLaborday2015_Strike_Npc_Text[4][8156]["Text4"] = "Haven`t you got the Snack yet? Hurry up and give my regards to my friend."

		tLaborday2015_Strike_Npc_Text[4][8153]["Text5"] = "You received some fireworks! Please set them off in Twin City (310,290)."
		tLaborday2015_Strike_Npc_Text[4][8154]["Text5"] = "You received some fireworks! Please set them off in Twin City (310,290)."
		tLaborday2015_Strike_Npc_Text[4][8155]["Text5"] = "You received some fireworks! Please set them off in Twin City (310,290)."
		tLaborday2015_Strike_Npc_Text[4][8156]["Text5"] = "You received some fireworks! Please set them off in Twin City (310,290)."

		tLaborday2015_Strike_Npc_Text[4][8153]["Text6"] = "What are you doing here? Why not set off the Union Firework in Twin City (310,290)?"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text6"] = "What are you doing here? Why not set off the Friendship Firework in Twin City (310,290)?"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text6"] = "What are you doing here? Why not set off the Colorful Firework in Twin City (310,290)?"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text6"] = "What are you doing here? Why not set off the Joy Firework in Twin City (310,290)?"

		tLaborday2015_Strike_Npc_Text[4][8153]["Text7"] = "That is exactly what I need. Thanks!"
		tLaborday2015_Strike_Npc_Text[4][8154]["Text7"] = "That is exactly what I need. Thanks!"
		tLaborday2015_Strike_Npc_Text[4][8155]["Text7"] = "That is exactly what I need. Thanks!'"
		tLaborday2015_Strike_Npc_Text[4][8156]["Text7"] = "That is exactly what I need. Thanks!'"

--模板??
		tLaborday2015_Strike_Text = {}
		tLaborday2015_Strike_Text[8151] = {}
		tLaborday2015_Strike_Text[8151]["Text111"] = "Labor Day is a holiday that honors the working man!"
		tLaborday2015_Strike_Text[8151]["Text112"] = "~Say, have you heard? The Union will hold a celebration from Sep. 7th to 13th."
		tLaborday2015_Strike_Text[8151]["Text113"] = "~After the whole year`s hard work, let`s relax and join in these fun activities!"
		tLaborday2015_Strike_Text[8151]["Option1"] = "Not~interested."

		tLaborday2015_Strike_Text[8151]["Option2"] = "General~Strike."
		tLaborday2015_Strike_Text[8151]["Option3"] = "Celebration~Card."
		tLaborday2015_Strike_Text[8151]["Option4"] = "Finding~Fault."
		tLaborday2015_Strike_Text[8151]["Option5"] = "Not~interested."

		tLaborday2015_Strike_Text[8151]["Text211"] = "The Union is planning a General Strike, to get fair treatment and higher wages."
		tLaborday2015_Strike_Text[8151]["Text212"] = "~A series of events will be carried out, according to their strike schedule."
		tLaborday2015_Strike_Text[8151]["Text213"] = "~If you are interested and willing to give your support, please talk to President Sun for more details."
		tLaborday2015_Strike_Text[8151]["Option6"] = "OK."
		tLaborday2015_Strike_Text[8151]["Option7"] = "Other~events."
		
		tLaborday2015_Strike_Text[8151]["Text311"] = "You can draw Celebration Cards from the Lucky Box. Find the Card Collector and answer his questions,"
		tLaborday2015_Strike_Text[8151]["Text312"] = "~and you can get a Red, Blue or Purple Card. If you can answer all 3 questions, you will receive a gift!"
		tLaborday2015_Strike_Text[8151]["Option8"] = "That~would~be~great."

		tLaborday2015_Strike_Text[8151]["Text411"] = "The workers are practicing on the playground everyday, for their part in the parade. Most of them are practicing really hard,"
		tLaborday2015_Strike_Text[8151]["Text412"] = "~while some others are just plain lazy or behave weirdly. If you can help sort them out, I`m sure you would receive a reward from the Parade Organizer!"
		tLaborday2015_Strike_Text[8151]["Option9"] = "I`m~ready~to~help."

		tLaborday2015_Strike_Text[1] = "You received the Attendance Sheet. Please go find the 4 leaders."
		tLaborday2015_Strike_Text[2] = "You obtained one-hour Double EXP time!"
		tLaborday2015_Strike_Text[3] = "You`ve got a bag of forage. Hurry up and throw it into the river (Twin~City~250,380)!"
		tLaborday2015_Strike_Text[4] = "Your inventory is full."
		tLaborday2015_Strike_Text[5] = "Steal"
		tLaborday2015_Strike_Text[6] = "Please set off the fireworks around Twin City (310,290)."
		tLaborday2015_Strike_Text[7] = "You set off some fireworks! Happy Labor Day!"

		tLaborday2015_Strike_Text[8152] = {}
		tLaborday2015_Strike_Text[8152]["Text111"] = "We are planning a General Strike for fair treatment and higher wages. Our men just aren`t treated well enough!"
		tLaborday2015_Strike_Text[8152]["Text112"] = "~A series of events will be carried out, and we need good help. If you are interested and willing to help, please, join us!"
		tLaborday2015_Strike_Text[8152]["Option1"] = "The~details?"
		tLaborday2015_Strike_Text[8152]["Option2"] = "I`ll~think~about~it."
		tLaborday2015_Strike_Text[8152]["Text211"] = "Thanks for your support, but the schedule hasn`t been worked out, yet."
		tLaborday2015_Strike_Text[8152]["Text212"] = "~Please come speak to me, later. Sometime between Sep. 7th and 13th should be ok!"
		tLaborday2015_Strike_Text[8152]["Option3"] = "Alright."
		tLaborday2015_Strike_Text[8152]["Text221"] = "Now we are in the first phase, that is the `Fatigue Period`. It will last from Sep. 7th to 8th. The workers in Twin City are tired and overworked."
		tLaborday2015_Strike_Text[8152]["Text222"] = "Could you please give them a hand? They need time to take a break, so we can discuss our plans for the General Strike."
		tLaborday2015_Strike_Text[8152]["Option4"] = "No~problem."
		tLaborday2015_Strike_Text[8152]["Text311"] = "Now we are in the second phase, that is the Preparation Phase. It will last from Sep. 9th to 11th. The Union is making all the arrangements for the General Strike. My friend,"
		tLaborday2015_Strike_Text[8152]["Text312"] = "~please take this Attendance Sheet with you, and find the leaders, Ironsmith Chiu, Tailor Hui, Chef Zhang and Stableman Wong. After all of them have signed the document, please bring it back to me."
		tLaborday2015_Strike_Text[8152]["Option5"] = "No~problem."
		tLaborday2015_Strike_Text[8152]["Option6"] = "Here~it~is."
		tLaborday2015_Strike_Text[8152]["Option7"] = "Sorry,~I`m~busy."
		tLaborday2015_Strike_Text[8152]["Text321"] = "I`m sorry, but you haven`t reached Level 80, yet. Please come back and help later."
		tLaborday2015_Strike_Text[8152]["Option20"] = "Roger."
		tLaborday2015_Strike_Text[8152]["Text331"] = "What`s up? You`ve already got the Attendance Sheet!"
		tLaborday2015_Strike_Text[8152]["Option21"] = "Didn`t~notice~that."
		tLaborday2015_Strike_Text[8152]["Text341"] = "Hey, hey! Your inventory is full. Get rid of some things, and then take the Attendance Sheet."
		tLaborday2015_Strike_Text[8152]["Option22"] = "I~will."
		tLaborday2015_Strike_Text[8152]["Text351"] = "What`s wrong? I didn`t see the Attendance Sheet in your hand. Did you lose it?"
		tLaborday2015_Strike_Text[8152]["Option23"] = "My~fault."
		tLaborday2015_Strike_Text[8152]["Text361"] = "Hmm. Can you double check the names? It seems someone hasn`t signed the Attendance Sheet, yet."
		tLaborday2015_Strike_Text[8152]["Text362"] = ""
		tLaborday2015_Strike_Text[8152]["Option24"] = "Oh."
		tLaborday2015_Strike_Text[8152]["Text371"] = "Thanks! I owe you one. The leaders agreed to take part in the General Strike, on Sep. 12th."
		tLaborday2015_Strike_Text[8152]["Text372"] = "~But there is still a lot of work to do. Can you pay them a visit? I`m sure they`ll need your help."
		tLaborday2015_Strike_Text[8152]["Option25"] = "Sure."
		tLaborday2015_Strike_Text[8152]["Text381"] = "Now we`ve come to the 2nd phase, that is the Preparation Phase from Sep 9th to 11th. It`ll last. The Union is making all the arrangements for the General Strike."
		tLaborday2015_Strike_Text[8152]["Text382"] = "~Thanks for your help."
		tLaborday2015_Strike_Text[8152]["Option26"] = "You`re~welcome."
		tLaborday2015_Strike_Text[8152]["Text411"] = "We will never give in! We won`t stop until our demands are met! Our parade is held up.. It can`t reach the Twin City Gate."
		tLaborday2015_Strike_Text[8152]["Text412"] = "~And to make matter worse, we can`t get inside and talk with the officers. Please, we need your help!"
		tLaborday2015_Strike_Text[8152]["Option8"] = "What~shall~I~do?"
		tLaborday2015_Strike_Text[8152]["Option9"] = "I~can`t~do~anything."
		tLaborday2015_Strike_Text[8152]["Text511"] = "Please find Ironsmith Chiu, Tailor Hui, Chef Zhang and Stableman Wong. They`re outside of Twin City."
		tLaborday2015_Strike_Text[8152]["Text512"] = "~They will tell you what we need."
		tLaborday2015_Strike_Text[8152]["Option10"] = "OK."
		tLaborday2015_Strike_Text[8152]["Text611"] = "Thanks to the workers` persistence and your support, the government has promised that all demands will be met!"
		tLaborday2015_Strike_Text[8152]["Text612"] = "~And they`ve even given us 4 days off! Everybody is out celebrating the victory. But.... I`m afraid there is still something else."
		tLaborday2015_Strike_Text[8152]["Option11"] = "What~is~it?"
		tLaborday2015_Strike_Text[8152]["Option12"] = "Claim~Double~EXP."
		tLaborday2015_Strike_Text[8152]["Option13"] = "Leave."
		tLaborday2015_Strike_Text[8152]["Text711"] = "The strike ended in victory, but something else needs to be followed up on."
		tLaborday2015_Strike_Text[8152]["Text712"] = "~Can you pay a visit to Ironsmith Chiu, Tailor Hui, Chef Zhang and Stableman Wong? I believe they still need your help with some things."
		tLaborday2015_Strike_Text[8152]["Option14"] = "OK.~I~will."
		tLaborday2015_Strike_Text[8152]["Text811"] = "Labor Day is over.... We all have to go back to work, now."
		tLaborday2015_Strike_Text[8152]["Option15"] = "Bad."
		tLaborday2015_Strike_Text[8152]["Text831"] = "I`m sorry, but you have claimed your Double EXP time, today."
		tLaborday2015_Strike_Text[8152]["Option30"] = "Alright."
		
		tLaborday2015_Strike_Text[8164] = {}
		tLaborday2015_Strike_Text[8164]["Text111"] = "Why are the workers on strike? How unfortunate. I must get it settled, soon."
		tLaborday2015_Strike_Text[8164]["Text112"] = ""
		tLaborday2015_Strike_Text[8164]["Option1"] = "Good~luck."
		tLaborday2015_Strike_Text[8164]["Text121"] = "What? Are you crazy? That`s impossible!"
		tLaborday2015_Strike_Text[8164]["Option2"] = "They`re~serious."
		tLaborday2015_Strike_Text[8164]["Text211"] = "Hey, wait, wait. If they go on like this, I will lose my job! Well, I will try my best to persuade my bosses,"
		tLaborday2015_Strike_Text[8164]["Text212"] = "~but I can`t promise anything. But, they must return to work first, or there will be no discussion!"
		tLaborday2015_Strike_Text[8164]["Option3"] = "Will~do."

		tLaborday2015_Strike_Text[8153] = {}
		tLaborday2015_Strike_Text[8153]["Text111"] = "Oh my god! Why is there endless work, all the time?"
		tLaborday2015_Strike_Text[8153]["Option1"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8153]["Text121"] = "Alas! This work will never stop! I just want to go to the bathroom..."
		tLaborday2015_Strike_Text[8153]["Option2"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8153]["Option3"] = "Give~Knife."
		tLaborday2015_Strike_Text[8153]["Option4"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8153]["Text231"] = "Great! I know something. Do you think you can grind these Blunt Knives for me?"
		tLaborday2015_Strike_Text[8153]["Text232"] = "~It`s not too hard, and they don`t have to be perfect. Just give me a nice once over!"
		tLaborday2015_Strike_Text[8153]["Option7"] = "No~problem."
		tLaborday2015_Strike_Text[8153]["Text251"] = "Are you kidding me? Where is the Sharp Knife?"
		tLaborday2015_Strike_Text[8153]["Option9"] = "Oh,~sorry."
		tLaborday2015_Strike_Text[8153]["Text261"] = "Thanks very much. You have been a great help!"
		tLaborday2015_Strike_Text[8153]["Option10"] = "My~pleasure."
		tLaborday2015_Strike_Text[8153]["Text131"] = "Why are you in such a hurry? What can I do for you?"
		tLaborday2015_Strike_Text[8153]["Option11"] = "Sign~this~document."
		tLaborday2015_Strike_Text[8153]["Option12"] = "Nothing."
		tLaborday2015_Strike_Text[8153]["Text321"] = "Now, all the leaders have signed the Attendance Sheet. Be careful and return it to President Sun, please."
		tLaborday2015_Strike_Text[8153]["Text322"] = "~By the way, can you come back after that? I`ll still need your help, if you are free."
		tLaborday2015_Strike_Text[8153]["Option13"] = "No~problem."
		tLaborday2015_Strike_Text[8153]["Text331"] = "Nice to see you again! Could you do me a favor and hand out these Leaflets in Twin City?"
		tLaborday2015_Strike_Text[8153]["Text332"] = "~I believe more and more workers will join our cause, if we get the word out!"
		tLaborday2015_Strike_Text[8153]["Option14"] = "Good~idea."
		tLaborday2015_Strike_Text[8153]["Option15"] = "Not~now."
		tLaborday2015_Strike_Text[8153]["Text341"] = "OK. I`ve signed it. Now, you need to find the others and get them to sign."
		tLaborday2015_Strike_Text[8153]["Option16"] = "Yup."
		tLaborday2015_Strike_Text[8153]["Text351"] = "So... Where is the Attendance Sheet?"
		tLaborday2015_Strike_Text[8153]["Option17"] = "It~was~here."
		tLaborday2015_Strike_Text[8153]["Text361"] = "You got the Leaflets. Hurry up and hand them out in Twin City (310,290)."
		tLaborday2015_Strike_Text[8153]["Option18"] = "I~will."
		tLaborday2015_Strike_Text[8153]["Text411"] = "The city government is blocking the parade from reaching the Twin City Gate! So, how can we talk with them? We need to convince them to allow our men through!"
		tLaborday2015_Strike_Text[8153]["Option22"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8153]["Option23"] = "I`ve~talked~to~them."
		tLaborday2015_Strike_Text[8153]["Option24"] = "I`ve~got~to~go."
		tLaborday2015_Strike_Text[8153]["Text421"] = "Please talk to the Officer for me, and tell him our demands."
		tLaborday2015_Strike_Text[8153]["Option25"] = "OK.~I~will."
		tLaborday2015_Strike_Text[8153]["Text431"] = "Hmm. You promised to help the other leaders and talk to the Officer? Alright. Come see me, when you`re done."
		tLaborday2015_Strike_Text[8153]["Option26"] = "Got~it."
		tLaborday2015_Strike_Text[8153]["Text441"] = "Don`t bother me."
		tLaborday2015_Strike_Text[8153]["Option27"] = "Sorry."
		tLaborday2015_Strike_Text[8153]["Text511"] = "Really? Whatever can we do to thank you for all your hard work?"
		tLaborday2015_Strike_Text[8153]["Option28"] = "You`re~welcome."
		tLaborday2015_Strike_Text[8153]["Text151"] = "Justice always triumphs! What a wonderful day! We will all be able to rest, and enjoy our time!"
		tLaborday2015_Strike_Text[8153]["Option30"] = "What~shall~I~do?"
		tLaborday2015_Strike_Text[8153]["Text521"] = "Look. I`m an ironsmith, but I don`t even have a weapon of my own! You can`t imagine the looks I get...."
		tLaborday2015_Strike_Text[8153]["Text522"] = "~Do you think... maybe... you could go buy one for me?"
		tLaborday2015_Strike_Text[8153]["Option31"] = "Sure."
		tLaborday2015_Strike_Text[8153]["Option32"] = "I`m~afraid~not."
		tLaborday2015_Strike_Text[8153]["Text531"] = "Hi. I didn`t think you would come back so soon!"
		tLaborday2015_Strike_Text[8153]["Text532"] = "~Did you bring back the weapon? Nobody saw you coming back here, did they?"
		tLaborday2015_Strike_Text[8153]["Option33"] = "Here~it~is."
		tLaborday2015_Strike_Text[8153]["Option34"] = "Not~yet."
		tLaborday2015_Strike_Text[8153]["Text541"] = "Hey, hey! Can you give this bottle of Aged Wine to my friend?"
		tLaborday2015_Strike_Text[8153]["Text542"] = "~Oh, and give him my warm regards, too!"
		tLaborday2015_Strike_Text[8153]["Option35"] = "Sure."
		tLaborday2015_Strike_Text[8153]["Option36"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8153]["Option37"] = "I`m~busy."
		tLaborday2015_Strike_Text[8153]["Text551"] = "It is a great moment. I`m so very excited! Could you please set off the fireworks in Twin City (310,290)."
		tLaborday2015_Strike_Text[8153]["Text552"] = "~It would be beautiful."
		tLaborday2015_Strike_Text[8153]["Option38"] = "Gimme."
		tLaborday2015_Strike_Text[8153]["Option39"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8153]["Option40"] = "Not~interested."
		tLaborday2015_Strike_Text[8153]["Text611"] = "Justice always win! That`s wonderful. I will take a good rest."
		tLaborday2015_Strike_Text[8153]["Option41"] = "Good."
		
		tLaborday2015_Strike_Text[8154] = {}
		tLaborday2015_Strike_Text[8154]["Text111"] = "Oh my god! Will it never end? From sun up to sun down.... work, work, work!!!"
		tLaborday2015_Strike_Text[8154]["Option1"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8154]["Text121"] = "Alas! Why is there never any end to all this work???"
		tLaborday2015_Strike_Text[8154]["Option2"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8154]["Option3"] = "Give~Dress."
		tLaborday2015_Strike_Text[8154]["Option4"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8154]["Text231"] = "Great! Can you sew the buttons on these dresses, for me? I would really appreciate the hand."
		tLaborday2015_Strike_Text[8154]["Text232"] = "~I just have so much to do, and not nearly enough hands!"
		tLaborday2015_Strike_Text[8154]["Option7"] = "No~problem."
		tLaborday2015_Strike_Text[8154]["Text251"] = "Are you kidding me? Where is the Finished Dress?"
		tLaborday2015_Strike_Text[8154]["Option9"] = "Oh,~sorry."
		tLaborday2015_Strike_Text[8154]["Text261"] = "Thanks very much. You have been a great help!"
		tLaborday2015_Strike_Text[8154]["Option10"] = "My~pleasure."
		tLaborday2015_Strike_Text[8154]["Text131"] = "Why are you in such a hurry? What can I do for you?"
		tLaborday2015_Strike_Text[8154]["Option11"] = "Sign~this~document."
		tLaborday2015_Strike_Text[8154]["Option12"] = "Nothing."
		tLaborday2015_Strike_Text[8154]["Text321"] = "Now, all the leaders have signed the Attendance Sheet. Be careful and return it to President Sun, please."
		tLaborday2015_Strike_Text[8154]["Text322"] = "~By the way, can you come back after that? I`ll still need your help, if you are free."
		tLaborday2015_Strike_Text[8154]["Option13"] = "No~problem."
		tLaborday2015_Strike_Text[8154]["Text331"] = "Oh, hello again. Can you do me a favor and get some cloth? I`m going to make some banners for the General Strike, but,"
		tLaborday2015_Strike_Text[8154]["Text332"] = "~those damned Pheasants keep stealing my cloth! Please get it back from those blasted birds! I don`t care what it takes!"
		tLaborday2015_Strike_Text[8154]["Option14"] = "No~problem."
		tLaborday2015_Strike_Text[8154]["Option5"] = "Give~cloth."
		tLaborday2015_Strike_Text[8154]["Option15"] = "None~of~my~business."
		tLaborday2015_Strike_Text[8154]["Text341"] = "OK. I`ve signed it. Now, you need to find the others and get them to sign."
		tLaborday2015_Strike_Text[8154]["Option16"] = "Gotcha."
		tLaborday2015_Strike_Text[8154]["Text351"] = "So... Where is the Attendance Sheet?"
		tLaborday2015_Strike_Text[8154]["Option17"] = "Hmm."
		tLaborday2015_Strike_Text[8154]["Text361"] = "You can`t be serious. Where is my cloth?"
		tLaborday2015_Strike_Text[8154]["Option18"] = "Sorry."
		tLaborday2015_Strike_Text[8154]["Text411"] = "The city government is blocking the parade from reaching the Twin City Gate! So how can we talk with them? We need to convince them to allow our men through!"
		tLaborday2015_Strike_Text[8154]["Option22"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8154]["Option23"] = "I`ve~talked~to~them."
		tLaborday2015_Strike_Text[8154]["Option24"] = "I`ve~got~to~go."
		tLaborday2015_Strike_Text[8154]["Text421"] = "Please talk to the Officer for me, and tell him our demands."
		tLaborday2015_Strike_Text[8154]["Option25"] = "OK.~I~will."
		tLaborday2015_Strike_Text[8154]["Text431"] = "Hmm. You promised to help the other leaders and talk to the Officer? Alright. Come see me, when you`re done."
		tLaborday2015_Strike_Text[8154]["Option26"] = "Got~it."
		tLaborday2015_Strike_Text[8154]["Text441"] = "Don`t bother me."
		tLaborday2015_Strike_Text[8154]["Option27"] = "Sorry."
		tLaborday2015_Strike_Text[8154]["Text511"] = "Perfect! However can we thank you? You`ve truly done a great service."
		tLaborday2015_Strike_Text[8154]["Option28"] = "My~pleasure."
		tLaborday2015_Strike_Text[8154]["Text151"] = "Justice always triumphs! What a wonderful day! We will all be able to rest, and enjoy our time!"
		tLaborday2015_Strike_Text[8154]["Option30"] = "What~shall~I~do?"
		tLaborday2015_Strike_Text[8154]["Text521"] = "I`m planning to dye my hair. You know, I`m going to visit my old friends."
		tLaborday2015_Strike_Text[8154]["Text522"] = "~So, can you go buy some dye for me?"
		tLaborday2015_Strike_Text[8154]["Option31"] = "Sure."
		tLaborday2015_Strike_Text[8154]["Option32"] = "I`m~afraid~not."
		tLaborday2015_Strike_Text[8154]["Text531"] = "Well, hey there! I didn`t think you would come back so soon."
		tLaborday2015_Strike_Text[8154]["Text532"] = "~Well, did you bring back the dye?"
		tLaborday2015_Strike_Text[8154]["Option33"] = "Here~it~is."
		tLaborday2015_Strike_Text[8154]["Option34"] = "Not~yet."
		tLaborday2015_Strike_Text[8154]["Text541"] = "Hey, hey! Can you give this Sachet to my sister?"
		tLaborday2015_Strike_Text[8154]["Text542"] = "~Oh, and give her my regards, too."
		tLaborday2015_Strike_Text[8154]["Option35"] = "Sure."
		tLaborday2015_Strike_Text[8154]["Option36"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8154]["Option37"] = "I`m~busy."
		tLaborday2015_Strike_Text[8154]["Text551"] = "It`s a great moment! I`m so very excited! Could you please set off those fireworks in Twin City (310,290)?"
		tLaborday2015_Strike_Text[8154]["Text552"] = "~It will be beautiful."
		tLaborday2015_Strike_Text[8154]["Option38"] = "Gimme."
		tLaborday2015_Strike_Text[8154]["Option39"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8154]["Option40"] = "Not~interested."
		tLaborday2015_Strike_Text[8154]["Text611"] = "Justice always triumphs! What a wonderful day! We will all be able to rest, and enjoy our time!"
		tLaborday2015_Strike_Text[8154]["Option41"] = "Nice."

		tLaborday2015_Strike_Text[8155] = {}
		tLaborday2015_Strike_Text[8155]["Text111"] = "I`m sorry, but I`m far too busy to talk! There is so much work to be done!"
		tLaborday2015_Strike_Text[8155]["Option1"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8155]["Text121"] = "My hands are so stiff... So much wine to mix... I`ll never get it all done in time..."
		tLaborday2015_Strike_Text[8155]["Option2"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8155]["Option3"] = "Give~Mixed~Wine."
		tLaborday2015_Strike_Text[8155]["Option4"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8155]["Text231"] = "Great! Can you mix this wine for me? I have a big order to fill, but there`s no way I"
		tLaborday2015_Strike_Text[8155]["Text232"] = "~can get all of it done. Just mix the two together, and don`t spill any! It`s expensive!"
		tLaborday2015_Strike_Text[8155]["Option7"] = "No~problem."
		tLaborday2015_Strike_Text[8155]["Text251"] = "Are you kidding me? Where is the Mixed Wine?"
		tLaborday2015_Strike_Text[8155]["Option9"] = "Oh,~sorry."
		tLaborday2015_Strike_Text[8155]["Text261"] = "Thanks very much. You have been a great help!"
		tLaborday2015_Strike_Text[8155]["Option10"] = "My~pleasure."
		tLaborday2015_Strike_Text[8155]["Text131"] = "Why are you in such a hurry? What can I do for you?"
		tLaborday2015_Strike_Text[8155]["Option11"] = "Sign~this~document."
		tLaborday2015_Strike_Text[8155]["Option12"] = "Nothing."
		tLaborday2015_Strike_Text[8155]["Text321"] = "Now, all the leaders have signed the Attendance Sheet. Be careful and return it to President Sun, please."
		tLaborday2015_Strike_Text[8155]["Text322"] = "~By the way, can you come back after that? I`ll still need your help, if you are free."
		tLaborday2015_Strike_Text[8155]["Option13"] = "No~problem."
		tLaborday2015_Strike_Text[8155]["Text331"] = "Nice to see you again, my friend. Can you do me a favor? I need to collect some eggs from the Pheasants."
		tLaborday2015_Strike_Text[8155]["Text332"] = "~I`m preparing some food for the strikers, and I need someone to get the eggs, while I mix this batter. Do you think you can help me out with that?"
		tLaborday2015_Strike_Text[8155]["Option14"] = "I~won`t~let~you~down."
		tLaborday2015_Strike_Text[8155]["Option5"] = "Give~eggs."
		tLaborday2015_Strike_Text[8155]["Option15"] = "I`m~busy~now."
		tLaborday2015_Strike_Text[8155]["Text341"] = "Done. I`ve signed it. Now, you need to find the others and get them to sign."
		tLaborday2015_Strike_Text[8155]["Option16"] = "I~will."
		tLaborday2015_Strike_Text[8155]["Text351"] = "So... Where is the Attendance Sheet?"
		tLaborday2015_Strike_Text[8155]["Option17"] = "I~mess~it~up."
		tLaborday2015_Strike_Text[8155]["Text361"] = "Are you joking? Where are the eggs?"
		tLaborday2015_Strike_Text[8155]["Option18"] = "It`s~a~mistake."
		tLaborday2015_Strike_Text[8155]["Text411"] = "The city government is blocking the parade from reaching the Twin City Gate! So how can we talk with them? We need to convince them to allow our men through!"
		tLaborday2015_Strike_Text[8155]["Option22"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8155]["Option23"] = "I`ve~talked~to~them."
		tLaborday2015_Strike_Text[8155]["Option24"] = "I`ve~got~to~go."
		tLaborday2015_Strike_Text[8155]["Text421"] = "Please talk to the Officer for me, and tell him our demands."
		tLaborday2015_Strike_Text[8155]["Option25"] = "OK.~I~will."
		tLaborday2015_Strike_Text[8155]["Text431"] = "Hmm. You promised to help the other leaders and talk to the officers? Please come back after that."
		tLaborday2015_Strike_Text[8155]["Option26"] = "Got~it."
		tLaborday2015_Strike_Text[8155]["Text441"] = "Don`t bother me."
		tLaborday2015_Strike_Text[8155]["Option27"] = "Sorry."
		tLaborday2015_Strike_Text[8155]["Text511"] = "Amazing! How can we ever thank you! You have done us all a great service."
		tLaborday2015_Strike_Text[8155]["Option28"] = "My~pleasure."
		tLaborday2015_Strike_Text[8155]["Text151"] = "Justice always triumphs! What a wonderful day! We will all be able to rest, and enjoy our time!"
		tLaborday2015_Strike_Text[8155]["Option30"] = "What~shall~I~do?"
		tLaborday2015_Strike_Text[8155]["Text521"] = "My wife`s birthday is coming. I haven`t bought her a gift in years."
		tLaborday2015_Strike_Text[8155]["Text522"] = "~I was wondering if you can buy some jewelry for me."
		tLaborday2015_Strike_Text[8155]["Option31"] = "Sure."
		tLaborday2015_Strike_Text[8155]["Option32"] = "I`m~afraid~not."
		tLaborday2015_Strike_Text[8155]["Text531"] = "Welcome back! I didn`t think you would come back so soon."
		tLaborday2015_Strike_Text[8155]["Text532"] = "~Well, did you bring back the jewelry?"
		tLaborday2015_Strike_Text[8155]["Option33"] = "Here~it~is."
		tLaborday2015_Strike_Text[8155]["Option34"] = "Not~yet."
		tLaborday2015_Strike_Text[8155]["Text541"] = "Hey, hey! Can you give this Recipe to my brother?"
		tLaborday2015_Strike_Text[8155]["Text542"] = "~Oh, and give him my regards, too."
		tLaborday2015_Strike_Text[8155]["Option35"] = "Sure."
		tLaborday2015_Strike_Text[8155]["Option36"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8155]["Option37"] = "I`m~busy."
		tLaborday2015_Strike_Text[8155]["Text551"] = "It is a great moment! I`m so very excited! Could you please set off those fireworks in Twin City (310,290)?"
		tLaborday2015_Strike_Text[8155]["Text552"] = "~It would be beautiful."
		tLaborday2015_Strike_Text[8155]["Option38"] = "Gimme."
		tLaborday2015_Strike_Text[8155]["Option39"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8155]["Option40"] = "Not~interested."
		tLaborday2015_Strike_Text[8155]["Text611"] = "Justice always triumphs! What a wonderful day! We will all be able to rest, and enjoy our time!"
		tLaborday2015_Strike_Text[8155]["Option41"] = "Nice."

		tLaborday2015_Strike_Text[8156] = {}
		tLaborday2015_Strike_Text[8156]["Text111"] = "Look at all this feed! I can`t possibly get all this sent off... Sorry, but I`m too busy to talk, now."
		tLaborday2015_Strike_Text[8156]["Option1"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8156]["Text121"] = "Now they need more at the Stables! Why do they always change their orders?"
		tLaborday2015_Strike_Text[8156]["Option2"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8156]["Option3"] = "I~have~done~it."
		tLaborday2015_Strike_Text[8156]["Option4"] = "You~must~be~tired."
		tLaborday2015_Strike_Text[8156]["Text231"] = "Great! Can you take the Forage to the stable for me? I`d go, but I have to watch the shop."
		tLaborday2015_Strike_Text[8156]["Text232"] = ""
		tLaborday2015_Strike_Text[8156]["Option7"] = "No~problem."
		tLaborday2015_Strike_Text[8156]["Text251"] = "Are you serious? The Forage wasn`t delivered to the stables. What happened?"
		tLaborday2015_Strike_Text[8156]["Option9"] = "Oh,~sorry."
		tLaborday2015_Strike_Text[8156]["Text261"] = "Thanks very much. You have been a great help!"
		tLaborday2015_Strike_Text[8156]["Option10"] = "My~pleasure."
		tLaborday2015_Strike_Text[8156]["Text131"] = "Why are you in such a hurry? What can I do for you?"
		tLaborday2015_Strike_Text[8156]["Option11"] = "Sign~this~document."
		tLaborday2015_Strike_Text[8156]["Option12"] = "Nothing."
		tLaborday2015_Strike_Text[8156]["Text321"] = "Now, all the leaders have signed the Attendance Sheet. Be careful and return it to President Sun, please."
		tLaborday2015_Strike_Text[8156]["Text322"] = "~By the way, can you come back after that? I`ll still need your help, if you are free."
		tLaborday2015_Strike_Text[8156]["Option13"] = "No~problem."
		tLaborday2015_Strike_Text[8156]["Text331"] = "Nice to see you again. My boss tries every way he can think of to screw over the workers. I`m fed up! We`ll teach him a lesson! I was wondering..."
		tLaborday2015_Strike_Text[8156]["Text332"] = "~Maybe you can steal some of his bags of forage, and throw them into the river! That might make him respect all that we do for him!"
		tLaborday2015_Strike_Text[8156]["Option14"] = "You~can~count~on~me."
		tLaborday2015_Strike_Text[8156]["Option15"] = "I`ll~leave~you~alone."
		tLaborday2015_Strike_Text[8156]["Text341"] = "OK. I`ve signed it. Now, you need to find the others and get them to sign."
		tLaborday2015_Strike_Text[8156]["Option16"] = "Yup."
		tLaborday2015_Strike_Text[8156]["Text351"] = "So... Where is the Attendance Sheet?"
		tLaborday2015_Strike_Text[8156]["Option17"] = "It~was~here."
		tLaborday2015_Strike_Text[8156]["Text411"] = "The city government is blocking the parade from reaching the Twin City Gate! So how can we talk with them? We need to convince them to allow our men through!"
		tLaborday2015_Strike_Text[8156]["Option22"] = "Can~I~help?"
		tLaborday2015_Strike_Text[8156]["Option23"] = "I`ve~talked~to~them."
		tLaborday2015_Strike_Text[8156]["Option24"] = "I`ve~got~to~go."
		tLaborday2015_Strike_Text[8156]["Text421"] = "Please talk to the Officer for me, and tell him our demands."
		tLaborday2015_Strike_Text[8156]["Option25"] = "OK.~I~will."
		tLaborday2015_Strike_Text[8156]["Text431"] = "Hmm. You promised to help the other leaders and talk to the officers? Please come back after that."
		tLaborday2015_Strike_Text[8156]["Option26"] = "Got~it."
		tLaborday2015_Strike_Text[8156]["Text441"] = "Don`t bother me."
		tLaborday2015_Strike_Text[8156]["Option27"] = "Sorry."
		tLaborday2015_Strike_Text[8156]["Text511"] = "Amazing! How can we ever thank you! You have done us all a great service."
		tLaborday2015_Strike_Text[8156]["Option28"] = "My~pleasure."
		tLaborday2015_Strike_Text[8156]["Text151"] = "Justice always triumphs! What a wonderful day! We will all be able to rest, and enjoy our time!"
		tLaborday2015_Strike_Text[8156]["Option30"] = "What~shall~I~do?"
		tLaborday2015_Strike_Text[8156]["Text521"] = "Aha, I will not live in the stable any more. I`m going to clean myself up and go out and get a date!"
		tLaborday2015_Strike_Text[8156]["Text522"] = "~Anyway, my hair is just sooooo last season. Do you think you could you buy a hat for me?"
		tLaborday2015_Strike_Text[8156]["Option31"] = "No~problem."
		tLaborday2015_Strike_Text[8156]["Option32"] = "I`m~afraid~not."
		tLaborday2015_Strike_Text[8156]["Text531"] = "Hi. I didn`t think you would come back so soon."
		tLaborday2015_Strike_Text[8156]["Text532"] = "~Well, did you bring back the hat?"
		tLaborday2015_Strike_Text[8156]["Option33"] = "Here~it~is."
		tLaborday2015_Strike_Text[8156]["Option34"] = "Not~yet."
		tLaborday2015_Strike_Text[8156]["Text541"] = "Hey, hey! Can you give the Snack to my friend?"
		tLaborday2015_Strike_Text[8156]["Text542"] = "~Oh, and give him my regards, too."
		tLaborday2015_Strike_Text[8156]["Option35"] = "Sure."
		tLaborday2015_Strike_Text[8156]["Option36"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8156]["Option37"] = "I`m~busy."
		tLaborday2015_Strike_Text[8156]["Text551"] = "It is a great moment! I`m so very excited! Could you please set off those fireworks in Twin City (310,290)?"
		tLaborday2015_Strike_Text[8156]["Text552"] = "~It would be beautiful."
		tLaborday2015_Strike_Text[8156]["Option38"] = "Gimme."
		tLaborday2015_Strike_Text[8156]["Option39"] = "I`ve~done~it."
		tLaborday2015_Strike_Text[8156]["Option40"] = "I`m~busy."
		tLaborday2015_Strike_Text[8156]["Text611"] = "Justice always triumphs! What a wonderful day! We will all be able to rest, and enjoy our time!"
		tLaborday2015_Strike_Text[8156]["Option41"] = "Nice."
		
--常用??
		tLaborday2015_Strike_Text["Text"] = {}
		tLaborday2015_Strike_Text["Option"] = {}
		tLaborday2015_Strike_Text["Text"][1] = "That`s enough for today. Thanks very much. You have been a great help!"
		tLaborday2015_Strike_Text["Option"][1] = "My~pleasure."
		
		tLaborday2015_Strike_Text["Text"][2] = "I`m sorry, but you haven`t reached Level 80, yet. You can try to help when you are stronger."
		tLaborday2015_Strike_Text["Option"][2] = "Got~it."
		
		tLaborday2015_Strike_Text["Text"][3] = "Your inventory is full. Tidy it up and take the wine, please."
		tLaborday2015_Strike_Text["Option"][3] = "I~will."

--------------------------------------------------------------------------------
---Name:[英文征服][活动脚本]劳工节庆典卡片(9.7-9.13)
--Creator: 	陈莺
--Created:	2015/05/05
--------------------------------------------------------------------------------
--庆典卡片箱
 tLaborday2015_card_Text = {}
--提示
tLaborday2015_card_Text["MsgBox"] = {}
tLaborday2015_card_Text["MsgBox"]["GetCard"]="You`ve already had this card. Please submit it to the Card Collector (Twin City, 280,385)"
tLaborday2015_card_Text["MsgBox"]["bag"] = "Your inventory is full!"
tLaborday2015_card_Text["MsgBox"]["finish1"] = "You received 10 minutes` EXP."
tLaborday2015_card_Text["MsgBox"]["finish2"] = "Well done! You received a Festival Joy Pack."
tLaborday2015_card_Text["MsgBox"]["finish3"] = "You`ve got the right answers!"
tLaborday2015_card_Text["MsgBox"]["wrong"] = "Sorry, your answer is wrong!"
tLaborday2015_card_Text["MsgBox"]["finish4"] = "You received 5 Cultivation."
tLaborday2015_card_Text["MsgBox"]["Complete3"] = "You`ve finished the quest today. Please come tomorrow."
-- 活动前
tLaborday2015_card_Text[8046] = {}
tLaborday2015_card_Text[8046]["Text111"] = "    This box is filled with Celebration Cards. Talk to me between Sep 7th - 13th."
tLaborday2015_card_Text[8046]["Text112"] = "~I will tell you more about the magic box, then."
tLaborday2015_card_Text[8046]["Option1"] = "Can`t~wait."
-- 活动后
tLaborday2015_card_Text[8046]["Text121"] = "    Did you have fun during the holiday? I hope so! All my cards have already been taken. See you next year!"
tLaborday2015_card_Text[8046]["Option2"] = "See~ya."
-- 活动中
tLaborday2015_card_Text[8046]["Text131"] = "    This box is full of Celebration Cards with different colors. You can draw one card, every hour."
tLaborday2015_card_Text[8046]["Text132"] = "~If you get a Red, Blue or Purple Card, you should give it to the Card Collector."
tLaborday2015_card_Text[8046]["Text133"] = "~you will receive a gift, once you give correct answers to all his 3 questions."
tLaborday2015_card_Text[8046]["Option3"] = "Let`s~start."
tLaborday2015_card_Text[8046]["Option4"] = "Roger~that."
--等级不足
tLaborday2015_card_Text[8046]["Text211"] = "Sorry, you haven`t reached level 80, yet."
tLaborday2015_card_Text[8046]["Option5"] = "Alright."
--间隔时间不到1小时
tLaborday2015_card_Text[8046]["Text311"] = "I`m sorry, but you have to wait one hour to draw another card. Please wait a few more minutes."
tLaborday2015_card_Text[8046]["Option6"] = "I~have~to."


--韩方
tLaborday2015_card_Text[8045] = {}
--活动前
tLaborday2015_card_Text[8045]["Text111"] = "I have prepared many gifts, for the holiday."
tLaborday2015_card_Text[8045]["Text112"] = "~Please come find me with the Celebration Cards, between Sep 7th and 13th."
tLaborday2015_card_Text[8045]["Option1"] = "Alright."
--活动后
tLaborday2015_card_Text[8045]["Text121"] = "Did you enjoy yourself during the holiday? I hope so! All the gifts have been given out."
tLaborday2015_card_Text[8045]["Text122"] = "~See you next year!"
tLaborday2015_card_Text[8045]["Option2"] = "Bye."
--活动中
tLaborday2015_card_Text[8045]["Text131"] = "I have prepared many gifts, for the holiday."
tLaborday2015_card_Text[8045]["Text132"] = "~Once you get a Red, Blue or Purple Card, you can come find me and answer the questions."
tLaborday2015_card_Text[8045]["Text133"] = "~You will receive a gift, if you can answer all the 3 questions."
tLaborday2015_card_Text[8045]["Text134"] = "~So, which card do you have?'"
tLaborday2015_card_Text[8045]["Option3"] = "Blue~Card."
tLaborday2015_card_Text[8045]["Option4"] = "Red~Card."
tLaborday2015_card_Text[8045]["Option5"] = "Purple~Card."
tLaborday2015_card_Text[8045]["Option6"] = "How~to~get~a~card?"
tLaborday2015_card_Text[8045]["Option7"] = "Later."

--如何得到庆典卡片
tLaborday2015_card_Text[8045]["Text211"] = "You see that Lucky Box? It is full of Celebration Cards, with all different colors!"
tLaborday2015_card_Text[8045]["Text212"] = "~You can draw one card, every hour. Come find me if you get a Red, Blue or Purple Card."
tLaborday2015_card_Text[8045]["Option8"] = "I`m~on~my~way."
--一天答过3次
tLaborday2015_card_Text[8045]["Text311"] = "You`ve answered 3 questions today. Please come tomorrow."
tLaborday2015_card_Text[8045]["Option9"] = "Cool."



tLaborday2015_card_Text[8045]["Text411"] = "Well, you will receive a gift, if you can answer all the 3 questions."
tLaborday2015_card_Text[8045]["Text412"] = "~Otherwise, you have to wait until the next hour, to draw another card."
tLaborday2015_card_Text[8045]["Text413"] = "~Are you ready?"
tLaborday2015_card_Text[8045]["Option11"] = "I`m~ready."
tLaborday2015_card_Text[8045]["Option12"] = "Not~yet."


--获得卡片
 tLaborday2015_card_Text1={}
tLaborday2015_card_Text1[8046] = {}
tLaborday2015_card_Text1[8046][711289] = {}
-- tLaborday2015_card_Text1[8046][711289]["Text"] = "You`ve got a white Card. Happy Labor Day!"
-- tLaborday2015_card_Text1[8046][711289]["Option"] = "Thanks."

tLaborday2015_card_Text1[8046][711286] = {}
tLaborday2015_card_Text1[8046][711286]["Text"] = "You`ve got a Blue Card. Please submit it to the Card Collector (Twin City, 280,385)."
tLaborday2015_card_Text1[8046][711286]["Option"] = "I~will."

tLaborday2015_card_Text1[8046][711287] = {}
tLaborday2015_card_Text1[8046][711287]["Text"] = "You`ve got a red Card. Please submit it to the Card Collector (Twin City, 280,385)."
tLaborday2015_card_Text1[8046][711287]["Option"] = "I~will."

tLaborday2015_card_Text1[8046][711288] = {}
tLaborday2015_card_Text1[8046][711288]["Text"] = "You`ve got a purple Card. Please submit it to the Card Collector (Twin City, 280,385)."
tLaborday2015_card_Text1[8046][711288]["Option"] = "I~will."

tLaborday2015_card_Text1[8045] = {}
tLaborday2015_card_Text1[8045]["Text"] = "Hmm, where is the %s Card? I didn`t find it in your inventory."
tLaborday2015_card_Text1[8045]["Option"] = "Oh."

tLaborday2015_card_Text1["name"] = {}
-- tLaborday2015_card_Text1["name"][711289] = "white"
tLaborday2015_card_Text1["name"][711286] = "blue"
tLaborday2015_card_Text1["name"][711287] = "red"
tLaborday2015_card_Text1["name"][711288] = "purple"

	---蓝卡题目

	tLaborday2015_card_Question = {}
	tLaborday2015_card_Question[711286] = {}
	tLaborday2015_card_Question[711287] = {}
	tLaborday2015_card_Question[711288] = {}
	tLaborday2015_card_Question[711286]["Text"] = {}
	tLaborday2015_card_Question[711286]["Option1"] = {}
	tLaborday2015_card_Question[711286]["Option2"] = {}
	tLaborday2015_card_Question[711286]["Text"][1] = "It`s round and green outside, and yellow or red inside. We have it in summer. What is it?"
	tLaborday2015_card_Question[711286]["Option1"][1] = "Watermelon."
	tLaborday2015_card_Question[711286]["Option2"][1] = "Pineapple."

	
	tLaborday2015_card_Question[711286]["Text"][2] = "What animal sleeps in the day, but flies at night?"
	tLaborday2015_card_Question[711286]["Option1"][2] = "Cat."
	tLaborday2015_card_Question[711286]["Option2"][2] = "Bat."

	
	tLaborday2015_card_Question[711286]["Text"][3] = "What is there in your house that ought to be looked into?"
	tLaborday2015_card_Question[711286]["Option1"][3] = "Mirror."
	tLaborday2015_card_Question[711286]["Option2"][3] = "Door."

	
	tLaborday2015_card_Question[711286]["Text"][4] = "What is it that you have never seen, heard or felt, but it exists and still has a name?"
	tLaborday2015_card_Question[711286]["Option1"][4] = "Time."
	tLaborday2015_card_Question[711286]["Option2"][4] = "Nothing."
	
	
	tLaborday2015_card_Question[711286]["Text"][5] = "When I come down from the sky, I make everything wet. What am I?"
	tLaborday2015_card_Question[711286]["Option1"][5] = "Rain."
	tLaborday2015_card_Question[711286]["Option2"][5] = "Storm."
	
	
	tLaborday2015_card_Question[711286]["Text"][6] = "You can`t catch his body. You can`t see his shadow. When strong, he shakes the house. When weak, the tree leaves."
	tLaborday2015_card_Question[711286]["Option1"][6] = "Rain."
	tLaborday2015_card_Question[711286]["Option2"][6] = "Wind."

	
	tLaborday2015_card_Question[711286]["Text"][7] = "What ship has two mates, but no captain?"
	tLaborday2015_card_Question[711286]["Option1"][7] = "Courtship."
	tLaborday2015_card_Question[711286]["Option2"][7] = "Warship."

	
	tLaborday2015_card_Question[711286]["Text"][8] = "It looks like sugar, but it`s not sweet. It looks like cotton, but it can`t be spun. It comes in winter and makes the weather colder. What is it?"
	tLaborday2015_card_Question[711286]["Option1"][8] = "Rain."
	tLaborday2015_card_Question[711286]["Option2"][8] = "Snow."
	
	
	tLaborday2015_card_Question[711286]["Text"][9] = "What animal wears big black glasses on its face?"
	tLaborday2015_card_Question[711286]["Option1"][9] = "Panda."
	tLaborday2015_card_Question[711286]["Option2"][9] = "Lion."

	
	tLaborday2015_card_Question[711286]["Text"][10] = "What animal carries two humps on its back?"
	tLaborday2015_card_Question[711286]["Option1"][10] = "Camel."
	tLaborday2015_card_Question[711286]["Option2"][10] = "E.T."

	
	tLaborday2015_card_Question[711286]["Text"][11] = "What person does every man take his hat off to?"
	tLaborday2015_card_Question[711286]["Option1"][11] = "A~teacher."
	tLaborday2015_card_Question[711286]["Option2"][11] = "A~barber."

	
	tLaborday2015_card_Question[711286]["Text"][12] = "What rises in the morning and waves all day?"
	tLaborday2015_card_Question[711286]["Option1"][12] = "Flag."
	tLaborday2015_card_Question[711286]["Option2"][12] = "Sun."


	tLaborday2015_card_Question[711286]["Text"][13] = "It belongs to you. But always used by others. What is it?"
	tLaborday2015_card_Question[711286]["Option1"][13] = "Coat."
	tLaborday2015_card_Question[711286]["Option2"][13] = "Name."

	
	tLaborday2015_card_Question[711286]["Text"][14] = "What goes on four legs in the morning, on two at noon, and on three in the evening?"
	tLaborday2015_card_Question[711286]["Option1"][14] = "Human."
	tLaborday2015_card_Question[711286]["Option2"][14] = "Dragon."

	
	tLaborday2015_card_Question[711286]["Text"][15] = "What three letters turn a girl into a woman?"
	tLaborday2015_card_Question[711286]["Option1"][15] = "Eye."
	tLaborday2015_card_Question[711286]["Option2"][15] = "Age."

	
	tLaborday2015_card_Question[711286]["Text"][16] = "What country is popular on Thanksgiving Day?"
	tLaborday2015_card_Question[711286]["Option1"][16] = "Turkey."
	tLaborday2015_card_Question[711286]["Option2"][16] = "China."

	
	tLaborday2015_card_Question[711286]["Text"][17] = "What has two legs but can`t walk?"
	tLaborday2015_card_Question[711286]["Option1"][17] = "Compass."
	tLaborday2015_card_Question[711286]["Option2"][17] = "Clock."

	
	tLaborday2015_card_Question[711286]["Text"][18] = "What bank has no money?"
	tLaborday2015_card_Question[711286]["Option1"][18] = "World~Bank."
	tLaborday2015_card_Question[711286]["Option2"][18] = "River~bank."

	
	tLaborday2015_card_Question[711286]["Text"][19] = "What is always ready to come, but you never see?"
	tLaborday2015_card_Question[711286]["Option1"][19] = "Money."
	tLaborday2015_card_Question[711286]["Option2"][19] = "Tomorrow."

	
	tLaborday2015_card_Question[711286]["Text"][20] = "What do you call your father-in-law`s only child`s mother-in-law?"
	tLaborday2015_card_Question[711286]["Option1"][20] = "Mom."
	tLaborday2015_card_Question[711286]["Option2"][20] = "Aunt."

	
---红卡题目
	tLaborday2015_card_Question[711287]["Text"] = {}
	tLaborday2015_card_Question[711287]["Option1"] = {}
	tLaborday2015_card_Question[711287]["Option2"] = {}
	--tLaborday2015_card_Question[711287]["Option1Func"] = {}
	--tLaborday2015_card_Question[711287]["Option2Func"] = {}
	tLaborday2015_card_Question[711287]["Text"][1] = "Which is faster, heat or cold?"
	tLaborday2015_card_Question[711287]["Option1"][1] = "Heat."
	tLaborday2015_card_Question[711287]["Option2"][1] = "Cold."

	
	tLaborday2015_card_Question[711287]["Text"][2] = "There is a question, and you always have to answer `no`. What is it?"
	tLaborday2015_card_Question[711287]["Option1"][2] = "Are~you~crazy?"
	tLaborday2015_card_Question[711287]["Option2"][2] = "Are~you~asleep?"

	
	tLaborday2015_card_Question[711287]["Text"][3] = "What is the fastest way to double your money?"
	tLaborday2015_card_Question[711287]["Option1"][3] = "Fold~it."
	tLaborday2015_card_Question[711287]["Option2"][3] = "Deposit."

	
	tLaborday2015_card_Question[711287]["Text"][4] = "What is the largest ant in the world?"
	tLaborday2015_card_Question[711287]["Option1"][4] = "Driver~Ant."
	tLaborday2015_card_Question[711287]["Option2"][4] = "An~elephant."

	
	tLaborday2015_card_Question[711287]["Text"][5] = "What room has no walls, no doors, no windows and is uninhabited?"
	tLaborday2015_card_Question[711287]["Option1"][5] = "Stockroom."
	tLaborday2015_card_Question[711287]["Option2"][5] = "Mushroom."

	
	tLaborday2015_card_Question[711287]["Text"][6] = "What kind of dog doesn`t bite or bark?"
	tLaborday2015_card_Question[711287]["Option1"][6] = "Hot~dog."
	tLaborday2015_card_Question[711287]["Option2"][6] = "Nice~dog."

	
	tLaborday2015_card_Question[711287]["Text"][7] = "Who earns a living by driving his customers away?"
	tLaborday2015_card_Question[711287]["Option1"][7] = "Taxi~driver."
	tLaborday2015_card_Question[711287]["Option2"][7] = "Doctor."

	
	tLaborday2015_card_Question[711287]["Text"][8] = "How do we know the ocean is friendly?"
	tLaborday2015_card_Question[711287]["Option1"][8] = "It~crashes."
	tLaborday2015_card_Question[711287]["Option2"][8] = "It~waves."

	
	tLaborday2015_card_Question[711287]["Text"][9] = "What goes up and never comes down?"
	tLaborday2015_card_Question[711287]["Option1"][9] = "Age."
	tLaborday2015_card_Question[711287]["Option2"][9] = "Salary."

	
	tLaborday2015_card_Question[711287]["Text"][10] = "What is at the end of everything?"
	tLaborday2015_card_Question[711287]["Option1"][10] = "Letter~G."
	tLaborday2015_card_Question[711287]["Option2"][10] = "End."

	
	tLaborday2015_card_Question[711287]["Text"][11] = "What book has the most stirring chapters?"
	tLaborday2015_card_Question[711287]["Option1"][11] = "A~story~book."
	tLaborday2015_card_Question[711287]["Option2"][11] = "A~cook~book."

	
	tLaborday2015_card_Question[711287]["Text"][12] = "When are 2 and 2 more than 4?"
	tLaborday2015_card_Question[711287]["Option1"][12] = "When~they~make~22."
	tLaborday2015_card_Question[711287]["Option2"][12] = "Do~a~sum~wrong."


	tLaborday2015_card_Question[711287]["Text"][13] = "What letter makes a road broad?"
	tLaborday2015_card_Question[711287]["Option1"][13] = "A."
	tLaborday2015_card_Question[711287]["Option2"][13] = "B."

	
	tLaborday2015_card_Question[711287]["Text"][14] = "What is the only thing you can break when you say its name?"
	tLaborday2015_card_Question[711287]["Option1"][14] = "Humor."
	tLaborday2015_card_Question[711287]["Option2"][14] = "Silence."

	
	tLaborday2015_card_Question[711287]["Text"][15] = "What changes a pear into a pearl?"
	tLaborday2015_card_Question[711287]["Option1"][15] = "Magic."
	tLaborday2015_card_Question[711287]["Option2"][15] = "The~letter~`L`."

	
	tLaborday2015_card_Question[711287]["Text"][16] = "What is in the middle of the night?"
	tLaborday2015_card_Question[711287]["Option1"][16] = "G."
	tLaborday2015_card_Question[711287]["Option2"][16] = "Moon."

	
	tLaborday2015_card_Question[711287]["Text"][17] = "Which month have 28 days?"
	tLaborday2015_card_Question[711287]["Option1"][17] = "Feb~only."
	tLaborday2015_card_Question[711287]["Option2"][17] = "All~of~them."

	
	tLaborday2015_card_Question[711287]["Text"][18] = "What season is the most dangerous one?"
	tLaborday2015_card_Question[711287]["Option1"][18] = "Autumn."
	tLaborday2015_card_Question[711287]["Option2"][18] = "Winter."

	
	tLaborday2015_card_Question[711287]["Text"][19] = "What gets larger, the more you take away?"
	tLaborday2015_card_Question[711287]["Option1"][19] = "Water."
	tLaborday2015_card_Question[711287]["Option2"][19] = "Hole."

	
	tLaborday2015_card_Question[711287]["Text"][20] = "What month do soldiers hate?"
	tLaborday2015_card_Question[711287]["Option1"][20] = "March."
	tLaborday2015_card_Question[711287]["Option2"][20] = "May."

	
	
---紫卡题目
	tLaborday2015_card_Question[711288]["Text"] = {}
	tLaborday2015_card_Question[711288]["Option1"] = {}
	tLaborday2015_card_Question[711288]["Option2"] = {}
	--tLaborday2015_card_Question[711288]["Option1Func"] = {}
	--tLaborday2015_card_Question[711288]["Option2Func"] = {}
	tLaborday2015_card_Question[711288]["Text"][1] = "What stays hot, even if you put it in the fridge?"
	tLaborday2015_card_Question[711288]["Option1"][1] = "Pepper."
	tLaborday2015_card_Question[711288]["Option2"][1] = "Hot~dog."

	
	tLaborday2015_card_Question[711288]["Text"][2] = "What fruit is can never be called lonely?"
	tLaborday2015_card_Question[711288]["Option1"][2] = "Lemon."
	tLaborday2015_card_Question[711288]["Option2"][2] = "Pear."

	
	tLaborday2015_card_Question[711288]["Text"][3] = "Who works only one day in a year but never gets fired?"
	tLaborday2015_card_Question[711288]["Option1"][3] = "Policeman."
	tLaborday2015_card_Question[711288]["Option2"][3] = "Santa~Claus."

	
	tLaborday2015_card_Question[711288]["Text"][4] = "What letter is an animal?"
	tLaborday2015_card_Question[711288]["Option1"][4] = "B."
	tLaborday2015_card_Question[711288]["Option2"][4] = "C."

	
	tLaborday2015_card_Question[711288]["Text"][5] = "What letter is a vegetable?"
	tLaborday2015_card_Question[711288]["Option1"][5] = "B."
	tLaborday2015_card_Question[711288]["Option2"][5] = "P."

	
	tLaborday2015_card_Question[711288]["Text"][6] = "What letter is a question?"
	tLaborday2015_card_Question[711288]["Option1"][6] = "W."
	tLaborday2015_card_Question[711288]["Option2"][6] = "Y."

	
	tLaborday2015_card_Question[711288]["Text"][7] = "If you drop a white hat into the Red Sea, what does it become?"
	tLaborday2015_card_Question[711288]["Option1"][7] = "Wet."
	tLaborday2015_card_Question[711288]["Option2"][7] = "Red."

	
	tLaborday2015_card_Question[711288]["Text"][8] = "What does elephant`s left ear look like?"
	tLaborday2015_card_Question[711288]["Option1"][8] = "Fan."
	tLaborday2015_card_Question[711288]["Option2"][8] = "Right~ear."

	
	tLaborday2015_card_Question[711288]["Text"][9] = "From what number can you take half, and leave nothing?"
	tLaborday2015_card_Question[711288]["Option1"][9] = "8."
	tLaborday2015_card_Question[711288]["Option2"][9] = "9."

	
	tLaborday2015_card_Question[711288]["Text"][10] = "3 cats catch 3 mice in 3 days. How many mice will 9 cats catch in 9 days?"
	tLaborday2015_card_Question[711288]["Option1"][10] = "27."
	tLaborday2015_card_Question[711288]["Option2"][10] = "36."

	
	tLaborday2015_card_Question[711288]["Text"][11] = "What 5-letter word has 6 left, when you take 2 letters away?"
	tLaborday2015_card_Question[711288]["Option1"][11] = "Magic."
	tLaborday2015_card_Question[711288]["Option2"][11] = "Sixth."

	
	tLaborday2015_card_Question[711288]["Text"][12] = "What number gets bigger when you turn it upside down?"
	tLaborday2015_card_Question[711288]["Option1"][12] = "6."
	tLaborday2015_card_Question[711288]["Option2"][12] = "9."


	tLaborday2015_card_Question[711288]["Text"][13] = "What weather do mice and rats fear?"
	tLaborday2015_card_Question[711288]["Option1"][13] = "Rainy."
	tLaborday2015_card_Question[711288]["Option2"][13] = "Sunny."
	
	
	tLaborday2015_card_Question[711288]["Text"][14] = "What question can never be answered by `yes`?"
	tLaborday2015_card_Question[711288]["Option1"][14] = "Are~you~mad?"
	tLaborday2015_card_Question[711288]["Option2"][14] = "Are~you~asleep?"
	
	
	tLaborday2015_card_Question[711288]["Text"][15] = "What is it that found in the every center of America and Australia?"
	tLaborday2015_card_Question[711288]["Option1"][15] = "R."
	tLaborday2015_card_Question[711288]["Option2"][15] = "I."

	
	tLaborday2015_card_Question[711288]["Text"][16] = "Why is a river rich?"
	tLaborday2015_card_Question[711288]["Option1"][16] = "It~has~banks."
	tLaborday2015_card_Question[711288]["Option2"][16] = "It~has~water."

	
	tLaborday2015_card_Question[711288]["Text"][17] = "Which letter is very useful to a deaf woman?"
	tLaborday2015_card_Question[711288]["Option1"][17] = "E."
	tLaborday2015_card_Question[711288]["Option2"][17] = "I."

	
	tLaborday2015_card_Question[711288]["Text"][18] = "What`s too much for two and just right for one?"
	tLaborday2015_card_Question[711288]["Option1"][18] = "Secret."
	tLaborday2015_card_Question[711288]["Option2"][18] = "Room."
	
	
	tLaborday2015_card_Question[711288]["Text"][19] = "A car is to gas as coffee maker is to:"
	tLaborday2015_card_Question[711288]["Option1"][19] = "Water."
	tLaborday2015_card_Question[711288]["Option2"][19] = "Coffee."

	
	tLaborday2015_card_Question[711288]["Text"][20] = "What comes after the letter `A`?"
	tLaborday2015_card_Question[711288]["Option1"][20] = "All~the~other~letters."
	tLaborday2015_card_Question[711288]["Option2"][20] = "B~only."
	
	--正确选项配置
	---蓝卡
	tLaborday2015_card_Question[711286][1]=1
	tLaborday2015_card_Question[711286][2]=2
	tLaborday2015_card_Question[711286][3]=1
	tLaborday2015_card_Question[711286][4]=2
	tLaborday2015_card_Question[711286][5]=1
	tLaborday2015_card_Question[711286][6]=2
	tLaborday2015_card_Question[711286][7]=1
	tLaborday2015_card_Question[711286][8]=2
	tLaborday2015_card_Question[711286][9]=1
	tLaborday2015_card_Question[711286][10]=1
	tLaborday2015_card_Question[711286][11]=2
	tLaborday2015_card_Question[711286][12]=1
	tLaborday2015_card_Question[711286][13]=2
	tLaborday2015_card_Question[711286][14]=1
	tLaborday2015_card_Question[711286][15]=2
	tLaborday2015_card_Question[711286][16]=1
	tLaborday2015_card_Question[711286][17]=1
	tLaborday2015_card_Question[711286][18]=2
	tLaborday2015_card_Question[711286][19]=2
	tLaborday2015_card_Question[711286][20]=1
---红卡
	tLaborday2015_card_Question[711287][1]=1
	tLaborday2015_card_Question[711287][2]=2
	tLaborday2015_card_Question[711287][3]=1
	tLaborday2015_card_Question[711287][4]=2
	tLaborday2015_card_Question[711287][5]=2
	tLaborday2015_card_Question[711287][6]=1
	tLaborday2015_card_Question[711287][7]=1
	tLaborday2015_card_Question[711287][8]=2
	tLaborday2015_card_Question[711287][9]=1
	tLaborday2015_card_Question[711287][10]=1
	tLaborday2015_card_Question[711287][11]=2
	tLaborday2015_card_Question[711287][12]=1
	tLaborday2015_card_Question[711287][13]=2
	tLaborday2015_card_Question[711287][14]=2
	tLaborday2015_card_Question[711287][15]=2
	tLaborday2015_card_Question[711287][16]=1
	tLaborday2015_card_Question[711287][17]=2
	tLaborday2015_card_Question[711287][18]=1
	tLaborday2015_card_Question[711287][19]=2
	tLaborday2015_card_Question[711287][20]=1
---紫卡
	tLaborday2015_card_Question[711288][1]=1
	tLaborday2015_card_Question[711288][2]=2
	tLaborday2015_card_Question[711288][3]=2
	tLaborday2015_card_Question[711288][4]=1
	tLaborday2015_card_Question[711288][5]=2
	tLaborday2015_card_Question[711288][6]=2
	tLaborday2015_card_Question[711288][7]=1
	tLaborday2015_card_Question[711288][8]=2
	tLaborday2015_card_Question[711288][9]=1
	tLaborday2015_card_Question[711288][10]=1
	tLaborday2015_card_Question[711288][11]=2
	tLaborday2015_card_Question[711288][12]=1
	tLaborday2015_card_Question[711288][13]=1
	tLaborday2015_card_Question[711288][14]=2
	tLaborday2015_card_Question[711288][15]=1
	tLaborday2015_card_Question[711288][16]=1
	tLaborday2015_card_Question[711288][17]=2
	tLaborday2015_card_Question[711288][18]=1
	tLaborday2015_card_Question[711288][19]=2
	tLaborday2015_card_Question[711288][20]=1

-----------------------------------------------------------------------------------------------------
--Name:			[英文征服][活动脚本]劳工节活动之劳工大找茬(9.7-9.13)
--Creator: 		许乐
--Created:		2015/05/06
------------------------------------------------------------------------------------------------------
-- 命名前缀
-- LabourDay2015_FindMistake_

--8047 青龙校尉·萧勇对白 ParadeOrganizer
tLabourDay2015_FindMistake_Text = {}
	tLabourDay2015_FindMistake_Text[8047] = {}
	--活动前对白
	tLabourDay2015_FindMistake_Text[8047]["Text111"] = "	Hello, kid. I`m happy that I am the one in charge of training 100 workers, "
	tLabourDay2015_FindMistake_Text[8047]["Text112"] = "for the parade~If you are interested, you can pay a visit to our Playground later."
	tLabourDay2015_FindMistake_Text[8047]["Text113"] = " You are always welcome."
	
	--活动后对白
	tLabourDay2015_FindMistake_Text[8047]["Text121"] = "	How have you been these days? I hope you enjoyed yourself during the holiday!"
	
	--活动中对白
	tLabourDay2015_FindMistake_Text[8047]["Text131"] = "	The workers have been training on the Playground everyday, "
	tLabourDay2015_FindMistake_Text[8047]["Text132"] = "to get ready for the parade.~Most of them are practicing hard and seriously, "
	tLabourDay2015_FindMistake_Text[8047]["Text133"] = "while the others are lazy or acting very strange..~If you can sort them out, "
	tLabourDay2015_FindMistake_Text[8047]["Text134"] = "you can report to me and claim a reward."

	--接Option3：我要领取奖励。
	--每日只可领取一次
	tLabourDay2015_FindMistake_Text[8047]["Text311"] = "	Everybody can claim 1 gifts at most, each day. Please come back, tomorrow."	
	--未完成任务，不可领取（不满20个）
	tLabourDay2015_FindMistake_Text[8047]["Text321"] = "	I`m sorry, but seems you didn`t find all 20 poor workers in 5 minutes."	
	tLabourDay2015_FindMistake_Text[8047]["Text322"] = "~I`m afraid I can`t give you anything."

	--接Option4：请说得详细一些。
	tLabourDay2015_FindMistake_Text[8047]["Text411"] = "	20 of the workers are lazy or acting very strange.. If you can pick out all the "
	tLabourDay2015_FindMistake_Text[8047]["Text412"] = "20 workers in 5 minutes,~you can talk to me for a reward. But if you make a mistake, "
	tLabourDay2015_FindMistake_Text[8047]["Text413"] = "you will be punished!~Please remember, you only have 5 minutes, and then you will "
	tLabourDay2015_FindMistake_Text[8047]["Text414"] = "be sent back to Twin City."

	--接Option5：送我进入西校场。
	--当天次数大于1次
	tLabourDay2015_FindMistake_Text[8047]["Text511"] = "	I`m sorry, but I don`t want them to be bothered. You have 1 chances to enter "
	tLabourDay2015_FindMistake_Text[8047]["Text512"] = "the Playground everyday,~and you`ve used up your chances, today."
	--等级不足
	tLabourDay2015_FindMistake_Text[8047]["Text521"] = "	I`m sorry, but you haven`t reached Level 80, yet. Please come back and see me "
	tLabourDay2015_FindMistake_Text[8047]["Text522"] = "when you`re stronger."
	
	--选项
	tLabourDay2015_FindMistake_Text[8047]["Option1"] = "I will."
	tLabourDay2015_FindMistake_Text[8047]["Option2"] = "Bye."
	tLabourDay2015_FindMistake_Text[8047]["Option3"] = "Claim~reward."
	tLabourDay2015_FindMistake_Text[8047]["Option4"] = "Tell~me~more."
	tLabourDay2015_FindMistake_Text[8047]["Option5"] = "Send~me~there."
	tLabourDay2015_FindMistake_Text[8047]["Option6"] = "Later."
	tLabourDay2015_FindMistake_Text[8047]["Option7"] = "Alright."
	tLabourDay2015_FindMistake_Text[8047]["Option8"] = "I~see."
	tLabourDay2015_FindMistake_Text[8047]["Option9"] = "That`s~bad."
	
	
	--8048  白虎校尉·奉武对白  Trainer
	tLabourDay2015_FindMistake_Text[8048] = {}
	--活动中对白
	tLabourDay2015_FindMistake_Text[8048]["Text111"] = "	Hi, my friend. How have you been these days? I`m responsible for the training of "
	tLabourDay2015_FindMistake_Text[8048]["Text112"] = "~these new workers.You know, we will have a big parade! Most of them are "
	tLabourDay2015_FindMistake_Text[8048]["Text113"] = "~practicing hard and seriously,while the others are lazy or just acting very strange. "
	tLabourDay2015_FindMistake_Text[8048]["Text114"] = "Can you do me a favor and look them over?"
	
	--接Option1:我乐意帮你。
	tLabourDay2015_FindMistake_Text[8048]["Text211"] = "	Thanks. 20 of the workers are lazy or acting very strange.. If you can pick out "
	tLabourDay2015_FindMistake_Text[8048]["Text212"] = "~all the 20 workers in 5 minutes,you can talk to the Parade Organizer for a reward. "
	tLabourDay2015_FindMistake_Text[8048]["Text213"] = "~But if you make a mistake, you will be punished!Please remember, you only have 5 minutes,"
	tLabourDay2015_FindMistake_Text[8048]["Text214"] = " and then you will be sent back to Twin City."
	--等级不足
	tLabourDay2015_FindMistake_Text[8048]["Text221"] = "	I`m sorry, but you haven`t reached Level 80, yet. Please come back "
	tLabourDay2015_FindMistake_Text[8048]["Text222"] = "and see me when you`re stronger."
	--已接到任务
	tLabourDay2015_FindMistake_Text[8048]["Text231"] = "	You have got only 5 minutes. Hurry up!"
	--已完成任务 找到20个
	tLabourDay2015_FindMistake_Text[8048]["Text241"] = "	Well done. You picked out all the workers that are acting strangely. "
	tLabourDay2015_FindMistake_Text[8048]["Text242"] = "Find the Parade Organizer, to claim your reward."
	
	--选项
	tLabourDay2015_FindMistake_Text[8048]["Option1"] = "Glad~to~help."
	tLabourDay2015_FindMistake_Text[8048]["Option2"] = "Send~me~out~there."
	tLabourDay2015_FindMistake_Text[8048]["Option3"] = "Got~it."
	tLabourDay2015_FindMistake_Text[8048]["Option4"] = "I~see."
	tLabourDay2015_FindMistake_Text[8048]["Option5"] = "I~will."
	tLabourDay2015_FindMistake_Text[8048]["Option6"] = "Alright."
	
	--1010提示
	tLabourDay2015_FindMistake_Text["TalkChannel2005"] = {}
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["RightLabour"] = "Good job! You picked out a worker who`s acting strangely."
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["Successful"] = "Well done. You picked out all the workers that are acting strangely. Find the Parade Organizer, to claim your reward."
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["FindNext"] = "Hurry up and find the other strange ones!"
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["NoTask"] = "The workers are training. You should leave them alone, and not bother them."
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["TimeNotOut"] = "You made a mistake! Please wait 10 seconds."	
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["WrongTimes"] = "You made a mistake! Please wait 10 seconds."
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["WrongMaps"] = "You made a mistake! The angry worker kicked you out of the way!"
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["WrongBlood"] = "You made a mistake! You were beaten by the angry worker and lost some HP!"
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["GetReward"] = "You received a Festival Joy Pack. Check your inventory!"
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["NoBagSpace"] = "Your inventory is full."
	tLabourDay2015_FindMistake_Text["TalkChannel2005"]["TimeOut"] = "Five minutes has arrived, you were sent to Twin City."
	
	tLabourDay2015_FindMistake_Text["TalkChannel2007"]={}
	tLabourDay2015_FindMistake_Text["TalkChannel2007"]["GoBack"] = "You were sent to Twin City."
	tLabourDay2015_FindMistake_Text["TalkChannel2007"]["GoInto"] = "You entered the Playground."


----------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------
--Name:			150716[英文征服][活动脚本]天下第一活动制作
--Creator: 		许乐
--Created:		2015/07/16
------------------------------------------------------------------------------------------------------
-- 命名前缀
--Activity2015TheBestHero_

-- 18714  擂台管理员
tActivity2015TheBestHero_Text = {}
	tActivity2015TheBestHero_Text[18714] = {}
	--活动前对白
	tActivity2015TheBestHero_Text[18714]["Text111"] = "The Ultimate Fighter contest will start at 19:30, September 12th. If you`re Level 120+ or reborn, come and fight the way up to the peak!"
	tActivity2015TheBestHero_Text[18714]["Option1"] = "It`s~getting~exciting!"
	
	--活动后对白
	tActivity2015TheBestHero_Text[18714]["Text121"] = "I saw so many talents in the contest. The world belongs to the young."
	tActivity2015TheBestHero_Text[18714]["Option2"] = "Indeed."

	--活动中对白
	--玩家等级不满足条件
	tActivity2015TheBestHero_Text[18714]["Text131"] = "To compete for the Ultimate Fighter, you should reach at least Level 120 or get reborn."
	tActivity2015TheBestHero_Text[18714]["Option3"] = "I`ll~come~back~soon."

	--等级满足条件
	tActivity2015TheBestHero_Text[18714]["Text141"] = "Fame means a lot to fighters. To make the best of the best, we welcome heroes to fight on the Ultimate Stadium at 19:30 - 19:40, everyday from September 12th to 25th."
	tActivity2015TheBestHero_Text[18714]["Text142"] = "~The winner will crown the title of Ultimate Fighter, and win wonderful rewards like Flame Dragon garment, Permanent Stone and DB Scroll."
	tActivity2015TheBestHero_Text[18714]["Option4"] = "I`m~in!"  			--此项请在活动时间内显示
	tActivity2015TheBestHero_Text[18714]["Option5"] = "Claim~my~reward."		--此项请在当天擂台结束，且0点前显示
	tActivity2015TheBestHero_Text[18714]["Option6"] = "Claim~my~prize~for~ranking."		--此项请在当天擂台开始，且0点前显示
	tActivity2015TheBestHero_Text[18714]["Option7"] = "Tell~me~more."
	tActivity2015TheBestHero_Text[18714]["Option8"] = "What`re~the~rewards?"
	tActivity2015TheBestHero_Text[18714]["Option9"] = "It`s~too~violent."
	
	--接Option4：送我入场。
	--已闯关成功，不需入场
	tActivity2015TheBestHero_Text[18714]["Text411"] = "You`ve already conquered the Ultimate Stadium. Don`t forget to claim your reward from me."	
	--入场成功
	tActivity2015TheBestHero_Text["SendIn"] = "You`ve been on stage. It`s your show time!"

	--接Option5：领取活动奖励。 --此项请在当天擂台结束，且0点前显示
	--活动结束后，玩家在第1——10层，未通关
	tActivity2015TheBestHero_Text[18714]["Text511"] = "Great job! You`ve reached Floor %s. Please select your reward:"	
	tActivity2015TheBestHero_Text[18714]["Option10"] = "%s~minutes~of~EXP."
	tActivity2015TheBestHero_Text[18714]["Option11"] = "%s~Chi~Points."
	tActivity2015TheBestHero_Text[18714]["Option12"] = "%s!"               --8-10层有此选项
	tActivity2015TheBestHero_Text[18714]["Option13"] = "I`ll~think~about~it."
	
	tActivity2015TheBestHero_Text["OptionStr"] = {}
	tActivity2015TheBestHero_Text["OptionStr"][8] = "5~Special~Training~Pills~(B)."
	tActivity2015TheBestHero_Text["OptionStr"][9] = "3~Favored~Training~Pills~(B)."
	tActivity2015TheBestHero_Text["OptionStr"][10] = "2~Protection~Pills."

	--接10：X分钟经验。
	--经验满
	tActivity2015TheBestHero_Text[18714]["Text1011"] = "You`re at the max level, and the EXP is useless. Why not consider other rewards?"	
	tActivity2015TheBestHero_Text[18714]["Option14"] = "Okay."  ----（返回上一页）
	--接12：获得XX物品
	--背包满
	tActivity2015TheBestHero_Text[18714]["Text1211"] = "Buddy, you`re carrying a full inventory. Why not make some room, first?"	
	tActivity2015TheBestHero_Text[18714]["Option15"] = "I`ll~do~it~now."
	--接10,11,12  成功领取
	tActivity2015TheBestHero_Text["GetFloorReward"] = {}
	tActivity2015TheBestHero_Text["GetFloorReward"][1] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][2] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][3] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][4] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][5] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][6] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][7] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][8] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][9] = "You received %s!"
	tActivity2015TheBestHero_Text["GetFloorReward"][10] = "You received %s!"

	--接Option5：领取活动奖励。玩家通关
	tActivity2015TheBestHero_Text[18714]["Text521"] = "Excellent! You`ve broken through all the floors. Please choose your reward:"	
	tActivity2015TheBestHero_Text[18714]["Option16"] = "360~minutes~of~EXP."
	tActivity2015TheBestHero_Text[18714]["Option17"] = "120~Chi~Points."
	tActivity2015TheBestHero_Text[18714]["Option18"] = "1~Senior~Training~Pill~(B)."
	tActivity2015TheBestHero_Text[18714]["Option19"] = "I~need~to~think~about~it."
	--接16：360分钟经验。
	--经验满
	tActivity2015TheBestHero_Text[18714]["Text1611"] = "You`re at the max level, and the EXP is useless. Why not consider other rewards?"	
	tActivity2015TheBestHero_Text[18714]["Option20"] = "Okay."  ----（返回上一页）
	--接18：1 Senior Training Pill (B)。
	--背包满
	tActivity2015TheBestHero_Text[18714]["Text1811"] = "Buddy, you`re carrying a full inventory. Why not make some room, first?"	
	tActivity2015TheBestHero_Text[18714]["Option21"] = "I`ll~do~it~now."
	--接16,17,18  成功领取
	tActivity2015TheBestHero_Text["GetPassReward"] = "You received %s!"

	--接Option5：领取活动奖励  已领取过活动奖励
	tActivity2015TheBestHero_Text[18714]["Text531"] = "You`ve already claimed the reward, haven`t you?"
	--接Option5：领取活动奖励  未参与过活动
	tActivity2015TheBestHero_Text[18714]["Text541"] = "Sorry, I don`t see your name on the list. You need to fight in the Ultimate Stadium, first."

	--接Option6：领取排行奖励。  --此项请在当天擂台结束，且0点前显示
	tActivity2015TheBestHero_Text[18714]["Text611"] = "Rank           Character\n"
	tActivity2015TheBestHero_Text[18714]["Text612"] = "No.1         	%s\n"	
	tActivity2015TheBestHero_Text[18714]["Text613"] = "No.2        	 %s\n"	
	tActivity2015TheBestHero_Text[18714]["Text614"] = "No.3         	%s\n"	
	tActivity2015TheBestHero_Text[18714]["Text615"] = "No.4         	%s\n"	
	tActivity2015TheBestHero_Text[18714]["Text616"] = "No.5         	%s\n"	
	tActivity2015TheBestHero_Text[18714]["Text617"] = "No.6        	 %s\n"	
	tActivity2015TheBestHero_Text[18714]["Text618"] = "No.7         	%s\n"	
	tActivity2015TheBestHero_Text[18714]["Text619"] = "No.8         	%s\n"	
	tActivity2015TheBestHero_Text[18714]["Text620"] = "No.9         	%s\n"	
	tActivity2015TheBestHero_Text[18714]["Text621"] = "No.10       	%s\n"
	tActivity2015TheBestHero_Text[18714]["Option22"] = "Claim~my~prize~for~ranking."
	tActivity2015TheBestHero_Text[18714]["Option23"] = "I~have~something~else~to~ask."
	--tActivity2015TheBestHero_Text[18714]["Option24"] = "I`ll~talk~to~you~later."
	tActivity2015TheBestHero_Text[18714]["Null"] = "None"

	--接Option6：领取排行奖励。   未参加活动
	tActivity2015TheBestHero_Text[18714]["Text631"] = "It seems you haven`t fought in the stadium of Ultimate Fighter. Show me your power, first."

	--接22：领取排行奖励。
	--未上榜，无法领取
	tActivity2015TheBestHero_Text[18714]["Text2211"] = "Sorry, you didn`t enter the ranking of Ultimate Fighter. Keep trying your best!"	
	--已领取，无法领取
	tActivity2015TheBestHero_Text[18714]["Text2221"] = "You`ve already claimed today`s prize."	
	--背包满，无法领取
	tActivity2015TheBestHero_Text[18714]["Text2231"] = "Your inventory is full. Please make some room, first."	
	--成功领取
	tActivity2015TheBestHero_Text["GetRankReward"] = {}
	tActivity2015TheBestHero_Text["GetRankReward"][1] = "You received 600 Chi Points, 10 Favored Training Pills (B), 1 Senior Training Pill, 1 Dragon Ball and 5 Flame Dragon Fragments!"	
	tActivity2015TheBestHero_Text["GetRankReward"][2] = "You received 500 Chi Points, 7 Favored Training Pills (B), 1 Senior Training Pill and 3 Flame Dragon Fragments!"	
	tActivity2015TheBestHero_Text["GetRankReward"][3] = "You received 400 Chi Points, 5 Favored Training Pills (B), 1 Protection Pill and 2 Flame Dragon Fragments!"	
	tActivity2015TheBestHero_Text["GetRankReward"][4] = "You received 300 Chi Points, 3 Favored Training Pills (B), 1 Special Training Pill and 1 Flame Dragon Fragment!"
	tActivity2015TheBestHero_Text["GetRankReward"][5] = "You received 300 Chi Points, 3 Favored Training Pills (B), 1 Special Training Pill and 1 Flame Dragon Fragment!"
	tActivity2015TheBestHero_Text["GetRankReward"][6] = "You received 300 Chi Points, 3 Favored Training Pills (B), 1 Special Training Pill and 1 Flame Dragon Fragment!"
	tActivity2015TheBestHero_Text["GetRankReward"][7] = "You received 200 Chi Points, 1 Favored Training Pill (B) and 1 Flame Dragon Fragment!"
	tActivity2015TheBestHero_Text["GetRankReward"][8] = "You received 200 Chi Points, 1 Favored Training Pill (B) and 1 Flame Dragon Fragment!"
	tActivity2015TheBestHero_Text["GetRankReward"][9] = "You received 200 Chi Points, 1 Favored Training Pill (B) and 1 Flame Dragon Fragment!"
	tActivity2015TheBestHero_Text["GetRankReward"][10] = "You received 200 Chi Points, 1 Favored Training Pill (B) and 1 Flame Dragon Fragment!"

	--接Option7：了解活动详情
	tActivity2015TheBestHero_Text[18714]["Text711"] = "There are 10 floors you need to break through within 10 minutes and the 1st floor comes first. When you collect a certain amount of badges,"	
	tActivity2015TheBestHero_Text[18714]["Text712"] = "~you can advance into the next floor. In the Ultimate Stadium, defeating a fighter will certainly bring you a badge. You have a chance to collect badges"	
	tActivity2015TheBestHero_Text[18714]["Text713"] = "~by killing evil enemies on each floor. If you defeat the Dominator on Floor 3/6/9, you can directly enter the next floor. From the 4th floor, you may drop badges if you get attacked."	
	tActivity2015TheBestHero_Text[18714]["Option25"] = "Any~other~tips~for~me?"
	tActivity2015TheBestHero_Text[18714]["Option26"] = "I~have~something~else~to~ask."
	--接25：了解注意事项。
	tActivity2015TheBestHero_Text[18714]["Text2511"] = "You may probably find a merchant who sells Universal Badges and Power Amulets on each floor. On the first 9 floors, you can"	
	tActivity2015TheBestHero_Text[18714]["Text2512"] = "~use the Universal Badge to enter the next floor. The Power Amulet will enhance your attributes for 10 minutes. You can use as many amulets as you like."	
	tActivity2015TheBestHero_Text[18714]["Option28"] = "I~have~something~else~to~ask."   --返回到主对白
	tActivity2015TheBestHero_Text[18714]["Option29"] = "I~see."
	
	--接Option8：了解奖励详情
	tActivity2015TheBestHero_Text[18714]["Text811"] = "1. A breakthrough reward every time when you successfully advance into the next floor.\n"	
	tActivity2015TheBestHero_Text[18714]["Text812"] = "2. A speed reward when you spend less than 9 minutes in breaking through a floor.\n"	
	tActivity2015TheBestHero_Text[18714]["Text813"] = "3. An achievement reward according the highest floor you finally reached.\n"
	tActivity2015TheBestHero_Text[18714]["Text814"] = "4. A rank prize for the Top 10 heroes.\nWhich reward would you like to learn more about?"
	tActivity2015TheBestHero_Text[18714]["Option30"] = "Achievement~reward."
	tActivity2015TheBestHero_Text[18714]["Option31"] = "Speed~reward."
	tActivity2015TheBestHero_Text[18714]["Option32"] = "Breakthrough~reward."
	tActivity2015TheBestHero_Text[18714]["Option33"] = "The~reward~for~killing~BOSS."
	tActivity2015TheBestHero_Text[18714]["Option34"] = "Rank~prize."
	tActivity2015TheBestHero_Text[18714]["Option35"] = "I~have~something~else~to~ask."
	tActivity2015TheBestHero_Text[18714]["Option36"] = "I~see."
	--接30：活动结束奖励。--Previous~page.离开。
	tActivity2015TheBestHero_Text[18714]["Text3011"] = "Floor 1: 60 minutes of EXP or 20 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3012"] = "Floor 2: 90 minutes of EXP or 30 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3013"] = "Floor 3: 120 minutes of EXP or 40 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3014"] = "Floor 4: 150 minutes of EXP or 50 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3015"] = "Floor 5: 180 minutes of EXP or 60 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3016"] = "Floor 6: 210 minutes of EXP or 70 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3017"] = "Floor 7: 240 minutes of EXP or 80 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3018"] = "Floor 8: 270 minutes of EXP, 90 Chi Points, or 5 Special Training Pills (B);\n"	
	tActivity2015TheBestHero_Text[18714]["Text3019"] = "Floor 9: 300 minutes of EXP, 100 Chi Points, or 3 Favored Training Pills (B);\n"
	tActivity2015TheBestHero_Text[18714]["Text3020"] = "Floor 10: 330 minutes of EXP, 110 Chi Points, or 2 Protection Pills;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3021"] = "All Floors Cleared: 360 minutes of EXP, 120 Chi Points, or 1 Senior Training Pill (B).\n"	
	tActivity2015TheBestHero_Text[18714]["Option39"] = "I~have~something~else~to~ask."

	--接31：9分钟内通关奖励。--Previous~page.离开。
	tActivity2015TheBestHero_Text[18714]["Text3111"] = "Less than 5 minutes:	   300 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3112"] = "5 - 6 minutes:		         	250 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3113"] = "6 - 7 minutes:		         	200 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3114"] = "7 - 8 minutes:		         	150 Chi Points;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3115"] = "8 - 9 minutes:		         	100 Chi Points.\n"
	--接32：升层奖励。--Previous~page.离开。
	tActivity2015TheBestHero_Text[18714]["Text3211"] = "You`ll be rewarded with Martial Pack Scraps every time when you break through a floor.\n"	
	tActivity2015TheBestHero_Text[18714]["Text3212"] = "Floor 1: 1 Martial Pack Scrap;\n"
	tActivity2015TheBestHero_Text[18714]["Text3213"] = "Floor 2: 1 Martial Pack Scrap;\n"
	tActivity2015TheBestHero_Text[18714]["Text3214"] = "Floor 3: 1 Martial Pack Scrap;\n"
	tActivity2015TheBestHero_Text[18714]["Text3215"] = "Floor 4: 2 Martial Pack Scraps;\n"
	tActivity2015TheBestHero_Text[18714]["Text3216"] = "Floor 5: 1 Martial Pack Scrap;\n"
	tActivity2015TheBestHero_Text[18714]["Text3217"] = "Floor 6: 1 Martial Pack Scrap;\n"
	tActivity2015TheBestHero_Text[18714]["Text3218"] = "Floor 7: 3 Martial Pack Scraps;\n"
	tActivity2015TheBestHero_Text[18714]["Text3219"] = "Floor 8: 2 Martial Pack Scraps;\n"
	tActivity2015TheBestHero_Text[18714]["Text3220"] = "Floor 9: 3 Martial Pack Scraps;\n"
    tActivity2015TheBestHero_Text[18714]["Text3221"] = "Floor 10: 5 Martial Pack Scraps;\n"
	tActivity2015TheBestHero_Text[18714]["Text3222"] = "All Floors Cleared: 5 Martial Pack Scraps."
	tActivity2015TheBestHero_Text[18714]["Option37"] = "What`s~in~the~pack?"
	--接37：查询礼包奖励--Previous~page.离开。
	tActivity2015TheBestHero_Text[18714]["Text3711"] = "You can combine 5 Martial Pack Scraps into an Outstanding Martial Pack, and 5 Outstanding Martial Packs into an Incomparable Pack. Outstanding Martial Pack may bring you"	
	tActivity2015TheBestHero_Text[18714]["Text3712"] = "~DB (B), Chi Points, +2 Stone (B) and P6 Dragon Soul Bag, while the Incomparable Pack may give better rewards like 30-day Flame Dragon, Permanent Stone, DB Scroll, +6 Steed and +6 Stone."
	tActivity2015TheBestHero_Text[18714]["Option40"] = "I~have~something~else~to~ask."
	--接33：擂主奖励。--Previous~page.离开。
	tActivity2015TheBestHero_Text[18714]["Text3311"] = "The reward for killing BOSS on\n"	
	tActivity2015TheBestHero_Text[18714]["Text3312"] = "Floor 3				30Chi Points + 1 Free Course;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3313"] = "Floor 6				40Chi Points + 1 Free Course;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3314"] = "Floor 9				50Chi Points + 1 Free Course + Senior Training Pill (B).\n"	
	--接34：排行奖励。--Previous~page.离开。
	tActivity2015TheBestHero_Text[18714]["Text3411"] = "The prize for ranking Top 10 in the Ultimate Fighter contest:\n"	
	tActivity2015TheBestHero_Text[18714]["Text3412"] = "1st Place          600 Chi Points, 10 Favored Training Pills (B), 1 Senior Training Pill, 1 Dragon Ball and 5 Flame Dragon Fragments;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3413"] = "2nd Place         500 Chi Points, 7 Favored Training Pills (B), 1 Senior Training Pill and 3 Flame Dragon Fragments;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3414"] = "3rd Place          400 Chi Points, 5 Favored Training Pills (B), 1 Protection Pill and 2 Flame Dragon Fragments;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3415"] = "4-6th Place       300 Chi Points, 3 Favored Training Pills (B), 1 Special Training Pill and 1 Flame Dragon;\n"	
	tActivity2015TheBestHero_Text[18714]["Text3416"] = "7-10th Place     200 Chi Points, 1 Favored Training Pill (B) and 1 Flame Dragon Fragment.\n"	
	tActivity2015TheBestHero_Text[18714]["Option38"] = "Flame~Dragon~Fragment?"
	--接38：了解FlameDragon碎片。--Previous~page.离开。
	tActivity2015TheBestHero_Text[18714]["Text3811"] = "Number of Fragments Combined  	                          Garment\n"	
	tActivity2015TheBestHero_Text[18714]["Text3812"] = "				5										   7-day 1% Blessed Flame Dragon\n"	
	tActivity2015TheBestHero_Text[18714]["Text3813"] = "				10										15-day 1% Blessed Flame Dragon\n"	
	tActivity2015TheBestHero_Text[18714]["Text3814"] = "				20										30-day 1% Blessed Flame Dragon\n"	
	tActivity2015TheBestHero_Text[18714]["Text3815"] = "				60										Permanent 1% Blessed Flame Dragon\n"
	tActivity2015TheBestHero_Text[18714]["Option41"] = "I~have~something~else~to~ask."
	
-- 18715  行脚商人(第一层)
	tActivity2015TheBestHero_Text[18715] = {}
	--活动中对白
	tActivity2015TheBestHero_Text[18715]["Text111"] = "AWESOME stuff to sell! Universal Badge teleports you to the next floor, available for the first 9 floors. Power Amulet enhances your attributes for 10 minutes, and you can use as many as you like."
	tActivity2015TheBestHero_Text[18715]["Option1"] = "Buy~a~Universal~Badge~(40~CPs)"
	tActivity2015TheBestHero_Text[18715]["Option2"] = "Buy~a~Power.~(20~CPs)"
	tActivity2015TheBestHero_Text[18715]["Option3"] = "Please~send~me~to~Twin~City."
	tActivity2015TheBestHero_Text[18715]["Option4"] = "I`ll~find~you~if~I~need."
	--接1：购买万能金牌（40点天石）。
	--天石不足
	tActivity2015TheBestHero_Text[18715]["Text211"] = "Sorry, you don`t have enough CPs. Look, the Universal Badge is absolutely worth more than the money."
	tActivity2015TheBestHero_Text[18715]["Option5"] = "Okay."
	--背包满
	tActivity2015TheBestHero_Text[18715]["Text221"] = "Your inventory is full. Please make some room, first."
	--二次确认
	tActivity2015TheBestHero_Text[18715]["Text231"] = "The Universal Badge is for 40 CPs. Can I have you money, now?"
	tActivity2015TheBestHero_Text[18715]["Option6"] = "Here~you~are."
	tActivity2015TheBestHero_Text[18715]["Option7"] = "Wait,~I~changed~my~mind."
	--接2：购买增益符（20点天石）。
	--天石不足
	tActivity2015TheBestHero_Text[18715]["Text241"] = "Sorry, you don`t have 20 CPs to buy a Power Amulet."
	--背包满
	tActivity2015TheBestHero_Text[18715]["Text251"] = "Your inventory is full. Please make some room, first."
	--二次确认
	tActivity2015TheBestHero_Text[18715]["Text261"] = "Are you sure you want to pay 20 CPs for the Power Amulet?"
	tActivity2015TheBestHero_Text[18715]["Option8"] = "Yes."
	--接3：送我回双龙城。
	tActivity2015TheBestHero_Text[18715]["Text311"] = "If you leave now, you`ll lose all the badges. When you come back, you have to start from the 1st floor. Are you sure you want to leave for Twin City?"
	tActivity2015TheBestHero_Text[18715]["Option9"] = "Yes."
	
--125 提示	Sys_SystemBroadcast()
	tActivity2015TheBestHero_Text["TimeComing"] = "The `Ultimate Fighter` contest will start at 19:30. Talk to the Ultimate Stadium Manager (316,287) for more details."
	tActivity2015TheBestHero_Text["NumberOne"] = "Excellent! [%s] is the first hero who successfully challenged the Ultimate Stadium for the `Ultimate Fighter`."

--1010 2005提示	User_TalkChannel2005
	tActivity2015TheBestHero_Text["TimeEnding"] = "The stadium for `Ultimate Fighter` will be closed soon. Hurry up!"	
	tActivity2015TheBestHero_Text["SendOut"] = "The `Ultimate Fighter` contest has ended, and heroes have been teleported out."
	tActivity2015TheBestHero_Text["FullBag"] = "Your inventory is full. Please make some room, first."
	tActivity2015TheBestHero_Text["GetPill"] = "You received 1 Power Pill!"
	tActivity2015TheBestHero_Text["BuySuccess_1"] = "You received 1 Universal Badge!"
	tActivity2015TheBestHero_Text["BuySuccess_2"] = "You received 1 Power Amulet!"
	tActivity2015TheBestHero_Text["WrongMap"] = "You can`t use this item, here."
	
	tActivity2015TheBestHero_Text["PKOwnCards"] = {}
	tActivity2015TheBestHero_Text["PKOwnCards"][1] = "You defeated a fighter, and received 1 Fighter Badge!"
	tActivity2015TheBestHero_Text["PKOwnCards"][2] = "You defeated a fighter, and received 1 Elite Badge!"
	tActivity2015TheBestHero_Text["PKOwnCards"][3] = "You defeated a fighter, and received 1 Hero Badge!"
	tActivity2015TheBestHero_Text["PKOwnCards"][4] = "You defeated a fighter, and received 1 Conqueror Badge!"
	
	tActivity2015TheBestHero_Text["KillMonsCards"] = {}
	tActivity2015TheBestHero_Text["KillMonsCards"][1] = "You defeated %s, and received 1 Fighter Badge!"
	tActivity2015TheBestHero_Text["KillMonsCards"][2] = "You defeated %s, and received 1 Elite Badge!"
	tActivity2015TheBestHero_Text["KillMonsCards"][3] = "You defeated %s, and received 1 Hero Badge!"
	tActivity2015TheBestHero_Text["KillMonsCards"][4] = "You defeated %s, and received 1 Conqueror Badge!"
	
	tActivity2015TheBestHero_Text["Name"] = {}
	tActivity2015TheBestHero_Text["Name"][1] = "SolarFighter(F1)"
	tActivity2015TheBestHero_Text["Name"][2] = "ShadowFighter(F2)"
	tActivity2015TheBestHero_Text["Name"][3] = "NightFighter(F3)"
	tActivity2015TheBestHero_Text["Name"][4] = "FlameElite(F4)"
	tActivity2015TheBestHero_Text["Name"][5] = "WindElite(F5)"
	tActivity2015TheBestHero_Text["Name"][6] = "SkyElite(F6)"
	tActivity2015TheBestHero_Text["Name"][7] = "RuthlessHero(F7)"
	tActivity2015TheBestHero_Text["Name"][8] = "UnknownHero(F8)"
	tActivity2015TheBestHero_Text["Name"][9] = "ArrogantHero(F9)"
	tActivity2015TheBestHero_Text["Name"][10] = "CruelConqueror(F10)"

	tActivity2015TheBestHero_Text["KillBox"] = {}
	tActivity2015TheBestHero_Text["KillBox"][1] = "You broke open the treasure chest, and received 1 Fighter Badge!"
	tActivity2015TheBestHero_Text["KillBox"][2] = "You broke open the treasure chest, and received 1 Elite Badge!"
	tActivity2015TheBestHero_Text["KillBox"][3] = "You broke open the treasure chest, and received 1 Hero Badge!"
	tActivity2015TheBestHero_Text["KillBox"][4] = "You broke open the treasure chest, and received 1 Conqueror Badge!"

	tActivity2015TheBestHero_Text["KillBossUpFloor"] = {}
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7851] = {}
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7851][1] = "You defeated the Dominator on Floor 3, and received 5 Fighter Badges and 30 Chi Points!"					-- 无自创武功
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7851][2] = "You defeated the Dominator on Floor 3, and received 5 Fighter Badges, 30 Chi Points and 1 Free Course!"
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7852] = {}
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7852][1] = "You defeated the Dominator on Floor 6, and received 4 Elite Badges and 40 Chi Points!"
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7852][2] = "You defeated the Dominator on Floor 6, and received 4 Elite Badges, 40 Chi Points and 1 Free Course!"
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7853] = {}
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7853][1] = "You defeated the Dominator on Floor 9, and received 3 Hero Badges, 50 Chi Points and 1 Senior Training Pill (B)!"
	tActivity2015TheBestHero_Text["KillBossUpFloor"][7853][2] = "You defeated the Dominator on Floor 9, and received 3 Hero Badges, 50 Chi Points, 1 Free Course and 1 Senior Training Pill (B)!"

	tActivity2015TheBestHero_Text["UpFloorReward"] = "You`ve reached Floor %s, and received %s Martial Pack Scrap(s)!"
	tActivity2015TheBestHero_Text["UpFloorWithOutReward"] = "You`ve reached Floor %s!"
	tActivity2015TheBestHero_Text["UpToTopFloor"] = "You`ve successfully broken through, and received %s Martial Pack Scrap(s)!"
	tActivity2015TheBestHero_Text["SuccessReward"] = "Congratulations! You successfully broke through the floor within %s minutes, and received %s Chi Points."
	
	-- 当玩家来到这层时给玩家一个系统提示“击败本层地图中央的BOSS可获得奖励！”
	tActivity2015TheBestHero_Text["InBossFloor"] = "Go kill the Dominator at the center of this floor to win rewards!"
	-- 刷新时”本层地图中央的BOSS已重新出现，击败他就可获得奖励！“
	tActivity2015TheBestHero_Text["BossShowUp"] = "The Dominator of this follow has appeared. Hurry and defeat him to win rewards!"
	tActivity2015TheBestHero_Text["BossName"] = {}
	tActivity2015TheBestHero_Text["BossName"][3] = "Floor3Dominator"
	tActivity2015TheBestHero_Text["BossName"][6] = "Floor6Dominator"
	tActivity2015TheBestHero_Text["BossName"][9] = "Floor9Dominator"
	
--背包信相关
tBackpackLetter_Text[3007030] = {}
tBackpackLetter_Text[3007030]["NoSpace"] = "There is a letter from the Ultimate Stadium Manager. Please make some room in your inventory, and login again to receive it."
tBackpackLetter_Text[3007030]["RewardItem"] = "You received a letter from the Ultimate Stadium Manager. Hurry and check it in your inventory."

tActivity2015TheBestHero_Text[3007030] = {}
	tActivity2015TheBestHero_Text[3007030]["Text111"] = "The Ultimate Stadium is a glory land where fighters can make a great fame. We`re looking forward to rising martial stars to fight on stage"
	tActivity2015TheBestHero_Text[3007030]["Text112"] = "~at 19:30 - 19:40, everyday from September 12th to 25th. For more details, please pay a visit to the Ultimate Stadium Manager (TC 316,287)."
	tActivity2015TheBestHero_Text[3007030]["Option1"] = "Take~me~to~see~the~manager."

	tActivity2015TheBestHero_Text[3007030]["LetterFull"] = "There is a letter from the Ultimate Stadium Manager. Please make some room in your inventory, and login again to receive it."
	tActivity2015TheBestHero_Text[3007030]["GetLetter"] = "You received a letter from the Ultimate Stadium Manager. Hurry and check it in your inventory."
	tActivity2015TheBestHero_Text[3007030]["Get30Exp"] = "You received 30 minutes of EXP!"
	tActivity2015TheBestHero_Text[3007030]["Get15Cul"] = "You received 15 Study Points!"
	
--万能金牌相关
	tActivity2015TheBestHero_Text["30SecLimit"] = "You can`t use more than 1 Universal Badge within 30 seconds."
	tActivity2015TheBestHero_Text["CardCannot"] = "You can`t use the Universal Badge on Floor 10."
	tActivity2015TheBestHero_Text["EndDisappear"] = "The magic of the Universal Badge has vanished, and you threw it away."
	
--其他令牌相关
	tActivity2015TheBestHero_Text["NotEnough"] = "You should collect %s more %s(s) to advance into the next floor."
	tActivity2015TheBestHero_Text["CardTips"] = "You`ve collected %s badges, and you can challenge the next floor."
	tActivity2015TheBestHero_Text["CardsEnough"] = "You`ve collected %s badges. Right click the badge to enter the next floor."
	tActivity2015TheBestHero_Text["Disappear"] = "The magic of the badge has vanished, and you threw it away."
	
	tActivity2015TheBestHero_Text["CardsName"] = {}
	tActivity2015TheBestHero_Text["CardsName"][1] = "FighterBadge"
	tActivity2015TheBestHero_Text["CardsName"][2] = "FighterBadge"
	tActivity2015TheBestHero_Text["CardsName"][3] = "FighterBadge"
	tActivity2015TheBestHero_Text["CardsName"][4] = "EliteBadge"
	tActivity2015TheBestHero_Text["CardsName"][5] = "EliteBadge"
	tActivity2015TheBestHero_Text["CardsName"][6] = "EliteBadge"
	tActivity2015TheBestHero_Text["CardsName"][7] = "HeroBadge"
	tActivity2015TheBestHero_Text["CardsName"][8] = "HeroBadge"
	tActivity2015TheBestHero_Text["CardsName"][9] = "HeroBadge"
	tActivity2015TheBestHero_Text["CardsName"][10] = "ConquerorBadge"

--增益丹相关
	tActivity2015TheBestHero_Text["UseSuccess"] = "Your power is enhanced for 1 minute: Final P-Attack & Final M-Attack: +1000 points, Final P-Damage & Final M-Damage: -1000 points and P-Strike and M-Strike: +10%."

--增益符相关
	tActivity2015TheBestHero_Text["UseRepeat"] = "Failed to use. You`re having the effect of power enhanced."
	tActivity2015TheBestHero_Text["CannotUse"] = "You can`t use this item here."
	tActivity2015TheBestHero_Text["Using"] = "Your power is enhanced for 10 minute: Final P-Attack & Final M-Attack: +1000 points, Final P-Damage & Final M-Damage: -1000 points and P-Strike and M-Strike: +10%."
	
--武艺超群礼包碎片 相关   5个礼包碎片合成小礼包
tActivity2015TheBestHero_Text[3006977] = {}
	tActivity2015TheBestHero_Text[3006977]["CompoundItem"] = "You received an Outstanding Martial Pack!"
	tActivity2015TheBestHero_Text[3006977]["NoItem"] = "Make sure you have 5 Martial Pack Scraps into an Outstanding Martial Pack!"
	tActivity2015TheBestHero_Text[3006977]["FullBag"] = "Your inventory is full. Please make some room, first."

--外套碎片对白配置
tActivity2015TheBestHero_Text[3006980] = {}
	tActivity2015TheBestHero_Text[3006980]["111"] = "You can combine 5/10/20/60 Flame Dragon Fragments into a 7-day/15-day/30-day/permanent 1% Blessed Flame Dragon. How many fragments would you like to combine?"
	tActivity2015TheBestHero_Text[3006980]["Option1"] = "5~fragments.~(7-day~garment)"
	tActivity2015TheBestHero_Text[3006980]["Option2"] = "10~fragments.~(15-day~garment)"
	tActivity2015TheBestHero_Text[3006980]["Option3"] = "20~fragments.~(30-day~garment)"
	tActivity2015TheBestHero_Text[3006980]["Option4"] = "60~fragments.~(permanent)"

	tActivity2015TheBestHero_Text[3006980]["211"] = "Are you sure you want to combine 5 Flame Dragon Fragments into a 7-day 1% Blessed FlameDragon?"
	tActivity2015TheBestHero_Text[3006980]["Option5"] = "Yes."
	tActivity2015TheBestHero_Text[3006980]["Option6"] = "No."

	tActivity2015TheBestHero_Text[3006980]["221"] = "Are you sure you want to combine 10 Flame Dragon Fragments into a 15-day 1% Blessed FlameDragon?"
	tActivity2015TheBestHero_Text[3006980]["Option7"] = "Yes."

	tActivity2015TheBestHero_Text[3006980]["231"] = "Are you sure you want to combine 20 Flame Dragon Fragments into a 30-day 1% Blessed FlameDragon?"
	tActivity2015TheBestHero_Text[3006980]["Option8"] = "Yes."

	tActivity2015TheBestHero_Text[3006980]["241"] = "Are you sure you want to combine 60 Flame Dragon Fragments into a permanent 1% Blessed FlameDragon?"
	tActivity2015TheBestHero_Text[3006980]["Option9"] = "Yes."
	--碎片不足
	tActivity2015TheBestHero_Text[3006980]["251"] = "You don`t have enough Flame Dragon Fragments."
	tActivity2015TheBestHero_Text[3006980]["Option10"] = "Okay."
	--背包满
	tActivity2015TheBestHero_Text[3006980]["261"] = "Your inventory is full. Please make some room, first."

	tActivity2015TheBestHero_Text[3006980][1] = "You received a 7-day 1% Blessed Flame Dragon!"
	tActivity2015TheBestHero_Text[3006980][2] = "You received a 15-day 1% Blessed Flame Dragon!"
	tActivity2015TheBestHero_Text[3006980][3] = "You received a 30-day 1% Blessed Flame Dragon!"
	tActivity2015TheBestHero_Text[3006980][4] = "You received a permanent 1% Blessed Flame Dragon!"

--打开武艺超群礼包
tActivity2015TheBestHero_Text[3006978] = {}
	tActivity2015TheBestHero_Text[3006978]["111"] = "When you open this pack, you have a chance to receive wonderful rewards like Dragon Ball (B), Chi Points, +2 Stone (B), P6 Dragon Soul Bag."
	tActivity2015TheBestHero_Text[3006978]["112"] = "~You can also combine 5 packs into an Incomparable Pack. So, what do you say?"
	tActivity2015TheBestHero_Text[3006978]["Option1"] = "Directly~open."
	tActivity2015TheBestHero_Text[3006978]["Option2"] = "Combine~5~packs."
	--接1：直接打开。
	--背包满
	tActivity2015TheBestHero_Text[3006978]["211"] = "Your inventory is full. Please make some room, first."
	tActivity2015TheBestHero_Text[3006978]["Option3"] = "Okay."
	--成功获得奖励
	tActivity2015TheBestHero_Text[3006978][1] = "You received 2 Special Training Pills (B)!"
	tActivity2015TheBestHero_Text[3006978][2] = "You received 2 Favored Training Pills (B)!"
	tActivity2015TheBestHero_Text[3006978][3] = "You received 2 EXP Balls (B)!"
	tActivity2015TheBestHero_Text[3006978][4] = "You received a 200 Points Chi Pack (B)!"
	tActivity2015TheBestHero_Text[3006978][5] = "You received a Meteor Scroll!"
	tActivity2015TheBestHero_Text[3006978][6] = "You received a Dragon Ball (B)!"
	tActivity2015TheBestHero_Text[3006978][7] = "You received 2 +1 Steeds (B)!"
	tActivity2015TheBestHero_Text[3006978][8] = "You received a +2 Steed (B)!"
	tActivity2015TheBestHero_Text[3006978][9] = "You received 2 +1 Stones (B)!"
	tActivity2015TheBestHero_Text[3006978][10] = "You received a +2 Stone (B)!"
	tActivity2015TheBestHero_Text[3006978][11] = "You received 2 Elite Gem Bags!"
	tActivity2015TheBestHero_Text[3006978][12] = "You received a P6 Random Soul Pack!"
	tActivity2015TheBestHero_Text[3006978][13] = "You received a Flame Dragon garment (B)!"
	--接2：合成盖世无双礼包。
	--背包满
	tActivity2015TheBestHero_Text[3006978]["221"] = "Your inventory is full. Please make some room, first."
	--数量不足
	tActivity2015TheBestHero_Text[3006978]["231"] = "Make sure you have 5 Outstanding Martial Packs to combine them into an Incomparable Pack."
	--成功合成
	tActivity2015TheBestHero_Text[3006978]["CompoundItem"] = "You received an Incomparable Pack!"

--打开盖世无双礼包  提示
tActivity2015TheBestHero_Text[3006979] = {}
	--背包满
	tActivity2015TheBestHero_Text[3006979]["FullBag"] = "Your inventory is full. Please make some room, first."
	--打开成功，获得奖励
	tActivity2015TheBestHero_Text[3006979][1] = "You received a Vital Pill (B)!"
	tActivity2015TheBestHero_Text[3006979][2] = "You received 2 Protection Pills!"
	tActivity2015TheBestHero_Text[3006979][3] = "You received 5 FavoredTrainingPill (B)!"
	tActivity2015TheBestHero_Text[3006979][4] = "You received 5 SpecialTrainingPill (B)!"
	tActivity2015TheBestHero_Text[3006979][5] = "You received a Senior Training Pill (B)!"
	tActivity2015TheBestHero_Text[3006979][6] = "You received a Super Dragon Gem!"
	tActivity2015TheBestHero_Text[3006979][7] = "You received a Super Phoenix Gem!"
	tActivity2015TheBestHero_Text[3006979][8] = "You received a Dragon Ball!"
	tActivity2015TheBestHero_Text[3006979][9] = "You received a DB Scroll!"
	tActivity2015TheBestHero_Text[3006979][10] = "You received 1 MeteorScrollBox!"
	tActivity2015TheBestHero_Text[3006979][11] = "You received a +3 Steed (B)!"
	tActivity2015TheBestHero_Text[3006979][12] = "You received a +6 Steed!"
	tActivity2015TheBestHero_Text[3006979][13] = "You received a +3 Stone (B)!"
	tActivity2015TheBestHero_Text[3006979][14] = "You received a +6 Stone!"
	tActivity2015TheBestHero_Text[3006979][15] = "You received a Refined Thunder Gem!"
	tActivity2015TheBestHero_Text[3006979][16] = "You received a Refined Glory Gem!"
	tActivity2015TheBestHero_Text[3006979][17] = "You received a Refined Tortoise Gem!"
	tActivity2015TheBestHero_Text[3006979][18] = "You received a Super Thunder Gem!"
	tActivity2015TheBestHero_Text[3006979][19] = "You received a Super Glory Gem!"
	tActivity2015TheBestHero_Text[3006979][20] = "You received a Super Tortoise Gem!"
	tActivity2015TheBestHero_Text[3006979][21] = "You received a Permanent Stone!"
	tActivity2015TheBestHero_Text[3006979][22] = "You received a P7 Weapon Soul Pack (B)!"
	tActivity2015TheBestHero_Text[3006979][23] = "You received a P7 Armor Soul Pack (B)!"
	tActivity2015TheBestHero_Text[3006979][24] = "You received a 30-day Flame Dragon (B)!"
	tActivity2015TheBestHero_Text[3006979][25] = "You received a Flame Dragon Fragment!"


------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]线上成就榜.lua
--Purpose:	线上成就榜
--Creator: 	严振飞
--Created:	2015/08/19
------------------------------------------------------------------------------------
----------------------------------NPC对白--------------------------------------
tOnlineGreatevent_Text = {}
------------------------------【国战战备官】---------------------------------------
tOnlineGreatevent_Text[18797] = {}

-- 活动前
tOnlineGreatevent_Text[18797]["Text111"] = "Everything seems to indicate that the Kingdom War is coming. The heroes entered the Achievement List for the war will be wonderfully rewarded.\n"
tOnlineGreatevent_Text[18797]["Text112"] = "    Time: October 15th to November 15th\n"
tOnlineGreatevent_Text[18797]["Text113"] = "    Requirement: No level restriction, but only 2nd-reborn heroes are qualified to compete for the Achievement List.\n"
tOnlineGreatevent_Text[18797]["Text114"] = "    Rewards: tons of CPs (B), Epic Weapon Tickets, 1%% Blessed Song of Despair garment, etc."
tOnlineGreatevent_Text[18797]["111"] = "I~won`t~let~you~down."
-- 活动后
tOnlineGreatevent_Text[18797]["Text121"] = "Who will win and who will lose in the Kingdom War?"
tOnlineGreatevent_Text[18797]["121"] = "Life~is~filled~with~unknown."
-- 活动中
tOnlineGreatevent_Text[18797]["Text131"] = "The Kingdom War has broken out, getting numerous heroes involved in this terrific storm. Heroes who enter the Kingdom War Achievement List will be wonderfully rewarded.\n"
tOnlineGreatevent_Text[18797]["Text132"] = "    --------- You`ve earned %d Glory Points ---------\n    Time: October 15th to November 15th\n"
tOnlineGreatevent_Text[18797]["Text133"] = "    Requirement: No level restriction, but only 2nd-reborn heroes are qualified\n    to compete for the Achievement List.\n"
tOnlineGreatevent_Text[18797]["Text134"] = "    Rewards: tons of CPs (B), Epic Weapon Tickets, 1% Blessed Song of Despair garment, etc."
tOnlineGreatevent_Text[18797]["131"] = "Daily~sign~in."
tOnlineGreatevent_Text[18797]["132"] = "Submit~the~glory~badges."
tOnlineGreatevent_Text[18797]["133"] = "Exchange~for~EXP."
tOnlineGreatevent_Text[18797]["134"] = "Head~to~the~event~page."
tOnlineGreatevent_Text[18797]["135"] = "Tell~me~more."
tOnlineGreatevent_Text[18797]["136"] = "I`m~not~interested."

-- 当天已签到
tOnlineGreatevent_Text[18797]["Text141"]  = "You`ve already signed in, today. Go kill enemies, and earn yourself more Glory Points!"
tOnlineGreatevent_Text[18797]["141"] = "I~see."

-- 签到成功
tOnlineGreatevent_Text[18797]["Text151"] = "Ah, you look quite energetic. It seems you`re ready for the Kingdom War. This is your Online Badge. %s"
tOnlineGreatevent_Text[18797]["Text152"] = "~I await your good news!"
tOnlineGreatevent_Text[18797]["Text153"] = "~I`ll also give an extra reward since you`ve signed in for the war for %d days."
tOnlineGreatevent_Text[18797]["151"] = "Thanks!"


-- 【上交令牌】
-- 上交类型选择对白
tOnlineGreatevent_Text[18797]["Text211"] = "Which kind of glory badge would you like to submit?\n"
tOnlineGreatevent_Text[18797]["Text212"] = "    --------- You`ve earned %d Glory Points ---------"
tOnlineGreatevent_Text[18797]["211"] = "Online~Badge."
tOnlineGreatevent_Text[18797]["212"] = "Credit~Badge."
tOnlineGreatevent_Text[18797]["213"] = "Hunter~Badge."
tOnlineGreatevent_Text[18797]["214"] = "Learn~about~other~things."
-- 在线令牌
tOnlineGreatevent_Text[18797]["Text221"] = "The Online Badges are at 3 levels. Which one do you want to submit?"
tOnlineGreatevent_Text[18797]["221"] = "Online~Badge~(10)."
tOnlineGreatevent_Text[18797]["222"] = "Online~Badge~(50)."
tOnlineGreatevent_Text[18797]["223"] = "Online~Badge~(100)."
tOnlineGreatevent_Text[18797]["224"] = "Submit~other~badges."
-- 返利令牌
tOnlineGreatevent_Text[18797]["Text231"] = "The Credit Badges are at 5 levels. Which one do you want to submit?"
tOnlineGreatevent_Text[18797]["231"] = "Credit~Badge~(50)."
tOnlineGreatevent_Text[18797]["232"] = "Credit~Badge~(100)."
tOnlineGreatevent_Text[18797]["233"] = "Credit~Badge~(500)."
tOnlineGreatevent_Text[18797]["234"] = "Credit~Badge~(1000)."
tOnlineGreatevent_Text[18797]["235"] = "Credit~Badge~(10000)."
tOnlineGreatevent_Text[18797]["236"] = "Submit~other~badges."


-- 上交令牌数量输入对白
tOnlineGreatevent_Text[18797]["Text241"] = "How many %s do you want to submit?"
tOnlineGreatevent_Text[18797]["241"] = "Enter~the~amount:"
tOnlineGreatevent_Text[18797]["242"] = "Submit~other~kinds~of~badges."

-- 令牌不足
tOnlineGreatevent_Text[18797]["Text251"] = "I`ve heard about your brave deeds in the war, but you don`t have %d %s(s)."
tOnlineGreatevent_Text[18797]["251"] = "Really?~I`ll~check~it~again."

-- 输入字符有误
tOnlineGreatevent_Text[18797]["Text261"] = "Please enter a correct number of the glory badges you want to submit."
tOnlineGreatevent_Text[18797]["261"] = "Enter~the~amount."

-- 上交成功
tOnlineGreatevent_Text[18797]["Text271"] = "Your admirable performance in the Kingdom War has been recorded. So far, you`ve earned %d Glory Points."
tOnlineGreatevent_Text[18797]["271"] = "Submit~more~badges."
tOnlineGreatevent_Text[18797]["272"] = "Okay."


-- 【兑换经验】
-- 兑换类型选择对白
tOnlineGreatevent_Text[18797]["Text311"] = "Which kind of glory badge would you like to exchange for EXP?"
tOnlineGreatevent_Text[18797]["311"] = "Online~Badge."
tOnlineGreatevent_Text[18797]["312"] = "Credit~Badge."
tOnlineGreatevent_Text[18797]["313"] = "Hunter~Badge."
tOnlineGreatevent_Text[18797]["314"] = "Learn~about~other~things."


-- 在线令牌(经验)
tOnlineGreatevent_Text[18797]["Text321"] = "Online Badge (10) can be exchanged for 1 minute of EXP;\n"
tOnlineGreatevent_Text[18797]["Text322"] = "Online Badge (50) can be exchanged for 5 minutes of EXP;\n"
tOnlineGreatevent_Text[18797]["Text323"] = "Online Badge (100) can be exchanged for 10 minutes of EXP;\n"
tOnlineGreatevent_Text[18797]["Text324"] = "Which one would you like to exchange for EXP?"
tOnlineGreatevent_Text[18797]["321"] = "Online~Badge~(10)."
tOnlineGreatevent_Text[18797]["322"] = "Online~Badge~(50)."
tOnlineGreatevent_Text[18797]["323"] = "Online~Badge~(100)."
tOnlineGreatevent_Text[18797]["324"] = "Exchange~other~badges."

-- 返利令牌（经验）
tOnlineGreatevent_Text[18797]["Text331"] = "Credit Badge (50) can be exchanged for 5 minutes of EXP;\n"
tOnlineGreatevent_Text[18797]["Text332"] = "Credit Badge (100) can be exchanged for 10 minutes of EXP;\n"
tOnlineGreatevent_Text[18797]["Text333"] = "Credit Badge (500) can be exchanged for 50 minutes of EXP;\n"
tOnlineGreatevent_Text[18797]["Text334"] = "Credit Badge (1000) can be exchanged for 100 minutes of EXP;\n"
tOnlineGreatevent_Text[18797]["Text335"] = "Credit Badge (10000) can be exchanged for 1,000 minutes of EXP;\n"
tOnlineGreatevent_Text[18797]["Text336"] = "Which one would you like to exchange for EXP?"
tOnlineGreatevent_Text[18797]["331"] = "Credit~Badge~(50)."
tOnlineGreatevent_Text[18797]["332"] = "Credit~Badge~(100)."
tOnlineGreatevent_Text[18797]["333"] = "Credit~Badge~(500)."
tOnlineGreatevent_Text[18797]["334"] = "Credit~Badge~(1000)."
tOnlineGreatevent_Text[18797]["335"] = "Credit~Badge~(10000)."
tOnlineGreatevent_Text[18797]["336"] = "Learn~about~other~things."


-- 兑换成功(经验)
tOnlineGreatevent_Text[18797]["Text341"] = "Congratulations! You successfully exchanged %d %s (s) for %d minute(s) of EXP. Go and fight for a brighter future!"
tOnlineGreatevent_Text[18797]["341"] = "Exchange~more~badges."
tOnlineGreatevent_Text[18797]["342"] = "Thanks!"


-- 兑换令牌数量输入对白
tOnlineGreatevent_Text[18797]["Text351"] = "How many %s would you like to exchange?"
tOnlineGreatevent_Text[18797]["351"] = "Enter~the~amount:"
tOnlineGreatevent_Text[18797]["352"] = "Exchange~other~badges."

-- 兑换成功(修行值)
tOnlineGreatevent_Text[18797]["Text361"] = "As you`ve reached the max level, your %d %s(s) brought you %d Study Points. Go and fight for a brighter future!"
tOnlineGreatevent_Text[18797]["361"] = "Exchange~more~badges."
tOnlineGreatevent_Text[18797]["362"] = "Thanks!"

-- 令牌不足
tOnlineGreatevent_Text[18797]["Text371"] = "I`m afraid I can`t help you since you don`t have %d %s(s)."
tOnlineGreatevent_Text[18797]["371"] = "I`ll~come~back~later."

-- 输入错误
tOnlineGreatevent_Text[18797]["Text381"] = "Please enter a correct number of glory badges you want to exchange."
tOnlineGreatevent_Text[18797]["381"] = "Enter~the~amount."

-- 【了解活动详情】
tOnlineGreatevent_Text[18797]["Text411"] = "During the event, heroes can collect various glory badges by signing in for Kingdom War everyday, killing fierce BOSS,"
tOnlineGreatevent_Text[18797]["Text412"] = "~and crediting TQ Point Cards. For 2nd-reborn heroes or above, these badges can be used to accumulate Glory Points"
tOnlineGreatevent_Text[18797]["Text413"] = "~to compete for the Achievement List, or exchange them for EXP. I`ve prepared wonderful rewards for the heroes listed on the Achievement List."
tOnlineGreatevent_Text[18797]["Text414"] = "~While for non-2nd-reborn heroes, you can only exchange these badges for EXP."
tOnlineGreatevent_Text[18797]["411"] = "What`re~the~Bosses~you~mentioned?"

-- 贼匪魔王是哪些
tOnlineGreatevent_Text[18797]["Text421"] = "     BOSS                          Hunter Badges Rewarded\n"
tOnlineGreatevent_Text[18797]["Text422"] = "Nemesis Tyrant            3 Hunter Badges (10)\n"
tOnlineGreatevent_Text[18797]["Text423"] = "Snow Banshee             2 Hunter Badges (10)\n"
tOnlineGreatevent_Text[18797]["Text424"] = "Thrilling Spook 3           2 Hunter Badges (10)\n"
tOnlineGreatevent_Text[18797]["Text425"] = "Thrilling Spook 2           1 Hunter Badge (10)\n"
tOnlineGreatevent_Text[18797]["Text426"] = "Please note that there is only one chance for you to earn glory Hunter Badges from Thrilling Spook`s lair."
tOnlineGreatevent_Text[18797]["421"] = "I~see."


-- 其他
tOnlineGreatevent_Text["SpaceFull"] = "Your inventory is full. Please make some room, first."
tOnlineGreatevent_Text["SpaceMonster"] = "Your inventory is full, and you can`t received the reward for the glory badge."
tOnlineGreatevent_Text["Sign"] = "Since you`ve signed in for the Kingdom War for %d days in a row, you received an Online Badge (10) %s"
tOnlineGreatevent_Text["SignAdd"] = "~and %d %s(s)!"
tOnlineGreatevent_Text["MonsterLimit"] = "You`ve earned 6,000 points by killing monsters, and you can`t receive more badges."
tOnlineGreatevent_Text["ThrillingSpook"] = "There is only one chance for you to earn Hunter Badges from Thrilling Spook`s lair, every day."
tOnlineGreatevent_Text["MonsterToken"] = "You received %d %s(s)!"
tOnlineGreatevent_Text["DelToken"] = "%s is broken, and you threw it away."
tOnlineGreatevent_Text["GetPoint"] = "You received %d Glory Points!"
tOnlineGreatevent_Text["ExpToken"] = {}
tOnlineGreatevent_Text["ExpToken"][4] = "You received %d minutes of EXP!"
tOnlineGreatevent_Text["ExpToken"][6] = "You received %d Study Points!"
tOnlineGreatevent_Text["TokenPag"] = "You received %d %s(s)%s!"
tOnlineGreatevent_Text["TokenPagAdd"] = " and %d %s(s)"

------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]联盟功能背包信
--Purpose:	联盟功能背包信
--Creator: 	郑鋆
--Created:	2015/07/30
------------------------------------------------------------------------------------

tAllianceBackpackLetter_Text = {}
-- 联盟国战问答官
tAllianceBackpackLetter_Text[18740] = {}
tAllianceBackpackLetter_Text[18740]["111"] = "The Realm which connects each server has been unlocked, and the threat from foreign servers is getting fierce. At the right moment,"
tAllianceBackpackLetter_Text[18740]["112"] = "~the Unions come into being where heroes are bound by a common hatred for enemies. If you know the system well, take a quiz to win a nice reward."
tAllianceBackpackLetter_Text[18740]["Option1"] = "Start~the~quiz."
tAllianceBackpackLetter_Text[18740]["Option2"] = "Learn~about~Union~and~Kingdom."
tAllianceBackpackLetter_Text[18740]["Option21"] = "I`m~not~ready,~yet."

tAllianceBackpackLetter_Text[18740]["211"] = "Union Leader can issue a Loot Token to start a Plunder War against the reigning hostile Kingdom and loot its Treasury."
tAllianceBackpackLetter_Text[18740]["212"] = "~After the war, if this Union has the most Gold Bricks in the server, it can overthrow the old regime and upgrade to be a Kingdom."
tAllianceBackpackLetter_Text[18740]["Option3"] = "What`re~Tempest~Wings?"

tAllianceBackpackLetter_Text[18740]["221"] = "Tempest Wings are an attractive talisman equipped on the back to enhances the wearer`s Attributes and Battle Power."
tAllianceBackpackLetter_Text[18740]["222"] = "~By upgrading, Tempest Wings have its appearance to evolve. The wings for the Emperor are exclusive which delivers"
tAllianceBackpackLetter_Text[18740]["223"] = "~a sense of supremacy."
tAllianceBackpackLetter_Text[18740]["Option4"] = "What`s~Kingdom~Mission?"

tAllianceBackpackLetter_Text[18740]["231"] = "Heroes gather for the Kingdom Mission to jointly resist foreign enemies and win wonderful rewards."
tAllianceBackpackLetter_Text[18740]["232"] = "~For Jiang Hu fighters, completing the mission will also reduce their training time. Additionally, mission accumulation function"
tAllianceBackpackLetter_Text[18740]["233"] = "~is coming soon, which allows heroes to accumulate the mission rewards up to 7 days. When they complete the mission, the rewards are given altogether."
tAllianceBackpackLetter_Text[18740]["Option5"] = "What`s~Inner~Power?"

tAllianceBackpackLetter_Text[18740]["241"] = "The Potency Points that heroes have attained in Kingdom Mission can be used to acquire the Inner Power for bonus Attributes."
tAllianceBackpackLetter_Text[18740]["242"] = "~If your Inner Power is not Perfect, you can reshape it to get better Attributes. It`s free, and only requires Potency Points."
tAllianceBackpackLetter_Text[18740]["243"] = "~In the near future, you`ll be able to transfer the bonus Attributes of your Inner Power to others. It`s amazing, isn`t it?"
tAllianceBackpackLetter_Text[18740]["Option6"] = "Indeed."

tAllianceBackpackLetter_Text[18740]["311"] = "Union Leader can issue a Loot Token and start a Plunder War against the reigning hostile Kingdom to loot its Treasury."
tAllianceBackpackLetter_Text[18740]["312"] = "~What should this Union rank the top in the server to upgrade to be a Kingdom after the war?"
tAllianceBackpackLetter_Text[18740]["Option7"] = "Gold~Bricks~reserve.~(Correct)"
tAllianceBackpackLetter_Text[18740]["Option8"] = "Kills."

tAllianceBackpackLetter_Text[18740]["321"] = "Tempest Wings are an attractive talisman equipped on the back to greatly enhance the wearer`s Attributes and Battle Power."
tAllianceBackpackLetter_Text[18740]["322"] = "~By upgrading, Tempest Wings have its appearance to evolve. What do the Emperor`s wings look like?"
tAllianceBackpackLetter_Text[18740]["323"] = ""
tAllianceBackpackLetter_Text[18740]["Option9"] = "Exclusive~wings.(Correct)"
tAllianceBackpackLetter_Text[18740]["Option10"] = "Normal~wings."

tAllianceBackpackLetter_Text[18740]["331"] = "Heroes gather for the Kingdom Mission to jointly resist foreign enemies. Except the benefit for Jiang Hu fighters,"
tAllianceBackpackLetter_Text[18740]["332"] = "~heroes who complete the Kingdom Mission will also receive rewards. What do you think of these rewards?"
tAllianceBackpackLetter_Text[18740]["Option11"] = "Wonderful~rewards.~(Correct)"
tAllianceBackpackLetter_Text[18740]["Option12"] = "Normal~rewards."

tAllianceBackpackLetter_Text[18740]["341"] = "The mission accumulation function is coming soon. How many times can you accumulate the mission rewards?"
tAllianceBackpackLetter_Text[18740]["Option13"] = "Up~to~7~times.~(Correct)"
tAllianceBackpackLetter_Text[18740]["Option14"] = "Only~1~time."

tAllianceBackpackLetter_Text[18740]["351"] = "The Potency Points earned in the Kingdom Mission can be used to acquire the Inner Power. Is there anything else needed to acquire the Inner Power?"
tAllianceBackpackLetter_Text[18740]["Option15"] = "Nothing~else.~It`s~free."
tAllianceBackpackLetter_Text[18740]["Option16"] = "Chi~Points."

tAllianceBackpackLetter_Text[18740]["361"] = "If your Inner Power is not Perfect, you can reshape it. What should the bonus Attribute become after the reshape?"
tAllianceBackpackLetter_Text[18740]["Option17"] = "Better."
tAllianceBackpackLetter_Text[18740]["Option18"] = "Worse."

tAllianceBackpackLetter_Text[18740]["371"] = "What can you do with the bonus Attribute of your Inner Power?"
tAllianceBackpackLetter_Text[18740]["Option19"] = "Transfer~attributes~to~others."
tAllianceBackpackLetter_Text[18740]["Option20"] = "Adjust~the~attributes."
	
tAllianceBackpackLetter_Text[18740]["411"] = "Correct! Here`s 10 minutes of EXP for you! Pay attention to the next question."
tAllianceBackpackLetter_Text[18740]["421"] = "Correct! Here are 10 Chi Points for you! Pay attention to the next question."
tAllianceBackpackLetter_Text[18740]["Option22"] = "I`m~ready."

tAllianceBackpackLetter_Text[18740]["431"] = "You`ve answered all the questions correctly. If you`re still interested, come and find me tomorrow. I`ll give you 10 minutes of EXP for every correct answer."
tAllianceBackpackLetter_Text[18740]["441"] = "You`ve answered all the questions correctly. If you`re still interested, come and find me tomorrow. I`ll give you 10 Chi Points for every correct answer."
tAllianceBackpackLetter_Text[18740]["Option23"] = "Okay. "

tAllianceBackpackLetter_Text[18740]["451"] = "You should reach at least Level 100 to take the Kingdom War Quiz."
tAllianceBackpackLetter_Text[18740]["452"] = ""
tAllianceBackpackLetter_Text[18740]["Option24"] = "Alright. "

tAllianceBackpackLetter_Text[18740]["Error"] = "Sorry, wrong answer. Please think it over, and give me your anwer again."
tAllianceBackpackLetter_Text[18740]["Reward"] = "Congratulations! You`ve completed the Kingdom War Quiz. Wish you an overwhelming victory in the Kingdom War!"
tAllianceBackpackLetter_Text[18740]["Complete"] = "Congratulations! You`ve answered all the questions correctly! Wish you a splendid exploit in the Kingdom War!"

-- 3006952		联盟告示
tAllianceBackpackLetter_Text[3006952] = {}
tAllianceBackpackLetter_Text[3006952]["111"] = "The Kingdom War is about to break out which involves millions of heroes. Noboby cares where you come from."
tAllianceBackpackLetter_Text[3006952]["112"] = "~Heroes are bound by a common hatred for enemies and a great ambition of world unification."
tAllianceBackpackLetter_Text[3006952]["Option1"] = "Learn~about~Union~and~Kingdom."
tAllianceBackpackLetter_Text[3006952]["Option2"] = "Take~the~Kingdom~War~Quiz."
tAllianceBackpackLetter_Text[3006952]["Option113"] = "Learn~about~the~Glory~Badge."

tAllianceBackpackLetter_Text[3006952]["211"] = "Union Leader can issue a Loot Token to start a Plunder War against the reigning hostile Kingdom and loot its Treasury."
tAllianceBackpackLetter_Text[3006952]["212"] = "~After the war, if this Union has the most Gold Bricks in the server, it can overthrow the old regime and upgrade to be a Kingdom."
tAllianceBackpackLetter_Text[3006952]["Option3"] = "What`re~Tempest~Wings?"

tAllianceBackpackLetter_Text[3006952]["221"] = "Tempest Wings are an attractive talisman equipped on the back to enhances the wearer`s Attributes and Battle Power."
tAllianceBackpackLetter_Text[3006952]["222"] = "~By upgrading, Tempest Wings have its appearance to evolve. The wings for the Emperor are exclusive which delivers"
tAllianceBackpackLetter_Text[3006952]["223"] = "~a sense of supremacy."
tAllianceBackpackLetter_Text[3006952]["Option4"] = "What`s~Kingdom~Mission?"

tAllianceBackpackLetter_Text[3006952]["231"] = "Heroes gather for the Kingdom Mission to jointly resist foreign enemies and win wonderful rewards."
tAllianceBackpackLetter_Text[3006952]["232"] = "~For Jiang Hu fighters, completing the mission will also reduce their training time. Additionally, mission accumulation function"
tAllianceBackpackLetter_Text[3006952]["233"] = "~is coming soon, which allows heroes to accumulate the mission rewards up to 7 days. When they complete the mission, the rewards are given altogether."
tAllianceBackpackLetter_Text[3006952]["Option5"] = "What`s~Inner~Power?"

tAllianceBackpackLetter_Text[3006952]["241"] = "The Potency Points that heroes have attained in Kingdom Mission can be used to acquire the Inner Power for bonus Attributes."
tAllianceBackpackLetter_Text[3006952]["242"] = "~If your Inner Power is not Perfect, you can reshape it to get better Attributes. It`s free, and only requires Potency Points."
tAllianceBackpackLetter_Text[3006952]["243"] = "~In the near future, you`ll be able to transfer the bonus Attributes of your Inner Power to others."
tAllianceBackpackLetter_Text[3006952]["Option6"] = "I~see."

-- 10月成就榜
tAllianceBackpackLetter_Text[3006952]["311"] = "From Oct. 15th to Nov. 15th, 2nd-reborn heroes can collect various Glory Badges by daily sign-in for Kingdom War, monster hunting or TQ Point Card "
tAllianceBackpackLetter_Text[3006952]["312"] = "credit. These badges are used to accumulate Glory Points at the Achievement List Officer. The more points you earn, the bigger the chance to enter the "
tAllianceBackpackLetter_Text[3006952]["313"] = "Achievement List for fabulous rewards. While non-2nd-reborn heroes can exchange the Badges for EXP."
tAllianceBackpackLetter_Text[3006952]["Option311"] = "Take~me~to~see~the~Officer."

tAllianceBackpackLetter_Text[3006952]["NoSpace"] = "Your inventory is full. Please make some room, and login again to receive a Union Notice."
tAllianceBackpackLetter_Text[3006952]["RewardItem"] = "You received a Union Notice. Hurry and check it in your inventory."

------------------------------------------------------------------------------------
------------------------------------------------ 联盟版本的相关文字------------------------------------------------
------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
--Name:			跨服任务刺探SQL数据
--Purpose:		跨服任务刺探SQL数据
--Creator: 		张磊
--Created:		12/03/2014
------------------------------------------------------------------------------------

------------------------------------------NPC对话-------------------------------------------

--中文索引
tNational_War_Text = {}
tNational_War_Text[17400] = {}
tNational_War_Text[17401] = {}
tNational_War_Text[17402] = {}
tNational_War_Text[17403] = {}
tNational_War_Text[17404] = {}
tNational_War_Text[17405] = {}

--国战任务军师 

--任务主对白
tNational_War_Text[17400]["Text111"] = "A real hero must harbor great ambition for his kingdom. In the name of Conquer,"
tNational_War_Text[17400]["Text112"] = "~I`m calling heroes who`ve reached Level 110 of the 1st rebirth to take the Kingdom Mission."
tNational_War_Text[17400]["Text113"] = "~Let`s fight and make a way to explore the new world."

--接受刺探任务对白
tNational_War_Text[17400]["Text211"] = "City Lurker, Mr. Mystery, Military Spy, Official Agent, and Travelling Scout were dispatched to the Realm"
tNational_War_Text[17400]["Text212"] = "~to collect military intelligence. Um... 10 years ago? 20? 30? Anyway, I want you to contact 3 of them, and bring"
tNational_War_Text[17400]["Text213"] = "~back the latest intelligence."

--成功接收任务
tNational_War_Text[17400]["Text221"] = "It`s getting late. Hurry and contact any three of the undercover men in the Realm, and bring back the latest intelligence."
tNational_War_Text[17400]["Text222"] = "~Don`t forget to deliver my deepest consolation to them."
tNational_War_Text[17400]["Text223"] = ""



--玩家选1, 失败, 玩家等级不足
tNational_War_Text[17400]["Text311"] = "You haven`t reached Level 110 of the 1st rebirth. The Realm is so dangerous that you`d better keep away."
tNational_War_Text[17400]["Text312"] = ""

--玩家选1, 失败, 当日已完成该任务
tNational_War_Text[17400]["Text411"] = "You`ve already brought back intelligence from the Realm. No need to take the risk, again."

--玩家选1, 成功完成任务
tNational_War_Text[17400]["Text511"] = "Aha, it`s exactly what I want! You really did us a good service!"
tNational_War_Text[17400]["Text512"] = "~See, this is your reward."

tNational_War_Text[17400]["Option26"] = "It`s~my~duty."

--玩家选1, 未完成任务
tNational_War_Text[17400]["Text611"] = "It seems you haven`t collected intelligence in the Realm. Hurry and contact any three of City Lurker, Mr. Mystery,"
tNational_War_Text[17400]["Text612"] = "~Military Spy, Official Agent, and Travelling Scout in the Realm, and bring back the latest information."
tNational_War_Text[17400]["Text613"] = ""


--没有任务的提示
tNational_War_Text[17400]["Text931"] = "You should accept the mission, first."

--天石不足
tNational_War_Text[17400]["Text941"] = "You don`t have enough CPs to directly complete this mission."

--玖．Kingdom Deed兑换奖励
tNational_War_Text[17400]["Text951"] = "You can collect up to 300 Kingdom Deeds in a day from the Kingdom Mission. If you`ve earned 50, 150 or 300 points,"
tNational_War_Text[17400]["Text952"] = "~you can swap them for corresponding reward with me. So, which pack would you like to claim?"
tNational_War_Text[17400]["Text953"] = "\n    Your Kingdom Deeds: %d."

tNational_War_Text[17400]["Option33"] = "Outstanding~Exploit~Pack.~(50)"
tNational_War_Text[17400]["Option34"] = "Brilliant~Exploit~Pack.~(150)"
tNational_War_Text[17400]["Option35"] = "Supreme~Exploit~Pack.~(300)"
tNational_War_Text[17400]["Option40"] = "What`re~in~the~packs?"
tNational_War_Text[17400]["Option41"] = "I`ll~think~about~it."


--【玩家选择, 失败, 积分未达到】
tNational_War_Text[17400]["Text961"] = "Sorry, you can`t claim this pack since you don`t have %s Kingdom Deeds. Go complete the Kingdom Mission to earn more Kingdom Deeds."
tNational_War_Text[17400]["Option36"] = "Alright."

--【玩家选择, 失败, 已领取该礼包】
tNational_War_Text[17400]["Text971"] = "You`ve already claimed this pack, today."
tNational_War_Text[17400]["Option37"] = "Okay."

--【玩家选择, 成功领取】
tNational_War_Text[17400]["Text981"] = "Thanks for your service. Here`s a reward for you."
tNational_War_Text[17400]["Option38"] = "Thanks!"

--【背包空间不足】
tNational_War_Text[17400]["Text991"] = "Please make some room in your inventory for the reward, first."

--【背包空间不足_不能完成任务】
tNational_War_Text[17400]["Text1011"] = "Please make some room in your inventory for the reward, first."

--查看礼包内容
tNational_War_Text[17400]["Text1111"] = "You can swap 50 Kingdom Deeds for an Outstanding Exploit Pack: 50 War Exploits, 400 Potency Points and 50 points for Union Fund;\n"
tNational_War_Text[17400]["Text1112"] = "~150 Kingdom Deeds for a Brilliant Exploit Pack: 50 Champion Points (excluded if maximum is reached), 100 War Exploits, 600 Potency Points and 160 points to Union Fund;\n"
tNational_War_Text[17400]["Text1113"] = "~300 Kingdom Deeds for a Supreme Exploit Pack: 100 Champion Points (excluded if maximum is reached), 150 War Exploits, 1000 Potency Points and 360 points to Union Fund.\n"

tNational_War_Text[17400]["Option42"] = "I~see."


-- 闹市潜伏者
--任务对白
tNational_War_Text[17401]["Text111"] = "Tons of Gold Bricks were missing in the Treasury. People have no idea."
tNational_War_Text[17401]["Text112"] = "~I`ve got some clues, but not for sure. I`ll call you later."

--闲聊对白
tNational_War_Text[17401]["Text121"] = "Hey, I got a great deal on these two fish in the market. Which one is better? Okay, I`ll make it my dinner."
tNational_War_Text[17401]["Text131"] = "Hey, I got a great deal on these two fish in the market. Which one is better?\n(Whisper: I`ve told you what I knew. Go find the other undercover men. The quest interface can help you locate them.)"

---- 神秘卧底
--任务对白
tNational_War_Text[17402]["Text111"] = "Recently, I saw many strangers here. What plot are they brewing? I`ll figure it out."
tNational_War_Text[17402]["Text112"] = ""

--闲聊对白
tNational_War_Text[17402]["Text121"] = "Haha, I just bought a hospital for my enemies. Please don`t love me. I know I`m generous."
tNational_War_Text[17402]["Text131"] = "I bought a hospital for my enemies. I`m always generous to anyone.\n(Whisper: I`ve told you what I knew. Go find the other undercover men. The quest interface can help you locate them.)"

-- 军营间谍
--任务对白
tNational_War_Text[17403]["Text111"] = "Hush! Let`s find another place to talk. In the barracks, generals are unhappy with each other."
tNational_War_Text[17403]["Text112"] = "~Everyone is hatching a plot. I`m expecting a chaos."

--闲聊对白
tNational_War_Text[17403]["Text121"] = "Do you want to see my new horse? I bet you`ll be amazed."
tNational_War_Text[17403]["Text131"] = "Do you want to see my new horse? I bet you`ll be amazed.\n(Whisper: I`ve told you what I knew. Go find the other undercover men. The quest interface can help you locate them.)"


-- 朝廷细作
--任务对白
tNational_War_Text[17404]["Text111"] = "The court here is suffering from constant civil strife. The two major parties are in a struggle for power."
tNational_War_Text[17404]["Text112"] = "~I`m afraid a bloody civil war cannot be avoided."

--闲聊对白
tNational_War_Text[17404]["Text121"] = "I feel uncomfortable, not on duty today."
tNational_War_Text[17404]["Text131"] = "I feel uncomfortable, not on duty today.\n(Whisper: I`ve told you what I knew. Go find the other undercover men. The quest interface can help you locate them.)"

-- 武林探子
--任务对白
tNational_War_Text[17405]["Text111"] = "There is no big event recently. Heroes are looking for a legendary book of martial art."
tNational_War_Text[17405]["Text112"] = "~I`ll keep an eye on it."

--闲聊对白
tNational_War_Text[17405]["Text121"] = "Ah, it`s really a good day to air my quilt."
tNational_War_Text[17405]["Text131"] = "Ah, it`s really a good day to air my quilt.\n(Whisper: I`ve told you what I knew. Go find the other undercover men. The quest interface can help you locate them.)"




--选项
tNational_War_Text[17400]["Option1"] = "The~Undercover~[Realm]."

tNational_War_Text[17400]["Option4"] = "I~see."

tNational_War_Text[17400]["Option5"] = "I`m~on~the~way."
tNational_War_Text[17400]["Option30"] = "I`ve~got~the~intelligence."
tNational_War_Text[17400]["Option31"] = "Directly~complete~it.~(10~CPs)"
tNational_War_Text[17400]["Option32"] = "Exchange~Kingdom~Deeds."

tNational_War_Text[17400]["Option20"] = "Sorry~to~bother~you."
tNational_War_Text[17400]["Option21"] = "I`m~getting~ready."
tNational_War_Text[17400]["Option22"] = "Okay."
tNational_War_Text[17400]["Option23"] = "Thanks."
tNational_War_Text[17400]["Option24"] = "Okay."
tNational_War_Text[17400]["Option25"] = "I~see."

tNational_War_Text[17401]["Option8"] = "Boring..."
tNational_War_Text[17401]["Option9"] = "I~got~you."

tNational_War_Text[17402]["Option10"] = "You`re~rich."
tNational_War_Text[17402]["Option11"] = "I~got~you."

tNational_War_Text[17403]["Option12"] = "Maybe~next~time."
tNational_War_Text[17403]["Option13"] = "I~got~you."

tNational_War_Text[17404]["Option14"] = "It`s~okay."
tNational_War_Text[17404]["Option15"] = "I~got~you."

tNational_War_Text[17405]["Option16"] = "Good~girl."
tNational_War_Text[17405]["Option17"] = "I~got~you."




tNational_War_Text["SysMsgCom"] = "You`ve completed the mission. Go back to your server and claim rewards from the Kingdom Mission Envoy."
tNational_War_Text["SysMsg"] = "You`ve successfully collected the intelligence."
tNational_War_Text["GreaterThan"] = "You can`t take Kingdom Mission more times, today."
tNational_War_Text["DirectlyRward"] = "You`ve already completed the mission. You`re able to claim the reward."
tNational_War_Text["NoComplete"] = "You`ve accepted this mission. Go and scout for intelligence in other servers."
tNational_War_Text["NoLeague"] = "You should joined a Union to receive the points for Union Fund."
tNational_War_Text["MaxLeagueMoney"] = "Your Union Fund has reached the maximum. You can`t receive more points."
tNational_War_Text["GetLeagueMoney"] = "You received %d Potency Points and %d War Exploits!"
tNational_War_Text["GetCultureValue"] = "You received %d Potency Points, %d War Exploits and %d points for Union Fund!"
tNational_War_Text["GetCultureValue_1"] = "You received %d Champion Points, %d Potency Points, %d War Exploits and %d points for Union Fund!"
tNational_War_Text["Get_JF"] = "You received %d Kingdom Deeds and %d Protection Pills!"
tNational_War_Text["Get_JF_1"] = "You received %d Protection Pills!"

tNational_War_Text["MoreCulture"] = "You`re carrying the maximum amount of Potency Points. Please consume some in Inner Power practice, first."

--领取过积分礼包
tNational_War_Text["GetRepeat"] = "You`ve already claimed this Exploit Pack."


--------------------------物品对白
tNational_War_Text[727505] = {}
tNational_War_Text[727505]["111"] = "The Realm acting as the frontline is under threat from growing crisis. With the Kingdom Mission Token, you`ll know how to complete the mission in the [Realm] and win abundant rewards.\n"
tNational_War_Text[727505]["112"] = "~Your Current Kingdom Deeds: %d"

tNational_War_Text[727505]["Option1"] = "The~Undercover~[Realm]."
tNational_War_Text[727505]["Option2"] = "Evil~Invasion~[Realm]."
tNational_War_Text[727505]["Option3"] = "Doom~of~Beast~[Realm]."
tNational_War_Text[727505]["Option4"] = "Struggle~for~Ores~[Realm]."
tNational_War_Text[727505]["Option10"] = "Talk~to~Kingdom~Mission~Envoy."

--【国境】刺探风云: 
tNational_War_Text[727505]["211"] = "Several years ago, a group of undercover men were sent to the Realm to collect intelligence."
tNational_War_Text[727505]["212"] = "~You`re asked to contact them and bring back the latest intelligence."

tNational_War_Text[727505]["Option5"] = "I`m~on~the~way."


--【国境】魔军入侵: 
tNational_War_Text[727505]["221"] = "Evil invasion! The frontier is asking for reinforcement! Go to the Realm, and fight against the Warriors of Rage and the enemies from other servers!"
tNational_War_Text[727505]["222"] = "\n     You`ve earned %d Strike Points."

tNational_War_Text[727505]["Option6"] = "Got~it!"

--【国境】国境妖龙。
tNational_War_Text[727505]["231"] = "Thunder Dragon is breaking the peace in the Realm. Go to the Realm, and hunt this fearsome beast that appears once every hour."
tNational_War_Text[727505]["232"] = ""

tNational_War_Text[727505]["Option7"] = "Head~to~see~the~dragon."

--【国境】国境晶矿。
tNational_War_Text[727505]["241"] = "Ore resources in the Realm are abundant. If you`re able to collect Small or Big Magic Ores for the Kingdom Mission Envoy, you`ll be nicely rewarded."
tNational_War_Text[727505]["242"] = ""

tNational_War_Text[727505]["Option8"] = "Go~collect~the~Magic~Ores."

--添加付费充满功勋值
tNational_War_Text[17400]["Option50"] = "Buy~Kingdom~Deeds."

tNational_War_Text[17400]["Text1211"] = "So far, you`ve earned %d Kingdom Deeds. If you want it full of 300 points immediately, you need to pay CPs. The costs:\n"
tNational_War_Text[17400]["Text1212"] = "    0 - 49 Kingdom Deeds: 99 CPs;\n    50 - 149 Kingdom Deeds: 54 CPs;\n    150 - 299 Kingdom Deeds: 27 CPs.\n"

tNational_War_Text[17400]["Option51"] = "Pay~CPs~to~fill~up~with~deeds."
tNational_War_Text[17400]["Option52"] = "I`ll~think~about~it."

-- 二次确认是否花费天石补满跨服功勋值
tNational_War_Text[17400]["Text1511"] = "Are you sure you want to pay %d CPs to fill up your Kingdom Deed?"
tNational_War_Text[17400]["Option55"] = "Yes."
tNational_War_Text[17400]["Option56"] = "Wait,~I~haven`t~decided."

-- 功勋值已满
tNational_War_Text[17400]["Text1521"] = "You`ve already collected 300 Kingdom Deeds. No need to fill it up with CPs."

--功勋值已满
tNational_War_Text[17400]["Text1311"] = "You`ve already collected 300 Kingdom Deeds. No need to fill it up with CPs."
tNational_War_Text[17400]["Option53"] = "I~see."

--天石不够
tNational_War_Text[17400]["Text1411"] = "Sorry, you don`t have enough CPs to fill up your Kingdom Deed."
tNational_War_Text[17400]["Option54"] = "Alright."


--成功补足积分
tNational_War_Text["CostCP"] = "Your Kingdom Deed is full, and you can exchange for a reward now."

--点击NPC领取了对应的礼包后提示‘
tNational_War_Text["GetJFPack"] = "The court just rewarded you with an exploit pack according to your War Exploits. Hurry and check it in your inventory."

------------------------------------------------------------------------------------
--Name:			[征服][任务脚本]跨服任务卫国英雄
--Purpose:		跨服任务卫国英雄
--Creator: 		郑鋆
--Created:		2014/12/10
------------------------------------------------------------------------------------
-- 文字提示
tServiceTask_PatrioticHero_Text = {}
-- NPC文字对白
tServiceTask_PatrioticHero_Text[17400] = {}

-- 保家卫国任务对白
tServiceTask_PatrioticHero_Text[17400]["Text10011"] = "During the Plunder War at 21:00 - 21:30 from Wednesday to Friday, Union members are called to fight against enemies from other unions"
tServiceTask_PatrioticHero_Text[17400]["Text10012"] = "~in local server or in Twin City of the target server. Killing 1 enemy gives 3 Kingdom Deeds, and locking the enemy`s soul will bring you 1"
tServiceTask_PatrioticHero_Text[17400]["Text10013"] = "~1 more Deed. Reviving or unlocking a union-mate`s soul gives 1 Kingdom Deed. For teams, all the members share the kills."

tServiceTask_PatrioticHero_Text[17400]["Text10021"] = "If you want to go back to your own server, I can help. While for the affairs of this server, I have nothing to tell you."

-- 【玩家选接受任务, 失败, 玩家等级不足】
tServiceTask_PatrioticHero_Text[17400]["Text10111"] = "The undercover enemies from other servers are stronger than you imagined. You should reach at least Level 100 of the 1st rebirth"
tServiceTask_PatrioticHero_Text[17400]["Text10112"] = "~to take the mission."

-- 【玩家选接受任务, 失败, 当日已无完成次数】
tServiceTask_PatrioticHero_Text[17400]["Text10211"] = "You`ve completed the Kingdom Mission 3 times today. I suggest you to take a rest."

-- 【玩家选接受任务, 成功, 玩家接受任务】
tServiceTask_PatrioticHero_Text[17400]["Text10311"] = "Remember, you should enable the Kingdom Mode to attack the enemies from other servers. If you make a team for this mission,"
tServiceTask_PatrioticHero_Text[17400]["Text10312"] = "~all members will share the kills. Be careful!"

-- 选项
tServiceTask_PatrioticHero_Text[17400]["Option100"] = "Bloody~Fight~[Kingdom]."
tServiceTask_PatrioticHero_Text[17400]["Option101"] = "I`m~on~the~way."
tServiceTask_PatrioticHero_Text[17400]["Option105"] = "Alright."
tServiceTask_PatrioticHero_Text[17400]["Option106"] = "Okay."
tServiceTask_PatrioticHero_Text[17400]["Option109"] = "I`m~getting~ready."

tServiceTask_PatrioticHero_Text["Kill"] = "You killed an enemy from other Union, and received %d Kingdom Deed(s)!"
tServiceTask_PatrioticHero_Text["TeamKill"] = "Your teammate killed an enemy from other Union. You share the kill, and received Kingdom Deeds!"
tServiceTask_PatrioticHero_Text["SaveUser"] = "You successfully revived a hero, and received 3 Kingdom Deeds!"
tServiceTask_PatrioticHero_Text["KeepGhost"] = "You successfully used Soul Shackle on an enemy`s ghost, and received 10 Kingdom Deeds!"
tServiceTask_PatrioticHero_Text["ClearKeepGhost"] = "Your admirable mercy earned yourself 3 Kingdom Deeds!"

tServiceTask_PatrioticHero_Text["KeepGhost_TeamShare"] = "Your teammate successfully used Soul Shackle on an enemy`s ghost, and you shared 10 Kingdom Deeds!"
tServiceTask_PatrioticHero_Text["SaveUser_TeamShare"] = "Your teammate successfully revived a union-mate, and you shared 3 Kingdom Deeds!"
tServiceTask_PatrioticHero_Text["ClearKeepGhost_TeamShare"] = "Your teammate successfully freed a union-mate from Soul Shackle, and you shared 3 Kingdom Deeds!"


------------------------------------------------------------------------------------
--Name:			[征服][任务脚本]跨服任务搬金砖
--Purpose:		跨服任务搬金砖
--Creator: 		郑鋆
--Created:		2014/12/16
------------------------------------------------------------------------------------
---------------------------------------------------------- 文字提示
tServiceTask_MoveGold_Text = {}
tServiceTask_MoveGold_Text[17400] = {}
tServiceTask_MoveGold_Text[17400]["Option156"] = "Gold~Brick~Plunder~[Kingdom]."

-- 联盟建立指挥官
tServiceTask_MoveGold_Text[10003] = {}

tServiceTask_MoveGold_Text[10003]["111"] = "A great war which involves millions of heroes is about to fire. If you have a Level 9+ Guild ranking Top 8 in Capture the Flag of your server,"
tServiceTask_MoveGold_Text[10003]["112"] = "~you can pay me 3,000 Guild CPs to create a Union. It`s time to gather the elites and picture a new world in your hands."
tServiceTask_MoveGold_Text[10003]["Option1"] = "Create~a~Union."
tServiceTask_MoveGold_Text[10003]["Option6"] = "Change~Union`s~Name."
tServiceTask_MoveGold_Text[10003]["Option2"] = "It`s~time~to~strike!"

tServiceTask_MoveGold_Text[10003]["211"] = "I can`t help you create a Union since you`re not a Guild leader."
tServiceTask_MoveGold_Text[10003]["Option3"] = "Alright."

tServiceTask_MoveGold_Text[10003]["221"] = "Sorry, you don`t have 3,000 Guild CPs to create a Union."
tServiceTask_MoveGold_Text[10003]["Option4"] = "Hard~times..."

tServiceTask_MoveGold_Text[10003]["231"] = "Wait, you`ve already joined a Union. You can`t create your own Union now."
tServiceTask_MoveGold_Text[10003]["Option5"] = "I~see."

-- 系统提示
tServiceTask_MoveGold_Text["MoveSuccess"] = "You`ve obtained a Gold Brick. Hurry and give it to the Kingdom Mission Envoy!"
tServiceTask_MoveGold_Text["MoveFail"] = "The plunder was interrupted. Please click the target Treasury to continue."
tServiceTask_MoveGold_Text["Read"] = "Looting"
tServiceTask_MoveGold_Text["NoGold"] = "This Treasury is empty."
tServiceTask_MoveGold_Text["MoveGoldSuccess"] = "You successfully submitted 1 Gold Brick!"
tServiceTask_MoveGold_Text["MoveGoldFail"] = "Failed to submit the Gold Brick."
tServiceTask_MoveGold_Text["NoLevel"] = "Make sure you`ve reached Level 110 of the 1st rebirth to loot the Gold Bricks."
tServiceTask_MoveGold_Text["NoAlliance"] = "After joining a Union, you can loot Gold Bricks from this Kingdom`s Treasury during the Plunder War."
tServiceTask_MoveGold_Text["NoPredatoryWar"] = "You can`t loot Gold Bricks now. The Plunder War hasn`t started."
tServiceTask_MoveGold_Text["TheAU"] = "Gold Brick Reserve: %d. You can`t loot the Gold Bricks since you`re serving this Union."
tServiceTask_MoveGold_Text["NoAU"] = "There is no reigning Union (Kingdom) in this server."
tServiceTask_MoveGold_Text["NoLeague"] = "You haven`t joined a Union."
tServiceTask_MoveGold_Text["DynamicAlliance"] = "Failed to change the Union`s name. You`re not the Union Leader."
tServiceTask_MoveGold_Text["ItemNumFull"] = "You have collected 100 Broadcast Horns. You need to spend some, first."
tServiceTask_MoveGold_Text["RewardItem"] = "You received %d Broadcast Horns!"
tServiceTask_MoveGold_Text["NoSpace1"] = "Your inventory is full. Please make some room, and login again to receive the Broadcast Horns."
tServiceTask_MoveGold_Text["NoGangRank"] = "Failed to create a Union. Your Guild hasn`t reached Level 9."
tServiceTask_MoveGold_Text["NoStandardTournam"] = "Failed to create a Union. Your Union didn`t advance into Top 8 in the Capture the Flag."
tServiceTask_MoveGold_Text["NoRange"] = "You should get closer to the Treasury to loot the Gold Bricks."

tServiceTask_MoveGold_Text["Success"] = "You received %d Kingdom Deeds!"
tServiceTask_MoveGold_Text["NoSpace"] = "Failed to open the pack. Your inventory is full."
tServiceTask_MoveGold_Text["OpenPackage"] = "You received %d Protection Pills and %d Favored Training Pills (B)!"
tServiceTask_MoveGold_Text["OpenSalaryPackage"] = "You received %d Chi Points, %d Horse Racing Points and %d Study Points!"

tServiceTask_MoveGold_Text["RewardStrengthValue"] = "You received %d Chi Points!"
tServiceTask_MoveGold_Text["RewardTrainPackage"] = "You received %s!"
tServiceTask_MoveGold_Text["HaveJLZ"] = "Failed to plunder. This Treasury is under the protection of Dragon Shield for 3 minutes."

tServiceTask_MoveGold_Text[3007259] = {}
tServiceTask_MoveGold_Text[3007259]["111"] = "This pack is exclusive for the Emperor to claim 2,000 Chi Points or an Emperor Supply Pack which contains 10 Protection Pills, 10 Favored Training Pills and 1,500 Chi Points. Which one do you like?"
tServiceTask_MoveGold_Text[3007259]["Option1"] = "Emperor~Supply~Pack."
tServiceTask_MoveGold_Text[3007259]["Option2"] = "2000~Chi~Points."

tServiceTask_MoveGold_Text[3007260] = {}
tServiceTask_MoveGold_Text[3007260]["111"] = "This pack is for the Empress, Marshals or Premiers to claim 1,600 Chi Pts or a Royal Supply Pack which contains 8 Protection Pills, 8 Favored Training Pills and 1,000 Chi Pts. Which one do you like?"
tServiceTask_MoveGold_Text[3007260]["Option1"] = "Royal~Supply~Pack"
tServiceTask_MoveGold_Text[3007260]["Option2"] = "1600~Chi~Points."

tServiceTask_MoveGold_Text[3007261] = {}
tServiceTask_MoveGold_Text[3007261]["111"] = "This pack is for the Four Generals and Concubines to claim 1,000 Chi Pts or a Nobles Supply Pack which contains 5 Protection Pills, 5 Favored Training Pills and 700 Chi Pts. Which one do you like?"
tServiceTask_MoveGold_Text[3007261]["Option1"] = "Nobles~Supply~Pack."
tServiceTask_MoveGold_Text[3007261]["Option2"] = "1000~Chi~Points."

tServiceTask_MoveGold_Text[3007262] = {}
tServiceTask_MoveGold_Text[3007262]["111"] = "This pack is for the Imperial Guards to claim 600 Chi Points or a Guard Supply Pack which contains 2 Protection Pills, 3 Favored Training Pills and 500 Chi Pts. Which one do you like?"
tServiceTask_MoveGold_Text[3007262]["Option1"] = "Guard~Supply~Pack."
tServiceTask_MoveGold_Text[3007262]["Option2"] = "600~Chi~Points."

tServiceTask_MoveGold_Text[3007263] = {}
tServiceTask_MoveGold_Text[3007263]["111"] = "This pack is for the Senior Concubines to claim 1,200 Chi Points or a Senior Concubine Supply Pack which contains 6 Protection Pills, 6 Favored Training Pills and 800 Chi Pts. Which one do you like?"
tServiceTask_MoveGold_Text[3007263]["Option1"] = "Senior~Concubine~Supply~Pack."
tServiceTask_MoveGold_Text[3007263]["Option2"] = "1200~Chi~Points."

tServiceTask_MoveGold_Text["Quality"] = {}
tServiceTask_MoveGold_Text["Quality"][0] = "You successfully looted 1 normal Gold Brick! You can submit it to the Kingdom Mission Envoy to gain 50 Kingdom Deeds."
tServiceTask_MoveGold_Text["Quality"][1] = "You successfully looted 1 Refined Gold Brick! You can submit it to the Kingdom Mission Envoy to gain 70 Kingdom Deeds."
tServiceTask_MoveGold_Text["Quality"][2] = "You successfully looted 1 Unique Gold Brick! You can submit it to the Kingdom Mission Envoy to gain 100 Kingdom Deeds."
tServiceTask_MoveGold_Text["Quality"][3] = "You successfully looted 1 Elite Gold Brick! You can submit it to the Kingdom Mission Envoy to gain 150 Kingdom Deeds."
tServiceTask_MoveGold_Text["Quality"][4] = "You successfully looted 1 Super Gold Brick! You can submit it to the Kingdom Mission Envoy to gain 200 Kingdom Deeds."

------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]国战任务优化
--Purpose:	国战任务优化
--Creator: 	丁晨
--Created:	2015/07/26
------------------------------------------------------------------------------------
tInterServeTask_Killer_Text ={}
tInterServeTask_Killer_Text["KillMonster"] = "You`ve received %s Strike Points by killing enemies fiercely. When you earn 300 Strike Points, you can claim a reward from the Kingdom Mission Envoy."
tInterServeTask_Killer_Text["KillMonsterBoss"] = "Excellent! You killed the Thunder Dragon! You deserve the title of hero."
tInterServeTask_Killer_Text["KillMonsterBoss1"] = "%s killed the Thunder Dragon, and successfully proved himself/herself above and beyond!"
tInterServeTask_Killer_Text["Killer"] = "You`ve received %s Strike Points by killing enemies fiercely. When you earn 300 Strike Points, you can claim a reward from the Kingdom Mission Envoy."
tInterServeTask_Killer_Text["KillerMax"] = "You`ve earned 300 Strike Points by killing enemies fiercely. Go claim your reward from the Kingdom Mission Envoy!"
tInterServeTask_Killer_Text["MonsterName"] ={}
tInterServeTask_Killer_Text["MonsterName"][7854] ="WarriorofRage"
tInterServeTask_Killer_Text["MonsterName"][7855] ="FrostBeast"
tInterServeTask_Killer_Text["MonsterName"][7856] ="ViolentBeast"
tInterServeTask_Killer_Text["MonsterName"][7857] ="BloodyBeast"
tInterServeTask_Killer_Text["MonsterName"][7858] ="StormBeast"
tInterServeTask_Killer_Text["MonsterName"][7859] ="ThunderDragon"



tInterServeTask_Killer_Text[17400] = {}
--非本盟玩家
tInterServeTask_Killer_Text[17400]["Text111"] = "If you want to go back to your own server, I can help. While for the affairs of this server, I have nothing to tell you."
tInterServeTask_Killer_Text[17400]["Text112"] = ""
tInterServeTask_Killer_Text[17400]["Option1"] = "Sorry~to~bother~you."

--接受任务
tInterServeTask_Killer_Text[17400]["Text213"] = "The frontier is asking for reinforcement! The Warriors of Rage and the enemies from other servers are invading the Realm. Go sweep the evils off the Realm!"
tInterServeTask_Killer_Text[17400]["Text214"] = "~Killing every enemy brings you some Strike Points. When you earn 300 Strike Points, you can claim rewards like CPs (B), Favored Training Pill, Broadcast Horns, and 50 Kingdom Deeds."
tInterServeTask_Killer_Text[17400]["Option2"] = "Strike~now!"
tInterServeTask_Killer_Text[17400]["Option3"] = "Claim~my~reward."
--tInterServeTask_Killer_Text[17400]["Option4"] = "What`re~the~exchange~rules?"
tInterServeTask_Killer_Text[17400]["Option5"] = "I`m~not~ready,~yet."

--等级不足
tInterServeTask_Killer_Text[17400]["Text115"] = "I do appreciate your bravery, but you`re still too green. You should reach at least Level 110 of the 1st rebirth to enter the Realm."
tInterServeTask_Killer_Text[17400]["Text116"] = ""
tInterServeTask_Killer_Text[17400]["Option6"] = "Alright."


tInterServeTask_Killer_Text[17400]["Text117"] = "You should travel to the Realm, and earn 300 Strike Points by killing Warriors of Rage and the enemies from other servers."
tInterServeTask_Killer_Text[17400]["Option7"] = "I~see."


--兑换成功
tInterServeTask_Killer_Text[17400]["Text138"] = "Good job! Thanks for your help! Here`s the reward I promised."
tInterServeTask_Killer_Text[17400]["Text139"] = ""
tInterServeTask_Killer_Text[17400]["Option27"] = "It`s~my~pleasure!"

--天石上限已满
tInterServeTask_Killer_Text[17400]["Text140"] = "Hey, you`re carrying the maximum amount of CPs (B). You need to spend some, first."
tInterServeTask_Killer_Text[17400]["Option28"] = "Okay."

--未兑换成功
-- tInterServeTask_Killer_Text[17400]["Text127"] = "    该种积分阁下当前并未累积满%s点，无法兑换奖励，还请累积达到时再来。"
-- tInterServeTask_Killer_Text[17400]["Text128"] = "一切请多加小心！"
-- tInterServeTask_Killer_Text[17400]["Option16"] = "好的，不必担心！"

tInterServeTask_Killer_Text[17400]["Text118"] ="No. 1: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text119"] ="No. 2: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text120"] ="No. 3: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text121"] ="No. 4: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text122"] ="No. 5: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text123"] ="No. 6: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text124"] ="No. 7: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text125"] ="No. 8: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text126"] ="No. 9: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text127"] ="No. 10: %s , Monsters Killed: %s\n"
tInterServeTask_Killer_Text[17400]["Text128"] ="This is a daily leaderboard that heroes are ranked according to the number of monsters they killed in the Realm everyday:\n"
tInterServeTask_Killer_Text[17400]["Text129"] ="This is a general leaderboard that heroes are ranked according to the total number of monsters they`ve killed in the Realm:\n"

tInterServeTask_Killer_Text["NoPlayer"] ="None"
--可接任务
-- tInterServeTask_Killer_Text[17400]["Text117"] = "    还请阁下通过跨服进入国境服，击杀某种敌寇或其它联盟贼人，获得一定积分后回来找\n"
-- tInterServeTask_Killer_Text[17400]["Text118"] = "①	击杀【混沌气力兽】100只可获100点气力积分，气力积分用于兑换气力值；\n"
-- tInterServeTask_Killer_Text[17400]["Text119"] = "②	击杀【禁锢修为兽】100只可获100点修为积分，修为积分用于兑换 Potency Points；\n"
-- tInterServeTask_Killer_Text[17400]["Text120"] = "③	击杀【雷鸣强炼兽】100只可获100点强炼积分，强炼积分用于兑换强炼丹；\n"
-- tInterServeTask_Killer_Text[17400]["Text121"] = "④	击杀【血月聚神兽】10只可获10点聚神积分，聚神积分用于兑换聚神丹；\n"
-- tInterServeTask_Killer_Text[17400]["Text122"] = "⑤	击杀【咆哮通神兽】50只可获50点通神积分，通神积分用于兑换究极通神丹；\n"
-- tInterServeTask_Killer_Text[17400]["Text123"] = "⑥	击杀其他联盟贼人300人可获300点赤炼积分，赤炼积分用于兑换+3赤炼石。\n"

-- tInterServeTask_Killer_Text[17400]["Option7"] = "好的。"


--成功完成任务
-- tInterServeTask_Killer_Text[17400]["Text124"] = "    听闻阁下 War Exploits 卓绝，还请阁下选择想要兑换的奖励，我定如数献上: "
-- tInterServeTask_Killer_Text[17400]["Option8"] = "100点气力值（100点气力积分）。"
-- tInterServeTask_Killer_Text[17400]["Option9"] = "100点 Potency Points（100点修为积分）。"
-- tInterServeTask_Killer_Text[17400]["Option10"] = "5颗强炼丹（100点强炼积分）。"
-- tInterServeTask_Killer_Text[17400]["Option11"] = "1颗聚神丹（10点聚神积分）。"
-- tInterServeTask_Killer_Text[17400]["Option12"] = "1颗究极通神丹（50点通神积分）。"
-- tInterServeTask_Killer_Text[17400]["Option13"] = "1颗+3赤炼石（300点赤炼积分）。"
-- tInterServeTask_Killer_Text[17400]["Option14"] = "暂不兑换。"

--兑换成功
-- tInterServeTask_Killer_Text[17400]["Text125"] = "    大侠肝胆狭义，有勇有谋，这么快就获得如数积分，我们的盛世霸业少不了"
-- tInterServeTask_Killer_Text[17400]["Text126"] = "您的贡献！还请收下这一点报酬！"
-- tInterServeTask_Killer_Text[17400]["Option15"] = "边境对抗，人人有责！"

--未兑换成功
-- tInterServeTask_Killer_Text[17400]["Text127"] = "    该种积分阁下当前并未累积满%s点，无法兑换奖励，还请累积达到时再来。"
-- tInterServeTask_Killer_Text[17400]["Text128"] = "一切请多加小心！"
-- tInterServeTask_Killer_Text[17400]["Option16"] = "好的，不必担心！"

--今日已兑换过
tInterServeTask_Killer_Text[17400]["Text141"] = "You`ve already claimed the reward, today. If you have more points, you can swap them tomorrow."
tInterServeTask_Killer_Text[17400]["Option29"] = "Okay."


--
tInterServeTask_Killer_Text[17400]["Option400"] ="Evil~Invasion~[Realm]."
tInterServeTask_Killer_Text[17400]["Option401"] ="Doom~of~Beast~[Realm]."
tInterServeTask_Killer_Text[17400]["Option402"] ="Daily~Kill~Ranking~(Realm)."
tInterServeTask_Killer_Text[17400]["Option403"] ="General~Kill~Ranking~(Realm)."

--击杀妖龙
tInterServeTask_Killer_Text[17400]["Text130"] = "Thunder Dragon is breaking the peace in the Realm. This fearsome beast appears once every hour. If you`re able to kill it,"
tInterServeTask_Killer_Text[17400]["TextXXX"] = "~you`ll win wonderful rewards like random P6/P7 Dragon Soul, Sacred/Super Refinery material and awesome garment fragments."
tInterServeTask_Killer_Text[17400]["Option18"] ="I~won`t~let~you~down!"
tInterServeTask_Killer_Text[17400]["Option19"] ="I`ve~killed~the~dragon."
tInterServeTask_Killer_Text[17400]["Option20"] ="I`m~not~ready,~yet."

--失败等级不足
tInterServeTask_Killer_Text[17400]["Text131"] = "The Thunder Dragon is not to be trifled with. You should reach at least Level 110 of the 1st rebirth to challenge."
tInterServeTask_Killer_Text[17400]["Text132"] = ""
tInterServeTask_Killer_Text[17400]["Option21"] ="Alright."

--我已击杀蛟龙
tInterServeTask_Killer_Text[17400]["Text133"] = "You`re really experienced in this kind of hunting. Please take your rewards."
-- tInterServeTask_Killer_Text[17400]["Text132"] = ""
tInterServeTask_Killer_Text[17400]["Option22"] ="It`s~my~pleasure."

--未完成任务
tInterServeTask_Killer_Text[17400]["Text134"] = "You should killed the Thunder Dragon in the Realm before you can claim a reward from me."
tInterServeTask_Killer_Text[17400]["Option23"] ="I~see."

--接受成功
tInterServeTask_Killer_Text[17400]["Text135"] = "You look pretty good. Hope you`re able to retrieve peace in the Realm."
tInterServeTask_Killer_Text[17400]["Option24"] ="You~can~count~on~me."

--背包空间已满
tInterServeTask_Killer_Text[17400]["Text136"] = "Buddy, you`re carrying a full inventory. Why not make some room, first?"
tInterServeTask_Killer_Text[17400]["Option25"] ="I`ll~do~it~now."

--尚未杀满500只
tInterServeTask_Killer_Text[17400]["Text137"] = "You haven`t earned %s Strike Points. Warriors of Rage and the enemies from other servers are ruining the Realm. Hurry and go eliminate them!"
tInterServeTask_Killer_Text[17400]["Option26"] ="I~see."
tInterServeTask_Killer_Text[17400]["Option30"] ="Directly~complete~it.~(10~CPs)"

--天石不足
tInterServeTask_Killer_Text[17400]["Text145"] ="Sorry, you don`t have 10 CPs to directly complete this mission."
tInterServeTask_Killer_Text[17400]["Option31"] ="Okay."

tInterServeTask_Killer_Text["BagFullText"] ="Your inventory is too full to contain more things. Please make some room, first."
tInterServeTask_Killer_Text["GongXi"] ="You received a 7-day %s!"
--tInterServeTask_Killer_Text["GongX1i"] ="You received %s"
tInterServeTask_Killer_Text["Ts"] ="You received %s CPs (B)!"
tInterServeTask_Killer_Text["dxg"] ="You received %s 7StarOintment (B)!"
tInterServeTask_Killer_Text["xlb"] ="You received %s Free Training Pills (B)!"
tInterServeTask_Killer_Text["gzjf"] ="You received %s Kingdom Deeds!"
tInterServeTask_Killer_Text["qlcy"] ="You received %s Broadcast Horns (B)!"
tInterServeTask_Killer_Text["suij1"] ="You received Garment Fragment (B)!"
tInterServeTask_Killer_Text["suij2"] ="You received Mount Armor Fragment (B)!"
tInterServeTask_Killer_Text["suij3"] ="You received Weapon Accessory Fragment (B)!"

------------------------------------------------------------------------------------
--Name:			[征服][任务脚本]跨服任务开疆功臣
--Purpose:		跨服任务开疆功臣
--Creator: 		郑鋆
--Created:		2014/12/16
------------------------------------------------------------------------------------
-- 文字提示
tServiceTask_HisHero_Text = {}
tServiceTask_HisHero_Text[17400] = {}

-- 【玩家选1, 接受任务】
tServiceTask_HisHero_Text[17400]["Text20011"] = "To conquer the whole world, you should explore new lands. During the Plunder War from 21:00 to 21:30 every Wednesday, Thursday and Friday,"
tServiceTask_HisHero_Text[17400]["Text20012"] = "~heroes can enable Kingdom or Invasion Mode to attack other servers. Killing 1 fighter in other servers gives 3 Kingdom Deeds."
tServiceTask_HisHero_Text[17400]["Text20013"] = "~Reviving a hero, using or removing Soul Shackle give 1 Kingdom Deed. For teams, all the members share the kills. "
tServiceTask_HisHero_Text[17400]["Text20014"] = ""

tServiceTask_HisHero_Text[17400]["Text20021"] = "If you want to go back to your own server, I can help. While for the affairs of this server, I have nothing to tell you."

-- 【玩家选A-E, 失败, 玩家等级不足】
tServiceTask_HisHero_Text[17400]["Text20111"] = "You should reach at least Level 110 of the 1st rebirth to travel to other servers. Make yourself stronger!"
tServiceTask_HisHero_Text[17400]["Text20112"] = ""

-- 【玩家选A-E, 失败, 当日已无完成次数】
tServiceTask_HisHero_Text[17400]["Text20211"] = "You`ve completed the Kingdom Mission 3 times today. I suggest you to take a rest."

-- 【玩家选A-E, 成功, 玩家接受任务】
tServiceTask_HisHero_Text[17400]["Text20311"] = "To attack fighters in other servers, you should enable Kingdom or Invasion Mode."
tServiceTask_HisHero_Text[17400]["Text20312"] = "~If you make a team for this mission, all members will share the kills. Good luck!"

-- 选项
tServiceTask_HisHero_Text[17400]["Option200"] = "The~Pioneers~[Kingdom]."

tServiceTask_HisHero_Text[17400]["Option201"] = "Count~on~me!"
tServiceTask_HisHero_Text[17400]["Option205"] = "That`s~fine."
tServiceTask_HisHero_Text[17400]["Option206"] = "Okay."
tServiceTask_HisHero_Text[17400]["Option209"] = "I`m~not~ready,~yet."
tServiceTask_HisHero_Text[17400]["Option211"] = "Sorry~to~bother~you."

tServiceTask_HisHero_Text["Kill"] = "You successfully killed a fighter in other server, and received %d Kingdom Deeds!"
tServiceTask_HisHero_Text["TeamKill"] = "Your teammate killed a fighter in other server. You share the kill, and received %d Kingdom Deed(s)!"
tServiceTask_HisHero_Text["SaveUser"] = "You revived a hero, and received 1 Kingdom Deed!"
tServiceTask_HisHero_Text["KeepGhost"] = "You successfully used Soul Shackle on a fighter`s ghost, and received 1 Kingdom Deed!"
tServiceTask_HisHero_Text["ClearKeepGhost"] = "You removed the Soul Shackle from a fighter, and received 1 Kingdom Deed!"

------------------------------------------------------------------------------------
--Name:			[简体征服][任务脚本]国境晶矿
--Purpose:		[简体征服][任务脚本]国境晶矿
--Creator: 		张磊
--Created:		26/07/2015
------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
--文字提示
tServiceTask_Crystal_Text = {}
tServiceTask_Crystal_Text[17400] = {}
-- 等级不够对白
tServiceTask_Crystal_Text[17400]["Text30011"] = "The Realm is too dangerous for you. If you really want to help, keep practice and come back when you reach Level 110 of the 1st rebirth."
tServiceTask_Crystal_Text[17400]["Text30012"] = ""

tServiceTask_Crystal_Text[17400]["Option300"] = "Alright."

-- 没有任务提示
tServiceTask_Crystal_Text[17400]["Text30021"] = "You should take this mission, first."
tServiceTask_Crystal_Text[17400]["Text30022"] = "Failed to claim the reward."

-- 有任务但是data1 大水晶
tServiceTask_Crystal_Text[17400]["Text30031"] = "You should bring me the Big Magic Ores to claim a reward."
tServiceTask_Crystal_Text[17400]["Text30032"] = ""

-- 有任务但是data2 小水晶
tServiceTask_Crystal_Text[17400]["Text30041"] = "You should bring me the Small Magic Ores to claim a reward."
tServiceTask_Crystal_Text[17400]["Text30042"] = ""

------接任务部分
-- 已有任务 提示
tServiceTask_Crystal_Text[17400]["Text30051"] = "You`ve accepted this mission."
tServiceTask_Crystal_Text[17400]["Text30052"] = "You can`t take this mission repeatedly."

-- 接取任务成功提示 提示
tServiceTask_Crystal_Text[17400]["Text30061"] = "As you know, we`re in urgent need of Small Magic Ores and Big Magic Ores. You can probably collect some in the Realm."
tServiceTask_Crystal_Text[17400]["Text30062"] = "~I await your good news."

tServiceTask_Crystal_Text[17400]["Option307"] = "Count~on~me!"


tServiceTask_Crystal_Text[17400]["Option301"] = "Realm~Ore~[Realm]."

tServiceTask_Crystal_Text[17400]["Text30071"] = "Ore resources in the Realm are abundant. You can collect Small or Big Magic Ores, and swap them with Kingdom Mission Envoy for rewards like CPs (B), Free Training Pills, Protection Pills,"
tServiceTask_Crystal_Text[17400]["Text30072"] = "~Chi Points and Senior Training Pill. The envoy gives a better reward to big ores.\nYou can swap big ores for [%d] times.\nYou can swap small ores for [%d] times."

tServiceTask_Crystal_Text[17400]["Option302"] = "I`m~in!"

tServiceTask_Crystal_Text[17400]["Option304"] = "I`m~not~ready,~yet."

tServiceTask_Crystal_Text[17400]["Text30081"] = "Which kind of Magic Ores would you like to exchange for rewards?"
tServiceTask_Crystal_Text[17400]["Text30082"] = ""

tServiceTask_Crystal_Text[17400]["Option305"] = "Big~Magic~Ores."
tServiceTask_Crystal_Text[17400]["Option306"] = "Small~Magic~Ores."


--系统提示
tServiceTask_Crystal_Text["DamageCrystal_Small"] = "You received a Small Magic Ore!"
tServiceTask_Crystal_Text["DamageCrystal_Big"] = "You received a Big Magic Ore!"
tServiceTask_Crystal_Text["NoSpace"] = "Please clear at least 2 empty spaces in your inventory, first."
tServiceTask_Crystal_Text["MoreEmoney"] = "You`re carrying the maximum amount of CPs (B). Please spend some, first."
tServiceTask_Crystal_Text["Get_JF"] = "You received %d Kingdom Deed！"
tServiceTask_Crystal_Text["Sys_YDJF"] = "You`ve received %s Strike Points by killing enemies fiercely. When you earn 300 Strike Points, you can claim a reward from the Kingdom Mission Envoy."

tServiceTask_Crystal_GetReward = {}
--大水晶的领奖提示
tServiceTask_Crystal_GetReward[1] = {}
tServiceTask_Crystal_GetReward[1]["1-1"] = "You received an Endeavor Scroll (B)!"
tServiceTask_Crystal_GetReward[1]["1-2"] = "You received 200 Chi Points!"

tServiceTask_Crystal_GetReward[1]["2-1"] = "You received 100 CPs (B)!"
tServiceTask_Crystal_GetReward[1]["2-2"] = "You received 200 Chi Points!"
tServiceTask_Crystal_GetReward[1][3] = "You received a Justice Scroll (B)!"
tServiceTask_Crystal_GetReward[1]["4-1"] = "You received 200 Chi Points!"
tServiceTask_Crystal_GetReward[1][5] = "You received 3 Senior Training Pills (B)!"

tServiceTask_Crystal_GetReward[1]["6-1"] = "You received a Strength Token!"
tServiceTask_Crystal_GetReward[1]["6-2"] = "You received a Spirit Token!"
tServiceTask_Crystal_GetReward[1]["6-3"] = "You received a Vitality Token!"
tServiceTask_Crystal_GetReward[1]["6-4"] = "You received an Agility Token!"

tServiceTask_Crystal_GetReward[1][7] = "You received 10 Free Training Pills (B)!"


--小水晶的领奖提示
tServiceTask_Crystal_GetReward[2] = {}
tServiceTask_Crystal_GetReward[2]["1-1"] = "You received an Endeavor Scroll (B)!"
tServiceTask_Crystal_GetReward[2]["1-2"] = "You received 50 Chi Points!"

tServiceTask_Crystal_GetReward[2]["2-1"] = "You received 2 Free Courses and 1 Broadcast Horn (B)!"
tServiceTask_Crystal_GetReward[2]["2-2"] = "You received 50 Chi Points!"

tServiceTask_Crystal_GetReward[2]["3-1"] = "You received 2 Protection Pills!"
tServiceTask_Crystal_GetReward[2]["3-2"] = "You received an EXP Ball (B)!"

tServiceTask_Crystal_GetReward[2]["4-1"] = "You received 50 Chi Points and a Broadcast Horn (B)!"
tServiceTask_Crystal_GetReward[2][5] = "You received a Free Training Pill (B)!"
tServiceTask_Crystal_GetReward[2][6] = "You received a Favored Training Pill (B)!"
tServiceTask_Crystal_GetReward[2][7] = "You received a Special Training Pill (B)!"
tServiceTask_Crystal_GetReward[2][8] = "You received a piece of Frozen Chi Pill Scrap!"

tBackpackLetter_Text[727505] = {}
tBackpackLetter_Text[727505]["NoSpace"] = "Your inventory is full. Please make some room, and login again to receive a Kingdom Mission Token which will help you win tons of rewards."
tBackpackLetter_Text[727505]["RewardItem"] = "The horn of Kingdom War is blowing! The Kingdom Mission Token will guide you in the Kingdom Mission and help you win abundant rewards."

------------------------------------------------------------------------------------
--Name:		[征服][任务脚本]添加黄金联赛积分
--Purpose:	添加黄金联赛积分
--Creator: 	郑鋆
--Created:	2015/08/11
------------------------------------------------------------------------------------
tInternalTaskCheats_Text = {}
tInternalTaskCheats_Text["Top"] = "Your Champion Points have reached the maximum amount of %d points. You can collect more points tomorrow."
tInternalTaskCheats_Text["NoGongFu"] = "You should become a Jiang Hu fighter before you can open this pack."
tInternalTaskCheats_Text["GolderLeaguePoint"] = "You received %d Champion Points!"

tInternalTaskCheats_Text["ArenicCompetes"] = "You competed in the Qualifier, and received an Arena Pack!"
tInternalTaskCheats_Text["ArenicWins"] = "You won a fight in the Qualifier, and received an Arena Pack!"
tInternalTaskCheats_Text["ArenicTeamCompetes"] = "You competed in the Team Qualifier, and received a Team Arena Pack!"
tInternalTaskCheats_Text["ArenicTeamWins"] = "You won a fight in the Team Qualifier, and received a Team Arena Pack!"

tInternalTaskCheats_Text[3007285] = {}
tInternalTaskCheats_Text[3007285]["Space"] = "Please make at least 14 empty spaces in your inventory, first."
tInternalTaskCheats_Text[3007285]["Reward"] = "You received 15 Protection Pills!"

tInternalTaskCheats_Text[3007284] = {}
tInternalTaskCheats_Text[3007284]["FreePractice"] = "Your Free Courses have reached the upper limit of 90. You need to consume some, first."
tInternalTaskCheats_Text[3007284]["Reward"] = "You received 10 Free Courses for Jiang Hu training!"

tInternalTaskCheats_Text[3007286] = {}
tInternalTaskCheats_Text[3007286]["Space"] = "Please make at least 3 empty spaces in your inventory, first."
tInternalTaskCheats_Text[3007286]["Reward"] = "You received 20 7Star Ointments (B)!"

tInternalTaskCheats_Text[3007283] = {}
tInternalTaskCheats_Text[3007283]["111"] = "You can claim one from the three +3 Steeds (B) in this pack. Which one do you like?"
tInternalTaskCheats_Text[3007283]["Option1"] = "+3~White~Steed~(B)."
tInternalTaskCheats_Text[3007283]["Option2"] = "+3~Black~Steed~(B)."
tInternalTaskCheats_Text[3007283]["Option3"] = "+3~Maroon~Steed~(B)."

tInternalTaskCheats_Text[3007283]["211"] = "Are you sure you want to claim a +3 White Steed (B) from this pack?"
tInternalTaskCheats_Text[3007283]["Option4"] = "Yes."
tInternalTaskCheats_Text[3007283]["Option5"] = "No."
   
tInternalTaskCheats_Text[3007283]["221"] = "Are you sure you want to claim a +3 Black Steed (B) from this pack?"
tInternalTaskCheats_Text[3007283]["Option6"] = "Yes."
tInternalTaskCheats_Text[3007283]["Option7"] = "No."

tInternalTaskCheats_Text[3007283]["231"] = "Are you sure you want to claim a +3 Maroon Steed (B) from this pack?"
tInternalTaskCheats_Text[3007283]["Option8"] = "Yes."
tInternalTaskCheats_Text[3007283]["Option9"] = "No."

tInternalTaskCheats_Text[3007283][1] = "You received +3 White Steed (B)!"
tInternalTaskCheats_Text[3007283][2] = "You received +3 Black Steed (B)!"
tInternalTaskCheats_Text[3007283][3] = "You received +3 Maroon Steed (B)!"

tInternalTaskCheats_Text[3007287] = {}
tInternalTaskCheats_Text[3007287]["111"] = "You can claim one from the three +1 Steeds (B) in this pack. Which one do you like?"
tInternalTaskCheats_Text[3007287]["Option1"] = "+1~White~Steed~(B)."
tInternalTaskCheats_Text[3007287]["Option2"] = "+1~Black~Steed~(B)."
tInternalTaskCheats_Text[3007287]["Option3"] = "+1~Maroon~Steed~(B)."

tInternalTaskCheats_Text[3007287]["211"] = "Are you sure you want to claim a +1 White Steed (B) from this pack?"
tInternalTaskCheats_Text[3007287]["Option4"] = "Yes."
tInternalTaskCheats_Text[3007287]["Option5"] = "No."
   
tInternalTaskCheats_Text[3007287]["221"] = "Are you sure you want to claim a +1 Black Steed (B) from this pack?"
tInternalTaskCheats_Text[3007287]["Option6"] = "Yes."
tInternalTaskCheats_Text[3007287]["Option7"] = "No."

tInternalTaskCheats_Text[3007287]["231"] = "Are you sure you want to claim a +1 Maroon Steed (B) from this pack?"
tInternalTaskCheats_Text[3007287]["Option8"] = "Yes."
tInternalTaskCheats_Text[3007287]["Option9"] = "No."

tInternalTaskCheats_Text[3007287][4] = "You received +1 White Steed (B)!"
tInternalTaskCheats_Text[3007287][5] = "You received +1 Black Steed (B)!"
tInternalTaskCheats_Text[3007287][6] = "You received +1 Maroon Steed (B)!"

tInternalTaskCheats_Text[3007434] = {}
tInternalTaskCheats_Text[3007434]["Space"] = "Please clear at least 3 empty spaces in your inventory, first."
tInternalTaskCheats_Text[3007434]["Reward"] = "You received 20 Serenity Pills (B)!"

----------------------------------------------------------------------------
--Name:		[征服][功能脚本]内功系统数据.lua
--Purpose:	内功功能
--Creator: 	郑鋆
--Created:	2014/11/21
----------------------------------------------------------------------------
---------------------------------命名规范----------------------------------
tInternalSystem_Msg = {}
tInternalSystem_Msg["NoItems"] = "Please make sure you have this item with you."
tInternalSystem_Msg["Learn"] = "You`ve already acquired this Inner Power concept."
tInternalSystem_Msg["NotLearn"] = "You should complete the study of the primary Inner Power, first."
tInternalSystem_Msg["NotVlue"] = "Your Inner Power Score is not high enough to acquire this Inner Power concept."
tInternalSystem_Msg["Condition"] = "You haven`t met the requirement for this Inner Power."
tInternalSystem_Msg["Study"] = "You successfully acquired %s!"
tInternalSystem_Msg["CultureValue"] = "Your Potency has reached the maximum."
tInternalSystem_Msg["AddCultureValue"] = "You received %d Potency Points!"

tInternalSystem_InnerName = {}
tInternalSystem_InnerName[1] = "Universal Concept (A)"
tInternalSystem_InnerName[2] = "Universal Concept (B)"
tInternalSystem_InnerName[3] = "Secret of Breath (A)"
tInternalSystem_InnerName[4] = "Secret of Breath (B)"
tInternalSystem_InnerName[5] = "Demon Rider (A)"
tInternalSystem_InnerName[6] = "Demon Rider (B)"
tInternalSystem_InnerName[7] = "Dragon Tactics (A)"
tInternalSystem_InnerName[8] = "Dragon Tactics (B)"
tInternalSystem_InnerName[9] = "Boundless Heart (A)"
tInternalSystem_InnerName[10] = "Boundless Heart (B)"
tInternalSystem_InnerName[11] = "Boundless Heart (C)"
tInternalSystem_InnerName[12] = "Doctrine of Deity (A)"
tInternalSystem_InnerName[13] = "Doctrine of Deity (B)"
tInternalSystem_InnerName[14] = "Doctrine of Deity (C)"
tInternalSystem_InnerName[15] = "Puzzle of Life (A)"
tInternalSystem_InnerName[16] = "Puzzle of Life (B)"
tInternalSystem_InnerName[17] = "Puzzle of Life (C)"
------------------------------------------------------------------------------------
--Name:		151010[英文征服][任务脚本]付费移服判断增加
--Purpose:		付费移服判断增加
--Creator:		许乐
--Created:		2015/10/10
------------------------------------------------------------------------------------
tServerTransferOfficer_Text = {}
	tServerTransferOfficer_Text[15702] = {}
	tServerTransferOfficer_Text[15702]["Text111"] = "Guys, I`m responsible for transferring servers. If you don`t wanna stay here anymore, "
	tServerTransferOfficer_Text[15702]["Text112"] = "I can help anytime except 07:00 - 09:30 and 15:00 - 17:30. Anything?"
	tServerTransferOfficer_Text[15702]["Option1"] = "Yeah,~tell~me~more~about~it."
	tServerTransferOfficer_Text[15702]["Option2"] = "I~wanna~transfer~now."
	tServerTransferOfficer_Text[15702]["Option3"] = "Nothing."

	tServerTransferOfficer_Text[15702]["Text211"] = "I like curious people, seriously. There`re something you need to do before transferring:\n"
	tServerTransferOfficer_Text[15702]["Text212"] = "1. Cancel all your relationships with this server, including guild, clan, mentor, trade partner and marriage.\n"
	tServerTransferOfficer_Text[15702]["Text213"] = "2. Make sure you aren`t detaining other players` equipment, or your equipment is being detained by others.\n"
	tServerTransferOfficer_Text[15702]["Text214"] = "3. Retrieve all items from your itemboxes in your house.\n"
	tServerTransferOfficer_Text[15702]["Text215"] = "4. Claim all your TQ Point Cards. \n5. View all your unread messages. \n6. Claim your mentor exp.\n"
	tServerTransferOfficer_Text[15702]["Text216"] = "After that, give me enough CPs, done!" ---1999  3999
	tServerTransferOfficer_Text[15702]["Option4"] = "I~got~it."

	tServerTransferOfficer_Text[15702]["Text311"] = "Now select a server group."
	tServerTransferOfficer_Text[15702]["Option5"] = "Wild~Kingdom"
	tServerTransferOfficer_Text[15702]["Option6"] = "Gem~World"
	tServerTransferOfficer_Text[15702]["Option7"] = "Dreams"
	tServerTransferOfficer_Text[15702]["Option8"] = "Nature"
	tServerTransferOfficer_Text[15702]["Option9"] = "Galaxy"
	tServerTransferOfficer_Text[15702]["Option10"] = "Wonders"
	tServerTransferOfficer_Text[15702]["Option11"] = "next"

	tServerTransferOfficer_Text[15702]["Text321"] = "Now select a server group."
	tServerTransferOfficer_Text[15702]["Option12"] = "Celebrities"
	tServerTransferOfficer_Text[15702]["Option13"] = "Constellations"
	tServerTransferOfficer_Text[15702]["Option14"] = "FairyTales"
	tServerTransferOfficer_Text[15702]["Option15"] = "Myths"
	tServerTransferOfficer_Text[15702]["Option16"] = "Elements"
	tServerTransferOfficer_Text[15702]["Option17"] = "Celebrations"
	tServerTransferOfficer_Text[15702]["Option21"] = "next"
	
	tServerTransferOfficer_Text[15702]["Text331"] = "Now select a server group."
	tServerTransferOfficer_Text[15702]["Option18"] = "CelebrationsII"
	tServerTransferOfficer_Text[15702]["Option19"] = "BattleArts"
	tServerTransferOfficer_Text[15702]["Option20"] = "Classic"
	tServerTransferOfficer_Text[15702]["Option22"] = "Columbus"   --对白不同的服务器
	
	tServerTransferOfficer_Text[15702]["Text511"] = "Select a server to transfer."
	tServerTransferOfficer_Text[15702]["Option511"] = "Dragon"
	tServerTransferOfficer_Text[15702]["Option512"] = "Phoenix"
	tServerTransferOfficer_Text[15702]["Option513"] = "Lion"
	tServerTransferOfficer_Text[15702]["Option514"] = "Kylin"
	tServerTransferOfficer_Text[15702]["Option515"] = "Eagle"

	tServerTransferOfficer_Text[15702]["Option611"] = "Crystal"
	tServerTransferOfficer_Text[15702]["Option612"] = "Emerald"
	tServerTransferOfficer_Text[15702]["Option613"] = "Turquoise"

	tServerTransferOfficer_Text[15702]["Option711"] = "Triumph"
	tServerTransferOfficer_Text[15702]["Option712"] = "Honor"
	tServerTransferOfficer_Text[15702]["Option713"] = "Freedom"
	tServerTransferOfficer_Text[15702]["Option714"] = "Faith"
	tServerTransferOfficer_Text[15702]["Option715"] = "Eternity"

	tServerTransferOfficer_Text[15702]["Option811"] = "Volcano"
	tServerTransferOfficer_Text[15702]["Option812"] = "Thunder"
	tServerTransferOfficer_Text[15702]["Option813"] = "Lightning"
	tServerTransferOfficer_Text[15702]["Option814"] = "Sunshine"
	tServerTransferOfficer_Text[15702]["Option815"] = "Snowfall"

	tServerTransferOfficer_Text[15702]["Option911"] = "Venus"
	tServerTransferOfficer_Text[15702]["Option912"] = "Mercury"
	tServerTransferOfficer_Text[15702]["Option913"] = "Uranus"
	tServerTransferOfficer_Text[15702]["Option914"] = "Neptune"

	tServerTransferOfficer_Text[15702]["Option1011"] = "HangingGardens"
	tServerTransferOfficer_Text[15702]["Option1012"] = "Mausoleum"
	tServerTransferOfficer_Text[15702]["Option1013"] = "Pyramid"
	--tServerTransferOfficer_Text[15702]["Option1014"] = "Pharos"

	tServerTransferOfficer_Text[15702]["Option1211"] = "Hebby"
	tServerTransferOfficer_Text[15702]["Option1212"] = "BabyIcey"

	tServerTransferOfficer_Text[15702]["Option1311"] = "Aquarius"
	tServerTransferOfficer_Text[15702]["Option1312"] = "Cancer"
	tServerTransferOfficer_Text[15702]["Option1313"] = "Virgo"
	tServerTransferOfficer_Text[15702]["Option1314"] = "Libra"
	tServerTransferOfficer_Text[15702]["Option1315"] = "Scorpio"

	tServerTransferOfficer_Text[15702]["Option1411"] = "SnowWhite"
	tServerTransferOfficer_Text[15702]["Option1412"] = "WildSwan"

	tServerTransferOfficer_Text[15702]["Option1511"] = "Gryphon"
	tServerTransferOfficer_Text[15702]["Option1512"] = "Titan"
	tServerTransferOfficer_Text[15702]["Option1513"] = "Basilisk"

	tServerTransferOfficer_Text[15702]["Option1611"] = "Dark"
	tServerTransferOfficer_Text[15702]["Option1612"] = "Light"
	tServerTransferOfficer_Text[15702]["Option1613"] = "Storm"
	tServerTransferOfficer_Text[15702]["Option1614"] = "Fire"

	tServerTransferOfficer_Text[15702]["Option1711"] = "Lucky7"
	tServerTransferOfficer_Text[15702]["Option1712"] = "Legends"

	tServerTransferOfficer_Text[15702]["Option1811"] = "Champions_EU"
	tServerTransferOfficer_Text[15702]["Option1812"] = "SummerWind"
	tServerTransferOfficer_Text[15702]["Option1813"] = "JiangHu_EU"
	tServerTransferOfficer_Text[15702]["Option1814"] = "DragonPunch"
	tServerTransferOfficer_Text[15702]["Option1815"] = "DragonRoar"

	tServerTransferOfficer_Text[15702]["Option1911"] = "BreathFocus"
	tServerTransferOfficer_Text[15702]["Option1912"] = "FastBlade"
	
	tServerTransferOfficer_Text[15702]["Option2011"] = "Royalty_EU_O"
	tServerTransferOfficer_Text[15702]["Option2012"] = "Liberty_O"
	
	--新加 npcid = 16399   id_action = 97197000
	tServerTransferOfficer_Text[15702]["Text2211"] = "Columbus server is region-locked, which means only players using a U.S. IP address are able to login to this server."
	tServerTransferOfficer_Text[15702]["Text2212"] = "~Are you sure you want to transfer to Columbus server?"
	tServerTransferOfficer_Text[15702]["Option23"] = "Yes."
	tServerTransferOfficer_Text[15702]["Option24"] = "No."

	--105提示
	tServerTransferOfficer_Text[105] = {}
	tServerTransferOfficer_Text[105]["30Days"] = "You~can~only~transfer~into~another~server~once~each~30~days."
	tServerTransferOfficer_Text[105]["SureToTrans"] = "Are~you~sure~you~want~to~transfer~to~%s~server?"  --%s  服务器名称
	tServerTransferOfficer_Text[105]["NotRightTime"] = "Server~Transfer~is~unavailable~during~07:00~-~09:30~and~15:00~-~17:30.~Please~try~again~later."
	tServerTransferOfficer_Text[105]["PasswordTip"] = "Make~sure~you~have~entered~a~correct~secondary~password~in~the~warehouse!"
	tServerTransferOfficer_Text[105]["FailConnect"] = "Failed~to~connect~to~the~Account~Server.~Please~try~again~later."
	tServerTransferOfficer_Text[105]["InSameServer"] = "You~are~already~in~this~server."
	tServerTransferOfficer_Text[105]["FailServer"] = "Failed~to~connect~to~the~server.~Please~try~again~later."
	tServerTransferOfficer_Text[105]["BusyServer"] = "Sever~is~busy.~Please~try~again~later."
	tServerTransferOfficer_Text[105]["NoEnoughEmoney"] = "You~don`t~have~%s~CPs."  --%s   1999  3999
	tServerTransferOfficer_Text[105]["Marriage"] = "Please~cancel~your~marriage~relationship~before~transferring."
	tServerTransferOfficer_Text[105]["Guild"] = "Please~cancel~your~guild~relationship~before~transferring."
	tServerTransferOfficer_Text[105]["Family"] = "Please~cancel~your~clan~relationship~before~transferring."
	tServerTransferOfficer_Text[105]["Mentor"] = "Please~cancel~your~mentor~relationship~before~transferring."
	tServerTransferOfficer_Text[105]["TradePartner"] = "Please~cancel~your~trade~partner~relationship~before~transferring."
	tServerTransferOfficer_Text[105]["Auction"] = "Please~retrieve~your~items~in~the~auction~center~before~transferring."
	tServerTransferOfficer_Text[105]["Email"] = "Please~view~all~your~unread~messages~and~delete~all~the~messages~before~transferring."
	tServerTransferOfficer_Text[105]["TargetPKItem"] = "Please~redeem~the~equipment~detained~by~other~players~or~wait~till~the~detaining~time~ends~before~transferring."
	tServerTransferOfficer_Text[105]["HunterPKItem"] = "Please~ask~players~to~redeem~the~equipment~detained~by~you~or~wait~till~the~detaining~time~ends~before~transferring."
	tServerTransferOfficer_Text[105]["ClaimEmoney"] = "Please~claim~all~your~TQ~Point~Cards~before~transferring."
	tServerTransferOfficer_Text[105]["League"] = "Please~quit~your~Union~before~transferring."

	tServerTransferOfficer_Text["ServerName"] = {}
	tServerTransferOfficer_Text["ServerName"][1] ="Dragon"
	tServerTransferOfficer_Text["ServerName"][2] = "Phoenix"
	tServerTransferOfficer_Text["ServerName"][3] = "Lion"
	tServerTransferOfficer_Text["ServerName"][4] = "Kylin"
	tServerTransferOfficer_Text["ServerName"][5] = "Eagle"
	tServerTransferOfficer_Text["ServerName"][6] ="Crystal"
	tServerTransferOfficer_Text["ServerName"][7] = "Emerald"
	tServerTransferOfficer_Text["ServerName"][8] = "Turquoise"
	tServerTransferOfficer_Text["ServerName"][9] ="Triumph"
	tServerTransferOfficer_Text["ServerName"][10] = "Honor"
	tServerTransferOfficer_Text["ServerName"][11] = "Freedom"
	tServerTransferOfficer_Text["ServerName"][12] = "Faith"
	tServerTransferOfficer_Text["ServerName"][13] = "Eternity"
	tServerTransferOfficer_Text["ServerName"][14] ="Volcano"
	tServerTransferOfficer_Text["ServerName"][15] = "Thunder"
	tServerTransferOfficer_Text["ServerName"][16] = "Lightning"
	tServerTransferOfficer_Text["ServerName"][17] = "Sunshine"
	tServerTransferOfficer_Text["ServerName"][18] = "Snowfall"
	tServerTransferOfficer_Text["ServerName"][19] ="Venus"
	tServerTransferOfficer_Text["ServerName"][20] = "Mercury"
	tServerTransferOfficer_Text["ServerName"][21] = "Uranus"
	tServerTransferOfficer_Text["ServerName"][22] = "Neptune"
	tServerTransferOfficer_Text["ServerName"][23] = "HangingGardens"
	tServerTransferOfficer_Text["ServerName"][24] = "Mausoleum"
	tServerTransferOfficer_Text["ServerName"][25] = "Pyramid"
	--tServerTransferOfficer_Text["ServerName"][26] = "Pharos"
	tServerTransferOfficer_Text["ServerName"][27] = "Hebby"
	tServerTransferOfficer_Text["ServerName"][28] = "BabyIcey"
	tServerTransferOfficer_Text["ServerName"][29] = "Aquarius"
	tServerTransferOfficer_Text["ServerName"][30] = "Cancer"
	tServerTransferOfficer_Text["ServerName"][31] = "Virgo"
	tServerTransferOfficer_Text["ServerName"][32] = "Libra"
	tServerTransferOfficer_Text["ServerName"][33] = "Scorpio"
	tServerTransferOfficer_Text["ServerName"][34] = "SnowWhite"
	tServerTransferOfficer_Text["ServerName"][35] = "WildSwan"
	tServerTransferOfficer_Text["ServerName"][36] = "Gryphon"
	tServerTransferOfficer_Text["ServerName"][37] = "Titan"
	tServerTransferOfficer_Text["ServerName"][38] = "Basilisk"
	tServerTransferOfficer_Text["ServerName"][39] = "Dark"
	tServerTransferOfficer_Text["ServerName"][40] = "Light"
	tServerTransferOfficer_Text["ServerName"][41] = "Storm"
	tServerTransferOfficer_Text["ServerName"][42] = "Fire"
	tServerTransferOfficer_Text["ServerName"][43] = "Lucky7"
	tServerTransferOfficer_Text["ServerName"][44] = "Legends"
	tServerTransferOfficer_Text["ServerName"][45] = "Champions_EU"
	tServerTransferOfficer_Text["ServerName"][46] = "SummerWind"
	tServerTransferOfficer_Text["ServerName"][47] = "JiangHu_EU"
	tServerTransferOfficer_Text["ServerName"][48] = "DragonPunch"
	tServerTransferOfficer_Text["ServerName"][49] = "DragonRoar"
	tServerTransferOfficer_Text["ServerName"][50] = "BreathFocus"
	tServerTransferOfficer_Text["ServerName"][51] = "Royalty_EU_O"
	tServerTransferOfficer_Text["ServerName"][52] = "Liberty_O"
	tServerTransferOfficer_Text["ServerName"][53] = "Columbus"
	tServerTransferOfficer_Text["ServerName"][54] = "FastBlade"

	--价格不同的服务器  --1999
	tServerTransferOfficer_Text["EMoney"] = {}
	tServerTransferOfficer_Text["EMoney"][1] = "DragonPunch"
	tServerTransferOfficer_Text["EMoney"][2] = "DragonRoar"
	tServerTransferOfficer_Text["EMoney"][3] = "BreathFocus"
	tServerTransferOfficer_Text["EMoney"][4] = "Royalty_EU_O"
	tServerTransferOfficer_Text["EMoney"][5] = "Liberty_O"
	
	--18873  ServerTransferOfficer   1002,284,289
	tServerTransferOfficer_Text[18873] = {}
	tServerTransferOfficer_Text[18873]["Text111"] = "Server transfer is not availalbe now. This paid service is expected to be restored on Oct. 17th."
	tServerTransferOfficer_Text[18873]["Option1"] = "Okay."

------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]内功秘籍任务制作
--Purpose:	内功秘籍任务制作
--Creator: 	郑鋆
--Created:	2015/08/07
------------------------------------------------------------------------------------
-- 中文文字表
tBackpackLetter_Text[3007294] = {}
tBackpackLetter_Text[3007294]["NoSpace"] = "Your inventory is full. Please make some room first, and login again to receive a Celestial Secret Card."
tBackpackLetter_Text[3007294]["RewardItem"] = "With a flash of light, you received a Celestial Secret Card. Hurry and check it in your inventory."

-- tInternalTaskCheats_Text = tInternalTaskCheats_Text or {}
tInternalTaskCheats_Text[18786] = {}

-- 【玩家等级不足，无法接受任务】
tInternalTaskCheats_Text[18786]["111"] = "Kid, you`re not strong enough to study my Inner Power Books. The unusual power may hurt you instead."
tInternalTaskCheats_Text[18786]["112"] = "If you`re really interested, find me when you reach at least Level 15 of 2nd-reborn."
tInternalTaskCheats_Text[18786]["Option1"] = "Alright."

-- 第一次对白（脚本注意：领取秘籍后不再显示）
tInternalTaskCheats_Text[18786]["121"] = "To concentrate on the study of humankind`s potential, I completely isolated myself from the outside world 100 years ago. Finally, I achieved the essence of Inner Power."
tInternalTaskCheats_Text[18786]["Option2"] = "How~to~acquire~Inner~Power?"
tInternalTaskCheats_Text[18786]["Option3"] = "Not~interested."

-- 【领取秘籍后对白】
tInternalTaskCheats_Text[18786]["131"] = "Good to see you again. If you`ve finished studying the Inner Power Book I gave you last time, you can claim the next book."
tInternalTaskCheats_Text[18786]["Option7"] = "Claim~the~next~book."
tInternalTaskCheats_Text[18786]["Option8"] = "Reclaim~an~Inner~Power~Book."
tInternalTaskCheats_Text[18786]["Option9"] = "I`m~just~passing~by."

-- 【玩家选1，了解内功修炼法门】（脚本注意：领取秘籍后不再显示）
tInternalTaskCheats_Text[18786]["211"] = "During the 100 years, I created numerous concepts, but found it too complicated. Therefore, I embodies these concepts through books."
tInternalTaskCheats_Text[18786]["212"] = "~It also requires Potency Points to make breakthrough while training the Inner Power. You can collect Potency Points from the Kingdom Missions."
tInternalTaskCheats_Text[18786]["Option4"] = "How~can~I~get~the~books?"

-- 【玩家选1.1，了解如何获取内功秘籍】（脚本注意：领取秘籍后不再显示）
tInternalTaskCheats_Text[18786]["221"] = "Meeting you was fate. I`d like present you a Universal Concept (A). When you acquire its essence and reach the perfect level, you can"
tInternalTaskCheats_Text[18786]["222"] = "find me to claim the next book."
tInternalTaskCheats_Text[18786]["Option5"] = "Thanks~a~lot!"

-- 【玩家选1.1.1，失败，背包空间不足】
tInternalTaskCheats_Text[18786]["231"] = "You`re carrying a full bag. Why not make some room, first?"
tInternalTaskCheats_Text[18786]["Option6"] = "I`ll~do~it~now."

-- 玩家选1，失败，上一本秘籍还未圆功
tInternalTaskCheats_Text[18786]["311"] = "Kid, you need to train the power of %s to the perfect level before you can claim the next book."
tInternalTaskCheats_Text[18786]["312"] = "~Be patient, okay?"
tInternalTaskCheats_Text[18786]["Option10"] = "Okay."

-- 玩家选1，失败，暂时没有更多的秘籍了
tInternalTaskCheats_Text[18786]["321"] = "You`ve acquired all the Inner Power Books I have finished till now. You do have a gift for exploring Inner Power."
tInternalTaskCheats_Text[18786]["322"] = "~When I make new books, find me to claim one."
tInternalTaskCheats_Text[18786]["Option11"] = "Okay."

-- 玩家选1，成功，提示领取秘籍所需的材料
tInternalTaskCheats_Text[18786]["331"] = "I knew you wouldn`t let me down. To claim the book %s, you need to give me %s."
tInternalTaskCheats_Text[18786]["332"] = "~Are you ready?"
tInternalTaskCheats_Text[18786]["Option12"] = "Yes!~I`m~ready~for~exchange."
tInternalTaskCheats_Text[18786]["Option13"] = "I`m~not~ready,~yet."

-- 玩家选1.1，失败，背包空间不足
tInternalTaskCheats_Text[18786]["341"] = "Your inventory is full. Please make some room, first."
tInternalTaskCheats_Text[18786]["Option14"] = "I`ll~do~it~now."

-- 玩家选1.1，失败，材料不足
tInternalTaskCheats_Text[18786]["351"] = "It seems you don`t have the item I want. Actually, I care not what you bring to me,"
tInternalTaskCheats_Text[18786]["352"] = "~but it`s a way to show your sincerity."
tInternalTaskCheats_Text[18786]["Option15"] = "Sorry,~I`ll~talk~to~you~later."

-- 玩家选1.1，成功，获得秘籍
tInternalTaskCheats_Text[18786]["361"] = "Good job. You`ve proved your sincerity, and I`ll give you the book %s."
tInternalTaskCheats_Text[18786]["Option16"] = "Thanks!"

-- 玩家选2，补领秘籍
tInternalTaskCheats_Text[18786]["411"] = "You`re too careless. It`s not good for an Inner Power trainer. Anyway, I`ll give another one. Take it well!"
tInternalTaskCheats_Text[18786]["Option17"] = "I~know."

-- 玩家选2.1，失败，已有该秘籍
tInternalTaskCheats_Text[18786]["421"] = "You`ve already had a %s in your inventory. You don`t need more."
tInternalTaskCheats_Text[18786]["Option18"] = "Okay."

-- 玩家选2.1，失败，背包空间不足
tInternalTaskCheats_Text[18786]["431"] = "Your inventory is too full to contain more things. Make some room, first."
tInternalTaskCheats_Text[18786]["Option19"] = "I`ll~do~it~now."

tInternalTaskCheats_Text["RewardItem"] = "You received an Inner Power Book, %s. Hurry and go training your Inner Power with it!"

-- 需求的中文显示
tInternalTaskCheats_Text["Demand"] = {}
tInternalTaskCheats_Text["Demand"][1] = "None"
tInternalTaskCheats_Text["Demand"][2] = "1 Meteor"
tInternalTaskCheats_Text["Demand"][3] = "1 +1 Stone"
tInternalTaskCheats_Text["Demand"][4] = "1 +1 Stone"
tInternalTaskCheats_Text["Demand"][5] = "3 Dragon Pills"
tInternalTaskCheats_Text["Demand"][6] = "3 Dragon Pills"
tInternalTaskCheats_Text["Demand"][7] = "1 Refined Tortoise Gem"
tInternalTaskCheats_Text["Demand"][8] = "1 Refined Tortoise Gem"
tInternalTaskCheats_Text["Demand"][9] = "300 pieces of Grotto Ice"
tInternalTaskCheats_Text["Demand"][10] = "400 pieces of Grotto Ice"
tInternalTaskCheats_Text["Demand"][11] = "500 pieces of Grotto Ice"
tInternalTaskCheats_Text["Demand"][12] = "7 Meteor Scrolls"
tInternalTaskCheats_Text["Demand"][13] = "8 Meteor Scrolls"
tInternalTaskCheats_Text["Demand"][14] = "9 Meteor Scrolls"
tInternalTaskCheats_Text["Demand"][15] = "3 Celestial Stones"
tInternalTaskCheats_Text["Demand"][16] = "4 Celestial Stones"
tInternalTaskCheats_Text["Demand"][17] = "5 Celestial Stones"

-- 背包信
tInternalTaskCheats_Text[3007294] = {}
tInternalTaskCheats_Text[3007294]["111"] = "After 100 years of isolation and study, the Celestial Sage (Market 314,232) finally achieved the essence of Inner Power."
tInternalTaskCheats_Text[3007294]["112"] = "~He has returned, and is recruiting talented successors. If you`ve reached Level 15 of 2nd-reborn, pay him a visit. This card will disappear after you read it."
tInternalTaskCheats_Text[3007294]["Option1"] = "Take~me~to~see~the~sage."
tInternalTaskCheats_Text[3007294]["Cultivation"] = "You received 15 Study Points!"
tInternalTaskCheats_Text[3007294]["ExpTime"] = "You received 30 minutes of EXP!"
	

----------------------------------------------------------------------------
--Name:		[征服][功能脚本]性别转换服务专员.lua
--Purpose:	性别转换服务专员
--Creator: 	郑鋆
--Created:	2015/07/27
----------------------------------------------------------------------------


tGenderTransitionService_Text = {}
-- 性别转换服务专员
tGenderTransitionService_Text[15805] = {}
tGenderTransitionService_Text[15805]["111"] = "If you want to change your gender for a new life, I can help you. Before making the"
tGenderTransitionService_Text[15805]["112"] = "~magic of gender reassignment works, you need to pay 1075 CPs, and make sure you`ve understood"
tGenderTransitionService_Text[15805]["113"] = "~the rules and procedures about the reassignment."
tGenderTransitionService_Text[15805]["Option1"] = "Please~change~my~gender,~now."
tGenderTransitionService_Text[15805]["Option2"] = "What~else~should~I~know?"
tGenderTransitionService_Text[15805]["Option3"] = "Not~interested."

-- 咨询注意事项。
tGenderTransitionService_Text[15805]["211"] = "To make gender reassignment as simple as possible, I want you to be single, not married. Make sure you`ve"
tGenderTransitionService_Text[15805]["212"] = "~properly handled with your clan and guild affairs. After the reassignment, your name will be removed"
tGenderTransitionService_Text[15805]["213"] = "~from the Charm ranking, and your garment will be put into your inventory. All in all, you need to carefully"
tGenderTransitionService_Text[15805]["214"] = "~consider before making this decision."
tGenderTransitionService_Text[15805]["Option4"] = "I~want~to~change~my~gender."
tGenderTransitionService_Text[15805]["Option5"] = "I`ll~think~about~it."

-- 已婚状态
tGenderTransitionService_Text[15805]["311"] = "You`re married. To change your gender, you need to get a divorce, first."
tGenderTransitionService_Text[15805]["Option6"] = "Okay."

-- 1075天石
tGenderTransitionService_Text[15805]["321"] = "Gender reassignment requires 1,075 CPs. Make sure you have enough money with you."
tGenderTransitionService_Text[15805]["322"] = ""
tGenderTransitionService_Text[15805]["Option7"] = "Okay."

-- 二次确认
tGenderTransitionService_Text[15805]["331"] = "As mentioned, your name will be removed from the Charm ranking after you change the gender,"
tGenderTransitionService_Text[15805]["332"] = "~and the garment in you will be put into your inventory. Are you ready to pay 1075 CPs for gender reassignment?"
tGenderTransitionService_Text[15805]["Option8"] = "Yes."
tGenderTransitionService_Text[15805]["Option9"] = "No."

tGenderTransitionService_Text[15805]["411"] = "Please clear at least 2 empty spaces for garments, first."
tGenderTransitionService_Text[15805]["Option10"] = "I`ll~do~it~now."

tGenderTransitionService_Text[15805]["421"] = "Wait, you`re an Emperor accompanied with several cucubines. You can`t change your gender."
tGenderTransitionService_Text[15805]["Option11"] = "Alright."

tGenderTransitionService_Text[15805]["431"] = "As a %s, you can`t change your gender."
tGenderTransitionService_Text[15805]["Option12"] = "Alright."

tGenderTransitionService_Text["Success"] = {}
tGenderTransitionService_Text["Success"][1] = "With the help of Transgender Doctor, %s successfully changed his gender, and revealed a new life as a woman."
tGenderTransitionService_Text["Success"][2] = "With the help of Transgender Doctor, %s successfully changed her gender, and revealed a new life as a man."



	
	------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]万圣节活动之南瓜大赛
--Purpose:	万圣节活动之南瓜大赛
--Creator: 	郑鋆
--Created:	2015/08/12
------------------------------------------------------------------------------------
--NPC文字对白
-- 中文索引表
tHalloween2015_PumpkinContest_Text = {}
-- 军方蔬菜供应商大瓜	17058
tHalloween2015_PumpkinContest_Text[17058] = {}
tHalloween2015_PumpkinContest_Text[17058]["111"] = "Hello Halloween, I love you! See, I`ve been fully prepared for the festival. From Oct. 29th to Nov. 4th,"
tHalloween2015_PumpkinContest_Text[17058]["112"] = "~all heroes above Level 80 or reborn are invited for the pumpkin contest in my farm. So, what`re you waiting for?"
tHalloween2015_PumpkinContest_Text[17058]["113"] = "~Come and grab your prize!"
tHalloween2015_PumpkinContest_Text[17058]["Option1"] = "I~can`t~wait~to~show~my~skills!"

tHalloween2015_PumpkinContest_Text[17058]["121"] = "Hah, hah! Halloween has arrived! If you`ve reached Level 80 or got reborn, go compete in"
tHalloween2015_PumpkinContest_Text[17058]["122"] = "~the Pumpkin Contest in my farm. Wonderful rewards await you!"
tHalloween2015_PumpkinContest_Text[17058]["Option2"] = "Enter~the~farm."
tHalloween2015_PumpkinContest_Text[17058]["Option3"] = "Tell~me~more."
tHalloween2015_PumpkinContest_Text[17058]["Option4"] = "It`s~not~my~game."

tHalloween2015_PumpkinContest_Text[17058]["131"] = "Hello! Have you enjoyed yourself on Halloween? I hope you did. See you."
tHalloween2015_PumpkinContest_Text[17058]["Option5"] = "Claim~yesterday`s~top~prize."
tHalloween2015_PumpkinContest_Text[17058]["Option6"] = "See~you."

tHalloween2015_PumpkinContest_Text[17058]["211"] = "Let me explain the rules: Sign up with Siu Kua in the farm, and he`ll start a 300-second countdown for you."
tHalloween2015_PumpkinContest_Text[17058]["212"] = "~During the time, you need to hurdle to pick a pumpkin and give it to Siu Kua. Remember, you have to hand in the pumpkin to continue after each picking."
tHalloween2015_PumpkinContest_Text[17058]["213"] = "~Handing in at least 5 pumpkins to Siu Kua will earn yourself a Festival Joy Pack, once in a day. For the champion who hands in the most"
tHalloween2015_PumpkinContest_Text[17058]["214"] = "~number of pumpkins, we`ll present an extra prize on the next day."
tHalloween2015_PumpkinContest_Text[17058]["Option7"] = "Learn~about~other~things."
tHalloween2015_PumpkinContest_Text[17058]["Option8"] = "I~see."

tHalloween2015_PumpkinContest_Text[17058]["311"] = "You should be at least Level 80 or get reborn to sign up for the contest."
tHalloween2015_PumpkinContest_Text[17058]["Option9"] = "Alright."

tHalloween2015_PumpkinContest_Text[17058]["ChgMap"] = "You arrived at Tai Kua`s farm."

-- 小瓜		17059
tHalloween2015_PumpkinContest_Text[17059] = {}
tHalloween2015_PumpkinContest_Text[17059]["121"] = "Hello, everyone! I`m Tai Kua`s little brother, as well as the director of the Pumpkin Contest. When the contest starts, you have 300 seconds"
tHalloween2015_PumpkinContest_Text[17059]["122"] = "~to pick pumkins. Hand in at least 5 pumkins to me within the time, and you`ll win a Festival Joy Pack and Pumpkin Furniture."
tHalloween2015_PumpkinContest_Text[17059]["Option2"] = "Let`s~begin!"
tHalloween2015_PumpkinContest_Text[17059]["Option3"] = "Claim~my~reward."
tHalloween2015_PumpkinContest_Text[17059]["Option4"] = "Hand~in~a~pumpkin."
tHalloween2015_PumpkinContest_Text[17059]["Option5"] = "Claim~yesterday`s~top~prize."
tHalloween2015_PumpkinContest_Text[17059]["Option6"] = "View~today`s~champion."
tHalloween2015_PumpkinContest_Text[17059]["Option7"] = "What`re~the~rewards?"
tHalloween2015_PumpkinContest_Text[17059]["Option8"] = "Return~to~Twin~City."

tHalloween2015_PumpkinContest_Text[17059]["131"] = "Hello! Have you enjoyed yourself on Halloween? I hope you did. See you."
tHalloween2015_PumpkinContest_Text[17059]["Option9"] = "Return~to~Twin~City."

tHalloween2015_PumpkinContest_Text[17059]["211"] = "Buddy, you`ve already claimed your reward today, haven`t you?"
tHalloween2015_PumpkinContest_Text[17059]["Option10"] = "Sorry,~I~forgot."

tHalloween2015_PumpkinContest_Text[17059]["221"] = "You should hand in at least 5 pumpkins before you can claim a reward from me. So, do your job first!"
tHalloween2015_PumpkinContest_Text[17059]["Option11"] = "Alright."

tHalloween2015_PumpkinContest_Text[17059]["231"] = "Hey, your inventory is full! Make some room first, and then come back to claim your reward."
tHalloween2015_PumpkinContest_Text[17059]["Option12"] = "I`ll~do~it~now."

tHalloween2015_PumpkinContest_Text[17059]["241"] = "I don`t see any pumpkins in your inventory. Hurry up! You`ve already handed in %d pumpkin(s)."
tHalloween2015_PumpkinContest_Text[17059]["Option13"] = "Okay."

tHalloween2015_PumpkinContest_Text[17059]["251"] = "Sorry, you didn`t rank the 1st place in the contest yesterday, so I can`t give you the prize."
tHalloween2015_PumpkinContest_Text[17059]["Option14"] = "Alright."

tHalloween2015_PumpkinContest_Text[17059]["261"] = "You`ve already claimed the reward, haven`t you?"
tHalloween2015_PumpkinContest_Text[17059]["Option15"] = "Sorry,~I~forgot."

tHalloween2015_PumpkinContest_Text[17059]["271"] = "Hey, your inventory is full! Make some room first, and then come back to claim your reward."
tHalloween2015_PumpkinContest_Text[17059]["Option16"] = "Okay."

tHalloween2015_PumpkinContest_Text[17059]["311"] = "The 1st place has not been occupied. Work harder for the championship, and you`ll be able to claim a wonderful prize on the next day."
tHalloween2015_PumpkinContest_Text[17059]["Option17"] = "I`ll~try~my~best."

tHalloween2015_PumpkinContest_Text[17059]["321"] = "Currently, you rank the 1st place with a score of %d. Hold your place, and you`ll win a big prize on the next day."
tHalloween2015_PumpkinContest_Text[17059]["Option18"] = "Great."

tHalloween2015_PumpkinContest_Text[17059]["331"] = "Currently, %s ranks the 1st place with a score of %d."
tHalloween2015_PumpkinContest_Text[17059]["Option19"] = "I~see."

tHalloween2015_PumpkinContest_Text[17059]["411"] = "You can claim the festival reward once in a day. If you hand in the most number of pumpkins, you`ll get an extra prize from me on the next day."
tHalloween2015_PumpkinContest_Text[17059]["Option20"] = "I~have~something~else~to~ask."
tHalloween2015_PumpkinContest_Text[17059]["Option21"] = "I~see."

tHalloween2015_PumpkinContest_Text[17059]["Begin"] = "The Pumpkin Contest has begun. Hurry and hand in as many pumpkins as you can!"
tHalloween2015_PumpkinContest_Text["Failure"] = "Your challenge failed. Talk to Siu Kua to try again."
tHalloween2015_PumpkinContest_Text["Success"] = "Your challenge succeeded! Go claim your reward from Siu Kua!"
tHalloween2015_PumpkinContest_Text["AlreadyAward"] = "This challenge has ended. You can talk to Siu Kua to try again for a better result."
tHalloween2015_PumpkinContest_Text["NoComplete"] = "You need to complete the challenge before you can claim a reward."
tHalloween2015_PumpkinContest_Text["Reward"] = "You received a piece of Pumpkin Furniture and a Festival Joy Pack!"
tHalloween2015_PumpkinContest_Text["NormalHand"] = "Good job! You handed in 1 pumpkin. So far, you`ve handed in %d pumpkin(s)."
tHalloween2015_PumpkinContest_Text["ExpHand"] = "Good job! You handed in 1 pumpkin, and received 10 minutes of EXP! So far, you`ve handed in %d pumpkin(s)."
tHalloween2015_PumpkinContest_Text["CultivatHand"] = "Good job! You handed in 1 pumpkin, and received 5 Study Points! So far, you`ve handed in %d pumpkin(s)."
tHalloween2015_PumpkinContest_Text["RankExp"] = "You received a Pumpkin Specter furniture and 500 minutes of EXP!"
tHalloween2015_PumpkinContest_Text["Rank"] = "You received a Pumpkin Specter furniture!"
tHalloween2015_PumpkinContest_Text["ChgMap"] = "You`ve returned to Wind Plain."
tHalloween2015_PumpkinContest_Text["NotRegistered"] = "You should sign up for the Pumpkin Contest with Siu Kua before you can pick the pumpkins."
tHalloween2015_PumpkinContest_Text["MonsterName"] = "PumpkinSpecter"
tHalloween2015_PumpkinContest_Text["HaveMonster"] = "Pumpkin Specter appeared! Go defeat it!"
tHalloween2015_PumpkinContest_Text["HaveTaskItem"] = "You`ve had a pumpkin in your inventory. Hand in it to Siu Kua, first."
tHalloween2015_PumpkinContest_Text["RewardItem"] = "You received a pumpkin!"
tHalloween2015_PumpkinContest_Text["NoSpace"] = "Your inventory is too full to contain more pumpkins."
tHalloween2015_PumpkinContest_Text["Frozen"] = "You felt a sense of coldness, and got frozen."
tHalloween2015_PumpkinContest_Text["Burn"] = "It`s awfully hot, and you fell into a chaos."
tHalloween2015_PumpkinContest_Text["Flight"] = "Wow, you were blown into the air."
tHalloween2015_PumpkinContest_Text["Decelerat"] = "Oh bad, you were frozen."
tHalloween2015_PumpkinContest_Text["Vertigo"] = "There are many little stars in your eyes."
tHalloween2015_PumpkinContest_Text["FlyingArray"] = "You felt dizzy, and found yourself in a strange place."
tHalloween2015_PumpkinContest_Text["Exciting"] = "You saw an super storm."
tHalloween2015_PumpkinContest_Text["ItemOverdue"] = "Halloween is over and the item is of no use, so you threw it away."
tHalloween2015_PumpkinContest_Text["ItemUse3004677"] = "Hurry and hand in the pumpkin to Siu Kua."


------------------------------------------------------------------------------------
--Name:			150706[简体征服][活动脚本]万圣节大变身(10.29-11.4)
--Purpose:		万圣节(10.29-11.4)之万圣节大变身
--Creator:		黄昕哲
--Created:		2015/07/08
------------------------------------------------------------------------------------
--前缀Halloween2015_Transform_
tHalloween2015_Transform_Text = {}
--变身巫师蒙迪尔17053
tHalloween2015_Transform_Text[17053] = {}
tHalloween2015_Transform_Text[17053]["Text111"] = "Hey, I see you! Don`t stare at my magic stone like that! I know it`s beautiful, but it`s not time to reveal its power."
tHalloween2015_Transform_Text[17053]["Text112"] = "~From Oct. 29th to Nov. 4th, I`ll share the secret with heroes who`ve reached Level 80 or got reborn."
tHalloween2015_Transform_Text[17053]["Text113"] = ""
tHalloween2015_Transform_Text[17053]["Option1"] = "I~can`t~wait~to~see!"

tHalloween2015_Transform_Text[17053]["Text121"] = "Listen, everybody! I`ve successfully tested the shapeshift spell on frogs. I wonder how it will work on humans. If you`ve"
tHalloween2015_Transform_Text[17053]["Text122"] = "~reached Level 80 or got reborn, come and help me perform the test between Oc. 29th and Nov. 4th. If it succeeds,"
tHalloween2015_Transform_Text[17053]["Text123"] = "~I`ll give you a nice reward. It`s funny, isn`t it?"
tHalloween2015_Transform_Text[17053]["Option2"] = "Let~me~have~a~try."
tHalloween2015_Transform_Text[17053]["Option3"] = "Claim~my~reward."
tHalloween2015_Transform_Text[17053]["Option4"] = "How~can~I~do?"
tHalloween2015_Transform_Text[17053]["Option5"] = "Sounds~terrible."

tHalloween2015_Transform_Text[17053]["Text131"] = "The Shapeshift Stone seems not perfect yet. How about to add some bat blood? Hey, you came at the right time! Would you like"
tHalloween2015_Transform_Text[17053]["Text132"] = "~to test my new spell?"
tHalloween2015_Transform_Text[17053]["Option6"] = "Come~on,~give~me~a~break."

tHalloween2015_Transform_Text[17053]["Text211"] = "Hey, you`re not qualified yet. I suggest you not to take the risk. What if you can`t change back?"
tHalloween2015_Transform_Text[17053]["Option7"] = "Alright."

tHalloween2015_Transform_Text[17053]["Text221"] = "Good! Please allow me some time to adjust the Shapeshift Stone. Okay, it`ll turn you into ."
tHalloween2015_Transform_Text[17053]["Text222"] = "[%s]~Now, take a breath, and put your hand on the stone. Let`s see what will happen. If it succeeds, I`ll"
tHalloween2015_Transform_Text[17053]["Text223"] = "~give you a nice reward."
tHalloween2015_Transform_Text[17053]["Option8"] = "Well,~I~don`t~feel~well."

tHalloween2015_Transform_Text[17053]["Text231"] = "Hurry up! Go touch the Shapeshift Stone, and turn yourself into [%s]. I can`t wait to see the result. If you fail, try more times."
tHalloween2015_Transform_Text[17053]["Option9"] = "Okay."

tHalloween2015_Transform_Text[17053]["Text311"] = "You`ve already claimed your reward, haven`t you? Look, I love turning greedy guys into frogs. So, what do you say?"
tHalloween2015_Transform_Text[17053]["Option10"] = "Sorry,~I~forgot."

tHalloween2015_Transform_Text[17053]["Text321"] = "Get closer to me! Um... You`re still you, without any changes. Stop playing around. Go try at the Shapeshift Stone."
tHalloween2015_Transform_Text[17053]["Text322"] = "~If you are successfully turned into [%s], report back to me. If you fail, try more times. Trust me, it`s safe."
tHalloween2015_Transform_Text[17053]["Option11"] = "Alright."

tHalloween2015_Transform_Text[17053]["Text331"] = "Ah, I`m a genius! With the shapeshift spell, I`ll be able to marry the princess, and enjoy a splendid fame."
tHalloween2015_Transform_Text[17053]["Text333"] = "~Thanks! This is the reward I promised. Look, I need your help everyday during the event."
tHalloween2015_Transform_Text[17053]["Option12"] = "Thanks!"

tHalloween2015_Transform_Text[17053]["Text411"] = "All heroes above Level 80 or reborn have a chance to take the test at me, everyday from Oct. 29th to Nov. 4th. I tell you what"
tHalloween2015_Transform_Text[17053]["Text412"] = "~you need to turn into, and then you just go activate the Shapeshift Stone. Since the magic is still under testing,"
tHalloween2015_Transform_Text[17053]["Text413"] = "~something unexpected may happen. Don`t worry, I can handle it. If you successfully turn into what I want, I`ll reward you a nice gift."
tHalloween2015_Transform_Text[17053]["Option13"] = "Got~it!"

tHalloween2015_Transform_Text[17053]["Talk1"] = "You helped Mondial finish the test, and received a Festival Joy Pack."

tHalloween2015_Transform_Text[17053]["FullBag"] = "Hey, your inventory is full! Make some room first, and then come back to claim your reward."


--变身魔法石17054
tHalloween2015_Transform_Text[17054] = {}

tHalloween2015_Transform_Text[17054]["Text111"] = "The stone floating over the formation reflects dazzling lights."
tHalloween2015_Transform_Text[17054]["Option1"] = "It`s~so~beautiful."

tHalloween2015_Transform_Text[17054]["Text121"] = "The stone floating over the formation reflects dazzling lights. What do you want to do?"
tHalloween2015_Transform_Text[17054]["Option2"] = "What`s~this?"
tHalloween2015_Transform_Text[17054]["Option3"] = "Activate~the~stone."
tHalloween2015_Transform_Text[17054]["Option4"] = "It`s~weird."

tHalloween2015_Transform_Text[17054]["Text131"] = "The stone is still beautiful. Though you feel curious about the mysterious magic in it, you wouldn`t like to touch it again."

tHalloween2015_Transform_Text[17054]["Text211"] = "It seems the stone contains marvellous power. What is it used for? Talk to Wizard Mondial to get more details."
tHalloween2015_Transform_Text[17054]["Option6"] = "Okay."


tHalloween2015_Transform_Text[17054]["Msg1"] = "You`ve finished the shapeshift test. Go claim your reward from Mondial."
tHalloween2015_Transform_Text[17054]["Stone"] = "With a strange sound, you were turned into a(n) %s!"
tHalloween2015_Transform_Text[17054]["Candy"] = "You ate the candy, and felt weird. Suddenly, you transformed into a(n) %s, and received the Shape Restore Potion. It`ll last for 1 hour."

tHalloween2015_Transform_Text[17054]["Talk1"] = "You put your hands on the stone, but nothing happened. Mondial reminded that you need to wait 5 seconds to try again after each test."
tHalloween2015_Transform_Text[17054]["Talk2"] = "You nervously put hands on the stone, and found yourself surrounded by strange smog. You failed to transform, and felt dizzy for 1 second."
tHalloween2015_Transform_Text[17054]["Talk3"] = "You nervously put hands on the stone, and found yourself in reversal state for 15 seconds."
tHalloween2015_Transform_Text[17054]["Talk4"] = "You nervously put hands on the stone. Amazedly, you received 20 Study Points!"
tHalloween2015_Transform_Text[17054]["Talk5"] = "You nervously put hands on the stone, and found your inventory too full to contain the Shapeshift Candy."
tHalloween2015_Transform_Text[17054]["Talk6"] = "You nervously put hands on the stone, and found a Shapeshift Candy in your inventory."

tHalloween2015_Transform_Text[3003675] = {}
tHalloween2015_Transform_Text[3003675]["Talk1"] = "Halloween is over and the item is of no use, so you threw it away."

tHalloween2015_Transform_Text[3003676] = {}
tHalloween2015_Transform_Text[3003676]["Talk1"] = "You drank the Shape Restore Potion, and found yourself changed back."
tHalloween2015_Transform_Text[3003676]["Talk2"] = "The potion doesn`t work since you haven`t transformed yourself."

tHalloween2015_Transform_Text["Form"] = {}
tHalloween2015_Transform_Text["Form"][1] = "HalfGhost"
tHalloween2015_Transform_Text["Form"][2] = "ResentfulSpecter"
tHalloween2015_Transform_Text["Form"][3] = "HeadlessSoldier"
tHalloween2015_Transform_Text["Form"][4] = "SkeletonSoldier"
tHalloween2015_Transform_Text["Form"][5] = "WickedWizard"
tHalloween2015_Transform_Text["Form"][6] = "ClawedZombie"

------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]2015万圣节之女巫的南瓜
--Purpose:	2015万圣节之女巫的南瓜
--Creator: 	严振飞
--Created:	2015/07/23
------------------------------------------------------------------------------------
----------------------------------NPC对白--------------------------------------
tHalloween2015_Witch_Text = {}
------------------------------【女巫桑德拉】---------------------------------------
tHalloween2015_Witch_Text[17055] = {}
-- 进活动地图
tHalloween2015_Witch_Text[17055]["ChgMap"] = "You felt dizzy, and what you saw became fuzzy. After a while, you found yourself in Twin City in the night."

-- 活动前
tHalloween2015_Witch_Text[17055]["Text111"] = "The land is beautiful, but the monsters here are really annoying. Some troublemakers ruined my pumpkin field!"
tHalloween2015_Witch_Text[17055]["Text112"] = "~Have you reached Level 80 or got reborn? If you did, come and help me teach those troublemakers a lesson"
tHalloween2015_Witch_Text[17055]["Text113"] = "~from Oct. 29th to Nov. 4th. Don`t forget to bring back my pumpkins. I promise you a nice reward."
tHalloween2015_Witch_Text[17055]["Option111"] = "I`m~looking~for~a~fight."
-- 活动后
tHalloween2015_Witch_Text[17055]["Text121"] = "Those troublemakers are making me mad! If not the pumpkin feast, I would go finish them all by myself!"
tHalloween2015_Witch_Text[17055]["Text122"] = "~Kids, thanks for bring me back the pumpkins. I feel much better now."
tHalloween2015_Witch_Text[17055]["Option121"] = "Kids..."
-- 活动中
tHalloween2015_Witch_Text[17055]["Text131"] = "Have you heard about the Halloween troublemakers? They can change their shapes, and they love pumpkins. Each time Halloween arrives, they`ll loot a pumpkin field."
tHalloween2015_Witch_Text[17055]["Text132"] = "~This time, they stupidly chose mine. I won`t let things go. If you`ve reached Level 80 or got reborn, come and help me retrieve my pumpkins from Oct. 29th to Nov. 4th."
--这句要单独出来
tHalloween2015_Witch_Text[17055]["Text133"] = "~These troublemakers only live in the dark, and I know where they are."
tHalloween2015_Witch_Text[17055]["Option131"] = "Show~me~the~way."
tHalloween2015_Witch_Text[17055]["Option132"] = "Return~to~Wind~Plain."
tHalloween2015_Witch_Text[17055]["Option133"] = "I~brought~you~the~pumpkins!"
tHalloween2015_Witch_Text[17055]["Option134"] = "What~can~I~do?"
tHalloween2015_Witch_Text[17055]["Option135"] = "I`m~on~the~way."
-- 已领取奖励
tHalloween2015_Witch_Text[17055]["Text141"] = "You`ve helped me retrieve some pumpkins, today. Why not take a break?"
tHalloween2015_Witch_Text[17055]["Option141"] = "Alright."
-- 等级不足
tHalloween2015_Witch_Text[17055]["Text151"] = "You need to know yourself well. Obviously, you`re still too green to deal with the troublemakers. Keep practicing, and come back when you reach Level 80 or get reborn."
tHalloween2015_Witch_Text[17055]["Option151"] = "I`ll~try~my~best."
-- 没有任务道具
tHalloween2015_Witch_Text[17055]["Text161"] = "Where is my pumpkin? You should do the job before you can claim a reward from me."
tHalloween2015_Witch_Text[17055]["Text162"] = "~Dare to cheat me again, I`ll turn you into a goat! Oh, what about a pumpkin?"
tHalloween2015_Witch_Text[17055]["Option161"] = "I`ll~go~find~your~pumpkins."


---【我具体要怎么做呢？】
-- 双龙城第一句
tHalloween2015_Witch_Text[17055]["Text211"] = "I`ll lead you to the troublemakers` lair. Once you find the troublemakers, kill them and retrieve my pumpkins."
-- 活动司徒第一句
tHalloween2015_Witch_Text[17055]["Text212"] = "The troublemakers stole my pumpkins. I want you to kill them and retrieve my pumpkins."
-- 后面几句
tHalloween2015_Witch_Text[17055]["Text213"] = "~Don`t be surprised if you see the troublemaker suddenly grow bigger in fight. It`s a trick. Sometimes, it may"
tHalloween2015_Witch_Text[17055]["Text214"] = "~reveal its real look. The Real Troublemaker is purple, and it has my pumpkin."
tHalloween2015_Witch_Text[17055]["Option211"] = "Real~Troublemaker?"
tHalloween2015_Witch_Text[17055]["Option212"] = "I~see."

---【紫色的捣蛋鬼真身？】
tHalloween2015_Witch_Text[17055]["Text221"] = "Yes. The bigger the troublemaker, the wider the possibility it will reveal its real look. Only the Real Troublemakers in purple"
tHalloween2015_Witch_Text[17055]["Text222"] = "~have my pumpkins. Got it? If you retrieve my pumpkins, bring it back to me within 1 hour. I await the pumpkins for my soup."
tHalloween2015_Witch_Text[17055]["Text223"] = "~I look forward to your help once in a day."
tHalloween2015_Witch_Text[17055]["Option221"] = "Got~it."


------------------------------【活动地图内--女巫桑德拉】---------------------------------------
tHalloween2015_Witch_Text[17056] = {}
-- 传送回双龙城
tHalloween2015_Witch_Text[17056]["ChgMap"] = "With a flash of bright light, Sandra teleported you back to Wind Plain."


-------------------------------------【桑德拉的南瓜】---------------------------------------
tHalloween2015_Witch_Text["DelItem"] = "Halloween is over and the item is of no use, so you threw it away."
tHalloween2015_Witch_Text["NowTime"] = "Hurry up! You need to give the pumpkin back to Sandra within 1 hour."

-------------------------------------【怪物提示】---------------------------------------
tHalloween2015_Witch_Text["DescText"] = "It`s the Real Troublemaker that you need to kill to get a pumpkin."
tHalloween2015_Witch_Text["SpaceFull"] = "Your inventory is full. Please make some room, first."
tHalloween2015_Witch_Text["Pumpkin"] = "You knocked the Real Troublemaker down, and received Sandra`s Pumpkin. Hurry and give it back to Sandra within 1 hour."
-- 怪物变大提示
tHalloween2015_Witch_Text["MonsLarger"] = {}
tHalloween2015_Witch_Text["MonsLarger"][7582] = "Oops, the Mini Troublemaker suddenly grew bigger. It`s disgusting. Go kill it!"
tHalloween2015_Witch_Text["MonsLarger"][7583] = "Oops, the Little Troublemaker suddenly grew bigger. It`s disgusting. Go kill it!"
tHalloween2015_Witch_Text["MonsLarger"][7584] = "Oops, the Troublemaker suddenly grew bigger. Be brave, and kill it!"
tHalloween2015_Witch_Text["MonsLarger"][7585] = "Oops, the Adult Troublemaker suddenly grew bigger. Don`t be scared. You can handle it."
tHalloween2015_Witch_Text["MonsLarger"][7586] = "Watch out! The Troublemaker revealed its real look and became a Real Troublemaker. Kill it, and retrieve Sandra`s Pumpkin!"


------------------------------------------------------------------------------------
--Name:			150728[简体征服][活动脚本]万圣节(10.29-11.4)之双龙游园
--Purpose:		万圣节(10.29-11.4)之双龙游园
--Creator:		黄昕哲
--Created:		2015/07/28
------------------------------------------------------------------------------------
--前缀	tHalloween2015_Garden_Text
tHalloween2015_Garden_Text = {}
------------------------------------------------------------------------------------
--游园大使小兔
tHalloween2015_Garden_Text[17080] = {}

--活动前
tHalloween2015_Garden_Text[17080]["Text111"] = "When you hear costume ball and masquerade party, what comes to your mind? Of course, Halloween!"
tHalloween2015_Garden_Text[17080]["Text112"] = "~From Oct. 29th to Nov. 4th, Bunny will host a masquerade party in Twin City for heroes above Level 80 or reborn."
tHalloween2015_Garden_Text[17080]["Text113"] = ""
tHalloween2015_Garden_Text[17080]["Option1"] = "Sounds~good!"

--活动中
tHalloween2015_Garden_Text[17080]["Text121"] = "Happy Halloween! An unusual masquerade party will be held at 9:00, 12:00, 19:00 and 22:00,"
tHalloween2015_Garden_Text[17080]["Text122"] = "~everyday during the celebration. If you`ve reached Level 80 or got reborn, come and join us!"
tHalloween2015_Garden_Text[17080]["Option2"] = "Let`s~start!"
tHalloween2015_Garden_Text[17080]["Option3"] = "I`ve~completed~the~trip."
tHalloween2015_Garden_Text[17080]["Option4"] = "Tell~me~more."
tHalloween2015_Garden_Text[17080]["Option5"] = "I`ll~talk~to~you~later."

--活动后
tHalloween2015_Garden_Text[17080]["Text131"] = "Halloween is over! I truly had a wonderful festival. See you next year!"
tHalloween2015_Garden_Text[17080]["Option6"] = "See~you."

--开始游园
--等级不足
tHalloween2015_Garden_Text[17080]["Text211"] = "Hey, you are still too green! Keep practicing, and come back when you reach Level 80 or get reborn."
tHalloween2015_Garden_Text[17080]["Option7"] = "Alright."

--时间段不正确
tHalloween2015_Garden_Text[17080]["Text221"] = "Be patient, the masquerade party will be held at 9:00, 12:00, 19:00 and 22:00, every day."
tHalloween2015_Garden_Text[17080]["Option8"] = "Okay."

--已经完成
tHalloween2015_Garden_Text[17080]["Text231"] = "You`ve completed your trip in the masquerade party. Why not leave the chances to others?"
tHalloween2015_Garden_Text[17080]["Option9"] = "You`re~right."

--参加活动
tHalloween2015_Garden_Text[17080]["Text241"] = "I`ve changed your form. Enjoy your trip in the masquerade party!"
tHalloween2015_Garden_Text[17080]["Option10"] = "Great!"

tHalloween2015_Garden_Text[17080]["Msg1"] = "Your shape has been successfully changed. Go change it at the first changing spot at (348,334)!"
--完成游园
--已完成过

tHalloween2015_Garden_Text[17080]["Text311"] = "Hey, I know you. You`ve completed your trip in the masquerade party."
tHalloween2015_Garden_Text[17080]["Option11"] = "Oh,~yes."

--超时,任务失败
tHalloween2015_Garden_Text[17080]["Text321"] = "You need to change your form orderly at (348,334), (388,257), (445,310) and (394,382) within 1 hour. If you are disconnected, you can continue at the last changing spot."
tHalloween2015_Garden_Text[17080]["Option12"] = "I~see."

tHalloween2015_Garden_Text[17080]["Talk1"] = "You received a Festival Joy Pack, a piece of mount-shaped furniture and a Shape shift Spell!"

--背包已满
tHalloween2015_Garden_Text[17080]["Text331"] = "Hey, your inventory is full! Make some room first, and then come back to claim your reward."
tHalloween2015_Garden_Text[17080]["Option13"] = "Alright."

--了解活动详情
tHalloween2015_Garden_Text[17080]["Text411"] = "When it starts, you`re given 1 hour to change your form at (348,334), (388,257), (445,310) and (394,382) in turns,"
tHalloween2015_Garden_Text[17080]["Text412"] = "~and then return to me. If you succeed, you`ll win a Festival Joy Pack, a piece of mount furniture, and a Shapeshift Spell."
tHalloween2015_Garden_Text[17080]["Text413"] = "~If you fail, you can try again in the next round. While if you are disconnect, you can continue at the last changing spot."
tHalloween2015_Garden_Text[17080]["Text414"] = ""
tHalloween2015_Garden_Text[17080]["Option14"] = "Learn~about~other~things."
tHalloween2015_Garden_Text[17080]["Option15"] = "I`ll~talk~to~you~later."

--变身点提示
tHalloween2015_Garden_Text["Trap"] = {}
tHalloween2015_Garden_Text["Trap"]["Msg1"] = "You need to find Party Host Bunny to change your form, first."
tHalloween2015_Garden_Text["Trap"]["Msg2"] = "Your next changing spot is at (%s,%s)."

tHalloween2015_Garden_Text["Trap"]["Msg3"] = "Please keep your transformed status, and report back to Party Host Bunny to finish the trip."


--物品相关
--变身符
tHalloween2015_Garden_Text[3007094] = {}
tHalloween2015_Garden_Text[3007094]["Talk1"] = "Halloween is over and the item is of no use, so you threw it away."
tHalloween2015_Garden_Text[3007094]["Talk2"] = "Failed to use the Shapeshift Spell. You`re already in a transformation state."
tHalloween2015_Garden_Text[3007094]["Talk3"] = "Please wait 60 seconds to use the Shapeshift Spell."

--家具
tHalloween2015_Garden_Text["Furniture"] = {}
tHalloween2015_Garden_Text["Furniture"]["Talk1"] = "%s is broken, so you threw it away."

tHalloween2015_Garden_Text["Furniture"]["Text111"] = "Are you sure you want to pack away the %s?"
tHalloween2015_Garden_Text["Furniture"]["Option1"] = "Pack~it~away."

tHalloween2015_Garden_Text["Furniture"]["Text121"] = "You should upgrade your house to Level 2 before you can place furniture in it."
tHalloween2015_Garden_Text["Furniture"]["Option2"] = "I~see."


tHalloween2015_Garden_Text["Furniture"]["TalkError1"] = "You can`t place furniture outside of your house."
tHalloween2015_Garden_Text["Furniture"]["TalkError2"] = "You can only place furniture in your own house."
tHalloween2015_Garden_Text["Furniture"]["TalkError3"] = "For a Level %s house, you can place up to %s pieces of furniture in it."
tHalloween2015_Garden_Text["Furniture"]["TalkError4"] = "Your inventory is almost full. Please make some room, first."

--动态npc
tHalloween2015_Garden_DynaNpc = {}
	tHalloween2015_Garden_DynaNpc["Type"] = 2
	tHalloween2015_Garden_DynaNpc["Sort"] = 32
	tHalloween2015_Garden_DynaNpc["Ownertype"] = 1
	tHalloween2015_Garden_DynaNpc["Life"] = 0
	tHalloween2015_Garden_DynaNpc["RegionType"] = 0
	tHalloween2015_Garden_DynaNpc["Base"] = 1
	tHalloween2015_Garden_DynaNpc["Linkid"] = 0
	tHalloween2015_Garden_DynaNpc[3007095] = {"HalloweenGhost",31980,94007582}
	tHalloween2015_Garden_DynaNpc[3007096] = {"SpiritBeast",31990,94007583}
	tHalloween2015_Garden_DynaNpc[3007097] = {"FortuneKylin",32000,94007584}
	tHalloween2015_Garden_DynaNpc[3007098] = {"ThunderBeast",32010,94007585}
	tHalloween2015_Garden_DynaNpc[3007099] = {"VoidZebra",32020,94007586}
	tHalloween2015_Garden_DynaNpc[3007100] = {"InfernoBeast",32030,94007587}

	--背包信
tBackpackLetter_Text[3004809] = {}
tBackpackLetter_Text[3004809]["NoSpace"] = "Your inventory is full. Please make some room, first."
tBackpackLetter_Text[3004809]["RewardItem"] = "You received an invitation. Hurry and check it in your inventory."
tBackpackLetter_Text[3004809]["AftTime"] = "Halloween is over and the item is of no use, so you threw it away."

tBackpackLetter_Text[3004809]["Dialog1"] = "From Oct. 29th to Nov. 4th, the Halloween Envoy and his partners have arranged a series of interesting events: daily sign-in, Pumpkin Contest, Masquerade Party, Jack`s Game,"
tBackpackLetter_Text[3004809]["Dialog2"] = "the Witch`s Pumpkin, Speed Transformation, Pumpkin vs Zombies and Trick or Treat Would you like to learn about more with the Halloween Envoy?"
tBackpackLetter_Text[3004809]["Option1"] = "Yes!~(discard~after~reading)"
tBackpackLetter_Text[3004809]["Reward1"] = "You received 30 minutes of EXP!"
tBackpackLetter_Text[3004809]["Reward2"] = "You received 15 Study Points!"

------------------------------------------------------------------------------------
--Name:			150729[简体征服][活动脚本]万圣节不给糖就捣乱
--Purpose:		万圣节不给糖就捣乱
--Creator:		张磊
--Created:		2015/07/29
------------------------------------------------------------------------------------

-----------------------NPC对白模版-------------------------
--文字提示
--17149 土豪磊

tHalloween2015_RockTheBoat_Text = {}
--土豪磊
tHalloween2015_RockTheBoat_Text[17149] = {}
--活动对白
tHalloween2015_RockTheBoat_Text[17149]["Text111"] = "Hey, who is there? Don`t hinder me when I was digging for gold. No, no, no, I mean digging for carrots. Get out!"
tHalloween2015_RockTheBoat_Text[17149]["Text112"] = "~I don`t have candies for you."
tHalloween2015_RockTheBoat_Text[17149]["Text113"] = ""

tHalloween2015_RockTheBoat_Text[17149]["Option1"] = "Leave."
tHalloween2015_RockTheBoat_Text[17149]["Option2"] = "Sneak~into~the~house."


--豆子熊
tHalloween2015_RockTheBoat_Text[17150] = {}

--活动时间前
tHalloween2015_RockTheBoat_Text[17150]["Text111"] = "Look, I have a plan. Rich Ray is pretty rich, but he shows little mercy to the poor. I`m going to"
tHalloween2015_RockTheBoat_Text[17150]["Text112"] = "~ask him for candies on Halloween. If he refuses, I`ll gather people to make a mess in his house."
tHalloween2015_RockTheBoat_Text[17150]["Text113"] = "~If you`re interested, come and find me from Oct. 29th to Nov. 4th."

tHalloween2015_RockTheBoat_Text[17150]["Option1"] = "Of~course!"

--活动时间内
tHalloween2015_RockTheBoat_Text[17150]["Text121"] = "I knew it! I knew Rich Ray wouldn`t give us candies. Humph! Since he has chosen `Trick`, I`ll"
tHalloween2015_RockTheBoat_Text[17150]["Text122"] = "~make him wish come true, and throw his house into a mess! If you`ve reached Level 80 or got reborn,"
tHalloween2015_RockTheBoat_Text[17150]["Text123"] = "~give me a hand from Oct. 29th to Nov. 4th."

tHalloween2015_RockTheBoat_Text[17150]["Option2"] = "Count~me~in!"
tHalloween2015_RockTheBoat_Text[17150]["Option3"] = "Claim~my~reward."
tHalloween2015_RockTheBoat_Text[17150]["Option4"] = "What~can~I~do?"
tHalloween2015_RockTheBoat_Text[17150]["Option5"] = "Forgive~him,~okay?"

--活动时间后
tHalloween2015_RockTheBoat_Text[17150]["Text131"] = "I feel so good. Rich Ray must be mad, now."

tHalloween2015_RockTheBoat_Text[17150]["Option6"] = "He~deserved~it."

--接任务，等级不足
tHalloween2015_RockTheBoat_Text[17150]["Text211"] = "Hey, you`re still too green to disable the defense on Ray`s house. Keep practicing, and come back when you reach Level 80 or get reborn."

tHalloween2015_RockTheBoat_Text[17150]["Option7"] = "Alright."

--当天已经完成
tHalloween2015_RockTheBoat_Text[17150]["Text311"] = "You`ve made a mess in Ray`s house, today. I suggest you not to enrage him again, today."

tHalloween2015_RockTheBoat_Text[17150]["Option8"] = "Oh~really?~I`m~scared."

--完成了，但是还未领取奖励
tHalloween2015_RockTheBoat_Text[17150]["Text411"] = "You`ve successfully sunk Ray`s house in a mess, today. Don`t forget to claim your reward."

tHalloween2015_RockTheBoat_Text[17150]["Option9"] = "Okay."

--成功接取任务，传送进地图
tHalloween2015_RockTheBoat_Text[17150]["Text511"] = "Ha, you`re really evil. Listen, I`ll help you enter Rich Ray`s house. Ray is at the courtyard."
tHalloween2015_RockTheBoat_Text[17150]["Text512"] = "~Don`t let him catch you. So, you know what to do in his house? Smash all you can smash,"
tHalloween2015_RockTheBoat_Text[17150]["Text513"] = "~and leave nothing fine."

tHalloween2015_RockTheBoat_Text[17150]["Option10"] = "Count~on~me!"

--请教我怎么捣乱！
tHalloween2015_RockTheBoat_Text[17150]["Text611"] = "I`ll lead you to Ray`s house. Once you enter the house, destroy all furniture you can see."
tHalloween2015_RockTheBoat_Text[17150]["Text612"] = "~Let me teach you: rumple the clothes in the coatroom; mess all papers on the table; turn all books upside"
tHalloween2015_RockTheBoat_Text[17150]["Text613"] = "~down on the bookshelf; black the mirror; pour cold water into his bathtub. After that, you can"
tHalloween2015_RockTheBoat_Text[17150]["Text614"] = "~sneak out and claim your reward from me."

tHalloween2015_RockTheBoat_Text[17150]["Option11"] = "Sounds~challenging."

--领奖部分提示
--已经领取过了奖励
tHalloween2015_RockTheBoat_Text[17150]["Text711"] = "You`ve claimed your reward today. Come on, I`m not that rich as Ray. I don`t have more rewards for you."

tHalloween2015_RockTheBoat_Text[17150]["Option12"] = "Alright."

--有任务但是还未完成
tHalloween2015_RockTheBoat_Text[17150]["Text811"] = "Just destroy what you want to destroy, including all his furniture. You`ll not be blamed since it is Halloween. Have fun!"
tHalloween2015_RockTheBoat_Text[17150]["Text812"] = ""

tHalloween2015_RockTheBoat_Text[17150]["Option13"] = "Take~me~to~Ray`s~house."
tHalloween2015_RockTheBoat_Text[17150]["Option14"] = "Sounds~crazy."

--没接任务
tHalloween2015_RockTheBoat_Text[17150]["Text911"] = "Hey, you should have told me the you were going to make a mess in Ray`s house."
tHalloween2015_RockTheBoat_Text[17150]["Text912"] = "~I only give rewards to those who`ve accepted my request."

tHalloween2015_RockTheBoat_Text[17150]["Option15"] = "Alright."

--成功领取奖励，完成任务
tHalloween2015_RockTheBoat_Text[17150]["Text1011"] = "Wow, you do have a talent of destruction! Thanks for your help! This is the reward I promised."
tHalloween2015_RockTheBoat_Text[17150]["Text1012"] = ""

tHalloween2015_RockTheBoat_Text[17150]["Option16"] = "Thanks."


--衣柜
tHalloween2015_RockTheBoat_Text[17151] = {}

tHalloween2015_RockTheBoat_Text[17151]["Text111"] = "Wow! What a big coatroom! It`s big enough to contain a whole family."
tHalloween2015_RockTheBoat_Text[17151]["Text112"] = "~Wow, wow, wow! There are thousands of clothes in it!"

tHalloween2015_RockTheBoat_Text[17151]["Option1"] = "Rumple~all~clothes."
tHalloween2015_RockTheBoat_Text[17151]["Option2"] = "I~can`t~help~tidying~them~up."
tHalloween2015_RockTheBoat_Text[17151]["Option3"] = "It`s~amazing!"

--犯强迫症了，帮他整理吧！
----非活动时间内
tHalloween2015_RockTheBoat_Text[17151]["Text211"] = "It`s not time to make a mess."
tHalloween2015_RockTheBoat_Text[17151]["Option4"] = "Alright."

----等级不够
tHalloween2015_RockTheBoat_Text[17151]["Text311"] = "Hey, you`re still too green to disable the defense on Ray`s house. Keep practicing, and come back when you reach Level 80 or get reborn."

--弄乱所有的衣服，哼！
--超过1天了，任务重置
tHalloween2015_RockTheBoat_Text[17151]["Text411"] = "You need to accept the quest from Bean Bear, first."


--案几
tHalloween2015_RockTheBoat_Text[17152] = {}

tHalloween2015_RockTheBoat_Text[17152]["Text111"] = "It seems the papers on the table are quite important. Ah, is it his account book?"
tHalloween2015_RockTheBoat_Text[17152]["Text112"] = ""

tHalloween2015_RockTheBoat_Text[17152]["Option1"] = "Mess~all~papers~up."
tHalloween2015_RockTheBoat_Text[17152]["Option2"] = "Put~the~papers~in~order."
tHalloween2015_RockTheBoat_Text[17152]["Option3"] = "What~a~rich~man!"

--这么乱，帮他归类放好吧！
----非活动时间内
tHalloween2015_RockTheBoat_Text[17152]["Text211"] = "It`s not time to make a mess."
tHalloween2015_RockTheBoat_Text[17152]["Option4"] = "Alright."

----等级不够
tHalloween2015_RockTheBoat_Text[17152]["Text311"] = "Hey, you`re still too green to disable the defense on Ray`s house. Keep practicing, and come back when you reach Level 80 or get reborn."

--吹乱所有书稿账本页，哼！
--超过1天了，任务重置
tHalloween2015_RockTheBoat_Text[17152]["Text411"] = "You need to accept the quest from Bean Bear, first."

--书架
tHalloween2015_RockTheBoat_Text[17153] = {}

tHalloween2015_RockTheBoat_Text[17153]["Text111"] = "It`s unbelievable. Where did Ray collect so many rare books? It`s not good to enjoy so many resources by his own, isn`t it?"
tHalloween2015_RockTheBoat_Text[17153]["Text112"] = ""

tHalloween2015_RockTheBoat_Text[17153]["Option1"] = "Put~all~books~upside~down."
tHalloween2015_RockTheBoat_Text[17153]["Option2"] = "Let~me~clean~his~bookshelf."
tHalloween2015_RockTheBoat_Text[17153]["Option3"] = "It`s~good~time~for~reading."

--好心帮他擦擦书的灰尘吧！
----非活动时间内
tHalloween2015_RockTheBoat_Text[17153]["Text211"] = "It`s not time to make a mess."
tHalloween2015_RockTheBoat_Text[17153]["Option4"] = "Alright."

----等级不够
tHalloween2015_RockTheBoat_Text[17153]["Text311"] = "Hey, you`re still too green to disable the defense on Ray`s house. Keep practicing, and come back when you reach Level 80 or get reborn."

--把所有的书都倒着放，哼！
--超过1天了，任务重置
tHalloween2015_RockTheBoat_Text[17153]["Text411"] = "You need to accept the quest from Bean Bear, first."

--镜子
tHalloween2015_RockTheBoat_Text[17154] = {}

tHalloween2015_RockTheBoat_Text[17154]["Text111"] = "What a big mirror! Ray must be a narcissistic man."
tHalloween2015_RockTheBoat_Text[17154]["Text112"] = ""

tHalloween2015_RockTheBoat_Text[17154]["Option1"] = "I`ll~black~this~mirror."
tHalloween2015_RockTheBoat_Text[17154]["Option2"] = "Let~me~clean~the~mirror."
tHalloween2015_RockTheBoat_Text[17154]["Option3"] = "Mirror,~mirror...~I~love~you!"

--哇，我要把镜面擦得透亮！
----非活动时间内
tHalloween2015_RockTheBoat_Text[17154]["Text211"] = "It`s not time to make a mess."
tHalloween2015_RockTheBoat_Text[17154]["Option4"] = "Alright."

----等级不够
tHalloween2015_RockTheBoat_Text[17154]["Text311"] = "Hey, you`re still too green to disable the defense on Ray`s house. Keep practicing, and come back when you reach Level 80 or get reborn."

--用毛笔把镜面画花，哼！
--超过1天了，任务重置
tHalloween2015_RockTheBoat_Text[17154]["Text411"] = "You need to accept the quest from Bean Bear, first."

--浴盆
tHalloween2015_RockTheBoat_Text[17155] = {}

tHalloween2015_RockTheBoat_Text[17155]["Text111"] = "Stupid Ray! He really know how to enjoy life. See, there are warm water, rose petals, and toys in the bathtub."
tHalloween2015_RockTheBoat_Text[17155]["Text112"] = ""

tHalloween2015_RockTheBoat_Text[17155]["Option1"] = "Add~cold~water~in~it."
tHalloween2015_RockTheBoat_Text[17155]["Option2"] = "Pick~up~the~soaps."
tHalloween2015_RockTheBoat_Text[17155]["Option3"] = "What~a~good~life."

--帮他把地上的肥皂捡起！
----非活动时间内
tHalloween2015_RockTheBoat_Text[17155]["Text211"] = "It`s not time to make a mess."
tHalloween2015_RockTheBoat_Text[17155]["Option4"] = "Alright."

----等级不够
tHalloween2015_RockTheBoat_Text[17155]["Text311"] = "Hey, you`re still too green to disable the defense on Ray`s house. Keep practicing, and come back when you reach Level 80 or get reborn."

--往里面加冰水，哼！
--超过1天了，任务重置
tHalloween2015_RockTheBoat_Text[17155]["Text411"] = "You need to accept the quest from Bean Bear, first."



--活动提示
--1.任务未接
tHalloween2015_RockTheBoat_Text["NoTask"] = "You successfully sneaked out of Ray`s house, but you forgot to accept the quest first."

--2.已经领取了奖励
tHalloween2015_RockTheBoat_Text["GetGift"] = "You successfully sneaked out of Ray`s house, but you`ve already claimed a reward today. Go play tomorrow."

--3.离开土豪磊家，却没有完成破坏
tHalloween2015_RockTheBoat_Text["NotDamageAll"] = "You successfully sneaked out of Ray`s house, but you haven`t destroyed all the targets. "

--4.离开土豪磊家，完成所有破坏
tHalloween2015_RockTheBoat_Text["DamageAll"] = "Fortunately, you sneaked out of Ray`s house. Good luck!"

--5.背包空间不足
tHalloween2015_RockTheBoat_Text["NoSpace"] = "Your inventory is full. Please make some room, first."

tHalloween2015_RockTheBoat_Text["SysMsg"] = {}
tHalloween2015_RockTheBoat_Text["SysMsg"][17151] = {}
--6.整理衣柜的提示
tHalloween2015_RockTheBoat_Text["SysMsg"][17151][1]= "You felt satisfied after tidying all clothes up. However, it seems you`ve forgot your task here."

--7.当天已经弄乱衣服
tHalloween2015_RockTheBoat_Text["SysMsg"][17151][2] = "You`ve already rumple all the clothes. Finish your job, and take a chance to sneak out."

--8.成功弄乱衣服
tHalloween2015_RockTheBoat_Text["SysMsg"][17151][3] = "A few minutes later, you rumpled all the clothes. Ray will spend a long time to tidy up."

tHalloween2015_RockTheBoat_Text["SysMsg"][17152] = {}
--9.整理案几的提示
tHalloween2015_RockTheBoat_Text["SysMsg"][17152][1] = "You classified all the papers and books on the table. However, it seems you`ve forgot your task here."

--10.当天已经弄乱案几
tHalloween2015_RockTheBoat_Text["SysMsg"][17152][2] = "You`ve already messed papers on the table. No need to do it again. Finish your job, and take a chance to sneak out."

--11.成功弄乱案几
tHalloween2015_RockTheBoat_Text["SysMsg"][17152][3] = "Excellent! There are papers everywhere. It`s impossible for Ray to put them in order."

tHalloween2015_RockTheBoat_Text["SysMsg"][17153] = {}
--9.整理书架的提示
tHalloween2015_RockTheBoat_Text["SysMsg"][17153][1] = "After cleaning all the books, you realized that you`ve forgot your real aim here."

--10.当天已经弄乱书架
tHalloween2015_RockTheBoat_Text["SysMsg"][17153][2] = "You`ve already put all the books upside down. No need to do it again. Finish your job, and take a chance to sneak out."

--11.成功弄乱书架
tHalloween2015_RockTheBoat_Text["SysMsg"][17153][3] = "Haha, all the books have been upside down. Ray has to stand on his head to find books."

tHalloween2015_RockTheBoat_Text["SysMsg"][17154] = {}
--12.整理镜子的提示
tHalloween2015_RockTheBoat_Text["SysMsg"][17154][1] = "You cleaned the mirror, and made it shining. However, it seems you`ve forgot your task here."

--13.当天已经弄乱镜子
tHalloween2015_RockTheBoat_Text["SysMsg"][17154][2] = "You`ve already blacked the mirror. No need to do it again. Finish your job, and take a chance to sneak out."

--14.成功弄乱镜子
tHalloween2015_RockTheBoat_Text["SysMsg"][17154][3] = "Ray must be sacred when he saw such a black wall here."

tHalloween2015_RockTheBoat_Text["SysMsg"][17155] = {}
--15.肥皂捡起的提示
tHalloween2015_RockTheBoat_Text["SysMsg"][17155][1] = "You picked up the soap, and realized that you`ve forgot your real aim here."

--16.当天已经弄乱镜子
tHalloween2015_RockTheBoat_Text["SysMsg"][17155][2] = "You`ve already added cold water in the bathtub. No need to it again. Finish your job, and take a chance to sneak out."

--17.成功加冰水
tHalloween2015_RockTheBoat_Text["SysMsg"][17155][3] = "Wow, it`s really cold! Ray will be frozen into an ice sucker."

--完成了所有的捣乱
tHalloween2015_RockTheBoat_Text["SysMsg_DamageAll"] = "You`ve successfully destroyed all things you can. Hurry and sneak out of Ray`s house!"

--------------------读条文字
-- tHalloween2015_RockTheBoat_Text[17151]["DT"] = "弄乱衣服"
-- tHalloween2015_RockTheBoat_Text[17152]["DT"] = "弄乱案几"
-- tHalloween2015_RockTheBoat_Text[17153]["DT"] = "弄乱书架"
-- tHalloween2015_RockTheBoat_Text[17154]["DT"] = "画花镜子"
-- tHalloween2015_RockTheBoat_Text[17155]["DT"] = "加冷水"


tHalloween2015_RockTheBoat_Str = {}
	-- 读条文字
	tHalloween2015_RockTheBoat_Str[17151] = "RumplingClothes"
	tHalloween2015_RockTheBoat_Str[17152] = "MessingTable"
	tHalloween2015_RockTheBoat_Str[17153] = "MessingBookshelf"
	tHalloween2015_RockTheBoat_Str[17154] = "BlackingMirror"
	tHalloween2015_RockTheBoat_Str[17155] = "AddingColdWater"
---Name:[简体征服][活动脚本]万圣节活动之杰克的游戏(10.29-11.4)
--Creator: 	陈莺
--Created:	2015/07/22
--------------------------------------------------------------------------------
tHalloween2015_JackGame_Text = {}

--提示
tHalloween2015_JackGame_Text["MsgBox"] = {}
tHalloween2015_JackGame_Text["MsgBox"]["Error"]="Jack burst into a sudden laughter and threw you miles away. You realized you gave a wrong number."
tHalloween2015_JackGame_Text["MsgBox"]["Right"]="What a pity, you gave the right number, but your inventory was too full to contain any rewards. Make some room, first."
tHalloween2015_JackGame_Text["MsgBox"]["First"]="You ate the Pumpkin Cake, and found a number card which was written with %d on it."
tHalloween2015_JackGame_Text["MsgBox"]["Over"]="Halloween is over and the item is of no use, so you threw it away."
tHalloween2015_JackGame_Text["MsgBox"]["Eat"]="EatingPumpkinCake"
tHalloween2015_JackGame_Text["MsgBox"]["CardTip"]="There is a number on the card. Use it with other 3 cards to guess the number that Jack is thinking about."
tHalloween2015_JackGame_Text["MsgBox"]["NotAccept"]="You need to claim a Pumpkin Cake, first."
tHalloween2015_JackGame_Text["MsgBox"]["Eat4"] = "You`ve obtained 4 number cards. Go and play number guess game with Jack."
tHalloween2015_JackGame_Text["MsgBox"]["BagFull"] = "Your inventory is full. Please make some room, first."
--南瓜怪人杰克
--活动前
tHalloween2015_JackGame_Text[17057] = {}
tHalloween2015_JackGame_Text[17057]["Text111"] = "Trick or Treat! How could you celebrate Halloween without candies? Look, I got a good idea. If you`ve reached Level 80"
tHalloween2015_JackGame_Text[17057]["Text112"] = "~or got reborn, come and find me from Oct. 29th to Nov. 4th. Let`s play a funny game at that time."
tHalloween2015_JackGame_Text[17057]["Text113"] = ""
tHalloween2015_JackGame_Text[17057]["Option1"] = "I`m~looking~forward~to~it."
--活动中
tHalloween2015_JackGame_Text[17057]["Text121"] = "Happy Halloween! I`m looking for challengers who`ve reached Level 80 or got reborn for a Number Guess Game"
tHalloween2015_JackGame_Text[17057]["Text122"] = "~from Oct. 29th to Nov. 4th. See, I put the number card in the Pumpkin Cake. You`ll find it when you eat the cake."
tHalloween2015_JackGame_Text[17057]["Text123"] = "~When you collect 4 cards, come and play the game with me!"

tHalloween2015_JackGame_Text[17057]["Option2"] = "Give~me~the~cakes."
tHalloween2015_JackGame_Text[17057]["Option3"] = "I`m~ready~to~guess."
tHalloween2015_JackGame_Text[17057]["Option4"] = "What`re~the~rules?"
tHalloween2015_JackGame_Text[17057]["Option5"] = "I`ll~talk~to~you~later."
--活动后
tHalloween2015_JackGame_Text[17057]["Text131"] = "Halloween is over, and it`s time to say goodbye. Have fun!"
tHalloween2015_JackGame_Text[17057]["Option6"] = "See~you."
--这个小游戏要怎么玩
tHalloween2015_JackGame_Text[17057]["Text211"] = "The rules are easy. If you`ve reached Level 80 or got reborn, I`ll give you 4 Pumpkin Cakes."
tHalloween2015_JackGame_Text[17057]["Text212"] = "~Eat the cakes to collect 4 number cards, and then come to guess the four-digits number that I`m thinking about"
tHalloween2015_JackGame_Text[17057]["Text213"] = "~with the 4 numbers you have."
tHalloween2015_JackGame_Text[17057]["Option7"] = "And~then?"

tHalloween2015_JackGame_Text[17057]["Text221"] = "If you give a wrong number, you`ll be specially punished. Take it easy, I`ll give you tips. "
tHalloween2015_JackGame_Text[17057]["Text222"] = "~While if you hit the number, I`ll present you a wonderful gift. If you want to start over"
tHalloween2015_JackGame_Text[17057]["Text223"] = "~just claim new Pumpkin Cakes from me."
tHalloween2015_JackGame_Text[17057]["Option8"] = "Sounds~interesting!"
--我要领南瓜糕点！
--今日已完成
tHalloween2015_JackGame_Text[17057]["Text311"] = "Do you like my cakes? Thanks. Look, I have only few cakes left."
tHalloween2015_JackGame_Text[17057]["Text312"] = "~Why not leave the chances to others?"
tHalloween2015_JackGame_Text[17057]["Option9"] = "Alright."
--有卡或糕点
tHalloween2015_JackGame_Text[17057]["Text321"] = "Hey, you must have the cakes or number cards with you. Are you sure you want to claim new cakes?"
tHalloween2015_JackGame_Text[17057]["Text322"] = "~If you did, the number you need to guess will also be changed."
tHalloween2015_JackGame_Text[17057]["Option10"] = "I`ve~decided."
tHalloween2015_JackGame_Text[17057]["Option11"] = "I~haven`t~decided,~yet!"
--等级判断
tHalloween2015_JackGame_Text[17057]["Text331"] = "I only play with hardworking heroes. Why not get tougher and come see me when you reach Level 80 or get reborn?"
tHalloween2015_JackGame_Text[17057]["Option12"] = "Alright."
--背包满
tHalloween2015_JackGame_Text[17057]["Text341"] = "Hey, you inventory is full. Please make some room for the cakes."
tHalloween2015_JackGame_Text[17057]["Option13"] = "I`ll~do~it~now."
--领取糕点
tHalloween2015_JackGame_Text[17057]["Text351"] = "Hah, take your cakes. I bet you will like them. Oh, don`t forget to check the card in the cake."
tHalloween2015_JackGame_Text[17057]["Text352"] = "~When you have 4 number cards, come and play game with me! I can`t wait!"
tHalloween2015_JackGame_Text[17057]["Text353"] = ""
--我要猜数字！
--今日已完成
tHalloween2015_JackGame_Text[17057]["Text411"] = "Are you ready? Oh wait, you`ve already played the game with me today. Even you hit the number,"
tHalloween2015_JackGame_Text[17057]["Text412"] = "~I have no reward for you."

tHalloween2015_JackGame_Text[17057]["Text421"] = "Stop kidding me! You haven`t finished the Pumpkin Cakes. Remember, the number cards are in the"
tHalloween2015_JackGame_Text[17057]["Text422"] = "~cakes. Enjoy yourself!"
tHalloween2015_JackGame_Text[17057]["Text423"] = ""
tHalloween2015_JackGame_Text[17057]["Option14"] = "I~see."
--今日完成后
tHalloween2015_JackGame_Text[17057]["Text431"] = "Wow, you gave the correct number! It`s amazing! Well, give me the cards, and I`ll give you the reward as promised."
tHalloween2015_JackGame_Text[17057]["Text432"] = "~Have fun!"
tHalloween2015_JackGame_Text[17057]["Text433"] = ""
tHalloween2015_JackGame_Text[17057]["Option15"] = "Sure."
--数字提示框
tHalloween2015_JackGame_Text[17057]["Text441"] = "Well, show me your number cards. Okay, I have a four-digits number in my mind now. And let me tell you,"
tHalloween2015_JackGame_Text[17057]["Text442"] = {}
tHalloween2015_JackGame_Text[17057]["Text442"][0] = "~from left to right is [%d???]. It must be easy for you to guess the rest based on your cards."
tHalloween2015_JackGame_Text[17057]["Text442"][1] = "~from left to right is [%d??]. It must be easy for you to guess the rest based on your cards."
tHalloween2015_JackGame_Text[17057]["Text442"][2] = "~from left to right is [%d?]. It must be easy for you to guess the rest based on your cards."

tHalloween2015_JackGame_Text[17057]["Text443"] = "Please enter"
tHalloween2015_JackGame_Text[17057]["Option16"] = "Let~me~see."


------------------------------------------------------------------------------------
--Name:			150728[简体征服][活动脚本]中秋节(好礼大派送)(9.22-9.28)
--Purpose:		好礼大派送
--Creator:		王倩娜
--Created:		2015/07/29
------------------------------------------------------------------------------------

tHalloween2015_SignInGift_Text = {}

		tHalloween2015_SignInGift_Text[18781] = {}
		tHalloween2015_SignInGift_Text[18781]["Text111"] = "Halloween is fast approaching, and it will be time for a scream party! As long as you`ve reached Level 80 or got reborn,"
		tHalloween2015_SignInGift_Text[18781]["Text112"] = "~come and join us from Oct. 29th to Nov. 4th of 2015! For the details, I prefer to keep it a secret now."
		tHalloween2015_SignInGift_Text[18781]["Text113"] = ""
		tHalloween2015_SignInGift_Text[18781]["Option1"] = "I~can`t~wait~to~join!"
		
		tHalloween2015_SignInGift_Text[18781]["Text121"] = "Scream for Halloween! From Oct. 29th to Nov. 4th of 2015, we welcome heroes above Level 80 or reborn"
		tHalloween2015_SignInGift_Text[18781]["Text122"] = "~to join us in amazing celebration events! I promise you an unforgettable festival. Come and sign in here"
		tHalloween2015_SignInGift_Text[18781]["Text123"] = "~to claim a gift everyday. When you finish all events on the day, I`ll reward you an extra gift."
		tHalloween2015_SignInGift_Text[18781]["Option2"] = "Learn~about~the~events."
		tHalloween2015_SignInGift_Text[18781]["Option3"] = "I~want~to~sign~in."
		tHalloween2015_SignInGift_Text[18781]["Option4"] = "Claim~my~extra~reward."
		tHalloween2015_SignInGift_Text[18781]["Option5"] = "Sounds~great."
		
		tHalloween2015_SignInGift_Text[18781]["Text131"] = "Thanks for your participation! The Halloween celebration is over. Time to say goodbye."
		tHalloween2015_SignInGift_Text[18781]["Option6"] = "Bye."
	                                                 
		tHalloween2015_SignInGift_Text[18781]["Text211"] = "There are 7 events arranged for Halloween. So, which one are you interested?"
		tHalloween2015_SignInGift_Text[18781]["Option7"] = "Pumpkin~Contest."
		tHalloween2015_SignInGift_Text[18781]["Option8"] = "Masquerade~Party."
		tHalloween2015_SignInGift_Text[18781]["Option9"] = "Jack`s~Game."
		tHalloween2015_SignInGift_Text[18781]["Option10"] = "The~Witch`s~Pumpkin."
		tHalloween2015_SignInGift_Text[18781]["Option11"] = "Speed~Transformation."
		tHalloween2015_SignInGift_Text[18781]["Option12"] = "Pumpkin~vs~Zombies."
		tHalloween2015_SignInGift_Text[18781]["Option13"] = "Trick~or~Treat."
		tHalloween2015_SignInGift_Text[18781]["Option14"] = "Let~me~see."
		
		tHalloween2015_SignInGift_Text[18781]["Option15"] = "I~have~something~else~to~ask~you."

		-- 南瓜大赛。
		tHalloween2015_SignInGift_Text[18781]["Text311"] = "Tai Kua is so happy to be a vegetable supplier for the army that he decides to hold a pumpkin contest."
		tHalloween2015_SignInGift_Text[18781]["Text312"] = "~All heroes above Level 80 or reborn are welcome to compete for fun and wonderful prizes!"
		tHalloween2015_SignInGift_Text[18781]["Option16"] = "Take~me~to~see~Tai~Kua."
		
		-- 双龙游园。
		tHalloween2015_SignInGift_Text[18781]["Text321"] = "A stunning masquerade party is going to light up Twin City. If you`ve reached Level 80 or got reborn, go find Bunny to"
		tHalloween2015_SignInGift_Text[18781]["Text322"] = "~join the party at 9:00, 12:00, 19:00 or 21:00, everyday during the celebration. The more people, the more fun we will have."
		tHalloween2015_SignInGift_Text[18781]["Option17"] = "Take~me~to~see~Bunny."
		
		-- 杰克的游戏。
		tHalloween2015_SignInGift_Text[18781]["Text331"] = "Weird Jack just finished some pumpkin cakes. He is calling heroes above Level 80 or reborn to taste his new dessert"
		tHalloween2015_SignInGift_Text[18781]["Text332"] = "~and play a number guess game with him. If you hit the right number, you`ll be rewarded with a wonderful gift. Delicious food and funny game, definitely worth a try."
		tHalloween2015_SignInGift_Text[18781]["Option18"] = "Take~me~to~see~Jack."
		
		-- 女巫的南瓜。
		tHalloween2015_SignInGift_Text[18781]["Text341"] = "When Sandra was busy preparing the pumpkin feast, a group of troublemakers sneaked into her pumpkin field and made it empty."
		tHalloween2015_SignInGift_Text[18781]["Text342"] = "The witch is irritated, and the consequence will be severe. See, Sandra is recruiting heroes to deal with those troublemakers. If you`re above Level 80 or reborn, give Sandra a hand."
		tHalloween2015_SignInGift_Text[18781]["Option19"] = "Take~me~to~see~Sandra."

		-- 万圣节大变身。
		tHalloween2015_SignInGift_Text[18781]["Text351"] = "Halloween without fancy costumes is not Halloween. While this year, Mondial who have acquired the shapeshift spell will make it a little different."
		tHalloween2015_SignInGift_Text[18781]["Text352"] = "~All heroes above Level 80 or reborn are invited to test his new spell. Mondial promises nice rewards to those who succeed in the test."
		tHalloween2015_SignInGift_Text[18781]["Option20"] = "Take~me~to~see~Mondial."

		-- 南瓜大战僵尸。
		tHalloween2015_SignInGift_Text[18781]["Text361"] = "It`s horrible! A crowd of Dark Zombies appeared in the Pumpkin Garden, and they`ve crushed many small pumpkins. Unfortunately, the Old Gardener is"
		tHalloween2015_SignInGift_Text[18781]["Text362"] = "~counting on the pumpkins to make a living. If you`ve reached Level 80 or got reborn, go eliminate the Dark Zombies for the Old Gardener."
		tHalloween2015_SignInGift_Text[18781]["Option21"] = "Take~me~to~see~the~gardener."

		-- 不给糖果就捣乱。
		tHalloween2015_SignInGift_Text[18781]["Text371"] = "Rich Ray is incredibly rich, but he pays very little attention to the poor. Bean Bear has had enough of his indifference"
		tHalloween2015_SignInGift_Text[18781]["Text372"] = "~and decided to sink his house into a mess. Of course, he is asking help from heroes above Level 80 or reborn."
		tHalloween2015_SignInGift_Text[18781]["Option22"] = "Take~to~see~the~Bean~Bear."

		tHalloween2015_SignInGift_Text["Msg"] = {}
		tHalloween2015_SignInGift_Text["Msg"]["UnderLevel"] = "Sorry, you should be at least Level 80 or reborn to join the event."
		tHalloween2015_SignInGift_Text["Msg"]["OnlyOnce"] = "Let me see. You`ve already signed in at me, today."
		tHalloween2015_SignInGift_Text["Msg"]["Exp"] = "Congratulations! You received 1-day Heaven Blessing and 60 minutes of EXP!"
		tHalloween2015_SignInGift_Text["Msg"]["Cultivation"] = "Congratulations! You received 1-day Heaven Blessing and 50 Study Points!"
		tHalloween2015_SignInGift_Text["Msg"]["Bless"] = "Happy Halloween! You received 1 day of Heaven Blessing!"
		
		tHalloween2015_SignInGift_Text["Msg"]["HavedReward"] = "Hey, you`ve already claimed an extra reward, haven`t you?"
		tHalloween2015_SignInGift_Text["Msg"]["NoSpace"] = "Come on, I can`t put anything into a full bag. Make some room in your inventory, first."
		tHalloween2015_SignInGift_Text["Msg"]["LessThan"] = "Please note that only people who`ve completed all the Halloween events on the day can ask for an extra reward."
		tHalloween2015_SignInGift_Text["Msg"]["GetFestivalID"] = "You received a Festival Joy Pack!"
		
		
		
-----------------------------------------------------------------------------------------------------
--Name:			150803[简体征服][活动脚本]万圣节之南瓜大战僵尸(10.29-11.04)
--Creator: 		许乐
--Created:		2015/08/03
------------------------------------------------------------------------------------------------------
-- 命名前缀
--Halloween2015_PumpkinVSZombie_

-- 17144 南瓜园园丁大叔
tHalloween2015_PumpkinVSZombie_Text = {}
	tHalloween2015_PumpkinVSZombie_Text[17144] = {}
	--活动前对白
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text111"] = "It`s hard to believe, but I did see a crowd of Dark Zombies appear in my pumpkin garden at certain time."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text112"] = "~They`ve crushed many small pumpkins. Alas... I was expecting for a great harvest. Could you help me"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text113"] = "~drive them away from Oct. 29th to Nov. 4th?"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option1"] = "I~will!"

	--活动中对白
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text121"] = "Did you see that? Dark Zombies in my garden! Look, I`ve made some Secret Treasure Chests to deal with them."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text122"] = "~I know they would appear at fixed times. I really need help of heroes who have reached Level 80 or got reborn"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text123"] = "~from Oct. 29th to Nov. 4th. Come and fight against the Dark Zombies!"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option2"] = "Count~on~me!"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option3"] = "Claim~my~reward."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option4"] = "What~can~I~do?"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option5"] = "Can`t~we~be~friends?"
	
	--活动后对白
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text131"] = "Thanks for driving Dark Zombies away from my pumpkin garden! I finally have some pumpkins safe."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option8"] = "Good~luck!"

	--接Option2：我要大战僵尸！
	--失败, 判断等级不足
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text211"] = "Thanks for coming, but you`re still too young to deal with those disgusting Dark Zombies. Keep practicing,"	
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text212"] = "~and come back when you reach Level 80 or get reborn."	
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option9"] = "Alright."
	--在活动时间内，stc(129,39) >= 2
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text221"] = "You`ve already helped me once, today. You look so tired now. Why not take a break?"	
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text222"] = ""
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option10"] = "You`re~right."
	--在活动时间内，杀完3只 stc(129,41) >= 3
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text231"] = "You`ve already helped me once, today. Thanks again! Don`t forget to claim your reward from me!"	
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option11"] = "Okay."  --接到“领取僵尸的奖励”逻辑上 option3
	--接受了任务，但是没有杀完3只，删除相关道具，给魔盒，背包满提示
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text241"] = "Sorry, I can`t put anything into your full inventory. Please make some room, first."	
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option12"] = "I`ll~do~it~now."
	--传送进地图
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text251"] = "See, I have a Secret Treasure Chest for you. Open the chest to get 5 exorcism items."	
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text252"] = "~Use the item, and then target a Dark Zombie to attack. Well, not all the items can directly kill a Dark Zombie."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text253"] = "~If you kill 3 Dark Zombies in a round, you can claim a reward from me."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option13"] = "Got~it."

	--接Option3：领取战胜僵尸的奖励。
	--等级不足
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text311"] = "Thanks for coming, but you`re still too young to deal with those disgusting Dark Zombies. Keep practicing, and come back when you reach Level 80 or get reborn."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option14"] = "Alright."
	--没有进入过地图，没有奖励
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text321"] = "It seems you haven`t solved my problem in the Pumpkin Garden. I`m afraid I have no rewards for you."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option15"] = "Sorry,~I~forgot."
	--已领取过奖励
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text331"] = "You`ve claimed your reward today. You must be too excited to check."
	--未完成任务，不可领取
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text341"] = "Only the heroes who`ve killed 3 Dark Zombies deserve my rewards. Do your job, first!"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option16"] = "I~see."
	--背包满提示
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text351"] = "Your inventory is full. Make some room for the reward, first."
	
	--接Option4：我该如何战胜僵尸？
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text411"] = "I`ll send you to the garden and give you a Secret Treasure Chest. Open the chest to get 5 exorcism item."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text412"] = "~Use the item, and then target a Dark Zombie to attack. Not all the items can directly kill a Dark Zombie."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option6"] = "What`re~these~items?"
	--接Option6：都是哪些法宝呢？
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text611"] = "Corpse Flower, Giant Pumpkin and Hot Pepper can be used to kill the Dark Zombies, while Blooming Tulip and Halloween Candy"
	tHalloween2015_PumpkinVSZombie_Text[17144]["Text612"] = "~can do nothing to the zombies. They`re quite normal things, but effective. Only the heroes who kill 3 Dark Zombies deserve my rewards."
	tHalloween2015_PumpkinVSZombie_Text[17144]["Option7"] = "Got~it."
	
-- 17145 南瓜宝宝
	tHalloween2015_PumpkinVSZombie_Text[17145] = {}
	--活动中对白
	tHalloween2015_PumpkinVSZombie_Text[17145]["Text111"] = "I`m the biggest pumpkin here, and I`m not afraid of Zombies. I could protect my baby pumpkins."
	tHalloween2015_PumpkinVSZombie_Text[17145]["Text112"] = "~Oh no, a crowd of Zombies are approaching! Help me! Use something to hit them! Hurry! Hurry!"
	--选项
	tHalloween2015_PumpkinVSZombie_Text[17145]["Option1"] = "I~want~to~leave."
	tHalloween2015_PumpkinVSZombie_Text[17145]["Option2"] = "Calm~down."

	--使用道具相关提示
	tHalloween2015_PumpkinVSZombie_Text[3007246] = "You placed the Corpse Flower on the ground, and it quickly swallowed a stupid Dark Zombie."
	tHalloween2015_PumpkinVSZombie_Text[3007247] = "You threw the Giant Pumpkin at the Dark Zombie, and immediately killed it."
	tHalloween2015_PumpkinVSZombie_Text[3007248] = "Hah, the Dark Zombie bit the Hot Pepper, and set itself on fire."
	tHalloween2015_PumpkinVSZombie_Text[3007249] = "The Tulip swung in the wind. It`s too weak to withstand the Dark Zombie`s kicks."
	tHalloween2015_PumpkinVSZombie_Text[3007250] = "Should I really use the Halloween Candy to hit the Dark Zombies? They`ll laugh at me!"

	--1010 2005提示	User_TalkChannel2005
	tHalloween2015_PumpkinVSZombie_Text["SendInMap"] = "You arrived at the Pumpkin Garden. Prepare your exorcism items to deal with the Dark Zombies!"
	tHalloween2015_PumpkinVSZombie_Text["BackToMapWithTask"] = "You`ve left the Pumpkin Garden, but you failed to kill 3 Dark Zombies. Watch your time next time."
	tHalloween2015_PumpkinVSZombie_Text["BackToMap"] = "You`ve left the Pumpkin Garden, being far away from the Dark Zombies."

	tHalloween2015_PumpkinVSZombie_Text["DelItem"] = "Halloween is over and the item is of no use, so you threw it away."
	tHalloween2015_PumpkinVSZombie_Text["FullBag"] = "Your inventory is full. You need to clear at least 4 empty spaces, first."
	tHalloween2015_PumpkinVSZombie_Text["OpenBox"] = "You opened the Secret Treasure Chest, and received 5 magic items. Hurry and use them to deal with the zombies!"
	tHalloween2015_PumpkinVSZombie_Text["Level"] = "You should reach at least Level 80 or get reborn before you can use this item."
	tHalloween2015_PumpkinVSZombie_Text["UseInMap"] = "You can only use this item at the event zone."
	tHalloween2015_PumpkinVSZombie_Text["UseInTime"] = "You can only use this item during the event time."
	tHalloween2015_PumpkinVSZombie_Text["UseWithTask"] = "You need to accept the quest before you can use this item."
	tHalloween2015_PumpkinVSZombie_Text["CompleteTask"] = "You`ve already completed this quest today. Come and play tomorrow."
	tHalloween2015_PumpkinVSZombie_Text["GoGetReward"] = "You`ve already killed 3 Dark Zombies. Go claim your reward!"
	
	tHalloween2015_PumpkinVSZombie_Text["UseBox"] = "Hurry and right click to open the Secret Treasure Chest!"
	tHalloween2015_PumpkinVSZombie_Text["Successful"] = "You`ve eliminated 3 Dark Zombies. Talk to the Baby Pumpkin to leave the garden, and claim your reward from the Old Gardener."
	tHalloween2015_PumpkinVSZombie_Text["UseItem"] = "Right click the magic item in your inventory to attack the zombies."
	

----------------------------------------------------------------------------------------
--Name:		151104[英文征服][任务脚本]国境任务调查问卷
--Purpose:		国境任务调查问卷
--Creator:		林辉山
--Created:		2015/11/04
----------------------------------------------------------------------------------------




tBorderTaskSurvey_Text = {}
tBorderTaskSurvey_Text["Survey"] = {}
tBorderTaskSurvey_Text["Survey"]["Text111"] = "We`re inviting heroes to complete an online questionnaire about the Realm Mission that will help us perfect this mission. Would you like to fill it out?"
tBorderTaskSurvey_Text["Survey"]["Option1"] = "Yes,~of~course."


tBorderTaskSurvey_Text["WebSite"] = "http://poll.91.com/survey.php?sv_id=733"
------------------------------------------------------------------------------------
--Name:		[征服][活动脚本]2015感恩节之蔓越橘竞赛（11.24-11.30）
--Purpose:	2015感恩节之蔓越橘竞赛（11.24-11.30）
--Creator: 	严振飞
--Created:	2015/08/04
------------------------------------------------------------------------------------

----------------------------------NPC对白--------------------------------------
tThanksGiving2015_Cranberry_Text = {}
------------------------------【小莓姑娘】---------------------------------------
tThanksGiving2015_Cranberry_Text[17176] = {}
-- 活动前
tThanksGiving2015_Cranberry_Text[17176]["Text111"] = "Cranberries bring good fortune. Recently, making stings of cranberries becomes quite popular."
tThanksGiving2015_Cranberry_Text[17176]["Text112"] = "~If you`ve reached Level 80 or got reborn, come and find me from Nov. 24th to 30th."
tThanksGiving2015_Cranberry_Text[17176]["Text113"] = "~I bet you`ll embrace luck, and also wonderful rewards."
tThanksGiving2015_Cranberry_Text[17176]["Option111"] = "I~can`t~wait~to~play!"
-- 活动后
tThanksGiving2015_Cranberry_Text[17176]["Text121"] = "People did enjoy themselves in the celebration. I`ve collected many smiles."
tThanksGiving2015_Cranberry_Text[17176]["Text122"] = "~I guess the magic of cranberries works."
tThanksGiving2015_Cranberry_Text[17176]["Option121"] = "Claim~the~prize~for~winner."
tThanksGiving2015_Cranberry_Text[17176]["Option122"] = "Luck~is~never~enough."

-- 活动中
tThanksGiving2015_Cranberry_Text[17176]["Text131"] = "Cranberries bring good fortune. Recently, making strings of cranberries becomes quite popular."
tThanksGiving2015_Cranberry_Text[17176]["Text132"] = "~If you`ve reached Level 80 or got reborn, come and find me for the stringing cranberries contest in Twin City"
tThanksGiving2015_Cranberry_Text[17176]["Text133"] = "~from Nov. 24th to 30th. In the contest, you`ll embrace luck, and also wonderful prizes."
tThanksGiving2015_Cranberry_Text[17176]["Text134"] = "~Your outstanding performance will also earn yourself extra rewards. Even better, the winner on the day"
tThanksGiving2015_Cranberry_Text[17176]["Text135"] = "~can claim a Dragon Ball on the next day! Don`t be late!"
tThanksGiving2015_Cranberry_Text[17176]["Option131"] = "Claim~yesterday`s~top~prize."
tThanksGiving2015_Cranberry_Text[17176]["Option132"] = "What`re~the~rules?"
tThanksGiving2015_Cranberry_Text[17176]["Option133"] = "View~the~ranking."
tThanksGiving2015_Cranberry_Text[17176]["Option134"] = "Claim~my~reward."
tThanksGiving2015_Cranberry_Text[17176]["Option135"] = "Claim~my~extra~reward."
tThanksGiving2015_Cranberry_Text[17176]["Option136"] = "I`ll~talk~to~you~later."

-- 我想了解一下比赛规则
tThanksGiving2015_Cranberry_Text[17176]["Text141"] = "There is a Fruit Bowl beside me. Check it well, and you`ll need it. In the contest, you`ll be given"
tThanksGiving2015_Cranberry_Text[17176]["Text142"] = "~60 seconds to string cranberries. Just a string of 15 cranberries is enough for a reward, and 20+ cranberries"
tThanksGiving2015_Cranberry_Text[17176]["Text143"] = "~for an extra reward. If someday you win the championship, you can claim a Dragon Ball on the next day."
tThanksGiving2015_Cranberry_Text[17176]["Text144"] = "~Remember, you should complete the cranberry string within a day."
tThanksGiving2015_Cranberry_Text[17176]["Option141"] = "How~to~string?"
tThanksGiving2015_Cranberry_Text[17176]["Option142"] = "Learn~about~other~things."

-- 应该怎么串蔓越橘呢？
tThanksGiving2015_Cranberry_Text[17176]["Text151"] = "I recommend you 3 methods: quick string, accurate string and skillful string. The difficulty and time cost"
tThanksGiving2015_Cranberry_Text[17176]["Text152"] = "~increase from left to right. The harder the method, the more cranberries you can string together,"
tThanksGiving2015_Cranberry_Text[17176]["Text153"] = "~but the more time to cost, and the lower the success rate. The result depends on your skill and luck."
tThanksGiving2015_Cranberry_Text[17176]["Text154"] = "~Don`t worry, it must be easy for smart heroes like you. Let`s play!"
tThanksGiving2015_Cranberry_Text[17176]["Option151"] = "I`m~ready."

-- 【我来领取奖励】
-- 等级不足
tThanksGiving2015_Cranberry_Text[17176]["Text211"] = "It seems you`re not strong enough for the contest. Keep practicing!"
tThanksGiving2015_Cranberry_Text[17176]["Option211"] = "Alright."

-- 蔓越橘没串15个以上
tThanksGiving2015_Cranberry_Text[17176]["Text221"] = "You should make a string of 15 cranberries before you can claim a reward from me."
tThanksGiving2015_Cranberry_Text[17176]["Option221"] = "Alright."

-- 已领取奖励
tThanksGiving2015_Cranberry_Text[17176]["Text231"] = "Come on, you`ve claimed your reward today, haven`t you?"
tThanksGiving2015_Cranberry_Text[17176]["Option231"] = "Sorry,~I~forgot."

-- 背包空间
tThanksGiving2015_Cranberry_Text[17176]["Text241"] = "You must be too lazy to check your inventory. It`s full, and you need to make some room, first."
tThanksGiving2015_Cranberry_Text[17176]["Option241"] = "I`ll~do~it~now."

-- 获得节日礼包
tThanksGiving2015_Cranberry_Text[17176]["Text251"] = "Good job! I knew you wouldn`t let me down. Here is a reward for you."
tThanksGiving2015_Cranberry_Text[17176]["Option251"] = "Thanks!"

-- 额外奖励（数量不够）
tThanksGiving2015_Cranberry_Text[17176]["Text261"] = "Hey, you didn`t string enough cranberries for the reward. Work harder, next time!"
tThanksGiving2015_Cranberry_Text[17176]["Option261"] = "Alright."

-- 额外奖励（已领取奖励）
-- 已领取奖励
tThanksGiving2015_Cranberry_Text[17176]["Text271"] = "Buddy, you`ve claimed the extra reward, today."
tThanksGiving2015_Cranberry_Text[17176]["Option271"] = "Oh,~my~fault."

-- 额外奖励（经验）
tThanksGiving2015_Cranberry_Text[17176]["Text281"] = "Excellent! You successfully made a string of %d cranberries."
tThanksGiving2015_Cranberry_Text[17176]["Text282"] = "~I`ll reward you 60 minutes of EXP."
tThanksGiving2015_Cranberry_Text[17176]["Option281"] = "Thanks!"

-- 额外奖励（修行值）
tThanksGiving2015_Cranberry_Text[17176]["Text291"] = "Great! You successfully made a string of %d cranberries. Since you`re at the max level,"
tThanksGiving2015_Cranberry_Text[17176]["Text292"] = "~I`ll reward you 50 Study Points."
tThanksGiving2015_Cranberry_Text[17176]["Option291"] = "Thanks!"


-- 【查看排行榜】
-- 第一名（第一天）
tThanksGiving2015_Cranberry_Text[17176]["Text311"] = "Wow, you rank the top with a score of %d. You do have a gift for it."
tThanksGiving2015_Cranberry_Text[17176]["Option311"] = "Thanks."

-- 第一名（非第一天）
tThanksGiving2015_Cranberry_Text[17176]["Text321"] = "Currently, you rank the top with a score of %d. You do surprise me!"
tThanksGiving2015_Cranberry_Text[17176]["Option321"] = "Claim~yesterday`s~top~prize."

-- 今天未参加比赛
tThanksGiving2015_Cranberry_Text[17176]["Text331"] = "My hero, you should participate in today`s contest, first."
tThanksGiving2015_Cranberry_Text[17176]["Option331"] = "Okay."

-- 非第一名（第一天）
tThanksGiving2015_Cranberry_Text[17176]["Text341"] = "Let me see... Your score is %d, and"
tThanksGiving2015_Cranberry_Text[17176]["Text342"] = "~%s ranks the 1st Place with a score of %d. Try harder to take the first place!"
tThanksGiving2015_Cranberry_Text[17176]["Option341"] = "I~will!"

-- 非第一名（非第一天）
tThanksGiving2015_Cranberry_Text[17176]["Text351"] = "So far, your score is %d, and"
tThanksGiving2015_Cranberry_Text[17176]["Text352"] = "~%s ranks the 1st Place with a score of %d. Try harder to take the first place!"
tThanksGiving2015_Cranberry_Text[17176]["Option351"] = "Claim~yesterday`s~top~prize."
tThanksGiving2015_Cranberry_Text[17176]["Option352"] = "I~will!"

-- 【领取昨天冠军奖励】
-- 不是昨天冠军
tThanksGiving2015_Cranberry_Text[17176]["Text411"] = "Hey, you`re not the champion. According to the record, %s ranked the 1st place yesterday with a score of %d."
tThanksGiving2015_Cranberry_Text[17176]["Option411"] = "I~see."

-- 已领取奖励
tThanksGiving2015_Cranberry_Text[17176]["Text421"] = "Sorry, you`ve already claimed your prize. No more for you, today."
tThanksGiving2015_Cranberry_Text[17176]["Option421"] = "Okay."

-- 背面空间
tThanksGiving2015_Cranberry_Text[17176]["Text431"] = "I can`t put anything into a full bag. You need to clear some room in your inventory, first."
tThanksGiving2015_Cranberry_Text[17176]["Option431"] = "I`ll~do~it~now."

-- 获得昨天冠军奖励
tThanksGiving2015_Cranberry_Text[17176]["Text441"] = "You`re brilliant! You deserve the Dragon Ball! Take it."
tThanksGiving2015_Cranberry_Text[17176]["Option441"] = "Thanks!"

-- 不存在昨天冠军
tThanksGiving2015_Cranberry_Text[17176]["Text451"] = "Sorry, you can`t be yesterday`s champion since there was no participants for yesterday`s contest."
tThanksGiving2015_Cranberry_Text[17176]["Option451"] = "What?!"


------------------------------【小果盆】---------------------------------------
-- 等级不足
tThanksGiving2015_Cranberry_Text[17177] = {}
tThanksGiving2015_Cranberry_Text[17177]["Text111"] = "I`m a beautiful fruit bowl filled with sweet cranberries. You should take me well. (Make sure you`ve reached Level 80.)"
tThanksGiving2015_Cranberry_Text[17177]["Option111"] = "I~see."

-- 活动外
tThanksGiving2015_Cranberry_Text[17177]["Text121"] = "I`m a beautiful fruit bowl filled with sweet cranberries. You should take me well."
tThanksGiving2015_Cranberry_Text[17177]["Option121"] = "I~see."


-- 活动中对白
tThanksGiving2015_Cranberry_Text[17177]["Text131"] = "I`ve prepared you abundant resource, and the flame of competition has been lighten up. Waste no more time, go fight and compete!"
tThanksGiving2015_Cranberry_Text[17177]["Option131"] = "What`re~the~rules?"
tThanksGiving2015_Cranberry_Text[17177]["Option132"] = "Let`s~go!"
tThanksGiving2015_Cranberry_Text[17177]["Option133"] = "What~a~talkative~thing."

-- 规则是什么样的呢？
tThanksGiving2015_Cranberry_Text[17177]["Text141"] = "When the contest starts, you have 60 seconds to string cranberries. As long as you can string 15 cranberries"
tThanksGiving2015_Cranberry_Text[17177]["Text142"] = "~together, you can claim a reward from the Cherry Girl. If you make a string of 20+ cranberries, you`ll get"
tThanksGiving2015_Cranberry_Text[17177]["Text143"] = "~an extra reward. What`s better, the champion on a day will be rewarded with a Dragon Ball on the next day by the Cherry Girl!"
tThanksGiving2015_Cranberry_Text[17177]["Option141"] = "How~to~string?"
tThanksGiving2015_Cranberry_Text[17177]["Option142"] = "Learn~about~other~things."

-- 应该怎么串蔓越橘呢？
tThanksGiving2015_Cranberry_Text[17177]["Text151"] = "There are 3 methods: quick string, accurate string and skillful string. The difficulty and time cost"
tThanksGiving2015_Cranberry_Text[17177]["Text152"] = "~increase from left to right. The harder the method, the more cranberries you can string together,"
tThanksGiving2015_Cranberry_Text[17177]["Text153"] = "~but the more time to cost, and the lower the success rate. The result depends on your skill and luck."
tThanksGiving2015_Cranberry_Text[17177]["Text154"] = "~Got it?"
tThanksGiving2015_Cranberry_Text[17177]["Option151"] = "Yes.~Let`s~begin!"

-- 第一次串
tThanksGiving2015_Cranberry_Text[17177]["Text161"] = "Hi, you look able. Are you ready? Just string 15 cranberries together, and you can claim a reward."
tThanksGiving2015_Cranberry_Text[17177]["Text162"] = "~So, which method would you want to take?"
tThanksGiving2015_Cranberry_Text[17177]["Text163"] = ""
tThanksGiving2015_Cranberry_Text[17177]["Option161"] = "Quick~string."
tThanksGiving2015_Cranberry_Text[17177]["Option162"] = "Accurate~string."
tThanksGiving2015_Cranberry_Text[17177]["Option163"] = "Skillful~string."

-- 第二次以后串
tThanksGiving2015_Cranberry_Text[17177]["Text171"] = "Good job! You`ve strung %d cranberries together. So, which method would you want to take now?"
tThanksGiving2015_Cranberry_Text[17177]["Option171"] = "Quick~string."
tThanksGiving2015_Cranberry_Text[17177]["Option172"] = "Accurate~string."
tThanksGiving2015_Cranberry_Text[17177]["Option173"] = "Skillful~string."

------------------------------其他
-- 额外奖励提示
tThanksGiving2015_Cranberry_Text["AddReward"] = {}
tThanksGiving2015_Cranberry_Text["AddReward"][4] = "You received 60 minutes of EXP!"
tThanksGiving2015_Cranberry_Text["AddReward"][6] = "You received 50 Study Points!"
-- 昨天冠军奖励
tThanksGiving2015_Cranberry_Text["YdayReward"] = "You received a Dragon Ball!"
-- 倒计时结束
tThanksGiving2015_Cranberry_Text["NextDay"] = "You failed to complete the cranberry string within a day, and you`re disqualified for a reward."
tThanksGiving2015_Cranberry_Text["TaskEnd"] = "Time is up. You made a string of %d cranberries. You need to practice more."

-- 读条文字
tThanksGiving2015_Cranberry_Text["Explore"] = {}
tThanksGiving2015_Cranberry_Text["Explore"][1] = "Quick~stringing..."
tThanksGiving2015_Cranberry_Text["Explore"][2] = "Accurate~string..."
tThanksGiving2015_Cranberry_Text["Explore"][3] = "Skillful~string..."
tThanksGiving2015_Cranberry_Text["Explore"]["Stop"] = "Timeout"

-- 串蔓越橘结束
tThanksGiving2015_Cranberry_Text["TaskExplore"] = {}
--快速串
tThanksGiving2015_Cranberry_Text["TaskExplore"][1] = {}
tThanksGiving2015_Cranberry_Text["TaskExplore"][1]["2"] = "You want things to be quick, so you took `quick string` method, and successfully strung 2 cranberries together."
tThanksGiving2015_Cranberry_Text["TaskExplore"][1]["1"] = "You want things to be quick, so you took `quick string` method, and you strung 1 cranberry."
tThanksGiving2015_Cranberry_Text["TaskExplore"][1]["0"] = "You want things to be quick, so you took `quick string` method, however the cranberry slipped out of your hand. No cranberry strung."
tThanksGiving2015_Cranberry_Text["TaskExplore"][1]["-1"] = "You want things to be quick, so you took `quick string` method, however you carelessly lost one cranberry from the string."
--认真串
tThanksGiving2015_Cranberry_Text["TaskExplore"][2] = {}
tThanksGiving2015_Cranberry_Text["TaskExplore"][2]["2"] = "`Accurate string` is the safest method. You successfully strung 2 cranberries together."
tThanksGiving2015_Cranberry_Text["TaskExplore"][2]["1"] = "`Accurate string` is the safest method. You successfully strung 1 cranberry."
--技巧串
tThanksGiving2015_Cranberry_Text["TaskExplore"][3] = {}
tThanksGiving2015_Cranberry_Text["TaskExplore"][3]["5"] = "To string more cranberries together, you take some special techniques. Excellent, you successfully strung 5 cranberries together!"
tThanksGiving2015_Cranberry_Text["TaskExplore"][3]["3"] = "To string more cranberries together, you take some special techniques. Great, you successfully strung 3 cranberries together!"
tThanksGiving2015_Cranberry_Text["TaskExplore"][3]["2"] = "To string more cranberries together, you take some special techniques. Good job, you successfully strung 2 cranberries together!"
tThanksGiving2015_Cranberry_Text["TaskExplore"][3]["-1"] = "To string more cranberries together, you take some special techniques. However, you carelessly lost 1 cranberry."


------------------------------------------------------------------------------------
--Name:			150616[简体征服][活动脚本]感恩节（11.24-11.30）-猩猩投果子
--Purpose:		感恩节（11.24-11.30）-猩猩投果子
--Creator:		黄昕哲
--Created:		2015/03/16
------------------------------------------------------------------------------------
tThanksGiving2015_ThrowFruit_Explorer = {}
tThanksGiving2015_ThrowFruit_Explorer["Choose"] = "PickingFruit"
tThanksGiving2015_ThrowFruit_Explorer["Throwing"] = "Shooting"

tThanksGiving2015_ThrowFruit_Text = {}
tThanksGiving2015_ThrowFruit_Text[17195] = {}
tThanksGiving2015_ThrowFruit_Text[17195]["Text111"] = "We`ve been waiting for Thanksgiving for a year, but our village head decided not to hold a celebration this year. Anyway, I`ll carry out a fruit shooting game here."
tThanksGiving2015_ThrowFruit_Text[17195]["Text112"] = "~Fom Nov. 24th to 30th, all Level 80+ or reborn heroes are welcome to compete here. Tons of rewards are ready for you."
tThanksGiving2015_ThrowFruit_Text[17195]["Option1"] = "I`ll~come~to~you~then."

tThanksGiving2015_ThrowFruit_Text[17195]["Text121"] = "Thanksgiving has arrived! Level 80+ heroes are welcome to play fruit shooting game between Nov. 24th and 30th. See, I`ve prepared wonderful rewards for the winners!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option5"] = "What`re~the~rules?"
tThanksGiving2015_ThrowFruit_Text[17195]["Option6"] = "I`m~in!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option7"] = "Claim~my~reward."
tThanksGiving2015_ThrowFruit_Text[17195]["Option8"] = "Claim~my~extra~reward."
tThanksGiving2015_ThrowFruit_Text[17195]["Option9"] = "View~the~ranking."
tThanksGiving2015_ThrowFruit_Text[17195]["Option10"] = "I`ll~talk~to~you~later."

tThanksGiving2015_ThrowFruit_Text[17195]["Text131"] = "People in Twin City are really friendly and active. I`ve received many blessings. I believe next year will better."
tThanksGiving2015_ThrowFruit_Text[17195]["Option2"] = "Claim~the~champion~prize."
tThanksGiving2015_ThrowFruit_Text[17195]["Option3"] = "Wish~you~big~harvest!"

--想了解一下规则
tThanksGiving2015_ThrowFruit_Text[17195]["Text211"] = "When the game starts, you`ll become an ape. You need to stand inside the fence, and throw fruits to the empty containers outside the fence. If you get 10 points within 1 minute, you can get a reward,"
tThanksGiving2015_ThrowFruit_Text[17195]["Text212"] = "~and 20 points for an extra reward. The smaller the container, the harder you can hit. The champion can claim a DB from me on the next day. Remember, you can claim each reward once in a day."
tThanksGiving2015_ThrowFruit_Text[17195]["Option11"] = "How~to~score?"
tThanksGiving2015_ThrowFruit_Text[17195]["Option12"] = "Learn~about~other~things."

tThanksGiving2015_ThrowFruit_Text[17195]["Text311"] = "Look at the containers outside the fence. Small Bathtub gives 1 point. Small Stove gives 2 points. Small Vase gives 3 points."
tThanksGiving2015_ThrowFruit_Text[17195]["Option13"] = "Got~it!"
--我来参赛了！
tThanksGiving2015_ThrowFruit_Text[17195]["Text221"] = "I do love turning heroes into apes. Well, you`ll have 1 minute for the game. Take 10 fruits on the table, and"
tThanksGiving2015_ThrowFruit_Text[17195]["Text222"] = "~throw them to the empty containers on the ground. When your score a certain amount of points, you can claim"
tThanksGiving2015_ThrowFruit_Text[17195]["Text223"] = "~a reward from me. The higher your score, the more you`ll receive. Ready!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option14"] = "I`m~ready~to~be~an~ape!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option15"] = "Not~yet."

tThanksGiving2015_ThrowFruit_Text[17195]["Text321"] = "Come on, you`re still too young. Go back to your training."
tThanksGiving2015_ThrowFruit_Text[17195]["Text322"] = ""
tThanksGiving2015_ThrowFruit_Text[17195]["Option16"] = "Alright."

tThanksGiving2015_ThrowFruit_Text[17195]["Text331"] = "You`re already an ape, and the game has started. Hurry up!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option17"] = "Okay."


--领取奖品
tThanksGiving2015_ThrowFruit_Text[17195]["Text231"] = "It seems you failed to score 10 points. Sorry, there is no reward for that."
tThanksGiving2015_ThrowFruit_Text[17195]["Option19"] = "Alright."

tThanksGiving2015_ThrowFruit_Text[17195]["Text241"] = "You`ve claimed your reward today, haven`t you?"
tThanksGiving2015_ThrowFruit_Text[17195]["Option20"] = "Sorry,~I~forgot."


tThanksGiving2015_ThrowFruit_Text[17195]["Text251"] = "Don`t show me your full bag. You need to clear some room in your inventory, first."
tThanksGiving2015_ThrowFruit_Text[17195]["Option21"] = "I`ll~do~it~now."

tThanksGiving2015_ThrowFruit_Text[17195]["Text261"] = "Young is energetic. See, I have a reward for you."
tThanksGiving2015_ThrowFruit_Text[17195]["Option22"] = "Thanks!"

--领取额外奖励
tThanksGiving2015_ThrowFruit_Text[17195]["Text411"] = "It seems you failed to score 20 points within 1 minute. Sorry, there is no extra reward for that."
tThanksGiving2015_ThrowFruit_Text[17195]["Option23"] = "Alright."

tThanksGiving2015_ThrowFruit_Text[17195]["Text421"] = "Am I wrong? You`ve claimed your reward today, haven`t you?"
tThanksGiving2015_ThrowFruit_Text[17195]["Option24"] = "Sorry,~I~forgot."

tThanksGiving2015_ThrowFruit_Text[17195]["Text431"] = "Great job! You scored %s points in the shooting game. I`ll reward you extra 60 minutes of EXP."
tThanksGiving2015_ThrowFruit_Text[17195]["Option25"] = "Thanks!"

tThanksGiving2015_ThrowFruit_Text[17195]["Text441"] = "Great job! You scored %s points in the shooting game. I`ll reward you extra 50 Study Points."
tThanksGiving2015_ThrowFruit_Text[17195]["Option26"] = "Thanks!"
--查看排行榜
tThanksGiving2015_ThrowFruit_Text[17195]["Text511"] = "Wow, you scored %s points and ranked the 1st Place! I love you!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option27"] = "Claim~yesterday`s~top~prize."
tThanksGiving2015_ThrowFruit_Text[17195]["Option28"] = "Thanks."

tThanksGiving2015_ThrowFruit_Text[17195]["Text521"] = "You`ve scored %s, ~%s ranks the top with a score of %s points. Work harder to claim the top place!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option29"] = "Claim~yesterday`s~top~prize."
tThanksGiving2015_ThrowFruit_Text[17195]["Option30"] = "I`ll~try~my~best."

tThanksGiving2015_ThrowFruit_Text[17195]["Text531"] = "Currently, %s ranks the 1st place with a score of %s. Try your hardest to surpass him/her!"

tThanksGiving2015_ThrowFruit_Text[17195]["Text541"] = "You need to play this game at least once before you can check the ranking."
tThanksGiving2015_ThrowFruit_Text[17195]["Option32"] = "I`ll~talk~to~you~later."

tThanksGiving2015_ThrowFruit_Text[17195]["Text611"] = "Wait, you`re not the champion. It was %s ranking the 1st place yesterday with a score of %s points."
tThanksGiving2015_ThrowFruit_Text[17195]["Option33"] = "I~see."

tThanksGiving2015_ThrowFruit_Text[17195]["Text621"] = "Why you again? You`ve claimed your reward today, haven`t you?"
tThanksGiving2015_ThrowFruit_Text[17195]["Option34"] = "Sorry,~I~forgot."

tThanksGiving2015_ThrowFruit_Text[17195]["Text631"] = "Your inventory is full. Make some room, first."
tThanksGiving2015_ThrowFruit_Text[17195]["Option35"] = "I`ll~do~it~now."

tThanksGiving2015_ThrowFruit_Text[17195]["Text641"] = "You really impressed me! Take the Dragon Ball!"
tThanksGiving2015_ThrowFruit_Text[17195]["Option36"] = "Thanks!"

tThanksGiving2015_ThrowFruit_Text[17195]["Text651"] = "Sorry, you can`t be yesterday`s champion since there was no participants for yesterday`s contest."
tThanksGiving2015_ThrowFruit_Text[17195]["Option37"] = "Oops..."

tThanksGiving2015_ThrowFruit_Text[17265] = {}
tThanksGiving2015_ThrowFruit_Text[17265]["Text111"] = "People just say I`m lucky to carry so many fruits, but who really understand my pains. It`s really...really... heavy!"
tThanksGiving2015_ThrowFruit_Text[17265]["Option1"] = "I~understand."
tThanksGiving2015_ThrowFruit_Text[17265]["Text121"] = "Come on, the fruits are really...really... heavy!"
tThanksGiving2015_ThrowFruit_Text[17265]["Text122"] = "~Hurry and take fruits one by one for the shooting game."
tThanksGiving2015_ThrowFruit_Text[17265]["Option2"] = "Take~a~fruit."
tThanksGiving2015_ThrowFruit_Text[17265]["Option3"] = "I`ll~think~about~it."

tThanksGiving2015_ThrowFruit_Text[17265]["Text211"] = "You still have fruits with you. Look at the 3 containers on the upper right. Hurry and hit them!"
tThanksGiving2015_ThrowFruit_Text[17265]["Option4"] = "Okay."

tThanksGiving2015_ThrowFruit_Text[17266] = {}
tThanksGiving2015_ThrowFruit_Text[17266]["Text111"] = "I just want to live in peace. Why do you people shoot at me without a stop?"
tThanksGiving2015_ThrowFruit_Text[17266]["Option1"] = "It`s~interesting."
tThanksGiving2015_ThrowFruit_Text[17266]["Text121"] = "Hit me! Hit me! I`ll give 1 point for 1 hit!"
tThanksGiving2015_ThrowFruit_Text[17266]["Option2"] = "I~need~to~calm~down."
tThanksGiving2015_ThrowFruit_Text[17266]["Option3"] = "You`re~not~my~target."

tThanksGiving2015_ThrowFruit_Text[17267] = {}
tThanksGiving2015_ThrowFruit_Text[17267]["Text111"] = "Are you sure? How could you hit such a cute stove like me?"
tThanksGiving2015_ThrowFruit_Text[17267]["Option1"] = "Yeah,~you`re~beautiful."
tThanksGiving2015_ThrowFruit_Text[17267]["Text121"] = "Hurry, hurry! I`m ready for your fruits! 2 points for one hit! So far, you`ve earned %s points."
tThanksGiving2015_ThrowFruit_Text[17267]["Option2"] = "Let~me~hit~you!"
tThanksGiving2015_ThrowFruit_Text[17267]["Option3"] = "You`re~not~my~target."

tThanksGiving2015_ThrowFruit_Text[17268] = {}
tThanksGiving2015_ThrowFruit_Text[17268]["Text111"] = "Please... Give me mercy. Don`t hit me! You may break me."
tThanksGiving2015_ThrowFruit_Text[17268]["Option1"] = "You`re~so~small."
tThanksGiving2015_ThrowFruit_Text[17268]["Text121"] = "Hurry, feed me fruits. I love that. Look, 3 points for a hit! So far, you`ve earned %s points."
tThanksGiving2015_ThrowFruit_Text[17268]["Option2"] = "Get~ready~for~a~hit!"
tThanksGiving2015_ThrowFruit_Text[17268]["Option3"] = "You`re~not~my~target."


tThanksGiving2015_ThrowFruit_Text["Msg1"] = "You have no fruit in your inventory. Hurry and take some from the table."
tThanksGiving2015_ThrowFruit_Text["Msg2"] = "You should stand inside the fence before you can start shooting."

tThanksGiving2015_ThrowFruit_Text["Msg3"] = "Thanksgiving celebration is over, and you threw the fruits away."
tThanksGiving2015_ThrowFruit_Text["Msg4"] = "There is nothing here for you to throw the fruits."



tThanksGiving2015_ThrowFruit_Text["TransformEnd1"] = "You`ve exerted all your strength, but only scored %s points which is not enough for a reward. Talk to Farmer Tu to try again."
tThanksGiving2015_ThrowFruit_Text["TransformEnd2"] = "You threw fruits fast and accurately. After 1 minute, you scored %s points. Go and claim your reward from Farmer Tu."
tThanksGiving2015_ThrowFruit_Text["TransformEnd3"] = "Excellent! You scored %s points! Go claim an extra reward from Farmer Tu!"


tThanksGiving2015_ThrowFruit_Text["TalkChannel1"] = "The game has started! Hurry and take fruits on the table! Remember, you only have 1 minute for the game!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel2"] = "You received 1 Festival Joy Pack!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel3"] = "You received 60 minutes of EXP!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel4"] = "You received 50 Study Points!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel5"] = "You received 1 Dragon Ball!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel6"] = "Your inventory is full. Please make some room, first."
tThanksGiving2015_ThrowFruit_Text["TalkChannel7"] = "You took %s fruit(s). Hurry and shoot!"

tThanksGiving2015_ThrowFruit_Text["TalkChannel11"] = "You successfully threw a fruit into the Small Bathtub, and earned 1 point!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel12"] = "You`re lucky to throw in a fruit and earned 1 point."
tThanksGiving2015_ThrowFruit_Text["TalkChannel13"] = "What a pity, you missed the target. Try again!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel14"] = "You became angry since you missed the target and threw the fruit to a place far away."
tThanksGiving2015_ThrowFruit_Text["TalkChannel15"] = "You shot too hard and fell down."
tThanksGiving2015_ThrowFruit_Text["TalkChannel16"] = "Oops, you shot too hard and fell down, losing 20% HP."


tThanksGiving2015_ThrowFruit_Text["TalkChannel21"] = "You successfully threw a fruit into the Small Stove, and earned 2 points!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel22"] = "You`re lucky to throw in a fruit and earned 2 points."
tThanksGiving2015_ThrowFruit_Text["TalkChannel23"] = "What a pity, you missed the target. Try again!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel24"] = "You became angry since you missed the target and threw the fruit to a place far away."
tThanksGiving2015_ThrowFruit_Text["TalkChannel25"] = "You shot too hard and fell down."
tThanksGiving2015_ThrowFruit_Text["TalkChannel26"] = "Oops, you shot too hard and fell down, losing 20% HP."

tThanksGiving2015_ThrowFruit_Text["TalkChannel31"] = "You successfully threw a fruit into the Small Vase, and earned 3 points!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel32"] = "You`re lucky to throw in a fruit and earned 3 points."
tThanksGiving2015_ThrowFruit_Text["TalkChannel33"] = "What a pity, you missed the target. Try again!"
tThanksGiving2015_ThrowFruit_Text["TalkChannel34"] = "You became angry since you missed the target and threw the fruit to a place far away."
tThanksGiving2015_ThrowFruit_Text["TalkChannel35"] = "You shot too hard and fell down."
tThanksGiving2015_ThrowFruit_Text["TalkChannel36"] = "Oops, you shot too hard and fell down, losing 20% HP."



------------------------------------------------------------------------------------
--Name:			150826[简体征服][活动脚本]2015感恩节-火鸡总动园（11.24-11.30）
--Purpose:		火鸡总动园（11.24-11.30）
--Creator:		王倩娜
--Created:		2015-08-26
------------------------------------------------------------------------------------
tThanksGiving2015_FreeBirdsIntMacth_Text = {}

	tThanksGiving2015_FreeBirdsIntMacth_Text[17224] = {}

		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text111"] = "Don`t ask who I am, but what I do here. I`m going to serve people with hundreds of turkey dishes"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text112"] = "~in different flavors. However, I don`t have that many turkeys. If you`re free, come and help me"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text113"] = "~from Nov. 24th to 30th. I await you!"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option1"] = "See~you~then."

		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text121"] = "Don`t ask who I am, but what I do here. I`m going to serve people with the mythical `Mushroom Turkey` dishes."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text122"] = "~However, I don`t have enough turkeys. If you`ve reached Level 80 or got reborn, come and help"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text123"] = "~me capture turkeys at 10:00 - 10:30, 12:00 - 12:30, 18:00 - 18:30 and 22:00 - 22:30, from"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text124"] = "~Nov. 24th to 30th."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option2"] = "Count~on~me!"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option3"] = "Claim~my~reward."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option4"] = "Claim~yesterday`s~top~prize."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option5"] = "View~capture~info."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option6"] = "How~to~capture?"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option7"] = "No~love~for~turkey."

		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text131"] = "Thanks for your helps, and I received tons of praises from people on my `Mushroom Turkey`."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text132"] = ""
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option8"] = "Claim~yesterday`s~top~prize."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option9"] = "You`re~welcome."

		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text211"] = "The Turkey Park will be open at 10:00 - 10:30, 12:00 - 12:30, 18:00 - 18:30 and 22:00 - 22:30,"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text212"] = "~everyday during the celebration. Go kill turkeys in the Park to earn points. You`ll gain 1 point"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text213"] = "~for killing a Normal Turkey, 2 points for a Raging Turkey, 3 points for an Angry Turkey, and 5 points for"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text214"] = "~a Burning Turkey. Look, I have a reward for those who earn totally 300 points, and a top prize for the champion with the highest score."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option10"] = "Sounds~great."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text311"] = "Shh, turkeys are still in bed. Only when living in comfortable environment, the turkeys taste good."
        tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text312"] = "~I`ll let you in at 10:00 - 10:30, 12:00 - 12:30, 18:00 - 18:30 and 22:00 - 22:30,"
        tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text313"] = "~everyday during the celebration."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option11"] = "Alright."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text321"] = "Buddy, you`re still too green to deal with the naughty turkeys. Keep practicing, and come back"
        tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text322"] = "~when you reach Level 80 or get reborn."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option12"] = "Alright."

		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text331"] = "Sorry, the Turkey Park is full. Be early next time."
        tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text332"] = ""
        tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option13"] = "Alright."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text341"] = "You`ve claimed your reward. Why not leave the chance to others?"
		-- tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text342"] = "确定入内吗？"
		-- tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option14"] = "确定。"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option15"] = "You`re~right."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text351"] = "Once entering the Park, you can capture turkeys as many as you like. It`s 1 point for killing 1 Normal Turkey, 2 points for 1 Raging Turkey, 3 points for"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text352"] = "~1 Angry Turkey, and 5 points for 1 Burning Turkey. You need to earn totally 300 points within 30 minutes after the hour to claim a reward from me."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text353"] = "~For the champion with the highest score, I`ve also prepared a top prize.If you leave now, you can`t enter the Turkey Park at this round."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option16"] = "No~problem."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text411"] = "Sorry, you haven`t earned 300 points, and I can`t give you the reward."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text412"] = ""
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option17"] = "Alright."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text421"] = "You`ve claimed your reward. Don`t tell me you want to taste more turkey."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option18"] = "Alright."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text431"] = "Ah, you brought me so many turkeys! Thanks! Here`s the reward for you, or you want to taste my turkey soup?"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text432"] = ""
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option19"] = "Oh,~I~want~my~reward."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text511"] = "Wait, yesterday`s top prize has already been claimed. You claimed it, didn`t you?"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option20"] = "Sorry,~I~forgot."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text521"] = "Sorry, you`re not the champion yesterday, so I can`t give you the reward."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text522"] = "~It`s easy to understand, right?"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option21"] = "Bye,~bye."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text531"] = "You did me a good service yesterday! I`ll give you 500 minutes of EXP."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text532"] = "Would you like to taste my turkey soup?"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option22"] = "Soup?~No,~thank!"

		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text541"] = "You did me a good service yesterday! I`ll give you 250 Study Points."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text542"] = "~Would you like to taste my turkey soup?"

		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text611"] = "You`ve earned %d point(s) from hunting turkeys."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text612"] = "~The championship has not been claimed, yet. Work for the big prize on the next day!"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Text613"] = "~Currently, [%s] ranks the 1st place, with a store of %d points."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17224]["Option23"] = "Got~it."
		
		
	tThanksGiving2015_FreeBirdsIntMacth_Text[17225] = {}
	
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Text111"] = "You`ll be given 30 minutes to capture turkeys and earn points in the Park. It`s 1 point for killing 1 Normal Turkey,"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Text112"] = "~2 points for 1 Raging Turkey, 3 points for 1 Angry Turkey, and 5 points for 1 Burning Turkey."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Text113"] = "~As long as you earn totally 300 points, you can claim a reward from me. For the champion with the highest score, I`ve prepared a top prize."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Option1"] = "Leave~the~Park."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Option2"] = "View~capture~info."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Option3"] = "I~need~a~break."
		
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Text121"] = "If you leave the park now, you can`t enter here again at this round. Have you decided?"
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Option4"] = "I~have~to~go."
		tThanksGiving2015_FreeBirdsIntMacth_Text[17225]["Option5"] = "I`d~like~to~stay."
		                                                          
	tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"] = {}	
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["BagSpace"] = "Your inventory is too full to contain more items. Please make some room, first."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["MoveToMap"] = {}
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["MoveToMap"][17224] = "You entered the Turkey Park, and the turkeys welcome you by pecking."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["MoveToMap"][17225] = "You`ve left the Turkey Park, and felt so quiet."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["DaoJiShi"] = "The turkeys are going to sleep, and you were teleported our of the park."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["Tukey"] = {}
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["Tukey"][7631] = "The Normal Turkey fell down under your weapon, and you gained 1 point.Currently, you`ve earned totally %d points."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["Tukey"][7632] = "You knocked a Raging Turkey down, and gained 2 points.Currently, you`ve earned totally %d points."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["Tukey"][7633] = "You killed an Angry Turkey, and gained 3 points.Currently, you`ve earned totally %d points."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["Tukey"][7634] = "The Burning Turkey fell down under your weapon, and you gained 5 points.Currently, you`ve earned totally %d points."
		tThanksGiving2015_FreeBirdsIntMacth_Text["MsgText"]["CountTips"] = "You`ve earned over 300 points. Go and claim a reward from the Royal Chef in the Turkey Park."
		
-----------------------------------------------------------------------------------------------------
--Name:			150810[英文征服][活动脚本]感恩节之特赦火鸡(11.24-11.30)
--Creator: 		许乐
--Created:		2015/08/10
------------------------------------------------------------------------------------------------------
-- 命名前缀
--Thanksgiving2015_AbsolveTurkey_

-- 17226  火鸡协会主席
tThanksgiving2015_AbsolveTurkey_Text = {}
	tThanksgiving2015_AbsolveTurkey_Text[17226] = {}
	--活动前对白
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text111"] = "Traditionally, Thanksgiving dinner always includes turkey. To show thanksgiving, we avoid"
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text112"] = "~a turkey from being a dish, every year. Now, we would like to give the right to you. Come"
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text113"] = "~and pick the lucky turkey from Nov. 24th to 30th."
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Option1"] = "It`s~interesting."

	--活动中对白
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text121"] = "Traditionally, Thanksgiving dinner always includes turkey. To show thanksgiving, we avoid"
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text122"] = "~a turkey from being a dish, every year. Now, we would like to give the right to you. All heroes"
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text123"] = "~above Level 80 or got reborn are able to pick the lucky turkey from Nov. 24th to 30th."
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Option3"] = "Show~me~the~turkeys."
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Option4"] = "I`ll~frighten~the~turkeys."

	--活动后对白
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text131"] = "For your mercy, we made an exception and avoided 3 turkeys from being a dish this year."
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text132"] = "~They`ll live in a luxury life."
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Option2"] = "Good."

	--接Option3：请给我介绍介绍它们吧！
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text311"] = "They`re the best turkeys, and live in a comfortable life with music, movies, candies, and soft beds."	
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text312"] = "~The Sunny Turkey in red is a reincarnation of hope. The Flying Turkey grows blue feather, symbolizing the peace."	
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text313"] = "~The Rocket Turkey with green feather delivers a sense of rebirth."	
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Text314"] = ""	
	tThanksgiving2015_AbsolveTurkey_Text[17226]["Option5"] = "Beautiful~things."

-- 17227  向阳火鸡
tThanksgiving2015_AbsolveTurkey_Text[17227] = {}
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Text111"] = "This is a %s."
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Option1"] = "It`s~cute."
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Option2"] = "Avoid~this~turkey."
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Option3"] = "Go~check~other~turkeys."

	--接Option2：赦免它
	--成功赦免
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Text211"] = "You successfully avoided %s from being a dish, and you received a reward from the Turkey Club.To thank you,  the %s wants to be with you."
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Text212"] = ""
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Option4"] = "Great!"
	--已赦免过，无法赦免
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Text221"] = "You`ve already picked a turkey. No more chances, today."
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Option5"] = "Alright."
	--等级不足
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Text231"] = "You`re not strong enough to decide the fate of turkeys."
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Option6"] = "Alright."
	--背包已满
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Text241"] = "Your inventory is full. You need to clear at least 2 empty spaces, first."
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Option7"] = "I`ll~do~it~now."
	--不在活动时间
	tThanksgiving2015_AbsolveTurkey_Text[17227]["Text251"] = "This is not the correct time to avoid a turkey. Be patient, please."

	
	tThanksgiving2015_AbsolveTurkey_Text["Turkey"] = {}
	tThanksgiving2015_AbsolveTurkey_Text["Turkey"][17227] = "red Sunny Turkey which is a reincarnation of hope"
	tThanksgiving2015_AbsolveTurkey_Text["Turkey"][17228] = "blue Flying Turkey which symbolizes the peace"
	tThanksgiving2015_AbsolveTurkey_Text["Turkey"][17229] = "Rocket Turkey with green feather which delivers a sense of rebirth"
	
	tThanksgiving2015_AbsolveTurkey_Text["Name"] = {}
	tThanksgiving2015_AbsolveTurkey_Text["Name"]["Furniture"] = "ThanksgivingTurkey"
	tThanksgiving2015_AbsolveTurkey_Text["Name"][17227] = "SunnyTurkey"
	tThanksgiving2015_AbsolveTurkey_Text["Name"][17228] = "FlyingTurkey"
	tThanksgiving2015_AbsolveTurkey_Text["Name"][17229] = "RocketTurkey"

 --3007288  火鸡家具
tThanksgiving2015_AbsolveTurkey_Text[3007288] = {}
	tThanksgiving2015_AbsolveTurkey_Text[3007288]["Text111"] = "Do you want to pack up the Thanksgiving Turkey?"
	tThanksgiving2015_AbsolveTurkey_Text[3007288]["Option1"] = "Yes."
	
--1010 2005提示	
	tThanksgiving2015_AbsolveTurkey_Text["DatePassed"] = "Thanksgiving celebration is over, and you threw the item away."
	tThanksgiving2015_AbsolveTurkey_Text["DeleteItem"] = "The Thanksgiving Turkey is broken, so you threw it away."
	tThanksgiving2015_AbsolveTurkey_Text["FullBag"] = "Please clear some room in your inventory, first."
	tThanksgiving2015_AbsolveTurkey_Text["NotInHouse"] = "You can only place this furniture in your own house."
	tThanksgiving2015_AbsolveTurkey_Text["NotReslev"] = "You should have a Level 2+ house to place this furniture."
	tThanksgiving2015_AbsolveTurkey_Text["EnoghNum"] = "You can`t place more than 12 pieces of furniture in your house."

--------------------------------------------------------------------------------
---Name:[简体征服][活动脚本]感恩节之黄金脆皮火鸡（11.24-11.30）
--Creator: 	陈莺
--Created:	2015/08/06
--------------------------------------------------------------------------------
tThanksgiving2015GoldenCrispyTurky_Text = {}

--提示
tThanksgiving2015GoldenCrispyTurky_Text["MsgBox"] = {}
-- tThanksgiving2015GoldenCrispyTurky_Text["MsgBox"]["Failure"]="This Golden Turkey is too old to collect a piece of Golden Fried Turkey."
tThanksgiving2015GoldenCrispyTurky_Text["MsgBox"]["Success"]="Excellent! You killed the Golden Turkey in a second, and received a piece of Golden Fried Turkey. Eat it!"
tThanksgiving2015GoldenCrispyTurky_Text["MsgBox"]["Over"]="Thanksgiving celebration is over, and you threw the item away."
tThanksgiving2015GoldenCrispyTurky_Text["MsgBox"]["Appear"]="Pay attention! The Golden Fried Turkey appeared among bandits and monsters. Go and kill!"
tThanksgiving2015GoldenCrispyTurky_Text["MsgBox"]["Turky"]="You can't eat more than 2 today."
--吮指原味火鸡坤哥
--活动前
tThanksgiving2015GoldenCrispyTurky_Text[17230] = {}
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text111"] = "This is a battle between the Original Turkeys and the Golden Turkeys. I`ll go collect more"
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text112"] = "~information. Will you stand on my side? Let`s go defeat the Golden Turkeys from Nov. 24th"
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text113"] = "~to 30th."
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Option1"] = "I`ll~follow~you."
--活动中
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text121"] = "Original Turkey and Golden Turkey, only one can survive. Recently, I found some Golden Turkeys"
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text122"] = "~wandering in the wild. It`s a chance to eliminate them. If you`ve reached Level 80 or got reborn,"
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text123"] = "~come and help me on Nov. 24th to 30th."

tThanksgiving2015GoldenCrispyTurky_Text[17230]["Option2"] = "What~can~I~do?"
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Option3"] = "I`ll~think~about~it."
--活动后
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text131"] = "Hurray! We, the Original Turkeys, finally won the battle! The Golden Turkeys have tasted their own medicine."
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Option4"] = "Congratulations!"
--我该怎么帮你噜？
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text211"] = "The Golden Turkeys are hiding among other monsters in the wild. You need to kill at least 100 monsters in the main zones"
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text212"] = "~to discover them. When you find a Golden Turkey, eat it to get a reward from me. For your health, please not to eat more"
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Text213"] = "~than 2 pieces of Golden Fried Turkey, in a day."
tThanksgiving2015GoldenCrispyTurky_Text[17230]["Option5"] = "I~see."


------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]感恩节活动之敲宝箱
--Creator:		张世超
--Created:		2015/08/25
------------------------------------------------------------------------------------
--前缀 Thanksgiving2015_Chest_

--活动前
Thanksgiving2015_Chest_Text = {}
Thanksgiving2015_Chest_Text[17095] = {}
Thanksgiving2015_Chest_Text[17096] = {}
Thanksgiving2015_Chest_Text[17095]["Text111"] = "I`m really weary of roaming. As Thanksgiving is approaching, I want to return home and"
Thanksgiving2015_Chest_Text[17095]["Text112"] = "~share my treasures with Level 80+ or reborn heroes from Nov. 24th to 30th."
Thanksgiving2015_Chest_Text[17095]["Text113"] = ""
Thanksgiving2015_Chest_Text[17095]["Text114"] = ""
Thanksgiving2015_Chest_Text[17095]["Option1"] = "I`m~looking~forward~it."

--活动时间后
Thanksgiving2015_Chest_Text[17095]["Text121"] = "Gulp! I forgot to leave something for myself. How could I have a good festival? Please, help me..."
Thanksgiving2015_Chest_Text[17095]["Option2"] = "Bye~bye!"

--活动时间内
Thanksgiving2015_Chest_Text[17095]["Text131"] = "I don`t want to be a rootless merchant anymore. Listen, I`ve decided to return home and share all my treasure chests."
Thanksgiving2015_Chest_Text[17095]["Text132"] = "~If you`re Level 80+ or reborn, come to find me from Nov. 24th to 30th."
Thanksgiving2015_Chest_Text[17095]["Text133"] = "~By the way, opening the chest may not always surprise you, but frighten you sometimes."
Thanksgiving2015_Chest_Text[17095]["Text134"] = ""
Thanksgiving2015_Chest_Text[17095]["Option3"] = "Show~me~the~treasure~chests!"
Thanksgiving2015_Chest_Text[17095]["Option4"] = "How~will~you~share?"
Thanksgiving2015_Chest_Text[17095]["Option5"] = "I~give~up."

--怎么瓜分来着？
Thanksgiving2015_Chest_Text[17095]["Text211"] = "If you`re qualified, I`ll lead you to my fortune warehouse. But remember, you have to break the evil force that"
Thanksgiving2015_Chest_Text[17095]["Text212"] = "~sealed the chest to win the treasure. Put it more directly: the chest may contain vicious monsters. Good luck!"
Thanksgiving2015_Chest_Text[17095]["Text213"] = ""
Thanksgiving2015_Chest_Text[17095]["Option10"] = "I~can~handle~it."

--判断等级
Thanksgiving2015_Chest_Text[17095]["Text311"] = "Kid, you should be at least Level 80 or get reborn to open my treasure chests."
Thanksgiving2015_Chest_Text[17095]["Text312"] = "~You know, it`s not always surprise in the chest, sometimes it`s frightening thing."
Thanksgiving2015_Chest_Text[17095]["Option11"] = "Alright."

--已完成
Thanksgiving2015_Chest_Text[17095]["Text321"] = "Buddy, you`ve taken the treasure from the chest today. For a rich hero like you, I have no more to share with you."
Thanksgiving2015_Chest_Text[17095]["Option12"] = "Okay."

--没有完成
Thanksgiving2015_Chest_Text[17095]["Text331"] = "I collected these chests from foreign lands. To win the treasure inside, you have to break the evil force that sealed the chest."
Thanksgiving2015_Chest_Text[17095]["Text332"] = "~Put it more directly: the chest may contain vicious monsters. Good luck!"
Thanksgiving2015_Chest_Text[17095]["Option13"] = "I~can~handle~it."

--1010对白
Thanksgiving2015_Chest_Text["MessageBox1"] = "You entered the Dark Merchant`s warehouse, and felt astonished by the room full of treasure chests."
Thanksgiving2015_Chest_Text["MessageBox2"] = "You were teleported out of the Dark Merchant`s warehouse."
Thanksgiving2015_Chest_Text["MessageBox3"] = "You opened the chest, and received 1,000 Silver."
Thanksgiving2015_Chest_Text["MessageBox4"] = "You opened the chest, and received 10,000 Silver."
Thanksgiving2015_Chest_Text["MessageBox5"] = "You opened the chest, and received 100,000 Silver."
Thanksgiving2015_Chest_Text["MessageBox6"] = "Be careful! A Tomb Bat jumped out of the chest!"
Thanksgiving2015_Chest_Text["MessageBox7"] = "Watch out! A Bull Monster ran out of the chest."
Thanksgiving2015_Chest_Text["NoLeftSpace"] = "Your inventory is too full to contain anything. Make some room, first."
Thanksgiving2015_Chest_Text["MessageBox8"] = "Wow, you`re so lucky for winning the gift pack so fast. Now, you can leave with your gains."

--地图内npc
Thanksgiving2015_Chest_Text[17096]["Text111"] = "I collected these chests from foreign lands. To win the treasure inside, you have to"
Thanksgiving2015_Chest_Text[17096]["Text112"] = "~break the evil force that sealed the chest. Put it more directly: the chest may contain vicious monsters. Good luck!"
Thanksgiving2015_Chest_Text[17096]["Option1"] = "I~want~to~leave."
Thanksgiving2015_Chest_Text[17096]["Option2"] = "I~see."

------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]感恩节活动之感恩节大使
--Creator:		张世超
--Created:		2015/08/25
------------------------------------------------------------------------------------
--前缀 Thanksgiving2015_Npc_

--对白
Thanksgiving2015_Npc_Name = {}
Thanksgiving2015_Npc_Name[1] = "OriginalTurkeyKun"
Thanksgiving2015_Npc_Name[2] = "TurkeyClubPresident"
Thanksgiving2015_Npc_Name[3] = "RoyalChef"
Thanksgiving2015_Npc_Name[4] = "CherryGirl"
Thanksgiving2015_Npc_Name[5] = "DarkMerchant"
Thanksgiving2015_Npc_Name[6] = "FarmerTu"

Thanksgiving2015_Npc_Text = {}
Thanksgiving2015_Npc_Text["MessageBox"] = {}
Thanksgiving2015_Npc_Text["MessageBox1"] = "You should have a Magic Cranberries to claim a festival pack."
Thanksgiving2015_Npc_Text["MessageBox2"] = "Officer Happy presented you a special gift pack for your kindness. In return, you gave the last cranberry to him."
Thanksgiving2015_Npc_Text["MessageBox3"] = "The Thanksgiving celebration is over, and the Magic Cranberries went bad."
Thanksgiving2015_Npc_Text["MessageBox4"] = "You`ve already ate a cranberry. Go share your cranberries with the Original Turkey Kun, the Turkey Club President, the Royal Chef, the Cherry Girl, the Dark Merchant and Farmer Tu."

Thanksgiving2015_Npc_Text["MessageBox"][1] = "I`m so happy to receive your blessing. Let`s go clear the Golden Turkeys together!"
Thanksgiving2015_Npc_Text["MessageBox"][2] = "Ah, you`re so kind. You must be interested in doing good. Go and pick a turkey to prevent it from being a dish."
Thanksgiving2015_Npc_Text["MessageBox"][3] = "Thanks for your kindness. Hey, you came at the right time. I just finished the turkey soup."
Thanksgiving2015_Npc_Text["MessageBox"][4] = "Thank you for bringing met he cranberries! With the blessing of the Magic Cranberry, I`ll have a wonderful festival."
Thanksgiving2015_Npc_Text["MessageBox"][5] = "I`m so surprised for receiving such a warm gift like this. I wish you a big harvest in my treasure warehouse!"
Thanksgiving2015_Npc_Text["MessageBox"][6] = "Thanks for the cranberry, my lovely kid. I`ll give you the biggest fruit for the shooting game. "


tBackpackLetter_Text[3004937] = {}
tBackpackLetter_Text[3004937]["NoSpace"] = "You saw a bunch of Magic Cranberries on the ground, but your inventory is too full to contain it. Make some room first, and then login again to receive the cranberries."
tBackpackLetter_Text[3004937]["RewardItem"]	= "You received a bunch of Magic Cranberries. Eat it to get 1 day of Heaven Blessing."


Thanksgiving2015_Npc_Text[17231] = {}
--活动前
Thanksgiving2015_Npc_Text[17231]["Text111"] = "Thanksgiving Day is coming! It`s time for Level 80+ and reborn heroes to harvest happiness and rewards from Nov. 24th to 30th!"
Thanksgiving2015_Npc_Text[17231]["Text112"] = "~I await your visit!"
Thanksgiving2015_Npc_Text[17231]["Option1"] = "I`m~looking~forward~to~it."

--活动后
Thanksgiving2015_Npc_Text[17231]["Text121"] = "What a perfect celebration! People speak highly of the celebration I held. See you next year!"
Thanksgiving2015_Npc_Text[17231]["Text122"] = ""
Thanksgiving2015_Npc_Text[17231]["Option2"] = "See~you!"

--活动中
Thanksgiving2015_Npc_Text[17231]["Text131"] = "We finally embraced Thanksgiving Day! It`s time for Level 80+ and reborn heroes to harvest happiness and rewards from Nov. 24th to 30th!"
Thanksgiving2015_Npc_Text[17231]["Text132"] = "~The heroes who`ve completed all the event for Thanksgiving will be able to claim a special reward from me. Let`s play!"
Thanksgiving2015_Npc_Text[17231]["Text133"] = ""
Thanksgiving2015_Npc_Text[17231]["Option3"] = "How~to~share~cranberries?"
Thanksgiving2015_Npc_Text[17231]["Option4"] = "Learn~about~the~events."
Thanksgiving2015_Npc_Text[17231]["Option5"] = "Claim~the~special~reward."
Thanksgiving2015_Npc_Text[17231]["Option6"] = "Sounds~great."

--未完成
Thanksgiving2015_Npc_Text[17231]["Text311"] = "You`re so generous. As I know, the Original Turkey Kun, the Turkey Club President, the Royal Chef, the Cherry Girl, the Dark Merchant and Farmer Tu"
Thanksgiving2015_Npc_Text[17231]["Text312"] = "~haven`t claimed their own blessing. Go share your cranberries with all of them, and come back to claim a gift after you finish the job."
Thanksgiving2015_Npc_Text[17231]["Text313"] = ""
Thanksgiving2015_Npc_Text[17231]["Option10"] = "Okay."

--领取奖励
Thanksgiving2015_Npc_Text[17231]["Text321"] = "What a wonderful festival! You`ve delivered blessings to people, and I`ll also give you my blessing. Here is a gift pack for you."
Thanksgiving2015_Npc_Text[17231]["Option11"] = "Thanks!"

--我想了解一下活动详情。
Thanksgiving2015_Npc_Text[17231]["Text211"] = "There are 6 events for the Thanksgiving celebration. For each event, you can sign up with specific person around the parterre in Twin City."
Thanksgiving2015_Npc_Text[17231]["Text212"] = "~After completing all the event, you can claim a special reward from me. Only once. So, which event are you interested now?"
Thanksgiving2015_Npc_Text[17231]["Text213"] = ""
Thanksgiving2015_Npc_Text[17231]["Option20"] = "Original~VS~Golden."
Thanksgiving2015_Npc_Text[17231]["Option21"] = "The~Lucky~Turkeys."
Thanksgiving2015_Npc_Text[17231]["Option22"] = "Bustle~Turkey~Park."
Thanksgiving2015_Npc_Text[17231]["Option23"] = "The~Cranberries~Contest."
Thanksgiving2015_Npc_Text[17231]["Option24"] = "Secret~Golden~Chests."
Thanksgiving2015_Npc_Text[17231]["Option25"] = "Fruit~Shooting~Game."
Thanksgiving2015_Npc_Text[17231]["Option26"] = "Let~me~see."

--黄金脆皮火鸡活动。
Thanksgiving2015_Npc_Text[17231]["Text1111"] = "This is a battle between the Original Turkeys and the Golden Turkeys. The Original Turkey Kun is gathering intelligence."
Thanksgiving2015_Npc_Text[17231]["Text1112"] = "~Fight on the side of the Original Turkeys, and eliminate the Golden Turkeys from Nov. 24th to 30th. Don`t forget to eat"
Thanksgiving2015_Npc_Text[17231]["Text1113"] = "~up to 2 pieces of the Golden Fried Turkey for a reward."
Thanksgiving2015_Npc_Text[17231]["Option30"] = "I`m~on~the~way."
Thanksgiving2015_Npc_Text[17231]["Option31"] = "Learn~about~other~events."

--特赦火鸡活动。
Thanksgiving2015_Npc_Text[17231]["Text1211"] = "To show thanksgiving, the Turkey Club ask people to pick one of the Sunny Turkey, Flying Turkey and Rocket Turkey,"
Thanksgiving2015_Npc_Text[17231]["Text1212"] = "~and prevent it from being a dish. If you`re interested, go find the Turkey Club President on Nov. 24th to 30th. There is"
Thanksgiving2015_Npc_Text[17231]["Text1213"] = "~also a gift for the participators."
Thanksgiving2015_Npc_Text[17231]["Option32"] = "I`m~on~the~way."

--火鸡总动员活动。
Thanksgiving2015_Npc_Text[17231]["Text1311"] = "The Royal Chef is going to serve people with the mythical `Mushroom Turkey` dishes, and he needs lots of turkeys."
Thanksgiving2015_Npc_Text[17231]["Text1312"] = "~Listen, he is calling able men to capture turkeys for him at 10:00 - 10:30, 12:00 - 12:30, 18:00 - 18:30 and 22:00 - 22:30,"
Thanksgiving2015_Npc_Text[17231]["Text1313"] = "~from Nov. 24th to 30th. What`s more, people who earned totally 300 points will be nicely rewarded by the Royal Chef."
Thanksgiving2015_Npc_Text[17231]["Text1314"] = ""
Thanksgiving2015_Npc_Text[17231]["Option33"] = "I`m~on~the~way."

--蔓越莓竞赛活动。
Thanksgiving2015_Npc_Text[17231]["Text1411"] = "Cranberries bring good fortune. Recently, making string of cranberries becomes quite popular. If you`ve reached Level 80 or got reborn,"
Thanksgiving2015_Npc_Text[17231]["Text1412"] = "~sign up for the stringing cranberries contest with the Cherry Girl in Twin City, from Nov. 24th to 30th. Just make a string of"
Thanksgiving2015_Npc_Text[17231]["Text1413"] = "~15 cranberries within 1 minute, and you can earn a reward."
Thanksgiving2015_Npc_Text[17231]["Option34"] = "I`m~on~the~way."

--敲宝箱活动。
Thanksgiving2015_Npc_Text[17231]["Text1511"] = "The Dark Merchant has been weary of roaming. He`s decided to return home and share treasure chests with Level 80+"
Thanksgiving2015_Npc_Text[17231]["Text1512"] = "~or reborn heroes from Nov. 24th to 30th. Please note that, the chest may contain some evil force or fierce monsters."
Thanksgiving2015_Npc_Text[17231]["Text1513"] = "~Defeat them, and you`ll win the treasure."
Thanksgiving2015_Npc_Text[17231]["Option35"] = "I`m~on~the~way."

--猩猩投果子活动。
Thanksgiving2015_Npc_Text[17231]["Text1611"] = "The head of Harvest Village decided not hold a celebration this year, so Farmer Tu came to Twin City to carry out his own idea."
Thanksgiving2015_Npc_Text[17231]["Text1612"] = "~From Nov. 24th to 30th, all Level 80+ or reborn heroes are welcome to compete in his shooting game. Just earn 20 points within"
Thanksgiving2015_Npc_Text[17231]["Text1613"] = "~1 minute, and you can claim a reward from Farmer Tu. Go find Farmer Tu for more details."
Thanksgiving2015_Npc_Text[17231]["Option36"] = "I`m~on~the~way."

--等级
Thanksgiving2015_Npc_Text[17231]["Text2011"] = "Sorry, you`re not strong enough for the event. Keep practicing, and come back to me when you`re qualified."
Thanksgiving2015_Npc_Text[17231]["Option40"] = "I`ll~try~my~best."

--已领取
Thanksgiving2015_Npc_Text[17231]["Text2021"] = "We just met minutes ago, didn`t we? Come on, you`ve already claimed your reward. No more."
Thanksgiving2015_Npc_Text[17231]["Option41"] = "Alright."

--背包
Thanksgiving2015_Npc_Text[17231]["Text2031"] = "Tell me how can I put something into a full bag. Please make some room in your inventory, first!"
Thanksgiving2015_Npc_Text[17231]["Option42"] = "I`ll~do~it~now."

--未完成
Thanksgiving2015_Npc_Text[17231]["Text2041"] = "Wait, it seems you haven`t completed the quest at %s. Finish your job, first."
Thanksgiving2015_Npc_Text[17231]["Option43"] = "I~see."

--领取奖励
Thanksgiving2015_Npc_Text[17231]["Text2051"] = "Wow, you`re fast as lightning! Here`s a reward for you. Take it well."
Thanksgiving2015_Npc_Text[17231]["Option44"] = "Thanks!"

Thanksgiving2015_Npc_Text[3004937] = {}
Thanksgiving2015_Npc_Text[3004937]["111"] = "You ate a cranberry and received 1 day of Heaven Blessing. See, you still have a bunch of Magic Cranberries to taste."
Thanksgiving2015_Npc_Text[3004937]["112"] = "~You know, there are some people who are too busy to claim their blessing. Why not share the cranberries with them?"
Thanksgiving2015_Npc_Text[3004937]["113"] = "~Officer Happy will be glad to tell you more. You`ll see him around the parterre in Twin City from Nov. 24th to 30th."
Thanksgiving2015_Npc_Text[3004937]["Option1"] = "Head~to~see~Happy."

Thanksgiving2015_Npc_Text[3004937]["121"] = "The Thanksgiving celebration is over, and the Magic Cranberries went bad. Why not throw it away?"
Thanksgiving2015_Npc_Text[3004937]["Option2"] = "Okay."

------------------------------------------------------------------------------------
--Name:			[征服][活动脚本]boss月活动制作
--Creator: 		魏贻逵
--Created:		2015/08/04
------------------------------------------------------------------------------------
		tBossmonth_Text = {}
		tBossmonth_Text[1] = "           ┌─────────────┐\n"
		tBossmonth_Text[2] = "        ---------------------------------------------"
		tBossmonth_Text[3] = "Claim~a~design~drawing."
		tBossmonth_Text[4] = "Random~drawing~(200,000Silver)"
		tBossmonth_Text[5] = "I`ll~talk~to~you~later."
		tBossmonth_Text[6] = {}
		tBossmonth_Text[6][1] = "You received Frozen Fantasy`s Upper Part, Lower Part and Sleeves!"
		tBossmonth_Text[6][5] = "You receive Frozen Fantasy`s Upper Parts, Lower Parts and Sleeves, each for 5 pieces!"

		tBossmonth_Text[7] = "Please spread the design drawing, and start making the Frozen Fantasy garment. The success rate varies according to the quality of the drawing."
		tBossmonth_Text[8] = "You should have the design drawing of Frozen Fantasy to make this garment."

		tBossmonth_Text[10] = "1~set.~(100,000~Silver)"
		tBossmonth_Text[11] = "5~sets.~(500,000~Silver)"
		tBossmonth_Text[12] = "I~want~to~ask~something~else."
		tBossmonth_Text[13] = "I`ll~talk~to~you~later."

		tBossmonth_Text[20] = "Yes."
		tBossmonth_Text[21] = "No."
		
		tBossmonth_Text[30] = "The event has ended, and you threw away the design drawing."
		tBossmonth_Text[31] = "Please use the drawing of the Garment`s Upper Part, Lower Part or Sleeves to make corresponding part."
		tBossmonth_Text[32] = "You should have the drawing of the Garment`s Upper Part, Lower Part or Sleeves to make corresponding part."
		tBossmonth_Text[33] = "You`ve already made the garment 5 times. Please retry tomorrow."
		tBossmonth_Text[34] = "You received a splendid garment, Frozen Fantasy [Glory]!"
		tBossmonth_Text[35] = "Congratulations! [%s] received a splendid garment, Frozen Fantasy [Glory]!"

		tBossmonth_Text[40] = "Make sure you have Frozen Fantasy`s Upper Part, Lower Part and Sleeves with you."
		tBossmonth_Text[41] = "Your inventory is full. Please make some room, first."
		
		tBossmonth_Text[3007157] = {}
		tBossmonth_Text[3007157][1] = "The event has ended, and you threw the Secret of Snow away."
		tBossmonth_Text[3007157][2] = "Frozen Fantasy, the most beautiful garment in the Ice Kingdom, has been lost for a long time. However, people have never given up"
		tBossmonth_Text[3007157][3] = "~the hope of retrieving it. Snow Olaf (Market 294,226) finally got some clues. The materials of making this garment are scattered"
		tBossmonth_Text[3007157][4] = "~around the world, waiting for Level 100+ heroes to discover them between Dec. 3rd and Dec. 16th. Discard it after reading."
		tBossmonth_Text[3007157][5] = "Head~to~see~Olaf."
		tBossmonth_Text[3007157][6] = "You received 30 minutes of EXP!"
		tBossmonth_Text[3007157][7] = "You received 15 Study Points!"

		tBossmonth_Text[3007158] = {}
		tBossmonth_Text[3007158][4] = {}
		tBossmonth_Text[3007158][4][3002029] = "Though you failed to make the Frozen Fantasy garment, you received a Protection Pill!"
		tBossmonth_Text[3007158][4][3002554] = "Though you failed to make the Frozen Fantasy garment, you received an Excellent Chi Pack!"
		tBossmonth_Text[3007158][4][3007159] = "Though you failed to make the Frozen Fantasy garment, you received an Elite Drawing of Frozen Fantasy!"
		tBossmonth_Text[3007158][4][3007205] = "Though you failed to make the Frozen Fantasy garment, you received a Garment Pack Fragment!"
		tBossmonth_Text[3007158][4][3007206] = "Though you failed to make the Frozen Fantasy garment, you received a Senior Garment Pack Fragment!"
		tBossmonth_Text[3007158][4][3007201] = "You successfully made a 7-day Frozen Fantasy Pack (B)!"
		tBossmonth_Text[3007158][4][3007199] = "Though you failed to make the Frozen Fantasy garment, you received a Frozen Heart Fragment!"
		tBossmonth_Text[3007158][5] = "~You were also rewarded with 60 minutes of EXP!"
		tBossmonth_Text[3007158][6] = "~You were also rewarded with 30 Study Points!"

		tBossmonth_Text[3007159] = {}
		tBossmonth_Text[3007159][4] = {}
		tBossmonth_Text[3007159][4][3002029] = "Though you failed to make the Frozen Fantasy garment, you received a Protection Pill!"
		tBossmonth_Text[3007159][4][3002554] = "Though you failed to make the Frozen Fantasy garment, you received an Excellent Chi Pack!"
		tBossmonth_Text[3007159][4][3007160] = "Though you failed to make the Frozen Fantasy garment, you received a Super Drawing of Frozen Fantasy!"
		tBossmonth_Text[3007159][4][3007205] = "Though you failed to make the Frozen Fantasy garment, you received a Garment Pack Fragment!"
		tBossmonth_Text[3007159][4][3007206] = "Though you failed to make the Frozen Fantasy garment, you received a Senior Garment Pack Fragment!"
		tBossmonth_Text[3007159][4][3007201] = "You successfully made a 7-day Frozen Fantasy Pack (B)!"
		tBossmonth_Text[3007159][4][3007199] = "Though you failed to make the Frozen Fantasy garment, you received a Frozen Heart Fragment!"
		tBossmonth_Text[3007159][5] = "~You were also rewarded with 120 minutes of EXP!"
		tBossmonth_Text[3007159][6] = "~You were also rewarded with 60 Study Points!"
		tBossmonth_Text[3007159][7] = "Are you sure you want to combine 5 Unique Drawings of Frozen Fantasy into 1 Elite Drawing of Frozen Fantasy?"
		tBossmonth_Text[3007159][8] = "You received an Elite Drawing of Frozen Fantasy!"

		tBossmonth_Text[3007160] = {}
		tBossmonth_Text[3007160][4] = {}
		tBossmonth_Text[3007160][4][3002029] = "Though you failed to make the Frozen Fantasy garment, you received a Protection Pill!"
		tBossmonth_Text[3007160][4][3002554] = "Though you failed to make the Frozen Fantasy garment, you received an Excellent Chi Pack!"
		tBossmonth_Text[3007160][4][3007205] = "Though you failed to make the Frozen Fantasy garment, you received a Garment Pack Fragment!"
		tBossmonth_Text[3007160][4][3007206] = "Though you failed to make the Frozen Fantasy garment, you received a Senior Garment Pack Fragment!"
		tBossmonth_Text[3007160][4][3007201] = "You successfully made a 7-day Frozen Fantasy Pack (B)!"
		tBossmonth_Text[3007160][4][3007199] = "Though you failed to make the Frozen Fantasy garment, you received a Frozen Heart Fragment!"
		tBossmonth_Text[3007160][4][3007200] = "Though you failed to make the Frozen Fantasy garment, you received a Frozen Heart!"
		tBossmonth_Text[3007160][5] = "~You were also rewarded with 180 minutes of EXP!"
		tBossmonth_Text[3007160][6] = "~You were also rewarded with 90 Study Points!"
		tBossmonth_Text[3007160][7] = "Are you sure you want to combine 5 Elite Drawings of Frozen Fantasy into 1 Super Drawing of Frozen Fantasy?"
		tBossmonth_Text[3007160][8] = "You received a Super Drawing of Frozen Fantasy!"

--------------------
		tBossmonth_Text[3007163] = {}
		tBossmonth_Text[3007163][1] = "How many Frozen Fantasy`s Upper Parts do you want?"

		tBossmonth_Text[3007163][2] = {}
		tBossmonth_Text[3007163][2][1] = "Are you sure you want to pay 100,000 Silver for 1 Frozen Fantasy`s Upper Part?"
		tBossmonth_Text[3007163][2][2] = "Are you sure you want to pay 500,000 Silver for 5 Frozen Fantasy`s Upper Parts?"

		tBossmonth_Text[3007163][3] = {}
		tBossmonth_Text[3007163][3][1] = "You received 1 Frozen Fantasy`s Upper Part!"
		tBossmonth_Text[3007163][3][2] = "You received 5 Frozen Fantasy`s Upper Parts!"
		
		tBossmonth_Text[3007163][4] = "The event has ended, and you threw the Frozen Fantasy`s Upper Part away."

		tBossmonth_Text[3007164] = {}
		tBossmonth_Text[3007164][1] = "How many Frozen Fantasy`s Lower Parts do you want?"
		tBossmonth_Text[3007164][2] = {}
		tBossmonth_Text[3007164][2][1] = "Are you sure you want to combine 100,000 Silver for 1 Frozen Fantasy`s Lower Part?"
		tBossmonth_Text[3007164][2][2] = "Are you sure you want to combine 500,000 Silver for 5 Frozen Fantasy`s Lower Parts?"
		
		tBossmonth_Text[3007164][3] = {}
		tBossmonth_Text[3007164][3][1] = "You received 1 Frozen Fantasy`s Upper Part!"
		tBossmonth_Text[3007164][3][2] = "You received 5 Frozen Fantasy`s Upper Parts!"
		
		tBossmonth_Text[3007164][4] = "The event has ended, and you threw the Lower Part away."

		tBossmonth_Text[3007165] = {}
		tBossmonth_Text[3007165][1] = "How many pairs of Frozen Fantasy`s Sleeves do you want?"
		
		tBossmonth_Text[3007165][2] = {}
		tBossmonth_Text[3007165][2][1] = "Are you sure you want to combine 100,000 Silver for 1 pair of Frozen Fantasy`s Sleeves?"
		tBossmonth_Text[3007165][2][2] = "Are you sure you want to combine 500,000 Silver for 5 pairs of Frozen Fantasy`s Sleeves?"
		
		tBossmonth_Text[3007165][3] = {}
		tBossmonth_Text[3007165][3][1] = "You received 1 pair of Frozen Fantasy`s Sleeves!"
		tBossmonth_Text[3007165][3][2] = "You received 5 pairs of Frozen Fantasy`s Sleeves!"

		tBossmonth_Text[3007165][4] = "The event has ended, and you threw Frozen Fantasy`s Sleeves away."

		tBossmonth_Text[3007168] = {}
		tBossmonth_Text[3007168][1] = "Make sure you have 1 Velvet, 1 Delicate Chiffon, 1 Silk Thread, and 5 pieces of Snowwhite Silk with you, first. "
		tBossmonth_Text[3007168][2] = "Though you failed to make Frozen Fantasy`s Upper Part, you received 10 minutes of EXP!"
		tBossmonth_Text[3007168][3] = "Though you failed to make Frozen Fantasy`s Upper Part, you received 5 Study Points!"
		tBossmonth_Text[3007168][4] = "You successfully made Frozen Fantasy`s Upper Part!"

		tBossmonth_Text[3007169] = {}
		tBossmonth_Text[3007169][1] = "Make sure you have 1 Velvet, 1 Delicate Chiffon, 1 Silk Thread, and 5 pieces of Snowwhite Silk with you, first. "
		tBossmonth_Text[3007169][2] = "Though you failed to make Frozen Fantasy`s Lower Part, you received 10 minutes of EXP!"
		tBossmonth_Text[3007169][3] = "Though you failed to make Frozen Fantasy`s Lower Part, you received 5 Study Points!"
		tBossmonth_Text[3007169][4] = "You successfully made Frozen Fantasy`s Lower Part!"

		tBossmonth_Text[3007170] = {}
		tBossmonth_Text[3007170][1] = "Make sure you have 1 Velvet, 1 Delicate Chiffon, 1 Silk Thread, and 5 pieces of Snowwhite Silk with you, first. "
		tBossmonth_Text[3007170][2] = "Though you failed to make Frozen Fantasy`s Sleeves, you received 10 minutes of EXP!"
		tBossmonth_Text[3007170][3] = "Though you failed to make Frozen Fantasy`s Sleeves, you received 5 Study Points!"
		tBossmonth_Text[3007170][4] = "You successfully made Frozen Fantasy`s Sleeves!"
		
		tBossmonth_Text[3007183] = {}
		tBossmonth_Text[3007183][1] = "The event has ended, and you threw the Velvet away."
		
		tBossmonth_Text[3007184] = {}
		tBossmonth_Text[3007184][1] = "The event has ended, and you threw the Delicate Chiffon away."
		
		tBossmonth_Text[3007185] = {}
		tBossmonth_Text[3007185][1] = "The event has ended, and you threw the Silk Thread away."
		
		tBossmonth_Text[3007186] = {}
		tBossmonth_Text[3007186][1] = "The event has ended, and you threw the Snowwhite Silk away."

		tBossmonth_Text[3007199] = {}
		tBossmonth_Text[3007199][1] = "How many Frozen Heart Fragments would you like to study?"
		tBossmonth_Text[3007199][2] = "1."
		tBossmonth_Text[3007199][3] = "10."
		tBossmonth_Text[3007199][4] = {}
		tBossmonth_Text[3007199][4][1] = "You don`t have enough number of Frozen Heart Fragments. Do you want to pay 15 CPs instead?"
		tBossmonth_Text[3007199][5] = {}
		tBossmonth_Text[3007199][5][1] = "Yes.~Here~are~15~CPs."
		tBossmonth_Text[3007199][6] = "You received %d %s!"
		tBossmonth_Text[3007199]["strengthvalue"] = "You received 60 Chi Points!"
		tBossmonth_Text[3007199][7] = "You can combine 6 Frozen Heart Fragments into a Frozen Heart, or directly study it. So, what do you say?"
		tBossmonth_Text[3007199][8] = "Directly~study~this~fragment."
		tBossmonth_Text[3007199][9] = "Combine~6~fragments."
		tBossmonth_Text[3007199][10] = "Make sure you have 6 Frozen Heart Fragments, first!"
		tBossmonth_Text[3007199][11] = "You received 1 Frozen Heart!"

		tBossmonth_Text[3007200] = {}
		tBossmonth_Text[3007200][1] = "How many Frozen Hearts would you like to study?"
		tBossmonth_Text[3007200][2] = "1."
		tBossmonth_Text[3007200][3] = "10."
		tBossmonth_Text[3007200][4] = {}
		tBossmonth_Text[3007200][4][1] = "You don`t have enough number of Frozen Hearts. Do you want to pay 77 CPs instead?"
		tBossmonth_Text[3007200][4][10] = "You don`t have enough number of Frozen Hearts. Do you want to pay 770 CPs instead?"
		tBossmonth_Text[3007200][5] = {}
		tBossmonth_Text[3007200][5][1] = "Yes.~Here~are~77~CPs."
		tBossmonth_Text[3007200][5][10] = "Yes.~Here~are~770~CPs."
		tBossmonth_Text[3007200][6] = "You received %d %s!"
		tBossmonth_Text[3007200]["strengthvalue"] = "You received 300 Chi Points!"

		tBossmonth_Text[3007201] = {}
		tBossmonth_Text[3007201]["AwardItem"] = "You received a 7-day 1% Blessed Frozen Fantasy garment (B)!"
		tBossmonth_Text[3007201]["Text"] = "You received 1 Frozen Heart Fragment!"
		tBossmonth_Text[3007201][1] = "If you have 7-day Frozen Fantasy Packs (B) you don`t need, you can sell them to the Cloth Merchant. But make sure they have not been opened."
		tBossmonth_Text[3007201][2] = "~So, do you want to open this pack?"

		tBossmonth_Text[3007202] = {}
		tBossmonth_Text[3007202]["AwardItem"] = "You received a permanent 1% Blessed Frozen Fantasy garment (B)!"
		tBossmonth_Text[3007202]["Text"] = "You received 3 Frozen Hearts!"
		tBossmonth_Text[3007202][1] = ""
		tBossmonth_Text[3007202][2] = "The Permanent Frozen Fantasy Packs (B) you don`t need and have not been opened can be sold to the Cloth Merchant. So, do you want to open this pack?"

		tBossmonth_Text[3007203] = {}
		tBossmonth_Text[3007203]["AwardItem"] = "You received a permanent 1% Blessed Frozen Fantasy [Glaze] garment!"
		tBossmonth_Text[3007203][1] = ""
		tBossmonth_Text[3007203][2] = "Do you want to open this pack, now?"

		tBossmonth_Text[3007204] = {}
		tBossmonth_Text[3007204]["AwardItem"] = "You received a permanent 1% Blessed Frozen Fantasy [Glaze] garment!"
		
		tBossmonth_Text[3007205] = {}
		tBossmonth_Text[3007205]["strengthvalue"] = "The event has ended, and you threw the Garment Pack Fragment away."
		tBossmonth_Text[3007205][1] = "You have 1/7 chance to get a 7-day Frozen Fantasy Pack (B) if using this fragment alone. If you combine 7 fragments, you can surely get that pack."
		tBossmonth_Text[3007205][2] = "~So, how many fragments would you like to use?"
		tBossmonth_Text[3007205][3] = "1."
		tBossmonth_Text[3007205][4] = "7."
		tBossmonth_Text[3007205][5] = "You received a 7-day Frozen Fantasy Pack (B)!"
		tBossmonth_Text[3007205][6] = {}
		tBossmonth_Text[3007205][6][1] = "Though you failed to make a pack, you received 20 minutes of EXP!"
		tBossmonth_Text[3007205][6][2] = "Though you failed to make a pack, you received 10 Study Points!"
		tBossmonth_Text[3007205][7] = "You don`t have enough number of fragments."

		tBossmonth_Text[3007206] = {}
		tBossmonth_Text[3007206]["strengthvalue"] = "The event has ended, and you threw the Garment Pack Fragment away."
		tBossmonth_Text[3007206][1] = "You have 1/15 chance to get a Permanent Frozen Fantasy Pack (B) if using this fragment alone. If you combine 15 fragments, you can surely get that pack."
		tBossmonth_Text[3007206][2] = "~So, how many fragments would you like to use?"
		tBossmonth_Text[3007206][3] = "1."
		tBossmonth_Text[3007206][4] = "15."
		tBossmonth_Text[3007206][5] = "You received a Permanent Frozen Fantasy Pack (B)!"
		tBossmonth_Text[3007206][6] = {}
		tBossmonth_Text[3007206][6][1] = "Though you failed to make a pack, you received 200 minutes of EXP!"
		tBossmonth_Text[3007206][6][2] = "Though you failed to make a pack, you received 100 Study Points!"
		tBossmonth_Text[3007206][7] = "You don`t have enough number of fragments."

		tBossmonth_Text[3007207] = {}
		tBossmonth_Text[3007207][2] = "Make sure you have Snow Queen`s Grace and 15 Grace Gems, so you can use them to make a Permanent Frozen Fantasy Pack (B) unbound."
		tBossmonth_Text[3007207][3] = "You successfully made the Permanent Frozen Fantasy Pack (B) unbound with Snow Queen`s Grace."

		tBossmonth_Text[3007208] = {}
		tBossmonth_Text[3007208][2] = "Make sure you have Snow Queen`s Blessing and 15 Blessing Crystals, so you can use them to shine a permanent Frozen Fantasy!"
		tBossmonth_Text[3007208][3] = "You successfully transformed the permanent Frozen Fantasy into a Frozen Fantasy [Glaze]!"

		tBossmonth_Text[3007209] = {}
		tBossmonth_Text[3007209]["strengthvalue_1"] = "Though you failed to get Snow Queen`s Grace, you received 100 Chi Points."
		tBossmonth_Text[3007209]["strengthvalue"] = "You received 200 Chi Points!"
		tBossmonth_Text[3007209][1] = "You have 1/10 chance to get Snow Queen`s Grace if you using this fragment alone. If failed, you`ll get 100 Chi Points."
		tBossmonth_Text[3007209][2] = "~Well, if you combine 10 fragments, you can surely get that Grace. So, how many fragments would you like to use?"
		tBossmonth_Text[3007209][3] = "1."
		tBossmonth_Text[3007209][4] = "10."
		tBossmonth_Text[3007209][5] = "You received 1 piece of Snow Queen`s Grace!"
		tBossmonth_Text[3007209][6] = "The magic of this fragment has vanished, and you got nothing."
		tBossmonth_Text[3007209][7] = "You don`t have enough number of fragments."
		
		tBossmonth_Text[3007210] = {}
		tBossmonth_Text[3007210]["strengthvalue_1"] = "Though you failed to get Snow Queen`s Blessing, you received 300 Chi Points."
		tBossmonth_Text[3007210]["strengthvalue"] = "You received 600 Chi Points!"
		tBossmonth_Text[3007210][1] = "You have 1/10 chance to get Snow Queen`s Blessing if you using this fragment alone. If failed, you`ll get 300 Chi Points."
		tBossmonth_Text[3007210][2] = "~Well, if you combine 10 fragments, you can surely get that Blessing. So, how many fragments would you like to use?"
		tBossmonth_Text[3007210][3] = "1."
		tBossmonth_Text[3007210][4] = "10."
		tBossmonth_Text[3007210][5] = "You received 1 piece of Snow Queen`s Blessing!"
		tBossmonth_Text[3007210][6] = "The magic of this fragment has vanished, and you got nothing."
		tBossmonth_Text[3007210][7] = "You don`t have enough number of fragments."
		
		tBossmonth_Text[3007211] = {}
		tBossmonth_Text[3007211]["strengthvalue"] = "You received 200 Chi Points!"
		tBossmonth_Text[3007211]["Text"] = "When you have Snow Queen`s Grace and 15 Grace Gems, you can click Snow Queen`s Grace to make a Permanent Frozen Fantasy Pack (B) unbound."

		tBossmonth_Text[3007214] = {}
		tBossmonth_Text[3007214]["strengthvalue"] = "You received 600 Chi Points!"
		tBossmonth_Text[3007214]["Text"] = "When you have Snow Queen`s Blessing and 15 Blessing Crystals, you can click Snow Queen`s Blessing to upgrade a Permanent Frozen Fantasy Pack to Frozen Fantasy [Glaze]."

		tBossmonth_Text[3007220] = {}
		tBossmonth_Text[3007220][1] = "The event has ended, and you threw the Magic Design Code away."
		tBossmonth_Text[3007220][2] = "You`ve activated the Magic Design Code, and you can use the design drawing one more time."
		tBossmonth_Text[3007220][3] = "You haven`t used the design drawing today. No need to activate this code."

		tBossmonth_Text[3007221] = {}
		tBossmonth_Text[3007221][1] = "The event has ended, and you threw the Magic Design Code Fragment away."
		tBossmonth_Text[3007221][2] = "You should have 7 Magic Design Code Fragments to combine them into a complete code."
		tBossmonth_Text[3007221][3] = "You successfully combined the fragments into a complete Magic Design Code!"
		
		

-------------------------------------------------NPC模板部分-------------------------------------------------
		tBossmonth_Text[10870] = {}
		tBossmonth_Text[10870]["Text111"] = "Hey, you know Frozen Fantasy? It`s the most beautiful garment in the Ice Kingdom. However, it was lost, long long ago."
		tBossmonth_Text[10870]["Text112"] = "~I`m so lucky to get some clues yesterday. Guess what, the materials of making this mythical garment are scattered around"
		tBossmonth_Text[10870]["Text113"] = "~the world. If you`ve reached Level 100 or got reborn, find me between Dec. 3rd and Dec. 16th. I have more details to share."
		tBossmonth_Text[10870]["Option1"] = "I`m~looking~forward~to~it."

		tBossmonth_Text[10870]["Text121"] = "It`s incredible, isn`t it? We finally made the Frozen Fantasy! It`s also Snow Queen`s favoriate garment. I`ll give her a surprise!"
		tBossmonth_Text[10870]["Text122"] = ""
		tBossmonth_Text[10870]["Option2"] = "She~must~be~happy."

		tBossmonth_Text[10870]["Text131"] = "To make Frozen Fantasy garment, you need to collect materials around the world. It`s so dangerous for you."
		tBossmonth_Text[10870]["Text132"] = "~If you`re really interested, you can find me when you reach Level 100 or get reborn."
		tBossmonth_Text[10870]["Option3"] = "I~see."

		tBossmonth_Text[10870]["Text141"] = "Wow, I got some clues about the Frozen Fantasy! I can`t wait to share with you. Look, the materials of making this mythical garment"
		tBossmonth_Text[10870]["Text142"] = "~are scattered around the world. You can collect them to make yourself a great Frozen Fantasy between Dec. 3rd and Dec. 16th."
		tBossmonth_Text[10870]["Text143"] = "~If you have 3 Frozen Fantasy [Glaze] garments, find me to combine them into a more splendid Frozen Fantasy [Glory]."
		tBossmonth_Text[10870]["Option7"] = "Tell~me~more."
		tBossmonth_Text[10870]["Option6"] = "Study~for~better~drawing."
		tBossmonth_Text[10870]["Option8"] = "Make~a~Frozen~Fantasy~[Glory]."

		tBossmonth_Text[10870]["Text221"] = "My hero, your inventory is too full to contain anything. Why not make some room, first?"

		tBossmonth_Text[10870]["Text241"] = "Yeah, you can study the Frozen Heart or its fragment to get Frozen Fantasy garment and beautifying material."
		tBossmonth_Text[10870]["Text242"] = "~Studying a complete Frozen Heart gives a better chance of getting such materials which can upgrade"
		tBossmonth_Text[10870]["Text243"] = "~your Frozen Fantasy to Frozen Fantasy [Glaze]."
		tBossmonth_Text[10870]["Option40"] = "Study~the~fragment~/~15~CPs."
		tBossmonth_Text[10870]["Option41"] = "Study~the~Frozen~Heart."
		tBossmonth_Text[10870]["Option42"] = "I~see."

		tBossmonth_Text[10870]["Text251"] = "You have a chance to make Frozen Fantasy with its design drawing, upper part, lower part and sleeves. The success rate varies according to the drawing`s quality. Go collect the parts from monsters,"
		tBossmonth_Text[10870]["Text252"] = "~and get Elite drawing from Honoring the Ancestors [Daily], Exorcism, or by using Chi Token, and Super drawing by opening the Outstanding Exploit Pack exchanged with 50 Kingdom Deeds."
		tBossmonth_Text[10870]["Option50"] = "Learn~about~monster~drops."
		tBossmonth_Text[10870]["Option54"] = "I~want~to~ask~something~else."
		tBossmonth_Text[10870]["Option55"] = "I`ll~talk~to~you~later."

		tBossmonth_Text[10870]["Text261"] = "Are you sure you want to combine 3 Frozen Fantasy [Glaze] garments into a Frozen Fantasy [Glory]?"
		tBossmonth_Text[10870]["Option70"] = "Yes."
		tBossmonth_Text[10870]["Option71"] = "No."

		tBossmonth_Text[10870]["Text311"] = "How many Frozen Hearts would you like to study? If you don`t have enough Frozen Hearts, you can pay a certain amount of CPs instead."
		tBossmonth_Text[10870]["Option51"] = "1~heart.~(or~77~CPs)"
		tBossmonth_Text[10870]["Option52"] = "10~hearts.~(or~770~CPs)"

		tBossmonth_Text[10870]["Text341"] = "While hunting monsters in Canyon, Desert, Dungeon, Frozen Grotto, Sea Of Death, or on Island, you`ll have a chance to receive Frozen Fantasy`s parts, or design drawing."
		tBossmonth_Text[10870]["Text342"] = "\n\nIn addition, Snow Enchantress will appear in Dungeon, Frozen Grotto, Sea Of Death, or outside the main cities. Killing it will certainly bring you those parts and design drawing."
		tBossmonth_Text[10870]["Option60"] = "I~want~to~ask~something~else."
		tBossmonth_Text[10870]["Option61"] = "Got~it."

		tBossmonth_Text[10870]["Text411"] = "Sorry, I can`t help you if you don`t have enough CPs."
		tBossmonth_Text[10870]["Option100"] = "Alright."

		tBossmonth_Text[10870]["Text421"] = "It requires 3 Frozen Fantasy [Glaze] garments to make a Frozen Fantasy [Glory]."


		tBossmonth_Text[10888] = {}
		tBossmonth_Text[10888]["Text111"] = "Frozen Fantasy is the most beautiful garment in the Ice Kingdom, but has been lost for a long time. Olaf told me the materials of making this garment are scattered around the world."
		tBossmonth_Text[10888]["Text112"] = "~You need to collect all the materials between Dec. 3rd and Dec. 16th to make this mythical garment. Olaf also knows how to combine 3 Frozen Fantasy [Glaze] into a Frozen Fantasy [Glory]."
		tBossmonth_Text[10888]["Option1"] = "I`m~looking~forward~to~it."
		tBossmonth_Text[10888]["Text121"] = "Frozen Fantasy is so beautiful that I can move my eyes off it."
		tBossmonth_Text[10888]["Option2"] = "Indeed."
		tBossmonth_Text[10888]["Text131"] = "As mentioned, you need to collect the materials of making Frozen Fantasy garment around the world. It`s so dangerous for you. I suggest you take this challenge when you reach Level 100 or get reborn."
		tBossmonth_Text[10888]["Option3"] = "Alright."

		tBossmonth_Text[10888]["Text141"] = "It`s kind of special of making Frozen Fantasy garment. I know how to upgrade the design drawing, and where you can find"
		tBossmonth_Text[10888]["Text142"] = "~the materials. So, what do you want from me?"
		tBossmonth_Text[10888]["Option5"] = "Upgrade~to~Elite~drawing."
		tBossmonth_Text[10888]["Option6"] = "Upgrade~to~Super~drawing."
		tBossmonth_Text[10888]["Option7"] = "Swap~garment~packs."
		tBossmonth_Text[10888]["Option8"] = "Drawing,~and~buy~materials."

		tBossmonth_Text[10888]["Text211"] = "If you have garment packs you don`t need, you can swap them with me. I offer 1 Frozen Heart Fragment for"
		tBossmonth_Text[10888]["Text212"] = "~a 7-day Frozen Fantasy Pack, and 3 Frozen Hearts for a Permanent Frozen Fantasy Pack. Which one would you like to swap?"
		tBossmonth_Text[10888]["Option10"] = "7-dayFrozenFantasyPack(B)."
		tBossmonth_Text[10888]["Option11"] = "PermanentFrozenFantasyPack(B)."
		tBossmonth_Text[10888]["Option12"] = "I~have~something~else~to~ask."
		tBossmonth_Text[10888]["Option13"] = "I`ll~talk~to~you~later."

		tBossmonth_Text[10888]["Text221"] = "While hunting monsters in Canyon, Desert, Dungeon, Frozen Grotto, Sea Of Death, or on Island, you`ll have a chance to receive Frozen Fantasy`s parts, or design drawing."
		tBossmonth_Text[10888]["Text222"] = "\n\nIn addition, Snow Enchantress will appear in Dungeon, Frozen Grotto, Sea Of Death, or outside the main cities. Killing it will certainly bring you those parts and design drawing."
		tBossmonth_Text[10888]["Option15"] = "I~want~to~buy~the~parts."
		tBossmonth_Text[10888]["Option16"] = "I~see."

		tBossmonth_Text[10888]["Text311"] = "Your inventory is full. Please make some room, first."
		tBossmonth_Text[10888]["Option20"] = "Okay."

		tBossmonth_Text[10888]["Text321"] = "Come on, you don`t have a Frozen Fantasy Pack (B) with you."

		tBossmonth_Text[10888]["Text331"] = "You should have 5 design drawings of the same quality, so I can combine them into a better one."
		tBossmonth_Text[10888]["Text341"] = "Which part would you like to buy?"
		tBossmonth_Text[10888]["Option30"] = "Frozen~Fantasy`s~Upper~Part."
		tBossmonth_Text[10888]["Option31"] = "Frozen~Fantasy`s~Lower~Part."
		tBossmonth_Text[10888]["Option32"] = "Frozen~Fantasy`s~Sleeves"
		tBossmonth_Text[10888]["Option33"] = "All~the~3~parts."
		tBossmonth_Text[10888]["Option34"] = "Let~me~see."

		tBossmonth_Text[10888]["Text411"] = "How many sets of Frozen Fantasy`s parts would you like to buy?"
		tBossmonth_Text[10888]["Option40"] = "1~set.~(300,000~Silver)"
		tBossmonth_Text[10888]["Option41"] = "5~sets.~(1,500,000~Silver)"
		tBossmonth_Text[10888]["Option42"] = "I~want~to~ask~something~else."

		tBossmonth_Text[10888]["Text421"] = "Sorry, you don`t have enough Silver."
		tBossmonth_Text[10888]["Text511"] = "Are you sure you want to pay 300,000 Silver for a set of Frozen Fantasy`s parts?"
		tBossmonth_Text[10888]["Option50"] = "Yes."
		tBossmonth_Text[10888]["Option51"] = "No."

		tBossmonth_Text[10888]["Text521"] = "Are you sure you want to combine 1,500,000 Silver for 5 sets of Frozen Fantasy`s parts"
		tBossmonth_Text[10888]["Option52"] = "Yes."
		tBossmonth_Text[10888]["Option53"] = "No."

----------------------------------------------------------------------------
--Name:		[??][????]??????.lua
--Purpose:	??????
--Creator: 	??
--Created:	2016/06/14
----------------------------------------------------------------------------
tSendMail_Text = {}

tSendMail_Text[90123300] = {}
tSendMail_Text[90123300]["Sender"] = "System"
tSendMail_Text[90123300]["Title"] = "Consecutive Sign-in Reward"
tSendMail_Text[90123300]["Content"] = "You`ve signed in 2 consecutive days. Please claim the reward."
tSendMail_Text[90123300]["Talk"] = "You received a 2-day Sign-in Pack for successfully signing in 2 consecutive days!"

tSendMail_Text[90123301] = {}
tSendMail_Text[90123301]["Sender"] = "System"
tSendMail_Text[90123301]["Title"] = "Consecutive Sign-in Reward"
tSendMail_Text[90123301]["Content"] = "You`ve signed in 7 consecutive days. Please claim the reward."
tSendMail_Text[90123301]["Talk"] = "You received a 7-day Sign-in Pack for successfully signing in 7 consecutive days!"

tSendMail_Text[90123302] = {}
tSendMail_Text[90123302]["Sender"] = "System"
tSendMail_Text[90123302]["Title"] = "Consecutive Sign-in Reward"
tSendMail_Text[90123302]["Content"] = "You`ve signed in 14 consecutive days. Please claim the reward."
tSendMail_Text[90123302]["Talk"] = "You received a 14-day Sign-in Pack for successfully signing in 14 consecutive days!"

tSendMail_Text[90123303] = {}
tSendMail_Text[90123303]["Sender"] = "System"
tSendMail_Text[90123303]["Title"] = "Consecutive Sign-in Reward"
tSendMail_Text[90123303]["Content"] = "You`ve signed in 21 consecutive days. Please claim the reward."
tSendMail_Text[90123303]["Talk"] = "You received a 21-day Sign-in Pack for successfully signing in 21 consecutive days!"

tSendMail_Text[90123304] = {}
tSendMail_Text[90123304]["Sender"] = "System"
tSendMail_Text[90123304]["Title"] = "Consecutive Sign-in Reward"
tSendMail_Text[90123304]["Content"] = "You`ve signed in 28 consecutive days. Please claim the reward."
tSendMail_Text[90123304]["Talk"] = "You received a 28-day Sign-in Pack for successfully signing in 28 consecutive days!"
------------------------------------------------------------------------------------
--Name?            180801[????][????]PC???????
--Creator:      ??
--Created:     2018/08/01
------------------------------------------------------------------------------------
tSendMail_Text["Mail"] = {}
tSendMail_Text["Mail"]["Sender"] = "System"
tSendMail_Text["Mail"]["Title"] = "Mail"
tSendMail_Text["Mail"]["Content"] = {}
tSendMail_Text["Mail"]["Content"][1] = "Receive %s Silvers"
tSendMail_Text["Mail"]["Content"][2] = "Receive %s CPs"
tSendMail_Text["Mail"]["Content"][3] = "Receive %s Beans"
