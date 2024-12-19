

using BaseProject.Dtos.Car;
using BaseProject.Dtos.Cinema;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieService
    {

    
        Task<ServiceResponse> AddNewMovieAsync(AddNewMovie input);

        Task<ServiceResponse<Pagination<GetMovieListOutput>>> GetMovieList(GetMovieListInput input);

        Task<ServiceResponse<object>> AddCinemaAsync(CinemaDto cinemaDto);
        Task<ServiceResponse<object>> AddHallToCinemaAsync(int cinemaId, HallDto hallDto);
        Task<ServiceResponse> UpdateMovieAsync(int id, UpdateMovieInput input);

        Task<ServiceResponse<object>> DeleteMovieAsync(int id);
        Task<ServiceResponse<object>> ScheduleMovieAsync(MovieScheduleDto scheduleDto);
        
    }


}
