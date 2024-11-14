using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Shop;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class ShopService : IShopServices
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public ShopService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> AddShopAsync(AddNewShopDto input)
        {
            try
            {
                var shop = new Shop
                {
                    Name = input.Name,
                    Location = input.Location,
                    Size = input.Size,
                    Price = input.Price,
                    ShopType = (Models.ShopType)input.ShopType
              ,
                    CreatedAt = DateTime.UtcNow
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

        public async Task<ServiceResponse<ShopOutputDto>> GetShopAsync(int shopId)
        {
            try
            {
                var shop = await _dataContext
                    .Shops
                    .AsNoTracking()
                    .Select(r => new ShopOutputDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Location = r.Location,
                        Size = r.Size,
                        Price = r.Price,
                        
                       
                        CreatedAt = r.CreatedAt,
                        UpdatedAt = r.UpdatedAt
                    })
                    .FirstOrDefaultAsync(x => x.Id == shopId);

                if (shop == null)
                {
                    return _messageHandler.GetServiceResponse<ShopOutputDto>(ErrorMessage.NotFound, null, "Shop");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, shop);
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<ShopOutputDto>(ErrorMessage.ServerInternalError, null, "GetShopAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<ShopListOutputDto>>> GetShopListAsync(GlobalFilterDto input)
        {
            try
            {
                var query = _dataContext.Shops.AsNoTracking();

                var totalItems = await query.CountAsync();

                var shops = await query
                    .Skip(input.PageSize * (input.Page - 1))
                    .Take(input.PageSize)
                    .Select(x => new ShopListOutputDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Location = x.Location,
                        Size = x.Size,
                        Price = x.Price,
                       
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                    .ToListAsync();

                var paginationList = new Pagination<ShopListOutputDto>(shops, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<Pagination<ShopListOutputDto>>(ErrorMessage.ServerInternalError, null, "GetShopListAsync");
            }
        }

        public async Task<ServiceResponse> UpdateShopAsync(int shopId, UpdateShopDto input)
        {
            try
            {
                var shop = await _dataContext.Shops.FirstOrDefaultAsync(x => x.Id == shopId);

                if (shop == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Shop");
                }

                shop.Name = input.Name;
                shop.Location = input.Location;
                shop.Size = input.Size;
                shop.Price = input.Price;
               
                shop.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Shop");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateShopAsync");
            }
        }
    }
}
