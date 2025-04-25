using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public string? Tutorial { get; set; }
        [Required]
        public List<string> ImageUrls { get; set; }
        public AgeRange? AgeRange { get; set; }
        public string? MaterialsList { get; set; }

        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }

        public List<FavoriteProject> Favoritos { get; set; } = new();
        public List<Comment>? Comments { get; set; }
    }
}
