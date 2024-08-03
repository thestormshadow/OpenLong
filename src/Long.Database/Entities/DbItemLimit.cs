namespace Long.Database.Entities
{
    namespace Long.Database.Entities
    {
        [Table("cq_item_limit")]
        public class DbItemLimit
        {
            [Key]
            [Column("id")]
            public virtual uint Id { get; set; }
            [Column("type")]
            public virtual uint Type { get; set; }
            [Column("limit_level")]
            public virtual uint LimitLevel { get; set; }
        }
    }

}
