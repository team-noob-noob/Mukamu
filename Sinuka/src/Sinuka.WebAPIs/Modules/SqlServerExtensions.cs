using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<SinukaDbContext>(b => b.UseLazyLoadingProxies());
            services.AddScoped<ISinukaDbContext>(provider => provider.GetService<SinukaDbContext>());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserFactory, ModelFactory>();
            services.AddScoped<ISessionFactory, ModelFactory>();
            services.AddScoped<ISessionTokenFactory, ModelFactory>();
            services.AddScoped<IRefreshTokenFactory, ModelFactory>();
            services.AddScoped<IPasswordResetFactory, ModelFactory>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IEmailAddressRepository, EmailAddressRepository>();
            services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();

            return services;
        }
    }
}
