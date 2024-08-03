using Long.Kernel.Database.Repositories;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using System.Collections.Concurrent;

namespace Long.Kernel.States
{
    public sealed class PkStatistic
    {
        private readonly Character user;
        private readonly ConcurrentDictionary<uint, PkStatisticInfo> Statistics = new();

        public PkStatistic(Character user)
        {
            this.user = user;
        }

        public async Task InitializeAsync()
        {
            var stcs = await PkStatisticRepository.GetAsync(user.Identity);
            foreach (var stc in stcs)
            {
                var statistic = new PkStatisticInfo(stc);
                if (await statistic.InitializeAsync())
                {
                    Statistics.TryAdd(stc.TargetId, statistic);
                }
            }
        }

        public async Task KillAsync(Character target)
        {
            if (user.Map.IsPrisionMap()
                || user.Map.IsPkField()
                || user.Map.IsSynMap()
                || user.Map.IsFamilyMap()
                || user.Map.IsArenicMapInGeneral())
                return;

            uint now = (uint)UnixTimestamp.Now;
            if (!Statistics.TryGetValue(target.Identity, out var stc))
            {
                stc = new PkStatisticInfo(user, target);
                Statistics.TryAdd(stc.TargetId, stc);
            }
            else
            {
                if ((now - stc.PkTime) < 180)
                {
                    return;
                }
            }

            stc.KillTimes += 1;
            stc.TargetBattleEffect = (ushort)target.BattlePower;
            stc.PkTime = now;
            stc.MapId = target.MapIdentity;

            await stc.SaveAsync();
        }

        public async Task SubmitAsync(int page)
        {
            const int ipp = 10;
            MsgPkStatistic msg = new()
            {
                Action = 1,
                MaxCount = Statistics.Count
            };

            foreach (var stc in Statistics.Values
                .OrderByDescending(x => x.KillTimes)
                .ThenBy(x => x.TargetBattleEffect)
                .Skip(page * ipp)
                .Take(ipp))
            {
                msg.Statistics.Add(new MsgPkStatistic.PkStatistic
                {
                    Name = stc.TargetName,
                    BattlePower = stc.TargetBattleEffect,
                    LastKillTime = (int)stc.PkTime,
                    MapId = stc.MapId,
                    Times = (int)stc.KillTimes
                });
            }

            await user.SendAsync(msg);
        }
    }
}
