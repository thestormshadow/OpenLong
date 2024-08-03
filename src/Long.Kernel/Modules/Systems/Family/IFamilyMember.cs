using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Family
{
    public interface IFamilyMember
    {
        uint FamilyIdentity { get; }
        string FamilyName { get; }
        uint Identity { get; }
        bool IsOnline { get; }
        DateTime JoinDate { get; }
        byte Level { get; }
        uint LookFace { get; }
        uint MateIdentity { get; }
        string Name { get; }
        ushort Profession { get; }
        uint Proffer { get; set; }
        IFamily.FamilyRank Rank { get; set; }
        Character User { get; }

        void ChangeName(string name);
        Task<bool> DeleteAsync();
        Task<bool> SaveAsync();
    }
}
