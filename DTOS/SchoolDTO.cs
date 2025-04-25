namespace EcoLudicoAPI.DTOS
{
    public class SchoolDTO
    {
        public int SchoolId { get; set; }
        public string Name { get; set; } = String.Empty;
        public AddressDTO? Address { get; set; }
        public string Contact { get; set; } = String.Empty;
        public string OperatingHours { get; set; } = String.Empty;
        public List<UserDTO>? Teachers { get; set; }
        public List<ProjectDTO>? Projects { get; set; }
    }


}
