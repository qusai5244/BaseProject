using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Shop
{
    public class ShopItemDto
    {

        public int Id { get; set; }
        public string PName { get; set; } = string.Empty;
        public long Pprice { get; set; }

    }
}
