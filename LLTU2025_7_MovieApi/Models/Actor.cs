using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class Actor
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    public int BirthYear { get; set; }

    public ICollection<Movie> Movies { get; set; } = [];
}
