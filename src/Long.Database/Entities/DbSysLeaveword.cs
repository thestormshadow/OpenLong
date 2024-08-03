namespace Long.Database.Entities
{
    [Table("cq_sys_leaveword")]
    public class DbSysLeaveword
    {
        [Key]
        [Column("Id")]
        public virtual uint Id { get; set; }
        [Column("type")]
        public virtual ushort Type { get; set; }
        [Column("user_id")]
        public virtual uint UserId { get; set; }
        [Column("send_name")]
        public virtual string SendName { get; set; }
        [Column("time")]
        public virtual uint Time { get; set; }
        [Column("words")]
        public virtual string Words { get; set; }
        [Column("item_id")]
        public virtual uint ItemId { get; set; }
        /*
         * ChopsteR decided to change name to Demon.GorgoN.
         */
    }
}
