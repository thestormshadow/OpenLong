namespace Long.Database.Entities
{
    [Table("cq_user_title")]
    public class DbUserTitle
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("player_id")] public virtual uint PlayerId { get; set; }
        [Column("type")] public virtual uint Type { get; set; }
        [Column("title_id")] public virtual uint TitleId { get; set; }
        [Column("status")] public virtual uint Status { get; set; }
        [Column("del_time")] public virtual uint DelTime { get; set; }
    }
}
