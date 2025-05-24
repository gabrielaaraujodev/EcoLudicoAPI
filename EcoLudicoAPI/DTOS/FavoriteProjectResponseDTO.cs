namespace EcoLudicoAPI.DTOS
{
    public class FavoriteProjectResponseDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public string? AgeRange { get; set; } 
    }
}
