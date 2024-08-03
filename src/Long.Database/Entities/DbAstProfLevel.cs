namespace Long.Database.Entities
{
    [Table("cq_ast_prof_level")]
    public class DbAstProfLevel
    {
        [Key]
        [Column("id")]
        public virtual uint Identity { get; protected set; }
        [Column("user_id")]
        public virtual uint UserIdentity { get; set; }
        [Column("ast_prof")]
        public virtual byte AstProf { get; set; }
        [Column("level")]
        public virtual byte Level { get; set; }
    }
}
