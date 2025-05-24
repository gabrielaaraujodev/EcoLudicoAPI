using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.DTOS
{
    public class ProjectDTO
    {
        public int ProjectId {  get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public string? Tutorial { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public AgeRange? AgeRange { get; set; }
        public string? MaterialsList { get; set; }
        public List<CommentResponseDTO>? Comments { get; set; } 
        public int? SchoolOwnerUserId { get; set; } 
        public SchoolDTO? School { get; set; }

        public bool IsFavorite { get; set; }
    }
}
