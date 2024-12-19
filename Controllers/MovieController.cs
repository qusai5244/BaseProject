using BaseProject.Models;
using BaseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies([FromQuery] string searchQuery)
        {
            var movies = await _movieService.SearchMoviesAsync(searchQuery);
            if (movies == null || !movies.Any())
            {
                return NotFound();
            }
            return Ok(movies);
        }
    }
}
