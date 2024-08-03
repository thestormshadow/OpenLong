using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game;
using Long.Kernel.States;
using Long.Kernel.States.MessageBoxes;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Serilog;
using static Long.Kernel.States.User.Character;
using static Long.Kernel.StrRes;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgSyndicate : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgSyndicate>();

        public SyndicateRequest Mode { get; set; }
        public uint Identity { get; set; }
        public int ConditionLevel { get; set; }
        public int ConditionMetempsychosis { get; set; }
        public int ConditionProfession { get; set; }
        public List<string> Strings { get; set; } = new();

        /// <summary>
        ///     Decodes a byte packet into the packet structure defined by this message class.
        ///     Should be invoked to structure data from the client for processing. Decoding
        ///     follows TQ Digital's byte ordering rules for an all-binary protocol.
        /// </summary>
        /// <param name="bytes">Bytes from the packet processor or client socket</param>
        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (SyndicateRequest)reader.ReadUInt32();
            Identity = reader.ReadUInt32();
            ConditionLevel = reader.ReadInt32();
            ConditionMetempsychosis = reader.ReadInt32();
            ConditionProfession = reader.ReadInt32();
            Strings = reader.ReadStrings();
        }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSyndicate);
            writer.Write((uint)Mode);
            writer.Write(Identity);
            writer.Write(ConditionLevel);
            writer.Write(ConditionMetempsychosis);
            writer.Write(ConditionProfession);
            writer.Write(Strings);
            return writer.ToArray();
        }

        public enum SyndicateRequest : uint
        {
            JoinRequest = 1,
            InviteRequest = 2,
            Quit = 3,
            Query = 6,
            Ally = 7,
            Unally = 8,
            Enemy = 9,
            Unenemy = 10,
            DonateSilvers = 11,
            Refresh = 12,
            Disband = 19,
            DonateConquerPoints = 20,
            SetRequirements = 24,
            Bulletin = 27,
            Promotion = 28,
            AcceptRequest = 29,
            Discharge = 30, // normal position
            Resign = 32,
            Discharge2 = 33,
            PaidPromotion = 34,
            DischargePaid = 36, // paid position
            PromotionList = 37,
            DenyJoinRequest = 48
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Mode)
            {
                case SyndicateRequest.JoinRequest:
                    {
                        if (Identity == 0 || user.Syndicate != null)
                        {
                            return;
                        }

                        Character leader = RoleManager.GetUser(Identity);
                        if (leader == null)
                        {
                            return;
                        }

                        if (leader.SyndicateIdentity == 0 ||
                            leader.SyndicateRank < ISyndicateMember.SyndicateRank.Manager)
                        {
                            return;
                        }

                        if (user.QueryRequest(RequestType.InviteSyndicate) == Identity)
                        {
                            user.PopRequest(RequestType.InviteSyndicate);
                            await leader.Syndicate.AppendMemberAsync(user, leader, ISyndicate.JoinMode.Invite);
                        }
                        else
                        {
                            leader.SetRequest(RequestType.JoinSyndicate, user.Identity);
                            Identity = user.Identity;
                            await leader.SendRelationAsync(user);
                            await leader.SendAsync(this);
                        }

                        break;
                    }

                case SyndicateRequest.InviteRequest:
                    {
                        if (Identity == 0 || user.Syndicate == null)
                        {
                            return;
                        }

                        Character target = RoleManager.GetUser(Identity);
                        if (target == null)
                        {
                            return;
                        }

                        if (user.SyndicateIdentity == 0
                            || user.SyndicateRank < ISyndicateMember.SyndicateRank.DeputyLeader)
                        {
                            return;
                        }

                        if (user.QueryRequest(RequestType.JoinSyndicate) == Identity)
                        {
                            user.PopRequest(RequestType.JoinSyndicate);
                            await user.Syndicate.AppendMemberAsync(target, user, ISyndicate.JoinMode.Request);
                        }
                        else
                        {
                            target.SetRequest(RequestType.InviteSyndicate, user.Identity);
                            Identity = user.Identity;
                            ConditionLevel = user.Syndicate.LevelRequirement;
                            ConditionMetempsychosis = user.Syndicate.MetempsychosisRequirement;
                            ConditionProfession = (int)user.Syndicate.ProfessionRequirement;
                            await target.SendRelationAsync(user);
                            await target.SendAsync(this);
                        }

                        break;
                    }

                case SyndicateRequest.Quit: // 3
                    {
                        if (user.SyndicateIdentity == 0)
                        {
                            return;
                        }

                        await user.Syndicate.QuitSyndicateAsync(user);
                        break;
                    }

                case SyndicateRequest.Query: // 6
                    {
                        ISyndicate queryTarget = ModuleManager.SyndicateManager.GetSyndicate((int)Identity);
                        if (queryTarget == null)
                        {
                            return;
                        }

                        await queryTarget.SendAsync(user);
                        break;
                    }

                case SyndicateRequest.DonateSilvers:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        var amount = (int)Identity;
                        if (!await user.SpendMoneyAsync(amount))
                        {
                            await user.SendAsync(StrNotEnoughMoney);
                            return;
                        }

                        user.Syndicate.Money += amount;
                        await user.Syndicate.SaveAsync();
                        user.SyndicateMember.Silvers += (int)Identity;
                        user.SyndicateMember.SilversTotal += Identity;
                        await user.SyndicateMember.SaveAsync();
                        await States.Syndicate.SendSyndicateAsync(user);

                        await user.Syndicate.SendAsync(string.Format(StrSynDonateMoney, user.SyndicateRank,
                                                                     user.Name, Identity));
                        break;
                    }

                case SyndicateRequest.Refresh: // 12
                    {
                        if (user.Syndicate != null)
                        {
                            await States.Syndicate.SendSyndicateAsync(user);
                            await user.Syndicate.SendRelationAsync(user);
                            await user.Syndicate.SendMinContributionAsync(user);
                        }

                        break;
                    }

                case SyndicateRequest.DonateConquerPoints:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        var amount = (int)Identity;
                        if (!await user.SpendConquerPointsAsync(amount))
                        {
                            await user.SendAsync(StrNotEnoughEmoney);
                            return;
                        }

                        await user.SaveEmoneyLogAsync(EmoneyOperationType.Syndicate, 0, 0, (uint)amount);

                        user.Syndicate.ConquerPoints += Identity;
                        await user.Syndicate.SaveAsync();
                        user.SyndicateMember.ConquerPointsDonation += Identity;
                        user.SyndicateMember.ConquerPointsTotalDonation += Identity;
                        await user.SyndicateMember.SaveAsync();
                        await States.Syndicate.SendSyndicateAsync(user);

                        await user.Syndicate.SendAsync(
                            string.Format(StrSynDonateEmoney, user.SyndicateRank, user.Name, Identity));
                        break;
                    }

                case SyndicateRequest.Bulletin:
                    {
                        if (user?.Syndicate == null || user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        if (user.Syndicate.Money < ISyndicate.SYNDICATE_ACTION_COST)
                        {
                            await user.SendAsync(string.Format(StrSynNoMoney, ISyndicate.SYNDICATE_ACTION_COST));
                            return;
                        }

                        string message = Strings[0].Substring(0, Math.Min(128, Strings[0].Length));

                        user.Syndicate.Announce = message;
                        user.Syndicate.AnnounceDate = DateTime.Now;
                        await user.Syndicate.SaveAsync();

                        await States.Syndicate.SendSyndicateAsync(user);
                        break;
                    }

                case SyndicateRequest.Promotion:
                case SyndicateRequest.PaidPromotion:
                    {
                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        if (user?.Syndicate == null)
                        {
                            return;
                        }

                        await user.Syndicate.PromoteAsync(user, Strings[0], (ISyndicateMember.SyndicateRank)Identity);
                        await client.Character.Syndicate.SendMembersAsync(0, user);

                        break;
                    }

                case SyndicateRequest.PromotionList:
                    {
                        if (user.Syndicate != null)
                        {
                            await user.Syndicate.SendPromotionListAsync(user);
                        }
                        break;
                    }

                case SyndicateRequest.Discharge:
                case SyndicateRequest.DischargePaid:
                    {
                        if (user.Syndicate == null || Strings.Count == 0)
                        {
                            return;
                        }

                        await user.Syndicate.DemoteAsync(user, Strings[0]);
                        await client.Character.Syndicate.SendMembersAsync(0, user);
                        break;
                    }

                case SyndicateRequest.Ally:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        if (user.Syndicate.AlliesCount >= user.Syndicate.MaxAllies())
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        ISyndicate target = ModuleManager.SyndicateManager.GetSyndicate(Strings[0]);
                        if (target == null || target.Deleted)
                        {
                            return;
                        }

                        Character targetLeader = target.Leader.User;
                        if (targetLeader == null)
                        {
                            return;
                        }

                        if (targetLeader.MessageBox is CaptchaBox)
                        {
                            await user.SendAsync(StrMessageBoxCannotCaptcha);
                            return;
                        }

                        //var box = new SyndicateRelationBox(targetLeader);
                        //if (!await box.CreateAsync(user, targetLeader, SyndicateRelationBox.RelationType.Ally))
                        //{
                        //    return;
                        //}

                        //targetLeader.MessageBox = box;
                        Strings.Clear();
                        Strings.Add(user.SyndicateName);
                        await targetLeader.SendAsync(this);
                        break;
                    }

                case SyndicateRequest.Unally:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        ISyndicate target = ModuleManager.SyndicateManager.GetSyndicate(Strings[0]);
                        if (target == null)
                        {
                            return;
                        }

                        await user.Syndicate.DisbandAllianceAsync(user, target);
                        break;
                    }

                case SyndicateRequest.Enemy:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        if (user.Syndicate.EnemyCount >= user.Syndicate.MaxEnemies())
                        {
                            return;
                        }

                        if (Strings.Count == 0)
                        {
                            return;
                        }

                        ISyndicate target = ModuleManager.SyndicateManager.GetSyndicate(Strings[0]);
                        if (target == null || target.Deleted)
                        {
                            return;
                        }

                        await user.Syndicate.AntagonizeAsync(user, target);
                        break;
                    }

                case SyndicateRequest.Unenemy:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        ISyndicate target = ModuleManager.SyndicateManager.GetSyndicate(Strings[0]);
                        if (target == null)
                        {
                            return;
                        }

                        await user.Syndicate.PeaceAsync(user, target);
                        break;
                    }

                case SyndicateRequest.SetRequirements:
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        user.Syndicate.LevelRequirement = (byte)Math.Min(Role.MAX_UPLEV, Math.Max(1, ConditionLevel));
                        user.Syndicate.ProfessionRequirement = (ISyndicate.ProfessionPermission)(uint)Math.Min((uint)ISyndicate.ProfessionPermission.All, Math.Max(0, ConditionProfession));
                        user.Syndicate.MetempsychosisRequirement = (byte)Math.Min(2, Math.Max(0, ConditionMetempsychosis));
                        await user.Syndicate.SaveAsync();
                        break;
                    }

                case SyndicateRequest.DenyJoinRequest:
                    {
                        user.PopRequest(RequestType.InviteSyndicate);
                        user.PopRequest(RequestType.JoinSyndicate);
                        break;
                    }

                default:
                    logger.Warning("Type: {0}, Subtype: {1} not handled\n" + PacketDump.Hex(Encode()), Type, Mode);
                    break;
            }
        }
    }
}
