namespace Long.Database.Entities
{
    [Table("cq_monster_cluster")]
    public class DbMonsterCluster
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("monster0")] public virtual uint Monster0 { get; set; }
        [Column("ratio0")] public virtual byte Ratio0 { get; set; }
        [Column("monster1")] public virtual uint Monster1 { get; set; }
        [Column("ratio1")] public virtual byte Ratio1 { get; set; }
        [Column("monster2")] public virtual uint Monster2 { get; set; }
        [Column("ratio2")] public virtual byte Ratio2 { get; set; }
        [Column("monster3")] public virtual uint Monster3 { get; set; }
        [Column("ratio3")] public virtual byte Ratio3 { get; set; }
        [Column("monster4")] public virtual uint Monster4 { get; set; }
        [Column("ratio4")] public virtual byte Ratio4 { get; set; }
        [Column("monster5")] public virtual uint Monster5 { get; set; }
        [Column("ratio5")] public virtual byte Ratio5 { get; set; }
        [Column("monster6")] public virtual uint Monster6 { get; set; }
        [Column("ratio6")] public virtual byte Ratio6 { get; set; }
        [Column("monster7")] public virtual uint Monster7 { get; set; }
        [Column("ratio7")] public virtual byte Ratio7 { get; set; }
        [Column("monster8")] public virtual uint Monster8 { get; set; }
        [Column("ratio8")] public virtual byte Ratio8 { get; set; }
        [Column("monster9")] public virtual uint Monster9 { get; set; }
        [Column("ratio9")] public virtual byte Ratio9 { get; set; }
    }
}
