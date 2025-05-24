using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.DTOS
{
    public class ProjectCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Tutorial { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public AgeRange AgeRange { get; set; }
        public string? MaterialsList { get; set; }
    }

}
