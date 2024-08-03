using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.Items;
using Long.Kernel.States.World;
using Long.Network.Packets;
using Long.Shared.Mathematics;
using Long.World.Map;
using Long.Kernel.States.Status;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgInteract;
using static Long.Kernel.States.Items.Item;
using static Long.Kernel.Network.Game.Packets.MsgGodExp;
using Microsoft.EntityFrameworkCore;
using Long.Kernel.States.Mails;
using Long.Kernel.Processors;
using Long.Shared.Helpers;
using Long.Kernel.Network.Cross;
using Long.Kernel.Network.Cross.Server.Packets;
using Long.Network.Packets.Cross;
using Long.Kernel.Modules.Systems.AstProf;
using Long.Kernel.States.Storage;
using System.Data;
using Long.Network.Packets.Ai;
using Long.Game.Network.Ai.Packets;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Piglet;
using Long.Kernel.Network.Piglet.Packets;
using static Long.Kernel.Network.Game.Packets.MsgHangUp;
using static Long.Kernel.States.Magics.MagicData;
using SharpCompress.Common;
using System.Numerics;

namespace Long.Kernel.States.User
{
    public partial class Character : Role
    {
        private static readonly ILogger logger = Log.ForContext<Character>();
        private readonly DbUser user;

        private readonly TimeOut dateSyncTimer = new();
        private readonly TimeOut autoHealTimer = new(AUTOHEALLIFE_TIME);
		private readonly TimeOut miningTimer = new();

		public Character(GameClient client, DbUser user)
        {			
			this.user = user;
            Client = client;

            mesh = user.Mesh;
            currentX = user.X;
            currentY = user.Y;
            idMap = user.MapID;

            Screen = new Screen(this);
            UserPackage = new UserPackage(this);
            WeaponSkill = new WeaponSkill(this);
            StatusSet = new StatusSet(this);
            Achievements = new Achievements(this);
            MailBox = new MailBox(this);
            StageGoal = new ProcessGoal(this);
            SignIn = new DailySignIn(this);
            CoatStorage = new CoatStorage(this);
            TitleStorage = new TitleStorage(this);
			PkStatistic = new PkStatistic(this);


			energyTimer.Update();
            autoHealTimer.Update();
            pkDecreaseTimer.Update();
            xpPointsTimer.Update();
            dateSyncTimer.Startup(30);
			//ReallyRevive(true,false);
		}

        public GameClient Client { get; }
        public Screen Screen { get; }

		public override bool IsBowman => RightHand?.IsBow() == true;
		public bool IsAssassin => RightHand?.IsAssassinKnife() == true;

		#region Identity

		public override uint Identity
        {
            get => user.Identity;
        }

        public override string Name
        {
            get => user.Name;
        }

        public void ChangeName(string newName)
        {
            user.Name = newName;
        }

        public string MateName { get; set; } = StrNone;

        public uint MateIdentity
        {
            get => user.Mate;
            set => user.Mate = value;
        }

		public uint WindWalker
		{
			get => user.Flag;
			set => user.Flag = value;
		}

		public PrivilegeFlag Flag
		{
			get => (PrivilegeFlag)user.Flag;
			set => user.Flag = (uint)value;
		}

		#endregion

		#region Authority

		public bool IsPm()
        {
            return Name.Contains("[PM]");
        }

        public bool IsGm()
        {
            return IsPm() || Name.Contains("[GM]");
        }

        #endregion

        #region Appearence

        private uint mesh;
        private ushort transformationMesh;

        public int Gender => Body == BodyType.AgileMale || Body == BodyType.MuscularMale ? 1 : 2;

        public ushort TransformationMesh
        {
            get => transformationMesh;
            set
            {
                transformationMesh = value;
                Mesh = (uint)((uint)value * 10000000 + Avatar * 10000 + (uint)Body);
            }
        }

        public override uint Mesh
        {
            get => mesh;
            set
            {
                mesh = value;
                user.Mesh = value % 10000000;
            }
        }

        public BodyType Body
        {
            get => (BodyType)(Mesh % 10000);
            set => Mesh = (uint)value + Avatar * 10000u;
        }

        public ushort Avatar
        {
            get => (ushort)(Mesh % 10000000 / 10000);
            set => Mesh = (uint)(value * 10000 + (int)Body);
        }

        public ushort Hairstyle
        {
            get => (ushort)(ProfessionSort == 6 && Gender == 1 ? 0 : user.Hairstyle);
            set => user.Hairstyle = value;
        }

        #endregion

        #region Profession

        public byte ProfessionSort => (byte)(Profession / 10);

        public byte ProfessionLevel => (byte)(Profession % 10);

        public byte Profession
        {
            get => user?.Profession ?? 0;
            set => user.Profession = value;
        }

        public byte PreviousProfession
        {
            get => user?.PreviousProfession ?? 0;
            set => user.PreviousProfession = value;
        }

        public byte FirstProfession
        {
            get => user?.FirstProfession ?? 0;
            set => user.FirstProfession = value;
        }

		#endregion

		#region Pk Statistic

		public PkStatistic PkStatistic { get; init; }

		#endregion

		#region Level and Experience
		public bool AutoAllot
		{
			get => user.AutoAllot != 0;
			set => user.AutoAllot = (byte)(value ? 1 : 0);
		}

		public override byte Level
		{
			get => user?.Level ?? 0;
			set => user.Level = Math.Min(MAX_UPLEV, Math.Max((byte)1, value));
		}

		public ulong Experience
		{
			get => user?.Experience ?? 0;
			set
			{
				if (Level >= MAX_UPLEV)
				{
					return;
				}

				user.Experience = value;
			}
		}

		public ulong AutoHangUpExperience
		{
			get;
			set;
		}

		public byte Metempsychosis
		{
			get => user?.Rebirths ?? 0;
			set => user.Rebirths = value;
		}

		public bool IsAutoHangUp { get; set; }
        #endregion

		#region Attribute Points

		public ushort Strength
        {
            get => user?.Strength ?? 0;
            set => user.Strength = value;
        }

        public ushort Speed
        {
            get => user?.Agility ?? 0;
            set => user.Agility = value;
        }

        public ushort Vitality
        {
            get => user?.Vitality ?? 0;
            set => user.Vitality = value;
        }

        public ushort Spirit
        {
            get => user?.Spirit ?? 0;
            set => user.Spirit = value;
        }

        public ushort AttributePoints
        {
            get => user?.AttributePoints ?? 0;
            set => user.AttributePoints = value;
        }

        public int TotalAttributePoints => Strength + Speed + Vitality + Spirit + AttributePoints;

        #endregion

        #region Life and Mana

        public override uint Life
        {
            get => user.HealthPoints;
            set => user.HealthPoints = Math.Min(MaxLife, value);
        }

        public override uint MaxLife
        {
            get
            {
                var result = (uint)(Vitality * 24);
                result += (uint)((Strength + Speed + Spirit) * 3);
                switch (Profession)
                {
                    case 11:
                        result = (uint)(result * 1.05d);
                        break;
                    case 12:
                        result = (uint)(result * 1.08d);
                        break;
                    case 13:
                        result = (uint)(result * 1.10d);
                        break;
                    case 14:
                        result = (uint)(result * 1.12d);
                        break;
                    case 15:
                        result = (uint)(result * 1.15d);
                        break;
                }
                for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                {
                    result += (uint)(UserPackage.GetEquipment(pos)?.Life ?? 0);
                }
                result += (uint)CoatStorage.MaxLife;
                result += (uint)(AstProf?.GetPower(IAstProf.AstProfType.Wrangler) ?? 0);
                result += (uint)(Fate?.HealthPoints ?? 0);
                IStatus status = QueryStatus(StatusSet.BUFF_MAX_HEALTH + 1);
                if (status != null)
                {
                    result += (uint)status.Power;
                }
				result += (uint)(JiangHu?.MaxLife??0);
				return result;
            }
        }

        public override uint Mana
        {
            get => user.ManaPoints;
            set => user.ManaPoints = (ushort)Math.Min(MaxMana, value);
        }

        public override uint MaxMana
        {
            get
            {
				byte ManaBoost = 5;

				sbyte Class = (sbyte)(Profession / 10);
				if (Class == 13 || Class == 14)
					ManaBoost += (byte)(5 * (Class - (Profession * 10)));

				var result = (uint)(Spirit * ManaBoost);

                for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                {
                    var a = UserPackage.GetEquipment(pos);
					result += (uint)(UserPackage.GetEquipment(pos)?.Mana ?? 0);
				}
				result += (uint)(JiangHu?.MaxMana??0);
				return result;
			}
		}



		#endregion
		#region Currency

        public ulong Silvers
        {
            get => user?.Silver ?? 0;
            set => user.Silver = value;
        }

        public uint ConquerPoints
        {
            get => user?.ConquerPoints ?? 0;
            set => user.ConquerPoints = value;
        }

        public uint ConquerPointsBound
        {
            get => user?.ConquerPointsBound ?? 0;
            set => user.ConquerPointsBound = value;
        }

        public uint StorageMoney
        {
            get => user?.StorageMoney ?? 0;
            set => user.StorageMoney = value;
        }

        public uint StudyPoints
        {
            get => user?.Cultivation ?? 0;
            set => user.Cultivation = value;
        }

        public uint ChiPoints
        {
            get => user?.StrengthValue ?? 0;
            set => user.StrengthValue = value;
        }

        public uint HorseRacingPoints
        {
            get => user?.RidePetPoint ?? 0;
            set => user.RidePetPoint = value;
        }

        public async Task<bool> ChangeMoneyAsync(long amount, bool notify = false)
        {
            if (amount > 0)
            {
                await AwardMoneyAsync(amount);
                return true;
            }
            else if (amount < 0)
            {
                return await SpendMoneyAsync(amount * -1, notify);
            }
            return false;
        }

        public async Task AwardMoneyAsync(long amount)
        {
            if (amount <= 0)
            {
                logger.Warning("Invalid award money amount {0} for user {1} {2}", amount, Identity, Name);
                return;
            }

            Silvers = Math.Min(MAX_INVENTORY_MONEY, Silvers + (ulong)amount);
            await SynchroAttributesAsync(ClientUpdateType.Money, Silvers);
            await SaveAsync();
        }

        public async Task<bool> SpendMoneyAsync(long amount, bool notify = false)
        {
            if (amount <= 0)
            {
                logger.Warning("Invalid spend money amount {0} for user {1} {2}", amount, Identity, Name);
                return false;
            }

            if ((ulong)amount > Silvers)
            {
                if (notify)
                {
                    await SendAsync(StrNotEnoughMoney);
                }
                return false;
            }

            Silvers = (ulong)Math.Max(0, (long)Silvers - amount);
            await SynchroAttributesAsync(ClientUpdateType.Money, Silvers);
            await SaveAsync();
            return true;
        }

        public async Task<bool> ChangeConquerPointsAsync(int amount, bool notify = false)
        {
            if (amount == 0)
            {
                return false;
            }
            if (amount > 0)
            {
                await AwardConquerPointsAsync(amount);
                return true;
            }
            return await SpendConquerPointsAsync(amount * -1, notify);
        }

        public async Task AwardConquerPointsAsync(int amount)
        {
            if (amount <= 0)
            {
                logger.Warning("Invalid award emoney amount {0} for user {1} {2}", amount, Identity, Name);
                return;
            }

            ConquerPoints = (uint)Math.Min(MAX_INVENTORY_EMONEY, ConquerPoints + amount);
            await SynchroAttributesAsync(ClientUpdateType.ConquerPoints, ConquerPoints);
        }

        public async Task<bool> SpendConquerPointsAsync(int amount, bool notify = false)
        {
            if (amount <= 0)
            {
                logger.Warning("Invalid spend emoney amount {0} for user {1} {2}", amount, Identity, Name);
                return false;
            }

            if (amount > ConquerPoints)
            {
                if (notify)
                {
                    await SendAsync(StrNotEnoughEmoney);
                }
                return false;
            }

            ConquerPoints = (uint)Math.Max(0, ConquerPoints - amount);
            await SynchroAttributesAsync(ClientUpdateType.ConquerPoints, ConquerPoints);
            return true;
        }
		public async Task<bool> SpendConquerPointsAsync(int amount, bool bound, bool notify)
		{
			if (!bound || ConquerPointsBound == 0)
			{
				return await SpendConquerPointsAsync(amount, notify);
			}

			if (amount > ConquerPoints + ConquerPointsBound)
			{
				if (notify)
				{
					await SendAsync(StrNotEnoughEmoney, TalkChannel.TopLeft, Color.Red);
				}

				return false;
			}

			if (ConquerPointsBound > amount)
			{
				return await SpendBoundConquerPointsAsync(amount, notify);
			}

			int remain = (int)(amount - ConquerPointsBound);
			await SpendBoundConquerPointsAsync((int)ConquerPointsBound);
			await SpendConquerPointsAsync(remain);
			return true;
		}
		public async Task SaveEmoneyLogAsync(EmoneyOperationType type, uint target, uint targetBalance, long amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                amount *= -1;
            }

            uint timestamp = (uint)UnixTimestamp.Now;
            uint checkSum = CalculateEmoneyCheckSum(ConquerPoints, Identity);
            user.ConquerPointsCheckSum = checkSum;
            using var context = new ServerDbContext();
            context.Users.Update(user);
            context.EMoneyLogs.Add(new DbEMoney
            {
                IdSource = Identity,
                IdTarget = target,
                Number = (uint)amount,
                ChkSum = checkSum,
                TimeStamp = timestamp,
                Type = (byte)type,
                TargetBalance = targetBalance,
                SourceBalance = ConquerPoints
            });
            await context.SaveChangesAsync();
        }

        public async Task<bool> ChangeBoundConquerPointsAsync(int amount, bool notify = false)
        {
            if (amount == 0)
            {
                return false;
            }
            if (amount > 0)
            {
                await AwardBoundConquerPointsAsync(amount);
                return true;
            }
            return await SpendBoundConquerPointsAsync(amount * -1, notify);
        }

        public async Task AwardBoundConquerPointsAsync(int amount)
        {
            if (amount <= 0)
            {
                logger.Warning("Invalid award emoney(B) amount {0} for user {1} {2}", amount, Identity, Name);
                return;
            }

            ConquerPointsBound = (uint)Math.Min(MAX_INVENTORY_EMONEY, ConquerPointsBound + amount);
            await SynchroAttributesAsync(ClientUpdateType.BoundConquerPoints, ConquerPointsBound);
        }

        public async Task<bool> SpendBoundConquerPointsAsync(int amount, bool notify = false)
        {
            if (amount <= 0)
            {
                logger.Warning("Invalid spend emoney(B) amount {0} for user {1} {2}", amount, Identity, Name);
                return false;
            }

            if (amount > ConquerPointsBound)
            {
                if (notify)
                {
                    await SendAsync(StrNotEnoughEmoneyMono);
                }
                return false;
            }

            ConquerPointsBound = (uint)Math.Max(0, ConquerPointsBound - amount);
            await SynchroAttributesAsync(ClientUpdateType.BoundConquerPoints, ConquerPointsBound);
            return true;
        }

        public async Task<bool> SpendBoundConquerPointsAsync(EmoneyOperationType type, int amount, bool notify = false)
        {
            if (amount <= 0)
            {
                logger.Warning("Invalid spend emoney(B) amount {0} for user {1} {2}", amount, Identity, Name);
                return false;
            }

            int totalConquerPoints = (int)(ConquerPoints + ConquerPointsBound);
            if (amount > ConquerPointsBound)
            {
                if (amount > totalConquerPoints)
                {
                    if (notify)
                    {
                        await SendAsync(StrNotEnoughEmoneyMono);
                    }
                    return false;
                }

                int spendFromEmoneyMono = (int)ConquerPointsBound;
                int spendFromEmoney = amount - (int)ConquerPointsBound;
                await SpendBoundConquerPointsAsync((int)ConquerPointsBound);
                await SpendConquerPointsAsync(spendFromEmoney);

                await SaveEmoneyMonoLogAsync(type, 0, 0, (uint)spendFromEmoneyMono);
                await SaveEmoneyLogAsync(type, 0, 0, (uint)spendFromEmoney);
                return true;
            }

            if (!await SpendBoundConquerPointsAsync(amount, notify))
            {
                return false;
            }

            await SaveEmoneyMonoLogAsync(type, 0, 0, (uint)amount);
            return true;
        }

        public async Task SaveEmoneyMonoLogAsync(EmoneyOperationType type, uint target, uint targetBalance, long amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                amount *= -1;
            }

            uint timestamp = (uint)UnixTimestamp.Now;
            uint checkSum = CalculateEmoneyCheckSum(ConquerPointsBound, Identity);
            using var context = new ServerDbContext();
            context.Users.Update(user);
            context.EMoneyMonoLogs.Add(new DbEMoneyMono
            {
                IdSource = Identity,
                IdTarget = target,
                Number = (uint)amount,
                ChkSum = checkSum,
                TimeStamp = timestamp,
                Type = (byte)type,
                TargetBalance = targetBalance,
                SourceBalance = ConquerPointsBound
            });
            await context.SaveChangesAsync();
        }

        public static uint CalculateEmoneyCheckSum(uint value, uint identity)
        {
            value ^= identity;
            value = (value + 0x7ed55d16) + (value << 12);
            value = (value ^ 0xc761c23c) ^ (value >> 19);
            value = (value + 0x165667b1) + (value << 5);
            value = (value + 0xd3a2646c) ^ (value << 9);
            value = (value + 0xfd7046c5) + (value << 3);
            value = (value ^ 0xb55a4f09) ^ (value >> 16);
            return value;
        }

        public async Task<bool> ChangeStrengthValueAsync(int amount)
        {
            if (amount > 0)
            {
                await AwardStrengthValueAsync(amount);
                return true;
            }
            if (amount < 0)
            {
                return await SpendStrengthValueAsync(amount * -1);
            }
            return false;
        }

        public async Task AwardStrengthValueAsync(int amount)
        {
            ChiPoints = (uint)(ChiPoints + amount);
            if (Fate != null)
            {
                await Fate.SendAsync(true);
            }
            await SaveAsync();
        }

        public async Task<bool> SpendStrengthValueAsync(int amount, bool sync = true)
        {
            if (amount > ChiPoints)
            {
                return false;
            }
            ChiPoints = (uint)(ChiPoints - amount);
            if (Fate != null && sync)
            {
                await Fate.SendAsync(true);
            }
            await SaveAsync();
            return true;
        }

        public async Task<bool> ChangeHorseRacePointsAsync(int amount)
        {
            if (amount > 0)
            {
                await AwardHorseRacePointsAsync(amount);
                return true;
            }
            if (amount < 0)
            {
                return await SpendHorseRacePointsAsync(amount * -1);
            }
            return false;
        }

        public async Task AwardHorseRacePointsAsync(int amount)
        {
            HorseRacingPoints = (uint)(HorseRacingPoints + amount);
            await SynchroAttributesAsync(ClientUpdateType.RidePetPoint, HorseRacingPoints);
            await SaveAsync();
        }

        public async Task<bool> SpendHorseRacePointsAsync(int amount)
        {
            if (amount > HorseRacingPoints)
            {
                return false;
            }
            HorseRacingPoints = (uint)(HorseRacingPoints - amount);
            await SynchroAttributesAsync(ClientUpdateType.RidePetPoint, HorseRacingPoints);
            await SaveAsync();
            return true;
        }

        #endregion

        #region PK

        private readonly TimeOut pkDecreaseTimer = new(PK_DEC_TIME);

        public PkModeType PkMode { get; set; }

        public ushort PkPoints
        {
            get => user?.KillPoints ?? 0;
            set => user.KillPoints = value;
        }

        public async Task SetPkModeAsync(PkModeType mode)
        {
            if (PkMode == PkModeType.JiangHu)
            {
                await JiangHu.ExitJiangHuAsync();
            }

            PkMode = mode;

            if (PkMode == PkModeType.JiangHu)
            {
                await JiangHu.SendStatusAsync();
            }

            await SendAsync(new MsgAction
            {
                Identity = Identity,
                Action = ActionType.CharacterPkMode,
                Command = (uint)PkMode
            });

            switch (PkMode)
            {
                case PkModeType.FreePk:
                    await SendAsync(StrFreePkMode);
                    break;
                case PkModeType.Peace:
                    await SendAsync(StrSafePkMode);
                    break;
                case PkModeType.Team:
                    await SendAsync(StrTeamPkMode);
                    break;
                case PkModeType.Capture:
                    await SendAsync(StrArrestmentPkMode);
                    break;
                case PkModeType.Revenge:
                    await SendAsync(StrRevengePkMode);
                    break;
                case PkModeType.Syndicate:
                    await SendAsync(StrSyndicatePkMode);
                    break;
                case PkModeType.JiangHu:
                    await SendAsync(StrJiangHuPkMode);
                    break;
                case PkModeType.CrossServer:
                    await SendAsync(StrOsPkMode);
                    break;
                case PkModeType.Union:
                    await SendAsync(StrLeaguePkMode);
                    break;
            }
        }

        #endregion

        #region Position

        /// <summary>
        ///     The current map identity for the role.
        /// </summary>
        public override uint MapIdentity
        {
            get => idMap;
            set => idMap = value;
        }

        /// <summary>
        ///     Current X position of the user in the map.
        /// </summary>
        public override ushort X
        {
            get => currentX;
            set => currentX = value;
        }

        /// <summary>
        ///     Current X position of the user in the map.
        /// </summary>
        public override ushort Y
        {
            get => currentY;
            set => currentY = value;
        }

        public uint RecordMapIdentity
        {
            get => user.MapID;
            set => user.MapID = value;
        }

        public ushort RecordMapX
        {
            get => user.X;
            set => user.X = value;
        }

        public ushort RecordMapY
        {
            get => user.Y;
            set => user.Y = value;
        }

        public override async Task EnterMapAsync()
        {
            Map = MapManager.GetMap(idMap);
            if (Map != null)
            {
                await Map.AddAsync(this);
                await Map.SendMapInfoAsync(this);
                await ProcessAfterMoveAsync();
                MsgAiAction action = new MsgAiAction
                {
                    Data = new MsgAiActionContract
                    {
                        Action = AiActionType.FlyMap,
                        Identity = Identity,
                        TargetIdentity = idMap,
                        X = X,
                        Y = Y
                    }
                };

                NpcServer.Instance.Send(NpcServer.NpcClient, action.Encode());
				await OnEnterMapAsync(this, Map);
            }
        }

        public override async Task LeaveMapAsync()
        {
            if (Map != null)
            {
                await ProcessOnMoveAsync();
                await OnLeaveMapAsync(this, Map);
                await Map.RemoveAsync(Identity);
				MsgAiAction action = new MsgAiAction
				{
					Data = new MsgAiActionContract
					{
						Action = AiActionType.FlyMap,
						Identity = Identity,
						TargetIdentity = idMap,
						X = X,
						Y = Y
					}
				};
				NpcServer.Instance.Send(NpcServer.NpcClient, action.Encode());
			}

            await Screen.ClearAsync();
        }

        public async Task SavePositionAsync(uint idMap, ushort x, ushort y)
        {
            GameMap map = MapManager.GetMap(idMap);
            if (map?.IsRecordDisable() == false)
            {
                user.X = x;
                user.Y = y;
                user.MapID = idMap;
                await SaveAsync();
            }
        }

        public async Task<bool> FlyMapAsync(uint idMap, int x, int y)
        {
            if (Map == null)
            {
                logger.Warning("FlyMap user [{Identity}] not in map", Identity);
                return false;
            }

            if (idMap == 0)
            {
                idMap = MapIdentity;
            }

            GameMap newMap = MapManager.GetMap(idMap);
            if (newMap == null || !newMap.IsValidPoint(x, y))
            {
                logger.Error("FlyMap user fly invalid position {idMap}[{x},{y}]", idMap, x, y);
                return false;
            }

            if (!newMap.IsStandEnable(x, y))
            {
                bool succ = false;
                for (int i = 0; i < 8; i++)
                {
                    int testX = x + GameMapData.WalkXCoords[i];
                    int testY = y + GameMapData.WalkYCoords[i];

                    if (newMap.IsStandEnable(testX, testY))
                    {
                        x = testX;
                        y = testY;
                        succ = true;
                        break;
                    }
                }

                if (!succ)
                {
                    newMap = MapManager.GetMap(1002);
                    x = 300;
                    y = 278;
                }
            }

            try
            {
                await LeaveMapAsync(); // leave map on current partition

                this.idMap = newMap.Identity;
                X = (ushort)x;
                Y = (ushort)y;

                if (!newMap.IsRecordDisable())
                {
                    await SavePositionAsync(MapIdentity, X, Y);
                }

                await SendAsync(new MsgAction
                {
                    Identity = Identity,
                    Command = newMap.MapDoc,
                    X = X,
                    Y = Y,
                    Action = ActionType.MapTeleport,
                    Direction = (ushort)Direction
                });

                Task characterEnterMapTaskAsync() // this is here just to display the name on processor catch
                {
                    return EnterMapAsync();
                }

                if (newMap.Partition == Map.Partition)
                {
                    await characterEnterMapTaskAsync();
                }
                else
                {
                    QueueAction(characterEnterMapTaskAsync);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Fly map error", ex.Message);
            }
            return true;
        }

        public async Task<bool> SynPositionAsync(ushort x, ushort y, int nMaxDislocation)
        {
            if (nMaxDislocation <= 0 || x == 0 && y == 0) // ignore in this condition
            {
                return true;
            }

            int nDislocation = GetDistance(x, y);
            if (nDislocation >= nMaxDislocation)
            {
                return false;
            }

            if (nDislocation <= 0)
            {
                return true;
            }

            if (IsGm())
            {
                await SendAsync($"syn move: ({X},{Y})->({x},{y})", TalkChannel.Talk, Color.Red);
            }

            if (!Map.IsValidPoint(x, y))
            {
                return false;
            }

            await ProcessOnMoveAsync();
            await JumpPosAsync(x, y);
            await Screen.BroadcastRoomMsgAsync(new MsgAction
            {
                Identity = Identity,
                Action = ActionType.Kickback,
                ArgumentX = x,
                ArgumentY = y,
                Command = (uint)((y << 16) | x),
                Direction = (ushort)Direction
            });

            return true;
        }

        public Task KickbackAsync()
        {
            return SendAsync(new MsgAction
            {
                Identity = Identity,
                Direction = (ushort)Direction,
                Map = MapIdentity,
                X = X,
                Y = Y,
                Action = ActionType.Kickback,
                Timestamp = (uint)Environment.TickCount
            });
        }

        public override async Task ProcessOnMoveAsync()
        {
            if (QueryStatus(StatusSet.LUCKY_DIFFUSE) != null)
            {
                foreach (Character user in Screen.Roles.Values
                                                 .Where(x => x.IsPlayer() &&
                                                             x.QueryStatus(StatusSet.LUCKY_ABSORB)?.CasterId ==
                                                             Identity).Cast<Character>())
                {
                    await user.DetachStatusAsync(StatusSet.LUCKY_DIFFUSE);
                }
            }

            await ExploreFailAsync();

            if (IsAway)
            {
                IsAway = false;

                await BroadcastRoomMsgAsync(new MsgAction
                {
                    Identity = Identity,
                    Action = ActionType.Away
                }, true);
            }

            idLuckyTarget = 0;

            protectionTimer.Clear();

            await base.ProcessOnMoveAsync();
        }

        public override async Task ProcessAfterMoveAsync()
        {
            energyTimer.Startup(ADD_ENERGY_STAND_MS);
            await ExploreFailAsync();

            await base.ProcessAfterMoveAsync();

        }

        public override async Task ProcessOnAttackAsync()
        {
            energyTimer.Startup(ADD_ENERGY_STAND_MS);

            if (AwaitingProgressBar != null && !AwaitingProgressBar.Completed)
            {
                await GameAction.ExecuteActionAsync(AwaitingProgressBar.IdNextFail, this, null, null, string.Empty);
            }

            if (QueryStatus(StatusSet.LUCKY_DIFFUSE) != null)
            {
                foreach (Character user in Screen.Roles.Values
                                                 .Where(x => x.IsPlayer() &&
                                                             x.QueryStatus(StatusSet.LUCKY_ABSORB)?.CasterId ==
                                                             Identity).Cast<Character>())
                {
                    await user.DetachStatusAsync(StatusSet.LUCKY_DIFFUSE);
                }
            }

            if (IsAway)
            {
                IsAway = false;

                await BroadcastRoomMsgAsync(new MsgAction
                {
                    Identity = Identity,
                    Action = ActionType.Away
                }, true);
            }

            protectionTimer.Clear();
            await base.ProcessOnAttackAsync();
        }

		public Role QueryRole(uint idRole)
		{
			return Map.QueryAroundRole(this, idRole);
		}

		#endregion

		#region XP and Stamina

		public int KoCount { get; set; }
        public byte Energy { get; private set; } = DEFAULT_USER_ENERGY;
        public byte MaxEnergy => (byte)(IsBlessed ? 150 : 100);

        private readonly TimeOutMS energyTimer = new(ADD_ENERGY_STAND_MS);
        private readonly TimeOut xpPointsTimer = new(3);

        public byte XpPoints { get; set; }

        public async Task ProcXpValAsync()
        {
            if (!IsAlive)
            {
                await ClsXpValAsync();
                return;
            }

            IStatus pStatus = QueryStatus(StatusSet.START_XP);
            if (pStatus != null)
            {
                return;
            }

            if (XpPoints >= 100)
            {
                await BurstXpAsync();
                await SetXpAsync(0);
                xpPointsTimer.Update();
            }
            else
            {
                if (Map != null && Map.IsBoothEnable())
                {
                    return;
                }

                await AddXpAsync(1);
            }
        }

        public async Task<bool> BurstXpAsync()
        {
            if (XpPoints < 100)
            {
                return false;
            }

            IStatus pStatus = QueryStatus(StatusSet.START_XP);
            if (pStatus != null)
            {
                return true;
            }

            await AttachStatusAsync(this, StatusSet.START_XP, 0, 20, 0);
            return true;
        }

        public async Task SetXpAsync(byte nXp)
        {
            if (nXp > 100)
            {
                return;
            }

            await SetAttributesAsync(ClientUpdateType.XpCircle, nXp);
        }

        public async Task AddXpAsync(byte nXp)
        {
            if (nXp <= 0
                || !IsAlive
                || QueryStatus(StatusSet.START_XP) != null
                || QueryStatus(StatusSet.SHURIKEN_VORTEX) != null
                || QueryStatus(StatusSet.CHAIN_BOLT_ACTIVE) != null
                || QueryStatus(StatusSet.BLADE_FLURRY) != null
                || QueryStatus(StatusSet.BLACK_BEARDS_RAGE) != null
                || QueryStatus(StatusSet.CANNON_BARRAGE) != null
                || QueryStatus(StatusSet.SUPER_CYCLONE) != null)
            {
                return;
            }

            await AddAttributesAsync(ClientUpdateType.XpCircle, nXp);
        }

        public async Task ClsXpValAsync()
        {
            XpPoints = 0;
            await StatusSet.DelObjAsync(StatusSet.START_XP);
        }

        public async Task FinishXpAsync()
        {
            int currentPoints = SupermanManager.GetSupermanPoints(Identity);
            if (KoCount >= 25
                && currentPoints < KoCount)
            {
                await SupermanManager.AddOrUpdateSupermanAsync(Identity, KoCount);
                int rank = SupermanManager.GetSupermanRank(Identity);
                if (rank < 100)
                {
                    await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSupermanBroadcast, Name, KoCount, rank), TalkChannel.Talk, Color.White);
                }
            }
            KoCount = 0;
        }

        #endregion

        #region Vigor

        private readonly TimeOutMS vigorTimer = new(1500);

        public int Vigor { get; set; }

        public int MaxVigor
        {
            get
            {
                int value = 0;
                if (QueryStatus(StatusSet.RIDING) != null)
                {
                    value += UserPackage[ItemPosition.Mount]?.Vigor ?? 0;
                    value += UserPackage[ItemPosition.Crop]?.Vigor ?? 0;
                }
                return value;
            }
        }

        public void UpdateVigorTimer()
        {
            vigorTimer.Update();
        }

        #endregion

        #region VIP

        public uint VipLevel { get; set; }

        public VipFlags UserVipFlag
        {
            get
            {
                return VipLevel switch
                {
                    1 => VipFlags.VipOne,
                    2 => VipFlags.VipTwo,
                    3 => VipFlags.VipThree,
                    4 => VipFlags.VipFour,
                    5 => VipFlags.VipFive,
                    6 => VipFlags.VipSix,
                    _ => 0,
                };
            }
        }

        #endregion

        #region Leave word

        private Dictionary<string, string> awaitingLeaveWords = new();

        public async Task LoadLeaveWordAsync()
        {
            List<DbLeaveword> leavewords = LeavewordRepository.Get(Identity);
            foreach (var leaveword in leavewords)
            {
                await SendAsync(new MsgTalk(TalkChannel.Offline, Color.FromArgb(255, 255, 255, 255), leaveword.Words)
                {
                    RecipientName = Name,
                    SenderMesh = leaveword.Lookface,
                    SenderName = leaveword.SendName,
                    Suffix = leaveword.Time
                });
            }
            await ServerDbContext.DeleteRangeAsync(leavewords);

            List<DbSysLeaveword> sysLeavewords = LeavewordRepository.GetSys(Identity);
            foreach (var leaveword in sysLeavewords)
            {
                await SendAsync(new MsgTalk(TalkChannel.Offline, Color.FromArgb(255, 255, 255, 255), leaveword.Words)
                {
                    RecipientName = Name,
                    SenderMesh = MsgTalk.SystemLookface,
                    SenderName = leaveword.SendName,
                    Suffix = UnixTimestamp.ToDateTime(leaveword.Time).ToString("yyyyMMddHHmmss")
                });
            }
            await ServerDbContext.DeleteRangeAsync(sysLeavewords);
        }

        public string GetPendingLeaveWord(string targetName)
        {
            return awaitingLeaveWords.TryGetValue(targetName, out var result) ? result : string.Empty;
        }

        public async Task ClearLeaveWordAsync(string targetName)
        {
            DbUser targetUser = await UserRepository.FindAsync(targetName);
            if (targetUser == null)
            {
                await SendAsync(new MsgLeaveWord
                {
                    Action = MsgLeaveWord.LeaveWordAction.Error,
                    Data = new()
                    {
                        targetName
                    }
                });
                return;
            }

            List<DbLeaveword> leavewords = LeavewordRepository.GetWords(Name, targetUser.Identity).Where(x => x.RecvId == targetUser.Identity).ToList();
            if (leavewords.Count > 0)
            {
                await ServerDbContext.DeleteRangeAsync(leavewords);
            }
        }

        public async Task LeaveWordAsync(string targetName, string message, bool skip = false)
        {
            DbUser targetUser = await UserRepository.FindAsync(targetName);
            if (targetUser == null)
            {
                await SendAsync(new MsgLeaveWord
                {
                    Action = MsgLeaveWord.LeaveWordAction.Error,
                    Data = new()
                    {
                        targetName
                    }
                });
                return;
            }

            var words = LeavewordRepository.GetWords(Name, targetUser.Identity);
            if (words.Count > 0 && !skip)
            {
                awaitingLeaveWords.Remove(targetName);
                awaitingLeaveWords.Add(targetName, message);

                await SendAsync(new MsgLeaveWord
                {
                    Action = MsgLeaveWord.LeaveWordAction.Replace,
                    Data = new()
                    {
                        targetName
                    }
                });
                return;
            }

            awaitingLeaveWords.Remove(targetName);
            await ServerDbContext.CreateAsync(new DbLeaveword
            {
                RecvId = targetUser.Identity,
                Words = message,
                Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Lookface = Mesh,
                SendName = Name
            });
            await SendAsync(new MsgLeaveWord
            {
                Action = MsgLeaveWord.LeaveWordAction.Submit,
                Data = new()
                {
                    targetName
                }
            });
        }

		#endregion

		#region Offline TG

		public ushort MaxTrainingMinutes => (ushort)Math.Min(1440 + 60 * VipLevel, (UnixTimestamp.ToDateTime(user.HeavenBlessing) - DateTime.Now).TotalMinutes);

		public ushort CurrentTrainingMinutes => (ushort)Math.Min((DateTime.Now - LastLogin).TotalMinutes * 10, MaxTrainingMinutes);

		public ushort CurrentOfflineTrainingTime
		{
			get
			{
				if (user.AutoExercise == 0 || user.LogoutTime2 == 0)
				{
					return 0;
				}

				DateTime endTime = UnixTimestamp.ToDateTime(user.LogoutTime2).AddMinutes(user.AutoExercise);
				if (endTime < DateTime.Now)
				{
					return CurrentTrainingTime;
				}

				var remainingTime = (int)Math.Min((DateTime.Now - UnixTimestamp.ToDateTime(user.LogoutTime2)).TotalMinutes, CurrentTrainingTime);
				return (ushort)remainingTime;
			}
		}

		public ushort CurrentTrainingTime => user.AutoExercise;

		public bool IsOfflineTraining => user.AutoExercise != 0;

		public async Task EnterAutoExerciseAsync()
		{
			if (!IsBlessed)
			{
				return;
			}

			user.AutoExercise = CurrentTrainingMinutes;
			user.LogoutTime2 = (uint)DateTime.Now.ToUnixTimestamp();
			await SaveAsync();
		}

		public async Task LeaveAutoExerciseAsync()
		{
			await AwardExperienceAsync(CalculateExpBall(GetAutoExerciseExpTimes()), true);

			await FlyMapAsync(RecordMapIdentity, RecordMapX, RecordMapY);

			user.AutoExercise = 0;
			user.LogoutTime2 = 0;
			await SaveAsync();
		}

		public int GetAutoExerciseExpTimes()
		{
			const int MAX_REWARD = 3000; // 5 Exp Balls every 8 hours
			const double REWARD_EVERY_N_MINUTES = 480;
			return (int)(Math.Min(CurrentOfflineTrainingTime, CurrentTrainingTime) / REWARD_EVERY_N_MINUTES *
						  MAX_REWARD);
		}

		public ExperiencePreview GetCurrentOnlineTGExp()
		{
			return PreviewExpBallUsage(GetAutoExerciseExpTimes());
		}

		#endregion

		#region Weapon Skill

		public WeaponSkill WeaponSkill { get; init; }

        public async Task AddWeaponSkillExpAsync(ushort type, int experience, bool byAction = false)
        {
            DbWeaponSkill skill = WeaponSkill[type];
            if (skill == null)
            {
                await WeaponSkill.CreateAsync(type, 0);
                if ((skill = WeaponSkill[type]) == null)
                {
                    return;
                }
            }

            if (skill.Level >= MAX_WEAPONSKILLLEVEL)
            {
                return;
            }

            if (skill.Unlearn != 0)
            {
                skill.Unlearn = 0;
            }

            // experience = (int)(experience * (1 + VioletGemBonus / 100d));

            uint increaseLev = 0;
            if (skill.Level > MASTER_WEAPONSKILLLEVEL)
            {
                int ratio = 100 - (skill.Level - MASTER_WEAPONSKILLLEVEL) * 20;
                if (ratio < 10)
                {
                    ratio = 10;
                }

                experience = Calculations.MulDiv(experience, ratio, 100) / 2;
            }

            var newExp = (int)Math.Max(experience + skill.Experience, skill.Experience);

            uint oldPercent = 0;
            int level = skill.Level;
            if (level < MAX_WEAPONSKILLLEVEL)
            {
                if (newExp > skill.WeaponSkillUp.ReqExp || level >= skill.OldLevel / 2 && level < skill.OldLevel)
                {
                    newExp = 0;
                    increaseLev = 1;
                }
                else if (skill.WeaponSkillUp != null && skill.WeaponSkillUp.ReqExp > 0)
                {
                    oldPercent = (uint)(skill.Experience / (double)skill.WeaponSkillUp.ReqExp * 100);
                }
            }

            if (byAction || skill.Level < Level / 10 + 1
                         || skill.Level >= MASTER_WEAPONSKILLLEVEL)
            {
                skill.Experience = (uint)newExp;

                if (increaseLev > 0)
                {
                    skill.Level += (byte)increaseLev;
                    skill.WeaponSkillUp = await WeaponSkillRepository.GetAsync((ushort)skill.Type, skill.Level);
                    await SendAsync(StrWeaponSkillUp);
                    await WeaponSkill.SaveAsync(skill);
                }
                else
                {
                    var newPercent = (int)(skill.Experience / (double)skill.WeaponSkillUp.ReqExp * 100);
                    if (oldPercent - oldPercent % 10 != newPercent - newPercent % 10)
                    {
                        await WeaponSkill.SaveAsync(skill);
                    }
                }

                await SendAsync(new MsgWeaponSkill(skill));
            }
        }

        #endregion

        #region User Secondary Password

        public ulong SecondaryPassword
        {
            get => user.LockKey;
            set => user.LockKey = value;
        }

        public bool IsUnlocked()
        {
            return SecondaryPassword == 0 || VarData[0] != 0;
        }

        public void UnlockSecondaryPassword()
        {
            VarData[0] = 1;
        }

        public bool CanUnlock2ndPassword()
        {
            return VarData[1] <= 2;
        }

        public void Increment2ndPasswordAttempts()
        {
            VarData[1] += 1;
        }

        public async Task SendSecondaryPasswordInterfaceAsync()
        {
            await GameAction.ExecuteActionAsync(8003020, this, null, null, string.Empty);
        }

        #endregion

        #region Layout

        public byte CurrentLayout
        {
            get => user.ShowType;
            set => user.ShowType = value;
        }

		#endregion

		#region Jar

		public async Task AddJarKillsAsync(int stcType)
		{
			Item jar = UserPackage.GetItemByType(Item.TYPE_JAR);
			if (jar != null)
				if (jar.MaximumDurability == stcType)
				{
					jar.Data += 1;
					await jar.SaveAsync();

					if (jar.Data % 50 == 0)
					{
						await jar.SendJarAsync();
					}
				}
		}

		#endregion

		#region Cool Action

		private readonly TimeOut coolSyncTimer = new(5);

        public bool IsCoolEnable()
        {
            return coolSyncTimer.ToNextTime();
        }

        public bool IsFullSuper()
        {
            for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
            {
                Item item = GetEquipment(pos);
                if (item == null)
                {
                    switch (pos)
                    {
                        case ItemPosition.Mount:
                        case ItemPosition.Gourd:
                        case ItemPosition.Garment:
                        case ItemPosition.RightHandAccessory:
                        case ItemPosition.LeftHandAccessory:
                        case ItemPosition.MountArmor:
                        case (ItemPosition)13:
                        case (ItemPosition)14:
                            continue;
                        default:
                            return false;
                    }
                }

                if (!item.IsEquipment())
                {
                    continue;
                }

                if (item.GetQuality() % 10 < 9)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsArmorSuper()
        {
            return GetEquipment(ItemPosition.Armor)?.GetQuality() == 9;
        }

        #endregion

        #region Merchant

        public int Merchant => user.Business == 0 ? 0 : IsMerchant() ? 255 : 1;

        public int BusinessManDays => (int)(user.Business == 0 ? 0 : Math.Ceiling((UnixTimestamp.ToDateTime(user.Business) - DateTime.Now).TotalDays));


        public bool IsMerchant()
        {
            return user.Business != 0 && UnixTimestamp.ToDateTime(user.Business) < DateTime.Now;
        }

        public bool IsAwaitingMerchantStatus()
        {
            return user.Business != 0 && UnixTimestamp.ToDateTime(user.Business) > DateTime.Now;
        }

        public async Task<bool> SetMerchantAsync()
        {
            if (IsMerchant())
            {
                return false;
            }

            if (Level <= 30 && Metempsychosis == 0)
            {
                user.Business = (uint)DateTime.Now.ToUnixTimestamp();
                await SynchroAttributesAsync(ClientUpdateType.Merchant, 255);
            }
            else
            {
                user.Business = (uint)DateTime.Now.AddDays(5).ToUnixTimestamp();
            }
            await SaveAsync();
            return true;
        }

        public async Task RemoveMerchantAsync()
        {
            user.Business = 0;
            await SynchroAttributesAsync(ClientUpdateType.Merchant, 0);
            await SaveAsync();
        }

        public async Task SendMerchantAsync()
        {
            if (IsMerchant())
            {
                await SynchroAttributesAsync(ClientUpdateType.Merchant, 255);
                return;
            }

            if (IsAwaitingMerchantStatus())
            {
                await SynchroAttributesAsync(ClientUpdateType.Merchant, 1);
                await SendAsync(new MsgInteract
                {
                    Action = MsgInteractType.MerchantProgress,
                    Command = BusinessManDays
                });
                return;
            }

            if (Level <= 30 && Metempsychosis == 0)
            {
                await SendAsync(new MsgInteract
                {
                    Action = MsgInteractType.InitialMerchant
                });
                return;
            }

            await SynchroAttributesAsync(ClientUpdateType.Merchant, 0);
        }

        #endregion

        #region Synchronization

        public override async Task<bool> AddAttributesAsync(ClientUpdateType type, long value)
        {
            bool screen = false,
                 save = false;

            switch (type)
            {
                case ClientUpdateType.Level:
                    {
                        if (value < 0)
                        {
                            return false;
                        }

                        screen = true;
                        value = Level = (byte)Math.Max(1, Math.Min(MAX_UPLEV, Level + value));

                        if (Syndicate != null)
                        {
                            SyndicateMember.Level = Level;
                        }

                        save = true;
                        await GameAction.ExecuteActionAsync(USER_UPLEV_ACTION, this, null, null, string.Empty);
                        break;
                    }

                case ClientUpdateType.Experience:
                    {
                        if (value < 0)
                        {
                            Experience = Math.Max(0, Experience - (ulong)(value * -1));
                        }
                        else
                        {
                            Experience += (ulong)value;
                        }

                        value = (long)Experience;
                        break;
                    }

                case ClientUpdateType.Strength:
                    {
                        if (value < 0)
                        {
                            return false;
                        }

                        int maxAddPoints = MAX_ATTRIBUTE_POINTS - TotalAttributePoints;
                        if (maxAddPoints < 0)
                        {
                            return false;
                        }

                        value = Math.Min(maxAddPoints, value);
                        value = Strength = (ushort)Math.Max(0, Math.Min(ushort.MaxValue, Strength + value));
                        save = true;
                        break;
                    }
                case ClientUpdateType.Agility:
                    {
                        if (value < 0)
                        {
                            return false;
                        }

                        int maxAddPoints = MAX_ATTRIBUTE_POINTS - TotalAttributePoints;
                        if (maxAddPoints < 0)
                        {
                            return false;
                        }
                        value = Math.Min(maxAddPoints, value);
                        value = Speed = (ushort)Math.Max(0, Math.Min(ushort.MaxValue, Speed + value));
                        save = true;
                        break;
                    }

                case ClientUpdateType.Vitality:
                    {
                        if (value < 0)
                        {
                            return false;
                        }

                        int maxAddPoints = MAX_ATTRIBUTE_POINTS - TotalAttributePoints;
                        if (maxAddPoints < 0)
                        {
                            return false;
                        }

                        value = Math.Min(maxAddPoints, value);
                        value = Vitality = (ushort)Math.Max(0, Math.Min(ushort.MaxValue, Vitality + value));
                        save = true;
                        break;
                    }

                case ClientUpdateType.Spirit:
                    {
                        if (value < 0)
                        {
                            return false;
                        }

                        int maxAddPoints = MAX_ATTRIBUTE_POINTS - TotalAttributePoints;
                        if (maxAddPoints < 0)
                        {
                            return false;
                        }
                        value = Math.Min(maxAddPoints, value);
                        value = Spirit = (ushort)Math.Max(0, Math.Min(ushort.MaxValue, Spirit + value));
                        save = true;
                        break;
                    }

                case ClientUpdateType.Atributes:
                    {
                        int maxAddPoints = MAX_ATTRIBUTE_POINTS - TotalAttributePoints;
                        if (maxAddPoints < 0)
                        {
                            return false;
                        }

                        value = Math.Min(maxAddPoints, value);
                        value = AttributePoints = (ushort)Math.Max(0, Math.Min(ushort.MaxValue, AttributePoints + value));
                        save = true;
                        break;
                    }

                case ClientUpdateType.XpCircle:
                    {
                        if (value < 0)
                        {
                            XpPoints = (byte)Math.Max(0, XpPoints - value * -1);
                        }
                        else
                        {
                            XpPoints = (byte)Math.Max(0, XpPoints + value);
                        }

                        value = XpPoints;
                        break;
                    }

                case ClientUpdateType.Stamina:
                    {
                        if (value < 0)
                        {
                            Energy = (byte)Math.Max(0, Energy - value * -1);
                        }
                        else
                        {
                            Energy = (byte)Math.Max(0, Math.Min(MaxEnergy, Energy + value));
                        }

                        value = Energy;
                        break;
                    }

                case ClientUpdateType.PkPoints:
                    {
                        value = PkPoints = (ushort)Math.Max(0, Math.Min(PkPoints + value, ushort.MaxValue));
                        await CheckPkStatusAsync();
                        screen = true;
                        save = true;
                        break;
                    }

                case ClientUpdateType.Vigor:
                    {
                        Vigor = Math.Max(0, Math.Min(MaxVigor, (int)value + Vigor));
                        await SendAsync(new MsgData
                        {
                            Action = MsgData.DataAction.SetMountMovePoint,
                            Year = Vigor
                        });
                        return true;
                    }

                case ClientUpdateType.Hitpoints:
                    {
                        value = Life = (uint)Math.Min(MaxLife, Math.Max(Life + value, 0));
                        await BroadcastTeamLifeAsync();
                        break;
                    }

                case ClientUpdateType.QuizPoints:
                    {
                        value = QuizPoints = (uint) Math.Max(0, value + QuizPoints);
                        screen = true;
                        save = true;
                        break;
                    }

                default:
                    {
						bool result = await base.AddAttributesAsync(type, value);
                        await SaveAsync();
						return result;
					}
            }

            if (save)
            {
                await SaveAsync();
            }

            await SynchroAttributesAsync(type, (ulong)value, screen);
            return true;
        }

        public override async Task<bool> SetAttributesAsync(ClientUpdateType type, ulong value)
        {
            bool screen = false,
                save = false;
            switch (type)
            {
                case ClientUpdateType.Level:
                    {
                        save = screen = true;
                        Level = (byte)Math.Max(1, Math.Min(MAX_UPLEV, value));
                        break;
                    }

                case ClientUpdateType.Experience:
                    {
                        Experience = Math.Max(0, value);
                        break;
                    }

                case ClientUpdateType.XpCircle:
                    {
                        XpPoints = (byte)Math.Max(0, Math.Min(value, 100));
                        break;
                    }

                case ClientUpdateType.Stamina:
                    {
                        Energy = (byte)Math.Max(0, Math.Min(value, MaxEnergy));
                        break;
                    }

                case ClientUpdateType.Atributes:
                    {
                        save = true;
                        AttributePoints = (ushort)Math.Max(0, Math.Min(ushort.MaxValue, value));
                        break;
                    }

                case ClientUpdateType.PkPoints:
                    {
                        PkPoints = (ushort)Math.Max(0, Math.Min(ushort.MaxValue, value));
                        await CheckPkStatusAsync();
                        break;
                    }

                case ClientUpdateType.Mesh:
                    {
                        screen = true;
                        save = true;
                        Mesh = (uint)value;
                        break;
                    }

                case ClientUpdateType.HairStyle:
                    {
                        screen = true;
                        save = true;
                        Hairstyle = (ushort)value;
                        break;
                    }

                case ClientUpdateType.Strength:
                    {
                        save = true;
                        value = Strength = (ushort)Math.Min(ushort.MaxValue, value);
                        break;
                    }

                case ClientUpdateType.Agility:
                    {
                        save = true;
                        value = Speed = (ushort)Math.Min(ushort.MaxValue, value);
                        break;
                    }

                case ClientUpdateType.Vitality:
                    {
                        save = true;
                        value = Vitality = (ushort)Math.Min(ushort.MaxValue, value);
                        break;
                    }

                case ClientUpdateType.Spirit:
                    {
                        save = true;
                        value = Spirit = (ushort)Math.Min(ushort.MaxValue, value);
                        break;
                    }

                case ClientUpdateType.Class:
                    {
                        save = true;
                        screen = true;
                        Profession = (byte)value;

                        if (SyndicateMember != null)
                        {
                            SyndicateMember.Profession = (int)value;
                        }
                        break;
                    }

                case ClientUpdateType.FirstProfession:
                    {
                        save = true;
                        FirstProfession = (byte)value;
                        break;
                    }

                case ClientUpdateType.PreviousProfession:
                    {
                        save = true;
                        PreviousProfession = (byte)value;
                        break;
                    }

                case ClientUpdateType.Reborn:
                    {
                        save = true;
                        Metempsychosis = (byte)Math.Min(2, Math.Max(0, value));
                        value = Math.Min(2, value);
                        break;
                    }

                case ClientUpdateType.VipLevel:
                    {
                        value = VipLevel = (uint)Math.Max(0, Math.Min(6, value));
                        await SendAsync(new MsgVipFunctionValidNotify() { Flags = (int)UserVipFlag });
                        break;
                    }

                case ClientUpdateType.Vigor:
                    {
                        Vigor = Math.Max(0, Math.Min(MaxVigor, (int)value));
                        await SendAsync(new MsgData
                        {
                            Action = MsgData.DataAction.SetMountMovePoint,
                            Year = Vigor
                        });
                        return true;
                    }

                case ClientUpdateType.Money:
                    {
                        Silvers = (uint)Math.Max(0, Math.Min(int.MaxValue, value));
                        break;
                    }

                case ClientUpdateType.ConquerPoints:
                    {
                        ConquerPoints = (uint)Math.Max(0, Math.Min(int.MaxValue, value));
                        break;
                    }

                case ClientUpdateType.BoundConquerPoints:
                    {
                        ConquerPointsBound = (uint)Math.Max(0, Math.Min(int.MaxValue, value));
                        break;
                    }

                case ClientUpdateType.Hitpoints:
                    {
                        Life = (uint)Math.Min(value, MaxLife);
                        screen = true;
                        await BroadcastTeamLifeAsync();
                        break;
                    }

                default:
                    {
                        return await base.SetAttributesAsync(type, value);
                    }
            }

            if (save)
            {
                await SaveAsync();
            }
            await SynchroAttributesAsync(type, value, screen);
            return true;
        }

        public async Task CheckPkStatusAsync()
        {
            if (PkPoints > 99 && QueryStatus(StatusSet.BLACK_NAME) == null)
            {
                await DetachStatusAsync(StatusSet.RED_NAME);
                await AttachStatusAsync(this, StatusSet.BLACK_NAME, 0, int.MaxValue, 1);
            }
            else if (PkPoints > 29 && PkPoints < 100 && QueryStatus(StatusSet.RED_NAME) == null)
            {
                await DetachStatusAsync(StatusSet.BLACK_NAME);
                await AttachStatusAsync(this, StatusSet.RED_NAME, 0, int.MaxValue, 1);
            }
            else if (PkPoints < 30)
            {
                await DetachStatusAsync(StatusSet.BLACK_NAME);
                await DetachStatusAsync(StatusSet.RED_NAME);
            }
        }

        #endregion

        #region Lucky

        private int blessPoints = 0;
        private uint idLuckyTarget = 0;
        private int luckyTimeCount = 0;

        private readonly TimeOut luckyAbsorbStartTimer = new(2);
        private readonly TimeOut luckyStepTimer = new(1);

        public const int LUCKY_TIME_SECS_LIMIT = 60 * 120;

        public Task ChangeLuckyTimerAsync(int value)
        {
            ulong ms = 0;

            luckyTimeCount += value;
            if (luckyTimeCount > 0 && value > 0)
            {
                user.LuckyTime = (uint)DateTime.Now.AddSeconds(Math.Min(LUCKY_TIME_SECS_LIMIT, luckyTimeCount)).ToUnixTimestamp();
            }

            if (IsLucky)
            {
                ms = (ulong)(UnixTimestamp.ToDateTime(user.LuckyTime) - DateTime.Now).TotalSeconds * 1000UL;
            }

            return SynchroAttributesAsync(ClientUpdateType.LuckyTimeTimer, ms);
        }

        public bool IsLucky => user.LuckyTime != 0 && UnixTimestamp.ToDateTime(user.LuckyTime) > DateTime.Now;

        public async Task SendLuckAsync()
        {
            if (IsLucky)
            {
                await SynchroAttributesAsync(ClientUpdateType.LuckyTimeTimer, (ulong)(UnixTimestamp.ToDateTime(user.LuckyTime) - DateTime.Now).TotalSeconds * 1000UL);
            }
        }

        #endregion

        #region Heaven Blessing

        private readonly TimeOut heavenBlessingTimer = new(60);

        public async Task SendBlessAsync()
        {
            if (IsBlessed)
            {
                DateTime now = DateTime.Now;
                await SynchroAttributesAsync(ClientUpdateType.HeavensBlessing,
                                             (uint)(HeavenBlessingExpires - now).TotalSeconds);

                if (Map != null && !Map.IsTrainingMap())
                {
                    await SynchroAttributesAsync(ClientUpdateType.OnlineTraining, 0);
                }
                else
                {
                    await SynchroAttributesAsync(ClientUpdateType.OnlineTraining, 1);
                }

                await AttachStatusAsync(this, StatusSet.HEAVEN_BLESS, 0,
                                        (int)(HeavenBlessingExpires - now).TotalSeconds, 0);
            }
        }

        public async Task<bool> AddBlessingAsync(uint amount)
        {
            DateTime now = DateTime.Now;
            if (user.HeavenBlessing != 0 && UnixTimestamp.ToDateTime(user.HeavenBlessing) > now)
            {
                user.HeavenBlessing = (uint)UnixTimestamp.ToDateTime(user.HeavenBlessing).AddHours(amount).ToUnixTimestamp();
                heavenBlessingTimer.Update();
            }
            else
            {
                user.HeavenBlessing = (uint)now.AddHours(amount).ToUnixTimestamp();
            }

            await SendBlessAsync();
            return true;
        }

        public DateTime HeavenBlessingExpires => UnixTimestamp.ToDateTime(user.HeavenBlessing);

        public bool IsBlessed => UnixTimestamp.ToDateTime(user.HeavenBlessing) > DateTime.Now;

        #endregion

        #region Home

        public uint HomeIdentity
        {
            get => user?.HomeIdentity ?? 0u;
            set => user.HomeIdentity = value;
        }

        #endregion

        #region Progress Bar

        public ProgressBar AwaitingProgressBar { get; set; }

        public class ProgressBar
        {
            private readonly TimeOut timeOut;

            public ProgressBar(int seconds)
            {
                timeOut = new TimeOut();
                timeOut.Startup(seconds);
            }

            public ProgressBar(int seconds, string script, string failScript)
            {
                timeOut = new TimeOut(seconds);
                IsLua = true;
                Script = script;
                FailScript = failScript;
            }

            public bool IsLua { get; }

            public uint IdNext { get; init; }
            public uint IdNextFail { get; init; }
            public string Script { get; init; }
            public string FailScript { get; init; }
            public uint Command { get; init; }
            public bool Completed => timeOut.IsActive() && timeOut.IsTimeOut();
        }

        private async Task ExploreFailAsync()
        {
            if (AwaitingProgressBar != null && !AwaitingProgressBar.Completed)
            {
                if (AwaitingProgressBar.IsLua)
                {
                    string script = LuaScriptManager.ParseTaskDialogAnswerToScript(AwaitingProgressBar.FailScript);
                    LuaScriptManager.Run(this, null, null, [], script);
                }
                else
                {
                    await GameAction.ExecuteActionAsync(AwaitingProgressBar.IdNextFail, this, null, null, []);
                }
                AwaitingProgressBar = null;

                await SendAsync(new MsgAction
                {
                    Action = ActionType.ProgressBar,
                    Identity = Identity,
                    Command = 0,
                    Direction = 1,
                    MapColor = 0,
                    Strings = new List<string>
                    {
                        "Error"
                    }
                });
            }
        }

        #endregion

        #region Requests

        private readonly ConcurrentDictionary<RequestType, uint> requests = new();
        private int requestData;

        public void SetRequest(RequestType type, uint target, int data = 0)
        {
            requests.TryRemove(type, out _);
            if (target == 0)
            {
                return;
            }

            requestData = data;
            requests.TryAdd(type, target);
        }

        public uint QueryRequest(RequestType type)
        {
            return requests.TryGetValue(type, out uint value) ? value : 0;
        }

        public int QueryRequestData(RequestType type)
        {
            if (requests.TryGetValue(type, out _))
            {
                return requestData;
            }

            return 0;
        }

        public uint PopRequest(RequestType type)
        {
            if (requests.TryRemove(type, out uint value))
            {
                requestData = 0;
                return value;
            }
            return 0;
        }

        #endregion

        #region Logger

        public string GetDefaultLoggerPrefix()
        {
            return $"{Identity},{Name},{Level},{Metempsychosis},{Profession}";
        }

        #endregion

        #region Nationality

        public PlayerCountry Nationality
        {
            get => (PlayerCountry)user.Nationality;
            set => user.Nationality = (ushort)value;
        }

        #endregion

        #region Vip Teleport

        private const int PERSONAL_VIP_TELEPORT_COOLDOWN = 180;
        private const int TEAM_VIP_TELEPORT_COOLDOWN = 300;

        private readonly TimeOut portalTeleportTimer = new();
        private readonly TimeOut cityTeleportTimer = new();
        private readonly TimeOut teamPortalTeleportTimer = new();
        private readonly TimeOut teamCityPortalTeleportTimer = new();

        public bool CanUseVipPortal() => VipLevel >= 3 && (!portalTeleportTimer.IsActive() || portalTeleportTimer.IsTimeOut());
        public bool CanUseVipCityTeleport() => VipLevel >= 2 && (!cityTeleportTimer.IsActive() || cityTeleportTimer.IsTimeOut());
        public bool CanUseVipTeamPortal() => VipLevel >= 3 && (!teamPortalTeleportTimer.IsActive() || teamPortalTeleportTimer.IsTimeOut());
        public bool CanUseVipTeamCityTeleport() => VipLevel >= 3 && (!teamCityPortalTeleportTimer.IsActive() || teamCityPortalTeleportTimer.IsTimeOut());

        public void UseVipPortal()
        {
            portalTeleportTimer.Startup(PERSONAL_VIP_TELEPORT_COOLDOWN);
        }

        public void UseVipCityPortal()
        {
            cityTeleportTimer.Startup(PERSONAL_VIP_TELEPORT_COOLDOWN);
        }

        public void UseVipTeamPortal()
        {
            teamPortalTeleportTimer.Startup(TEAM_VIP_TELEPORT_COOLDOWN);
        }

        public void UseVipTeamCityPortal()
        {
            teamCityPortalTeleportTimer.Startup(TEAM_VIP_TELEPORT_COOLDOWN);
        }

        #endregion

        #region Achievements

        public Achievements Achievements { get; }

        #endregion

        #region Mail Box

        public MailBox MailBox { get; }

        #endregion

        #region Slot Machine

        public uint SlotMachineId { get; set; }
        public string SlotMachineName { get; set; } = string.Empty;
        public long SlotMachineReward { get; set; }
        public SlotMachineManager.SlotWinningRuleType SlotWinningRuleType { get; set; } = SlotMachineManager.SlotWinningRuleType.None;
        public SlotMachineManager.WinningRule? SlotMachineResult { get; set; }

        public async Task GetSlotMachineRewardAsync()
        {
            if (!SlotMachineResult.HasValue || SlotWinningRuleType == SlotMachineManager.SlotWinningRuleType.None)
            {
                return;
            }

            if (SlotMachineReward > 0)
            {
                string broadcastMessage = string.Empty;
                if (SlotWinningRuleType == SlotMachineManager.SlotWinningRuleType.Money)
                {
                    await AwardMoneyAsync(SlotMachineReward);

                    if (SlotMachineReward > 1_250_000)
                    {
                        broadcastMessage = string.Format(StrSlotMoney1250, Name, SlotMachineName, SlotMachineReward);
                    }
                    else if (SlotMachineReward > 6_250_000)
                    {
                        broadcastMessage = string.Format(StrSlotMoney6250, Name, SlotMachineName, SlotMachineReward);
                    }
                    else if (SlotMachineReward > 12_500_000)
                    {
                        broadcastMessage = string.Format(StrSlotMoney12500, Name, SlotMachineName, SlotMachineReward);
                    }
                    else if (SlotMachineReward > 666_665_000)
                    {
                        broadcastMessage = string.Format(StrSlotMoney666665, Name, SlotMachineName, SlotMachineReward);
                    }
                }
                else
                {
                    await AwardConquerPointsAsync((int)SlotMachineReward);
                    await SaveEmoneyLogAsync(EmoneyOperationType.SlotMachineReward, SlotMachineId, 0, SlotMachineReward);

                    if (SlotMachineReward > 270)
                    {
                        broadcastMessage = string.Format(StrSlotEmoney270, Name, SlotMachineName, SlotMachineReward);
                    }
                    else if (SlotMachineReward > 1350)
                    {
                        broadcastMessage = string.Format(StrSlotEmoney1350, Name, SlotMachineName, SlotMachineReward);
                    }
                    else if (SlotMachineReward > 2700)
                    {
                        broadcastMessage = string.Format(StrSlotEmoney2700, Name, SlotMachineName, SlotMachineReward);
                    }
                    else if (SlotMachineReward > 666_665)
                    {
                        broadcastMessage = string.Format(StrSlotEmoney666665, Name, SlotMachineName, SlotMachineReward);
                    }
                }

                if (!string.IsNullOrEmpty(broadcastMessage))
                {
                    await RoleManager.BroadcastWorldMsgAsync(broadcastMessage, TalkChannel.TopLeft, Color.White);
                }
            }

            SlotMachineManager.WinningRule rule = SlotMachineResult.Value;
            await SendAsync(new MsgSlotResult
            {
                Action = MsgSlotResult.SlotResultType.Stop,
                Identity = SlotMachineId,
                Reward = (ulong)SlotMachineReward
            });

#if DEBUG
            if (IsPm() && SlotMachineReward > 0)
            {
                await SendAsync($"slot machine reward: {SlotMachineReward} {SlotWinningRuleType}", TalkChannel.Talk, Color.White);
            }
#endif

            SlotMachineResult = null;
            SlotMachineId = 0;
            SlotMachineName = string.Empty;
            SlotMachineReward = 0;
            SlotWinningRuleType = SlotMachineManager.SlotWinningRuleType.None;
        }

        #endregion

        #region Lottery

        private static readonly ILogger lotteryLogger = Logger.CreateLogger("lottery");

        private struct LotteryData
        {
            public byte Color { get; init; }
            public byte Rank { get; init; }
            public DbItemtype ItemType { get; init; }
            public byte Addition { get; init; }
            public byte SocketNum { get; init; }
        }

        public int LotteryRetries { get; set; }
        private LotteryData? lotteryReward;
        private int currentLotteryPool;

        public bool IsLotteryEnable => 10 + 10 * VipLevel > Statistic.GetDailyValue(22);

        public async Task<bool> PlayLotteryAsync(int pool)
        {
            if (pool == int.MaxValue)
            {
                pool = currentLotteryPool;
            }

            // here we do not check anything!
            var rewardItemType = await LotteryManager.GetRandomItemAsync(pool, lotteryReward?.ItemType?.Type ?? 0);
            if (!rewardItemType.HasValue)
            {
                logger.Error("Could not fetch lottery item for user {0} {1} on pool {2}", Identity, Name, pool);
                return false;
            }

            lotteryReward = new LotteryData
            {
                ItemType = ItemManager.GetItemtype(rewardItemType.Value.ItemIdentity),
                Color = rewardItemType.Value.Color,
                Addition = rewardItemType.Value.Plus,
                Rank = (byte)rewardItemType.Value.Rank,
                SocketNum = rewardItemType.Value.SocketNum
            };

            currentLotteryPool = pool;

            await SendAsync(new MsgLottery
            {
                Action = MsgLottery.LotteryRequest.Show,
                Addition = lotteryReward.Value.Addition,
                Color = lotteryReward.Value.Color,
                ItemType = lotteryReward.Value.ItemType.Type,
                SocketOne = (byte)(lotteryReward.Value.SocketNum > 0 ? 255 : 0),
                SocketTwo = (byte)(lotteryReward.Value.SocketNum > 1 ? 255 : 0),
                UsedChances = (byte)(LotteryRetries)
            });

            await ActivityManager.UpdateTaskActivityAsync(this, ActivityManager.ActivityType.Lottery);
            return true;
        }

        public async Task AcceptLotteryPrizeAsync()
        {
            if (!lotteryReward.HasValue)
            {
                return;
            }

            DbItem dbItem = CreateEntity(lotteryReward.Value.ItemType.Type);
            dbItem.Magic3 = lotteryReward.Value.Addition;
            dbItem.Color = 3;
            dbItem.Gem1 = (byte)(lotteryReward.Value.SocketNum > 0 ? 255 : 0);
            dbItem.Gem2 = (byte)(lotteryReward.Value.SocketNum > 1 ? 255 : 0);

            Item item = new(this);
            if (!await item.CreateAsync(dbItem))
            {
                return;
            }

            await UserPackage.AddItemAsync(item);

            if (lotteryReward.Value.Rank > 5)
            {
                await SendAsync(string.Format(StrLotteryLow, lotteryReward.Value.ItemType.Name));
            }
            else
            {
                await RoleManager.BroadcastWorldMsgAsync(string.Format(StrLotteryHigh, Name, lotteryReward.Value.ItemType.Name), TalkChannel.Talk, Color.White);
            }

            ResetLottery();
        }

        public void ResetLottery()
        {
            LotteryRetries = 0;
            lotteryReward = null;
        }

        #endregion

        #region User Delete

        private bool deleted;

        public async Task<bool> DeleteUserAsync()
        {
            user.LogoutTime = (uint)UnixTimestamp.Now;
            await SaveAsync();

            await using var ctx = new ServerDbContext();
            await ctx.Database.BeginTransactionAsync();
            try
            {
                await OnUserDeletedAsync(this, ctx);

                await ctx.Database.ExecuteSqlRawAsync($"INSERT INTO `cq_deluser` SELECT * FROM `cq_user` WHERE id = {Identity} LIMIT 1;");

                ctx.Users.Remove(user);

                await ctx.SaveChangesAsync();
                await ctx.Database.CommitTransactionAsync();

                deleted = true;
            }
            catch (Exception ex)
            {
                await ctx.Database.RollbackTransactionAsync();
                logger.Error(ex, "Error on delete user: {0}", ex.Message);
                return false;
            }

            logger.Information("Deleted user {0} {1}", Identity, Name);
            return true;
        }

		#endregion

		#region Session

		

		//public async Task OnDisconnectAsync()
		//{
		//	if (PigletClient.Instance?.Actor != null)
		//	{
		//		await PigletClient.Instance.Actor.SendAsync(new MsgPigletUserLogin()
		//		{
		//			Data = new MsgPigletUserLogin<PigletActor>.UserLoginData
		//			{
		//				Users = new List<MsgPigletUserLogin<PigletActor>.UserData>
		//					{
		//						new MsgPigletUserLogin<PigletActor>.UserData
		//						{
		//							AccountId = Client.AccountIdentity,
		//							UserId = Identity,
		//							IsLogin = false
		//						}
		//					}
		//			}
		//		});
		//	}

		//	using var ctx = new ServerDbContext();
		//	if (Map?.IsRecordDisable() == false)
		//	{
		//		if (IsAlive)
		//		{
		//			user.MapID = idMap;
		//			user.X = currentX;
		//			user.Y = currentY;
		//		}
		//	}

		//	user.LogoutTime = (uint)DateTime.Now.ToUnixTimestamp();
		//	user.OnlineSeconds += (int)(LastLogout - LastLogin).TotalSeconds;

		//	if (Booth != null)
		//	{
		//		await Booth.LeaveMapAsync();
		//	}

		//	if (Team != null && Team.IsLeader(Identity))
		//	{
		//		await Team.DismissAsync(this, true);
		//	}
		//	else if (Team != null)
		//	{
		//		await Team.DismissMemberAsync(this);
		//	}

		//	if (Trade != null)
		//	{
		//		await Trade.SendCloseAsync();
		//	}

		//	await EventManager.OnLogoutAsync(this);
		//	await NotifyOfflineFriendAsync();

		//	foreach (Tutor apprentice in apprentices.Values.Where(x => x.Student != null))
		//	{
		//		await apprentice.SendTutorAsync();
		//		await apprentice.Student.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower, 0, 0);
		//	}

		//	if (tutorAccess != null)
		//	{
		//		ctx.TutorAccess.Update(tutorAccess);
		//	}

		//	foreach (IStatus status in StatusSet.Status.Values.Where(x => x.Model != null))
		//	{
		//		if (status is StatusMore && status.RemainingTimes == 0)
		//		{
		//			continue;
		//		}

		//		status.Model.LeaveTimes = (uint)status.RemainingTimes;
		//		status.Model.RemainTime = (uint)status.RemainingTime;

		//		if (status.Identity == 0)
		//		{
		//			ctx.Status.Add(status.Model);
		//		}
		//		else
		//		{
		//			ctx.Status.Update(status.Model);
		//		}
		//	}

		//	await WeaponSkill.SaveAllAsync(ctx);

		//	if (Syndicate != null && SyndicateMember != null)
		//	{
		//		SyndicateMember.LastLogout = DateTime.Now;
		//		await SyndicateMember.SaveAsync();
		//	}

		//	if (Map != null)
		//	{
		//		QueueAction(LeaveMapAsync);
		//	}

		//	await Fate.SaveAsync();
		//	await JiangHu.LogoutAsync();

		//	if (!isDeleted)
		//	{
		//		ctx.Users.Update(user);
		//	}
		//	ctx.GameLoginRecords.Add(new DbGameLoginRecord
		//	{
		//		AccountIdentity = Client.AccountIdentity,
		//		UserIdentity = Identity,
		//		LoginTime = LastLogin,
		//		LogoutTime = LastLogout,
		//		ServerVersion = $"6609",
		//		IpAddress = Client.IpAddress,
		//		MacAddress = Client.MacAddress,
		//		OnlineTime = (uint)(LastLogout - LastLogin).TotalSeconds
		//	});
		//	await ctx.SaveChangesAsync();
		//}



		#endregion

		#region Timer

        public async Task OnBattleTimerAsync()
        {
            if (BattleSystem.IsActive()
                && BattleSystem.NextAttack(GetInterAtkRate()))
            {
                await BattleSystem.ProcessAttackAsync();
            }

            if (MagicData.State != MagicState.None)
            {
                await MagicData.OnTimerAsync();
            }
        }
		public override async Task OnTimerAsync()
        {
            if (Map == null)
            {
                return;
            }

            await base.OnTimerAsync();

            if (MessageBox?.HasExpired == true)
            {
                MessageBox = null;
            }

            if (MessageBox != null)
            {
                QueueAction(MessageBox.OnTimerAsync);
            }

            if (AwaitingProgressBar?.Completed == true)
            {
                QueueAction(async () =>
                {
                    if (AwaitingProgressBar != null)
                    {
                        Role role = RoleManager.GetRole(InteractingNpc);
                        Item item = UserPackage.GetInventory(InteractingItem);
                        if (AwaitingProgressBar.IsLua)
                        {
                            string script = LuaScriptManager.ParseTaskDialogAnswerToScript(AwaitingProgressBar.Script);
                            LuaScriptManager.Run(this, role, item, [], script);
                        }
                        else
                        {
                            await GameAction.ExecuteActionAsync(AwaitingProgressBar.IdNext, this, role, item, string.Empty);
                        }
                        AwaitingProgressBar = null;
                    }
                });
            }

            if (pkDecreaseTimer.ToNextTime(PK_DEC_TIME) && PkPoints > 0)
            {
                QueueAction(async () =>
                {
                    if (Map?.IsPrisionMap() == true)
                    {
                        await AddAttributesAsync(ClientUpdateType.PkPoints, PKVALUE_DEC_ONCE_IN_PRISON);
                    }
                    else
                    {
                        await AddAttributesAsync(ClientUpdateType.PkPoints, PKVALUE_DEC_ONCE);
                    }
                });
            }

            if (IsBlessed && heavenBlessingTimer.ToNextTime() && !Map.IsTrainingMap())
            {
                blessPoints++;
                if (blessPoints >= 10)
                {
                    GodTimeExp += 60;

                    if (GodTimeExp >= 60000 && Level < ExperienceManager.GetLevelLimit())
                    {
                        await SendAsync(new MsgGodExp
                        {
                            Action = MsgGodExpAction.MaximimBlessExpTimeAlert
                        });
                    }

                    await SynchroAttributesAsync(ClientUpdateType.OnlineTraining, 5);
                    await SynchroAttributesAsync(ClientUpdateType.OnlineTraining, 0);
                    blessPoints = 0;
                }
                else
                {
                    await SynchroAttributesAsync(ClientUpdateType.OnlineTraining, 4);
                    await SynchroAttributesAsync(ClientUpdateType.OnlineTraining, 3);
                }
            }

            if (idLuckyTarget == 0 && Metempsychosis < 2 && QueryStatus(StatusSet.LUCKY_DIFFUSE) == null)
            {
                QueueAction(() =>
                {
                    if (QueryStatus(StatusSet.LUCKY_ABSORB) == null)
                    {
                        foreach (Character user in Screen.Roles.Values.Where(x => x is Character).Cast<Character>())
                        {
                            if (user.QueryStatus(StatusSet.LUCKY_DIFFUSE) != null && GetDistance(user) <= 3)
                            {
                                idLuckyTarget = user.Identity;
                                luckyAbsorbStartTimer.Startup(3);
                                break;
                            }
                        }
                    }
                    return Task.CompletedTask;
                });
            }
            else if (QueryStatus(StatusSet.LUCKY_DIFFUSE) == null)
            {
                QueueAction(async () =>
                {
                    var role = QueryRole(idLuckyTarget) as Character;
                    if (luckyAbsorbStartTimer.IsActive() && luckyAbsorbStartTimer.IsTimeOut() && role != null)
                    {
                        await AttachStatusAsync(role, StatusSet.LUCKY_ABSORB, 0, 1000000, 0);
                        idLuckyTarget = 0;
                        luckyAbsorbStartTimer.Clear();
                    }
                });
            }

            if (luckyStepTimer.ToNextTime() && IsLucky)
            {
                QueueAction(() =>
                {
                    if (QueryStatus(StatusSet.LUCKY_DIFFUSE) == null && QueryStatus(StatusSet.LUCKY_ABSORB) == null)
                    {
                        return ChangeLuckyTimerAsync(-1);
                    }
                    return Task.CompletedTask;
                });
            }

            if (Team != null)
            {
                if (!Team.IsLeader(Identity) && Team.Leader.MapIdentity == MapIdentity 
                    && teamLeaderPosTimer.ToNextTime())
                {
                    await SendAsync(new MsgAction
                    {
                        Action = ActionType.MapTeamLeaderStar,
                        Command = Team.Leader.Identity,
                        X = Team.Leader.X,
                        Y = Team.Leader.Y
                    });
                }

                //if (Team.IsLeader(Identity) && Team.IsAutoInvite)
                //{
                //    foreach (var targetUser in Screen.Roles.Values.Where(x => x is Character user && user.Team == null).Cast<Character>())
                //    {
                // auto invite may not be a good idea btw
                //    }
                //}
            }

            QueueAction(UserPackage.OnTimerAsync);
            await CoatStorage.OnTimerAsync();
            await TitleStorage.OnTimerAsync();

            if (dateSyncTimer.ToNextTime())
            {
                await SendAsync(new MsgData(DateTime.Now));
            }

            if (!IsAlive)
            {
                return;
            }

            if (Transformation != null && transformationTimer.IsActive() && transformationTimer.IsTimeOut())
            {
                await ClearTransformationAsync();
            }

            if (vigorTimer.ToNextTime() && QueryStatus(StatusSet.RIDING) != null && Vigor < MaxVigor)
            {
                await AddAttributesAsync(ClientUpdateType.Vigor, (long)Math.Max(10, Math.Min(200, MaxVigor * 0.005)));
            }

            if (energyTimer.ToNextTime(ADD_ENERGY_STAND_MS) && Energy < MaxEnergy)
            {
                byte energyAmount = ADD_ENERGY_STAND;
                if (IsWing)
                {
                    energyAmount = ADD_ENERGY_STAND / 2;
                }
                else
                {
                    if (Action == EntityAction.Sit)
                    {
                        energyAmount = ADD_ENERGY_SIT;
                    }
                    else if (Action == EntityAction.Lie)
                    {
                        energyAmount = ADD_ENERGY_LIE;
                    }
                }

                var ridingCrop = UserPackage[ItemPosition.Crop];
                if (ridingCrop != null)
                {
                    energyAmount += (byte)ridingCrop.RecoverEnergy;
                }

                QueueAction(() => AddAttributesAsync(ClientUpdateType.Stamina, energyAmount));
            }

            if (xpPointsTimer.ToNextTime())
            {
                await ProcXpValAsync();
            }

            if (autoHealTimer.ToNextTime() && IsAlive && Life < MaxLife)
            {
                QueueAction(async () =>
                {
                    if (IsAlive)
                    {
                        await AddAttributesAsync(ClientUpdateType.Hitpoints, AUTOHEALLIFE_EACHPERIOD);
                    }
                });
            }
        }

		#endregion

		#region Call Pet

		private TimeOut callPetKeepSecs = new();
		private Monster callPet;

		public async Task<bool> CallPetAsync(uint type, ushort x, ushort y, int keepSecs = 0)
		{
			await KillCallPetAsync();

			Monster pet = await Monster.CreateCallPetAsync(this, type, x, y);
			if (pet == null)
				return false;

			callPet = pet;

			if (keepSecs > 0)
			{
				callPetKeepSecs.Startup(keepSecs);
			}
			else
			{
				callPetKeepSecs.Clear();
			}
			return true;
		}

		public async Task KillCallPetAsync(bool now = false)
		{
			if (callPet == null)
				return;

			if (!callPet.IsDeleted())
			{
				await callPet.DelMonsterAsync(now);
				callPet = null;
			}
		}

		public Role GetCallPet()
		{
			return callPet;
		}

		#endregion

		#region Hung Up

		public async Task FinishAutoHangUpAsync(HangUpMode mode)
		{
			if (!IsAutoHangUp)
			{
				return;
			}

			if (mode == HangUpMode.KilledNoBlessing || mode == HangUpMode.ChangedMap)
			{
				await SendAsync(new MsgHangUp
				{
					Action = mode,
					Experience = AutoHangUpExperience
				});

				await SendAsync(new MsgHangUp
				{
					Action = HangUpMode.End
				});
			}

			await AwardExperienceAsync((long)AutoHangUpExperience, true);
			AutoHangUpExperience = 0;

			IsAutoHangUp = false;
		}

		#endregion

		#region Cool Action

		public bool IsFullUnique()
		{
			for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
			{
				Item item = UserPackage[pos];
				if (item == null)
				{
					switch (pos)
					{
						case ItemPosition.Mount:
						case ItemPosition.Gourd:
						case ItemPosition.Garment:
						case ItemPosition.RightHandAccessory:
						case ItemPosition.LeftHandAccessory:
						case ItemPosition.MountArmor:
						case (ItemPosition)13:
						case (ItemPosition)14:
							continue;
						default:
							return false;
					}
				}

				if (!item.IsEquipment())
				{
					continue;
				}

				if (item.GetQuality() % 10 < 7)
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Change Name

		public int GetChangeNameRemainingAttempts()
		{
			uint periodInterval = (uint)(UnixTimestamp.Now - MsgChangeName.CHANGE_NAME_PERIOD);
			using var ctx = new ServerDbContext();
			int amount = ctx.ChangeNameBackups
				.Where(x => x.IdUser == Identity
							&& !x.OldName.Contains("[Z"))
				.Count(x => x.ChangeTime >= periodInterval);
			return Math.Max(0, Math.Min(MsgChangeName.MAX_CHANGES_PERIOD, MsgChangeName.MAX_CHANGES_PERIOD - amount));
		}

		public async Task BroadcastNewNameAsync()
		{
			if (SyndicateMember != null)
			{
				SyndicateMember.ChangeName(Name);
			}

			if (FamilyMember != null)
			{
				FamilyMember.ChangeName(Name);
			}
		}

		#endregion

		#region Purchases

		public async Task CheckFirstCreditAsync()
		{
			if (Flag.HasFlag(PrivilegeFlag.FirstCreditReady))
			{
				return;
			}

			if (PigletClient.Instance?.Actor != null)
			{
				await PigletClient.Instance.Actor.SendAsync(new MsgPigletUserCreditInfo(user.AccountIdentity).Encode());
			}
		}

		public async Task SetFirstCreditAsync()
		{
			if (SashSlots < MAXIMUM_SASH_SLOTS)
			{
				await SetSashSlotAmountAsync(MAXIMUM_SASH_SLOTS);
				await SendAsync(StrVipQkdSashUpgrade, TalkChannel.Talk, Color.White);
			}

			if (Flag.HasFlag(PrivilegeFlag.FirstCreditReady))
			{
				return;
			}

			Flag |= PrivilegeFlag.FirstCreditReady;
			await SynchroAttributesAsync(ClientUpdateType.PrivilegeFlag, (ulong)Flag);
		}

		public async Task ClaimFirstCreditGiftAsync()
		{
			if (!Flag.HasFlag(PrivilegeFlag.FirstCreditReady) || Flag.HasFlag(PrivilegeFlag.FirstCreditClaimed))
			{
				return;
			}

			if (!UserPackage.IsPackSpare(9))
			{
				await SendAsync(string.Format(StrNotEnoughSpaceN, 9));
				return;
			}

			var logger = Logger.CreateConsoleLogger("first_credit");
			var rewards = await AwardConfigRepository.GetFirstCreditRewardsAsync(ProfessionSort);
			foreach (var reward in rewards)
			{
				if (await UserPackage.AwardItemAsync((uint)reward.Data2, ItemPosition.Inventory, reward.Data3 != 0, reward.Data4 != 0))
				{
					logger.Information($"{Identity},{Profession},{Metempsychosis},{reward.Data2},{reward.Data3}");
				}
			}

			if (PigletClient.Instance?.Actor != null)
			{
				await PigletClient.Instance.Actor.SendAsync(new MsgPigletClaimFirstCredit(Client.AccountIdentity));
			}

			Flag |= PrivilegeFlag.FirstCreditClaimed | PrivilegeFlag.MapItemDisplay;
			await SynchroAttributesAsync(ClientUpdateType.PrivilegeFlag, (ulong)Flag);
			await SaveAsync();
		}

		#endregion

		#region Mining

		private static readonly ILogger mineLogger = Logger.CreateConsoleLogger("mining");
		private int mineCount;
		private int oreCount;
		private int itemCount;

		public void StartMining()
		{
			miningTimer.Startup(3);
			mineCount = 0;
			oreCount = 0;
			itemCount = 0;
		}

		public void StopMining()
		{
			miningTimer.Clear();
		}

		public async Task DoMineAsync()
		{
			if (!IsAlive)
			{
				await SendAsync(StrDead);
				StopMining();
				return;
			}

			if (!Map.IsMineField())
			{
				await SendAsync(StrNoMine);
				StopMining();
				return;
			}

			if (UserPackage[Item.ItemPosition.RightHand]?.GetItemSubType() != 562)
			{
				await SendAsync(StrMineWithPecker);
				StopMining();
				return;
			}

			try
			{
				if (UserPackage.IsPackFull())
				{
					await SendAsync(StrYourBagIsFull);
				}
				else
				{
					uint idItem = 0;
					float nChance = 30f + (float)(WeaponSkill[562]?.Level ?? 0) / 2;
					if (await ChanceCalcAsync(nChance))
					{
						const int euxiniteOre = 1072031;
						const int ironOre = 1072010;
						const int copperOre = 1072020;
						const int silverOre = 1072040;
						const int goldOre = 1072050;
						int oreRate = await NextAsync(100);
						int oreLevel = await NextAsync(10) % 10;
						switch (Map.ResLev) // TODO gems
						{
							case 1:
								{
									if (oreRate < 4) // 4% Euxinite
									{
										idItem = euxiniteOre;
									}
									else if (oreRate < 6) // 6% Gold Ore
									{
										idItem = (uint)(goldOre + oreLevel);
									}
									else if (oreRate < 50) // 40% Iron Ore
									{
										idItem = (uint)(ironOre + oreLevel);
									}

									break;
								}
							case 2:
								{
									if (oreRate < 5) // 5% Gold Ore
									{
										idItem = (uint)(goldOre + oreLevel);
									}
									else if (oreRate < 15) // 10% Copper Ore
									{
										idItem = (uint)(copperOre + oreLevel);
									}
									else if (oreRate < 50) // 35% Iron Ore
									{
										idItem = (uint)(ironOre + oreLevel);
									}

									break;
								}
							case 3:
								{
									if (oreRate < 5) // 5% Gold Ore
									{
										idItem = (uint)(goldOre + oreLevel);
									}
									else if (oreRate < 12) // 7% Silver Ore
									{
										idItem = (uint)(silverOre + oreLevel);
									}
									else if (oreRate < 25) // 13% Copper Ore
									{
										idItem = (uint)(copperOre + oreLevel);
									}
									else if (oreRate < 50) // 25% Iron Ore
									{
										idItem = (uint)(ironOre + oreLevel);
									}

									break;
								}
						}

						oreCount++;
					}
					else
					{
						idItem = await MineManager.MineAsync(MapIdentity, this);
						itemCount++;
					}

					DbItemtype itemtype = ItemManager.GetItemtype(idItem);
					if (itemtype == null)
					{
						return;
					}

					if (await UserPackage.AwardItemAsync(idItem))
					{
						await SendAsync(string.Format(StrMineItemFound, itemtype.Name));
						mineLogger.Information($"{Identity},{Name},{idItem},{MapIdentity},{Map?.Name},{X},{Y}");
					}

					mineCount++;
				}
			}

			catch (Exception ex)
			{
				logger.Fatal(ex, "Error on mining. {ex}", ex.Message);
			}
			finally
			{
				await BroadcastRoomMsgAsync(new MsgAction
				{
					Identity = Identity,
					Command = 0,
					ArgumentX = X,
					ArgumentY = Y,
					Action = ActionType.MapMine
				}, true);
			}
		}

		#endregion

		#region Socket

		public override Task SendAsync(IPacket msg)
        {
            return SendAsync(msg.Encode());
        }

        public override Task SendAsync(byte[] msg)
        {
            if (Client != null)
            {
                if (Client is CrossGameClient)
                {
                    uint serverId;
                    uint userId;
                    if (OriginServer.ServerId != RealmManager.ServerIdentity)
                    {
                        serverId = OriginServer.ServerId;
                        userId = OriginalUserId;
                    }
                    else if (CurrentServer.HasValue)
                    {
                        serverId = CurrentServer.Value.ServerId;
                        userId = RealmUserId;
                    }
                    else
                    {
                        return Task.CompletedTask;
                    }
                    return SendOSMsgAsync(new MsgCrossRedirectToUserPacketS()
                    {
                        Data = new CrossRedirectToUserPacketPB
                        {
                            Packet = msg,
                            UserId = userId
                        }
                    }, serverId);
                }
                return Client.SendAsync(msg);
            }
            return Task.CompletedTask;
        }

        public async Task SendWindowToAsync(Character player)
        {
            await player.SendAsync(new MsgPlayer(this, player)
            {
                SpawnType = 1
            });

            if (Flower != null && Flower.FairyType != 0)
            {
                await player.SendAsync(new MsgSuitStatus
                {
                    Action = 1,
                    Data = (int)Flower.FairyType,
                    Param = (int)Identity
                });
            }
        }

        public override async Task SendSpawnToAsync(Character player)
        {
            await player.SendAsync(new MsgPlayer(this, player));

            if (Flower != null && Flower.FairyType != 0)
            {
                await player.SendAsync(new MsgSuitStatus
                {
                    Action = 1,
                    Data = (int)Flower.FairyType,
                    Param = (int)Identity
                });
            }
        }

        public override async Task SendSpawnToAsync(Character player, int x, int y)
        {
            await player.SendAsync(new MsgPlayer(this, player, (ushort)x, (ushort)y));

            if (Flower != null && Flower.FairyType != 0)
            {
                await player.SendAsync(new MsgSuitStatus
                {
                    Action = 1,
                    Data = (int)Flower.FairyType,
                    Param = (int)Identity
                });
            }
        }

        #endregion

        #region Database

        public Task SaveAsync()
        {
            return ServerDbContext.UpdateAsync(user);
        }

		#endregion

		#region Session
		public DateTime LastLogin => UnixTimestamp.ToDateTime(user.LoginTime);
		public DateTime? LastLogout
		{
			get => UnixTimestamp.ToNullableDateTime(user.LogoutTime);
			set => user.LogoutTime = (uint)UnixTimestamp.FromDateTime(value);
		}
		public int TotalOnlineTime => user.OnlineSeconds;

		public DateTime? PreviousLoginTime { get; private set; }

		public TimeSpan OnlineTime => TimeSpan.Zero
											  .Add(new TimeSpan(0, 0, 0, user.OnlineSeconds))
											  .Add(new TimeSpan(
													   0, 0, 0,
													   (int)(DateTime.Now - LastLogin).TotalSeconds));

		public TimeSpan SessionOnlineTime => TimeSpan.Zero
													 .Add(new TimeSpan(
															  0, 0, 0,
															  (int)(DateTime.Now - LastLogin)
															  .TotalSeconds));

		public async Task SetLoginAsync()
		{
			PreviousLoginTime = UnixTimestamp.ToDateTime(user.LoginTime);
			user.LoginTime = (uint)DateTime.Now.ToUnixTimestamp();
			await SaveAsync();
		}
		

		public async Task OnLoginAsync()
		{
			if (user.FirstLogin == 0)
			{
				LuaScriptManager.Run(this, null, null, null, "System_PlayLoginFirst()");
				user.FirstLogin = (uint)UnixTimestamp.Now;
			}

			DbUser mate = await UserRepository.FindByIdentityAsync(MateIdentity);
			if (mate != null)
			{
				MateName = mate.Name;
			}

			await NpcServer.SendAsync(new MsgAiPlayerLogin(this));

			await DoDailyResetAsync(true);
			await GameAction.ExecuteActionAsync(1000000, this, null, null);

			if (user.LoginTime > 0)
			{
				PreviousLoginTime = UnixTimestamp.ToDateTime(user.LoginTime);
			}

			if (ConquerPoints > 0)
			{
				uint currentCheckSum = CalculateEmoneyCheckSum(user.ConquerPoints, user.Identity);
				if (user.ConquerPointsCheckSum != currentCheckSum)
				{
					logger.Warning("User {0} {1} has invalid value {2} of emoney. Last registered checksum {3} and current is {4}", Identity, Name, ConquerPoints, user.ConquerPointsCheckSum, currentCheckSum);
					await SetAttributesAsync(ClientUpdateType.ConquerPoints, 0);
				}
			}

			await InitializeActivityTasksAsync();
			await StageGoal.InitializeAsync();

			if (!IsUnlocked())
			{
				await SendAsync(new Msg2ndPsw
				{
					Password = 0x1,
					Action = Msg2ndPsw.PasswordRequestType.CorrectPassword
				});
			}

			await LoadTitlesAsync();
			await SendMerchantAsync();
			await LoadLeaveWordAsync();
			await LoadStatusAsync();
			await SendBlessAsync();
			LoadExperienceData();
			await SendMultipleExpAsync();
			await SendLuckAsync();

			await SynchroAttributesAsync(ClientUpdateType.CurrentSashSlots, SashSlots);
			await SynchroAttributesAsync(ClientUpdateType.MaximumSashSlots, MAXIMUM_SASH_SLOTS);

			await Screen.SynchroScreenAsync();

#if !DEBUG
            if (IsGm())
            {
                await TransformAsync(3321, int.MaxValue, true);
            }
#endif

			await SendAsync(new MsgSignIn
			{
				Action = MsgSignIn.MsgSignInType.Display
			});

			await MailBox.InitializeAsync();
			await Achievements.InitializeAsync();

			user.LoginTime = (uint)UnixTimestamp.Now;
			await UserRepository.UpdateAsync(user);
		}

		public async Task OnLogoutAsync()
		{
			try
			{
				if (!deleted)
				{
					await OnUserLogoutAsync(this);
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Error on module logout. {0}", ex.Message);
			}

			try
			{
				if (!deleted)
				{
					if (Map?.IsRecordDisable() == false)
					{
						if (!IsAlive)
							await ReallyRevive(true, false);

						user.MapID = idMap;
						user.X = currentX;
						user.Y = currentY;
					}
				}

				await LeaveMapAsync();

				await NpcServer.SendAsync(new MsgAiPlayerLogout
				{
					Data = new MsgAiPlayerLogoutContract
					{
						Timestamp = Environment.TickCount,
						Id = user.Identity
					}
				});

				if (!deleted)
				{
					user.LogoutTime = (uint)UnixTimestamp.Now;
					await UserRepository.UpdateAsync(user);
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Error on user logout! {0}", ex.Message);
			}
			finally
			{
				logger.Information("User {1} {0} has disconnected", Name, Identity);
				WorldProcessor.Instance.Queue(WorldProcessor.NO_MAP_GROUP, () =>
				{
					RoleManager.ForceLogoutUser(Identity);
					return Task.CompletedTask;
				});
			}
		}
		public async Task OnLoginAfterModulesAsync()
		{
			if (!IsUnlocked())
			{
				await SendAsync(new Msg2ndPsw
				{
					Password = 0x1,
					Action = Msg2ndPsw.PasswordRequestType.CorrectPassword
				});
			}

			await LoadTitlesAsync();
			await SendMerchantAsync();
			await LoadLeaveWordAsync();
			await LoadStatusAsync();
			await SendBlessAsync();
			LoadExperienceData();
			await SendMultipleExpAsync();
			await SendLuckAsync();

			await SynchroAttributesAsync(ClientUpdateType.CurrentSashSlots, SashSlots);
			await SynchroAttributesAsync(ClientUpdateType.MaximumSashSlots, MAXIMUM_SASH_SLOTS);

            if (Life == 0)
				await ReallyRevive(true, false);

			await SignIn.SendAsync();
			await MailBox.InitializeAsync();
			await Achievements.InitializeAsync();
			await TitleStorage.SendAllAsync();

			await LoadUnionAsync();

			await Screen.SynchroScreenAsync();
		}
		public async Task DoDailyResetAsync(bool login)
		{
			if (login && (!PreviousLoginTime.HasValue || PreviousLoginTime.Value.Date >= DateTime.Now.Date || LastLogout?.Date >= DateTime.Now.Date))
			{
				// already reseted
				return;
			}

			if (!login)
			{
				Statistic.ClearDailyStatistic();

				if (TaskDetail != null)
				{
					await TaskDetail.DailyResetAsync();
				}
			}
		}

		#endregion


		public enum EmoneyOperationType
        {
            None,
            Npc,
            Trade,
            Booth,
            Item,
            Monster,
            EmoneyShop,
            Nobility,
            ChestPackage,
            SuperFlag,
            WeaponSkillUpgrade,
            DegradeItem,
            Syndicate,
            Pigeon,
            AuctionBid,
            AuctionBuy,
            Mail,
            Lua,
            AwardCommand,
            FateBuyStrength,
            JiangHuStudy,
            JiangHuRestore,
            JiangHuGatherPoints,
            SlotMachine,
            SlotMachineReward
        }

        /// <summary>Enumeration type for body types for player characters.</summary>
        public enum BodyType : ushort
        {
            AgileMale = 1003,
            MuscularMale = 1004,
            AgileFemale = 2001,
            MuscularFemale = 2002
        }

        /// <summary>Enumeration type for base classes for player characters.</summary>
        public enum BaseClassType : ushort
        {
            Trojan = 10,
            Warrior = 20,
            Archer = 40,
            Ninja = 50,
            Monk = 60,
            Pirate = 70,
            DragonWarrior = 80,
            Taoist = 100
        }

        public enum PkModeType
        {
            FreePk,
            Peace,
            Team,
            Capture,
            Revenge,
            Syndicate,
            JiangHu,
            CrossServer,
            Union = 11
        }

        [Flags]
        public enum JiangPkMode
        {
            None = 0,
            NotHitFriends = 1,
            NotHitClanMembers = 2,
            NotHitGuildMembers = 4,
            NotHitAlliedGuild = 8,
            NoHitAlliesClan = 16
        }

        public enum PrivilegeFlag : uint
        {
            None = 0,
            FirstCreditReady = 1,
            MapItemDisplay = 2,
            FirstCreditClaimed = 4,
            OnMeleeAttack = 8
        }

        [Flags]
        public enum VipFlags
        {
            VipOne = ItemStatusExtraTime | Friends | BlessTime,
            VipTwo = VipOne | BonusLottery | VipFurniture | CityTeleport,
            VipThree = VipTwo | PortalTeleport | CityTeleportTeam,
            VipFour = VipThree | Avatar | DailyQuests | VipHairStyles,
            VipFive = VipFour | FrozenGrotto,
            VipSix = PortalTeleport | Avatar | MoreForVip | FrozenGrotto | TeleportTeam
                      | CityTeleport | CityTeleportTeam | BlessTime | OfflineTrainingGround | ItemStatusExtraTime
                      | Friends | VipHairStyles | Labirint | DailyQuests | VipFurniture | BonusLottery,

            PortalTeleport = 0x1,
            Avatar = 0x2,
            MoreForVip = 0x4,
            FrozenGrotto = 0x8,
            TeleportTeam = 0x10,
            CityTeleport = 0x20,
            CityTeleportTeam = 0x40,
            BlessTime = 0x80,
            OfflineTrainingGround = 0x100,
            /// <summary>
            /// Refinery and Artifacts
            /// </summary>
            ItemStatusExtraTime = 0x200,
            Friends = 0x400,
            VipHairStyles = 0x800,
            Labirint = 0x1000,
            DailyQuests = 0x2000,
            VipFurniture = 0x4000,
            BonusLottery = 0x8000,

            None = 0
        }

        public enum PlayerCountry
        {
            UnitedArabEmirates = 1,
            Argentine,
            Australia,
            Belgium,
            Brazil,
            Canada,
            China,
            Colombia,
            CostaRica,
            CzechRepublic,
            Conquer,
            Germany,
            Denmark,
            DominicanRepublic,
            Egypt,
            Spain,
            Estland,
            Finland,
            France,
            UnitedKingdom,
            HongKong,
            Indonesia,
            India,
            Israel,
            Italy,
            Japan,
            Kuwait,
            SriLanka,
            Lithuania,
            Mexico,
            Macedonia,
            Malaysia,
            Netherlands,
            Norway,
            NewZealand,
            Peru,
            Philippines,
            Poland,
            PuertoRico,
            Portugal,
            Palestine,
            Qatar,
            Romania,
            Russia,
            SaudiArabia,
            Singapore,
            Sweden,
            Thailand,
            Turkey,
            UnitedStates,
            Venezuela,
            Vietnam = 52
        }

        public enum RequestType
        {
            Friend,
            InviteSyndicate,
            JoinSyndicate,
            TeamApply,
            TeamInvite,
            Trade,
            Marriage,
            TradePartner,
            Guide,
            Family,
            CoupleInteraction
        }
    }
}
