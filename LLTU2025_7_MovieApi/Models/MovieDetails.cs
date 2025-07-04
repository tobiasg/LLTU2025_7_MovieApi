﻿using LLTU2025_7_MovieApi.Models.DTO;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;

namespace LLTU2025_7_MovieApi.Models;

public class MovieDetails : EntityBase
{
    public int Id { get; set; }

    [Required]
    public string Synopsis { get; set; } = string.Empty;

    [Required]
    public string Language { get; set; } = string.Empty;

    public decimal Budget { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    internal MovieDetailsDto MapToDto()
    {
        return new MovieDetailsDto
        {
            Synopsis = Synopsis,
            Language = Language,
            Budget = Budget
        };
    }
}