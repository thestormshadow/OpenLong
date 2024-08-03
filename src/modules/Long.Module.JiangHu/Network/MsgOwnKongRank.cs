using Long.Database.Entities;
using Long.Kernel.Network.Game;
using Long.Module.JiangHu.Repositories;
using Long.Network.Packets;

namespace Long.Module.JiangHu.Network
{
    public sealed class MsgOwnKongRank : MsgBase<GameClient>
    {
        public byte Page { get; set; }
        public byte UserRanking { get; set; }
        public byte Count { get; set; }
        public byte Total { get; set; }
        public List<KongFuRank> Ranks { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Page = reader.ReadByte();
            UserRanking = reader.ReadByte();
            Count = reader.ReadByte();
            Total = reader.ReadByte();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgOwnKongRank);
            writer.Write(Page);
            writer.Write(UserRanking);
            writer.Write((byte)Ranks.Count);
            writer.Write(Total);
            foreach (KongFuRank kongFu in Ranks)
            {
                writer.Write(kongFu.Position);
                writer.Write(kongFu.InnerPower);
                writer.Write(kongFu.Level);
                writer.Write(kongFu.UserName, 16);
                writer.Write(kongFu.GongFuName, 16);
            }

            return writer.ToArray();
        }

        public struct KongFuRank
        {
            public byte Position { get; set; }
            public int InnerPower { get; set; }
            public int Level { get; set; }
            public string UserName { get; set; }
            public string GongFuName { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            const int ipp = 10;
            List<DbJiangHuPlayer> players = await JiangHuPlayerRepository.QueryRankAsync();
            UserRanking = (byte)(players.FindIndex(x => x.PlayerId == client.Character.Identity) + 1);
            Total = (byte)Math.Min(100, players.Count);
            int position = (Page - 1) * ipp + 1;
            foreach (DbJiangHuPlayer jiangHu in players
                         .Skip((Page - 1) * ipp)
                         .Take(ipp))
            {
                Ranks.Add(new KongFuRank
                {
                    Position = (byte)position++,
                    InnerPower = (int)jiangHu.TotalPowerValue,
                    GongFuName = jiangHu.Name,
                    Level = jiangHu.Player?.Level ?? 0,
                    UserName = jiangHu.Player?.Name ?? StrNone,
                });
            }

            await client.SendAsync(this);
        }
    }
}