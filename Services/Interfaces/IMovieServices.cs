using BaseProject.Dtos;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.Ciname;
using BaseProject.Dtos.Hall;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieServices
    {

        Task<ServiceResponse> AddMovieAsync(AddNewMovieDto input);

        Task<ServiceResponse<getMovieDto>> GetMovieAsync(int mid);


        Task<ServiceResponse<Pagination<getMovieListDto>>> GetMoveAvailableAsync(GlobalFilterDto globalFilter);


        Task<ServiceResponse<Pagination<getMovieListDto>>> GetMoveListAsync(GlobalFilterDto globalFilter);

        Task<ServiceResponse> UpdateMovieAsync(int mid, updateMovieDto input);


        Task<ServiceResponse> DeleteMovieAsync(int mid);





    }
}
