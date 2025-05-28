using EcoLudicoAPI.Enums;
using EcoLudicoAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class UserUpdateDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public AddressDTO Address { get; set; } = new();
    }

}
