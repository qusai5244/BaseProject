using BaseProject.Data;
using BaseProject.Dtos;
using BaseProject.Dtos.Ciname;
using BaseProject.Dtos.Hall;
using BaseProject.Helpers;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services
{
    public class HallService : IHallServices
    {
        private readonly DataContext _dataContext;
        private readonly IMessageHandler _messageHandler;

        public HallService(DataContext dataContext, IMessageHandler messageHandler) 
        {
             _dataContext = dataContext;
             _messageHandler = messageHandler;
        }


        public async Task<ServiceResponse> AddHallAsync(AddNewHallDto input)
        {
            try
            {
                var check = await _dataContext.Halls.FirstOrDefaultAsync(h => h.HName == input.HName && h.cinema_id == input.cinema_id);

                if (check is not null)
                {
                    return _messageHandler.GetServiceResponse(ErrorMessage.AlreadyExists, "Hall");
                }

                var hall = new Hall
                {
                    HName = input.HName,
                    capcity = input.capcity,
                    cinema_id = input.cinema_id,
                    CreatedAt = DateTime.UtcNow,
                };

                await _dataContext.Halls.AddAsync(hall);
                await _dataContext.SaveChangesAsync();

                return _messageHandler.GetServiceResponse(SuccessMessage.Created, "Hall");


            }
            catch (Exception ex)
            {
                return _messageHandler.GetServiceResponse(ErrorMessage.ServerInternalError, "AddHall");

            }
        }

        public async Task<ServiceResponse<gethallDto>> GetHallAsync(int hid)
        {
            try
            {
                var query = await _dataContext
                                .Halls
                                .AsNoTracking()
                                .Select(h => new gethallDto
                                {
                                    Id = h.Id,
                                    HName = h.HName,
                                    capcity = h.capcity,
                                    cinema_id = h.cinema_id,
                                    CName = h.Cinema.CName,
                                    CLocation = h.Cinema.Clocation
                                }).FirstOrDefaultAsync(c => c.Id == hid);


                if (query == null)
                {
                    return _messageHandler.GetServiceResponse<gethallDto>(ErrorMessage.NotFound, null, "hall");

                }
                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, query);


            }
            catch (Exception ex)
            {

                return _messageHandler.GetServiceResponse<gethallDto>(ErrorMessage.ServerInternalError, null, "GetHallByIDAsync");
            }
        }

        public async Task<ServiceResponse<Pagination<gethallListDto>>> GetHallListAsync(GlobalFilterDto input)
        {
            try
            {
                var query = _dataContext.Halls.AsNoTracking();
                var totalItems = await query.CountAsync();



                var hall = await query
                              .Skip(input.PageSize * (input.Page - 1))
                             .Take(input.PageSize)
                            .Select(h => new gethallListDto
                            {
                                Id = h.Id,
                                HName = h.HName,
                                capcity = h.capcity,
                                cinema_id = h.cinema_id,
                                CName = h.Cinema.CName,
                                CLocation = h.Cinema.Clocation

                            })
                            .ToListAsync();

                var paginationList = new Pagination<gethallListDto>(hall, totalItems, input.Page, input.PageSize);

                return _messageHandler.GetServiceResponse(SuccessMessage.Retrieved, paginationList);





            }
            catch
            {
                return _messageHandler.GetServiceResponse<Pagination<gethallListDto>>(ErrorMessage.ServerInternalError, null, "GetHallAsync");

            }

        }
    }
}
