using BaseProject.Dtos.Product;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productServices, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly IProductService _productServices = productServices;

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductInputDto input)
        {
            return GetServiceResponse(await _productServices.AddProductAsync(input));
        }
    }
}
