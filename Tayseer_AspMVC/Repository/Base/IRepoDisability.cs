using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Repository.Base
{
    public interface IRepoDisability : IRepository<Disability>
    {
        IEnumerable<Disability> Disability();
    }
}
