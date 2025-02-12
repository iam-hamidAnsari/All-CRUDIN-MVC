using All_CRUDIN_MVC.Models;
using All_CRUDIN_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace All_CRUDIN_MVC.Controllers
{
    public class RepoController : Controller
    {
        private readonly IEmpRepo repo;
        public RepoController(IEmpRepo repo) {this.repo = repo;}

        public IActionResult Index()
        {
            var data = repo.GetAllEmp();
            return View(data);
        }

        public IActionResult Addemp()
        { 
            return View(); 
        }

        [HttpPost]
        public IActionResult Addemp(Emp e,IFormFile img)
        {
            repo.AddEmp(e, img);
            TempData["ok"] = "Employee Added successfully..";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        { 
            var data = repo.GetEmpById(id);
            return View(data); 
        }

        [HttpPost]
        public IActionResult Edit(Emp e,IFormFile img)
        {
            repo.EditEmp(e, img);
            TempData["update"] = "EMployee Updated Successfulyy...";
            return RedirectToAction("Index");
        }

        public IActionResult delete(int id)
        {
            repo.DeleteEmp(id);
            TempData["error"] = "Employee deleted successfully..";
            return RedirectToAction("Index");
        }
    }
}
