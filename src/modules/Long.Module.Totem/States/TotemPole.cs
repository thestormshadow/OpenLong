using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Modules.Systems.Totem;
using Long.Shared;
using System.Collections.Concurrent;

namespace Long.Module.Totem.States
{
    public sealed class TotemPole
    {
        private DbTotemAdd enhancement;

        public ConcurrentDictionary<uint, TotemItem> Totems = new();

        public TotemPole(ITotem.TotemPoleType flag, DbTotemAdd enhance = null)
        {
            Type = flag;
            enhancement = enhance;
        }

        public ITotem.TotemPoleType Type { get; }
        public long Donation => Totems.Values.Sum(x => x.Points);
        public bool Locked { get; set; } = true;

        public int Enhancement
        {
            get => EnhancementExpiration.HasValue && EnhancementExpiration.Value > DateTime.Now
                       ? enhancement.BattleAddition
                       : 0;
            set => enhancement.BattleAddition = (byte)Math.Min(2, Math.Max(0, value));
        }

        public DateTime? EnhancementExpiration => UnixTimestamp.ToNullableDateTime(enhancement?.TimeLimit);

        public int BattlePower
        {
            get
            {
                var result = 0;
                long donation = Donation;
                if (donation >= 2000000)
                {
                    result++;
                }

                if (donation >= 4000000)
                {
                    result++;
                }

                if (donation >= 10000000)
                {
                    result++;
                }

                return Math.Min(3, result);
            }
        }

        public int SharedBattlePower => Math.Max(0, Math.Min(Enhancement + BattlePower, 3));

        public int GetUserContribution(uint idUser)
        {
            return Totems.Values.Where(x => x.PlayerIdentity == idUser).Sum(x => x.Points);
        }

        public async Task<bool> SetEnhancementAsync(DbTotemAdd totem)
        {
            if (totem != null && await ServerDbContext.CreateAsync(totem))
            {
                enhancement = totem;
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveEnhancementAsync()
        {
            if (enhancement != null)
            {
                await ServerDbContext.DeleteAsync(enhancement);
                enhancement = null;
                return true;
            }

            return true;
        }
    }
}
