namespace Long.Database.Entities
{
    [Table("cq_item")]
    public class DbItem
    {
        /// <summary>
        ///     The unique identification of the item.
        /// </summary>
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }

        /// <summary>
        ///     The itemtype of the item.
        /// </summary>
        [Column("type")]
        public virtual uint Type { get; set; }

        /// <summary>
        ///     The owner (shop or player) where the player got the item from.
        /// </summary>
        [Column("owner_id")]
        public virtual uint OwnerId { get; set; }

        /// <summary>
        ///     The player who actually owns the item.
        /// </summary>
        [Column("player_id")]
        public virtual uint PlayerId { get; set; }

        /// <summary>
        ///     The actual durability of the item.
        /// </summary>
        [Column("amount")]
        public virtual ushort Amount { get; set; }

        /// <summary>
        ///     The max amount of the durability of the item.
        /// </summary>
        [Column("amount_limit")]
        public virtual ushort AmountLimit { get; set; }

        /// <summary>
        ///     Not sure yet.
        /// </summary>
        [Column("ident")]
        public virtual byte Ident { get; set; }

        /// <summary>
        ///     The actual position of the item. +200 for warehouses.
        /// </summary>
        [Column("position")]
        public virtual byte Position { get; set; }

        /// <summary>
        ///     The gem on socket 1. 255 for open hole.
        /// </summary>
        [Column("gem1")]
        public virtual byte Gem1 { get; set; }

        /// <summary>
        ///     The gem on socket 2. 255 for open hole.
        /// </summary>
        [Column("gem2")]
        public virtual byte Gem2 { get; set; }

        /// <summary>
        ///     The item effect.
        /// </summary>
        [Column("magic1")]
        public virtual ushort Magic1 { get; set; }

        /// <summary>
        ///     Not sure yet.
        /// </summary>
        [Column("magic2")]
        public virtual byte Magic2 { get; set; }

        /// <summary>
        ///     The item plus.
        /// </summary>
        [Column("magic3")]
        public virtual byte Magic3 { get; set; }

        /// <summary>
        ///     Socket progress.
        /// </summary>
        [Column("data")]
        public virtual uint Data { get; set; }

        /// <summary>
        ///     The item blessing.
        /// </summary>
        [Column("reduce_dmg")]
        public virtual byte ReduceDmg { get; set; }

        /// <summary>
        ///     Item enchantment.
        /// </summary>
        [Column("add_life")]
        public virtual byte AddLife { get; set; }

        /// <summary>
        ///     The green attribute. Not used tho.
        /// </summary>
        [Column("anti_monster")]
        public virtual byte AntiMonster { get; set; }

        /// <summary>
        ///     Not sure yet.
        /// </summary>
        [Column("chk_sum")]
        public virtual uint ChkSum { get; set; }

        /// <summary>
        ///     Suspicious items.
        /// </summary>
        [Column("plunder")]
        public virtual ushort Plunder { get; set; }


        /// <summary>
        ///     Forbbiden or not?
        /// </summary>
        [Column("SpecialFlag")]
        public virtual uint Specialflag { get; set; }

        /// <summary>
        ///     The color of the item.
        /// </summary>
        [Column("color")]
        public virtual uint Color { get; set; }

        /// <summary>
        ///     The progress of the plus.
        /// </summary>
        [Column("Addlevel_exp")]
        public virtual uint AddlevelExp { get; set; }

        /// <summary>
        ///     The kind of item. (Bound, Quest, etc)
        /// </summary>
        [Column("monopoly")]
        public virtual byte Monopoly { get; set; }

        [Column("syndicate")] public virtual uint Syndicate { get; set; }

        [Column("del_time")] public virtual int DeleteTime { get; set; }

        [Column("save_time")] public virtual uint SaveTime { get; set; }

		[Column("accumulate_num")] public virtual uint AccumulateNum { get; set; }
		[Column("PerfectionLevel")] public virtual uint PerfectionLevel { get; set; }
		[Column("PerfectionProgress")] public virtual uint PerfectionProgress { get; set; }
		[Column("PerfectionOwnerGuid")] public virtual uint PerfectionOwnerGuid { get; set; }
		[Column("PerfectionOwnerName")] public virtual string PerfectionOwnerName { get; set; }
		[Column("Signature")] public virtual string Signature { get; set; }
	}
}
