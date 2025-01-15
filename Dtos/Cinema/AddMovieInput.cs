using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Cinema
{
    public class AddMovieInput
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [EnumDataType(typeof(MovieType))]
        public MovieType Type { get; set; }
        [EnumDataType(typeof(MovieStatus))]
        public MovieStatus Status { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Range(1, int.MaxValue)]
        public int DurationInMinutes { get; set; }
    }
}
