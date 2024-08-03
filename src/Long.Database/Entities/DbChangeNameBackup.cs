namespace Long.Database.Entities
{
    [Table("cq_changename_backup")]
    public class DbChangeNameBackup
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("iduser")]
        public virtual uint IdUser { get; set; }
        [Column("oldname")]
        public virtual string OldName { get; set; }
        [Column("newname")]
        public virtual string NewName { get; set; }
        [Column("changetime")]
        public virtual uint ChangeTime { get; set; }
    }
}
