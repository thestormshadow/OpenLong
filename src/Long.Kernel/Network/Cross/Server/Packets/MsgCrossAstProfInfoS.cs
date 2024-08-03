using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Server.Packets
{
    public sealed class MsgCrossAstProfInfoS : MsgCrossAstProfInfo<CrossServerActor>
    {
        public override Task ProcessAsync(CrossServerActor client)
        {
            return RealmManager.ReceiveAstProfDataAsync(Data, client);
        }
    }
}
