namespace EcoLudicoAPI.DTOS
{
    public class FavoriteProjectDTO
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public ProjectDTO? Projeto { get; set; }
    }
}
