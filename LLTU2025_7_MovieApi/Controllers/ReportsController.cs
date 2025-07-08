using LLTU2025_7_MovieApi.Data;
using LLTU2025_7_MovieApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLTU2025_7_MovieApi.Controllers;

[Route("reports")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ReportsController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("/reports/movies/top-rated-movie")]
    public async Task<ActionResult<MovieDetailsDto>> GetTopRatedMovie()
    {
        var topRatedMovie = await _context.Movies
            .AsNoTracking()
            .Where(movie => movie.Reviews.Any())
            .OrderByDescending(movie => movie.Reviews.Select(review => review.Rating).Average())
            .Select(movie => new MovieDetailsDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Duration = movie.Duration,
                AverageRating = movie.Reviews.Count > 0 ? Math.Round(movie.Reviews.Select(review => review.Rating).Average(), 1) : 0,
                Genres = movie.Genres.Select(genre => new GenreDto
                {
                    Id = genre.Id,
                    Name = genre.Name
                }).ToList(),
                Synopsis = movie.Details != null ? movie.Details.Synopsis : string.Empty,
                Language = movie.Details != null ? movie.Details.Language : string.Empty,
                Budget = movie.Details != null ? movie.Details.Budget : 0m,
                Reviews = movie.Reviews.Select(review => new ReviewDto
                {
                    Id = review.Id,
                    Name = review.Name,
                    Rating = review.Rating,
                    Comment = review.Comment
                }).ToList(),
                Actors = movie.Actors.Select(actor => new ActorDto
                {
                    Id = actor.Id,
                    Name = actor.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return topRatedMovie == null ? NotFound() : Ok(topRatedMovie);
    }
}
