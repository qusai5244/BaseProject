using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Ciname
{
    public class AddNewCinameDto
    {
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
        public string BulidingName { get; set; } = string.Empty;

    }
}
