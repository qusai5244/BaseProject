using BaseProject.Data; 
using BaseProject.Services; 
using Microsoft.EntityFrameworkCore;
using BaseProject.Services.Interfaces; 
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ICinemaService, CinemaService>(); 
builder.Services.AddScoped<ICinemaHallService, CinemaHallService>(); 
builder.Services.AddScoped<IMovieService, MovieService>(); 
builder.Services.AddScoped<IMovieScheduleService, MovieScheduleService>();




builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
