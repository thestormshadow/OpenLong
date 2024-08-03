namespace Long.Database.Entities
{
    [Table("cq_title_rule")]
    public class DbTitleRule
    {
        [Key][Column("title_id")] public virtual uint Identity { get; protected set; }
        [Column("title_type")] public virtual uint Type { get; set; }
        [Column("rule")] public virtual byte Rule { get; set; }
        [Column("relation")] public virtual byte Relation { get; set; }
        [Column("data1")] public virtual uint Data1 { get; set; }
        [Column("data2")] public virtual uint Data2 { get; set; }
        [Column("data3")] public virtual uint Data3 { get; set; }
        [Column("data4")] public virtual uint Data4 { get; set; }
        [Column("data5")] public virtual uint Data5 { get; set; }
        [Column("data6")] public virtual uint Data6 { get; set; }
        [Column("data7")] public virtual uint Data7 { get; set; }
        [Column("data8")] public virtual uint Data8 { get; set; }

        public uint GetData(int idx)
        {
            switch (idx)
            {
                case 1: return Data1;
                case 2: return Data2;
                case 3: return Data3;
                case 4: return Data4;
                case 5: return Data5;
                case 6: return Data6;
                case 7: return Data7;
                case 8: return Data8;
            }
            return 0;
        }
    }
}
