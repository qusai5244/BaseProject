using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Hall;
using BaseProject.Dtos.Movie;
using BaseProject.Dtos.MovieShowTime;
using BaseProject.Extensions;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BaseProject.Services
{
    public class MovieShowTimeService : IMovieShowTime
    {

        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public MovieShowTimeService(DataContext dataContext, IMessageHandler messageHandler)
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }
        public async Task<ServiceResponse> AddMovieAsync(AddNewMovieShowTimeDto input)
        {
            try
            {

                var checkCapcity = await _dataContext.Halls.Where(h => h.Id == input.HallId && h.capcity >= input.AvailableTickets).FirstOrDefaultAsync();
                if (checkCapcity  == null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.AlreadyExists, "Ticket is more than hall capcity");
                }


                var movieShow = new MovieShowTime
                {
                    HallId = input.HallId,
                    MovieId = input.MovieId,
                    price = input.price,
                    AvailableTickets = input.AvailableTickets,

                };

                await _dataContext.MoviesShowTime.AddAsync(movieShow);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "AddMovie");


            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddMovie");

            }
        }






        public async Task<ServiceResponse<Pagination<getMovieShowTimeDto>>> GetMovieByCinemaIdAsync([FromQuery] GlobalFilterDto input, int cinemaId)
        {
            try
            {
                var Gethall = await _dataContext.Halls.Where(h => h.cinema_id == cinemaId).FirstOrDefaultAsync();

                if (Gethall == null)
                {
                    return _messageHandler.GetServiceResponse<Pagination<getMovieShowTimeDto>>(ErrorMessage.ServerInternalError, null, "GetHall By cinemaId");
                }

                var query = _dataContext.MoviesShowTime.AsNoTracking();
                var totalItems = await query.CountAsync();

                var showTime = await query
                                  .Where(h => h.HallId == Gethall.Id)
                                  .Include(st => st.Movies)
                                  .Select(st => new getMovieShowTimeDto
                                  {
                                      HallId = st.HallId,
                                      MovieId = st.MovieId,
                                      price = st.price,
                                      AvailableTickets = st.AvailableTickets,
                                      MName = st.Movies.MName,
                                      Title = st.Movies.Title,
                                  }).ToListAsync();

                var paginationList = new Pagination<getMovieShowTimeDto>(showTime, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);

            }
            catch
            {
                return _messageHandler.GetServiceResponse<Pagination<getMovieShowTimeDto>>(ErrorMessage.ServerInternalError, null, "GetHallAsync");
            }
        }










        public async Task<ServiceResponse<Pagination<getMovieShowTimeDto>>> GetMovieByCinemaIdAsync(GlobalFilterDto input)
        {
            try
            {
                var query = _dataContext.MoviesShowTime.AsNoTracking();
                var totalItems = await query.CountAsync();



                var hall = await query
                              .Skip(input.PageSize * (input.Page - 1))
                             .Take(input.PageSize)
                            .Select(m => new getMovieShowTimeDto
                            {
                                MovieId = m.MovieId



                            })
                            .ToListAsync();

                var paginationList = new Pagination<getMovieShowTimeDto>(hall, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);





            }
            catch
            {
                return _messageHandler.GetServiceResponse<Pagination<getMovieShowTimeDto>>(ErrorMessage.ServerInternalError, null, "GetHallAsync");

            }
        }











    }
}

