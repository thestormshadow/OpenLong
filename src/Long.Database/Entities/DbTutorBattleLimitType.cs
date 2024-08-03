namespace Long.Database.Entities
{
    [Table("cq_tutor_battle_limit_type")]
    public class DbTutorBattleLimitType
    {
        /// <summary>
        ///     Battle power of the apprentice.
        /// </summary>
        [Key]
        [Column("id")]
        public virtual ushort Id { get; set; }

        /// <summary>
        ///     Maximum addition battle power for that level.
        /// </summary>
        [Column("Battle_lev_limit")]
        public virtual ushort BattleLevelLimit { get; set; }
    }
}
