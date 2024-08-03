namespace Long.Database.Entities
{
    [Table("cq_tutor_type")]
    public class DbTutorType
    {
        [Key][Column("id")] public virtual uint Id { get; set; }

        /// <summary>
        ///     Minimum user level for this rule. Below the lowest one can't be a mentor.
        /// </summary>
        [Column("User_lev_min")]
        public virtual byte UserMinLevel { get; set; }

        /// <summary>
        ///     Maximum user level for this rule. After the highest one, get the higher.
        /// </summary>
        [Column("User_lev_max")]
        public virtual byte UserMaxLevel { get; set; }

        /// <summary>
        ///     Maximum number of students a user can have.
        /// </summary>
        [Column("Student_num")]
        public virtual byte StudentNum { get; set; }

        /// <summary>
        ///     Percentage of battle power to share.
        /// </summary>
        [Column("Battle_lev_share")]
        public virtual byte BattleLevelShare { get; set; }
    }
}
