using EcoLudicoAPI.Enums;

namespace EcoLudicoAPI.DTOS
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public DateOnly DateBirth { get; set; }
        public AddressDTO Address { get; set; } 
        public int? SchoolId { get; set; }
        public SchoolDTO? School { get; set; }
        public List<SchoolDTO>? FavoriteSchools { get; set; }
        public List<FavoriteProjectDTO> FavoriteProjects { get; set; } = new();
        public List<CommentDTO> MadeComments { get; set; } = new();
    }

}
