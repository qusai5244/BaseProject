using BaseProject.Dtos.MovieShowTime;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieShowTimeController(IMovieShowTime movieShowTime, IMessageHandler messageHandler) : BaseController(messageHandler)

    {
        public IMovieShowTime _movieShowTime = movieShowTime;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddNewMovieShowTimeDto input)
        {
            return GetServiceResponse(await _movieShowTime.AddMovieAsync(input));
        }
    }
}
