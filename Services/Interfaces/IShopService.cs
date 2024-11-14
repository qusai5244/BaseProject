using BaseProject.Dtos.Car;
using BaseProject.Dtos;
using BaseProject.Helpers;
using BaseProject.Dtos.Shop;

namespace BaseProject.Services.Interfaces
{
    public interface IShopService
    {
        Task<ServiceResponse> AddShopAsync(AddShopInputDto input);
    }
}
