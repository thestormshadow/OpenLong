namespace Long.Database.Entities
{
    [Table("cq_synattr")]
    public class DbSyndicateAttr
    {
        [Key][Column("id")] public virtual uint UserIdentity { get; set; }
        [Column("syn_id")] public virtual uint SynId { get; set; }
        [Column("rank")] public virtual ushort Rank { get; set; }
        [Column("proffer")] public virtual long Proffer { get; set; }
        [Column("proffer_his")] public virtual ulong ProfferTotal { get; set; }
        [Column("proffer_emoney")] public virtual uint Emoney { get; set; }
        [Column("proffer_emoney_his")] public virtual uint EmoneyTotal { get; set; }
        [Column("proffer_pk")] public virtual int Pk { get; set; }
        [Column("proffer_pk_his")] public virtual int PkTotal { get; set; }
        [Column("proffer_edu")] public virtual uint Guide { get; set; }
        [Column("proffer_edu_his")] public virtual uint GuideTotal { get; set; }
        [Column("proffer_totem")] public virtual uint Arsenal { get; set; }
        [Column("flower")] public uint Flower { get; set; }
        [Column("flower_w")] public uint WhiteFlower { get; set; }
        [Column("flower_lily")] public uint Orchid { get; set; }
        [Column("flower_tulip")] public uint Tulip { get; set; }
        [Column("duty_time_limit")] public virtual uint Expiration { get; set; }
        [Column("assistant_id")] public uint AssistantIdentity { get; set; }
        [Column("master_id")] public uint MasterId { get; set; }
        [Column("proffer_merit")] public uint Merit { get; set; }
        [Column("join_date")] public virtual uint JoinDate { get; set; }
        [Column("profession")] public uint Profession { get; set; }
        [Column("last_logout")] public uint LastLogout { get; set; }
    }
}
