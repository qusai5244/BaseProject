using BaseProject.Dtos.Cinema;
using BaseProject.Dtos.Movies;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class CinemaController(ICinemaService cinemaService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        private readonly ICinemaService _cinemaService = cinemaService;

        [HttpPost]
        public async Task<IActionResult> AddCinemaAsync(AddCinemaInput input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            return GetServiceResponse(await _cinemaService.AddCinemaAsync(input));
        }

        [HttpPost("{cinemaId}/hall")]
        public async Task<IActionResult> AddCinemaHallAsync([FromRoute]int cinemaId, [FromBody]AddHallInput input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            return GetServiceResponse(await _cinemaService.AddCinemaHallAsync(cinemaId, input));
        }

        [HttpPost("{cinemaId}/movie")]
        public async Task<IActionResult> AddMovieAsync([FromRoute] int cinemaId, [FromBody] AddMovieInput input)
        {
            return GetServiceResponse(await _cinemaService.AddMovieAsync(cinemaId, input));
        }

        [HttpGet("{cinemaId}/movies")]
        public async Task<IActionResult> GetMoviesAsync([FromRoute] int cinemaId, [FromQuery] GetMoviesInput input)
        {
            return GetServiceResponse(await _cinemaService.GetMoviesAsync(cinemaId, input));
        }

        [HttpPost("assign-movie")]
        public async Task<IActionResult> AssignMovieToHallAsync([FromForm] AssignMovieToHallInput input)
        {
            return GetServiceResponse(await _cinemaService.AssignMovieToHallAsync(input));
        }

        [HttpGet("movies-time")]
        public async Task<IActionResult> GetMoviesTimeAsync([FromQuery] GetMoviesTimeInput input)
        {
            return GetServiceResponse(await _cinemaService.GetMoviesTimeAsync(input));
        }
    }
}
