using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public ShopType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }

    public enum ShopType
    {
        Food = 1,
        Electronics = 2,
        Clothing = 3,
        Books = 4
    }
}
