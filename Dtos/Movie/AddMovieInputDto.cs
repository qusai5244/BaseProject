using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Movie
{
    public class AddMovieInputDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieType Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }

        [Required]
        public int CinemaHallId { get; set; }
    }
}
