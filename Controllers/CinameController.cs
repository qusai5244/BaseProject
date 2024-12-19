using BaseProject.Dtos;
using BaseProject.Dtos.Ciname;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{


    public class CinameController(ICinameServices cinameServices, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        private readonly ICinameServices _cinameServices = cinameServices;


        [HttpPost]

        public async Task<IActionResult> Post([FromBody] AddNewCinameDto cinameDto)
        {
            return GetServiceResponse(await _cinameServices.AddCinameAsync(cinameDto));
        }


        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto globalFilter)
        {
            return GetServiceResponse(await _cinameServices.GetCinemaListAsync(globalFilter));
        }

        [HttpGet("{cnid}")]

        public async Task<IActionResult> Get(int cnid)
        {
            return GetServiceResponse(await _cinameServices.GetCinemaAsync(cnid));
        }







    }

}
