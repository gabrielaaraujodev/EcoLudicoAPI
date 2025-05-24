namespace EcoLudicoAPI.DTOS
{
    public class FavoriteSchoolResponseDTO
    {
        public int SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public AddressDTO? Address { get; set; } 
        public string? OperatingHours { get; set; } = string.Empty;
        public string? Contact { get; set; } = string.Empty;
    }

}
