using webapi.Models;
using webapi.DataAccess.Repository.IRepository;

namespace webapi.DataAccess.Repository
{
    public class ListTasksRepository : Repository<TasksL>, IListTasksRepository
    {
        private readonly ApplicationDbContext _db;
        public ListTasksRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TasksL obj)
        {
            _db.TasksList!.Update(obj);
        }
    }
}
