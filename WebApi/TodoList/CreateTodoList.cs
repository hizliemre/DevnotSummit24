using Application;
using MediatR;

namespace WebApi.TodoList;

internal class CreateTodoListRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}

internal static class CreateTodoList
{
    public const string ROUTE_NAME = "CreateTodoList";

    public static RouteHandlerBuilder MapRoute(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todo/lists");

        return group.MapPost(string.Empty,
                async (CreateTodoListRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    CreateTodoListCommand command = new CreateTodoListCommand
                    {
                        Name = request.Name,
                        Description = request.Description
                    };

                    string id = await sender.Send(command, cancellationToken);

                    return TypedResults.CreatedAtRoute(GetTodoList.ROUTE_NAME, new { id });
                })
            .WithName(ROUTE_NAME);
    }
}
