using Long.Kernel.Modules.Interfaces;

namespace Long.Kernel.Modules.Systems.Nobility
{
    public interface INobility : IInitializeSystem
    {
        NobilityRank Rank { get; }
        int Position { get; }
        ulong Donation { get; set; }

        void SetRank(NobilityRank rank);
        Task BroadcastAsync();
    }
}
