using BaseProject.Dtos;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(IMovieServices movieServices, IMessageHandler messageHandler) : BaseController(messageHandler)

    {
        private readonly IMovieServices _movieServices = movieServices;



        [HttpPost]

        public async Task<IActionResult> Post([FromBody] AddNewMovieDto input)
        {
            return GetServiceResponse(await _movieServices.AddMovieAsync(input));
        }


        [HttpGet]

        public async Task<IActionResult> Get([FromQuery]GlobalFilterDto input)
        {
            return GetServiceResponse(await _movieServices.GetMoveListAsync(input));
        }



        [HttpGet("GetAvilableMovie")]

        public async Task<IActionResult> GetAvilableMovie([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _movieServices.GetMoveAvailableAsync(input));
        }


        [HttpGet("{mid}")]

        public async Task<IActionResult> Get(int mid)
        {
            return GetServiceResponse(await _movieServices.GetMovieAsync(mid));

        }

        [HttpPut("update/{mid}")]

        public async Task<IActionResult> Update(int mid, [FromBody] updateMovieDto input)
        {
            return GetServiceResponse(await _movieServices.UpdateMovieAsync(mid, input));
        }


        [HttpPut("delete/{mid}")]

        public async Task<IActionResult> Deleted(int mid)
        {
            return GetServiceResponse(await _movieServices.DeleteMovieAsync(mid));
        }
    }
}
