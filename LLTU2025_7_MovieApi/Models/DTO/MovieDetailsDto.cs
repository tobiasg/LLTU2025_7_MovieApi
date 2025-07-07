namespace LLTU2025_7_MovieApi.Models.DTO;

public class MovieDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Duration { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public List<GenreDto> Genres { get; set; } = [];
    public List<ReviewDto> Reviews { get; set; } = [];
    public List<ActorDto> Actors { get; set; } = [];
}