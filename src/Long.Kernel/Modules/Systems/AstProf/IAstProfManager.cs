using Long.Database.Entities;
using Long.Kernel.States.User;
using static Long.Kernel.Modules.Systems.AstProf.IAstProf;

namespace Long.Kernel.Modules.Systems.AstProf
{
    public interface IAstProfManager
    {
        Task InitializeAsync();

        IAstProf CreateOSAstProf(Character user);

        Task<bool> CanInaugurateAsync(Character user, AstProfType type);
        Task<bool> CanUpLevAsync(Character user, AstProfType type, int currentLevel);
        bool CanPromote(Character user, AstProfType type, byte level);

        byte GetRank(Character user, AstProfType type);
        void SetRank(Character user, AstProfType type, byte value);
        int GetPower(AstProfType type, int level);

        DbLevelExperience GetAstProfExperience(AstProfType type, int currentLevel);
    }
}
