using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models.DTO;

public class CreateMovieDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }
    
    [Required]
    public int Duration { get; set; }

    public int GenreId { get; set; }
}