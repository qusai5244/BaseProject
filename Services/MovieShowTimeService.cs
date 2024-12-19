using BaseProject.Data;
using BaseProject.Dtos.MovieShowTime;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class MovieShowTimeService : IMovieShowTime
    {

        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public MovieShowTimeService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }
        public async Task<ServiceResponse> AddMovieAsync(AddNewMovieShowTimeDto input)
        {
            try
            {

                var checkCapcity = await _dataContext.Halls.Where(h => h.Id == input.HallId && h.capcity >= input.AvailableTickets).FirstOrDefaultAsync();
                if (checkCapcity  == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.AlreadyExists, "Ticket is more than hall capcity");
                }


                var movieShow = new MovieShowTime
                {
                    HallId = input.HallId,
                    MovieId = input.MovieId,
                    price = input.price,
                    AvailableTickets = input.AvailableTickets,

                };

                await _dataContext.MoviesShowTime.AddAsync(movieShow);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "AddMovie");


            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddMovie");

            }
        }
    }
}
