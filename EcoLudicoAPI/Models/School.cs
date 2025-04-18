using System.ComponentModel.DataAnnotations;

namespace EcoLudicoAPI.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public Address Address { get; set; }

        [Phone]
        public string Contact { get; set; }

        public string OperatingHours { get; set; }

        public List<User>? Teachers { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
