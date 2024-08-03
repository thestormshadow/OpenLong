namespace Long.Shared.Managers
{
    public sealed class IdentityManager
    {
        public const int SCENEID_FIRST = 1;
        public const int SYSNPCID_FIRST = 1;
        public const int SYSNPCID_LAST = 99999;
        public const int DYNANPCID_FIRST = 100000;
        public const int DYNANPCID_LAST = 199999;
        public const int SCENE_NPC_MIN = 200000;
        public const int SCENE_NPC_MAX = 299999;
        public const int SCENEID_LAST = 299999;

        public const int NPCSERVERID_FIRST = 400001;
        public const int MONSTERID_FIRST = 400001;
        public const int MONSTERID_LAST = 499999;
        public const int PETID_FIRST = 500001;
        public const int PETID_LAST = 599999;
        public const int NPCSERVERID_LAST = 699999;

        public const int CALLPETID_FIRST = 700001;
        public const int CALLPETID_LAST = 799999;

        public const int MAPITEM_FIRST = 800001;
        public const int MAPITEM_LAST = 899999;

        public const int TRAPID_FIRST = 900001;
        public const int MAGICTRAPID_FIRST = 900001;
        public const int MAGICTRAPID_LAST = 989999;
        public const int SYSTRAPID_FIRST = 990001;
        public const int SYSTRAPID_LAST = 999999;
        public const int TRAPID_LAST = 999999;

        public const int DYNAMAP_FIRST = 1000000;

        public const int PLAYER_ID_FIRST = 1000000;
        public const int PLAYER_ID_LAST = 1999999999;

        public static IdentityManager MapItem = new(MAPITEM_FIRST, MAPITEM_LAST);
        public static IdentityManager Pet = new(CALLPETID_FIRST, CALLPETID_LAST);
        public static IdentityManager Furniture = new(SCENE_NPC_MIN, SCENE_NPC_MAX);
        public static IdentityManager Traps = new(MAGICTRAPID_FIRST, MAGICTRAPID_LAST);
        public static IdentityManager Monster = new(MONSTERID_FIRST, MONSTERID_LAST);

        public static IdentityManager Instances = new(100_000_000, 101_000_000);

        private readonly Queue<long> queue = new();
        private readonly long max = uint.MaxValue;
        private readonly long min;

        private object IdentityLock = new();

        public IdentityManager(long min, long max)
        {
            this.min = min;
            this.max = max;

            for (long i = this.min; i <= this.max; i++)
            {
                queue.Enqueue(i);
            }
        }

        public long GetNextIdentity
        {
            get
            {
                lock (IdentityLock)
                {
                    if (queue.TryDequeue(out long result))
                    {
                        return result;
                    }
                    return 0;
                }
            }
        }

        public void ReturnIdentity(long id)
        {
            if (id < min || id > max)
            {
                return;
            }

            lock (IdentityLock)
            {
                if (!queue.Contains(id))
                {
                    queue.Enqueue(id);
                }
            }
        }

        public int IdentitiesCount()
        {
            lock (IdentityLock)
            {
                return queue.Count;
            }
        }
    }
}
