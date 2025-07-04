namespace LLTU2025_7_MovieApi.Models.DTO;

public class ActorDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public List<MovieDto> Movies { get; set; } = [];
}
