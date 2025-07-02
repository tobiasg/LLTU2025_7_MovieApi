namespace LLTU2025_7_MovieApi.Models.DTO;

public class ReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}