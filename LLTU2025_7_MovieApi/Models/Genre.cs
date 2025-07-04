﻿using LLTU2025_7_MovieApi.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class Genre : EntityBase
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = [];

    internal GenreDto MapToDto()
    {
        return new GenreDto
        {
            Id = Id,
            Name = Name
        };
    }
}
