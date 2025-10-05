using Tayseer_AspMVC.Migrations;
using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Repository.Base
{
    public interface IRepoEmployee : IRepository<Employee>
    {

        Employee Login(string username, string password);

        IEnumerable<Employee> FindAllEmployee();
    }
}
