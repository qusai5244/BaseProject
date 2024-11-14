using System;

namespace BaseProject.Dtos.Shop
{
    public class ShopOutputDto
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

   
}
