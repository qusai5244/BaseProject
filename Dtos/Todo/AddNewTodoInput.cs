using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Todo
{
    public class AddNewTodoInput
    {
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        [EnumDataType(typeof(TodoPriorityLevel))]
        public TodoPriorityLevel PriorityLevel { get; set; }
    }
}
