namespace BaseProject.Models
{
    public class MovieTime : BaseModel
    {
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        public Hall Hall { get; set; }
        public Movie Movie { get; set; }
    }
}
