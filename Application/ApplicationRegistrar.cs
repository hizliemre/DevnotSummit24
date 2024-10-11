using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationRegistrar
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => { configuration.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly); });
        return services;
    }
}
