namespace Long.Database.Entities
{
    [Table("cq_ast_prof_inauguration_condition")]
    public class DbAstProfInaugurationCondition
    {
        [Key]
        [Column("id")]
        public virtual uint Identity { get; protected set; }
        [Column("ast_prof_type")]
        public virtual byte AstProfType { get; set; }
        [Column("metempsychosis")]
        public virtual byte Metempsychosis { get; set; }
        [Column("user_level")]
        public virtual byte UserLevel { get; set; }
        [Column("itemtype1")]
        public virtual uint ItemType1 { get; set; }
        [Column("itemtype1_num")]
        public virtual uint ItemType1Amount { get; set; }
        [Column("itemtype2")]
        public virtual uint ItemType2 { get; set; }
        [Column("itemtype2_num")]
        public virtual uint ItemType2Amount { get; set; }
    }
}
