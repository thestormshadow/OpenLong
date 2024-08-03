using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using Serilog;
using static Long.Kernel.States.User.Character;
using static Long.Kernel.StrRes;

namespace Long.Module.Team.Network
{
    public sealed class MsgTeam : MsgBase<GameClient>
    {
        public enum TeamAction
        {
            Create = 0,
            RequestJoin = 1,
            LeaveTeam = 2,
            AcceptInvite = 3,
            RequestInvite = 4,
            AcceptJoin = 5,
            Dismiss = 6,
            Kick = 7,
            Forbid = 8,
            RemoveForbid = 9,
            CloseMoney = 10,
            OpenMoney = 11,
            CloseItem = 12,
            OpenItem = 13,
            Leader = 15
        }

        private static readonly ILogger logger = Log.ForContext<MsgTeam>();

        public int Timestamp { get; set; }
        public TeamAction Action { get; set; }
        public uint Identity { get; set; }

        /// <summary>
        ///     Decodes a byte packet into the packet structure defined by this message class.
        ///     Should be invoked to structure data from the client for processing. Decoding
        ///     follows TQ Digital's byte ordering rules for an all-binary protocol.
        /// </summary>
        /// <param name="bytes">Bytes from the packet processor or client socket</param>
        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (TeamAction)reader.ReadUInt32();
            Identity = reader.ReadUInt32();
        }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgTeam);
            writer.Write((uint)Action);
            writer.Write(Identity);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Character target = RoleManager.GetUser(Identity);
            if (target == null && Identity != 0)
            {
                logger.Error("Team no target");
                return;
            }

#if !DEBUG
            if (user.IsGm() && target != null && !target.IsGm())
            {
                logger.Warning($"GM Character trying to team with no GM");
                return;
            }
#endif

            if (user.Map.IsTeamDisable())
            {
                return;
            }

            if (user.Team != null && !user.Team.IsLeader(user.Identity))
            {
                await user.DetachStatusAsync(StatusSet.TEAM_LEADER);
            }

            switch (Action)
            {
                case TeamAction.Create:
                    {
                        if (user.Team != null) return;

                        States.Team team = new States.Team(user);
                        if (!team.Create()) return;

                        await ModuleManager.OnTeamCreateAsync(user, team);

                        await user.SendAsync(this);
                        await user.AttachStatusAsync(user, StatusSet.TEAM_LEADER, 0, int.MaxValue, 0);
                        Action = TeamAction.Leader;
                        await user.SendAsync(this);
                        break;
                    }

                case TeamAction.Dismiss:
                    {
                        if (user.Team == null) return;

                        if (await user.Team.DismissAsync(user))
                        {
                            await user.SendAsync(this);
                            await user.DetachStatusAsync(StatusSet.TEAM_LEADER);
                        }

                        break;
                    }

                case TeamAction.RequestJoin:
                    {
                        if (target == null) return;

                        if (user.Team != null)
                        {
                            await user.SendAsync(StrTeamAlreadyNoJoin);
                            return;
                        }

                        if (target.Identity == user.Identity || user.GetDistance(target) > Screen.VIEW_SIZE)
                        {
                            await user.SendAsync(StrTeamLeaderNotInRange);
                            return;
                        }

                        if (target.Team == null)
                        {
                            await user.SendAsync(StrNoTeam);
                            return;
                        }

                        if (!target.Team.JoinEnable)
                        {
                            await user.SendAsync(StrTeamClosed);
                            return;
                        }

                        if (target.Team.MemberCount >= States.Team.MAX_MEMBERS)
                        {
                            await user.SendAsync(StrTeamFull);
                            return;
                        }

                        if (!target.IsAlive)
                        {
                            await user.SendAsync(StrTeamLeaderDead);
                            return;
                        }

                        if (!target.Team.IsLeader(target.Identity))
                        {
                            await user.SendAsync(StrTeamNoLeader);
                            return;
                        }

                        user.SetRequest(RequestType.TeamApply, target.Identity);
                        Identity = user.Identity;
                        await target.SendRelationAsync(user);
                        await target.SendAsync(this);

                        await user.SendAsync(StrTeamApplySent);
                        break;
                    }

                case TeamAction.AcceptJoin:
                    {
                        if (target == null) return;

                        if (user.Team == null)
                        {
                            await user.SendAsync(StrNoTeamToInvite);
                            return;
                        }

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        if (user.Team.MemberCount >= States.Team.MAX_MEMBERS)
                        {
                            await user.SendAsync(StrTeamFull);
                            return;
                        }

                        if (user.GetDistance(target) > Screen.VIEW_SIZE)
                        {
                            await user.SendAsync(StrTeamTargetNotInRange);
                            return;
                        }

                        if (target.Team != null)
                        {
                            await user.SendAsync(StrTeamTargetAlreadyTeam);
                            return;
                        }

                        var application = target.QueryRequest(RequestType.TeamApply);
                        if (application == user.Identity)
                        {
                            target.PopRequest(RequestType.TeamApply);
                            await user.SendAsync(this);
                            await user.Team.EnterTeamAsync(target);
                        }
                        else
                        {
                            await user.SendAsync(StrTeamTargetHasNotApplied);
                        }

                        break;
                    }

                case TeamAction.RequestInvite:
                    {
                        if (target == null)
                        {
                            await user.SendAsync(StrTeamInvitedNotFound);
                            return;
                        }

                        if (user.Team == null)
                        {
                            await user.SendAsync(StrNoTeam);
                            return;
                        }

                        if (!user.Team.JoinEnable)
                        {
                            await user.SendAsync(StrTeamClosed);
                            return;
                        }

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        if (user.Team.MemberCount >= States.Team.MAX_MEMBERS)
                        {
                            await user.SendAsync(StrTeamFull);
                            return;
                        }

                        if (target.Team != null)
                        {
                            await user.SendAsync(StrTeamTargetAlreadyTeam);
                            return;
                        }

                        if (!target.IsAlive)
                        {
                            await user.SendAsync(StrTargetIsNotAlive);
                            return;
                        }

                        user.SetRequest(RequestType.TeamInvite, target.Identity);

                        Identity = user.Identity;
                        await target.SendRelationAsync(user);
                        await target.SendAsync(this);

                        await user.SendAsync(StrInviteSent);
                        break;
                    }

                case TeamAction.AcceptInvite:
                    {
                        if (user.Team != null)
                        {
                            // ?? send message
                            return;
                        }

                        if (target == null)
                        {
                            await user.SendAsync(StrTeamTargetNotInRange);
                            return;
                        }

                        if (target.Team == null)
                        {
                            await user.SendAsync(StrTargetHasNoTeam);
                            return;
                        }

                        if (target.Team.MemberCount >= States.Team.MAX_MEMBERS)
                        {
                            await user.SendAsync(StrTeamFull);
                            return;
                        }

                        if (!target.Team.IsLeader(target.Identity))
                        {
                            await user.SendAsync(StrTeamNoLeader);
                            return;
                        }

                        var inviteApplication = target.QueryRequest(RequestType.TeamInvite);
                        if (inviteApplication == user.Identity || target.Team.IsAutoInvite)
                        {
                            target.PopRequest(RequestType.TeamInvite);
                            await target.SendAsync(this);
                            await target.Team.EnterTeamAsync(user);
                        }
                        else
                        {
                            await user.SendAsync(StrTeamNotInvited);
                        }

                        break;
                    }

                case TeamAction.LeaveTeam:
                    {
                        if (user.Team == null) return;

                        if (user.Team.IsLeader(user.Identity))
                        {
                            await user.Team.DismissAsync(user);
                            return;
                        }

                        await user.Team.LeaveTeamAsync(user);
                        await user.SendAsync(this);
                        break;
                    }

                case TeamAction.Kick:
                    {
                        if (user.Team == null || !user.Team.IsLeader(user.Identity)) return;

                        if (target?.Team == null || target.Team.IsLeader(target.Identity)) return;

                        await user.Team.KickMemberAsync(user, Identity);
                        break;
                    }

                case TeamAction.Forbid:
                    {
                        if (user.Team == null) return;

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        user.Team.JoinEnable = false;
                        break;
                    }

                case TeamAction.RemoveForbid:
                    {
                        if (user.Team == null) return;

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        user.Team.JoinEnable = true;
                        break;
                    }

                case TeamAction.CloseMoney:
                    {
                        if (user.Team == null) return;

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        user.Team.MoneyEnable = false;
                        break;
                    }

                case TeamAction.OpenMoney:
                    {
                        if (user.Team == null) return;

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        user.Team.MoneyEnable = true;
                        break;
                    }

                case TeamAction.CloseItem:
                    {
                        if (user.Team == null) return;

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        user.Team.ItemEnable = false;
                        break;
                    }

                case TeamAction.OpenItem:
                    {
                        if (user.Team == null) return;

                        if (!user.Team.IsLeader(user.Identity))
                        {
                            await user.SendAsync(StrTeamNoCapitain);
                            return;
                        }

                        user.Team.ItemEnable = true;
                        break;
                    }

                case TeamAction.Leader:
                default:
                    {
                        logger.Warning($"MsgTeam:{Action} unhandled subtype");
                        break;
                    }
            }
        }
    }
}
