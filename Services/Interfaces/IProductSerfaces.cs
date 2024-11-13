using BaseProject.Dtos.Car;
using BaseProject.Dtos.Product;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IProductSerfaces
    {
        Task<ServiceResponse> AddProductAsync(AddProductInputDto input);
    }
}
