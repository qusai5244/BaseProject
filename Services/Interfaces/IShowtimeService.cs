using BaseProject.Models;

namespace BaseProject.Services
{
    public interface IShowtimeService
    {
        Task<IEnumerable<Showtime>> GetAllShowtimesAsync();
        Task<Showtime> GetShowtimeByIdAsync(int id);
        Task AddShowtimeAsync(Showtime showtime);
        Task UpdateShowtimeAsync(Showtime showtime);
        Task DeleteShowtimeAsync(int id);
    }
}
