using Long.Network.Packets;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgGLRankingList : MsgProtoBufBase<GameClient, MsgGLRankingList.GoldenLeagueRankingListData>
    {
        public MsgGLRankingList()
            : base(PacketType.MsgGLRankingList)
        {
        }

        public MsgGLRankingList(uint points, uint historyPoints)
            : base(PacketType.MsgGLRankingList)
        {
            Data = new GoldenLeagueRankingListData
            {
                Points = points,
                //HistoryPoints = points
            };
        }

        [ProtoContract]
        public struct GoldenLeagueRankingListData
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Points { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            await client.SendAsync(new MsgGLRankingList(client.Character.LeaguePoints, 0));
        }
    }
}
