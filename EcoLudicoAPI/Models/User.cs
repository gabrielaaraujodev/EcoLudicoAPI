using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public UserType Type { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public DateOnly? DateBirth { get; set; }

        [Required]
        public string Password { get; set; }

        public Address Address { get; set; }

        public int SchoolId { get; set; }
        public School? School { get; set; }

        public List<School>? FavoriteSchools { get; set; }
        public List<FavoriteProject> FavoriteProjects { get; set; } = new();
        public List<Comment>? MadeComments { get; set; }
    }
}
