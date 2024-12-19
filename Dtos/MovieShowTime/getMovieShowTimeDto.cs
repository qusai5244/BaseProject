using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.MovieShowTime
{
    public class getMovieShowTimeDto
    {

        public int MovieId { get; set; }
   
        public int HallId { get; set; }

        public int AvailableTickets { get; set; }
        public decimal price { get; set; }
        public string MName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;



    }
}
