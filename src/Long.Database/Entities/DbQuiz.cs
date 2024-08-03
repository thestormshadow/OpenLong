namespace Long.Database.Entities
{
    [Table("cq_quiz")]
    public class DbQuiz
    {
        [Key][Column("id")] public uint Identity { get; set; }

        [Column("type")] public byte Type { get; set; }
        [Column("level")] public byte Level { get; set; }
        [Column("term")] public byte Term { get; set; }
        [Column("question")] public string Question { get; set; }
        [Column("answer1")] public string Answer1 { get; set; }
        [Column("answer2")] public string Answer2 { get; set; }
        [Column("answer3")] public string Answer3 { get; set; }
        [Column("answer4")] public string Answer4 { get; set; }
        [Column("result")] public byte Result { get; set; }
    }
}
