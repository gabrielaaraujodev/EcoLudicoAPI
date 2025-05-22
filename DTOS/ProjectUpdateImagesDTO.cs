namespace EcoLudicoAPI.DTOS
{
    public class ProjectUpdateImagesDTO
    {
        public List<string>? KeepImageUrls { get; set; } 
        public List<IFormFile>? NewFiles { get; set; }   
    }

}
