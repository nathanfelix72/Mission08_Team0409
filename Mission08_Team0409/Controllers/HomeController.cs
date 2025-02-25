using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0409.Models;
using TaskItem = Mission08_Team0409.Models.TaskItem;

namespace Mission08_Team0409.Controllers
{
    public class HomeController : Controller
    {        
        private ITaskRepository _repo;
        
        public HomeController(ITaskRepository temp)
        {
            _repo = temp;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _repo.Tasks
                .Where(t => ! t.Completed)//get the uncompleted tasks
                .OrderBy(t => t.Date) //order by the due date
                .ToList();
            
            return View(tasks);
        }

        [HttpGet]
        public IActionResult TaskForm()
        {
            //get list of categories from the db
            ViewBag.Categories = _repo.Categories.Select(c=>c.CategoryName).ToList();
            
            return View(new TaskItem());
        }

        [HttpPost]
        public IActionResult TaskForm(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _repo.AddTask(task); //add it to db
                _repo.SaveChanges();
                return RedirectToAction("Index"); //go back to index
            }
            else
            {
                return View(task);
            }
        }

        [HttpGet]
        public IActionResult MarkComplete(int id)
        {
            var task = _repo.Tasks.FirstOrDefault(t => t.TaskId == id);
            return View(task);
        }

        [HttpPost]
        public IActionResult MarkComplete(TaskItem task)
        {
            if (task != null)
            {
                task.Completed = true; // Mark as completed
                _repo.UpdateTask(task);
                _repo.SaveChanges();
            }
            return RedirectToAction("Index"); // Return to task list
        }
    }
}
