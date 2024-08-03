namespace Long.World.Roles
{
    public abstract class WorldObject
    {
        public virtual uint Identity { get; init; }
        public virtual string Name { get; protected set; }

        public virtual ushort X { get; set; }
        public virtual ushort Y { get; set; }

        public override string ToString()
        {
            return $"({Identity}){Name}";
        }
    }
}