namespace Long.Database.Entities
{
    [Table("cq_friend")]
    public class DbFriend
    {
        [Key]
        [Column("id")] 
        public virtual uint Id { get; set; }

        [Column("userid")] 
        public virtual uint UserId { get; set; }

        [Column("friend")] 
        public virtual uint TargetId { get; set; }

        public virtual DbUser User { get; set; }
        public virtual DbUser Target { get; set; }
    }
}
