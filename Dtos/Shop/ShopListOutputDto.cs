using System;

namespace BaseProject.Dtos.Shop
{
    public class ShopListOutputDto
    {
        public int Id { get; set; }  
        public string Name { get; set; }  
        public string Location { get; set; } 
        public int Size { get; set; }  
        public int Price { get; set; }  
        public ShopType ShopType { get; set; }  
        public OperatingStatus Status { get; set; }  
        public DateTime CreatedAt { get; set; } 
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
        TemporarilyClosed = 4,
    }
}
