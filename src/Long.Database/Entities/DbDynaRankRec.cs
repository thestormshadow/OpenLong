namespace Long.Database.Entities
{
    [Table("cq_dyna_rank_rec")]
    public class DbDynaRankRec
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("Rank_type")]
        public virtual uint RankType { get; set; }
        [Column("Value")]
        public virtual long Value { get; set; }
        [Column("Obj_id")]
        public virtual uint ObjId { get; set; }
        [Column("user_id")]
        public virtual uint UserId { get; set; }

        public virtual DbUser User { get; set; }
    }
}
