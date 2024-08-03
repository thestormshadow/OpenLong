using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using SLOT_RULES_DICT =
    System.Collections.Concurrent.ConcurrentDictionary<Long.Kernel.Managers.SlotMachineManager.SlotWinningRuleType,
        System.Collections.Generic.List<Long.Kernel.Managers.SlotMachineManager.WinningRule>>;

namespace Long.Kernel.Managers
{
    public class SlotMachineManager
    {
        private static readonly ILogger logger = Log.ForContext<SlotMachineManager>();

        protected SlotMachineManager()
        {
            /* Static constructor */
        }

        public enum SlotWinningRuleType
        {
            None,
            Money,
            Emoney
        }

        public struct WinningRule(uint identity, ushort pattern, uint weight, ushort multiple)
        {
            public uint Identity { get; } = identity;
            public ushort Pattern { get; } = pattern;
            public uint Weight { get; } = weight;
            public ushort Multiple { get; } = multiple;
        }

        private static readonly SLOT_RULES_DICT winningRules = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Slot machine manager is now loading");

            List<DbSlotWinningRule> dbMoneyRules =
                await SlotMachineRepository.GetAsync((byte)SlotWinningRuleType.Money);
            List<WinningRule> moneyRules = new();
            uint weight = 0;
            foreach (DbSlotWinningRule m in dbMoneyRules)
            {
                weight += m.Weight;
                WinningRule winningRule = new(m.Identity, m.Pattern, weight, (ushort)m.Multiple);
                moneyRules.Add(winningRule);
            }

            winningRules.TryAdd(SlotWinningRuleType.Money, moneyRules);

            List<DbSlotWinningRule> dbEmoneyRules =
                await SlotMachineRepository.GetAsync((byte)SlotWinningRuleType.Emoney);
            List<WinningRule> emoneyRules = new();
            weight = 0;
            foreach (DbSlotWinningRule m in dbEmoneyRules)
            {
                weight += m.Weight;
                WinningRule winningRule = new(m.Identity, m.Pattern, weight, (ushort)m.Multiple);
                emoneyRules.Add(winningRule);
            }

            winningRules.TryAdd(SlotWinningRuleType.Emoney, emoneyRules);
        }

        public static async Task<WinningRule> GetWinningRuleAsync(SlotWinningRuleType type)
        {
            if (winningRules.TryGetValue(type, out List<WinningRule> winningRule))
            {
                int random = await NextAsync(1_000_000);
                foreach (WinningRule rule in winningRule.OrderBy(x => x.Weight))
                {
                    if (rule.Weight >= random)
                    {
                        return rule;
                    }
                }
            }

            return default;
        }

        public static async Task<WinningRule> GetRandomWinningRuleAsync(SlotWinningRuleType type)
        {
            if (winningRules.TryGetValue(type, out List<WinningRule> winningRule))
            {
                int random = await NextAsync(winningRule.Count);
                return winningRule[random % winningRule.Count];
            }

            return default;
        }
    }
}