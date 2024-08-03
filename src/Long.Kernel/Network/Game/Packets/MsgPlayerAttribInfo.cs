using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgPlayerAttribInfo : MsgBase<GameClient>
    {
        public MsgPlayerAttribInfo()
        {

        }

        public MsgPlayerAttribInfo(Character user)
        {
            Timestamp = Environment.TickCount;
            Identity = user.Identity;
            Life = (int)user.MaxLife;
            Mana = (int)user.MaxMana;
            MaxAttack = user.MaxAttack;
            MinAttack = user.MinAttack;
            PhysicalDefense = user.Defense;
            MagicalAttack = user.MagicAttack;
            MagicalDefense = user.MagicDefense;
            Dodge = user.Dodge;
            Agility = user.Agility;
            Accuracy = user.Accuracy;
            DragonGemBonus = user.DragonGemBonus;
            PhoenixGemBonus = user.PhoenixGemBonus;
            MagicDefenseBonus = user.MagicDefenseBonus;
            TortoiseGemBonus = user.TortoiseGemBonus;
            Blessing = user.Blessing;

            CriticalStrike = user.CriticalStrike;
            SkillCriticalStrike = user.SkillCriticalStrike;
            Immunity = user.Immunity;
            Counteraction = user.Counteraction;
            Block = user.Block;
            Penetration = user.Penetration;
            Breakthrough = user.Breakthrough;
            Detoxication = user.Detoxication;

            MetalResistance = user.MetalResistance;
            WaterResistance = user.WaterResistance;
            FireResistance = user.FireResistance;
            EarthResistance = user.EarthResistance;
            WoodResistance = user.WoodResistance;

            FinalPhysicalDamage = user.AddFinalAttack;
            FinalPhysicalDefense = user.AddFinalDefense;
            FinalMagicalDamage = user.AddFinalMAttack;
            FinalMagicalDefense = user.AddFinalMDefense;
        }

        public int Timestamp { get; set; }
        public uint Identity { get; set; }
        public int Life { get; set; }
        public int Mana { get; set; }
        public int MaxAttack { get; set; }
        public int MinAttack { get; set; }
        public int PhysicalDefense { get; set; }
        public int MagicalAttack { get; set; }
        public int MagicalDefense { get; set; }
        public int Dodge { get; set; }
        public int Agility { get; set; }
        public int Accuracy { get; set; }
        public int DragonGemBonus { get; set; }
        public int PhoenixGemBonus { get; set; }
        public int MagicDefenseBonus { get; set; }
        public int TortoiseGemBonus { get; set; }
        public int Blessing { get; set; }
        public int CriticalStrike { get; set; }
        public int SkillCriticalStrike { get; set; }
        public int Immunity { get; set; }
        public int Penetration { get; set; }
        public int Block { get; set; }
        public int Breakthrough { get; set; }
        public int Counteraction { get; set; }
        public int Detoxication { get; set; }
        public int FinalPhysicalDamage { get; set; }
        public int FinalMagicalDamage { get; set; }
        public int FinalPhysicalDefense { get; set; }
        public int FinalMagicalDefense { get; set; }
        public int MetalResistance { get; set; }
        public int WoodResistance { get; set; }
        public int WaterResistance { get; set; }
        public int FireResistance { get; set; }
        public int EarthResistance { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32();
            Identity = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgPlayerAttribInfo);
            writer.Write(Timestamp); // 4
            writer.Write(Identity); // 8
            writer.Write(Life); // 12
            writer.Write(Mana); // 16
            writer.Write(MaxAttack); // 20
            writer.Write(MinAttack); // 24
            writer.Write(PhysicalDefense); // 28
            writer.Write(MagicalAttack); // 32
            writer.Write(MagicalDefense); // 36
            writer.Write(Dodge); // 40
            writer.Write(Agility); // 44
            writer.Write(Accuracy); // 48
            writer.Write(DragonGemBonus); // 52
            writer.Write(PhoenixGemBonus); // 56
            writer.Write(MagicDefenseBonus); // 60
            writer.Write(TortoiseGemBonus); // 64
            writer.Write(Blessing); // 68
            writer.Write(CriticalStrike); // 72
            writer.Write(SkillCriticalStrike); // 76
            writer.Write(Immunity); // 80
            writer.Write(Penetration); // 84
            writer.Write(Block); // 88
            writer.Write(Breakthrough); // 92
            writer.Write(Counteraction); // 96
            writer.Write(Detoxication); // 100
            writer.Write(FinalPhysicalDamage); // 104
            writer.Write(FinalMagicalDamage); // 108
            writer.Write(FinalPhysicalDefense); // 112
            writer.Write(FinalMagicalDefense); // 116
            writer.Write(MetalResistance); // 120
            writer.Write(WoodResistance); // 124
            writer.Write(WaterResistance); // 128
            writer.Write(FireResistance); // 132
            writer.Write(EarthResistance); // 136
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user.CurrentServer.HasValue && user.CurrentServerID != RealmManager.ServerIdentity)
            {
                return user.SendCrossMsgAsync(this);
            }
            
            if (Identity == client.Character.Identity)
            {
                return user.SendAsync(new MsgPlayerAttribInfo(user));
            }
            else
            {
                var target = RoleManager.GetUser(Identity);
                if (target != null)
                {
                    return user.SendAsync(new MsgPlayerAttribInfo(target));
                }
                return Task.CompletedTask;
            }
        }
    }
}
