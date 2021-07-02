using Microsoft.Extensions.DependencyInjection;
using Sinuka.Application.UseCases.Register;
using Sinuka.Application.UseCases.Login;
using Sinuka.Application.UseCases.Authorization;
using Sinuka.Application.UseCases.Logout;
using Sinuka.Application.UseCases.Refresh;

namespace Sinuka.WebAPIs.Modules
{
    public static class UseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUseCase, RegisterUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IAuthorizationUseCase, AuthorizationUseCase>();
            services.AddScoped<ILogoutUseCase, LogoutUseCase>();
            services.AddScoped<IRefreshUseCase, RefreshUseCase>();

            return services;
        }
    }
}
