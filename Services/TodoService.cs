using BaseProject.Data;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.Todo;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class TodoService(DataContext dataContext, IMessageHandler messageHandler) : ITodoService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IMessageHandler _messageHandler = messageHandler;

        public async Task<ServiceResponse> AddNewTodoAsync(AddNewTodoInput input)
        {
            try
            {
                var todo = new Todo
                {
                    Title = input.Title,
                    Description = input.Description,
                    DueDate = input.DueDate,
                    PriorityLevel = input.PriorityLevel,
                    Status = TodoStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                await _dataContext.Todos.AddAsync(todo);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Todo");

            }
            catch (Exception ex)
            {

                throw;
            }
        } 

        public async Task<ServiceResponse<Pagination<GetTodosListOutput>>> GetTodosList(GetTodosListInput input)
        {
            try
            {
                var query = _dataContext.Todos.Where(x => !x.IsDeleted).OrderByDescending(x => x.Id).AsQueryable();

                if(input.SortBy.GetValueOrDefault() > 0)
                {
                    if(input.SortBy == TodoSortByTypes.Priority)
                    {
                        query = query.OrderByDescending(x => x.PriorityLevel);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.DueDate);
                    }
                }

                if (input.Status.GetValueOrDefault() > 0)
                {
                    query = query.Where(x => x.Status == input.Status);
                }

                var totalCount = await query.CountAsync();

                var data = await query
                                 .Skip(input.PageSize * (input.Page - 1))
                                 .Take(input.PageSize)
                                 .Select(x => new GetTodosListOutput
                                 {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Description = x.Description,
                                    PriorityLevel = x.PriorityLevel.ToString(),
                                    Status = x.Status.ToString(),
                                    DueDate = x.DueDate,
                                    CreatedAt = x.CreatedAt,
                                    UpdatedAt = x.UpdatedAt,
                                 })
                                 .ToListAsync();

                var paginationList = new Pagination<GetTodosListOutput>(data, totalCount, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ServiceResponse> UpdateTodoAsync(int id,UpdateTodoInput input)
        {
            try
            {
                var todo = await _dataContext
                                 .Todos
                                 .Where(x => x.Id == id && !x.IsDeleted)
                                 .FirstOrDefaultAsync();

                if(todo == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Todo");
                }

                todo.Title = input.Title;
                todo.Description = input.Description;
                todo.PriorityLevel = input.PriorityLevel;
                todo.Status = input.Status;
                todo.DueDate = input.DueDate;

                todo.UpdatedAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Todo");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ServiceResponse> DeleteTodoAsync(int id)
        {
            try
            {
                var todo = await _dataContext
                                 .Todos
                                 .Where(x => x.Id == id && !x.IsDeleted)
                                 .FirstOrDefaultAsync();

                if (todo == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Todo");
                }

                todo.IsDeleted = true;
                todo.UpdatedAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Deleted, "Todo");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
