using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Number { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Complement { get; set; } = string.Empty;

        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [MaxLength(50)]
        public string State { get; set; } = string.Empty;

        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}
