namespace Long.Database.Entities
{
    [Table("cq_inner_strength_secret")]
    public class DbInnerStrenghtSecret
    {
        [Key]
        [Column("id")] public virtual uint Identity { get; set; }
        [Column("player_id")] public virtual uint PlayerIdentity { get; set; }
        [Column("secret_type")] public virtual byte SecretType { get; set; }
    }
}
