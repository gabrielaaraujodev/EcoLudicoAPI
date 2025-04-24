namespace EcoLudicoAPI.DTOS
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int ProjectId { get; set; }
        public ProjectDTO Project { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
