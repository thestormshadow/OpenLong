namespace Long.Database.Entities
{
    [Table("cq_dyna_global_data")]
    public class DbDynaGlobalData
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("data0")]
        public virtual long Data0 { get; set; }
        [Column("data1")]
        public virtual long Data1 { get; set; }
        [Column("data2")]
        public virtual long Data2 { get; set; }
        [Column("data3")]
        public virtual long Data3 { get; set; }
        [Column("data4")]
        public virtual long Data4 { get; set; }
        [Column("data5")]
        public virtual long Data5 { get; set; }
        [Column("datastr0")]
        public virtual string Datastr0 { get; set; }
        [Column("datastr1")]
        public virtual string Datastr1 { get; set; }
        [Column("datastr2")]
        public virtual string Datastr2 { get; set; }
        [Column("datastr3")]
        public virtual string Datastr3 { get; set; }
        [Column("datastr4")]
        public virtual string Datastr4 { get; set; }
        [Column("datastr5")]
        public virtual string Datastr5 { get; set; }
        [Column("time0")]
        public virtual uint Time0 { get; set; }
        [Column("time1")]
        public virtual uint Time1 { get; set; }
        [Column("time2")]
        public virtual uint Time2 { get; set; }
        [Column("time3")]
        public virtual uint Time3 { get; set; }
        [Column("time4")]
        public virtual uint Time4 { get; set; }
        [Column("time5")]
        public virtual uint Time5 { get; set; }
    }
}
