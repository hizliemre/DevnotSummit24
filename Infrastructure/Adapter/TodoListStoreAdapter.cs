using Domain.Port;
using Domain.UseCase;

namespace Infrastructure.Adapter;

internal class TodoListStoreAdapter(Store store): ITodoListPort
{
    public Task<string> Create(CreateTodoList useCase)
    {
        TodoListEntity entity = new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = useCase.Name,
            Description = useCase.Description,
            CreatedAt = DateTime.Now
        };

        store.TodoListEntities.Add(entity);
        return Task.FromResult(entity.Id);
    }

    public Task<TodoList?> Get(GetTodoList useCase)
    {
        TodoListEntity entity = store.TodoListEntities.FirstOrDefault(x => x.Id == useCase.Id);
        if (entity == null)
        {
            return Task.FromResult<TodoList>(null);
        }

        return Task.FromResult(new TodoList
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        })!;
    }

    public Task<bool> HasList(string id)
    {
        return Task.FromResult(store.TodoListEntities.Any(x => x.Id == id));
    }
}
