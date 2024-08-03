

using Long.Kernel.States.Items;
using Long.Kernel.States.Magics;
using Long.Kernel.States.User;
using Long.Kernel.States.World;

namespace Long.Kernel.States.Events
{
    public abstract class GameEvent
    {
        protected enum EventStage
        {
            Idle,
            Running,
            Ending
        }

        public enum EventType
        {
            None,
            TimedGuildWar,
            GuildPk,
            GuildContest,
            LineSkillPk,
            UserArenaQualifier,
            TeamArenaQualifier,
            QuizShow,
            CaptureTheFlag,
            ElitePkTournament,
            Exorcism,
            HorseRacing,
            TeamPkTournament,
            SkillTeamPkTournament,
            Limit
        }

        public static bool IsQualifierEvent(EventType type)
        {
            switch (type)
            {
                case EventType.UserArenaQualifier:
                case EventType.TeamArenaQualifier:
                case EventType.ElitePkTournament:
                case EventType.TeamPkTournament:
                case EventType.SkillTeamPkTournament:
                    return true;
            }
            return false;
        }

        public const int RANK_REFRESH_RATE_MS = 10000;

        private readonly TimeOutMS eventCheck;

        protected GameEvent(string name, int timeCheck = 1000)
        {
            Name = name;
            eventCheck = new TimeOutMS(timeCheck);
        }

        public virtual EventType Identity { get; } = EventType.None;

        public string Name { get; }

        protected EventStage Stage { get; set; } = EventStage.Idle;

        public virtual GameMap Map { get; protected set; }

        public virtual bool IsInTime { get; } = false;
        public virtual bool IsActive { get; } = false;
        public virtual bool IsEnded { get; } = false;

        public virtual bool IsAttackEnable(Role sender, Magic magic = null) => true;

        public bool ToNextTime() => eventCheck.ToNextTime();

        public virtual bool IsAllowedToJoin(Role sender)
        {
            return true;
        }

        public virtual Task<bool> CreateAsync()
        {
            return Task.FromResult(false);
        }

        public virtual Task OnEnterAsync(Character sender)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnEnterMapAsync(Character sender)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnExitAsync(Character sender)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnExitMapAsync(Character sender, GameMap currentMap)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnMoveAsync(Character sender)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnAttackAsync(Character sender)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnBeAttackAsync(Role attacker, Role target, int damage = 0, Magic magic = null)
        {
            return Task.CompletedTask;
        }

        public virtual Task<int> GetDamageLimitAsync(Role attacker, Role target, int power)
        {
            return Task.FromResult(power);
        }

        public virtual Task OnHitAsync(Role attacker, Role target, Magic magic = null) // magic null is auto attack
        {
            return Task.CompletedTask;
        }

        public virtual Task OnKillAsync(Role attacker, Role target, Magic magic = null)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnBeKillAsync(Role attacker, Role target, Magic magic = null)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Executes when user revive.
        /// </summary>
        /// <param name="sender">The user being revived.</param>
        /// <param name="selfRevive">User pressed revive button?</param>
        /// <returns>True if this overrides the default revive behavior.</returns>
        public virtual Task<bool> OnReviveAsync(Character sender, bool selfRevive)
        {
            return Task.FromResult(false);
        }

        public virtual Task OnLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Perform daily clean-up on event. MUST NOT CHANGE USER DATA!
        /// User data is refreshed on their own class, this must clean up the event data!
        /// </summary>
        public virtual Task OnDailyResetAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnTimerAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task<bool> OnActionCommandAsync(string param, Character user, Role role, Item item, string input)
        {
            return Task.FromResult(true);
        }

        public virtual Task<RevivePosition> GetRevivePositionAsync(Character sender)
        {
            return Task.FromResult(new RevivePosition(sender.RecordMapIdentity, sender.RecordMapX, sender.RecordMapY));
        }

        public virtual bool IsInscribed(uint idUser)
        {
            return false;
        }

        public virtual bool IsInEventMap(uint idMap)
        {
            return false;
        }

        public enum SetMeed
        {
            CaptureTheFlag = 1
        }

        public struct RevivePosition
        {
            public uint Id;
            public ushort X;
            public ushort Y;

            public RevivePosition(uint recordMapIdentity, ushort recordMapX, ushort recordMapY) : this()
            {
                Id = recordMapIdentity;
                X = recordMapX;
                Y = recordMapY;
            }
        }
    }
}
