namespace Long.Database.Entities
{
    [Table("cq_message_log")]
    public class DbMessageLog
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("sender_id")] public virtual uint SenderIdentity { get; set; }
        [Column("sender_name")] public virtual string SenderName { get; set; }
        [Column("target_id")] public virtual uint TargetIdentity { get; set; }
        [Column("target_name")] public virtual string TargetName { get; set; }
        [Column("channel")] public virtual ushort Channel { get; set; }
        [Column("message")] public virtual string Message { get; set; }
        [Column("date")] public virtual DateTime Time { get; set; }
    }
}
