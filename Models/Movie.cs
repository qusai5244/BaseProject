using System.ComponentModel.DataAnnotations;
namespace BaseProject.Models
{
    public class Movie : BaseModel
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

     
        [EnumDataType(typeof(MovieType))]
        public MovieType Type { get; set; }

       
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        [Range(1, 500)]
        public int Duration { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<MovieSchedule> MovieSchedules { get; set; }
    }

    public enum MovieType
    {
        Action = 1,
        Comedy = 2,
        Drama = 3
    }
}
