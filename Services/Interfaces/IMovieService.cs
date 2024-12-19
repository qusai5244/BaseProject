

using BaseProject.Dtos.Movie;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ServiceResponse> AddNewMovieAsync(AddNewMovie input);
    }
}
