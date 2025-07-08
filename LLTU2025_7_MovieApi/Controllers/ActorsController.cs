using LLTU2025_7_MovieApi.Data;
using LLTU2025_7_MovieApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLTU2025_7_MovieApi.Controllers;

[Route("actors")]
[ApiController]
[Produces("application/json")]
public class ActorsController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ActorsController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
    public async Task<ActionResult<IEnumerable<ActorDto>>> GetMovies()
    {
        return await _context.Actors
            .AsNoTracking()
            .Select(actor => actor.MapToDto()).ToListAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDetailsDto))]
    public async Task<ActionResult<ActorDetailsDto>> GeActor(int id)
    {
        var actor = await _context.Actors
            .AsNoTracking()
            .Include(a => a.Movies)
            .ThenInclude(m => m.Genres)
            .Include(a => a.Movies)
            .ThenInclude(m => m.Reviews)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (actor == null)
        {
            return NotFound();
        }

        return actor.MapToDetailsDto();
    }

    [HttpPost("/movies/{movieId}/actors/{actorId}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActorDto))]
    public async Task<ActionResult<ActorDto>> AddActorToMovie(int movieId, int actorId)
    {
        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        var actor = await _context.Actors.FindAsync(actorId);

        if (movie == null || actor == null)
        {
            return NotFound();
        }
        
        movie.Actors.Add(actor);
        await _context.SaveChangesAsync();

        return actor.MapToDto();
    }
}
