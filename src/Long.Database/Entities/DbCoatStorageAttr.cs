namespace Long.Database.Entities
{
    [Table("cq_coat_storage_attr")]
    public class DbCoatStorageAttr
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("coat_type")] public virtual uint CoatType { get; set; }
        [Column("amount")] public virtual int Amount { get; set; }
        [Column("attr_type_1")] public virtual byte AttrType1 { get; set; }
        [Column("attr_1")] public virtual int Attr1 { get; set; }
        [Column("attr_type_2")] public virtual byte AttrType2 { get; set; }
        [Column("attr_2")] public virtual int Attr2 { get; set; }
        [Column("attr_type_3")] public virtual byte AttrType3 { get; set; }
        [Column("attr_3")] public virtual int Attr3 { get; set; }
        [Column("attr_type_4")] public virtual byte AttrType4 { get; set; }
        [Column("attr_4")] public virtual int Attr4 { get; set; }

        public int GetAttrType(int idx)
        {
            switch (idx)
            {
                case 0: return AttrType1;
                case 1: return AttrType2;
                case 2: return AttrType3;
                case 3: return AttrType4;
                default: return 0;
            }
        }

        public int GetAttrValue(int idx)
        {
            switch (idx)
            {
                case 0: return Attr1;
                case 1: return Attr2;
                case 2: return Attr3;
                case 3: return Attr4;
                default: return 0;
            }
        }
    }
}
