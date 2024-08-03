using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Module.NeiGong.Network
{
    public sealed class MsgInnerStrengthOpt : MsgBase<GameClient>
    {
        public enum InnerStrengthOptType
        {
            View = 0,
            Perfect = 1,
            Reshape = 2,
            UpLevel = 3,
            UnLock = 4,
            Display = 5,
            Transfer = 6,
        }

        public InnerStrengthOptType Action { get; set; }
        public int Param { get; set; }
        public int Data { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (InnerStrengthOptType)reader.ReadByte();
            Param = reader.ReadInt32();
            Data = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgInnerStrengthOpt);
            writer.Write((byte)Action);
            writer.Write(Param);
            writer.Write(Data);
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (!user.IsAlive)
            {
                return;
            }

            switch (Action)
            {
                case InnerStrengthOptType.View:
                    {
                        if (Param == user.Identity)
                        {
                            await user.InnerStrength.SendInfoAsync((byte)Data);
                            return;
                        }

                        Character targetUser = RoleManager.GetUser((uint)Param);
                        if (targetUser != null)
                        {
                            await user.InnerStrength.SendInfoAsync((byte)Data, targetUser);
                        }
                        break;
                    }

                case InnerStrengthOptType.Perfect:
                    {
                        if (await user.InnerStrength.PerfectAsync((byte)Param))
                        {
                            await user.SendAsync(this);
                        }
                        break;
                    }

                case InnerStrengthOptType.Reshape:
                    {
                        Item item = user.UserPackage.GetItemByType(Item.POWER_ERASER);
                        if (item == null)
                        {
                            return;
                        }

                        if (await user.InnerStrength.ReshapeAsync((byte)Param))
                        {
                            await user.UserPackage.SpendItemAsync(item);
                            await user.SendAsync(this);
                        }
                        break;
                    }

                case InnerStrengthOptType.UpLevel:
                    {
                        if (await user.InnerStrength.UpLevelAsync((byte)Param, (byte)Data))
                        {
                            await user.SendAsync(this);
                        }
                        break;
                    }

                case InnerStrengthOptType.UnLock:
                    {
                        LuaScriptManager.Run(user, null, null, Array.Empty<string>(), $"InternalSystem_InnerType({Param},{user.Identity})");
                        break;
                    }

                case InnerStrengthOptType.Display:
                    {
                        if (Param == user.Identity)
                        {
                            await user.InnerStrength.SendFullAsync();
                            return;
                        }

                        Character targetUser = RoleManager.GetUser((uint)Param);
                        if (targetUser != null)
                        {
                            await user.InnerStrength.SendAsync(targetUser);
                        }
                        break;
                    }
            }
        }
    }
}
