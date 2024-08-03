using Long.Database.Entities;
using Long.Kernel.States.Items;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgDetainItemInfo : MsgBase<GameClient>
    {
        public MsgDetainItemInfo(DbDetainedItem dbDetainItem, Item item, Mode mode)
        {
            Identity = dbDetainItem.Identity;
            ItemIdentity = item?.Identity ?? 0;
            ItemType = item?.Type ?? 0;
            Amount = item?.Durability ?? 0;
            AmountLimit = item?.MaximumDurability ?? 0;
            Action = mode;
            SocketProgress = item?.SocketProgress ?? 0;
            SocketOne = (byte)(item?.SocketOne ?? Item.SocketGem.NoSocket);
            SocketTwo = (byte)(item?.SocketTwo ?? Item.SocketGem.NoSocket);
            Effect = (ushort)(item?.Effect ?? Item.ItemEffect.None);
            Addition = item?.Plus ?? 0;
            Blessing = (byte)(item?.Blessing ?? 0);
            Bound = item?.IsBound ?? false;
            Enchantment = item?.Enchantment ?? 0;
            Suspicious = item?.IsSuspicious() ?? false;
            Locked = item?.IsLocked() ?? false;
            Color = (byte)(item?.Color ?? Item.ItemColor.Orange);

            OwnerIdentity = dbDetainItem.HunterIdentity;
            OwnerName = dbDetainItem.HunterName;
            TargetIdentity = dbDetainItem.TargetIdentity;
            TargetName = dbDetainItem.TargetName;

            DetainDate = int.Parse(UnixTimestamp.ToDateTime(dbDetainItem.HuntTime).ToString("yyyyMMdd"));
            Cost = dbDetainItem.RedeemPrice;
            if (item != null)
            {
                RemainingDays = Math.Max(0, Math.Min(8, (int)Math.Ceiling((DateTime.Now - UnixTimestamp.ToDateTime(dbDetainItem.HuntTime)).TotalDays)));
            }
            else
            {
                Reward = dbDetainItem.RedeemPrice;
                Expired = true;
                RemainingDays = 7;
            }
        }

        public uint Identity { get; set; }
        public uint ItemIdentity { get; set; }
        public uint ItemType { get; set; }
        public ushort Amount { get; set; }
        public ushort AmountLimit { get; set; }
        public Mode Action { get; set; }
        public uint SocketProgress { get; set; }
        public byte SocketOne { get; set; }
        public byte SocketTwo { get; set; }
        public ushort Effect { get; set; }
        public byte Addition { get; set; }
        public byte Blessing { get; set; }
        public bool Bound { get; set; }
        public byte Enchantment { get; set; }
        public bool Suspicious { get; set; }
        public bool Locked { get; set; }
        public byte Color { get; set; }
        public uint OwnerIdentity { get; set; }
        public string OwnerName { get; set; }
        public uint TargetIdentity { get; set; }
        public string TargetName { get; set; }
        public int DetainDate { get; set; }
        public bool Expired { get; set; }
        public int Reward { get; set; }
        public int Cost { get; set; }
        public int RemainingDays { get; set; }

        public override byte[] Encode()
        {
            PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgDetainItemInfo);
            writer.Write(Identity);                      // 4
            writer.Write(ItemIdentity);                  // 8
            writer.Write(ItemType);                      // 12
            writer.Write(Amount);                        // 16
            writer.Write(AmountLimit);                   // 18
            writer.Write((int)Action);                  // 20
            writer.Write(SocketProgress);                // 24
            writer.Write(SocketOne);                     // 28
            writer.Write(SocketTwo);                     // 29
            writer.Write(Effect);                   // 30
            writer.Write(new byte[5]); // 32
            writer.Write(Addition);                      // 37
            writer.Write(Blessing);                      // 38
            writer.Write(Bound);                         // 39 
            writer.Write(Enchantment);                   // 40
            writer.Write(0);                             // 41
            writer.Write((ushort)(Suspicious ? 1 : 0)); // 45
            writer.Write((byte)(Locked ? 1 : 0));     // 47
            writer.Write(0);                            // 48
            writer.Write((int)Color);                   // 52
            writer.Write(OwnerIdentity);                 // 56
            writer.Write(OwnerName ?? StrNone, 16);                 // 60
            writer.Write(TargetIdentity);                // 76
            writer.Write(TargetName ?? StrNone, 16);                // 80
            writer.Write(0);                  // 96
            writer.Write(Reward);                // 100
            writer.Write(Cost);            // 104
            writer.Write(RemainingDays);   // 108
            writer.Write(Expired ? 1 : 0); // 112
            writer.Write(DetainDate);      // 116
            return writer.ToArray();
        }

        public const int MAX_REDEEM_DAYS = 7;
        public const int MAX_REDEEM_SECONDS = 60 * 60 * 24 * MAX_REDEEM_DAYS;

        public enum Mode
        {
            DetainPage,
            ClaimPage,
            ReadyToClaim
        }
    }
}
