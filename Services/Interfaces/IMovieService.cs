using BaseProject.Models;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task<Movie> CreateMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int movieId);
    }
}
