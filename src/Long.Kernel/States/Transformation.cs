using Long.Database.Entities;
using Long.Kernel.States.Status;
using Long.Shared.Mathematics;

namespace Long.Kernel.States
{
    public sealed class Transformation
    {
        private DbMonstertype monsterType;
        private readonly Role role;

        public Transformation(Role owner)
        {
            role = owner;
        }

        public ushort Life { get; set; }

        public int MaxLife => monsterType.Life;

        public int MinAttack => monsterType.AttackMin;

        public int MaxAttack => monsterType.AttackMax;

        public int Attack => (MinAttack + MaxAttack) / 2;

        public int Defense => monsterType.Defence;

        public uint Defense2 => monsterType.Defence2;

        public uint Dexterity => monsterType.Dexterity;

        public uint Dodge => monsterType.Dodge;

        public int MagicDefense => monsterType.MagicDef;

        public int InterAtkRate
        {
            get
            {
                int rate = IntervalAtkRate;
                IStatus status = role.QueryStatus(StatusSet.CYCLONE);
                if (status != null)
                {
                    rate = Calculations.CutTrail(0, Calculations.AdjustDataEx(rate, status.Power));
                }

                return rate;
            }
        }

        public int IntervalAtkRate => monsterType.AttackSpeed;

        public int MagicHitRate => (int)monsterType.MagicHitrate;
        public int Lookface => monsterType.Lookface;

        public bool IsJumpEnable => (monsterType.AttackUser & Monster.ATKUSER_JUMP) != 0;

        public bool IsMoveEnable => (monsterType.AttackUser & Monster.ATKUSER_FIXED) == 0;

        public bool Create(DbMonstertype pTrans)
        {
            if (role == null || pTrans == null || pTrans.Life <= 0)
            {
                return false;
            }

            monsterType = pTrans;
            Life = (ushort)Calculations.CutTrail(1, Calculations.MulDiv(role.Life, MaxLife, role.MaxLife));

            return true;
        }

        public int GetAttackRange(int nTargetSizeAdd)
        {
            return (int)((monsterType.AttackRange + GetSizeAdd() + nTargetSizeAdd + 1) / 2);
        }

        public uint GetSizeAdd()
        {
            return monsterType.SizeAdd;
        }
    }
}
