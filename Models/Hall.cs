using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Hall : BaseModel

    {
        [Required]
        [StringLength(500)]
        public string HallName { get; set; }

        [Required]
        [Range(1, 500)]
        public int SeatingCapacity { get; set; }

        [Required]
        [Range(1, 500)]
        public int CinemaId { get; set; }

        
        public Cinema Cinema { get; set; }
    }
}
