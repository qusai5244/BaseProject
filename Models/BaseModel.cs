namespace BaseProject.Models
{
    public class BaseModel
    {
        public int Id { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now; 
        public DateTime? UpdatedDate { get; set; }  

   
        public bool IsDeleted { get; set; } = false;  
    }
}

