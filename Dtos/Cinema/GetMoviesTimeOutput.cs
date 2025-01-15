namespace BaseProject.Dtos.Cinema
{
    public class GetMoviesTimeOutput
    {

        public string HallName { get; set; }
        public string MovieName { get; set; }
        public DateTime Date { get; set; }
        public List<DisplayTimes> Times { get; set; }
    }

    public class DisplayTimes
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
