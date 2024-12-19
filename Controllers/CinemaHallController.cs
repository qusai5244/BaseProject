using BaseProject.Dtos;
using BaseProject.Dtos.CinemaHall;
using BaseProject.Helpers.MessageHandler;
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
        public async Task<IActionResult> AddCinemaHallAsync([FromBody] AddCinemaHallInputDto input)
        {
            return GetServiceResponse(await _cinemaHallService.AddCinemaHallAsync(input));
        }

        [HttpGet]
        public async Task<IActionResult> GetCinemaHallListAsync([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _cinemaHallService.GetCinemaHallListAsync(input));
        }
    }
}
