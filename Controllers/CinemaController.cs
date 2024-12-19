using BaseProject.Dtos.Car;
using BaseProject.Dtos.Cinema;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CinemaController(ICinemaService cinemaService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly ICinemaService _cinemaService = cinemaService;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCinemaInputDto input)
        {
            return GetServiceResponse(await _cinemaService.AddCinemaAsync(input));
        }
    }
}
