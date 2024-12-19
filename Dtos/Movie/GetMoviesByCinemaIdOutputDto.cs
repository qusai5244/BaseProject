namespace BaseProject.Dtos.Movie
{
    public class GetMoviesByCinemaIdOutputDto
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string CinemaHall { get; set; }
    }
}
