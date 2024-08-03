using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Settings;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Network.Packets.Cross;
using Long.Network.Sockets;
using Long.Shared.Helpers;
using Long.Shared.Managers;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using Long.Kernel.Network.Cross;
using Long.Network.Packets;
using Long.Kernel.Network.Game;
using Long.Kernel.States.World;
using Long.Kernel.Network.Cross.Server.Packets;
using static Long.Kernel.States.User.Character;

namespace Long.Kernel.Managers
{
    public class RealmManager
    {
        private static readonly ILogger logger = Log.ForContext<RealmManager>();
        private static readonly ILogger crossConnectionLogger = Logger.CreateSysConsoleLogger(GameServerSettings.Instance != null ? $"cross_connection_{GameServerSettings.Instance.Game.Name}" : "cross_connection");

        private static readonly IdentityManager crossPlayerIds = new IdentityManager(3_999_900_001, 3_999_999_999);
        private static Dictionary<uint, ServerConfig> sharedRealmServers = new();

        public static uint ServerIdentity => GameServerSettings.Instance?.Game.Id ?? 0u;
        public static uint RealmIdentity { get; set; }

        #region Initialization

        public static async Task InitializeAsync()
        {
            // reset realm npc positions            
            foreach (var realmNpc in RoleManager.QueryRoles(x => x is BaseNpc npc && npc.Type == BaseNpc.FRONTIER_SERVER_TRANS_NPC).Cast<BaseNpc>())
            {
                if (realmNpc.MapIdentity != 5000)
                {
                    await realmNpc.ChangePosAsync(5000, 99, 99);
                }
            }

            var clientConfig = ClientConfigRepository.Get(ServerIdentity);
            if (clientConfig == null)
            {
                throw new InvalidOperationException("Client config has not been found for this server.");
            }

            sharedRealmServers.Add(ServerIdentity, new ServerConfig
            {
                ServerId = ServerIdentity,
                ServerName = clientConfig.ServerName,
                Attribute = clientConfig.Attribute,
                MapId = clientConfig.MapId,
                NpcId = clientConfig.NpcId,
                PosX = clientConfig.PosX,
                PosY = clientConfig.PosY
            });

            var realmServers = ClientConfigRepository.GetRealm(clientConfig.CrossServer);
            foreach (var server in realmServers.Where(x => x.Id != ServerIdentity))
            {
                sharedRealmServers.Add(server.Id, new ServerConfig
                {
                    Attribute = server.Attribute,
                    MapId = server.MapId,
                    NpcId = server.NpcId,
                    PosX = server.PosX,
                    PosY = server.PosY,
                    ServerId = server.Id,
                    ServerName = server.ServerName
                });

                if (server.Attribute != 0)
                {
                    RealmIdentity = server.Id;
                }
            }

            RealmConnectionManager.NoRealmServer = sharedRealmServers.Count == 1;

            if (!GameServerSettings.IsRealm)
            {
                return;
            }

            foreach (var server in sharedRealmServers.Values)
            {
                var serverNpc = RoleManager.GetRole<BaseNpc>(x => x is BaseNpc npc && npc.Type == BaseNpc.FRONTIER_SERVER_TRANS_NPC && npc.Data0 == server.ServerId);
                if (serverNpc == null)
                {
                    logger.Warning("NPC not found for server {0} {1} for map {2}", server.ServerId, server.ServerName, server.MapId);
                    continue;
                }

                if (serverNpc.MapIdentity == serverNpc.Data1)
                {
                    continue; // already there? why? skip
                }

                if (await serverNpc.ChangePosAsync((uint)serverNpc.Data1, (ushort)serverNpc.Data2, (ushort)serverNpc.Data3))
                {
                    logger.Information("NPC {0} {1} entered realm map {2} {3}", serverNpc.Identity, serverNpc.Name, serverNpc.MapIdentity, serverNpc.Map.Name);
                }
            }
        }

        #endregion

        #region Transfer to

        private static readonly ConcurrentDictionary<uint, CrossPlayerSubmitQueue> crossPlayerQueueDict = new();

        public static Task<bool> ConnectUserToServerAsync(Character user, string serverName)
        {
            var server = sharedRealmServers.Values.FirstOrDefault(x => x.ServerName.Equals(serverName));
            if (!server.Equals(default))
            {
                return ConnectUserToServerAsync(user, server.ServerId);
            }
            logger.Error("Realm {0} not found for user {1} {2}", serverName, user.Identity, user.Name);
            return Task.FromResult(false);
        }

        public static async Task<bool> ConnectUserToServerAsync(Character user, uint serverId)
        {
            if (!GetServer(serverId, out var server))
            {
                return false;
            }

            if (user.IsOSUser()
                && user.ServerID != serverId
                && !server.IsRealm) // cannot travel between servers directly! Origin <-> Realm <-> Target
            {
                logger.Error("User {0} {1} attempted to travel to a invalid realm {2} {3}", user.Identity, user.Name, server.ServerId, server.ServerName);
                return false;
            }

            if (!IsRealmConnected())
            {
                return false;
            }

            if (!IsServerConnected(serverId))
            {
                return false;
            }

#if DEBUG
			//crossPlayerQueueDict.TryRemove(user.Identity, out _);
			if (crossPlayerQueueDict.TryGetValue(user.Identity, out _)) // player already in queue
			{
				return false;
			}
#else
            if (crossPlayerQueueDict.TryGetValue(user.Identity, out _)) // player already in queue
            {
                return false;
            }
#endif

			//3999900000
			CrossPlayerSubmitQueue queue = new()
            {
                SessionId = BitConverter.ToUInt64(RandomNumberGenerator.GetBytes(8)),
                User = user,
                RequestTime = UnixTimestamp.Now,
                TargetServerId = serverId
            };
            crossPlayerQueueDict.TryAdd(user.Identity, queue);

            crossConnectionLogger.Information("User {1} {2} is being transfered to server {3}",
                queue.SessionId, user.Identity, user.Name, server.ServerName);

            await SendOSMsgAsync(new MsgCrossRequestSwitchC
            {
                Data = new RequestSwitchPB
                {
                    AccountId = user.Client.AccountIdentity,
                    ServerId = ServerIdentity,
                    UserId = user.Identity,
                    SessionId = queue.SessionId,
                    NewUserId = user.RealmUserId
                }
            }, serverId); // we wont queue this, receiving the reply with the information is already enough.

            await SendOSMsgAsync(new MsgCrossUserInfoC
            {
                Data = new()
                {
                    UserID = user.Identity,
                    UserName = user.Name,
                    Mate = user.MateName,

                    Lookface = user.Mesh,
                    Hair = user.Hairstyle,

                    Level = user.Level,
                    Experience = user.Experience,
                    Metempsychosis = user.Metempsychosis,

                    Force = user.Strength,
                    Speed = user.Speed,
                    Health = user.Vitality,
                    Spirit = user.Spirit,
                    AttributePoints = user.AttributePoints,

                    Money = user.Silvers,
                    ConquerPoints = user.ConquerPoints,
                    PresentConquerPoints = user.ConquerPointsBound,

                    Profession = user.Profession,
                    FirstProfession = user.FirstProfession,
                    LastProfession = user.PreviousProfession,

                    SessionId = queue.SessionId,
                    VipLevel = (byte)user.VipLevel,

                    RealmUserId = user.RealmUserId,

                    NobilityRank = (byte)(user.NobilityRank),

                    SyndicateId = user.SyndicateIdentity,
                    SyndicateRank = (uint)user.SyndicateRank,
                    SyndicateName = user.SyndicateName,

                    FamilyId = user.FamilyIdentity,
                    FamilyRank = (uint)user.FamilyRank,
                    FamilyName = user.FamilyName,

                    UnionId = user.UnionIdentity,
                    UnionOfficialFlag = (uint)user.UnionOfficialFlag,
                    UnionContributionLevel = user.LeagueContributionLevel,
                    UnionName = user.UnionName,
                    IsKingdom = user.Union?.IsKingdom ?? false,

                    FlowerCharm = user.Flower?.Charm ?? 0,
                    ShowType = user.CurrentLayout,

                    WingId = user.WingId,
                    TitleId = user.TitleId,
                    TitleScore = user.TitleScore
                }
            }, serverId);

            await user.WeaponSkill.SendOSDataAsync(queue.SessionId, serverId);
            await user.MagicData.SendOSDataAsync(queue.SessionId, serverId);

            var itemMsg = new MsgCrossItemInfoC
            {
                Data = new()
                {
                    SessionId = queue.SessionId,
                    Items = new List<CrossItemInfoPB>()
                }
            };

            foreach (var item in user.UserPackage.GetEquipment())
            {
                if (itemMsg.Data.Items.Count >= MsgCrossItemInfoC.MAX_PER_MSG)
                {
                    await SendOSMsgAsync(itemMsg, serverId);
                    itemMsg.Data.Items.Clear();
                }

                CrossItemStatusInfoPB? artifact = null,
                    refinery = null;
                if (item.ItemStatus?.CurrentArtifact != null)
                {
                    artifact = new CrossItemStatusInfoPB
                    {
                        Id = item.ItemStatus.CurrentArtifact.ItemStatus.Id,
                        Data = item.ItemStatus.CurrentArtifact.ItemStatus.Data,
                        ItemId = item.ItemStatus.CurrentArtifact.ItemStatus.ItemId,
                        Level = item.ItemStatus.CurrentArtifact.ItemStatus.Level,
                        Position = item.ItemStatus.CurrentArtifact.ItemStatus.Position,
                        Power1 = item.ItemStatus.CurrentArtifact.ItemStatus.Power1,
                        Power2 = item.ItemStatus.CurrentArtifact.ItemStatus.Power2,
                        RealSeconds = item.ItemStatus.CurrentArtifact.ItemStatus.RealSeconds,
                        Status = item.ItemStatus.CurrentArtifact.ItemStatus.Status
                    };
                }

                if (item.ItemStatus?.CurrentRefinery != null)
                {
                    refinery = new CrossItemStatusInfoPB
                    {
                        Id = item.ItemStatus.CurrentRefinery.ItemStatus.Id,
                        Data = item.ItemStatus.CurrentRefinery.ItemStatus.Data,
                        ItemId = item.ItemStatus.CurrentRefinery.ItemStatus.ItemId,
                        Level = item.ItemStatus.CurrentRefinery.ItemStatus.Level,
                        Position = item.ItemStatus.CurrentRefinery.ItemStatus.Position,
                        Power1 = item.ItemStatus.CurrentRefinery.ItemStatus.Power1,
                        Power2 = item.ItemStatus.CurrentRefinery.ItemStatus.Power2,
                        RealSeconds = item.ItemStatus.CurrentRefinery.ItemStatus.RealSeconds,
                        Status = item.ItemStatus.CurrentRefinery.ItemStatus.Status
                    };
                }

                itemMsg.Data.Items.Add(new CrossItemInfoPB
                {
                    Id = item.Identity,
                    Type = item.Type,
                    Amount = item.Durability,
                    AmountLimit = item.MaximumDurability,
                    Position = (byte)item.Position,
                    Gem1 = (byte)item.SocketOne,
                    Gem2 = (byte)item.SocketTwo,
                    Magic1 = item.Magic1,
                    Magic2 = item.Magic2,
                    Magic3 = item.Plus,
                    Data = item.SocketProgress,
                    ReduceDmg = item.ReduceDamage,
                    AddLife = item.Enchantment,
                    ChkSum = item.CheckSum,
                    Plunder = (ushort)(item.IsSuspicious() ? 1 : 0),
                    Specialflag = (uint)(item.IsLocked() ? 1 : 0),
                    Color = (uint)item.Color,
                    AddlevelExp = item.CompositionProgress,
                    Monopoly = (byte)(item.IsBound ? 3 : 0),
                    AccumulateNum = item.AccumulateNum,
                    Syndicate = item.SyndicateIdentity,
                    DeleteTime = item.DeleteTime,
                    SaveTime = (uint)item.SaveTime,

                    Artifact = artifact,
                    Refinery = refinery
                });
            }

            if (itemMsg.Data.Items.Count > 0)
            {
                await SendOSMsgAsync(itemMsg, serverId);
            }

            await OnTransferOSDataAsync(user, queue.SessionId, serverId);

            await SendOSMsgAsync(new MsgCrossRequestSwitchC
            {
                Data = new RequestSwitchPB
                {
                    AccountId = user.Client.AccountIdentity,
                    ServerId = ServerIdentity,
                    UserId = user.Identity,
                    SessionId = queue.SessionId,
                    NewUserId = user.RealmUserId
                }
            }, serverId); // we wont queue this, receiving the reply with the information is already enough.
            return true;
        }

        public static async Task<bool> ReturnToRealmAsync(Character user)
        {
            if (!user.IsOSUser())
            {
                return false;
            }

            if (GameServerSettings.IsRealm) // already in realm
            {
                return false;
            }

            if (user.OriginServer.ServerId == ServerIdentity) // on origin server, do not return to realm
            {
                return false;
            }

            await DisconnectPlayerAsync(user);
            await SendOSMsgAsync(new MsgCrossRealmActionC
            {
                Data = new()
                {
                    Action = (uint)CrossRealmAction.TransferServer,
                    Data = user.OriginalUserId,
                    Command = user.Identity,
                    Param = RealmIdentity
                }
            }, user.OriginServer.ServerId);
            return true;
        }

        public static async Task<bool> ReturnToServerAsync(Character user)
        {
            if (!user.IsOSUser())
            {
                return false;
            }

            uint serverId = user.OriginServer.ServerId;
            if (serverId == ServerIdentity)
            {
                return false;
            }

            await DisconnectPlayerAsync(user);
            await user.ReturnServerAsync();
            return true;
        }

        #endregion

        #region OS Acceptance

        private static readonly ConcurrentDictionary<ulong, CrossPlayerReceiveQueue> crossReceiveQueue = new();

        public static async Task ReceiveSwitchRequestAsync(RequestSwitchPB switchInfo, TcpServerActor actor)
        {
            if (crossReceiveQueue.TryRemove(switchInfo.SessionId, out var queuedInfo))
            {
                // initialize equipment

                uint mapId = 3935;
                ushort x = 387;
                ushort y = 386;

                if (!GameServerSettings.IsRealm)
                {
                    mapId = 1002;
                    x = 300;
                    y = 278;
                }

                Character user = queuedInfo.User;
                user.MapIdentity = mapId;
                user.X = x;
                user.Y = y;

                user.Life = user.MaxLife;
                user.Mana = user.MaxMana;

                if (!await RoleManager.LoginUserAsync(user, queuedInfo.OriginServerId))
                {
                    logger.Error("Could not login cross user {0} {1} {2}!!!", user.Identity, user.OriginalUserId, user.Name);
                    return;
                }

                // process is complete, authorize cross switch
                await actor.SendAsync(new MsgCrossRequestSwitchExC
                {
                    Data = new CrossRequestSwitchExPB()
                    {
                        AccountId = queuedInfo.AccountId,
                        NewUserId = queuedInfo.User.Identity,
                        UserId = queuedInfo.User.OriginalUserId,
                        ServerId = ServerIdentity,
                        SessionID = queuedInfo.SessionId,
                        TargetMapID = mapId,
                        TargetX = x,
                        TargetY = y
                    }
                });

                GameMap map = MapManager.GetMap(mapId); // mapid is a constant, so it MUST exist
                map.QueueAction(async () =>
                {
                    await user.EnterMapAsync();
                    await user.SendAsync(new MsgAction
                    {
                        Identity = user.Identity,
                        Command = user.Map.MapDoc,
                        X = user.X,
                        Y = user.Y,
                        Action = ActionType.MapTeleport,
                        Direction = (ushort)user.Direction
                    });

                    await user.SetPkModeAsync(PkModeType.CrossServer);
                    await user.SetAttributesAsync(ClientUpdateType.Stamina, 70);
                    await user.SetAttributesAsync(ClientUpdateType.Hitpoints, user.MaxLife);
                    await user.SetAttributesAsync(ClientUpdateType.Mana, user.MaxMana);
                    await user.Screen.SynchroScreenAsync();
                });
                // after this, origin server must remove user from map and request entermap on target server.
                return;
            }

            if (!sharedRealmServers.TryGetValue(switchInfo.ServerId, out var server))
            {
                logger.Warning("Invalid server {0} requesting switch authorization.", switchInfo.ServerId);
                return;
            }

            var queue = new CrossPlayerReceiveQueue
            {
                AccountId = switchInfo.AccountId,
                OriginServerId = switchInfo.ServerId,
                Items = new(),
                RequestTime = UnixTimestamp.Now,
                SessionId = switchInfo.SessionId,
                UserId = switchInfo.UserId
            };

            crossConnectionLogger.Information("[{0}] {1} {2} from {3} {4} is awaiting data to be transfered.", queue.SessionId, queue.AccountId, queue.UserId, server.ServerId, server.ServerName);

            crossReceiveQueue.TryAdd(switchInfo.SessionId, queue);
        }

        public static Task ReceiveUserDataAsync(CrossUserInfoPB userInfo, TcpServerActor actor) // must create a data model for this? grr
        {
            if (!crossReceiveQueue.TryGetValue(userInfo.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for user data", userInfo.SessionId);
                return Task.CompletedTask;
            }

            //crossConnectionLogger.Information("[{0}] {1} {2} {3} from {4} has received user data.", value.SessionId, value.AccountId, value.UserId, userInfo.UserName, value.OriginServerId);

            uint newUserId = userInfo.RealmUserId;
            if (newUserId == 0)
            {
                newUserId = (uint)crossPlayerIds.GetNextIdentity;
            }

            var client = new CrossGameClient(actor);
            client.Character = new Character(client, new DbUser
            {
                AccountIdentity = value.AccountId,
                Identity = newUserId,
                Name = userInfo.UserName,
                Mesh = userInfo.Lookface,
                Level = userInfo.Level,
                Rebirths = userInfo.Metempsychosis,
                Experience = userInfo.Experience,
                Hairstyle = userInfo.Hair,
                Strength = userInfo.Force,
                Agility = userInfo.Speed,
                Vitality = userInfo.Health,
                Spirit = userInfo.Spirit,
                AttributePoints = userInfo.AttributePoints,
                ConquerPoints = userInfo.ConquerPoints,
                ConquerPointsBound = userInfo.PresentConquerPoints,
                Silver = userInfo.Money,
                ShowType = userInfo.ShowType,
                VipValue = userInfo.VipLevel,
                Profession = (byte)userInfo.Profession,
                FirstProfession = (byte)userInfo.FirstProfession,
                PreviousProfession = (byte)userInfo.LastProfession
            })
            {
                OriginalUserId = value.UserId,
                MateName = userInfo.Mate,

                WingId = userInfo.WingId,
                TitleScore = userInfo.TitleScore,
                TitleId = userInfo.TitleId,
            };

            client.Character.SetOSSyndicateData(
                userInfo.SyndicateId,
                (Modules.Systems.Syndicate.ISyndicateMember.SyndicateRank)userInfo.SyndicateRank,
                userInfo.SyndicateName,
                0
                );
            client.Character.SetOSFamilyData(
                userInfo.FamilyId,
                (Modules.Systems.Family.IFamily.FamilyRank)userInfo.FamilyRank,
                userInfo.FamilyName
                );
            client.Character.SetOSUnionData(
                userInfo.UnionId,
                (States.Realm.KingdomOfficial.OfficialPositionFlag)userInfo.UnionOfficialFlag,
                userInfo.UnionName,
                userInfo.IsKingdom
                );

            client.Character.NobilityRank = (Modules.Systems.Nobility.NobilityRank)userInfo.NobilityRank;

            value.User = client.Character;
            return Task.CompletedTask;
        }

        public static async Task ReceiveItemDataAsync(CrossItemInfoListPB itemList, TcpServerActor actor)
        {
            if (!crossReceiveQueue.TryGetValue(itemList.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for item info data", itemList.SessionId);
                return;
            }

            //crossConnectionLogger.Information("[{0}] {1} {2} {3} from {4} has received {5} item data.", value.SessionId, value.AccountId, value.UserId, value.User.Name, value.OriginServerId, itemList.Items.Count);

            foreach (var recvItem in itemList.Items)
            {
                DbItem dbItem = new DbItem
                {
                    Id = recvItem.Id,
                    Type = recvItem.Type,
                    AccumulateNum = recvItem.AccumulateNum,
                    AddlevelExp = recvItem.AddlevelExp,
                    AddLife = recvItem.AddLife,
                    Amount = recvItem.Amount,
                    AmountLimit = recvItem.AmountLimit,
                    ChkSum = recvItem.ChkSum,
                    Color = recvItem.Color,
                    Data = recvItem.Data,
                    DeleteTime = recvItem.DeleteTime,
                    Gem1 = recvItem.Gem1,
                    Gem2 = recvItem.Gem2,
                    Magic1 = recvItem.Magic1,
                    Magic2 = recvItem.Magic2,
                    Magic3 = recvItem.Magic3,
                    Monopoly = recvItem.Monopoly,
                    PlayerId = value.User.Identity,
                    Plunder = recvItem.Plunder,
                    Position = recvItem.Position,
                    ReduceDmg = recvItem.ReduceDmg,
                    SaveTime = recvItem.SaveTime,
                    Specialflag = recvItem.Specialflag,
                    Syndicate = recvItem.Syndicate
                };

                DbItemStatus artifact = null;
                if (recvItem.Artifact != null)
                {
                    var tmpArtifact = recvItem.Artifact.Value;
                    artifact = new DbItemStatus
                    {
                        Id = tmpArtifact.Id,
                        Data = tmpArtifact.Data,
                        ItemId = tmpArtifact.ItemId,
                        Level = tmpArtifact.Level,
                        Position = tmpArtifact.Position,
                        Power1 = tmpArtifact.Power1,
                        Power2 = tmpArtifact.Power2,
                        RealSeconds = tmpArtifact.RealSeconds,
                        Status = tmpArtifact.Status,
                        UserId = value.User.Identity
                    };
                }

                DbItemStatus refinery = null;
                if (recvItem.Refinery != null)
                {
                    var tmpRefinery = recvItem.Refinery.Value;
                    refinery = new DbItemStatus
                    {
                        Id = tmpRefinery.Id,
                        Data = tmpRefinery.Data,
                        ItemId = tmpRefinery.ItemId,
                        Level = tmpRefinery.Level,
                        Position = tmpRefinery.Position,
                        Power1 = tmpRefinery.Power1,
                        Power2 = tmpRefinery.Power2,
                        RealSeconds = tmpRefinery.RealSeconds,
                        Status = tmpRefinery.Status,
                        UserId = value.User.Identity
                    };
                }

                var item = new Item(value.User);
                if (!await item.InitializeOSItemAsync(dbItem, artifact, refinery))
                {
                    logger.Warning("Failed to load item {0} type {1} for user {2} {3} {4}", recvItem.Id, recvItem.Type, value.User.Identity, value.User.OriginalUserId, value.User.Name);
                    continue;
                }

                if (recvItem.Position >= (byte)Item.ItemPosition.EquipmentBegin
                    && recvItem.Position <= (byte)Item.ItemPosition.EquipmentEnd)
                {
                    value.User.UserPackage.EquipOSItem(item);
                }
            }
        }

        public static async Task ReceiveAstProfDataAsync(CrossAstProfListInfoPB info, TcpServerActor actor)
        {
            if (!crossReceiveQueue.TryGetValue(info.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for astprof data", info.SessionId);
                return;
            }

            if (!IsModuleLoaded(ModuleEnum.AstProf))
            {
                return;
            }

            value.User.AstProf = AstProfManager.CreateOSAstProf(value.User);
            value.User.AstProfRanks = info.AstProfRank;
            if (info.List != null)
            {
                await value.User.AstProf.InitializeOSInfoAsync(info.List);
            }
        }

        public static async Task ReceiveFateDataAsync(CrossTrainingVitalityInfoPB info, TcpServerActor actor)
        {
            if (!crossReceiveQueue.TryGetValue(info.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for fate data", info.SessionId);
                return;
            }

            if (!IsModuleLoaded(ModuleEnum.Fate))
            {
                return;
            }

            value.User.Fate = FateManager.CreateOSFate(value.User);
            await value.User.Fate.InitializeOSDataAsync(info);
        }

        public static async Task ReceiveJiangHuDataAsync(CrossOwnKongFuListInfoPB info, TcpServerActor actor)
        {
            if (!crossReceiveQueue.TryGetValue(info.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for own kongfu data", info.SessionId);
                return;
            }

            if (!IsModuleLoaded(ModuleEnum.JiangHu))
            {
                return;
            }

            value.User.JiangHu = JiangHuManager.InitializeOSData(value.User);
            if (info.List != null)
            {
                await value.User.JiangHu.InitializeOSDataAsync(info);
            }
        }

        public static async Task ReceiveNeiGongDataAsync(CrossNeigongInfoPB info, TcpServerActor actor)
        {
            if (!crossReceiveQueue.TryGetValue(info.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for neigong data", info.SessionId);
                return;
            }

            if (!IsModuleLoaded(ModuleEnum.NeiGong))
            {
                return;
            }

            value.User.InnerStrength = NeiGongManager.InitializeOSData(value.User);
            if (info.Secrets != null)
            {
                await value.User.InnerStrength.InitializeOSDataAsync(info);
            }
        }

        public static Task ReceiveWeaponSkillUserDataAsync(CrossWeaponSkillInfoListPB info, TcpServerActor actor)
        {
            if (!crossReceiveQueue.TryGetValue(info.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for weapon skill user data", info.SessionId);
                return Task.CompletedTask;
            }

            if (info.Infos != null)
            {
                foreach (var dbws in info.Infos)
                {
                    value.User.WeaponSkill.AddOSData((ushort)dbws.Type, dbws.Level);
                }
            }
            return Task.CompletedTask;
        }

        public static Task ReceiveMagicUserDataAsync(CrossMagicListInfoPB info, TcpServerActor actor)
        {
            if (!crossReceiveQueue.TryGetValue(info.SessionId, out var value))
            {
                logger.Warning("Invalid session id {0} for weapon skill user data", info.SessionId);
                return Task.CompletedTask;
            }

            if (info.MagicList != null)
            {
                foreach (var mgc in info.MagicList)
                {
                    value.User.MagicData.AddOSMagic(mgc);
                }
            }
            return Task.CompletedTask;
        }

        #endregion

        #region Cross Packet processing

        public static async Task ProcessUserPacketAsync(uint userId, byte[] packet)
        {
            var length = BitConverter.ToUInt16(packet, 0);
            var type = (PacketType)BitConverter.ToUInt16(packet, 2);

            Character user = RoleManager.GetUser(userId);
            if (user == null)
            {
#if DEBUG
                logger.Debug("Invalid user {0} for packet {1} from another server.", userId, type);
#endif
                return;
            }

            MsgBase<GameClient> msg;
            switch (type)
            {
                case PacketType.MsgWalk: msg = new MsgWalk(); break;
                case PacketType.MsgAction: msg = new MsgAction(); break;
                case PacketType.MsgItem: msg = new MsgItem(); break;
                case PacketType.MsgName: msg = new MsgName(); break;
                case PacketType.MsgTalk: msg = new MsgTalk(); break;
                case PacketType.MsgNpc: msg = new MsgNpc(); break;
                case PacketType.MsgNpcInfo: msg = new MsgNpcInfo(); break;
                case PacketType.MsgTaskDialog: msg = new MsgTaskDialog(); break;
                case PacketType.MsgPlayerAttribInfo: msg = new MsgPlayerAttribInfo(); break;
                default:
                    {
                        logger.Error("RealmManager->ProcessUserPacketAsync unhandled type {0} length {1}\n" + PacketDump.Hex(packet), type, length);
                        return;
                    }
            }

            if (user.Map == null)
            {
                // must NEVER happen
                logger.Error("User {0} {1} {2} requested cross packet out of a map!!?", user.Identity, user.OriginalUserId, user.Name);
                return;
            }

            msg.Decode(packet);
            user.QueueAction(() => msg.ProcessAsync(user.Client));
        }

        public static async Task ProcessMsgRealmActionAsync(CrossRealmActionPB data, TcpServerActor client)
        {
            switch ((CrossRealmAction)data.Action)
            {
                case CrossRealmAction.Ping:
                    {
                        if (data.Data == 0)
                        {
                            await client.SendAsync(new MsgCrossRealmActionS
                            {
                                Data = new CrossRealmActionPB()
                                {
                                    Action = (uint)CrossRealmAction.Ping,
                                    Command = data.Command,
                                    Data = (uint)UnixTimestamp.Now
                                }
                            });
                            return;
                        }

                        if (data.Data != 0)
                        {
                            // hmm do nothing for now
                        }
                        break;
                    }

                case CrossRealmAction.KickoutPlayer:
                    {
                        Character user = RoleManager.GetUser(data.Command);
                        if (user == null)
                        {
                            user = RoleManager.GetUser(data.Data);
                            if (user == null)
                            {
                                return;
                            }
                        }

                        if (user.OriginServer.ServerId == ServerIdentity)
                        {
                            await RoleManager.KickOutAsync(user.Identity, data.Strings.FirstOrDefault());
                            return;
                        }

                        await DisconnectPlayerAsync(user);
                        break;
                    }

                case CrossRealmAction.ReturnPlayer:
                    {
                        Character user = RoleManager.GetUser(data.Command);
                        if (user == null)
                        {
                            user = RoleManager.GetUser(data.Data);
                            if (user == null)
                            {
                                return;
                            }
                        }

                        if (user.CurrentServerID == ServerIdentity)
                        {
                            return; // already in this server?
                        }

                        logger.Information("User {0} has returned from the realm.", user.Name);
                        await user.ReturnServerAsync();
                        break;
                    }

                case CrossRealmAction.TransferServer:
                    {
                        /*
                         *  await SendOSMsgAsync(new MsgCrossRealmActionC
                            {
                                Data = new()
                                {
                                    Action = (uint)CrossRealmAction.TransferServer,
                                    Data = user.OriginalUserId,
                                    Command = user.Identity,
                                    Param = RealmIdentity
                                }
                            }, user.OriginServer.ServerId);
                         */

                        Character user = RoleManager.GetUser(data.Data);
                        if (user == null)
                        {
                            // hummm
                            return;
                        }

                        if (!IsServerConnected(data.Param) || !GetServer(data.Param, out var server))
                        {
                            // hmmmmmmmmm
                            return;
                        }

                        logger.Information("User {0} {1} has requested transfer from {2} to {3}.", user.Identity, user.Name, user.CurrentServerName, server.ServerName);

                        await ConnectUserToServerAsync(user, server.ServerId);
                        break;
                    }
            }
        }

        private static Task DisconnectPlayerAsync(Character user)
        {
            logger.Information("{0} from {1} has disconnected.", user.Name, user.OriginServer.ServerName);
            user.QueueAction(user.LeaveMapAsync);
            RoleManager.ForceLogoutUser(user.Identity);
            crossPlayerIds.ReturnIdentity(user.Identity);
            return Task.CompletedTask;
        }

        #endregion

        #region Realm checks

        public static bool GetServer(uint serverId, out ServerConfig config)
        {
            return sharedRealmServers.TryGetValue(serverId, out config);
        }

        public static bool GetServer(string serverName, out ServerConfig config)
        {
            config = default;
            var server = sharedRealmServers.Values.FirstOrDefault(x => x.ServerName.Equals(serverName));
            if (!server.Equals(default))
            {
                config = server;
                return true;
            }
            return false;
        }

        private static bool isAccountServerUp = false;

        public static void SetAccountServerStatus(bool online)
        {
            isAccountServerUp = online;
        }

        public static bool IsAccountServerConnected()
        {
            return isAccountServerUp;
        }

        public static bool IsRealmConnected()
        {
            return RealmConnectionManager.RealmSession?.Actor?.Socket.Connected == true;
        }

        public static bool IsServerConnected(uint serverId)
        {
            if (RealmIdentity == serverId)
            {
                return IsRealmConnected();
            }
            return RealmConnectionManager.IsServerConnected(serverId);
        }

        public static bool IsServerConnected(string serverName)
        {
            if (GetServer(serverName, out var config))
            {
                return IsServerConnected(config.ServerId);
            }
            return false;
        }

        #endregion

        #region Timer

        public static async Task OnTimerAsync()
        {
            foreach (var req in crossPlayerQueueDict.Values)
            {
                int deltaTime = UnixTimestamp.Now - req.RequestTime;
                if (deltaTime > 30)
                {
                    crossPlayerQueueDict.TryRemove(req.User.Identity, out _);
                }
            }

            foreach (var req in crossReceiveQueue.Values)
            {
                int deltaTime = UnixTimestamp.Now - req.RequestTime;
                if (deltaTime > 30)
                {
                    crossReceiveQueue.TryRemove(req.SessionId, out _);
                }
            }
        }

        #endregion

        public static Task SubmitServerListAsync(Character user)
        {
            MsgSameGroupServerList msg = new MsgSameGroupServerList
            {
                Data = new MsgSameGroupServerList.MsgSameGroupServerListPB
                {
                    Servers = sharedRealmServers.Values.OrderBy(x => x.ServerId).Select(x => new MsgSameGroupServerList.ServerDetailPB
                    {
                        ServerIdentity = x.ServerId,
                        Attribute = x.Attribute,
                        MapID = x.MapId,
                        Name = x.ServerName,
                        X = x.PosX,
                        Y = x.PosY
                    }).ToArray()
                }
            };
            return user.SendAsync(msg);
        }

        public struct ServerConfig
        {
            public uint ServerId { get; set; }
            public string ServerName { get; set; }
            public uint MapId { get; set; }
            public uint PosX { get; set; }
            public uint PosY { get; set; }
            public uint NpcId { get; set; }
            public readonly bool IsRealm => (Attribute & 0x1) == 0x1;
            public uint Attribute { get; set; }
        }

        private struct CrossPlayerSubmitQueue
        {
            public ulong SessionId { get; set; }
            public uint TargetServerId { get; set; }
            public Character User { get; set; }
            public int RequestTime { get; set; }
        }

        private class CrossPlayerReceiveQueue
        {
            public ulong SessionId { get; set; }
            public uint OriginServerId { get; set; }
            public uint AccountId { get; set; }
            public uint UserId { get; set; }
            public Character User { get; set; }
            public int RequestTime { get; set; }
            public List<Item> Items { get; set; }
        }
    }
}
