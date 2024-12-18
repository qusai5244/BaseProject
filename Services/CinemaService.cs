using BaseProject.Data;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class CinemaService: ICinemaService
    {
        private readonly DataContext _context;

        public CinemaService(DataContext context)
        {
            _context = context;
        }

        public async Task<Cinema> CreateCinemaAsync(Cinema cinema)
        {
            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();
            return cinema;
        }

        public async Task<Cinema> GetCinemaByIdAsync(int cinemaId)
        {
            return await _context.Cinemas.Include(c => c.CinemaHalls)
                                          .FirstOrDefaultAsync(c => c.Id == cinemaId);
        }

        public async Task<IEnumerable<Cinema>> GetAllCinemasAsync()
        {
            return await _context.Cinemas.ToListAsync();
        }
    }
}
