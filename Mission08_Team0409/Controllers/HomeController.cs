using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0409.Migrations;
using Mission08_Team0409.Models;
using static System.Net.Mime.MediaTypeNames;
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
            _repo.DeleteTask(deletedInfo);
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var task = _repo.Tasks.Single(x => x.TaskId == id);
            task.Completed = true;
            _repo.UpdateTask(task);
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _repo.Tasks
                .Single(x => x.TaskId == id);

            ViewBag.Category = _repo.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("TaskForm", recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem updatedInfo)
        {
            _repo.Edit(updatedInfo);
            _repo.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}