using BaseProject.Dtos.Ciname;
using BaseProject.Dtos;
using BaseProject.Dtos.Hall;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IHallServices
    {
        Task<ServiceResponse> AddHallAsync(AddNewHallDto input);



        Task<ServiceResponse<gethallDto>> GetHallAsync(int hid);

        Task<ServiceResponse<Pagination<gethallListDto>>> GetHallListAsync(GlobalFilterDto globalFilter);



    }
}
