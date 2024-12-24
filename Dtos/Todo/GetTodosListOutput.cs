using BaseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Dtos.Todo
{
    public class GetTodosListOutput
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string PriorityLevel { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
