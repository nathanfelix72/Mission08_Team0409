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
        public IActionResult Delete(int id)
        {
            var recordToDelete = _repo.Tasks
                .Single(x => x.TaskId == id); // Go out and look for one record
            return View(recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(TaskItem deletedInfo)
        {
            _repo.Tasks.Remove(deletedInfo);
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
