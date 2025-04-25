using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoLudicoAPI.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required, MaxLength(500)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; } 

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
