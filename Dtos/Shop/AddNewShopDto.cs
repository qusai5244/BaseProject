using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Shop
{
    public class AddNewShopDto
    {

        [Required]
        [MaxLength(50)]
        public string shop_name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string shop_description { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string shop_location { get; set; } = string.Empty;



    }
}
