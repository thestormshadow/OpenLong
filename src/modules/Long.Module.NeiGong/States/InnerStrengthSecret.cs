using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Modules.Systems.NeiGong;

namespace Long.Module.NeiGong.States
{
    public sealed class InnerStrengthSecret
    {
        private readonly DbInnerStrenghtSecret innerStrenghtSecret;
        private readonly List<InnerStrengthPower> innerPowers = new();

        public InnerStrengthSecret(DbInnerStrenghtSecret innerStrenghtSecret)
        {
            this.innerStrenghtSecret = innerStrenghtSecret;
        }

        public byte SecretType => innerStrenghtSecret.SecretType;

        public bool IsPerfect => innerPowers.Count > 0 && innerPowers.All(x => x.IsPerfect);

        public int TotalValue => innerPowers.Sum(x => x.Value);

        public bool HasBook(int type)
        {
            return innerPowers.Any(x => x.Identity == type);
        }

        public bool AddBook(InnerStrengthPower power)
        {
            if (HasBook(power.Identity))
            {
                return false;
            }
            innerPowers.Add(power);
            return true;
        }

        public InnerStrengthPower GetBook(int type)
        {
            return innerPowers.FirstOrDefault(x => x.Identity == type);
        }

        public List<InnerStrengthPower> GetPowers()
        {
            return new List<InnerStrengthPower>(innerPowers);
        }

        public Dictionary<INeiGongManager.InnerStrengthAttrType, int> GetPower()
        {
            Dictionary<INeiGongManager.InnerStrengthAttrType, int> powers = new();
            foreach (var power in innerPowers)
            {
                if (power.MaxLife > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.MaxLife, power.MaxLife);
                }
                if (power.PhysicAttackNew > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Attack, power.PhysicAttackNew);
                }
                if (power.MagicAttack > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.MagicAttack, power.MagicAttack);
                }
                if (power.PhysicDefenseNew > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Defense, power.PhysicDefenseNew);
                }
                if (power.MagicDefense > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.MagicDefense, power.MagicDefense);
                }
                if (power.FinalPhysicAdd > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalPhysicalDamage, power.FinalPhysicAdd);
                }
                if (power.FinalMagicAdd > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalMagicalDamage, power.FinalMagicAdd);
                }
                if (power.FinalPhysicReduce > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalPhysicalDefense, power.FinalPhysicReduce);
                }
                if (power.FinalMagicReduce > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalMagicalDefense, power.FinalMagicReduce);
                }
                if (power.PhysicCrit > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.CriticalStrike, power.PhysicCrit);
                }
                if (power.MagicCrit > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.SkillCriticalStrike, power.MagicCrit);
                }
                if (power.DefenseCrit > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Immunity, power.DefenseCrit);
                }
                if (power.SmashRate > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Breakthrough, power.SmashRate);
                }
                if (power.FirmDefenseRate > 0)
                {
                    AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Counteraction, power.FirmDefenseRate);
                }
            }

            if (!IsPerfect)
            {
                return powers;
            }

            var secretType = NeiGongManager.QuerySecretType(SecretType);
            if (secretType.MaxLife > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.MaxLife, (int)secretType.MaxLife);
            }
            if (secretType.PhysicAttackNew > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Attack, (int)secretType.PhysicAttackNew);
            }
            if (secretType.MagicAttack > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.MagicAttack, (int)secretType.MagicAttack);
            }
            if (secretType.PhysicDefenseNew > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Defense, (int)secretType.PhysicDefenseNew);
            }
            if (secretType.MagicDefense > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.MagicDefense, (int)secretType.MagicDefense);
            }
            if (secretType.FinalPhysicAdd > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalPhysicalDamage, secretType.FinalPhysicAdd);
            }
            if (secretType.FinalMagicAdd > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalMagicalDamage, secretType.FinalMagicAdd);
            }
            if (secretType.FinalPhysicReduce > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalPhysicalDefense, secretType.FinalPhysicReduce);
            }
            if (secretType.FinalMagicReduce > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.FinalMagicalDefense, secretType.FinalMagicReduce);
            }
            if (secretType.PhysicCrit > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.CriticalStrike, secretType.PhysicCrit);
            }
            if (secretType.MagicCrit > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.SkillCriticalStrike, secretType.MagicCrit);
            }
            if (secretType.DefenseCrit > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Immunity, secretType.DefenseCrit);
            }
            if (secretType.SmashRate > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Breakthrough, secretType.SmashRate);
            }
            if (secretType.FirmDefenseRate > 0)
            {
                AddOrIncrease(powers, INeiGongManager.InnerStrengthAttrType.Counteraction, secretType.FirmDefenseRate);
            }
            return powers;
        }

        private void AddOrIncrease(Dictionary<INeiGongManager.InnerStrengthAttrType, int> target, INeiGongManager.InnerStrengthAttrType type, int power)
        {
            if (target.ContainsKey(type))
            {
                target[type] += power;
            }
            else
            {
                target.Add(type, power);
            }
        }

        public Task SaveAsync()
        {
            if (innerStrenghtSecret.Identity == 0)
            {
                return ServerDbContext.CreateAsync(innerStrenghtSecret);
            }
            return ServerDbContext.UpdateAsync(innerStrenghtSecret);
        }
    }
}
