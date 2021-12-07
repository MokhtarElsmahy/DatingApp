using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
         public static IServiceCollection AddIdentityService(this IServiceCollection services , IConfiguration Configuration)
         {
              services.AddScoped<ITokenService,TokenService>();
             services.AddDbContext<DataContext>(option=>{
                  option.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
             });

             return services;
         }
    }
}