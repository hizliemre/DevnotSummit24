using Application;
using MediatR;

namespace WebApi.TodoItem;

internal class GetAllTodoItemsResponse
{
    public IEnumerable<GetTodoItemResponse> Items { get; set; }
}

internal class GetAllTodoItemsResponseV2
{
    public IEnumerable<GetTodoItemResponseV2> Items { get; set; }
}

internal class GetTodoItemResponse
{
    public string Id { get; set; }
    public string ListId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

internal class TodoItemDetails
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

internal class GetTodoItemResponseV2
{
    public string Id { get; set; }
    public string ListId { get; set; }
    public TodoItemDetails Details { get; set; }
}

internal static class GetTodoItem
{
    public const string ROUTE_NAME = "GetTodoItem";

    public static RouteHandlerBuilder MapRoute(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todo/lists/{listId}/items");

        return group.MapGet("{id}", async (string listId, string id, ISender sender, CancellationToken cancellationToken) =>
            {
                GetTodoItemQuery query = new()
                {
                    ListId = listId,
                    Id = id
                };

                var todoItem = await sender.Send(query, cancellationToken);

                GetTodoItemResponse response = new()
                {
                    Id = todoItem.Id,
                    ListId = todoItem.ListId,
                    Title = todoItem.Title,
                    Description = todoItem.Description,
                    IsDone = todoItem.IsDone,
                    StartDate = todoItem.StartDate,
                    EndDate = todoItem.EndDate
                };

                return Results.Ok(response);
            })
            .WithName(ROUTE_NAME);
    }
}

internal static class GetAllTodoItemsV2
{
    public const string ROUTE_NAME = "GetAllTodoItemsV2";

    public static RouteHandlerBuilder MapRoute(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todo/lists/{listId}/items");

        return group.MapGet(string.Empty, async (string listId, ISender sender, CancellationToken cancellationToken) =>
            {
                GetAllTodoItemsQuery query = new()
                {
                    ListId = listId
                };

                var todoItems = await sender.Send(query, cancellationToken);

                GetAllTodoItemsResponseV2 response = new()
                {
                    Items = todoItems.Select(todoItem => new GetTodoItemResponseV2
                    {
                        Id = todoItem.Id,
                        ListId = todoItem.ListId,
                        Details = new TodoItemDetails
                        {
                            Title = todoItem.Title,
                            Description = todoItem.Description,
                            IsDone = todoItem.IsDone,
                            StartDate = todoItem.StartDate,
                            EndDate = todoItem.EndDate
                        }
                    })
                };

                return Results.Ok(response);
            })
            .WithName(ROUTE_NAME);
    }
}

internal static class GetTodoItemV2
{
    public const string ROUTE_NAME = "GetTodoItemV2";

    public static RouteHandlerBuilder MapRoute(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todo/lists/{listId}/items");

        return group.MapGet("{id}", async (string listId, string id, ISender sender, CancellationToken cancellationToken) =>
            {
                GetTodoItemQuery query = new()
                {
                    ListId = listId,
                    Id = id
                };

                var todoItem = await sender.Send(query, cancellationToken);

                GetTodoItemResponseV2 response = new()
                {
                    Id = todoItem.Id,
                    ListId = todoItem.ListId,
                    Details = new TodoItemDetails
                    {
                        Title = todoItem.Title,
                        Description = todoItem.Description,
                        IsDone = todoItem.IsDone,
                        StartDate = todoItem.StartDate,
                        EndDate = todoItem.EndDate
                    }
                };

                return Results.Ok(response);
            })
            .WithName(ROUTE_NAME);
    }
}

internal static class GetAllTodoItems
{
    public const string ROUTE_NAME = "GetAllTodoItems";

    public static RouteHandlerBuilder MapRoute(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/todo/lists/{listId}/items");

        return group.MapGet(string.Empty, async (string listId, ISender sender, CancellationToken cancellationToken) =>
            {
                GetAllTodoItemsQuery query = new()
                {
                    ListId = listId
                };

                var todoItems = await sender.Send(query, cancellationToken);

                GetAllTodoItemsResponse response = new()
                {
                    Items = todoItems.Select(todoItem => new GetTodoItemResponse
                    {
                        Id = todoItem.Id,
                        ListId = todoItem.ListId,
                        Title = todoItem.Title,
                        Description = todoItem.Description,
                        IsDone = todoItem.IsDone,
                        StartDate = todoItem.StartDate,
                        EndDate = todoItem.EndDate
                    })
                };

                return Results.Ok(response);
            })
            .WithName(ROUTE_NAME);
    }
}
