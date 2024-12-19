namespace BaseProject.Dtos.Movie
{
    public class GetCinemasByMovieNameOutputDto
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string CinemaLocation { get; set; }
        public string CinemaHall { get; set; }
    }
}
