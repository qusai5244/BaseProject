using BaseProject.Models;

namespace BaseProject.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<Cinema> CreateCinemaAsync(Cinema cinema);
        Task<Cinema> GetCinemaByIdAsync(int cinemaId);
        Task<IEnumerable<Cinema>> GetAllCinemasAsync();
    }
}

