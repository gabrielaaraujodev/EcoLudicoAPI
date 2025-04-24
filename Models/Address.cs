using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.Models
{
    public class Address
    {
        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string Number { get; set; }

        [MaxLength(50)]
        public string Complement { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
