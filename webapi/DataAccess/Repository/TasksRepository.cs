using webapi.Models;
using webapi.DataAccess.Repository.IRepository;

namespace webapi.DataAccess.Repository
{
    public class TasksRepository : Repository<Tasks>, ITasksRepository
    {
        private readonly ApplicationDbContext _db;
        public TasksRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Tasks obj)
        {
            _db.Tasks!.Update(obj);
        }
    }
}
