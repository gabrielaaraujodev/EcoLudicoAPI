using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.DTOS
{
    public class ProjectCreateWithImageDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Tutorial { get; set; }
        public IFormFile? File { get; set; } 
        public AgeRange AgeRange { get; set; }
        public string? MaterialsList { get; set; }
    }
}
