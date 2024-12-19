﻿using BaseProject.Dtos;
using BaseProject.Dtos.Cinema;
using BaseProject.Helpers;

namespace BaseProject.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<ServiceResponse> AddCinemaAsync(AddCinemaInputDto input);
        Task<ServiceResponse<Pagination<GetCinemasOutputDto>>> GetCinemasListAsync(GlobalFilterDto input);
    }
}
