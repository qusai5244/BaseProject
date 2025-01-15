using BaseProject.Dtos.Cinema;
using BaseProject.Dtos.Movies;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<ServiceResponse> AddCinemaAsync(AddCinemaInput input);
        Task<ServiceResponse> AddCinemaHallAsync(int cinemaId, AddHallInput input);
        Task<ServiceResponse> AddMovieAsync(int cinemaId, AddMovieInput input);
        Task<ServiceResponse> AssignMovieToHallAsync(AssignMovieToHallInput input);
        Task<ServiceResponse<Pagination<GetMoviesOutput>>> GetMoviesAsync(int cinemaId, GetMoviesInput input);
        Task<ServiceResponse<GetMoviesTimeOutput>> GetMoviesTimeAsync(GetMoviesTimeInput input);
    }
}
