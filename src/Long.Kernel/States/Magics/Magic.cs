using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using static Long.Kernel.States.Magics.MagicData;

namespace Long.Kernel.States.Magics
{
    public sealed class Magic
    {
        private static readonly ILogger logger = Log.ForContext<Magic>();

        private readonly Role role;

        private DbMagic magic;
        private DbMagictype magictype;
        private readonly TimeOutMS delayTimer = new();

        private byte maxLevel;

        public Magic(Role role)
        {
            this.role = role;
        }

        public string Name => magictype?.Name ?? StrNone;
        public ushort Type => magic?.Type ?? 0;
        public byte Level => (byte)(magic?.Level ?? 0);
        public byte MaxLevel => maxLevel;
        public uint Experience
        {
            get => magic?.Experience ?? 0;
            set => magic.Experience = value;
        }
        public byte OldLevel
        {
            get => (byte)magic.OldLevel;
            set => magic.OldLevel = value;
        }
        public MagicSort Sort => (MagicSort)magictype.Sort;
        public AutoActive AutoActive => (AutoActive)magictype.AutoActive;
        public byte Crime => magictype.Crime;
        public byte Ground => magictype.Ground;
        public byte Multi => magictype.Multi;
        public byte Target => (byte)magictype.Target;
        public uint UseMana => magictype.UseMp;
        public int Power => magictype.Power;
        public uint IntoneSpeed => magictype.IntoneSpeed;
        public uint Percent => magictype.Percent;
        public uint StepSeconds => magictype.StepSecs;
        public uint Range => magictype.Range;
        public uint Distance => magictype.Distance;
        public int Status => StatusSet.GetRealStatus((int)magictype.Status);
        public uint NeedProf => magictype.NeedProf;
        public int NeedExp => magictype.NeedExp;
        public uint NeedLevel => magictype.NeedLevel;
        public MagicType UseXp => (MagicType)magictype.UseXp;
        public uint WeaponSubtype => magictype.WeaponSubtype;
        public uint ActiveTimes => magictype.ActiveTimes;
        public uint FloorAttr => magictype.FloorAttr;
        public byte AutoLearn => magictype.AutoLearn;
        public byte DropWeapon => magictype.DropWeapon;
        public uint UseStamina => magictype.UseEp;
        public byte WeaponHit => magictype.WeaponHit;
        public uint UseItem => magictype.UseItem;
        public uint NextMagic => magictype.NextMagic;
        public uint Width => magictype.Width;
        public uint DurTime => magictype.DurTime;
        public uint AtkInterval => magictype.AtkInterval;

        public byte CurrentEffectType
        {
            get => magic.CurrentEffectType;
            set => magic.CurrentEffectType = value;
        }
        public uint AvailableEffectType
        {
            get => magic.AvailableEffectType;
            set => magic.AvailableEffectType = value;
        }
        public uint EffectMonopoly
        {
            get => magic.EffectMonopoly;
            set => magic.EffectMonopoly = value;
        }
        public uint EffectExorbitant
        {
            get => magic.EffectExorbitant;
            set => magic.EffectExorbitant = value;
        }

        public bool Unlearn
        {
            get => (magic?.Unlearn ?? 0) != 0;
            set => magic.Unlearn = (byte)(value ? 1 : 0);
        }

        public int DelayMs
        {
            get
            {
                if (magictype != null)
                {
                    if (magictype.Timeout > 0)
                    {
                        return (int)magictype.Timeout;
                    }
                    return (int)magictype.DelayMs;
                }
                return MAGIC_DELAY;
            }
        }
        public uint UseItemNum => magictype.UseItemNum;
        public byte ElementType => magictype.ElementType;
        public uint ElementPower => magictype.ElementPower;
        public uint ReqUplevTime => magictype.ReqUplevTime;
        public uint EmoneyCost => (uint)(magictype.ReqUplevTime / 22.22d);

        public uint Data => magictype.Data;
        public uint StatusData0 => magictype.StatusData0;
        public uint StatusData1 => magictype.StatusData1;
        public uint StatusData2 => magictype.StatusData2;

        public async Task<bool> CreateAsync(uint idMagicType, ushort level = 0)
        {
            magictype = MagicManager.GetMagictype(idMagicType, level);
            if (magictype == null)
            {
                logger.Error("Invalid magic type for [{type},{level}]", idMagicType, level);
                return false;
            }

            magic = new DbMagic
            {
                OwnerId = role.Identity,
                Level = level,
                Type = (ushort)idMagicType
            };

            maxLevel = MagicManager.GetMaxLevel(magic.Type);

            if (role is Character)
            {
                await SaveAsync();
                await SendAsync();
            }
            return true;
        }

        public bool Create(DbMagic magic)
        {
            magictype = MagicManager.GetMagictype(magic.Type, magic.Level);
            if (magictype == null)
            {
                logger.Error("Invalid magic type for [{0},{1},{2}]", magic.Id, magic.Type, magic.Level);
                return false;
            }

            this.magic = magic;
            maxLevel = MagicManager.GetMaxLevel(magic.Type);
            return true;
        }

        public async Task<bool> ChangeLevelAsync(byte newLevel)
        {
            if (newLevel > maxLevel)
            {
                return false;
            }

            DbMagictype magicType = MagicManager.GetMagictype(magic.Type, newLevel);
            if (magicType == null)
            {
                logger.Error("Invalid new level for {0} > {1}", magic.Type, newLevel);
                return false;
            }

            magic.Level = newLevel;
            magictype = magicType;
            await SaveAsync();
            return true;
        }

        public void SetDelay()
        {
            delayTimer.Startup(DelayMs);
        }

        public bool Use()
        {
            if (!IsReady())
            {
                return false;
            }
            SetDelay();
            return true;
        }

        public bool IsReady()
        {
            if (!delayTimer.IsActive())
            {
                delayTimer.Startup(DelayMs);
                return true;
            }
            return delayTimer.IsTimeOut(DelayMs);
        }

        public void ResetDelay()
        {
            delayTimer.Clear();
        }

        public Task SendAsync(MsgMagicInfo.MagicAction magicAction = MsgMagicInfo.MagicAction.AddExsisting)
        {
            if (role is Character user)
            {
                uint availableEffects = magic.AvailableEffectType;
                if (availableEffects > 0)
                {
                    availableEffects |= 1;
                }
                byte currentEffect = 0;
                if (magic.CurrentEffectType > 0)
                {
                    currentEffect = (byte)(1 << magic.CurrentEffectType - 1);
                }
                return user.SendAsync(new MsgMagicInfo
                {
                    Magictype = Type,
                    Level = Level,
                    Experience = Experience,
                    Action = magicAction,
                    CurrentEffect = currentEffect,
                    AvailableEffects = availableEffects,
                    ExorbitantEffects = magic.EffectExorbitant
                });
            }
            return Task.CompletedTask;
        }

        public Task SaveAsync()
        {
            return ServerDbContext.UpdateAsync(magic);
        }

        public Task DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(magic);
        }

        public override string ToString()
        {
            return $"{Type} - {Name}";
        }

        public enum MagicType
        {
            None = 0,
            Normal = 1,
            XpSkill = 2
        }
    }
}
