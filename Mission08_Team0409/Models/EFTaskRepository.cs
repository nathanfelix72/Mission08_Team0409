namespace Mission08_Team0409.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private TaskContext _context;

        public EFTaskRepository(TaskContext context)
        {
            _context = context;
        }

        public List<TaskItem> Tasks => _context.Tasks.ToList();
        public List<Category> Categories => _context.Categories.ToList();
        
        public void AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskItem task)
        {
            _context.Tasks.Update(task); // Explicitly tell EF Core to track the change
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
