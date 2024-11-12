using BaseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
