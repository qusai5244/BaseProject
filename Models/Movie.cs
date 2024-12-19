using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Movie : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public MovieType Type { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }

        // Foreign Key to CinemaHall
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; }
    }
    
    public enum MovieType
    {
        Action = 1,
        Comedy = 2,
        Drama =3
    }
}
