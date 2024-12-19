using BaseProject.Dtos.Movie;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(IMovieService movieService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        private readonly IMovieService _movieService = movieService;

        [HttpPost]
        public async Task<IActionResult> AddNewMovieAsync([FromBody] AddNewMovie input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            return GetServiceResponse(await _movieService.AddNewMovieAsync(input));

        }
    }
}
