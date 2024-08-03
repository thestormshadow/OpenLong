using Long.World.Roles;
using System.Collections.Concurrent;

namespace Long.World.Map
{
    /// <summary>
    ///     A block is a set of the map which will hold a collection with all entities in an area. This will help us
    ///     iterating over a limited number of roles when trying to process AI and movement. Instead of iterating a list with
    ///     thousand roles in the entire map, we'll just iterate the blocks around us.
    /// </summary>
    public sealed class MapBlock<TRole, TUser> where TRole : WorldObject
    {
        private static int nextBlockId = 1;

        public const int BLOCK_SIZE = 18;

        private int processableUnit;
        private readonly int blockId;

        public ConcurrentDictionary<uint, TRole> RoleSet = new();

        public bool ProcessableChunk => processableUnit > 0;

        public int Index { get; }

        public MapBlock(int index)
        {
            blockId = nextBlockId++;
            Index = index;
        }

        public int Id => blockId;

        public bool Add(TRole role)
        {
            bool addRole = RoleSet.TryAdd(role.Identity, role);
            if (addRole)
            {
                Interlocked.Increment(ref processableUnit);
            }
            return addRole;
        }

        public bool Remove(TRole role)
        {
            return Remove(role.Identity);
        }

        public bool Remove(uint idRole)
        {
            bool removeRole = RoleSet.TryRemove(idRole, out TRole role);
            if (removeRole)
            {
                Interlocked.Decrement(ref processableUnit);
            }
            return removeRole;
        }
    }
}
