using BaseProject.Dtos;
using BaseProject.Dtos.Car;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface ICarServices
    {
        Task<ServiceResponse> AddCarAsync(AddNewCardDto input);
        Task<ServiceResponse<CarOutputDto>> GetCarAsync(int carId);
        Task<ServiceResponse<Pagination<CarListOutputDto>>> GetCarListAsync(GlobalFilterDto input);
        Task<ServiceResponse> UpdateCarAsync(int carId, UpdateCardDto input);
    }
}

