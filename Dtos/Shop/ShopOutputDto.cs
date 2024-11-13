using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Shop
{
    public class ShopOutputDto
    {
        public int Id { get; set; }

        public string shop_name { get; set; } = string.Empty;

        public string shop_description { get; set; } = string.Empty;

        public string shop_location { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
