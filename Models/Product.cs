using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
    public enum ProductType
    {
        Food = 1,
        Electronics = 2,
        Clothing = 3,
        Furniture = 4,
        Books = 5
    }
}
