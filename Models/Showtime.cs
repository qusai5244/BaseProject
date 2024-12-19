using System;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models;

public class Showtime: BaseModel
{
    [Key]
    public int ShowtimeID { get; set; }

    [Required]
    public int MovieID { get; set; } // Foreign Key

    [Required]
    public int HallID { get; set; } // Foreign Key

    [Required]
    public DateTime ShowDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

}
