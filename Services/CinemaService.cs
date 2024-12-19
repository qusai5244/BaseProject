using System;
using BaseProject.Data;
using BaseProject.Helpers.MessageHandler;
using BaseProject.Models;
using BaseProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Services;

public class CinemaService : ICinemaService
{
   
    private readonly DataContext _dataContext;

    public CinemaService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Cinema>> GetAllCinemasAsync()
    {
        return await _dataContext.Cinemas.ToListAsync();
    }

    public async Task<Cinema> GetCinemaByIdAsync(int id)
    {
        return await _dataContext.Cinemas.FirstOrDefaultAsync(c => c.CinemaID == id);
    }

    public async Task AddCinemaAsync(Cinema cinema)
    {
        await _dataContext.Cinemas.AddAsync(cinema);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateCinemaAsync(Cinema cinema)
    {
        _dataContext.Cinemas.Update(cinema);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteCinemaAsync(int id)
    {
        var cinema = await _dataContext.Cinemas.FindAsync(id);
        if (cinema != null)
        {
            _dataContext.Cinemas.Remove(cinema);
            await _dataContext.SaveChangesAsync();
        }
    }
    
}
