using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class SchoolDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public AddressDTO? Address { get; set; }

        public string Contact { get; set; } = string.Empty;

        public string OperatingHours { get; set; } = string.Empty;
    }
}
