using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Kernel.States;
using Long.Network.Packets;
using Serilog;
using System.Drawing;
using static Long.Kernel.StrRes;
using Long.Kernel.Modules.Systems.Family;
using Long.Module.Family.Managers;

namespace Long.Module.Family.Network
{
    public sealed class MsgFamilyOccupy : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgFamilyOccupy>();

        public FamilyPromptType Action { get; set; } // 4
        public uint Identity { get; set; }           // 8
        public string CityName { get; set; }         // 56
        public uint OccupyDays { get; set; }         // 96
        public string OccupyName { get; set; }       // 20
        public uint RequestNpc { get; set; }         // 12
        public uint SubAction { get; set; }          // 16
        public bool UnknownBool3 { get; set; }       // 95
        public bool WarRunning { get; set; }         // 92
        public bool CanApplyChallenge { get; set; }  // 93
        public bool CanRemoveChallenge { get; set; } // 94
        public uint DailyPrize { get; set; }         // 100
        public uint WeeklyPrize { get; set; }        // 104
        public uint IsChallenged { get; set; }       // 112
        public uint GoldFee { get; set; }            // 120
        public bool CanClaimExperience { get; set; }
        public bool CanClaimRevenue { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (FamilyPromptType)reader.ReadInt32();
            Identity = reader.ReadUInt32();
            RequestNpc = reader.ReadUInt32();
            SubAction = reader.ReadUInt32();
            OccupyName = reader.ReadString(16);
            reader.BaseStream.Seek(20, SeekOrigin.Current);
            CityName = reader.ReadString(16);
            reader.BaseStream.Seek(24, SeekOrigin.Current);
            OccupyDays = reader.ReadUInt32();
            DailyPrize = reader.ReadUInt32();
            WeeklyPrize = reader.ReadUInt32();
            reader.BaseStream.Seek(12, SeekOrigin.Current);
            GoldFee = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)(Type = PacketType.MsgFamilyOccupy));
            writer.Write((int)Action);
            writer.Write(Identity);
            writer.Write(RequestNpc);
            writer.Write(SubAction);
            writer.Write(OccupyName ?? string.Empty, 16);
            writer.BaseStream.Seek(20, SeekOrigin.Current);
            writer.Write(CityName ?? string.Empty, 16);
            writer.BaseStream.Seek(20, SeekOrigin.Current);
            writer.Write(WarRunning);
            writer.Write(CanApplyChallenge);
            writer.Write(CanRemoveChallenge);
            writer.Write(UnknownBool3);
            writer.Write(OccupyDays);
            writer.Write(DailyPrize);
            writer.Write(WeeklyPrize);
            writer.Write(0);
            writer.Write(IsChallenged); // Challenged by other clans
            writer.Write(0);
            writer.Write(GoldFee);
            writer.Write(0);
            writer.Write(CanClaimRevenue);    // allow claim
            writer.Write(CanClaimExperience); // claim exp
            writer.Write((ushort)0);
            writer.Write(0);
            return writer.ToArray();
        }

        public enum FamilyPromptType
        {
            Challenge = 0, // Client -> Server 
            CancelChallenge = 1,
            AbandonMap = 2,
            RemoveChallenge = 3,
            ChallengeMap = 4,
            Unknown5 = 5,          // Probably Client -> Server
            RequestNpc = 6,        // Npc Click Client -> Server -> Client
            AnnounceWarBegin = 7,  // Call to war Server -> Client
            AnnounceWarAccept = 8, // Answer Ok to annouce Client -> Server
            ClaimExperience = 10,
            WrongClaimTime = 13,    // Claim once a day
            CannotClaim = 12,       // New members cannot claim
            ClaimOnceADay = 14,     // Claimed
            ClaimedAlready = 15,    // Claimed, claim tomorrow
            WrongExpClaimTime = 16, // Claimed
            ReachedMaxLevel = 17,
            ClaimRevenue = 18
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            FamilyWarManager war = FamilyWarManager.Instance;
            if (war == null)
            {
                return;
            }

            switch (Action)
            {
                case FamilyPromptType.Challenge:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.Family.Challenge == Identity)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        if ((DateTime.Now - user.Family.CreationDate).TotalDays < 1)
                        {
                            // not enough time
                            await user.SendMenuMessageAsync("Your clan cannot join the clan war yet, please try again tomorrow.");
                            return;
                        }

                        var npc = RoleManager.FindRole<DynamicNpc>(x => x.Identity == Identity);
                        if (npc == null)
                        {
                            return;
                        }

                        uint fee = war.GetGoldFee(Identity);
                        if (fee == 0)
                        {
                            return;
                        }

                        if (user.Family.Money < fee)
                        {
                            await user.SendAsync(StrNotEnoughFamilyMoneyToChallenge);
                            return;
                        }

                        user.Family.Money -= fee;
                        user.Family.Challenge = npc.Identity;
                        await user.Family.SaveAsync();
                        await user.Family.SendFamilyAsync(user);

                        GameMap map = war.GetMapByNpc(user.Family.Challenge);
                        if (map == null) //??
                        {
                            return;
                        }

                        await user.Family.SendAsync(string.Format(StrPrepareToChallengeFamily, map.Name));

                        IFamily owner = war.GetFamilyOwner(npc.Identity);
                        if (owner != null)
                        {
                            await owner.SendAsync(string.Format(StrPrepareToDefendFamily, map.Name));
                        }

                        break;
                    }

                case FamilyPromptType.CancelChallenge:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        user.Family.Challenge = 0;
                        await user.Family.SaveAsync();
                        await user.Family.SendFamilyAsync(user);
                        break;
                    }

                case FamilyPromptType.RequestNpc:
                    {
                        DailyPrize = war.GetNextReward(RequestNpc);
                        WeeklyPrize = war.GetNextWeekReward(RequestNpc);

                        var npc = user.Map.QueryRole<DynamicNpc>(RequestNpc);
                        Identity = RequestNpc;
                        OccupyDays = (uint)war.GetFamilyOccupyDaysByNpc(RequestNpc);
                        OccupyName = npc.OwnerName;


                        if (npc.OwnerIdentity == user.FamilyIdentity)
                        {
                            WarRunning = war.IsAllowedToJoin(user);
                            SubAction = user.FamilyRank == IFamily.FamilyRank.ClanLeader ? 1u : 2u;

                            CanClaimRevenue = user.FamilyRank == IFamily.FamilyRank.ClanLeader && war.HasRewardToClaim(user);
                            CanClaimExperience = user.Level < ExperienceManager.GetLevelLimit() ? war.HasExpToClaim(user) : false;

                            IsChallenged = war.GetChallengersByNpc(npc.Identity).Count > 0 ? 1u : 0u;
                        }
                        else
                        {
                            WarRunning = war.IsAllowedToJoin(user) && user.Family != null && user.Family.Challenge == npc?.Data1;
                            CanRemoveChallenge = npc?.Identity == user.Family?.Challenge && !WarRunning;
                            if (CanRemoveChallenge)
                            {
                                if (user.FamilyRank == IFamily.FamilyRank.ClanLeader)
                                {
                                    SubAction = 3;
                                }
                            }
                            else
                            {
                                CanApplyChallenge = user.Family != null && RequestNpc != user.Family.Challenge && !WarRunning;
                                if (CanApplyChallenge)
                                {
                                    if (user.FamilyRank == IFamily.FamilyRank.ClanLeader)
                                    {
                                        SubAction = 5;
                                    }
                                }
                            }
                        }

                        GoldFee = war.GetGoldFee(RequestNpc);
                        await user.SendAsync(this);
                        break;
                    }

                case FamilyPromptType.AnnounceWarAccept:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (!war.IsInTime)
                        {
                            return;
                        }

                        if (!war.IsAllowedToJoin(user))
                        {
                            return;
                        }

                        DynamicNpc npc = war.GetDominatingNpc(user.Family);
                        if (npc == null)
                        {
                            npc = war.GetChallengeNpc(user.Family);
                            if (npc == null)
                            {
                                return;
                            }
                        }

                        GameMap map = MapManager.GetMap((uint)npc.Data1);
                        if (map == null)
                        {
                            return;
                        }

                        if ((DateTime.Now - user.FamilyMember.JoinDate).TotalHours < 24)
                        {
                            return;
                        }

                        Point targetPos = await map.QueryRandomPositionAsync();
                        if (targetPos.Equals(default))
                        {
                            targetPos = new Point(50, 50);
                        }

                        await user.FlyMapAsync(map.Identity, targetPos.X, targetPos.Y);
                        break;
                    }

                case FamilyPromptType.ClaimExperience:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (war.IsInTime)
                        {
                            return;
                        }

                        DynamicNpc npc = war.GetDominatingNpc(user.Family);
                        if (npc == null)
                        {
                            return;
                        }

                        GameMap map = MapManager.GetMap((uint)npc.Data1);
                        if (map == null)
                        {
                            return;
                        }

                        if ((DateTime.Now - user.FamilyMember.JoinDate).TotalDays < 1)
                        {
                            Action = FamilyPromptType.CannotClaim;
                            await user.SendAsync(this);
                            return;
                        }

                        if (!war.HasExpToClaim(user))
                        {
                            Action = FamilyPromptType.WrongExpClaimTime;
                            await user.SendAsync(this);
                            return;
                        }

                        if (user.Level >= Role.MAX_UPLEV)
                        {
                            Action = FamilyPromptType.ReachedMaxLevel;
                            await user.SendAsync(this);
                            return;
                        }

                        double exp = war.GetNextExpReward(user);

                        if (exp == 0)
                        {
                            return;
                        }

                        DbLevelExperience currLevExp = ExperienceManager.GetLevelExperience(user.Level);
                        if (currLevExp == null)
                        {
                            return;
                        }

                        await RoleManager.BroadcastWorldMsgAsync(
                            string.Format(StrFetchFamilyNpcExpSuccess, user.Name, map.Name, user.Level, exp * 100),
                            TalkChannel.Center);

                        var awardExp = (long)(currLevExp.Exp * exp);
                        await user.AwardExperienceAsync(awardExp);
                        await war.SetExpRewardAwardedAsync(user);
                        break;
                    }

                case FamilyPromptType.ClaimRevenue:
                    {
                        if (user.Family == null)
                        {
                            return;
                        }

                        if (war.IsInTime)
                        {
                            return;
                        }

                        if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
                        {
                            return;
                        }

                        DynamicNpc npc = war.GetDominatingNpc(user.Family);
                        if (npc == null)
                        {
                            return;
                        }

                        GameMap map = MapManager.GetMap((uint)npc.Data1);
                        if (map == null)
                        {
                            return;
                        }

                        if (!war.HasRewardToClaim(user))
                        {
                            Action = DateTime.Now.Hour >= 21
                                         ? FamilyPromptType.ClaimedAlready
                                         : FamilyPromptType.ClaimOnceADay;
                            await user.SendAsync(this);
                            return;
                        }

                        if (!user.UserPackage.IsPackSpare(5))
                        {
                            await user.SendAsync(string.Format(StrNotEnoughSpaceN, 5), TalkChannel.TopLeft,
                                                 Color.Red);
                            return;
                        }

                        uint idItem = war.GetNextReward(RequestNpc);
                        if (idItem == 0)
                        {
                            return;
                        }

                        await war.SetRewardAwardedAsync(user);

                        await user.UserPackage.AwardItemAsync(idItem);
                        await user.Family.SendAsync(
                            string.Format(StrFetchFamilyNpcIncomeSuccess, user.Name, map.Name));
                        break;
                    }

                default:
                    {
                        if (client.Character.IsPm())
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
