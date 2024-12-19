using BaseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Movies> Movies { get; set; }
    }
}
