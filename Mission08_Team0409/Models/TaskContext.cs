using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0409.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {

        }

        public DbSet<Task> Tasks { get; set; }
    }
}
