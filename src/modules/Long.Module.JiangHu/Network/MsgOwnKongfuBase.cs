using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Serilog;
using static Long.Kernel.States.User.Character;

namespace Long.Module.JiangHu.Network
{
    public sealed class MsgOwnKongfuBase : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgOwnKongfuBase>();

        public enum KongfuBaseMode : byte
        {
            IconBar = 0,
            SetName = 1,
            UpdateTalent = 5,
            SendStatus = 7,
            QueryTargetInfo = 9,
            RestoreStar = 10,
            UpdateStar = 11,
            OpenStage = 12,
            UpdateTime = 13,
            SendInfo = 14,
            ProtectionPillUsage = 16,
            GatherTalentPoints = 17
        }

        public KongfuBaseMode Mode { get; set; }
        public List<string> Strings { get; set; } = new List<string>();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Mode = (KongfuBaseMode)reader.ReadByte();
            Strings = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgOwnKongfuBase);
            writer.Write((byte)Mode);
            writer.Write(Strings);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Mode)
            {
                case KongfuBaseMode.SetName:
                    {
                        if (Strings.Count < 1)
                        {
                            logger.Error("Set name called without strings!! {0}", user.Name);
                            return;
                        }

                        await user.JiangHu.CreateAsync(Strings[0]);
                        break;
                    }

                case KongfuBaseMode.UpdateTime:
                    {
                        await user.JiangHu.SendTimeAsync();
                        break;
                    }

                case KongfuBaseMode.QueryTargetInfo:
                    {
                        if (uint.TryParse(Strings[0], out var idUser))
                        {
                            Character target = RoleManager.GetUser(idUser);
                            if (target != null)
                            {
                                await target.JiangHu.SendInfoAsync(user);
                                await target.JiangHu.SendStarsAsync(user);
                                await target.JiangHu.SendStarAsync(user);
                                await target.JiangHu.SendTimeAsync(user);
                            }
                        }
                        break;
                    }

                case KongfuBaseMode.RestoreStar:
                    {
                        if (Strings.Count < 2)
                        {
                            return;
                        }

                        if (!byte.TryParse(Strings[0], out var powerLevel) || !byte.TryParse(Strings[1], out var star))
                        {
                            return;
                        }

                        if (!await user.SpendBoundConquerPointsAsync(EmoneyOperationType.JiangHuRestore,  20, true))
                        {
                            return;
                        }

                        await user.JiangHu.RestoreAsync(powerLevel, star);
                        break;
                    }

                case KongfuBaseMode.ProtectionPillUsage:
                    {
                        if (Strings.Count < 2)
                        {
                            return;
                        }

                        if (!byte.TryParse(Strings[0], out var powerLevel) || !byte.TryParse(Strings[1], out var star))
                        {
                            return;
                        }

                        if (!await user.UserPackage.SpendItemAsync(Item.PROTECTION_PILL)
                            && !await user.UserPackage.SpendItemAsync(Item.SUPER_PROTECTION_PILL))
                        {
                            return;
                        }

                        await user.JiangHu.RestoreAsync(powerLevel, star);
                        break;
                    }

                case KongfuBaseMode.GatherTalentPoints:
                    {
                        if (user.JiangHu?.HasJiangHu != true)
                        {
                            return;
                        }

                        // Talent 1 = 3 CPs
                        if (!await user.SpendBoundConquerPointsAsync(EmoneyOperationType.JiangHuGatherPoints, 20, true))
                        {
                            return;
                        }

                        await user.JiangHu.AwardTalentAsync(1);
                        break;
                    }
            }
        }
    }
}
