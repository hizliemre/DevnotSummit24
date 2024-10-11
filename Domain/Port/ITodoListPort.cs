using Domain.UseCase;

namespace Domain.Port;

public interface ITodoListPort
{
    public Task<string> Create(CreateTodoList useCase);
    public Task<TodoList?> Get(GetTodoList useCase);
    public Task<bool> HasList(string id);
}
