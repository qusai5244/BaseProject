using BaseProject.Dtos;
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
    public class ProductController(IProductServices productService, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly IProductServices _productServices = productService;

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProductInputDto input)
        {
            return GetServiceResponse(await _productServices.AddProductAsync(input));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _productServices.GetProductListAsync(input));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int productId)
        {
            return GetServiceResponse(await _productServices.GetProductAsync(productId));
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Put(int productId, [FromBody] UpdateProductInputDto input)
        {
            return GetServiceResponse(await _productServices.UpdateProductAsync(productId, input));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            return GetServiceResponse(await _productServices.DeleteProductAsync(productId));
        }
    }
}
