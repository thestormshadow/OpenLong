namespace Long.Database.Entities
{
    [Table("cq_client_config")]
    public class DbClientConfig
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("server_name")] public virtual string ServerName { get; set; }
        [Column("cross_server")] public virtual string CrossServer { get; set; }
        [Column("map_id")] public virtual uint MapId { get; set; }
        [Column("pos_x")] public virtual uint PosX { get; set; }
        [Column("pos_y")] public virtual uint PosY { get; set; }
        [Column("range")] public virtual uint Range { get; set; }
        [Column("npc_id")] public virtual uint NpcId { get; set; }
        [Column("attribute")] public virtual uint Attribute { get; set; }
    }
}
