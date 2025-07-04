using LLTU2025_7_MovieApi.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class Actor
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    public int BirthYear { get; set; }

    public ICollection<Movie> Movies { get; set; } = [];

    internal ActorDto MapToDto()
    {
        return new ActorDto
        {
            Id = Id,
            Name = Name,
            BirthYear = BirthYear
        };
    }

    internal ActorDetailsDto MapToDetailsDto()
    {
        return new ActorDetailsDto
        {
            Id = Id,
            Name = Name,
            BirthYear = BirthYear,
            Movies = Movies.Select(movie => movie.MapToDto()).ToList()
        };
    }
}
