using Domain.Port;
using Domain.UseCase;
using MediatR;

namespace Application;

public class CreateTodoListCommand : IRequest<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
}

internal class CreateTodoListCommandHandler(ITodoListPort todoListPort) : IRequestHandler<CreateTodoListCommand, string>
{
    public async Task<string> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        CreateTodoList useCase = new()
        {
            Name = request.Name,
            Description = request.Description
        };

        return await todoListPort.Create(useCase);
    }
}
