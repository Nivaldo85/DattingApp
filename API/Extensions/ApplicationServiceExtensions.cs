using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace API;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
    IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt=>{
        opt.UseSqlite (config.GetConnectionString("DefaultConnection"));
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        services.AddScoped<ITokenService,TokenService>();
        services.AddCors();
         return services;
            }
           
}
