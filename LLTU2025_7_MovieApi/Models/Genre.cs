using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class Genre
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = [];
}
