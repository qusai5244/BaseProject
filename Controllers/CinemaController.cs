﻿using BaseProject.Dtos.Cinema;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{


        [Route("api/[controller]")]
        [ApiController]
        public class CinemaController : BaseController
        {
        private readonly IMovieService _movieService;

        public CinemaController(IMovieService movieService, IMessageHandler messageHandler)
            : base(messageHandler)
        {
            _movieService = movieService;
        }

        [HttpPost("add-cinema")]
        public async Task<IActionResult> AddCinemaAsync([FromBody] CinemaDto cinemaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return GetServiceResponse(await _movieService.AddCinemaAsync(cinemaDto));
        }

        [HttpPost("add-hall/{cinemaId}")]
        public async Task<IActionResult> AddHallToCinemaAsync(int cinemaId, [FromBody] HallDto hallDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return GetServiceResponse(await _movieService.AddHallToCinemaAsync(cinemaId, hallDto));
        }

        [HttpPost("schedule-movie")]
        public async Task<IActionResult> ScheduleMovieAsync([FromBody] MovieScheduleDto scheduleDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return GetServiceResponse(await _movieService.ScheduleMovieAsync(scheduleDto));
        }
    }
    }

        
        