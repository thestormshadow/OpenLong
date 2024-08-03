using Long.Kernel.States.User;

namespace Long.Kernel.States.Events.Interfaces
{
    /// <summary>
    /// Type of interface to be implemented on tournament like events.
    /// </summary>
    /// <typeparam name="TParticipant">Character, Team, Guild, Family etc</typeparam>
    public interface ITournamentEvent<TParticipant, TEntity> where TParticipant 
        : ITournamentEventParticipant<TEntity>
    {
        Task<bool> InscribeAsync(TParticipant entity);
        Task UnsubscribeAsync(TParticipant entity);
        Task UnsubscribeAsync(uint identity);

        Task SubmitEventWindowAsync(Character target, int page = 0);
    }
}
