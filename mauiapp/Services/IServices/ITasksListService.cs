using TodoREST.Models;

namespace TodoREST.Services.IServices
{
    public interface ITasksListService
    {
        Task<List<TasksList>> GetTasksListAsync();

        Task RefreshTasksListItemAsync(TasksList item);

        Task DeleteTasksListItemAsync(TasksList item);

    }
}