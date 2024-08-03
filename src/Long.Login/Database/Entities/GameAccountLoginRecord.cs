using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Long.Login.Database.Entities
{
    [Table("conquer_account_login_record")]
    public class GameAccountLoginRecord
    {
        [Key]
        public virtual uint Id { get; protected set; }
        public virtual int AccountId { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual uint LocationId { get; set; }
        public virtual DateTime LoginTime { get; set; }
        public virtual bool Success { get; set; }
    }
}
