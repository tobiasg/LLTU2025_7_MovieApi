using LLTU2025_7_MovieApi.Data;
using LLTU2025_7_MovieApi.Models;
using LLTU2025_7_MovieApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLTU2025_7_MovieApi.Controllers;

[Route("[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ApplicationContext _context;

    public MoviesController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] int? year)
    {
        var movies = _context.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .AsQueryable();

        if (year.HasValue)
        {
            movies = movies.Where(movie => movie.Year == year.Value);
        }

        return await movies.Select(movie => movie.MapToDto()).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie.MapToDto();
    }

    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .Where(movie => movie.Id == id)
            .Select(movie => new MovieDetailsDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Duration = movie.Duration,
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

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, UpdateMovieDto updateMovieDto)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        movie.Title = updateMovieDto.Title;
        movie.Year = updateMovieDto.Year;
        movie.Duration = updateMovieDto.Duration;

        _context.Entry(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostMovie(CreateMovieDto createMovieDto)
    {
        var movie = new Movie
        {
            Title = createMovieDto.Title,
            Year = createMovieDto.Year,
            Duration = createMovieDto.Duration
        };

        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie.MapToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}
