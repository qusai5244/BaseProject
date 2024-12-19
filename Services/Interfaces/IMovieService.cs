

using BaseProject.Dtos.Car;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ServiceResponse> AddNewMovieAsync(AddNewMovie input);

        Task<ServiceResponse<Pagination<GetMovieListOutput>>> GetMovieList(GetMovieListInput input);


        Task<ServiceResponse> UpdateMovieAsync(int id, UpdateMovieInput input);

        Task<ServiceResponse<object>> DeleteMovieAsync(int id);





    }


}
