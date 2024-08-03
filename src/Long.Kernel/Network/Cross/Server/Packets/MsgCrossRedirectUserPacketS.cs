using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Server.Packets
{
    public sealed class MsgCrossRedirectUserPacketS : MsgCrossRedirectUserPacket<CrossServerActor>
    {
        public MsgCrossRedirectUserPacketS()
        {            
        }

        public MsgCrossRedirectUserPacketS(byte[] msg) => Data = new CrossRedirectUserPacketPB
        {
            Packet = msg
        };

        public MsgCrossRedirectUserPacketS(uint userId, byte[] msg) => Data = new CrossRedirectUserPacketPB
        {
            Packet = msg,
            UserID = userId,
        };

        public override Task ProcessAsync(CrossServerActor client)
        {
            return RealmManager.ProcessUserPacketAsync(Data.UserID, Data.Packet);
        }
    }
}
