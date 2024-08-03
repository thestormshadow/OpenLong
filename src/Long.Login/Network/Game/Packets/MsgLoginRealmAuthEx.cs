using Long.Network.Packets.Login;

namespace Long.Login.Network.Game.Packets
{
    public sealed class MsgLoginRealmAuthEx : MsgLoginRealmAuthEx<GameClient>
    {
        public MsgLoginRealmAuthEx(ResponseCode responseCode)
        {
            Data = new RealmAuthDataEx
            {
                Response = (int)responseCode
            };
        }
    }
}
