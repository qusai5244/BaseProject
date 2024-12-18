using BaseProject.Models;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieScheduleService
    {
        Task<MovieSchedule> CreateMovieScheduleAsync(MovieSchedule movieSchedule);
        Task<IEnumerable<MovieSchedule>> GetSchedulesByMovieIdAsync(int movieId);
        Task<IEnumerable<MovieSchedule>> GetSchedulesByCinemaIdAsync(int cinemaId);
    }
}
