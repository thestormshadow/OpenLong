namespace Long.World.Enums
{
    [Flags]
    public enum MapTypeFlag : ulong
    {
        Normal = 0,
        PkField = 0x1,          //0x1 1
        ChangeMapDisable = 0x2, //0x2 2
        RecordDisable = 0x4,    //0x4 4 
        PkDisable = 0x8,        //0x8 8
        BoothEnable = 0x10,     //0x10 16
        TeamDisable = 0x20,     //0x20 32
        TeleportDisable = 0x40, // 0x40 64
        GuildMap = 0x80,        // 0x80 128
        PrisonMap = 0x100,      // 0x100 256
        WingDisable = 0x200,    // 0x200 512
        Family = 0x400,         // 0x400 1024
        MineField = 0x800,      // 0x800 2048
        CallNewbieDisable = 0x1000,        // 0x1000 4098
        RebornNowEnable = 0x2000,    // 0x2000 8196
        NewbieProtect = 0x4000,    // 0x4000 16392
        ArenicMap = 0x200000,
        DoublePkMap = 0x400000,
        RaceTrackMap = 0x2000000,
        FamilyArenicMap = 0x8000000,
        FactionPkMap = 0x10000000,
        EliteMap = 0x20000000,
        TeamPkArenicMap = 0x100000000,
        TeamArenaMap = 0x2000000000,
        BattleEffectLimitMap = 0x4000000000,
        TeamPopPkMap = 0x8000000000,
        NoExpMap = 0x10000000000,
        GoldenLeagueAdditionLevelLimit = 0x20000000000,
        ForbidCampMap = 0x100000000000,
        GoldenLeagueMap = 0x200000000000,
        JiangHuMap = 0x400000000000
    }
}
