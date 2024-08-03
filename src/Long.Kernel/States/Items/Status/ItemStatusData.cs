using Long.Database.Entities;
using Long.Kernel.Database;

namespace Long.Kernel.States.Items.Status
{
    public class ItemStatusData
    {
        public DbItemStatus ItemStatus { get; init; }
        public DbItemtype ItemType { get; init; }

        public uint Power1 => ItemStatus?.Power1 ?? 0;
        public ItemStatusAttribute Attribute1 { get; set; }

        public uint Power2 => ItemStatus?.Power2 ?? 0;
        public ItemStatusAttribute Attribute2 { get; set; }

        public bool IsPermanent => ItemStatus?.RealSeconds == 0;
        public int ExpiresIn => (int)((ItemStatus?.RealSeconds ?? 0) - UnixTimestamp.Now);
        public bool HasExpired => !IsPermanent && ExpiresIn <= 0;

        public Task SaveAsync()
        {
            if (ItemStatus.Id == 0)
            {
                return ServerDbContext.CreateAsync(ItemStatus);
            }
            return ServerDbContext.UpdateAsync(ItemStatus);
        }

        public Task DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(ItemStatus);
        }
    }
}
