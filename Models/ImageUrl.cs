namespace EcoLudicoAPI.Models
{
    public class ImageUrl
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }

}
