using BaseProject.Dtos.Car;
using BaseProject.Dtos.Product;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductSerfaces productService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly IProductSerfaces _productSerfaces = productService;

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductInputDto input)
        {
            return GetServiceResponse(await _productSerfaces.AddProductAsync(input));
        }
    }
}
