using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.States.User;

namespace Long.Module.Relation.States
{
    public sealed class Relation<TEntity> where TEntity : class
    {
        private readonly TEntity entity;

        public Relation(uint idTarget, string name, TEntity entity)
        {
            Id = idTarget;
            Name = name;
            this.entity = entity;
        }

        public uint Id { get; }
        public string Name { get; }

        public Character GetUser() => RoleManager.GetUser(Id);

        public Task CreateAsync()
        {
            return ServerDbContext.CreateAsync(entity);
        }

        public Task DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(entity);
        }
    }
}
