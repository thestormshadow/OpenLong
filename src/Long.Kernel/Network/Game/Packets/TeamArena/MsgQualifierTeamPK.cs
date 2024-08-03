using Long.Network.Packets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Network.Game.Packets.TeamArena
{
    public sealed class MsgQualifierTeamPK : MsgProtoGenericBase<GameClient, MsgTeamPopPKArenicContract>
    {
        public MsgQualifierTeamPK() : base(PacketType.MsgTeamPKArenic)
        {
        }
    }

    [Contract]
    public class MsgTeamPopPKArenicContract
    {
        [ContractProperty(1)]
        public uint Type { get; set; }
        [ContractProperty(2)]
        public uint dwParam { get; set; }
        [ContractProperty(3)]
        public uint OpponentUID { get; set; }
        [ContractProperty(4, 16)]
        public string OpponentName { get; set; }
        [ContractProperty(5)]
        public uint TimeLeft { get; set; }
    }
}
