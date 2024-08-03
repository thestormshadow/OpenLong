namespace Long.Database.Entities
{
    [Table("cq_mail")]
    public class DbMail
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("receiver_id")] public virtual uint ReceiverId { get; set; }
        [Column("Sender_name")] public virtual string SenderName { get; set; }
        [Column("title")] public virtual string Title { get; set; }
        [Column("content")] public virtual string Content { get; set; }
        [Column("money")] public virtual ulong Money { get; set; }
        [Column("emoney")] public virtual uint ConquerPoints { get; set; }
        [Column("action")] public virtual uint Action { get; set; }
        [Column("expiration_date")] public virtual uint ExpirationDate { get; set; }
        [Column("item_id")] public virtual uint ItemId { get; set; }
        [Column("emoney_record_type")] public virtual byte EmoneyRecordType { get; set; }
    }
}
