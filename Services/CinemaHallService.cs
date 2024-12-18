using BaseProject.Models;
using BaseProject.Services.Interfaces;
using BaseProject.Data;

namespace BaseProject.Services
{
    public class CinemaHallService: ICinemaHallService
    {
        private readonly DataContext _context;

        public CinemaHallService(DataContext context)
        {
            _context = context;
        }

        public async Task<CinemaHall> CreateCinemaHallAsync(CinemaHall cinemaHall)
        {
            _context.CinemaHalls.Add(cinemaHall);
            await _context.SaveChangesAsync();
            return cinemaHall;
        }

        public async Task<IEnumerable<CinemaHall>> GetCinemaHallsByCinemaIdAsync(int cinemaId)
        {
            return await _context.CinemaHalls.Where(ch => ch.CinemaId == cinemaId).ToListAsync();
        }
    }
}

