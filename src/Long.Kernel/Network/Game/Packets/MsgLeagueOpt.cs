using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.States.Realm;
using Long.Kernel.States.User;
using Long.Network.Packets;
using System.Drawing;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueOpt : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgLeagueOpt>();

        public LeagueOpt Action { get; set; }
        public uint Identity { get; set; }
        public uint Data { get; set; }
        public uint Param { get; set; }
        public List<string> Strings { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (LeagueOpt)reader.ReadUInt16();
            Identity = reader.ReadUInt32();
            Data = reader.ReadUInt32();
            Param = reader.ReadUInt32();
            Strings = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeagueOpt);
            writer.Write((ushort)Action);
            writer.Write(Identity);
            writer.Write(Data);
            writer.Write(Param);
            writer.Write(Strings);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client?.Character;
            if (user == null)
            {
                return;
            }

            switch (Action)
            {
                case LeagueOpt.Info: // 0
                    {
                        if (user.Union != null)
                        {
                            await user.Union.SendInfoAsync(user);
                        }
                        break;
                    }
                case LeagueOpt.SynList: // 1
                    {
                        if (user.Union == null)
                        {
                            return;
                        }

                        await KingdomManager.SubmitSyndicateListAsync(user, user.Union, (int)Param);
                        break;
                    }
                case LeagueOpt.Members: // 2
                    {
                        const int ipp = 10;

                        if (user.Union == null 
                            || !user.Union.IsAllegiance(Identity))
                        {
                            return;
                        }

                        ISyndicate syndicate = SyndicateManager?.GetSyndicate(Identity);
                        if (syndicate == null)
                        {
                            return;
                        }

                        var members = syndicate.GetMembers().OrderByDescending(x => x.IsOnline ? 1 : 0).ThenByDescending(x => x.Rank).ToList();

                        MsgLeagueMemList msg = new()
                        {
                            SyndicateId = syndicate.Identity,
                            LeaderId = syndicate.Leader.UserIdentity,
                            TotalCount = members.Count
                        };                        
                        foreach (var member in members
                            .Skip((int)(ipp * Param))
                            .Take(ipp))
                        {
                            if (member.IsOnline)
                            {
                                Character memberUser = member.User;
                                msg.Members.Add(new MsgLeagueMemList.MemberListData
                                {
                                    Exploits = 1,
                                    Identity = member.UserIdentity,
                                    BattlePower = (ushort)memberUser.BattlePower,
                                    Level = memberUser.Level,
                                    Mesh = memberUser.Mesh,
                                    Name = memberUser.Name,
                                    NobilityRank = (uint)(memberUser.NobilityRank),
                                    OfficialPosition = (uint)(memberUser.UnionMember?.OfficialTypeFlag ?? 0),
                                    Online = true,
                                    Profession = memberUser.Profession
                                });
                            }
                            else
                            {
                                var unionMember = user.Union.QueryMember(member.UserIdentity);
                                msg.Members.Add(new MsgLeagueMemList.MemberListData
                                {
                                    Exploits = 1,
                                    Identity = member.UserIdentity,
                                    BattlePower = 0,
                                    Level = member.Level,
                                    Mesh = member.LookFace,
                                    Name = member.UserName,
                                    NobilityRank = (uint)(member.NobilityRank),
                                    OfficialPosition = (uint)unionMember.OfficialTypeFlag,
                                    Online = false,
                                    Profession = (uint)member.Profession
                                });
                            }
                        }
                        await user.SendAsync(msg);                        
                        break;
                    }
                case LeagueOpt.ViewOthers: // 3
                    {
                        const int ipp = 10;
                        if (user.Union == null)
                        {
                            return;
                        }

                        var members = user.Union.GetOtherMembers();
                        MsgLeagueMemList msg = new()
                        {
                            SyndicateId = 0,
                            LeaderId = 0,
                            TotalCount = members.Count
                        };
                        foreach (var member in members
                            .Skip((int)(ipp * Param))
                            .Take(ipp))
                        {
                            if (member.IsOnline)
                            {
                                Character memberUser = member.User;
                                msg.Members.Add(new MsgLeagueMemList.MemberListData
                                {
                                    Exploits = 0,
                                    Identity = member.Identity,
                                    BattlePower = (ushort)memberUser.BattlePower,
                                    Level = memberUser.Level,
                                    Mesh = memberUser.Mesh,
                                    Name = memberUser.Name,
                                    NobilityRank = (uint)(memberUser.NobilityRank),
                                    OfficialPosition = (uint)member.OfficialTypeFlag,
                                    Online = true,
                                    Profession = memberUser.Profession
                                });
                            }
                            else
                            {
                                msg.Members.Add(new MsgLeagueMemList.MemberListData
                                {
                                    Exploits = 0,
                                    Identity = member.Identity,
                                    BattlePower = 0,
                                    Level = member.Level,
                                    Mesh = member.Mesh,
                                    Name = member.Name,
                                    NobilityRank = (uint)(member.NobilityRank),
                                    OfficialPosition = (uint)member.OfficialTypeFlag,
                                    Online = false,
                                    Profession = member.Profession
                                });
                            }
                        }
                        await user.SendAsync(msg);
                        break;
                    }
                case LeagueOpt.List: // 4
                    {
                        await KingdomManager.SubmitAllegianceListAsync(user, (int)Param);
                        break;
                    }
                case LeagueOpt.KingdomRank: // 5 - top3
                    {
                        await KingdomManager.SubmitTop3UnionsAsync(user, (int)Param);
                        break;
                    }
                case LeagueOpt.Create: // 7
                    {
                        if (user.Union != null || user.Syndicate == null)
                        {
                            return;
                        }

                        string name = Strings[0];
                        if (!RoleManager.IsValidName(name))
                        {
                            await user.SendAsync(StrForbiddenLeagueName);
                            return;
                        }

                        Union existingUnion = KingdomManager.GetUnion(name);
                        if (existingUnion != null)
                        {
                            await user.SendAsync(StrForbiddenLeagueName);
                            return;
                        }

                        await KingdomManager.CreateNewUnionAsync(user, name);
                        break;
                    }
                case LeagueOpt.Pledge: // 8
                    {
                        if (user.Union != null || user.Syndicate != null)
                        {
                            return;
                        }

                        Union union = KingdomManager.GetUnion(Param);
                        if (union == null)
                        {
                            return;
                        }

                        await union.PledgeAsync(user);
                        break;
                    }
                case LeagueOpt.Quit: // 9
                    {
                        if (user.Union == null || user.Union.Leader.Identity == user.Identity)
                        {
                            return;
                        }

                        await user.Union.QuitAsync(user);
                        break;
                    }
                case LeagueOpt.GuildPledge: // 11
                    {
                        if (user.Syndicate == null)
                        {
                            return;
                        }

                        if (user.Union != null)
                        {
                            return; // ???
                        }

                        if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        Union union = KingdomManager.GetUnion(Param);
                        if (union == null)
                        {
                            return;
                        }

                        if (await union.PledgeSyndicateAsync(user.Syndicate))
                        {
                            await user.SendAsync(this);
                        }
                        break;
                    }
                case LeagueOpt.UnionRank: // 16
                    {
                        await KingdomManager.SubmitGlobalUnionRankAsync(user, (int)Param);
                        break;
                    }
                case LeagueOpt.ViewRecruitDeclaraton: // 19
                    {
                        if (user.Union == null)
                        {
                            return;
                        }
                        Identity = user.Union.Identity;
                        Strings.Add(user.Union.Declaration);
                        await user.SendAsync(this);
                        break;
                    }

                case LeagueOpt.RecruitDeclaration: // 20
                    {
                        if (user.Union == null
                            || !user.Union.IsLeader(user.Identity))
                        {
                            return;
                        }

                        string declaration = Strings[0];
                        declaration = declaration[..Math.Min(declaration.Length, 255)];

                        user.Union.Declaration = declaration;
                        await user.SendAsync(this);
                        await user.Union.SaveAsync();
                        break;
                    }

                case LeagueOpt.CoreOfficials: // 27
                    {
                        if (user.Union == null
                            || !user.Union.IsKingdom)
                        {
                            return;
                        }

                        MsgLeagueImperialCourtList msg = new MsgLeagueImperialCourtList();
                        msg.Action = MsgLeagueImperialCourtList.CourtType.Officials;
                        var officials = user.Union.GetCoreOfficials();
                        foreach (var official in officials)
                        {
                            var member = official.User;
                            msg.Officials.Add(new MsgLeagueImperialCourtList.CourtMember
                            {
                                BattlePower = ((uint?)member?.BattlePower ?? official.BattlePower),
                                Exploits = 0,
                                Identity = official.Identity,
                                Level = official.Level,
                                Mesh = official.Mesh,
                                Name = official.Name,
                                NobilityRank = (uint)official.NobilityRank,
                                Online = official.IsOnline,
                                Profession = official.Profession,
                                UnionRank = (ushort)official.OfficialType
                            });
                        }
                        await user.SendAsync(msg);
                        break;
                    }

                case LeagueOpt.AppointCoreOffical:
                    {
                        if (user.Union == null)
                        {
                            return;
                        }

                        Union union = user.Union;
                        if (!union.IsLeader(user.Identity))
                        {
                            return;
                        }

                        if (!union.IsKingdom)
                        {
                            return;
                        }

                        var officialType = KingdomManager.GetOfficialType(Param);
                        if (officialType == null)
                        {
                            return;
                        }

                        if (union.GetOfficialCount((KingdomOfficial.OfficialPosition)Param) >= officialType.SetupNum)
                        {
                            await user.SendAsync(StrLeagueOtherNoVacancy);
                            return;
                        }

                        if (Strings[0].Equals(user.Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            await user.SendAsync(StrLeagueCantAppointSelf);
                            return;
                        }

                        var targetUser = RoleManager.GetUser(Strings[0]);
                        if (targetUser == null)
                        {
                            await user.SendAsync(StrLeagueUserOffline);
                            return;
                        }

                        if (targetUser.Union?.Identity != union.Identity)
                        {
                            await user.SendAsync(StrLeagueOtherLeagueUser);
                            return;
                        }

                        if (targetUser.Level < 70)
                        {
                            await user.SendAsync(StrLeagueOtherLowLevel);
                            return;
                        }

                        KingdomOfficial.OfficialPosition officialPosition = (KingdomOfficial.OfficialPosition)Param;
                        switch (officialPosition)
                        {
                            case KingdomOfficial.OfficialPosition.LeftMinister:
                                { 
                                    if (targetUser.UnionMember.IsOfficialType(KingdomOfficial.OfficialPosition.RightMinister))
                                    {
                                        return;
                                    }
                                    break;
                                }
                            case KingdomOfficial.OfficialPosition.RightMinister:
                                {
                                    if (targetUser.UnionMember.IsOfficialType(KingdomOfficial.OfficialPosition.LeftMinister))
                                    {
                                        return;
                                    }
                                    break;
                                }
                            case KingdomOfficial.OfficialPosition.LeftMarshal:
                                {
                                    if (targetUser.UnionMember.IsOfficialType(KingdomOfficial.OfficialPosition.RightMarshal))
                                    {
                                        return;
                                    }
                                    break;
                                }
                            case KingdomOfficial.OfficialPosition.RightMarshal:
                                {
                                    if (targetUser.UnionMember.IsOfficialType(KingdomOfficial.OfficialPosition.LeftMarshal))
                                    {
                                        return;
                                    }
                                    break;
                                }
                        }

                        if (await union.SetOfficialAsync(officialPosition, targetUser))
                        {
                            await union.BroadcastAsync(string.Format(StrLeagueAppointImperialCourt, targetUser.Name, officialType.OfficialName), TalkChannel.Talk, Color.White);
                            await user.SendAsync(this);
                        }
                        break;
                    }

                case LeagueOpt.Officials: // 34
                    {
                        if (user.Union == null
                            || !user.Union.IsKingdom)
                        {
                            return;
                        }

                        MsgLeagueImperialCourtList msg = new MsgLeagueImperialCourtList();
                        msg.Action = MsgLeagueImperialCourtList.CourtType.OfficialsList;
                        foreach (var official in user.Union.GetOfficials())
                        {
                            var member = official.User;
                            msg.Officials.Add(new MsgLeagueImperialCourtList.CourtMember
                            {
                                BattlePower = ((uint?)member?.BattlePower ?? official.BattlePower),
                                Exploits = 0,
                                Identity = official.Identity,
                                Level = official.Level,
                                Mesh = official.Mesh,
                                Name = official.Name,
                                NobilityRank = (uint)official.NobilityRank,
                                Online = official.IsOnline,
                                Profession = official.Profession,
                                UnionRank = (ushort)official.OfficialType
                            });
                        }
                        await user.SendAsync(msg);
                        break;
                    }
                case LeagueOpt.Tokens: // 37
                    {
                        await KingdomManager.SubmitTokenTypesAsync(user);
                        break;
                    }
                case LeagueOpt.Announce: // 66
                    {
                        if (user.Union == null
                            || !user.Union.IsLeader(user.Identity))
                        {
                            return;
                        }

                        string announce = Strings[0];
                        announce = announce[..Math.Min(announce.Length, 255)];

                        user.Union.Announcement = announce;
                        await user.Union.SaveAsync();
                        await user.Union.BroadcastInfoAsync();

                        await user.SendAsync(this);
                        await user.SendAsync(StrLeagueChangeManifesto);
                        break;
                    }
                default:
                    {
                        logger.Warning("{0} {1} is not handled!\n" + PacketDump.Hex(Encode()), Type, Action);
                        return;
                    }
            }
        }

        public enum LeagueOpt : byte
        {
            Info = 0,
            SynList = 1,
            Members = 2,
            ViewOthers = 3,
            List = 4,
            MyUnion = 5,
            NewUnionName = 6,
            Create = 7,
            Pledge = 8,
            Quit = 9,
            ExpelMember = 10,
            GuildPledge = 11,
            GuildQuit = 12,
            ExpelGuild = 13,
            TransferLeader = 14,
            KingdomRank = 15,
            UnionRank = 16,
            KingdomTitleGui = 17,
            NewUnionTitle = 18,
            ViewRecruitDeclaraton = 19,
            RecruitDeclaration = 20,
            NewNameDone = 22,
            ImperialGuards = 23,
            AppointImperialGuard = 24,
            RemovePalaceGuard = 25,
            ReSignPalaceGuard = 26,
            CoreOfficials = 27,
            AppointCoreOffical = 28,
            RemoveCoreOffical = 29,
            ReSignCoreOffical = 30,
            Officials = 34,
            Tokens = 37,
            SaveLeagueOrderStatu = 38,
            Stipend = 39,
            ImperialHarem = 40,
            AppointLeagueConcubines = 41,
            UppdateLeagueConcubines = 42,
            RemoveHarem = 44,
            Allowance = 46,
            AppointCore = 47,
            AppointGuard = 48,
            AppointHarem = 49,
            UpdateHarem = 50,
            ChangeUnionName = 51,
            Announce = 66,
            PlunderWar = 69,
            JoinPlunderWar = 71
        }
    }
}
