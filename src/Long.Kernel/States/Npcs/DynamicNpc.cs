using System.Collections.Concurrent;
using System.Globalization;
using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;

namespace Long.Kernel.States.Npcs
{
    public sealed class DynamicNpc : BaseNpc
    {
        private readonly DbDynanpc npc;
        private readonly ConcurrentDictionary<uint, Score> scores = new();
        private readonly TimeOut deathTimer = new();

        public DynamicNpc(DbDynanpc npc)
            : base(npc.Id)
        {
            this.npc = npc;

            idMap = npc.Mapid;
            currentX = npc.Cellx;
            currentY = npc.Celly;

            Name = npc.Name;

            if (IsSynFlag() && OwnerIdentity > 0)
            {

				//ISyndicate syn = SyndicateManager.GetSyndicate((int)OwnerIdentity);
    //            if (syn != null)
    //            {
    //                Name = syn.Name;
    //            }
            }
        }

        #region Type

        public override uint Identity
        {
            get => npc.Id % 1_000_000;
            init => base.Identity = value;
        }

        public override uint Mesh
        {
            get => npc.Lookface;
            set => npc.Lookface = (ushort)value;
        }

        public override ushort Type => npc.Type;

        public void SetType(ushort type)
        {
            npc.Type = type;
        }

        public override NpcSort Sort => (NpcSort)npc.Sort;

        public override int Base => (int)npc.Base;

        public void SetSort(ushort sort)
        {
            npc.Sort = sort;
        }

        public override uint OwnerType
        {
            get => npc.OwnerType;
            set => npc.OwnerType = value;
        }

        public override uint OwnerIdentity
        {
            get => npc.Ownerid;
            set => npc.Ownerid = value;
        }

        public override int SizeAddition => 1;

        #endregion

        #region Life

        public override uint Life
        {
            get => npc.Life;
            set => npc.Life = value;
        }

        public override uint MaxLife => npc.Maxlife;

        #endregion

        #region Position

        public override async Task<bool> ChangePosAsync(uint idMap, ushort x, ushort y)
        {
            if (await base.ChangePosAsync(idMap, x, y))
            {
                npc.Mapid = idMap;
                npc.Celly = y;
                npc.Cellx = x;
                await SaveAsync();
                return true;
            }

            return false;
        }

        #endregion

        #region Ownership

        public override async Task DelNpcAsync()
        {
            await SetAttributesAsync(ClientUpdateType.Hitpoints, 0);
            deathTimer.Startup(5);

            if (IsSynFlag() || IsCtfFlag())
            {
                await Map.SetStatusAsync(1, false);
            }
            else if (!IsGoal())
            {
                await DeleteAsync();
            }
            await LeaveMapAsync();
        }

        public async Task<bool> SetOwnerAsync(uint idOwner, bool withLinkMap = false)
        {
            if (idOwner == 0)
            {
                OwnerIdentity = 0;
                Name = "";

                await BroadcastRoomMsgAsync(new MsgNpcInfoEx(this), false);
                await SaveAsync();
                return true;
            }

            OwnerIdentity = idOwner;
            if (IsSynNpc())
            {
                ISyndicate syn = SyndicateManager.GetSyndicate((int)OwnerIdentity);
                if (syn == null)
                {
                    OwnerIdentity = 0;
                    Name = "";
                }
                else
                {
                    Name = syn.Name;
                }
            }

            // TODO
            /*if (withLinkMap)
            {
                foreach (var player in Kernel.RoleManager.QueryUserSetByMap(MapIdentity))
                {

                }
            }*/

            await SaveAsync();
            await BroadcastRoomMsgAsync(new MsgNpcInfoEx(this) { Lookface = (ushort)npc.Lookface }, false);
            return true;
        }

        #endregion

        #region Score

        public async Task CheckFightTimeAsync()
        {
            if (!IsSynFlag())
            {
                return;
            }

            if (Data1 == 0 || Data2 == 0)
            {
                return;
            }

            var strNow = "";
            DateTime now = DateTime.Now;
            strNow += (now.DayOfWeek == 0 ? 7 : (int)now.DayOfWeek).ToString(CultureInfo.InvariantCulture);
            strNow += now.Hour.ToString("00");
            strNow += now.Minute.ToString("00");
            strNow += now.Second.ToString("00");

            int now0 = int.Parse(strNow);
            if (now0 < Data1 || now0 >= Data2)
            {
                if (Map.IsWarTime())
                {
                    await OnFightEndAsync();
                }

                return;
            }

            if (!Map.IsWarTime())
            {
                await Map.SetStatusAsync(1, true);
                await Map.BroadcastMsgAsync(StrWarStart, TalkChannel.System);
            }
        }

        public async Task OnFightEndAsync()
        {
            await Map.SetStatusAsync(1, false);
            await Map.BroadcastMsgAsync(StrWarEnd, TalkChannel.System);
            // Map.ResetBattle();
        }

        public async Task BroadcastRankingAsync()
        {
            if (!IsSynFlag() || /*!IsAttackable(null) ||*/ scores.Count == 0)
            {
                return;
            }

            await Map.BroadcastMsgAsync(StrWarRankingStart, TalkChannel.GuildWarRight1);
            var i = 0;
            foreach (Score score in scores.Values.OrderByDescending(x => x.Points))
            {
                if (i++ >= 5)
                {
                    break;
                }

                await Map.BroadcastMsgAsync(string.Format(StrWarRankingNo, i, score.Name, score.Points),
                                            TalkChannel.GuildWarRight2);
            }
        }

        public void AddWarScore(uint id, string name, long score)
        {
            if (!scores.ContainsKey(id))
            {
                scores.TryAdd(id, new Score(id, name));
            }

            scores[id].Points += score;
        }
		public void AddSynWarScore(ISyndicate syn, long score)
		{
			if (syn == null)
			{
				return;
			}

			if (!scores.ContainsKey(syn.Identity))
			{
				scores.TryAdd(syn.Identity, new Score(syn.Identity, syn.Name));
			}

			scores[syn.Identity].Points += score;
		}
		public Score GetTopScore()
        {
            return scores.Values.OrderByDescending(x => x.Points).ThenBy(x => x.Identity).FirstOrDefault();
        }

        public List<Score> GetTopScores()
        {
            return scores.Values.OrderByDescending(x => x.Points).ToList();
        }

        public void ClearScores()
        {
            scores.Clear();
        }

        #endregion

        #region Task

        public uint LinkId
        {
            get => npc.Linkid;
            set => npc.Linkid = value;
        }

        public override uint Task0 => npc.Task0;
        public override uint Task1 => npc.Task1;
        public override uint Task2 => npc.Task2;
        public override uint Task3 => npc.Task3;
        public override uint Task4 => npc.Task4;
        public override uint Task5 => npc.Task5;
        public override uint Task6 => npc.Task6;
        public override uint Task7 => npc.Task7;

        public override int Data0
        {
            get => npc.Data0;
            set => npc.Data0 = value;
        }

        public override int Data1
        {
            get => npc.Data1;
            set => npc.Data1 = value;
        }

        public override int Data2
        {
            get => npc.Data2;
            set => npc.Data2 = value;
        }

        public override int Data3
        {
            get => npc.Data3;
            set => npc.Data3 = value;
        }

        public override string DataStr
        {
            get => npc.Datastr;
            set => npc.Datastr = value;
        }


        public string OwnerName
        {
            get => npc.OwnerName;
            set => npc.OwnerName = value;
        }
        public string DefaultOwnerName
        {
            get => npc.DefaultOwnerName;
        }
        public uint HarvestDate
        {
            get => npc.HarvestDate;
            set => npc.HarvestDate = value;
        }

        public void SetTask(int id, uint task)
        {
            switch (id)
            {
                case 0:
                    npc.Task0 = task;
                    break;
                case 1:
                    npc.Task1 = task;
                    break;
                case 2:
                    npc.Task2 = task;
                    break;
                case 3:
                    npc.Task3 = task;
                    break;
                case 4:
                    npc.Task4 = task;
                    break;
                case 5:
                    npc.Task5 = task;
                    break;
                case 6:
                    npc.Task6 = task;
                    break;
                case 7:
                    npc.Task7 = task;
                    break;
            }
        }

        public uint GetTask(int id)
        {
            switch (id)
            {
                case 0: return npc.Task0;
                case 1: return npc.Task1;
                case 2: return npc.Task2;
                case 3: return npc.Task3;
                case 4: return npc.Task4;
                case 5: return npc.Task5;
                case 6: return npc.Task6;
                case 7: return npc.Task7;
            }
            return 0;
        }

        #endregion

        #region Database

        public override Task<bool> SaveAsync()
        {
            return ServerDbContext.UpdateAsync(npc);
        }

        public override Task<bool> DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(npc);
        }

        #endregion

        #region Socket

        public override Task SendSpawnToAsync(Character player)
        {
            return player.SendAsync(new MsgNpcInfoEx(this));
        }

        #endregion

        public class Score
        {
            public Score(uint id, string name)
            {
                Identity = id;
                Name = name;
            }

            public uint Identity { get; }
            public string Name { get; }
            public long Points { get; set; }
        }
    }
}
