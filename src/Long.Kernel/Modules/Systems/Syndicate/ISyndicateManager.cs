using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Syndicate
{
    public interface ISyndicateManager
    {
        Task<bool> InitializeAsync();
        Task<bool> CreateSyndicateAsync(Character user,string name, int price = 1000000);
        Task<bool> ChangeSyndicateNameAsync(Character user, string newName);
        Task<bool> DisbandSyndicateAsync(Character user);
        bool AddSyndicate(ISyndicate syn);
        ISyndicate GetSyndicate(int idSyndicate);
        ISyndicate GetSyndicate(string name);
        ISyndicate FindByUser(uint idUser);
        ISyndicate GetSyndicate(uint ownerIdentity);
        List<ISyndicate> GetByLeague(uint leagueId);
        bool HasSyndicateAdvertise(uint idSyn);
        Task JoinByAdvertisingAsync(Character user, ushort syndicateIdentity);
        Task SubmitEditAdvertiseScreenAsync(Character user);
        Task PublishAdvertisingAsync(Character user,
            long money,
            string description,
            int requiredLevel,
            int requiredMetempsychosis,
            int requiredProfession,
            int conditionBattle, // not sure
            int conditionSex,
            bool autoJoin);
        Task ReplaceAdvertisingAsync(Character user,
            long money,
            string description,
            int requiredLevel,
            int requiredMetempsychosis,
            int requiredProfession,
            int conditionBattle, // not sure
            int conditionSex,
            bool autoJoin);
        Task SubmitAdvertisingListAsync(Character user, int startIndex);
        Task OnTimerAsync();
    }
}
