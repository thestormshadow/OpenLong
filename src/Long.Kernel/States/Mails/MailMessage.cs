using Long.Database.Entities;
using Long.Kernel.Database;

namespace Long.Kernel.States.Mails
{
    public sealed class MailMessage
    {
        [Flags]
        public enum MessageFlag : byte
        {
            None = 0,
            Read = 0x1,
            Deleted = 0x2,
            MoneyClaimed = 0x4,
            EmoneyClaimed = 0x8,
            ItemClaimed = 0x10,
            Notified = 0x20,
            ActionClaimed = 0x40
        }

        private readonly DbMail mail;

        public MailMessage(DbMail mail)
        {
            this.mail = mail;
        }

        //private MessageFlag Flag => (MessageFlag)mail.Flag;

        public bool HasClaimedMoney => Money == 0;
        public bool HasClaimedConquerPoints => ConquerPoints == 0;
        public bool IsEmoneyMono => mail.EmoneyRecordType != 0;
        public bool HasClaimedItem => Item == 0;
        public bool HasClaimedAction => Action == 0;
        public bool HasExpired => Expiration > 0 && UnixTimestamp.Now > Expiration;

        public uint Identity => mail.Id;
        public string SenderName => mail.SenderName;
        public string Title => mail.Title;
        public string Content => mail.Content;

        public ulong Money
        {
            get => mail.Money;
            set => mail.Money = value;
        }

        public uint ConquerPoints
        {
            get => mail.ConquerPoints;
            set => mail.ConquerPoints = value;
        }
        public uint Item
        {
            get => mail.ItemId;
            set => mail.ItemId = value;
        }
        public uint Action
        {
            get => mail.Action;
            set => mail.Action = value;
        }
        public uint Expiration => mail.ExpirationDate;

        public int Order => !HasClaimedConquerPoints || !HasClaimedItem || !HasClaimedMoney ? 1 : 0;

        public Task SaveAsync() => ServerDbContext.UpdateAsync(mail);
        public Task DeleteAsync() => ServerDbContext.DeleteAsync(mail);
    }
}
