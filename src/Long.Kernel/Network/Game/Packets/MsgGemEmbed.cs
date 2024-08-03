using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgGemEmbed : MsgBase<GameClient>
    {
        public uint Timestamp { get; private set; }
        public uint Identity { get; set; }
        public uint MainIdentity { get; set; }
        public uint MinorIdentity { get; set; }
        public ushort Position { get; set; }
        public EmbedAction Action { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadUInt32();
            Identity = reader.ReadUInt32();
            MainIdentity = reader.ReadUInt32();
            MinorIdentity = reader.ReadUInt32();
            Position = reader.ReadUInt16();
            Action = (EmbedAction)reader.ReadUInt16();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgGemEmbed);
            writer.Write(Timestamp = (uint)Environment.TickCount);
            writer.Write(Identity);
            writer.Write(MainIdentity);
            writer.Write(MinorIdentity);
            writer.Write(Position);
            writer.Write((ushort)Action);
            return writer.ToArray();
        }

        public enum EmbedAction : ushort
        {
            Embed = 0,
            TakeOff = 1
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Item item = user.UserPackage.FindItemByIdentity(MainIdentity);
            if (item == null)
            {
                return;
            }

            switch (Action)
            {
                case EmbedAction.Embed:
                    {
                        Item minor = user.UserPackage.GetInventory(MinorIdentity);
                        if (minor == null || !minor.IsGem())
                        {
                            await user.SendAsync(StrNoGemEmbed);
                            return;
                        }

                        Item.SocketGem gem = (Item.SocketGem)(minor.Type % 1000);
                        if (!Enum.IsDefined(typeof(Item.SocketGem), (byte)gem))
                        {
                            await user.SendAsync(StrNoGemEmbed);
                            return;
                        }

                        if (item.IsAttackTalisman())
                        {
                            switch (gem)
                            {
                                case Item.SocketGem.NormalThunderGem:
                                case Item.SocketGem.RefinedThunderGem:
                                case Item.SocketGem.SuperThunderGem:
                                    break;
                                default:
                                    await user.SendAsync(StrNoGemEmbed);
                                    return;
                            }
                        }
                        else if (item.IsDefenseTalisman())
                        {
                            switch (gem)
                            {
                                case Item.SocketGem.NormalGloryGem:
                                case Item.SocketGem.RefinedGloryGem:
                                case Item.SocketGem.SuperGloryGem:
                                    break;
                                default:
                                    await user.SendAsync(StrNoGemEmbed);
                                    return;
                            }
                        }
                        else if (item.IsWing())
                        {
                            if (Position == 1)
                            {
                                switch (gem)
                                {
                                    case Item.SocketGem.NormalThunderGem:
                                    case Item.SocketGem.RefinedThunderGem:
                                    case Item.SocketGem.SuperThunderGem:
                                        break;
                                    default:
                                        await user.SendAsync(StrNoGemEmbed);
                                        return;
                                }
                            }
                            else
                            {
                                switch (gem)
                                {
                                    case Item.SocketGem.NormalGloryGem:
                                    case Item.SocketGem.RefinedGloryGem:
                                    case Item.SocketGem.SuperGloryGem:
                                        break;
                                    default:
                                        await user.SendAsync(StrNoGemEmbed);
                                        return;
                                }
                            }
                        }
                        else
                        {
                            switch (gem)
                            {
                                case Item.SocketGem.NormalGloryGem:
                                case Item.SocketGem.RefinedGloryGem:
                                case Item.SocketGem.SuperGloryGem:
                                case Item.SocketGem.NormalThunderGem:
                                case Item.SocketGem.RefinedThunderGem:
                                case Item.SocketGem.SuperThunderGem:
                                    await user.SendAsync(StrNoGemEmbed);
                                    return;
                                default:
                                    break;
                            }
                        }


                        if (Position == 1 || (Position == 2 && item.SocketOne == Item.SocketGem.EmptySocket))
                        {
                            if (item.SocketOne == Item.SocketGem.NoSocket)
                            {
                                await user.SendAsync(StrEmbedTargetNoSocket);
                                return;
                            }

                            if (item.SocketOne != Item.SocketGem.EmptySocket)
                            {
                                await user.SendAsync(StrEmbedSocketAlreadyFilled);
                                return;
                            }

                            if (!await user.UserPackage.SpendItemAsync(minor))
                            {
                                await user.SendAsync(StrEmbedNoRequiredItem);
                                return;
                            }

                            item.SocketOne = gem;
                            break;
                        }

                        if (Position == 2)
                        {
                            if (item.SocketOne == Item.SocketGem.NoSocket || item.SocketOne == Item.SocketGem.EmptySocket)
                            {
                                return;
                            }

                            if (item.SocketTwo == Item.SocketGem.NoSocket)
                            {
                                await user.SendAsync(StrEmbedNoSecondSocket);
                                return;
                            }

                            if (item.SocketTwo != Item.SocketGem.EmptySocket)
                            {
                                await user.SendAsync(StrEmbedSocketAlreadyFilled);
                                return;
                            }

                            if (!await user.UserPackage.SpendItemAsync(minor))
                            {
                                await user.SendAsync(StrEmbedNoRequiredItem);
                                return;
                            }

                            item.SocketTwo = gem;
                            break;
                        }


                        break;
                    }

                case EmbedAction.TakeOff:
                    {
                        if (Position == 1)
                        {
                            if (item.SocketOne == Item.SocketGem.NoSocket)
                            {
                                return;
                            }

                            if (item.SocketOne == Item.SocketGem.EmptySocket)
                            {
                                return;
                            }

                            item.SocketOne = Item.SocketGem.EmptySocket;

                            if (item.SocketTwo != Item.SocketGem.NoSocket && item.SocketTwo != Item.SocketGem.EmptySocket)
                            {
                                item.SocketOne = item.SocketTwo;
                                item.SocketTwo = Item.SocketGem.EmptySocket;
                            }

                            await item.SaveAsync();
                            break;
                        }

                        if (Position == 2)
                        {
                            if (item.SocketTwo == Item.SocketGem.NoSocket)
                            {
                                return;
                            }

                            if (item.SocketTwo == Item.SocketGem.EmptySocket)
                            {
                                return;
                            }

                            item.SocketTwo = Item.SocketGem.EmptySocket;
                            await item.SaveAsync();
                            break;
                        }

                        break;
                    }
            }

            if (item.MaximumDurability < item.Durability)
            {
                item.Durability = item.MaximumDurability;
            }

            await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
            await user.SendAsync(this);
            await item.SaveAsync();
        }
    }
}
