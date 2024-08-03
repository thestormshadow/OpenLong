namespace Long.Kernel.States.Items.Status
{
    public enum ItemStatusAttribute : uint
    {
        None,
        MagicDefense = 1,
        CriticalStrike = 2,
        SkillCriticalStrike = 3,
        Immunity = 4,
        Breakthrough = 5,
        Counteraction = 6,
        Detoxication = 7,
        Block = 8,
        Penetration = 9,
        Intensification = 10,
        FireResist = 11,
        WaterResist = 12,
        WoodResist = 13,
        MetalResist = 14,
        EarthResist = 15,
        FinalMagicAttack = 16,
        FinalMagicDamage = 17,
        FinalMagicDefense,
        FinalAttack,
        FinalDamage,
        FinalDefense
    }

    public enum ItemStatusPosition
    {
        None,
        Headwear,
        Neck,
        Armor,
        SingleHand,
        DoubleHand,
        Shield,
        Ring,
        Bracelet = Ring,
        Shoes
    }
}