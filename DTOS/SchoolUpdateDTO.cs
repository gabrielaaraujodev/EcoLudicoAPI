using EcoLudicoAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class SchoolUpdateDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public Address Address { get; set; }

        [Phone]
        public string Contact { get; set; } = string.Empty;

        public string OperatingHours { get; set; } = string.Empty;
    }
}
