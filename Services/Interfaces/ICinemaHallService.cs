using BaseProject.Dtos.CinemaHall;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface ICinemaHallService
    {
        Task<ServiceResponse> AddCinemaHallAsync(AddCinemaHallInputDto input);
    }
}
