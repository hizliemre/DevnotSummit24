using Domain.Port;
using Domain.UseCase;
using MediatR;

namespace Application;

public class GetTodoListQuery : IRequest<TodoList>
{
    public string Id { get; set; }
}

internal class GetTodoListQueryHandler(ITodoListPort todoListPort) : IRequestHandler<GetTodoListQuery, TodoList?>
{
    public async Task<TodoList?> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        GetTodoList useCase = new()
        {
            Id = request.Id
        };

        return await todoListPort.Get(useCase);
    }
}
