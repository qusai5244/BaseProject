using BaseProject.Data;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace BaseProject.Services
{
    public class MovieService : IMovieService
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public MovieService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }

        public async Task<ServiceResponse> AddNewMovieAsync(AddNewMovie input)
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




        public async Task<ServiceResponse<Pagination<GetMovieListOutput>>> GetMovieList(GetMovieListInput input)
        {
            try
            {
                var query = _dataContext.Movies.OrderByDescending(x => x.Id).AsQueryable();


                if (input.SortBy.GetValueOrDefault() > 0)
                {
                    if (input.SortBy == MovieSortByTypes.Type)
                    {
                        query = query.OrderByDescending(x => x.Type);
                    }

                    else
                    {
                        query = query.OrderByDescending(x => x.ReleaseDate);
                    }

                }
                if (input.Type.GetValueOrDefault() > 0)
                {
                    query = query.Where(x => x.Type == input.Type);

                }

                var totalCount = await query.CountAsync();
                var data = await query
                    .Skip(input.PageSize * (input.Page - 1))
                    .Take(input.PageSize)
                    .Select(x => new GetMovieListOutput
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Type = x.Type.ToString(),
                        ReleaseDate = x.ReleaseDate,
                        Duration = x.Duration,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                    })
                    .ToListAsync();

                var paginationList = new Pagination<GetMovieListOutput>(data, totalCount, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<ServiceResponse> UpdateMovieAsync(int id, UpdateMovieInput input)
        {
            try
            {
                var movie = await _dataContext.Movies
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();
                if (movie == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "Movies");
                }

                movie.Title = input.Title;
                movie.Description = input.Description;
                movie.Type = input.Type;
                movie.ReleaseDate = input.ReleaseDate;
                movie.Duration = input.Duration;

                movie.UpdatedAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Movie");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<object>> DeleteMovieAsync(int id)
        {
            try
            {
                var movie = await _dataContext.Movies
                    .Where(x => x.Id == id && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                if (movie == null)
                {
                    return _messageHandler.GetServiceResponse<object>(ErrorMessage.NotFound, "Movies");
                }

                movie.IsDeleted = true;
                movie.UpdatedAt = DateTime.UtcNow;
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse<object>(SuccessMessage.Deleted, null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

