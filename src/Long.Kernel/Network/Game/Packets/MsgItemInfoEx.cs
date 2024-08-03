using Long.Kernel.States.Items;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgItemInfoEx : MsgBase<GameClient>
    {
        public MsgItemInfoEx(Item item, ViewMode mode)
        {
            Identity = item.Identity;
            TargetIdentity = item.PlayerIdentity;
            ItemType = item.Type;
            Amount = item.Durability;
            AmountLimit = item.MaximumDurability;
            Position = (ushort)item.Position;
            SocketOne = (byte)item.SocketOne;
            SocketTwo = (byte)item.SocketTwo;
            Effect = (byte)item.Effect;
            Addition = item.Plus;
            Blessing = (byte)item.Blessing;
            IsBound = item.IsBound;
            Enchantment = item.Enchantment;
            IsSuspicious = item.IsSuspicious();
            IsLocked = item.IsLocked();
            Color = (byte)item.Color;
            Mode = mode;
            SocketProgress = item.SocketProgress;
            CompositionProgress = item.CompositionProgress;
            if (item.RemainingSeconds != 0)
            {
                RemainingTime = item.RemainingSeconds;
            }
            else if (item.DeleteTime != 0)
            {
                SaveTime = item.DeleteTime;
            }
            StackAmount = (ushort)item.AccumulateNum;
            IsInscribed = item.SyndicateIdentity != 0;
            PerfectionLevel = item.PerfectionLevel;
            PerfectionProgress = item.PerfectionProgress;
            PerfectionOwnerId = item.PerfectionOwnerGuid;
            PerfectionOwnerName = item.PerfectionOwnerName;
            PerfectionOwnerSignature = item.PerfectionSignature;
        }

        public uint Identity { get; set; }
        public uint TargetIdentity { get; set; }
        public ulong Price { get; set; }
        public uint ItemType { get; set; }
        public ushort Amount { get; set; }
        public ushort AmountLimit { get; set; }
        public ViewMode Mode { get; set; }
        public ushort Position { get; set; }
        public uint SocketProgress { get; set; }
        public byte SocketOne { get; set; }
        public byte SocketTwo { get; set; }
        public byte Effect { get; set; }
        public byte Addition { get; set; }
        public byte Blessing { get; set; }
        public bool IsBound { get; set; }
        public byte Enchantment { get; set; }
        public bool IsSuspicious { get; set; }
        public bool IsLocked { get; set; }
        public uint CompositionProgress { get; set; }
        public bool IsInscribed { get; set; }
        public byte Color { get; set; }
        public int RemainingTime { get; set; }
        public int SaveTime { get; set; }
        public ushort StackAmount { get; set; }

        public uint PerfectionLevel { get; set; }
        public uint PerfectionProgress { get; set; }
        public uint PerfectionOwnerId { get; set; }
        public string PerfectionOwnerName { get; set; } = string.Empty;
        public string PerfectionOwnerSignature { get; set; } = string.Empty;

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgItemInfoEx);
            writer.Write(Identity);             // 4
            writer.Write(TargetIdentity);       // 8
            writer.Write(Price);                // 12
            writer.Write(ItemType);             // 20
            writer.Write(Amount);               // 24
            writer.Write(AmountLimit);          // 26
            writer.Write((ushort)Mode);         // 28
            writer.Write((byte)Position);       // 30
            writer.Write(SocketProgress);       // 31
            writer.Write(SocketOne);            // 35
            writer.Write(SocketTwo);            // 36
            writer.Write(Effect);               // 37
            writer.Write(new byte[4]);          // 38
            writer.Write(Addition);             // 42
            writer.Write(Blessing);             // 43
            writer.Write(IsBound);              // 44
            writer.Write(Enchantment);          // 45
            writer.Write(new byte[5]);          // 46
            writer.Write(IsSuspicious);         // 51
            writer.Write(IsLocked);             // 52
            writer.Write((byte)0);              // 53
            writer.Write((ushort)Color);        // 54
            writer.Write(CompositionProgress);  // 56
            writer.Write(IsInscribed ? 1 : 0);  // 60 Is Inscribed?
            writer.Write(RemainingTime);        // 64
            writer.Write(SaveTime);             // 68
            writer.Write(StackAmount);          // 72
            writer.Write(0);                    // 74 TempGodEquipTypeID
            writer.Write(0);                    // 78 GodEquipTypeID
            writer.Write(PerfectionLevel);      // 82
            writer.Write(PerfectionProgress);   // 86 RefineExp
            writer.Write(PerfectionOwnerId);    // 90 RefineOwner
            writer.Write(PerfectionOwnerName, 16); // 94
            writer.Write(PerfectionOwnerSignature, 32); // 110
            //writer.Write(Purification);         // 76
            return writer.ToArray();
        }

        public enum ViewMode : ushort
        {
            None,
            Silvers,
            Unknown,
            Emoney,
            ViewEquipment
        }
    }
}
