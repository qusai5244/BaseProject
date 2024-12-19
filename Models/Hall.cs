using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProject.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string HName { get; set; } = string.Empty;
        [Required]
        [Range(1, 99999, ErrorMessage = "Capcity must be between 1 and 99999")]
        public int capcity { get; set; }
        [Required]
        [ForeignKey("Cinema")]
        public int cinema_id { get; set; }

        public Cinema Cinema { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
