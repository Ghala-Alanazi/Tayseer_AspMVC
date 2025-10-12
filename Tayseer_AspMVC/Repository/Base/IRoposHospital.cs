using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Repository.Base
{
    public interface IReposHospital : IRepository<Hospital>
    {

        IEnumerable<DisabilityHospital> DisabilityHospital();

        Hospital FindByUIdHospital(string uid);





    }
}
