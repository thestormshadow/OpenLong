using Long.Network.Packets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Network.Game.Packets.TeamArena
{
    public sealed class MsgDominateTeamName : MsgProtoBufBase<GameClient, MsgAthleteShopContract>
    {
        public MsgDominateTeamName() : base(PacketType.MsgDominateTeamName)
        {

        }

        public MsgDominateTeamName(uint type, uint teamId, string teamName) : base(PacketType.MsgDominateTeamName)
        {
            Data = new MsgAthleteShopContract()
            {
                Type = type,
                TeamID = teamId,
                TeamName = teamName
            };
        }
    }

    [ProtoContract]
    public class MsgAthleteShopContract
    {
        [ProtoMember(1, IsRequired = true)]
        public uint Type { get; set; }
        [ProtoMember(2, IsRequired = true)]
        public uint TeamID { get; set; }
        [ProtoMember(3, IsRequired = true)]
        public string TeamName { get; set; }
    }
}
