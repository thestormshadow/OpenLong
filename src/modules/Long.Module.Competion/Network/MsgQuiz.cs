using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Competion.States;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Competion.Network
{
    public sealed class MsgQuiz : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgQuiz>();

        public List<QuizRank> Scores = new();

        public List<string> Strings = new();
        public QuizAction Action { get; set; }

        /// <remarks>Countdown | Score | Question Number</remarks>
        public ushort Param1 { get; set; }

        /// <remarks>Last Correct Answer | Time Taken | Reward</remarks>
        public ushort Param2 { get; set; }

        /// <remarks>Time Per Question | Exp. Awarded |  Rank</remarks>
        public ushort Param3 { get; set; }

        /// <remarks>First Prize | Time Taken</remarks>
        public ushort Param4 { get; set; }

        /// <remarks>Second Prize | Current Score</remarks>
        public ushort Param5 { get; set; }

        /// <remarks>Third Prize</remarks>
        public ushort Param6 { get; set; }

        public ushort Param7 { get; set; }
        public ushort Param8 { get; set; }
        public ushort Param9 { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();            // 0
            Type = (PacketType)reader.ReadUInt16(); // 2
            Action = (QuizAction)reader.ReadUInt16();
            Param1 = reader.ReadUInt16();
            Param2 = reader.ReadUInt16();
            Param3 = reader.ReadUInt16();
            Param4 = reader.ReadUInt16();
            Param5 = reader.ReadUInt16();
            Param6 = reader.ReadUInt16();
            Param7 = reader.ReadUInt16();
            Param8 = reader.ReadUInt16();
            Param9 = reader.ReadUInt16();
            Strings = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgQuiz);
            writer.Write((ushort)Action); // 4
            writer.Write(Param1); // 6
            writer.Write(Param2); // 8
            writer.Write(Param3); // 10
            writer.Write(Param4); // 12
            writer.Write(Param5); // 14
            writer.Write(Param6); // 16
            writer.Write(Param7); // 18
            if (Scores.Count > 0)
            {
                writer.Write(Scores.Count); // 20
                foreach (QuizRank score in Scores)
                {
                    writer.Write(score.Name, 16);
                    writer.Write(score.Score);
                    writer.Write(score.Time);
                }
            }
            else
            {
                writer.Write(Param8); // 20
                writer.Write(Param9); // 22
                writer.Write(Strings); // 24
            }

            return writer.ToArray();
        }

        public struct QuizRank
        {
            public string Name { get; set; }
            public ushort Score { get; set; }
            public ushort Time { get; set; }
        }

        public enum QuizAction : ushort
        {
            None,
            Start,
            Question,
            Reply,
            AfterReply,
            Finish,
            Quit = 8
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Action)
            {
                case QuizAction.Reply:
                    {
                        if (QuizShowManager.IsCanceled(user.Identity))
                        {
                            return;
                        }

                        await QuizShowManager.OnReplyAsync(user, Param1, Param2);
                        return;
                    }

                case QuizAction.Quit:
                    {
                        if (QuizShowManager.IsCanceled(user.Identity))
                        {
                            return;
                        }

                        QuizShowManager.Cancel(user.Identity);
                        return;
                    }

                default:
                    {
                        await client.SendAsync(this);
                        if (client.Character.IsGm())
                        {
                            await client.SendAsync(new MsgTalk(TalkChannel.Service, $"Missing packet {Type}, Action {Action}, Length {Length}"));
                        }

                        logger.Warning("Missing packet {0}, Action {1}, Length {2}\n" + PacketDump.Hex(Encode()), Type, Action, Length);
                        return;
                    }
            }
        }
    }
}
