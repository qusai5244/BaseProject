using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProject.Models
{
    public class ShopItem
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string PName { get; set; }= string.Empty;

        [Required]
        public long Pprice { get; set; }

        public int shopId { get; set; }
        public Shop Shop { get; set; }



    }
}
