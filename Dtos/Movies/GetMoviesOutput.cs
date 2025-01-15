using BaseProject.Models;

namespace BaseProject.Dtos.Movies
{
    public class GetMoviesOutput
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public MovieStatus Status { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationInMinutes { get; set; }
        public int CinemaId { get; set; }
    }
}
