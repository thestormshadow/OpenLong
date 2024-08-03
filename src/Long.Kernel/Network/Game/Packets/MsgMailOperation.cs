using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMailOperation : MsgBase<GameClient>
    {
        public Mode Action { get; set; }
        public uint EmailIdentity { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (Mode)reader.ReadInt32();
            EmailIdentity = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgMailOperation);
            writer.Write((int)Action);
            writer.Write(EmailIdentity);
            return writer.ToArray();
        }

        public enum Mode
        {
            None,
            Open,
            Delete,
            ClaimMoney,
            ClaimConquerPoints,
            ClaimItem,
            ClaimAttachment
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            switch (Action)
            {
                case Mode.Open:
                    {
                        await user.MailBox.SendMessageAsync(EmailIdentity);
                        break;
                    }

                case Mode.Delete:
                    {
                        await user.MailBox.DeleteMessageAsync(EmailIdentity);
                        await user.SendAsync(this);
                        break;
                    }

                case Mode.ClaimMoney:
                    {
                        await user.MailBox.ClaimMoneyAsync(EmailIdentity);
                        await user.SendAsync(this);
                        break;
                    }

                case Mode.ClaimConquerPoints:
                    {
                        await user.MailBox.ClaimConquerPointsAsync(EmailIdentity);
                        await user.SendAsync(this);
                        break;
                    }

                case Mode.ClaimItem:
                    {
                        await user.MailBox.ClaimActionAsync(EmailIdentity);
                        await user.SendAsync(this);
                        break;
                    }

                case Mode.ClaimAttachment:
                    {
                        await user.MailBox.ClaimItemAsync(EmailIdentity);
                        await user.SendAsync(this);
                        break;
                    }
            }
        }
    }
}
