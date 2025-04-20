using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
