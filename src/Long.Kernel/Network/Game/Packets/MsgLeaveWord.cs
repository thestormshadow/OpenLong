using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeaveWord : MsgBase<GameClient>
    {
        public enum LeaveWordAction
        {
            Confirm = 3,
            Replace,
            Submit,
            Error
        }

        public LeaveWordAction Action { get; set; }
        public List<string> Data { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (LeaveWordAction)reader.ReadInt32();
            Data = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeaveWord);
            writer.Write((int)Action);
            writer.Write(Data);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            switch (Action)
            {
                case LeaveWordAction.Replace:
                    {
                        if (Data.Count == 0)
                        {
                            return;
                        }

                        string targetName = Data[0];
                        string message = user.GetPendingLeaveWord(targetName);
                        if (string.IsNullOrEmpty(message))
                        {
                            return;
                        }

                        await user.ClearLeaveWordAsync(targetName);
                        await user.LeaveWordAsync(targetName, message, true);
                        break;
                    }
            }
        }
    }
}
