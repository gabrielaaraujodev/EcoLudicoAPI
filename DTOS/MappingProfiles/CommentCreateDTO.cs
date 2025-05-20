using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.DTOS
{
    public class CommentCreateDTO
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = string.Empty;
    }
}