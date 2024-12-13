using BaseProject.Dtos;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.Product;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ServiceResponse> AddProductAsync(AddProductInputDto input);

        //Get list of product
        Task<ServiceResponse<Pagination<GetProductOutputDto>>> GetProductListAsync(GlobalFilterDto input);

        //Get product by id
        Task<ServiceResponse<GetProductOutputDto>> GetProductAsync(int productId);

        //Update
        Task<ServiceResponse> UpdateProductAsync(int productid, UpdateProductInputDto input);

        //Delete
        Task<ServiceResponse> DeleteProductAsync(int productid);
    }
}
