using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Modules.Systems.Flower;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Flower.Repositories;
using System.Globalization;

namespace Long.Module.Flower.States
{
    public sealed class Flower : IFlower
    {
        private readonly Character user;

        public Flower(Character user)
        {
            this.user = user;
        }

        public async Task InitializeAsync()
        {
            FlowerToday = await FlowerRepository.GetFromUserAsync(user.Identity);
            FlowerToday ??= new DbFlower
            {
                UserId = user.Identity
            };

            if (IsSendGiftEnable())
            {
                await user.SendAsync(new MsgFlower
                {
                    Mode = user.Gender == 1 ? MsgFlower.RequestMode.QueryFlower : MsgFlower.RequestMode.QueryGift,
                    RedRoses = 1
                });
            }
        }

        public uint RedRoses
        {
            get => user.FlowerRed;
            set => user.FlowerRed = value;
        }

        public uint WhiteRoses
        {
            get => user.FlowerWhite;
            set => user.FlowerWhite = value;
        }

        public uint Orchids
        {
            get => user.FlowerOrchid;
            set => user.FlowerOrchid = value;
        }

        public uint Tulips
        {
            get => user.FlowerTulip;
            set => user.FlowerTulip = value;
        }

        public uint LastFlowerDate
        {
            get => user.SendFlowerTime;
            set => user.SendFlowerTime = value;
        }

        public DbFlower FlowerToday { get; set; }

        public uint Charm { get; set; }
        public uint FairyType { get; set; }

        public bool IsSendGiftEnable()
        {
            if (LastFlowerDate == 0 && user.Level > 50)
            {
                return true;
            }

            if (LastFlowerDate == 0)
            {
                return true;
            }

            DateTime today = DateTime.Now.Date;
            DateTime lastSendFlower = DateTime.ParseExact(LastFlowerDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).Date;
            return lastSendFlower < today;
        }

        public Task SaveAsync()
        {
            if (FlowerToday.Identity != 0)
            {
                return ServerDbContext.UpdateAsync(FlowerToday);
            }
            return ServerDbContext.CreateAsync(FlowerToday);
        }
    }
}
