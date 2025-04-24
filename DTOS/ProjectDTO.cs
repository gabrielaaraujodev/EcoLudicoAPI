using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.DTOS
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Tutorial { get; set; }
        public List<string> ImageUrls { get; set; }
        public AgeRange? AgeRange { get; set; }
        public string? MaterialsList { get; set; }
        public int SchoolId { get; set; }
        public SchoolDTO School { get; set; }
        public List<FavoriteProjectDTO> Favoritos { get; set; }
        public List<CommentDTO>? Comments { get; set; }
    }


}
