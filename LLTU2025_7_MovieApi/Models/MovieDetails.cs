using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class MovieDetails
{
    public int Id { get; set; }

    [Required]
    public string Synopsis { get; set; } = string.Empty;

    [Required]
    public string Language { get; set; } = string.Empty;

    public decimal Budget { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}