namespace Long.Database.Entities
{
    [Table("cq_init_fate_attrib")]
    public class DbInitFateAttrib
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("prof_sort")] public virtual uint ProfSort { get; set; }
        [Column("fate1_attrib1")] public virtual int Fate1Attrib1 { get; set; } // Dragon
        [Column("fate1_attrib2")] public virtual int Fate1Attrib2 { get; set; } // Dragon
        [Column("fate1_attrib3")] public virtual int Fate1Attrib3 { get; set; } // Dragon
        [Column("fate1_attrib4")] public virtual int Fate1Attrib4 { get; set; } // Dragon
        [Column("fate2_attrib1")] public virtual int Fate2Attrib1 { get; set; } // Phoenix
        [Column("fate2_attrib2")] public virtual int Fate2Attrib2 { get; set; } // Phoenix
        [Column("fate2_attrib3")] public virtual int Fate2Attrib3 { get; set; } // Phoenix
        [Column("fate2_attrib4")] public virtual int Fate2Attrib4 { get; set; } // Phoenix
        [Column("fate3_attrib1")] public virtual int Fate3Attrib1 { get; set; } // Tiger
        [Column("fate3_attrib2")] public virtual int Fate3Attrib2 { get; set; } // Tiger
        [Column("fate3_attrib3")] public virtual int Fate3Attrib3 { get; set; } // Tiger
        [Column("fate3_attrib4")] public virtual int Fate3Attrib4 { get; set; } // Tiger
        [Column("fate4_attrib1")] public virtual int Fate4Attrib1 { get; set; } // Turtle
        [Column("fate4_attrib2")] public virtual int Fate4Attrib2 { get; set; } // Turtle
        [Column("fate4_attrib3")] public virtual int Fate4Attrib3 { get; set; } // Turtle
        [Column("fate4_attrib4")] public virtual int Fate4Attrib4 { get; set; } // Turtle
    }
}
