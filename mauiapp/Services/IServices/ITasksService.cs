using TodoREST.Models;

namespace TodoREST.Services.IServices
{
    public interface ITasksService
    {
        Task<List<Tasks>> GetTasksAsync(int tasksListId);

        Task RefreshTasksItemAsync(Tasks item, bool isNewItem);

        Task DeleteTasksItemAsync(Tasks item);

    }
}