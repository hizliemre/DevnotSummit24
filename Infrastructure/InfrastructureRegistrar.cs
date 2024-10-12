using Domain.Port;
using Infrastructure.Adapter;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureRegistrar
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<Store>();
        services.AddTransient<ITodoListPort, TodoListStoreAdapter>();
        services.AddTransient<ITodoItemPort, TodoItemStoreAdapter>();
        services.AddKeyedTransient<ITodoItemPort, TodoItemStoreAdapter>("default");
        services.AddKeyedTransient<ITodoItemPort, XCompanyTodoItemStoreAdapter>("x");

        return services;
    }
}
