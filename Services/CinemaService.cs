using BaseProject.Data;
using BaseProject.Dtos.Cinema;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;

namespace BaseProject.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public CinemaService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> AddCinemaAsync(AddCinemaInputDto input)
        {
            try
            {
                var cinema = new Cinema
                {
                    Name = input.Name,
                    Location = input.Location,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dataContext.Cinemas.AddAsync(cinema);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Cinema");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddCinemaAsync");
            }
        }

    }
}
