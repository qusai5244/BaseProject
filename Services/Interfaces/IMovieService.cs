using BaseProject.Models;

namespace BaseProject.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);

        Task<IEnumerable<Movie>> SearchMoviesAsync(string searchQuery);
    }
}
