using BaseProject.Dtos;
using BaseProject.Dtos.Ciname;
using BaseProject.Dtos.Hall;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController(IHallServices hallServices, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        private readonly IHallServices _hallServices = hallServices;

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] AddNewHallDto input)
        {
            return GetServiceResponse(await _hallServices.AddHallAsync(input));
        }


        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto globalFilter)
        {
            return GetServiceResponse(await _hallServices.GetHallListAsync(globalFilter));
        }

        [HttpGet("{hid}")]

        public async Task<IActionResult> Get(int hid)
        {
            return GetServiceResponse(await _hallServices.GetHallAsync(hid));
        }



    }
}
