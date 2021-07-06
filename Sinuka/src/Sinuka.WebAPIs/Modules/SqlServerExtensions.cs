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

            services.AddSingleton<IUserFactory, ModelFactory>();
            services.AddSingleton<ISessionFactory, ModelFactory>();
            services.AddSingleton<ISessionTokenFactory, ModelFactory>();
            services.AddSingleton<IRefreshTokenFactory, ModelFactory>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IEmailAddressRepository, EmailAddressRepository>();

            return services;
        }
    }
}
