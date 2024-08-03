namespace Long.Database.Entities
{
    [Table("family_attr")]
    public class DbFamilyAttr
    {
        [Key][Column("id")] public uint UserIdentity { get; set; }
        [Column("family_id")] public uint FamilyIdentity { get; set; }
        [Column("rank")] public byte Rank { get; set; }
        [Column("proffer")] public uint Proffer { get; set; }
        [Column("join_date")] public uint JoinDate { get; set; }
        [Column("auto_exercise")] public byte AutoExercise { get; set; }
        [Column("exp_date")] public uint ExpDate { get; set; }
    }
}