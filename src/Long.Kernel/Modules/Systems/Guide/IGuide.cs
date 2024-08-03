namespace Long.Kernel.Modules.Systems.Guide
{
    public interface IGuide
    {
        public ITutor Tutor { get; set; }

        ulong MentorExpTime { get; set; }
        ushort MentorAddLevexp { get; set; }
        ushort MentorGodTime { get; set; }
        int ApprenticeCount { get; }

        bool AddStudent(ITutor student);
        ITutor GetStudent(uint idStudent);
        Task InitializeAsync();
        bool IsApprentice(uint idGuide);
        bool IsTutor(uint idApprentice);
        Task OnLogoutAsync();
        void RemoveApprentice(uint idApprentice);
        Task SaveAsync();
        Task SynchroApprenticesSharedBattlePowerAsync();
        Task SynchroStudentsAsync();
    }
}
