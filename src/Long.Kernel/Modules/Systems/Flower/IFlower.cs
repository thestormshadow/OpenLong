using Long.Database.Entities;
using Long.Kernel.Modules.Interfaces;

namespace Long.Kernel.Modules.Systems.Flower
{
    public interface IFlower : IInitializeSystem
    {
        uint RedRoses { get; set; }
        uint WhiteRoses { get; set; }
        uint Orchids { get; set; }
        uint Tulips { get; set; }
        uint LastFlowerDate { get; set; }

        uint Charm { get; set; }
        uint FairyType { get; set; }

        DbFlower FlowerToday { get; set; }

        Task SaveAsync();
    }
}
