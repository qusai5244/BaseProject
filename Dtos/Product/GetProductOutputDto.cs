using BaseProject.Models;

namespace BaseProject.Dtos.Product
{
    public class GetProductOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
