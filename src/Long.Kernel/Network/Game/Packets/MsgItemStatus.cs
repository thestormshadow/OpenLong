using Long.Kernel.States.Items.Status;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgItemStatus : MsgBase<GameClient>
    {
        public MsgItemStatus()
        {

        }

        public MsgItemStatus(ItemStatusData data, ItemStatusType action)
        {
            Statuses.Add(new ItemStatus
            {
                Identity = data.ItemStatus.ItemId,
                Type = action,
                Duration = (uint)(data.IsPermanent ? 0 : data.ExpiresIn),
                Level = data.ItemStatus.Level,
                PurificationIdentity = data.ItemStatus.Status,
                Percent = data.Power1,
                Percent2 = data.Power2
            });
        }

        public int Count { get; set; }
        public List<ItemStatus> Statuses { get; init; } = new List<ItemStatus>();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgItemStatus);
            writer.Write(Count = Statuses.Count);
            foreach (var status in Statuses)
            {
                writer.Write(status.Identity); // 0
                writer.Write((int)status.Type); // 4
                writer.Write(status.PurificationIdentity); // 8
                writer.Write(status.Level); // 12
                writer.Write(status.Percent); // 16
                writer.Write(status.Percent2); // 20
                writer.Write(status.Duration); // 24
                writer.Write(0);
            }
            return writer.ToArray();
        }

        public struct ItemStatus
        {
            public uint Identity { get; set; }
            public ItemStatusType Type { get; set; }
            public uint PurificationIdentity { get; set; }
            public uint Level { get; set; }
            public uint Percent { get; set; }
            public uint Duration { get; set; }
            public uint Percent2 { get; set; }
        }

        public enum ItemStatusType
        {
            RefineryEffect = 1,
            RefineryAdd = 2,
            PermanentRefinery = 3,
            RefineryStabilizationEffect = 4,
            PurificationEffect = 5,
            ArtifactAdd = 6,
            ArtifactExpired = 7,
            PermanentArtifact = 8,
            ArtifactStabilizationEffect = 9
        }
    }
}