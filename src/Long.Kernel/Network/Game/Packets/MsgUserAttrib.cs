using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgUserAttrib : MsgBase<GameClient>
    {
        private readonly List<UserAttribute> Attributes = new();

        public MsgUserAttrib()
        {
        }

        public MsgUserAttrib(uint idRole, ClientUpdateType type, ulong value)
        {
            Identity = idRole;
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, value));
        }

        public MsgUserAttrib(uint idRole, ClientUpdateType type, uint value0, uint value1)
        {
            Identity = idRole;
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, value0, value1));
        }

        public MsgUserAttrib(uint idRole, ClientUpdateType type, ulong value, ulong value2)
        {
            Identity = idRole;
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, value, value2));
        }

        public MsgUserAttrib(uint idRole, ClientUpdateType type, ulong value, ulong value2, ulong value3)
        {
            Identity = idRole;
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, value, value2, value3));
        }

        public MsgUserAttrib(uint idRole, ClientUpdateType type, uint value0, uint value1, uint value3, uint value4)
        {
            Identity = idRole;
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, value0, value1, value3, value4));
        }

        public MsgUserAttrib(uint idRole, ClientUpdateType type, uint value0, uint value1, uint value3, uint value4, uint value5, uint value6)
        {
            Identity = idRole;
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, value0, value1, value3, value4, value5, value6));
        }

        public int Timestamp { get; set; }
        public uint Identity { get; set; }
        public int Amount { get; set; }

        public List<UserAttribute> GetAttributes()
        {
            return Attributes.ToList();
        }

        public void Append(ClientUpdateType type, ulong data)
        {
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, data));
        }

        public void Append(ClientUpdateType type, ulong data, ulong data2)
        {
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, data, data2));
        }

        public void Append(ClientUpdateType type, uint data, uint data2)
        {
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, data, data2));
        }

        public void Append(ClientUpdateType type, ulong data, ulong data2, ulong data3)
        {
            Amount++;
            Attributes.Add(new UserAttribute((uint)type, data, data2, data3));
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgUserAttrib);
            writer.Write(Environment.TickCount); // 4
            writer.Write(Identity); // 8
            Amount = Attributes.Count;
            writer.Write(Amount); // 12
            for (var i = 0; i < Amount; i++)
            {
                writer.Write(Attributes[i].Type);
                writer.Write(Attributes[i].Data);
                writer.Write(Attributes[i].Data2);
                writer.Write(Attributes[i].Data3);
            }

            return writer.ToArray();
        }

        public readonly struct UserAttribute
        {
            public UserAttribute(uint type, ulong data)
            {
                Type = type;
                Data = data;
                Data2 = 0;
                Data3 = 0;
            }

            public UserAttribute(uint type, ulong data, ulong data2)
            {
                Type = type;
                Data = data;
                Data2 = data2;
                Data3 = 0;
            }

            public UserAttribute(uint type, ulong data, ulong data2, ulong data3)
            {
                Type = type;
                Data = data;
                Data2 = data2;
                Data3 = data3;
            }

            public UserAttribute(uint type, uint left, uint right)
            {
                Type = type;
                Data = ((ulong)left << 32) | right;
                Data2 = 0;
                Data3 = 0;
            }

            public UserAttribute(uint type, uint left, uint right, uint left2, uint right2)
            {
                Type = type;
                Data = ((ulong)left << 32) | right;
                Data2 = ((ulong)left2 << 32) | right2;
                Data3 = 0;
            }

            public UserAttribute(uint type, uint left, uint right, uint left2, uint right2, uint left3, uint right3)
            {
                Type = type;
                Data = ((ulong)left << 32) | right;
                Data2 = ((ulong)left2 << 32) | right2;
                Data3 = ((ulong)left3 << 32) | right3;
            }

            public readonly uint Type;
            public readonly ulong Data;
            public readonly ulong Data2;
            public readonly ulong Data3;
        }
    }

    public enum ClientUpdateType
    {
		Hitpoints = 0,
        TeamMemberMaxHP = 1,
        Mana = 2,
        MaxMana = 3,
        Money = 4,
        Experience = 5,
        PkPoints = 6,
        Class = 7,
        Stamina = 8,
        Atributes = 10,
        Mesh,
        Level,
        Spirit,
        Vitality,
        Strength,
        Agility,
        HeavensBlessing,
        MultipleExpTimer,
        SyndicateProffer,
        CursedTimer = 20,
        Reborn = 22,
        Virtue, // ?
        StatusFlag = 25,
        HairStyle = 26,
        XpCircle = 27,
        LuckyTimeTimer = 28,
        ConquerPoints = 29,
        OnlineTraining = 31,
        Enthrallment, // ? probably chinese login time limit function
        SynchronizeOnlineTime, // ? probably chinese login time limit function
        WallowTime, // ? probably chinese login time limit function
        ResetWallow, // ? probably chinese login time limit function
        ExtraBattlePower = 36,
        MeteLev,
        Merchant = 38,
        VipLevel = 39,
        QuizPoints = 40,
        EnlightenPoints = 41,
        FamilySharedBattlePower = 42,
        DoublePKPartnerLife,
        TotemPoleBattlePower = 44,
        BoundConquerPoints = 45,
        RidePetPoint = 47,
        FactionBattleRestTime,
        AzureShield = 49,
        PreviousProfession = 50,
        FirstProfession = 51,
        TeamID = 52,
        SoulShackleTimer = 54,
        Fatigue = 55,
        PhysicCritRate = 59,
        MagicCritRate = 60,
        AntiCritRate = 61,
        SmashAttackRate = 62,
        FirmDefenseRate = 63,
        AddMaxLife = 64,
        AddPhysicAttack = 65,
        AddMagicAttack = 66,
        AddPhysicDamageReduce = 67,
        AddMagicDamageReduce = 68,
        AddFinalPhysicDamage = 69,
        AddFinalMagicDamage = 70,
        PrivilegeFlag = 71,
        ExpProtection = 73,
        DragonFury = 74,
        DragonSwing = 75,
        HouseLev = 76,
        NeiGongValue = 77,
        AppendIcon = 78,
        CurrentSashSlots = 79,
        MaximumSashSlots = 80,
        LeagueID = 81,
        UnionContributionPosition = 82,
        UnionOfficialPosition = 83,
        UnionContributionValue = 84,
        SetCustomLookface, // 85 ???
        ManageTitleData = 87,
        EquipTitle = 88,
        TitleScore = 89,
        Anger = 90,
        XpList = 101,

        Vigor = 10000
    }

    public enum AttrUpdateType : uint
    {
        Accelerated = 52,
        Decelerated = 53,
        Flustered = 54,
        Sprint = 55,
        DivineShield = 57,
        Stun = 58,
        Freeze = 59,
        Dizzy = 60,
        AzureShield = 93,
        SoulShackle = 111
    }
}
