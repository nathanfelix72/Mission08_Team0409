using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0409.Models;
using Task = Mission08_Team0409.Models.Task;

namespace Mission08_Team0409.Controllers
{
    public class HomeController : Controller
    {
        private TaskContext _context;
        
        private ITaskRepository _repo;
        
        public HomeController(TaskContext context, ITaskRepository temp)
        {
            _context = context; // Initialize _context 
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
        public IActionResult AddTask()
        {
            //get list of categories from the db
            ViewBag.Categories = _repo.Categories.Select(c=>c.CategoryName).ToList();
            
            return View(new Task());
        }

        [HttpPost]
        public IActionResult AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                _repo.Tasks.Add(task); //add it to db
                _repo.SaveChanges();
                return RedirectToAction("Index"); //go back to index
            }
            else
            {
                return View(task);
            }
        }
    }
}
