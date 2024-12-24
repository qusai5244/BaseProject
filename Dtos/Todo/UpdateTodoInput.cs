using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Todo
{
    public class UpdateTodoInput
    {
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        [EnumDataType(typeof(TodoPriorityLevel))]
        public TodoPriorityLevel PriorityLevel { get; set; }

        [Required]
        [EnumDataType(typeof(TodoStatus))]
        public TodoStatus Status { get; set; }
    }
}
