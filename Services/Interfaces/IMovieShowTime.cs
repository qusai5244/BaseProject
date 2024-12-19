using BaseProject.Dtos.Movie;
using BaseProject.Dtos.MovieShowTime;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieShowTime
    {
        Task<ServiceResponse> AddMovieAsync(AddNewMovieShowTimeDto input);

    }
}
