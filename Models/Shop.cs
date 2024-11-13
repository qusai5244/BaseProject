using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProject.Models
{

    public class Shop
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        public string shop_name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string shop_description { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string shop_location { get; set; } = string.Empty;

        public List<ShopItem> shopItems { get; set; } = new();


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }



}
