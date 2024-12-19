using BaseProject.Models;

namespace BaseProject.Dtos.Movie
{
    public class GetMovieOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieType Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public int CinemaHallId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
