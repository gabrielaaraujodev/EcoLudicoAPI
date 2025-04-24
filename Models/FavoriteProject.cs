using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoLudicoAPI.Models
{
    public class FavoriteProject
    {
        [Key]
        public int FavoriteId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public Project Projeto { get; set; }
    }
}
