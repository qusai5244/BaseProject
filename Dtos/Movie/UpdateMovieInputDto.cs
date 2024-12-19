using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Movie
{
    public class UpdateMovieInputDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public MovieType Type { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int CinemaHallId { get; set; }
    }
}
