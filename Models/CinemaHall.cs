using System;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models;

public class CinemaHall: BaseModel
{
    [Key]
    public int HallID { get; set; }

    [Required]
    public int CinemaID { get; set; } // Foreign Key

    [Required]
    [MaxLength(50)]
    public required string HallName { get; set; }

    [Required]
    [Range(1, 1000)]
    public int SeatingCapacity { get; set; }
}
