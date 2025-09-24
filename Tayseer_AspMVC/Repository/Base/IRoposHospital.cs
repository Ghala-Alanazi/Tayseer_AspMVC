using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Repository.Base
{
    public interface IRoposHospital : IRepository<Hospital>
    {

        IEnumerable<DisabilityHospital> DisabilityHospital();


    }
}
