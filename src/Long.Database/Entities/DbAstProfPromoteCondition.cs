namespace Long.Database.Entities
{
    [Table("cq_ast_prof_promote_condition")]
    public class DbAstProfPromoteCondition
    {
        [Key]
        [Column("id")]
        public virtual uint Identity { get; protected set; }
        [Column("ast_prof_type")]
        public virtual byte AstProfType { get; set; }
        [Column("ast_prof_rank")]
        public virtual byte AstProfRank { get; set; }
        [Column("metempsychosis")]
        public virtual byte? Metempsychosis { get; set; }
        [Column("user_level")]
        public virtual byte? UserLevel { get; set; }
        [Column("ast_prof_level")]
        public virtual byte? AstProfLevel { get; set; }
    }
}
