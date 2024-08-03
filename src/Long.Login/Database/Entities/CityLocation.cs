using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Long.Login.Database.Entities
{
    [Table("city_location")]
    public class CityLocation
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("ip_from")] public virtual ulong IpFrom { get; set; }
        [Column("ip_to")] public virtual ulong IpTo { get; set; }
        [Column("country_code")] public virtual string CountryCode { get; set; }
        [Column("country_name")] public virtual string CountryName { get; set; }
        [Column("state")] public virtual string State { get; set; }
        [Column("city")] public virtual string City { get; set; }
        [Column("latitude")] public virtual double Latitude { get; set; }
        [Column("longitude")] public virtual double Longitude { get; set; }
        [Column("zip_code")] public virtual string ZipCode { get; set; }
    }
}
