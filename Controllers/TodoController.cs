using BaseProject.Dtos.Car;
using BaseProject.Dtos.Todo;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    public class TodoController(ITodoService todoService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        private readonly ITodoService _todoService = todoService;

        [HttpPost]
        public async Task<IActionResult> AddNewTodoAsync([FromBody] AddNewTodoInput input)
        {
            if (!ModelState.IsValid) return InvaidInput();
            return GetServiceResponse(await _todoService.AddNewTodoAsync(input));
        }


        [HttpGet]
        public async Task<IActionResult> GetTodosList([FromQuery] GetTodosListInput input)
        {
            return GetServiceResponse(await _todoService.GetTodosList(input));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodosList([FromRoute]int id, [FromBody] UpdateTodoInput input)
        {
            return GetServiceResponse(await _todoService.UpdateTodoAsync(id ,input));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoAsync([FromRoute] int id)
        {
            return GetServiceResponse(await _todoService.DeleteTodoAsync(id));
        }

    }
}
