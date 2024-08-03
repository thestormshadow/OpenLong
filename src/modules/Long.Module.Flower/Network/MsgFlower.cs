using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using static Long.Kernel.StrRes;
using static Long.Kernel.Modules.Systems.Rank.IDynamicRankManager;

namespace Long.Module.Flower.Network
{
    public class MsgFlower : Kernel.Network.Game.Packets.MsgFlower
    {
        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Mode)
            {
                case RequestMode.SendGift:
                case RequestMode.SendFlower:
                {
                    uint idTarget = Identity;

                    Character target = RoleManager.GetUser(idTarget);

                    if (!user.IsAlive)
                    {
                        await user.SendAsync(StrFlowerSenderNotAlive);
                        return;
                    }

                    if (target == null)
                    {
                        await user.SendAsync(StrTargetNotOnline);
                        return;
                    }

                    if (user.Gender == target.Gender)
                    {
                        await user.SendAsync(StrGiftSameGender);
                        return;
                    }

                    if (user.Level < 50)
                    {
                        await user.SendAsync(StrFlowerLevelTooLow);
                        return;
                    }

                    string giftName = StrFlowerNameRed;
                    FlowerType type = SendFlowerType;
                    FlowerEffect effect = FlowerEffect.RedRose;

                    if (user.Gender == 2)
                    {
                        type += 4;
                        effect = FlowerEffect.Kiss + 4;
                    }

                    ushort amount;
                    if (ItemIdentity == 0) // daily flower
                    {
                        if (user.SendFlowerTime != 0
                            && user.SendFlowerTime >= int.Parse(DateTime.Now.ToString("yyyyMMdd")))
                        {
                            await user.SendAsync(StrFlowerHaveSentToday);
                            return;
                        }

                        switch (user.VipLevel)
                        {
                            case 0:
                                amount = 1;
                                break;
                            case 1:
                                amount = 2;
                                break;
                            case 2:
                                amount = 5;
                                break;
                            case 3:
                                amount = 7;
                                break;
                            case 4:
                                amount = 9;
                                break;
                            case 5:
                                amount = 12;
                                break;
                            default:
                                amount = 30;
                                break;
                        }

                        user.SendFlowerTime = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
                        await user.SaveAsync();
                    }
                    else
                    {
                        Item flowerItem = user.UserPackage.FindItemByIdentity(ItemIdentity);
                        if (flowerItem == null)
                        {
                            return;
                        }

                        switch (flowerItem.GetItemSubType())
                        {
                            case 751:
                                type = FlowerType.RedRose;
                                effect = FlowerEffect.RedRose;
                                giftName = StrFlowerNameRed;
                                break;
                            case 752:
                                type = FlowerType.WhiteRose;
                                effect = FlowerEffect.WhiteRose;
                                giftName = StrFlowerNameWhite;
                                break;
                            case 753:
                                type = FlowerType.Orchid;
                                effect = FlowerEffect.Orchid;
                                giftName = StrFlowerNameLily;
                                break;
                            case 754:
                                type = FlowerType.Tulip;
                                effect = FlowerEffect.Tulip;
                                giftName = StrFlowerNameTulip;
                                break;
                            case 755:
                                type = FlowerType.Kiss;
                                effect = FlowerEffect.Kiss;
                                giftName = StrGiftKisses;
                                break;
                            case 756:
                                type = FlowerType.Love;
                                effect = FlowerEffect.Love;
                                giftName = StrGiftLoveLetters;
                                break;
                            case 757:
                                type = FlowerType.Tins;
                                effect = FlowerEffect.Tins;
                                giftName = StrGiftTinsOfBeer;
                                break;
                            case 758:
                                type = FlowerType.Jade;
                                effect = FlowerEffect.Jade;
                                giftName = StrGiftJades;
                                break;
                        }

                        amount = flowerItem.Durability;
                        await user.UserPackage.SpendItemAsync(flowerItem);
                    }

                    switch (type)
                    {
                        case FlowerType.RedRose:
                        case FlowerType.Kiss:
                            target.Flower.RedRoses += amount;
                            target.Flower.FlowerToday.RedRose += amount;
                            if (type == FlowerType.RedRose)
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(RedRose, target.Identity,
                                    target.FlowerRed);
                            }
                            else
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(Kiss, target.Identity,
                                    target.FlowerRed);
                            }

                            break;
                        case FlowerType.WhiteRose:
                        case FlowerType.Love:
                            target.Flower.WhiteRoses += amount;
                            target.Flower.FlowerToday.WhiteRose += amount;
                            if (type == FlowerType.WhiteRose)
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(WhiteRose, target.Identity,
                                    target.FlowerWhite);
                            }
                            else
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(Love, target.Identity,
                                    target.FlowerWhite);
                            }

                            break;
                        case FlowerType.Orchid:
                        case FlowerType.Tins:
                            target.Flower.Orchids += amount;
                            target.Flower.FlowerToday.Orchids += amount;
                            if (type == FlowerType.Orchid)
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(Orchid, target.Identity,
                                    target.FlowerOrchid);
                            }
                            else
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(Tins, target.Identity,
                                    target.FlowerOrchid);
                            }

                            break;
                        case FlowerType.Tulip:
                        case FlowerType.Jade:
                            target.Flower.Tulips += amount;
                            target.Flower.FlowerToday.Tulips += amount;
                            if (type == FlowerType.Tulip)
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(Tulip, target.Identity,
                                    target.FlowerTulip);
                            }
                            else
                            {
                                await ModuleManager.DynamicRankManager.CreateOrUpdateAsync(Jade, target.Identity,
                                    target.FlowerTulip);
                            }

                            break;
                    }

                    if (user.Gender == 2)
                    {
                        await user.SendAsync(StrFlowerSendSuccess);
                    }
                    else
                    {
                        await user.SendAsync(StrGiftSendSuccess);
                    }

                    await user.Achievements.AwardAchievementAsync(AchievementManager.AchievementType.Whatabeautifulflower);
                    await target.Achievements.AwardAchievementAsync(AchievementManager.AchievementType.Whatabeautifulflower);

                    if (ItemIdentity != 0 && amount >= 99)
                    {
                        await RoleManager.BroadcastWorldMsgAsync(
                            string.Format(StrFlowerGmPromptAll, user.Name, amount, giftName, target.Name),
                            TalkChannel.Center);
                    }

                    await target.SendAsync(string.Format(StrFlowerReceiverPrompt, user.Name));
                    await user.BroadcastRoomMsgAsync(new MsgFlower
                    {
                        Identity = Identity,
                        ItemIdentity = ItemIdentity,
                        SenderName = user.Name,
                        ReceiverName = target.Name,
                        SendAmount = amount,
                        SendFlowerType = type,
                        SendFlowerEffect = effect,
                        FlowerIdentity = ItemIdentity,
                        Mode = user.Gender == 1 ? RequestMode.SendFlower : RequestMode.SendGift
                    }, true);

                    MsgFlower msg = new()
                    {
                        SenderName = user.Name,
                        ReceiverName = target.Name,
                        SendAmount = amount
                    };
                    await user.SendAsync(msg);
                    await target.Flower.SaveAsync();

                    await ActivityManager.UpdateTaskActivityAsync(user, ActivityManager.ActivityType.FlowerGifts);
                    break;
                }
            }
        }
    }
}