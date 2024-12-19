using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class MovieSchedule : BaseModel
    {
       
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public DateTime ShowTime { get; set; }

        [Required]
        [Range(1, 500)]
        public int AvailableSeats { get; set; }
    }
}
