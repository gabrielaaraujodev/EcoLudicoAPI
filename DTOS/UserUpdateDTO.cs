using EcoLudicoAPI.Enums;
using EcoLudicoAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class UserUpdateDTO
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public AddressDTO Address { get; set; } = new();
    }
}
