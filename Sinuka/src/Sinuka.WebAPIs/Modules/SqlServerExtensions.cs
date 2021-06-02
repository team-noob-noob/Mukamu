using Microsoft.Extensions.DependencyInjection;
using Sinuka.Application.Interfaces;
using Sinuka.Infrastructure.Database;
using Sinuka.Infrastructure.Database.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Interfaces.Repositories;

namespace Sinuka.WebAPIs.Modules
{
    public static class SqlServerExtensions
    {
        public static IServiceCollection AddMySqlServer(this IServiceCollection services)
        {
            services.AddDbContext<SinukaDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserFactory, ModelFactory>();
            services.AddScoped<ISessionFactory, ModelFactory>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}