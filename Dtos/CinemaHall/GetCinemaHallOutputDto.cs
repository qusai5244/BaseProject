namespace BaseProject.Dtos.CinemaHall
{
    public class GetCinemaHallOutputDto
    {
        public int Id { get; set; }
        public string HallName { get; set; }
        public int SeatingCapacity { get; set; }
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string Location { get; set; }
    }
}
