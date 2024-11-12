using BaseProject.Data;
using BaseProject.Dtos.Product;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;

namespace BaseProject.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;
        public ProductService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> AddProductAsync(AddProductInputDto input)
        {
            try
            {
                var product = new Product
                {
                    Name = input.Name,
                    Price = input.Price,
                    Type = input.Type,
                    CreatedAt = DateTime.UtcNow,
                };

                await _dataContext.Products.AddAsync(product);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Product");
            }
            catch (Exception ex)
            {
                // will add logs later
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddProductAsync");
            }
        }
    }
}
