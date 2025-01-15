using BaseProject.Data;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.Cinema;
using BaseProject.Dtos.Movies;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class CinemaService(DataContext dataContext, IMessageHandler messageHandler) : ICinemaService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IMessageHandler _messageHandler = messageHandler;

        public async Task<ServiceResponse> AddCinemaAsync(AddCinemaInput input)
        {
            try
            {
                var cinema = new Cinema
                {
                    Name = input.Name,
                    Location = input.Location,
                };

                await _dataContext.Cinemas.AddAsync(cinema);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Cinema");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<Pagination<GetMoviesOutput>>> GetMoviesAsync(GetMoviesInput input)
        {
            try
            {
                var query = _dataContext.Movies.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(input.Search)) 
                {
                    query = query.Where(m => m.Title.StartsWith(input.Search));
                }

                if (input.CinemaId != null) 
                {
                    query = query.Where(m => m.CinemaId ==  input.CinemaId);
                }

                var totalCount = await query.CountAsync();

                var movies = await query
                                 .OrderByDescending(x => x.Id)
                                 .Skip(input.PageSize * (input.Page - 1))
                                 .Take(input.PageSize)
                                 .Select(x => new GetMoviesOutput
                                 {
                                     Id = x.Id,
                                     Description = x.Description,
                                     ReleaseDate = x.ReleaseDate,
                                     Status = x.Status,
                                     Title = x.Title,
                                     Type = x.Type,
                                     CinemaId = x.CinemaId,
                                     DurationInMinutes = x.DurationInMinutes,
                                 })
                                 .ToListAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Cinema");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
