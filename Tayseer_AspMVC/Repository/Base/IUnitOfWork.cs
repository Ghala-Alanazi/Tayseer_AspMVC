using System.Security;
using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Repository.Base
{
    public interface IUnitOfWork
    {

        IRepository<Disability> Disabilitys { get; }
        IRepository<Hospital> Hospitals { get; }

        IRoposHospital roposHospital { get; }
        IRepoSchool Schools { get; }
        IRepoEmployee Employees { get; }
        IRepository<Centers> Centers { get; }
        IRepository<UserRole> UserRoles { get; }

        void Save();
    }
}
