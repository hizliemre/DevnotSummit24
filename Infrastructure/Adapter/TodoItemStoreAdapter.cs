using Domain.Port;
using Domain.UseCase;

namespace Infrastructure.Adapter;

internal class TodoItemStoreAdapter(Store store): ITodoItemPort
{
    public Task<string> Create(CreateTodoItem useCase)
    {
        TodoItemEntity entity = new()
        {
            Id = Guid.NewGuid().ToString(),
            ListId = useCase.ListId,
            Title = useCase.Title,
            Description = useCase.Description,
            IsDone = useCase.IsDone,
            StartDate = useCase.StartDate,
            EndDate = useCase.EndDate,
            CreatedAt = DateTime.Now
        };

        store.TodoItemEntities.Add(entity);
        return Task.FromResult(entity.Id);

    }

    public Task<TodoItem> Get(GetTodoItem useCase)
    {
        TodoItemEntity entity = store.TodoItemEntities.FirstOrDefault(x => x.Id == useCase.Id && x.ListId == useCase.ListId);
        if (entity is null)
        {
            return Task.FromResult<TodoItem?>(null)!;
        }

        return Task.FromResult(new TodoItem
        {
            Id = entity.Id,
            ListId = entity.ListId,
            Title = entity.Title,
            Description = entity.Description,
            IsDone = entity.IsDone,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        });
    }

    public Task<IEnumerable<TodoItem>> GetAll(GetAllTodoItems useCase)
    {
        IEnumerable<TodoItem> todoItems = store.TodoItemEntities
            .Where(x => x.ListId == useCase.ListId)
            .Select(x => new TodoItem
            {
                Id = x.Id,
                ListId = x.ListId,
                Title = x.Title,
                Description = x.Description,
                IsDone = x.IsDone,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            });

        return Task.FromResult(todoItems);
    }
}
