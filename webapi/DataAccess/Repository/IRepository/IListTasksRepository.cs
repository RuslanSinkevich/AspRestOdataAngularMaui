using webapi.Models;

namespace webapi.DataAccess.Repository.IRepository
{
    public interface IListTasksRepository : IRepository<TasksL>
    {
        void Update(TasksL obj);
    }
}
