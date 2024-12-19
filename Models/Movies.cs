using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProject.Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string MName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
       
        [Required]

        public MType Mtype { get; set; }
        [Required]

        public DateTime release_date { get; set; }
        [Required]

        public int duration { get; set; }
        [Required]
        public string language { get; set; } = string.Empty;

        public bool isDeleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }

    public enum MType
    {
        Drama = 1,
        Action = 2,
        Romantic = 3,
        Comedy = 4,
        Cartoon = 5,
    }
}
