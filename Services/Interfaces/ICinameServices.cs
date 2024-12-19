using BaseProject.Dtos;
using BaseProject.Dtos.Ciname;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface ICinameServices
    {

        
        Task<ServiceResponse> AddCinameAsync(AddNewCinameDto input);

        Task<ServiceResponse<getCinemaDto>> GetCinemaAsync(int cnid);

        Task<ServiceResponse<Pagination<getCinemaDto>>> GetCinemaListAsync(GlobalFilterDto globalFilter);



    }
}
