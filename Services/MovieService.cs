using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Movie;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<ServiceResponse> AddMovieAsync(AddMovieInputDto input)
        {
            try
            {
                // Check if CinemaHall exists
                var cinemaHall = await _dataContext.CinemaHalls.FindAsync(input.CinemaHallId);
                if (cinemaHall == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "CinemaHall");
                }

                var movie = new Movie
                {
                    Title = input.Title,
                    Description = input.Description,
                    Type = input.Type,
                    ReleaseDate = input.ReleaseDate,
                    Duration = input.Duration,
                    CinemaHallId = input.CinemaHallId,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dataContext.Movies.AddAsync(movie);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Movie");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddMovieAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<GetMovieOutputDto>>> GetMovieListAsync(GlobalFilterDto input)
        {
            try
            {
                // Query for all movies
                var query = _dataContext.Movies.AsNoTracking();

                // Search filter
                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(m => m.Title.Contains(input.Search) || m.Description.Contains(input.Search));
                }

                // Get total count of items for pagination
                var totalItems = await query.CountAsync();

                // Apply pagination
                var movies = await query
                    .Skip((input.Page - 1) * input.PageSize)
                    .Take(input.PageSize)
                    .Select(m => new GetMovieOutputDto
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Description = m.Description,
                        Type = m.Type,
                        ReleaseDate = m.ReleaseDate,
                        Duration = m.Duration,
                        IsDeleted = m.IsDeleted,
                        CinemaHallId = m.CinemaHallId,
                        CreatedAt = m.CreatedAt,
                        UpdatedAt = m.UpdatedAt
                    })
                    .ToListAsync();

                // Check movies
                if (!movies.Any())
                {
                    return _messageHandler.GetServiceResponse<Pagination<GetMovieOutputDto>>(ErrorMessage.NotFound, null, "movies");
                }

                // Create pagination result
                var pagination = new Pagination<GetMovieOutputDto>(movies, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, pagination, "Movies");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<Pagination<GetMovieOutputDto>>(ErrorMessage.ServerInternalError, null, "GetMovieListAsync");
            }
        }

        public async Task<ServiceResponse<List<GetCinemasByMovieNameOutputDto>>> GetCinemasByMovieNameAsync(string movieName)
        {
            try
            {
                var query = from cinema in _dataContext.Cinemas
                            join cinemaHall in _dataContext.CinemaHalls on cinema.Id equals cinemaHall.CinemaId
                            join movie in _dataContext.Movies on cinemaHall.Id equals movie.CinemaHallId
                            where movie.Title.Contains(movieName) && !movie.IsDeleted
                            select new GetCinemasByMovieNameOutputDto
                            {
                                MovieName = movie.Title,
                                ReleaseDate = movie.ReleaseDate,
                                CinemaId = cinema.Id,
                                CinemaName = cinema.Name,
                                CinemaLocation = cinema.Location,
                                CinemaHall = cinemaHall.HallName
                            };

                var cinemas = await query.ToListAsync();

                if (!cinemas.Any())
                {
                    return _messageHandler.GetServiceResponse<List<GetCinemasByMovieNameOutputDto>>(ErrorMessage.NotFound, null, "cinemas");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, cinemas, "Cinemas");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<List<GetCinemasByMovieNameOutputDto>>(ErrorMessage.ServerInternalError, null, "GetCinemasByMovieNameAsync");
            }
        }

        public async Task<ServiceResponse<List<GetMoviesByCinemaIdOutputDto>>> GetMoviesByCinemaIdAsync(int cinemaId)
        {
            try
            {
                var query = from cinema in _dataContext.Cinemas
                            join cinemaHall in _dataContext.CinemaHalls on cinema.Id equals cinemaHall.CinemaId
                            join movie in _dataContext.Movies on cinemaHall.Id equals movie.CinemaHallId
                            where cinema.Id == cinemaId && !movie.IsDeleted // Ensure movie is not deleted
                            select new GetMoviesByCinemaIdOutputDto
                            {
                                MovieName = movie.Title,
                                ReleaseDate = movie.ReleaseDate,
                                CinemaId = cinema.Id,
                                CinemaName = cinema.Name,
                                CinemaHall = cinemaHall.HallName
                            };

                var movies = await query.ToListAsync();

                if (!movies.Any())
                {
                    return _messageHandler.GetServiceResponse<List<GetMoviesByCinemaIdOutputDto>>(ErrorMessage.NotFound, null, "Movies in this cinema");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, movies, "Movies");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse<List<GetMoviesByCinemaIdOutputDto>>(ErrorMessage.ServerInternalError, null, "GetMoviesByCinemaIdAsync");
            }
        }

        public async Task<ServiceResponse> UpdateMovieAsync(int movieId, UpdateMovieInputDto input)
        {
            try
            {
                // Find the movie by ID
                var movie = await _dataContext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

                if (movie == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "movie");
                }

                // Update movie properties
                movie.Title = input.Title;
                movie.Description = input.Description;
                movie.Type = input.Type;
                movie.ReleaseDate = input.ReleaseDate;
                movie.Duration = input.Duration;
                movie.CinemaHallId = input.CinemaHallId;
                movie.UpdatedAt = DateTime.UtcNow;

                // Save changes to the database
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Movie");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateMovieAsync");
            }
        }

        public async Task<ServiceResponse> DeleteMovieAsync(int movieId)
        {
            try
            {
                // Find movie by ID
                var movie = await _dataContext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

                if (movie == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "movie");
                }

                movie.IsDeleted = true;
                movie.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Deleted, "Movie");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "DeleteMovieAsync");
            }
        }

    }
}
