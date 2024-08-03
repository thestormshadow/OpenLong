using Long.Kernel.States.MessageBoxes;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgInviteTrans : MsgBase<GameClient>
    {
        public Action Mode { get; set; }
        public int Message { get; set; }
        public int Priority { get; set; }
        public int Seconds { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (Action)reader.ReadInt32();
            Message = reader.ReadInt32();
            Priority = reader.ReadInt32();
            Seconds = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgInviteTrans);
            writer.Write((int)Mode);
            writer.Write(Message);
            writer.Write(Priority);
            writer.Write(Seconds);
            return writer.ToArray();
        }

        public enum Action
        {
            Pop,
            Accept,
            AcceptMessage
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Mode)
            {
                case Action.Accept:
                    {
                        if (user.MessageBox is not TimedMessageBox box)
                        {
                            return;
                        }

                        if (box.HasExpired)
                        {
                            user.MessageBox = null;
                            return;
                        }

                        await user.MessageBox.OnAcceptAsync();
                        await user.SendAsync(new MsgInviteTrans
                        {
                            Mode = Action.AcceptMessage,
                            Message = box.AcceptMsgId
                        });

                        user.MessageBox = null;
                        break;
                    }
            }
        }
    }
}
