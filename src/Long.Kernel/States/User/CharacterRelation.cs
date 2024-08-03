using Long.Database.Entities;
using Long.Kernel.Modules.Systems.Guide;
using Long.Kernel.Modules.Systems.Relation;
using Long.Kernel.Modules.Systems.Trade;
using Long.Kernel.Network.Game.Packets;
using System.Collections.Concurrent;

namespace Long.Kernel.States.User
{
    public partial class Character
    {
        #region Marriage

        public bool IsMate(Character user)
        {
            return user.Identity == MateIdentity;
        }

        public bool IsMate(uint idMate)
        {
            return idMate == MateIdentity;
        }

        #endregion

        #region Relation

        public IRelation Relation { get; set; }



		#endregion

		#region Friends        		
		public bool IsFriend(uint idTarget) => Relation.IsFriend(idTarget);
		#endregion

		#region Enemies		
		public bool IsEnemy(uint idTarget) => Relation.IsEnemy(idTarget);
        public async Task CreateEnemyAsync(uint idTarget) => await Relation.AddEnemyAsync(idTarget);
		#endregion

		#region Trade Partner

		public ITradePartnerRelation TradePartnerRelation { get; set; }

        public bool IsValidTradePartner(uint userId)
        {
            if (TradePartnerRelation == null)
            {
                return true;
            }
            return TradePartnerRelation.IsValidTradePartner(userId);
        }

		#endregion

		#region Player Pose

		private uint coupleInteractionTarget;
		private bool coupleInteractionStarted;

		public bool HasCoupleInteraction()
		{
			return coupleInteractionTarget != 0;
		}

		public Character GetCoupleInteractionTarget()
		{
			return Map.GetUser(coupleInteractionTarget);
		}

		public EntityAction CoupleAction { get; private set; }

		public async Task<bool> SetActionAsync(EntityAction action, uint target)
		{
			// hum
			CoupleAction = action;
			coupleInteractionTarget = target;
			return true;
		}

		public void CancelCoupleInteraction()
		{
			CoupleAction = EntityAction.None;
			coupleInteractionTarget = 0;
			PopRequest(RequestType.CoupleInteraction);
			coupleInteractionStarted = false;
		}

		public void StartCoupleInteraction()
		{
			coupleInteractionStarted = true;
		}

		public bool HasCoupleInteractionStarted() => coupleInteractionStarted;

		#endregion

		#region Enlightment

		private readonly TimeOut enlightenTimeExp = new(ENLIGHTENMENT_EXP_PART_TIME);

		public const int ENLIGHTENMENT_MAX_TIMES = 5;
		public const int ENLIGHTENMENT_UPLEV_MAX_EXP = 600;
		public const int ENLIGHTENMENT_EXP_PART_TIME = 60 * 20;
		public const int ENLIGHTENMENT_MIN_LEVEL = 90;

		private const int EnlightenmentUserStc = 1127;

		public uint EnlightenPoints
		{
			get => user.MentorOpportunity;
			set => user.MentorOpportunity = value;
		}

		public uint EnlightenedTimes
		{
			get => user.MentorAchieve;
			set => user.MentorAchieve = value;
		}

		public uint EnlightenExperience
		{
			get => user.MentorUplevTime;
			set => user.MentorUplevTime = value;
		}

		public uint EnlightmentLastUpdate
		{
			get => user.MentorDay;
			set => user.MentorDay = value;
		}

		public void SetEnlightenLastUpdate()
		{
			EnlightmentLastUpdate = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
		}

		public bool CanBeEnlightened(Character mentor)
		{
			if (mentor == null)
			{
				return false;
			}

			if (EnlightenedTimes >= ENLIGHTENMENT_MAX_TIMES)
			{
				return false;
			}

			if (EnlightenExperience >= ENLIGHTENMENT_UPLEV_MAX_EXP / 2 * ENLIGHTENMENT_MAX_TIMES)
			{
				return false;
			}

			if (mentor.Level - Level < 20)
			{
				return false;
			}

			DbStatistic stc = Statistic.GetStc(EnlightenmentUserStc, mentor.Identity);
			if (stc?.Timestamp != null)
			{
				int day = (int)stc.Timestamp;
				int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
				return day != now;
			}
			return true;
		}

		public async Task<bool> EnlightenPlayerAsync(Character target)
		{
			if (Map != null && Map.IsNoExpMap())
			{
				return false;
			}

			var enlightTimes = (int)(EnlightenPoints / 100);
			if (enlightTimes <= 0)
			{
				return false;
			}

			if (target.Level > Level - 20)
			{
				// todo send message
				return false;
			}

			if (!target.CanBeEnlightened(this))
			{
				// todo send message
				return false;
			}

			EnlightenPoints = Math.Max(EnlightenPoints - 100, 0);
			if (target.EnlightenedTimes == 0 || !enlightenTimeExp.IsActive())
			{
				enlightenTimeExp.Startup(ENLIGHTENMENT_EXP_PART_TIME); // 20 minutes
			}

			target.EnlightenedTimes += 1;
			target.EnlightenExperience += ENLIGHTENMENT_UPLEV_MAX_EXP / 2;

			await target.Statistic.AddOrUpdateAsync(EnlightenmentUserStc, Identity, 1, true);

			// we will send instand 300 uplev exp and 300 will be awarded for 5 minutes later
			await target.AwardExperienceAsync(CalculateExpBall(ENLIGHTENMENT_UPLEV_MAX_EXP / 2), true);
			await target.SendAsync(new MsgUserAttrib(Identity, ClientUpdateType.EnlightenPoints, 0));
			//await SynchroAttributesAsync(ClientUpdateType.EnlightenPoints, EnlightenPoints, true);

			await SaveAsync();
			await target.SaveAsync();
			return true;
		}

		public async Task ResetEnlightenmentAsync()
		{
			if (EnlightmentLastUpdate >= uint.Parse(DateTime.Now.ToString("yyyyMMdd")))
			{
				return;
			}

			EnlightmentLastUpdate = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));

			EnlightenedTimes = 0;

			EnlightenPoints = 0;
			if (Level >= 90)
			{
				EnlightenPoints += 100;
			}

			switch (NobilityRank)
			{
				case Modules.Systems.Nobility.NobilityRank.Knight:
				case Modules.Systems.Nobility.NobilityRank.Baron:
					EnlightenPoints += 100;
					break;
				case Modules.Systems.Nobility.NobilityRank.Earl:
				case Modules.Systems.Nobility.NobilityRank.Duke:
					EnlightenPoints += 200;
					break;
				case Modules.Systems.Nobility.NobilityRank.Prince:
					EnlightenPoints += 300;
					break;
				case Modules.Systems.Nobility.NobilityRank.King:
					EnlightenPoints += 400;
					break;
			}

			switch (VipLevel)
			{
				case 1:
				case 2:
				case 3:
					EnlightenPoints += 100;
					break;
				case 4:
				case 5:
					EnlightenPoints += 200;
					break;
				case 6:
					EnlightenPoints += 300;
					break;
			}

			await SynchroAttributesAsync(ClientUpdateType.EnlightenPoints, EnlightenPoints, true);
		}

		#endregion

		public Task SendRelationAsync(Character target)
        {
            return SendAsync(new MsgRelation
            {
                SenderIdentity = target.Identity,
                Level = target.Level,
                BattlePower = target.BattlePower,
                IsSpouse = target.Identity == MateIdentity,
                IsTradePartner = IsValidTradePartner(target.Identity),
                IsTutor = Guide?.IsTutor(target.Identity) == true,
                TargetIdentity = Identity
            });
        }
    }
}
