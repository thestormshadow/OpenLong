using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using static Long.Kernel.States.User.Character;
using static Long.Kernel.StrRes;

namespace Long.Module.Trade.Network
{
    public sealed class MsgTrade : MsgBase<GameClient>
    {
        public uint Data { get; set; }
        public int Param { get; set; }
        public TradeAction Action { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Data = reader.ReadUInt32();
            Param = reader.ReadInt32();
            Action = (TradeAction)reader.ReadUInt16();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTrade);
            writer.Write(Data);
            writer.Write(Param);
            writer.Write((uint)Action);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Character target = null;

            switch (Action)
            {
                case TradeAction.Apply:
                    {
                        if (Data == 0)
                        {
                            return;
                        }

                        target = user.QueryRole(Data) as Character;
                        if (target == null || target.GetDistance(user) > Screen.VIEW_SIZE)
                        {
                            await user.SendAsync(StrTargetNotInRange);
                            return;
                        }

                        if (user.Trade != null)
                        {
                            await user.SendAsync(StrTradeYouAlreadyTrade);
                            return;
                        }

                        if (target.Trade != null)
                        {
                            await user.SendAsync(StrTradeTargetAlreadyTrade);
                            return;
                        }

                        if (target.QueryRequest(RequestType.Trade) == user.Identity)
                        {
                            target.PopRequest(RequestType.Trade);
                            user.Trade = target.Trade = new States.Trade(target, user);
                            await user.SendAsync(new MsgTrade { Action = TradeAction.Open, Data = target.Identity });
                            await target.SendAsync(new MsgTrade { Action = TradeAction.Open, Data = user.Identity });
                            return;
                        }

                        Data = user.Identity;
                        await target.SendAsync(this);
                        await target.SendRelationAsync(user);
                        user.SetRequest(RequestType.Trade, target.Identity);
                        await user.SendAsync(StrTradeRequestSent);
                        break;
                    }

                case TradeAction.Quit:
                    {
                        if (user.Trade != null)
                        {
                            await user.Trade.SendCloseAsync();
                        }

                        break;
                    }

                case TradeAction.AddItem:
                    {
                        if (user.Trade != null)
                        {
                            await user.Trade.AddItemAsync(Data, user);
                        }

                        break;
                    }

                case TradeAction.AddMoney:
                    {
                        if (user.Trade != null)
                        {
                            await user.Trade.AddMoneyAsync(Data, user);
                        }

                        break;
                    }

                case TradeAction.Accept:
                    {
                        if (user.Trade != null)
                        {
                            await user.Trade.AcceptAsync(user.Identity, false);
                        }

                        break;
                    }

                case TradeAction.AddConquerPoints:
                    {
                        if (user.Trade != null)
                        {
                            await user.Trade.AddEmoneyAsync(Data, user);
                        }

                        break;
                    }

                case TradeAction.SuspiciousTradeAgree:
                    {
                        if (user.Trade != null)
                        {
                            await user.Trade.AcceptAsync(user.Identity, true);
                        }
                        break;
                    }
            }
        }

        public enum TradeAction
        {
            Apply = 1,
            Quit,
            Open,
            Success,
            Fail,
            AddItem,
            AddMoney,
            ShowMoney,
            Accept = 10,
            AddItemFail,
            ShowConquerPoints,
            AddConquerPoints,
            SuspiciousTradeNotify = 15,
            SuspiciousTradeAgree,
            Timeout = 17
        }
    }
}
