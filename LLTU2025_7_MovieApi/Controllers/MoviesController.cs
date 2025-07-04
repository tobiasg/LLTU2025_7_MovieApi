using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LLTU2025_7_MovieApi.Data;
using LLTU2025_7_MovieApi.Models;
using LLTU2025_7_MovieApi.Models.DTO;

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
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        return await _context.Movies
            .AsNoTracking()
            .Include(m => m.Genre)
            .Include(m => m.Details)
            .Include(m => m.Reviews)
            .Include(m => m.Actors)
            .Select(movie => movie.MapToDto()).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .Include(m => m.Genre)
            .Include(m => m.Details)
            .Include(m => m.Reviews)
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie.MapToDto();
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
        movie.GenreId = updateMovieDto.GenreId;

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
            Duration = createMovieDto.Duration,
            GenreId = createMovieDto.GenreId
        };

        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        await _context.Entry(movie).Reference(m => m.Genre).LoadAsync();

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
