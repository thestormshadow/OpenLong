using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgEquipLock : MsgBase<GameClient>
    {
        public uint Identity { get; set; }
        public LockMode Action { get; set; }
        public byte Mode { get; set; }
        public uint Param { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Identity = reader.ReadUInt32();
            Action = (LockMode)reader.ReadByte();
            Mode = reader.ReadByte();
            reader.ReadUInt16();
            Param = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgEquipLock);
            writer.Write(Identity);
            writer.Write((byte)Action);
            writer.Write(Mode);
            writer.Write((ushort)0);
            writer.Write(Param);
            return writer.ToArray();
        }

        public enum LockMode : byte
        {
            RequestLock = 0,
            RequestUnlock = 1,
            UnlockDate = 2,
            UnlockedItem = 3,
            LockedItemNotify = 4
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            Item item = user.UserPackage.FindItemByIdentity(Identity);
            if (item == null)
            {
                await user.SendAsync(StrItemNotFound);
                return;
            }

            switch (Action)
            {
                case LockMode.RequestLock:
                    {
                        if (item.IsLocked() && !item.IsUnlocking())
                        {
                            await user.SendAsync(StrEquipLockAlreadyLocked);
                            return;
                        }

                        if (!item.IsEquipment() && !item.IsMount() && !item.IsAccessory() && !item.IsMountArmor() && !item.IsGarment())
                        {
                            await user.SendAsync(StrEquipLockCantLock);
                            return;
                        }

                        await item.SetLockAsync();
                        await client.SendAsync(this);
                        await client.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        break;
                    }

                case LockMode.RequestUnlock:
                    {
                        if (!item.IsLocked())
                        {
                            await user.SendAsync(StrEquipLockNotLocked);
                            return;
                        }

                        if (item.IsUnlocking())
                        {
                            await user.SendAsync(StrEquipLockAlreadyUnlocking);
                            return;
                        }

                        await item.SetUnlockAsync();
                        await client.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        await item.TryUnlockAsync();
                        break;
                    }
            }
        }
    }
}
