namespace LLTU2025_7_MovieApi.Models.DTO;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Duration { get; set; }
    public string Genre { get; set; } = string.Empty;
    public MovieDetailsDto? Details { get; set; }
    public List<ReviewDto> Reviews { get; set; } = [];
    public List<ActorDto> Actors { get; set; } = [];
}
