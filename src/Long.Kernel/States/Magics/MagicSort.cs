namespace Long.Kernel.States.Magics
{
    public enum MagicSort
    {
        Attack = 1,
        Recruit = 2, // support auto active.
        Cross = 3,
        Fan = 4, // support auto active(random).
        Bomb = 5,
        Attachstatus = 6,
        Detachstatus = 7,
        Square = 8,
        Jumpattack = 9,   // move, a-lock
        Randomtrans = 10, // move, a-lock
        Dispatchxp = 11,
        Collide = 12,   // move, a-lock & b-synchro
        Serialcut = 13, // auto active only.
        Line = 14,      // support auto active(random).
        Atkrange = 15,  // auto active only, forever active.
        Atkstatus = 16, // support auto active, random active.
        Callteammember = 17,
        Recordtransspell = 18,
        Transform = 19,
        Addmana = 20, // support self target only.
        Laytrap = 21,
        Dance = 22,       // ÌøÎè(only use for client)
        Callpet = 23,     // ÕÙ»½ÊÞ
        Vampire = 24,     // ÎüÑª£¬power is percent award. use for call pet
        Instead = 25,     // ÌæÉí. use for call pet
        Declife = 26,     // ¿ÛÑª(µ±Ç°ÑªµÄ±ÈÀý)
        Groundsting = 27, // µØ´Ì,
        Vortex = 28,
        Activateswitch = 29,
        Spook = 30,
        Warcry = 31,
        Riding = 32,
        AttachstatusArea = 34,
        FanStatus = 35, // fuck tq i dont know what name to use _|_
        BombStatus = 36,
        ChainXp = 37,
        Knockback = 38,
        Dashwhirl = 40,
        Perseverance = 41,
		ArrowHail = 44,
        Selfdetach = 46,
        Detachbadstatus = 47,
        CloseLine = 48,
        Compassion = 50,
        Teamflag = 51,
        Increaseblock = 52,
        Oblivion = 53,
        Stunbomb = 54,
        Tripleattack = 55,
        ScurvyBomb,
        CannonBarrage,
        BlackSpot,
        AdrenalineRush = 59,
        GaleBomb,
        Dashdeadmark = 61,
        KrakensRevenge,
        BlackbeardsRage,
        Mountwhirl = 64,
        Targetdrag = 65,
        KineticSpark = 67,
        Assassinvortex = 68,
        Blisteringwave = 69,
        BreathFocus = 70,
        FatalCross = 71,
        FatalSpin = 73
    }
}
