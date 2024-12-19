using BaseProject.Data;
using BaseProject.Dtos.CinemaHall;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;

namespace BaseProject.Services
{
    public class CinemaHallService : ICinemaHallService
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public CinemaHallService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> AddCinemaHallAsync(AddCinemaHallInputDto input)
        {
            try
            {
                // Check if Cinema exists
                var cinema = await _dataContext.Cinemas.FindAsync(input.CinemaId);
                if (cinema == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Cinema");
                }

                // Create CinemaHall
                var cinemaHall = new CinemaHall
                {
                    HallName = input.HallName,
                    SeatingCapacity = input.SeatingCapacity,
                    CinemaId = input.CinemaId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dataContext.CinemaHalls.AddAsync(cinemaHall);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "CinemaHall");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddCinemaHallAsync");
            }
        }
    }
}
