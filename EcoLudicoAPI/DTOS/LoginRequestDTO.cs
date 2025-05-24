using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class LoginRequestDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
