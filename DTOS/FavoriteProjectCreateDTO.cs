using System.ComponentModel.DataAnnotations; 

public class FavoriteProjectCreateDTO
{
    [Required(ErrorMessage = "O ID do projeto é obrigatório.")]
    public int ProjectId { get; set; }
}