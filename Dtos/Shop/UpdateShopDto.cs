using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Shop
{
    public class UpdateShopDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Location { get; set; }

        [Range(0, 100000)]
        public int Size { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public ShopType ShopType { get; set; }

        [Required]
        public OperatingStatus Status { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
