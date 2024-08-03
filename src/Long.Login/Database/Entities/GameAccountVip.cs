using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Long.Login.Database.Entities
{
    [Table("conquer_account_vip")]
    public class GameAccountVip
    {
        [Key]
        public virtual int Id { get; protected set; }
        public virtual int ConquerAccountId { get; set; }
        public virtual byte VipLevel { get; set; }
        public virtual uint DurationMinutes { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual DateTime CreationDate { get; set; }
    }
}
