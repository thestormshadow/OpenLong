using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossTrainingVitalityInfo<TActor>
        : MsgProtoBufBase<TActor, CrossTrainingVitalityInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossTrainingVitalityInfo()
            : base(PacketType.MsgCrossTrainingVitalityInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossTrainingVitalityInfoPB
    {
        [ProtoMember(1)]
        public ulong SessionId { get; set; }
        [ProtoMember(2)]
        public int Fate1Attrib1 { get; set; } // Dragon
        [ProtoMember(3)]
        public int Fate1Attrib2 { get; set; } // Dragon
        [ProtoMember(4)]
        public int Fate1Attrib3 { get; set; } // Dragon
        [ProtoMember(5)]
        public int Fate1Attrib4 { get; set; } // Dragon
        [ProtoMember(6)]
        public int Fate2Attrib1 { get; set; } // Phoenix
        [ProtoMember(7)]
        public int Fate2Attrib2 { get; set; } // Phoenix
        [ProtoMember(8)]
        public int Fate2Attrib3 { get; set; } // Phoenix
        [ProtoMember(9)]
        public int Fate2Attrib4 { get; set; } // Phoenix
        [ProtoMember(10)]
        public int Fate3Attrib1 { get; set; } // Tiger
        [ProtoMember(11)]
        public int Fate3Attrib2 { get; set; } // Tiger
        [ProtoMember(12)]
        public int Fate3Attrib3 { get; set; } // Tiger
        [ProtoMember(13)]
        public int Fate3Attrib4 { get; set; } // Tiger
        [ProtoMember(14)]
        public int Fate4Attrib1 { get; set; } // Turtle
        [ProtoMember(15)]
        public int Fate4Attrib2 { get; set; } // Turtle
        [ProtoMember(16)]
        public int Fate4Attrib3 { get; set; } // Turtle
        [ProtoMember(17)]
        public int Fate4Attrib4 { get; set; } // Turtle
    }
}
