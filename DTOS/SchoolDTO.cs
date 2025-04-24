namespace EcoLudicoAPI.DTOS
{
    public class SchoolDTO
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public string Contact { get; set; }
        public string OperatingHours { get; set; }
        public List<UserDTO>? Teachers { get; set; }
        public List<ProjectDTO>? Projects { get; set; }
    }


}
