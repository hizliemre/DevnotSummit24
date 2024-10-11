using Application;
using MediatR;

namespace WebApi.TodoList;

internal class GetTodoListResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

internal static class GetTodoList
{
    public const string ROUTE_NAME = "GetTodoList";

    public static void MapRoute(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todo/lists");

        group.MapGet("{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
            {
                GetTodoListQuery query = new()
                {
                    Id = id
                };

                var todoList = await sender.Send(query, cancellationToken);

                GetTodoListResponse response = new()
                {
                    Id = todoList.Id,
                    Name = todoList.Name,
                    Description = todoList.Description
                };

                return Results.Ok(response);
            })
            .WithName(ROUTE_NAME);
    }
}
