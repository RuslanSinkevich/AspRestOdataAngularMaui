using webapi.Models;

namespace webapi.DataAccess.Repository.IRepository
{
    public interface ITasksRepository : IRepository<Tasks>
    {
        void Update(Tasks obj);
    }
}
