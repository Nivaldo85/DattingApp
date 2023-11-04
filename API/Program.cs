using API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt=>{
    opt.UseSqlite (builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.MapControllers();

app.Run();
