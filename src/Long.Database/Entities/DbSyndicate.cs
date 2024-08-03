namespace Long.Database.Entities
{
    [Table("cq_syndicate")]
    public class DbSyndicate
    {
        [Key]
        [Column("id")]
        public virtual uint Identity { get; set; }
        [Column("name")]
        public virtual string Name { get; set; }
        [Column("announce")]
        public virtual string Announce { get; set; }
        [Column("leader_title")]
        public virtual string LeaderTitle { get; set; }
        [Column("member_title")]
        public virtual sbyte MemberTitle { get; set; }
        [Column("leader_id")]
        public virtual uint LeaderId { get; set; }
        [Column("money")]
        public virtual long Money { get; set; }
        [Column("emoney")]
        public virtual uint Emoney { get; set; }
        [Column("fealty_syn")]
        public virtual uint FealtySyn { get; set; }
        [Column("del_flag")]
        public virtual byte DelFlag { get; set; }
        [Column("enemy0")]
        public virtual uint Enemy0 { get; set; }
        [Column("enemy1")]
        public virtual uint Enemy1 { get; set; }
        [Column("enemy2")]
        public virtual uint Enemy2 { get; set; }
        [Column("enemy3")]
        public virtual uint Enemy3 { get; set; }
        [Column("enemy4")]
        public virtual uint Enemy4 { get; set; }
        [Column("enemy5")]
        public virtual uint Enemy5 { get; set; }
        [Column("enemy6")]
        public virtual uint Enemy6 { get; set; }
        [Column("enemy7")]
        public virtual uint Enemy7 { get; set; }
        [Column("enemy8")]
        public virtual uint Enemy8 { get; set; }
        [Column("enemy9")]
        public virtual uint Enemy9 { get; set; }
        [Column("enemy10")]
        public virtual uint Enemy10 { get; set; }
        [Column("enemy11")]
        public virtual uint Enemy11 { get; set; }
        [Column("enemy12")]
        public virtual uint Enemy12 { get; set; }
        [Column("enemy13")]
        public virtual uint Enemy13 { get; set; }
        [Column("enemy14")]
        public virtual uint Enemy14 { get; set; }
        [Column("ally0")]
        public virtual uint Ally0 { get; set; }
        [Column("ally1")]
        public virtual uint Ally1 { get; set; }
        [Column("ally2")]
        public virtual uint Ally2 { get; set; }
        [Column("ally3")]
        public virtual uint Ally3 { get; set; }
        [Column("ally4")]
        public virtual uint Ally4 { get; set; }
        [Column("ally5")]
        public virtual uint Ally5 { get; set; }
        [Column("ally6")]
        public virtual uint Ally6 { get; set; }
        [Column("ally7")]
        public virtual uint Ally7 { get; set; }
        [Column("ally8")]
        public virtual uint Ally8 { get; set; }
        [Column("ally9")]
        public virtual uint Ally9 { get; set; }
        [Column("ally10")]
        public virtual uint Ally10 { get; set; }
        [Column("ally11")]
        public virtual uint Ally11 { get; set; }
        [Column("ally12")]
        public virtual uint Ally12 { get; set; }
        [Column("ally13")]
        public virtual uint Ally13 { get; set; }
        [Column("ally14")]
        public virtual uint Ally14 { get; set; }
        [Column("condition_level")]
        public virtual byte ConditionLevel { get; set; }
        [Column("condition_metem")]
        public virtual byte ConditionMetem { get; set; }
        [Column("condition_prof")]
        public virtual uint ConditionProf { get; set; }
        [Column("Publish_time")]
        public virtual uint PublishTime { get; set; }
        [Column("totem_pole")]
        public virtual uint TotemPole { get; set; }
        [Column("synrank")]
        public virtual byte SynRank { get; set; }
        [Column("copper")]
        public virtual uint Copper { get; set; }
        [Column("iron")]
        public virtual uint Iron { get; set; }
        [Column("silver")]
        public virtual uint Silver { get; set; }
        [Column("gold")]
        public virtual uint Gold { get; set; }
        [Column("mulberry_wood")]
        public virtual uint MulberryWood { get; set; }
        [Column("oak_wood")]
        public virtual uint OakWood { get; set; }
        [Column("pear_wood")]
        public virtual uint PearWood { get; set; }
        [Column("league_id")]
        public virtual uint LeagueId { get; set; }
    }
}