using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Long.Login.Database.Entities
{
    [Table("realm_user")]
    public class RealmUser
    {
        [Key]
        public virtual uint PlayerId { get; protected set; }
        public virtual int RealmId { get; set; }
        public virtual int AccountId { get; set; }
        public virtual DateTime CreationDate { get; set; }
    }
}
