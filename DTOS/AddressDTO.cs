namespace EcoLudicoAPI.DTOS
{
    public class AddressDTO
    {
        public string Street { get; set; } = String.Empty;
        public string Number { get; set; } = String.Empty;
        public string Complement { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string State { get; set; } = String.Empty;
        public string Latitude { get; set; } = String.Empty;
        public string Longitude { get; set; } = String.Empty;
    }
}
