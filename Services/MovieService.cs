using BaseProject.Data;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;

namespace BaseProject.Services
{
    public class MovieService (DataContext dataContext, IMessageHandler messageHandler) : IMovieService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IMessageHandler _messageHandler = messageHandler;

        public async Task<ServiceResponse> AddNewMovieAsync (AddNewMovie input)
        {
            try
            {
                var movie = new Movie
                {
            Title = input.Title,
            Description = input.Description,
            Type = input.Type,
            ReleaseDate = input.ReleaseDate,
            Duration = input.Duration,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

                };

                await _dataContext.Movies.AddAsync(movie);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Movie");

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
