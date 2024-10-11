using Domain.Port;
using Domain.UseCase;
using MediatR;

namespace Application;

public class CreateTodoItemCommand: IRequest<string>
{
    public string ListId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

internal class CreateTodoItemCommandHandler(ITodoListPort todoListPort, ITodoItemPort todoItemPort) : IRequestHandler<CreateTodoItemCommand, string>
{
    public async Task<string> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        bool hasList = await todoListPort.HasList(request.ListId);

        if (!hasList)
        {
            throw new Exception("List not found");
        }

        CreateTodoItem useCase = new()
        {
            ListId = request.ListId,
            Title = request.Title,
            Description = request.Description,
            IsDone = request.IsDone,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        return await todoItemPort.Create(useCase);
    }
}
