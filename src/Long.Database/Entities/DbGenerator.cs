namespace Long.Database.Entities
{
    [Table("cq_generator")]
    public class DbGenerator
    {
        [Key][Column("id")] public virtual uint Id { get; set; }

        [Column("mapid")] public virtual uint Mapid { get; set; }
        [Column("bound_x")] public virtual ushort BoundX { get; set; }
        [Column("bound_y")] public virtual ushort BoundY { get; set; }
        [Column("bound_cx")] public virtual ushort BoundCx { get; set; }
        [Column("bound_cy")] public virtual ushort BoundCy { get; set; }
        [Column("maxnpc")] public virtual int MaxNpc { get; set; }
        [Column("rest_secs")] public virtual int RestSecs { get; set; }
        [Column("max_per_gen")] public virtual int MaxPerGen { get; set; }
        [Column("npctype")] public virtual uint Npctype { get; set; }
        [Column("timer_begin")] public virtual int TimerBegin { get; set; }
        [Column("timer_end")] public virtual int TimerEnd { get; set; }
        [Column("born_x")] public virtual int BornX { get; set; }
        [Column("born_y")] public virtual int BornY { get; set; }
        [Column("mask")] public virtual byte Mask { get; set; }
        [Column("cluster_type")] public virtual bool ClusterType { get; set; }
    }
}