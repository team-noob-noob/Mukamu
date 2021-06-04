using Microsoft.Extensions.DependencyInjection;
using Sinuka.Application.UseCases.Register;
using Sinuka.Application.UseCases.Login;

namespace Sinuka.WebAPIs.Modules
{
    public static class UseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUseCase, RegisterUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();

            return services;
        }
    }
}
