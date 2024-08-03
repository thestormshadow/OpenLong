using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using System.Drawing;

namespace Long.Kernel.States.Npcs
{
    public abstract class BaseNpc : Role
    {
        protected BaseNpc(uint idNpc)
        {
            Identity = idNpc;
        }

        public virtual async Task<bool> InitializeAsync()
        {
            ShopGoods = await GoodsRepository.GetAsync(Identity);
            return true;
        }

        #region Constants

        public const int
            ROLE_NPC_NONE = 0,			// 非法NPC
            ROLE_SHOPKEEPER_NPC = 1,			// 商店NPC
            ROLE_TASK_NPC = 2,			// 任务NPC
            ROLE_STORAGE_NPC = 3,			// 寄存处NPC
            ROLE_TRUNCK_NPC = 4,			// 箱子NPC
            ROLE_FORGE_NPC = 6,			// 锻造NPC
            ROLE_EMBED_NPC = 7,			// 镶嵌NPC
            ROLE_COMPOSE_NPC = 8,			// 乾坤五行炉
            ROLE_STATUARY_NPC = 9,			// 雕像NPC
            ROLE_SYNFLAG_NPC = 10,			// 帮派标记NPC
            ROLE_PLAYER = 11,			// 其他玩家
            ROLE_HERO = 12,			// 自己
            ROLE_MONSTER = 13,			// 怪物
            ROLE_BOOTH_NPC = 14,			// 摊位NPC
            SYNTRANS_NPC = 15,			// 帮派传送NPC(用于00:00收费)(LINKID为固定NPC的ID，与其它使用LINKID的互斥)
            ROLE_BOOTH_FLAG_NPC = 16,			// 摊位标志NPC
            ROLE_MOUSE_NPC = 17,			// 鼠标上的NPC
            ROLE_MAGICITEM = 18,			// 陷阱火墙
            ROLE_DICE_NPC = 19,			// 骰子NPC
            ROLE_WEAPONGOAL_NPC = 21,			// 近身攻击NPC
            ROLE_MAGICGOAL_NPC = 22,			// 魔法攻击靶子NPC
            ROLE_BOWGOAL_NPC = 23,			// 弓箭靶子NPC
            ROLE_TARGET_NPC = 24,			// 挨打，不触发任务
            ROLE_FURNITURE_NPC = 25,			// 家具NPC
            ROLE_CITY_GATE_NPC = 26,			// 城门NPC
            ROLE_NEIGHBOR_DOOR = 27,			// 邻居的门
            ROLE_CALL_PET = 28,			// 召唤兽
            ROLE_TELEPORT = 29,			// 传送NPC
            ROLE_MOUNT_APPEND = 30,			// 骑宠合成NPC
            ROLE_FAMILY_OCCUPY_NPC = 31,
            TASK_SHOPKEEPER_NPC = 32,			// 任务商店NPC
            TASK_FORGE_NPC = 33,			// 任务锻造NPC
            TASK_EMBED_NPC = 34,			// 任务镶嵌NPC
            COMPOSE_GEM_NPC = 35,			// 宝石合成NPC
            REDUCE_DMG_NPC = 36,			// 装备神佑NPC
            MAKE_ITEM_HOLE_NPC = 37,			// 物品开洞NPC
            SOLIDIFY_ITEM_NPC = 38,			// 固化装备NPC
            COMPETE_BARRIER_NPC_ = 39,			// 骑宠比赛栅栏NPC
            FACTION_MATCH_FLAG = 40,			// 帮派争霸赛旗帜
            FM_LEFT_BARRIER_NPC_ = 41,			// 帮派争霸左大本营栅栏
            FM_RIGHT_BARRIER_NPC_ = 42,			// 帮派争霸右大本营栅栏
            WARFLAG_FLAGALTAR = 43,			// 跨服战旗争霸赛战旗旗坛
            WARFLAG_PRESENTFLAG = 44,			// 跨服战旗争霸赛授旗NPC
            WARFLAG_FLAG = 45,			// 跨服战旗争霸赛战旗
            VEXILLUM_FLAGALTAR = 46,			// 新战旗争霸赛旗坛(被砍的大战旗)
            VEXILLUM_FLAG = 47,			// 新战旗争霸赛战旗(陷阱类的小战旗)
            SLOT_MACHINE_NPC = 60,			// 老虎机NPC
            OS_LANDLORD = 61,			// 跨服玩家可攻击NPC
            CHANGE_LOOKFACE_TASK_NPC = 62,			// 可以改变lookface的task npc
            ROLE_DESTRUCTIBLE_NPC = 63,			// 可破坏NPC
            ROLE_SYNBUFF_NPC = 64,			// 帮派Buff柱NPC
            SYN_BOSS = 65,			// 帮派BOSS
            ROLE_3DFURNITURE_NPC = 101,			// 3D家具NPC 
            ROLE_CITY_WALL_NPC = 102,			// 城墙NPC
            ROLE_CITY_MOAT_NPC = 103,			// 护城河NPC
            ROLE_TEXAS_TABLE_NPC = 110,			// 赌桌NPC
            ROLE_TRAP_MONSTER = 111,			// 陷阱Monster
            ROLE_ROULETTE_TABLE_NPC = 112,			// 轮盘赌桌NPC
            FRONTIER_SERVER_TRANS_NPC = 113,			// 国境服传送NPC
            ROLE_TRAP_CAN_BE_ATTACK_NPC = 114,			// 可以被攻击的陷阱NPC
            ROLE_SH_TABLE_NPC = 115,			// 梭哈赌桌NPC
            ROLE_RAIDER_TABLE_NPC = 116,			// 夺宝奇兵赌桌NPC
            ROLE_TURRET_NPC = 117,			// 炮台NPC
            ROLE_DOMINO_TABLE_NPC = 118,			// 多米诺赌桌NPC
            ROLE_TRAP_TRAPSORT_PORTAL = 119,			// 水道蓝色符文-瞬间传送门
            ROLE_NEWSLOT_NPC = 120,			// 5*3老虎机NPC
            ROLE_BLACKJACK_TABLE_NPC = 121,			// 21点赌桌NPC
            ROLE_FRUIT_MACHINE_NPC = 122,			// 水果机NPC
            ROLE_SHEN_DING_NPC = 123,			// 神鼎NPC
            ROLE_SWORD_PRISON = 124;			// 剑牢NPC

        [Flags]
        public enum NpcSort
        {
            None = 0,
            Task = 1,           // ÈÎÎñÀà
            Recycle = 2,            // ¿É»ØÊÕÀà
            Scene = 4,          // ³¡¾°Àà(´øµØÍ¼Îï¼þ)
            LinkMap = 8,            // ¹ÒµØÍ¼Àà(LINKIDÎªµØÍ¼ID£¬ÓëÆäËüÊ¹ÓÃLINKIDµÄ»¥³â)
            DieAction = 16,         // ´øËÀÍöÈÎÎñ(LINKIDÎªACTION_ID£¬ÓëÆäËüÊ¹ÓÃLINKIDµÄ»¥³â)
            DelEnable = 32,         // ¿ÉÒÔÊÖ¶¯É¾³ý(²»ÊÇÖ¸Í¨¹ýÈÎÎñ)
            Event = 64,         // ´ø¶¨Ê±ÈÎÎñ, Ê±¼äÔÚdata3ÖÐ£¬¸ñÊ½ÎªMMWWHHMMSS¡£(LINKIDÎªACTION_ID£¬ÓëÆäËüÊ¹ÓÃLINKIDµÄ»¥³â)
            Table = 128,            // ´øÊý¾Ý±íÀàÐÍ

            //		NPCSORT_SHOP		= ,			// ÉÌµêÀà
            //		NPCSORT_DICE		= ,			// ÷»×ÓNPC

            NpcsortUseLinkId = LinkMap | DieAction | Event,
        };

        #endregion

        #region Common Checks

        public bool IsLinkNpc()
        {
            return (Sort & NpcSort.LinkMap) != 0;
        }

        public bool IsShopNpc()
        {
            return Type == ROLE_SHOPKEEPER_NPC;
        }

        public bool IsTaskNpc()
        {
            return Type == ROLE_TASK_NPC;
        }

        public bool IsStorageNpc()
        {
            return Type == ROLE_STORAGE_NPC;
        }

        public bool IsUserNpc()
        {
            return OwnerType == 1;
        }

        public bool IsSynNpc()
        {
            return OwnerType == 2;
        }

        public bool IsFamilyNpc()
        {
            return OwnerType == 4;
        }

        public bool IsSynFlag()
        {
            return Type == ROLE_SYNFLAG_NPC && IsSynNpc();
        }

        public bool IsFlag()
        {
            return Type == VEXILLUM_FLAG;
        }

        public bool IsSysTrans()
        {
            return Type == SYNTRANS_NPC;
        }

        public bool IsCtfFlag()
        {
            return Type == VEXILLUM_FLAGALTAR && IsSynNpc();
        }

        public bool IsAwardScore()
        {
            return IsSynFlag() || IsCtfFlag();
        }

        public bool IsGoal()
        {
            return Type == ROLE_WEAPONGOAL_NPC || Type == ROLE_MAGICGOAL_NPC || Type == ROLE_BOWGOAL_NPC;
        }

        public bool IsCityGate()
        {
            return Type == ROLE_CITY_GATE_NPC;
        }

        public bool IsSlotNpc()
        {
            return Type == SLOT_MACHINE_NPC;
        }

        public bool IsSynMoneyEmpty()
        {
            if (!IsSynFlag())
            {
                return false;
            }

            ISyndicate syn = SyndicateManager.GetSyndicate((int)OwnerIdentity);
            return syn != null && syn.Money <= 0;
        }

        #endregion

        #region Type Identity

        public virtual ushort Type { get; }

        public virtual uint OwnerType { get; set; }

        public virtual NpcSort Sort { get; }

        public virtual int Base { get; }

        public virtual Task DelNpcAsync()
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Position

        /// <remarks>If ran in a different thread, remember to send this action to map queue.</remarks>
        public virtual async Task<bool> ChangePosAsync(uint idMap, ushort x, ushort y)
        {
            GameMap map = MapManager.GetMap(idMap);
            if (map != null)
            {
                if (!map.IsValidPoint(x, y) && idMap != 5000)
                {
                    return false;
                }

                await LeaveMapAsync();
                this.idMap = idMap;
                X = x;
                Y = y;

                Task crossThreadTask()
                {
                    return EnterMapAsync();
                }

                if (map.Partition == Map?.Partition)
                {
                    await crossThreadTask();
                }
                else
                {
                    QueueAction(crossThreadTask);
                }
                return true;
            }
            return false;
        }

        #endregion

        #region Map

        public override async Task EnterMapAsync()
        {
            Map = MapManager.GetMap(MapIdentity);
            if (Map != null)
            {
                await Map.AddAsync(this);
                Map.AddTerrainObject(Identity, X, Y, Mesh);
            }
        }

        public override async Task LeaveMapAsync()
        {
            if (Map != null)
            {
                await Map.RemoveAsync(Identity);
                Map.DelTerrainObj(Identity);
            }
        }

        #endregion

        #region Functions

        #region Shop

        public List<DbGoods> ShopGoods = new();

        #endregion

        #region Task

        public async Task<bool> ActivateNpcAsync(Character user)
        {
            bool result = false;
            uint task = TestTasks(user);
            if (task != 0)
            {
                result = await GameAction.ExecuteActionAsync(task, user, this, null, "");
            }
            else if (user.IsPm())
            {
                await user.SendAsync($"Unhandled NPC[{Identity}:{Name}]->{Task0},{Task1},{Task2},{Task3},{Task4},{Task5},{Task6},{Task6},{Task7}", TalkChannel.Talk, Color.Red);
            }

            return result;
        }

        private uint TestTasks(Character user)
        {
            for (int i = 0; i < 8; i++)
            {
                DbTask task = ScriptManager.GetTask(GetTask($"task{i}"));
                if (task != null && user.TestTask(task))
                {
                    return task.IdNext;
                }
            }
            return 0;
        }

        #endregion

        #endregion

        #region Task and Data

        public int GetData(string szAttr)
        {
            switch (szAttr.ToLower())
            {
                case "data0": return Data0;
                case "data1": return Data1;
                case "data2": return Data2;
                case "data3": return Data3;
            }
            return 0;
        }

        public bool SetData(string szAttr, int value)
        {
            switch (szAttr.ToLower())
            {
                case "data0": Data0 = value; return true;
                case "data1": Data1 = value; return true;
                case "data2": Data2 = value; return true;
                case "data3": Data3 = value; return true;
            }
            return false;
        }

        public bool AddData(string szAttr, int value)
        {
            switch (szAttr.ToLower())
            {
                case "data0": Data0 += value; return true;
                case "data1": Data1 += value; return true;
                case "data2": Data2 += value; return true;
                case "data3": Data3 += value; return true;
            }

            return false;
        }

        public uint GetTask(string task)
        {
            switch (task.ToLower())
            {
                case "task0": return Task0;
                case "task1": return Task1;
                case "task2": return Task2;
                case "task3": return Task3;
                case "task4": return Task4;
                case "task5": return Task5;
                case "task6": return Task6;
                case "task7": return Task7;
                default: return 0;
            }
        }

        public virtual bool Vending { get; set; }
        public virtual uint Task0 { get; }
        public virtual uint Task1 { get; }
        public virtual uint Task2 { get; }
        public virtual uint Task3 { get; }
        public virtual uint Task4 { get; }
        public virtual uint Task5 { get; }
        public virtual uint Task6 { get; }
        public virtual uint Task7 { get; }

        public virtual int Data0 { get; set; }
        public virtual int Data1 { get; set; }
        public virtual int Data2 { get; set; }
        public virtual int Data3 { get; set; }

        public virtual string DataStr { get; set; }

        #endregion

        #region Database

        public virtual Task<bool> SaveAsync()
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> DeleteAsync()
        {
            return Task.FromResult(true);
        }


        #endregion
    }
}
