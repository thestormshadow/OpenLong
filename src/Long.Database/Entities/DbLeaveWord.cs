namespace Long.Database.Entities
{
    [Table("cq_leaveword")]
    public class DbLeaveword
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("recv_id")]
        public virtual uint? RecvId { get; set; }
        [Column("send_name")]
        public virtual string SendName { get; set; }
        [Column("time")]
        public virtual string Time { get; set; }
        [Column("words")]
        public virtual string Words { get; set; }
        [Column("lookface")]
        public virtual uint Lookface { get; set; }
        [Column("type")]
        public virtual byte Type { get; set; }
        /*
         * Your Trade Partner Giba1 has deleted his/her character.
         * REFEN has ended up the partnership with you.
         */
    }
}
