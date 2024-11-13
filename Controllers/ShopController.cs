using BaseProject.Dtos.Product;
using BaseProject.Dtos.Shop;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController(IShopServises shopServises, IMessageHandler messageHandler) : BaseController(messageHandler)
    {
        public readonly IShopServises _shopServises = shopServises;

        [HttpPost]
        public async Task<IActionResult> AddShopAsync([FromBody] AddShopInputDto input)
        {
            return GetServiceResponse(await _shopServises.AddShopAsync(input));
        }

    }
}
