using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.CinemaHall;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ServiceResponse<Pagination<GetCinemaHallOutputDto>>> GetCinemaHallListAsync(GlobalFilterDto input)
        {
            try
            {
                // Query for all cinema halls
                var query = _dataContext.CinemaHalls.AsNoTracking();

                // Search filter for hall name or cinema name
                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(ch => ch.HallName.Contains(input.Search));
                }

                // Get total count of items for pagination
                var totalItems = await query.CountAsync();

                // Apply pagination
                var cinemaHalls = await query
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(ch => new GetCinemaHallOutputDto
                    {
                        Id = ch.Id,
                        HallName = ch.HallName,
                        SeatingCapacity = ch.SeatingCapacity,
                        CinemaId = ch.CinemaId,
                        CinemaName = ch.Cinema.Name,
                        Location = ch.Cinema.Location
                    })
                    .ToListAsync();

                // Check if cinema halls are found
                if (!cinemaHalls.Any())
                {
                    return _messageHandler.GetServiceResponse<Pagination<GetCinemaHallOutputDto>>(ErrorMessage.NotFound, null, "cinema halls");
                }

                // Create pagination result
                var pagination = new Pagination<GetCinemaHallOutputDto>(cinemaHalls, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, pagination, "Cinema Halls");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<Pagination<GetCinemaHallOutputDto>>(ErrorMessage.ServerInternalError, null, "GetCinemaHallListAsync");
            }
        }
    }
}
