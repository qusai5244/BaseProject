using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Cinema
{
    public class AddHallInput
    {
        [Required]
        public string Name { get; set; }
        [Range(1,100)]
        public int SeatingCapacity { get; set; }
    }
}
