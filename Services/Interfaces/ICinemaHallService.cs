using BaseProject.Models;

namespace BaseProject.Services
{
    public interface ICinemaHallService
    {
        Task<IEnumerable<CinemaHall>> GetAllCinemaHallsAsync();
        Task<CinemaHall> GetCinemaHallByIdAsync(int id);
        Task AddCinemaHallAsync(CinemaHall cinemaHall);
        Task UpdateCinemaHallAsync(CinemaHall cinemaHall);
        Task DeleteCinemaHallAsync(int id);
    }
}
