using Long.Kernel.Managers;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using System.Data;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgSlotAction : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgSlotAction>();

        public SlotActionType Action { get; set; }
        public byte Multiplier { get; set; }
        public uint Identity { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (SlotActionType)reader.ReadByte();
            Multiplier = reader.ReadByte();
            reader.ReadUInt16();
            Identity = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSlotAction);
            writer.Write((byte)Action);
            writer.Write(Multiplier);
            writer.Write((ushort)0);
            writer.Write(Identity);
            return writer.ToArray();
        }

        public enum SlotActionType : ushort
        {
            Start = 0,
            Stop = 1,
            Finish = 2
        }

        public enum SlotActionItems : byte
        {
            Stancher = 0,
            Meteor = 1,
            Sword = 2,
            TwoSwords = 3,
            SwordAndShield = 4,
            ExpBall = 5,
            DragonBall = 6
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user == null)
            {
                return;
            }

            switch (Action)
            {
                case SlotActionType.Start:
                    {
                        BaseNpc slotNpc = user.QueryRole(Identity) as BaseNpc;
                        if (slotNpc == null)
                        {
                            logger.Warning("Attempt to get unexisting slot npc {0}", Identity);
                            return;
                        }

                        if (!slotNpc.IsSlotNpc())
                        {
                            logger.Warning("Attempt to get invalid slot npc {0} {1} {2} {3}", slotNpc.Identity, slotNpc.Name, user.Identity, user.Name);
                            return;
                        }

                        if (user.GetDistance(slotNpc) > Screen.VIEW_SIZE)
                        {
                            return;
                        }

                        SlotMachineManager.SlotWinningRuleType ruleType = (SlotMachineManager.SlotWinningRuleType)slotNpc.Data0;
                        uint price = 0;
                        Multiplier = Math.Max((byte)1, Math.Min((byte)3, Multiplier));
                        switch (ruleType)
                        {
                            case SlotMachineManager.SlotWinningRuleType.Money:
                                {
                                    const uint MONEY = 10_000;
                                    price = Multiplier * MONEY;

                                    if (!await user.SpendMoneyAsync((int)price, true))
                                    {
                                        return;
                                    }
                                    break;
                                }
                            case SlotMachineManager.SlotWinningRuleType.Emoney:
                                {
                                    price = (uint)(Multiplier * slotNpc.Data1);

                                    if (!await user.SpendConquerPointsAsync((int)price, true))
                                    {
                                        return;
                                    }

                                    await user.SaveEmoneyLogAsync(Character.EmoneyOperationType.SlotMachine, slotNpc.Identity, 0, price);
                                    break;
                                }
                            default:
                                {
                                    logger.Error("Invalid reward type [{0}] for slot npc {1} {2}", slotNpc.Data0, user.Identity, user.Name);
                                    return;
                                }
                        }

                        SlotMachineManager.WinningRule result = await SlotMachineManager.GetWinningRuleAsync(ruleType);
                        if (result.Equals(default))
                        {
                            return;
                        }

                        user.SlotMachineReward = result.Multiple * price;
                        user.SlotMachineId = slotNpc.Identity;
                        user.SlotMachineName = slotNpc.Name;
                        user.SlotWinningRuleType = ruleType;
                        user.SlotMachineResult = result;

                        await user.SendAsync(new MsgSlotResult
                        {
                            Action = MsgSlotResult.SlotResultType.Start,
                            Identity = slotNpc.Identity,
                            One = (byte)(result.Pattern / 100),
                            Two = (byte)((result.Pattern % 100) / 10),
                            Three = (byte)(result.Pattern % 10)
                        });
                        break;
                    }

                case SlotActionType.Finish:
                    {
                        SlotMachineManager.WinningRule result = user.SlotMachineResult.GetValueOrDefault();
                        if (result.Equals(default))
                        {
                            return;
                        }

                        await user.GetSlotMachineRewardAsync();
                        break;
                    }
            }
        }
    }
}