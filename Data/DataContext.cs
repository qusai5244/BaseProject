using BaseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<MovieSchedule> MovieSchedules { get; set; }

        // Additional model configuration can be added here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(m => m.Title)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(m => m.Description)
                      .HasMaxLength(1000);
                entity.Property(m => m.Type)
                      .HasMaxLength(50);
                entity.Property(m => m.ReleaseDate)
                      .IsRequired();
                entity.Property(m => m.Duration)
                      .IsRequired();
            });

         
            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(c => c.Location)
                      .IsRequired()
                      .HasMaxLength(300);

                entity.HasMany(c => c.CinemaHalls)
                      .WithOne(ch => ch.Cinema)
                      .HasForeignKey(ch => ch.CinemaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        
        }
    }
}