using Azure;
using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Car;
using BaseProject.Dtos.Ciname;
using BaseProject.Extensions;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class CinameService : ICinameServices
    {
        private readonly DataContext _dataContext;

        private readonly IMessageHandler _messageHandler;


        public CinameService(DataContext  dataContext, IMessageHandler messageHandler )
        {
            _dataContext = dataContext;
            _messageHandler = messageHandler;
        }



        public async Task<ServiceResponse> AddCinameAsync(AddNewCinameDto input)
        {

            try
            {
                var check = await _dataContext.Cinemas.FirstOrDefaultAsync(c => c.CName == input.CName && c.Clocation == input.Clocation && c.BulidingName == input.BulidingName);

                if (check is not null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.AlreadyExists, "Already add this cinema");

                }


                var ciname = new Cinema
                {
                    CName = input.CName,
                    Clocation = input.Clocation,
                    Cphone = input.Cphone,
                    CEmail = input.CEmail,
                    BulidingName = input.BulidingName,
                    CreatedAt = DateTime.UtcNow,
                    
                };

                await _dataContext.Cinemas.AddAsync(ciname);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Ciname");

            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddCiname");
            }
        }

        public async Task<ServiceResponse<getCinemaDto>> GetCinemaAsync(int cnid)
        {
            try
            {
                var query = await _dataContext
                                .Cinemas
                                .AsNoTracking()
                                .Select(c => new getCinemaDto
                                {
                                    Id = c.Id,
                                    CName = c.CName,
                                    Clocation = c.Clocation,
                                    Cphone = c.Cphone,
                                    CEmail = c.CEmail,
                                   BulidingName = c.BulidingName
                                }).FirstOrDefaultAsync(c => c.Id == cnid);


                if (query == null)
                {
                    return _messageHandler.GetServiceResponse<getCinemaDto>(ErrorMessage.NotFound, null, "Cinema");

                }
                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, query);


            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<getCinemaDto>(ErrorMessage.ServerInternalError, null, "GetCinemaAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<getCinemaDto>>> GetCinemaListAsync(GlobalFilterDto input)
        {
            try
            {
                var query = _dataContext.Cinemas.AsNoTracking();
                var totalItems = await query.CountAsync();



                var cinema = await query
                              .Skip(input.PageSize * (input.Page - 1))
                             .Take(input.PageSize)
                            .Sort(input.SortOrder)
                            .Select(c => new getCinemaDto
                            {
                                Id = c.Id,
                                CName = c.CName,
                                Clocation = c.Clocation,
                                Cphone = c.Cphone,
                                CEmail = c.CEmail
                            })
                            .ToListAsync();

                var paginationList = new Pagination<getCinemaDto>(cinema, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);





            }
            catch
            {
                return _messageHandler.GetServiceResponse<Pagination<getCinemaDto>>(ErrorMessage.ServerInternalError, null, "AddCarAsync");

            }
        }
    }
}
