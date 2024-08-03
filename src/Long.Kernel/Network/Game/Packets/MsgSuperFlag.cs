using Long.Kernel.Managers;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using System.Drawing;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgSuperFlag : MsgBase<GameClient>
    {
        public SuperFlagAction Action { get; set; }
        public uint Identity { get; set; }
        public uint Index { get; set; }
        public int Unknown16 { get; set; }
        public int Unknown20 { get; set; }
        public int Durability { get; set; }
        public int Count { get; set; }
        public int Unknown32 { get; set; }
        public int Unknown36 { get; set; }
        public int Unknown40 { get; set; }
        public int Unknown44 { get; set; }
        public string Name { get; set; }

        public List<LocationStruct> Locations { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (SuperFlagAction)reader.ReadInt32(); // 4
            Identity = reader.ReadUInt32(); // 8
            Index = reader.ReadUInt32(); // 12
            Unknown16 = reader.ReadInt32(); // 16
            Unknown20 = reader.ReadInt32(); // 20
            Durability = reader.ReadInt32(); // 24
            Count = reader.ReadInt32(); // 28
            Unknown32 = reader.ReadInt32(); // 32
            Unknown36 = reader.ReadInt32(); // 36
            Unknown40 = reader.ReadInt32(); // 40
            Unknown44 = reader.ReadInt32(); // 44
            Name = reader.ReadString(32); // 48
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgSuperFlag);
            writer.Write((int)Action); // 4
            writer.Write(Identity); // 8
            writer.Write(Index); // 12
            writer.Write(Unknown16); // 16
            writer.Write(Unknown20); // 20
            writer.Write(Durability); // 24
            writer.Write(Locations.Count); // 28
            foreach (var location in Locations)
            {
                writer.Write(location.LocationIdx);
                writer.Write(location.MapId);
                writer.Write(location.X);
                writer.Write(location.Y);
                writer.Write(location.Name, 32);
            }
            return writer.ToArray();
        }

        public struct LocationStruct
        {
            public uint LocationIdx { get; set; }
            public uint MapId { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public string Name { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Item item = user.UserPackage.GetInventory(Identity);

            if (item == null || !item.IsSuperFlag())
            {
                return;
            }

            switch (Action)
            {
                case SuperFlagAction.SaveLocation:
                    {
                        if (user.Map.IsDynamicMap())
                        {
                            return;
                        }

                        if (user.Map.IsRecordDisable())
                        {
                            return;
                        }

                        if (user.Map.IsPkField())
                        {
                            return;
                        }

                        if (user.Map.IsBoothEnable())
                        {
                            return;
                        }

                        if (user.Map.IsFamilyMap() || user.Map.IsSynMap())
                        {
                            return;
                        }

                        if (user.Map.IsMineField() || user.Map.IsPrisionMap())
                        {
                            return;
                        }

                        if (Index < item.SuperFlagCount)
                        {
                            await item.UpdateLocationAsync((int)Index, Name, user.MapIdentity, user.X, user.Y);
                        }
                        else
                        {
                            await item.SaveLocationAsync(Name, user.MapIdentity, user.X, user.Y);
                        }

                        break;
                    }

                case SuperFlagAction.ChangeName:
                    {
                        if (Name.Length <= 0 || !RoleManager.IsValidName(Name.Replace(" ", "~")))
                        {
                            return;
                        }

                        string name = Name.Substring(0, Math.Min(Name.Length, 32));
                        await item.UpdateNameAsync((int)Index, name);
                        break;
                    }

                case SuperFlagAction.GotoLocation:
                    {
                        if (user.Map.IsChgMapDisable() || user.Map.IsPrisionMap())
                        {
                            return;
                        }

                        uint idMap = 0;
                        Point pos = new();
                        if (!item.GetTeleportLocation((int)Index, ref idMap, ref pos))
                        {
                            return;
                        }

                        GameMap targetMap = MapManager.GetMap(idMap);
                        if (targetMap == null)
                        {
                            return;
                        }

                        if (item.Durability <= 0)
                        {
                            return;
                        }

                        item.Durability -= 1;
                        await item.SaveAsync();
                        await item.SendSuperFlagListAsync();
                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));

                        await user.FlyMapAsync(idMap, pos.X, pos.Y);
                        break;
                    }

                case SuperFlagAction.Refill:
                    {
                        if (item.Durability == item.MaximumDurability)
                        {
                            return;
                        }

                        int cost = Math.Max((item.MaximumDurability - item.Durability) / 2, 1);
                        if (!await user.SpendBoundConquerPointsAsync(Character.EmoneyOperationType.SuperFlag, cost, true))
                        {
                            return;
                        }

                        item.Durability = item.MaximumDurability;
                        await item.SendSuperFlagListAsync();
                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        break;
                    }
            }
        }
    }

    public enum SuperFlagAction
    {
        SaveLocation = 1,
        ChangeName,
        GotoLocation,
        Refill
    }
}
