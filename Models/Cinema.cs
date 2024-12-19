using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string CName { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Clocation { get; set; } = string.Empty;
        [Required]
        [MaxLength(8)]
        [RegularExpression(@"^[972]\d{7}$", ErrorMessage = "رقم الهاتف يجب أن يبدأ بـ 9 أو 7 أو 2 ويتكون من 8 أرقام.")]
        public string Cphone { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string CEmail { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string BulidingName { get; set; } = string.Empty;

        public bool isDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
