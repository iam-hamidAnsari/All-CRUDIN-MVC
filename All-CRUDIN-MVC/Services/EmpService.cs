using All_CRUDIN_MVC.Data;
using All_CRUDIN_MVC.Models;
using All_CRUDIN_MVC.Repositories;

namespace All_CRUDIN_MVC.Services
{
    public class EmpService : IEmpRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;
        public EmpService(ApplicationDbContext db,IWebHostEnvironment env) { this.db = db; this.env = env; }

        public void AddEmp(Emp e, IFormFile img)
        {
            string filepath = Path.Combine(env.WebRootPath,"Images",img.FileName);
            using (var stream = new FileStream(filepath,FileMode.Create))
            {
                img.CopyTo(stream);
            }

            e.img = $"Images/{img.FileName}";
            db.emp.Add(e);
            db.SaveChanges();
        }

        public void DeleteEmp(int id)
        {
            if (id != null)
            {
                var data = db.emp.Find(id);
                db.emp.Remove(data);
                db.SaveChanges();
            }
        }

        public void EditEmp(Emp e, IFormFile img)
        {
            var data = db.emp.Find(e.id);
            if (img != null)
            {
                string filepath = Path.Combine(env.WebRootPath,"Images",img.FileName);
                using (var stream = new FileStream(filepath,FileMode.Create))
                { 
                    img.CopyTo(stream);
                }
                data.img = $"/Images/{img.FileName}";
            }

            data.ename = e.ename;
            data.salary = e.salary;
            db.SaveChanges();
        }

        public List<Emp> GetAllEmp()
        {
            var data = db.emp.ToList();
            return data;
        }

        public Emp GetEmpById(int id)
        {
            var data = db.emp.Find(id);
            return data;
        }
    }
}
