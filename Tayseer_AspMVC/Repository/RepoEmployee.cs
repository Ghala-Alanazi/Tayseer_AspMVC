using Tayseer_AspMVC.Data;
using Tayseer_AspMVC.Migrations;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Repository
{
    public class RepoEmployee : MainRepository<Employee>, IRepoEmployee
    {
        private readonly ApplicationDbContext _context;
        public RepoEmployee(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public Employee Login(string username, string password)
        //{
        //    var emp = _context.Employees.FirstOrDefault(e => e.Username == username && e.Password == password);
        //    return emp;
        //}


        public IEnumerable<Employee> FindAllEmployee()
        {
            return (IEnumerable<Employee>)_context.Employees.Where(e => !e.IsDelete).ToList();
        }


    }
}
