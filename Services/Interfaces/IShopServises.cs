using BaseProject.Dtos.Product;
using BaseProject.Dtos.Shop;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IShopServises
    {
        Task<ServiceResponse> AddShopAsync(AddShopInputDto input);
    }
}
