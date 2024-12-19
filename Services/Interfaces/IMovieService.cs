using BaseProject.Dtos;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ServiceResponse> AddMovieAsync(AddMovieInputDto input);
        Task<ServiceResponse<Pagination<GetMovieOutputDto>>> GetMovieListAsync(GlobalFilterDto input);
        Task<ServiceResponse<List<GetCinemasByMovieNameOutputDto>>> GetCinemasByMovieNameAsync(string movieName);
        Task<ServiceResponse> UpdateMovieAsync(int movieId, UpdateMovieInputDto input);
        Task<ServiceResponse> DeleteMovieAsync(int movieId);
    }
}
