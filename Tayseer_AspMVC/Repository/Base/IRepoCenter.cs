using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Repository.Base
{
    public interface IRepoCenter : IRepository<Disability>
    {
        IEnumerable<DisabilityCenter> DisabilityCenter();

    }
}
