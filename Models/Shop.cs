using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ShopType Type { get; set; }

        [Required]
        public string place { get; set; }




        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }


    public enum ShopType
    {
        BookShop = 1,
        ClothesShop = 2,
        ToysShop = 3,
        FoodShop= 4
    }
}
