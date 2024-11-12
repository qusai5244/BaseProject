using BaseProject.Dtos.Product;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> AddProductAsync(AddProductInputDto input);
    }
}
