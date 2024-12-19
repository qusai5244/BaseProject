using BaseProject.Helpers.MessageHandler;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.CinemaHall
{
    public class AddCinemaHallInputDto
    {
        public string HallName { get; set; }
        public int SeatingCapacity { get; set; }

        [Required]
        public int CinemaId { get; set; }
    }
}
