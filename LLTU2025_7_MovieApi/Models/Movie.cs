using LLTU2025_7_MovieApi.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class Movie : EntityBase
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public int Year { get; set; }

    public int Duration { get; set; }

    public MovieDetails? Details { get; set; }
    public ICollection<Genre> Genres { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Actor> Actors { get; set; } = [];

    internal MovieDto MapToDto()
    {
        return new MovieDto
        {
            Id = Id,
            Title = Title,
            Year = Year,
            Duration = Duration,
            Genres = Genres.Select(genre => genre.MapToDto()).ToList(),
        };
    }

    internal MovieDetailsDto MapToDetailsDto()
    {
        return new MovieDetailsDto
        {
            Id = Id,
            Title = Title,
            Year = Year,
            Duration = Duration,
            Synopsis = Details?.Synopsis ?? string.Empty,
            Language = Details?.Language ?? string.Empty,
            Budget = Details?.Budget ?? 0m,
            Genres = Genres.Select(genre => genre.MapToDto()).ToList(),
            Reviews = Reviews.Select(review => review.MapToDto()).ToList(),
            Actors = Actors.Select(actor => actor.MapToDto()).ToList()
        };
    }
}
