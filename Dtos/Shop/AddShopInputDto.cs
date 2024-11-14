using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Shop
{
    public class AddShopInputDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ShopType Type { get; set; }

        [Required]
        public string place { get; set; }
    }
}
