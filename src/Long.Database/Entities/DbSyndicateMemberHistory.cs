namespace Long.Database.Entities
{
    [Table("cq_syn_history")]
    public class DbSyndicateMemberHistory
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("user_id")] public virtual uint UserIdentity { get; set; }
        [Column("syn_id")] public virtual uint SyndicateIdentity { get; set; }
        [Column("join_date")] public virtual DateTime JoinDate { get; set; }
        [Column("leave_date")] public virtual DateTime LeaveDate { get; set; }
        [Column("silver")] public virtual long Silver { get; set; }
        [Column("emoney")] public virtual uint ConquerPoints { get; set; }
        [Column("pk")] public virtual uint PkPoints { get; set; }
        [Column("guide")] public virtual uint Guide { get; set; }
        [Column("rank")] public virtual ushort Rank { get; set; }
    }
}