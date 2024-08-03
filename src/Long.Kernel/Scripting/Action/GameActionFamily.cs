using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        #region Family 3500-3599

        private static async Task<bool> ExecuteActionFamilyAttrAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param, 3);
            if (splitParam.Length < 3)
            {
                return false;
            }

            string field = splitParam[0],
                   opt = splitParam[1];
            long value = long.Parse(splitParam[2]);

            long data = -1;
            if (user?.Family != null)
            {
                if (field.Equals("money"))
                {
                    if (opt.Equals("+="))
                    {
                        if (value < 0)
                        {
                            var temp = (ulong)(value * -1);
                            if (user.Family.Money < temp)
                            {
                                return false;
                            }

                            user.Family.Money -= temp;
                        }
                        else
                        {
                            var temp = (ulong)value;
                            user.Family.Money += temp;
                        }

                        return await user.Family.SaveAsync();
                    }

                    data = (long)user.Family.Money;
                }
                else if (field.Equals("rank"))
                {
                    if (opt.Equals("="))
                    {
                        user.Family.Rank = (byte)Math.Min(4, value);
                        return await user.Family.SaveAsync();
                    }

                    data = user.Family.Rank;
                }
                else if (field.Equals("star_tower"))
                {
                    if (opt.Equals("="))
                    {
                        user.Family.BattlePowerTower = (byte)Math.Min(4, value);
                        return await user.Family.SaveAsync();
                    }

                    data = user.Family.BattlePowerTower;
                }
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

        private static async Task<bool> ExecuteActionFamilyMemberAttrAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param, 3);
            if (splitParam.Length < 3)
            {
                return false;
            }

            string field = splitParam[0],
                   opt = splitParam[1];
            long value = long.Parse(splitParam[2]);
            long data = 0;

            if (user?.Family != null)
            {
                if (field.Equals("rank"))
                {
                    data = (long)user.FamilyRank;
                }
            }

            switch (opt)
            {
                case "==": return data == value;
                case ">=": return data >= value;
                case "<=": return data <= value;
                case ">": return data > value;
                case "<": return data < value;
            }

            return true;
        }

        private static async Task<bool> ExecuteActionFamilyWarActivityCheckAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user?.Family == null)
            {
                return false;
            }

            var npc = RoleManager.FindRole<DynamicNpc>(x => x.Identity == action.Data);
            if (npc == null)
            {
                return false;
            }

            if (npc.Identity != user.Family.Challenge && npc.Identity != user.Family.Occupy)
            {
                return false;
            }

            var war = ModuleManager.FamilyManager.FamilyWar;
            if (war == null)
            {
                return false;
            }

            return await war.ValidateResultAsync(user, npc.Identity);
        }

        private static async Task<bool> ExecuteActionFamilyWarAuthorityCheckAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            var war = ModuleManager.FamilyManager.FamilyWar;
            if (war == null)
            {
                return false;
            }

            if (user?.Family == null)
            {
                return false;
            }

            var npc = RoleManager.FindRole<DynamicNpc>(x => x.Identity == action.Data);
            if (npc == null)
            {
                return false;
            }

            if (npc.Identity != user.Family.Challenge)
            {
                return false;
            }

            return true;
        }

        private static async Task<bool> ActionFamilyWarRegisterCheckAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            var war = ModuleManager.FamilyManager.FamilyWar;
            if (war == null)
            {
                return false;
            }

            if (user?.Family == null)
            {
                return false;
            }

            var npc = RoleManager.FindRole<DynamicNpc>(x => x.Identity == action.Data);
            if (npc == null)
            {
                return false;
            }

            if (npc.Identity != user.Family.Occupy)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
