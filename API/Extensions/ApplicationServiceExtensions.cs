using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
         public static IServiceCollection AddApplicationService(this IServiceCollection services , IConfiguration Configuration)
         {
            
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
             AddJwtBearer(option=>{
                   option.TokenValidationParameters = new TokenValidationParameters
                   {

                       ValidateIssuerSigningKey =true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                   };

             });
             return services;
         }
    }
}