namespace Long.Database.Entities
{
    [Table("e_money_mono")]
    public class DbEMoneyMono
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("type")]
        public virtual byte Type { get; set; }
        [Column("id_source")]
        public virtual uint IdSource { get; set; }
        [Column("id_target")]
        public virtual uint IdTarget { get; set; }
        [Column("number")]
        public virtual uint Number { get; set; }
        [Column("chk_sum")]
        public virtual uint ChkSum { get; set; }
        [Column("time_stamp")]
        public virtual uint TimeStamp { get; set; }
        [Column("source_balance")]
        public virtual uint SourceBalance { get; set; }
        [Column("target_balance")]
        public virtual uint TargetBalance { get; set; }
    }
}
