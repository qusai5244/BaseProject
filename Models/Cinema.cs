namespace BaseProject.Models
{
    public class Cinema: BaseModel

    {
        public string Name { get; set; }
        public string Location { get; set; }

        public List<CinemaHall> CinemaHalls { get; set; } = new List<CinemaHall>();
    }
}
