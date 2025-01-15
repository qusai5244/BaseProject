namespace BaseProject.Models
{
    public class MovieTime : BaseModel
    {
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        // Navigation properties
        public Hall Hall { get; set; }
        public Movie Movie { get; set; }
    }
}
