namespace Long.Kernel.States.Magics
{
    [Flags]
    public enum MagicAutoActive
    {
        None = 0,
        Kill = 0x1,
        OnAttack = 0x4,
        OnBeAttack = 0x8,
        AfterSkill = 0x40
    }
}
