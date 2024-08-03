using Long.Network.Packets.Cross;

namespace Long.Kernel.Modules.Systems.AstProf
{
    public interface IAstProf
    {
        int Count { get; }

        Task<bool> InitializeAsync();
        Task InitializeOSInfoAsync(List<CrossAstProfInfoPB> data);
        Task<bool> LearnAsync(AstProfType type, bool actionOnly = false);
        Task<bool> UpLevAsync(AstProfType type);
        Task<bool> PromoteAsync(AstProfType type);
        Task<bool> PromoteAsync(AstProfType type, int rank);
        Task<bool> ActivateAsync(AstProfType type);
        int GetPower(AstProfType type);
        int GetLevel(AstProfType type);
        int GetPromotion(AstProfType type);
        Task SendAsync();
        Task UpdateStudyAsync(uint add = 0);
        Task TransferOSDataAsync(ulong sessionId, uint idServer);

        public enum AstProfType : byte
        {
            None = 0,
            MartialArtist = 1,
            Warlock = 2,
            ChiMaster = 3,
            Sage = 4,
            Apothecary = 5,
            Performer = 6,
            Wrangler = 9
        }
    }
}
