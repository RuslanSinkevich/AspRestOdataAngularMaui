using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using webapi.DataAccess;
using webapi.DataAccess.Repository.IRepository;

namespace webapi.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            DbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T Find(int id)
        {
            return DbSet.Find(id)!;
        }

        public T FirstOfDefault(Expression<Func<T, bool>>? filter = null!, string? includeProperties = null!, bool isTracking = true)
        {
            IQueryable<T> queer = DbSet;
            if (filter != null)
            {
                queer = queer.Where(filter);
            }

            if (includeProperties != null)
            {
                queer = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(queer, (current, includeProp) => current.Include(includeProp));
            }

            if (!isTracking)
            {
                queer = queer.AsNoTracking();
            }
            return queer.FirstOrDefault()!;

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, bool isTracking = true)
        {
            //System.Threading.Thread.Sleep(10000);
            IQueryable<T> queer = DbSet;
            if (filter != null)
            {
                queer = queer.Where(filter);
            }

            if (includeProperties != null)
            {
                queer = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(queer, (current, includeProp) => current.Include(includeProp));
            }

            if (orderBy != null)
            {
                queer = orderBy(queer);
            }

            if (!isTracking)
            {
                queer = queer.AsNoTracking();
            }

            return queer.ToList();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            foreach (var obj in entity)
            {
                DbSet.Remove(obj);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }


    }
}
