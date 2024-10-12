using Domain.Port;
using Domain.UseCase;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public class CreateTodoItemCommand: IRequest<string>
{
    public string ServiceKey { get; set; } = "default";
    public string ListId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

internal class CreateTodoItemCommandHandler(IServiceProvider provider, ITodoListPort todoListPort) : IRequestHandler<CreateTodoItemCommand, string>
{
    public async Task<string> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        ITodoItemPort todoItemPort =
            provider.GetKeyedService<ITodoItemPort>(request.ServiceKey) ?? provider.GetRequiredService<ITodoItemPort>();

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
