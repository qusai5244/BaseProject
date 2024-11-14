using BaseProject.Data;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.Shop;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;

namespace BaseProject.Services
{
    public class ShopService : IShopService
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;
        public ShopService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;


        }

        public async Task<ServiceResponse> AddShopAsync(AddShopInputDto input)
        {
            try
            {
             var shop = new Shop
                {
                    Name = input.Name,
                    Description = input.Description,
                    Type = input.Type,
                    place = input.place,
                    CreatedAt = DateTime.UtcNow,
                };
                await _dataContext.Shops.AddAsync(shop);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Shop");
            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddShopAsync");
            }
        }
    }
    
}
