using Long.Database.Entities;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;

namespace Long.Module.Totem.States
{
    public sealed class TotemItem
    {
        public TotemItem(DbItem item, string playerName)
        {
            ItemIdentity = item.Id;
            PlayerIdentity = item.PlayerId;
            PlayerName = playerName;

            ItemType = item.Type;
            SocketOne = (Item.SocketGem)item.Gem1;
            SocketTwo = (Item.SocketGem)item.Gem2;
            Addition = item.Magic3;
        }

        public TotemItem(Character owner, Item item)
        {
            ItemIdentity = item.Identity;
            PlayerIdentity = owner.Identity;
            PlayerName = owner.Name;

            ItemType = item.Type;
            SocketOne = item.SocketOne;
            SocketTwo = item.SocketTwo;
            Addition = item.Plus;
        }

        public uint ItemIdentity { get; }
        public uint PlayerIdentity { get; }
        public string PlayerName { get; }

        public uint ItemType { get; private set; }
        public byte Quality => (byte)(ItemType % 10);
        public Item.SocketGem SocketOne { get; private set; }
        public Item.SocketGem SocketTwo { get; private set; }
        public byte Addition { get; private set; }

        public void SynchronizeItem(Item item)
        {
            if (item.Identity != ItemIdentity)
            {
                return;
            }

            ItemType = item.Type;
            SocketOne = item.SocketOne;
            SocketTwo = item.SocketTwo;
            Addition = item.Plus;
        }

        public int Points
        {
            get
            {
                var result = 0;
                switch (Quality)
                {
                    case 8:
                        result += 1000;
                        break;
                    case 9:
                        result += 16660;
                        break;
                }

                if (SocketTwo > Item.SocketGem.NoSocket)
                {
                    result += 133330;
                }
                else if (SocketOne > Item.SocketGem.NoSocket)
                {
                    result += 33330;
                }

                switch (Addition)
                {
                    case 1:
                        result += 90;
                        break;
                    case 2:
                        result += 490;
                        break;
                    case 3:
                        result += 1350;
                        break;
                    case 4:
                        result += 4070;
                        break;
                    case 5:
                        result += 12340;
                        break;
                    case 6:
                        result += 37030;
                        break;
                    case 7:
                        result += 111110;
                        break;
                    case 8:
                        result += 333330;
                        break;
                    case 9:
                        result += 1000000;
                        break;
                    case 10:
                        result += 1033330;
                        break;
                    case 11:
                        result += 1101230;
                        break;
                    case 12:
                        result += 1212340;
                        break;
                }

                return result;
            }
        }
    }
}
