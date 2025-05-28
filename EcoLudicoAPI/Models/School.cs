using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public Address Address { get; set; }

        [Phone]
        public string Contact { get; set; } = string.Empty;

        public string OperatingHours { get; set; } = string.Empty;

        public List<User> Teachers { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<User> UsersWhoFavorited { get; set; } = new();
    }
}
