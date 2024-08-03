using Long.Database.Entities;
using Long.Kernel.Modules.Interfaces;

namespace Long.Kernel.Modules.Systems.TaskDetail
{
    public interface ITaskDetail : IInitializeSystem
    {
        public static readonly uint[] DailyTasks =
        {
            6329, // Treasure Chest
            6245, // Everything has a price
            6049, // Fellow Fighters
            6366, // Rare Materials
            2375, // Spirit Beads
            6126, // Tower of Mistery
            6014 // Magnolias all around
        };

        Task<bool> CreateNewAsync(uint idTask);
        DbTaskDetail QueryTaskData(uint idTask);
        Task<bool> SetCompleteAsync(uint idTask, int value, bool save = true);
        int GetData(uint idTask, string name);
        Task<bool> AddDataAsync(uint idTask, string name, int data);
        Task<bool> SetDataAsync(uint idTask, string name, int data);
        Task<bool> DeleteTaskAsync(uint idTask);
        Task<bool> SaveAsync(DbTaskDetail detail);
        Task<bool> DeleteAsync(DbTaskDetail detail);
        Task DailyResetAsync();
    }
}
