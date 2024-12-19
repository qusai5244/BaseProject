using BaseProject.Dtos;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.CinemaHall;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services;
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
        public async Task<IActionResult> Post([FromBody] AddMovieInputDto input)
        {
            return GetServiceResponse(await _movieService.AddMovieAsync(input));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _movieService.GetMovieListAsync(input));
        }

        [HttpPut("{movieId}")]
        public async Task<IActionResult> Put(int movieId, [FromBody] UpdateMovieInputDto input)
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
