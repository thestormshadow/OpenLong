using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicateMember;
using static Long.Kernel.Modules.Systems.Totem.ITotem;

namespace Long.Module.Totem.Network
{
    public sealed class MsgTotemPole : MsgBase<GameClient>
    {
        public ActionMode Action { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int Unknown20 { get; set; }
        public int Unknown24 { get; set; }


        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (ActionMode)reader.ReadUInt32();
            Data1 = reader.ReadInt32();
            Data2 = reader.ReadInt32();
            Data3 = reader.ReadInt32();
            Unknown20 = reader.ReadInt32();
            Unknown24 = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTotemPole);
            writer.Write((uint)Action);
            writer.Write(Data1);
            writer.Write(Data2);
            writer.Write(Data3);
            writer.Write(Unknown20);
            writer.Write(Unknown24);
            return writer.ToArray();
        }

        public enum ActionMode
        {
            UnlockArsenal,
            InscribeItem,
            UnsubscribeItem,
            Enhance,
            Refresh
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = RoleManager.GetUser(client.Character.Identity);
            if (user == null)
            {
                client.Disconnect();
                return;
            }

            if (user.Syndicate?.Totem == null)
            {
                return;
            }

            switch (Action)
            {
                case ActionMode.UnlockArsenal:
                    {
                        if (user.SyndicateRank != SyndicateRank.GuildLeader
                            && user.SyndicateRank != SyndicateRank.DeputyLeader
                            && user.SyndicateRank != SyndicateRank.HonoraryDeputyLeader
                            && user.SyndicateRank != SyndicateRank.LeaderSpouse)
                        {
                            return;
                        }

                        TotemPoleType type = (TotemPoleType)Data1;
                        if (type == TotemPoleType.None)
                        {
                            return;
                        }

                        if (user.Syndicate.Totem.LastOpenTotem != null)
                        {
                            int now = int.Parse($"{DateTime.Now:yyyyMMdd}");
                            int lastOpenTotem = int.Parse($"{user.Syndicate.Totem.LastOpenTotem.Value:yyyyMMdd}");
                            if (lastOpenTotem >= now)
                            {
                                return;
                            }
                        }

                        int price = user.Syndicate.Totem.UnlockTotemPolePrice();
                        if (user.Syndicate.Money < price)
                        {
                            return;
                        }

                        if (!await user.Syndicate.Totem.OpenTotemPoleAsync(type))
                        {
                            return;
                        }

                        user.Syndicate.Money -= price;
                        await user.Syndicate.SaveAsync();

                        await user.Syndicate.Totem.SendTotemPolesAsync(user);
                        await user.Syndicate.SendSyndicateToUserAsync(user);
                        break;
                    }

                case ActionMode.InscribeItem:
                    {
                        Item item = user.UserPackage.GetInventory((uint)Data2);
                        if (item == null)
                        {
                            return;
                        }

                        await user.Syndicate.Totem.InscribeItemAsync(user, item);
                        break;
                    }

                case ActionMode.UnsubscribeItem:
                    {
                        await user.Syndicate.Totem.UnsubscribeItemAsync((uint)Data2, user.Identity);
                        break;
                    }

                case ActionMode.Enhance:
                    {
                        if (user.SyndicateRank != SyndicateRank.GuildLeader)
                        {
                            return;
                        }

                        if (await user.Syndicate.Totem.EnhanceTotemPoleAsync((TotemPoleType)Data1, (byte)Data3))
                        {
                            await user.Syndicate.Totem.SendTotemPolesAsync(user);
                            await user.Syndicate.SendSyndicateToUserAsync(user);
                        }

                        break;
                    }

                case ActionMode.Refresh:
                    {
                        await user.Syndicate.Totem.SendTotemPolesAsync(user);
                        break;
                    }
            }
        }
    }
}
