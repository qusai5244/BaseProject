using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Hall
{
    public class AddNewHallDto
    {

        [Required]
        [MaxLength(50)]
        public string HName { get; set; } = string.Empty;
        [Required]
        public int capcity { get; set; }
        [Required]
        public int cinema_id { get; set; }

    }
}
