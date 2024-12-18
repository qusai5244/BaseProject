using BaseProject.Models;
using BaseProject.Services.Interfaces;
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

       
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            if (movies == null || !movies.Any())
            {
                return NotFound("No movies found.");
            }
            return Ok(movies);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }
            return Ok(movie);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest("Movie data is required.");
            }

            var createdMovie = await _movieService.CreateMovieAsync(movie);

            return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, createdMovie);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (movie == null || movie.Id != id)
            {
                return BadRequest("Movie data is invalid or ID mismatch.");
            }

            var existingMovie = await _movieService.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            var updatedMovie = await _movieService.UpdateMovieAsync(movie);
            return Ok(updatedMovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound($"Movie with ID {id} not found.");
            }

            await _movieService.DeleteMovieAsync(id);
            return NoContent();
        }
    }
}

