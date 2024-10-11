using Domain.UseCase;

namespace Domain.Port;

public interface ITodoItemPort
{
    public Task<string> Create(CreateTodoItem useCase);
    public Task<TodoItem?> Get(GetTodoItem useCase);
    public Task<IEnumerable<TodoItem>> GetAll(GetAllTodoItems useCase);
}
