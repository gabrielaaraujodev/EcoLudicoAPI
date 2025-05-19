using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class SchoolCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public AddressDTO? Address { get; set; }

        public string Contact { get; set; } = string.Empty;

        public string OperatingHours { get; set; } = string.Empty;
    }

}
