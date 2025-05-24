using System.ComponentModel.DataAnnotations;

namespace EcoLudico.DTOS
{
    public class CommentUpdateDTO
    {
        [Required]
        public int CommentId { get; set; } 

        [Required]
        [MaxLength(500)]
        public string Content { get; set; } = string.Empty;
    }
}