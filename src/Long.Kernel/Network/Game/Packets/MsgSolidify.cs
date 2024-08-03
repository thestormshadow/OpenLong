using Long.Kernel.States.Items;
using Long.Kernel.States.Items.Status;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgSolidify : MsgBase<GameClient>
    {
        public enum SolidifyMode
        {
            Refinery,
            Artifact
        }

        public SolidifyMode Action { get; set; }
        public uint MainIdentity { get; set; }
        public int Count { get; set; }
        public List<uint> Consumables { get; } = new List<uint>();


        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (SolidifyMode)reader.ReadInt32();
            MainIdentity = reader.ReadUInt32();
            Count = reader.ReadInt32();
            for (int i = 0; i < Count; i++)
            {
                Consumables.Add(reader.ReadUInt32());
            }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Item item = user.UserPackage.GetInventory(MainIdentity);

            int pointsSum = 0;
            List<Item> usable = new();
            foreach (var idConsumable in Consumables)
            {
                Item consumable = user.UserPackage.GetInventory(idConsumable);
                if (consumable.Type == Item.PERMANENT_STONE)
                {
                    pointsSum += 10;
                }
                else if (consumable.Type == Item.BIGPERMANENT_STONE)
                {
                    pointsSum += 100;
                }
                else
                {
                    continue;
                }

                usable.Add(consumable);
            }

            switch (Action)
            {
                case SolidifyMode.Refinery:
                    {
                        if (item.ItemStatus.CurrentRefinery == null || item.ItemStatus.CurrentRefinery.IsPermanent)
                        {
                            return;
                        }

                        ItemStatusData original = item.ItemStatus.GetOriginalRefinery();
                        int needPoints = ItemStatus.RefinerySolidifyPoints[item.ItemStatus.CurrentRefinery.ItemStatus.Level];
                        if (original != null)
                        {
                            needPoints = Math.Max(10, needPoints - ItemStatus.RefinerySolidifyPoints[original.ItemStatus.Level]);
                        }

                        if (needPoints > pointsSum)
                        {
                            return;
                        }

                        foreach (var consume in usable)
                        {
                            await user.UserPackage.SpendItemAsync(consume);
                        }

                        await item.ItemStatus.StabilizeRefineryAsync();
                        await item.ItemStatus.SendToAsync(user);
                        await user.SendAsync(new MsgItemStatus(item.ItemStatus.CurrentRefinery, MsgItemStatus.ItemStatusType.RefineryStabilizationEffect));
                        break;
                    }

                case SolidifyMode.Artifact:
                    {
                        if (item.ItemStatus.CurrentArtifact == null || item.ItemStatus.CurrentArtifact.IsPermanent)
                        {
                            return;
                        }

                        ItemStatusData original = item.ItemStatus.GetOriginalArtifact();
                        int needPoints = ItemStatus.ArtifactSolidifyPoints[item.ItemStatus.CurrentArtifact.ItemStatus.Level];
                        if (original != null)
                        {
                            needPoints = Math.Max(10, needPoints - ItemStatus.ArtifactSolidifyPoints[original.ItemStatus.Level]);
                        }

                        if (needPoints > pointsSum)
                        {
                            return;
                        }

                        foreach (var consume in usable)
                        {
                            await user.UserPackage.SpendItemAsync(consume);
                        }

                        await item.ItemStatus.StabilizeArtifactAsync();
                        await item.ItemStatus.SendToAsync(user);
                        await user.SendAsync(new MsgItemStatus(item.ItemStatus.CurrentArtifact, MsgItemStatus.ItemStatusType.ArtifactStabilizationEffect));
                        break;
                    }
            }
        }
    }
}
