using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class CinemaHall : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string HallName { get; set; }
        [Required]
        public int SeatingCapacity { get; set; }

        // Foreign Key to Cinema
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        // Relationship between CinemaHall and Movies (One-to-Many)
        public ICollection<Movie> Movies { get; set; }
    }
}
