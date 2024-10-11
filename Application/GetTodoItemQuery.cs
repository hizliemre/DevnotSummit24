using Domain.Port;
using Domain.UseCase;
using MediatR;

namespace Application;

public class GetTodoItemQuery : IRequest<TodoItem>
{
    public string ListId { get; set; }
    public string Id { get; set; }
}

internal class GetTodoItemQueryHandler(ITodoItemPort todoListPort) : IRequestHandler<GetTodoItemQuery, TodoItem?>
{
    public async Task<TodoItem?> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        GetTodoItem useCase = new()
        {
            ListId = request.ListId,
            Id = request.Id
        };

        return await todoListPort.Get(useCase);
    }
}
