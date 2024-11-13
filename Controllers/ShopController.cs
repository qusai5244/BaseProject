using BaseProject.Dtos;
using BaseProject.Dtos.Shop;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController(IShopServices shopServices, IMessageHandler messageHandler) : BaseController(messageHandler)
    {

        private readonly IShopServices _shopServices = shopServices;

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] AddNewShopDto input)
        {
             return GetServiceResponse(await _shopServices.AddShopAsync(input));
        }













        [HttpGet("shopId")]

        public async Task<IActionResult> Get(int shopId)
        {
            return GetServiceResponse(await _shopServices.GetShopAsync(shopId));
        }




        [HttpGet]

        public async Task<IActionResult> Get([FromQuery] GlobalFilterDto input)
        {
            return GetServiceResponse(await _shopServices.GetShopListAsync(input));
        }


        [HttpPut("shopId")]
        public async Task<IActionResult> Put([FromBody] UpdateShopDto input , int shopId)
        {
            return GetServiceResponse(await _shopServices.UpdateShopAsync(shopId,input));
        }



    }
}
