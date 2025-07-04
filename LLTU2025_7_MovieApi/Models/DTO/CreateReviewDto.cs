using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models.DTO;

public class CreateReviewDto
{
    [Required]
    public int Rating { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Comment { get; set; } = string.Empty;
}