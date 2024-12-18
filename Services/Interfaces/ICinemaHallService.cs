using BaseProject.Models;

namespace BaseProject.Services.Interfaces
{
    public interface ICinemaHallService
    {
        Task<CinemaHall> CreateCinemaHallAsync(CinemaHall cinemaHall);
        Task<IEnumerable<CinemaHall>> GetCinemaHallsByCinemaIdAsync(int cinemaId);
    }
}
