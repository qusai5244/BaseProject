using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Cinema : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }

        // Relationship between Cinema and CinemaHalls (One-to-Many)
        public ICollection<CinemaHall> CinemaHalls { get; set; }

    }
}
