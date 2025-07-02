using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public int Year { get; set; }

    public int Duration { get; set; }

    public int GenreId { get; set; }
    
    public Genre Genre { get; set; }

    public ICollection<Review> Reviews { get; set; } = [];
}
