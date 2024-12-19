using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Hall;
using BaseProject.Dtos.Movie;
using BaseProject.Extensions;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BaseProject.Services
{
    public class MovieService : IMovieServices
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public MovieService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }
        public async Task<ServiceResponse> AddMovieAsync(AddNewMovieDto input)
        {
            try
            {
               

                var movie = new Movies
                {
                   MName = input.MName,
                   Title = input.Title,
                   Description = input.Description,
                   duration = input.duration,
                   Mtype = input.Mtype,
                   release_date = input.release_date,
                   language = input.language,
                   CreatedAt = DateTime.UtcNow

                };

                await _dataContext.Movies.AddAsync(movie);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "AddMovie");


            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddMovie");

            }
        }

        public async Task<ServiceResponse<Pagination<getMovieListDto>>> GetMoveListAsync(GlobalFilterDto input)
        {
            try
            {
                var query = _dataContext.Movies.AsNoTracking();
                var totalItems = await query.CountAsync();



                var hall = await query
                              .Sort(input.SortOrder)
                               .Search(input.Search)
                               .Fillter(input.fillter)
                              .Skip(input.PageSize * (input.Page - 1))
                             .Take(input.PageSize)
                            .Select(m => new getMovieListDto
                            {
                                Id = m.Id,
                                MName = m.MName,
                                Title = m.Title,
                                Description = m.Description,
                                duration = m.duration,
                                language = m.language,
                                Mtype  = m.Mtype,


                            })
                            .ToListAsync();

                var paginationList = new Pagination<getMovieListDto>(hall, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);





            }
            catch
            {
                return _messageHandler.GetServiceResponse<Pagination<getMovieListDto>>(ErrorMessage.ServerInternalError, null, "GetHallAsync");

            }
        }

        public async Task<ServiceResponse<getMovieDto>> GetMovieAsync(int mid)
        {
            try
            {

                var query = await _dataContext
                              .Movies
                              .AsNoTracking()
                            .Select(m => new getMovieDto
                            {
                                Id = m.Id,
                                MName = m.MName,
                                Title = m.Title,
                                Description = m.Description,
                                duration = m.duration,
                                language = m.language,
                                Mtype = m.Mtype


                            }).FirstOrDefaultAsync(m => m.Id == mid);
                            


                if(query is null)
                {
                    return _messageHandler.GetServiceResponse<getMovieDto>(ErrorMessage.NotFound, null, "Movie");

                }

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, query);





            }
             catch(Exception ex) 
            {
                return _messageHandler.GetServiceResponse<getMovieDto>(ErrorMessage.ServerInternalError, null, "GetMovieAsync");

            }
        }




        public async Task<ServiceResponse> UpdateMovieAsync(int mid, updateMovieDto input)
        {
            try
            {
                var movie = await _dataContext.Movies.FirstOrDefaultAsync(x => x.Id == mid);

                if (movie == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "movie");
                }

                movie.MName = input.MName;
                movie.Title = input.Title;
                movie.Description = input.Description;
                movie.duration = input.duration;
                movie.language = input.language;
                movie.Mtype = input.Mtype;
                movie.UpdatedAt = DateTime.UtcNow;

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Updated, "Movie");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "UpdateMovieAsync");
            }
        }






        public async Task<ServiceResponse> DeleteMovieAsync(int mid)
        {
            try
            {
                var movie = await _dataContext.Movies.FirstOrDefaultAsync(x => x.Id == mid);

                if (movie is null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.NotFound, "movie");
                }

                movie.isDeleted = true;
                

                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Deleted, "Movie");
            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "DeletedMovieAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<getMovieListDto>>> GetMoveAvailableAsync(GlobalFilterDto input)
        {
            try
            {
                var query = _dataContext.Movies.AsNoTracking();
                var totalItems = await query.CountAsync();



                var hall = await query
                              .Skip(input.PageSize * (input.Page - 1))
                             .Take(input.PageSize)
                             .Where(m => m.isDeleted == false)
                            .Select(m => new getMovieListDto
                            {
                                Id = m.Id,
                                MName = m.MName,
                                Title = m.Title,
                                Description = m.Description,
                                duration = m.duration,
                                language = m.language,
                                Mtype = m.Mtype,
                                
                                 

                            })

                            .ToListAsync();

                var paginationList = new Pagination<getMovieListDto>(hall, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);





            }
            catch
            {
                return _messageHandler.GetServiceResponse<Pagination<getMovieListDto>>(ErrorMessage.ServerInternalError, null, "GetHallAsync");

            }
        }

    }
}
