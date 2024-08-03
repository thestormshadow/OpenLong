using Long.Kernel.Managers;
using Long.Kernel.Processors;
using Long.Kernel.States;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using static Long.Kernel.States.Role;
using System.Drawing;
using Long.Database.Entities;
using Long.Kernel.States.Items;
using static Long.Kernel.States.User.Character;
using Long.Kernel.States.Status;
using Long.Network.Packets.Ai;
using Long.Game.Network.Ai.Packets;
using Long.Kernel.Network.Ai;
using Long.Kernel.States.Magics;

namespace Long.Kernel.Network.Game.Packets
{
    /// <remarks>Packet Type 1010</remarks>
    /// <summary>
    ///     Message containing a general action being performed by the client. Commonly used
    ///     as a request-response protocol for question and answer like exchanges. For example,
    ///     walk requests are responded to with an answer as to if the step is legal or not.
    /// </summary>
    public sealed class MsgAction : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgAction>();

        public MsgAction()
        {
            Timestamp = (uint)Environment.TickCount;
            Unknown = (int)Timestamp;
        }

        // Packet Properties
        public int Unknown { get; set; }
        public uint Timestamp { get; set; }
        public uint Identity { get; set; }
        public uint Data { get; set; }
        public uint Command { get; set; }

        public ushort CommandX
        {
            get => (ushort)(Command - (CommandY << 16));
            set => Command = (uint)(CommandY << 16 | value);
        }

        public ushort CommandY
        {
            get => (ushort)(Command >> 16);
            set => Command = (uint)(value << 16) | Command;
        }

        public uint Argument { get; set; }

        public ushort ArgumentX
        {
            get => (ushort)(Argument - (ArgumentY << 16));
            set => Argument = (uint)(ArgumentY << 16 | value);
        }

        public ushort ArgumentY
        {
            get => (ushort)(Argument >> 16);
            set => Argument = (uint)(value << 16) | Argument;
        }
        public ushort Direction { get; set; }
        public ActionType Action { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }
        public uint Map { get; set; }
        public uint MapColor { get; set; }
        public byte Sprint { get; set; }
        public List<string> Strings { get; init; } = new();

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
            Unknown = reader.ReadInt32(); // 4
            Identity = reader.ReadUInt32(); // 8
            Command = reader.ReadUInt32(); // 12
            Argument = reader.ReadUInt32(); // 16
            Timestamp = reader.ReadUInt32(); // 20
            Action = (ActionType)reader.ReadUInt16(); // 24
            Direction = reader.ReadUInt16(); // 26
            X = reader.ReadUInt16(); // 28
            Y = reader.ReadUInt16(); // 30
            Map = reader.ReadUInt32(); // 32
            MapColor = reader.ReadUInt32(); // 36
            Sprint = reader.ReadByte(); // 40
            Strings.AddRange(reader.ReadStrings()); // 41
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
            writer.Write((ushort)PacketType.MsgAction);
            writer.Write(Environment.TickCount); // 4
            writer.Write(Identity); // 8
            writer.Write(Command); // 12
            writer.Write(Argument); // 16
            writer.Write(Timestamp); // 20
            writer.Write((ushort)Action); // 24
            writer.Write(Direction); // 26
            writer.Write(X); // 28
            writer.Write(Y); // 30
            writer.Write(Map); // 32
            writer.Write(MapColor); // 36
            writer.Write(Sprint); // 40
            writer.Write(Strings); // 41
            return writer.ToArray();
        }

        /// <summary>
        ///     Defines actions that may be requested by the user, or given to by the server.
        ///     Allows for action handling as a packet subtype. Enums should be named by the
        ///     action they provide to a system in the context of the player actor.
        /// </summary>
        public enum ActionType
        {
            LoginSpawn = 74,
            LoginInventory,
            LoginRelationships,
            LoginProficiencies,
            LoginSpells,
            CharacterDirection,
            CharacterEmote = 81,
            MapPortal = 85,
            MapTeleport,
            CharacterLevelUp = 92,
            SpellAbortXp,
            CharacterRevive,
            CharacterDelete,
            CharacterPkMode,
            LoginGuild,
            MapMine = 99,
            MapTeamLeaderStar = 101,
            MapQuery,
            AbortMagic = 103,
            MapArgb = 104,
            MapTeamMemberStar = 106,
            Kickback = 108,
            SpellRemove,
            ProficiencyRemove,
            BoothSpawn,
            BoothSuspend,
            BoothResume,
            BoothLeave,
            ClientCommand = 116,
            CharacterObservation,
            SpellAbortTransform,
            SpellAbortFlight = 120,
            MapGold,
            RelationshipsEnemy = 123,
            ClientDialog = 126,
            MapEffect = 134,
            RemoveEntity = 135,
            MapJump = 137,
            CharacterDead = 145,
            SyncScreen = 146,
            RelationshipsFriend = 148,
            CharacterAvatar = 151,
            QueryTradeBuddy = 152,
            ItemDetained = 153,
            ItemDetainedEx = 155,
            NinjaStep = 156,
            Countdown = 159,
            OpenShop = 160,
            Away = 161,
            PathFinding = 162,
            MonsterTrap = 163,
            ProgressBar = 164,
            BulletinInviteTrans = 166,
            MouseSetFace = 171,
            MouseProcess = 172,
            MouseCancel = 173,
            MouseResetFace = 174,
            MouseResetClick = 175,
            ShowType = 178,
            LoginComplete = 251,
            UpgradeMagicSkill = 252,
            UpgradeWeaponSkill = 253,
            AwardFirstCredit = 255,
            InventorySash = 256,
            FriendObservation = 310,
            StartRaceTrack = 401,
            EndRaceTrack = 402,
            UserAttribInfo = 408,
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Role role = RoleManager.GetRole(Identity);
            Character targetUser = RoleManager.GetUser(Command);

            if (user.CurrentServer.HasValue && user.CurrentServerID != RealmManager.ServerIdentity)
            {
                switch (Action)
                {
                    case ActionType.CharacterDirection:
                    case ActionType.CharacterEmote:
                    case ActionType.SpellAbortXp:
                    case ActionType.CharacterRevive:
                    case ActionType.CharacterPkMode:
                    case ActionType.MapQuery:
                    case ActionType.AbortMagic:
                    case ActionType.SpellAbortTransform:
                    case ActionType.SpellAbortFlight:
                    case ActionType.CharacterObservation:
                    case ActionType.MapTeamLeaderStar:
                    case ActionType.MapTeamMemberStar:
                    case ActionType.MapJump:
                    case ActionType.Away:
                    case ActionType.UserAttribInfo:
                        {
                            await user.SendCrossMsgAsync(this);
                            return;
                        }
                    case ActionType.UpgradeWeaponSkill:
                    case ActionType.UpgradeMagicSkill:
                    case ActionType.CharacterDelete:
                        {
                            return;
                        }
                }
            }

            switch (Action)
            {
                case ActionType.UpgradeWeaponSkill:
                case ActionType.UpgradeMagicSkill:
                    {
                        if (!user.IsUnlocked())
                        {
                            await user.SendSecondaryPasswordInterfaceAsync();
                            return;
                        }
                        break;
                    }
            }

            switch (Action)
            {
                case ActionType.LoginSpawn: // 74
                    {
                        if (user == null)
                        {
                            return;
                        }

                        if (user.MapIdentity == 5000 && !user.IsGm())
                        {
                            user.MapIdentity = 6002;
                            user.X = 61;
                            user.Y = 54;
                            await user.SavePositionAsync(6002, 61, 54);
                        }
#if !DEBUG
                        else if (user.IsGm())
                        {
                            user.MapIdentity = 5000;
                            user.X = 37;
                            user.Y = 73;
                        }
#endif                   

                        GameMap targetMap = MapManager.GetMap(user.MapIdentity);
                        if (targetMap == null)
                        {
                            await user.SavePositionAsync(1002, 300, 278);
                            client.Disconnect();
                            return;
                        }

                        Command = targetMap.MapDoc;
                        X = user.X;
                        Y = user.Y;
                        Map = targetMap.Identity;

                        async Task enterMapPartitionTaskAsync()
                        {
                            await user.EnterMapAsync();
                            await user.SendAsync(this);
                        }

                        WorldProcessor.Instance.Queue(targetMap.Partition, enterMapPartitionTaskAsync); // sends the current player from Partition 0 the proper partition
                        break;
                    }

                case ActionType.LoginInventory: // 75
                    {
                        if (user == null)
                        {
                            return;
                        }

						await user.SynchroAttributesAsync(ClientUpdateType.CurrentSashSlots, user.SashSlots);
						await user.SynchroAttributesAsync(ClientUpdateType.MaximumSashSlots, MAXIMUM_SASH_SLOTS);
						await user.UserPackage.SendAsync();
                        await user.TitleStorage.InitializeAsync();
						await user.SendDetainRewardAsync();
						await user.SendDetainedEquipmentAsync();
						await user.SendAsync(this);
                        break;
                    }

                case ActionType.LoginRelationships: // 76
                    {
                        if (user == null)
                        {
                            return;
                        }

                        if (user.MateIdentity != 0)
                        {
                            Character mate = RoleManager.GetUser(user.MateIdentity);
                            if (mate != null)
                            {
                                await mate.SendAsync(user.Gender == 1 ? StrMaleMateLogin : StrFemaleMateLogin);
                            }
                        }

                        if (user.Relation != null)
                        {
                            await user.Relation.SendAllFriendAsync();
                            await user.Relation.SendAllEnemyAsync();
                        }

						await user.PkStatistic.InitializeAsync();

						await user.SendAsync(this);
                        break;
                    }

                case ActionType.LoginProficiencies: // 77
                    {
                        if (user == null)
                        {
                            return;
                        }

						await user.WeaponSkill.InitializeAsync();
						await user.WeaponSkill.SendAsync();
						// user.AstProf.InitializeAsync();
						await user.Fate.InitializeAsync();
						await user.JiangHu.InitializeAsync();
						await user.InnerStrength.InitializeAsync();
						await user.Achievements.InitializeAsync();
						await user.SendAsync(this);
                        break;
                    }

                case ActionType.LoginSpells: // 78
                    {
                        if (user == null)
                        {
                            return;
                        }

						await user.MagicData.InitializeAsync();
						await user.LoadMonsterKillsAsync();
						if (user.IsGm() && !user.MagicData.CheckType(3321))
						{
							await user.MagicData.CreateAsync(3321, 0);
						}

						await user.SendBlessAsync();
                        await user.SendAsync(this);
                        break;
                    }

                case ActionType.CharacterDirection: // 79
                    {
                        await user.SetDirectionAsync((FacingDirection)(Direction % 8), false);
                        await user.BroadcastRoomMsgAsync(this, true);
                        break;
                    }

                case ActionType.CharacterEmote: // 81
                    {
                        if (user != null && user.Identity == Identity)
                        {
                            await role.SetActionAsync((EntityAction)Command, false);

                            if ((EntityAction)Command == EntityAction.Cool && user.IsCoolEnable())
                            {
                                if (user.IsFullSuper())
                                {
                                    Argument = Command |= (uint)(user.Profession * 0x00010000 + 0x01000000);
                                }
                                else if (user.IsArmorSuper())
                                {
                                    Argument = Command |= (uint)(user.Profession * 0x010000);
                                }
                            }

                            await role.BroadcastRoomMsgAsync(this, user?.Identity == Identity);
                        }
                        break;
                    }

                case ActionType.CharacterDelete: // 95
                    {
                        if (user == null)
                        {
                            return;
                        }

                        if (user.SecondaryPassword != Command)
                        {
                            await user.SendMenuMessageAsync(StrDeleteInvalidPassword);
                            return;
                        }

                        if (await user.DeleteUserAsync())
                        {
                            await RoleManager.KickOutAsync(user.Identity, "User Delete Role.");
                        }

                        break;
                    }

                case ActionType.CharacterPkMode: // 96
                    {
                        if (!Enum.IsDefined(typeof(PkModeType), (int)Command))
                        {
                            return;
                        }

                        await user.SetPkModeAsync((PkModeType)Command);
                        break;
                    }

                case ActionType.LoginGuild: // 97
                    {
                        if (user == null)
                        {
                            return;
                        }

                        await user.SendAsync(this);
                        break;
                    }

                case ActionType.MapPortal: // 85
                    {
                        uint idMap = 0;
                        var tgtPos = new Point();
                        Point sourcePos;
                        bool result;
                        if (Command == 0)
                        {
                            sourcePos = new Point(user.X, user.Y);
                            result = user.Map.GetPassageById((int)MapColor, ref idMap, ref tgtPos, ref sourcePos);
                        }
                        else
                        {
                            sourcePos = new Point(CommandX, CommandY);
                            result = user.Map.GetPassageMap(ref idMap, ref tgtPos, ref sourcePos);
                        }

                        if (!result)
                        {
                            user.Map.GetRebornMap(ref idMap, ref tgtPos);
                        }

                        GameMap targetMap = MapManager.GetMap(idMap);
                        if (targetMap.IsRecordDisable())
                        {
                            await user.SavePositionAsync(user.RecordMapIdentity, user.RecordMapX, user.RecordMapY);
                        }

                        await user.FlyMapAsync(idMap, tgtPos.X, tgtPos.Y);
                        break;
                    }

                case ActionType.SpellAbortXp: // 93
                    {
                        if (user.QueryStatus(StatusSet.START_XP) != null)
                        {
                            await user.DetachStatusAsync(StatusSet.START_XP);
                        }

                        break;
                    }
				case ActionType.MapMine: // 99
					{
						if (user == null)
						{
							return;
						}

						if (!user.IsAlive)
						{
							await user.SendAsync(StrDead);
							return;
						}

						if (!user.Map.IsMineField())
						{
							await user.SendAsync(StrNoMine);
							return;
						}

						user.StartMining();
						break;
					}
				case ActionType.CharacterRevive: // 94
                    {
                        if (user == null)
                        {
                            return;
                        }

                        if (user.IsAlive || !user.CanRevive())
                        {
                            return;
                        }

                        await user.RebornAsync(Command == 0);
                        break;
                    }

                case ActionType.MapTeamLeaderStar: // 101
                    {
                        if (user == null)
                        {
                            return;
                        }

                        if (user.Team == null || user.Team.Leader.MapIdentity != user.MapIdentity)
                        {
                            return;
                        }

                        targetUser = user.Team.Leader;
                        CommandX = targetUser.X;
                        CommandY = targetUser.Y;
                        await user.SendAsync(this);
                        break;
                    }

                case ActionType.MapQuery: // 102
                    {
                        Role targetRole = RoleManager.GetRole(Command);
                        if (targetRole != null)
                        {
                            await targetRole.SendSpawnToAsync(user);
                        }
                        break;
                    }

                case ActionType.MapTeamMemberStar: // 106
                    {
                        if (user == null)
                        {
                            return;
                        }

                        if (user.Team == null || targetUser == null || !user.Team.IsMember(targetUser.Identity) ||
                            targetUser.MapIdentity != user.MapIdentity)
                        {
                            return;
                        }

                        Command = targetUser.MapIdentity;
                        X = targetUser.X;
                        Y = targetUser.Y;
                        await user.SendAsync(this);
                        break;
                    }

                case ActionType.CharacterObservation: // 117
                    {
                        if (user == null)
                        {
                            return;
                        }

                        targetUser = RoleManager.GetUser(Command);
                        if (targetUser == null)
                        {
                            return;
                        }

                        for (var pos = Item.ItemPosition.EquipmentBegin;
                             pos <= Item.ItemPosition.EquipmentEnd;
                             pos++)
                        {
                            Item item = targetUser.GetEquipment(pos);
                            if (item != null)
                            {
                                await user.SendAsync(new MsgItemInfoEx(item, MsgItemInfoEx.ViewMode.ViewEquipment));
                                if (item.ItemStatus != null)
                                {
                                    await item.ItemStatus.SendToAsync(user);
                                }
                            }
                        }
                        await user.SendAsync(new MsgPlayerAttribInfo(targetUser));
                        break;
                    }

                case ActionType.SpellAbortTransform: // 118
                    {
                        if (user.Transformation != null)
                        {
                            await user.ClearTransformationAsync();
                        }

                        break;
                    }

                case ActionType.SpellAbortFlight: // 120
                    {
                        if (user.QueryStatus(StatusSet.FLY) != null)
                        {
                            await user.DetachStatusAsync(StatusSet.FLY);
                        }

                        break;
                    }

                case ActionType.RelationshipsEnemy:
                    {
                        if (user.Relation != null && user.Relation.IsEnemy(Command))
                        {
                            await user.Relation.SendEnemyInfoAsync(Command);
                        }
                        break;
                    }

                case ActionType.MapJump: // 137
                    {
                        if (role == null)
                        {
                            return;
                        }

                        if (!role.IsAlive)
                        {
                            if (role is Character player)
                            {
                                await player.KickbackAsync();
                                await player.SendAsync(StrDead);
                            }
                            return;
                        }

                        if (role.Map.IsRaceTrack())
                        {
                            if (role is Character player)
                            {
                                await player.KickbackAsync();
                            }
                            return;
                        }

                        ushort newX = (ushort)Command;
                        ushort newY = (ushort)(Command >> 16);

                        if (Identity == user.Identity)
                        {
                            if (!user.IsAlive)
                            {
                                await user.SendAsync(StrDead, TalkChannel.System, Color.Red);
                                return;
                            }

                            if (user.GetDistance(newX, newY) >= 2 * Screen.VIEW_SIZE)
                            {
                                await user.SendAsync(StrInvalidMsg, TalkChannel.System, Color.Red);
                                await RoleManager.KickOutAsync(user.Identity, "big jump");
                                return;
                            }
                        }

                        ArgumentX = user.X;
                        ArgumentY = user.Y;

                        await user.ProcessOnMoveAsync();
                        bool result = await role.JumpPosAsync(newX, newY);

                        if (result)
                        {
                            X = user.X;
                            Y = user.Y;

                            await role.SetDirectionAsync((FacingDirection)(Direction % 8), false);
                            if (role is Character roleUser)
                            {
                                await role.SendAsync(this);
                                await roleUser.Screen.UpdateAsync(this);
                            }
                            else
                            {
                                await role.BroadcastRoomMsgAsync(this, true);
                            }
                            await role.ProcessAfterMoveAsync();
                            MsgAiAction action = new MsgAiAction
                            {
                                Data = new MsgAiActionContract { Action = AiActionType.Jump, Identity = user.Identity, X = user.X, Y = user.Y, Direction = (int)user.Direction }
                            };
                            NpcServer.Instance.Send(NpcServer.NpcClient, action.Encode());
						}
                        break;
                    }

                case ActionType.CharacterDead: // 145
                    {
                        if (user == null)
                        {
                            return;
                        }

                        if (user.IsAlive)
                        {
                            return;
                        }

                        await user.SetGhostAsync();
                        break;
                    }

                case ActionType.SyncScreen: // 146
                    {
                        await user.Screen.SynchroScreenAsync();
                        break;
                    }

                case ActionType.RelationshipsFriend: // 148
                    {
                        if (user.Relation != null && user.Relation.IsFriend(Command))
                        {
                            await user.Relation.SendFriendInfoAsync(Command);
                        }
                        break;
                    }

                case ActionType.CharacterAvatar: // 151
                    {
                        if (user.Gender == 1 && Command >= 200 || user.Gender == 2 && Command < 200)
                        {
                            return;
                        }

                        user.Avatar = (ushort)Command;
                        await user.BroadcastRoomMsgAsync(this, true);
                        await user.SaveAsync();
                        break;
                    }

                case ActionType.QueryTradeBuddy: // 152
                    {
                        if (user.TradePartnerRelation != null)
                        {
                            await user.TradePartnerRelation.SendInfoAsync(Command);
                        }
                        break;
                    }

                case ActionType.ItemDetained: // 153
                    {
                        await user.SendAsync(new MsgAction
                        {
                            Action = ActionType.ClientDialog,
                            X = user.X,
                            Y = user.Y,
                            Identity = user.Identity,
                            Data = 336
                        });

                        await user.SendAsync(new MsgAction
                        {
                            Action = ActionType.ClientDialog,
                            X = user.X,
                            Y = user.Y,
                            Identity = user.Identity,
                            Data = 337
                        });

                        await user.SendAsync(this);
                        break;
                    }

                case ActionType.Away: // 161
                    {
                        if (user == null)
                        {
                            return;
                        }

                        user.IsAway = Command != 0;

                        if (user.IsAway && user.Action != EntityAction.Sit)
                        {
                            await user.SetActionAsync(EntityAction.Sit, true);

                            //if (user.Trade != null)
                            //{
                            //    await user.Trade.SendCloseAsync();
                            //}

                        }
                        else if (!user.IsAway && user.Action == EntityAction.Sit)
                        {
                            await user.SetActionAsync(EntityAction.Stand, true);
                        }

                        await user.BroadcastRoomMsgAsync(this, true);
                        break;
                    }

                case ActionType.BulletinInviteTrans: // 166
                    {
                        await ScriptManager.BulletinInvitationAsync(user, Command);
                        break;
                    }

                case ActionType.ShowType: // 178
                    {
                        Identity = user.Identity;
                        user.CurrentLayout = (byte)Command;
                        await user.SaveAsync();
                        await user.BroadcastRoomMsgAsync(this, true);
                        break;
                    }

                case ActionType.LoginComplete: // 251
                    {
                        if (user == null)
                        {
                            return;
                        }

                        await user.OnLoginAsync();
                        await OnUserLoginCompleteAsync(user);
                        await user.OnLoginAfterModulesAsync();

                        await user.SendAsync(this);
                        break;
                    }

                case ActionType.UpgradeWeaponSkill: // 253
                    {
                        if (!user.IsUnlocked())
                        {
                            await user.SendSecondaryPasswordInterfaceAsync();
                            return;
                        }

                        DbWeaponSkill ws = user.WeaponSkill[(ushort)Command];
                        if (ws == null)
                        {
                            return;
                        }

                        if (ws.Level >= MAX_WEAPONSKILLLEVEL)
                        {
                            return;
                        }

                        int cost = (int)Math.Ceiling(ws.WeaponSkillUp.ReqUplevtime / 22.2d);
                        if (!await user.SpendBoundConquerPointsAsync(EmoneyOperationType.WeaponSkillUpgrade, cost, true))
                        {
                            return;
                        }

                        ws.Level += 1;
                        ws.Experience = 0;

                        await user.WeaponSkill.SendAsync(ws);
                        await user.WeaponSkill.SaveAsync(ws);
                        await user.SendAsync(this);
                        break;
                    }

				case ActionType.UpgradeMagicSkill: // 252
					{
						if (!user.IsUnlocked())
						{
							await user.SendSecondaryPasswordInterfaceAsync();
							return;
						}

						const int minCps = 1;
						Magic skill = user.MagicData[(ushort)Command];
						if (skill == null)
						{
							return;
						}

						if (skill.Level >= skill.MaxLevel)
						{
							return;
						}

						int amount = Math.Max(minCps, (int)((1 - skill.Experience / (double)skill.NeedExp) * skill.EmoneyCost));
						if (!await user.SpendConquerPointsAsync(amount, true, true))
						{
							return;
						}

						await skill.ChangeLevelAsync((byte)(skill.Level + 1));
						skill.Experience = 0;

						await skill.SendAsync();
						await skill.SaveAsync();
						await user.SendAsync(this);
						break;
					}

				case ActionType.InventorySash: // 256
                    {
                        await user.AddSashSpaceAsync(1);
                        break;
                    }

                case ActionType.FriendObservation: // 310
                    {
                        targetUser = RoleManager.GetUser(Command);
                        if (targetUser == null)
                        {
                            return;
                        }

                        await targetUser.SendWindowToAsync(user);
                        await targetUser.SendAsync(string.Format(StrObservingEquipment, user.Name), TalkChannel.Talk);
                        break;
                    }

                case ActionType.UserAttribInfo: // 408
                    {
                        await client.SendAsync(new MsgPlayerAttribInfo(client.Character));
                        break;
                    }
				case ActionType.AwardFirstCredit:
					{
						await user.ClaimFirstCreditGiftAsync();
						break;
					}
				case ActionType.EndRaceTrack: // 402
					{
						//HorseRacing horseRacing = EventManager.GetEvent<HorseRacing>();
						//if (horseRacing == null)
						//{
						//	return;
						//}

						//await user.SendAsync(this);
						//await horseRacing.CrossFinishLineAsync(user);
						break;
					}

				default:
                    {
                        logger.Warning("MsgAction unhandled subtype {0}.\n" + PacketDump.Hex(Encode()), Action);
#if DEBUG
                        Console.WriteLine("MsgAction unhandled subtype {0}.\n" + PacketDump.Hex(Encode()), Action);
#endif
						break;
                    }
            }
        }
    }
}
