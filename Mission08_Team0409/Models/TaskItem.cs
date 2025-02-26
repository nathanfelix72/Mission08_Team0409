using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0409.Models
{
    public class TaskItem
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        [Required(ErrorMessage = "You need to enter the task")]
        public string TaskToDo { get; set; }
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "You need to enter a quadrant")]
        public int Quadrant { get; set; }
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool Completed { get; set; }
    }
}
