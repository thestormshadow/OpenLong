namespace Long.Database.Entities
{
    [Table("cq_sign_everyday")]
    public class DbSignInEveryday
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("player_id")] public virtual uint PlayerId { get; set; }
        [Column("award_camulate")] public virtual byte AwardCamulate { get; set; }
        [Column("sign_day")] public virtual uint SignInDay { get; set; }
        [Column("fill_sign_times")] public virtual byte FillSignTimes { get; set; }
    }
}
