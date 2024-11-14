using BaseProject.Dtos;
using BaseProject.Dtos.Shop;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IShopServices
    {
        Task<ServiceResponse> AddShopAsync(AddNewShopDto input);
        Task<ServiceResponse<ShopOutputDto>> GetShopAsync(int shopId);
        Task<ServiceResponse<Pagination<ShopListOutputDto>>> GetShopListAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateShopAsync(int shopId, UpdateShopDto input);
    }
}

