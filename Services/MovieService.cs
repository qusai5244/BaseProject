using BaseProject.Data;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
 
        public class MovieService : IMovieService
        {
            private readonly DataContext _context;

            public MovieService(DataContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
            {
                return await _context.Movies.ToListAsync();
            }

            public async Task<Movie> GetMovieByIdAsync(int movieId)
            {
                return await _context.Movies.FindAsync(movieId);
            }

            public async Task<Movie> CreateMovieAsync(Movie movie)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return movie;
            }

            public async Task<Movie> UpdateMovieAsync(Movie movie)
            {
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return movie;
            }

            public async Task DeleteMovieAsync(int movieId)
            {
                var movie = await _context.Movies.FindAsync(movieId);
                if (movie != null)
                {
                    _context.Movies.Remove(movie);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
