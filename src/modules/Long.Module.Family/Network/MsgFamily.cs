using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Serilog;
using static Long.Kernel.States.User.Character;

namespace Long.Module.Family.Network
{
    public sealed class MsgFamily : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgFamily>();

        public FamilyAction Action { get; set; }
        public uint Identity { get; set; }
        public List<object> Objects { get; set; } = new();
        public List<string> Strings { get; set; } = new();
        public uint Unknown { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (FamilyAction)reader.ReadUInt32();
            Identity = reader.ReadUInt32();
            Unknown = reader.ReadUInt32();
            Strings = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgFamily);
            writer.Write((int)Action);
            writer.Write(Identity);
            writer.Write(Unknown);
            if (Objects.Count == 0)
            {
                writer.Write(Strings);
            }
            else
            {
                var idx = 0;
                writer.Write(Objects.Count);
                foreach (object obj in Objects)
                {
                    if (obj is MemberListStruct member)
                    {
                        writer.Write(member.Name, 16);
                        writer.Write((int)member.Level);
                        writer.Write(member.Rank);
                        writer.Write((ushort)(member.Online ? 1 : 0));
                        writer.Write((int)member.Profession);
                        writer.Write(0);
                        writer.Write(member.Donation);
                    }
                    else if (obj is RelationListStruct relation)
                    {
                        writer.Write(idx + 101);
                        writer.Write(relation.Name, 16);
                        writer.Write(0);
                        writer.Write(0);
                        writer.Write(0);
                        writer.Write(0);
                        writer.Write(0);
                        writer.Write(relation.LeaderName, 16);
                    }

                    idx++;
                }
            }

            return writer.ToArray();
        }

        public struct MemberListStruct
        {
            public string Name;
            public byte Level;
            public ushort Rank;
            public bool Online;
            public ushort Profession;
            public uint Donation;
        }

        public struct RelationListStruct
        {
            public string Name;
            public string LeaderName;
        }

        public enum FamilyAction
        {
            Query = 1,
            QueryMemberList = 4,
            Recruit = 9,
            AcceptRecruit = 10,
            Join = 11,
            AcceptJoinRequest = 12,
            SendEnemy = 13,
            AddEnemy = 14,
            DeleteEnemy = 15,
            SendAlly = 16,
            AddAlly = 17,
            AcceptAlliance = 18,
            DeleteAlly = 20,
            Abdicate = 21,
            KickOut = 22,
            Quit = 23,
            Announce = 24,
            SetAnnouncement = 25,
            Dedicate = 26,
            QueryOccupy = 29
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = RoleManager.GetUser(client.Character?.Identity ?? 0);
            if (user == null)
            {
                client.Disconnect();
                return;
            }

            switch (Action)
            {
                case FamilyAction.Query:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        await user.Family.SendFamilyAsync(user);
                        break;
                    }

                case FamilyAction.QueryMemberList:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        await user.Family.SendMembersAsync(0, user);
                        break;
                    }

                case FamilyAction.Recruit:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (user.Family.PureMembersCount >= States.Family.MAX_MEMBERS)
                        {
                            return;
                        }

                        Character target = RoleManager.GetUser(Identity);
                        if (target == null)
                        {
                            return;
                        }

                        if (target.Family != null)
                        {
                            return;
                        }

                        user.SetRequest(RequestType.Family, target.Identity);

                        Strings.Clear();

                        Identity = user.FamilyIdentity;
                        Strings.Add(user.FamilyName);
                        Strings.Add(user.Name);
                        await target.SendRelationAsync(user);
                        await target.SendAsync(this);
                        break;
                    }

                case FamilyAction.AcceptRecruit:
                    {
                        if (user.Family != null)
                        {
                            return;
                        }

                        IFamily family = ModuleManager.FamilyManager.GetFamily(Identity);
                        if (family == null)
                        {
                            return;
                        }

                        if (family.PureMembersCount >= States.Family.MAX_MEMBERS)
                        {
                            return;
                        }

                        Character leader = family.Leader.User;
                        if (leader == null)
                        {
                            return;
                        }

                        if (leader.QueryRequest(RequestType.Family) != user.Identity)
                        {
                            return;
                        }

                        leader.PopRequest(RequestType.Family);
                        await family.AppendMemberAsync(leader, user);
                        break;
                    }

                case FamilyAction.Join:
                    {
                        if (user.Family != null)
                        {
                            return;
                        }

                        Character leader = RoleManager.GetUser(Identity);
                        if (leader == null)
                        {
                            return;
                        }

                        if (leader.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (leader.Family.PureMembersCount >= States.Family.MAX_MEMBERS)
                        {
                            return;
                        }

                        user.SetRequest(RequestType.Family, leader.Identity);

                        Strings.Clear();

                        Identity = user.Identity;
                        Strings.Add(user.Name);
                        await leader.SendRelationAsync(user);
                        await leader.SendAsync(this);
                        break;
                    }

                case FamilyAction.AcceptJoinRequest:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        Character requester = RoleManager.GetUser(Identity);
                        if (requester == null)
                        {
                            return;
                        }

                        if (requester.Family != null)
                        {
                            return;
                        }

                        if (requester.QueryRequest(RequestType.Family) != user.Identity)
                        {
                            return;
                        }

                        requester.PopRequest(RequestType.Family);
                        await user.Family.AppendMemberAsync(user, requester);
                        break;
                    }

                case FamilyAction.AddEnemy:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (user.Family.EnemyCount >= States.Family.MAX_RELATION)
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        IFamily target = ModuleManager.FamilyManager.GetFamily(Strings[0]);
                        if (target == null)
                        {
                            return;
                        }

                        if (user.Family.IsEnemy(target.Identity) || user.Family.IsAlly(target.Identity))
                        {
                            return;
                        }

                        user.Family.SetEnemy(target);
                        await user.Family.SaveAsync();
                        await user.Family.SendRelationsAsync();
                        break;
                    }

                case FamilyAction.DeleteEnemy:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        IFamily target = ModuleManager.FamilyManager.GetFamily(Strings[0]);
                        if (target == null)
                        {
                            return;
                        }

                        if (!user.Family.IsEnemy(target.Identity))
                        {
                            return;
                        }

                        user.Family.UnsetEnemy(target.Identity);
                        await user.Family.SaveAsync();

                        Identity = target.Identity;
                        await user.Family.SendAsync(this);
                        break;
                    }

                case FamilyAction.AddAlly:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (user.Family.AllyCount >= States.Family.MAX_RELATION)
                        {
                            return;
                        }

                        if (Identity == 0)
                        {
                            return;
                        }

                        Character targetUser = RoleManager.GetUser(Identity);
                        if (targetUser == null)
                        {
                            return;
                        }

                        if (targetUser.FamilyIdentity == 0 || targetUser.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        IFamily target = targetUser.Family;
                        if (target == null || !target.Leader.IsOnline)
                        {
                            return;
                        }

                        if (user.Family.IsEnemy(target.Identity) || user.Family.IsAlly(target.Identity))
                        {
                            return;
                        }

                        Strings.Clear();
                        Identity = user.Family.Identity;
                        Strings = new List<string>
                    {
                        user.FamilyName,
                        user.Name
                    };

                        await target.Leader.User.SendAsync(this);
                        break;
                    }

                case FamilyAction.AcceptAlliance:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (user.Family.AllyCount >= States.Family.MAX_RELATION)
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        Character targetUser = RoleManager.GetUser(Strings[0]);
                        if (targetUser == null)
                        {
                            return;
                        }

                        if (targetUser.FamilyIdentity == 0 || targetUser.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        IFamily target = targetUser.Family;
                        if (target == null)
                        {
                            return;
                        }

                        if (user.Family.IsEnemy(target.Identity) || user.Family.IsAlly(target.Identity))
                        {
                            return;
                        }

                        user.Family.SetAlly(target);
                        await user.Family.SaveAsync();

                        target.SetAlly(user.Family);
                        await target.SaveAsync();

                        await user.Family.SendRelationsAsync();
                        await target.SendRelationsAsync();
                        break;
                    }

                case FamilyAction.DeleteAlly:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        IFamily target = ModuleManager.FamilyManager.GetFamily(Strings[0]);
                        if (target == null)
                        {
                            return;
                        }

                        if (!user.Family.IsAlly(target.Identity))
                        {
                            return;
                        }

                        user.Family.UnsetAlly(target.Identity);
                        await user.Family.SaveAsync();

                        target.UnsetAlly(user.FamilyIdentity);
                        await target.SaveAsync();

                        Identity = target.Identity;
                        await user.Family.SendAsync(this);

                        Identity = user.FamilyIdentity;
                        Strings = new List<string> { user.FamilyName };
                        await target.SendAsync(this);
                        break;
                    }

                case FamilyAction.Abdicate:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        Character target = RoleManager.GetUser(Strings[0]);
                        if (target == null)
                        {
                            return;
                        }

                        if (target.FamilyIdentity != user.FamilyIdentity)
                        {
                            return;
                        }

                        if (target.FamilyRank != IFamily.FamilyRank.Member)
                        {
                            return;
                        }

                        await user.Family.AbdicateAsync(user, Strings[0]);
                        break;
                    }

                case FamilyAction.KickOut:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        var target = user.Family.GetMember(Strings[0]);
                        if (target == null)
                        {
                            return;
                        }

                        if (target.FamilyIdentity != user.FamilyIdentity)
                        {
                            return;
                        }

                        if (target.Rank != IFamily.FamilyRank.Member)
                        {
                            return;
                        }

                        await user.Family.KickOutAsync(user, target.Identity);
                        break;
                    }

                case FamilyAction.Quit:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank == IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if (user.FamilyRank == IFamily.FamilyRank.Spouse)
                        {
                            return;
                        }

                        await user.Family.LeaveAsync(user);
                        break;
                    }

                case FamilyAction.Announce:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        Identity = user.FamilyIdentity;
                        Strings.Add(user.Family.Announcement);
                        await user.SendAsync(this);
                        break;
                    }

                case FamilyAction.SetAnnouncement:
                    {
                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        user.Family.Announcement = Strings[0].Substring(0, Math.Min(127, Strings[0].Length));
                        await user.Family.SaveAsync();

                        Action = FamilyAction.Announce;
                        await user.Family.SendAsync(this);
                        break;
                    }

                case FamilyAction.Dedicate:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (!await user.SpendMoneyAsync(Identity, true))
                        {
                            return;
                        }

                        user.Family.Money += Identity;
                        user.FamilyMember.Proffer += Identity;
                        await user.Family.SaveAsync();
                        await user.FamilyMember.SaveAsync();
                        await user.Family.SendFamilyAsync(user);
                        break;
                    }

                case FamilyAction.QueryOccupy:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        await user.Family.SendFamilyAsync(user);
                        await user.Family.SendFamilyOccupyAsync(user);
                        break;
                    }

                default:
                    {
                        if (user.IsPm())
                        {
                            await client.SendAsync(new MsgTalk(TalkChannel.Service, $"Missing packet {Type}, Action {Action}, Length {Length}"));
                        }

                        logger.Warning("Missing packet {0}, Action {1}, Length {2}\n" + PacketDump.Hex(Encode()), Type, Action, Length);
                        break;
                    }
            }
        }
    }
}
