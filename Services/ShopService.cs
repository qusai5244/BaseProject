using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Car;
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
                    shop_name = input.shop_name,
                    shop_description = input.shop_description,
                    shop_location = input.shop_location,
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





       public async  Task<ServiceResponse<ShopOutputDto>> GetShopAsync(int shopId)
        {
            try
            {
                var shop = await _dataContext
                               .Shops
                               .AsNoTracking()
                          .Select(s => new ShopOutputDto
                          {
                              Id = s.id,
                              shop_name = s.shop_name,
                              shop_description = s.shop_description,
                              shop_location = s.shop_location,
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

                var query =  _dataContext.Shops.AsNoTracking();


                var totalItems = await _dataContext.Shops.CountAsync();

                var shops = await query
                    .Skip(input.PageSize * (input.Page - 1))
                     .Take(input.PageSize)
                     .Select(x =>  new ShopListOutputDto
                     {
                         shop_name = x.shop_name,
                         shop_description = x.shop_description,
                         shop_location = x.shop_location,
                     }).ToListAsync();

                var paginationList = new Pagination<ShopListOutputDto>(shops, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);

            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<Pagination<ShopListOutputDto>>(ErrorMessage.ServerInternalError, null, "AddShopAsync");


            }



        }

        public async Task<ServiceResponse> UpdateShopAsync(int shopId, UpdateShopDto input)
        {
            try
            {
                var shop = await _dataContext.Shops.FirstOrDefaultAsync(x => x.id == shopId);

                if (shop == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Shop");
                }

                   shop.shop_name = input.shop_name;
                   shop.shop_description = input.shop_description;
                   shop.shop_location = input.shop_location;
                   shop.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();
                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Shop");


            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "Shop");

            }
        }
    }
}
