using BaseProject.Dtos;
using BaseProject.Dtos.Shop;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : BaseController
    {
        private readonly IShopServices _shopServices;

        public ShopController(IShopServices shopServices, IMessageHandler messageHandler) : base(messageHandler)
        {
            _shopServices = shopServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddNewShopDto input)
        {
            return GetServiceResponse(await _shopServices.AddShopAsync(input));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _shopServices.GetShopListAsync(input));
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> Get(int shopId)
        {
            return GetServiceResponse(await _shopServices.GetShopAsync(shopId));
        }

        [HttpPut("{shopId}")]
        public async Task<IActionResult> Put(int shopId, [FromBody] UpdateShopDto input)
        {
            return GetServiceResponse(await _shopServices.UpdateShopAsync(shopId, input));
        }
    }
}
