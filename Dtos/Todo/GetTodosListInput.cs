using BaseProject.Models;

namespace BaseProject.Dtos.Todo
{
    public class GetTodosListInput : GlobalFilterDto
    {
        public TodoSortByTypes? SortBy { get; set; }
        public TodoStatus? Status { get; set; }
    }

    public enum TodoSortByTypes
    {
        Priority = 1 ,
        DueDate
    }
}
