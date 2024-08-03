namespace Long.Database.Entities
{
    [Table("cq_official_type")]
    public class DbOfficialType
    {
        [Key]
        [Column("id")] public virtual ushort Id { get; set; }
        [Column("setup_num")] public virtual byte SetupNum { get; set; }
        [Column("speaker_num")] public virtual byte SpeakerNum { get; set; }
        [Column("official_name")] public virtual string OfficialName { get; set; }
        [Column("official_name_ex")] public virtual string OfficialNameEx { get; set; }
        [Column("fee")] public virtual uint Fee { get; set; }
        [Column("award_id")] public virtual uint AwardId { get; set; }
    }
}
