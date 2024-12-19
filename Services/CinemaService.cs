using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Cinema;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ServiceResponse<Pagination<GetCinemasOutputDto>>> GetCinemasListAsync(GlobalFilterDto input)
        {
            try
            {
                // Query for all cinemas
                var query = _dataContext.Cinemas.AsNoTracking();

                // Search filter for cinema name or location
                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(c => c.Name.Contains(input.Search) || c.Location.Contains(input.Search));
                }

                // Get total count of items for pagination
                var totalItems = await query.CountAsync();

                // Apply pagination
                var cinemas = await query
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(c => new GetCinemasOutputDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Location = c.Location,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    })
                    .ToListAsync();

                // Check cinemas
                if (!cinemas.Any())
                {
                    return _messageHandler.GetServiceResponse<Pagination<GetCinemasOutputDto>>(ErrorMessage.NotFound, null, "cinemas");
                }

                // Create pagination result
                var pagination = new Pagination<GetCinemasOutputDto>(cinemas, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, pagination, "Cinemas");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<Pagination<GetCinemasOutputDto>>(ErrorMessage.ServerInternalError, null, "GetCinemasListAsync");
            }
        }
    }
}
