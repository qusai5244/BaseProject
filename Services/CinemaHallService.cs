using BaseProject.Data;
using BaseProject.Models;
using Microsoft.EntityFrameworkCore;


namespace BaseProject.Services
{
    public class CinemaHallService : ICinemaHallService
    {
        private readonly DataContext _dataContext;

        public CinemaHallService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<CinemaHall>> GetAllCinemaHallsAsync()
        {
            return await _dataContext.CinemaHalls.Include(ch => ch.CinemaID).ToListAsync();
        }

        public async Task<CinemaHall> GetCinemaHallByIdAsync(int id)
        {
            return await _dataContext.CinemaHalls.Include(ch => ch.CinemaID).FirstOrDefaultAsync(ch => ch.HallID == id);
        }

        public async Task AddCinemaHallAsync(CinemaHall cinemaHall)
        {
            await _dataContext.CinemaHalls.AddAsync(cinemaHall);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateCinemaHallAsync(CinemaHall cinemaHall)
        {
            _dataContext.CinemaHalls.Update(cinemaHall);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteCinemaHallAsync(int id)
        {
            var cinemaHall = await _dataContext.CinemaHalls.FindAsync(id);
            if (cinemaHall != null)
            {
                _dataContext.CinemaHalls.Remove(cinemaHall);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
