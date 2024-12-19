namespace BaseProject.Dtos.Cinema
{
    public class MovieScheduleDto
    {
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime ShowTime { get; set; }
        public int AvailableSeats { get; set; }
    }
}
