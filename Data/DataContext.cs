using BaseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Car>? Cars { get; set; }

        public DbSet<Cinema>? Cinemas { get; set; }
        public DbSet<CinemaHall>? CinemaHalls { get; set; }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Showtime>? Showtimes { get; set; }
    }
}
