namespace BaseProject.Models
{
    public class CinemaHall: BaseModel

    {
        public string HallName { get; set; }
        public int SeatingCapacity { get; set; }

        public int CinemaId { get; set; }  
        public Cinema Cinema { get; set; }
    }
}
