namespace Long.Database.Entities
{
    [Table("cq_title_type")]
    public class DbTitleType
    {
        [Column("type")] public virtual uint Type { get; set; }
        [Column("id")] public virtual uint Identity { get; set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("save_time")] public virtual uint SaveTime { get; set; }
        [Column("cost_7")] public virtual uint Cost7 { get; set; }
        [Column("cost_30")] public virtual uint Cost30 { get; set; }
        [Column("cost_forever")] public virtual uint CostForever { get; set; }
        [Column("score")] public virtual uint Score { get; set; }

        public override string ToString()
        {
            return $"{Type} - {Identity} {Name}";
        }
    }
}
