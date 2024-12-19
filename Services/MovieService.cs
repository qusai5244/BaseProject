using BaseProject.Data;
using BaseProject.Dtos.Cinema;
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
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ServiceResponse<object>> AddCinemaAsync(CinemaDto cinemaDto)
        {
            try
            {
                var cinema = new Cinema
                {
                    Name = cinemaDto.Name,
                    Location = cinemaDto.Location
                };

                await _dataContext.Cinemas.AddAsync(cinema);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse<object>(SuccessMessage.Created, "Cinema");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the cinema.", ex);
            }
        }


        public async Task<ServiceResponse<object>> AddHallToCinemaAsync(int cinemaId, HallDto hallDto)
        {
            try
            {
                var cinema = await _dataContext.Cinemas.FindAsync(cinemaId);
                if (cinema == null)
                {
                    return _messageHandler.GetServiceResponse<object>(ErrorMessage.NotFound, "Cinema");
                }

                var hall = new Hall
                {
                    HallName = hallDto.HallName,
                    SeatingCapacity = hallDto.SeatingCapacity,
                    CinemaId = cinemaId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dataContext.Halls.AddAsync(hall);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse<object>(SuccessMessage.Created, "Hall added successfully");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the hall.", ex);
            }
        }



        public async Task<ServiceResponse<object>> ScheduleMovieAsync(MovieScheduleDto scheduleDto)
        {
            try
            {
                var movie = await _dataContext.Movies.FindAsync(scheduleDto.MovieId);
                if (movie == null)
                {
                    return _messageHandler.GetServiceResponse<object>(ErrorMessage.NotFound, "Movie");
                }

                var hall = await _dataContext.Halls.FindAsync(scheduleDto.HallId);
                if (hall == null)
                {
                    return _messageHandler.GetServiceResponse<object>(ErrorMessage.NotFound, "Hall");
                }

                var schedule = new MovieSchedule
                {
                    MovieId = scheduleDto.MovieId,
                    HallId = scheduleDto.HallId,
                    ShowTime = scheduleDto.ShowTime,
                    AvailableSeats = scheduleDto.AvailableSeats,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _dataContext.MovieSchedules.AddAsync(schedule);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse<object>(SuccessMessage.Created, "Movie scheduled successfully");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while scheduling the movie.", ex);
            }
        }



        public async Task<ServiceResponse<object>> GetMoviesByCinemaIdAsync(int cinemaId)
        {
            try
            {
                var movies = await _dataContext.MovieSchedules
                    .Include(ms => ms.Movie)
                    .Include(ms => ms.Hall)
                    .Where(ms => ms.Hall.CinemaId == cinemaId)
                    .Select(ms => new
                    {
                        MovieId = ms.Movie.Id,
                        Title = ms.Movie.Title,
                        Description = ms.Movie.Description,
                        Type = ms.Movie.Type,
                        ReleaseDate = ms.Movie.ReleaseDate,
                        ShowTime = ms.ShowTime,
                        AvailableSeats = ms.AvailableSeats
                    })
                    .Distinct()
                    .ToListAsync();

                if (!movies.Any())
                {
                    return _messageHandler.GetServiceResponse<object>(ErrorMessage.NotFound, "No movies found for the specified cinema.");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, (object)movies);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving movies by cinema ID.", ex);
            }
        }


        public async Task<ServiceResponse<object>> SearchMovieByNameAsync(string movieName)
        {
            try
            {
                
                if (string.IsNullOrEmpty(movieName))
                {
                    return _messageHandler.GetServiceResponse<object>(ErrorMessage.InvalidInput, "Movie name is required.");
                }

                
                var movies = await _dataContext.MovieSchedules
                    .Include(ms => ms.Movie)
                    .Include(ms => ms.Hall)
                    .Where(ms => EF.Functions.Like(ms.Movie.Title, $"%{movieName}%")) // Case-insensitive LIKE search
                    .Select(ms => new
                    {
                        MovieId = ms.Movie.Id,
                        Title = ms.Movie.Title,
                        Description = ms.Movie.Description,
                        Type = ms.Movie.Type,
                        ReleaseDate = ms.Movie.ReleaseDate,
                        ShowTime = ms.ShowTime,
                        AvailableSeats = ms.AvailableSeats
                    })
                    .ToListAsync();

                if (!movies.Any())
                {
                    return _messageHandler.GetServiceResponse<object>(ErrorMessage.NotFound, "No movies found with the specified name.");
                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, (object)movies);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while searching for the movie.", ex);
            }
        }








    }
}

