using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLottery : MsgBase<GameClient>
    {
        public LotteryRequest Action { get; set; }
        public byte Unknown6 { get; set; }
        public byte SocketOne { get; set; }
        public byte SocketTwo { get; set; }
        public byte Addition { get; set; }
        public byte Color { get; set; }
        public byte UsedChances { get; set; }
        public uint ItemType { get; set; } // or chances
        public int Unknown16 { get; set; }
        public int Unknown20 { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (LotteryRequest)reader.ReadUInt16();
            Unknown6 = reader.ReadByte();
            SocketOne = reader.ReadByte();
            SocketTwo = reader.ReadByte();
            Addition = reader.ReadByte();
            Color = reader.ReadByte();
            UsedChances = reader.ReadByte();
            ItemType = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgLottery);
            writer.Write((ushort)Action);
            writer.Write(Unknown6);
            writer.Write(SocketOne);
            writer.Write(SocketTwo);
            writer.Write(Addition);
            writer.Write(Color);
            writer.Write(UsedChances);
            writer.Write(ItemType);
            return writer.ToArray();
        }

        public enum LotteryRequest : ushort
        {
            Accept = 0,
            AddJade = 1,
            Continue = 2,
            Show = 259
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Action)
            {
                case LotteryRequest.Accept:
                    {
                        await user.AcceptLotteryPrizeAsync();
                        break;
                    }
                case LotteryRequest.AddJade:
                    {
                        if (user.LotteryRetries > 2)
                        {
                            return;
                        }

                        if (!await user.UserPackage.SpendItemAsync(Item.SMALL_LOTTERY_TICKET))
                        {
                            return;
                        }

                        if (await user.PlayLotteryAsync(int.MaxValue))
                        {
                            user.LotteryRetries++;
                        }
                        break;
                    }
                case LotteryRequest.Continue:
                    {
                        if (!user.IsLotteryEnable)
                        {
                            return;
                        }

                        if (user.UserPackage.IsPackFull())
                        {
                            return;
                        }

                        if (!await user.UserPackage.SpendItemAsync(Item.SMALL_LOTTERY_TICKET, 3))
                        {
                            await user.SendAsync(StrEmbedNoRequiredItem);
                            return;
                        }

                        user.ResetLottery();

                        if (await user.PlayLotteryAsync(int.MaxValue))
                        {
                            await user.Statistic.IncrementDailyValueAsync(22, 0, 1);
                            user.LotteryRetries++;
                        }

                        break;
                    }
            }
        }
    }
}
