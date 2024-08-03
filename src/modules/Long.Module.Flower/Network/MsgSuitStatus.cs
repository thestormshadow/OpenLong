using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Flower.Repositories;
using Long.Module.Flower.States;

namespace Long.Module.Flower.Network
{
    public sealed class MsgSuitStatus : Kernel.Network.Game.Packets.MsgSuitStatus
    {
        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            if (user.Gender != 2 || user.Transformation != null)
            {
                return;
            }

            Param = (int)user.Identity;
            if (Action == 2)
            {
                user.Flower.FairyType = 0;
                await user.BroadcastRoomMsgAsync(this, true);
                return;
            }

            UserRanking ranking;
            UserRanking rankingToday;

            switch (Data) // validate :]
            {
                case 1000: // RedRose
                    {
                        ranking = DynaRankRepository.GetUserPos(user.Identity, RedRose, 100);
                        rankingToday = FlowerRepository.GetRedRoseTodayRank(user.Identity, 100);
                        break;
                    }

                case 1002: // Orchids
                    {
                        ranking = DynaRankRepository.GetUserPos(user.Identity, Orchid, 100);
                        rankingToday = FlowerRepository.GetOrchidsTodayRank(user.Identity, 100);
                        break;
                    }

                case 1003: // Tulips
                    {
                        ranking = DynaRankRepository.GetUserPos(user.Identity, Tulip, 100);
                        rankingToday = FlowerRepository.GetTulipsTodayRank(user.Identity, 100);
                        break;
                    }

                case 1001: // Lily
                    {
                        ranking = DynaRankRepository.GetUserPos(user.Identity, WhiteRose, 100);
                        rankingToday = FlowerRepository.GetWhiteRoseTodayRank(user.Identity, 100);
                        break;
                    }

                default:
                    {
                        return;
                    }
            }

            if ((ranking.Position <= 0 || ranking.Position > 100) && (rankingToday.Position <= 0 || rankingToday.Position > 100))
            {
                return; // not in top 100
            }

            // let's limit the amount of fairies (per type)
            int fairyCount = RoleManager.QueryUserSet().Count(x => x.Flower.FairyType == Data);
            if (fairyCount >= 3)
            {
                // message? na
                return;
            }

            if (user.Flower.FairyType != 0)
            {
                await user.BroadcastRoomMsgAsync(new MsgSuitStatus
                {
                    Action = 2,
                    Data = (int)user.Flower.FairyType,
                    Param = Param
                }, true);
            }

            user.Flower.FairyType = (uint)Data;
            await user.BroadcastRoomMsgAsync(this, true);
        }
    }
}
