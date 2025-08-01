﻿namespace LLTU2025_7_MovieApi.Models.DTO;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int Duration { get; set; }
    public double AverageRating { get; set; }
    public List<GenreDto> Genres { get; set; } = [];
}
