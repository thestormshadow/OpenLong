using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Peerage
{
    public interface INobilityManager
    {
        Task DonateAsync(Character user, ulong amount);
        ulong GetDonation(int position);
        ulong GetNextRankSilver(NobilityRank rank, ulong donation);
        int GetPosition(uint idUser);
        NobilityRank GetRanking(uint idUser);
        Task InitializeAsync();
        Task SaveAsync();
        Task SendRankingAsync(Character target, int page);
    }
}
