using Long.Database.Entities;
using Long.Kernel.States.User;
using static Long.Kernel.Modules.Systems.Fate.IFate;

namespace Long.Kernel.Modules.Systems.Fate
{
    public interface IFateManager
    {
        T GetFateManager<T>() where T: IFateManager;

        Task InitializeAsync();
        Task InitialFateAttributeAsync(Character user, FateType type, DbFatePlayer fate);
        Task<bool> GenerateAsync(Character user, FateType type, DbFatePlayer fate, TrainingSave flag);
        void UpdateStatus(Character user);
        int GetPower(DbFatePlayer fate, TrainingAttrType attr);
        int GetPowerByIndex(DbFatePlayer fate, FateType type, int num);
        void SetAttribute(DbFatePlayer fate, FateType type, int num, int value);
        int GetScore(DbFatePlayer record, FateType type);
        int GetPlayerRank(uint idUser, FateType mode);
        DbFateRule GetRule(FateType type, TrainingAttrType attrType);
        DbConfig GetInitializationRequirements(FateType type);
        TrainingAttrType ReferenceType(int power);
        Task SaveRankAsync();

        IFate CreateOSFate(Character user);
    }
}
