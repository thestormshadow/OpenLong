using Long.Database.Entities;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Managers
{
    public interface IFamilyManager
    {
        IFamilyWarManager FamilyWar { get; }

        Task<bool> InitializeAsync();
        bool AddFamily(IFamily family);
        IFamily GetFamily(uint idFamily);
        IFamily GetFamily(string name);
        IFamily GetOccupyOwner(uint idNpc);
        IFamily FindByUser(uint idUser);
        IList<IFamily> QueryFamilies(Func<IFamily, bool> predicate);
        DbFamilyBattleEffectShareLimit GetSharedBattlePowerLimit(int level);
        Task SendNoFamilyAsync(Character user);

        Task<bool> CreateFamilyAsync(Character user, string name, uint proffer);
        Task<bool> ChangeFamilyNameAsync(IFamily family, string newName);
        Task<bool> DisbandFamilyAsync(Character user, IFamily family);
    }
}
