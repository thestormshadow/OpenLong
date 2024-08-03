namespace Long.Database.Entities
{
    [Table("cq_itemstatus")]
    public class DbItemStatus
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("iditem")]
        public virtual uint ItemId { get; set; }
        [Column("userid")]
        public virtual uint UserId { get; set; }
        [Column("statu")]
        public virtual uint Status { get; set; }
        [Column("lev")]
        public virtual uint Level { get; set; }
        [Column("power1")]
        public virtual uint Power1 { get; set; }
        [Column("power2")]
        public virtual uint Power2 { get; set; }
        [Column("realsecs")]
        public virtual uint RealSeconds { get; set; }
        [Column("data")]
        public virtual uint Data { get; set; }
        [Column("position")]
        public virtual byte Position { get; set; }
    }
}
