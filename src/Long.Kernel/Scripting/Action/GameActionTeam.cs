using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Kernel.States;
using Long.Shared.Managers;
using System.Drawing;
using static Long.Kernel.States.User.Character;
using Long.Kernel.Database.Repositories;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionTeamBroadcastAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.Team == null || user.Team.MemberCount < 1)
            {
                logger.Warning($"ExecuteActionTeamBroadcast user or team is null {action.Id}");
                return false;
            }

            if (!user.Team.IsLeader(user.Identity))
            {
                return false;
            }

            await user.Team.SendAsync(new MsgTalk(TalkChannel.Team, Color.White, param));
            return true;
        }

        private static async Task<bool> ExecuteActionTeamAttrAsync(DbAction action, string param, Character user,
                                                                   Role role,
                                                                   Item item, params string[] inputs)
        {
            if (user?.Team == null)
            {
                logger.Warning($"ExecuteActionTeamAttr user or team is null {action.Id}");
                return false;
            }

            string[] splitParams = SplitParam(param, 3);
            if (splitParams.Length < 3) // invalid param num
            {
                return false;
            }

            string cmd = splitParams[0].ToLower();
            string opt = splitParams[1];
            long.TryParse(splitParams[2], out long value);

            if (cmd.Equals("count"))
            {
                if (opt.Equals("<"))
                {
                    return user.Team.MemberCount < value;
                }

                if (opt.Equals("=="))
                {
                    return user.Team.MemberCount == value;
                }
            }

            foreach (Character member in user.Team.Members)
            {
                if (cmd.Equals("money"))
                {
                    if (opt.Equals("+="))
                    {
                        await member.ChangeMoneyAsync(value);
                    }
                    else if (opt.Equals("<"))
                    {
                        return member.Silvers < (ulong)value;
                    }
                    else if (opt.Equals("=="))
                    {
                        return member.Silvers == (ulong)value;
                    }
                    else if (opt.Equals(">"))
                    {
                        return member.Silvers > (ulong)value;
                    }
                }
                else if (cmd.Equals("emoney"))
                {
                    if (opt.Equals("+="))
                    {
                        EmoneyOperationType op = EmoneyOperationType.None;
                        if (role is BaseNpc)
                        {
                            op = EmoneyOperationType.Npc;
                        }
                        else if (role is Monster)
                        {
                            op = EmoneyOperationType.Monster;
                        }
                        else if (item != null)
                        {
                            op = EmoneyOperationType.Item;
                        }
                        if (await member.ChangeConquerPointsAsync((int)value))
                        {
                            await member.SaveEmoneyLogAsync(op, 0, 0, (uint)value);
                        }
                    }
                    else if (opt.Equals("<"))
                    {
                        return member.ConquerPoints < value;
                    }
                    else if (opt.Equals("=="))
                    {
                        return member.ConquerPoints == value;
                    }
                    else if (opt.Equals(">"))
                    {
                        return member.ConquerPoints > value;
                    }
                }
                else if (cmd.Equals("level"))
                {
                    if (opt.Equals("<"))
                    {
                        return member.Level < value;
                    }

                    if (opt.Equals("=="))
                    {
                        return member.Level == value;
                    }

                    if (opt.Equals(">"))
                    {
                        return member.Level > value;
                    }
                }
                else if (cmd.Equals("vip"))
                {
                    if (opt.Equals("<"))
                    {
                        return member.VipLevel < value;
                    }

                    if (opt.Equals("=="))
                    {
                        return member.VipLevel == value;
                    }

                    if (opt.Equals(">"))
                    {
                        return member.VipLevel > value;
                    }
                }
                else if (cmd.Equals("mate"))
                {
                    if (member.Identity == user.Identity)
                    {
                        continue;
                    }

                    if (member.MateIdentity != user.Identity)
                    {
                        return false;
                    }
                }
                else if (cmd.Equals("friend"))
                {
                    if (member.Identity == user.Identity)
                    {
                        continue;
                    }

                    if (user.Relation?.IsFriend(member.Identity) == true)
                    {
                        return false;
                    }
                }
                else if (cmd.Equals("count_near"))
                {
                    if (member.Identity == user.Identity)
                    {
                        continue;
                    }

                    if (!(member.MapIdentity == user.MapIdentity && member.IsAlive))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionTeamLeavespaceAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user?.Team == null)
            {
                logger.Warning($"ExecuteActionTeamLeavespace user or team is null {action.Id}");
                return false;
            }

            if (!int.TryParse(param, out int space))
            {
                return false;
            }

            foreach (Character member in user.Team.Members)
            {
                if (!member.UserPackage.IsPackSpare(space))
                {
                    return false;
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionTeamItemAddAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user?.Team == null)
            {
                logger.Warning($"ExecuteActionTeamItemAdd user or team is null {action.Id}");
                return false;
            }

            return true;
        }

        private static async Task<bool> ExecuteActionTeamItemDelAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user?.Team == null)
            {
                logger.Warning($"ExecuteActionTeamItemDel user or team is null {action.Id}");
                return false;
            }

            foreach (Character member in user.Team.Members)
            {
                await member.UserPackage.AwardItemAsync(action.Data);
            }

            return true;
        }

        private static async Task<bool> ExecuteActionTeamItemCheckAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.Team == null)
            {
                logger.Warning($"ExecuteActionTeamItemCheck user or team is null {action.Id}");
                return false;
            }

            foreach (Character member in user.Team.Members)
            {
                await member.UserPackage.SpendItemAsync(action.Data);
            }

            return true;
        }

        private static async Task<bool> ExecuteActionTeamChgmapAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user?.Team == null)
            {
                logger.Warning($"ExecuteActionTeamChgmap user or team is null {action.Id}");
                return false;
            }

            foreach (Character member in user.Team.Members)
            {
                if (member.UserPackage.GetItemByType(action.Data) == null)
                {
                    return false;
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionTeamChkIsleaderAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user?.Team == null)
            {
                logger.Warning($"ExecuteActionTeamChkIsleader user or team is null {action.Id}");
                return false;
            }

            return user.Team.IsLeader(user.Identity);
        }

        private static async Task<bool> ExecuteActionTeamCreateDynamapAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                logger.Warning($"ExecuteActionTeamCreateDynamapAsync user is null {action.Id}");
                return false;
            }

            DbInstanceType instanceType = InstanceTypeRepository.Get(action.Data);
            if (instanceType == null)
            {
                logger.Warning($"Could not find instance type {action.Data}");
                return false;
            }

            if (user.Level < instanceType.LevelMin || user.Level > instanceType.LevelMax)
            {
                return false;
            }

            if (user.BattlePower < instanceType.BattleMin)
            {
                return false;
            }

            GameMap baseGameMap = MapManager.GetMap(instanceType.MapId);
            if (baseGameMap == null)
            {
                logger.Warning($"Base map not found for instance [{instanceType.Name}] type [{action.Data}] map [{instanceType.MapId}]");
                return false;
            }

            InstanceMap currentInstance = MapManager.FindInstanceByUser(action.Data, user.Identity);
            if (currentInstance == null && user.Team != null && !user.Team.IsLeader(user.Identity))
            {
                currentInstance = MapManager.FindInstanceByUser(action.Data, user.Team.Leader.Identity);
            }

            if (currentInstance == null)
            {
                var dynamicMap = new DbDynamap
                {
                    Identity = (uint)IdentityManager.Instances.GetNextIdentity,
                    Name = instanceType.Name,
                    Description = $"{user.Name}`s map",
                    Type = (uint)baseGameMap.Type,
                    OwnerIdentity = user.Identity,
                    LinkMap = user.MapIdentity,
                    LinkX = user.X,
                    LinkY = user.Y,
                    MapDoc = baseGameMap.MapDoc,
                    OwnerType = 1
                };

                currentInstance = new InstanceMap(dynamicMap, instanceType)
                {
                    BaseMapId = instanceType.MapId
                };

                if (!await currentInstance.InitializeAsync())
                {
                    logger.Error($"Could not initialize instance!");
                    return false;
                }

                var npcs = baseGameMap.QueryRoles().Where(x => x is BaseNpc).Cast<BaseNpc>();
                foreach (var npc in npcs)
                {
                    await currentInstance.AddAsync(npc);
                }

                await MapManager.AddMapAsync(currentInstance);
            }

            uint requestId = user.MapIdentity;
            ushort x = user.X;
            ushort y = user.Y;

            List<Character> targets = new();
            if (user.Team != null)
            {
                foreach (var member in user.Team.Members)
                {
                    if (member.MapIdentity != requestId) continue;
                    if (member.GetDistance(x, y) > Screen.VIEW_SIZE) continue;
                    targets.Add(member);
                }
            }
            else
            {
                targets.Add(user);
            }

            foreach (Character target in targets)
            {
                Point pos = await currentInstance.QueryRandomPositionAsync();
                await target.FlyMapAsync(currentInstance.Identity, pos.X, pos.Y);
            }
            return true;
        }
    }
}
