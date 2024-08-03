namespace Long.Database.Entities
{
    [Table("cq_vipminetime")]
    public class DbVipMineTime
    {
        [Key]
        [Column("iduser")] public virtual uint UserId { get; set; }
        [Column("lastentertime")] public virtual uint LastEnterTime { get; set; }
    }
}
