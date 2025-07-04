namespace LLTU2025_7_MovieApi.Models.DTO;

public class UpdateMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Duration { get; set; }
    public int GenreId { get; set; }
}
