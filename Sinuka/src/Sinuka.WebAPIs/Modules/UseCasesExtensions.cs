using Microsoft.Extensions.DependencyInjection;
using Sinuka.Application.UseCases.Register;

namespace Sinuka.WebAPIs.Modules
{
    public static class UseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUseCase, RegisterUseCase>();

            return services;
        }
    }
}
