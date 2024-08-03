namespace Long.Database.Entities
{
    [Table("cq_pk_item")]
    public class DbDetainedItem
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("item")] public virtual uint ItemIdentity { get; set; }

        [Column("target")] public virtual uint TargetIdentity { get; set; }

        [Column("target_name")] public virtual string TargetName { get; set; }

        [Column("hunter")] public virtual uint HunterIdentity { get; set; }

        [Column("hunter_name")] public virtual string HunterName { get; set; }

        [Column("manhunt_time")] public virtual int HuntTime { get; set; }

        [Column("bonus")] public virtual ushort RedeemPrice { get; set; }
    }
}