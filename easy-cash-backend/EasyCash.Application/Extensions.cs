namespace EasyCash.Application
{
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using EasyCash.Application.Settings;

    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(assemblies: Assemblies.Application);

            AppSettings appSettings  = new();
            configuration.GetSection(nameof(AppSettings)).Bind(appSettings);
            services.AddSingleton(appSettings);

            return services;
        }
    }
}