using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Long.Login.Database.Entities
{
    [Table("conquer_account_authority")]
    public class GameAccountAuthority
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
