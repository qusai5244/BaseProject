using BaseProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.MovieShowTime
{
    public class AddNewMovieShowTimeDto
    {
        public int MovieId { get; set; }
        [Required]
        [ForeignKey("Hall")]
        public int HallId { get; set; }

        [Required]
        public int AvailableTickets { get; set; }
        [Required]
        public decimal price { get; set; }
    }
}
