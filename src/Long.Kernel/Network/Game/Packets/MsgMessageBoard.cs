using Long.Kernel.States.User;
using Long.Kernel.States;
using Long.Network.Packets;
using System.Drawing;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMessageBoard : MsgBase<GameClient>
    {
        public List<string> Messages = new();

        public ushort Index { get; set; }
        public BoardChannel Channel { get; set; }
        public BoardAction Action { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            reader.ReadInt32();
            Index = reader.ReadUInt16();
            Channel = (BoardChannel)reader.ReadUInt16();
            Action = (BoardAction)reader.ReadByte();
            Messages = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgMessageBoard);
            writer.Write(Environment.TickCount);
            writer.Write(Index);
            writer.Write((ushort)Channel);
            writer.Write((byte)Action);
            writer.Write(Messages);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Action)
            {
                case BoardAction.GetList:
                    var list = MessageBoard.GetMessages((TalkChannel)Channel, Index);
                    if (list.Count == 0)
                        return;

                    foreach (var msg in list)
                    {
                        if (Messages.Count >= 8)
                            break;

                        Messages.Add(msg.Sender);
                        Messages.Add(msg.Message.Substring(0, Math.Min(44, msg.Message.Length)));
                        Messages.Add(msg.Time.ToString("yyyyMMddHHmmss"));
                    }

                    Action = BoardAction.List;
                    await user.SendAsync(this);
                    break;

                case BoardAction.GetWords:
                    string message = MessageBoard.GetMessage(Messages[0], (TalkChannel)Channel);
                    await user.SendAsync(new MsgTalk
                    {
                        Channel = (TalkChannel)Channel,
                        Color = Color.White,
                        Message = message,
                        SenderName = Messages[0],
                        RecipientName = user.Name,
                        Style = TalkStyle.Normal,
                        Suffix = ""
                    });
                    break;
            }
        }

        public enum BoardAction : byte
        {
            None = 0,
            Del = 1,     // to server					// no return
            GetList = 2, // to server: index(first index)
            List = 3,    // to client: index(first index), name, words, time...
            GetWords = 4 // to server: index(for get)	// return by MsgTalk
        }

        public enum BoardChannel : ushort
        {
            None = 0,
            MsgTrade = 2201,
            MsgFriend = 2202,
            MsgTeam = 2203,
            MsgSyn = 2204,
            MsgOther = 2205,
            MsgSystem = 2206
        }
    }
}
