using BaseProject.Data;
using BaseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class MovieService : IMovieService
    {
        private readonly DataContext _dataContext;

        public MovieService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _dataContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _dataContext.Movies.FindAsync(id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _dataContext.Movies.AddAsync(movie);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _dataContext.Movies.Update(movie);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _dataContext.Movies.FindAsync(id);
            if (movie != null)
            {
                _dataContext.Movies.Remove(movie);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string searchQuery)
        {
            return await _dataContext.Movies
            .Where(m => m.Title.Contains(searchQuery))
            .ToListAsync();
        }
    }
}
