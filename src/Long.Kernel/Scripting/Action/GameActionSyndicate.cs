using Long.Database.Entities;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicateMember;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionSynCreateAsync(DbAction action, string param, Character user,
                                                                    Role role,
                                                                    Item item, params string[] inputs)
        {
            if (user == null || user.Syndicate != null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 2)
            {
                logger.Warning("Invalid param count for guild creation: {0}, {1}", param, action.Id);
                return false;
            }

            if (!int.TryParse(splitParam[0], out int level))
            {
                return false;
            }

            if (user.Level < level)
            {
                await user.SendAsync(StrNotEnoughLevel);
                return false;
            }

            if (!int.TryParse(splitParam[1], out int price))
            {
                return false;
            }

            if (user.Silvers < (ulong)price)
            {
                await user.SendAsync(StrNotEnoughMoney);
                return false;
            }

            return await SyndicateManager.CreateSyndicateAsync(user, inputs[0], price);
        }


        private static async Task<bool> ExecuteActionSynDestroyAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user?.Syndicate == null)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                await user.SendAsync(StrSynNotLeader);
                return false;
            }

            return await SyndicateManager.DisbandSyndicateAsync(user);
        }

        private static async Task<bool> ExecuteActionSynSetAssistantAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user?.Syndicate == null || user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            return await user.Syndicate.PromoteAsync(user, inputs[0], SyndicateRank.DeputyLeader);
        }

        private static async Task<bool> ExecuteActionSynClearRankAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (user?.Syndicate == null || user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            return await user.Syndicate.DemoteAsync(user, inputs[0]);
        }

        private static async Task<bool> ExecuteActionSynChangeLeaderAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user?.Syndicate == null || user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            return await user.Syndicate.PromoteAsync(user, inputs[0], SyndicateRank.GuildLeader);
        }

        private static async Task<bool> ExecuteActionSynAntagonizeAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.Syndicate == null)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            ISyndicate target = SyndicateManager.GetSyndicate(inputs[0]);
            if (target == null)
            {
                return false;
            }

            if (target.Identity == user.SyndicateIdentity)
            {
                return false;
            }

            return await user.Syndicate.AntagonizeAsync(user, target);
        }

        private static async Task<bool> ExecuteActionSynClearAntagonizeAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user?.Syndicate == null)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            ISyndicate target = SyndicateManager.GetSyndicate(inputs[0]);
            if (target == null)
            {
                return false;
            }

            if (target.Identity == user.SyndicateIdentity)
            {
                return false;
            }

            return await user.Syndicate.PeaceAsync(user, target);
        }

        private static async Task<bool> ExecuteActionSynAllyAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user?.Syndicate == null)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            ISyndicate target = user.Team?.Members.FirstOrDefault(x => x.Identity != user.Identity)?.Syndicate;
            if (target == null)
            {
                return false;
            }

            if (target.Identity == user.SyndicateIdentity)
            {
                return false;
            }

            return await user.Syndicate.CreateAllianceAsync(user, target);
        }

        private static async Task<bool> ExecuteActionSynClearAllyAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (user?.Syndicate == null)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            ISyndicate target = SyndicateManager.GetSyndicate(inputs[0]);
            if (target == null)
            {
                return false;
            }

            if (target.Identity == user.SyndicateIdentity)
            {
                return false;
            }

            return await user.Syndicate.DisbandAllianceAsync(user, target);
        }

        private static async Task<bool> ExecuteActionSynAttrAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param, 4);
            if (splitParam.Length < 3)
            {
                return false;
            }

            string field = splitParam[0],
                   opt = splitParam[1];
            long value = long.Parse(splitParam[2]);

            ISyndicate target = null;
            if (splitParam.Length < 4)
            {
                target = user.Syndicate;
            }
            else
            {
                target = SyndicateManager.GetSyndicate(int.Parse(splitParam[3]));
            }

            if (target == null)
            {
                return true;
            }

            long data = 0;
            if (field.Equals("money", StringComparison.InvariantCultureIgnoreCase))
            {
                if (opt.Equals("+="))
                {
                    if (target.Money + value < 0)
                    {
                        return false;
                    }

                    target.Money = (int)Math.Max(0, target.Money + value);
                    return await target.SaveAsync();
                }

                data = target.Money;
            }
            else if (field.Equals("membernum", StringComparison.InvariantCultureIgnoreCase))
            {
                data = target.MemberCount;
            }

            switch (opt)
            {
                case "==": return data == value;
                case ">=": return data >= value;
                case "<=": return data <= value;
                case ">": return data > value;
                case "<": return data < value;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionSynChangeNameAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user?.Syndicate == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 2);
            uint nextIdAction = uint.Parse(splitParams[0]);

            if (await SyndicateManager.ChangeSyndicateNameAsync(user, inputs[0]))
            {
                return await ExecuteActionAsync(nextIdAction, user, role, item, inputs[0]);
            }
            return false;
        }
    }
}
