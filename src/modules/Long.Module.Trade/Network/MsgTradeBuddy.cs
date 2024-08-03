using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;
using static Long.Kernel.States.User.Character;
using static Long.Kernel.StrRes;

namespace Long.Module.Trade.Network
{
    public sealed class MsgTradeBuddy : MsgBase<GameClient>
    {
        public uint Identity { get; set; }
        public TradeBuddyAction Action { get; set; }
        public int HoursLeft { get; set; }
        public bool IsOnline { get; set; }
        public ushort Level { get; set; }
        public string Name { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Identity = reader.ReadUInt32();
            Action = (TradeBuddyAction)reader.ReadByte();
            IsOnline = reader.ReadBoolean();
            HoursLeft = reader.ReadInt32();
            Level = reader.ReadUInt16();
            Name = reader.ReadString(16);
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTradeBuddy);
            writer.Write(Identity);
            writer.Write((byte)Action);
            writer.Write(IsOnline);
            writer.Write(HoursLeft);
            writer.Write(Level);
            writer.Write(Name ?? string.Empty, 16);
            return writer.ToArray();
        }

        public enum TradeBuddyAction
        {
            RequestPartnership = 0,
            RejectRequest = 1,
            SetUserOnline,
            SetUserOffline,
            BreakPartnership = 4,
            AddPartner = 5,
            AwaitingPartnersList = 8
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Character target = RoleManager.GetUser(Identity);

            switch (Action)
            {
                case TradeBuddyAction.RequestPartnership:
                    {
                        if (target == null)
                        {
                            await user.SendAsync(StrTargetNotInRange);
                            return;
                        }

                        if (user.QueryRequest(RequestType.TradePartner) == target.Identity)
                        {
                            user.PopRequest(RequestType.TradePartner);
                            await user.TradePartnerRelation.CreateRelationAsync(target);
                            return;
                        }

                        target.SetRequest(RequestType.TradePartner, user.Identity);
                        Identity = user.Identity;
                        Name = user.Name;
                        await target.SendRelationAsync(user);
                        await target.SendAsync(this);
                        break;
                    }

                case TradeBuddyAction.RejectRequest:
                    {
                        if (target == null)
                        {
                            return;
                        }

                        Identity = user.Identity;
                        Name = user.Name;
                        IsOnline = true;
                        await target.SendAsync(this);
                        break;
                    }

                case TradeBuddyAction.BreakPartnership:
                    {
                        await user.TradePartnerRelation.DeleteRelationAsync(Identity);
                        break;
                    }
            }
        }
    }
}
