using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.DTOS
{
    public class ProjectUpdateDTO
    {
        public int UserId { get; set; }  
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Tutorial { get; set; }
        public List<string>? ImageUrls { get; set; }
        public AgeRange? AgeRange { get; set; }
        public string? MaterialsList { get; set; }
    }

}
