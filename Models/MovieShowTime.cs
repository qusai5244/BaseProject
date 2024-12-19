using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProject.Models
{
    public class MovieShowTime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        public Movies Movies { get; set; }
        [Required]
        [ForeignKey("Hall")]
        public int HallId { get; set; }

        public Hall Hall { get; set; }
        [Required]
        public int AvailableTickets { get; set; }
        [Required]
        public decimal price { get; set; }

    }
}
