namespace Long.Kernel.States.Events.Interfaces
{
    public interface ITournamentEventParticipant<TType>
    {
        uint Identity { get; }
        string Name { get; }

        TType Participant { get; }
        bool Bye { get; }
    }
}