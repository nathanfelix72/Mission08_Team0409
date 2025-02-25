namespace Mission08_Team0409.Models
{
    public interface ITaskRepository
    {
        List<Task> Tasks { get; }
        List<Category> Categories { get; }
        
        public void SaveChanges();

        public void AddTask(Task task);
    }
}
