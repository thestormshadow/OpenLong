using Long.Ai.Processors;
using Long.Database.Entities;
using Serilog;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace Long.Ai.States
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi, Size = 16)]
    public struct StatusInfoStruct
    {
        public int Status;
        public int Power;
        public int Seconds;
        public int Times;

        public StatusInfoStruct(int status, int power, int secs, int times)
            : this()
        {
            Status = status;
            Power = power;
            Seconds = secs;
            Times = times;
        }
    }

    public sealed class StatusOnce : IStatus
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<StatusOnce>();

		private Role owner;
        private TimeOutMS keep;
        private long autoFlash;
        private TimeOutMS interval;

        public StatusOnce()
        {
            owner = null;
            Identity = 0;
        }

        public StatusOnce(Role pOwner)
        {
            owner = pOwner;
            Identity = 0;
        }

        public int Identity { get; private set; }

        public bool IsValid => keep.IsActive() && !keep.IsTimeOut();

        public int Power { get; set; }

        public DbStatus Model { get; set; }

        public byte Level { get; private set; }

        public int RemainingTimes => 0;

        public int Time => keep.GetInterval();

        public int RemainingTime => keep.GetRemain() / 1000;

        public bool GetInfo(ref StatusInfoStruct info)
        {
            info.Power = Power;
            info.Seconds = keep.GetRemain() / 1000;
            info.Status = Identity;
            info.Times = 0;

            return IsValid;
        }

        public async Task<bool> ChangeDataAsync(int power, int secs, int times = 0, uint caster = 0U)
        {
            try
            {
                Power = power;
                keep.SetInterval((int)Math.Min(int.MaxValue, (long)secs * 1000));
                keep.Update();

                if (caster != 0)
                {
                    CasterId = caster;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IncTime(int ms, int limit)
        {
            int nInterval = Math.Min(ms + keep.GetRemain(), limit);
            keep.SetInterval(nInterval);
            return keep.Update();
        }

        public bool ToFlash()
        {
            if (!IsValid)
            {
                return false;
            }

            if (autoFlash == 0 && keep.GetRemain() <= 5000)
            {
                autoFlash = 1;
                return true;
            }

            return false;
        }

        public uint CasterId { get; private set; }

        public bool IsUserCast => CasterId == owner.Identity || CasterId == 0;

        public async Task OnTimerAsync()
        {
            if (!IsValid || !keep.ToNextTime())
            {
                return;
            }

            try
            {
                switch (Identity)
                {
                    
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "StatusOnce.OnTimerAsync: {Message}", ex.Message);
            }
        }

        public async Task<bool> CreateAsync(Role role, int status, int power, int secs, int times, uint caster = 0,
                                            byte level = 0, bool save = false)
        {
            owner = role;
            CasterId = caster;
            Identity = status;
            Power = power;
            keep = new TimeOutMS(Math.Min(int.MaxValue, secs * 1000));
            keep.Startup((int)Math.Min((long)secs * 1000, int.MaxValue));
            keep.Update();
            interval = new TimeOutMS(1000);
            interval.Update();
            Level = level;
            return true;
        }
    }

    public sealed class StatusMore : IStatus
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<StatusMore>();

		private Role owner;
        private TimeOut keep;
        private long autoFlash;

        public StatusMore()
        {
            owner = null;
            Identity = 0;
        }

        public StatusMore(Role owner)
        {
            this.owner = owner;
            Identity = 0;
        }

        public int Identity { get; private set; }

        public bool IsValid => RemainingTimes > 0;

        public int Power { get; set; }

        public DbStatus Model { get; set; }

        public byte Level { get; private set; }

        public int RemainingTimes { get; private set; }

        public int RemainingTime => keep.GetRemain();

        public int Time => keep.GetInterval();

        public bool GetInfo(ref StatusInfoStruct info)
        {
            info.Power = Power;
            info.Seconds = keep.GetRemain();
            info.Status = Identity;
            info.Times = RemainingTimes;

            return IsValid;
        }

        public async Task<bool> ChangeDataAsync(int power, int secs, int times = 0, uint caster = 0U)
        {
            try
            {
                Power = power;
                keep.SetInterval(secs);
                keep.Update();
                CasterId = caster;

                if (times > 0)
                {
                    RemainingTimes = times;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IncTime(int ms, int limit)
        {
            int nInterval = Math.Min(ms + keep.GetRemain(), limit);
            keep.SetInterval(nInterval);
            return keep.Update();
        }

        public bool ToFlash()
        {
            if (!IsValid)
            {
                return false;
            }

            if (autoFlash == 0 && keep.GetRemain() <= 5000)
            {
                autoFlash = 1;
                return true;
            }

            return false;
        }

        public uint CasterId { get; private set; }

        public bool IsUserCast => CasterId == owner.Identity || CasterId == 0;

        public async Task OnTimerAsync()
        {
            try
            {
                if (!IsValid || !keep.ToNextTime())
                {
                    return;
                }

                if (owner != null)
                {
                    RemainingTimes--;
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "StatusMore.OnTimerAsync: {Message}", ex.Message);
            }
        }

        ~StatusMore()
        {
            // todo destroy and detach status
        }

        public async Task<bool> CreateAsync(Role role, int status, int power, int secs, int times, uint caster = 0,
                                            byte level = 0, bool save = false)
        {
            owner = role;
            Identity = status;
            Power = power;
            keep = new TimeOut(secs);
            keep.Update(); // no instant start
            RemainingTimes = times;
            CasterId = caster;
            Level = level;
            return true;
        }
    }

    public sealed class StatusSet
    {
        public const int NONE = 0,
                         CRIME = 1,
                         POISONED = 2,
                         FULL_INVISIBLE = 3,
                         FADE = 4,
                         START_XP = 5,
                         GHOST = 6,
                         TEAM_LEADER = 7,
                         STAR_OF_ACCURACY = 8,
                         SHIELD = 9,
                         STIGMA = 10,
                         DEAD = 11,
                         INVISIBLE = 12,
                         UNKNOWN13 = 13,
                         UNKNOWN14 = 14,
                         RED_NAME = 15,
                         BLACK_NAME = 16,
                         UNKNOWN17 = 17,
                         UNKNOWN18 = 18,
                         SUPERMAN = 19,
                         REFLECTTYPE_THING = 20,
                         DIF_REFLECT_THING = 21,
                         FREEZE = 22,
                         PARTIALLY_INVISIBLE = 23,
                         CYCLONE = 24,
                         UNKNOWN25 = 25,
                         UNKNOWN26 = 26,
                         DODGE = 27,
                         FLY = 28,
                         INTENSIFY = 29,
                         UNKNOWN30 = 30,
                         LUCKY_DIFFUSE = 31,
                         LUCKY_ABSORB = 32,
                         CURSED = 33,
                         HEAVEN_BLESS = 34,
                         TOP_GUILD = 35,
                         TOP_DEP = 36,
                         MONTH_PK = 37,
                         WEEK_PK = 38,
                         TOP_WARRIOR = 39,
                         TOP_TRO = 40,
                         TOP_ARCHER = 41,
                         TOP_WATER = 42,
                         TOP_FIRE = 43,
                         TOP_NINJA = 44,
                         POISON_STAR = 45,
                         TOXIC_FOG = 46,
                         VORTEX = 47,
                         FATAL_STRIKE = 48,
                         ORANGE_HALO_GLOW = 49,
                         UNKNOWN50 = 50,
                         LOW_VIGOR_UNABLE_TO_JUMP = 51,
                         RIDING = 51,
                         TOP_SPOUSE = 52,
                         SPARKLE_HALO = 53,
                         NO_POTION = 54,
                         DAZED = 55,
                         BLUE_RESTORE_AURA = 56,
                         MOVE_SPEED_RECOVERED = 57,
                         SUPER_SHIELD_HALO = 58,
                         HUGE_DAZED = 59,
                         ICE_BLOCK = 60,
                         CONFUSED = 61,
                         UNKNOWN62 = 62,
                         UNKNOWN63 = 63,
                         UNKNOWN64 = 64,
                         WEEKLY_TOP8_PK = 65,
                         WEEKLY_TOP2_PK_GOLD = 66,
                         WEEKLY_TOP2_PK_BLUE = 67,
                         MONTHLY_TOP8_PK = 68,
                         MONTHLY_TOP2_PK = 69,
                         MONTHLY_TOP3_PK = 70,
                         TOP8_FIRE = 71,
                         TOP2_FIRE = 72,
                         TOP3_FIRE = 73,
                         TOP8_WATER = 74,
                         TOP2_WATER = 75,
                         TOP3_WATER = 76,
                         TOP8_NINJA = 77,
                         TOP2_NINJA = 78,
                         TOP3_NINJA = 79,
                         TOP8_WARRIOR = 80,
                         TOP2_WARRIOR = 81,
                         TOP3_WARRIOR = 82,
                         TOP8_TROJAN = 83,
                         TOP2_TROJAN = 84,
                         TOP3_TROJAN = 85,
                         TOP8_ARCHER = 86,
                         TOP2_ARCHER = 87,
                         TOP3_ARCHER = 88,
                         TOP3_SPOUSE_BLUE = 89,
                         TOP2_SPOUSE_BLUE = 90,
                         TOP3_SPOUSE_YELLOW = 91,
                         CONTESTANT = 92,
                         CHAIN_BOLT_ACTIVE = 93,
                         AZURE_SHIELD = 94,
                         AZURE_SHIELD_FADE = 95,
                         CARRYING_FLAG = 96,
                         UNKNOWN97 = 97,
                         TYRANT_AURA_TEAM = 98,
                         TYRANT_AURA = 99,
                         FEND_AURA_TEAM = 100,
                         FEND_AURA = 101,
                         METAL_AURA_TEAM = 102,
                         METAL_AURA = 103,
                         WOOD_AURA_TEAM = 104,
                         WOOD_AURA = 105,
                         WATER_AURA_TEAM = 106,
                         WATER_AURA = 107,
                         FIRE_AURA_TEAM = 108,
                         FIRE_AURA = 109,
                         EARTH_AURA_TEAM = 110,
                         EARTH_AURA = 111,
                         SOUL_SHACKLE = 112,
                         OBLIVION = 113,
                         UNKNOWN114 = 114,
                         TOP_MONK = 115,
                         TOP8_MONK = 116,
                         TOP2_MONK = 117,
                         TOP3_MONK = 118,
                         CTF_FLAG = 119,
                         SCURVY_BOMB = 120,
                         CANNON_BARRAGE = 121,
                         BLACK_BEARDS_REVENGE = 122,
                         TOP_PIRATE = 123,
                         TOP_PIRATE8 = 124,
                         TOP_PIRATE2 = 125,
                         TOP_PIRATE3 = 126,
                         DEFENSIVE_INSTANCE = 127,
                         MAGIC_DEFENDER = 129,
                         ASSASSIN = 146,
                         BLADE_FLURRY = 147,
                         KINETIC_SPARK = 148,
                         AUTO_HUNTING = 149;

		private static readonly Serilog.ILogger logger = Log.ForContext<StatusSet>();

		private readonly Role owner;
        public ConcurrentDictionary<int, IStatus> Status;

        public StatusSet(Role role)
        {
            if (role == null)
            {
                return;
            }

            owner = role;

            Status = new ConcurrentDictionary<int, IStatus>(5, 64);
        }

        private ulong StatusFlag1
        {
            get => owner.StatusFlag1;
            set => owner.StatusFlag1 = value;
        }

        private ulong StatusFlag2
        {
            get => owner.StatusFlag2;
            set => owner.StatusFlag2 = value;
        }

        private uint StatusFlag3
        {
            get => owner.StatusFlag3;
            set => owner.StatusFlag3 = value;
        }

        public IStatus this[int nKey]
        {
            get
            {
                try
                {
                    return Status.TryGetValue(nKey, out IStatus ret) ? ret : null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public int GetAmount()
        {
            return Status.Count;
        }

        public IStatus GetObjByIndex(int nKey)
        {
            return Status.TryGetValue(nKey, out IStatus ret) ? ret : null;
        }

        public IStatus GetObj(ulong nKey, bool b64 = false)
        {
            return Status.TryGetValue(InvertFlag(nKey, b64), out IStatus ret) ? ret : null;
        }

        public async Task<bool> AddObjAsync(IStatus status)
        {
            var info = new StatusInfoStruct();
            status.GetInfo(ref info);
            if (Status.ContainsKey(info.Status))
            {
                return false; // status already exists
            }

            int flagRef = (info.Status - 1) % 64;
            ulong flag = 1UL << flagRef;
            if (info.Status < 65)
            {
                StatusFlag1 |= flag;
            }
            else if (info.Status < 129)
            {
                StatusFlag2 |= flag;
            }
            else
            {
                StatusFlag3 |= (uint)flag;
            }

            Status.TryAdd(info.Status, status);
            return true;
        }

        public async Task<bool> DelObjAsync(int nFlag)
        {
            if (nFlag > 192)
            {
                return false;
            }

            if (!Status.TryRemove(nFlag, out IStatus status))
            {
                return false;
            }

            int flagRef = (nFlag - 1) % 64;
            ulong uFlag = 1UL << flagRef;
            if (nFlag < 65)
            {
                StatusFlag1 &= ~uFlag;
            }
            else if (nFlag < 129)
            {
                StatusFlag2 &= ~uFlag;
            }
            else
            {
                StatusFlag3 &= ~(uint)uFlag;
            }
            return true;
        }

        /// <summary>
        ///     Gotta check if there is a faster way to do this.
        /// </summary>
        /// <param name="flag">The flag that will be checked.</param>
        /// <param name="b64">If it's a effect 2 flag, you should set this true.</param>
        /// <param name="b128">If it's a effect 3 flag, you should set this true.</param>
        /// <returns></returns>
        public static int InvertFlag(ulong flag, bool b64 = false, bool b128 = false)
        {
            ulong inv = flag;
            int ret = -1;
            for (var i = 0; inv > 1; i++)
            {
                inv = flag >> i;
                ret++;
            }

            return !b64 ? ret : !b128 ? ret + 64 : ret + 128;
        }

        public static ulong GetFlag(int status)
        {
            return 1UL << status - 1;
        }
    }

    public interface IStatus
    {
        /// <summary>
        ///     This method will get the status id.
        /// </summary>
        int Identity { get; }

        /// <summary>
        ///     This method will check if the status still valid and running.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        ///     This method will return the power of the status. This wont make percentage checks. The value is a short.
        /// </summary>
        int Power { get; set; }

        int Time { get; }

        int RemainingTimes { get; }

        int RemainingTime { get; }

        byte Level { get; }
        uint CasterId { get; }
        bool IsUserCast { get; }

        /// <summary>
        ///     This method will get the status information into another param.
        /// </summary>
        /// <param name="info">The structure that will be filled with the information.</param>
        bool GetInfo(ref StatusInfoStruct info);

        /// <summary>
        ///     This method will override the old values from the status.
        /// </summary>
        /// <param name="power">The new power of the status.</param>
        /// <param name="secs">The remaining time to the status.</param>
        /// <param name="times">How many times the status will appear. If StatusMore.</param>
        /// <param name="caster">The identity of the caster.</param>
        Task<bool> ChangeDataAsync(int power, int secs, int times = 0, uint caster = 0);

        bool IncTime(int ms, int limit);
        bool ToFlash();
        Task OnTimerAsync();

        DbStatus Model { get; set; }
    }
}
