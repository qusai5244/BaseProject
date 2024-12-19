using BaseProject.Models;
using BaseProject.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly DataContext _dataContext;

        public ShowtimeService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<Showtime>> GetAllShowtimesAsync()
        {
            return await _dataContext.Showtimes
                                 .Include(s => s.MovieID)
                                 .Include(s => s.HallID)
                                 .ToListAsync();
        }

        public async Task<Showtime> GetShowtimeByIdAsync(int id)
        {
            return await _dataContext.Showtimes
                                 .Include(s => s.MovieID)
                                 .Include(s => s.HallID)
                                 .FirstOrDefaultAsync(s => s.ShowtimeID == id);
        }

        public async Task AddShowtimeAsync(Showtime showtime)
        {
            await _dataContext.Showtimes.AddAsync(showtime);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateShowtimeAsync(Showtime showtime)
        {
            _dataContext.Showtimes.Update(showtime);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteShowtimeAsync(int id)
        {
            var showtime = await _dataContext.Showtimes.FindAsync(id);
            if (showtime != null)
            {
                _dataContext.Showtimes.Remove(showtime);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
