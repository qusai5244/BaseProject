using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models;

public class Cinema: BaseModel
{
    [Key]
    public int CinemaID { get; set; }

    [Required]
    [MaxLength(255)]
    public required string  Name { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Location { get; set; }
}
