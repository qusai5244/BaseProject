namespace BaseProject.Models
{
    public class MovieSchedule : BaseModel
    {
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime Showtime { get; set; }

        public Movie Movie { get; set; }
        public CinemaHall CinemaHall { get; set; }
    }
}
