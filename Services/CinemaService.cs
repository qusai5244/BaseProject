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


        public async Task<ServiceResponse> AddCinemaHallAsync(int cinemaId, AddHallInput input)
        {
            try
            {
                var isCinemaExist = await _dataContext.Cinemas.AnyAsync(x => x.Id == cinemaId);

                if (!isCinemaExist)
                {
                    return _messageHandler.GetServiceResponse<Pagination<GetMoviesOutput>>(ErrorMessage.NotFound, null, "Cinema");
                }

                var hall = new Hall
                {
                    Name = input.Name,
                    SeatingCapacity = input.SeatingCapacity,
                    CinemaId = cinemaId,
                };

                await _dataContext.Halls.AddAsync(hall);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Hall");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse> AddMovieAsync(int cinemaId, AddMovieInput input)
        {
            try
            {
                var isCinemaExist = await _dataContext.Cinemas.AnyAsync(x => x.Id == cinemaId);

                if (!isCinemaExist)
                {
                    return _messageHandler.GetServiceResponse<Pagination<GetMoviesOutput>>(ErrorMessage.NotFound, null, "Cinema");
                }

                var movie = new Movie
                {
                    Title = input.Title,
                    Description = input.Description,
                    Type = input.Type,
                    Status = input.Status,
                    ReleaseDate = input.ReleaseDate,
                    DurationInMinutes = input.DurationInMinutes,
                    CinemaId = cinemaId,
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
        public async Task<ServiceResponse<Pagination<GetMoviesOutput>>> GetMoviesAsync(int cinemaId, GetMoviesInput input)
        {
            try
            {
                var isCinemaExist = await _dataContext.Cinemas.AnyAsync(x => x.Id == cinemaId);

                if (!isCinemaExist)
                {
                    return _messageHandler.GetServiceResponse<Pagination<GetMoviesOutput>>(ErrorMessage.NotFound, null, "Cinema");
                }

                var query = _dataContext.Movies.Where(x => x.CinemaId == cinemaId).AsNoTracking();

                if (!string.IsNullOrWhiteSpace(input.Search))
                {
                    query = query.Where(m => m.Title.StartsWith(input.Search));
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
                                     Type = x.Type.ToString(),
                                     CinemaId = x.CinemaId,
                                     DurationInMinutes = x.DurationInMinutes,
                                 })
                                 .ToListAsync();

                var pagination = new Pagination<GetMoviesOutput>(movies, totalCount, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, pagination, "Cinema");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse> AssignMovieToHallAsync(AssignMovieToHallInput input)
        {
            try
            {
                //var validationResponse =  await Validation(input);

                //if (!validationResponse.Success)
                //{
                //    return _messageHandler.GetServiceResponse((ErrorMessage)validationResponse.Code, validationResponse.Description);
                //}

                //var movieDurationInMinutes = validationResponse.Result;

                //List<MovieTime> movieTimes = [];  

                //foreach (var time in input.StartTimes)
                //{
                //    var movieTime = new MovieTime
                //    {
                //        MovieId = input.MovieId,
                //        HallId = input.HallId,
                //        StartTime = time.ToString(),
                //        EndTime = time + (movieDurationInMinutes/60),
                //    };

                //    movieTimes.Add(movieTime);
                //}

                //await _dataContext.MovieTimes.AddRangeAsync(movieTimes);
                //await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Movie Hall");
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<ServiceResponse<int>> Validation(AssignMovieToHallInput input)
        {
            var isCinemaExist = await _dataContext.Cinemas.AnyAsync(x => x.Id == input.CinemaId);

            if (!isCinemaExist)
            {
                return _messageHandler.GetServiceResponse<int>(ErrorMessage.NotFound, 0, "Cinema");
            }

            var isHallExist = await _dataContext.Halls.AnyAsync(x => x.Id == input.HallId && x.CinemaId == input.CinemaId);

            if (!isHallExist)
            {
                return _messageHandler.GetServiceResponse<int>(ErrorMessage.NotFound, 0, "Hall");
            }

            var movie = await _dataContext.Movies.FirstOrDefaultAsync(x => x.Id == input.MovieId && x.CinemaId == input.CinemaId);

            if (movie is null)
            {
                return _messageHandler.GetServiceResponse<int>(ErrorMessage.NotFound, 0, "Movie");
            }

            return _messageHandler.GetServiceResponse(SuccessMessage.Created, movie.DurationInMinutes, "Movie Hall");
        }

        public async Task<ServiceResponse<GetMoviesTimeOutput>> GetMoviesTimeAsync(GetMoviesTimeInput input)
        {
            try
            {

                //var query = _dataContext
                //            .MovieTimes
                //            .Include(x => x.Hall)
                //            .Include(x => x.Movie)
                //            .Where(x => x.HallId == input.HallId && x.MovieId == input.MovieId)
                //            .AsNoTracking();

                //var movieTimes = await query
                //                 .OrderByDescending(x => x.Id)
                //                 .ToListAsync();


                //if (movieTimes.Count == 0)
                //{
                //    return _messageHandler.GetServiceResponse<GetMoviesTimeOutput>(ErrorMessage.NotFound, null, "Movie Times");
                //}

                //var response = new GetMoviesTimeOutput
                //{
                //    HallName = movieTimes.FirstOrDefault().Hall.Name,
                //    MovieName = movieTimes.FirstOrDefault().Movie.Title,
                //    Times = movieTimes.Select(x => new DisplayTimes
                //    {
                //        StartTime = x.StartTime,
                //        EndTime = x.EndTime,
                //    }).ToList(),
                //};

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, new GetMoviesTimeOutput(), "Movie Time");

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
