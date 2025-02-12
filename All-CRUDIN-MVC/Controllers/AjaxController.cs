using All_CRUDIN_MVC.Data;
using All_CRUDIN_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace All_CRUDIN_MVC.Controllers
{
    public class AjaxController : Controller
    {
        private readonly ApplicationDbContext db;
        private IWebHostEnvironment env;
        public AjaxController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllEmp()
        {
            var data = db.emp.ToList();
            return new JsonResult(data);
        }

        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(Emp e, IFormFile img)
        {
            string filepath = Path.Combine(env.WebRootPath , "Images",img.FileName);
            using (var stream = new FileStream(filepath, FileMode.Create))
            { 
                img.CopyTo(stream);
            }
            e.img = $"/Images/{img.FileName}";

            db.emp.Add(e);
            db.SaveChanges();
            TempData["ok"] = "Employee Adde Succesfully..";
            return RedirectToAction("Index");
        }

        public IActionResult delete(int id)
        { 
            var data = db.emp.Find(id);
            db.emp.Remove(data);
            db.SaveChanges();
            TempData["error"] = "Employee deleted succesfully...";
            return RedirectToAction("Index");
        }

        public IActionResult EditEmp(int id)
        {
            var data = db.emp.Find(id);

            return View(data);
        }


        [HttpPost]
        public IActionResult Edit(Emp e, IFormFile img)
        {
            var data = db.emp.Find(e.id);
            data.ename = e.ename;
            data.salary = e.salary;

            if (img != null)
            {
                string filepath = Path.Combine(env.WebRootPath, "Images", img.FileName);
                using (var s = new FileStream(filepath, FileMode.Create))
                {
                    img.CopyTo(s);
                }
                data.img = $"/Images/{img.FileName}";
            }

            //db.emp.Update(e);
            db.SaveChanges();
            TempData["update"] = "Emp Updated Successfully...";
            return RedirectToAction("Index");
        }
    }
}
