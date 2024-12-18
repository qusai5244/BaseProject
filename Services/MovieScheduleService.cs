using BaseProject.Models;
using BaseProject.Services.Interfaces;
using BaseProject.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class MovieScheduleService: IMovieScheduleService
    {
        private readonly DataContext _context;

        public MovieScheduleService(DataContext context)
        {
            _context = context;
        }

        public async Task<MovieSchedule> CreateMovieScheduleAsync(MovieSchedule movieSchedule)
        {
            _context.MovieSchedules.Add(movieSchedule);
            await _context.SaveChangesAsync();
            return movieSchedule;
        }

        public async Task<IEnumerable<MovieSchedule>> GetSchedulesByMovieIdAsync(int movieId)
        {
            return await _context.MovieSchedules
                .Where(ms => ms.MovieId == movieId).ToListAsync();
        }

        public async Task<IEnumerable<MovieSchedule>> GetSchedulesByCinemaIdAsync(int cinemaId)
        {
            return await _context.MovieSchedules
                .Where(ms => ms.CinemaHall.CinemaId == cinemaId)
                .ToListAsync();
        }
    }
}
