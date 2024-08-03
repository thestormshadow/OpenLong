using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items.Status;
using Long.Kernel.States.User;
using static Long.Kernel.Network.Game.Packets.MsgItemStatus;
using ItemStatus = Long.Kernel.Network.Game.Packets.MsgItemStatus.ItemStatus;

namespace Long.Kernel.States.Items
{
    public sealed class ItemQuench
    {
        public static readonly int[] ArtifactSolidifyPoints = { 0, 10, 30, 50, 100, 150, 200, 300 };
        public static readonly int[] RefinerySolidifyPoints = { 0, 10, 30, 70, 150, 270, 430 };

        private static readonly ILogger logger = Log.ForContext<ItemQuench>();

        private readonly List<QuenchData> artifacts = new();
        private readonly List<QuenchData> refineries = new();

        private Item item;
        public uint ItemIdentity => item?.Identity ?? 0;

        public QuenchData CurrentArtifact { get; private set; }// => artifacts.OrderByDescending(x => x.ItemStatus.Position).FirstOrDefault();
        public QuenchData CurrentRefinery { get; private set; }// => refineries.OrderByDescending(x => x.ItemStatus.Position).FirstOrDefault();

        public async Task InitializeAsync(Item item)
        {
            this.item = item;

            var readOnly = await ItemStatusRepository.GetAsync(item.Identity);
            foreach (var status in readOnly.OrderBy(x => x.Position))
            {
                await AppendAsync(status);
            }

            ActivateNextArtifact();
            ActivateNextRefinery();
        }

        public async Task<QuenchData> AppendAsync(DbItemStatus status)
        {
            DbItemtype itemType = ItemManager.GetItemtype(status.Status);
            if (itemType == null)
            {
                logger.Information("Invalid itemtype {ItemType} for ItemStatus {Id}", status.Status, status.Id);
                return null;
            }

            QuenchData data;
            if (Item.IsArtifact(status.Status) || Item.IsRefinery(status.Status))
            {
                data = new QuenchData
                {
                    ItemStatus = status,
                    ItemType = itemType
                };
            }
            else
            {
                logger.Warning("Status {Id} of type {Status} is not an artifact or refinery", status.Id, status.Status);
                return null;
            }

            if (data.HasExpired)
            {
                logger.Information("Deleting expired artifact or refinery [{Id}]", data.ItemStatus.Id);
                await data.DeleteAsync();
                return null;
            }

            if (Item.IsArtifact(status.Status))
            {
                if (artifacts.Count > 0)
                {
                    data.ItemStatus.Position = (byte)(artifacts.Max(x => x.ItemStatus.Position) + 1);
                }
                await FixOrderAsync(artifacts);
                artifacts.Add(data);
                CurrentArtifact = data;
            }
            else
            {
                if (!ItemManager.GetQuenchInfoData((int)status.Data, out var quenchInfoData))
                {
                    if (data.ItemStatus.Id != 0)
                    {
                        await data.DeleteAsync();
                    }
                    logger.Information("Invalid status data for refinery!!! {status}", status.Status);
                    return null;
                }

                data.Attribute1 = quenchInfoData.Attribute1;
                data.Attribute2 = quenchInfoData.Attribute2;

                if (refineries.Count > 0)
                {
                    data.ItemStatus.Position = (byte)(refineries.Max(x => x.ItemStatus.Position) + 1);
                }
                await FixOrderAsync(refineries);
                refineries.Add(data);
                CurrentRefinery = data;
            }

            if (data.ItemStatus.Id == 0)
            {
                await data.SaveAsync();
            }

            return data;
        }

        public void ActivateNextArtifact()
        {
            CurrentArtifact = artifacts.OrderByDescending(x => x.ItemStatus.Position).FirstOrDefault();
        }

        public QuenchData GetOriginalArtifact()
        {
            return artifacts.FirstOrDefault(x => x.IsPermanent);
        }

        public async Task StabilizeArtifactAsync()
        {
            // leave only the permanent one
            for (int i = artifacts.Count - 1; i >= 0; i--)
            {
                var artifact = artifacts[i];
                if (CurrentArtifact.ItemStatus.Id != artifact.ItemStatus.Id)
                {
                    artifacts.RemoveAt(i);
                    await artifact.DeleteAsync();
                }
            }

            CurrentArtifact.ItemStatus.RealSeconds = 0;
            await CurrentArtifact.SaveAsync();
            await FixOrderAsync(artifacts);
            ActivateNextArtifact();
        }

        public void ActivateNextRefinery()
        {
            CurrentRefinery = refineries.OrderByDescending(x => x.ItemStatus.Position).FirstOrDefault();
        }

        public QuenchData GetOriginalRefinery()
        {
            return refineries.FirstOrDefault(x => x.IsPermanent);
        }

        public async Task StabilizeRefineryAsync()
        {
            // leave only the permanent one
            for (int i = refineries.Count - 1; i >= 0; i--)
            {
                var refinery = refineries[i];
                if (CurrentRefinery.ItemStatus.Id != refinery.ItemStatus.Id)
                {
                    refineries.RemoveAt(i);
                    await refinery.DeleteAsync();
                }
            }

            CurrentRefinery.ItemStatus.RealSeconds = 0;
            await CurrentRefinery.SaveAsync();
            await FixOrderAsync(refineries);
            ActivateNextRefinery();
        }

        public static async Task FixOrderAsync(List<QuenchData> list)
        {
            if (list.Count == 0)
            {
                return;
            }

            byte currPos = 0;
            foreach (var quench in list.OrderBy(x => x.IsPermanent ? 0 : 1).ThenBy(x => x.ItemStatus.Position))
            {
                quench.ItemStatus.Position = currPos++;
                await quench.SaveAsync();
            }
        }

        public async Task SendToAsync(Character user)
        {
            MsgItemStatus msg = new();
            foreach (var artifact in artifacts.OrderBy(x => x.IsPermanent ? 0 : 1).ThenBy(x => x.ItemStatus.Position))
            {
                if (artifact.IsPermanent)
                {
                    msg.Statuses.Add(new ItemStatus
                    {
                        Identity = ItemIdentity,
                        Type = ItemStatusType.PermanentArtifact,
                        Duration = 0,
                        Level = artifact.ItemStatus.Level,
                        PurificationIdentity = artifact.ItemStatus.Status
                    });
                }
                else
                {
                    msg.Statuses.Add(new ItemStatus
                    {
                        Identity = ItemIdentity,
                        Type = ItemStatusType.ArtifactAdd,
                        Duration = (uint)artifact.ExpiresIn,
                        Level = artifact.ItemStatus.Level,
                        PurificationIdentity = artifact.ItemStatus.Status
                    });
                }
            }

            foreach (var refinery in refineries.OrderBy(x => x.IsPermanent ? 0 : 1).ThenBy(x => x.ItemStatus.Position))
            {
                if (refinery.IsPermanent)
                {
                    msg.Statuses.Add(new ItemStatus
                    {
                        Identity = ItemIdentity,
                        Type = ItemStatusType.PermanentRefinery,
                        Duration = 0,
                        Level = refinery.ItemStatus.Level,
                        PurificationIdentity = refinery.ItemStatus.Data,
                        Percent = refinery.Power1,
                        Percent2 = refinery.Power2
                    });
                }
                else
                {
                    msg.Statuses.Add(new ItemStatus
                    {
                        Identity = ItemIdentity,
                        Type = ItemStatusType.RefineryAdd,
                        Duration = (uint)refinery.ExpiresIn,
                        Level = refinery.ItemStatus.Level,
                        PurificationIdentity = refinery.ItemStatus.Data,
                        Percent = refinery.Power1,
                        Percent2 = refinery.Power2
                    });
                }
            }
            if (msg.Statuses.Count > 0)
            {
                await user.SendAsync(msg);
            }
        }


        public async Task RemoveAsync(uint statusId)
        {
            for (int i = artifacts.Count - 1; i >= 0; i--)
            {
                QuenchData data = artifacts[i];
                if (data.ItemStatus.Id == statusId)
                {
                    artifacts.RemoveAt(i);
                    await data.DeleteAsync();
                    return;
                }
            }

            for (int i = refineries.Count - 1; i >= 0; i--)
            {
                QuenchData data = refineries[i];
                if (data.ItemStatus.Id == statusId)
                {
                    refineries.RemoveAt(i);
                    await data.DeleteAsync();
                    return;
                }
            }
        }

        public async Task OnTimerAsync(Character user)
        {
            if (CurrentArtifact != null && CurrentArtifact.HasExpired)
            {
                await RemoveAsync(CurrentArtifact.ItemStatus.Id);

                MsgItemStatus msg = new();
                msg.Statuses.Add(new ItemStatus
                {
                    Identity = ItemIdentity,
                    Type = ItemStatusType.ArtifactExpired
                });
                await user.SendAsync(msg);
                ActivateNextArtifact();
                await SendToAsync(user);
            }

            if (CurrentRefinery != null && CurrentRefinery.HasExpired)
            {
                await RemoveAsync(CurrentRefinery.ItemStatus.Id);

                MsgItemStatus msg = new();
                msg.Statuses.Add(new ItemStatus
				{
                    Identity = ItemIdentity,
                    Type = ItemStatusType.RefineryStabilizationEffect
                });
                await user.SendAsync(msg);
                ActivateNextRefinery();
                await SendToAsync(user);
            }
        }

        public class QuenchData
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

            public async Task SaveAsync()
            {
                await ServerDbContext.UpdateAsync(ItemStatus);
            }

            public async Task DeleteAsync()
            {
                await ServerDbContext.DeleteAsync(ItemStatus);
            }
        }

        public struct QuenchInfoData
        {
            public ItemStatusAttribute Attribute1 { get; init; }
            public ItemStatusAttribute Attribute2 { get; init; }
        }

        public enum QuenchPosition
        {
            None,
            Headwear,
            Neck,
            Armor,
            SingleHand,
            DoubleHand,
            Shield,
            Ring,
            Bracelet = Ring,
            Shoes
        }                
    }
}
