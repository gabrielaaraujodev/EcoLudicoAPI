using EcoLudicoAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class UserRegisterDTO
    {
        [Required]
        public UserType Type { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateOnly DateBirth { get; set; }

        [Required]
        public AddressDTO? Address { get; set; }

        public SchoolCreateDTO? School { get; set; } // substituindo SchoolId
    }


}
