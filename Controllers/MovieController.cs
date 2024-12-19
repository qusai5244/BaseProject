using BaseProject.Dtos;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController(IMovieService movieService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly IMovieService _movieService = movieService;

        [HttpPost]
        public async Task<IActionResult> AddMovieAsync([FromBody] AddMovieInputDto input)
        {
            return GetServiceResponse(await _movieService.AddMovieAsync(input));
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieListAsync([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _movieService.GetMovieListAsync(input));
        }

        [HttpGet("GetCinemasByMovieName/{movieName}")]
        public async Task<IActionResult> GetCinemasByMovieName(string movieName)
        {
            return GetServiceResponse(await _movieService.GetCinemasByMovieNameAsync(movieName));
        }

        [HttpGet("GetMoviesByCinema/{cinemaId}")]
        public async Task<IActionResult> GetMoviesByCinemaId(int cinemaId)
        {
            return GetServiceResponse(await _movieService.GetMoviesByCinemaIdAsync(cinemaId));
        }

        [HttpPut("{movieId}")]
        public async Task<IActionResult> UpdateMovieAsync(int movieId, [FromBody] UpdateMovieInputDto input)
        {
            return GetServiceResponse(await _movieService.UpdateMovieAsync(movieId, input));
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovieAsync([FromRoute] int movieId)
        {
            return GetServiceResponse(await _movieService.DeleteMovieAsync(movieId));
        }
    }
}
