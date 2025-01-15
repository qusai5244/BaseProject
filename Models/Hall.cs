namespace BaseProject.Models
{
    public class Hall : BaseModel
    {
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
        public int CinemaId { get; set; }

        // Navigation properties
        public Cinema Cinema { get; set; }
        public ICollection<MovieTime> MovieTimes { get; set; }
    }
}
