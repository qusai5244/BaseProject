using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.Product;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class ProductService : IProductServices
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

        public async Task<ServiceResponse<Pagination<GetProductOutputDto>>> GetProductListAsync(GlobalFilterDto input)
        {
            try
            {
                
                var query = _dataContext.Products.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(p => p.Name.Contains(input.Search));
                }

                // تطبيق التصفية والصفحة
                var totalItems = await query.CountAsync();
                var products = await query
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(p => new GetProductOutputDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Type = p.Type,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt
                    })
                    .ToListAsync();

                // التحقق من وجود نتائج
                if (!products.Any())
                {
                    return _messageHandler.GetServiceResponse<Pagination<GetProductOutputDto>>(ErrorMessage.NotFound, null, "product");
                }

                // إنشاء بيانات التصفية
                var pagination = new Pagination<GetProductOutputDto>(products, query.Count(), input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, pagination, "Products");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, "An error occurred while executing GetAllProductsAsync");
                return _messageHandler.GetServiceResponse<Pagination<GetProductOutputDto>>(ErrorMessage.ServerInternalError, null, "GetAllProductsAsync");
            }
        }

        public async Task<ServiceResponse<GetProductOutputDto>> GetProductAsync(int productId)
        {
            try
            {
                var product = await _dataContext
                                .Products
                                .AsNoTracking()
                                .Select(r => new GetProductOutputDto
                                {
                                    Id = r.Id,
                                    Name = r.Name,
                                    Price = r.Price,
                                    Type = r.Type,
                                    CreatedAt = r.CreatedAt,
                                    UpdatedAt = r.UpdatedAt
                                })
                                .FirstOrDefaultAsync(r => r.Id == productId);

                if (product == null)
                {
                    return _messageHandler.GetServiceResponse<GetProductOutputDto>(ErrorMessage.NotFound, null, "Product");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, product);
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<GetProductOutputDto>(ErrorMessage.ServerInternalError, null, "GetProductAsync");
            }
        }

        public async Task<ServiceResponse> UpdateProductAsync(int productId, UpdateProductInputDto input)
        {
            try
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

                if (product == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "product");
                }

                // Update product properties
                product.Name = input.Name;
                product.Price = input.Price;
                product.Type = input.Type;
                product.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Product");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateProductAsync");
            }
        }

        public async Task<ServiceResponse> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _dataContext.Products.FindAsync(productId);

                if (product == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "product");
                }

                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Deleted, "Product");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UDeleteProductAsync");
            }

        }

    }
}
