namespace BaseProject.Models
{
    public class Movie : BaseModel

    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } 
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; } 

        public List<MovieSchedule> Schedules { get; set; } = new List<MovieSchedule>();
    }
}
