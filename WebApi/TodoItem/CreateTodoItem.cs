using Application;
using MediatR;

namespace WebApi.TodoItem;

internal class CreateTodoItemRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

internal static class CreateTodoItem
{
    public const string ROUTE_NAME = "CreateTodoItem";

    public static RouteHandlerBuilder MapRoute(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todo/lists/{listId}/items");

        return group.MapPost(string.Empty,
                async (string listId, CreateTodoItemRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    CreateTodoItemCommand command = new CreateTodoItemCommand
                    {
                        ListId = listId,
                        Title = request.Title,
                        Description = request.Description,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate
                    };

                    string id = await sender.Send(command, cancellationToken);

                    return TypedResults.CreatedAtRoute(GetTodoItem.ROUTE_NAME, new { id });
                })
            .WithName(ROUTE_NAME);
    }
}
