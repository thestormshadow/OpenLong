namespace Long.Database.Entities
{
    [Table("cq_npc")]
    public class DbNpc
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("ownerid")] public virtual uint Ownerid { get; set; }
        [Column("playerid")] public virtual uint Playerid { get; set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("type")] public virtual ushort Type { get; set; }
        [Column("lookface")] public virtual uint Lookface { get; set; }
        [Column("idxserver")] public virtual int Idxserver { get; set; }
        [Column("mapid")] public virtual uint Mapid { get; set; }
        [Column("cellx")] public virtual ushort Cellx { get; set; }
        [Column("celly")] public virtual ushort Celly { get; set; }
        [Column("task0")] public virtual uint Task0 { get; set; }
        [Column("task1")] public virtual uint Task1 { get; set; }
        [Column("task2")] public virtual uint Task2 { get; set; }
        [Column("task3")] public virtual uint Task3 { get; set; }
        [Column("task4")] public virtual uint Task4 { get; set; }
        [Column("task5")] public virtual uint Task5 { get; set; }
        [Column("task6")] public virtual uint Task6 { get; set; }
        [Column("task7")] public virtual uint Task7 { get; set; }
        [Column("data0")] public virtual int Data0 { get; set; }
        [Column("data1")] public virtual int Data1 { get; set; }
        [Column("data2")] public virtual int Data2 { get; set; }
        [Column("data3")] public virtual int Data3 { get; set; }
        [Column("datastr")] public virtual string Datastr { get; set; }
        [Column("linkid")] public virtual uint Linkid { get; set; }
        [Column("life")] public virtual ushort Life { get; set; }
        [Column("maxlife")] public virtual ushort Maxlife { get; set; }
        [Column("base")] public virtual uint Base { get; set; }
        [Column("sort")] public virtual ushort Sort { get; set; }
        [Column("itemid")] public virtual uint? Itemid { get; set; }
    }
}
