using Domain.Port;
using Domain.UseCase;
using MediatR;

namespace Application;

public class GetAllTodoItemsQuery : IRequest<IEnumerable<TodoItem>>
{
    public string ListId { get; set; }
}

internal class GetAllTodoItemsQueryHandler(ITodoItemPort todoListPort) : IRequestHandler<GetAllTodoItemsQuery, IEnumerable<TodoItem>>
{
    public async Task<IEnumerable<TodoItem>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        GetAllTodoItems useCase = new()
        {
            ListId = request.ListId
        };

        return await todoListPort.GetAll(useCase);
    }
}
