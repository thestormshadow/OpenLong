namespace Long.Database.Entities
{
    [Table("cq_trade")]
    public class DbTrade
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("type")] public virtual TradeType Type { get; set; }
        [Column("user_id")] public virtual uint UserIdentity { get; set; }
        [Column("target_id")] public virtual uint TargetIdentity { get; set; }
        [Column("user_money")] public virtual uint UserMoney { get; set; }
        [Column("target_money")] public virtual uint TargetMoney { get; set; }
        [Column("user_emoney")] public virtual uint UserEmoney { get; set; }
        [Column("target_emoney")] public virtual uint TargetEmoney { get; set; }
        [Column("map_id")] public virtual uint MapIdentity { get; set; }
        [Column("user_x")] public virtual ushort UserX { get; set; }
        [Column("user_y")] public virtual ushort UserY { get; set; }
        [Column("target_x")] public virtual ushort TargetX { get; set; }
        [Column("target_y")] public virtual ushort TargetY { get; set; }
        [Column("timestamp")] public virtual DateTime Timestamp { get; set; }
        [Column("user_ip_addr")] public virtual string UserIpAddress { get; set; }
        [Column("user_mac_addr")] public virtual string UserMacAddress { get; set; }
        [Column("target_ip_addr")] public virtual string TargetIpAddress { get; set; }
        [Column("target_mac_addr")] public virtual string TargetMacAddress { get; set; }
        [Column("suspicious")] public virtual bool IsSuspicious { get; set; }

        public enum TradeType
        {
            Trade,
            Booth
        }
    }
}
