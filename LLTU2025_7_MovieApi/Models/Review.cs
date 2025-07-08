using LLTU2025_7_MovieApi.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class Review : EntityBase
{
    public int Id { get; set; }

    [Range(1.0, 5.0)]
    public double Rating { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Comment { get; set; } = string.Empty;

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    internal ReviewDto MapToDto()
    {
        return new ReviewDto
        {
            Id = Id,
            Rating = Rating,
            Name = Name,
            Comment = Comment
        };
    }
}
