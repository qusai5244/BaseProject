using BaseProject.Dtos.Todo;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface ITodoService
    {
        Task<ServiceResponse> AddNewTodoAsync(AddNewTodoInput input);
        Task<ServiceResponse> DeleteTodoAsync(int id);
        Task<ServiceResponse<Pagination<GetTodosListOutput>>> GetTodosList(GetTodosListInput input);
        Task<ServiceResponse> UpdateTodoAsync(int id, UpdateTodoInput input);
    }
}
