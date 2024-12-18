using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CinemaController: ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCinemas()
        {
            var cinemas = await _cinemaService.GetAllCinemasAsync();
            return Ok(cinemas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCinema(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);
            if (cinema == null) return NotFound();
            return Ok(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCinema(Cinema cinema)
        {
            var createdCinema = await _cinemaService.CreateCinemaAsync(cinema);
            return CreatedAtAction(nameof(GetCinema), new { id = createdCinema.Id }, createdCinema);
        }
    }
}
