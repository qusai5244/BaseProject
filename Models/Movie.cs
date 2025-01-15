namespace BaseProject.Models
{
    public class Movie : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public MovieType Type { get; set; } 
        public MovieStatus Status { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationInMinutes { get; set; }
        public int CinemaId { get; set; }

        // Navigation properties
        public Cinema Cinema { get; set; }
        public ICollection<MovieTime> MovieTimes { get; set; }
    }

    public enum MovieStatus
    {
        NotAvailable = 1,
        Available = 2,
        Upcoming = 3,
    }

    public enum MovieType
    {
        Action = 1, 
        Comedy, 
        Drama
    }
}
