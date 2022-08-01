using HumanRegistrationApp.BussinessLogic.DbServices;
using HumanRegistrationApp.BussinessLogic.ImageServices;
using HumanRegistrationApp.BussinessLogic.JwtService;
using HumanRegistrationSystem.ImageHandler.ImageServices;
using Microsoft.Extensions.DependencyInjection;


namespace HumanRegistrationApp.BussinessLogic.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBussinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService.JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IImageService, ImageService>();
            return services;
        }
    }
}
