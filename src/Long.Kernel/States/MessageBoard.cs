using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using System.Collections.Concurrent;

namespace Long.Kernel.States
{
    public static class MessageBoard
    {
        private static ConcurrentDictionary<uint, MessageInfo> tradeMessages = new();
        private static ConcurrentDictionary<uint, MessageInfo> teamMessages = new();
        private static ConcurrentDictionary<uint, MessageInfo> friendMessages = new();
        private static ConcurrentDictionary<uint, MessageInfo> syndicateMessages = new();
        private static ConcurrentDictionary<uint, MessageInfo> otherMessages = new();
        private static ConcurrentDictionary<uint, MessageInfo> systemMessages = new();

        public static bool AddMessage(Character user, string message, TalkChannel channel)
        {
            ConcurrentDictionary<uint, MessageInfo> board;
            switch (channel)
            {
                case TalkChannel.TradeBoard:
                    board = tradeMessages;
                    break;
                case TalkChannel.TeamBoard:
                    board = teamMessages;
                    break;
                case TalkChannel.FriendBoard:
                    board = friendMessages;
                    break;
                case TalkChannel.GuildBoard:
                    board = syndicateMessages;
                    break;
                case TalkChannel.OthersBoard:
                    board = otherMessages;
                    break;
                case TalkChannel.Bbs:
                    board = systemMessages;
                    break;
                default:
                    return false;
            }

            board.TryRemove(user.Identity, out _);

            // todo verify silence
            // todo filter words

            board.TryAdd(user.Identity, new MessageInfo
            {
                SenderIdentity = user.Identity,
                Sender = user.Name,
                Message = message[..Math.Min(message.Length, 255)],
                Time = DateTime.Now
            });

            return true;
        }

        public static List<MessageInfo> GetMessages(TalkChannel channel, int page)
        {
            List<MessageInfo> msgs;
            switch (channel)
            {
                case TalkChannel.TradeBoard:
                    msgs = tradeMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.TeamBoard:
                    msgs = teamMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.FriendBoard:
                    msgs = friendMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.GuildBoard:
                    msgs = syndicateMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.OthersBoard:
                    msgs = otherMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.Bbs:
                    msgs = systemMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                default:
                    return new List<MessageInfo>();
            }

            if (page * 8 > msgs.Count)
                return new List<MessageInfo>();

            return msgs.Skip(page * 8).Take(8).ToList();
        }

        public static string GetMessage(string name, TalkChannel channel)
        {
            List<MessageInfo> msgs;
            switch (channel)
            {
                case TalkChannel.TradeBoard:
                    msgs = tradeMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.TeamBoard:
                    msgs = teamMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.FriendBoard:
                    msgs = friendMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.GuildBoard:
                    msgs = syndicateMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.OthersBoard:
                    msgs = otherMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                case TalkChannel.Bbs:
                    msgs = systemMessages.Values.OrderByDescending(x => x.Time).ToList();
                    break;
                default:
                    return string.Empty;
            }

            return msgs.FirstOrDefault(x => x.Sender.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Message ?? string.Empty;
        }
    }

    public struct MessageInfo
    {
        public uint SenderIdentity;
        public string Sender;
        public string Message;
        public DateTime Time;
    }
}
