namespace Long.Database.Entities
{
    [Table("cq_goods")]
    public class DbGoods
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("ownerid")] public virtual uint OwnerIdentity { get; set; }
        [Column("itemtype")] public virtual uint Itemtype { get; set; }
        [Column("moneytype")] public virtual uint Moneytype { get; set; }
        [Column("honor_price")] public virtual uint HonorPrice { get; set; }
        [Column("compete_place")] public virtual ushort CompetePlace { get; set; }
        [Column("riding_price")] public virtual uint RidingPrice { get; set; }
        [Column("goldenleague_price")] public virtual uint GoldenLeaguePrice { get; set; }
    }
}
