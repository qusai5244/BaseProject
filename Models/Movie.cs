using System;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models;

public class Movie :BaseModel
{
    [Key]
    public int MovieID { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(1000)]
    public required string Description { get; set; }

    [Required]
    [EnumDataType(typeof(MovieType))]
    public MovieType Type { get; set; } // Use the MovieType enum

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [Range(1, 500)]
    public int Duration { get; set; } // In minutes
    
    public enum MovieType
    {
        Action = 1,
        Comedy = 2,
        Drama = 3,
        Horror = 4,
        SciFi = 5,
        Romance = 6,
        Documentary = 7,
        Fantasy = 8
    }


}
