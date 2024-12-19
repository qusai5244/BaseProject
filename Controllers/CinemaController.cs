using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController(ICinemaService cinemaService) : ControllerBase
    {
        private readonly ICinemaService _cinemaService= cinemaService;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cinema>>> GetAllCinemas()
        {
            return Ok(await _cinemaService.GetAllCinemasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cinema>> GetCinemaById(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }
            return Ok(cinema);
        }

        [HttpPost]
        public async Task<ActionResult> AddCinema([FromBody] Cinema cinema)
        {
            await _cinemaService.AddCinemaAsync(cinema);
            return CreatedAtAction(nameof(GetCinemaById), new { id = cinema.CinemaID }, cinema);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCinema(int id, [FromBody] Cinema cinema)
        {
            if (id != cinema.CinemaID)
            {
                return BadRequest();
            }
            await _cinemaService.UpdateCinemaAsync(cinema);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCinema(int id)
        {
            await _cinemaService.DeleteCinemaAsync(id);
            return NoContent();
        }
    }
}
