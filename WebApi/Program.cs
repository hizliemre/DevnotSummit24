using Application;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.OpenApi;
using WebApi.TodoItem;
using WebApi.TodoList;

IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddEndpointsApiExplorer();
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

    services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    services.RegisterApplication();
    services.RegisterInfrastructure();
});

builder.Configure((context, app) =>
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
    });

    app.UseRouting();
    app.UseEndpoints(routeBuilder =>
    {
        RouteGroupBuilder api = routeBuilder.MapGroup("api/v{version:apiVersion}");
        ApiVersionSet versionSet = routeBuilder.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        CreateTodoList.MapRoute(api).WithApiVersionSet(versionSet).MapToApiVersion(1);
        GetTodoList.MapRoute(api).WithApiVersionSet(versionSet).MapToApiVersion(1);
        CreateTodoItem.MapRoute(api).WithApiVersionSet(versionSet).MapToApiVersion(1);
        GetTodoItem.MapRoute(api).WithApiVersionSet(versionSet).MapToApiVersion(1);
        GetAllTodoItems.MapRoute(api).WithApiVersionSet(versionSet).MapToApiVersion(1);
        GetTodoItemV2.MapRoute(api).WithApiVersionSet(versionSet).MapToApiVersion(2);
        GetAllTodoItemsV2.MapRoute(api).WithApiVersionSet(versionSet).MapToApiVersion(2);
    });


});

IWebHost app = builder.Build();
app.Run();
