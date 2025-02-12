using All_CRUDIN_MVC.Models;

namespace All_CRUDIN_MVC.Repositories
{
    public interface IEmpRepo
    {
        List<Emp> GetAllEmp();

        Emp GetEmpById(int id);

        void AddEmp(Emp e,IFormFile img);

        void DeleteEmp(int id);

        void EditEmp(Emp e,IFormFile img);
    }
}
