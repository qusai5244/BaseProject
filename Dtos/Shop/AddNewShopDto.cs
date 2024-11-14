using System;
using System.ComponentModel.DataAnnotations;

public class AddNewShopDto
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }  
    [Required]
    [MaxLength(500)]
    public string Location { get; set; }  

    [Range(0, 100000)]
    public int Size { get; set; }  

    [Required]
    [Range(0, 10000000)]  
    public int Price { get; set; }

    [Required]
    public ShopType ShopType { get; set; } 

    [Required]
    public OperatingStatus Status { get; set; }  

    public DateTime CreatedAt { get; set; } = DateTime.Now;  

    public DateTime? UpdatedAt { get; set; } 
}

public enum ShopType
{
    Retail = 1, 
    Online = 2,  
    Hybrid = 3, 
}

public enum OperatingStatus
{
    Open = 1,
    Closed = 2,
    UnderRenovation = 3,
    TemporarilyClosed = 4
}
