using LLTU2025_7_MovieApi.Data;
using LLTU2025_7_MovieApi.Models;
using LLTU2025_7_MovieApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLTU2025_7_MovieApi.Controllers
{
    [Route("reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ReviewsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("/movies/{movieId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Where(review => review.MovieId == movieId)
                .Select(review => review.MapToDto())
                .ToListAsync();
        }

        [HttpPost("/movies/{movieId}/reviews")]
        public async Task<ActionResult<ReviewDto>> CreateReview(int movieId, CreateReviewDto createReviewDto)
        {
            var review = new Review
            {
                MovieId = movieId,
                Rating = createReviewDto.Rating,
                Name = createReviewDto.Name,
                Comment = createReviewDto.Comment
            };

            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();

            return Ok(review.MapToDto());
        }

        [HttpDelete("/reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
