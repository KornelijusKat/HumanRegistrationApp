using humanRegistrationApp.Database.Repositories;
using HumanRegistrationApp.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;




namespace HumanRegistrationApp.Database.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            return services;
        }
    }
}
