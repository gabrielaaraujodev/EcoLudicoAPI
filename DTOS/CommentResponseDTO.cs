namespace EcoLudicoAPI.DTOS
{
    public class CommentResponseDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

}
