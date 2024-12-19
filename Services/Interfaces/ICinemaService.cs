using System;
using BaseProject.Models;

namespace BaseProject.Services.Interfaces;

public interface ICinemaService
{
    Task<IEnumerable<Cinema>> GetAllCinemasAsync();
    Task<Cinema> GetCinemaByIdAsync(int id);
    Task AddCinemaAsync(Cinema cinema);
    Task UpdateCinemaAsync(Cinema cinema);
    Task DeleteCinemaAsync(int id);

}
