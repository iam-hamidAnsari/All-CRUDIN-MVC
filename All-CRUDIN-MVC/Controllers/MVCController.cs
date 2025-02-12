using All_CRUDIN_MVC.Data;
using All_CRUDIN_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace All_CRUDIN_MVC.Controllers
{
    public class MVCController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;
        public MVCController(ApplicationDbContext db , IWebHostEnvironment env) { this.db = db; this.env = env; }


        public IActionResult Index()
        {
            var data = db.emp.ToList();
            return View(data);
        }

        public IActionResult Addemp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Addemp(Emp e, IFormFile img)
        {
            string filepath = Path.Combine(env.WebRootPath , "Images",img.FileName);
            if (!Directory.Exists(Path.Combine(env.WebRootPath, "Images")))
            {
                Directory.CreateDirectory(Path.Combine(env.WebRootPath, "Images"));
            }
            using (var stream = new FileStream(filepath,FileMode.Create)) 
            { 
                img.CopyTo(stream);
            }
            e.img = $"/Images/{img.FileName}";
            db.Add(e);
            db.SaveChanges();
            TempData["ok"] = "Emp Added Successfully...";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var data = db.emp.Find(id);
            db.emp.Remove(data);
            db.SaveChanges();
            TempData["error"] = "Emp Deleted Successfully...";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        { 
            var data = db.emp.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Emp e , IFormFile img)
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
