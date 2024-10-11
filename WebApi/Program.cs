using Application;
using Infrastructure;
using Microsoft.AspNetCore;
using WebApi.TodoItem;
using WebApi.TodoList;

IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.RegisterApplication();
    services.RegisterInfrastructure();
});

builder.Configure((context, app) =>
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();
    app.UseEndpoints(routeBuilder =>
    {
        RouteGroupBuilder api = routeBuilder.MapGroup("api/v1");
        CreateTodoList.MapRoute(api);
        GetTodoList.MapRoute(api);
        CreateTodoItem.MapRoute(api);
        GetTodoItem.MapRoute(api);
        GetAllTodoItems.MapRoute(api);

    });
});

IWebHost app = builder.Build();
app.Run();
