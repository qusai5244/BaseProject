namespace BaseProject.Dtos.Cinema
{
    public class AssignMovieToHallInput
    {

        public int CinemaId { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public List<int> StartTimes { get; set; }
        public DateTime Date { get; set; }
    }
}
