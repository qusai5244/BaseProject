using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class Todo : BaseModel
    {
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TodoPriorityLevel PriorityLevel { get; set; }
        public TodoStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum TodoPriorityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public enum TodoStatus
    {
        Pending = 1,
        InProcess = 2,
        Completed = 3,
    }
}
