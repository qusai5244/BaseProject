using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Movie
{
    public class AddNewMovie
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public MovieType Type { get; set; } 

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 500)]
        public int Duration { get; set; }

        
    }
}
