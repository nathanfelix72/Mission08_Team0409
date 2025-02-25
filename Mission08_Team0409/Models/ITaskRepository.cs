namespace Mission08_Team0409.Models
{
    public interface ITaskRepository
    {
        List<TaskItem> Tasks { get; }
        List<Category> Categories { get; }
        
        public void SaveChanges();

        public void AddTask(TaskItem task);
    }
}
