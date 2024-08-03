namespace Long.Database.Entities
{
    [Table("cq_enemy")]
    public class DbEnemy
    {
        [Key]
        [Column("id")] 
        public virtual uint Id { get; set; }

        [Column("userid")] 
        public virtual uint UserId { get; set; }

        [Column("enemy")] 
        public virtual uint TargetId { get; set; }

        [Column("time")] 
        public virtual uint Time { get; set; }

        public virtual DbUser Target {  get; set; }
    }
}
