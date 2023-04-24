using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Tasks>? Tasks { get; set; }
        public DbSet<TasksL>? TasksList { get; set; }


    }
}
