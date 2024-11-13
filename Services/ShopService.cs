using BaseProject.Data;
using BaseProject.Dtos.Product;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Helpers;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using BaseProject.Dtos.Shop;

namespace BaseProject.Services
{
    public class ShopService : IShopServises
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
                    Adress = input.Adress,
                    Type = input.Type,
                    CreatedAt = DateTime.UtcNow,
                };

                await _dataContext.Shops.AddAsync(shop);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Shop");
            }
            catch (Exception ex)
            {
                // will add logs later
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddShopAsync");
            }
        }
    }
}
