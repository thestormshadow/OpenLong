using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;

namespace Long.Kernel.States.Storage
{
    public sealed class TitleStorageObject
    {
        private readonly DbUserTitle storageObject;
        private readonly DbTitleType storageType;

        public TitleStorageObject(DbUserTitle obj)
        {
            storageObject = obj;
            storageType = TitleStorageManager.GetTitleType(obj.Type, obj.TitleId);

            if (storageType == null)
            {
                throw new InvalidDataException($"Invalid Title/Wing type {obj.Type}.");
            }
        }

        public uint Type => storageObject.Type;
        public uint TitleId => storageObject.TitleId;
        
        public bool IsActive
        {
            get => storageObject.Status != 0;
            set => storageObject.Status = value ? 1u : 0u;
        }

        public uint DelTime
        {
            get => storageObject.DelTime;
            set => storageObject.DelTime = value;
        }

        public uint Score => storageType.Score;
        public bool IsWing => Type >= 4000;
        public bool IsTitle => Type < 4000;

        public bool HasExpired()
        {
            if (storageObject.DelTime == 0)
            {
                return false;
            }
            return UnixTimestamp.Now >= storageObject.DelTime;
        }

        public Task SaveAsync()
        {
            return ServerDbContext.UpdateAsync(storageObject);
        }

        public Task DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(storageObject);
        }
    }
}
