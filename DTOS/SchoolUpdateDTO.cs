using EcoLudicoAPI.DTOS;
using System.ComponentModel.DataAnnotations;

public class SchoolUpdateDTO
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public AddressDTO Address { get; set; } = new AddressDTO();

    [Phone]
    public string Contact { get; set; } = string.Empty;

    public string OperatingHours { get; set; } = string.Empty;
}
