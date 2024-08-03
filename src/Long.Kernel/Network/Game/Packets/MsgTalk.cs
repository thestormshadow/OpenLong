using System.Configuration.Internal;
using System.Drawing;
using Long.Database.Entities;
using Long.Game.Network.Ai.Packets;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Kernel.Processors;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.Magics;
using Long.Kernel.States.MessageBoxes;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using Long.Network.Packets.Cross;
using Long.Shared.Helpers;
using Long.Shared.Managers;
using Long.World.Enums;
using Long.World.Map;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using static Long.Kernel.Network.Game.Packets.MsgAction;

namespace Long.Kernel.Network.Game.Packets
{
    /// <remarks>Packet Type 1004</remarks>
    /// <summary>
    ///     Message defining a chat message from one player to the other, or from the system
    ///     to a player. Used for all chat systems in the game, including messages outside of
    ///     the game world state, such as during character creation or to tell the client to
    ///     continue logging in after connect.
    /// </summary>
    public sealed class MsgTalk : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgTalk>();
        private static readonly ILogger commandLogger = Logger.CreateConsoleLogger("gm_command");

        public const uint SystemLookface = 2962001;

        public MsgTalk()
        {
        }

        /// <summary>
        ///     Instantiates a new instance of <see cref="MsgTalk" /> using the recipient's
        ///     character ID, a destination channel, and text to display. By default, sends
        ///     from "SYSTEM" to "ALLUSERS".
        /// </summary>
        /// <param name="channel">Destination channel to send the text on</param>
        /// <param name="text">Text to be displayed in the client</param>
        public MsgTalk(TalkChannel channel, string text)
        {
            Timestamp = Environment.TickCount;
            Color = Color.White;
            Channel = channel;
            Style = TalkStyle.Normal;
            CurrentTime = uint.Parse(DateTime.Now.ToString("HHmm"));
            SenderMesh = SystemLookface;
            SenderName = SYSTEM;
            RecipientName = ALLUSERS;
            Suffix = string.Empty;
            Message = text;
        }

        /// <summary>
        ///     Instantiates a new instance of <see cref="MsgTalk" /> using the recipient's
        ///     character ID, a destination channel, a text color, and text to display. By
        ///     default, sends from "SYSTEM" to "ALLUSERS".
        /// </summary>
        /// <param name="channel">Destination channel to send the text on</param>
        /// <param name="color">Color text is to be displayed in</param>
        /// <param name="text">Text to be displayed in the client</param>
        public MsgTalk(TalkChannel channel, Color color, string text)
        {
            Timestamp = Environment.TickCount;
            Color = color;
            Channel = channel;
            Style = TalkStyle.Normal;
            CurrentTime = uint.Parse(DateTime.Now.ToString("HHmm"));
            SenderMesh = SystemLookface;
            SenderName = SYSTEM;
            RecipientName = ALLUSERS;
            Suffix = string.Empty;
            Message = text;
        }

        /// <summary>
        ///     Instantiates a new instance of <see cref="MsgTalk" /> using the recipient's
        ///     character ID, a destination channel, a text color, sender and recipient's name,
        ///     and text to display.
        /// </summary>
        /// <param name="channel">Destination channel to send the text on</param>
        /// <param name="color">Color text is to be displayed in</param>
        /// <param name="recipient">Name the message displays it is to</param>
        /// <param name="sender">Name the message displays it is from</param>
        /// <param name="text">Text to be displayed in the client</param>
        public MsgTalk(TalkChannel channel, Color color, string recipient, string sender, string text)
        {
            Timestamp = Environment.TickCount;
            Color = color;
            Channel = channel;
            Style = TalkStyle.Normal;
            CurrentTime = uint.Parse(DateTime.Now.ToString("HHmm"));
            SenderMesh = SystemLookface;
            SenderName = sender;
            RecipientName = recipient;
            Suffix = string.Empty;
            Message = text;
        }

        // Packet Properties
        public int Timestamp { get; set; }
        public Color Color { get; set; }
        public TalkChannel Channel { get; set; }
        public TalkStyle Style { get; set; }
        public uint CurrentTime { get; set; }
        public uint RecipientMesh { get; set; }
        public uint SenderMesh { get; set; }
        public uint FirstOfficialType { get; set; }
        public byte Gender { get; set; }
        public byte MessageType { get; set; }
        public List<string> Strings { get; private set; } = new()
        {
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
        };
        public string SenderName 
        {
            get => Strings.ElementAtOrDefault(0);
            set => Strings[0] = value;
        }
        public string RecipientName
        {
            get => Strings[1];
            set => Strings[1] = value;
        }
        public string Suffix
        {
            get => Strings[2];
            set => Strings[2] = value;
        }
        public string Message
        {
            get => Strings[3];
            set => Strings[3] = value;
        }
        public string TargetServer
        {
            get => Strings[6];
            set => Strings[6] = value;
        }

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
            Timestamp = reader.ReadInt32(); // 4
            Color = Color.FromArgb(reader.ReadInt32()); // 8
            Channel = (TalkChannel)reader.ReadUInt16(); // 12
            Style = (TalkStyle)reader.ReadUInt16(); // 14
            CurrentTime = reader.ReadUInt32(); // 16
            RecipientMesh = reader.ReadUInt32(); // 20
            SenderMesh = reader.ReadUInt32(); // 24
            FirstOfficialType = reader.ReadUInt32(); // 28
            Gender = reader.ReadByte();
            MessageType = reader.ReadByte();
            // 33 has a bool check on client, idk what it does tho
            /*
              CGameMsg::SetMsgType((CGameMsg *)(*(_BYTE *)(*((_DWORD *)this + 257) + 33) == 1), (int)v23);
              CMsgTalk::ProcessCheckedMsg(v34);
              CGameMsg::SetMsgType(0, v24);
             */

            Strings = [.. reader.ReadStrings()];
            //if (strings.Count > 3)
            //{
            //    SenderName = strings[0];
            //    RecipientName = strings[1];
            //    Suffix = strings[2];
            //    Message = strings[3];

            //    if (strings.Count > 6)
            //    {
            //        TargetServer = strings[6];
            //    }
            //}
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
            writer.Write((ushort)PacketType.MsgTalk);
            writer.Write(Timestamp); // 4
            writer.Write(Color.ToArgb()); // 8
            writer.Write((ushort)Channel); // 12
            writer.Write((ushort)Style); // 14
            writer.Write(CurrentTime); // 16
            writer.Write(RecipientMesh); // 20
            writer.Write(SenderMesh); // 24
            writer.Write(FirstOfficialType); // 28
            writer.Write(Gender); // 32
            writer.Write(MessageType); // 33
            writer.Write(Strings);
            return writer.ToArray();
        }

        // Static messages
        public const string SYSTEM = "SYSTEM";
        public const string ALLUSERS = "ALLUSERS";

        public static MsgTalk LoginOk { get; } = new(TalkChannel.Login, "ANSWER_OK");
        public static MsgTalk LoginInvalid { get; } = new(TalkChannel.Login, "Invalid login");
        public static MsgTalk LoginNewRole { get; } = new(TalkChannel.Login, "NEW_ROLE");
        public static MsgTalk RegisterOk { get; } = new(TalkChannel.Register, "ANSWER_OK");
        public static MsgTalk RegisterInvalid { get; } = new(TalkChannel.Register, "Invalid request token.");
        public static MsgTalk RegisterInvalidBody { get; } = new(TalkChannel.Register, "Invalid character body.");
        public static MsgTalk RegisterInvalidProfession { get; } = new(TalkChannel.Register, "Invalid character profession.");
        public static MsgTalk RegisterNameTaken { get; } = new(TalkChannel.Register, "This name has already been taken.");
        public static MsgTalk RegisterTryAgain { get; } = new(TalkChannel.Register, "Error, please try later.");
        public static MsgTalk RegisterServerNotSetUp { get; } = new(TalkChannel.Register, "Servers are not set up properly! Try again later.");
        public static MsgTalk LoginRealmInvalid { get; } = new(TalkChannel.Login, "You cannot connect to this server.");

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (!user.Name.Equals(SenderName))
            {
#if DEBUG
                if (user.IsGm())
                {
                    await user.SendAsync("Invalid sender name????");
                }
#endif
                logger.Warning("[Cheat] User {0} {1} attempted to send message with invalid {2} SenderName.\n{3}",
                    user.Identity, user.Name, SenderName, PacketDump.Hex(Encode()));
                return;
            }

            Character target = RoleManager.GetUser(RecipientName);
            await ServerDbContext.CreateAsync(new DbMessageLog
            {
                SenderIdentity = user.Identity,
                SenderName = user.Name,
                TargetIdentity = target?.Identity ?? 0,
                TargetName = target?.Name ?? RecipientName,
                Channel = (ushort)Channel,
                Message = Message,
                Time = DateTime.Now
            });

            string tempMessage = Message;
            if (tempMessage.StartsWith("#") && user.Gender == 2 && tempMessage.Length > 7)
            {
                // let's suppose that the user is with flower charm
                tempMessage = tempMessage[3..^3];
            }

            if (tempMessage.StartsWith("/"))
            {
                string[] splitCommand = tempMessage.Split(' ', 2);
                if (splitCommand.Length > 0)
                {
                    string command = splitCommand[0][1..];
                    string args = string.Empty;
                    if (splitCommand.Length > 1)
                    {
                        args = splitCommand[1];
                    }

                    if (await ExecuteCommandAsync(user, command.ToLower(), args))
                    {
                        commandLogger.Information($"{user.Name} >> {Message}");
                        return;
                    }
                }
            }

            if (user.CurrentServer.HasValue 
                && user.CurrentServerID != RealmManager.ServerIdentity)
            {
                switch (Channel)
                {
                    case TalkChannel.Talk:
                        {
                            await user.SendCrossMsgAsync(this);
                            return;
                        }
                }
            }

            SenderMesh = user.ServerID;
            FirstOfficialType = (uint)(user.UnionMember?.OfficialType ?? 0);

            switch (Channel)
            {
                case TalkChannel.Talk:
                    {
                        if (!user.IsAlive)
                        {
                            return;
                        }

                        await user.BroadcastRoomMsgAsync(this, false);
                        break;
                    }

                case TalkChannel.Whisper:
                    {
                        if (target == null)
                        {
                            await user.SendAsync(StrTargetNotOnline, TalkChannel.Talk, Color.White);
                            await user.LeaveWordAsync(RecipientName, Message);
                            return;
                        }

                        SenderMesh = user.Mesh;
                        RecipientMesh = target.Mesh;
                        await target.SendAsync(this);
                        break;
                    }

                case TalkChannel.Team:
                    {
                        if (user.Team == null)
                        {
                            return;
                        }

                        await user.Team.SendAsync(this, user.Identity);
                        break;
                    }

                case TalkChannel.Friend:
                    {
                        if (user.Relation == null)
                        {
                            return;
                        }

                        await user.Relation.SendToFriendsAsync(this);
                        break;
                    }

                case TalkChannel.Guild:
                    {
                        if (user.SyndicateIdentity == 0)
                        {
                            return;
                        }

                        await user.Syndicate.SendAsync(this, user.Identity);
                        break;
                    }

                case TalkChannel.Family:
                    {
                        if (user.FamilyIdentity == 0)
                        {
                            return;
                        }

                        await user.Family.SendAsync(this, user.Identity);
                        break;
                    }

                case TalkChannel.Ally:
                    {
                        if (user.SyndicateIdentity == 0)
                        {
                            return;
                        }

                        await user.Syndicate.SendAsync(this, user.Identity);
                        await user.Syndicate.BroadcastToAlliesAsync(this);
                        break;
                    }

                case TalkChannel.Union:
                    {
                        if (user.Union == null)
                        {
                            return;
                        }

                        SenderMesh = RealmManager.ServerIdentity;
                        await user.Union.BroadcastAsync(this, user.Identity);
                        break;
                    }

                case TalkChannel.Announce:
                    {
                        if (user.SyndicateIdentity == 0 ||
                            user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        user.Syndicate.Announce = Message[..Math.Min(127, Message.Length)];
                        user.Syndicate.AnnounceDate = DateTime.Now;
                        await user.Syndicate.SaveAsync();
                        break;
                    }

                case TalkChannel.CrossServer: // CS
                    {
#if !DEBUG
                        if (!user.IsGm()
                            && !await user.UserPackage.SpendItemAsync(Item.CROSS_BROADCAST_HORN))
                        {
                            return;
                        }
#endif

                        await RoleManager.BroadcastWorldMsgAsync(this);
                        await BroadcastToServersAsync(new MsgCrossBroadcastPacketC
                        {
                            Data = new()
                            {
                                IgnoreServerId = RealmManager.ServerIdentity,
                                Packet = Encode()
                            }
                        });
                        break;
                    }

                case TalkChannel.Bbs:
                case TalkChannel.GuildBoard:
                case TalkChannel.FriendBoard:
                case TalkChannel.OthersBoard:
                case TalkChannel.TeamBoard:
                case TalkChannel.TradeBoard:
                    {
                        MessageBoard.AddMessage(user, Message, Channel);
                        break;
                    }

                default:
                    {
                        logger.Warning("Unhandled {0} talk channel.\n" + PacketDump.Hex(Encode()), Channel);
                        break;
                    }
            }
        }

        private async Task<bool> ExecuteCommandAsync(Character user, string command, string arg)
        {
            if (user.IsPm())
            {
                switch (command)
                {
                    case "revive":
                        {
                            await user.ReallyRevive(false,false);
                            return true;
                        }
                    case "summonmonster":
						{
                            if (uint.TryParse(arg, out uint MonsterType))
                            {
                                DbMonstertype monstertype = RoleManager.GetMonstertype(MonsterType);
                                var monster = new Monster(monstertype, (uint)IdentityManager.Monster.GetNextIdentity);
                                if (!await monster.InitializeAsync(user.MapIdentity, user.X, user.Y))
                                {
                                }

                                RoleManager.AddRole(monster);
                                await monster.EnterMapAsync();
                            }
							return true;
						}
					case "awardmagic":
					case "awardskill":
						{
							byte skillLevel = 0;
							string[] awardSkill = arg.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
							if (!ushort.TryParse(awardSkill[0], out ushort skillType))
							{
								return true;
							}

							if (awardSkill.Length > 1 && !byte.TryParse(awardSkill[1], out skillLevel))
							{
								return true;
							}

							Magic magic;
							if (user.MagicData.CheckType(skillType))
							{
								magic = user.MagicData[skillType];
								if (await magic.ChangeLevelAsync(skillLevel))
								{
									await magic.SaveAsync();
									await magic.SendAsync();
								}
							}
							else
							{
								if (!await user.MagicData.CreateAsync(skillType, skillLevel))
								{
									await user.SendAsync("[Award Skill] Could not create skill!");
								}
							}

							return true;
						}
					case "vip":
						{
							if (byte.TryParse(arg, out byte vip))
							{
								await user.SetAttributesAsync(ClientUpdateType.VipLevel, vip);
							}

							return true;
						}
					case "tele":
						{
							string[] awardwskill = arg.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
							if (!ushort.TryParse(awardwskill[0], out ushort map))
							{
								return true;
							}
							if (!ushort.TryParse(awardwskill[1], out ushort x))
							{
								return true;
							}
							if (!ushort.TryParse(awardwskill[2], out ushort y))
							{
								return true;
							}

							await user.FlyMapAsync(map, x, y);

							return true;
						}
					case "effect":
						{
							string[] splitParams = arg.Split(" ");
							if (user == null)
							{
								return false;
							}
							if (splitParams.Length == 0)
							{
								return false;
							}
							await user.SendEffectAsync(splitParams[0], true);
							return true;
						}
					case "creategen":
						{
							await user.SendAsync(
								"Attention, use this command only on localhost tests or the generator thread may crash.");
							// mobid mapid mapx mapy boundcx boundcy maxnpc rest maxpergen
							string[] szComs = arg.Split(' ');
							if (szComs.Length < 9)
							{
								await user.SendAsync(
									"/creategen mobid mapid mapx mapy boundcx boundcy maxnpc rest maxpergen timerBegin timerEnd");
								return true;
							}

							ushort idMob = ushort.Parse(szComs[0]);
							uint idMap = uint.Parse(szComs[1]);
							ushort mapX = ushort.Parse(szComs[2]);
							ushort mapY = ushort.Parse(szComs[3]);
							ushort boundcx = ushort.Parse(szComs[4]);
							ushort boundcy = ushort.Parse(szComs[5]);
							ushort maxNpc = ushort.Parse(szComs[6]);
							ushort restSecs = ushort.Parse(szComs[7]);
							ushort maxPerGen = ushort.Parse(szComs[8]);
							int timerBegin = int.Parse(szComs[9]);
							int timerEnd = int.Parse(szComs[10]);

							if (idMap == 0)
							{
								idMap = user.MapIdentity;
							}

							if (mapX == 0 || mapY == 0)
							{
								mapX = user.X;
								mapY = user.Y;
							}

							var newGen = new DbGenerator
							{
								Mapid = idMap,
								Npctype = idMob,
								BoundX = mapX,
								BoundY = mapY,
								BoundCx = boundcx,
								BoundCy = boundcy,
								MaxNpc = maxNpc,
								RestSecs = restSecs,
								MaxPerGen = maxPerGen,
								BornX = 0,
								BornY = 0,
								TimerBegin = timerBegin,
								TimerEnd = timerEnd
							};

							await NpcServer.SendAsync(new MsgAiGeneratorManage(newGen));
							return true;
						}
                    case "level":
					case "uplev":
                        {
                            if (byte.TryParse(arg, out byte level))
                            {
                                await user.AwardLevelAsync(level);
                            }

                            return true;
                        }

                    case "pro":
                        {
                            if (byte.TryParse(arg, out byte proProf))
                            {
                                await user.SetAttributesAsync(ClientUpdateType.Class, proProf);
                            }

                            return true;
                        }

                    case "life":
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Hitpoints, user.MaxLife);
                            return true;
                        }

                    case "mana":
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Mana, user.MaxMana);
                            return true;
                        }

                    case "restore":
                        {
                            if (user.IsAlive)
                            {
                                await user.SetAttributesAsync(ClientUpdateType.Hitpoints, user.MaxLife);
                                await user.SetAttributesAsync(ClientUpdateType.Mana, user.MaxMana);
                            }
                            else
                            {
                                await user.RebornAsync(false, true);
                            }

                            return true;
                        }

                    case "superman":
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Strength, 176);
                            await user.SetAttributesAsync(ClientUpdateType.Agility, 256);
                            await user.SetAttributesAsync(ClientUpdateType.Vitality, 110);
                            await user.SetAttributesAsync(ClientUpdateType.Spirit, 125);
                            return true;
                        }

                    case "xp":
                        {
                            await user.AddXpAsync(100);
                            await user.BurstXpAsync();
                            return true;
                        }

                    case "sp":
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Stamina, user.MaxEnergy);
                            return true;
                        }

                    case "rangedrop":
                        {
                            string[] splitParams = arg.Split(" ");
                            if (splitParams.Length < 4)
                            {
                                await user.SendAsync("Command requires arguments! /rangedrop itemtype range num secs");
                                return true;
                            }

                            if (!uint.TryParse(splitParams[0], out uint itemType))
                            {
                                await user.SendAsync("Invalid itemtype value. Must be a valid number.");
                                return true;
                            }

                            DbItemtype dbItemtype = ItemManager.GetItemtype(itemType);
                            if (dbItemtype == null)
                            {
                                await user.SendAsync("Invalid itemtype value. Item does not exist.");
                                return true;
                            }

                            if (!uint.TryParse(splitParams[1], out uint range) || range > 100)
                            {
                                await user.SendAsync("Invalid range value. Must be a number below 100.");
                                return true;
                            }

                            if (!uint.TryParse(splitParams[2], out uint num) || num > 100)
                            {
                                await user.SendAsync("Invalid num value. Must be a number below 100.");
                                return true;
                            }

                            if (!uint.TryParse(splitParams[3], out uint secs))
                            {
                                await user.SendAsync("Invalid seconds value. Must be a number.");
                                return true;
                            }

                            uint mapId = user.MapIdentity;
                            int x = (int)(user.X - range);
                            int y = (int)(user.Y - range);

                            const string format = "Map_DropMultiItems({0},{1},{2},{3},{4},{5},{6},{7})";
                            string luaScript =
                                string.Format(format, mapId, itemType, x, y, range * 2, range * 2, num, secs);
                            LuaScriptManager.Run(luaScript);
                            return true;
                        }

                    case "awarditem":
                        {
                            string[] splitParam = arg.Split(' ');
                            if (!uint.TryParse(splitParam[0], out uint idAwardItem))
                            {
                                return true;
                            }

                            int count;
                            if (splitParam.Length < 2 || !int.TryParse(splitParam[1], out count))
                            {
                                count = 1;
                            }

                            DbItemtype itemtype = ItemManager.GetItemtype(idAwardItem);
                            if (itemtype == null)
                            {
                                await user.SendAsync($"[AwardItem] Itemtype {idAwardItem} not found");
                                return true;
                            }

                            await user.UserPackage.AwardItemAsync(idAwardItem, count);
                            await user.SendAsync($"[AwardItem] {itemtype.Name} award success!");
                            return true;
                        }

					case "money":
					case "awardmoney":
                        {
                            if (int.TryParse(arg, out int moneyAmount))
                            {
                                await user.AwardMoneyAsync(moneyAmount);
                            }

                            return true;
                        }

#if DEBUG
                    case "cps":
                    case "awardemoney":
                        {
                            if (int.TryParse(arg, out int emoneyAmount))
                            {
                                await user.AwardConquerPointsAsync(emoneyAmount);
                                await user.SaveEmoneyLogAsync(Character.EmoneyOperationType.AwardCommand, 0, 0,
                                    (uint)emoneyAmount);
                            }

                            return true;
                        }

                    case "awardemoneymono":
                        {
                            if (int.TryParse(arg, out int emoneyAmount))
                            {
                                await user.AwardBoundConquerPointsAsync(emoneyAmount);
                                await user.SaveEmoneyMonoLogAsync(Character.EmoneyOperationType.AwardCommand, 0, 0,
                                    (uint)emoneyAmount);
                            }

                            return true;
                        }
#endif

                    case "awardridingpoint":
                        {
                            if (int.TryParse(arg, out int ridingPoints))
                            {
                                await user.AwardHorseRacePointsAsync(ridingPoints);
                            }

                            return true;
                        }

                    case "awardbattlexp":
                        {
                            if (!long.TryParse(arg, out long exp))
                            {
                                return true;
                            }

                            await user.AwardBattleExpAsync(exp, true);
                            return true;
                        }

                    case "awardexpball":
                        {
                            if (user.Map != null && user.Map.IsNoExpMap())
                            {
                                await user.SendAsync("Cannot award experience in no-exp map.");
                                return true;
                            }

                            if (!int.TryParse(arg, out int amount))
                            {
                                return true;
                            }

                            long exp = user.CalculateExpBall(Math.Max(1, amount) * Role.EXPBALL_AMOUNT);
                            await user.AwardExperienceAsync(exp);
                            return true;
                        }

                    case "awardcultivation":
                        {
                            if (int.TryParse(arg, out int amount))
                            {
                                await user.AwardCultivationAsync(amount);
                            }

                            return true;
                        }

                    case "awardstrength":
                        {
                            if (int.TryParse(arg, out int amount))
                            {
                                await user.AwardStrengthValueAsync(amount);
                            }

                            return true;
                        }

                    case "awardculture":
                        {
                            if (int.TryParse(arg, out int amount))
                            {
                                await user.AwardCultureAsync(amount);
                            }

                            return true;
                        }

                    case "awardwskill":
                        {
                            byte level = 1;

                            string[] awardwskill = arg.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (!ushort.TryParse(awardwskill[0], out ushort type))
                            {
                                return true;
                            }

                            if (awardwskill.Length > 1 && !byte.TryParse(awardwskill[1], out level))
                            {
                                return true;
                            }

                            if (user.WeaponSkill[type] == null)
                            {
                                await user.WeaponSkill.CreateAsync(type, level);
                            }
                            else
                            {
                                user.WeaponSkill[type].Level = level;
                                await user.WeaponSkill.SaveAsync(user.WeaponSkill[type]);
                                await user.WeaponSkill.SendAsync(user.WeaponSkill[type]);
                            }

                            return true;
                        }

                    case "setmetempsychosis":
                        {
                            string[] p = arg.Split(' ');
                            if (p.Length == 1)
                            {
                                await user.SetAttributesAsync(ClientUpdateType.Class, uint.Parse(p[0]));
                                await user.SetAttributesAsync(ClientUpdateType.Reborn, 0);
                            }
                            else if (p.Length == 2)
                            {
                                await user.SetAttributesAsync(ClientUpdateType.Class, uint.Parse(p[1]));
                                await user.SetAttributesAsync(ClientUpdateType.FirstProfession, uint.Parse(p[0]));
                                await user.SetAttributesAsync(ClientUpdateType.Reborn, 1);
                            }
                            else if (p.Length == 3)
                            {
                                await user.SetAttributesAsync(ClientUpdateType.Class, uint.Parse(p[2]));
                                await user.SetAttributesAsync(ClientUpdateType.FirstProfession, uint.Parse(p[0]));
                                await user.SetAttributesAsync(ClientUpdateType.PreviousProfession, uint.Parse(p[1]));
                                await user.SetAttributesAsync(ClientUpdateType.Reborn, 2);
                                await user.SaveAsync();
                            }

                            return true;
                        }

                    case "targetsetmetempsychosis":
                        {
                            string[] p = arg.Split(' ');
                            Character target = RoleManager.GetUser(p[0]);
                            if (target == null)
                            {
                                await user.SendAsync("[PM] target not found.");
                                return true;
                            }

                            if (p.Length == 2)
                            {
                                await target.SetAttributesAsync(ClientUpdateType.Class, uint.Parse(p[1]));
                                await target.SetAttributesAsync(ClientUpdateType.Reborn, 0);
                            }
                            else if (p.Length == 3)
                            {
                                await target.SetAttributesAsync(ClientUpdateType.Class, uint.Parse(p[2]));
                                await target.SetAttributesAsync(ClientUpdateType.FirstProfession, uint.Parse(p[1]));
                                await target.SetAttributesAsync(ClientUpdateType.Reborn, 1);
                            }
                            else if (p.Length == 4)
                            {
                                await target.SetAttributesAsync(ClientUpdateType.Class, uint.Parse(p[3]));
                                await target.SetAttributesAsync(ClientUpdateType.FirstProfession, uint.Parse(p[1]));
                                await target.SetAttributesAsync(ClientUpdateType.PreviousProfession, uint.Parse(p[2]));
                                await target.SetAttributesAsync(ClientUpdateType.Reborn, 2);
                                await target.SaveAsync();
                            }

                            return true;
                        }

                    case "kickoutall":
                        {
                            await RoleManager.KickOutAllAsync(arg, true);
                            return true;
                        }

                    case "action":
                        {
                            if (uint.TryParse(arg, out uint action))
                            {
                                await GameAction.ExecuteActionAsync(action, user, null, null);
                            }

                            return true;
                        }

                    case "reloadaction":
                        {
                            if (arg.Equals("all"))
                            {
                                logger.Warning("Reload action all called by {0} {1}", user.Identity, user.Name);
                                await user.SendAsync(
                                    "[Reload Action] Do not run this command in production unless you know what you're doing! This command may freeze the server.");
                                await ScriptManager.LoadActionsAsync();
                            }
                            else if (uint.TryParse(arg, out uint action))
                            {
                                if (await ScriptManager.ReloadActionAsync(action))
                                {
                                    await user.SendAsync(string.Format("[Reload Action] Action {0} reloaded.", action));
                                }
                                else
                                {
                                    await user.SendAsync(
                                        $"[Reload Action] Failed to load action {action}.");
                                }
                            }

                            return true;
                        }

                    #region DEBUG test commands

#if DEBUG

                    case "stagegoal":
                        {
                            string[] p = arg.Split(' ');
                            if (p.Length < 2)
                            {
                                return true;
                            }

                            if (!int.TryParse(p[0], out int goal)
                                || !uint.TryParse(p[1], out uint value))
                            {
                                return true;
                            }

                            await user.StageGoal.SetProgressAsync((ProcessGoalManager.GoalType)goal, value);
                            return true;
                        }

                    case "testpaint":
                        {
                            MsgPaint msg = new();
                            for (int i = 0; i < GameMapData.WalkXCoords.Length; i++)
                            {
                                msg.Points.Add(new Point(user.X + GameMapData.WalkXCoords[i],
                                    user.Y + GameMapData.WalkYCoords[i]));
                            }

                            await user.SendAsync(msg);
                            return true;
                        }

                    case "pledge_syn":
                        {
                            if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
                            {
                                await user.SendAsync("You must be a Guild Leader in order to pledge your loyalty.");
                                return true;
                            }

                            await KingdomManager.CreateNewUnionAsync(user);
                            return true;
                        }

                    case "forcekingdom":
                        {
                            if (user.Union == null)
                            {
                                await user.SendAsync("You must be in a Union to make it a Kingdom.");
                                return true;
                            }

                            await KingdomManager.SetKingdomAsync(user.Union);
                            return true;
                        }

                    case "sendleague":
                        {
                            await user.SendUnionAsync();
                            return true;
                        }

                    case "leagueopt":
                        {
                            await user.SendAsync(new MsgLeagueOpt
                            {
                                Action = (MsgLeagueOpt.LeagueOpt)byte.Parse(arg)
                            });
                            return true;
                        }

                    case "goldbricks":
                        {
                            await user.AddGoldBricksAsync(10);
                            return true;
                        }

                    case "signin":
                        {
                            await user.SendAsync(new MsgSignIn
                            {
                                Action = (MsgSignIn.MsgSignInType)byte.Parse(arg)
                            });
                            return true;
                        }

                    case "switchto":
                        {
                            string[] args = arg.Split(' ');
                            if (args.Length < 1)
                            {
                                return true;
                            }

                            string serverName = string.Empty;
                            switch (args[0])
                            {
                                case "realm":
                                    {
                                        if (!RealmManager.GetServer(RealmManager.RealmIdentity,
                                                out RealmManager.ServerConfig realm))
                                        {
                                            await user.SendAsync("Realm is not connected.");
                                            return false;
                                        }

                                        serverName = realm.ServerName;
                                        break;
                                    }
                                case "server":
                                    {
                                        await user.SendAsync("Switching servers is not yet implemented.");
                                        return true;
                                    }
                            }

                            if (string.IsNullOrEmpty(serverName))
                            {
                                return true;
                            }

                            await RealmManager.ConnectUserToServerAsync(user, serverName);
                            return true;
                        }

                    case "backtoserver":
                        {
                            if (user.CurrentServerID == RealmManager.ServerIdentity)
                            {
                                // already here!
                                return true;
                            }

                            await SendOSMsgAsync(new MsgCrossRealmActionC
                            {
                                Data = new()
                                {
                                    Action = (uint)CrossRealmAction.KickoutPlayer,
                                    Command = user.RealmUserId,
                                    Data = user.Identity,
                                    Strings = new()
                                }
                            }, user.CurrentServerID);
                            await user.ReturnServerAsync();
                            return true;
                        }

                    case "submitservername":
                        {
                            await user.SendAsync(new MsgName
                            {
                                Action = MsgName.StringAction.CurrServerName,
                                Identity = user.Identity,
                                Strings = new List<string>()
                                {
                                    user.CurrentServerName
                                }
                            });
                            return true;
                        }

                    case "awarditemdbg":
                        {
                            string[] p = arg.Split(' ');
                            if (p.Length < 1 || string.IsNullOrEmpty(p[0]))
                            {
                                await user.SendAsync("/awarditemdbg itemtype [plus] [gem1] [gem2] [add_life] [reduce_dmg]");
                                return true;
                            }

                            DbItem dbItem = Item.CreateEntity(uint.Parse(p[0]), true);
                            if (dbItem == null)
                            {
                                await user.SendAsync("[Award Item] Invalid item type");
                                return true;
                            }

                            if (p.Length > 1)
                            {
                                dbItem.Magic3 = byte.Parse(p[1]);
                            }

                            if (p.Length > 2)
                            {
                                dbItem.Gem1 = byte.Parse(p[2]);
                            }

                            if (p.Length > 3)
                            {
                                dbItem.Gem2 = byte.Parse(p[3]);
                            }

                            if (p.Length > 4)
                            {
                                dbItem.AddLife = byte.Parse(p[4]);
                            }

                            if (p.Length > 5)
                            {
                                dbItem.ReduceDmg = byte.Parse(p[5]);
                            }

                            Item item = new(user);
                            if (!await item.CreateAsync(dbItem))
                            {
                                return false;
                            }

                            if (item.IsCountable())
                            {
                                item.AccumulateNum = Math.Max(1, item.AccumulateNum);
                            }

                            if (item.IsActivable())
                            {
                                await item.ActivateAsync();
                            }

                            await user.UserPackage.AddItemAsync(item);
                            return true;
                        }

					case "awarditemfull":
						{
							if (!uint.TryParse(arg, out uint idAwardItem))
							{
								return true;
							}

							DbItemtype itemtype = ItemManager.GetItemtype(idAwardItem);
							if (itemtype == null)
							{
								await user.SendAsync($"[AwardItem] Itemtype {idAwardItem} not found");
								return true;
							}

							Item item = new(user);
							if (!await item.CreateAsync(itemtype, Item.ItemPosition.Inventory, true))
							{
								return true;
							}

							if (item.IsCountable())
							{
								item.AccumulateNum = Math.Max(1, item.AccumulateNum);
							}

							if (item.IsActivable())
							{
								await item.ActivateAsync();
							}

							if (item.IsEquipment())
							{
								if (item.IsGourd() || item.IsGarment() || item.IsAccessory() || item.IsMountArmor())
								{
									item.ReduceDamage = 1;
								}
								else
								{
									item.ChangeAddition(12);
									item.ReduceDamage = 7;
									item.Enchantment = 255;
								}

								switch (item.GetItemSubType())
								{
									case 421:
									case 620:
									case 134:
									case 152:
										{
											item.SocketOne = Item.SocketGem.SuperPhoenixGem;
											item.SocketTwo = Item.SocketGem.SuperPhoenixGem;
											break;
										}

									case 201:
										{
											item.SocketOne = Item.SocketGem.SuperThunderGem;
											item.SocketTwo = Item.SocketGem.SuperThunderGem;
											break;
										}

									case 202:
										{
											item.SocketOne = Item.SocketGem.SuperGloryGem;
											item.SocketTwo = Item.SocketGem.SuperGloryGem;
											break;
										}
									case 203:
										{
											break;
										}
									case 204:
										{
											item.SocketOne = Item.SocketGem.SuperThunderGem;
											item.SocketTwo = Item.SocketGem.SuperGloryGem;
											break;
										}

									case 300:
										{
											break;
										}

									default:
										{
											if (user.ProfessionSort >= 10)
											{
												item.SocketOne = Item.SocketGem.SuperTortoiseGem;
												item.SocketTwo = Item.SocketGem.SuperTortoiseGem;
											}
											else
											{
												item.SocketOne = Item.SocketGem.SuperDragonGem;
												item.SocketTwo = Item.SocketGem.SuperDragonGem;
											}
											break;
										}
								}
							}
							else if (item.IsMount())
							{
								item.ChangeAddition(12);
							}

							await user.UserPackage.AddItemAsync(item, false);
							return true;
						}

					case "enthrallment":
                        {
                            await user.SynchroAttributesAsync(ClientUpdateType.ResetWallow, 0);
                            return true;
                        }

                    case "talk":
                        {
                            string[] args = arg.Split(' ', 2);
                            if (args.Length != 2)
                            {
                                return true;
                            }

                            await user.SendAsync(new MsgTalk((TalkChannel)int.Parse(args[0]), args[1])
                            {
                                Gender = (byte)user.Gender,
                                MessageType = 1,
                                SenderName = user.Name
                            });
                            return true;
                        }

                    case "syncompete":
                        {
                            await user.SendAsync(new MsgSynCompete
                            {
                                Action = MsgSynCompete.SynCompeteAction.SetHeroMerit,
                                CompetePoint = 123
                            });
                            await user.SendAsync(new MsgSynCompete
                            {
                                Action = MsgSynCompete.SynCompeteAction.SetHeroSynCompetePoint,
                                Merit = 456
                            });
                            await user.SendAsync(new MsgSynCompete
                            {
                                Action = MsgSynCompete.SynCompeteAction.List,
                                Rank = 
                                [
                                    new MsgSynCompete.SynCompeteRank
                                    {
                                        Data = 1,
                                        DataLong = 2,
                                        Name = "Teste123"
                                    },
                                    new MsgSynCompete.SynCompeteRank
                                    {
                                        Data = 2,
                                        DataLong = 4,
                                        Name = "Teste456"
                                    }
                                ]
                            });
                            return true;
                        }

                    case "awardtitle":
                        {
                            string[] args = arg.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                            uint titleType = uint.Parse(args[0]);
                            uint titleId = uint.Parse(args[1]);
                            uint secs = 0;
                            if (args.Length >= 3)
                            {
                                secs = uint.Parse(args[1]);
                            }

                            if (await user.TitleStorage.AwardTitleAsync(titleType, titleId, secs))
                            {
                                await user.SendAsync($"[AwardTitle] Title awarded or updated!");
                            }
                            return true;
                        }

					case "awardalltitles":
						{
							foreach (var titletype in TitleStorageManager.GetTitlesType())                            
								await user.TitleStorage.AwardTitleAsync(titletype.Type, titletype.Type, titletype.SaveTime);
                            
							await user.SendAsync($"[AwardTitle] Title awarded or updated!");
							return true;
						}

#endif

						#endregion
				}
            }

            if (user.IsGm())
            {
                if (user.CurrentServer.HasValue && user.CurrentServerID != RealmManager.ServerIdentity)
                {
                    switch (command)
                    {
                        case "bring":
                        case "chgmap":
                        case "find":
                        case "fly":
                        case "kickout":
                        case "moveto":
                            {
                                await user.SendCrossMsgAsync(this);
                                return true;
                            }
                    }
                }

                switch (command)
                {
                    case "bring":
                        {
                            if (user.MapIdentity == 5000)
                            {
                                await user.SendAsync("You cannot bring players to GM area.");
                                return true;
                            }

                            Character bringTarget = RoleManager.GetUser(arg);
                            if (bringTarget == null && uint.TryParse(arg, out uint idFindTarget))
                            {
                                bringTarget = RoleManager.GetUser(idFindTarget);
                            }

                            if (bringTarget == null)
                            {
                                await user.SendAsync("Target not found");
                                return true;
                            }

                            if ((bringTarget.MapIdentity == 6002 || bringTarget.MapIdentity == 6010) && !user.IsPm())
                            {
                                await user.SendAsync("You cannot move players from jail.");
                                return true;
                            }

                            await bringTarget.FlyMapAsync(user.MapIdentity, user.X, user.Y);
                            return true;
                        }

                    case "cmd":
                        {
                            string[] cmdParams = arg.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                            string subCmd = cmdParams[0];

                            if (command.Length > 1)
                            {
                                string subParam = cmdParams[1];

                                switch (subCmd.ToLower())
                                {
                                    case "broadcast":
                                        await RoleManager.BroadcastWorldMsgAsync(subParam, TalkChannel.Center,
                                            Color.White);
                                        break;

                                    case "gmmsg":
                                        await RoleManager.BroadcastWorldMsgAsync($"{user.Name} says: {subParam}",
                                            TalkChannel.Center, Color.White);
                                        break;

                                    case "player":
                                        if (subParam.Equals("all", StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            await user.SendAsync(
                                                $"Players Online: {RoleManager.OnlinePlayers}, Distinct: {RoleManager.OnlineUniquePlayers} (max: {RoleManager.MaxOnlinePlayers})",
                                                TalkChannel.TopLeft, Color.White);
                                        }
                                        else if (subParam.Equals("map", StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            await user.SendAsync(
                                                $"Map Online Players: {user.Map.PlayerCount} ({user.Map.Name})",
                                                TalkChannel.TopLeft, Color.White);
                                        }

                                        break;
                                }

                                return true;
                            }

                            return true;
                        }

                    case "chgmap":
                        {
                            string[] chgMapParams = arg.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
                            if (chgMapParams.Length < 3)
                            {
                                return true;
                            }

                            if (uint.TryParse(chgMapParams[0], out uint chgMapId)
                                && ushort.TryParse(chgMapParams[1], out ushort chgMapX)
                                && ushort.TryParse(chgMapParams[2], out ushort chgMapY))
                            {
                                await user.FlyMapAsync(chgMapId, chgMapX, chgMapY);
                            }

                            return true;
                        }

                    case "openui":
                        {
                            if (uint.TryParse(arg, out uint ui))
                            {
                                await user.SendAsync(new MsgAction
                                {
                                    Action = MsgAction.ActionType.ClientCommand,
                                    Identity = user.Identity,
                                    Command = ui,
                                    ArgumentX = user.X,
                                    ArgumentY = user.Y
                                });
                            }

                            return true;
                        }

                    case "openwindow":
                        {
                            if (uint.TryParse(arg, out uint window))
                            {
                                await user.SendAsync(new MsgAction
                                {
                                    Action = MsgAction.ActionType.ClientDialog,
                                    Identity = user.Identity,
                                    Command = window,
                                    ArgumentX = user.X,
                                    ArgumentY = user.Y
                                });
                            }

                            return true;
                        }

                    case "kickout":
                        {
                            Character findTarget = RoleManager.GetUser(arg);
                            if (findTarget == null && uint.TryParse(arg, out uint idFindTarget))
                            {
                                findTarget = RoleManager.GetUser(idFindTarget);
                            }

                            if (findTarget == null)
                            {
                                await user.SendAsync("Target not found");
                                return true;
                            }

                            try
                            {
                                findTarget.Client.Disconnect();
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Error on kickout", ex.Message);
                                WorldProcessor.Instance.Queue(WorldProcessor.NO_MAP_GROUP, () =>
                                {
                                    RoleManager.ForceLogoutUser(findTarget.Identity);
                                    return Task.CompletedTask;
                                });
                            }

                            return true;
                        }

                    case "find":
                        {
                            Character findTarget = RoleManager.GetUser(arg);
                            if (findTarget == null && uint.TryParse(arg, out uint idFindTarget))
                            {
                                findTarget = RoleManager.GetUser(idFindTarget);
                            }

                            if (findTarget == null)
                            {
                                await user.SendAsync("Target not found");
                                return true;
                            }

                            await user.FlyMapAsync(findTarget.MapIdentity, findTarget.X, findTarget.Y);
                            return true;
                        }

                    case "fly":
                        {
                            string[] chgMapParams = arg.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
                            if (chgMapParams.Length < 1)
                            {
                                await user.SendAsync("/fly mapid: for random position in map", TalkChannel.Talk);
                                await user.SendAsync("/fly x y: for random position in same map around coords",
                                    TalkChannel.Talk);
                                await user.SendAsync("/fly mapid x y: for random position in map around coords",
                                    TalkChannel.Talk);
                                return true;
                            }

                            GameMap gameMap;
                            uint mapId;
                            int x;
                            int y;

                            if (chgMapParams.Length == 1)
                            {
                                mapId = uint.Parse(chgMapParams[0]);
                                gameMap = MapManager.GetMap(mapId);
                                if (gameMap == null)
                                {
                                    await user.SendAsync("Invalid map");
                                    return true;
                                }

                                Point result = await gameMap.QueryRandomPositionAsync();
                                if (result == default)
                                {
                                    await user.SendAsync("Could not find valid position.");
                                    return true;
                                }

                                x = result.X;
                                y = result.Y;
                            }
                            else if (chgMapParams.Length == 2)
                            {
                                mapId = user.MapIdentity;
                                gameMap = user.Map;
                                x = int.Parse(chgMapParams[0]);
                                y = int.Parse(chgMapParams[1]);

                                Point result = await gameMap.QueryRandomPositionAsync(x, y, 18);
                                if (result != default)
                                {
                                    x = result.X;
                                    y = result.Y;
                                }
                            }
                            else
                            {
                                mapId = uint.Parse(chgMapParams[0]);
                                gameMap = MapManager.GetMap(mapId);
                                if (gameMap == null)
                                {
                                    await user.SendAsync("Invalid map");
                                    return true;
                                }

                                x = int.Parse(chgMapParams[1]);
                                y = int.Parse(chgMapParams[2]);

                                Point result = await gameMap.QueryRandomPositionAsync(x, y, 18);
                                if (result != default)
                                {
                                    x = result.X;
                                    y = result.Y;
                                }
                            }

                            if (!gameMap.IsStandEnable(x, y))
                            {
                                await user.SendAsync(StrInvalidCoordinate);
                                return true;
                            }

                            bool error = false;
                            List<Role> roleSet = user.Map.Query9Blocks(x, y);
                            foreach (Role role in roleSet)
                            {
                                if (role is BaseNpc npc
                                    && role.X == x && role.Y == y)
                                {
                                    error = true;
                                    break;
                                }
                            }

                            if (!error)
                            {
                                await user.FlyMapAsync(gameMap.Identity, x, y);
                            }
                            else
                            {
                                await user.SendAsync(StrInvalidCoordinate);
                            }

                            return true;
                        }

                    case "moveto":
                        {
                            if (arg.StartsWith("npc"))
                            {
                                string argument = arg.Substring(4);
                                Role findTarget = RoleManager.GetUser(argument);
                                if (findTarget == null && uint.TryParse(argument, out uint idFindTarget))
                                {
                                    findTarget = RoleManager.GetRole(idFindTarget);
                                }

                                if (findTarget == null)
                                {
                                    await user.SendAsync("Target not found");
                                    return true;
                                }

                                await user.SendAsync(new MsgAction
                                {
                                    Action = ActionType.PathFinding,
                                    Identity = user.Identity,
                                    Timestamp = 0,
                                    Command = findTarget.MapIdentity,
                                    X = findTarget.X,
                                    Y = findTarget.Y
                                });
                                return true;
                            }

                            string[] args = arg.Split(' ');
                            if (args.Length < 3)
                            {
                                return true;
                            }

                            if (!uint.TryParse(args[0], out var mapId)
                                || !ushort.TryParse(args[1], out var x)
                                || !ushort.TryParse(args[2], out var y)
                                )
                            {
                                return true;
                            }

                            GameMap gameMap = MapManager.GetMap(mapId);
                            if (gameMap == null)
                            {
                                return true;
                            }

                            var point = await gameMap.QueryRandomPositionAsync(x, y, 5);
                            if (point.Equals(default))
                            {
                                return true;
                            }

                            await user.SendAsync(new MsgAction
                            {
                                Action = ActionType.PathFinding,
                                Identity = user.Identity,
                                Timestamp = 0,
                                Command = gameMap.Identity,
                                X = (ushort)point.X,
                                Y = (ushort)point.Y
                            });
                            return true;
                        }



                    case "bot":
                        {
                            string[] myParams = arg.Split(new[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);

                            if (myParams.Length < 2)
                            {
                                await user.SendAsync("/bot [target_name] [reason]", TalkChannel.Talk);
                                return true;
                            }

                            Character target = RoleManager.GetUser(myParams[0]);
                            if (target != null)
                            {
                                await target.SendAsync(StrBotjail);
                                await target.FlyMapAsync(6002, 28, 74);
                                await target.SaveAsync();
                                await target.SendAsync(new MsgCheatingProgram(target.Identity, myParams[1]));
                            }

                            return true;
                        }

                    case "macro":
                        {
                            string[] myParams = arg.Split(new[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);

                            if (myParams.Length < 2)
                            {
                                await user.SendAsync("/macro [target_name] [reason]", TalkChannel.Talk);
                                return true;
                            }

                            Character target = RoleManager.GetUser(myParams[0]);
                            if (target != null)
                            {
                                await target.SendAsync(StrMacrojail);
                                await target.FlyMapAsync(6010, 28, 74);
                                await target.SaveAsync();
                                await target.SendAsync(new MsgCheatingProgram(target.Identity, myParams[1]));
                            }

                            return true;
                        }
                }
            }

            switch (command)
            {
                case "pos":
                    {
                        await user.SendAsync($"MapID[{user.MapIdentity}],Name[{user.Map?.Name}],Pos[{user.X},{user.Y}]", TalkChannel.Talk, Color.White);
                        return true;
                    }
                case "bp":
                    {
                        await user.SendAsync($"Battle effect: {user.BattlePower}");
                        return true;
                    }
                case "dc":
                case "disconnect":
                    {
                        await RoleManager.KickOutAsync(user.Identity, "/kickout");
                        return true;
                    }
                case "clearinventory":
                    {
                        if (user.MessageBox != null)
                        {
                            await user.SendAsync(StrClearInventoryCloseBoxes);
                            return true;
                        }

                        user.MessageBox = new CleanInventoryMessageBox(user);
                        await user.MessageBox.SendAsync();
                        return true;
                    }
            }

            return false;
        }
    }

    /// <summary>
    ///     Enumeration for defining the channel text is printed to. Can also print to
    ///     separate states of the client such as character registration, and can be
    ///     used to change the state of the client or deny a login.
    /// </summary>
    public enum TalkChannel : ushort
    {
        Talk = 2000,
        Whisper,
        Action,
        Team,
        Guild,
        Family = 2006,
        System,
        Yell,
        Friend,
        Center = 2011,
        TopLeft,
        Ghost,
        Service,
        Tip,
        PushUnread,
        World = 2021,
        Qualifier = 2022,
        Ally = 2025,
        Union = 2028,
        Winner = 2032,
        Register = 2100,
        Login,
        Shop,
        Vendor = 2104,
        Website,
        GuildWarRight1 = 2108,
        GuildWarRight2,
        Offline,
        Announce,
        MessageBox,
        PinkText = 2115,
        TradeBoard = 2201,
        FriendBoard,
        TeamBoard,
        GuildBoard,
        OthersBoard,
        Bbs,
        Realm = 2242,
        ChangePasswordNotify = 2300,
        CrossServer = 2402,
        LuckyPocket = 2404,
        Broadcast = 2500,
        Monster = 2600
    }

    /// <summary>
    ///     Enumeration type for controlling how text is stylized in the client's chat
    ///     area. By default, text appears and fades overtime. This can be overridden
    ///     with multiple styles, hard-coded into the client.
    /// </summary>
    [Flags]
    public enum TalkStyle : ushort
    {
        Normal = 0,
        Scroll = 1 << 0,
        Flash = 1 << 1,
        Blast = 1 << 2
    }
}