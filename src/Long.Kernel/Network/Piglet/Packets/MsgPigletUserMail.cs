using Long.Kernel.States.Mails;
using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletUserMail : MsgPigletUserMail<PigletActor>
    {
        public override async Task ProcessAsync(PigletActor client)
        {
            await MailBox.SendAsync(Data.UserId, Data.SenderName, Data.Subject, Data.Content, (uint)(UnixTimestamp.Now + Data.ExpirationSeconds),
                (uint)Data.Money, Data.ConquerPoints, Data.IsMono, Data.ItemId, Data.ItemType, Data.ActionId);
        }
    }
}
