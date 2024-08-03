using Long.Kernel.States.User;
using static Long.Kernel.Network.Game.Packets.MsgItem;

namespace Long.Module.Booth.Network
{
    public static class MsgItem
    {
        public static async Task ProcessAsync(Kernel.Network.Game.Packets.MsgItem msg, Character user)
        {
            switch (msg.Action)
            {
                case ItemActionType.BoothQuery: // 21
                    {
                        var targetNpc = user.Screen.Roles.Values.FirstOrDefault(x => x is Character targetUser && targetUser.Booth?.Identity == msg.Identity) as Character;
                        if (targetNpc?.Booth == null)
                        {
                            return;
                        }

                        await targetNpc.Booth.QueryItemsAsync(user);
                        break;
                    }

                case ItemActionType.BoothSell: // 22
                    {
                        if (user.Booth == null)
                        {
                            return;
                        }

                        if (!user.Booth.ValidateItem(msg.Identity))
                        {
                            return;
                        }

                        if (user.Booth.AddItem(user.UserPackage.GetInventory(msg.Identity), msg.Command, Moneytype.Silver))
                        {
                            await user.SendAsync(msg);
                        }
                        break;
                    }

                case ItemActionType.BoothRemove: // 23
                    {
                        if (user.Booth == null)
                        {
                            return;
                        }

                        if (user.Booth.RemoveItem(msg.Identity))
                        {
                            await user.SendAsync(msg);
                        }
                        break;
                    }

                case ItemActionType.BoothPurchase: // 24
                    {
                        var target = user.Screen.Roles.Values.FirstOrDefault(x => x is Character targetUser && targetUser.Booth?.Identity == msg.Command) as Character;
                        if (target?.Booth == null)
                        {
                            return;
                        }

                        if (await target.Booth.SellBoothItemAsync(msg.Identity, user))
                        {
                            msg.Action = ItemActionType.BoothRemove;
                            await target.SendAsync(msg);
                            await user.SendAsync(msg);
                        }
                        break;
                    }

                case ItemActionType.BoothEmoneySell: // 29
                    {
                        if (user.Booth == null)
                        {
                            return;
                        }

                        if (!user.Booth.ValidateItem(msg.Identity))
                        {
                            return;
                        }

                        if (user.Booth.AddItem(user.UserPackage.GetInventory(msg.Identity), msg.Command, Moneytype.ConquerPoints))
                        {
                            await user.SendAsync(msg);
                        }
                        break;
                    }
            }
        }
    }
}
