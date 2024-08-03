using Long.Kernel.States.User;

namespace Long.Kernel.States.Events.Interfaces
{
    public interface IWitnessEvent
    {
        Task WatchAsync(Character user, uint target);
        Task WitnessExitAsync(Character user);
        Task WitnessVoteAsync(Character user, uint target);
        bool IsWitness(Character user);
    }
}
