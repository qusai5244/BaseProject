using BaseProject.Dtos.Movie;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BaseProject.Helpers;



namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService, IMessageHandler messageHandler)
            : base(messageHandler)
        {
            _movieService = movieService;
        }

      


       
        [HttpPost]
        public async Task<IActionResult> AddNewMovieAsync([FromBody] AddNewMovie input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return GetServiceResponse(await _movieService.AddNewMovieAsync(input)); 
        }


        [HttpGet]
        public async Task<IActionResult> GetMovieList([FromQuery] GetMovieListInput input)
        {
            return GetServiceResponse(await _movieService.GetMovieList(input));
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync([FromRoute] int id, [FromBody] UpdateMovieInput input)
        {
            return GetServiceResponse(await _movieService.UpdateMovieAsync(id, input));
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync([FromRoute] int id)
        {
          
            return GetServiceResponse(await _movieService.DeleteMovieAsync(id));
        }
    }
}
