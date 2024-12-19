using BaseProject.Dtos.CinemaHall;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CinemaHallController(ICinemaHallService cinemaHallService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly ICinemaHallService _cinemaHallService = cinemaHallService;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCinemaHallInputDto input)
        {
            return GetServiceResponse(await _cinemaHallService.AddCinemaHallAsync(input));
        }
    }
}
