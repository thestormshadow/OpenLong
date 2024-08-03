namespace Long.Database.Entities
{
    [Table("cq_syn_advertising_info")]
    public class DbSynAdvertisingInfo
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("idsyn")]
        public virtual uint IdSyn { get; set; }
        [Column("content")]
        public virtual string Content { get; set; }
        [Column("expense")]
        public virtual uint Expense { get; set; }
        [Column("end_date")]
        public virtual uint EndDate { get; set; }
        [Column("auto_recruit")]
        public virtual byte AutoRecruit { get; set; }
        [Column("condition_level")]
        public virtual byte ConditionLevel { get; set; }
        [Column("condition_metem")]
        public virtual byte ConditionMetem { get; set; }
        [Column("condition_prof")]
        public virtual ushort ConditionProf { get; set; }
        [Column("condition_sex")]
        public virtual byte ConditionSex { get; set; }
        [Column("condition_battle")]
        public virtual ushort ConditionBattle { get; set; }
    }
}
