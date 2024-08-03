using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossRedirectUserPacketC : MsgCrossRedirectUserPacket<CrossClientActor>
    {
        public MsgCrossRedirectUserPacketC()
        {
        }

        public MsgCrossRedirectUserPacketC(byte[] msg) => Data = new CrossRedirectUserPacketPB
        {
            Packet = msg
        };

        public MsgCrossRedirectUserPacketC(uint userId, byte[] msg) => Data = new CrossRedirectUserPacketPB
        {
            Packet = msg,
            UserID = userId,
        };

        public override Task ProcessAsync(CrossClientActor client)
        {
            return RealmManager.ProcessUserPacketAsync(Data.UserID, Data.Packet);
        }
    }
}
