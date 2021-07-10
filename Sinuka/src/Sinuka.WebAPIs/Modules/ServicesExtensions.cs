using Microsoft.Extensions.DependencyInjection;
using Sinuka.Core.Interfaces.Services;
using Sinuka.Infrastructure.Services;

namespace Sinuka.WebAPIs.Modules
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHashingService, BcryptHashingService>();
            
            return services;
        }
    }
}
